angular.module('App').controller('NumeracaoCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.numeracao = "";
    $scope.PermissaoNova = false;
    $scope.ListadeEmpresas = [];
    //========================Verifica Permissoes
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }

    //==========================Busca dados da Categoria do Cliente
    $scope.CarregaDados = function () {

        var _url = "Numeracao/GetData/" + $scope.Parameters.Id;
        if ($scope.Parameters.Id == 0) {

            $scope.PermissaoNova = true;

        }
        else {

            $scope.PermissaoNova = false;
        }


        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.numeracao = response.data;

                //if ($scope.Parameters.Action == "Edit") {  $scope.numeracao.competencia_Nova = "" };

                if ($scope.numeracao.Competencia_Vigente == 0) {
                    $scope.numeracao.Competencia_Vigente = "";
                }

                if ($scope.numeracao.Numero == 0) {
                    $scope.numeracao.Numero = "";
                }
            }

        });
    }
    $scope.CarregaDados();

    //==========================Salvar Categoria do Cliente
    $scope.SalvarNumeracao = function (pnumeracao) {

        if (pnumeracao.Cod_Empresa == "") {
            ShowAlert("Empresa  não pode ficar em branco.");
            return;
        }
        $scope.numeracao.Id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("Numeracao/Salvar", pnumeracao).then(function (response) {
            if (response) {
                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');

                    if ($scope.Parameters.Action == 'New') {
                        $location.path("/numeracao");
                    }
                    else { $location.path("/numeracao"); }

                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');

                }
            }
        })
    };


}]);




