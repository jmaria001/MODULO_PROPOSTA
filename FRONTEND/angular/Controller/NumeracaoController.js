angular.module('App').controller('NumeracaoController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {
    //========================Verifica Permissoes
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.Competencia_Nova = CurrentMMYYYY();
    $scope.CurrentShow = "Grid";
    //========================Parametros do Grid
    $scope.ShowFilter = true;
    $scope.gridheaders = [{ 'title': 'Sel.', 'visible': true, 'searchable': false, 'sortable': false },
    { 'title': 'Empresa', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Competência', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Ultima Emissão', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Tipo', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Ultimo Nº', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Encerrado Por', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Critica', 'visible': true, 'searchable': true, 'sortable': true },
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
    $scope.CarregarNumeracao = function () {
        $rootScope.routeloading = true;
        $scope.NumeracaoFiscais = [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();
        httpService.Get('Numeracao/Listar').then(function (response) {
            if (response) {
                $scope.NumeracaoFiscais = response.data;
                if ($scope.NumeracaoFiscais.length == 0) {
                    $scope.RepeatFinished();
                };
            };
        });
    };
    
    //====================Funcao para configurar o Grid
    $scope.ConfiguraGrid = function () {
        param = {};
        param.language = fnDataTableLanguage();
        param.lengthMenu = [[7, 10, 25, 50, -1], [7, 10, 25, 50, "Todos"]];
        param.pageLength = 7;
      //  param.scrollCollapse = true;
        param.paging = true;

        param.dom = "<'row'<'col-sm-3'l><'col-sm-4'f><'col-sm-5'B>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
            param.buttons = [
                {
                    text: 'Exportar<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning HideButton', extend: 'excel', exportOptions: {
                        columns: ':visible:not(:first-child)'
                    }
                },

            ];
        param.order = [[1, 'asc']];
        param.autoWidth = false;
        param.columns = [];
        for (var i = 0; i < $scope.gridheaders.length; i++) {
            param.columns.push({ "visible": $scope.gridheaders[i].visible, "searchable": $scope.gridheaders[i].searchable, "sortable": $scope.gridheaders[i].sortable });
        }
        $('#dataTable').DataTable(param);

    };
    
    $scope.FecharCompetencia = function (pNumeracao) {
        var _selecionado = false;
        for (var i = 0; i < pNumeracao.length; i++) {
            if (pNumeracao[i].Selected) {
                _selecionado = true;
                break;
            }
        };
        if (!_selecionado) {
            ShowAlert("Nenhum Empresa foi Selecionada para fechamento.");
            return;
        }
        $scope.CurrentShow = "Base";
    };
    $scope.ConfirmarFechamento = function (pNumeracao) {
        if (!$scope.Competencia_Nova) {
            ShowAlert("Nova Competência não foi Informada");
            return;
        }
        for (var i = 0; i < pNumeracao.length; i++) {
            pNumeracao[i].Competencia_Nova = $scope.Competencia_Nova;
        };
        httpService.Post("Numeracao/Confirmar", pNumeracao).then(function (response) {
            if (response.data) {
                $scope.NumeracaoFiscais = response.data;
                $scope.CurrentShow = 'Grid';
            };
        });
    };
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });

    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.ConfiguraGrid();
        $scope.CarregarNumeracao();
  
    });
        //===========================Funcao MarcarDismarcar
    $scope.MarcarDismarcar = function (pNumeracao, pvalue) {
        for (var i = 0; i < pNumeracao.length; i++) {
            pNumeracao[i].Selected = pvalue;
        }
    };

}]);

