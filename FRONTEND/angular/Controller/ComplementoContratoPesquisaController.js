angular.module('App').controller('ComplementoContratoPesquisaController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {


    //====================Inicializa scopes
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.Filtro = {};
    $scope.currentRateio = 0;
    $scope.ComplementoDados = "";
    $scope.NewFiltro = function () {
        $scope.Filtro = { 'Negociacao': '', 'Cod_Empresa_Faturamento': '', 'Contrato': '', 'Sequencia': '', 'Agencia': '', 'Cliente': '','Indica_Somente_Pendente':false };
        localStorage.removeItem('ComplementosPesquisa_Filter');
    }
    //========================Verifica Permissoes
    $scope.PermissaoExclusao = false;
    httpService.Get("credential/Complemento@Destroy").then(function (response) {
        $scope.PermissaoExclusao = response.data;
    });
    //======================Verifica se tem filtro anterior
    var _Filter = JSON.parse(localStorage.getItem('ComplementosPesquisa_Filter'));
    if (!_Filter) {

        $scope.NewFiltro()
    }

    $scope.ShowGrid = false;
    $scope.ShowFilter = true;
    $scope.ShowDados = false;
    //========================Parametros do Grid
    $scope.ShowFilter = true;
    $scope.gridheaders = [{ 'Complemento': '', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Origem', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Negociação', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Emp.', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'N.Fatura.', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Contrato', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Agência', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Cliente', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Valor', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Produto', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Excluir', 'visible': true, 'searchable': true, 'sortable': false},
    ];

    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.ShowGrid = true;
        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        }, 10)
    };
    var intCompetencia = null;
    //====================Carrega Contrato            
    $scope.CarregaComplementos = function (pFiltro) {
        if (!pFiltro.Cod_Empresa_Faturamento) {
            ShowAlert("Filtro Empresa de Faturamento é obrigatório.")
            return
        }
        $rootScope.routeloading = true;
        $scope.Complementos = [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();
        $scope.ShowFilter = false;
        httpService.Post('ComplementosPesquisar',pFiltro).then(function (response) {
            if (response) {
                $scope.Complementos = response.data;
                if ($scope.Complementos.length == 0) {
                    $scope.RepeatFinished();
                }
            }
            localStorage.setItem('ComplementosPesquisa_Filter', JSON.stringify($scope.Filtro));
        });
    };
    //====================Carrega Dados de um Complemento
    $scope.CarregaComplemento = function (pNumeroComplemento) {
        $scope.currentRateio = 0;
        httpService.Get('ComplementosGet/'+ pNumeroComplemento).then(function (response) {
            if (response) {
                $scope.ComplementoDados = response.data;
                $scope.ShowFilter = false;
                $scope.ShowGrid = false;
                $scope.ShowDados = true;
            }
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
    //===========================Remover Complemento
    $scope.ExcluirComplemento = function (pComplemento) {
        swal({
            title: "Confirma a exclusão desse Complemento ?" ,
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir",
            cancelButtonText: "Não,Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("ExcluirComplemento", pComplemento).then(function (response) {
                if (response) {
                    if (response.data[0].Status == 1) {
                        $scope.CarregaComplementos($scope.Filtro);
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem);
                    }
                };
            });
        });
    };
    //===========================Seta a tab do rateio que foi clicado
    $scope.SetCurrentRateio = function (pNumeroRateio) {
        for (var i = 0; i < $scope.ComplementoDados.Rateios.length; i++) {
            if ($scope.ComplementoDados.Rateios[i].Numero_Rateio==pNumeroRateio) {
                $scope.currentRateio = i;
                break;
            };
        };
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
            //$scope.RateioImportar($scope.Filtro);
        }
    });

}]);

