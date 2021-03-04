angular.module('App').controller('BaixaVeiculacoesController', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location) {

    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;
    //$scope.PermissaoHorario = true;


    httpService.Get("credential/BaixaVeiculacoes@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/BaixaVeiculacoes@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });

    $scope.ShowFiltro = true;
    $scope.BaixaVeiculacoesS = "";

    var Cod_Qualidade_Ant = "";
    var Horario_Exibicao_Ant = "";
    var bCritica = false;
    var vCod_Veiculo = "";


    //====================Inicializa scopes

    $scope.CurrentShow = "Filtro";


    $scope.gridheaders = [{ 'title': '', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Veiculo', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Data Exib.', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Programa', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Chave', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Qual.', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Horário', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Docto.De', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Docto.Para', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Contrato', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'T.Midia', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Titulo', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Tipo', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Dur.', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Net', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Mensagem', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    ];
    //====================Inicializa o Filtro
    $scope.Filtro = {};
    $scope.NewFiltro = function () {
        //localStorage.removeItem('BaixaVeiculacoes');
        return {
            'Cod_Veiculo': '',
            'Data_Exibicao': '',
            'Cod_Programa': '',
            'Chave_Acesso': '',
            'Numero_Mr': '',
            'Sequencia_Mr': '',
            'Cod_Empresa': '',
            'Cod_Comercial': '',
            'Duracao': '',
            'Indica_Net': ''
        }

    }
    //===========================Se ja tiver filtro anterior gravado
    var _Filter = JSON.parse(localStorage.getItem('BaixaVeiculacoes'));

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

    $scope.CarregarBaixaVeiculacoes = function (pFiltro) {
        if (pFiltro.Cod_Veiculo == "") {
            ShowAlert("Veículo não pode ficar em branco.");
            return;
        }
        if (pFiltro.Data_Exibicao == "") {
            ShowAlert("Data de Exibição não pode ficar em branco.");
            return;
        }

        if (pFiltro.Duracao == "" && pFiltro.Cod_Comercial == "" && pFiltro.Cod_Empresa == "" &&
            pFiltro.Numero_Mr == "" && pFiltro.Sequencia_Mr == "" && pFiltro.Cod_Veiculo == "" && pFiltro.Data_Exibicao == "" && pFiltro.Cod_Programa == "") {
            ShowAlert("Nenhum Filtro foi informado.");
            return;
        }


        $scope.BaixaVeiculacoesS = [];
        //localStorage.setItem('BaixaVeiculacoes', JSON.stringify($scope.Filtro));
        $scope.CurrentShow = '';
        $('#dataTable').dataTable().fnDestroy();
        var _url = 'BaixaVeiculacoesListar';
        _url += '?cod_Veiculo=' + pFiltro.Cod_Veiculo;
        _url += '&Data_Exibicao=' + pFiltro.Data_Exibicao;
        _url += '&Cod_Programa=' + pFiltro.Cod_Programa;
        _url += '&Chave_Acesso=' + pFiltro.Chave_Acesso;
        _url += '&Numero_Mr=' + pFiltro.Numero_Mr;
        _url += '&Sequencia_Mr=' + pFiltro.Sequencia_Mr;
        _url += '&Cod_Empresa=' + pFiltro.Cod_Empresa;
        _url += '&Cod_Comercial=' + pFiltro.Cod_Comercial;
        _url += '&Duracao=' + pFiltro.Duracao;
        _url += '&';

        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.BaixaVeiculacoesS = response.data;
                if ($scope.BaixaVeiculacoesS.length == 0) {
                    $scope.RepeatFinished();
                }

                vCod_Veiculo = pFiltro.Cod_Veiculo;
                // $scope.ShowFiltro = false;

                for (var i = 0; i < $scope.BaixaVeiculacoesS.length; i++) {
                    if (response.data[i].Indica_Net == 0) {

                        $scope.BaixaVeiculacoesS[i].Indica_Net = "";
                    }
                }
            }
        });
    };

    //==================== Nova Fita
    //$scope.NovaMateriaisFitas = function () {
    //    $location.path("/MateriaisFitasCadastro/New/0")
    //}
    //==================== Edicao da fita
    //$scope.NovaMateriaisFitas = function (pIdFita) {
    //    $location.path("/MateriaisFitasCadastro/Edit/" + pIdMateriaisFitas)
    //}
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
                $scope.CarregarBaixaVeiculacoes(_Filter);
            }
        }
    });



    //===========================Validar Código de Qualidade
    $scope.ValidarQualidade = function (pCodQualidade) {
        if (pCodQualidade.Cod_Qualidade == "") {
            return;
        }


        if (pCodQualidade.Cod_Qualidade == pCodQualidade.Cod_Qualidade_Ant) {
            return;
        }

        pCodQualidade.Flag_Tipo = 1

        httpService.Post('BaixaVeiculacoes/ValidarQualidade', pCodQualidade).then(function (response) {
            if (response.data) {
                if (response.data[0].Status == 0) {
                    ShowAlert(response.data[0].Mensagem)
                    pCodQualidade.Cod_Qualidade = pCodQualidade.Cod_Qualidade_Ant;
                    pCodQualidade.Horario_Exibicao = pCodQualidade.Horario_Exibicao_Ant;

                    return;
                }
                if (response.data[0].Critica == 1) {
                    pCodQualidade.Cod_Qualidade = pCodQualidade.Cod_Qualidade_Ant
                    return;
                }
            }
        });
    };



    //===========================Baixa de Veiculações 
    $scope.DaBaixaVeiculacoes = function (pBaixaVeiculacao) {
        //pBaixaVeiculacao.Cod_Qualidade_Ant = Cod_Qualidade_Ant;
        pBaixaVeiculacao.Cod_Veiculo = vCod_Veiculo;
        //console.log(pBaixaVeiculacao);
        swal({
            title: "Deseja baixar as veiculações ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Baixar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {

            httpService.Post("BaixaVeiculacoes/DaBaixaVeiculaçoes", pBaixaVeiculacao).then(function (response) {
                if (response) {
                    if (response.message) {
                        ShowAlert(response.message);
                        return;
                    }
                    $scope.BaixaVeiculacoesS = response.data;
                    if (response.data[0].Critica) {
                        ShowAlert("Houve critica em alguma linha da baixa. Verifique.");
                        return;
                    };
                    if (response.data[0].Qtd_Baixados == 0) {
                        ShowAlert("Nenhuma veiculação foi baixada. Verifique.");
                        return;
                    }
                    else {
                        ShowAlert("Baixa concluida com Sucesso.");
                    };
                };
            });
        });
    };

    //===============Clicou na lupa de qualidade
    $scope.PesquisaQualidade = function (pQualidade) {7
        $scope.PesquisaTabelas = NewPesquisaTabela();
        httpService.Get('ListarTabela/Qualidade').then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção de Qualidade";
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pQualidade.Cod_Qualidade = value.Codigo; pQualidade.Descricao = value.Descricao };
                $("#modalTabela").modal(true);
            }
        });
    }


    //===========================CancelaRoteiro
    $scope.CancelarBaixa = function () {
        $scope.BaixaVeiculacoesS = "";
        $scope.Filtro = $scope.NewFiltro();
        $scope.ShowFiltro = true;

    };



}]);


