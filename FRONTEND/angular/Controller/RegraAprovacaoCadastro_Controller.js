angular.module('App').controller('RegraAprovacaoCadastro_Controller', ['$scope', '$rootScope', '$routeParams', '$location', 'httpService', '$location', function ($scope, $rootScope, $routeParams, $location, httpService, $location) {

    //============ Inicializa Variaveis Scopes
    $scope.Parameters = $routeParams;
    $scope.PesquisaTabelas = {};
    $scope.ListaAprovadores = { "Items": [], 'FiltroTexto': '', ClickCallBack: '' };
    $rootScope.routeName = 'Regras de Aprovação de Descontos - ' + $scope.Parameters.Action
    $scope.currentRange = -1;

    //=======================Carrega a Regra
    $scope.CarregarRegra = function (pId_Regra) {
        httpService.Get("GetRegraAprovacao/" + pId_Regra).then(function (response) {
            if (response.data) {
                $scope.Regra = response.data;
            }
        });
    };
    $scope.CarregarRegra($scope.Parameters.Id);



    //=======================Selecionar Aprovadores
    $scope.SelecionarAprovadores = function (pRange) {
        $scope.currentRange = pRange;
        $scope.ListaAprovadores = { "Items": [], 'FiltroTexto': '', ClickCallBack: '' };
        httpService.Get("ListarTabela/Usuario").then(function (response) {
            if (response.data) {
                $scope.ListaAprovadores.Items = response.data;
                if (pRange.Aprovadores) {
                    for (var x = 0; x < pRange.Aprovadores.length; x++) {
                        for (var y = 0; y < $scope.ListaAprovadores.Items.length; y++) {

                            if (pRange.Aprovadores[x].Id_Usuario == $scope.ListaAprovadores.Items[y].Codigo) {
                                $scope.ListaAprovadores.Items[y].Selected = true;
                                $scope.ListaAprovadores.Items[y].Tipo = (pRange.Aprovadores[x].Indica_Obrigatorio) ? "1" : "2";
                            }
                        }
                    }
                }
                $('#ModalSelecaoUsuario').modal('show');
            }
        })
    }
    //=======================Ok em selecao de aprovadores
    $scope.SelecaoAprovadorOk = function () {
        $scope.currentRange.Aprovadores = [];
        for (var i = 0; i < $scope.ListaAprovadores.Items.length; i++) {
            if ($scope.ListaAprovadores.Items[i].Selected) {
                var _temp = {
                    'Id_Range': $scope.currentRange.Id_Range,
                    'Id_Usuario': $scope.ListaAprovadores.Items[i].Codigo,
                    'Nome_Usuario': $scope.ListaAprovadores.Items[i].Descricao,
                    'Indica_Obrigatorio': ($scope.ListaAprovadores.Items[i].Tipo == '1') ? true : false
                }
                $scope.currentRange.Aprovadores.push(_temp);
            }
        }
    }
    //=======================Remove o Aprovador
    $scope.RemoverAprovador = function (pId_Range, pId_Usuario) {
        for (var i = 0; i < $scope.Regra.Range.length; i++) {
            if ($scope.Regra.Range[i].Id_Range == pId_Range) {
                for (var y = 0; y < $scope.Regra.Range[i].Aprovadores.length; y++) {
                    if ($scope.Regra.Range[i].Aprovadores[y].Id_Usuario == pId_Usuario) {
                        $scope.Regra.Range[i].Aprovadores.splice(y, 1);
                        break;
                    }
                }
            };
        }
    }
    //=======================Adicionar Faixa de Desconto
    $scope.AdicionarFaixa = function () {
        $scope.Regra.Max_Id_Range++;
        $scope.Regra.Range.push({ 'Id_Range': $scope.Regra.Max_Id_Range });
    }
    //=======================Excluir Range
    $scope.ExcluirRange = function (pId_Range) {
        for (var i = 0; i < $scope.Regra.Range.length; i++) {
            if ($scope.Regra.Range[i].Id_Range == pId_Range) {
                $scope.Regra.Range.splice(i, 1);
                break;
            }
        }
    }
    //====================Selecionar Empresas 
    $scope.SelecionarEmpresas = function () {
        httpService.Get('ListarTabela/Empresa_Usuario').then(function (response) {
            $scope.PesquisaTabelas = {}
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data
                $scope.PesquisaTabelas.FiltroTexto = ""
                $scope.PesquisaTabelas.Titulo = 'Seleção de Empresas';
                $scope.PesquisaTabelas.MultiSelect = true;
                for (var x = 0; x < $scope.Regra.Empresas.length; x++) {
                    for (var y = 0; y < $scope.PesquisaTabelas.Items.length; y++) {
                        if ($scope.Regra.Empresas[x].Cod_Empresa == $scope.PesquisaTabelas.Items[y].Codigo) {
                            $scope.PesquisaTabelas.Items[y].Selected = true;
                            break;
                        }
                    }
                }
                $scope.PesquisaTabelas.ClickCallBack = function (value) {
                    $scope.Regra.Empresas = [];
                    for (var i = 0; i < $scope.PesquisaTabelas.Items.length; i++) {
                        if ($scope.PesquisaTabelas.Items[i].Selected) {
                            $scope.Regra.Empresas.push({ "Cod_Empresa": $scope.PesquisaTabelas.Items[i].Codigo, 'Nome_Empresa': $scope.PesquisaTabelas.Items[i].Descricao });
                        }
                    }
                }
                $("#modalTabela").modal(true);
            }
        });
    }
    $scope.RemoverEmpresa = function (pEmpresa) {
        for (var i = 0; i < $scope.Regra.Empresas.length; i++) {
            if ($scope.Regra.Empresas[i].Cod_Empresa == pEmpresa.Cod_Empresa) {
                $scope.Regra.Empresas.splice(i, 1);
                break;
            }
        }
    }
    //====================Selecionar Veiculos 
    $scope.SelecionarVeiculos = function () {
        httpService.Get('ListarTabela/Veiculo_Usuario').then(function (response) {
            $scope.PesquisaTabelas = {}
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data
                $scope.PesquisaTabelas.FiltroTexto = ""
                $scope.PesquisaTabelas.Titulo = 'Seleção de Veiculos';
                $scope.PesquisaTabelas.MultiSelect = true;
                for (var x = 0; x < $scope.Regra.Veiculos.length; x++) {
                    for (var y = 0; y < $scope.PesquisaTabelas.Items.length; y++) {
                        if ($scope.Regra.Veiculos[x].Cod_Veiculo == $scope.PesquisaTabelas.Items[y].Codigo) {
                            $scope.PesquisaTabelas.Items[y].Selected = true;
                            break;
                        }
                    }
                }
                $scope.PesquisaTabelas.ClickCallBack = function (value) {
                    $scope.Regra.Veiculos = [];
                    for (var i = 0; i < $scope.PesquisaTabelas.Items.length; i++) {
                        if ($scope.PesquisaTabelas.Items[i].Selected) {
                            $scope.Regra.Veiculos.push({ "Cod_Veiculo": $scope.PesquisaTabelas.Items[i].Codigo, 'Nome_Veiculo': $scope.PesquisaTabelas.Items[i].Descricao });
                        }
                    }
                }
                $("#modalTabela").modal(true);
            }
        });
    }
    $scope.RemoverVeiculo = function (pVeiculo) {
        for (var i = 0; i < $scope.Regra.Veiculos.length; i++) {
            if ($scope.Regra.Veiculos[i].Cod_Veiculo == pVeiculo.Cod_Veiculo) {
                $scope.Regra.Veiculos.splice(i, 1);
                break;
            }
        }
    }
    //====================Selecionar Agencias 
    $scope.SelecionarAgencias = function () {
        $scope.PesquisaTabelas = {
            'Items': [],
            'FiltroTexto': '',
            'PreFiltroTexto': '',
            'PreFilter': true,
            'Titulo': 'Seleção de Agencias',
            'MultiSelect': true,
            'ClickCallBack': function (value) {
                for (var i = 0; i < $scope.PesquisaTabelas.Items.length; i++) {
                    if ($scope.PesquisaTabelas.Items[i].Selected) {
                        var _exist = false
                        for (var y = 0; y < $scope.Regra.Agencias.length; y++) {
                            if ($scope.Regra.Agencias[y].Cod_Agencia == $scope.PesquisaTabelas.Items[i].Codigo) {
                                _exist = true;
                                break;
                            }
                        }
                        if (!_exist) {
                            $scope.Regra.Agencias.push({ "Cod_Agencia": $scope.PesquisaTabelas.Items[i].Codigo, 'Nome_Agencia': $scope.PesquisaTabelas.Items[i].Descricao });
                        }
                    }
                }
            },
            'LoadCallBack': function (pFilter) {
                httpService.Get('ListarTabela/Agencia/' + pFilter.trim()).then(function (response) {
                    if (response.data) {
                        $scope.PesquisaTabelas.Items = response.data
                        for (var x = 0; x < $scope.Regra.Agencias.length; x++) {
                            for (var y = 0; y < $scope.PesquisaTabelas.Items.length; y++) {
                                if ($scope.Regra.Agencias[x].Cod_Agencia == $scope.PesquisaTabelas.Items[y].Codigo) {
                                    $scope.PesquisaTabelas.Items[y].Selected = true;
                                    break;
                                }
                            }
                        }
                    }
                });
            },
        };
        $("#modalTabela").modal(true);
    }
    $scope.RemoverAgencia = function (pAgencia) {
        for (var i = 0; i < $scope.Regra.Agencias.length; i++) {
            if ($scope.Regra.Agencias[i].Cod_Agencia == pAgencia.Cod_Agencia) {
                $scope.Regra.Agencias.splice(i, 1);
                break;
            }
        }
    }
    //====================Selecionar Clientes
    $scope.SelecionarClientes = function () {
        $scope.PesquisaTabelas = {
            'Items': [],
            'FiltroTexto': '',
            'PreFiltroTexto':'',
            'PreFilter': true,
            'Titulo': 'Seleção de Clientes',
            'MultiSelect': true,
            'ClickCallBack': function (value) {
                for (var i = 0; i < $scope.PesquisaTabelas.Items.length; i++) {
                    if ($scope.PesquisaTabelas.Items[i].Selected) {
                        var _exist = false
                        for (var y = 0; y < $scope.Regra.Clientes.length; y++) {
                            if ($scope.Regra.Clientes[y].Cod_Cliente == $scope.PesquisaTabelas.Items[i].Codigo) {
                                _exist = true;
                                    break;
                            }
                        }
                        if (! _exist) {
                            $scope.Regra.Clientes.push({ "Cod_Cliente": $scope.PesquisaTabelas.Items[i].Codigo, 'Nome_Cliente': $scope.PesquisaTabelas.Items[i].Descricao });
                        }
                    }
                }
            },
            'LoadCallBack': function (pFilter) {
                httpService.Get('ListarTabela/Cliente/' + pFilter.trim()).then(function (response) {
                    if (response.data) {
                        $scope.PesquisaTabelas.Items = response.data
                        for (var x = 0; x < $scope.Regra.Clientes.length; x++) {
                            for (var y = 0; y < $scope.PesquisaTabelas.Items.length; y++) {
                                if ($scope.Regra.Clientes[x].Cod_Cliente == $scope.PesquisaTabelas.Items[y].Codigo) {
                                    $scope.PesquisaTabelas.Items[y].Selected = true;
                                    break;
                                }
                            }
                        }
                    }
                });
            },
        };
        $("#modalTabela").modal(true);
    }
    $scope.RemoverCliente = function (pCliente) {
        for (var i = 0; i < $scope.Regra.Clientes.length; i++) {
            if ($scope.Regra.Clientes[i].Cod_Cliente == pCliente.Cod_Cliente) {
                $scope.Regra.Clientes.splice(i, 1);
                break;
            }
        }
    }
    $scope.SalvarRegra = function (pRegra) {
        httpService.Post('SalvarRegraAprovacao', $scope.Regra).then(function (response) {
            ShowAlert(response.data[0].Mensagem, (response.data[0].Status == 1) ? 'success' : 'warning');
            if (response.data[0].Status == 1) {
                if ($scope.Parameters.Action == 'New') {
                    $scope.Parameters.Action = 'Edit';
                    $rootScope.routeName = 'Regras de Aprovação de Descontos - ' + $scope.Parameters.Action
                    $scope.Regra.Id_Regra = response.data[0].Id_Regra;
                }
            }
        });
    }
}]);


