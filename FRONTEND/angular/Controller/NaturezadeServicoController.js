angular.module('App').controller('NaturezadeServicoController', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location) {
                                  
    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;

    httpService.Get("credential/Natureza@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/Naturezade@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });

    //====================Inicializa scopes

    $scope.NaturezadeServicoS = "";
    $scope.CurrentShow = "Filtro";

    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }

    $scope.gridheaders = [{ 'title': '', 'visible': true, 'searchable': false, 'config': false, 'sortable': false },
    { 'title': 'Código', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Descrição', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'código Atividade', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Status', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Mídia', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': '%ISS', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'NFep', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'NFee', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Hist.', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'IR', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'CS', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'COFINS', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'PIS', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'INSS', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Data Desativação', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Usuário', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    ];
    //====================Inicializa o Filtro
    $scope.Filtro = {};
    $scope.NewFiltro = function () {
        localStorage.removeItem('NaturezadeServico');
        return {
            'Cod_Empresa': ''
        }
    }
    //===========================Se ja tiver filtro anterior gravado
    var _Filter = JSON.parse(localStorage.getItem('NaturezadeServico'));

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
          
    $scope.CarregarNaturezadeServico = function (pFiltro) {
        
        if (!pFiltro.Cod_Empresa) {
            ShowAlert("Codigo de Empresa é um filtro obrigatório");
            return;
        }
        localStorage.setItem('NaturezadeServico', JSON.stringify($scope.Filtro));
        $scope.CurrentShow = '';
        $('#dataTable').dataTable().fnDestroy();
        var _url = 'NaturezadeServicoListar';
        _url += '?Cod_Empresa=' + pFiltro.Cod_Empresa;
        _url += '&';
        $scope.NaturezadeServicoS = "";
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.NaturezadeServicoS = response.data;
                   if ($scope.NaturezadeServicoS.length == 0) {
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
            { text: 'Nova Natureza de Serviço', className: 'btn  btn-primary', action: function (e, dt, button, config) { $('#btnNovoNaturezadeServicoCadastro').click(); } },
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
                $scope.CarregarNaturezadeServico(_Filter);
            }
        }
    });

}]);


