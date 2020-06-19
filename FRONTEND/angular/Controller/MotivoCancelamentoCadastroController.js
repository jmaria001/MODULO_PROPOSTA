angular.module('App').controller('MotivoCancelamentoCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Variaveis
    $scope.currentTab = "Dados";
    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;
    $scope.PermissaoDesativar = false;
    $scope.PermissaoExcluir = false;
    httpService.Get("credential/MotivoCancelamento@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/MotivoCancelamento@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });
    httpService.Get("credential/MotivoCancelamento@Destroy").then(function (response) {
        $scope.PermissaoExcluir = response.data;
    });
    httpService.Get("credential/MotivoCancelamento@Activate").then(function (response) {
        $scope.PermissaoDesativar = response.data;
    });

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.MotivoCancelamento = "";
    //==========================Busca dados da MotivoCancelamento
    $scope.CarregaDados = function () {
        var _url = "GetMotivoCancelamentoData/" + $scope.Parameters.Id;
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.MotivoCancelamento = response.data;
            }
        });
    }
    $scope.CarregaDados();
    //==========================Salvar
    $scope.SalvarMotivoCancelamento = function (pMotivoCancelamento) {
        //if ($scope.Parameters.Action == "New")
        //{
        //    $scope.Veiculo.id_operacao = 'I';Parameters.Action
        //}
        //$scope.TipoMidia.id_operacao = $scope.sw == "New" ? 'I' : 'E';
        $scope.MotivoCancelamento.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("SalvarMotivoCancelamento", pMotivoCancelamento).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/MotivoCancelamento")
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };

    //======================Excluir
    $scope.ExcluirMotivoCancelamento = function (pMotivoCancelamento) {

        swal({
            title: "Tem certeza que deseja Excluir este Motivo de Cancelamento ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("ExcluirMotivoCancelamento", pMotivoCancelamento).then(function (response) {
                if (response) {

                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/MotivoCancelamento");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });

    };

}]);
