angular.module('App').controller('NaturezadeServicoCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {
                                  
    //========================Recebe Parametro
    $scope.PesquisaTabelas = { "Items": [], 'FiltroTexto': '', ClickCallBack: '', 'Titulo': '', 'MultiSelect': false };
    $scope.Parameters = $routeParams;
    $scope.NaturezadeServico = "";

    $scope.PermissaoDesativar = false;

    
    //========================Verifica Permissoes

    $scope.PermissaoDelete = false;
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;

     
    httpService.Get("credential/Naturezade@Destroy").then(function (response) {
        $scope.PermissaoDelete = response.data;
    });

    httpService.Get("credential/Naturezade@Activate").then(function (response) {
        $scope.PermissaoDesativar = response.data;
    });



    //==========================Busca dados dos Natureza de Servico

    //===============================Modo Edit

    $scope.GetNaturezadeServicoData = function () {

        var _url = 'GetNaturezadeServicoData'
        _url += '?Cod_Natureza=' + $scope.Parameters.Cod_Natureza;
        _url += '&Cod_Empresa=' + $scope.Parameters.Cod_Empresa;
        _url += '&';

        httpService.Get(_url).then(function (response) {
            if (response) {

                $scope.NaturezadeServico = response.data;
                if (response.data.length == 0) {
                    ShowAlert("Não existe natureza");
                    return;
                }


                if ($scope.Parameters.Action != "Edit") {
                    $scope.NaturezadeServico.Percentual_Iss = "";
                    $scope.NaturezadeServico.Perc_IR = "";
                    $scope.NaturezadeServico.Perc_CS = "";
                    $scope.NaturezadeServico.Perc_COFINS = "";
                    $scope.NaturezadeServico.Perc_PIS = "";
                    $scope.NaturezadeServico.PERC_INSS = "";
                }

                if ($scope.Parameters.Action == "Edit" && $scope.NaturezadeServico.Percentual_Iss == 0) { $scope.NaturezadeServico.Percentual_Iss = "" };
                if ($scope.Parameters.Action == "Edit" && $scope.NaturezadeServico.Perc_IR == 0) { $scope.NaturezadeServico.Perc_IR = ""};
                if ($scope.Parameters.Action == "Edit" && $scope.NaturezadeServico.Perc_CS == 0) { $scope.NaturezadeServico.Perc_CS = "" };
                if ($scope.Parameters.Action == "Edit" && $scope.NaturezadeServico.Perc_COFINS == 0) { $scope.NaturezadeServico.Perc_COFINS = "" };
                if ($scope.Parameters.Action == "Edit" && $scope.NaturezadeServico.Perc_PIS == 0) { $scope.NaturezadeServico.Perc_PIS = "" };
                if ($scope.Parameters.Action == "Edit" && $scope.NaturezadeServico.PERC_INSS == 0) { $scope.NaturezadeServico.PERC_INSS = "" };

            }
        });
    };


    ////==========================Salvar
    $scope.SalvarNaturezadeServico = function (pNaturezadeServico) {
        $scope.NaturezadeServico.Id_operacao = $scope.Parameters.Action == "Edit" ? 'E' : 'I';


        if (pNaturezadeServico.Cod_Natureza == null && pNaturezadeServico.Cod_Natureza == undefined) {

            ShowAlert("Código da Natureza de Serviços não pode ficar em branco");
            return;
        }


        if (pNaturezadeServico.Descricao == null && pNaturezadeServico.Descricao == undefined) {

            ShowAlert("Descrição da Natureza de Serviços não pode ficar em branco");
            return;
        }


        if (pNaturezadeServico.Cod_Historico == null && pNaturezadeServico.Cod_Historico == undefined) {

            ShowAlert("Número de Fita não pode ficar em branco");
            return;
        }


        if (pNaturezadeServico.Indica_Iss == true && (pNaturezadeServico.Percentual_Iss == null || pNaturezadeServico.Percentual_Iss == "")) {

            ShowAlert("Percentual de Iss não pode ficar em branco");
            return;
        }

        if (pNaturezadeServico.Indica_Iss == true && parseInt(pNaturezadeServico.Percentual_Iss) > 100) {

            ShowAlert("Percentual de Iss não pode ser maior que 100%");
            return;
        }

        if (pNaturezadeServico.Indica_NFE == true && pNaturezadeServico.Indica_NFEE == true         ) {

            ShowAlert("Não é permitido marcar as duas opções de Nota Fiscal Eletrônica");
            return;
        }

        httpService.Post("SalvarNaturezadeServico", pNaturezadeServico).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/NaturezadeServico");
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };


    //======================Excluir
           
    $scope.ExcluirNaturezadeServico = function (pNaturezadeServico) {

        swal({
            title: "Tem certeza que deseja Excluir esta  Natureza de Serviço ?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
                              
            httpService.Post("ExcluirNaturezadeServico", pNaturezadeServico).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/NaturezadeServico");
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
            })
        });

    };

    //====================Desativar
    $scope.DesativarReativarNaturezadeServico = function (pCod_Natureza,pCod_Empresa, pAction) {
        swal({
            title: "Tem certeza que deseja " + (pAction == 'D' ? "Desativar" : "Reativar") + " esse Natureza de Serviço ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim",
            cancelButtonText: "Não",
            closeOnConfirm: true
        }, function () {
                _Data = { 'Cod_Natureza': pCod_Natureza, 'Cod_Empresa': pCod_Empresa, 'Id_Acao': pAction }
            httpService.Post('DesativarReativarNaturezadeServico', _Data).then(function (response) {
                if (response) {
                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/NaturezadeServico");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            });
        });
    };




    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $rootScope.routeloading = false;
        $scope.GetNaturezadeServicoData();
    });



    




}]);

