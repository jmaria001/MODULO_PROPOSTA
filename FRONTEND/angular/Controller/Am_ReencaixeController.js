angular.module('App').controller('Am_ReencaixeController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;
    $scope.PermissaoExcluir = 'false';
    $scope.PermissaoMostraVeiculo = false;
    //====================Inicializa scopes
    //$scope.ShowGrid = false;
    $scope.checkBoxReencaixe = false;
    $scope.Parameters = $routeParams;
    $scope.Solucao = [];

    $scope.gridheaders = [{ 'title': '', 'visible': true, 'searchable': false, 'sortable': false },
    { 'title': 'Data', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Programa', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Com.', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Titulo', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Dur.', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Documento', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Qual', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Valor', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Ch.ACesso', 'visible': true, 'searchable': true, 'sortable': true },
    { 'title': 'Veiculo', 'visible': true, 'searchable': true, 'sortable': true },
    ];

    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.CurrentShow = 'Grid';
        //$scope.ShowGrid = true;
        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        }, 10)
    };

    //====================Carrega o Grid
    $scope.CarregarReencaixe = function () {
        $rootScope.routeloading = true;
        $scope.Amrs = [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();

        var _url = "AM/AmReencaixe"
        _url += "?Cod_Empresa=" + $scope.Parameters.Cod_Empresa;
        _url += "&Numero_Mr=" + $scope.Parameters.Numero_Mr;
        _url += "&Sequencia_Mr=" + $scope.Parameters.Sequencia_Mr;
        _url += "&Documento_Para=" + $scope.Parameters.Numero_Docto;
        _url += "&Competencia=" + $scope.Parameters.Competencia;
        _url += "&Cod_Veiculo=" + $scope.Parameters.Cod_Veiculo;
        _url += "&";

        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Amrs = response.data;
                if ($scope.Amrs.length == 0) {
                    $scope.RepeatFinished();
                }
            }
        });
    }
    // Definindo o reencaixe após feito as escolhas 
    $scope.EfetuarReencaixe = function (pParam) {

        swal({
            title: "confirma o reencaixe das falhas TQP?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Reencaixe",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
                var _data = [];
                for (var i = 0; i < pParam.length; i++) {
                    if (pParam[i].Selected == true) {

                        _data.push({
                            'Cod_Veiculo': pParam[i].Cod_Veiculo,
                            'Data_Exibicao': pParam[i].Data_Exibicao,
                            'Cod_Programa': pParam[i].Cod_Programa,
                            'Chave_Acesso': pParam[i].Chave_Acesso,
                            'Cod_Empresa': $scope.Parameters.Cod_Empresa,
                            'Numero_Mr': $scope.Parameters.Numero_Mr,
                            'Sequencia_Mr': $scope.Parameters.Sequencia_Mr,
                        });

                        //httpService.Post("AM/EfetuarReencaixe", _data).then(function (response) {
                        //    if (response) {
                        //        if (response.data[0].Status == 1) {
                        //            ShowAlert(response.data[0].Mensagem);
                        //            $scope.CarregarReencaixe();
                        //        }
                        //        //else {

                        //        //    $scope.CarregarReencaixe();
                        //        //}
                        //    }
                        //});

                    };
                };
                httpService.Post("AM/EfetuarReencaixe", _data).then(function (response) {
                    if (response) {
                        if (response.data) {
                            ShowAlert("Reencaixe completado com Sucesso");
                            $scope.CarregarReencaixe();
                        }
                        else {

                            ShowAlert("Houve erro durante o reencaixe, Verifique.")
                        }
                    }
                });
                //if (pParam.length == 0)
                //{
                //    $scope.CarregarReencaixe();
                //}

        });
    }




    
    //====================Funcao para configurar o Grid
    $scope.ConfiguraGrid = function () {
        param = {};
        param.language = fnDataTableLanguage();
        param.lengthMenu = [[7, 10, 25, 50, -1], [7, 10, 25, 50, "Todos"]];
        param.pageLength = 10;
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
        param.order = [[0, 'asc']];
        param.autoWidth = false;
        param.columns = [];
        for (var i = 0; i < $scope.gridheaders.length; i++) {
            param.columns.push({ "visible": $scope.gridheaders[i].visible, "searchable": $scope.gridheaders[i].searchable, "sortable": $scope.gridheaders[i].sortable });
        }
        $('#dataTable').DataTable(param);
    };

    //=====================Marcar / Desmarcar todos os Reencaixe
    $scope.MarcaReencaixe = function () {
        for (var i = 0; i < $scope.Amrs.length; i++) {
            $scope.Amrs[i].Selected = $scope.checkBoxReencaixe;
        }
    }


    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.ConfiguraGrid();
        $scope.CarregarReencaixe();
    });

}]);

