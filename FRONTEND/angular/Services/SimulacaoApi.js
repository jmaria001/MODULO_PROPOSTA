angular.module("App").service("SimulacaoApi", function ($http, config, $q, $cookies) {


    var _ListSimulacao = function () {
        var deferred = $q.defer();
        var _url = "";
        _url = config.baseUrl + "API/ListSimulacao"
        $http({
            method: 'GET',
            url: _url
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };
    var _GetOpcoesDesconto = function (pTipoDesconto) {
        var deferred = $q.defer();
        var _url = "";
        _url = config.baseUrl + "API/GetOpcoesDesconto/" + pTipoDesconto
        $http({
            method: 'GET',
            url: _url
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    var _GetVeiculos = function (pAbrangencia, pCodMercado, pCodEmpresa, pCodEmpresaFaturamento) {
        var deferred = $q.defer();
        var _url = "";
        if (!pCodMercado.trim()) {
            pCodMercado = "@";
        }
        if (!pCodEmpresa.trim()) {
            pCodEmpresa = "@";
        }
        if (!pCodEmpresaFaturamento.trim()) {
            pCodEmpresaFaturamento = "@";
        }
        _url = config.baseUrl + "API/GetVeiculos/" + pAbrangencia + '/' + pCodMercado.trim() + '/' + pCodEmpresa.trim() + '/' + pCodEmpresaFaturamento
        $http({
            method: 'GET',
            url: _url
        }
    ).then(function (response) {
        deferred.resolve(response);
    });
        return deferred.promise
    };

    var _GetProgramasGrade = function (pVeiculo, pCompetencia, pPrograma) {
        var deferred = $q.defer();
        var _Veiculos = [];
        for (var i = 0; i < pVeiculo.length; i++) {
            _Veiculos.push({ 'Cod_Veiculo': pVeiculo[i].Cod_Veiculo, 'Descricao': pVeiculo[i].Descricao })
        }
        var _data = {}
        _data.Veiculos = _Veiculos;
        _data.Competencia = pCompetencia;
        _data.Cod_Programa = pPrograma;
        $http({
            method: 'POST',
            url: config.baseUrl + "API/GetProgramasGrade",
            data: _data,
            //headers: "{'Content-Type': 'application/x-www-form-urlencoded'}"
            headers: { 'Content-Type': 'application/json' }
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };
    var _AdicionarInsercoes = function (pSimulacao, pEsquema, pDigitacaoEsquema) {
        
        var _Veiculos = [];
        for (var i = 0; i < pEsquema.Veiculos.length; i++) {
            _Veiculos.push({ 'Cod_Veiculo': pEsquema.Veiculos[i].Cod_Veiculo, 'Descricao': pEsquema.Veiculos[i].Descricao })
        }

        var deferred = $q.defer();
        var _data = {}
        _data.Competencia = pEsquema.Competencia;
        _data.Cod_Programa = pDigitacaoEsquema.Cod_Programa;
        _data.Veiculos = _Veiculos;
        _data.Cod_Empresa = pSimulacao.Cod_Empresa_Venda;
        _data.Cod_Empresa_Faturamento = pEsquema.Cod_Empresa_Faturamento;
        _data.Cod_Caracteristica = pDigitacaoEsquema.Cod_Caracteristica;
        _data.Cod_Tipo_Comercial = pDigitacaoEsquema.Cod_Tipo_Comercial;
        _data.Duracao = pDigitacaoEsquema.Duracao;
        _data.Qtd = pDigitacaoEsquema.Qtd;
        _data.Dia_Inicio = pDigitacaoEsquema.DiaInicio;
        _data.Dia_Fim = pDigitacaoEsquema.DiaFim;
        _data.Tabela_Preco = pSimulacao.Tabela_Preco;
        _data.Abrangencia = pEsquema.Abrangencia.Id
        $http({
            method: 'POST',
            url: config.baseUrl + "API/AdicionarVeiculacaoEsquema",
            data: _data,
            headers: { 'Content-Type': 'application/json' }
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    var _GetSimulacao = function (pIdSimulacao) {
        var deferred = $q.defer();
        $http({
            method: 'GET',
            url: config.baseUrl + "API/GetSimulacao/" + pIdSimulacao,
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };

    var _SalvarSimulacao = function (pSimulacao, pEsquema) {
        var deferred = $q.defer();
        //=====================Padroniza parametros
        var _Simulacao = {
            Id_Simulacao: pSimulacao.Id_Simulacao,
            Identificacao: pSimulacao.Identificacao,
            Inicio_Validade: StringToDate(pSimulacao.Inicio_Validade, "dd/mm/yyyy"),
            Termino_Validade: StringToDate(pSimulacao.Termino_Validade, "dd/mm/yyyy"),
            Cod_Empresa_Venda: pSimulacao.Cod_Empresa_Venda,
            Tabela_Preco: CompetenciaToInt(pSimulacao.Tabela_Preco),
            Valor_Tabela: DoubleVal(pSimulacao.Valor_Tabela),
            Valor_Negociado: DoubleVal(pSimulacao.Valor_Negociado),
            Valor_Informado: DoubleVal(pSimulacao.Valor_Informado),
            Desconto_Padrao: DoubleVal(pSimulacao.Desconto_Padrao),
            Desconto_Real: DoubleVal(pSimulacao.Desconto_Real),
            FixarValor: pSimulacao.FixarValor,
            FixarDesconto: pSimulacao.FixarDesconto,
        }

        var _DescontoDetalhado = []
        if (pSimulacao.DescontoDetalhado) {

            for (var i = 0; i < pSimulacao.DescontoDetalhado.length; i++) {
                _DescontoDetalhado.push({
                    'Id_Desconto_Detalhado': pSimulacao.DescontoDetalhado[i].Id_Desconto_Detalhado,
                    'TipoDesconto': pSimulacao.DescontoDetalhado[i].TipoDesconto,
                    'Chave': pSimulacao.DescontoDetalhado[i].Chave,
                    'Conteudo': pSimulacao.DescontoDetalhado[i].Conteudo,
                    'Desconto': DoubleVal(pSimulacao.DescontoDetalhado[i].Desconto)
                })
            }
        };
        var _Esquema = [];
        var _tempEsquema = {};
        var _TempInsercao = [];
        var _TempDigitacao = [];

        for (var iEsquema = 0; iEsquema < pEsquema.length; iEsquema++) {
            var _TempInsercao = [];
            _tempEsquema = {
                'Id_Esquema': pEsquema[iEsquema].Id_Esquema,
                'Competencia': pEsquema[iEsquema].Competencia,
                'Cod_Mercado': pEsquema[iEsquema].Cod_Mercado,
                'Cod_Empresa_Faturamento': pEsquema[iEsquema].Cod_Empresa_Faturamento,
                'Desconto_Padrao': DoubleVal(pEsquema[iEsquema].Desconto_Padrao),
                'Abrangencia': pEsquema[iEsquema].Abrangencia,
                'Valor_Negociado': DoubleVal(pEsquema[iEsquema].Valor_Negociado),
                'Valor_Tabela': DoubleVal(pEsquema[iEsquema].Valor_Tabela),
                'Participacao': DoubleVal(pEsquema[iEsquema].Participacao),
                'FixarDesconto': pEsquema[iEsquema].FixarDesconto,
                'FixarValor': pEsquema[iEsquema].FixarValor,
                'Veiculos': pEsquema[iEsquema].Veiculos
            };
            for (var iInsercao = 0; iInsercao < pEsquema[iEsquema].Insercoes.length; iInsercao++) {
                var _TempDigitacao = [];
                _TempInsercao.push({
                    'Id_Insercoes': pEsquema[iEsquema].Insercoes[iInsercao].Id_Insercoes,
                    'Cod_Programa': pEsquema[iEsquema].Insercoes[iInsercao].Cod_Programa,
                    'Cod_Caracteristica': pEsquema[iEsquema].Insercoes[iInsercao].Cod_Caracteristica,
                    'Cod_Comercial': pEsquema[iEsquema].Insercoes[iInsercao].Cod_Comercial,
                    'Cod_Tipo_Comercial': pEsquema[iEsquema].Insercoes[iInsercao].Cod_Tipo_Comercial,
                    'Titulo_Comercial': pEsquema[iEsquema].Insercoes[iInsercao].Titulo_Comercial,
                    'Duracao': pEsquema[iEsquema].Insercoes[iInsercao].Duracao,
                    'Valor_Tabela': DoubleVal(pEsquema[iEsquema].Insercoes[iInsercao].Valor_Tabela),
                    'Valor_Negociado': DoubleVal(pEsquema[iEsquema].Insercoes[iInsercao].Valor_Negociado),
                    'Desconto_Linha': DoubleVal(pEsquema[iEsquema].Insercoes[iInsercao].Desconto_Linha),
                });

                for (var iDia = 0; iDia < pEsquema[iEsquema].Insercoes[iInsercao].Digitacao.length; iDia++) {
                    _TempDigitacao.push({
                        'Dia': pEsquema[iEsquema].Insercoes[iInsercao].Digitacao[iDia].Dia,
                        'Qtd': pEsquema[iEsquema].Insercoes[iInsercao].Digitacao[iDia].Qtd,
                        'Valor_Tabela': DoubleVal(pEsquema[iEsquema].Insercoes[iInsercao].Digitacao[iDia].Valor_Tabela),
                        'Desconto_Detalhado': DoubleVal(pEsquema[iEsquema].Insercoes[iInsercao].Digitacao[iDia].Desconto_Detalhado)
                    });
                }
                _TempInsercao[iInsercao].Digitacao = _TempDigitacao;
            }
            _tempEsquema.Insercoes = _TempInsercao;
            _Esquema.push(_tempEsquema);
        }


        var _data = {
            'Simulacao': _Simulacao,
            'DescontoDetalhado': _DescontoDetalhado,
            'Esquema': _Esquema
        }
        $http({
            method: 'POST',
            url: config.baseUrl + "API/SalvarSimulacao",
            data: _data,
            headers: { 'Content-Type': 'application/json' }
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };
    var _CalcularSimulacao= function (pSimulacao, pEsquema) {
        var deferred = $q.defer();
        //=====================Padroniza parametros
        var _Simulacao = {
            Id_Simulacao: pSimulacao.Id_Simulacao,
            Identificacao: pSimulacao.Identificacao,
            Inicio_Validade: StringToDate(pSimulacao.Inicio_Validade, "dd/mm/yyyy"),
            Termino_Validade: StringToDate(pSimulacao.Termino_Validade, "dd/mm/yyyy"),
            Cod_Empresa_Venda: pSimulacao.Cod_Empresa_Venda,
            Tabela_Preco: CompetenciaToInt(pSimulacao.Tabela_Preco),
            Valor_Tabela: DoubleVal(pSimulacao.Valor_Tabela),
            Valor_Negociado: DoubleVal(pSimulacao.Valor_Negociado),
            Valor_Informado: DoubleVal(pSimulacao.Valor_Informado),
            Desconto_Padrao: DoubleVal(pSimulacao.Desconto_Padrao),
            Desconto_Real: DoubleVal(pSimulacao.Desconto_Real),
            FixarValor: pSimulacao.FixarValor,
            FixarDesconto: pSimulacao.FixarDesconto,
        }

        var _DescontoDetalhado = []
        if (pSimulacao.DescontoDetalhado) {

            for (var i = 0; i < pSimulacao.DescontoDetalhado.length; i++) {
                _DescontoDetalhado.push({
                    'Id_Desconto_Detalhado': pSimulacao.DescontoDetalhado[i].Id_Desconto_Detalhado,
                    'TipoDesconto': pSimulacao.DescontoDetalhado[i].TipoDesconto,
                    'Chave': pSimulacao.DescontoDetalhado[i].Chave,
                    'Conteudo': pSimulacao.DescontoDetalhado[i].Conteudo,
                    'Desconto': DoubleVal(pSimulacao.DescontoDetalhado[i].Desconto)
                })
            }
        };
        var _Esquema = [];
        var _tempEsquema = {};
        var _TempInsercao = [];
        var _TempDigitacao = [];

        for (var iEsquema = 0; iEsquema < pEsquema.length; iEsquema++) {
            var _TempInsercao = [];
            _tempEsquema = {
                'Id_Esquema': pEsquema[iEsquema].Id_Esquema,
                'Competencia': pEsquema[iEsquema].Competencia,
                'Cod_Mercado': pEsquema[iEsquema].Cod_Mercado,
                'Cod_Empresa_Faturamento': pEsquema[iEsquema].Cod_Empresa_Faturamento,
                'Desconto_Padrao': DoubleVal(pEsquema[iEsquema].Desconto_Padrao),
                'Abrangencia': pEsquema[iEsquema].Abrangencia,
                'Valor_Negociado': DoubleVal(pEsquema[iEsquema].Valor_Negociado),
                'Valor_Tabela': DoubleVal(pEsquema[iEsquema].Valor_Tabela),
                'Participacao': DoubleVal(pEsquema[iEsquema].Participacao),
                'FixarDesconto': pEsquema[iEsquema].FixarDesconto,
                'FixarValor': pEsquema[iEsquema].FixarValor,
                'Veiculos': pEsquema[iEsquema].Veiculos
            };
            for (var iInsercao = 0; iInsercao < pEsquema[iEsquema].Insercoes.length; iInsercao++) {
                var _TempDigitacao = [];
                _TempInsercao.push({
                    'Id_Insercoes': pEsquema[iEsquema].Insercoes[iInsercao].Id_Insercoes,
                    'Cod_Programa': pEsquema[iEsquema].Insercoes[iInsercao].Cod_Programa,
                    'Cod_Caracteristica': pEsquema[iEsquema].Insercoes[iInsercao].Cod_Caracteristica,
                    'Cod_Comercial': pEsquema[iEsquema].Insercoes[iInsercao].Cod_Comercial,
                    'Cod_Tipo_Comercial': pEsquema[iEsquema].Insercoes[iInsercao].Cod_Tipo_Comercial,
                    'Titulo_Comercial': pEsquema[iEsquema].Insercoes[iInsercao].Titulo_Comercial,
                    'Duracao': pEsquema[iEsquema].Insercoes[iInsercao].Duracao,
                    'Valor_Tabela': DoubleVal(pEsquema[iEsquema].Insercoes[iInsercao].Valor_Tabela),
                    'Valor_Negociado': DoubleVal(pEsquema[iEsquema].Insercoes[iInsercao].Valor_Negociado),
                    'Desconto_Linha': DoubleVal(pEsquema[iEsquema].Insercoes[iInsercao].Desconto_Linha),
                });

                for (var iDia = 0; iDia < pEsquema[iEsquema].Insercoes[iInsercao].Digitacao.length; iDia++) {
                    _TempDigitacao.push({
                        'Dia': pEsquema[iEsquema].Insercoes[iInsercao].Digitacao[iDia].Dia,
                        'Qtd': pEsquema[iEsquema].Insercoes[iInsercao].Digitacao[iDia].Qtd,
                        'Valor_Tabela': DoubleVal(pEsquema[iEsquema].Insercoes[iInsercao].Digitacao[iDia].Valor_Tabela),
                        'Desconto_Detalhado': DoubleVal(pEsquema[iEsquema].Insercoes[iInsercao].Digitacao[iDia].Desconto_Detalhado)
                    });
                }
                _TempInsercao[iInsercao].Digitacao = _TempDigitacao;
            }
            _tempEsquema.Insercoes = _TempInsercao;
            _Esquema.push(_tempEsquema);
        }

        var _data = {
            'Simulacao': _Simulacao,
            'DescontoDetalhado': _DescontoDetalhado,
            'Esquema': _Esquema
        }
        $http({
            method: 'POST',
            url: config.baseUrl + "API/CalcularSimulacao",
            data: _data,
            headers: { 'Content-Type': 'application/json' }
        }
        ).then(function (response) {
            deferred.resolve(response);
        });
        return deferred.promise
    };


    return {
        GetSimulacao: _GetSimulacao,
        ListSimulacao: _ListSimulacao,
        GetOpcoesDesconto: _GetOpcoesDesconto,
        GetVeiculos: _GetVeiculos,
        GetProgramasGrade: _GetProgramasGrade,
        AdicionarInsercoes: _AdicionarInsercoes,
        CalcularSimulacao:_CalcularSimulacao,
        SalvarSimulacao: _SalvarSimulacao,
    };



});

