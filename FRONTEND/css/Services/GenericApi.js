angular.module("App").service("GenericApi", function ($http, config, $q, $cookies) {

    var _GetConfig = function () {
        return config;
    };
   
    var _CarregarUsuarioAlerta = function (pAtividadeId) {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: config.baseUrl + "API/Atividade/GetUsuarioSimilar/"
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    var _ValidarTabela = function (pTabela, pCodigo) {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: config.baseUrl + "API/ValidarTabela/" + pTabela.trim() + '/' + pCodigo.trim() + '/'
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };
    var _ListarTabela = function (pTabela) {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: config.baseUrl + "API/ListarTabela/" + pTabela 
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    var _GetMensagem = function () {
        var deferred = $q.defer();
        $http({
            method: 'POST',
            url: config.baseUrl + "API/GetMensagem",
            headers: { 'Content-Type': 'application/json' }
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    var _VistoMensagem = function (pIdMensagem) {
        var deferred = $q.defer();
        var _data = {}
        _data.Id_Mensagem = pIdMensagem
        $http({
            method: 'POST',
            url: config.baseUrl + "api/VistoMensagem",
            data: _data,
            headers: "{'Content-Type': 'application/x-www-form-urlencoded'}"
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    var _RemoverMensagem = function (pIdMensagem) {
        var deferred = $q.defer();
        var _data = {}
        _data.Id_Mensagem = pIdMensagem
        $http({
            method: 'POST',
            url: config.baseUrl + "api/RemoverMensagem",
            data: _data,
            headers: "{'Content-Type': 'application/x-www-form-urlencoded'}"
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    

    var _GetGridConfig = function (pGridConfig, pGridName) {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: config.baseUrl + "API/GetGridConfig/" + pGridName + '/'
        }
        ).then(function (response) {

            deferred.resolve(response);
        });
        return deferred.promise
    };
    var _GetFiltroUsuario = function () {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: config.baseUrl + "API/GetFiltroUsuario"
        }
        ).then(function (response) {

            deferred.resolve(response);
        });
        return deferred.promise
    };
    var _SalvarGridConfig = function (pGrid, pGridConfig, pModo) {
        var deferred = $q.defer();
        var _data = {}
        _data.GridConfig = pGridConfig;
        _data.GridName = pGrid;
        _data.GridModo = pModo;

        $http({
            method: 'POST',
            url: config.baseUrl + "API/SalvarGridConfig",
            data: _data,
            headers: { 'Content-Type': 'application/json' }
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    var _GravarFiltro = function (pFiltro) {
        var deferred = $q.defer();
        var _data = {};
        _data.Projeto = pFiltro.Projeto;
        _data.Analista = pFiltro.Analista;
        _data.Solicitante = pFiltro.Solicitante;
        _data.Caracteristica = pFiltro.Caracteristica;
        _data.Situacao = pFiltro.Situacao;
        _data.Cliente = pFiltro.Cliente;
        $http({
            method: 'POST',
            url: config.baseUrl + "API/GravarFiltroUsuario",
            data: _data,
            headers: { 'Content-Type': 'application/json' }
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    var _EnviarMensagem = function (pMensagem) {
        var deferred = $q.defer();
        $http({
            method: 'POST',
            url: config.baseUrl + "API/EnviarMensagem",
            data: pMensagem,
            headers: { 'Content-Type': 'application/json' }
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };
    return {
        GetConfig: _GetConfig,
        ValidarTabela: _ValidarTabela,
        ListarTabela: _ListarTabela,
        GetMensagem: _GetMensagem,
        SalvarGridConfig: _SalvarGridConfig,
        GetGridConfig: _GetGridConfig,
        VistoMensagem: _VistoMensagem,
        RemoverMensagem: _RemoverMensagem,
        GetFiltroUsuario: _GetFiltroUsuario,
        GravarFiltro:_GravarFiltro,
        CarregarUsuarioAlerta: _CarregarUsuarioAlerta,
        EnviarMensagem:_EnviarMensagem
    };

});

