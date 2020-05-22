angular.module('App').controller('ProdutoController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {

    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;
    httpService.Get("credential/Produto@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/Produto@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });
    //====================Inicializa scopes
    $scope.ShowGrid = false;
    $scope.ShowFilter = true;
    $scope.Filtro = {'Segmento':'','Setor':'','Produto':''}
    $scope.gridheaders = [{ 'title': 'Segmento', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Setor', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Produto', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Horário', 'visible': true, 'searchable': true, 'sortable': true },
    ];

    $scope.NewFiltro = function()
    {
        return { 'Segmento': '', 'Setor': '', 'Produto': '' }
    }
    //======================Verifica se tem filtro anterior
    var _Filter = JSON.parse(localStorage.getItem('ProdutoFilter'));
    if (!_Filter) {
        $scope.NewFiltro()
    }
    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.ShowGrid = true;
        $scope.ShowFilter= false;
        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        }, 10)
    };

    //====================Carrega o Grid
    $scope.CarregarProduto = function (pFiltro) {
        $rootScope.routeloading = true;
        $scope.Produtos = [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();

        var _url = 'ProdutoListar';
        _url += '?Segmento=' + pFiltro.Segmento;  
        _url += '&Setor=' + pFiltro.Setor
        _url += '&Produto=' + pFiltro.Produto;
        _url += '&';
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Produtos = response.data;
                if ($scope.Produtos.length === 0) {
                    $scope.RepeatFinished();
                }
            }
            localStorage.setItem('ProdutoFilter', JSON.stringify($scope.Filtro));
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

    $scope.NovoFiltro = function () {
        $scope.ShowGrid = false;
        $scope.ShowFilter = true;
        $scope.Filtro = $scope.NewFiltro();
    }
    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.ConfiguraGrid();
        if (_Filter) {
            $scope.Filtro = _Filter;
            $scope.CarregarProduto($scope.Filtro);
        }
    });
    $scope.EditarProduto

}]);

