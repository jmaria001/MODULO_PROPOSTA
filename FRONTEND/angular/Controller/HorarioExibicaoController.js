angular.module('App').controller('HorarioExibicaoController', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location) {


    $scope.ShowFiltro = true;
    $scope.HorarioExibicaoS = "";

    var Cod_Qualidade_Ant = "";
    var Horario_Exibicao_Ant = "";
    var bCritica = false;
    var vCod_Veiculo = "";
    var vHorario_Valido = false;
    

    //====================Inicializa scopes
    
    $scope.CurrentShow = "Filtro";
    $scope.ListadeVeiculos = [];

    $scope.gridheaders = [{ 'title': 'Cod.Veículo', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Dt.Exibição', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Cod.Programa', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Título', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Início Previsto', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Final Previsto', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Início Real', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Final Real', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Critica', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    ];
    //====================Inicializa o Filtro
    $scope.Filtro = {};
    $scope.NewFiltro = function () {
        //localStorage.removeItem('BaixaVeiculacoes');
        return {
            'Cod_Veiculo': '',
            'Data_Exibicao': ''
        }

    }
    //===========================Se ja tiver filtro anterior gravado
    var _Filter = JSON.parse(localStorage.getItem('HorarioExibicao'));

    if (_Filter) {
        $scope.Filtro = _Filter;
    }
    else {
        $scope.Filtro = $scope.NewFiltro();

    }


    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.CurrentShow = 'Grid';
        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        }, 1000)
    };

    //====================Carrega o Grid

    $scope.CarregarHorarioExibicao = function (pFiltro) {
        if (pFiltro.Cod_Veiculo == "") {
            ShowAlert("Veículo não pode ficar em branco.");
            return;
        }
        if (pFiltro.Data_Exibicao == "") {
            ShowAlert("Data de Exibição não pode ficar em branco.");
            return;
        }

        if (pFiltro.Cod_Veiculo == "" && pFiltro.Data_Exibicao == "") {
            ShowAlert("Nenhum Filtro foi informado.");
            return;
        }

        $scope.HorarioExibicaoS = [];
        $scope.CurrentShow = '';
        $('#dataTable').dataTable().fnDestroy();
        var _url = 'HorarioExibicaoListar';
        _url += '?cod_Veiculo=' + pFiltro.Cod_Veiculo;
        _url += '&Data_Exibicao=' + pFiltro.Data_Exibicao;
        _url += '&';

        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.HorarioExibicaoS = response.data;
                $scope.HorarioExibicaoS.Veiculos = [];
                if ($scope.HorarioExibicaoS.length == 0) {
                    $scope.RepeatFinished();
                }
            }
        });
    };

    //====================Configuracao do Grid

    $scope.ConfiguraGrid = function () {

        param = {};
        param.language = fnDataTableLanguage();
        param.lengthMenu = [[7, 10, 25, 50, -1], [7, 10, 25, 50, "Todos"]];

        //param.scrollCollapse = true;
        param.paging = true;
        param.dom = "<'row'<'col-sm-6'B><'col-sm-3'l><'col-sm-3'f>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
            param.buttons = [
                //{ text: 'Novo Mapa Reserva', className: 'btn btn-primary btnNew', action: function (e, dt, button, config) { $('#btnNovoMapaReservaCompensacao').click(); } },
                {
                    text: 'Abrir no Excel<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning', extend: 'excel', exportOptions: {
                        columns: ':visible:not(:first-child)'
                    }
                },
                { text: 'Novo Filtro' + '<span class="fa fa-filter margin-left-10"></span>', className: 'btn btn-info', action: function (e, dt, button, config) { $('#btnNovoFiltro').click(); } },


            ];

        param.order = [[0, 'desc']];
        param.autoWidth = false;
        param.columns = [];
        for (var i = 0; i < $scope.gridheaders.length; i++) {
            param.columns.push({ "visible": $scope.gridheaders[i].visible, "searchable": $scope.gridheaders[i].searchable, "sortable": $scope.gridheaders[i].sortable });
        }
        $('#dataTable').DataTable(param);
        var table = $('#dataTable').DataTable();
        var buttons = table.buttons([0]);
        if (table.rows({ selected: true }).indexes().length === 0) {
            buttons.disable();
        }
        else {
            buttons.enable();
        }
    };

    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });

    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();

        if (_Filter) {
            var _Carregar = false
            angular.forEach(_Filter, function (value, key) {
                if (value) {
                    _Carregar = true;
                }
            });
            if (_Carregar) {
                $scope.CarregarHorarioExibicao(_Filter);
            }
        }
    });

    //==========================Salvar
    $scope.SalvarHorarioExibicao = function (pHorarioExibicao,pVeiculos) {
        vHorario_Valido = false;



        var _data = {
            'HorarioExibicao': pHorarioExibicao,
            'Veiculos': pVeiculos
        };
        console.log(_data)
        httpService.Post("SalvarHorarioExibicao", _data).then(function (response) {
            if (response) {
                $scope.HorarioExibicaoS = response.data.HorarioExibicao;

            }
    

        })
    };

    //=====================Validar hora 


    $scope.ValidarHorarioExibicao = function (pHorarioExibicao) {
      
        var hhr = pHorarioExibicao;
        hrs = (hhr.substring(0, 2));
        min = (hhr.substring(3, 5));
        if ((hrs < 00) || (hrs > 23) || (min < 00) || (min > 59)) {
            vHorario_Valido = true;
            return;
        }

    }

 

    $scope.SelecionarVeiculos = function () {

        $scope.PesquisaTabelas = NewPesquisaTabela();
        var _url = 'ListarTabela/Veiculo';
        httpService.Get(_url).then(function (response) {
            if (response.data) {
                $scope.ListadeVeiculos = response.data;

                $scope.PesquisaTabelas.Items = $scope.ListadeVeiculos;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção de Veiculos";
                $scope.PesquisaTabelas.MultiSelect = true;
                $scope.PesquisaTabelas.ClickCallBack = function () {

                };
                $("#modalTabela").modal(true);
            };
        });
    }
    //=====================Clicou no X da lista de Veiculos selecionados- remover Veiculos
    $scope.RemoverVeiculos = function (pVeiculo) {
        pVeiculo.Selected = false;
    }

}]);


