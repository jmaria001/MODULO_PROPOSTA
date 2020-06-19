angular.module('App').controller('AprovacaoProposta', ['$scope', '$rootScope', 'httpService', function ($scope, $rootScope, httpService) {

    
    $scope.Aviso = "";
    var _config = httpService.GetConfig();
    $rootScope.baseUrl = _config.baseUrl;
    $rootScope.pageUrl = _config.pageUrl;
    $rootScope.Islogged = false;
    $scope.ShowPage = false
    $scope.ShowOk= false
    $scope.Simulacao = "{}";
    $scope.Aviso = {'Mensagem':'','Status':false};
    $scope.Ok = false;
    $scope.ShowButton = false;
    $scope.optRecusar = false;   
    $scope.token = getUrlParameter('token');
    httpService.Get('GetAprovacaoData/' + $scope.token).then(function (response) {
        if (response) {
            $scope.Simulacao= response.data;
            if ($scope.Simulacao.Id_Simulacao == 0) {
                $scope.Aviso.Mensagem = "O Token para essa Aprovação não existe ou não é mais valido. A Proposta somente pode ser aprovada pelo Sistema";
                $scope.ShowOk = false;
                $scope.Aviso.Status = false;
            }
            else {
                $scope.ShowOk = true
                $scope.ShowButton = true;
            }
            
        }
    });
    $scope.AprovarProposta = function (pId_Simulacao,pToken) {
        var _data = { 'Id_Simulacao': pId_Simulacao, 'Token': pToken };
        httpService.Post("AprovarProposta", _data).then(function (response) {
            if (response) {
                $scope.Aviso = response.data[0];
                $scope.ShowOk = false;
                $scope.ShowButton = false;
            }
        });
    }

    //===================================Recusar Aprovacao
    $scope.RecusarProposta = function (pId_Simulacao, pMotivo,pToken) {

        var _data = { 'Id_Simulacao': pId_Simulacao, 'Action': 'Recusar', 'Motivo': pMotivo,'Token':pToken };
        httpService.Post("AprovarProposta", _data).then(function (response) {
            if (response) {
                $scope.Aviso = response.data[0];
                if (response.data[0].Status == 1) {
                    $scope.ShowButton = false;
                    $scope.optRecusar = false;
                }
            }
        });
    }
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.ShowPage = true;
    });
}]);
