angular.module('App').controller('CategoriaClienteCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {
    
    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.CategoriaCliente = "";
    //========================Verifica Permissoes
    $scope.PermissaoDelete= false;
    httpService.Get("credential/CategoriaCliente@Destroy").then(function (response) {
        $scope.PermissaoDelete = response.data;
    });

    //==========================Busca dados da Categoria do Cliente
    $scope.CarregaDados = function () {
        var _url = "GetCategoriaClienteData/" + $scope.Parameters.Id;
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.CategoriaCliente = response.data;
            }
        });
    }
    $scope.CarregaDados();
    
    //==========================Salvar Categoria do Cliente
    $scope.SalvarCategoriaCliente = function (pCategoriaCliente) {
        $scope.CategoriaCliente.id_operacao = $scope.Parameters.Action== "New"? 'I' :'E';
        httpService.Post("SalvarCategoriaCliente", pCategoriaCliente).then(function (response) {
            if (response) {
                if (response.data[0].Status)
                {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    if ($scope.Parameters.Action == 'New') {
                        $location.path("/CategoriaCliente");
                    }
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };



    //======================Excluir Categoria do Cliente
    $scope.ExcluirCategoriaCliente = function (pCategoriaCliente) {
        swal({
            title: "Tem certeza que deseja Excluir esta Categoria do Cliente ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
                httpService.Post("ExcluirCategoriaCliente", pCategoriaCliente).then(function (response) {
                if (response) {
                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/CategoriaCliente");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });

    };



}]);




