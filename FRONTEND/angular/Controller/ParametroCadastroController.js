angular.module('App').controller('ParametroCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Variaveis
    $scope.currentTab = "Dados";
    $scope.ShowEditar = false;
    $scope.Valor = { 'Cod_Parametro': '', 'Cod_Empresa_Faturamento': '', 'Cod_Empresa_Venda': '', 'Cod_Veiculo': '', 'Cod_Chave': '', 'Operacao': '' }
    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;
    $scope.PermissaoDesativar = false;
    $scope.PermissaoExcluir = false;
    httpService.Get("credential/Parametro@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/Parametro@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });
    httpService.Get("credential/Parametro@Destroy").then(function (response) {
        $scope.PermissaoExcluir = response.data;
    });
    httpService.Get("credential/Parametro@Activate").then(function (response) {
        $scope.PermissaoDesativar = response.data;
    });
    //});

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.Parametro = "";

    //==========================Busca Dados do Parametro
    $scope.CarregaDados = function () {
        var _url = "GetParametroData/" + $scope.Parameters.Id;
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Parametro = response.data;
                $scope.ShowEditar = false;
            }
        });
    }
    $scope.CarregaDados();
    //==========================Salvar
    $scope.SalvarParametro = function (pParametro, pFrom) {
        $scope.Parametro.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("SalvarParametro", pParametro).then(function (response) {
            if (response) {
                if (pFrom == 'V') {
                    if (response.data[0].Status == 1) {
                        $scope.Parameters.Id = response.data[0].Cod_Parametro;
                        $scope.Parameters.Action = 'Edit';
                        $scope.CarregaDados();
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    };
                };
                if (pFrom == 'P') {
                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                    $location.path("/Parametro");
                };
            };
        });
    };
    //======================Editar o Valor
    $scope.EditarValor = function (pOperacao, pValor) {
        if (pOperacao == 'New') {
            $scope.Valor.Cod_Parametro = $scope.Parametro.Cod_Parametro;
            $scope.Valor.Cod_Empresa_Faturamento = "";
            $scope.Valor.Nome_Empresa_Faturamento = "";
            $scope.Valor.Cod_Empresa_Venda = "";
            $scope.Valor.Nome_Empresa_Venda = "";
            $scope.Valor.Cod_Veiculo = "";
            $scope.Valor.Nome_Veiculo = "";
            $scope.Valor.Cod_Chave = "";
        }
        else {
            $scope.Valor.Cod_Parametro = pValor.Cod_Parametro;
            $scope.Valor.Cod_Empresa_Faturamento = pValor.Cod_Empresa_Faturamento;
            $scope.Valor.Nome_Empresa_Faturamento = pValor.Nome_Empresa_Faturamento;
            $scope.Valor.Cod_Empresa_Venda = pValor.Cod_Empresa_Venda;
            $scope.Valor.Nome_Empresa_Venda = pValor.Nome_Empresa_Venda;
            $scope.Valor.Cod_Veiculo = pValor.Cod_Veiculo;
            $scope.Valor.Nome_Veiculo = pValor.Nome_Veiculo;
            $scope.Valor.Cod_Chave = pValor.Cod_Chave;
            $scope.Valor.Sequenciador = pValor.Sequenciador;
        }
        $scope.Valor.Operacao = pOperacao;
        $scope.ShowEditar = true;
    }
    //======================Salvar o valor
    $scope.SalvarValor = function (pValor) {
        if (pValor.Operacao == 'New') {
            $scope.Parametro.MaxSequenciador++;
            pValor.Sequenciador = $scope.Parametro.MaxSequenciador;
            $scope.Parametro.Valores.push({
                'Cod_Parametro': pValor.Cod_Parametro,
                'Cod_Empresa_Faturamento': pValor.Cod_Empresa_Faturamento,
                'Cod_Empresa_Venda': pValor.Cod_Empresa_Venda,
                'Cod_Veiculo': pValor.Cod_Veiculo,
                'Cod_Chave': pValor.Cod_Chave.toUpperCase(),
                'Sequenciador': pValor.Sequenciador
            });
        }
        else {
            for (var i = 0; i < $scope.Parametro.Valores.length; i++) {
                if (pValor.Sequenciador == $scope.Parametro.Valores[i].Sequenciador) {
                    $scope.Parametro.Valores[i].Cod_Parametro = pValor.Cod_Parametro;
                    $scope.Parametro.Valores[i].Cod_Empresa_Faturamento = pValor.Cod_Empresa_Faturamento;
                    $scope.Parametro.Valores[i].Cod_Empresa_Venda = pValor.Cod_Empresa_Venda;
                    $scope.Parametro.Valores[i].Cod_Veiculo = pValor.Cod_Veiculo;
                    $scope.Parametro.Valores[i].Cod_Chave = pValor.Cod_Chave.toUpperCase();
                }
            }
        }
        $scope.ShowEditar = false;
        $scope.SalvarParametro($scope.Parametro, 'V');
    }
    //======================Remover o valor
    $scope.RemoverValor = function (pValor) {
        swal({
            title: "Tem certeza que deseja Excluir este Valor do Parâmetro ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            for (var i = 0; i < $scope.Parametro.Valores.length; i++) {
                if (pValor.Sequenciador == $scope.Parametro.Valores[i].Sequenciador) {
                    $scope.Parametro.Valores.splice(i, 1);
                    $scope.SalvarParametro($scope.Parametro, 'V');
                    break;
                }
            }
        });

    }
}]);

