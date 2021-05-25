angular.module('App').controller('PrevisaoVendasAgenciaController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout','$routeParams', function ($scope, $rootScope, httpService, $location, $timeout,$routeParams) {


    //====================Recebe Parametros
    $scope.Parameters = $routeParams;

    //====================Inicializa scopes
    $scope.Previsao = [];    
    $scope.gridheaders = [{ 'title': 'Agência', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Cliente', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Janeiro', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Fevereiro', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Março', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Abril', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Maio', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Junho', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Julho', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Agosto', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Setembro', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Outubro', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Novembro', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Dezembro', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Total', 'visible': true, 'searchable': true, 'sortable': true },
    ];

    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        }, 10)
    };

    //====================Carrega o Grid

    $scope.CarregarPrevisaoAgencia = function (pFiltro) {
        console.log ('carregando')
        $rootScope.routeloading = true;

        $scope.Previsao= [];
        $('#dataTable').dataTable().fnDestroy();
        httpService.Post('CarregarPrevisaoVendasAgencia',pFiltro).then(function (response) {
            if (response) {
                $scope.Previsao =  response.data;
                if ($scope.Previsao.length == 0) {
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
        console.log("carregar1");
        $scope.ConfiguraGrid();
        $scope.CarregarPrevisaoAgencia($scope.Parameters);
    });
   

}]);

