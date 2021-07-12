angular.module('App').controller('MapaReservaController', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location) {

    //====================Inicializa scopes
    $scope.CurrentShow = "Filtro";

    $scope.gridheaders = [{ 'title': 'Edit', 'visible': true, 'searchable': false, 'config': false, 'sortable': false },
        { 'title': 'Contrato', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Tipo', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Negociação', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Período Campanha', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Agencia', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Cliente', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Contato', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Tipo de Mídia', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Número PI', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Campanha', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Valor Tabela', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Desconto', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Valor Negociado', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Status', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    ];
    $scope.MesAnoKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.TipoVenda = [{ 'Codigo': 1, 'Descricao': 'On-Line' },{'Codigo':2,'Descricao':'Off-Line'},{'Codigo':3,'Descricao':'Ambos'}]

    //====================Inicializa o Filtro
    $scope.NewFiltro = function () {
        localStorage.removeItem('MapaReservaFilter');
        return {
            'Numero_Negociacao': '',
            'Numero_Mr': '',
            'Numero_Pi': '',
            'Competencia_Inicio': '',
            'Competencia_Fim': '',
            'Numero_Contrato': '',
            'Cod_Empresa_Venda': '',
            'Cod_Empresa_Faturamento': '',
            'Agencia': '',
            'Cliente': '',
            'Contato': '',
            'TipoVenda':'',
        }
    }
    //===========================Se ja tiver filtro anterior gravado
    var _Filter = JSON.parse(localStorage.getItem('MapaReservaFilter'));

    if (_Filter) {
        $scope.Filtro = _Filter;
    }
    else {
        $scope.Filtro = $scope.NewFiltro();
    }

    //====================Permissoes
    $scope.PermissaoNew = 'false';
    $scope.PermissaoEditar = 'false';
    httpService.Get("credential/" + "MapaReserva@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });
    httpService.Get("credential/" + "MapaReserva@New").then(function (response) {
        $scope.PermissaoNew = response.data;
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
    $scope.CarregarMapaReserva = function (pFiltro) {
        $scope.Contratos = [];
        localStorage.setItem('MapaReservaFilter', JSON.stringify($scope.Filtro));
        $scope.CurrentShow = '';
        $('#dataTable').dataTable().fnDestroy();
        var _url = 'MapaReserva/List';
        //_url += '?Numero_Negociacao=' + pFiltro.Numero_Negociacao;
        //_url += '&Numero_Mr=' + pFiltro.Numero_Mr;
        //_url += '&Numero_Pi=' + pFiltro.Numero_Pi;
        //_url += '&Competencia_Inicio=' + pFiltro.Competencia_Inicio;
        //_url += '&Competencia_Fim=' + pFiltro.Competencia_Fim;
        //_url += '&Cod_Empresa_Venda=' + pFiltro.Cod_Empresa_Venda;
        //_url += '&Cod_Empresa_Faturamento=' + pFiltro.Cod_Empresa_Faturamento;
        //_url += '&Agencia=' + pFiltro.Agencia;
        //_url += '&Cliente=' + pFiltro.Cliente;
        //_url += '&Contato=' + pFiltro.Contato;
        //_url += '&';
        httpService.Post(_url,pFiltro).then(function (response) {
            if (response) {
                $scope.Contratos = response.data;
                if ($scope.Contratos.length == 0) {
                    $scope.RepeatFinished();
                }
            }
        });
    };

    //==================== Novo MapaReserva
    $scope.NovaMapaReserva = function () {
        $location.path("/MapaReservaCadastro/New/0")
    }
    //==================== Edicao da MapaReserva
    $scope.NovaMapaReserva = function (pIdMapaReserva) {
        $location.path("/MapaReservaCadastro/Edit/" + pIdMapaReserva)
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
            { text: 'Novo Mapa Reserva', className: 'btn btn-primary btnNew', action: function (e, dt, button, config) { $('#btnNovoMapaReserva').click(); } },
            {
                text: 'Abrir no Excel<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning', extend: 'excel', exportOptions: {
                    columns: ':visible:not(:first-child)'
                }
            },
            { text: 'Novo Filtro' + '<span class="fa fa-filter margin-left-10"></span>', className: 'btn btn-info', action: function (e, dt, button, config) { $('#btnNovoFiltro').click(); } },
        ];
       // param.order = [[1, 'desc']];
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
                $scope.CarregarMapaReserva(_Filter);
            }
        }
    });

    $scope.NovaMapaReserva = function () {
        $location.path("/MapaReservaCadastro/New/0/");
    };

    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });
}]);


