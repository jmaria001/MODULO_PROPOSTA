angular.module('App').controller('MapaReservaDetalheController', ['$scope', '$rootScope', '$location', 'httpService', '$location', '$routeParams', function ($scope, $rootScope, $location, httpService, $location, $routeParams) {
    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.FiltroMidia = { 'Competencia': '', 'Cod_Veiculo': '', 'Indica_Demanda': true, 'Display': 1 };
    $scope.Display = [{ 'Id': 1, 'Nome': 'Midia Demandada' }, { 'Id': 2, 'Nome': 'Midia Exibivel' }, { 'Id': 3, 'Nome': 'Lista de Veiculações ' }]
    $scope.Contrato = {};
    $scope.Comerciais = [];
    $scope.Competencias = [];
    $scope.Veiculos = [];
    $scope.Midias = [];
    $scope.Resumos= [];
    $scope.Veiculacoes= [];
    $scope.Competencia_Text = ""
        //==========================Carrega Dados do Contrato
    $scope.CarregaContrato = function () {
        var _url = "MapaReserva/DetalheContrato/" + $scope.Parameters.Id;
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Contrato = response.data[0];
                $scope.CarregaComerciais();
            }
        });
    };
    //==========================Carrega Dados dos  Comerciais
    $scope.CarregaComerciais = function () {
        var _url = "MapaReserva/DetalheComercial/" + $scope.Parameters.Id;
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Comerciais = response.data;
                $scope.CarregaCompetencias();
            }
        });
    };
    //==========================Carrega Competencias
    $scope.CarregaCompetencias = function () {
        var _url = "MapaReserva/DetalheCompetencia/" + $scope.Parameters.Id;
        httpService.Get(_url).then(function (response) {
            if (response.data.length>0) {
                $scope.Competencias = response.data;
                $scope.FiltroMidia.Competencia = response.data[0].Competencia_Int;
                var _year = parseInt($scope.FiltroMidia.Competencia.substr(0, 4));
                var _mes = parseInt($scope.FiltroMidia.Competencia.substr(4, 2));
                $scope.Competencia_Text = MesExtenso($scope.FiltroMidia.Competencia)
                $scope.CarregaVeiculos();
            }
        });
    };
    //==========================Carrega Veiculos
    $scope.CarregaVeiculos = function () {
        var _url = "MapaReserva/DetalheVeiculo/" + $scope.Parameters.Id;
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Veiculos = response.data;
                $scope.FiltroMidia.Cod_Veiculo = response.data[0].Cod_Veiculo
                $scope.CarregaMidia();
                $scope.CarregaResumo();
            }
        });
    };
    //==========================Carrega Midia
    $scope.CarregaMidia = function () {
        if ($scope.FiltroMidia.Display == 3) {
            var _url = "MapaReserva/DetalheVeiculacao"
            _url += "?Id_Contrato=" + $scope.Parameters.Id;
            _url += "&Competencia=" + $scope.FiltroMidia.Competencia;
            _url += "&Cod_Veiculo=" + $scope.FiltroMidia.Cod_Veiculo;
            _url += "&Display=" + $scope.FiltroMidia.Display;
            _url += "&"
            httpService.Get(_url).then(function (response) {
                if (response) {
                    $scope.Veiculacoes = response.data;
                }
            });
        }
        else {
            var _url = "MapaReserva/DetalheMidia"
            _url += "?Id_Contrato=" + $scope.Parameters.Id;
            _url += "&Competencia=" + $scope.FiltroMidia.Competencia;
            _url += "&Cod_Veiculo=" + $scope.FiltroMidia.Cod_Veiculo;
            _url += "&Display=" + $scope.FiltroMidia.Display;
            _url += "&"
            httpService.Get(_url).then(function (response) {
                if (response) {
                    $scope.Midias = response.data;
                }
            });
        }
    };
    //==========================Carrega Midia
    $scope.CarregaResumo= function () {
        
        var _url = "MapaReserva/DetalheResumo"
        _url += "?Id_Contrato=" + $scope.Parameters.Id;
        _url += "&Competencia=" + $scope.FiltroMidia.Competencia;
        _url += "&Cod_Veiculo=" + $scope.FiltroMidia.Cod_Veiculo;
        _url += "&Display=" + $scope.FiltroMidia.Display;
        _url += "&"
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Resumos= response.data;
            }
        });
        
    };
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.CarregaContrato();
    });
    
}]);


