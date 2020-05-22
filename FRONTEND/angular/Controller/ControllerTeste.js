angular.module('App').controller('ControllerTeste', ['$scope', '$rootScope', 'httpService', function ($scope, $rootScope, httpService) {

    var chart = null; //essa variavel é necessária
    //===========================Inicializa Scopes 
    //$scope.Filtro = { 'CompetenciaInicio': CurrentMMYYYY(0), 'CompetenciaFim': CurrentMMYYYY(12) }
    $scope.Filtro = { 'CompetenciaInicio': '01/2020', 'CompetenciaFim': '12/2020' }
    $scope.CompetenciaInicioKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    
    $scope.CompetenciaInicioKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }

    $scope.CompetenciaFimKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.DashboardData = "";
    $scope.Show = 'Barra'
    $scope.SetaCompetenciaFinalKey = function () {
        
    }
    //===================================Mudou a competencia Inicio 
    $scope.$watch('Filtro.CompetenciaInicio', function (newValue, oldValue) {
        if (newValue) {
            var _ano = newValue.substr(3, 4);
            var _mes = newValue.substr(0, 2);
            var _anomes = _ano + _mes
            $scope.CompetenciaFimKeys = { 'Year': parseInt(_ano), 'First': parseInt(_anomes), 'Last': '' }
        }
    });
    //==========================Monta o Grafico ---essa precisa para montar o grafico
    $scope.SetGrap = function (pData,pGraph) {
        var ctx = document.getElementById(pGraph).getContext('2d');
        if (chart != null) {
            chart.destroy();
        }
        chart = new Chart(ctx, pData);
    }
    //==========================Carrega Dados para Grafico de barra 
    $scope.CarregarBarra = function (pFiltro) {
        $scope.Show = 'Barra';
        var _data = { 'Competencia_Inicio': pFiltro.CompetenciaInicio, 'Competencia_Fim': pFiltro.CompetenciaFim};
        httpService.Post("DashBoard/ModeloBarra", _data).then(function (response) {
            if (response) {
                $scope.DashboardData = response.data;
                $scope.SetGrap($scope.DashboardData,'chart_Barra');
            }
        });
    }
    //==========================Carrega Dados para Grafico de Linhas
    $scope.CarregarLinha = function (pFiltro) {
        $scope.Show = 'Line';
        var _data = { 'Competencia_Inicio': pFiltro.CompetenciaInicio, 'Competencia_Fim': pFiltro.CompetenciaFim };
        httpService.Post("DashBoard/ModeloLinha", _data).then(function (response) {
            if (response) {
                $scope.DashboardData = response.data;
                $scope.SetGrap($scope.DashboardData, 'chart_Line');
            }
        });
    }

    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.CarregarBarra($scope.Filtro); ///quando entra ja cxarrega o grafico de barra
    });
}]);
