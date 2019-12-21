angular.module("App").factory("loadingInterceptor", function ($q, $rootScope, $timeout) {
    $rootScope.RequestCount= 0;
    	return {
    	    request: function (config) {
	        $rootScope.RequestCount++;
	        $rootScope.loading = true;
			return config;
		},
    	    requestError: function (rejection) {
	        $rootScope.loading = false;
	        $rootScope.RequestCount = 0;
			return $q.reject(rejection);
		},
    	    response: function (response) {
	        $rootScope.RequestCount--
	        $timeout(function () {
	            if ($rootScope.RequestCount == 0) {
	                $rootScope.loading = false;
	            }
			},500);
			return response;
		},
    	    responseError: function (rejection) {
	        $rootScope.RequestCount.loading = false;
	        $rootScope.RequestCount --;
			return $q.reject(rejection);
		}
	};
});

