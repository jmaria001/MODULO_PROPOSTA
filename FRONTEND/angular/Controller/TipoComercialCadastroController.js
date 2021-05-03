angular.module('App').controller('TipoComercialCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.TipoComercial = "";

    //========================Verifica Permissoes
    $scope.PermissaoExclusao= false;
    httpService.Get("credential/TipoComercial@Destroy").then(function (response) {
        $scope.PermissaoExclusao = response.data;
    });
    //==========================Busca dados do Tipo Comercial
    var _url = "GetTipoComercialData/" + $scope.Parameters.Id;
    httpService.Get(_url.trim()).then(function (response) {
        if (response) {
            $scope.TipoComercial = response.data;
        }
    });

    //==========================Salvar
    $scope.SalvarTipoComercial = function (pTipoComercial) {
        if (pTipoComercial.Indica_Midia_On_Line == 0) {
            pTipoComercial.Cod_Tipo_Comercializacao = null;
            pTipoComercial.Desc_Tipo_Comercializacao = null;

        }

        $scope.TipoComercial.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("SalvarTipoComercial", pTipoComercial).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/TipoComercial");
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };

    //======================Excluir
    $scope.ExcluirTipoComercial = function (pTipoComercial) {

        swal({
            title: "Tem certeza que deseja Excluir este Tipo Comercial ?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("excluirtipocomercial", pTipoComercial).then(function (response) {
                if (response) {

                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/TipoComercial");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });

    };

}]);

