angular.module('App').controller('RegraAprovacao_Controller', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location) {

    //====================Inicializa scopes
    $scope.CurrentShow = "Grid";
    $scope.Regras = [];
    $scope.gridheaders = [  { 'title': 'Edit', 'visible': true, 'searchable': false, 'config': false, 'sortable': false},
                            { 'title': 'Id', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Nome da Regra', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Descrição da Regra', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Regra', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Delete', 'visible': true, 'searchable': false, 'config': false, 'sortable': false },

    ];

    //====================Permissoes
    $scope.PermissaoExcluir = 'false';
    $scope.PermissaoEditar= 'false';
    httpService.Get("credential/Aprovacao@Edit").then(function (response) {
        $scope.PermissaoEditar= response.data;
    });
    httpService.Get("credential/Aprovacao@Destroy").then(function (response) {
        $scope.PermissaoExcluir = response.data;
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
    $scope.CarregarRegra = function () {
        $scope.Regras = [];
        $scope.CurrentShow = '';
        $('#dataTable').dataTable().fnDestroy();
        httpService.Get('RegraAprovacaoListar/').then(function (response) {
            if (response) {
                $scope.Regras= response.data;
                if ($scope.Regras.length == 0) {
                    $scope.RepeatFinished();
                }
            } 
        });
    };

    //==================== Edicao da Regra
    $scope.EditarRegra= function (pIdRegra) {
        $location.path("/RegraCadastro/Edit/" + pIdRegra)
    }
    //==================== Exclusao da Regra
    $scope.ExcluirRegra= function (pIdRegra) {
        swal({
            title: "Tem certeza que deseja Excluir essa Regra?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim,Excluir",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post('ExcluirRegraAprovacao', { 'Id_Regra': pIdRegra }).then(function (response) {
                if (response) {
                    $scope.CarregarRegra();
                    $scope.CurrentShow = "Grid";
                }
            });
        });
    };

    //====================Configuracao do Grid
    $scope.ConfiguraGrid = function () {
        param = {};
        param.language = fnDataTableLanguage();
        param.lengthMenu = [[7, 10, 25, 50, -1], [7, 10, 25, 50, "Todos"]];
       
        param.scrollCollapse = true;
        param.paging = true;
        param.dom = "<'row'<'col-sm-6'B><'col-sm-3'l><'col-sm-3'f>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        param.buttons = [
            { text: 'Nova Regra'+'<span class="fa fa-file margin-left-10"></span>', className: 'btn btn-primary btnNew', action: function (e, dt, button, config) { $('#btnNovaRegra').click(); } },
            {
                text: 'Exportar<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning', extend: 'excel', exportOptions: {
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
        var table = $('#dataTable').DataTable();
        var buttons = table.buttons([1]);
        if (table.rows({ selected: true }).indexes().length === 0) {
            buttons.disable();
        }
        else {
            buttons.enable();
        }
    };

    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.CarregarRegra();
    });

    $scope.NovaRegra= function (){
        $location.path("/regracadastro/New/0");
    }
    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });
}]);


