angular.module('App').controller('NegociacaoController', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location) {

    //====================Inicializa scopes
    $scope.CurrentShow = "Filtro";
    $scope.ListStatus = "";
    $scope.gridheaders = [{ 'title': 'Edit', 'visible': true, 'searchable': false, 'config': false, 'sortable': false },
                            { 'title': 'Negociação', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Emp.Venda', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Emp.Faturamento', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Período', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Agencias', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Clientes', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Tipo de Mídia', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Tabela de Preços', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Verba Negociada', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Realizado Tabela', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Realizado Negociado', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Desconto Concedido', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Desconto Real', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Contatos', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Status', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    ];
    $scope.MesAnoKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    
    //====================Inicializa o Filtro
    $scope.NewFiltro = function () {
        localStorage.removeItem('NegociacaoFilter');
        return {
            'Id_Negociacao': '',
            'Validade_Inicio': '',
            'Validade_Termino': '',
            'Cod_Empresa_Venda': '',
            'Nome_Empresa_Venda': '',
            'Cod_Empresa_Faturamento': '',
            'Nome_Empresa_Faturamento': '',
            'Agencia': '',
            'Cliente': '',
            'Contato': '',
        }
    }
    //===========================Se ja tiver filtro anterior gravado
        var _Filter = JSON.parse(localStorage.getItem('NegociacaoFilter'));

    if (_Filter) {
        $scope.Filtro = _Filter;
    }
    else {
        $scope.Filtro = $scope.NewFiltro();
    }

    //====================Permissoes
    $scope.PermissaoNew= 'false';
    $scope.PermissaoEditar = 'false';
    httpService.Get("credential/" +  "Negociacao@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });
    httpService.Get("credential/" + "Negociacao@New").then(function (response) {
        $scope.PermissaoNew= response.data;
    });

    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.CurrentShow = 'Grid';
        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        }, 1000)
    };

    //====================Carrega o Grid
    $scope.CarregarNegociacao = function (pFiltro) {
        $scope.Negociacoes = [];
        localStorage.setItem('NegociacaoFilter', JSON.stringify($scope.Filtro));
        $scope.CurrentShow = '';
        $('#dataTable').dataTable().fnDestroy();
        var _url = 'Negociacao/List';
        _url += '?Numero_Negociacao=' + pFiltro.Id_Negociacao;
        _url += '&Validade_Inicio=' + pFiltro.Validade_Inicio;
        _url += '&Validade_Termino=' + pFiltro.Validade_Termino;
        _url += '&Cod_Empresa_Venda=' + pFiltro.Cod_Empresa_Venda;
        _url += '&Cod_Empresa_Faturamento=' + pFiltro.Cod_Empresa_Faturamento;
        _url += '&Agencia=' + pFiltro.Agencia;
        _url += '&Cliente=' + pFiltro.Cliente;
        _url += '&Contato=' + pFiltro.Contato;
        _url += '&';
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Negociacoes = response.data;
                if ($scope.Negociacoes.length == 0) {
                    $scope.RepeatFinished();
                }
            }
        });
    };

    //==================== Nova Negociacao
    $scope.NovaNegociacao = function () {
        $location.path("/NegociacaoCadastro/New/0")
    }
    //==================== Edicao da Negociacao
    $scope.NovaNegociacao = function (pIdNegociacao) {
        $location.path("/NegociacaoCadastro/Edit/" + pIdNegociacao)
    }
    //====================Configuracao do Grid
    $scope.ConfiguraGrid = function () {
        param = {};
        param.language = fnDataTableLanguage();
        param.lengthMenu = [[7, 10, 25, 50, -1], [7, 10, 25, 50, "Todos"]];

        //param.scrollCollapse = true;
        param.paging = true;
        param.dom = "<'row'<'col-sm-6'B><'col-sm-3'l><'col-sm-3'f>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        param.buttons = [
            { text: 'Nova Negociação', className: 'btn btn-primary btnNew', action: function (e, dt, button, config) { $('#btnNovaNegociacao').click(); } },
            {
                text: 'Abrir no Excel<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning', extend: 'excel', exportOptions: {
                    columns: ':visible:not(:first-child)'
                }
            },
            { text: 'Novo Filtro' + '<span class="fa fa-filter margin-left-10"></span>', className: 'btn btn-info', action: function (e, dt, button, config) { $('#btnNovoFiltro').click(); } },
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
        var buttonsNew = table.buttons([0]);
        if (!$scope.PermissaoNew) {
            buttonsNew.disable();
        }
        else {
            buttonsNew.enable();
        }
    };
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();

        if (_Filter) {
            var _Carregar = false
            angular.forEach(_Filter, function (value, key) {
                if (value) {
                    _Carregar = true;
                }
            });
            if (_Carregar) {
                $scope.CarregarNegociacao(_Filter);
            }
        }
    });

    $scope.NovaNegociacao = function () {
        $location.path("/NegociacaoCadastro/New/0/");
    };

    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });
}]);


