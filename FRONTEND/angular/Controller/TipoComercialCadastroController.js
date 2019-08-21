angular.module('App').controller('TipoComercialCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.TipoComercial = "";


    //==========================Busca dados do veiculo
    var _url = "GetTipoComercialData/" + $scope.Parameters.Id;
    httpService.Get(_url).then(function (response) {
        if (response) {
            $scope.TipoComercial = response.data;
        }
    });

    //==========================Salvar
    $scope.SalvarTipoComercial = function (pTipoComercial) {
        //if ($scope.Parameters.Action == "New")
        //{
        //    $scope.Veiculo.id_operacao = 'I';
        //}
        $scope.TipoComercial.id_operacao = $scope.sw == "New" ? 'I' : 'E';
        httpService.Post("SalvarTipoComercial", pTipoComercial).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };


}]);

