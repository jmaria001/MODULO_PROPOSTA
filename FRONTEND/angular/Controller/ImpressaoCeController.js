angular.module('App').controller('ImpressaoCeController', ['$scope', '$rootScope', 'httpService', function ($scope, $rootScope, httpService ) {

    //===========================Inicializa Scopes
    $scope.Filtro = "";
    $scope.CompetenciaKeys= { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    //===========================Novo Filtro
    $scope.newFilter = function()
    {
        return {
            'Cod_Empresa_Faturamento': '',
            'Competencia':'',
            'Nome_Empresa_Faturamento':'',
            'Data_Processamento': '',
            'Numero_Ce_Inicio': '',
            'Numero_Ce_Fim': '',
            'Cod_Empresa_Venda': '',
            'Numero_Mr': '',
            'Sequencia_Mr': '',
            'Numero_Fatura': '',
            'Agencia': '',
            'Cliente':'',
        }
    }
    $scope.Filtro = $scope.newFilter();
    console.log($scope.Filtro);
    //===========================Impressao do Ce
    $scope.ImprimirCe = function (pFiltro) {
        httpService.Post("ImpressaoCe", pFiltro).then(function (response) {
            if (response.data) {
                if (!response.data.Status) {
                    ShowAlert(response.data.Mensagem)
                }
                else {
                    url = $rootScope.baseUrl + "PDFFILES/COMPROVANTE/" + $rootScope.UserData.Login.trim() + "/" + response.data.pdfFileName;
                    var win = window.open(url, '_blank');
                    win.focus();
                }
            }
        });
    };
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
    });

}]);

