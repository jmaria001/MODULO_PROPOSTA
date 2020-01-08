angular.module('App').controller('MainController', ['$scope', '$rootScope', '$cookies', 'httpService', '$location', '$document', function ($scope, $rootScope, $cookies, httpService, $location, $document) {
    var _config = httpService.GetConfig();
    $rootScope.baseUrl = _config.baseUrl;
    $rootScope.pageUrl = _config.pageUrl;
    $rootScope.mobileUrl = _config.mobileUrl;
    
    $rootScope.AppVersion = _config.Version;
    $scope.User = {}
    var _token = $cookies.get('oAuth_token');
    if (_token === undefined) {
        $rootScope.Islogged = false;
        $location.path("/login");
    }
    else {
        $rootScope.Islogged = true;
        httpService.Get('GetUserData').then(function (response) {
            if (response) {
                $rootScope.UserData = response.data[0];
            }
        });

        //httpService.Post('GetMensagem').then(function (response) {
        //    if (response) {
        //        $rootScope.Mensagens = response.data;
        //    }
        //});
    }

    httpService.Get("GetDataBaseName").then(function (response) {
        if (response) {
            $scope.ServerName=response.data[0].ServerName;
            $scope.DataBaseName=response.data[0].DataBaseName;
        }
    });
    
    $rootScope.$watch('loading', function (newValue, oldValue) {
        if (newValue) {
            $("#buttonWait").click()
        }
        else {
            $("#ButtonCloseWait").click()
        }
    });
}]);


