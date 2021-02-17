angular.module('App').controller('ConsultaVeiculacaoController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {

    //====================Inicializa scopes
    $scope.Filtro =
    $scope.NewFiltro = function () {
        return {
            'Veiculo': '',
            'Empresa': '',
            'Contrato': '',
            'Sequencia': '',
            'Programa': '',
            'Data_Inicio': '',
            'Data_Termino': '',
            'Qualidade': '',
            'Par_Net': '',
            'Baixadas': true,
            'NaoBaixadas': true,
            'Ordenadas': true,
            'NaoOrdenadas': true,
            'Net': true,
            'Local': true
        };
    };
    $scope.NewFiltro();
    //======================Verifica se tem filtro anterior
    var _Filter = JSON.parse(localStorage.getItem('ConsultaVeiculacao_filter'));
    if (!_Filter) {
        
        $scope.Filtro=$scope.NewFiltro();
    }

    $scope.ShowGrid = false;
    //========================Parametros do Grid
    $scope.ShowFilter = true;
    $scope.gridheaders = [{ 'title': 'Data', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Progr', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Break', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Tipo', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Chv', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Vei.', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Contrato', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Comercial', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Produto', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Dur.', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'T.Com', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Qual', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Horário', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Docto De', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Docto Para', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Prog.Ordenado', 'visible': true, 'searchable': true, 'sortable': true },


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
    $scope.RoteiroExibir = function (pFiltro) {
        $rootScope.routeloading = true;
        $scope.Roteiros = [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();
        httpService.Post('ConsultaVeiculacaoGet',pFiltro).then(function (response) {
            if (response) {
                $scope.ShowFilter = false;
                $scope.Roteiros = response.data;
                if ($scope.Roteiros.length == 0) {
                    $scope.RepeatFinished();
                }
                localStorage.setItem('ConsultaVeiculacao_filter', JSON.stringify($scope.Filtro));
            }
        });
    };
    //====================Configuracao do Grid
    $scope.ConfiguraGrid = function () {
        param = {};
        param.language = fnDataTableLanguage();
        param.lengthMenu = [[7, 10, 25, 50, -1], [7, 10, 25, 50, "Todos"]];
        param.pageLength = 10;
        param.scrollCollapse = true;
        param.paging = true;
        param.dom = "<'row'<'col-sm-6'B><'col-sm-3'l><'col-sm-3'f>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        param.buttons = [
            {
                text: 'Abrir no Excel<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning', extend: 'excel', exportOptions: {
                    columns: ':visible:not(:first-child)'
                }
            },
            { text: 'Retornar' , className: 'btn btn-info', action: function (e, dt, button, config) { $('#btnVoltar').click(); } },
        ];
        param.order = [[1, 'asc'], [0, 'asc']];
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
        $scope.ConfiguraGrid();
        if (_Filter) {
            $scope.Filtro = _Filter;
        }
    });

}]);

