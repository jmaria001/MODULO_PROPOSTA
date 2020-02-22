angular.module('App').controller('QualidadeController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {

    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;
    $scope.PermissaoDesativar = false;
    $scope.PermissaoExcluir = false;
    httpService.Get("credential/Qualidade@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/Qualidade@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });
    httpService.Get("credential/Qualidade@Destroy").then(function (response) {
        $scope.PermissaoExcluir = response.data;
    });
    httpService.Get("credential/Qualidade@Activate").then(function (response) {
        $scope.PermissaoDesativar = response.data;
    });
    //====================Inicializa scopes
    $scope.ShowGrid = false;
    $scope.gridheaders = [{ 'title': '', 'visible': true, 'searchable': false, 'sortable': false },
    { 'title': 'Código', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Descrição', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Motivo Falha', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Horário', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Cálculo', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'CE', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Roteiro', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'AM', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Demanda', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'CCW', 'visible': true, 'searchable': true, 'sortable': true }
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

    //====================Carrega o Grid

    $scope.CarregarQualidade = function () {
        $rootScope.routeloading = true;
        $scope.Qualidades = [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();
        httpService.Get('QualidadeListar').then(function (response) {
            if (response) {
                $scope.Qualidades = response.data;
                if ($scope.Qualidades.length == 0) {
                    $scope.RepeatFinished();
                }
            }
        });
    };
    //====================Funcao para configurar o Grid
    $scope.ConfiguraGrid = function () {
        param = {};
        param.language = fnDataTableLanguage();
        param.lengthMenu = [[7, 10, 25, 50, -1], [7, 10, 25, 50, "Todos"]];
        param.pageLength = 7;
        param.scrollCollapse = true;
        param.paging = true;

        param.dom = "<'row'<'col-sm-3'l><'col-sm-4'f><'col-sm-5'B>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
            param.buttons = [
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
    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.ConfiguraGrid();
        $scope.CarregarQualidade();
    });
    $scope.EditarQualidade

}]);

