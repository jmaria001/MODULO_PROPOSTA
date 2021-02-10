angular.module('App').controller('MateriaisFitasController', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location) {

    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;
    $scope.PermissaoProgramaAvulso = false;

    httpService.Get("credential/MateriaisFitas@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/MateriaisFitas@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });


    //====================Inicializa scopes

    $scope.CurrentShow = "Filtro";


    $scope.gridheaders = [{ 'title': '', 'visible': true, 'searchable': false, 'config': false, 'sortable': false },
    { 'title': 'Titulo', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Numero Fita', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Cod.Veiculo', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Dur.', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Status', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    ];
    //====================Inicializa o Filtro
    $scope.Filtro = {};
    $scope.NewFiltro = function () {
        localStorage.removeItem('MateriaisFitas');
        return {
            'Cod_Agencia': '',
            'Cod_Cliente': ''
        }
    }
    //===========================Se ja tiver filtro anterior gravado
    var _Filter = JSON.parse(localStorage.getItem('MateriaisFitas'));

    if (_Filter) {
        $scope.Filtro = _Filter;
    }
    else {
        $scope.Filtro = $scope.NewFiltro();
    }


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

    $scope.CarregarMateriaisFitas = function (pFiltro) {
        $scope.MateriaisFitasS = [];
        if (!pFiltro.Cod_Agencia && !pFiltro.Cod_Cliente ) {
            ShowAlert("Agência ou Cliente é um filtro obrigatório");
            return;
        }

        localStorage.setItem('MateriaisFitas', JSON.stringify($scope.Filtro));
        $scope.CurrentShow = '';
        $('#dataTable').dataTable().fnDestroy();
        var _url = 'MateriaisFitasListar';
        _url += '?cod_Agencia=' + pFiltro.Cod_Agencia;
        _url += '&Cod_Cliente=' + pFiltro.Cod_Cliente;
        _url += '&';
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.MateriaisFitasS = response.data;
                if ($scope.MateriaisFitasS.length == 0) {
                    $scope.RepeatFinished();
                }
            }
        });
    };

    //==================== Nova Fita
    $scope.NovaMateriaisFitas = function () {
        $location.path("/MateriaisFitasCadastro/New/0")
    }
    //==================== Edicao da fita
    $scope.NovaMateriaisFitas = function (pIdFita) {
        $location.path("/MateriaisFitasCadastro/Edit/" + pIdMateriaisFitas)
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
                //{ text: 'Novo Mapa Reserva', className: 'btn btn-primary btnNew', action: function (e, dt, button, config) { $('#btnNovoMapaReservaCompensacao').click(); } },
                {
                    text: 'Abrir no Excel<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning', extend: 'excel', exportOptions: {
                        columns: ':visible:not(:first-child)'
                    }
                },
                { text: 'Novo Filtro' + '<span class="fa fa-filter margin-left-10"></span>', className: 'btn btn-info', action: function (e, dt, button, config) { $('#btnNovoFiltro').click(); } },
                { text: 'Novo Material', className: 'btn  btn-primary', action: function (e, dt, button, config) { $('#btnNovoMateriaisFitasCadastro').click(); } },
            ];
        param.order = [[1, 'desc']];
        param.autoWidth = false;
        param.columns = [];
        for (var i = 0; i < $scope.gridheaders.length; i++) {
            param.columns.push({ "visible": $scope.gridheaders[i].visible, "searchable": $scope.gridheaders[i].searchable, "sortable": $scope.gridheaders[i].sortable });
        }
        $('#dataTable').DataTable(param);
        var table = $('#dataTable').DataTable();
        var buttons = table.buttons([0]);
        if (table.rows({ selected: true }).indexes().length === 0) {
            buttons.disable();
        }
        else {
            buttons.enable();
        }
    };

    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });

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
                $scope.CarregarMateriaisFitas(_Filter);
            }
        }
    });

}]);


