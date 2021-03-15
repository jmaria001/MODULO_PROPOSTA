angular.module('App').controller('ConfirmacaoRoteiroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {

    //====================Inicializa scopes
    $scope.Parametros = {'Data_Confirmacao_Rede':'','Data_Confirmacao_Local':''};
    $scope.Veiculos = [];

    //====================Carrega o Grid
    $scope.CarregarVeiculos = function () {
        $rootScope.routeloading = true;
        $scope.Veiculos = [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();
        httpService.Get('ConfirmacaoRoteiroListar').then(function (response) {
            if (response) {
                $scope.Veiculos = response.data;
                if ($scope.Veiculos.length == 0) {
                    $scope.RepeatFinished();
                }
            }
        });
    };
    $scope.MarcarTodos = function(pVeiculo, pValue){
        for (var i = 0; i < pVeiculo.length; i++) {
            pVeiculo[i].Indica_Marcado = pValue;
        }
    };

    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.CarregarVeiculos();
    });

    //------------------- Confirmar Roteiro ------------------------
    $scope.ConfirmarRoteiro = function (pParam, pVeiculos) {
        //-----Consiste
        if (!pParam.Data_Confirmacao_Rede || !pParam.Data_Confirmacao_Local) {
            ShowAlert("Data de Confirmação da Rede ou Local deve ser informado");
            return;
        }
        var bolMarcado = false;
        for (var i = 0; i < pVeiculos.length; i++) {
            if (pVeiculos[i].Indica_Marcado) {
                bolMarcado = true;
                break;
            };
        };
        if (!bolMarcado) {
            ShowAlert("Nenhum Veículo foi Marcado");
            return;
        }
        swal({
            title: "Deseja Confirmar o Roteiro com as Datas Selecionadas ?",
            fontsize: 11,
            showCancelButton: true,
            confirmButtonClass: "btn-warning",
            confirmButtonText: "Sim,Confirmar",
            cancelButtonText: "Não, Cancelar",
            closeOnConfirm: true,
        }, function () {
            var _data = {
                'Data_Confirmacao_Rede': pParam.Data_Confirmacao_Rede,
                'Data_Confirmacao_Local': pParam.Data_Confirmacao_Local,
                'Veiculos': pVeiculos
            };
                httpService.Post('ConfirmaRoteiro', _data).then(function (response) {
                    if (response.data) {
                        $scope.Veiculos = response.data;
                        pParam.Data_Confirmacao_Rede = "";
                        pParam.Data_Confirmacao_Local = "";
                        $scope.chkMarcar = false;
                    };
                });
        });
    };

}]);

