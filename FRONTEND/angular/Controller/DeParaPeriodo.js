angular.module('App').controller('DeParaPeriodoController', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location) {
    //==========================Inicializa Scopes
    
    $scope.checkMarcarVeiculo = false
    //===========================Carrega Model Vazio
    $scope.CarregaDados = function () {
        httpService.Post("DeParaProgramacao/CarregarDadosPeriodo").then(function (response) {
            if (response.data) {
                $scope.DePara = response.data;
                $scope.checkMarcarVeiculo = false
            };
        });
    };
    $scope.CarregaDados();
    //===========================Marcar Todos os Veiculos
    $scope.MarcarTodosVeiculos = function (pParam,pValue) {
        for (var i = 0; i < pParam.Veiculos.length; i++) {
            pParam.Veiculos[i].Selected = pValue;
        };
    };
    //===========================Confirmar e fazer o De-Para
    $scope.ConfirmarDePara = function (pParam) {
        var _marcouveiculo = false;
        for (var i = 0; i < pParam.Veiculos.length; i++) {
            if (pParam.Veiculos[i].Selected) {
                _marcouveiculo = true;
                break;
            };
        };
        if (!_marcouveiculo) {
            ShowAlert("Nenhum Veículo foi Selecionado.");
            return;
        };
        if (!pParam.Data_Inicio || !pParam.Data_Termino) {
            ShowAlert("Data Início e Término da Progração são obrigatórios.");
            return;
        };
        if (!pParam.Dom && !pParam.Seg && !pParam.Ter && !pParam.Qua && !pParam.Qui && !pParam.Sex && !pParam.Sab) {
            ShowAlert("Nenhum dia da Semana foi Selecionado");
            return;
        };
        if (!pParam.Cod_Programa_De || !pParam.Cod_Programa_Para) {
            ShowAlert("Programa DE e PARA são obrigatórios.");
            return;
        };
        if (pParam.Cod_Programa_De == pParam.Cod_Programa_Para) {
            ShowAlert("Programa DE e PARA devem ser diferentes.");
            return;
        };

        swal({
            title: "Confirma o De-Para dessa Programação?",
            //type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Confimar",
            cancelButtonText: "Não,Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("DeParaProgramacao/ProcessaDeParaPeriodo", pParam).then(function (response) {
                if (response.data) {
                    ShowAlert(response.data[0].Mensagem)
                }
            });
        });
    }
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $rootScope.routeloading = false;
    });

}]);


