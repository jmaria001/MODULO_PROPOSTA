angular.module('App').controller('ParRetorPlayListController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {
    //------------------- Inicializa Scopes --------------------
    $scope.ShowGrids = false;
    $scope.ShowFilter = true;
    $scope.ParRetorPlayLists = "";
    $scope.ParRetorPlayList = { 'Cod_Veiculo': '', 'Nome_Veiculo': '' };
    $scope.NewFilter = function (fromButtom) {
        $scope.ParRetorPlayList = { 'Cod_Veiculo': '', 'Nome_Veiculo': '' };
        $scope.ParRetorPlayLists = [];
        $scope.ShowGrids = false;
        $scope.ShowFilter = true;
    };
    $scope.NewFilter();

    //------------------- Carrega os Grids ------------------------
    $scope.ParRetorPlayListsCarregar = function (pParRetorPlayList) {
        $rootScope.routeloading = true;
        $scope.ParRetorPlayLists = "";
        $scope.ShowGrids = '';
        $('#dataTable').dataTable().fnDestroy();
        var _url = "ParRetorPlayListFiltrar";
        _url += "?Cod_Veiculo=" + pParRetorPlayList.Cod_Veiculo;
        _url += "&=";
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.ParRetorPlayLists = response.data;
                $scope.ShowGrids = true;
                $scope.ShowFilter = false;
            }
        });
    };

    //------------------- Adicionar Validacao ----------------------
    $scope.AdicionarValidacao = function () {
        $scope.ParRetorPlayLists.Validacao.push({
            "Descricao": "",
            "Posicao": "",
            "Tamanho": "",
            "Conteudo": ""
        });
    };

    //-------------------- Salvar Alterações -----------------------
    $scope.SalvarAlteracoes = function (pParametro) {
        httpService.Post("ParRetorPlayListSalvar", pParametro).then(function (response) {
            if (response.data) {
                ShowAlert(response.data[0].Mensagem)
                if (response.data[0] == 1) {
                    $scope.NewFilter();
                }
            }
        });
    };

    //-------------------- fim do load da pagina -------------------
    $scope.$watch('$viewContentLoaded', function () {
    });

}]);


