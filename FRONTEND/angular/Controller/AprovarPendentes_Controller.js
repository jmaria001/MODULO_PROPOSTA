angular.module('App').controller('AprovarPendentes_Controller', ['$scope', '$rootScope', 'httpService', '$routeParams', function ($scope, $rootScope, httpService, $routeParams) {


    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    if ($scope.Parameters.From == 'Portal') {
        $scope.urlCallBack = '#portal'
    }
    if ($scope.Parameters.From == 'Pendencia') {
        $scope.urlCallBack = '#PendenciaAprovacao'
    }
    //========================Inicializa Scopes 
    $scope.Simulacao = "{}";
    $scope.Simulacao.Aprovado = false;
    $scope.optRecusar = false;

    
    httpService.Get('GetAprovacaoProposta/' + $scope.Parameters.Id).then(function (response) {
        if (response) {
            $scope.Simulacao= response.data;
        }
    });
    //===================================Aprovar Proposta
    $scope.AprovarProposta = function (pId_Simulacao) {
        var _data = { 'Id_Simulacao': pId_Simulacao ,'Action':'Aprovar'};
        httpService.Post("AprovarProposta", _data).then(function (response) {
            if (response) {
                $scope.Aviso = response.data[0];
                ShowAlert(response.data[0].Mensagem, response.data[0].Status ? 'success' : 'warning');
                if (response.data[0].Status==1) {
                    $scope.Simulacao.Aprovado = true;
                }
            }
        });
    }
    //===================================Recusar Aprovacao
    $scope.RecusarProposta = function (pId_Simulacao, pMotivo) {
        
        var _data = { 'Id_Simulacao': pId_Simulacao, 'Action': 'Recusar', 'Motivo': pMotivo };
        httpService.Post("AprovarProposta", _data).then(function (response) {
            if (response) {
                $scope.Aviso = response.data[0];
                ShowAlert(response.data[0].Mensagem, response.data[0].Status ? 'success' : 'warning');
                if (response.data[0].Status == 1) {
                    $scope.Simulacao.Aprovado = true;
                }
            }
        });
    }
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.ShowPage = true;
    });
}]);
