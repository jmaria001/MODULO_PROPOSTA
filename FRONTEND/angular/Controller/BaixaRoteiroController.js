angular.module('App').controller('BaixaRoteiroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //====================Inicializa scopes
    
    $scope.Filtro = {};
    $scope.BaixaRoteiro = "";
    $scope.Veiculos = [];
    
    //====================Carrega Dados
    $scope.CarregarRoteiroBaixa = function (pFiltro) {
 
        $rootScope.routeloading = true;
        $scope.BaixarRoteiro="";
        var _url = 'GetRoteiroBaixa'   
        _url += '?1=1';
        _url += '&';
        $scope.ShowFilter = false;
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.BaixaRoteiro = response.data;
                if ($scope.BaixaRoteiro == undefined) {
                    ShowAlert("Não existe dados cadastrado p/ este Filtro");
                    $scope.RepeatFinished();
                };
            };
        });
    };
    $scope.CarregarRoteiroBaixa();
    //====================Salva Roteiro
    $scope.SalvarRoteiroBaixa = function (pBaixaRoteiro) {
        $scope._varSemana = ",";
        for (ix = 0; ix <= 6; ix++) {
            if (pBaixaRoteiro.Domingo == true && ix == 0) {
                $scope._varSemana += (ix + 1) + ",";
            } else if (pBaixaRoteiro.Segunda == true && ix == 1) {
                $scope._varSemana += (ix + 1) + ",";
            } else if (pBaixaRoteiro.Terca == true && ix == 2) {
                $scope._varSemana += (ix + 1) + ",";
            } else if (pBaixaRoteiro.Quarta == true && ix == 3) {
                $scope._varSemana += (ix + 1) + ",";
            } else if (pBaixaRoteiro.Quinta == true && ix == 4) {
                $scope._varSemana += (ix + 1) + ",";
            } else if (pBaixaRoteiro.Sexta == true && ix == 5) {
                $scope._varSemana += (ix + 1) + ",";
            } else if (pBaixaRoteiro.Sabado == true && ix == 6) {
                $scope._varSemana += (ix + 1) + ",";
            };
        };
        //==================================Consistência
        if ($scope._varSemana == ",") {
            ShowAlert("Selecione pelo menos um dia da semana!");
            return
        }
        else {
            pBaixaRoteiro.DiaSemana = $scope._varSemana;
        };

        if (pBaixaRoteiro.Data_Inicial == undefined || pBaixaRoteiro.Data_Inicial == "") {
            ShowAlert("'Período inicial', é de seleção obrigatória!");
            return
        };

        if (pBaixaRoteiro.Cod_Programa == undefined || pBaixaRoteiro.Cod_Programa == "") {
            ShowAlert("'Programa' é de seleção obrigatória!");
            return
        };

        if (pBaixaRoteiro.Cod_Qualidade == undefined || pBaixaRoteiro.Cod_Qualidade == "") {
            ShowAlert("'Qualidade' é de seleção obrigatória!");
            return
        };

        var bolTemVeiculo = false;
        for (var i = 0; i < pBaixaRoteiro.Veiculos.length; i++) {
            if (true) {
                if (pBaixaRoteiro.Veiculos[i].Selected) {
                    bolTemVeiculo = true;
                    break;
                };
            };
        };

        if (!bolTemVeiculo) {
            ShowAlert("Selecione pelo menos um veículo!");
            return
        };

        httpService.Post("SalvarRoteiroBaixa", pBaixaRoteiro).then(function (response) {
            if (response) {
                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $scope.CarregarRoteiroBaixa()
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                };
            };
        })
    };
    //===========================Funcao MarcarDismarcar
    $scope.MarcarDismarcar = function (pRateios, pvalue) {
        for (var i = 0; i < pRateios.length; i++) {
            pRateios[i].Selected = pvalue;
        };
    };
}]);

