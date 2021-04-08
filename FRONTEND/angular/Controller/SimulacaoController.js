angular.module('App').controller('SimulacaoController', ['$scope', '$rootScope', '$filter', '$routeParams', 'httpService', '$timeout', function ($scope, $rootScope, $filter, $routeParams, httpService, $timeout) {
    //============ Inicializa Variaveis Scopes
    $scope.Parameters = $routeParams;
    $scope.currentShow = 'Base';
    $scope.Abrangencias = [{ 'Id': 0, 'Descricao': 'Net' }, { 'Id': 1, 'Descricao': 'Rede' }, { 'Id': 2, 'Descricao': 'Local' }];
    $scope.Forma_Pgto = [{ 'Id': 0, 'Descricao': 'Espécie' }, { 'Id': 1, 'Descricao': 'Permuta' }];
    $scope.Tipo_Vencimento = [{ 'Id': 1, 'Descricao': 'À Vista' }, { 'Id': 2, 'Descricao': 'DFM' }, { 'Id': 3, 'Descricao': 'DDL' }];
    $scope.Indica_Sem_Midia = false;
    $scope.Simulacao = {};
    $scope.currentEsquema = 0;
    $scope.PesquisaTabelas = { "Items": [], 'FiltroTexto': '', ClickCallBack: '', 'Titulo': '', 'MultiSelect': false };
    $scope.IniciarCalculo = false;
    $scope.DescontoDetalhado = "[]";
    $scope.Distribuicao = [{ 'Tipo': 'D', 'Descricao': 'Por Dia' }, { 'Tipo': 'M', 'Descricao': 'No Periodo' }]
    $scope.Info = { 'Title': '', 'Text': '' };
    $scope.GeracaoProposta = {};
    $scope.TotalizadorIndex = 0;
    $scope.TabelaPrecoKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.CompetenciaEsquemaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.HistoricoStatus = [];
    $scope.SetaCompetenciaEsquema = function (pDataInicio, pDataFim) {
        $scope.CompetenciaEsquemaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
        if (pDataInicio) {
            $scope.CompetenciaEsquemaKeys.Year = StringToDate(pDataInicio, 'dd/mm/yyyy').getFullYear();
            var _m = StringToDate(pDataInicio, 'dd/mm/yyyy').getMonth() + 1;
            var _y = StringToDate(pDataInicio, 'dd/mm/yyyy').getFullYear();
            var _ym = parseInt(_y.toString() + LeftZero(_m, 2));
            $scope.CompetenciaEsquemaKeys.First = _ym;
        }
        if (pDataFim) {
            $scope.CompetenciaEsquemaKeys.Year = StringToDate(pDataFim, 'dd/mm/yyyy').getFullYear();
            var _m = StringToDate(pDataFim, 'dd/mm/yyyy').getMonth() + 1;
            var _y = StringToDate(pDataFim, 'dd/mm/yyyy').getFullYear();
            var _ym = parseInt(_y.toString() + LeftZero(_m, 2));
            $scope.CompetenciaEsquemaKeys.Last = _ym;
        }
    }
    //=====================Check se processo é simulacao ou proposta
    if ($scope.Parameters.Processo == 'P') {
        $scope.Descricao_Processo = 'Proposta'
        $rootScope.routeName = 'Proposta - ' + $scope.Parameters.Action
    }
    else {
        $scope.Descricao_Processo = 'Modelo de Vendas'
        $rootScope.routeName = 'Modelo de Vendas - ' + $scope.Parameters.Action
    }
    //==========================Carrega Tabela de Condicao de Pagamento 
    $scope.TipoDesconto = [];
    httpService.Get('ListarTabela/Condica_Pagamento').then(function (response) {
        $scope.Condicao_Pagamento = response.data;
    });
    //==========================Carrega Tabela de Caracteristica do Contrato
    $scope.CaracteristicaContrato = [];
    httpService.Get('ListarTabela/Caracteristica_Contrato').then(function (response) {
        $scope.Caracteristica_Contrato = response.data;
    });
    //=====================Carrega a Simulacao 
    $scope.CarregarSimulacao = function (pId_Simulacao, pProcesso,fromImport) {
        $scope.GeracaoProposta = {};
        httpService.Get("GetSimulacao/" + pId_Simulacao + "/" + pProcesso).then(function (response) {
            if (response.data) {
                $scope.Simulacao = response.data;
                $scope.Indica_Sem_Midia = $scope.Simulacao.Indica_Sem_Midia;
                $scope.TotalizadorIndex = $scope.Simulacao.Totalizadores.length - 1;
                $timeout(function () {
                    $scope.IniciarCalculo = true;
                }, 1000);
                $scope.SetaCompetenciaEsquema($scope.Simulacao.Validade_Inicio, $scope.Simulacao.Validade_Termino);
                InitTermometro($scope.Simulacao.Termometro_Venda, !$scope.Simulacao.Permite_Editar);
                if ($scope.Parameters.Action == 'New' && !fromImport) {
                    $scope.Simulacao.Cod_Empresa_Venda = $scope.FnSetEmpresaDefault('Codigo');
                    $scope.Simulacao.Nome_Empresa_Venda = $scope.FnSetEmpresaDefault('Nome');
                };
            };
        });
    };
    $scope.CarregarSimulacao($scope.Parameters.Id, $scope.Parameters.Processo);
    //===================================Clicou na Aba dos Esquemas 
    $scope.SetCurrenEsquema = function (pIdEsquema) {
        for (var i = 0; i < $scope.Simulacao.Esquemas.length; i++) {
            if ($scope.Simulacao.Esquemas[i].Id_Esquema == pIdEsquema) {
                $scope.currentEsquema = i;
                break
            }
        }
    };
    //===================================Adicionar Esquema
    $scope.AdicionarEsquema = function () {
        $scope.Simulacao.ContadorEsquema++;
        httpService.Get("GetNewEsquema").then(function (response) {
            _tempEsquema = response.data;
            _tempEsquema.Id_Esquema = $scope.Simulacao.ContadorEsquema;
            _tempEsquema.Abrangencia = -1;
            _tempEsquema.RedeId = "";
            _tempEsquema.Cod_Empresa_Faturamento = $scope.FnSetEmpresaDefault("CODIGO");
            $scope.Simulacao.Esquemas.push(angular.copy(_tempEsquema));
        });
    }
    //===================================Adicionar Midia
    $scope.AdicionarMidia = function (pEsquema) {
        $scope.Simulacao.ContadorMidia++;
        var _mmyy = CompetenciaToInt(pEsquema.Competencia);
        httpService.Get("GetNewMidia/" + _mmyy).then(function (response) {
            var _tempMidia = response.data;
            var _year = pEsquema.Competencia.substr(3, 4);
            var _month = pEsquema.Competencia.substr(0, 2);
            var _ref_inicio = new Date(_year, _month - 1, _tempMidia.Dia_Inicio, 0, 0, 0, 0);
            var _ref_fim = new Date(_year, _month - 1, _tempMidia.Dia_Fim, 0, 0, 0, 0);
            if (_ref_inicio < StringToDate($scope.Simulacao.Validade_Inicio)) {
                _tempMidia.Dia_Inicio = StringToDate($scope.Simulacao.Validade_Inicio).getDate();
            };
            if (_ref_fim > StringToDate($scope.Simulacao.Validade_Termino)) {
                _tempMidia.Dia_Fim = StringToDate($scope.Simulacao.Validade_Termino).getDate();
            };
            _tempMidia.Id_Midia = $scope.Simulacao.ContadorMidia;
            _tempMidia.Id_Esquema = pEsquema.Id_Esquema,
            _tempMidia.IsValid = false;
            $scope.Simulacao.Esquemas[$scope.currentEsquema].Midias.push(angular.copy(_tempMidia));
        });
    }
    //==========================Mudou a abrangencia/ Se for Net ja carrega o veiculo
    $scope.Abrangencia_Change = function () {
        $scope.Simulacao.Esquemas[$scope.currentEsquema].Veiculos = [];
        if ($scope.Simulacao.Esquemas[$scope.currentEsquema].Abrangencia == 0) {
            $scope.Simulacao.Esquemas[$scope.currentEsquema].Cod_Mercado = "" // se for net limpa o mercado
            var _url = 'GetVeiculos'
            _url += '?Abrangencia=' + $scope.Simulacao.Esquemas[$scope.currentEsquema].Abrangencia;
            _url += '&Cod_Mercado=' + NullToString($scope.Simulacao.Esquemas[$scope.currentEsquema].Cod_Mercado);
            _url += '&Cod_Empresa=' + NullToString($scope.Simulacao.Cod_Empresa_Venda);
            _url += '&Cod_Empresa_Faturamento=' + NullToString($scope.Simulacao.Esquemas[$scope.currentEsquema].Cod_Empresa_Faturamento);
            _url += '&RedeId=' + $scope.Simulacao.Esquemas[$scope.currentEsquema].RedeId;
            _url += "&"
            httpService.Get(_url).then(function (response) {
                if (response.data) {
                    for (var i = 0; i < response.data.length; i++) {
                        $scope.Simulacao.Esquemas[$scope.currentEsquema].Veiculos.push({
                            'Id_Esquema': $scope.Simulacao.Esquemas[$scope.currentEsquema].Id_Esquema,
                            'Cod_Veiculo': response.data[i].Cod_Veiculo,
                            'Nome_Veiculo': response.data[i].Descricao
                        })
                    }
                }
            });
        }
    }
    //==========================Mudou o Mercado do Esquema
    $scope.MercadoChange = function (pCodMercado) {
        $scope.Simulacao.Esquemas[$scope.currentEsquema].Veiculos = [];
        if (pCodMercado) {
            var _url = 'GetVeiculos'
            _url += '?Abrangencia=' + $scope.Simulacao.Esquemas[$scope.currentEsquema].Abrangencia;
            _url += '&Cod_Mercado=' + pCodMercado
            _url += '&Cod_Empresa=' + NullToString($scope.Simulacao.Cod_Empresa_Venda);
            _url += '&Cod_Empresa_Faturamento=' + NullToString($scope.Simulacao.Esquemas[$scope.currentEsquema].Cod_Empresa_Faturamento);
            _url += '&RedeId=' + $scope.Simulacao.Esquemas[$scope.currentEsquema].RedeId;
            _url += "&"
            httpService.Get(_url).then(function (response) {
                if (response.data.length > 0) {
                    for (var i = 0; i < response.data.length; i++) {
                        $scope.Simulacao.Esquemas[$scope.currentEsquema].Veiculos.push({
                            'Id_Esquema': $scope.Simulacao.Esquemas[$scope.currentEsquema].Id_Esquema,
                            'Cod_Veiculo': response.data[i].Cod_Veiculo,
                            'Nome_Veiculo': response.data[i].Descricao
                        })
                    }
                }
                else {
                    ShowAlert('Mercado Inválido ou não tem Veículos associados', 'warning', 2000, 'topRight');

                }
            });
        }
    }  //==========================Mudou a Rede no Esquema
    $scope.RedeChange = function (pRedeId) {
        $scope.Simulacao.Esquemas[$scope.currentEsquema].Veiculos = [];
        $scope.Simulacao.Esquemas[$scope.currentEsquema].Cod_Mercado = "";
        httpService.Get('ValidarTabela/rede/' + pRedeId).then(function (response) {
            if (response.data) {
                if (response.data[0].Status == 1) {
                    $scope.Simulacao.Esquemas[$scope.currentEsquema].BackColorTab = response.data[0].Extra;
                };
            };
        });
    };
    //==============================Mudou algum dado da midia
    $scope.fnChangeMidia = function (pMidia, pField) {
        switch (pField) {
            case 'Dia_Inicio':
            case 'Dia_Fim':
            case 'Qtd_Insercoes':
            case 'Programa':
            case 'Distribuicao':
                for (var i = 0; i < pMidia.Insercoes.length; i++) {
                    pMidia.Insercoes[i].Qtd = "";
                    pMidia.IsValid = false;
                }
                break;
            case 'Qtd_Dia_Linha':
                var _tot = 0
                for (var i = 0; i < pMidia.Insercoes.length; i++) {
                    _tot += parseInt(pMidia.Insercoes[i].Qtd ? pMidia.Insercoes[i].Qtd : 0);
                }
                pMidia.Qtd_Total_Insercoes = _tot;
                if (pMidia.Distribuicao == 'M') {
                    pMidia.Qtd_Insercoes = _tot;
                }
                break;
            default:
                break;
        }
        $scope.ChangePendenteCalculo();
    };
    //=====================Consiste dia Inicio e Dia Fim    
    $scope.FnConsisteDia = function (pMidia, pTipo) {
        var _month = $scope.Simulacao.Esquemas[$scope.currentEsquema].Competencia.substr(0, 2);
        var _year = $scope.Simulacao.Esquemas[$scope.currentEsquema].Competencia.substr(3, 4);
        var _primeiro_dia = new Date(_year, _month - 1, 1, 0, 0, 0, 0);
        var _ultimo_dia = LastDay(_year, _month);
        if (_primeiro_dia < StringToDate($scope.Simulacao.Validade_Inicio)) {
            _primeiro_dia = StringToDate($scope.Simulacao.Validade_Inicio);
        };
        if (_ultimo_dia > StringToDate($scope.Simulacao.Validade_Termino)) {
            _ultimo_dia = StringToDate($scope.Simulacao.Validade_Termino);
        };
        var _dia = 0;
        if (pTipo == 'I') {
            _dia = pMidia.Dia_Inicio;
        }
        else {
            _dia = pMidia.Dia_Fim;
        }
        if (_dia > _ultimo_dia.getDate() || _dia < _primeiro_dia.getDate()) {
            ShowAlert('Dia Inválido ou fora da validade do Modelo')
            if (pTipo == 'I') {
                pMidia.Dia_Inicio = "";
            }
            else {
                pMidia.Dia_Fim = "";
            }
        }

    }
    //=====================Clicou em selecionar veiculos
    $scope.SelecionarVeiculos = function () {
        var _url = 'GetVeiculos'
        _url += '?Abrangencia=' + $scope.Simulacao.Esquemas[$scope.currentEsquema].Abrangencia;
        _url += '&Cod_Mercado=' + NullToString($scope.Simulacao.Esquemas[$scope.currentEsquema].Cod_Mercado);
        _url += '&Cod_Empresa=' + NullToString($scope.Simulacao.Cod_Empresa_Venda);
        _url += '&Cod_Empresa_Faturamento=' + NullToString($scope.Simulacao.Esquemas[$scope.currentEsquema].Cod_Empresa_Faturamento);
        _url += '&RedeId=' + $scope.Simulacao.Esquemas[$scope.currentEsquema].RedeId;
        _url += '&'
        httpService.Get(_url).then(function (response) {
            if (response.data) {
                $scope.ListadeVeiculos = response.data;
                for (var x = 0; x < $scope.Simulacao.Esquemas[$scope.currentEsquema].Veiculos.length; x++) {
                    for (var y = 0; y < $scope.ListadeVeiculos.length; y++) {
                        if ($scope.Simulacao.Esquemas[$scope.currentEsquema].Veiculos[x].Cod_Veiculo == $scope.ListadeVeiculos[y].Cod_Veiculo) {
                            $scope.ListadeVeiculos[y].Selected = true
                        }
                    }
                }
                if ($scope.Simulacao.Esquemas[$scope.currentEsquema].Cod_Mercado) {
                    $scope.Simulacao.Esquemas[$scope.currentEsquema].Veiculos = [];
                    for (var x = 0; x < $scope.ListadeVeiculos.length; x++) {
                        $scope.Simulacao.Esquemas[$scope.currentEsquema].Veiculos.push({
                            'Id_Esquema': $scope.Simulacao.Esquemas[$scope.currentEsquema].Id_Esquema, 'Cod_Veiculo': $scope.ListadeVeiculos[x].Cod_Veiculo, 'Nome_Veiculo': $scope.ListadeVeiculos[x].Descricao
                        });
                    }
                }
                else {
                    $("#modalVeiculo").modal(true);
                }
            }
        });
    }
    //=====================Clicou no X da lista de veiculos selecionados- remover veiculos
    $scope.RemoverVeiculo = function (pCodVeiculo) {
        for (var i = 0; i < $scope.Simulacao.Esquemas[$scope.currentEsquema].Veiculos.length; i++) {
            if ($scope.Simulacao.Esquemas[$scope.currentEsquema].Veiculos[i].Cod_Veiculo == pCodVeiculo) {
                $scope.Simulacao.Esquemas[$scope.currentEsquema].Veiculos.splice(i, 1);
                break;
            }
        }
    }
    //=====================Clicou em Ok da Selecão de Veiculos
    $scope.SelecaoVeiculoOk = function () {
        $scope.Simulacao.Esquemas[$scope.currentEsquema].Veiculos = [];
        var _Id_Esquema = $scope.Simulacao.Esquemas[$scope.currentEsquema].Id_Esquema;
        for (var i = 0; i < $scope.ListadeVeiculos.length; i++) {
            if ($scope.ListadeVeiculos[i].Selected) {
                $scope.Simulacao.Esquemas[$scope.currentEsquema].Veiculos.push({ 'Id_Esquema': _Id_Esquema, 'Cod_Veiculo': $scope.ListadeVeiculos[i].Cod_Veiculo, 'Nome_Veiculo': $scope.ListadeVeiculos[i].Descricao });
            }
        }
        $scope.ChangePendenteCalculo();
    }
    //===============Clicou na lupa da digitacao do programa do esquema
    $scope.PesquisaTabela_Grade = function (pMidia) {
        if ($scope.Simulacao.Esquemas[$scope.currentEsquema].Veiculos.length == 0 || !$scope.Simulacao.Esquemas[$scope.currentEsquema].Competencia) {
            ShowAlert('Informe a Competência e selecione os veiculos antes de informar o programa', 'warning', 2000, 'center');
            return
        }
        var _param = { 'Veiculos': $scope.Simulacao.Esquemas[$scope.currentEsquema].Veiculos, 'Competencia': $scope.Simulacao.Esquemas[$scope.currentEsquema].Competencia };
        $scope.PesquisaTabelas = NewPesquisaTabela();
        httpService.Post('GetProgramasGrade', _param).then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data
                $scope.PesquisaTabelas.Titulo = "Seleção de Programas da Grade"
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pMidia.Cod_Programa = value.Codigo; $scope.fnChangeMidia(pMidia, 'Programa') }
                $("#modalTabela").modal(true);
            }
        });
    }
    //===============Clicou na lupa da digitacao de Caracteristica do esquema
    $scope.PesquisaCaracteristica = function (pMidia) {
        httpService.Get('ListarTabela/Caracteristica_Veiculacao').then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data
                $scope.PesquisaTabelas.FiltroTexto = ""
                $scope.PesquisaTabelas.Titulo = "Seleção de Características"
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pMidia.Cod_Caracteristica = value.Codigo; }
                $("#modalTabela").modal(true);
            }
        });
    }
    //===============Clicou na lupa Tpo do Comercial do esquema
    $scope.PesquisaTipoComercial = function (pMidia) {
        httpService.Get('ListarTabela/Tipo_Comercial').then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data
                $scope.PesquisaTabelas.FiltroTexto = ""
                $scope.PesquisaTabelas.Titulo = "Seleção de Tipo de Comercial"
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pMidia.Cod_Tipo_Comercial = value.Codigo; }
                $("#modalTabela").modal(true);
            }
        });
    }

    //===============Controla a Digitacao de Desconto ou Valor Informado da Midia
    $scope.fnControleDescontoValorMidia = function (pField, pMidia) {
        if (pField == 'Desconto') {
            if (pMidia.Desconto_Informado) {
                pMidia.Valor_Informado = "";
            }
        }
        if (pField == 'Valor') {
            if (pMidia.Valor_Informado) {
                pMidia.Desconto_Informado = "";
            }
        }
        $scope.fnChangeMidia(pField)
    };
    //==================================Remover Midia
    $scope.RemoverMidia = function (Midia) {
        for (var i = 0; i < $scope.Simulacao.Esquemas[$scope.currentEsquema].Midias.length; i++) {
            if ($scope.Simulacao.Esquemas[$scope.currentEsquema].Midias[i].Id_Midia == Midia.Id_Midia) {
                $scope.Simulacao.Esquemas[$scope.currentEsquema].Midias.splice(i, 1);
            }
        }
        $scope.ChangePendenteCalculo();
    };
    //==================================Distribuir Insercoes no grid do mapa 
    $scope.DistrubuirInsercoes = function (pEsquema, pMidia) {
        var _data = {
            'Id_Midia': pMidia.Id_Midia,
            'Competencia': pEsquema.Competencia,
            'Cod_Programa': pMidia.Cod_Programa,
            'Cod_Tipo_Comercial': pMidia.Cod_Tipo_Comercial,
            'Cod_Caracteristica': pMidia.Cod_Caracteristica,
            'Qtd_Insercoes': pMidia.Qtd_Insercoes,
            'Distribuicao': pMidia.Distribuicao,
            'Dia_Inicio': pMidia.Dia_Inicio,
            'Dia_Fim': pMidia.Dia_Fim,
            'Veiculos': pEsquema.Veiculos,
            'Validade_Inicio': $scope.Simulacao.Validade_Inicio,
            'Validade_Termino': $scope.Simulacao.Validade_Termino,
            'Cod_Empresa_Faturamento': pEsquema.Cod_Empresa_Faturamento,
            'Duracao': pMidia.Duracao
        };
        httpService.Post("DistribuirInsercoes", _data).then(function (response) {
            if (response) {
                if (response.data[0].Status) {
                    pMidia.Insercoes = response.data;
                    pMidia.IsValid = true;
                    $scope.ChangePendenteCalculo();
                    $scope.SalvarSimulacao($scope.Simulacao, false); //verifica se é viavel ja salvar e revalorar ou espera clicar em recalcular
                }
                else {
                    ShowAlert(response.data[0].Critica, 'warning');
                }
            }
        });
    }
    //===================================watch na tabela de preco porque ui-data nao dispara change
    $scope.$watch('Simulacao.Tabela_Preco', function (newValue, oldValue) {
        if (newValue != oldValue && $scope.IniciarCalculo) {
            $scope.ChangePendenteCalculo();
        }
    });
    //===================================quando mudar a validade inicio ou final setar as chaves da competencia do esquema
    $scope.$watch('[Simulacao.Validade_Inicio,Simulacao.Validade_Termino]', function (newValue, oldValue) {
        $scope.SetaCompetenciaEsquema(newValue[0], newValue[1]);
    });
    //===================================Clicou em Fixar Desconto ou Valor
    $scope.Fixar = function (pTipo) {
        if (pTipo == 'Valor') {
            if ($scope.Simulacao.Fixar_Valor) {

                $scope.Simulacao.Fixar_Desconto = false;
                $scope.Simulacao.Desconto_Padrao = ""
            }
            else {
                $scope.Simulacao.Valor_Informado = "";
            }
        }
        if (pTipo = 'Desconto') {
            if ($scope.Simulacao.Fixar_Desconto) {
                //$scope.Simulacao.Id_Pacote = ""
                //$scope.Simulacao.Descricao_Pacote = ""
                $scope.Simulacao.Fixar_Valor = false;
                $scope.Simulacao.Valor_Informado = "";
            }
            else {
                $scope.Simulacao.Desconto_Padrao = "";
            }
        }
    };
    //===================================Salvar Simulacao
    $scope.SalvarSimulacao = function (pSimulacao, pShowMessage) {
        httpService.Post('SalvarSimulacao', pSimulacao).then(function (response) {
            if (response) {
                if (!response.data.Critica) {
                    $scope.Simulacao = response.data
                    if ($scope.Parameters.Action == 'New') {
                        $scope.Parameters.Action = "Edit";
                    }
                    if (pShowMessage) {
                        ShowAlert('Dados Gravados com Sucesso', 'success');
                    }
                    pSimulacao.PendenteCalculo = false;
                }
                else {
                    ShowAlert(response.data.Critica, 'warning');
                }
            }
        });
    };
    //===================================Recalcular Simulacao
    $scope.RecalcularSimulacao = function (pSimulacao) {
        $scope.SalvarSimulacao(pSimulacao, false)
        pSimulacao.PendenteCalculo = false;
    }
    //===================================Mudou campos que precisam recalcular simulacao
    $scope.ChangePendenteCalculo = function () {
        if ($scope.Simulacao.Esquemas) {
            if ($scope.Simulacao.Esquemas.length > 0) {
                $scope.Simulacao.PendenteCalculo = true;
                //$scope.RecalcularSimulacao($scope.Simulacao) //analisar se valor automaticamente ou somente ao clicar
            }
        }
    }
    //===================================Remover esquema
    $scope.RemoverEsquema = function (pIdEsquema, pTodos) {
        swal({
            title: "Tem certeza que deseja exclur " + (pTodos ? 'todos os Esquemas ?' : 'esse Esquema ?'),
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            if (pTodos) {
                $scope.Simulacao.Esquemas = [];
                $scope.currentEsquema = 0;
            }
            else {

                for (var i = 0; i < $scope.Simulacao.Esquemas.length; i++) {
                    if ($scope.Simulacao.Esquemas[i].Id_Esquema == pIdEsquema) {
                        $scope.Simulacao.Esquemas.splice(i, 1);
                        $scope.currentEsquema--;
                        if ($scope.currentEsquema < 0) {
                            $scope.currentEsquema = 0;
                        }
                        $scope.Simulacao.PendenteCalculo = true;
                        break;
                    }
                }
            }
            $scope.$digest();
            $scope.SalvarSimulacao($scope.Simulacao, false)
        });
    }
    //===================================Rebrir Proposta
    $scope.ReabrirProposta = function (pIdSimulacao) {
        swal({
            title: "Tem certeza que deseja reabrir a Proposta",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Reabrir",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Get("SimulacaoReabrir/" + pIdSimulacao).then(function (response) {
                if (response.data) {
                    ShowAlert("Proposta reaberta com Sucesso");
                    $scope.CarregarSimulacao(pIdSimulacao, $scope.Parameters.Processo)
                }
            });
        });
    };
    //===================================Mostra a critica da Valoracao
    $scope.MostraCritica = function (pLinha) {
        if (pLinha) {
            $scope.Info = {
                'Title': 'Crítica do Calculo',
                'Text': pLinha.Critica.split('#')
            }
            $("#modalInfo").modal(true);
        }

    }
    //===================================Detalhar Desconto
    $scope.DetalharDesconto = function (pMidia) {
        $scope.DescontoDetalhado = [];
        httpService.Get("DetalharDesconto/" + pMidia.Id_Midia).then(function (response) {
            if (response) {
                $scope.DescontoDetalhado = response.data;
                $("#modalDescontoDetalhe").modal(true);
            }
        });
    };
    //===================================Duplicar Esquemas
    $scope.DuplicarEsquema = function (pIdSimulacao, pId_Esquema, pTipo) {
        httpService.Get("DuplicarEsquema/" + pId_Esquema + '/' + pTipo).then(function (response) {
            if (response) {
                if (response.data[0].Qtd_Exportado > 0) {
                    $scope.CarregarSimulacao(pIdSimulacao, $scope.Parameters.Processo);
                }
                if (response.data[0].Critica) {
                    $scope.Info = {
                        'Title': 'Crítica da Duplicação de Esquemas',
                        'Text': response.data[0].Critica.split('#')
                    }
                    $("#modalInfo").modal(true);
                }
            }
        });
    }
    //===================================Processa a Importação da Simulacao para Gerar a Proposta
    $scope.ImportarSimulacao = function (pId_Simulacao) {
        var _data = {
            'Id_Simulacao': pId_Simulacao,
            'Validade_Inicio': ($scope.Simulacao.Validade_Inicio) ? $scope.Simulacao.Validade_Inicio : null,
            'Validade_Termino': ($scope.Simulacao.Validade_Termino) ? $scope.Simulacao.Validade_Termino : null,
        }


        httpService.Post('ImportarSimulacao', _data).then(function (response) {
            if (response) {
                $scope.CarregarSimulacao(response.data[0].Id_Simulacao, 'P',true)
            }
        });
    }
    //===================================Selecionar Simulacao Para Importacao e gerar nova proposta
    $scope.SelecionarImportacao = function () {
        httpService.Get('ListarTabela/Simulacao').then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção Modelos"
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) { $scope.ImportarSimulacao(value.Codigo, 'S', true) };
                $("#modalTabela").modal(true);
            }
        });
    }
    //===================================Mostrar Aprovadores
    $scope.MostrarAprovadores = function (pId_Simulacao) {
        httpService.Get("MostrarAprovadores/" + pId_Simulacao).then(function (response) {
            if (response.data) {
                var _text = [];
                for (var i = 0; i < response.data.length; i++) {
                    _text.push(response.data[i].Aprovador + '( Regra:' + response.data[i].Nome_Regra + ' - ' + response.data[i].Status + ' ) ');
                }
                $scope.Info = {
                    'Title': 'Aprovadores',
                    'Text': _text
                }
                $("#modalInfo").modal(true);
            }
        });
    }
    //===================================Enviar para aprovacao
    $scope.EnviarAprovacao = function (pId_Simulacao) {
        var _data = { 'Id_Simulacao': pId_Simulacao, 'url': $rootScope.pageUrl };
        httpService.Post('SolicitarAprovacao', _data).then(function (response) {
            if (response.data) {
                ShowAlert('Solicitação de aprovação enviada com sucesso.    ', 'success');
                $scope.CarregarSimulacao($scope.Parameters.Id, $scope.Parameters.Processo);
                //===================================Get Aprovadores chamar api da Genexos
                httpService.Get("MostrarAprovadores/" + pId_Simulacao).then(function (response) {
                    if (response.data) {
                        for (var i = 0; i < response.data.length; i++) {
                            var _urlMobile = $rootScope.mobileUrl + "anotificacaoenvia.aspx?" + response.data[i].Login_Aprovador + ',' + pId_Simulacao.toString() + ',0';
                            httpService.MobileGet(_urlMobile).then(function (response) {
                            });
                        };
                    };
                });
            }
        });
    }
    //===================================Aprovar Proposta
    $scope.AprovarProposta = function (pId_Simulacao) {
        var _data = { 'Id_Simulacao': pId_Simulacao };
        httpService.Post("AprovarProposta", _data).then(function (response) {
            if (response) {
                $scope.Aviso = response.data[0];
                $scope.ShowOk = false;
                ShowAlert(response.data[0].Mensagem, response.data[0].Status ? 'success' : 'warning');
                $scope.CarregarSimulacao($scope.Parameters.Id, $scope.Parameters.Processo);
            }
        });
    }

    //===================================Impressao da Midia
    $scope.ImprimirMidia = function (pId_Simulacao) {
        httpService.Get("ImprimirMidia/" + pId_Simulacao).then(function (response) {
            if (response.data) {
                url = $rootScope.baseUrl + "PDFFILES/MIDIA/" + $rootScope.UserData.Login.trim() + "/" + response.data;
                var win = window.open(url, '_blank');
                win.focus();
            }
            else {
                ShowAlert("Não existe mídia a ser impressa para essa " + $scope.Descricao_Processo, "warning")
            }
        });
    };
    //===================================Analisar Simulacao
    $scope.ImprimirSimulacao = function (pId_Simulacao) {
        httpService.Get("ImprimirAnalise/" + pId_Simulacao).then(function (response) {
            if (response.data) {
                url = $rootScope.baseUrl + "PDFFILES/ANALISE/" + $rootScope.UserData.Login.trim() + "/" + response.data;
                var win = window.open(url, '_blank');
                win.focus();
            }
            else {
                ShowAlert("Não existe dados para Análise", "warning")
            }
        });
    };
    //===================================PDF da Proposta
    $scope.GerarProposta = function (pProposta) {
        pProposta.Alerta = '';
        pProposta.Email_Contato = pProposta.Email_Contato.replace(';', ',');
        pProposta.Email_Copia = pProposta.Email_Copia.replace(',', ',');
        var _arrayEmail = pProposta.Email_Contato.split(',');
        for (var i = 0; i < _arrayEmail.length; i++) {
            if (!ValidaEmail(_arrayEmail[i])) {
                pProposta.Alerta = _arrayEmail[i] + ' não é um email válido'
                return;
            }
        }
        _arrayEmail = pProposta.Email_Copia.split(',');
        for (var i = 0; i < _arrayEmail.length; i++) {
            if (!ValidaEmail(_arrayEmail[i])) {
                pProposta.Alerta = _arrayEmail[i] + ' não é um email válido'
                return;
            }
        }
        httpService.Post("GerarProposta/", pProposta).then(function (response) {
            if (response.data) {
                $("#ModalGeracaoProposta").modal('hide');
                if ($scope.GeracaoProposta.Visualizar) {
                    url = $rootScope.baseUrl + "PDFFILES/Proposta/" + $rootScope.UserData.Login.trim() + "/" + response.data;
                    var win = window.open(url, '_blank');
                    win.focus();
                }
                else {
                    ShowAlert("Proposta Gerada e enviada com Sucesso!", "warning")
                    $scope.CarregarSimulacao($scope.Parameters.Id, $scope.Parameters.Processo);
                }
            }
            else {
                ShowAlert("Não há dados para geração da proposta", "warning")
            }
        });
    };
    //=========================Marcar/Desmarcar todos os veiculos
    $scope.SelectAllVeiculo = function (pLista, pCheck) {
        for (var i = 0; i < pLista.length; i++) {
            pLista[i].Selected = pCheck;
        }
    }
    //===================================Preencher dados para geracao da proposta
    $scope.PreencherProposta = function (pId_Simulacao) {
        $scope.GeracaoProposta = { 'Id_Simulacao': pId_Simulacao, 'Nome_Contato': '', 'Email_Contato': '', 'Email_Copia': '', 'Observacao': '', 'Alerta': '', 'Visualizar': false };
        $("#ModalGeracaoProposta").modal(true);
    };

    $scope.MostrarInconsistencias = function (pId_Simulacao) {
        httpService.Get('MostrarInconsistencias/' + pId_Simulacao).then(function (response) {
            if (response.data) {
                $scope.Info = {
                    'Title': $scope.Descricao_Processo + ' - Incosistências',
                    'Text': response.data
                };
                $("#modalInfo").modal(true);
            }
        });
    }
    $scope.ConfirmarVenda = function (pId_Simulacao) {
        var _data = { 'Id_Simulacao': pId_Simulacao };
        httpService.Post('ConfirmarVenda', _data).then(function (response) {
            if (response.data) {
                $scope.CarregarSimulacao(pId_Simulacao, $scope.Parameters.Processo);
            }
        });
    }
    //===================================Consiste a Competencia do Esquema
    $scope.ConsisteCompetencia = function (pCompetencia) {
        var _referenciaInicio = $scope.Simulacao.Validade_Inicio.substr(6, 5) + $scope.Simulacao.Validade_Inicio.substr(3, 2);
        var _referenciaFim = $scope.Simulacao.Validade_Termino.substr(6, 5) + $scope.Simulacao.Validade_Termino.substr(3, 2);
        var _mesano = pCompetencia.substr(3, 4) + pCompetencia.substr(0, 2)
        if (_mesano < _referenciaInicio || _mesano > _referenciaFim) {
            ShowAlert('Competencia ' + pCompetencia + ' fora da validade do Modelo', 'warning')
            $scope.Simulacao.Esquemas[$scope.currentEsquema].Competencia = "";
        }
    };
    //===================================Seta Iniciar calculo false apos o load da pagina
    //$timeout(function () {
    //    $scope.IniciarCalculo = true;
    //},30000);
    $scope.Indica_Sem_Midia_Change = function (pValue) {
        if (!$scope.Simulacao.Indica_Sem_Midia && $scope.Simulacao.Esquemas.length > 0 && pValue) {
            swal({
                title: "Ao Mudar para Sem Midia Todos os Esquemas Comerciais serão Apagados. Confirma a Alteração ?",
                //text: "Ao mudar para Sem Midia todos os Esquemas Comerciais serão apadagos",
                //type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Sim",
                cancelButtonText: "Não, Cancelar",
                closeOnConfirm: true
            }, function (pReturn) {
                if (pReturn) {
                    $scope.Simulacao.Indica_Sem_Midia = true;
                    $scope.ChangePendenteCalculo()
                    $scope.$digest();
                }
                else {
                    $scope.Indica_Sem_Midia = false;
                    $scope.$digest();
                };
            });
        }
        else {
            $scope.Simulacao.Indica_Sem_Midia = pValue
        }
    };
    $scope.SetTotalizadorIndex = function (pIndex) {
        $scope.TotalizadorIndex = pIndex;
    };
    //===========================Selecionar Pacote de Descontos
    $scope.SelecionarPacote = function (pValidadeInicio, pValidadeTermino) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        var _url = "SelecionarPacotes";
        _url += "?Id_pacote=0"
        _url += "&Validade_Inicio=" + pValidadeInicio;
        _url += "&Validade_Termino=" + pValidadeTermino;
        _url += "&";
        httpService.Get(_url).then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data
                $scope.PesquisaTabelas.FiltroTexto = ""
                $scope.PesquisaTabelas.Titulo = "Selecionar Pacote"
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) {
                    $scope.Simulacao.Id_Pacote = value.Codigo;
                    $scope.Simulacao.Descricao_Pacote = value.Descricao;
                };
                $("#modalTabela").modal(true);
            }
        });
    };
    $scope.ValidarPacote = function (pIdPacote) {
        if (!pIdPacote) {
            return;
        }
        var _url = "SelecionarPacotes";
        _url += "?Id_Pacote=" + pIdPacote.toString().trim();
        _url += "&Validade_Inicio=" + $scope.Simulacao.Validade_Inicio;
        _url += "&Validade_Termino=" + $scope.Simulacao.Validade_Termino;
        _url += "&";
        httpService.Get(_url).then(function (response) {

            if (response.data.length == 0) {
                ShowAlert("Pacote Inválido ou fora da Validade");
                $scope.Simulacao.Id_Pacote = "";
                $scope.Simulacao.Descricao_Pacote = "";
            }
            else {
                $scope.Simulacao.Descricao_Pacote = response.data[0].Descricao;
            }
        });
    };
    //===========================Mostrar Historico do Status 
    $scope.ShowHistorico = function (pId_Simulacao) {
        $scope.HistoricoStatus = [];
        httpService.Get("ListHistorico/" + pId_Simulacao).then(function (response) {
            if (response.data) {
                $scope.HistoricoStatus = response.data;
            }
        });
        $("#modalHistorico").modal(true);
    }
}]);
;