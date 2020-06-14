angular.module('App').controller('GradeController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {

    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;
    httpService.Get("credential/Grade@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/Grade@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });

    //===================Declarar scopes
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.ShowGrid= false;
    $scope.ShowFilter = true;
    $scope.Filtro = "";
    $scope.NewFilter = function () {
        $scope.Grades = [];
        $scope.ShowFilter = true;
        $scope.ShowGrid = false;
        $scope.Filtro ={'Cod_Veiculo':'','Competencia':''};
    };
    $scope.NewFilter();
    //===================Carregar o grid
    $scope.CarregarGrade = function (pFiltro) {
        $rootScope.routeloading = true;
        $scope.Grades = [];
        $scope.ShowGrid= '';
        var _url = "Grade/List";
        _url += "?Cod_Veiculo=" + pFiltro.Cod_Veiculo;
        _url += "&Competencia=" + pFiltro.Competencia;
        _url += "&";
        httpService.Get(_url).then(function (response) {
            console.log(response);
            if (response.data.Dias.length>0) {
                $scope.Grades = response.data;
                $scope.ShowGrid = true;
                $scope.ShowFilter = false;
            }
            else {
                ShowAlert("Não existe Grade para esse Veículo/Competência", "warning");
            }
        });
    }
}]);

