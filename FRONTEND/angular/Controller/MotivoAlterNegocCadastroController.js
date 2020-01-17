angular.module('App').controller('MotivoAlterNegocCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.MotivoAlterNegoc = "";



    //==========================Busca dados do cadastro na tabela
    var _url = "GetMotivoAlterNegocData/" + $scope.Parameters.Id;
    httpService.Get(_url).then(function (response) {
        if (response) {
            $scope.MotivoAlterNegoc = response.data;
        }
    });

    //==========================Salva dados do cadastro na tabela
    $scope.SalvarMotivoAlterNegoc = function (pMotivoAlterNegoc) {
        $scope.MotivoAlterNegoc.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("SalvarMotivoAlterNegoc", pMotivoAlterNegoc).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };



    //======================Excluir cadastro na tabela
    $scope.ExcluirMotivoAlterNegoc = function (pMotivoAlterNegoc) {
        swal({
            title: "Tem certeza que deseja Excluir este Motivo ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
                httpService.Post("ExcluirMotivoAlterNegoc", pMotivoAlterNegoc).then(function (response) {
                if (response) {
                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/MotivoAlterNegoc");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });

    };



}]);




