angular.module('App').controller('GeracaoFaturaController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {
    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.PermissaoEdit = false;
    

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.Contrato = "";
    console.log($scope.Parameters);

    //====================Inicializa scopes
    $scope.Filtro = {};
    $scope.NewFiltro = function () {
        const newLocal = $scope.Filtro = { 'Emp_Faturamento': '' };
        localStorage.removeItem('ContratosFaturaLista');
    }
    
    //======================Verifica se tem filtro anterior
    var _Filter = JSON.parse(localStorage.getItem('ContratosFaturaLista'));
    if (!_Filter) {

        $scope.NewFiltro()
    }

    $scope.ShowGrid = false;
    $scope.ShowDados = false;

    //========================Parametros do Grid
    $scope.ShowFilter = true;
    $scope.gridheaders = [{ 'title': 'Selecionar', 'visible': true, 'searchable': false, 'sortable': false },
        { 'title': 'Origem', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Negociação', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Contrato', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'TipoMidia', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Cliente', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Agencia', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Valor', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Complemento', 'visible': true, 'searchable': true, 'sortable': true },
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
    var intCompetencia = null;
    //====================Carrega contrato p faturamenmto            
    $scope.ContratosFaturaLista = function (pFiltro) {
        if (pFiltro.Emp_Faturamento == "") {
            ShowAlert("Empresa de Faturamento é de seleção obrigatória!");
            return
        }

        $rootScope.routeloading = true;
        $scope.ContratosFatura = [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();
        var _url = 'ContratosFaturaLista'
        _url += '?Emp_Faturamento=' + pFiltro.Emp_Faturamento;
        _url += '&';
        $scope.ShowFilter = false;
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.ContratosFatura = response.data;

                if ($scope.ContratosFatura.length == 0) {
                    ShowAlert("Não existe dados cadastrado p/ este Filtro");
                    $scope.RepeatFinished();
                }
            }
            localStorage.setItem('ContratosFaturaLista', JSON.stringify($scope.Filtro));
        });
    };

    //==========================Incluir solicitação de fatura
    $scope.IncluirSolicitacao = function (pContratos) {
        //if ($scope.Parameters.Action == "New")
        //{
        //    $scope.Veiculo.id_operacao = 'I';Parameters.Action
        //}
        //$scope.TipoMidia.id_operacao = $scope.sw == "New" ? 'I' : 'E';
        $scope.Contrato.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("IncluirSolicitacao", pContratos).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    if ($scope.Parameters.Action == 'New') {
                        $scope.CarregaDados();
                    }
                    else {
                        $location.path("/Contrato")
                    }
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
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
        param.order = [[1, 'asc']];
        param.autoWidth = false;
        param.columns = [];
        for (var i = 0; i < $scope.gridheaders.length; i++) {
            param.columns.push({ "visible": $scope.gridheaders[i].visible, "searchable": $scope.gridheaders[i].searchable, "sortable": $scope.gridheaders[i].sortable });
        }
        $('#dataTable').DataTable(param);
    };

    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });

    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.ConfiguraGrid();
        if (_Filter) {
            $scope.Filtro = _Filter;
            //$scope.RateioImportar($scope.Filtro);
        }
    });

    //===========================Funcao MarcarDismarcar
    $scope.MarcarDismarcar = function (pContratos, pvalue) {
        //console.log("chegando");
        for (var i = 0; i < pContratos.length; i++) {
            pContratos[i].Selected= pvalue;
        }
    };

}]);

