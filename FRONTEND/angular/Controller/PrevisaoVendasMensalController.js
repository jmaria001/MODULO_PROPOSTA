angular.module('App').controller('PrevisaoVendasMensalController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {


    //====================Recebe Parametros
    $scope.Parameters = $routeParams;

    //====================Inicializa scopes
    $scope.Previsao = [];
    $scope.gridheaders = [{ 'title': 'Mês', 'visible': true, 'searchable': true, 'sortable': true },
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

    $scope.CarregarPrevisaoVendasMensal = function (pFiltro) {
        $rootScope.routeloading = true;
        $scope.Previsao = [];
        $('#dataTable').dataTable().fnDestroy();
        httpService.Post('CarregarPrevisaoVendasMensal', pFiltro).then(function (response) {
            if (response) {

                $scope.Previsao = response.data;
                $scope.CurrentShow = "Filtro";

            };

            if ($scope.Previsao.length == 0) {
                $scope.RepeatFinished();
            };
        });
    };

           

    //====================Funcao para configurar o Grid
    $scope.ConfiguraGrid = function () {
        param = {};
        param.language = fnDataTableLanguage();
        param.lengthMenu = [[3, 6, 12,-1], [3, 6, 12, "Todos"]];
        param.pageLength =-1;
        param.scrollCollapse = true;
        param.paging = true;

        param.dom = "<'row'<'col-sm-3'l><'col-sm-9'f>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
            //param.buttons = [
            //    {
            //        text: 'Exportar<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning HideButton', extend: 'excel', exportOptions: {
            //            columns: ':visible:not(:first-child)'
            //        }
            //    }
            //];
        param.order = [];
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
        $scope.CarregarPrevisaoVendasMensal($scope.Parameters);
    });
}]);

