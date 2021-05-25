angular.module('App').controller('PrevisaoVendasController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {

    //========================Verifica Permissoes
    $scope.PosTipo = [
        { 'id': 1, 'nome': 'Agencia/Cliente' },
        { 'id': 2, 'nome': 'Veículo' },
        { 'id': 3, 'nome': 'Mensal' }
    ];


    //====================Inicializa scopes
    $scope.Filtro = {};
    $scope.NewFiltro = function () {
        $scope.Filtro = { 'Competencia': '', 'Cod_Contato': '', 'TipoPrevisao': '' };
        localStorage.removeItem('PrevisaoVendasFilter');
    };
    
    $scope.TipoPrevisaoVendas = [{ 'Codigo': 1, 'Nome': 'Agencia/Cliente' }, { 'Codigo': 2, 'Nome': 'Veiculo' }, { 'Codigo': 3, 'Nome': 'Mensal' }];


    $scope.Operacao = "";
    $scope.PesquisaTabelas = { "Items": [], 'FiltroTexto': '', ClickCallBack: '', 'Titulo': '', 'MultiSelect': false };
    //======================Verifica se tem filtro anterior
    var _Filter = JSON.parse(localStorage.getItem('PrevisaoVendasFilter'));
    if (!_Filter) {
        $scope.NewFiltro()
    };


    //=======================================================Carregar Previsao de Vendas 
    $scope.CarregarPrevisaoVendas = function (pFiltro) {
        if (!pFiltro.Competencia) {
            ShowAlert('Competência é obrigatório.')
            return
        }

        if (pFiltro.TipoPrevisao=="") {
            ShowAlert('Tipo de Previsão é obrigatório.')
            return
        }


        var _Param = pFiltro.Competencia;
        _Param += '/' + (pFiltro.Cod_Contato ? pFiltro.Cod_Contato  : undefined)

        if (pFiltro.TipoPrevisao==1) {
            $location.path("/PrevisaoVendasAgencia/"+_Param)
        }
        if (pFiltro.TipoPrevisao == 2) {
            $location.path("/PrevisaoVendasVeiculo/"+ _Param)
        }
        if (pFiltro.TipoPrevisao == 3) {
            $location.path("/PrevisaoVendasMensal/"+ _Param)
        }
    };

}]);

