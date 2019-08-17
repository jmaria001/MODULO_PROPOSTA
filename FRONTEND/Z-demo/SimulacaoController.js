angular.module('App').controller('SimulacaoController', ['$scope', '$filter', 'GenericApi', 'SimulacaoApi', '$routeParams', function ($scope, $filter, GenericApi, SimulacaoApi,  $routeParams) {

    //============ Inicializa Scopes e variaveis

    $scope.Parameters = $routeParams;
    $scope.NewDigitacaoEsquema = function () {
        return { 'Cod_Programa': '', 'Cod_Caracteristica': '', 'Cod_Tipo_Comercial': '', 'Titulo_Comercial': '', 'DiaInicio': '', 'DiaFim': '', 'Qtd': '', 'Desconto': '', 'Valor_Tagela': '', 'Valor_Negociado': '' };
    }
    $scope.NewSimulacao = function () {
        return {
            "Id_Simulacao": 0, "Identificacao": "", "Inicio_Validade": "", "Termino_Validade": "", "Cod_Empresa_Venda": "", "Nome_Empresa_Venda": "", "Tabela_Preco": "",
            "Valor_Tabela": "", "Valor_Negociado": "", 'Valor_Informado': "", "Desconto_Padrao": "", "Desconto_Real": "", "FixarValor": false, "FixarDesconto": false, 'DescontoDetalhado': []
        }
    };
    $scope.newInsercoes = function () {
        return []
        //return [{
        //    'Editavel': false,
        //    'Cod_Programa': '',
        //    'Cod_Tipo_Comercial': '',
        //    'Titulo_Comercial': 'A Determinar',
        //    'Duracao': '',
        //    'Digitacao': [],
        //    'Desconto_Simulacao': '',
        //    'Desconto_Esquema': '',
        //    'Desconto_Linha': '',
        //    'Desconto_Detalhado': '',
        //}]
    };

    $scope.DigitacaoEsquema = $scope.NewDigitacaoEsquema()
    $scope.PesquisaTabelas = { "Items": [], 'FiltroTexto': '', cback: '' };


    $scope.Abrangencias = [{ 'Id': 0, 'Descricao': 'Net' }, { 'Id': 1, 'Descricao': 'Rede' }, { 'Id': 2, 'Descricao': 'Local' }];
    $scope.Param_Desconto = [
                                { 'Codigo': 1, 'Descricao': 'Período' },
                                { 'Codigo': 2, 'Descricao': 'Produto' },
                                { 'Codigo': 4, 'Descricao': 'Mercado' },
                                { 'Codigo': 3, 'Descricao': 'Veiculo' },
                                { 'Codigo': 6, 'Descricao': 'Faixa Horária' },
                                { 'Codigo': 10, 'Descricao': 'Gênero' },
                                { 'Codigo': 5, 'Descricao': 'Programa' },
                                { 'Codigo': 7, 'Descricao': 'Dia da Semana' },
                                { 'Codigo': 8, 'Descricao': 'Tipo Comercial' },
                                { 'Codigo': 9, 'Descricao': 'Duração' }];

    $scope.Confirm = { 'Message': '', 'Params': { 'Id': '', 'Keys': [] } }
    $scope.currentShow = "Base";
    $scope.currentEsquema = -1;
    var Id_Desconto_Detalhado = 0;
    var Id_Esquema = 0;
    var Id_Insercoes = 0;
    $scope.DescontoDetalhado = { 'Id_Desconto_Detalhado': '', 'TipoDesconto': '', 'Chave': '', 'Conteudo': '', 'Desconto': '', 'Data_Inicio': '', 'Data_Termino': '' };
    $scope.OpcoesDesconto = [];
    $scope.TipoDescontoFilter = "";
    $scope.Critica_Simulacao = [];

    $scope.TemplateEsquema = function () {
        return {
            'Id_Esquema': 0,
            'Competencia': '',
            'Competencia_old': '',
            'DescontoEsquema': '',
            'Valor_Tabela': '',
            'Valor_Negociado': '',
            'FixarDesconto': false,
            'FixarValor': false,
            'Desconto_Padrao': '',
            'Desconto_Linha': '',
            'Abrangencia': '',
            'Cod_Mercado': '',
            'Cod_Empresa_Faturamento': '',
            'Veiculos': [],
            'Valido': true,  //quando excluir o esquema nao apagar, marcar Valido como false
            'Insercoes': $scope.newInsercoes(),
            'Participacao': ''
        }
    }

    $scope.Esquemas = [];
    $scope.ListadeVeiculos = [];
    $scope.DiasInicio = [];
    $scope.DiasFim = [];


    //=================================Carrega ou Instancia uma nova Simulacao
    if ($scope.Parameters.Action == "New") {
        $scope.Simulacao = $scope.NewSimulacao();
    }
    else {
        SimulacaoApi.GetSimulacao($scope.Parameters.Id).then(function (response) {
            if (response.data) {
                $scope.Simulacao =
                    {
                        'Id_Simulacao': response.data.Simulacao.Id_Simulacao,
                        'Identificacao': response.data.Simulacao.Identificacao,
                        'Cod_Empresa_Venda': response.data.Simulacao.Cod_Empresa_Venda,
                        'Nome_Empresa_Venda': response.data.Simulacao.Nome_Empresa_Venda,
                        'Tabela_Preco': response.data.Tabela_Preco,
                        'Valor_Negociado': $filter('currency')(response.data.Simulacao.Valor_Negociado, 'R$ ', 2),
                        'Valor_Tabela': $filter('currency')(response.data.Simulacao.Valor_Tabela, 'R$ ', 2),
                        'Desconto_Real': $filter('percentage')(response.data.Simulacao.Desconto_Real, 6),
                        'FixarValor': response.data.Simulacao.FixarValor,
                        'FixarDesconto': response.data.Simulacao.FixarDesconto,
                        'Inicio_Validade': $filter('date')(response.data.Simulacao.Inicio_Validade, 'dd/MM/yyyy'),
                        'Termino_Validade': $filter('date')(response.data.Simulacao.Termino_Validade, 'dd/MM/yyyy'),
                        'DescontoDetalhado': response.data.Desconto_Detalhado,
                    };
                //if ($scope.Simulacao.FixarDesconto) {
                    $scope.Simulacao.Desconto_Padrao = $filter('percentage')(response.data.Simulacao.Desconto_Padrao,  6);
                //}
                //if ($scope.Simulacao.FixarValor) {
                    $scope.Simulacao.Valor_Informado = $filter('currency')(response.data.Simulacao.Valor_Informado, 'R$ ', 2);
                //}

                SetDatepicker("txtInicioValidade", response.data.Simulacao.Inicio_Validade);
                SetDatepicker("txtTerminoValidade", response.data.Simulacao.Termino_Validade);

                $scope.Esquemas = response.data.Esquema;
                for (var i = 0; i < $scope.Esquemas.length; i++) {
                    $scope.Esquemas[i].Valor_Tabela = $filter('currency')($scope.Esquemas[i].Valor_Tabela, 'R$ ', 2);
                    if ($scope.Esquemas[i].Desconto_Padrao > 0) {
                        $scope.Esquemas[i].Desconto_Padrao = $filter('number')($scope.Esquemas[i].Desconto_Padrao, 6);
                    }
                    else {
                        $scope.Esquemas[i].Desconto_Padrao = "";
                    }

                    $scope.Esquemas[i].Valor_Negociado = $filter('currency')($scope.Esquemas[i].Valor_Negociado, 'R$ ', 2)
                    $scope.Esquemas[i].Abrangencia = $scope.Abrangencias[parseInt($scope.Esquemas[i].Abrangencia.Id)];
                };
                Id_Desconto_Detalhado = response.data.Max_Id_Desconto_Detalhado;
                Id_Insercoes = response.data.Max_Id_Insercao;
                Id_Esquema = response.data.Max_Id_Esquema;
                Id_Desconto_Detalhado = response.data.Max_Id_Desconto_Detalhado;
                if ($scope.Esquemas.length > 0) {
                    $scope.currentEsquema = 0;
                    var _yy = $scope.Esquemas[0].Competencia.substr(3, 4);
                    var _mm = $scope.Esquemas[0].Competencia.substr(0, 2);
                    SetDatepicker("txtEsquemaCompetencia", new Date(_yy, _mm - 1));
                }
                
            }
        });
    }
    //=================================Totaliza a Simulacao
    $scope.TotalizarSimulacao = function () {
        SimulacaoApi.CalcularSimulacao($scope.Simulacao, $scope.Esquemas).then(function (response) {
            if (response.data) {
                var _Total_Tabela_Esquema = 0;
                var _Total_Negociado_Esquema = 0;
                var _Total_Tabela_Simulacao = 0;
                var _Total_Negociado_Simulacao = 0;

                for (var iEsquema = 0; iEsquema < $scope.Esquemas.length; iEsquema++) {
                    _Total_Tabela_Esquema = 0;
                    _Total_Negociado_Esquema = 0;
                    for (var iInsercao = 0; iInsercao < $scope.Esquemas[iEsquema].Insercoes.length; iInsercao++) {
                        for (var iResponse = 0; iResponse < response.data.length; iResponse++) {
                            if ($scope.Esquemas[iEsquema].Insercoes[iInsercao].Id_Insercoes == response.data[iResponse].Id_Insercao) {
                                _Total_Tabela_Esquema += response.data[iResponse].Valor_Tabela;
                                _Total_Negociado_Esquema += response.data[iResponse].Valor_Negociado;
                                _Total_Tabela_Simulacao += response.data[iResponse].Valor_Tabela;
                                _Total_Negociado_Simulacao += response.data[iResponse].Valor_Negociado;
                                $scope.Esquemas[iEsquema].Insercoes[iInsercao].Valor_Tabela = response.data[iResponse].Valor_Tabela_Unitario;
                                $scope.Esquemas[iEsquema].Insercoes[iInsercao].Valor_Negociado = response.data[iResponse].Valor_Negociado;
                                $scope.Esquemas[iEsquema].Insercoes[iInsercao].Desconto_Real = response.data[iResponse].Desconto_Real;
                                $scope.Esquemas[iEsquema].Insercoes[iInsercao].Qtd_Total = response.data[iResponse].Qtd;
                                $scope.Esquemas[iEsquema].Insercoes[iInsercao].Critica_Tabela = response.data[iResponse].Critica;
                            }
                        }
                    }
                    $scope.Esquemas[iEsquema].Valor_Tabela = $filter('currency')(_Total_Tabela_Esquema, 'R$ ', 2)
                    $scope.Esquemas[iEsquema].Valor_Negociado= $filter('currency')(_Total_Negociado_Esquema, 'R$ ', 2)
                    var _Desconto_Esquema = (1 - (_Total_Negociado_Esquema / _Total_Tabela_Esquema)) * 100
                    $scope.Esquemas[iEsquema].Desconto_Padrao= $filter('number')(_Desconto_Esquema, 6)
                }
                $scope.Simulacao.Valor_Negociado = $filter('currency')(_Total_Negociado_Simulacao, 'R$ ', 2)
                $scope.Simulacao.Valor_Tabela = $filter('currency')(_Total_Tabela_Simulacao, 'R$ ', 2)
                var _Desconto_Simulacao = (1 - (_Total_Negociado_Simulacao / _Total_Tabela_Simulacao)) * 100
                $scope.Simulacao.Desconto_Real = $filter('percentage')(_Desconto_Simulacao, 6)

                if (!$scope.Simulacao.FixarDesconto) {
                    $scope.Simulacao.Desconto_Padrao= $filter('number')(_Desconto_Simulacao, 6)
                }
                if (!$scope.Simulacao.FixarValor) {
                    $scope.Simulacao.Valor_Informado = $filter('currency')(_Total_Negociado_Simulacao, 'R$ ', 2)
                }
            }
        });
    }

    //===================================Quando clicou no adicionar Desconto Detalhado
    $scope.AdicionarDescontoDetalhado = function () {
        var _qtd_Selecionado = 0;
        if ($scope.DescontoDetalhado.TipoDesconto.Codigo == 1) {
            var _TempDesconto = {}
            if (!$scope.DescontoDetalhado.Data_Inicio || !$scope.DescontoDetalhado.Data_Termino) {
                ShowAlert('Data Início e Data Término são obrigatórios', 'warning', 2000, 'center');
                return
            }
            _TempDesconto.TipoDesconto = $scope.DescontoDetalhado.TipoDesconto;
            _TempDesconto.Desconto = $scope.DescontoDetalhado.Desconto;
            _TempDesconto.Data_Inicio = $scope.DescontoDetalhado.Data_Inicio
            _TempDesconto.Data_Termino = $scope.DescontoDetalhado.Data_Termino
            _TempDesconto.Conteudo = $scope.DescontoDetalhado.Data_Inicio + ' a ' + $scope.DescontoDetalhado.Data_Termino
            Id_Desconto_Detalhado++
            _TempDesconto.Id_Desconto_Detalhado = Id_Desconto_Detalhado
            $scope.Simulacao.DescontoDetalhado.push(_TempDesconto);
        }
        else {
            for (var i = 0; i < $scope.OpcoesDesconto.length; i++) {
                if ($scope.OpcoesDesconto[i].Selecionado) {
                    _qtd_Selecionado++
                    var _TempDesconto = {}
                    _TempDesconto.TipoDesconto = $scope.DescontoDetalhado.TipoDesconto;
                    _TempDesconto.Desconto = $scope.DescontoDetalhado.Desconto;
                    _TempDesconto.Chave = $scope.OpcoesDesconto[i].Codigo;
                    _TempDesconto.Conteudo = $scope.OpcoesDesconto[i].Descricao;
                    Id_Desconto_Detalhado++
                    _TempDesconto.Id_Desconto_Detalhado = Id_Desconto_Detalhado
                    $scope.Simulacao.DescontoDetalhado.push(_TempDesconto);
                    console.log($scope.Simulacao.DescontoDetalhado);
                }
            }
            if (_qtd_Selecionado == 0) {
                ShowAlert('Nenhum Item foi selecionado', 'warning', 2000, 'center');
                return
            }
        }
        $scope.OpcoesDesconto = [];
        $scope.DescontoDetalhado = { 'Id_Desconto_Detalhado': '', 'TipoDesconto':'', 'Chave': '', 'Conteudo': '', 'Desconto': '', 'Data_Inicio': '', 'Data_Termino': '' };
        //$scope.DescontoDetalhado.TipoDesconto = '';

    }

    //===================================Quando clicou na Lixeira em uma linha do desconto detalhado
    $scope.RemoverDescontoDetalhado = function (pId_Desconto_Detalhado) {
        for (var x = 0; x < $scope.Simulacao.DescontoDetalhado.length; x++) {
            if ($scope.Simulacao.DescontoDetalhado[x].Id_Desconto_Detalhado == pId_Desconto_Detalhado) {
                $scope.Simulacao.DescontoDetalhado.splice(x, 1)
            }
        }
    }
    //===================================Quando mudou a opcao de tipo de desconto
    $scope.$watch('DescontoDetalhado.TipoDesconto', function (newValue, oldValue) {
        if (newValue) {
            console.log(newValue);
            if (newValue != oldValue) {
                $scope.TipoDescontoFilter = ""
                $scope.OpcoesDesconto = [];
                if (newValue.Codigo != 1) {
                    SimulacaoApi.GetOpcoesDesconto(newValue.Codigo).then(function (response) {
                        if (response.data) {
                            $scope.OpcoesDesconto = response.data;
                        }
                    });
                }
            }
        }
    });

    //===================================Clicou em Adicionar Esquema
    $scope.AdicionarEsquema = function () {
        $scope.Simulacao.Inicio_Validade = FormataData($scope.Simulacao.Inicio_Validade);
        $scope.Simulacao.Termino_Validade = FormataData($scope.Simulacao.Termino_Validade);
        if (StringToDate($scope.Simulacao.Inicio_Validade, 'dd-mm-yyyy') > StringToDate($scope.Simulacao.Termino_Validade, 'dd-mm-yyyy')) {
            ShowAlert('Início da Validade deve ser menor que Término da Validade', 'warning', 2000, 'center');
            return;
        }

        Id_Esquema++
        var _TempEsquema = $scope.TemplateEsquema();
        _TempEsquema.Id_Esquema = Id_Esquema
        $scope.Esquemas.push(_TempEsquema);
        $scope.currentEsquema = Id_Esquema - 1;
        $scope.Esquemas[$scope.currentEsquema].Cod_Empresa_Faturamento = $scope.Simulacao.Cod_Empresa_Venda //assume empresa de faturamento igual de venda
    }

    //===================================Clicou no botao competencia dos esquemas
    $scope.SetCurrenEsquema = function (pId_Esquema) {
        $scope.currentEsquema = pId_Esquema - 1;

    }

    //===================================Mudou a Competencia de um esquema
    $scope.$watch('Esquemas[currentEsquema].Competencia', function (newValue, oldValue) {
        var _confirm = false
        if (!newValue) {
            return;
        }
        if (!IsCompetencia($scope.Esquemas[$scope.currentEsquema].Competencia)) {
            ShowAlert('Competencia Inválida', 'warning', 2000, 'center');
            return
        }
        $scope.Esquemas[$scope.currentEsquema].Competencia = FormataCompetencia($scope.Esquemas[$scope.currentEsquema].Competencia);
        if ($scope.Esquemas[$scope.currentEsquema].Insercoes.Digitacao) {
            if ($scope.Esquemas[$scope.currentEsquema].Insercoes.Digitacao.length > 0) {
                _confirm = true
            }
        }
        if (_confirm) {
            $scope.Confirm.Message = 'Ao mudar a competência serão apagadas a simulação desse esquema. Confirma a mudança ?'
            $scope.Confirm.Params.Id = 'EsquemaCompetenciaChange'
            $("#modalConfirm").modal(true);
        }
        else {
            $scope.DigitacaoEsquema = $scope.NewDigitacaoEsquema();
        }
    });
    //==========================Quando mudou a abrangencia
    $scope.Abrangencia_Change = function (Id_Abrangencia) {
        $scope.Esquemas[$scope.currentEsquema].Veiculos = [];
        if (Id_Abrangencia === 0) {
            SimulacaoApi.GetVeiculos(Id_Abrangencia, "", $scope.Simulacao.Cod_Empresa_Venda, $scope.Esquemas[$scope.currentEsquema].Cod_Empresa_Faturamento).then(function (response) {
                if (response.data) {
                    for (var i = 0; i < response.data.length; i++) {
                        $scope.Esquemas[$scope.currentEsquema].Veiculos.push({ 'Cod_Veiculo': response.data[i].Cod_Veiculo, 'Descricao': response.data[i].Descricao })
                    }
                }
            });
        }
    }


    //=====================Clicou em selecionar veiculos
    $scope.SelecionarVeiculos = function () {
        SimulacaoApi.GetVeiculos($scope.Esquemas[$scope.currentEsquema].Abrangencia.Id, $scope.Esquemas[$scope.currentEsquema].Cod_Mercado, $scope.Simulacao.Cod_Empresa_Venda, $scope.Esquemas[$scope.currentEsquema].Cod_Empresa_Faturamento).then(function (response) {

            if (response.data) {
                $scope.ListadeVeiculos = response.data;
                var _Selecionados = $scope.Esquemas[$scope.currentEsquema].Veiculos.filter(function (el) {
                    return (el.Selected === true);
                });
                for (var x = 0; x < _Selecionados.length; x++) {
                    for (var y = 0; y < $scope.ListadeVeiculos.length; y++) {
                        if (_Selecionados[x].Cod_Veiculo == $scope.ListadeVeiculos[y].Cod_Veiculo) {
                            $scope.ListadeVeiculos[y].Selected = true
                        }
                    }
                }
                if ($scope.Esquemas[$scope.currentEsquema].Cod_Mercado) {
                    $scope.Esquemas[$scope.currentEsquema].Veiculos = [];
                    for (var x = 0; x < $scope.ListadeVeiculos.length; x++) {
                        $scope.Esquemas[$scope.currentEsquema].Veiculos.push($scope.ListadeVeiculos[x])
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
        for (var i = 0; i < $scope.Esquemas[$scope.currentEsquema].Veiculos.length; i++) {
            if ($scope.Esquemas[$scope.currentEsquema].Veiculos[i].Cod_Veiculo == pCodVeiculo) {
                $scope.Esquemas[$scope.currentEsquema].Veiculos.splice(i, 1);
                break;
            }
        }
    }
    //=====================Clicou em Ok da Selecão de Veiculos
    $scope.SelecaoVeiculoOk = function () {
        $scope.Esquemas[$scope.currentEsquema].Veiculos = [];
        for (var i = 0; i < $scope.ListadeVeiculos.length; i++) {
            if ($scope.ListadeVeiculos[i].Selected) {
                $scope.Esquemas[$scope.currentEsquema].Veiculos.push({ 'Cod_Veiculo': $scope.ListadeVeiculos[i].Cod_Veiculo, 'Descricao': $scope.ListadeVeiculos[i].Descricao });
            }
        }
    }
    //==================Confirmacoes 
    $scope.ConfirmAction = function (value) {
        if (value) { ///Confirmou
            switch ($scope.Confirm.Params.Id) {
                case 'EsquemaCompetenciaChange':
                    $scope.Esquemas[$scope.currentEsquema].Competencia_old = $scope.Esquemas[$scope.currentEsquema].Competencia
                    $scope.Esquemas[$scope.currentEsquema].Insercoes = $scope.newInsercoes();
                    $scope.DigitacaoEsquema = $scope.NewDigitacaoEsquema();
                default:
                    break

            }
        }
        else //nao confirmou
            switch ($scope.Confirm.Params.Id) {
                case 'EsquemaCompetenciaChange':
                    $scope.Esquemas[$scope.currentEsquema].Competencia = $scope.Esquemas[$scope.currentEsquema].Competencia_old
                default:
                    break
            }
    }
    //===============Clicou na lupa da digitacao do programa do esquema
    $scope.PesquisaTabela_Grade = function () {
        if ($scope.Esquemas[$scope.currentEsquema].Veiculos.length == 0 || !$scope.Esquemas[$scope.currentEsquema].Competencia) {
            ShowAlert('Informe a Competência e selecione os veiculos antes de informar o programa', 'warning', 2000, 'center');
            return
        }
        SimulacaoApi.GetProgramasGrade($scope.Esquemas[$scope.currentEsquema].Veiculos, $scope.Esquemas[$scope.currentEsquema].Competencia).then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data
                $scope.PesquisaTabelas.FiltroTexto = ""
                $scope.PesquisaTabelas.cback = function (value) { $scope.DigitacaoEsquema.Cod_Programa = value; }
                $("#modalTabela").modal(true);
            }
        });
    }
    //===============Clicou na lupa da digitacao de Caracteristica do esquema
    $scope.PesquisaCaracteristica = function () {
        GenericApi.ListarTabela("Caracteristica_Veiculacao").then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data
                $scope.PesquisaTabelas.FiltroTexto = ""
                $scope.PesquisaTabelas.cback = function (value) { $scope.DigitacaoEsquema.Cod_Caracteristica = value; }
                $("#modalTabela").modal(true);
            }
        });
    }
    //===============Clicou na lupa Tpo do Comercial do esquema
    $scope.PesquisaTipoComercial = function () {
        GenericApi.ListarTabela("Tipo_Comercial").then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data
                $scope.PesquisaTabelas.FiltroTexto = ""
                $scope.PesquisaTabelas.cback = function (value) { $scope.DigitacaoEsquema.Cod_Tipo_Comercial = value; }
                $("#modalTabela").modal(true);
            }
        });
    }
    //===============Clicou em Fixar Valor ou Desconto
    $scope.Fixar = function (pField) {
        if (pField == 'Valor') {
            $scope.Simulacao.FixarDesconto = false;
            $scope.Simulacao.Desconto_Padrao = ""
        }
        if (pField == 'Desconto') {
            $scope.Simulacao.FixarValor = false;
            $scope.Simulacao.Valor_Informado = ""
        }
        if (pField == 'ValorEsquema') {
            $scope.Esquemas[$scope.currentEsquema].FixarDesconto = false;
            $scope.Esquemas[$scope.currentEsquema].Desconto_Padrao = ""
        }
        if (pField == 'DescontoEsquema') {
            $scope.Esquemas[$scope.currentEsquema].FixarValor = false;
            $scope.Esquemas[$scope.currentEsquema].Valor_Negociado = "";
        }

        $scope.TotalizarSimulacao();
    }

    //======================Adicionar Insercoes ao esquema
    $scope.AdicionarInsercoes = function () {
        if ($scope.Esquemas[$scope.currentEsquema].Veiculos.length == 0) {
            ShowAlert('Nenhum Veiculo foi selecionado', 'warning', 2000, 'center');
            return
        }
        if (!$scope.Esquemas[$scope.currentEsquema].Cod_Empresa_Faturamento) {
            ShowAlert('Empresa de Faturamento é obrigatório', 'warning', 2000, 'center');
            return
        }

        if (!$scope.DigitacaoEsquema.Cod_Programa ||
                !$scope.DigitacaoEsquema.Cod_Caracteristica ||
                !$scope.DigitacaoEsquema.Cod_Tipo_Comercial ||
                !$scope.DigitacaoEsquema.Duracao ||
                !$scope.DigitacaoEsquema.DiaInicio ||
                !$scope.DigitacaoEsquema.DiaFim ||
                !$scope.DigitacaoEsquema.Qtd ||
                $scope.DigitacaoEsquema.Qtd == 0 ||
                $scope.DigitacaoEsquema.Duracao == 0
            ) {
            ShowAlert('Algum campo obrigatório não foi preenchido', 'warning', 2000, 'center');
            return
        }
        SimulacaoApi.AdicionarInsercoes($scope.Simulacao, $scope.Esquemas[$scope.currentEsquema], $scope.DigitacaoEsquema).then(function (response) {
            if (response.data) {
                if (response.data[0].Status == 0) {
                    ShowAlert(response.data[0].Critica, 'warning', 2000, 'center');
                    return
                }
                else {
                    Id_Insercoes++
                    //var _qtd_total = 0
                    //for (var i = 0; i < response.data.length; i++) {
                    //    _qtd_total += response.data[i].Qtd;
                    //}

                    $scope.Esquemas[$scope.currentEsquema].Insercoes.push({
                        'Id_Insercoes': Id_Insercoes,
                        'Cod_Programa': $scope.DigitacaoEsquema.Cod_Programa.toUpperCase(),
                        'Cod_Caracteristica': $scope.DigitacaoEsquema.Cod_Caracteristica.toUpperCase(),
                        'Cod_Tipo_Comercial': $scope.DigitacaoEsquema.Cod_Tipo_Comercial.toUpperCase(),
                        'Titulo_Comercial': $scope.DigitacaoEsquema.Titulo_Comercial.toUpperCase(),
                        'Duracao': $scope.DigitacaoEsquema.Duracao,
                        'Digitacao': response.data,
                        'Desconto_Linha': $scope.DigitacaoEsquema.Desconto,
                        //'Desconto_Detalhado': 0,
                        //'Valor_Tabela': response.data[0].Valor_Tabela,
                        //'Qtd_Total': _qtd_total,
                        //'Critica_Tabela': response.data[0].Critica_Tabela.split('<br/>'),
                        //'Critica_Parametro': response.data[0].Critica_Parametro.split('<br/>')
                    }
                    )
                    $scope.TotalizarSimulacao();
                }
            }
        });
    }
    //=================================Remover Linha da Digitacao
    $scope.RemoverLinhaDigitacao = function (pIdInsercoes) {
        var _Index = 0
        for (var i = 0; i < $scope.Esquemas[$scope.currentEsquema].Insercoes.length; i++) {
            if ($scope.Esquemas[$scope.currentEsquema].Insercoes[i].Id_Insercoes == pIdInsercoes) {
                _Index = i
                break
            }
        }
        $scope.Esquemas[$scope.currentEsquema].Insercoes.splice(_Index, 1);
        $scope.TotalizarSimulacao();
    }

    //=================================Mostra a critica de Valoracao
    $scope.MostraCritica = function (pLinha) {
        if (pLinha) {
            console.log(pLinha)
            $scope.Critica_Tabela = pLinha.Critica_Tabela.split('<br/>')
            console.log($scope.Critica_Tabela);
        }
        $("#modalCritica").modal(true);
    }

    $scope.Calcular = function () {
        $scope.TotalizarSimulacao()
    }
    //=================================Salvar a Simulacao
    $scope.SalvarSimulacao = function () {
        $scope.Critica_Simulacao = [];
        SimulacaoApi.SalvarSimulacao($scope.Simulacao, $scope.Esquemas).then(function (response) {
            if (response.data) {
                if (response.data[0].Critica) {
                    for (var i = 0; i < response.data.length; i++) {
                        $scope.Critica_Simulacao.push({ 'Critica': response.data[i].Critica })
                    }
                    ShowAlert('Houve Inconsistências na Gravação. Clique em ver inconsistências para mais detalhes', 'error', 2000, 'center');
                }
                else {
                    $scope.Simulacao.Id_Simulacao = response.data[0].Id_Simulacao;
                    ShowAlert('Dados Gravados com Sucesso', 'success', 2000, 'center');
                }
            }
        });
    }

    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {

    });
    //$scope.mock = function()

}]);
function SetDatepicker(element, date) {
    setTimeout(function () {
        var _date = new Date(date);
        $("#" + element).datepicker("update", _date);
    });

}

