angular.module('App').controller('VeiculoCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.Veiculo = "";
    //========================Verifica Permissoes
    
    $scope.PermissaoDelete= false;
    httpService.Get("credential/Veiculo@Destroy").then(function (response) {
        $scope.PermissaoDelete = response.data;
    });
    //==========================Busca dados dos Veiculo

    var _url = "GetVeiculoData/" + $scope.Parameters.Id;
    httpService.Get(_url).then(function (response) {
        if (response) {
            $scope.Veiculo = response.data;
        }
    });

    //==========================Salvar
    $scope.SalvarVeiculo = function (pVeiculo) {
        $scope.Veiculo.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("SalvarVeiculo", pVeiculo).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/veiculo");
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };

    //======================Excluir
    $scope.ExcluirVeiculo = function (pVeiculo) {

        swal({
            title: "Tem certeza que deseja Excluir esta  Veiculo ?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("ExcluirVeiculo", pVeiculo).then(function (response) {
                if (response) {

                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/veiculo");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });

    };
 }]);

