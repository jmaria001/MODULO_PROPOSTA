angular.module('App').controller('PropagacaoMapaController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {
                                  
    //============== Inicializa Scopes
    $scope.ShowGrid = false;
    $scope.Propagacao_Mapa = [];

    $scope.CurrentShow = "Filtro";

    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.PeriodoInicioKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' };
    $scope.PeriodoFimKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' };

    //====================Inicializa o Filtro
    $scope.Filtro = {};
    $scope.ShowGrid = false;
    $scope.NewFiltro = function () {

        localStorage.removeItem('PropagacaoMapa');
        return {
            'Empresa': '',
            'Nome_Empresa': '',
            'Numero_Mr': '',
            'Sequencia_Mr': '',
            'Competencia': '',
            'Competencia_Inicial': '',
            'Competencia_Final': ''
        }
    }
    $scope.Filtro = $scope.NewFiltro();
     
    //===================Carregar Mapa
    $scope.CarregarPropagacaoMapa = function (pFiltro) {

        if (pFiltro.Cod_Empresa == "" ) {
            ShowAlert("Codigo Empresa é  filtro Obrigatório.");
            return;
        }

        if (pFiltro.Numero_Mr == "") {
            ShowAlert("Numero do contrato é  filtro Obrigatório.");
            return;
        }

        if (pFiltro.Sequencia_Mr == "") {
            ShowAlert("Sequencia do contrato é  filtro Obrigatório.");
            return;
        }

        if (pFiltro.Competencia == "") {
            ShowAlert("Competência de origem é filtro Obrigatório.");
            return;
        }

        if (pFiltro.Competencia_Inicial == "" ) {
            ShowAlert("Competência Inicial  é filtro Obrigatório.");
            return;
        }

        if (pFiltro.Competencia_Final == "") {
            ShowAlert("Competência Final  é filtro Obrigatório.");
            return;
        }

        if (pFiltro.Competencia_Inicial > pFiltro.Competencia_Final) {
            ShowAlert("Competência Inicial  é  maior que a Competência Final");
            return;
        }

        if (pFiltro.Competencia_Inicial < pFiltro.Competencia) {
            ShowAlert("Competência destino inicial não pode ser menor que mês atual. Não é permitido importar mapa a passado");
            return;
        }

        $rootScope.routeloading = true;

        $scope.Propagacao_Mapa = [];
        
        httpService.Post("CarregarPropagacaoMapa", pFiltro).then(function (response) {
            if (response) {
                $scope.Propagacao_Mapa = response.data;

                if (response.data[0].Indica_Erro == 1) {
                    ShowAlert(response.data[0].Mensagem_Status);
                    return;
                }

                if (response.data[0].Indica_Erro == 0) {

                    for (var i = 0; i < response.data.length; i++) {
                        $scope.Propagacao_Mapa[i].Competencia = response.data[i].Competencia;
                        $scope.Propagacao_Mapa[i].Mensagem_Status = response.data[i].Mensagem_Status;

                    }
                    $scope.ShowGrid = true;
                    $scope.Filtro = $scope.NewFiltro();
                }
            }

        });
        //===========================fim do load da pagina
        $scope.$watch('$viewContentLoaded', function () {
            $rootScope.routeloading = false;
        });
    };
 
}]);

