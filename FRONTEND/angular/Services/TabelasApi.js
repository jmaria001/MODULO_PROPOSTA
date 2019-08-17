angular.module("App").service("TabelasApi", function ($http, config, $q, $cookies) {

    var _CarregarTabela= function (pTabela) {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: config.baseUrl + "API/TabelaCarregar/" + pTabela + '/',
            //headers: { 'Content-Type': 'application/json' }
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    return {
        CarregarTabela: _CarregarTabela
    };

});

