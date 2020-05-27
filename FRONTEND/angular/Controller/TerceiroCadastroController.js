angular.module('App').controller('TerceiroCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Variaveis
    $scope.currentTab = "Dados";
    $scope.PesquisaTabelas = { "Items": [], 'FiltroTexto': '', ClickCallBack: '', 'Titulo': '', 'MultiSelect': false };
    $scope.Pessoa = [{ 'Codigo': 0, 'Descricao': 'Jurídico' }, { 'Codigo': 1, 'Descricao': 'Física' }, { 'Codigo': 2, 'Descricao': 'Outros' }, ]
    $scope.Porte = [{ 'Codigo': 1, 'Descricao': 'Grande' }, { 'Codigo': 2, 'Descricao': 'Médio' }, { 'Codigo': 3, 'Descricao': 'Pequeno' }, ]
    $scope.ListadeEmpresas = [];
    $scope.CurrentEmpresa = 0;
    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;
    $scope.PermissaoDesativar = false;
    $scope.PermissaoExcluir = false;
    httpService.Get("credential/Terceiro@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/Terceiro@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });
    httpService.Get("credential/Terceiro@Destroy").then(function (response) {
        $scope.PermissaoExcluir = response.data;
    });
    httpService.Get("credential/Terceiro@Activate").then(function (response) {
        $scope.PermissaoDesativar = response.data;
    });

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.Terceiro = "";

    if ($scope.Parameters.Action == 'Edit') {
        $rootScope.routeName = 'Edição de Terceiros'
    }
    else {
        $rootScope.routeName = 'Inclusão de Terceiros'
    }

    //==========================Busca dados da Terceiro
    $scope.CarregaDados = function () {
        var _url = "GetTerceiroData/" + $scope.Parameters.Id;
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Terceiro = response.data;
            }
            if ($scope.Parameters.Action == 'New') {
                $scope.Terceiro.Funcao = "";
            }
            if ($scope.Parameters.Action == 'Dados') {
                $scope.Terceiro.Permite_Editar = false;
            }
        });
    }
    $scope.CarregaDados();

    //==========================Salvar
    $scope.SalvarTerceiro = function (pTerceiro) {
        $scope.Terceiro.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        for (var i = 0; i < $scope.Terceiro.Enderecos.length; i++) {
            $scope.Terceiro.Enderecos[i].Base_Edicao = false;
            if ($scope.Terceiro.Enderecos[i].Cod_Empresa == $scope.Terceiro.Enderecos[$scope.CurrentEmpresa].Cod_Empresa) {
                $scope.Terceiro.Enderecos[i].Base_Edicao = true;
            }
        };
        for (var i = 0; i < $scope.Terceiro.Complementar.length; i++) {
            $scope.Terceiro.Complementar[i].Base_Edicao = false;
            if ($scope.Terceiro.Complementar[i].Cod_Empresa == $scope.Terceiro.Complementar[$scope.CurrentEmpresa].Cod_Empresa) {
                $scope.Terceiro.Complementar[i].Base_Edicao = true;
            }
        };

        httpService.Post("SalvarTerceiro", pTerceiro).then(function (response) {
            if (response) {
                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/Terceiro")
                }
            else {
                ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };
    //======================Seta Empresa 
    $scope.SetaEmpresa = function (pIndex) {

        if (pIndex == 1 && $scope.CurrentEmpresa >= $scope.Terceiro.Enderecos.length - 1) {
            return
        }
        if (pIndex == -1 && $scope.CurrentEmpresa == 0) {
            return
        }
        $scope.CurrentEmpresa += pIndex
    };
    //======================Excluir
    $scope.ExcluirTerceiro = function (pTerceiro) {

        swal({
            title: "Tem certeza que deseja Excluir esse  Terceiro ?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("ExcluirTerceiro", pTerceiro).then(function (response) {
                if (response) {
                    if (response.data[0].Indica_Erro == 0) {
                        ShowAlert('Terceiro Excluido com Sucesso', 'success');
                        $location.path("/Terceiro");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });
    };

    //======================Reativar
    $scope.ReativarTerceiro = function (pTerceiro) {
        for (var i = 0; i < $scope.Terceiro.Empresas.length; i++) {
            if ($scope.Terceiro.Empresas[i].Cod_Empresa == pTerceiro.Cod_Empresa) {
                $scope.Terceiro.Empresas[i].Selected = true;
            };
        };
        swal({
            title: "Tem certeza que deseja reativar esta Terceiro ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Reativar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            var _data = { 'Cod_Terceiro': pTerceiro.Cod_Terceiro, 'Complementar': [$scope.Terceiro.Complementar[$scope.CurrentEmpresa]], 'Empresas': $scope.Terceiro.Empresas }
            httpService.Post("ReativarTerceiro", _data).then(function (response) {
                if (response.data) {
                    $scope.CarregaDados();
                }
            });
        });
    };

    //======================Desativar
    $scope.DesativarTerceiro = function (pTerceiro) {
        for (var i = 0; i < $scope.Terceiro.Empresas.length; i++) {
            if ($scope.Terceiro.Empresas[i].Cod_Empresa == pTerceiro.Cod_Empresa) {
                $scope.Terceiro.Empresas[i].Selected = true;
            };
        };
        swal({
            title: "Tem certeza que deseja desativar esta Terceiro ?",
            text: "Motivo do Cancelamento",
            type: "input",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Desativar",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function (inputValue) {
            if (inputValue === false) return false;
            $scope.Terceiro.Complementar[$scope.CurrentEmpresa].Motivo_Desativacao = inputValue;
            var _data = { 'Cod_Terceiro': pTerceiro.Cod_Terceiro, 'Complementar': [$scope.Terceiro.Complementar[$scope.CurrentEmpresa]], 'Empresas': $scope.Terceiro.Empresas }
            httpService.Post("DesativarTerceiro", _data).then(function (response) {
                if (response.data) {
                    $scope.CarregaDados();
                }

            });
        });
    };

    //==========================Selecionar Empresas
    $scope.SelecionarEmpresas = function () {
        $scope.ListadeEmpresas = [];
        for (var i = 0; i < $scope.Terceiro.Empresas.length; i++) {
            $scope.ListadeEmpresas.push({
                'Codigo': $scope.Terceiro.Empresas[i].Cod_Empresa,
                'Descricao': $scope.Terceiro.Empresas[i].Nome_Empresa,
                'Selected': $scope.Terceiro.Empresas[i].Selected
            });
        };
        $scope.PesquisaTabelas.Items = $scope.ListadeEmpresas;
        $scope.PesquisaTabelas.FiltroTexto = "";
        $scope.PesquisaTabelas.Titulo = "Seleção de Empresas";
        $scope.PesquisaTabelas.MultiSelect = true;
        $scope.PesquisaTabelas.MarcarTodos= false;
        $scope.PesquisaTabelas.ClickCallBack = function () {
            $scope.Terceiro.Empresas = [];
            for (var i = 0; i < $scope.ListadeEmpresas.length; i++) {
                $scope.Terceiro.Empresas.push({ 'Cod_Empresa': $scope.ListadeEmpresas[i].Codigo, 'Nome_Empresa': $scope.ListadeEmpresas[i].Descricao, 'Selected': $scope.ListadeEmpresas[i].Selected });
            };
        };
        $("#modalTabela").modal(true);
    }
    //=====================Validacao de Codigo IBGE
    $scope.ValidaIbge = function (pCodigo, pEndereco) {
        var _url = 'GetCodigoIbge/' + pCodigo.trim();
        httpService.Get(_url).then(function (response) {
            if (response.data.length == 0) {
                ShowAlert("Código de Município Inválido", 'warning');
                return;
            }
            else {
                if (pEndereco == 'Principal') {
                    $scope.Terceiro.Enderecos[$scope.CurrentEmpresa].Municipio1 = response.data[0].Nome_Municipio.trim();
                    $scope.Terceiro.Enderecos[$scope.CurrentEmpresa].Uf1 = response.data[0].Sigla_Uf.trim();
                }
                if (pEndereco == 'Complementar') {
                    $scope.Terceiro.Enderecos[$scope.CurrentEmpresa].Municipio2 = response.data[0].Nome_Municipio.trim();
                    $scope.Terceiro.Enderecos[$scope.CurrentEmpresa].Uf2 = response.data[0].Sigla_Uf.trim();
                }
            }
        });
    };
}]);

