angular.module('App').controller('PrevisaoVendasCadastroMensalController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //============== Inicializa Scopes
    $scope.ShowGrid = false;
    $scope.ShowHistorico = false;
    $scope.ShowAjuste= false;
    $scope.Previsao = [];
    $scope.Filtro = { 'Competencia': '', 'Cod_Contato': '', 'Nome_Contato': '' };
    
    $scope.PeriodoInicioKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' };
    $scope.PeriodoFimKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' };

    $scope.NewAjuste = function () {
        return  { 'Percentual_Vendas': '', 'Valor_Vendas': '', 'Competencia_Inicial': '', 'Competencia_Final': '', 'TipoAjuste': 1 };
    }
    $scope.Ajuste = $scope.NewAjuste();
    $scope.PosTipo = [
        { 'id': 1, 'nome': 'Aumentar' },
        { 'id': 2, 'nome': 'Diminuir' }
    ];


    $scope.TipoAumenta_Diminuir = [{ 'Codigo': 1, 'Nome': 'Aumenta' }, { 'Codigo': 2, 'Nome': 'Diminuir' }];

    //===================Carregar Previsao
    $scope.CarregarPrevisao = function (pFiltro) {

        if (pFiltro.Competencia=="" || pFiltro.Cod_Contato=="") {
            ShowAlert("Competência e Contato são filtros Obrigatórios.");
            return;
        }

        httpService.Post("CarregarPrevisaoCadastroMensal", pFiltro).then(function (response) {
            if (response.data) {
                $scope.Previsao = response.data;
                $scope.ShowGrid = true
                $scope.Ajuste = $scope.NewAjuste();
                $scope.ShowHistorico = false;
                $scope.ShowAjuste = false;

            }
        });
    };
    //===================Carregar Previsao
    $scope.CarregarHistorico= function (pPrevisao) {

        if ($scope.Filtro.Competencia == "" && $scope.Filtro.Cod_Contato == "") {
            ShowAlert("Competência e Contato são filtros Obrigatórios.");
            return;
        }
        httpService.Post("CarregarHistoricoMensal", pPrevisao).then(function (response) {
            if (response.data) {
                $scope.Previsao = response.data;
            }
        });
    };
    //========================== Valor Change / Atualiza total da previsao
    $scope.AtualizaTotal = function (pPrevisao) {
        var _total = 0;
        var _valor_mes = 0;
        var _index = 0;

        for (var i = 0; i < pPrevisao.length; i++) {
            if (pPrevisao[i].Tipo_Linha==1) {
                _valor_mes = DoubleVal(pPrevisao[i].Valor_Previsao);
                _total += _valor_mes;
            }
            else {
                _index = i;
            }
            
        };
        pPrevisao[_index].Valor_Previsao = MoneyFormat( _total,true);
    };

    //==========================Salvar
    $scope.SalvarPrevisaoVendsaMensal = function (pPrevisao,) {

        httpService.Post("SalvarPrevisaoVendasMensal", pPrevisao).then(function (response) {
            if (response) {
                if (response.data) {
                    ShowAlert("Dados Gravados com Sucesso.")
                }
            }
        })
    };


    //==========================Ajustar a Previsao
    $scope.AjustarPrevisao = function (pPrevisao, pAjuste) {

        if (pAjuste.Competencia_Final < pAjuste.Competencia_Inicial)
        {
            ShowAlert("Competência final esta menor que a competencia inicial.")
            return;
        }



        if (!pAjuste.TipoAjuste) {
            return;
        }
        var _valido = false;
        var _valor_Previsao = 0;
        var _valor_Ajuste= 0;
        var _novo_Valor = 0 
        var _pct = 0;
        for (var i = 0; i < pPrevisao.length; i++) {
            _valido = false;
            if (!pAjuste.Competencia_Inicial && !pAjuste.Competencia_Final) {
                _valido = true;
            }
            if (pPrevisao[i].Competencia >= CompetenciaToInt(pAjuste.Competencia_Inicial) && pPrevisao[i].Competencia <= CompetenciaToInt(pAjuste.Competencia_Final)) {
                _valido = true;
            }
            if (_valido) {
                _valor_Previsao = DoubleVal(pPrevisao[i].Valor_Previsao);
                _valor_Ajuste = DoubleVal(pAjuste.Valor_Vendas);
                _pct = DoubleVal(pAjuste.Percentual_Vendas);

                if (_pct) {
                    _valor_Ajuste = _valor_Previsao * (_pct / 100);
                }
                
                if (_valor_Ajuste && pPrevisao[i].Tipo_Linha == 1) {
                    if (pAjuste.TipoAjuste==2) {
                        _valor_Ajuste = _valor_Ajuste * -1;
                    }
                    
                    _novo_Valor = _valor_Previsao + _valor_Ajuste;
                   
                }

                pPrevisao[i].Valor_Previsao = MoneyFormat(_novo_Valor,true)
               
            };
        };
        $scope.AtualizaTotal(pPrevisao);
    };



    //======================Copiar Valores
    $scope.CopiarValores = function (pPrevisao, pTipo) {
        console.log(pPrevisao);
        if (pTipo==1) {
            for (var i = 0; i < pPrevisao.length; i++) {
                if (pPrevisao[i].Valor_Negociado) {
                    pPrevisao[i].Valor_Previsao = pPrevisao[i].Valor_Negociado;
                };
            };
        };
        if (pTipo==2) {
            pPrevisao.Valor_Previsao = pPrevisao.Valor_Negociado;
        };
        $scope.AtualizaTotal($scope.Previsao)
    };
       
}]);

