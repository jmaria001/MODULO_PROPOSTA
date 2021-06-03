angular.module('App').controller('FaturasPesquisaController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {

    //========================Verifica Permissão Cancelar NF
    $scope.PermissaoCancelaNF = false;
    httpService.Get("credential/FaturasPesquisa@Cancel").then(function (response) {
        $scope.PermissaoCancelaNF = response.data;
    });

    //====================Inicializa scopes
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.Filtro = {};
    $scope.Origem = [
                       { 'Origem': 0, 'Nome': 'Antecipado' },
                        { 'Origem': 1, 'Nome': 'Mídia' },
                        { 'Origem': 2, 'Nome': 'Midia Complementar' },
                        { 'Origem': 3, 'Nome': 'Outras Receitas' },
                        { 'Origem': 9, 'Nome': 'Todas' }];

    $scope.NewFiltro = function () {
        $scope.Filtro = {
            'Cod_Emp_Faturamento': '',
            'Numero_Negociacao': '',
            'Competencia': '',
            'Nota_Fiscal': '',
            'Cod_Empresa': '',
            'Contrato': '',
            'Sequencia': '',
            'Cod_Agencia': '',
            'Cod_Cliente': '',
            'Origem': 9,
            'Numero_Erp': ''
        };
        localStorage.removeItem('Pesquisa_Fatura_Filter');
    }
    //======================Verifica se tem filtro anterior
    var _Filter = JSON.parse(localStorage.getItem('Pesquisa_Fatura_Filter'));
    if (!_Filter) {
        $scope.NewFiltro()
    }


    $scope.LimpaCancel = function (pParam) {
        $scope.ShowCancela = false;
        $scope._AbriuCampos = false;
        pParam.Cod_Cancelamento = '';
        pParam.Motivo_Cancelamento = '';
        pParam.Reemissao = false;
        pParam.Obs_Cancelamento = '';
    }


    $scope.ShowFilter = true;
    $scope.ShowGrid = false;
    $scope.ShowDados = false;
    $scope.ShowCancela = false;
    var _AbriuCampos = false;

    //========================Parametros do Grid
    
    $scope.gridheaders = [/*{ 'title': '', 'visible': true, 'searchable': false, 'sortable': false },*/ //- se nao tem a coluna no grid nao pode ter no config
        { 'title': 'Fatura', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Negociação', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Origem', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Emp.Fat', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Contrato', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Seq.', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Produto', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Agência', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Cliente', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Vlr.Bruto', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Vlr.Comis.', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Vlr.Pago', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Vlr.Liqui.', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Nat.Serviço', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Cancelada', 'visible': true, 'searchable': true, 'sortable': true },
    ];

    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        }, 10)
    };

    //====================Carrega Contrato            
    $scope.CarregaFaturas = function (pFiltro) {
        if (pFiltro.Cod_Emp_Faturamento == "") {
            ShowAlert("Empresa de Faturamento é de seleção obrigatória!");
            return;
        }
        $rootScope.routeloading = true;
        $scope.Faturas = [];
        $('#dataTable').dataTable().fnDestroy();
        httpService.Post('faturaslistar',pFiltro).then(function (response) {
            if (response) {
                $scope.Faturas = response.data;
                if ($scope.Faturas.length == 0) {
                    ShowAlert("Não existe dados cadastrado p/ este Filtro");
                    $scope.RepeatFinished();
                }
                else {
                    $scope.ShowFilter = false;
                    $scope.ShowGrid = true;
                }
            }
            localStorage.setItem('Pesquisa_Fatura_Filter', JSON.stringify($scope.Filtro));
        });
    };

    //====================Configuracao do Grid
    $scope.ConfiguraGrid = function () {
        param = {};
        param.language = fnDataTableLanguage();
        param.lengthMenu = [[7, 10, 25, 50, -1], [7, 10, 25, 50, "Todos"]];
        param.scrollCollapse = true;
        param.paging = true;
        param.dom = "<'row'<'col-sm-6'B><'col-sm-3'l><'col-sm-3'f>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        param.buttons = [
            {
                text: 'Abrir no Excel<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-primary', extend: 'excel', exportOptions: {
                    columns: ':visible:not(:first-child)'
                }
            },
            { text: 'Retornar', className: 'btn btn-warning', action: function (e, dt, button, config) { $('#btnShowFiltro').click(); } },
        ];
        param.order = [[0, 'asc']];
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
    //===========================Carrega Dados da Fatura
    $scope.ShowFatura = function (pFatura) {
        $scope.ShowDados = true;
        $scope.ShowFilter = false;
        $scope.ShowGrid = false;
        httpService.Post('FaturaGet', pFatura).then(function (response) {
            if (response) {
                $scope.FaturaDados = response.data;
                if ($scope.FaturaDados.Cod_Cancelamento != "") {
                    $scope.ShowCancela = true;
                }
                else {
                    $scope._AbriuCampos = false;
                };
            };
        });
    };




    //===========================Abre Campos
    $scope.AbreCampos = function () {
        $scope.ShowCancela = true;
        $scope._AbriuCampos = true;
    }
    


    //===========================Cancela Fatura
    $scope.CancelaFatura = function (pFaturaDados) {
        //console.log(pFaturaDados);
        if (pFaturaDados.Cod_Cancelamento == "") {
            ShowAlert("Motivo do Cancelamento é obrigatório!");
            return;
        }


        httpService.Post('FaturaCancelar', pFaturaDados).then(function (response) {
            if (response) {
                if (response.data[0].Status) {
                    for (var x = 0; x < $scope.Faturas.length; x++) {
                        $scope.FaturaDados.Indica_Cancelado = true;
                        if ($scope.Faturas[x].Numero_Fatura == pFaturaDados.Numero_Fatura) {
                            $scope.Faturas[x].Data_Cancelamento = new Date();
                            break;
                        };
                    };
                }
                else {
                    ShowAlert(response.data[0].Retorno, 'warning');
                };
            };
            $scope._AbriuCampos = false;
        });
    };            



    

    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });

    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.ConfiguraGrid();
        if (_Filter) {
            $scope.Filtro = _Filter;
        }
    });

}]);

