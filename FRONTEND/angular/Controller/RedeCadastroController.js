angular.module('App').controller('RedeCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Variaveis
    $scope.currentTab = "Dados";
    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;
    $scope.PermissaoDesativar = false;
    $scope.PermissaoExcluir = false;
    httpService.Get("credential/Rede@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/Rede@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });
    httpService.Get("credential/Rede@Destroy").then(function (response) {
        $scope.PermissaoExcluir = response.data;
    });
    httpService.Get("credential/Rede@Activate").then(function (response) {
        $scope.PermissaoDesativar = response.data;
    });

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.Rede = "";
    console.log($scope.Parameters);

    //==========================Busca dados da Rede
    $scope.CarregaDados = function () {
        var _url = "GetRedeData/" + $scope.Parameters.Id;
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Rede = response.data;
            }
        });
    }
    $scope.CarregaDados();
    //==========================Salvar
    $scope.SalvarRede = function (pRede) {
        //if ($scope.Parameters.Action == "New")
        //{
        //    $scope.Veiculo.id_operacao = 'I';Parameters.Action
        //}
        //$scope.TipoMidia.id_operacao = $scope.sw == "New" ? 'I' : 'E';
        $scope.Rede.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("SalvarRede", pRede).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    if ($scope.Parameters.Action == 'New') {
                        $scope.CarregaDados();
                    }
                    else {
                        $location.path("/Rede")
                    }
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };

    //======================Excluir
    //$scope.excluirtipomidia = function (pTipoMidia) {

    //    swal({
    //        title: "Tem certeza que deseja Excluir este Tipo Midia ?",
    //        //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
    //        type: "warning",
    //        showCancelButton: true,
    //        confirmButtonClass: "btn-danger",
    //        confirmButtonText: "Sim, Excluir?",
    //        cancelButtonText: "Cancelar",
    //        closeOnConfirm: true
    //    }, function () {
    //        httpService.Post("excluirtipomidia", pTipoMidia).then(function (response) {
    //            if (response) {

    //                if (response.data[0].Status) {
    //                    ShowAlert(response.data[0].Mensagem, 'success');
    //                    $location.path("/TipoMidia");
    //                }
    //                else {
    //                    ShowAlert(response.data[0].Mensagem, 'warning');
    //                }
    //            }
    //        })
    //    });

    //};

}]);
