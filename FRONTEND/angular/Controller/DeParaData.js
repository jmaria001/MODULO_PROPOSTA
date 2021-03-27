angular.module('App').controller('DeParaDataController', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location) {
    //==========================Inicializa Scopes
    
    $scope.checkMarcarVeiculo = false
    //===========================Carrega Model Vazio
    $scope.CarregaDados = function () {
        httpService.Post("DeParaProgramacao/CarregarDadosData").then(function (response) {
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
        if (!pParam.Data_De|| !pParam.Data_Para) {
            ShowAlert("Data De e Para da Progração são obrigatórios.");
            return;
        };
        
        if (!pParam.Cod_Programa_De || !pParam.Cod_Programa_Para) {
            ShowAlert("Programa DE e PARA são obrigatórios.");
            return;
        };
        if (pParam.Cod_Programa_De == pParam.Cod_Programa_Para && pParam.Data_De==pParam.Data_Para) {
            ShowAlert("A Programação de e Para devem ter Programas ou Datas diferentes.");
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
            httpService.Post("DeParaProgramacao/ProcessaDeParaData", pParam).then(function (response) {
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


