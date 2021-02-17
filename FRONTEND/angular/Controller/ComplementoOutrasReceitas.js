angular.module('App').controller('ComplementoOutrasReceitas', ['$scope', '$rootScope', '$cookies', 'httpService', '$location', '$routeParams', function ($scope, $rootScope, $cookies, httpService, $location, $routeParams) {

    //====================Inicializa scopes
    $scope.Parameters = $routeParams;
    $rootScope.routeName = 'Complemento de Contratos - ' + ($scope.Parameters.Origem == 1 ? 'Mídias' : 'Antecipado');

    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.TipoIntermediario = [{ 'Codigo': 'C', 'Nome': 'Corretor' }, { 'Codigo': 'E', 'Nome': 'Terceiro' }, { 'Codigo': 'F', 'Nome': 'Afiliada' }, { 'Codigo': 'P', 'Nome': 'Parceiro' }, ]
    $scope.TipoComissao = [{ 'Codigo': 'B', 'Nome': 'Bruto' }, { 'Codigo': 'L', 'Nome': 'Líquido' }]
    $scope.FormaPgto= [{ 'Codigo': 0, 'Nome': 'Espécie' }, { 'Codigo': '1', 'Nome': 'Permuta' }]
    $scope.ContratoDados = "";
    $scope.Natureza_Servico = [];
    $scope.CondicoesPgto = []
    $scope.currentTab = 0;
    $scope.SequenciadorParcela = 0;
    $scope.SequenciadorRateio = 1;
    $scope.Sequenciador_Id_Rateio = 0;

    ///==============Busca um Modelo de Complemento Vazio
    $scope.CarregaComplemento = function () {
        httpService.Post('GetEmptyOutrasReceitas').then(function (response) {
            if (response) {
                $scope.ContratoDados = response.data;
            };
        });
    };
    $scope.CarregaComplemento();

    //----------------------------Carrega combo Condicoes de Pagamento
    _url = 'ListarTabela/Condica_Pagamento'
    httpService.Get(_url).then(function (response) {
        if (response.data) {
            $scope.CondicoesPgto = response.data;
        }
    });
    ///==============Quando mudar a Empresa de Faturamento carrega o combo de Natureza de servicos 
    $scope.$watch('ContratoDados.Cod_Empresa_Faturamento', function (newValue, oldValue) {
        if (newValue != oldValue) {
            $scope.Natureza_Servico = [];
            if (newValue) {
                $scope.ContratoDados.Cod_Natureza = "";
                var _url = "GetNaturezaRegra"
                _url += "?Cod_Empresa_Faturamento=" + $scope.ContratoDados.Cod_Empresa_Faturamento;
                _url += "&Cod_Empresa=";
                _url += "&Numero_Mr=";
                _url += "&Sequencia_Mr=";
                _url += "&Tipo=" + '0'
                _url += "&";
                httpService.Get(_url).then(function (response) {
                    if (response.data.length>0) {
                        $scope.Natureza_Servico = response.data
                        $scope.ContratoDados.Natureza_Servico = response.data[0].Cod_Natureza;
                        $scope.ContratoDados.Percentual_Iss = response.data[0].Percentual_Iss;
                        $scope.ContratoDados.Cod_Historico = response.data[0].Cod_Historico;
                    }
                    else {
                        ShowAlert("Nenhuma Natureza de Serviços está associada a essa Empresa de Faturamento.")
                    }

                });
            };
        };
    });

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
            $scope.CalculaParcelas($scope.currentTab);
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
                    title: "Confirma a exclusão da Parcela " + (pDuplicatas[_index].Parcela),
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
                    $scope.CalculaParcelas($scope.currentTab);
                    $scope.$apply();
                });

            };
        };
    };
    //===========================Recalcula as parcelas();
    $scope.CalculaParcelas = function (pRateio) {
        if (!$scope.ContratoDados.Rateios[pRateio].Data_Emissao) {
            return;
        }
        var _Tipo_Vencimento = "";
        var _Qtd_Dias = 0;
        var _condicaoPgto = ""
        var _diaBase = parseInt($scope.ContratoDados.Rateios[pRateio].Data_Emissao.substr(0, 2));
        var _mesBase = parseInt($scope.ContratoDados.Rateios[pRateio].Data_Emissao.substr(3, 2));
        var _anoBase = parseInt($scope.ContratoDados.Rateios[pRateio].Data_Emissao.substr(6, 4));
        var _database = undefined;
        var _VencimentoPrimeira = new Date();
        var _Valor_Total;
        var _Valor_Parcela;
        var _Soma = 0;
        var _qtd_parcelas = $scope.ContratoDados.Rateios[pRateio].Duplicatas.length;
        var _temp = undefined;
        //---------------------Acha a condicao de pagamento do Rateio
        for (var i = 0; i < $scope.CondicoesPgto.length; i++) {
            if ($scope.CondicoesPgto[i].Codigo.trim() == $scope.ContratoDados.Rateios[pRateio].Cod_Condicao.trim()) {
                _Tipo_Vencimento = $scope.CondicoesPgto[i].Tipo_Vencimento.trim().toUpperCase();
                _Qtd_Dias = $scope.CondicoesPgto[i].Qtd_Dias;
            };
        };
        if (!_Tipo_Vencimento) {
            return;
        };

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
        for (var i = 0; i < $scope.ContratoDados.Rateios[pRateio].Duplicatas.length; i++) {
            _temp = new Date(_VencimentoPrimeira.valueOf())
            _temp = addMonths(_temp, i);
            $scope.ContratoDados.Rateios[pRateio].Duplicatas[i].Vencimento = DateToString(_temp);
            $scope.ContratoDados.Rateios[pRateio].Duplicatas[i].Dia_Semana= _temp.getDay();
        };
        //-------------------Calcula valores das parcelas 
        _Valor_Total = DoubleVal($scope.ContratoDados.Rateios[pRateio].Vlr_A_Faturar);
        for (var i = 0; i < $scope.ContratoDados.Rateios[pRateio].Duplicatas.length - 1; i++) {
            _Valor_Parcela = parseFloat((_Valor_Total / _qtd_parcelas).toFixed(2));
            $scope.ContratoDados.Rateios[pRateio].Duplicatas[i].Valor = MoneyFormat(_Valor_Parcela);
            _Soma += _Valor_Parcela;
        }
        //----------------Ultima parcela fica com a diferenca de arredondamento
        $scope.ContratoDados.Rateios[pRateio].Duplicatas[$scope.ContratoDados.Rateios[pRateio].Duplicatas.length - 1].Valor = MoneyFormat(_Valor_Total - _Soma);
    };

    
    //===========================Quando mudou o valor total a fatuar
    $scope.RecalculaRateios = function () {
        var _Total_Fatura = DoubleVal($scope.ContratoDados.Vlr_A_Faturar);
        var _perc = 0;
        var _Valor_Rateio = 0 
        for (var i = 0; i < $scope.ContratoDados.Rateios.length; i++) {
            _perc = DoubleVal($scope.ContratoDados.Rateios[i].Perc_Rateio);
            _Valor_Rateio = _Total_Fatura * (_perc / 100);
            $scope.ContratoDados.Rateios[i].Vlr_A_Faturar = MoneyFormat(_Valor_Rateio);
            $scope.CalculaParcelas(i);
        }
        
        $scope.CalculaSaldo();
    }
    //===========================Quando mudou o valor a faturar do rateio
    $scope.ValorFaturarChange = function () {
        var _Total_Fatura = DoubleVal($scope.ContratoDados.Vlr_A_Faturar);
        var _Vlr_Rateio = DoubleVal($scope.ContratoDados.Rateios[$scope.currentTab].Vlr_A_Faturar);;
        $scope.ContratoDados.Saldo_A_Faturar = _Total_Fatura - _Vlr_Rateio;
        if (_Total_Fatura>0) {
            $scope.ContratoDados.Rateios[$scope.currentTab].Perc_Rateio = PercentFormat((_Vlr_Rateio / _Total_Fatura) * 100);
        }
        else {
            $scope.ContratoDados.Rateios[$scope.currentTab].Perc_Rateio = PercentFormat(0);
        }

        $scope.CalculaParcelas($scope.currentTab);
        $scope.CalculaSaldo();
    };
    //===========================Quando mudou o valor o Pct do rateio
    $scope.Perc_RateioChange = function () {
        var _Total_Fatura = $scope.ContratoDados.Vlr_A_Faturar;
        var _perc = DoubleVal($scope.ContratoDados.Rateios[$scope.currentTab].Perc_Rateio);
        var _Valor_Rateio = _Total_Fatura * (_perc / 100);
        $scope.ContratoDados.Rateios[$scope.currentTab].Vlr_A_Faturar = MoneyFormat(_Valor_Rateio);
        $scope.CalculaParcelas($scope.currentTab);
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
        $scope.CalculaParcelas($scope.currentTab);
    };
    //===========================Mudou a data de emissao
    $scope.$watch('ContratoDados.Rateios[currentTab].Data_Emissao', function (newValue, oldValue) {
        if (newValue != oldValue) {
            $scope.CalculaParcelas($scope.currentTab);
        }

    });
    //===========================Gravar o Complemento 
    $scope.SalvarComplemento = function (pComplemento) {
        httpService.Post('SalvarComplemento', pComplemento).then(function (response) {
            if (response) {
                ShowAlert(response.data[0].Mensagem);
            }
            if (response.data[0].Status == 1) {
                $scope.CarregaComplemento();
            };
        });
    };
    //===========================fim do load da pagina
    //$scope.$watch('$viewContentLoaded', function () {
    //    $scope.ConfiguraGrid();
    //});

}]);

