angular.module('App').controller('TabelaPrecosController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {[]
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;
    $scope.PermissaoImport= false;
    httpService.Get("credential/TabelaPrecos@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/TabelaPrecos@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });
    httpService.Get("credential/TabelaPrecos@Import").then(function (response) {
        $scope.PermissaoImport = response.data;
    });

    //====================Inicializa scopes
    $scope.Filtro = {};
    $scope.NewFiltro = function () {
        $scope.Filtro = { 'Competencia': '', 'Veiculo': '', 'Programa': '', 'Titulo': '', 'Nome_Veiculo': '','Indica_Vigente':false };
        localStorage.removeItem('TabelaPreco_Filter');
    }
    //======================Verifica se tem filtro anterior
    var _Filter = JSON.parse(localStorage.getItem('TabelaPreco_Filter'));
    if (!_Filter) {
        $scope.NewFiltro()
    }

    $scope.ShowGrid = false;
    //========================Parametros do Grid
    $scope.ShowFilter= true;
    $scope.gridheaders = [{ 'title': '', 'visible': true, 'searchable': false, 'sortable': false },
        { 'title': 'Titulo', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Nome_Veiculo', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Competencia', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Sequencia', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Tipo_Preco', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Valor', 'visible': true, 'searchable': true, 'sortable': true },
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
    $scope.CarregarTabelaPrecos = function (pFiltro) {
        //if (!pFiltro.Competencia && !pFiltro.Indica_Vigente) {
        //    ShowAlert("Filtro Competência ou Vigente é obrigatório");
        //    return;
        //}
        //if (!pFiltro.Veiculo && !pFiltro.Programa) {
        //    ShowAlert("Filtro Veículo ou Programa é obrigatório");
        //    return;
        //}
        if (!pFiltro.Competencia && !pFiltro.Indica_Vigente && !pFiltro.Veiculo && !pFiltro.Programa) {
            ShowAlert("Preencha pela menos um Filtro");
            return;
        }

        $rootScope.routeloading = true;
        $scope.TabelaPrecosS = [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();
        var _url = 'TabelaPrecosListar'
        _url += '?Competencia=' + pFiltro.Competencia;
        _url += '&Veiculo=' + pFiltro.Veiculo;
        _url += '&Programa=' + pFiltro.Programa;
        _url += '&Indica_Vigente=' + pFiltro.Indica_Vigente;
        _url += '&';
        $scope.ShowFilter = false;
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.TabelaPrecosS = response.data;
                if ($scope.TabelaPrecosS.length == 0) {
                    $scope.RepeatFinished();
                }
                localStorage.setItem('TabelaPreco_Filter', JSON.stringify($scope.Filtro));
            }
        });
    };
    //====================Importar Tabela de Precos
    $scope.ImportarPreco = function () {
        $location.path("/TabelaPrecosImport")
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
        if (_Filter) {
            $scope.Filtro = _Filter;
            $scope.CarregarTabelaPrecos($scope.Filtro);
        }
    });
}]);

