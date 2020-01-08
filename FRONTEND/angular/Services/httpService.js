angular.module("App").service("httpService", function ($http, config, $q, $cookies) {

    var _GetConfig = function () {
        return config;
    };
    var _HttpGet = function (pUrl) {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: config.baseUrl + "API/" + pUrl,
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    var _httpPost= function (pUrl,pData) {
        var deferred = $q.defer();
        $http({
            method: 'POST',
            url: config.baseUrl + "API/" + pUrl,
            data: pData,
            headers: "{'Content-Type': 'application/x-www-form-urlencoded'}"
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    var _MobileGet = function (pUrl) {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: pUrl
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };
    
    return {
        Get:_HttpGet,
        Post:_httpPost,
        GetConfig: _GetConfig,
        MobileGet: _MobileGet,
    };

});



