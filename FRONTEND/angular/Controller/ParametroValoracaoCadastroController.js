angular.module('App').controller('ParametroValoracaoCadastroController', ['$scope', '$rootScope', '$routeParams', '$location', 'httpService', '$location', function ($scope, $rootScope, $routeParams, $location, httpService, $location) {
    //========================Recebe Parametro
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.ParametroValoracao = "";
    //========================Verifica Permissoes
    $scope.PermissaoDelete = false;
    httpService.Get("credential/ParametroValoracao@Destroy").then(function (response) {
        $scope.PermissaoDelete = response.data;
    });

    //============ Inicializa Variaveis Scopes
    $scope.Parameters = $routeParams;
    $scope.PesquisaTabelas = {};
    $scope.ListaAprovadores = { "Items": [], 'FiltroTexto': '', ClickCallBack: '' };
    $rootScope.routeName = 'Parametro de Valoração - ' + $scope.Parameters.Action
    $scope.currentRange = -1;
      

    ////=======================Carrega a Parametro de Valoração
    //$scope.CarregarParametroValoracao = function (pId_ParametroValoracao) {
    //    httpService.Get("GetParametroValoracao/" + pId_ParametroValoracao).then(function (response) {
    //        if (response.data) {
    //            $scope.ParametroValoracao = response.data;
    //        }
    //    });
    //};
    //$scope.CarregarParametroValoracao($scope.Parameters.Id);

    //==========================Busca dados  Parametro de Valoração

    var _url = "GetParametroValoracaoData/" + $scope.Parameters.Id;

    httpService.Get(_url).then(function (response) {
        if (response) {
            $scope.ParametroValoracao = response.data;
        }
    });


    //=======================Adicionar Parametro de Valoração 
    $scope.AdicionarFaixa = function () {
        $scope.ParametroValoracao.Max_Id_Parametro++;
        $scope.ParametroValoracao.Parametro.push({ 'Id_Parametro': $scope.ParametroValoracao.Max_Id_Parametro });
    }
    //=======================Excluir Parametro de Valoração
    $scope.ExcluirParametro = function (pId_Parametro) {
        for (var i = 0; i < $scope.ParametroValoracao.Parametro.length; i++) {
            if ($scope.ParametroValoracao.Parametro[i].Id_Parametro == pId_Parametro) {
                $scope.ParametroValoracao.Parametro.splice(i, 1);
                break;
            }
        }
    }
 
    $scope.SalvarParametroValoracao = function (pParametroValoracao) {
        httpService.Post('SalvarParametroValoracao', $scope.ParametroValoracao).then(function (response) {
            ShowAlert(response.data[0].Mensagem, (response.data[0].Status == 1) ? 'success' : 'warning');
            if (response.data[0].Status == 1) {
                if ($scope.Parameters.Action == 'New') {
                    $scope.Parameters.Action = 'Edit';
                    $rootScope.routeName = 'Parametro de Valoração - ' + $scope.Parameters.Action
                    $scope.ParametroValoracao.Id_Parametro = response.data[0].Id_Parametro;
                }
            }
        });
    }
}]);


