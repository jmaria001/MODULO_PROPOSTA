angular.module('App').controller('DeterminacaoTituloController', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location) {

    //============================Inicializar Scopes
    $scope.NewFilter = function(){
        return {'Cod_Empresa':'','Numero_Mr':'','Sequencia_Mr':''}
    }
    $scope.Filtro = $scope.NewFilter();
    $scope.Determinacao = "";

    //============================Carregar Comerciais
    $scope.CarregarComerciais = function (pFiltro) {
        httpService.Post("Determinacao/CarregarDados", pFiltro).then(function (response) {
            if (response.data) {
                if (response.data.length==0) {
                    ShowAlert("Não existem dados para esse filtro")
                }
                $scope.Determinacao = response.data;
            }
        });
    };
}]);


