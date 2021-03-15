angular.module('App').controller('GeracaoCEController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {

    //----------------------Inicializa scopes
    $scope.ShowGridVei = true;
    $scope.ShowCampos = true;
    $scope.ShowGridLog = false;
    $scope.ShowCritica = false;
    $scope.Veiculos = [];
    $scope.Parametros = { 'Cod_Empresa': '', 'Razao_Social': '', 'Data_Limite': '', 'Indica_Geracao': false };
    $scope.Logs = [];
    $scope.Criticas = [];

    //------------------- Novo Filtro ------------------------
    $scope.NewFilter = function () {
        $scope.Criticas = [];
        $scope.Logs = [];
        $scope.ShowGridLog = false;
        $scope.Parametros = { 'Cod_Empresa': '', 'Razao_Social': '', 'Data_Limite': '', 'Indica_Geracao': false };
        $scope.Veiculos = [];
        $scope.chkMarcar = false;
        $scope.CarregarVeiculos();
    };

    //----------------------Carrega Veiculos
    $scope.CarregarVeiculos = function () {
        $rootScope.routeloading = true;
        $scope.Veiculos = [];
        $scope.ShowGridVei = true;
        $scope.ShowCampos = true;
        $scope.ShowGridLog = false;
        $scope.ShowCritica = false;
        httpService.Post('GeracaoCEListaVeiculos').then(function (response) {
            if (response) {
                $scope.Veiculos = response.data;
            }
        });
    };

    //----------------------Marca/Desmarca Todos
    $scope.MarcarTodos = function (pVeiculo, pValue) {
        for (var i = 0; i < pVeiculo.length; i++) {
            pVeiculo[i].Indica_Marcado = pValue;
        }
    };

    //----------------------fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.CarregarVeiculos();
    });

    //------------------- Adiciona Linha de Total no grid de Log------------------------
    $scope.MostraTotal = function (pLog) {
        var _pendente = 0;
        var _gerado = 0;
        var _criticado = 0;
        for (var i = 0; i < pLog.length; i++) {
            _pendente += pLog[i].Qtde_Pendente;
            _gerado += pLog[i].Qtde_Gerado;
            _criticado += pLog[i].Qtde_Criticado;
        };
        pLog.push({
            'Cod_Empresa_Faturamento': 'Total',
            'Qtde_Pendente': _pendente,
            'Qtde_Gerado': _gerado,
            'Qtde_Criticado': _criticado
        });
    };

    //------------------- Carrega Contratos Pendentes ------------------------
    $scope.GeracaoCECarregaPendentes = function (pParam, pVeiculos) {
        var bolMarcadoVeic = false;
        for (var i = 0; i < pVeiculos.length; i++) {
            if (pVeiculos[i].Indica_Marcado) {
                bolMarcadoVeic = true;
                break;
            };
        };
        if (!pParam.Data_Limite && !pParam.Cod_Empresa && !bolMarcadoVeic) {
            return;
        };
        if (!pParam.Data_Limite) {
            return;
        }
        else
            if (!pParam.Cod_Empresa && !bolMarcadoVeic) {
                return;
            };
        var _dia = parseInt(pParam.Data_Limite.substr(0, 2));
        var _mes = parseInt(pParam.Data_Limite.substr(3, 2));
        var _ano = parseInt(pParam.Data_Limite.substr(6, 4));
        var _datalim = new Date(_ano, _mes - 1, _dia).toDateString();  //--pega só a data sem a hora
        var _hoje = new Date().toDateString();  //--pega só a data de hoje sem a hora
        if (_datalim > _hoje) {
            ShowAlert("Data Limite não pode ser maior que hoje");
            return;
        };
        $rootScope.routeloading = true;
        $scope.Logs = [];
        $scope.ShowGridLog = true;
        var _data = {
            'Cod_Empresa': pParam.Cod_Empresa,
            'Data_Limite': pParam.Data_Limite,
            'Indica_Geracao': false,
            'Veiculos': pVeiculos
        };
        httpService.Post('GeraCE_CarregaPendentes', _data).then(function (response) {
            if (response) {
                $scope.Logs = response.data;
                $scope.MostraTotal($scope.Logs);
            };
        });
    };

    //------------------Quando mudar o filtro, carrega os dados----------------
    $scope.$watch('[Parametros.Cod_Empresa,Parametros.Data_Limite,Veiculos]', function (newValue, oldValue) {
        if (newValue != oldValue) {
            $scope.GeracaoCECarregaPendentes($scope.Parametros,$scope.Veiculos)
        }
    });

    //------------------- Gerar Comprovante de Exibição ------------------------
    $scope.GerarComprovante = function (pParam, pVeiculos) {
        var bolMarcadoVeic = false;
        for (var i = 0; i < pVeiculos.length; i++) {
            if (pVeiculos[i].Indica_Marcado) {
                bolMarcadoVeic = true;
                break;
            };
        };
        if (!pParam.Data_Limite && !pParam.Cod_Empresa && !bolMarcadoVeic) {
            return;
        };
        if (!pParam.Data_Limite) {
            return;
        }
        else
            if (!pParam.Cod_Empresa && !bolMarcadoVeic) {
                ShowAlert("É necessário selecionar pelo menos uma Empresa ou um Veículo para gerar comprovantes");
                return;
            };
        var _dia = parseInt(pParam.Data_Limite.substr(0, 2));
        var _mes = parseInt(pParam.Data_Limite.substr(3, 2));
        var _ano = parseInt(pParam.Data_Limite.substr(6, 4));
        var _datalim = new Date(_ano, _mes - 1, _dia).toDateString();  //--pega só a data sem a hora
        var _hoje = new Date().toDateString();  //--pega só a data de hoje sem a hora
        if (_datalim > _hoje) {
            ShowAlert("Data Limite não pode ser maior que hoje");
            return;
        };
        $rootScope.routeloading = true;
        $scope.Logs = [];
        $scope.ShowGridLog = true;
        var _data = {
            'Cod_Empresa': pParam.Cod_Empresa,
            'Data_Limite': pParam.Data_Limite,
            'Indica_Geracao': true,
            'Veiculos': pVeiculos
        };
        httpService.Post('GeraCE_CarregaPendentes', _data).then(function (response) {
            if (response) {
                $scope.Logs = response.data;
                $scope.MostraTotal($scope.Logs);
            };
        });
    };

    //---------------------- Ver Critica -------------------------------
    $scope.VerCritica = function (pParam) {
        $rootScope.routeloading = true;
        $scope.Criticas = [];
        $scope.ShowGridVei = false;
        $scope.ShowCampos = false;
        $scope.ShowGridLog = false;
        $scope.ShowCritica = true;
        httpService.Post('Carrega_Criticas', pParam).then(function (response) {
            if (response) {
                $scope.Criticas = response.data;
            }
        });
    }

}]);


