angular.module('App').controller('CondPgtoCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.CondPgto = "";
    //========================Verifica Permissoes
    $scope.PermissaoDelete= false;
    httpService.Get("credential/CondPgto@Destroy").then(function (response) {
        $scope.PermissaoDelete = response.data;
    });
    //==========================Busca dados do Condições de Pagamentos
    var _url = "GetCondPgtoData/" + $scope.Parameters.Id;
    httpService.Get(_url).then(function (response) {
        if (response) {
            $scope.CondPgto = response.data;
        }
    });


    //==========================Salvar
    $scope.SalvarCondPgto = function (pCondPgto) {
        $scope.CondPgto.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("SalvarCondPgto", pCondPgto).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/CondPgto");
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };

    //======================Excluir
    $scope.ExcluirCondPgto = function (pCondPgto) {

        swal({
            title: "Tem certeza que deseja Excluir este Condição de Pagamento ?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("ExcluirCondPgto", pCondPgto).then(function (response) {
                if (response) {

                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/CondPgto");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });

    };

}]);

