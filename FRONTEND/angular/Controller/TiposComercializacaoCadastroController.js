angular.module('App').controller('TiposComercializacaoCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.TiposComercializacao = "";
    //========================Verifica Permissoes


    $scope.PermissaoDelete    = false;
    $scope.PermissaoNew       = false;
    $scope.PermissaoEdit      = false;
    $scope.PermissaoDesativar = false;

    httpService.Get("credential/TiposComercializacao@Destroy").then(function (response) {
        $scope.PermissaoDelete = response.data;
    });

    httpService.Get("credential/TiposComercializacao@Activate").then(function (response) {
        $scope.PermissaoDesativar = response.data;
    });



    //==========================Busca dados do genero
    $scope.CarregaDados = function () {
        var _url = "GetTiposComercializacaoData/" + $scope.Parameters.Id;
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.TiposComercializacao = response.data;
                console.log($scope.Parameters.Action);       

                if (response.data.length == 0) {
                    ShowAlert("Não existe Tipo de Comercialização");
                    return;
                }

            }
        });
    }
    $scope.CarregaDados();
    //==========================Salvar
    $scope.SalvarTiposComercializacao = function (pTiposComercializacao) {
        //if ($scope.Parameters.Action == "New")
        //{
        //    $scope.Veiculo.id_operacao = 'I';Parameters.Action
        //}
        //$scope.Genero.id_operacao = $scope.sw == "New" ? 'I' : 'E';
        $scope.TiposComercializacao.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("SalvarTiposComercializacao", pTiposComercializacao).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/TiposComercializacao")
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };

    //======================Excluir
    $scope.ExcluirTiposComercializacao = function (pTiposComercializacao) {

        swal({
            title: "Tem certeza que deseja Excluir este Tipo de Comercialização?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
                httpService.Post("ExcluirTiposComercializacao", pTiposComercializacao).then(function (response) {
                if (response) {

                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/TiposComercializacao");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });

    };


    //====================Desativar/Reativar Tipos Comercializacao
    $scope.DesativarReativarTiposComercializacao = function (pCod_TiposComercializacao,pAction) {
        swal({
            title: "Tem certeza que deseja " + (pAction == 'D' ? "Desativar" : "Reativar") + " esse Tipo de Comercialização ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim",
            cancelButtonText: "Não",
            closeOnConfirm: true
        }, function () {
                _Data = { 'Cod_Tipo_Comercializacao': pCod_TiposComercializacao,  'Id_Acao': pAction }
                httpService.Post('DesativarReativarTiposComercializacao', _Data).then(function (response) {
                               
                if (response) {
                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/TiposComercializacao");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            });
        });
    };



}]);

