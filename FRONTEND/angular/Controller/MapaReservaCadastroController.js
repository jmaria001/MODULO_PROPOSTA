angular.module('App').controller('MapaReservaCadastroController', ['$scope', '$rootScope', '$location', 'httpService', '$location', '$routeParams', function ($scope, $rootScope, $location, httpService, $location, $routeParams) {

    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    if ($scope.Parameters.Action == 'Import') {
        $rootScope.routeName = 'Mapa Reserva - Importação de Propostas'
    }
    if ($scope.Parameters.Action == 'New') {
        $rootScope.routeName = 'Inclusão de Mapa Reserva'
    }
    if ($scope.Parameters.Action == 'Edit') {
        $rootScope.routeName = 'Edição de Mapa Reserva'
    }
    //========================Inicializa Scopes
    $scope.CaracContrato = [{ 'Codigo': 'NOR', 'Descricao': 'Normal' },
        { 'Codigo': 'MER', 'Descricao': 'Merchandising' },
        { 'Codigo': 'PAT', 'Descricao': 'Patrocinio' },
        { 'Codigo': 'CAL', 'Descricao': 'Calhau' }];

    $scope.Abrangencia = [{ 'Codigo': 0, 'Descricao': 'Net' }, { 'Codigo': 1, 'Descricao': 'Rede' }, { 'Codigo': 2, 'Descricao': 'Local' }]
    $scope.Periodo_Campanha_Inicio_Original = "";
    $scope.Periodo_Campanha_Termino_Original = "";
    //===========================Carrega Dados do Contrato / Vindo de Proposta
    $scope.CarregaContratoFromProposta = function (pIdEsquema) {
        httpService.Get('MapaReserva/CarregarEsquema/' + pIdEsquema).then(function (response) {
            if (response) {
                $scope.Contrato = response.data;
                $scope.Contrato.Competencia_Text = MesExtenso($scope.Contrato.Competencia);
                if ($scope.Contrato.Numero_Negociacao == 0) {
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
                $scope.Contrato.Operacao = $scope.Parameters.Action;
                $scope.Contrato.Competencia_Text = MesExtenso($scope.Contrato.Competencia);
                $scope.Periodo_Campanha_Inicio_Original = $scope.Contrato.Periodo_Campanha_Inicio;
                $scope.Periodo_Campanha_Termino_Original = $scope.Contrato.Periodo_Campanha_Termino;
                if ($scope.Parameters.Action == 'New') {
                    $scope.Contrato.Cod_Empresa_Venda= $scope.FnSetEmpresaDefault('Codigo');
                    $scope.Contrato.Nome_Empresa_Venda = $scope.FnSetEmpresaDefault('Nome');
                    $scope.Contrato.Cod_Empresa_Faturamento= $scope.FnSetEmpresaDefault('Codigo');
                    $scope.Contrato.Nome_Empresa_Faturamento= $scope.FnSetEmpresaDefault('Nome');
                }
                
                if ($scope.Contrato.Versao_Projeto == 0) {
                    $scope.Contrato.Versao_Projeto = ""
                };
                if ($scope.Contrato.Numero_Negociacao == 0) {
                    $scope.Contrato.Numero_Negociacao = "";
                };
                if ($scope.Contrato.Vlr_Informado == 0) {
                    $scope.Contrato.Vlr_Informado = "";
                };
                //nao pode alterar qq carac para 'mer'
                if ($scope.Parameters.Action == 'Edit' && $scope.Contrato.Caracteristica_Contrato != 'MER') {
                    $scope.CaracContrato = [{ 'Codigo': 'NOR', 'Descricao': 'Normal' },
                        { 'Codigo': 'PAT', 'Descricao': 'Patrocinio' },
                        { 'Codigo': 'CAL', 'Descricao': 'Calhau' }];
                };
            };
            console.log($scope.Contrato);
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
                            $scope.Contrato.Indica_Midia_Online = $scope.Negociacao.Indica_Midia_Online;
                            console.log($scope.Contrato);
                            if ($scope.Negociacao.Nucleos.length == 1) {
                                $scope.Contrato.Cod_Nucleo = $scope.Negociacao.Nucleos[0].Cod_Nucleo;
                                $scope.Contrato.Nome_Nucleo = $scope.Negociacao.Nucleos[0].Nome_Nucleo;
                                if ($scope.Parameters.Action == 'New') {
                                    $scope.Contrato.Editar_Nucleo = false;
                                }
                            };


                            $scope.Contrato.Competencia_Inicio_Negociacao = response.data[0].Competencia_Inicial;
                            $scope.Contrato.Competencia_Termino_Negociacao = response.data[0].Competencia_Final;
                            if ($scope.Parameters.Action != 'Import') {
                                if ($scope.Negociacao.Empresas_Faturamento.length == 1) {
                                    $scope.Contrato.Cod_Empresa_Faturamento = $scope.Negociacao.Empresas_Faturamento[0].Cod_Empresa;
                                    $scope.Contrato.Nome_Empresa_Faturamento = $scope.Negociacao.Empresas_Faturamento[0].Nome_Empresa;
                                    if ($scope.Parameters.Action == 'New') {
                                        $scope.Contrato.Editar_Empresa_Faturamento = false;
                                    }
                                };
                                if ($scope.Negociacao.Empresas_Venda.length == 1) {
                                    $scope.Contrato.Cod_Empresa_Venda = $scope.Negociacao.Empresas_Venda[0].Cod_Empresa;
                                    $scope.Contrato.Nome_Empresa_Venda = $scope.Negociacao.Empresas_Venda[0].Nome_Empresa;
                                    if ($scope.Parameters.Action == 'New') {
                                        $scope.Contrato.Editar_Empresa_Venda = false;
                                    }
                                };
                                if ($scope.Negociacao.Clientes.length == 1) {
                                    $scope.Contrato.Cod_Cliente = $scope.Negociacao.Clientes[0].Cod_Cliente;
                                    $scope.Contrato.Nome_Cliente = $scope.Negociacao.Clientes[0].Nome_Cliente;
                                    if ($scope.Parameters.Action == 'New') {
                                        $scope.Contrato.Editar_Cliente = false;
                                    }
                                };
                                if ($scope.Negociacao.Agencias.length == 1) {
                                    $scope.Contrato.Cod_Agencia = $scope.Negociacao.Agencias[0].Cod_Agencia;
                                    $scope.Contrato.Nome_Agencia = $scope.Negociacao.Agencias[0].Nome_Agencia;
                                    if ($scope.Parameters.Action == 'New') {
                                        $scope.Contrato.Editar_Agencia = false;
                                    }
                                };
                                if ($scope.Negociacao.Contatos.length == 1) {
                                    $scope.Contrato.Cod_Contato = $scope.Negociacao.Contatos[0].Cod_Contato;
                                    $scope.Contrato.Nome_Contato = $scope.Negociacao.Contatos[0].Nome_Contato;
                                    if ($scope.Parameters.Action == 'New') {
                                        $scope.Contrato.Editar_Contato = false;
                                    }
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
    //===========================Mudou o Tipo de Midia
    $scope.TipoMidiaChange = function (pContrato) {
        if (pContrato.Cod_Tipo_Midia) {
            httpService.Get("GetTipoMidiaData/" + pContrato.Cod_Tipo_Midia.trim()).then(function (response) {
                if (response.data) {
                    pContrato.Indica_Midia_Online = response.data.Indica_Midia_Online;
                    console.log($scope.Contrato);
                };
            });
        };
    };
    //===========================Adicionar Linhas de Comercial
    $scope.AdicionarComercial = function () {
        $scope.Contrato.Comerciais.push({});
    }
    //===============Clicou na lupa Tpo do Comercial 
    $scope.PesquisaTipoComercial = function (pComercial) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        var _url = ""
        if ($scope.Contrato.Indica_Midia_Online) {
            _url = 'ListarTabela/Tipo_Comercial_OnLine'
        }
        else {
            'ListarTabela/Tipo_Comercial'
        }
        httpService.Get(_url).then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção de Tipo de Comercial";
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) {
                    pComercial.Cod_Tipo_Comercial = value.Codigo;
                    pComercial.Nome_Tipo_Comercial = value.Descricao;
                    pComercial.Cod_Tipo_Comercializacao = value.Tipo_Comercializacao;
                    pComercial.Nome_Tipo_Comercializacao = value.Nome_Tipo_Comercializacao;
                };
                $("#modalTabela").modal(true);
            }
        });
    }
    //===============Validar o Tipo de Comercial
    $scope.TipoComercialChange = function (pComercial) {
        if (!pComercial.Cod_Tipo_Comercial) {
            pComercial.Nome_Tipo_Comercial = "";
            pComercial.Cod_Tipo_Comercializacao = "";
            pComercial.Nome_Tipo_Comercializacao = "";
            return;
        }
        var _url = ""
        if ($scope.Contrato.Indica_Midia_Online) {
            _url = 'ValidarTabela/Tipo_Comercial_Online/'
        }
        else {
            _url = 'ValidarTabela/Tipo_Comercial/'
        };
        httpService.Get(_url + pComercial.Cod_Tipo_Comercial).then(function (response) {
            if (response.data[0].Status == 0) {
                ShowAlert(response.data[0].Mensagem)
                pComercial.Cod_Tipo_Comercial = "";
                pComercial.Nome_Tipo_Comercial = "";
                pComercial.Cod_Tipo_Comercializacao = "";
                pComercial.Nome_Tipo_Comercializacao = "";
            }
            else {
                pComercial.Nome_Tipo_Comercial = response.data[0].Descricao
            }
        });
    }
    //===============Validar Produto
    $scope.ProdutoChange = function (pComercial) {
        if (!pComercial.Cod_Red_Produto) {
            pComercial.Nome_Produto = "";
            return;
        }
        httpService.Get('ValidarTabela/Produto/' + pComercial.Cod_Red_Produto).then(function (response) {
            if (response.data[0].Status == 0) {
                ShowAlert(response.data[0].Mensagem)
                pComercial.Cod_Red_Produto = "";
                pComercial.Nome_Produto = "";
            }
            else {
                pComercial.Nome_Produto = response.data[0].Descricao
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
    //==============================Remover Comercial
    $scope.RemoverComercial = function (pContrato, pComercial) {
        for (var i = 0; i < pContrato.Comerciais.length; i++) {
            if (pContrato.Comerciais[i].Cod_Comercial.trim().toUpperCase() == pComercial.Cod_Comercial.trim().toUpperCase()) {
                pContrato.Comerciais.splice(i, 1);
                break;
            };
        };
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
                if (response.data.length == 0) {
                    ShowAlert("Código Inválido para essa Negociação");
                    $scope.Contrato[pField_Codigo] = "";
                    $scope.Contrato[pField_Descricao] = "";
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

    //===========================Mudou a Abrangencia
    $scope.AbrangenciaChange = function (pContrato) {
        pContrato.Veiculos = [];
        if (pContrato.Indica_Grade == 0) {
            pContrato.Cod_Mercado = "";
            var _url = 'GetVeiculos'
            _url += '?Abrangencia=' + pContrato.Indica_Grade;
            _url += '&Cod_Mercado=' + NullToString(pContrato.Cod_Mercado);
            _url += '&Cod_Empresa=' + NullToString(pContrato.Cod_Empresa_Venda);
            _url += '&Cod_Empresa_Faturamento=' + NullToString(pContrato.Cod_Empresa_Faturamento);
            _url += "&"
            httpService.Get(_url).then(function (response) {
                if (response.data.length > 0) {
                    for (var i = 0; i < response.data.length; i++) {
                        pContrato.Veiculos.push({
                            'Cod_Veiculo': response.data[i].Cod_Veiculo,
                            'Nome_Veiculo': response.data[i].Descricao
                        })
                    }
                }
                else {

                    ShowAlert('Não existe Veículo Net para a Empresa de Venda/Faturamento Informadas')
                    pContrato.Indica_Grade = -1;
                }
            });
        }
    };

    //===========================Selecionar Veiculos
    $scope.SelecionarVeiculos = function (pContrato) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        if (pContrato.Indica_Grade == -1) {
            ShowAlert("Informe a Abrangência antes de Selecionar Veículos");
            return;
        }
        var _url = 'GetVeiculos'
        _url += '?Abrangencia=' + pContrato.Indica_Grade;
        _url += '&Cod_Mercado=' + NullToString(pContrato.Cod_Mercado);
        _url += '&Cod_Empresa=' + NullToString(pContrato.Cod_Empresa_Venda);
        _url += '&Cod_Empresa_Faturamento=' + NullToString(pContrato.Cod_Empresa_Faturamento);
        _url += '&RedeId='
        _url += '&Indica_Midia_Online=' + pContrato.Indica_Midia_Online;
        _url += '&'
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.PesquisaTabelas.Items = response.data;
                $scope.PesquisaTabelas.PreFiltroTexto = "";
                $scope.PesquisaTabelas.PreFilter = false;
                $scope.PesquisaTabelas.Titulo = "Seleção de Veículos";
                $scope.PesquisaTabelas.MultiSelect = true;
                $scope.PesquisaTabelas.ClickCallBack = function (value) {
                    pContrato.Veiculos = [];
                    for (var i = 0; i < value.length; i++) {
                        if (value[i].Selected) {
                            pContrato.Veiculos.push({ 'Cod_Veiculo': value[i].Codigo, 'Nome_Veiculo': value[i].Descricao });
                        };
                    };
                };
                for (var x = 0; x < pContrato.Veiculos.length; x++) {
                    for (var y = 0; y < $scope.PesquisaTabelas.Items.length; y++) {
                        if (pContrato.Veiculos[x].Cod_Veiculo == $scope.PesquisaTabelas.Items[y].Codigo) {
                            $scope.PesquisaTabelas.Items[y].Selected = true;
                        }
                    }
                };
                $("#modalTabela").modal(true);
            };
        });
    };
    //==========================Mudou o Mercado 
    $scope.MercadoChange = function (pContrato) {
        pContrato.Veiculos = [];
        if (pContrato.Cod_Mercado) {
            var _url = 'GetVeiculos'
            _url += '?Abrangencia=' + pContrato.Indica_Grade;
            _url += '&Cod_Mercado=' + pContrato.Cod_Mercado
            _url += '&Cod_Empresa=' + NullToString(pContrato.Cod_Empresa_Venda);
            _url += '&Cpd_Empresa_Faturamento=' + NullToString(pContrato.Cod_Empresa_Faturamento);
            _url += "&"
            httpService.Get(_url).then(function (response) {
                if (response.data.length > 0) {
                    for (var i = 0; i < response.data.length; i++) {
                        pContrato.Veiculos.push({
                            'Cod_Veiculo': response.data[i].Cod_Veiculo,
                            'Nome_Veiculo': response.data[i].Descricao
                        });
                    };
                }
                else {
                    pContrato.Cod_Mercado = "";
                    ShowAlert('Mercado Inválido ou não tem Veículos associados');
                };
            });
        };
    };
    //=====================Clicou no X da lista de veiculos selecionados- remover veiculos
    $scope.RemoverVeiculo = function (pCodVeiculo, pContrato) {
        for (var i = 0; i < pContrato.Veiculos.length; i++) {
            if (pContrato.Veiculos[i].Cod_Veiculo == pCodVeiculo) {
                pContrato.Veiculos.splice(i, 1);
                break;
            };
        };
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

    //===========================Adicionar Mídia
    $scope.AdicionarMidia = function (pContrato) {
        if (!pContrato.Periodo_Campanha_Inicio || !pContrato.Periodo_Campanha_Termino) {
            ShowAlert("Informar o Período de Campanha antes de Adicionar Mídias");
            return
        };
        if (pContrato.Comerciais.length == 0) {
            ShowAlert("Adicionar algum comercial antes de Adicionar Mídias");
            return
        };
        if (pContrato.Veiculos.length == 0) {
            ShowAlert("Selecionar os Veículos antes de Adicionar Mídias");
            return
        };
        var _param = {
            'Inicio_Campanha': pContrato.Periodo_Campanha_Inicio,
            'Fim_Campanha': pContrato.Periodo_Campanha_Termino,
            'Cod_Programa': null,
            'Indica_Midia_Online':pContrato.Indica_Midia_Online,
            'Veiculos': pContrato.Veiculos
        }
        httpService.Post('MapaReserva/NewMida', _param).then(function (response) {
            if (response.data) {
                pContrato.Sequenciador_Veiculacao++;
                var _NewVeiculacao = response.data;
                _NewVeiculacao.Id_Veiculacao = pContrato.Sequenciador_Veiculacao;
                if (pContrato.Indica_Midia_Online) {
                    pContrato.VeiculacoesOnLine.push(_NewVeiculacao);
                }
                else {
                    pContrato.Veiculacoes.push(_NewVeiculacao);
                }
                
            }
        });
    };
    //================================Remover Midia
    $scope.RemoverMidia = function (pContrato, pVeiculacao) {
        for (var i = 0; i < pContrato.Veiculacoes.length; i++) {
            if (pContrato.Veiculacoes[i].Id_Veiculacao == pVeiculacao.Id_Veiculacao) {
                pContrato.Veiculacoes.splice(i, 1);
            };
        };
        $scope.AtualizaTemVeiculacao(pContrato);
    };

    //===============================Selecionar programa
    $scope.SelecionarPrograma = function (pContrato, pVeiculacao) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        var _param = { 'Veiculos': pContrato.Veiculos, 'Competencia': pContrato.Competencia.toString().substr(4, 2) + '/' + pContrato.Competencia.toString().substr(0, 4) };
        httpService.Post("GetProgramasGrade", _param).then(function (response) {
            $scope.PesquisaTabelas.Titulo = "Selecionar Programas"
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data
                $scope.PesquisaTabelas.FiltroTexto = ""
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) {
                    pVeiculacao.Cod_Programa = value.Codigo;
                    if (!pContrato.Indica_Midia_Online) {
                        $scope.ProgramaChange(pVeiculacao, pContrato);
                    }
                    
                };
            }
            $("#modalTabela").modal(true);
        });
    }
    //=====================================Validar Programa
    $scope.ProgramaChange = function (pVeiculacao, pContrato) {
        var _param = {
            'Inicio_Campanha': pContrato.Periodo_Campanha_Inicio,
            'Fim_Campanha': pContrato.Periodo_Campanha_Termino,
            'Cod_Programa': pVeiculacao.Cod_Programa,
            'Veiculos': pContrato.Veiculos,
            'Indica_Midia_Online':pContrato.Indica_Midia_Online,
        }
        httpService.Post('MapaReserva/NewMida', _param).then(function (response) {
            if (response.data) {
                var _qtdComGrade = 0
                for (var i = 0; i < response.data.Insercoes.length; i++) {
                    if (response.data.Insercoes[i].Tem_Grade) {
                        _qtdComGrade++;
                    };
                };
                pVeiculacao.Insercoes = response.data.Insercoes;
                for (var i = 0; i < pVeiculacao.Insercoes.length; i++) {
                    pVeiculacao.Insercoes[i].Id_Veiculacao = pVeiculacao.Id_Veiculacao;
                }
                if (_qtdComGrade == 0) {
                    if (pVeiculacao.Cod_Programa) {
                        ShowAlert("Programa Inválido ou não tem Grade no Período")
                        pVeiculacao.Cod_Programa = "";
                    };
                }
            };
        });
    }
    //===============================Selecionar Caracteririca_Veiculacao
    $scope.SelecionarCaracteristica = function (pContrato, pVeiculacao) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        httpService.Get('ListarTabela/Caracteristica_Veiculacao').then(function (response) {
            $scope.PesquisaTabelas.Titulo = "Selecionar Característica"
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data
                $scope.PesquisaTabelas.FiltroTexto = ""
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) {
                    pVeiculacao.Cod_Caracteristica = value.Codigo;
                };
            }
            $("#modalTabela").modal(true);
        });
    };
    //=====================================Validar Programa on Line 
    $scope.ProgramaOnlineChange = function (pVeiculacao, pContrato) {
        var _param = {
            'Inicio_Campanha': pContrato.Periodo_Campanha_Inicio,
            'Fim_Campanha': pContrato.Periodo_Campanha_Termino,
            'Cod_Programa': pVeiculacao.Cod_Programa,
            'Veiculos': pContrato.Veiculos,
            'Indica_Midia_Online': pContrato.Indica_Midia_Online,
        }
        httpService.Post('MapaReserva/ValidarGradePeriodo', _param).then(function (response) {
            if (response.data) {
                if (response.data.length == 0) {
                    ShowAlert("Programa Inválido ou não tem Grade no Período")
                    pVeiculacao.Cod_Programa = "";
                }
            };
        });
    };
    //===============================Validar Caracteristica
    $scope.CaracVeiculacaoChange = function (pVeiculacao) {
        if (pVeiculacao.Cod_Caracteristica) {
            httpService.Get('ValidarTabela/Caracteristica_Veiculacao/' + pVeiculacao.Cod_Caracteristica.trim()).then(function (response) {
                if (response.data[0].Status == 0) {
                    ShowAlert(response.data[0].Mensagem)
                    pVeiculacao.Cod_Caracteristica = "";
                }
            });
        };
    };
    //===============================Selecionar Comercial
    $scope.SelecionarComercial = function (pContrato, pVeiculacao) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        $scope.PesquisaTabelas.Titulo = "Selecionar Comercial"
        for (var i = 0; i < pContrato.Comerciais.length; i++) {
            $scope.PesquisaTabelas.Items.push({
                'Codigo': pContrato.Comerciais[i].Cod_Comercial,
                'Descricao': pContrato.Comerciais[i].Titulo_Comercial,
                'Cod_Tipo_Comercializacao': pContrato.Comerciais[i].Cod_Tipo_Comercializacao,
                'Nome_Tipo_Comercializacao': pContrato.Comerciais[i].Nome_Tipo_Comercializacao,
            });
        };
        $scope.PesquisaTabelas.FiltroTexto = ""
        $scope.PesquisaTabelas.MultiSelect = false;
        $scope.PesquisaTabelas.ClickCallBack = function (value) {
            pVeiculacao.Cod_Comercial = value.Codigo;
            pVeiculacao.Titulo_Comercial = value.Descricao;
            pVeiculacao.Cod_Tipo_Comercializacao = value.Cod_Tipo_Comercializacao;
            pVeiculacao.Nome_Tipo_Comercializacao = value.Nome_Tipo_Comercializacao;
        };
        $("#modalTabela").modal(true);
    };
    //=================================Atualiza Indicador de Tem_Veiculacao no comercial
    $scope.AtualizaTemVeiculacao = function (pContrato) {
        for (var x = 0; x < pContrato.Comerciais.length; x++) {
            pContrato.Comerciais[x].Tem_Veiculacao = false;
            for (var y = 0; y < pContrato.Veiculacoes.length; y++) {
                if (pContrato.Comerciais[x].Cod_Comercial.trim().toUpperCase() == pContrato.Veiculacoes[y].Cod_Comercial.trim().toUpperCase()) {
                    pContrato.Comerciais[x].Tem_Veiculacao = true;
                };
            }
        }
    }
    //=================================Validar comercial da veiculacao
    $scope.VeiculacaoComercialChange = function (pContrato, pVeiculacao) {
        var _valido = false;
        pVeiculacao.Titulo_Comercial = "";
        pVeiculacao.Cod_Tipo_Comercializacao= "";
        pVeiculacao.Nome_Tipo_Comercializacao= "";
        if (pVeiculacao.Cod_Comercial) {
            for (var i = 0; i < pContrato.Comerciais.length; i++) {
                if (pVeiculacao.Cod_Comercial.toUpperCase().trim() == pContrato.Comerciais[i].Cod_Comercial.toUpperCase().trim()) {
                    if (pContrato.Comerciais[i].Cod_Comercial && pContrato.Comerciais[i].Cod_Red_Produto ) {
                        pVeiculacao.Titulo_Comercial = pContrato.Comerciais[i].Titulo_Comercial;
                        pVeiculacao.Cod_Tipo_Comercializacao = pContrato.Comerciais[i].Cod_Tipo_Comercializacao;
                        pVeiculacao.Nome_Tipo_Comercializacao = pContrato.Comerciais[i].Nome_Tipo_Comercializacao;
                        _valido = true;
                        break;
                    };
                };
            };
            if (!_valido) {
                ShowAlert("Comercial não existe ou falta preencher algum  campo");
                pVeiculacao.Cod_Comercial = "";
                pVeiculacao.Titulo_Comercial = "";
                pVeiculacao.Cod_Tipo_Comercializacao= "";
                pVeiculacao.Nome_Tipo_Comercializacao= "";
            };
        };
        $scope.AtualizaTemVeiculacao(pContrato);
    };
    //===================================Totaliza Veiculacaos
    $scope.fnTotalizaVeiculacao = function (pVeiculacao) {
        _total = 0
        for (var i = 0; i < pVeiculacao.Insercoes.length; i++) {
            _total += +pVeiculacao.Insercoes[i].Qtd;
        }
        pVeiculacao.Qtd_Total = _total;
    }

    //===================================Preencher linha das insercoes
    $scope.PreencherLinha = function (pVeiculacao) {
        var _qtd = 1
        if (pVeiculacao.Qtd_Replicar) {
            _qtd = pVeiculacao.Qtd_Replicar + 1;
        };
        for (var i = 0; i < pVeiculacao.Insercoes.length; i++) {
            if (pVeiculacao.Insercoes[i].Tem_Grade && pVeiculacao.Insercoes[i].Valido) {
                pVeiculacao.Insercoes[i].Qtd = _qtd;
            }
        }
        $scope.fnTotalizaVeiculacao(pVeiculacao);
        pVeiculacao.Qtd_Replicar = _qtd;
    };

    //===================================Change Periodo campanha
    $scope.PeriodoCampanhaChange = function (pContrato) {
        if (pContrato.Periodo_Campanha_Inicio) {
            var _anomes = parseInt(pContrato.Periodo_Campanha_Inicio.substr(6, 4) + pContrato.Periodo_Campanha_Inicio.substr(3, 2));
            if ($scope.Parameters.Action == 'New') {
                pContrato.Competencia = parseInt(pContrato.Periodo_Campanha_Inicio.substr(6, 4) + pContrato.Periodo_Campanha_Inicio.substr(3, 2));
                pContrato.Competencia_Text = MesExtenso(_anomes);
            }
        };
        httpService.Post('MapaReserva/ValidarPeriodo', pContrato).then(function (response) {
            if (response.data) {
                ShowAlert(response.data);
                pContrato.Periodo_Campanha_Inicio = $scope.Periodo_Campanha_Inicio_Original;
                pContrato.Periodo_Campanha_Termino = $scope.Periodo_Campanha_Termino_Original;
            }
            else {
                if (pContrato.Periodo_Campanha_Inicio && pContrato.Periodo_Campanha_Termino) {
                    for (var i = 0; i < pContrato.Veiculacoes.length; i++) {
                        for (var x = 0; x < pContrato.Veiculacoes[i].Insercoes.length; x++) {

                            var d1 = parseInt(pContrato.Periodo_Campanha_Inicio.substr(0,2))
                            var d2 = parseInt(pContrato.Periodo_Campanha_Termino.substr(0, 2))
                            if (pContrato.Veiculacoes[i].Insercoes[x].Dia < d1 || pContrato.Veiculacoes[i].Insercoes[x].Dia > d2) {
                                pContrato.Veiculacoes[i].Insercoes[x].Qtd = 0;
                                pContrato.Veiculacoes[i].Insercoes[x].Valido = false;
                            }
                            else {
                                pContrato.Veiculacoes[i].Insercoes[x].Valido = true;
                            };
                            $scope.fnTotalizaVeiculacao(pContrato.Veiculacoes[i]);
                        };
                    };
                    
                };
            };
        }); 
    }
    //===========================Salvar Contrato
    $scope.SalvarMapaReserva = function (pContrato) {
        httpService.Post('MapaReserva/Salvar', pContrato).then(function (response) {
            if (response.data[0].Status == 1) {
                ShowAlert(response.data[0].Mensagem, 'success');
                if ($scope.Parameters.Action == 'Import') {
                    $location.path('/MapaReservaImport');
                }
                else {
                    $location.path('/MapaReserva');
                }
            }
            else {
                ShowAlert(response.data[0].Mensagem), 'warning';
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


