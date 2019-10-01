angular.module('App').controller('PacoteDesconto_List_Controller', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {

    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;
    $scope.PermissaoDelete= false;

    httpService.Get("credential/Pacote@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/Pacote@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });
    httpService.Get("credential/Pacote@Destroy").then(function (response) {
        $scope.PermissaoDelete = response.data;
    });
    

    //====================Inicializa scopes
    $scope.ShowGrid = false;
    $scope.gridheaders = [  { 'title': '', 'visible': true, 'searchable': false, 'sortable': false },
                            { 'title': 'Id', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Descricao', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Inicio Validade', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Termino Validade', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': '', 'visible': true, 'searchable': false, 'sortable': false },
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
    $scope.CarregarPacote= function () {
        $rootScope.routeloading = true;
        $scope.Pacotes= [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();
        httpService.Get('PacoteListar').then(function (response) {
            if (response) {
                $scope.Pacotes = response.data;
                if ($scope.Pacotes.length == 0) {
                    $scope.RepeatFinished();
                }
            }
        });
    };

    //=================RemoverPacote
    $scope.RemoverPacote = function (pPacote) {
        swal({
            title: "Tem certeza que deseja Excluir esse Pacote ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post('ExcluirPacote', pPacote).then(function (response) {
                $scope.CarregarPacote();
            });
            
        });
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
                    text: 'Exportar<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning HideButton', extend: 'excel', filename:'Pacote Desconto',  title:'Pacote de Descontos', exportOptions: {
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
        $scope.CarregarPacote();
    });

}]);

