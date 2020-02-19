angular.module('App').controller('NegociacaoDetalheController', ['$scope', '$rootScope', '$routeParams', 'httpService', '$location', '$timeout', function ($scope, $rootScope, $routeParams,httpService, $location, $timeout) {
    
    //====================Inicializa scopes
    $scope.Parameters = $routeParams;
    $scope.ListStatus = "";
    $scope.gridheaders = [  { 'title': 'Contrato', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Mês Campanha', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Emp.Fat', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Agência', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Cliente', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Contato', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Tipo de Mídia', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Abrangência', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Vlr Exib. Tabela', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Vlr Exib. Negociado', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Vlr Faturado', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Desconto Real', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    ];
    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        }, 1000)
    };

    //====================Carrega o Grid
    $scope.CarregarDetalhe= function (pFiltro) {
        $scope.Contratos= [];
        
        $('#dataTable').dataTable().fnDestroy();
        var _url = 'Negociacao/Detalhe' 
        _url += '?Numero_Negociacao=' + $scope.Parameters.Id
        _url += '&';
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Contratos= response.data;
                if ($scope.Contratos.length == 0) {
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
            { text: 'Voltar', className: 'btn btn-primary', action: function (e, dt, button, config) { $('#btnSair').click(); } },
            { text: 'Abrir no Excel<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning', extend: 'excel', exportOptions: { columns: ':visible:not(:first-child)' } },
        ];
        param.order = [[1, 'desc']];
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
    //===========================Sair do Detalhe
    $scope.DetalheSair = function () {
        $location.path("/Negociacao")
    }
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.CarregarDetalhe();
    });
    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });
}]);


