angular.module('App').controller('MensagemController', ['$scope', 'GenericApi', '$location', function ($scope, GenericApi, $location) {

    //============ Inicializa Scope
        $scope.UsuarioAlerta = {};
        $scope.Mensagem = "";
        $scope.SelectCount = 0;
    //============ Carrega Usuarios Similares para mandar mensagem do posicionamento
    GenericApi.CarregarUsuarioAlerta().then(function (response) {
        if (response.data) {
            $scope.UsuarioAlerta = response.data;
        }
    });
    
    $scope.MarcarTodos = function (pValue) {
        for (var i = 0; i < $scope.UsuarioAlerta.length; i++) {
            $scope.UsuarioAlerta[i].Selecionado = pValue;
        }
        $scope.Count();

    }

    $scope.EnviarMensagem = function () {
        var _Mensagem = {};
        var _Usuario = $scope.UsuarioAlerta.filter(function (el) {
            return (el.Selecionado === true);
        });

        _Mensagem.Texto = $scope.Mensagem;
        _Mensagem.Usuario = _Usuario
        GenericApi.EnviarMensagem(_Mensagem).then(function (response) {
            if (response.data) {
                ShowAlert('Mensagem Enviada com Sucesso', 'success', 2000);
                $scope.Mensagem = "";
                $scope.MarcarTodos(false);
            }
        });
    }

    $scope.Count = function()
    {
        $scope.SelectCount = $scope.UsuarioAlerta.filter(function (el) {
            return (el.Selecionado === true);
        }).length;
        console.log($scope.SelectCount)
    }
    $scope.CancelarMensagem = function () {
                $location.path("/blank");
    }
}]);
