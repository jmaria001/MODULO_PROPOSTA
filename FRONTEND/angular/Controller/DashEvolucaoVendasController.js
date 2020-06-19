angular.module('App').controller('DashEvolucaoVendasController', ['$scope', '$rootScope', 'httpService', function ($scope, $rootScope, httpService) {

    var chart = null; //essa variavel é necessária
    //===========================Inicializa Scopes 
    $scope.Filtro = { 'Competencia_Inicio': CurrentMMYYYY(-5), 'Competencia_Fim': CurrentMMYYYY(0), 'Postipo': 'Contato', 'Indicador': '1' }
    $scope.CompetenciaInicioKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.CompetenciaInicioKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.CompetenciaFimKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.DashboardData = "";
    $scope.SetaCompetenciaFinalKey = function () {
        $scope.ShowBarra = true;
    }

    $scope.PosTipo = [
        { 'id': 'Contato', 'descricao': 'Contato' },
        { 'id': 'Rede', 'descricao': 'Rede' },
        { 'id': 'EmpresaVenda', 'descricao': 'Empresa de Vendas' }
    ]

    $scope.Indicador = [
        { 'id': '1', 'descricao': 'Quantidade' },
        { 'id': '2', 'descricao': 'Valor' }
    ]

    //===================================Mudou a competencia Inicio 
    $scope.$watch('Filtro.Competencia_Inicio', function (newValue, oldValue) {
        if (newValue != oldValue) {

            var _ano = newValue.substr(3, 4);
            var _mes = newValue.substr(0, 2);
            var _anomes = _ano + _mes
            $scope.CompetenciaFimKeys = { 'Year': parseInt(_ano), 'First': parseInt(_anomes), 'Last': '' }
            $scope.CarregarGrafico($scope.Filtro);
        }
    });
    //===================================Mudou a competencia Fim 
    $scope.$watch('Filtro.Competencia_Fim', function (newValue, oldValue) {
        if (newValue != oldValue) {
            $scope.CarregarGrafico($scope.Filtro);
        }
    });
    //====================Mudou o grupo
    $scope.GrupoChange = function (pFiltro) {
        $scope.CarregarGrafico(pFiltro);
    }
    //==========================Monta o Grafico ---essa precisa para montar o grafico
    $scope.SetGrap = function (pData, pGraph) {
        var ctx = document.getElementById(pGraph).getContext('2d');
        if (chart != null) {
            chart.destroy();
        }
        chart = new Chart(ctx, pData);
    }
    //==========================Carrega Dados para Grafico de Evolução de Vendas
    $scope.CarregarGrafico = function (pFiltro) {
        httpService.Post("DashBoard/EvolucaoVendas", pFiltro).then(function (response) {
            $scope.ShowBarra = false;
            if (response) {
                if (response.data.data.datasets.length > 0) {
                    $scope.ShowBarra = true;
                }
                $scope.DashboardData = response.data;
                if (pFiltro.Indicador == "2") {
                    FormatChart($scope.DashboardData, "MONEY");
                }
                else {
                    FormatChart($scope.DashboardData, "NUMBER");
                }
                $scope.SetGrap($scope.DashboardData, 'chart_Evolucao');
            }
        });
    }
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.CarregarGrafico($scope.Filtro); ///quando entra ja cxarrega o grafico 
    });
}]);
