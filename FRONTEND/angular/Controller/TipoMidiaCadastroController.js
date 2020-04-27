angular.module('App').controller('TipoMidiaCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.TipoMidia = "";
    //========================Verifica Permissoes
    $scope.PermissaoDelete = false;
    httpService.Get("credential/TipoMidia@Destroy").then(function (response) {
        $scope.PermissaoDelete = response.data;
    });
    //==========================Busca dados do Tipo Midia
    $scope.CarregaDados = function () {
        var _url = "GetTipoMidiaData/" + $scope.Parameters.Id;
        httpService.Get(_url.trim()).then(function (response) {
            if (response) {
                $scope.TipoMidia = response.data;
            }
        });
    }
    $scope.CarregaDados();
    //==========================Salvar
    $scope.SalvarTipoMidia = function (pTipoMidia) {
        //if ($scope.Parameters.Action == "New")
        //{
        //    $scope.Veiculo.id_operacao = 'I';Parameters.Action
        //}
        //$scope.TipoMidia.id_operacao = $scope.sw == "New" ? 'I' : 'E';
        $scope.TipoMidia.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("SalvarTipoMidia", pTipoMidia).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/TipoMidia")
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };

    //======================Excluir
    $scope.excluirtipomidia = function (pTipoMidia) {

        swal({
            title: "Tem certeza que deseja Excluir este Tipo Midia ?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("excluirtipomidia", pTipoMidia).then(function (response) {
                if (response) {

                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/TipoMidia");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });

    };

}]);

