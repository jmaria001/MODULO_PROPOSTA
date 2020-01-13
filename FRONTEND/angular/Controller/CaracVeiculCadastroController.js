angular.module('App').controller('CaracVeiculCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {
    
    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.CaracVeicul = "";
    $scope.PosCalc = [
        { 'id': 0, 'nome': 'Não Considera' },
        { 'id': 1, 'nome': 'Considera Normal' },
        { 'id': 2, 'nome': 'Considera por Crédito'}
    ]



    //==========================Busca dados da caracteristica da veiculacao
    var _url = "GetCaracVeiculData/" + $scope.Parameters.Id;
    httpService.Get(_url).then(function (response) {
        if (response) {
            $scope.CaracVeicul = response.data;
        }
    });

    //==========================Salvar Caracteristica da Veiculação
    $scope.SalvarCaracVeicul = function (pCaracVeicul) {
        $scope.CaracVeicul.id_operacao = $scope.Parameters.Action== "New"? 'I' :'E';
        httpService.Post("SalvarCaracVeicul", pCaracVeicul).then(function (response) {
            if (response) {

                if (response.data[0].Status)
                {
                    ShowAlert(response.data[0].Mensagem, 'success');
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };



    //======================Excluir Caracteristica da Veiculação
    $scope.ExcluirCaracVeicul = function (pCaracVeicul) {
        swal({
            title: "Tem certeza que deseja Excluir esta Característica da Veiculação ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
                httpService.Post("ExcluirCaracVeicul", pCaracVeicul).then(function (response) {
                if (response) {
                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/CaracVeicul");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });

    };



}]);




