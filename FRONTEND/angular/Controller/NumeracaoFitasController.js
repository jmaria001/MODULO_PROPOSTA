angular.module('App').controller('NumeracaoFitasController', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location) {


    //====================Inicializa scopes

    $scope.CurrentShow = "Filtro";

    $scope.gridheaders = [{ 'title': '', 'visible': true, 'searchable': false, 'config': false, 'sortable': false },
        { 'title': 'Contrato', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Tipo', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Tit.Comercial', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Produto', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Dur.', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'N.Fita', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Veículo', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Status', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Agencia', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Cliente', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Inicio.Prog', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Térm.Prog', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Apresentador', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Localização', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    ];
    //====================Inicializa o Filtro
    $scope.Filtro = {};
    $scope.NewFiltro = function () {
        localStorage.removeItem('NumeracaoFitas_filter');
        return {
            'Cod_Veiculo': '',
            'Cod_Programa': '',
            'Data_Inicio': '',
            'Data_Final': '',
            'Numero_Fita_Inicio': '',
            'Numero_Fita_Fim': '',
            'Indica_Pendentes_Numeracao': true,
            'Indica_Numeradas': true,
            'Indica_Desativadas_Devolvidas': true,
            'Indica_Ativas': true,
        }
    }
    //===========================Se ja tiver filtro anterior gravado
    var _Filter = JSON.parse(localStorage.getItem('NumeracaoFitas_filter'));
    if (_Filter) {
        $scope.Filtro = _Filter;
    }
    else {
        $scope.Filtro = $scope.NewFiltro();
    };
    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.CurrentShow = 'Grid';
    };
    //====================Carrega o Grid
    $scope.CarregarFitas = function (pFiltro) {
        
        if (!pFiltro.Cod_Veiculo) {
            ShowAlert("Codigo veículo é um filtro obrigatório");
            return;
        }
        localStorage.setItem('NumeracaoFitas_filter', JSON.stringify($scope.Filtro));
        $scope.CurrentShow = '';
        $('#dataTable').dataTable().fnDestroy();
        $scope.Fitas = "";
        httpService.Post("NumeracaoFitasListar",pFiltro).then(function (response) {
            if (response) {
                $scope.Fitas = response.data;
                if ($scope.Fitas.length == 0) {
                    $scope.RepeatFinished();
                }
            }
        });
    };
    //====================Carrega o Grid para Numeracao
    $scope.CarregarNumeracao = function (pFita) {
        $rootScope.routeloading = true;
        $scope.NumeracaoFitas = "";
        httpService.Post("ExibirVeiculosFitas",pFita).then(function (response) {
            if (response) {
                $scope.NumeracaoFitas = response.data;
                $scope.CurrentShow = 'Dados';
            }
        });
    };
    //====================Formata o Numero da Fita
    $scope.FormataNumeroFita = function (pNumeroFita) {
        if (pNumeroFita.Numero_Fita) {
            pNumeroFita.Numero_Fita = pNumeroFita.Numero_Fita.replace(/[^0-9]/g, '')
            pNumeroFita.Numero_Fita = '000000' + pNumeroFita.Numero_Fita;
            pNumeroFita.Numero_Fita = pNumeroFita.Numero_Fita.slice(pNumeroFita.Numero_Fita.length - 6);
            pNumeroFita.Numero_Fita = 'CO' + pNumeroFita.Numero_Fita;
        };
    };
    //=====================BuscarNumero Numero de fita vago
    $scope.BuscarNumero= function (pNumero_Fita) {
        var vCod_Veiculo = pNumero_Fita.Cod_Veiculo;
        var tipo_fita = 'CO';
        var _data = {
            'Cod_Veiculo': pNumero_Fita.Cod_Veiculo,
            'Tipo_Fita': tipo_fita,
            'Cod_Tipo_Midia': pNumero_Fita.Cod_Tipo_Midia,
            'Cod_Tipo_Comercial': pNumero_Fita.Cod_Tipo_Comercial
        };
        httpService.Post("RangeFitaNumeracao", _data).then(function (response) {
            if (response) {
                if (response.data[0].Status == 0) {
                    ShowAlert('Veículo não esta parametrizado corretamente em Paramêtros de Numeração de Fitas');
                    return;
                }
                else {
                    pNumero_Fita.Numero_Fita = response.data[0].Numero_Fita;
                };
            };
        });
    };
    //=======================Validacao de Apresentadores
    $scope.ValidarApresentador = function (pParam) {

        var _url = 'NumeracaoFitasValidarApresentador'
        _url += "?Cod_Apresentador=" + pParam.Cod_Apresentador;
        _url += "&";
        httpService.Get(_url).then(function (response) {
            if (response.data.length == 0) {
                pParam.Cod_Programa = "";
                ShowAlert("Apresentador não cadastrado");
                pParam.Cod_Apresentador = "";
                pParam.Nome_Apresentador = "";
            }
        });
    };
    //=======================Selecao de Apresentadores
    $scope.PesquisaApresentador = function (pParam) {
        var vCod_Veiculo = pParam.Cod_Veiculo;
        $scope.PesquisaTabelas = NewPesquisaTabela();
        $scope.listaApresentador = ""
        var _url = 'NumeracaoFitasApresentador'
        _url += "?Cod_Apresentador="
        _url += "&";
        httpService.Get(_url).then(function (response) {
            $scope.PesquisaTabelas.Items = response.data;
            $scope.PesquisaTabelas.FiltroTexto = ""
            $scope.PesquisaTabelas.PreFilter = false;
            $scope.PesquisaTabelas.Titulo = "Seleção de Apresentador"
            $scope.PesquisaTabelas.MultiSelect = false;
            $scope.PesquisaTabelas.ClickCallBack = function (value) {
                pParam.Cod_Apresentador = value.Codigo;
                param.Nome_Apresentador = value.Descricao;
            },
                $("#modalTabela").modal(true);
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
                {
                    text: 'Abrir no Excel<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning', extend: 'excel', exportOptions: {
                        columns: ':visible:not(:first-child)'
                    }
                },
                { text: 'Novo Filtro' + '<span class="fa fa-filter margin-left-10"></span>', className: 'btn btn-info', action: function (e, dt, button, config) { $('#btnNovoFiltro').click(); } },
            ];
        param.order = [[1, 'asc']];
        ////param.autoWidth = false;
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
    //======================= Gravar as fitas numeradas
    $scope.SalvarFitasNumeradas = function (pParam) {
        httpService.Post("SalvarNumeracaoFitas", pParam).then(function (response) {
            if (response.data) {
                $scope.NumeracaoFitas = response.data;
                for (var i = 0; i < $scope.NumeracaoFitas.length; i++) {
                    if ($scope.NumeracaoFitas[i].Status == 1) {
                        for (var x = 0; x < $scope.Fitas.length; x++) {
                            if ($scope.Fitas[x].Id_Numeracao == $scope.NumeracaoFitas[i].Id_Numeracao && $scope.Fitas[x].Cod_Veiculo == $scope.NumeracaoFitas[i].Cod_Veiculo) {
                                $scope.Fitas[x].Numero_Fita = $scope.NumeracaoFitas[i].Numero_Fita;
                                $scope.Fitas[x].Nome_Apresentador = $scope.NumeracaoFitas[i].Nome_Apresentador;
                            };
                        };
                    };
                };
            };
        });
    };
    //===========================Excluir Numero da Fita
    $scope.ExcluirNumeracao= function (pFita) {
        swal({
            title: "Confirma a Exclusão da Fita " + pFita.Numero_Fita + '?',   
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Não,Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("ExcluirNumeracaoFitas", pFita).then(function (response) {
                if (response) {
                    pFita.Numero_Fita = "";
                    pFita.Cod_Apresentador = "";
                };
            });
        });
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
                $scope.CarregarFitas(_Filter);
            }
        }
    });

}]);


