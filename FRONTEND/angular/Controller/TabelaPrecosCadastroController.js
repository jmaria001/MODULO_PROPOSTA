angular.module('App').controller('TabelaPrecosCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.TabelaPrecos = "";
    //========================Verifica Permissoes
    $scope.PermissaoDelete = false;
    httpService.Get("credential/TabelaPrecos@Destroy").then(function (response) {
        $scope.PermissaoDelete = response.data;
    });

    $scope.PosTipo = [
        { 'id': 'Normal', 'nome': 'Normal' },
        { 'id': 'Merchandising', 'nome': 'Merchandising' }
    ]

    //==========================Busca dados do Tabela de Preços
    var _url = "GetTabelaPrecosData/" + $scope.Parameters.Id;
    httpService.Get(_url).then(function (response) {
        if (response) {
            $scope.TabelaPrecos = response.data;
        }
    });
    //==========================Salvar
    $scope.SalvarTabelaPrecos = function (pTabelaPrecos) {
        $scope.TabelaPrecos.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("SalvarTabelaPrecos", pTabelaPrecos).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/TabelaPrecos");
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };

    //======================Excluir
    $scope.ExcluirTabelaPrecos = function (pTabelaPrecos) {

        swal({
            title: "Tem certeza que deseja Excluir este Funcao Terceiro ?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("ExcluirTabelaPrecos", pTabelaPrecos).then(function (response) {
                if (response) {

                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/TabelaPrecos");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });

    };

}]);




