angular.module('App').controller('ConsultaFitasOrdenadasController', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location) {

    //====================Inicializa scopes

    $scope.ShowFiltro = true;


    $scope.CurrentShow = "Filtro";

    $scope.gridheaders = [{ 'title': 'N.Fitas', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Origem', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Veiculo', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Agência', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Cliente', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Data', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Programa', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Comercial', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Dur.', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Breakk/Hor.', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Posição', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Tipo', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Empresa', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Contarto', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Sequencia', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },

    ];
    //====================Inicializa o Filtro
    $scope.Filtro = {};
    $scope.NewFiltro = function () {
        localStorage.removeItem('ConsultaFitasOrdenadas_filter');
        return {
            'Cod_Veiculo': '',
            'Cod_Programa': '',
            'Data_Inicio': '',
            'Data_Fim': '',
            'Numero_Fita_Inicio': '',
            'Numero_Fita_Fim': '',
            'Empresa': '',
            'Contrato': '',
            'Sequencia': ''
        }
    }
    //===========================Se ja tiver filtro anterior gravado
    var _Filter = JSON.parse(localStorage.getItem('ConsultaFitasOrdenadas_filter'));

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

    $scope.CarregarConsultaFitasOrdenadas = function (pFiltro) {
        $scope.ConsultaFitasOrdenadaS = [];
        //if (!pFiltro.Cod_Veiculo) {
        //    ShowAlert("Codigo veículo é um filtro obrigatório");
        //    return;
        //}
        localStorage.setItem('ConsultaFitasOrdenadas_filter', JSON.stringify($scope.Filtro));
        $scope.CurrentShow = '';
        $('#dataTable').dataTable().fnDestroy();
        var _url = 'ConsultaFitasOrdenadasListar';
        _url += '?Cod_Veiculo=' + pFiltro.Cod_Veiculo;
        _url += '&Cod_Programa=' + pFiltro.Cod_Programa;
        _url += '&Data_Inicio=' + pFiltro.Data_Inicio;
        _url += '&Data_Fim=' + pFiltro.Data_Fim;
        _url += '&Numero_Fita_Inicio=' + pFiltro.Numero_Fita_Inicio;
        _url += '&Numero_Fita_Fim=' + pFiltro.Numero_Fita_Fim;
        _url += '&Empresa=' + pFiltro.Empresa;
        _url += '&Numero_Mr=' + pFiltro.Numero_Mr;
        _url += '&Sequencia=' + pFiltro.Sequencia;
        _url += '&';
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.ConsultaFitasOrdenadaS = response.data;
                if ($scope.ConsultaFitasOrdenadaS.length == 0) {
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
                //{ text: 'Novo Mapa Reserva', className: 'btn btn-primary btnNew', action: function (e, dt, button, config) { $('#btnNovoMapaReservaCompensacao').click(); } },
                {
                    text: 'Abrir no Excel<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning', extend: 'excel', exportOptions: {
                        columns: ':visible:not(:first-child)'
                    }
                },
                { text: 'Novo Filtro' + '<span class="fa fa-filter margin-left-10"></span>', className: 'btn btn-info', action: function (e, dt, button, config) { $('#btnNovoFiltro').click(); } },
                //  { text: 'Nova Fita', className: 'btn  btn-primary', action: function (e, dt, button, config) { $('#btnNovoDepositorioFitasCadastro').click(); } },
            ];
        param.order = [[1, 'desc']];
        ////param.autoWidth = false;
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
                $scope.CarregarNumeracaoFitas(_Filter);
            }
        }
    });

}]);


