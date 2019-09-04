angular.module('App').controller('VeiculoController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {
    //===================Declarar scopes
    $scope.Veiculos = "";
    $scope.showGrid = false;
    $scope.gridheaders = [{ 'title': 'Código', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Nome', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Cidade', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Sigla', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Empresa', 'visible': true, 'searchable': true, 'sortable': true },
    ];
    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.showGrid = 'true';
        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        }, 10)
    };
    //===================Carregar o grid
    $scope.CarregarVeiculo = function () {
        $rootScope.routeloading = true;
        $scope.Veiculos = [];
        $scope.showGrid = '';
        $('#dataTable').dataTable().fnDestroy();
        httpService.Get("VeiculoListar").then(function (response) {
            if (response) {
                $scope.Veiculos = response.data;
                console.log($scope.Veiculos);
            }
            if ($scope.Veiculos.length == 0) {
                $scope.RepeatFinished();
            }
        });
    }
    //==========================================Configura Grid
    $scope.ConfiguraGrid = function () {
        param = {};
        param.language = fnDataTableLanguage();
        param.lengthMenu = [[7, 10, 25, 50, -1], [7, 10, 25, 50, "Todos"]];
        param.scrollCollapse = true;
        param.paging = true;

        param.dom = "<'row'<'col-sm-3'l><'col-sm-4'f><'col-sm-5'B>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        param.buttons = [
            //{
            //    text: 'Novo Usuário<span class="fa fa-file margin-left-10"></span>', className: 'btn btn-primary btnNew', action: function (e, dt, button, config) { $('#btnNovoUsuarioUsuario').click(); }
            //},
            {
                text: 'Exportar<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning HideButton', extend: 'excel', exportOptions: {
                    columns: ':visible:not(:first-child)'
                }
            }
        ];
        param.order = [[0, 'asc']];
        param.autoWidth = false;

        param.columns = [];
        for (var i = 0; i < $scope.gridheaders.length; i++) {
            param.columns.push({ "visible": $scope.gridheaders[i].visible, "searchable": $scope.gridheaders[i].searchable, "sortable": $scope.gridheaders[i].sortable });
        }
        $('#dataTable').DataTable(param);
    };
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.ConfiguraGrid();
        $scope.CarregarVeiculo();
    });
    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });

    $scope.salvar = function () {
        $scope.showbotao = true;
    }
}]);

