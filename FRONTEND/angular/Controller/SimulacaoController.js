angular.module('App').controller('SimulacaoController', ['$scope', '$rootScope', '$filter', '$routeParams', 'httpService', '$timeout', function ($scope, $rootScope, $filter, $routeParams, httpService, $timeout) {

    //============ Inicializa Variaveis Scopes
    $scope.Parameters = $routeParams;
    $scope.currentShow = 'Base';
    $scope.Abrangencias = [{ 'Id': 3, 'Descricao': '' }, { 'Id': 0, 'Descricao': 'Net' }, { 'Id': 1, 'Descricao': 'Rede' }, { 'Id': 2, 'Descricao': 'Local' }];
    $scope.Forma_Pgto = [{ 'Id': 1, 'Descricao': 'Espécie' }, { 'Id': 2, 'Descricao': 'Permuta' }];
    $scope.Tipo_Vencimento = [{ 'Id': 1, 'Descricao': 'À Vista' }, { 'Id': 2, 'Descricao': 'DFM' }, { 'Id': 3, 'Descricao': 'DDL' }];
    $scope.Simulacao = {};
    $scope.currentEsquema = 0;
    $scope.PesquisaTabelas = { "Items": [], 'FiltroTexto': '', cback: '' };
    $scope.IniciarCalculo = false;
    $scope.DescontoDetalhado = "[]";
    $scope.Distribuicao = [{ 'Tipo': 'D', 'Descricao': 'Por Dia' }, { 'Tipo': 'M', 'Descricao': 'No Periodo' }]
    //=====================Check processo simulacao ou proposta
    if ($scope.Parameters.Processo=='P') {
        $scope.Descricao_Processo = 'Proposta'
        $rootScope.routeName = 'Proposta - ' + $scope.Parameters.Action
    }
    else {
        $scope.Descricao_Processo = 'Simulação'
        $rootScope.routeName = 'Simulação - ' + $scope.Parameters.Action
    }
    
    //=====================Carrega a Simulacao 
    $scope.CarregarSimulacao = function (pId_Simulacao,pProcesso,pImportacao) {
        httpService.Get("GetSimulacao/" + pId_Simulacao + "/" + pProcesso).then(function (response) {
            if (response.data) {
                $scope.Simulacao = response.data;
                $timeout(function () {
                    $scope.IniciarCalculo = true;
                }, 1000);
                if (pImportacao) {
                    $scope.Simulacao.Id_Simulacao = 0;
                    $scope.Simulacao.Tipo = 'P';
                    
                }
            }
        });
    };
    $scope.CarregarSimulacao($scope.Parameters.Id, $scope.Parameters.Processo,false);
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
            $scope.Simulacao.Esquemas.push(angular.copy(_tempEsquema));
        });
    }
    //===================================Adicionar Midia
    $scope.AdicionarMidia = function (pEsquema) {
        $scope.Simulacao.ContadorMidia++;
        var _mmyy = CompetenciaToInt(pEsquema.Competencia);
        httpService.Get("GetNewMidia/" + _mmyy).then(function (response) {
            _tempMidia = response.data;
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
            _url += '&Mercado=' + NullToString($scope.Simulacao.Esquemas[$scope.currentEsquema].Cod_Mercado);
            _url += '&Empresa=' + NullToString($scope.Simulacao.Cod_Empresa_Venda);
            _url += '&Empresa_Faturamento=' + NullToString($scope.Simulacao.Esquemas[$scope.currentEsquema].Cod_Empresa_Faturamento);

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
            _url += '&Mercado=' + pCodMercado
            _url += '&Empresa=' + NullToString($scope.Simulacao.Cod_Empresa_Venda);
            _url += '&Empresa_Faturamento=' + NullToString($scope.Simulacao.Esquemas[$scope.currentEsquema].Cod_Empresa_Faturamento);
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
    }
    //==============================Mudou algum dado da midia
    $scope.fnChangeMidia = function (pMidia, pField) {
        switch (pField) {
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
    //=====================Clicou em selecionar veiculos
    $scope.SelecionarVeiculos = function () {
        var _url = 'GetVeiculos'
        _url += '?Abrangencia=' + $scope.Simulacao.Esquemas[$scope.currentEsquema].Abrangencia;
        _url += '&Mercado=' + NullToString($scope.Simulacao.Esquemas[$scope.currentEsquema].Cod_Mercado);
        _url += '&Empresa=' + NullToString($scope.Simulacao.Cod_Empresa_Venda);
        _url += '&Empresa_Faturamento=' + NullToString($scope.Simulacao.Esquemas[$scope.currentEsquema].Cod_Empresa_Faturamento);
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
                            'Cod_Veiculo': $scope.ListadeVeiculos[x].Cod_Veiculo, 'Nome_Veiculo': $scope.ListadeVeiculos[x].Descricao
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

        httpService.Post('GetProgramasGrade', _param).then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data
                $scope.PesquisaTabelas.FiltroTexto = ""
                $scope.PesquisaTabelas.cback = function (value) { pMidia.Cod_Programa = value; $scope.fnChangeMidia(pMidia, 'Programa') }
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
                $scope.PesquisaTabelas.cback = function (value) { pMidia.Cod_Caracteristica = value; }
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
                $scope.PesquisaTabelas.cback = function (value) { pMidia.Cod_Tipo_Comercial = value; }
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
            'Veiculos': pEsquema.Veiculos
        }
        httpService.Post("DistribuirInsercoes", _data).then(function (response) {
            if (response) {
                if (response.data[0].Status) {
                    pMidia.Insercoes = response.data;
                    //pMidia.IsValid = true;
                    //$scope.ChangePendenteCalculo();
                    $scope.SalvarSimulacao($scope.Simulacao, false);
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
    //===================================Clicou em Fixar Desconto ou Valor
    $scope.Fixar = function (pTipo) {
        if (pTipo == 'Valor') {
            $scope.Simulacao.Valor_Informado = "";
            //$scope.Simulacao.Id_Pacote = ""
            //$scope.Simulacao.Descricao_Pacote = ""
            $scope.Simulacao.Desconto_Padrao = ""
        }
        if (pTipo = 'Desconto') {
            if ($scope.Simulacao.Fixar_Desconto) {
                $scope.Simulacao.Id_Pacote = ""
                $scope.Simulacao.Descricao_Pacote = ""
            }
            //$scope.Simulacao.Desconto_Padrao = "";
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
                    pSimulacao.PendenteCalculo = false;
                }
                else {
                    ShowAlert(response.data.Critica,  'warning');
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
            $scope.SalvarSimulacao($scope.Simulacao)
        });
    }
    //===================================Mostra a critica da Valoracao
    $scope.MostraCritica = function (pLinha) {
        if (pLinha) {
            $scope.Critica_Tabela = pLinha.Critica.split('#');
            $("#modalCritica").modal(true);
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
    $scope.DuplicarEsquema = function (pIdSimulacao,pId_Esquema, pTipo) {
        httpService.Get("DuplicarEsquema/" + pId_Esquema + '/' + pTipo).then(function (response) {
            if (response) {
                if (response.data[0].Qtd_Exportado > 0) {
                    $scope.CarregarSimulacao(pIdSimulacao,$scope.Parameters.Processo,false);
                }
                if (response.data[0].Critica) {
                    $scope.Critica_Simulacao = response.data[0].Critica.split('#');
                    $("#modalCritica").modal(true);
                }
            }
        });
    }
    //===================================Importar Simulacao Para gerar Nova Proposta
    $scope.ImportarSimulacao = function () {
        httpService.Get('ListarTabela/Simulacao').then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.cback = function (value) { $scope.CarregarSimulacao(value,'S',true) };
                $("#modalTabela").modal(true);
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
                ShowAlert("Não existe mídia a sem impressa para essa " + $scope.Descricao_Processo, "warning")
            }
        });
    };
    //===================================Analisar Simulacao
    $scope.ImprimirSimulacao= function (pId_Simulacao) {
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
    //===================================Seta Iniciar calculo false apos o load da pagina
    //$timeout(function () {
    //    console.log("mudando para ")
    //    $scope.IniciarCalculo = true;
    //},30000);


}]);
