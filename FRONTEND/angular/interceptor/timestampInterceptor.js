angular.module("App").factory("timestampInterceptor", function ($cookies) {
    return {
            

        request: function (config) {
            var url = config.url;
            //var hash = "001.001.007";
            var hash = "";
            if (!hash) {
                hash = new Date().getTime();
            }
            url += "?hash=" + hash
		    var _token = $cookies.get('oAuth_token');
			if(_token)
			{
    			config.headers.Authorization = 'Bearer ' + _token;
			}
		config.url = url;
		return config;
    	}
	};
});

