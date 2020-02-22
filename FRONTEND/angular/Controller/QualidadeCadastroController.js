angular.module('App').controller('QualidadeCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Variaveis
    $scope.currentTab = "Dados";
    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;
    $scope.PermissaoDesativar = false;
    $scope.PermissaoExcluir = false;
    httpService.Get("credential/Qualidade@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/Qualidade@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });
    httpService.Get("credential/Qualidade@Destroy").then(function (response) {
        $scope.PermissaoExcluir = response.data;
    });
    httpService.Get("credential/Qualidade@Activate").then(function (response) {
        $scope.PermissaoDesativar = response.data;
    });

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.Qualidade = "";
    console.log($scope.Parameters);

    //==========================Busca dados da Qualidade
    $scope.CarregaDados = function () {
        var _url = "GetQualidadeData/" + $scope.Parameters.Id;
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Qualidade = response.data;
            }
        });
    }
    $scope.CarregaDados();
    //==========================Salvar
    $scope.SalvarQualidade = function (pQualidade) {
        //if ($scope.Parameters.Action == "New")
        //{
        //    $scope.Veiculo.id_operacao = 'I';Parameters.Action
        //}
        //$scope.TipoMidia.id_operacao = $scope.sw == "New" ? 'I' : 'E';
        $scope.Qualidade.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("SalvarQualidade", pQualidade).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    if ($scope.Parameters.Action == 'New') {
                        $scope.CarregaDados();
                    }
                    else {
                        $location.path("/Qualidade")
                    }
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };

    //======================Excluir
    $scope.ExcluirQualidade = function (pQualidade) {

        swal({
            title: "Tem certeza que deseja Excluir esta Qualidade ?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("ExcluirQualidade", pQualidade).then(function (response) {
                if (response) {
                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/Qualidade");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });
    };

    //======================Reativar
    //$scope.ReativarQualidade = function (pQualidade) {
    //    swal({
    //        title: "Tem certeza que deseja reativar esta Qualidade ?",
    //        //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
    //        type: "warning",
    //        showCancelButton: true,
    //        confirmButtonClass: "btn-danger",
    //        confirmButtonText: "Sim, Reativar",
    //        cancelButtonText: "Cancelar",
    //        closeOnConfirm: true
    //    }, function () {
    //        var _data = { 'Cod_Qualidade': pQualidade }
    //        httpService.Post("ReativarQualidade", _data).then(function (response) {
    //            if (response.data[0].Status) {
    //                //if (response.data[0].Status) {
    //                ShowAlert(response.data[0].Mensagem, 'success');
    //                $location.path("/Qualidade");
    //                //}
    //                //else {
    //                //    ShowAlert(response.data[0].Mensagem, 'warning');
    //                //}
    //            }
    //            $scope.CarregaDados();
    //            $scope.CurrentShow = 'Dados';
    //        });
    //    });
    //};

    //======================Desativar
    //$scope.DesativarQualidade = function (pQualidade) {
    //    swal({
    //        title: "Tem certeza que deseja desativar esta Qualidade ?",
    //        //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
    //        type: "warning",
    //        showCancelButton: true,
    //        confirmButtonClass: "btn-danger",
    //        confirmButtonText: "Sim, Desativar",
    //        cancelButtonText: "Cancelar",
    //        closeOnConfirm: true
    //    }, function () {
    //        var _data = { 'Cod_Qualidade': pQualidade }
    //        httpService.Post("DesativarQualidade", _data).then(function (response) {
    //            if (response.data[0].Status) {
    //                //if (response.data[0].Status) {
    //                ShowAlert(response.data[0].Mensagem, 'success');
    //                $location.path("/Qualidade");
    //                //}
    //                //else {
    //                //    ShowAlert(response.data[0].Mensagem, 'warning');
    //                //}
    //            }
    //            $scope.CarregaDados();
    //            $scope.CurrentShow = 'Dados';
    //        });
    //    });
    //};
}]);

