angular.module('App').controller('MenuDashboardController', ['$scope', '$rootScope', 'httpService', '$location', function ($scope, $rootScope, httpService, $location) {

    //===================Inicializa Scopes
    $scope.MenuDashboards = [
        { 'Link': 'FunilVendas', 'Title': 'Funil de Vendas' },
        { 'Link': 'VendasPeriodo', 'Title': 'Vendas no Período' },
        { 'Link': 'EvolucaoVendas', 'Title': 'Evolução de Vendas' },
    ];
        //===========================fim do load da pagina
        $scope.$watch('$viewContentLoaded', function () {
        });
}]);

