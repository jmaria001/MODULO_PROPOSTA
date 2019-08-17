angular.module('App').controller('Simulacao_List_Controller', ['$scope', '$rootScope', 'SimulacaoApi', '$location', function ($scope, $rootScope, SimulacaoApi, $location) {

    //====================Inicializa scopes
    $scope.CurrentShow = "Grid";

    $scope.gridheaders = [  { 'title': 'Crud', 'visible': true, 'searchable': false, 'config': false, 'sortable': false},
                            { 'title': 'Id', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Identificação', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Período Inicio', 'visible': true, 'searchable': false, 'config': true, 'sortable': true },
                            { 'title': 'Valor Tabela', 'visible': true, 'searchable': false, 'config': true, 'sortable': true },
                            { 'title': 'Valor Negociado', 'visible': true, 'searchable': false, 'config': true, 'sortable': true },
                            { 'title': 'Desconto Real.', 'visible': true, 'searchable': false, 'config': true, 'sortable': true },
                            { 'title': 'Autor', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Status', 'visible': true, 'searchable': true, 'config': true, 'sortable': true }
                        ];

    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.CurrentShow = 'Grid';
        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        }, 10)
        
        
    };

    //====================Carrega o Grid
    $scope.CarregarSimulacao= function () {
        $scope.Simulacao= [];
        $scope.CurrentShow = '';
        $('#dataTable').dataTable().fnDestroy();
        SimulacaoApi.ListSimulacao().then(function (response) {
            if (response) {
                $scope.Simulacoes= response.data;
                if ($scope.Simulacoes.length ==0) {
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

            param.scrollY = "500px",
            param.scrollCollapse = true;
            param.paging = false;

        //if ($scope.GridModo.ScroolY) {
        //    param.scrollY = "500px",
        //    param.scrollCollapse = true;
        //    param.paging = false;
        //};
        //if ($scope.GridModo.ScroolX) {
        //    param.scrollX = true;
        //    var ctrl = $document[0].getElementById('dataTable');
        //    $(ctrl).addClass('nowrap')
        //}
        //else {
        //    var ctrl = $document[0].getElementById('dataTable');
        //    $(ctrl).removeClass('nowrap')
        //};
        param.dom = "<'row'<'col-sm-6'B><'col-sm-3'l><'col-sm-3'f>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        param.buttons = [
            { text: 'Nova Simulação<span class="fa fa-file margin-left-10"></span>', className: 'btn btn-primary', action: function (e, dt, button, config) { $('#btnNovaSimulacao').click(); } },
            {
                text: 'Exportar<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning', extend: 'excel', exportOptions: {
                    columns: ':visible:not(:first-child)'
                }
            }
        ];
        param.order = [[1, 'asc']];
        param.autoWidth = false;

        param.columns = [];
        for (var i = 0; i < $scope.gridheaders.length; i++) {
            param.columns.push({ "visible": $scope.gridheaders[i].visible, "searchable": $scope.gridheaders[i].searchable, "sortable": $scope.gridheaders[i].sortable });
        }

        $('#dataTable').DataTable(param);
        var table = $('#dataTable').DataTable();
        var buttons = table.buttons([1]);
        if (table.rows({ selected: true }).indexes().length === 0) {
            buttons.disable();
        }
        else {
            buttons.enable();
        }
    };

    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.CarregarSimulacao();
    });

    $scope.NovaSimulacao= function (){
        $location.path("/SimulacaoCadastro/New/0");
    }
    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });
}]);


