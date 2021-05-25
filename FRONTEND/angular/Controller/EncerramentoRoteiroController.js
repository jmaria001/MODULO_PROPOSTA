angular.module('App').controller('EncerramentoRoteiroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout) {

    //---------------------- Inicializa Scope -------------------------
    $scope.Filtro = { 'Cod_Veiculo': '', 'Nome_Veiculo': '', 'Data': '' };

    //---------------------- Novo Filtro ------------------------
    $scope.NewFilter = function () {
        $scope.Filtro = { 'Cod_Veiculo': '', 'Nome_Veiculo': '', 'Data': '' };
    };

    //---------------------- Encerra Roteiro -----------------------
    $scope.Encerrar = function (pFiltro) {
        if (!pFiltro.Cod_Veiculo) {
            ShowAlert("O Veículo é Obrigatório");
            return;
        }
        if (!pFiltro.Data) {
            ShowAlert("A Data é Obrigatória");
            return;
        }

        httpService.Post('Roteiro/EncerrarRoteiro', pFiltro).then(function (response) {
            if (response) {
                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $scope.NewFilter();
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                    $scope.NewFilter();
                }
            }
        });
    };


}]);

