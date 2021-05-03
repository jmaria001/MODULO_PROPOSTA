angular.module('App').controller('CriticaValoracaoController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //====================Inicializa scopes
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.ShowFilter = false;
    $scope.ShowDados = false;
    $scope.Filtro = {};
    $scope.CriticaValoracao = "";

    //===================Novo Filtro
    $scope.NewFiltro = function () {
        $scope.Filtro = {
            'Competencia': '', 'Cod_Empresa_Faturamento': '', 'Razao_Social_Fat': '', 'Cod_Empresa': '', 'Razao_Social_Ven': '', 'Numero_Mr': '', 'Sequencia_Mr': '',
            'Id_Critica': '', 'Descricao_Critica': '',
            'Tabela_Preco': '', 'Sequencia_Tabela': '', 'Tipo_Tabela': '', 'Cod_Veiculo': '', 'Cod_Programa': '', 'Cod_Tipo_Comercial': '', 'Duracao': '',
        };
    }

    //====================Filtrar
    $scope.FiltrarCriticaValoracao = function (pFiltro) {
        //if (!pFiltro.Cod_Empresa_Faturamento) {
        //    ShowAlert("Cod. Empresa Faturamento é obrigatório");
        //    return;
        //}
        var _url = 'CriticaValoracaoGet'
        _url += '?Competencia=' + (pFiltro.Competencia ? pFiltro.Competencia : "");
        _url += '&Cod_Empresa_Faturamento=' + NullToString(pFiltro.Cod_Empresa_Faturamento);
        _url += '&Cod_Empresa=' + NullToString(pFiltro.Cod_Empresa);
        _url += '&Numero_Mr=' + pFiltro.Numero_Mr;
        _url += '&Sequencia_Mr=' + pFiltro.Sequencia_Mr;
        _url += '&';
        httpService.Get(_url).then(function (response) {
            if (response.data.length>0) {
                $scope.CriticaValoracao = response.data;
                $scope.ShowFilter = false;
                $scope.ShowDados = true;
            }
            else {
                ShowAlert("Nenhum registro foi selecionado com esse filtro.")
            };
        });
    };
    $scope.ShowFilter = true;
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        if ($routeParams.Numero_Mr) {
            $scope.ShowFilter = false;
            $scope.Filtro = {
                'Cod_Empresa': $rootScope.Cod_Empresa,
                'Numero_Mr': $routeParams.Numero_Mr,
                'Sequencia_Mr': $routeParams.Sequencia_Mr
            };
            $scope.FiltrarCriticaValoracao($scope.Filtro);
        }
    });

}]);

