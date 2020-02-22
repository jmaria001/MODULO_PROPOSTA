angular.module('App').controller('ContatoCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Variaveis
    $scope.currentTab = "Dados";
    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;
    $scope.PermissaoDesativar = false;
    $scope.PermissaoExcluir = false;
    httpService.Get("credential/Contato@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/Contato@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });
    httpService.Get("credential/Contato@Destroy").then(function (response) {
        $scope.PermissaoExcluir = response.data;
    });
    httpService.Get("credential/Contato@Activate").then(function (response) {
        $scope.PermissaoDesativar = response.data;
    });

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.Contato = "";
    console.log($scope.Parameters);

    //==========================Busca dados do Contato
    $scope.CarregaDados = function () {
        var _url = "GetContatoData/" + $scope.Parameters.Id;
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Contato = response.data;
            }
        });
    }
    $scope.CarregaDados();
    //==========================Salvar
    $scope.SalvarContato = function (pContato) {
        //if ($scope.Parameters.Action == "New")
        //{
        //    $scope.Veiculo.id_operacao = 'I';Parameters.Action
        //}
        //$scope.TipoMidia.id_operacao = $scope.sw == "New" ? 'I' : 'E';
        $scope.Contato.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("SalvarContato", pContato).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    if ($scope.Parameters.Action == 'New') {
                        $scope.CarregaDados();
                    }
                    else {
                        $location.path("/Contato")
                    }
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };

    //======================Excluir
    $scope.ExcluirContato = function (pContato) {

        swal({
            title: "Tem certeza que deseja Excluir este Contato ?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("ExcluirContato", pContato).then(function (response) {
                if (response) {
                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/Contato");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });
    };
    
    //======================Reativar
    $scope.ReativarContato = function (pContato) {
        swal({
            title: "Tem certeza que deseja reativar este Contato ?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Reativar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            var _data = { 'Cod_Contato': pContato }
            httpService.Post("ReativarContato", _data).then(function (response) {
                if (response.data[0].Status) {
                    //if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/Contato");
                    //}
                    //else {
                    //    ShowAlert(response.data[0].Mensagem, 'warning');
                    //}
                }
                $scope.CarregaDados();
                $scope.CurrentShow = 'Dados';
            });
        });
    };

//======================Desativar
$scope.DesativarContato = function (pContato) {
    swal({
        title: "Tem certeza que deseja desativar este Contato ?",
        //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Sim, Desativar",
        cancelButtonText: "Cancelar",
        closeOnConfirm: true
    }, function () {
        var _data = { 'Cod_Contato': pContato }
        httpService.Post("DesativarContato", _data).then(function (response) {
            if (response.data[0].Status) {
                //if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/Contato");
                //}
                //else {
                //    ShowAlert(response.data[0].Mensagem, 'warning');
                //}
            }
            $scope.CarregaDados();
            $scope.CurrentShow = 'Dados';
        });
    });
};
}]);

