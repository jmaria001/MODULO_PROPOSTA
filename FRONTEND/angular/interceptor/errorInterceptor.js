angular.module('App').factory("errorInterceptor", function ($q, $location, $rootScope) {
    return {
        responseError: function (rejection) {
            $('.modal').modal('hide');// closes all active pop ups.
            $('.modal-backdrop').remove(); // removes the grey overlay.
            var _url = rejection.config.url.toLowerCase();
            if (!_url) {
                _url = "";
            };
            if (_url.indexOf('api/security/token') > 0 || _url.indexOf('api/api/CheckLogin') > 0) {
                if (rejection.status == 404) {
                    $rootScope.App_Erro = _url + " Nao existe na Web Api"
                    $rootScope.Islogged = false;
                }
                else {
                    if (rejection.status != 200) {
                        $rootScope.App_Erro = rejection.data.error;
                        $rootScope.Islogged = false;
                    }
                }
            }
            else {
                switch (rejection.status) {
                    case 401:
                        $rootScope.App_Erro = rejection.data.Message;
                        $rootScope.Islogged = false;
                        $location.path("/unlogged");
                        break;
                    case 500:
                        $rootScope.App_Erro = rejection.data.ExceptionMessage;
                        $rootScope.loading = false;
                        break
                    case 404:
                        $rootScope.App_Erro = rejection.status + '-' + rejection.statusText + '-' + _url;
                        $rootScope.loading = false;
                        break;
                    case 405:
                        $rootScope.App_Erro = rejection.status + '-' + rejection.statusText + '-' + _url;
                        $rootScope.loading = false;
                        break;
                    case 400:
                        if (_url.indexOf('api/security/token') < 0) {
                            $rootScope.App_Erro = rejection.statusText + '-';
                            //  $location.path("/error")
                            $rootScope.loading = false;
                        }
                        break;
                    default:
                        $rootScope.App_Erro = rejection.data.statusText;
                        //$location.path("/error");
                        $rootScope.loading = false;
                        break
                }

                ShowAlert($rootScope.App_Erro, 'error');
            }
        }

    };
});

