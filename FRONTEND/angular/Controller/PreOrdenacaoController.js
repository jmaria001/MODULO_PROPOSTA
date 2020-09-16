angular.module('App').controller('PreOrdenacaoController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout) {
    $scope.FiltroPreOrd = {
        'Veiculo': '',
        'Nome_Veiculo': '',
        'Data': '',
        'Indica_Somente_Prg': false,
        'Programa': '',
        'Titulo': '',
        'Indica_Todos_Prgs': true,
        'Indica_PreOrdenar_Rotativos': true,
        'Indica_PreOrdenar_Vinhetas': true,
        'Indica_Evitar_Choque_Produtos': true,
        'Indica_Evitar_Choque_Apresent': true,
        'Indica_Nao_Colar_Comerciais': true
    };
    
    
    //====================Pré-Ordenar(Salvar)
    $scope.PreOrdenar = function (pFiltro) {

        //====Consistencias
        if (!pFiltro.Data) {
            ShowAlert("A Data é Obrigatória");
            return;
        }
        if (!pFiltro.Veiculo) {
            ShowAlert("O Veículo é Obrigatório");
            return;
        }
        if (pFiltro.Indica_Somente_Prg && !pFiltro.Programa) {
            ShowAlert("Favor informar o Programa");
            return;
        }

        //--Faz a Pré-Ordenação de Roteiro
        var _url = "Roteiro/ExisteRoteiroOrdenado";
        _url += "?Veiculo=" + pFiltro.Veiculo;
        _url += "&Data=" + pFiltro.Data;
        _url += "&Programa=" + pFiltro.Programa;
        _url += "&=";
        var _Mensagem = ""
        httpService.Get(_url).then(function (response) {
            if (response.data == true) {
                _Mensagem = "Já existe roteiro ordenado para esta data.";
                _Mensagem += "\n Caso prossiga, a ordenação desta data será excluida e pre-ordenada novamente.";
                _Mensagem += "\n Deseja Prosseguir ?";
            }
            else {
                _Mensagem = "Confirma a Pre-Ordenação ?"
            };
            swal({
                title: _Mensagem,
                fontsize: 11,
                showCancelButton: true,
                confirmButtonClass: "btn-warning",
                confirmButtonText: "Sim, Pré-Ordenar",
                cancelButtonText: "Não, Cancelar",
                closeOnConfirm: true,
            }, function () {
                    httpService.Post('Roteiro/PreOrdenar', pFiltro).then(function (response) {
                        if (response.data[0].Status == 0) {
                            ShowAlert(response.data[0].Mensagem, 'error');
                        }
                        else {
                            ShowAlert('Pré-Ordenação Realizada com Sucesso', 'success');
                        }
                    });        
            });
        });
        
    };

    $scope.SetaPrograma = function (pTipo) {
        if (pTipo == 'P') {
            if ($scope.FiltroPreOrd.Indica_Somente_Prg) {
                $scope.FiltroPreOrd.Indica_Todos_Prgs = false;
            };
        };
        if (pTipo == 'T') {
            if ($scope.FiltroPreOrd.Indica_Todos_Prgs) {
                $scope.FiltroPreOrd.Indica_Somente_Prg = false;
                $scope.FiltroPreOrd.Programa = "";
                $scope.FiltroPreOrd.Titulo = "";
            };
        };
    };
    
}]);

