angular.module('App').controller('MapaReservaCadastroController', ['$scope', '$rootScope', '$location', 'httpService', '$location', '$routeParams', function ($scope, $rootScope, $location, httpService, $location, $routeParams) {

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    if ($scope.Parameters.Action=='Import') {
        $rootScope.routeName = 'Mapa Reserva - Importação de Propostas'
    }
    if ($scope.Parameters.Action == 'New') {
        //tirar a linha abaixo quando for fazer new
        $location.path('/xxxx'); 
        $rootScope.routeName = 'Inclusão de Mapa Reserva'
    }
    if ($scope.Parameters.Action == 'Edit') {
        //tirar a linha abaixo quando for fazer Edit
        $location.path('/xxxx'); 
        $rootScope.routeName = 'Edição de Mapa Reserva'
    }
    //========================Inicializa Scopes
    $scope.CaracContrato = [{ 'Codigo': 'NOR', 'Descricao': 'Normal' },
        { 'Codigo': 'MER', 'Descricao': 'Merchandising' },
        { 'Codigo': 'PAT', 'Descricao': 'Patrocinio' },
        { 'Codigo': 'CAL', 'Descricao': 'Calhau' }];

    $scope.Abrangencia = [{ 'Codigo': 0, 'Descricao': 'Net' }, { 'Codigo': 1, 'Descricao': 'Rede' }, { 'Codigo': 2, 'Descricao': 'Local' }]
    //$scope.PesquisaTabelas = { "Items": [], 'FiltroTexto': '', ClickCallBack: '', 'Titulo': '', 'MultiSelect': false };

    //===========================Carrega Dados do Contrato / Vindo de Proposta
    $scope.CarregaContratoFromProposta = function (pIdEsquema) {
        httpService.Get('MapaReserva/CarregarEsquema/' + pIdEsquema).then(function (response) {
            if (response) {
                $scope.Contrato = response.data;
                $scope.Contrato.Competencia_Text = MesExtenso($scope.Contrato.Competencia);
                if ($scope.Contrato.Numero_Negociacao==0) {
                    $scope.Contrato.Numero_Negociacao = "";
                }
                $scope.Contrato.Versao_Projeto = "";
                $scope.Contrato.Operacao = $scope.Parameters.Action;
                if ($scope.Contrato.Vlr_Informado == 0) {
                    $scope.Contrato.Vlr_Informado = "";
                }
            }
        });
    };
    //===========================Carrega Dados do Contrato para New ou Edit
    $scope.CarregaContrato = function (pIdContrato) {
        httpService.Get('MapaReserva/GetContrato/' + pIdContrato).then(function (response) {
            if (response) {
                $scope.Contrato = response.data;
                $scope.Contrato.Competencia_Text = MesExtenso($scope.Contrato.Competencia);
                if ($scope.Contrato.Versao_Projeto ==0) {
                    $scope.Contrato.Versao_Projeto = ""
                } ;
                $scope.Contrato.Operacao = $scope.Parameters.Action;
                if ($scope.Contrato.Vlr_Informado == 0) {
                    $scope.Contrato.Vlr_Informado = "";
                }
            }
        });
    };
    //===========================Criar Negociacao Automatica
    $scope.CriarNegociacaoAutomatica = function (pValue) {
        $scope.Contrato.Editar_Negociacao = !pValue;
        $scope.Contrato.Numero_Negociacao = "";
        $scope.Contrato.Editar_Tipo_Midia = pValue
    };
    //===========================Mudou por Conta de Credito
    $scope.ContaCreditoChange = function (pValue) {
        if (pValue) {
            $scope.Contrato.Indica_Apoio = false
        }
    };
    //===========================Mudou Midia de Apoio
    $scope.MidiaApoioChange = function (pValue) {
        if (pValue) {
            $scope.Contrato.Indica_Por_Credito = false
        }
    };

    //===========================Mudou o Numero da Negociacao
    $scope.NegociacaoChange = function (pNegociacao) {
        if (!pNegociacao) {
            $scope.Contrato.Cod_Tipo_Midia = "";
            $scope.Contrato.Editar_Tipo_Midia = true;
            return;
        }
        httpService.Post('MapaReserva/ValidarNegociacao', $scope.Contrato).then(function (response) {
            if (response) {
                if (response.data[0].Status == 0) {
                    ShowAlert(response.data[0].Mensagem)
                    $scope.Contrato.Numero_Negociacao = "";
                    $scope.Contrato.Editar_Tipo_Midia = true;
                }
                else {
                    
                    httpService.Get('Negociacao/Get/?Numero_Negociacao=' + pNegociacao + '&').then(function (responseNegociacao) {
                        if (responseNegociacao) {
                            $scope.Negociacao = responseNegociacao.data;
                            $scope.Contrato.Cod_Tipo_Midia = $scope.Negociacao.Cod_Tipo_Midia;
                            $scope.Contrato.Editar_Tipo_Midia = false;
                            if ($scope.Negociacao.Nucleos.length == 1) {
                                $scope.Contrato.Cod_Nucleo = $scope.Negociacao.Nucleos[0].Cod_Nucleo;
                                $scope.Contrato.Nome_Nucleo = $scope.Negociacao.Nucleos[0].Nome_Nucleo;
                            };
                            if (param.Action != 'Import') {
                                if ($scope.Negociacao.Empresas_Faturamento.length == 1) {
                                    $scope.Contrato.Cod_Empresa_Faturamento = $scope.Negociacao.Empresas_Faturamento[0].Cod_Empresa;
                                    $scope.Contrato.Nome_Empresa_Faturamento = $scope.Negociacao.Empresas_Faturamento[0].Nome_Empresa;
                                };
                                if ($scope.Negociacao.Empresas_Venda.length == 1) {
                                    $scope.Contrato.Cod_Empresa_Venda = $scope.Negociacao.Empresas_Venda[0].Cod_Empresa;
                                    $scope.Contrato.Nome_Empresa_Venda = $scope.Negociacao.Empresas_Venda[0].Nome_Empresa;
                                };
                                if ($scope.Negociacao.Clientes.length == 1) {
                                    $scope.Contrato.Cod_Cliente = $scope.Negociacao.Clientes[0].Cod_Cliente;
                                    $scope.Contrato.Nome_Cliente = $scope.Negociacao.Clientes[0].Nome_Cliente;
                                };
                                if ($scope.Negociacao.Agencias.length == 1) {
                                    $scope.Contrato.Cod_Agencia = $scope.Negociacao.Agencias[0].Cod_Agencia;
                                    $scope.Contrato.Nome_Agencia = $scope.Negociacao.Agencias[0].Nome_Agencia;
                                };
                                if ($scope.Negociacao.Contatos.length == 1) {
                                    $scope.Contrato.Cod_Contato = $scope.Negociacao.Contatos[0].Cod_Contato;
                                    $scope.Contrato.Nome_Contato = $scope.Negociacao.Contatos[0].Nome_Contato;
                                };
                            };
                        }
                    });
                }
            }
        });
    }
    //===========================Localizar Negociacao
    $scope.LocalizarNegociacao = function (pContrato) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        httpService.Post('MapaReserva/LocalizarNegociacao', pContrato).then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data
                $scope.PesquisaTabelas.FiltroTexto = ""
                $scope.PesquisaTabelas.Titulo = "Localizar Negociacao"
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) {
                    $scope.Contrato.Numero_Negociacao = value.Codigo;
                    $scope.NegociacaoChange(value.Codigo);
                };
                $("#modalTabela").modal(true);
            }
        });
    };
    //===========================Adicionar Linhas de Comercial
    $scope.AdicionarComercial = function () {
        $scope.Contrato.Comerciais.push({});
    }
    //===============Clicou na lupa Tpo do Comercial 
    $scope.PesquisaTipoComercial = function (pComercial) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        httpService.Get('ListarTabela/Tipo_Comercial').then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção de Tipo de Comercial";
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pComercial.Cod_Tipo_Comercial = value.Codigo; pComercial.Nome_Tipo_Comercial = value.Descricao };
                $("#modalTabela").modal(true);
            }
        });
    }
    //===============Validar o Tipo de Comercial
    $scope.TipoComercialChange = function (pComercial) {
        if (!pComercial.Cod_Tipo_Comercial) {
            pComercial.Nome_Tipo_Comercial = "";
            return;
        }
        httpService.Get('ValidarTabela/Tipo_Comercial/' + pComercial.Cod_Tipo_Comercial).then(function (response) {
            if (response.data[0].Status == 0) {
                ShowAlert(response.data[0].Mensagem)
                pComercial.Cod_Tipo_Comercial = "";
                pComercial.Nome_Tipo_Comercial = "";
            }
            else {
                pComercial.Nome_Tipo_Comercial = response.data[0].Descricao
            }
        });
    }
    //===============Clicou na lupa Produto do Comercial
    $scope.PesquisaProduto = function (pComercial) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        $scope.PesquisaTabelas.Items = [];
        $scope.PesquisaTabelas.PreFiltroTexto = "";
        $scope.PesquisaTabelas.PreFilter = true;
        $scope.PesquisaTabelas.Titulo = "Seleção de Produtos";
        $scope.PesquisaTabelas.MultiSelect = false;
        if ($scope.Contrato.Cod_Cliente) {
            $scope.PesquisaTabelas.ButtonText = "Mostrar Produtos do Cliente " + $scope.Contrato.Cod_Cliente
            $scope.PesquisaTabelas.ButtonCallBack = function () {
                httpService.Get('MapaReserva/ListarProdutoCliente/' + $scope.Contrato.Cod_Cliente).then(function (response) {
                    $scope.PesquisaTabelas.Items = response.data;
                });
            };
        };
        $scope.PesquisaTabelas.ClickCallBack = function (value) { pComercial.Cod_Red_Produto = value.Codigo; pComercial.Nome_Produto = value.Descricao };
        $scope.PesquisaTabelas.LoadCallBack = function (pFilter) {
            httpService.Get('ListarTabela/Produto/' + pFilter).then(function (response) {
                $scope.PesquisaTabelas.Items = response.data;
            });
        }

        $("#modalTabela").modal(true);
    };
    //===========================Pesquisa  de Empresas/Cliente/Agencia/Nucleo/Contato da Negociacao
    $scope.PesquisaNegociacaoTerceiro = function (pContrato, pTabela) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        if (pContrato.Numero_Negociacao) {
            var _url = 'MapaReserva/GetTerceirosNegociacao'
            _url += '?Numero_Negociacao=' + pContrato.Numero_Negociacao;
            _url += '&Tabela=' + pTabela;
            _url += '&Codigo=';
            _url += '&';
            httpService.Get(_url).then(function (response) {
                $scope.PesquisaTabelas.Items = response.data;
            });
        };
        switch (pTabela.toLowerCase()) {
            case 'cliente':
                $scope.PesquisaTabelas.Titulo = "Pesquisa de Clientes";
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pContrato.Cod_Cliente = value.Codigo; pContrato.Nome_Cliente = value.Descricao };
                if (!pContrato.Numero_Negociacao) {
                    $scope.PesquisaTabelas.PreFilter = true;
                    $scope.PesquisaTabelas.LoadCallBack = function (pFilter) {
                        httpService.Get('ListarTabela/Cliente/' + pFilter).then(function (response) {
                            $scope.PesquisaTabelas.Items = response.data;
                        });
                    };
                };
                break;
            case 'agencia':
                $scope.PesquisaTabelas.Titulo = "Pesquisa de Agências";
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pContrato.Cod_Agencia = value.Codigo; pContrato.Nome_Agencia = value.Descricao };
                if (!pContrato.Numero_Negociacao) {
                    $scope.PesquisaTabelas.PreFilter = true;
                    $scope.PesquisaTabelas.LoadCallBack = function (pFilter) {
                        httpService.Get('ListarTabela/Agencia/' + pFilter).then(function (response) {
                            $scope.PesquisaTabelas.Items = response.data;
                        });
                    };
                };
                break;
            case 'contato':
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pContrato.Cod_Contato = value.Codigo; pContrato.Nome_Contato = value.Descricao };
                if (!pContrato.Numero_Negociacao) {
                    httpService.Get('ListarTabela/Contato').then(function (response) {
                        $scope.PesquisaTabelas.Items = response.data;
                    });
                };
                break;
            case 'nucleo':
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pContrato.Cod_Nucleo = value.Codigo; pContrato.Nome_Nucleo = value.Descricao };
                $scope.PesquisaTabelas.Titulo = "Pesquisa de Nucleos";
                if (!pContrato.Numero_Negociacao) {
                    httpService.Get('ListarTabela/Nucleo').then(function (response) {
                        $scope.PesquisaTabelas.Items = response.data;
                    });
                };
                break;
            case 'empresa_venda':
                $scope.PesquisaTabelas.Titulo = "Pesquisa de Empresas";
                if (!pContrato.Numero_Negociacao) {
                    httpService.Get('ListarTabela/Empresa_Usuario').then(function (response) {
                        $scope.PesquisaTabelas.Items = response.data;
                    });
                }
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pContrato.Cod_Empresa_Venda = value.Codigo; pContrato.Nome_Empresa_Venda = value.Descricao };
                break;
            case 'empresa_faturamento':
                $scope.PesquisaTabelas.Titulo = "Pesquisa de Empresas";
                if (!pContrato.Numero_Negociacao) {
                    httpService.Get('ListarTabela/Empresa_Usuario').then(function (response) {
                        $scope.PesquisaTabelas.Items = response.data;
                    });

                };
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pContrato.Cod_Empresa_Faturamento = value.Codigo; pContrato.Nome_Empresa_Faturamento = value.Descricao };
                break;
            default:
        };
        $("#modalTabela").modal(true);
    };
    //==============================Validar Terceiros da Negociacao - empresas/cliente/agencia/contato e nucleo
    $scope.ValidarNegociacaoTerceiro = function (pContrato, pTabela, pField_Codigo, pField_Descricao) {
        var _Codigo = $scope.Contrato[pField_Codigo].trim();
        if (_Codigo == '') {
            $scope.Contrato[pField_Descricao] = "";
            return;
        }
        var _url = ""
        
        if (pContrato.Numero_Negociacao) {
            var _url = 'MapaReserva/GetTerceirosNegociacao'
            _url += '?Numero_Negociacao=' + pContrato.Numero_Negociacao;
            _url += '&Tabela=' + pTabela;
            _url += '&Codigo=' + _Codigo;
            _url += '&';
            httpService.Get(_url).then(function (response) {
                if (response.data[0].length == 0) {
                    ShowAlert("Código Inválido para essa Negociação")
                }
                else {
                    $scope.Contrato[pField_Descricao] = response.data[0].Descricao;
                };
            });
        }
        else {
            switch (pTabela.toLowerCase()) {
                case 'cliente':
                    _url = 'ValidarTabela/Cliente/' + _Codigo;
                    break
                case 'agencia':
                    _url = 'ValidarTabela/Agencia/' + _Codigo;
                    break
                case 'contato':
                    _url = 'ValidarTabela/Contato/' + _Codigo;
                    break
                case 'nucleo':
                    _url = 'ValidarTabela/Nucleo/' + _Codigo;
                    break
                case 'empresa_venda':
                    _url = 'ValidarTabela/Empresa_Usuario/' + _Codigo;
                    break
                case 'empresa_faturamento':
                    _url = 'ValidarTabela/Empresa_Usuario/' + _Codigo;
                    break
            };

            httpService.Get(_url).then(function (response) {
                if (response.data[0].Status == 0) {
                    ShowAlert(response.data[0].Mensagem);
                    $scope.Contrato[pField_Descricao] = ""
                    $scope.Contrato[pField_Codigo] = ""
                }
                else {
                    $scope.Contrato[pField_Descricao] = response.data[0].Descricao;
                };
            });
        }
    };

    //===========================Cancelar Cadastro
    $scope.CancelarMapaReserva = function (pAction) {
        if (pAction == 'Import') {
            $location.path('/MapaReservaImport')
        }
        else {
            $location.path('/MapaReserva')
        }
    };
    //===========================Salvar Contrato
    $scope.SalvarMapaReserva = function (pContrato) {
        httpService.Post('MapaReserva/Salvar',pContrato).then(function (response) {
            if (response.data[0].Status == 1) {
                ShowAlert(response.data[0].Mensagem, 'success');
                if ($scope.Parameters.Action='Import') {
                    $location.path('/MapaReservaImport');
                }
                else {
                    $location.path('/MapaReserva');
                }
            }
        else {
                ShowAlert(response.data[0].Mensagem),'warning';
            }
        });
    };

    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        if ($scope.Parameters.Action == 'Import') {
            $scope.CarregaContratoFromProposta($scope.Parameters.Id);
        }
        else {
            $scope.CarregaContrato($scope.Parameters.Id);
        }
        $rootScope.routeloading = false;
    });
}]);


