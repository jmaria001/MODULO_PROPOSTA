angular.module('App').controller('EmpresaController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {
    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;
    httpService.Get("credential/Empresa@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/Empresa@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });
    //====================Inicializa scopes
    $scope.ShowGrid = false;
    $scope.gridheaders = [{ 'title': '', 'visible': true, 'searchable': false, 'sortable': false },
    { 'title': 'Codigo', 'visible': true, 'searchable': true, 'sortable': true },
    //{ 'title': 'Bairro', 'visible': true, 'searchable': true, 'sortable': true },
    //{ 'title': 'Cod_Uf', 'visible': true, 'searchable': true, 'sortable': true },
    //{ 'title': 'CEP', 'visible': true, 'searchable': true, 'sortable': true },
    //{ 'title': 'CNPJ', 'visible': true, 'searchable': true, 'sortable': true },
    //{ 'title': 'Cidade', 'visible': true, 'searchable': true, 'sortable': true },
    //{ 'title': 'Empresa_Pertence', 'visible': true, 'searchable': true, 'sortable': true },
    //{ 'title': 'Endereco', 'visible': true, 'searchable': true, 'sortable': true },
    //{ 'title': 'Inscricao_Estadual', 'visible': true, 'searchable': true, 'sortable': true },
    //{ 'title': 'Inscricao_Municipal', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Razao_Social', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Cod_Jove', 'visible': true, 'searchable': true, 'sortable': true },
    //{ 'title': 'Telefone', 'visible': true, 'searchable': true, 'sortable': true },
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
    $scope.CarregarEmpresa = function () {
        $rootScope.routeloading = true;
        $scope.Empresas = [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();
        httpService.Get('EmpresaListar').then(function (response) {
            if (response) {
                $scope.Empresas = response.data;
                if ($scope.Empresas.length === 0) {
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
        $scope.CarregarEmpresa();
    });
    $scope.EditarEmpresa

}]);

