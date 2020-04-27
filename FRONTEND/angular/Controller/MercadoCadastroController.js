angular.module('App').controller('MercadoCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.Mercado = "";
    //========================Verifica Permissoes
    $scope.PermissaoDestroy = false;
    httpService.Get("credential/Mercado@Destroy").then(function (response) {
        $scope.PermissaoDestroy = response.data;
    });
    //==========================Busca dados do Mercado
    var _url = "GetMercadoData/" + $scope.Parameters.Id.trim();
    httpService.Get(_url).then(function (response) {
        if (response) {
            $scope.Mercado = response.data;
        }
    });

    //==========================Salvar
    $scope.SalvarMercado = function (pMercado) {
        $scope.Mercado.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("SalvarMercado", pMercado).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/mercado");
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };

    //======================Excluir
    $scope.ExcluirMercado = function (pMercado) {

        swal({
            title: "Tem certeza que deseja Excluir este Mercado ?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?" ,
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("ExcluirMercado", pMercado).then(function (response) {
                if (response) {

                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/mercado");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });

    };
}
]);

