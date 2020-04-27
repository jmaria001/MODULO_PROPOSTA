angular.module('App').controller('MotivoFalhaCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.MotivoFalha = "";
    //console.log($scope.Parameters); //este linha permite vericar o que o parametro esta recebendo pelo browsers

    //==========================Seta o nome da rota
    if ($scope.Parameters.Action== 'New') {
        $rootScope.routeName = 'Inclusão de Motivo de Falha'
    }
    else {
        $rootScope.routeName = 'Alteração de Motivo de Falha'
    };
    //========================Verifica Permissoes
    $scope.PermissaoDelete= false;
    $scope.PermissaoDesativar = false;
    httpService.Get("credential/MotivoFalha@Destroy").then(function (response) {
        $scope.PermissaoDelete = response.data;
    });
    httpService.Get("credential/MotivoFalha@Activate").then(function (response) {
        $scope.PermissaoDesativar= response.data;
    });
    //==========================Busca dados
    var _url = "GetMotivoFalhaData/" + $scope.Parameters.Id;
    httpService.Get(_url.trim()).then(function (response) {
        if (response) {
            $scope.MotivoFalha = response.data;
        }
    });


    //==========================Salvar
    $scope.SalvarMotivoFalha = function (pMotivoFalha) {
        $scope.MotivoFalha.Id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("SalvarMotivoFalha", pMotivoFalha).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/MotivoFalha");
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };

    //======================Excluir
    $scope.ExcluirMotivoFalha = function (pMotivoFalha) {
        swal({
            title: "Tem certeza que deseja Excluir este Motivo de Falha ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
                httpService.Post("ExcluirMotivoFalha", pMotivoFalha).then(function (response) {
                if (response) {

                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/MotivoFalha");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });
    };



    //====================Desativar
    $scope.DesativarReativarMotivoFalha = function (pCodigo, pAction) {
        swal({
            title: "Tem certeza que deseja " + (pAction=='D' ? "Desativar" : "Reativar") + " esse Motivo de Falha ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim",
            cancelButtonText: "Não",
            closeOnConfirm: true
        }, function () {
            _Data = { 'Cod_Motivo_Falha': pCodigo, 'Id_Acao': pAction }
            httpService.Post('DesativarReativarMotivoFalha', _Data).then(function (response) {
                if (response) {
                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/MotivoFalha");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            });
        });
    };





}]);

