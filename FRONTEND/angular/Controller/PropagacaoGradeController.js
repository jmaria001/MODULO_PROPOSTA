angular.module('App').controller('PropagacaoGradeController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {
    //================================Recebe Parametros
    $scope.Parameters = $routeParams;
    //======================================Inicializa Scopes
    $scope.PropagacaoGrade = "";
    $scope.checkBoxVeiculo = false;
    $scope.checkBoxPrograma = false;
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' } 
    httpService.Get('Grade/CarregaParametrosPropagacao').then(function (response) {
        if (response) {
            $scope.ParametrosPropagacao = response.data;
        }
    });
    //=====================Marcar / Desmarcar todos as Veiculos da lista
    $scope.MarcarVeiculo = function () {
        for (var i = 0; i < $scope.ParametrosPropagacao.Veiculos.length; i++) {
            $scope.ParametrosPropagacao.Veiculos[i].Selected = $scope.checkBoxVeiculo;
        }
    }
    //=====================Marcar / Desmarcar todos as Programas da lista
    $scope.MarcarPrograma = function () {
        for (var i = 0; i < $scope.ParametrosPropagacao.Programas.length; i++) {
            $scope.ParametrosPropagacao.Programas[i].Selected = $scope.checkBoxPrograma;
        }
    }
    //===========================Propagar(Salvar)
    $scope.SalvarPropagacaoGrade = function () {
        //--------Consistencias
        var _qtdVeiculo = 0
        for (var i = 0; i < $scope.ParametrosPropagacao.Veiculos.length; i++) {
            if ($scope.ParametrosPropagacao.Veiculos[i].Selected) {
                _qtdVeiculo++;
            }
        }
        if (_qtdVeiculo > 1 && $scope.ParametrosPropagacao.Cod_Veiculo_Origem) {
            ShowAlert("Veículo origem somente pode ser informado quando a propagação for para apenas um veiculo destino");
            return;
        }
        if (_qtdVeiculo == 0) {
            ShowAlert("Nenhum Veículo foi Informado");
            return;
        }
        var _qtdPrograma = 0
        for (var i = 0; i < $scope.ParametrosPropagacao.Programas.length; i++) {
            if ($scope.ParametrosPropagacao.Programas[i].Selected) {
                _qtdPrograma++;
            };
        };
        if (_qtdPrograma == 0) {
            ShowAlert("Nenhum Programa foi Informado");
            return;
        }
        if (!$scope.ParametrosPropagacao.Competencia_Base) {
            ShowAlert("Competência Origem Não Foi Informada");
            return;
        }
        if (!$scope.ParametrosPropagacao.Data_Inicio) {
            ShowAlert("Competência Inicial de Destino não foi Informada");
            return;
        }
        if (!$scope.ParametrosPropagacao.Data_Fim) {
            ShowAlert("Competência Final de Destino não foi Informada");
            return;
        }
        var _mes = $scope.ParametrosPropagacao.Data_Inicio.substr(0, 2);
        var _ano = $scope.ParametrosPropagacao.Data_Inicio.substr(3, 4);
        var _competInicio = parseInt(_ano + _mes);
        _mes = $scope.ParametrosPropagacao.Data_Fim.substr(0, 2);
        _ano = $scope.ParametrosPropagacao.Data_Fim.substr(3, 4);
        var _competFim = parseInt(_ano + _mes);
        _mes = $scope.ParametrosPropagacao.Competencia_Base.substr(0, 2);
        _ano = $scope.ParametrosPropagacao.Competencia_Base.substr(3, 4);
        var _competBase = parseInt(_ano + _mes);
        if (_competFim < _competInicio) {
            ShowAlert("Competência Final deve ser maior que a Competência Inicial");
            return;
        }
        if (_competBase >= _competInicio && _competBase <= _competFim) {
            if (!$scope.ParametrosPropagacao.Cod_Veiculo_Origem) {
                ShowAlert("Não é possível propagar o(s) mesmo(s) veiculo(s) para o mesmo período. Informe o veiculo a ser copiado ou Altere o período.");
                return;
            }
            else {
                for (var i = 0; i < $scope.ParametrosPropagacao.Veiculos.length; i++) {
                    if ($scope.ParametrosPropagacao.Veiculos[i].Selected) {
                        if ($scope.ParametrosPropagacao.Veiculos[i].Cod_Veiculo == $scope.ParametrosPropagacao.Cod_Veiculo_Origem) {
                            ShowAlert("Não é possível propagar o mesmo veiculo para o mesmo período. Desmarque o veiculo na lista ou Altere o período.");
                            return;
                        };
                    };
                };
            }
        }
        httpService.Post('Grade/SalvarPropagacaoGrade', $scope.ParametrosPropagacao).then(function (response) {
            if (response.data[0].Status == 0) {
                ShowAlert(response.data[0].Mensagem, 'error');
            }
            else {
                ShowAlert('Grade Propagada com Sucesso', 'success');
                $location.path("/Grade");
            }
        });
    }
}]);

