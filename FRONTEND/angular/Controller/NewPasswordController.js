angular.module('App').controller('newPasswordController', ['$scope', '$rootScope', 'httpService', function ($scope, $rootScope, httpService) {

    
    $scope.Aviso = "";
    var _config = httpService.GetConfig();
    $rootScope.baseUrl = _config.baseUrl;
    $rootScope.pageUrl = _config.pageUrl;
    $rootScope.Islogged = false;
    $scope.ShowPage = false
    $scope.NewPassword = { 'Email': '', 'Login': '', 'EsqueceuLogin': false, 'Url': $rootScope.pageUrl,'Senha1':'','Senha2':'','Token':'' }
    
    $scope.Aviso = "";
    
    $scope.ChangePassword = function () {
        $scope.Aviso = "";
        httpService.Post('NewPassword', $scope.NewPassword).then(function (response) {
            if (response) {
                $scope.Aviso = response.data;
            }
        });
    }
    $scope.$watch('NewPassword.EsqueceuLogin', function (newValue, oldValue) {

        if (newValue) {
            $scope.NewPassword.Login = "";
        }
    });

    $scope.SetPassword = function () {
        
        var _newData = {};
        

        _newData.Senha = Salt($scope.NewPassword.Senha1);
        _newData.Token = $scope.NewPassword.Token;
        httpService.Post('SetPassword', _newData).then(function (response) {
            if (response) {
                $scope.Aviso = response.data;
            }
        });
    }

    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        
            $scope.ShowPage = true;
        
        
    });
}]);
