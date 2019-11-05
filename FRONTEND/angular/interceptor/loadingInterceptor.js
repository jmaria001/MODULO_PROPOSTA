angular.module("App").factory("loadingInterceptor", function ($q, $rootScope, $timeout) {
    var _RequestCount= 0;
    	return {
	    request: function (config) {
	        _RequestCount++;
	        $rootScope.loading = true;
			return config;
		},
	    requestError: function (rejection) {
	        $rootScope.loading = false;
	        _RequestCount = 0;
			return $q.reject(rejection);
		},
	    response: function (response) {
	        _RequestCount--
	        $timeout(function () {
	            if (_RequestCount== 0) {
	                $rootScope.loading = false;
	            }
			},500);
			return response;
		},
	    responseError: function (rejection) {
	        $rootScope.loading = false;
	        _RequestCount = 0;
			return $q.reject(rejection);
		}
	};
});

