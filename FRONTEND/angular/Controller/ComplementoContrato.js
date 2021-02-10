angular.module('App').controller('ComplementoContratoController', ['$scope', '$rootScope', '$cookies', 'httpService', '$location', '$routeParams', function ($scope, $rootScope, $cookies, httpService, $location, $routeParams) {

    //========================Verifica Permissoes
    $scope.Parameters = $routeParams;
    $rootScope.routeName = 'Complemento de Contratos - ' + ($scope.Parameters.Origem==1?'Mídias':'Antecipado');
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.filtro = "";
    //====================Inicializa scopes

    $scope.NewFiltro = function () {
        $scope.Filtro = {
            'Numero_Negociacao': '',
            'Empresa': '',
            'Contrato': '',
            'Sequencia': '',
            'Agencia': '',
            'Cliente': '',
            'Nucleo': '',
            'Contato': '',
            'Competencia': '',
            'Complemento': '',
            'Ind_Comprovado': '',
            'Retorno': '',
            'Emp_Faturamento': '',
            'Origem' : $scope.Parameters.Origem
        };
        localStorage.removeItem('Complemento_Filter_' + $scope.Parameters.Origem );
    }
    $scope.ContratoDados = "";
    $scope.Natureza_Servico = [];
    $scope.Nucleos = [];
    $scope.Contatos = [];
    $scope.Agencias = [];
    $scope.Clientes = [];
    $scope.Intermediarios = [];
    $scope.CondicoesPgto = []
    $scope.currentTab = 0;
    $scope.SequenciadorParcela = 0;
    $scope.SequenciadorRateio = 1;
    $scope.Sequenciador_Id_Rateio = 0;
    //======================Verifica se tem filtro anterior
    var _Filter = JSON.parse(localStorage.getItem('Complemento_Filter_' + $scope.Parameters.Origem));
    if (!_Filter) {
        $scope.NewFiltro()
        $scope.Filtro.Emp_Faturamento = $scope.FnSetEmpresaDefault('Codigo');
    }
    else {
        $scope.Filtro = _Filter;
    };

    $scope.ShowFilter = true;
    $scope.ShowGrid = false;
    $scope.ShowDados = false;
    //========================Parametros do Grid

    $scope.gridheaders = [{ 'title': 'Selecionar', 'visible': true, 'searchable': false, 'sortable': false },
        { 'title': 'Negociação', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Parcela', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Agência', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Cliente', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Contrato', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Aut.', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Valor', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Competência', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Produto', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Emp. Fat.', 'visible': true, 'searchable': true, 'sortable': true },
    ];

    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        if ($scope.Contratos.length > 0) {
            $scope.ShowGrid = true;
            $scope.ShowFilter = false;
            $scope.ShowDados = false;
        }
        else {
            $scope.ShowGrid = false;
            $scope.ShowDados = false;
            $scope.ShowFilter = true
        }

        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        }, 10)
    };
    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });

    //====================Carrega Contrato            
    $scope.CarregaContratosComplemento = function (pFiltro, pAviso) {
        if (!pFiltro.Emp_Faturamento ) {
            ShowAlert("Empresa de Faturamento é de seleção obrigatória!");
            return
        }

        $rootScope.routeloading = true;
        $scope.Contratos = [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();
        $scope.ShowFilter = false;
        $scope.Filtro.Origem = $scope.Parameters.Origem;

        httpService.Post('ContratosComplementoListar',pFiltro).then(function (response) {
            if (response) {
                $scope.Contratos = response.data;
                if ($scope.Contratos.length == 0) {
                    if (pAviso) {
                        ShowAlert("Não existe dados cadastrado p/ este Filtro");
                    };
                    $scope.RepeatFinished();
                }
            }
            localStorage.setItem('Complemento_Filter_'+ $scope.Parameters.Origem , JSON.stringify($scope.Filtro));
        });
    };

    //====================Funcao para configurar o Grid
    $scope.ConfiguraGrid = function () {
        param = {};
        param.language = fnDataTableLanguage();
        param.lengthMenu = [[7, 10, 25, 50, -1], [7, 10, 25, 50, "Todos"]];
        param.pageLength = 7;
        param.scrollCollapse = true;
        param.paging = true;

        param.dom = "<'row'<'col-sm-3'l><'col-sm-4'f><'col-sm-5'B>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
            param.buttons = [
                {
                    text: 'Exportar<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning HideButton', extend: 'excel', exportOptions: {
                        columns: ':visible:not(:first-child)'
                    }
                }
            ];
        param.order = [[1, 'asc']];
        param.autoWidth = false;
        param.columns = [];
        for (var i = 0; i < $scope.gridheaders.length; i++) {
            param.columns.push({ "visible": $scope.gridheaders[i].visible, "searchable": $scope.gridheaders[i].searchable, "sortable": $scope.gridheaders[i].sortable });
        }
        $('#dataTable').DataTable(param);
    };


    //===========================Clicou no botao complementar  
    $scope.Complementar = function (pContrato) {
        $scope.ContratoDados = "";
        $scope.Natureza_Servico = [];
        $scope.Nucleos = [];
        $scope.Contatos = [];
        $scope.Agencias = [];
        $scope.Clientes = [];
        $scope.Intermediarios = [];
        $scope.CondicoesPgto = []
        $scope.currentTab = 0;
        $scope.SequenciadorParcela = 0;
        $scope.SequenciadorRateio = 1;
        $scope.Sequenciador_Id_Rateio = 0;
        //----- Verifica se tem alguma linha marcada/ move as linhas marcadas para _tempArray
        var _tempArray = [];
        for (var i = 0; i < pContrato.length; i++) {
            if (pContrato[i].Selected) {
                _tempArray.push(pContrato[i]);
            }
        };
        if (_tempArray.length == 0) {
            ShowAlert("Nenhum contrato foi selecionado para complemento.");
            return;
        };
        if (_tempArray.length>1 && $scope.Parameters.Origem==0) {
            ShowAlert("Antecipados devem ser complementada uma Fatura por vez.");
            return;
        }
        //---------Consiste se contrato tem itens diferentes que nao podem agrupar
        for (var i = 0; i < _tempArray.length; i++) {
            for (var x = i + 1; x < _tempArray.length; x++) {
                if (_tempArray[i].Numero_Negociacao != _tempArray[x].Numero_Negociacao) {
                    ShowAlert('Contratos agrupados não pertencem a mesma Negociação');
                    return;
                };
                if (_tempArray[i].Cod_Agencia != _tempArray[x].Cod_Agencia) {
                    ShowAlert('Contratos agrupados não pertencem a mesma Agência');
                    return;
                };
                if (_tempArray[i].Cod_Cliente != _tempArray[x].Cod_Cliente) {
                    ShowAlert('Contratos agrupados não pertencem a mesma Cliente');
                    return;
                };
            };
        };
        $scope.ShowDados = true;
        $scope.ShowGrid = false;
        _tempArray.Origem = $scope.Parameters.Origem;
        httpService.Post("GetComplementoData", _tempArray).then(function (response) {
            if (response.data) {
                $scope.ContratoDados = response.data;
                //----------------------------Carrega o combo de Natureza de Servicos
                var _url = "GetNaturezaRegra"
                _url += "?Cod_Empresa_Faturamento=" + $scope.ContratoDados.Cod_Empresa_Faturamento;
                _url += "&Cod_Empresa=" + $scope.ContratoDados.Cod_Empresa;
                _url += "&Numero_Mr=" + $scope.ContratoDados.Numero_Mr;
                _url += "&Sequencia_Mr=" + $scope.ContratoDados.Sequencia_Mr;
                _url += "&Tipo=" + '0'
                _url += "&";
                httpService.Get(_url).then(function (response) {
                    if (response) {
                        $scope.Natureza_Servico = response.data
                        $scope.ContratoDados.Natureza_Servico = response.data[0].Cod_Natureza;
                        $scope.ContratoDados.Percentual_Iss = response.data[0].Percentual_Iss;
                        $scope.ContratoDados.Cod_Historico = response.data[0].Cod_Historico;
                    };
                });
                //----------------------------Carrega o Nucleo da Negociacao
                _url = 'MapaReserva/GetTerceirosNegociacao'
                _url += '?Numero_Negociacao=' + $scope.ContratoDados.Numero_Negociacao;
                _url += '&Tabela=' + 'Nucleo';
                _url += '&Codigo=';
                _url += '&';
                httpService.Get(_url).then(function (response) {
                    if (response) {
                        $scope.Nucleos = response.data;
                    };
                });
                //----------------------------Carrega o Contato da Negociacao
                _url = 'MapaReserva/GetTerceirosNegociacao'
                _url += '?Numero_Negociacao=' + $scope.ContratoDados.Numero_Negociacao;
                _url += '&Tabela=' + 'Contato';
                _url += '&Codigo=';
                _url += '&';
                httpService.Get(_url).then(function (response) {
                    if (response) {
                        $scope.Contatos = response.data;
                    };
                });
                //----------------------------Carrega os Clientes da Negociacao
                _url = 'MapaReserva/GetTerceirosNegociacao'
                _url += '?Numero_Negociacao=' + $scope.ContratoDados.Numero_Negociacao;
                _url += '&Tabela=' + 'Cliente';
                _url += '&Codigo=';
                _url += '&';
                httpService.Get(_url).then(function (response) {
                    if (response) {
                        $scope.Clientes = response.data;
                    }
                });
                //----------------------------Carrega as  Agencias da Negociacao
                _url = 'MapaReserva/GetTerceirosNegociacao'
                _url += '?Numero_Negociacao=' + $scope.ContratoDados.Numero_Negociacao;
                _url += '&Tabela=' + 'Agencia';
                _url += '&Codigo=';
                _url += '&';
                httpService.Get(_url).then(function (response) {
                    if (response) {
                        $scope.Agencias = response.data;
                    }
                });
                //----------------------------Carrega os Intermediarios
                _url = 'MapaReserva/GetTerceirosNegociacao'
                _url += '?Numero_Negociacao=' + $scope.ContratoDados.Numero_Negociacao;
                _url += '&Tabela=' + 'Intermediario';
                _url += '&Codigo=';
                _url += '&';
                httpService.Get(_url).then(function (response) {
                    if (response) {
                        if (response.data.length > 0) {
                            $scope.Intermediarios = response.data;
                            $scope.ContratoDados.Cod_Intermediario = response.data[0].Codigo;
                        };
                    };
                });
                //----------------------------Carrega combo Condicoes de Pagamento
                _url = 'ListarTabela/Condica_Pagamento'
                httpService.Get(_url).then(function (response) {
                    if (response.data) {
                        $scope.CondicoesPgto = response.data;
                    }
                });
            };
        });
    };
    

    ///==============Quando mudar a natureza de servico preenche iss
    $scope.$watch('ContratoDados.Natureza_Servico', function (newValue, oldValue) {
        if (newValue != oldValue) {
            for (var i = 0; i < $scope.Natureza_Servico.length; i++) {
                if ($scope.Natureza_Servico[i].Cod_Natureza == newValue) {
                    $scope.ContratoDados.PercISS = $scope.Natureza_Servico[i].Percentual_Iss;
                    $scope.ContratoDados.Cod_Historico = $scope.Natureza_Servico[i].Cod_Historico;
                }
            }

        }
    });
    //===========================Seta o currentRateio quando clicar na aba dos rateios
    $scope.SetCurrentRateio = function (pId_Rateio) {
        for (var i = 0; i < $scope.ContratoDados.Rateios.length; i++) {
            if ($scope.ContratoDados.Rateios[i].Id_Rateio == pId_Rateio) {
                $scope.currentTab = i;
                break;
            }
        }
    };
    //===============================Adicionar Rateios
    $scope.AdicionarRateio = function () {
        $scope.Sequenciador_Id_Rateio++;
        $scope.SequenciadorRateio++;
        var _tempRateio = angular.copy($scope.ContratoDados.Rateios[$scope.ContratoDados.Rateios.length - 1]);

        _tempRateio.Id_Rateio = $scope.Sequenciador_Id_Rateio;
        _tempRateio.Numero_Rateio = $scope.SequenciadorRateio;
        _tempRateio.Perc_Rateio = "";
        _tempRateio.Vlr_A_Faturar = "";
        _tempRateio.Duplicatas = [];
        $scope.SequenciadorParcela++;
        _tempRateio.Duplicatas.push(
            {
                'Id_Rateio': _tempRateio.Id_Rateio,
                'Parcela': 1,
                'Id_Parcela': $scope.SequenciadorParcela,
                'Vencimento': '',
                'Valor': 0
            }
        );
        $scope.ContratoDados.Rateios.push(_tempRateio);
    };

    //===========================Remover Rateio
    $scope.RemoveRateio = function (pid_Rateio) {
        if ($scope.ContratoDados.Rateios.length == 1) {
            ShowAlert("Não é permitido excluir o ultimo rateio.")
        }
        swal({
            title: "Confirma a exclusão do Rateio " + (pid_Rateio + 1),
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir",
            cancelButtonText: "Não,Cancelar",
            closeOnConfirm: true
        }, function () {
            var _index = -1;
            for (var i = 0; i < $scope.ContratoDados.Rateios.length; i++) {
                if ($scope.ContratoDados.Rateios[i].Id_Rateio==pid_Rateio) {
                    _index = i;
                    break
                }
            };
            $scope.ContratoDados.Rateios.splice(_index,1);
            for (var i = 0; i < $scope.ContratoDados.Rateios.length; i++) {
                $scope.ContratoDados.Rateios[i].Numero_Rateio = i+1;
            };
            $scope.currentTab = 0;
            $scope.currentTab = 0;
            $scope.CalculaSaldo();
            $scope.$digest();

        });
    };
    //===============================Adicionar Parceloa
    $scope.AdicionarParcela = function (pDuplicatas) {
        if (!$scope.ContratoDados.Rateios[$scope.currentTab].Data_Emissao) {
            ShowAlert('Informa a Data de Emissão da Fatura.')
            return;
        }
        if (pDuplicatas.length > 0) {
            $scope.SequenciadorParcela++;
            var _tempParcela = angular.copy(pDuplicatas[pDuplicatas.length - 1]);
            _tempParcela.Parcela = pDuplicatas.length + 1;
            _tempParcela.Id_Parcela = $scope.SequenciadorParcela;
            _tempParcela.Dia_Semana = StringToDate(_tempParcela.Vencimento).getDay();
            pDuplicatas.push(_tempParcela);
            $scope.CalculaParcelas();
        };
    };
    //===============================Remover parcela
    $scope.RemoverParcela = function (pDuplicatas, pId) {
        if (pDuplicatas.length > 1) {
            var _index = -1;
            for (var i = 0; i < pDuplicatas.length; i++) {
                if (pDuplicatas[i].Id_Parcela == pId) {
                    _index = i;
                    break;
                };
            };
            if (_index > -1) {
                swal({
                    title: "Confirma a exclusão da Parcela 3" + (pDuplicatas[_index].Parcela),
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Sim, Excluir",
                    cancelButtonText: "Não,Cancelar",
                    closeOnConfirm: true
                }, function () {
                    pDuplicatas.splice(_index, 1);
                    for (var i = 0; i < pDuplicatas.length; i++) {
                        pDuplicatas[i].Parcela = i + 1;
                    }
                    $scope.CalculaParcelas();
                    $scope.$apply();
                });

            };
        };
    };
    //===========================Recalcula as parcelas();
    $scope.CalculaParcelas = function () {
        if (!$scope.ContratoDados.Rateios[$scope.currentTab].Data_Emissao) {
            return;
        }
        var _Tipo_Vencimento = "";
        var _Qtd_Dias = 0;
        var _condicaoPgto = ""
        var _diaBase = parseInt($scope.ContratoDados.Rateios[$scope.currentTab].Data_Emissao.substr(0, 2));
        var _mesBase = parseInt($scope.ContratoDados.Rateios[$scope.currentTab].Data_Emissao.substr(3, 2));
        var _anoBase = parseInt($scope.ContratoDados.Rateios[$scope.currentTab].Data_Emissao.substr(6, 4));
        var _database = undefined;
        var _VencimentoPrimeira = new Date();
        var _Valor_Total;
        var _Valor_Parcela;
        var _Soma = 0;
        var _qtd_parcelas = $scope.ContratoDados.Rateios[$scope.currentTab].Duplicatas.length;
        var _temp = undefined;
        //---------------------Acha a condicao de pagamento do Rateio
        for (var i = 0; i < $scope.CondicoesPgto.length; i++) {
            if ($scope.CondicoesPgto[i].Codigo.trim() == $scope.ContratoDados.Rateios[$scope.currentTab].Cod_Condicao.trim()) {
                _Tipo_Vencimento = $scope.CondicoesPgto[i].Tipo_Vencimento.trim().toUpperCase();
                _Qtd_Dias = $scope.CondicoesPgto[i].Qtd_Dias;
            };
        };
        if (!_Tipo_Vencimento) {
            return;
        };
        //---------------------Acha a data base para calculo dos vencimentos 
        //if (_Tipo_Vencimento == 'DDL') {
        //    _database = new Date(_anoBase, _mesBase - 1, _diaBase, 0, 0, 0, 0); //Data da emissao com horarios zerados 
        //};
        if (_Tipo_Vencimento == 'DFM') {
            _database = new Date(_anoBase, _mesBase, 0, 0, 0, 0, 0); //ultimo dia do mes 
        }
        else {
            _database = new Date(_anoBase, _mesBase - 1, _diaBase, 0, 0, 0, 0); //Data da emissao com horarios zerados 
        }

        //---------------------Calcula Vencimento de primeira parcela e demais parcelas
        _VencimentoPrimeira = _database;
        _VencimentoPrimeira.setDate(_database.getDate() + _Qtd_Dias);
        //---------------------Calcula Vencimento de cada parcela / demais datas soma 1 mes 
        for (var i = 0; i < $scope.ContratoDados.Rateios[$scope.currentTab].Duplicatas.length; i++) {
            _temp = new Date(_VencimentoPrimeira.valueOf())
            _temp = addMonths(_temp, i);
            $scope.ContratoDados.Rateios[$scope.currentTab].Duplicatas[i].Vencimento = DateToString(_temp);
            $scope.ContratoDados.Rateios[$scope.currentTab].Duplicatas[i].Dia_Semana= _temp.getDay();
        };
        //-------------------Calcula valores das parcelas 
        _Valor_Total = DoubleVal($scope.ContratoDados.Rateios[$scope.currentTab].Vlr_A_Faturar);
        for (var i = 0; i < $scope.ContratoDados.Rateios[$scope.currentTab].Duplicatas.length - 1; i++) {
            _Valor_Parcela = parseFloat((_Valor_Total / _qtd_parcelas).toFixed(2));
            $scope.ContratoDados.Rateios[$scope.currentTab].Duplicatas[i].Valor = MoneyFormat(_Valor_Parcela);
            _Soma += _Valor_Parcela;
        }
        //----------------Ultima parcela fica com a diferenca de arredondamento
        $scope.ContratoDados.Rateios[$scope.currentTab].Duplicatas[$scope.ContratoDados.Rateios[$scope.currentTab].Duplicatas.length - 1].Valor = MoneyFormat(_Valor_Total - _Soma);
    };

    //===========================Quando mudor o valor a faturar do rateio
    $scope.ValorFaturarChange = function () {
        var _Vlr_Rateio = DoubleVal($scope.ContratoDados.Rateios[$scope.currentTab].Vlr_A_Faturar);;
        var _Total_Fatura = $scope.ContratoDados.Vlr_A_Faturar;
        $scope.ContratoDados.Saldo_A_Faturar = _Total_Fatura - _Vlr_Rateio;
        $scope.ContratoDados.Rateios[$scope.currentTab].Perc_Rateio = PercentFormat((_Vlr_Rateio / _Total_Fatura) * 100);
        $scope.CalculaParcelas();
        $scope.CalculaSaldo();
    };
    //===========================Quando mudor o valor o Pct do rateio
    $scope.Perc_RateioChange = function () {
        var _Total_Fatura = $scope.ContratoDados.Vlr_A_Faturar;
        var _perc = DoubleVal($scope.ContratoDados.Rateios[$scope.currentTab].Perc_Rateio);
        var _Valor_Rateio = _Total_Fatura * (_perc / 100);
        $scope.ContratoDados.Rateios[$scope.currentTab].Vlr_A_Faturar = MoneyFormat(_Valor_Rateio);
        $scope.CalculaParcelas();
        $scope.CalculaSaldo();
    };
    //===========================Calcula o Saldo a Faturar
    $scope.CalculaSaldo = function () {
        var _Total_Rateios = 0;
        var _Total_Fatura = $scope.ContratoDados.Vlr_A_Faturar;
        for (var i = 0; i < $scope.ContratoDados.Rateios.length; i++) {
            _Total_Rateios += DoubleVal($scope.ContratoDados.Rateios[i].Vlr_A_Faturar);
        };
        $scope.ContratoDados.Saldo_A_Faturar = _Total_Fatura - _Total_Rateios;
        $scope.CalculaParcelas();
    };
    //===========================Mudou a data de emissao
    $scope.$watch('ContratoDados.Rateios[currentTab].Data_Emissao', function (newValue, oldValue) {
        if (newValue != oldValue) {
            $scope.CalculaParcelas();
        }

    });
    //===========================Gravar o Complemento de Midia
    $scope.SalvarComplemento = function (pComplemento) {
        httpService.Post('SalvarComplemento', pComplemento).then(function (response) {
            if (response) {
                ShowAlert(response.data[0].Mensagem);
            }
            if (response.data[0].Status == 1) {
                $scope.CarregaContratosComplemento($scope.Filtro,false);
            };
        });
    };
    //===========================fim do load da pagina
    //$scope.$watch('$viewContentLoaded', function () {
    //    $scope.ConfiguraGrid();
    //});

}]);

