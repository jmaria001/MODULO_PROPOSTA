angular.module('App').controller('muralController', ['$scope', '$rootScope', 'httpService', function ($scope, $rootScope, httpService,$location) {
    $scope.VistoMensagem = function (pIdMensagem)
    {
        httpService.Post('VistoMensagem', {'Id_Mensagem':pIdMensagem}).then(function (response) {
            httpService.Post('GetMensagem').then(function (response) {
                $rootScope.Mensagens = response.data;
            });
        });
    };

    $scope.RemoverMensagem = function (pIdMensagem) {
        httpService.Post('RemoverMensagem', { 'Id_Mensagem': pIdMensagem }).then(function (response) {
            httpService.Post('GetMensagem').then(function (response) {
                $rootScope.Mensagens = response.data;
            });
        });
    };
    $scope.CloseSidePanel = function () {
        $(".app").removeClass("app-sidepanel-open");
        app.hideOverlay();
    };
}]);
