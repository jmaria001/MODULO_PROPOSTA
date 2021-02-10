angular.module('App').controller('ConsultaProgramacaoDiariaDetalheController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;

    //===========Grid
    $scope.gridheaders = [
        { 'title': '', 'visible': true, 'searchable': false, 'sortable': false },
        { 'title': 'Indica_Grade', 'visible': true, 'searchable': false, 'sortable': false },
        { 'title': 'Cod_Empresa', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Cod_Nucleo', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Cod_Cliente', 'visible': true, 'searchable': false, 'sortable': false },
        { 'title': 'Cod_Agencia', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Forma_Pgto', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Merchandising', 'visible': true, 'searchable': false, 'sortable': false },
        { 'title': 'Cod_Programa', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Cod_Tipo_Midia', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Duracao', 'visible': true, 'searchable': false, 'sortable': false },
        { 'title': 'Cod_Caracteristica', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Cod_Qualidade', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Documento_DE', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Descricao', 'visible': true, 'searchable': false, 'sortable': false },
        { 'title': 'Numero_MR', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Indica_Absorcao', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Indica_Estouro', 'visible': true, 'searchable': false, 'sortable': false },
        { 'title': 'Data_Cadastramento', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Chave_Acesso', 'visible': true, 'searchable': true, 'sortable': true },
    ];

    //=========Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.ShowGrid = true;
        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        }, 10)
    };

    //====================Carrega o Grid
    $scope.ConsultaProgramacaoDiariaDetalhe = function (pfiltro2) {
        $rootScope.routeloading = true;
        $scope.ConsultaProgramacaoDiariaDetalhes = [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();

        var _url = 'ListarConsultaProgramacaoDiariaDetalhe'
        _url += '?Cod_Veiculo=' + pfiltro2.Veiculo;
        _url += '&Data_Exibicao=' + pfiltro2.Data;
        _url += '&Cod_Programa=' + pfiltro2.Programa;
        _url += '&Indica_Grade=' + pfiltro2.Grade;
        _url += '&';
        
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.ConsultaProgramacaoDiariaDetalhes = response.data;
                if ($scope.ConsultaProgramacaoDiariaDetalhes.length == 0) {
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
        param.pageLength = 10;
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
        param.order = [[5, 'asc']];
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
        $scope.ConsultaProgramacaoDiariaDetalhe($scope.Parameters);
    });

}]);
