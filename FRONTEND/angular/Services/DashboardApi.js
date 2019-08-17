angular.module("App").service("DashboardApi", function ($http, config, $q, $cookies) {

    var _CarregarDashboard= function (pFiltro) {
        var deferred = $q.defer();
        $http({
            method: 'POST',
            url: config.baseUrl + "API/Dashboard/Carregar",
            data:pFiltro,
            headers: { 'Content-Type': 'application/json' }
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    return {
        CarregarDashboard: _CarregarDashboard
    };

});

