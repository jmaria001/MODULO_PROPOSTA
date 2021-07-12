angular.module('App').controller('CalculoValoracaoController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {


    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    //====================Inicializa scopes
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.Contratos = [];

    $scope.NewContrato = function () {
        return {
            'Cod_Empresa': '',
            'Numero_Mr': '',
            'Sequencia_Mr': ''
        };
    };

    $scope.NewNegociacao = function () {
        return {
            'Competencia': '',
            'Numero_Negociacao': '',
            'Mensagem': ''
        };
    };


    $scope.Negociacao = $scope.NewNegociacao();

    //===========================Adicionar Linhas de Contrato
    $scope.AdicionarCalculoValoracao = function () {
        $scope.Contratos.push($scope.NewContrato());
    }


    //=======================Salvar Calculo de Valoracao
    $scope.SalvarCalculoValoracao = function (pContrato, pNegociacao) {
        if (pContrato.length == 0 && !pNegociacao) {
            ShowAlert("Contratos, Negociação ou Competência não foram definidos !");
            return
        }
        if (pNegociacao.Competencia || pNegociacao.Numero_Negociacao) {
            httpService.Post("ValoracaoContratosNego", pNegociacao).then(function (response) {
                if (response) {
                    if (response.data[0].Qtd > 0) {
                        $scope.Negociacao.Mensagem = response.data[0].Qtd + ' Contratos(s) adicionado(s) a Fila de Valoração'
                    }
                    else {
                        $scope.Negociacao.Mensagem = 'Nenhum contrato foi encontrado para a Competência/Negociação'
                    };
                };
            });
        };

        if (pContrato.length > 0) {
            httpService.Post("ValoracaoContratos", pContrato).then(function (response) {
                if (response) {
                    if (response.data) {
                        $scope.Contratos = response.data;
                    };
                };
            });
        };
    };
    $scope.CancelaEdicao = function () {
        $scope.Contratos = [];
        $scope.Negociacao = $scope.NewNegociacao();
    }
    $scope.CalculoValoracaoExcluir = function (pContrato) {

        for (var i = 0; i < $scope.pContrato.length; i++) {

            if ($scope.Contratos[i].Cod_Empresa == pContrato.Cod_Empresa && $scope.Contratos[i].Numero_Mr == pContrato.Numero_Mr && $scope.Contratos[i].Sequencia_Mr == pContrato.Sequencia_Mr) {
                $scope.Contratos.splice(i, 1);
                break;
            };
        };
    };

}]);

