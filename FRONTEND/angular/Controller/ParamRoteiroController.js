angular.module('App').controller('ParamRoteiroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {
    //====================Inicializa scopes
    $scope.ShowGrid = false;
    $scope.ShowFilter = true;
    $scope.ParamRoteiros = "";
    $scope.ParamRoteiro = {'Cod_Veiculo':'','Nome_Veiculo':''};
    $scope.NewFilter = function (fromButtom) {
        $scope.ParamRoteiro = { 'Cod_Veiculo': '', 'Nome_Veiculo': '' };
        $scope.ParamRoteiros = [];
        $scope.ShowFilter = true;
    };
    $scope.NewFilter();

    //====================Carrega o Grid
    $scope.ParamRoteirosCarregar = function (pParamRoteiro) {
        $rootScope.routeloading = true;
        $scope.ParamRoteiros = [];
        $scope.ShowGrid = '';
        $('#dataTable').dataTable().fnDestroy();
        var _url = "ParamRoteiroFiltrar";
        _url += "?Cod_Veiculo=" + pParamRoteiro.Cod_Veiculo;
        _url += "&=";
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.ParamRoteiros = response.data;
                $scope.ShowGrid = true;
                $scope.ShowFilter = false;
            }
        });
    };

    $scope.SalvarAlteracoes = function (pParametro) {
        httpService.Post("ParamRoteiroSalvar",pParametro).then(function (response) {
            if (response.data ){
                ShowAlert(response.data[0].Mensagem)
                if (response.data[0]==1) {
                    $scope.NewFilter();
                }
            }
        });
    };

    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
    });

}]);

