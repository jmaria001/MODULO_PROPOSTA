angular.module("App").service("tokenApi", function ($cookies) {

    //this.getToken = function (Key) {
    //    return $cookies.get (Key)
    //};
    
    this.setToken= function (Key,Value) {
        var cook = Key + "=" + Value + ";SameSite=Lax"; 
        document.cookie = cook;
    };
    this.removeAll = function (Key, Value) { //exceto filter
        
        var cookies = $cookies.getAll();
        angular.forEach(cookies, function (values, key)
        {
            if (key.indexOf("filter") == -1) {
                $cookies.remove(key);
            }
            
        });
    };
});


