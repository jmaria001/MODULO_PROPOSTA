angular.module("App").service("AccountApi", function ($http, config, $q, $cookies) {

    var _GetConfig = function () {
        return config;
    };
    var _GetUserData = function () {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: config.baseUrl + "API/GetUserData" 
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    var _ValidarLogin = function (UserData) {
        var deferred = $q.defer();
        $http({
            method: 'POST',
            url: config.baseUrl + "api/security/token",
            data: UserData,
            headers: "{'Content-Type': 'application/x-www-form-urlencoded'}"
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };
    var _NewPassword= function (pParam ) {
        var deferred = $q.defer();
        $http({
            method: 'POST',
            url: config.baseUrl + "api/NewPassword",
            headers: "{'Content-Type': 'application/x-www-form-urlencoded'}",
            data:pParam
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };
    var _SetPassword= function (pParam) {
        var deferred = $q.defer();
        $http({
            method: 'POST',
            url: config.baseUrl + "api/SetPassword",
            headers: "{'Content-Type': 'application/x-www-form-urlencoded'}",
            data: pParam
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    var _Credential = function (pRouteId) {
        var deferred = $q.defer();
        var _data = {'Rota': pRouteId};
        $http({
            method: 'GET',
            url: config.baseUrl + "api/credential/" + pRouteId,
            //data: _data,
            //headers: "{'Content-Type': 'application/x-www-form-urlencoded'}"
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    return {
        ValidarLogin: _ValidarLogin,
        Credential: _Credential,
        GetConfig: _GetConfig,
        GetUserData: _GetUserData,
        NewPassword: _NewPassword,
        SetPassword: _SetPassword
    };

});



