angular.module('App').controller('loginController', ['$scope', '$rootScope', 'tokenApi', 'httpService',  '$location', function ($scope, $rootScope, tokenApi, httpService, $location) {
       
    $scope.CookieEnabled = navigator.cookieEnabled;
    $scope.app_Logout = function (event) {
        $rootScope.Islogged = false;
        tokenApi.removeAll();
        $rootScope.Mensagens = [];
        $rootScope.UserData = {};
        $location.path("/login");
        for (key in localStorage) {
            delete localStorage[key];
        }
    };
    
    $scope.setLogin = function (user) {
        {
            $scope.CookieEnabled = navigator.cookieEnabled;
            if (!$scope.CookieEnabled) return
            //var cuser = Salt(user.login);
            //var cpassword = Salt(user.password);
            var cuser = user.login;
            var cpassword = user.password;
            $rootScope.App_Erro = "";
            var _data = "username=" + cuser + "&password=" + cpassword + "&grant_type=password";
            httpService.Post('security/token', _data).then(function (response) {
                var _valido = true
                if ($rootScope.App_Erro) {
                    ShowAlert($rootScope.App_Erro, 'warning', 2000, 'topRight')
                    _valido=false
                } 
                if (_valido == true) {
                    tokenApi.setToken('oAuth_token', response.data['access_token']);
                    $rootScope.Islogged = true;
                    delete $scope.app_user

                    httpService.Get('GetUserData').then(function (response) {
                        $rootScope.UserData = response.data[0];
                    });

                    $rootScope.Mensagens = [];
                    httpService.Post('GetMensagem').then(function (response) {
                        $rootScope.Mensagens = response.data;
                        $rootScope.ShowMensagem = $rootScope.Mensagens.length;
                    });
                    $location.path("/portal")
                }
                else {
                    $rootScope.Islogged = false;
                    tokenApi.removeAll();
                    delete $scope.app_user
                    //ShowAlert(_Error, 'warning', 2000,'topRight') 
                }
            });

        }
    }
    $scope.CheckCookie = function () {
        $scope.CookieEnabled = navigator.cookieEnabled;
    }
}]);





