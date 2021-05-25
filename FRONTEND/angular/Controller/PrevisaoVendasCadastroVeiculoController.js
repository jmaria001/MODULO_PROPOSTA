angular.module('App').controller('PrevisaoVendasCadastroVeiculoController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //============== Inicializa Scopes
    $scope.ShowGrid = false;
    $scope.ShowHistorico = false;
    $scope.ShowAjuste = false;
    $scope.Previsao = [];
    $scope.ShowResult = false;
    $scope.Filtro = { 'Competencia': '', 'Cod_Contato': '', 'Nome_Contato': '' };
    $scope.Results = "[]";

    $scope.PeriodoInicioKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' };
    $scope.PeriodoFimKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' };

    $scope.NewAjuste = function () {
        return { 'Percentual_Vendas': '', 'Valor_Vendas': '', 'Competencia_Inicial': '', 'Competencia_Final': '', 'TipoAjuste': 1 };
    }
    $scope.Ajuste = $scope.NewAjuste();
    $scope.PosTipo = [
        { 'id': 1, 'nome': 'Aumentar' },
        { 'id': 2, 'nome': 'Diminuir' }
    ];


    $scope.TipoAumenta_Diminuir = [{ 'Codigo': 1, 'Nome': 'Aumenta' }, { 'Codigo': 2, 'Nome': 'Diminuir' }];

    //===================Carregar Previsao
    $scope.CarregarPrevisao = function (pFiltro) {

        if (pFiltro.Competencia == "" || pFiltro.Cod_Contato == "") {
            ShowAlert("Competência e Contato são filtros Obrigatórios.");
            return;
        }

        httpService.Post("CarregarPrevisaoConsisteCompetencia", pFiltro).then(function (responsecheck) {
            if (responsecheck.data) {
                httpService.Post("CarregarPrevisaoCadastroVeiculo", pFiltro).then(function (response) {
                    if (response.data) {
                        $scope.Previsao = response.data;
                        $scope.ShowGrid = true
                        $scope.Ajuste = $scope.NewAjuste();
                        $scope.ShowHistorico = false;
                        $scope.ShowAjuste = false;
                        $scope.AtualizaTotal($scope.Previsao);
                    };
                });
            }
            else {
                ShowAlert("Não Existe Previsão de Venda Mensal para essa ano.")
                $scope.Previsao = [];
                $scope.ShowGrid = false;
                $scope.ShowHistorico = false;
                $scope.ShowAjuste = false;
                $scope.AtualizaTotal($scope.Previsao);
            };
        });


    };
    //===================Carregar Historico
    $scope.CarregarHistorico = function (pPrevisao) {

        if ($scope.Filtro.Competencia == "" && $scope.Filtro.Cod_Contato == "") {
            ShowAlert("Competência e Contato são filtros Obrigatórios.");
            return;
        }
        httpService.Post("CarregarHistoricoVeiculo", pPrevisao).then(function (response) {
            if (response.data) {
                $scope.Previsao = response.data;
                $scope.AtualizaTotal($scope.Previsao);
            }
        });
    };
    //========================== Valor Change / Atualiza total da previsao
    $scope.AtualizaTotal = function (pPrevisao) {
        $scope.Total_Jan = 0;
        $scope.Total_Fev = 0;
        $scope.Total_Mar = 0;
        $scope.Total_Abr = 0;
        $scope.Total_Mai = 0;
        $scope.Total_Jun = 0;
        $scope.Total_Jul = 0;
        $scope.Total_Ago = 0;
        $scope.Total_Set = 0;
        $scope.Total_Out = 0;
        $scope.Total_Nov = 0;
        $scope.Total_Dez = 0;
        $scope.Total_Negociado = 0;
        $scope.Total_Previsao = 0;
        var _Total_Previsao = 0

        for (var i = 0; i < pPrevisao.length; i++) {
            if (pPrevisao[i].Tipo_Linha == 1) {
                _Total_Previsao = 0;
                $scope.Total_Jan += DoubleVal(pPrevisao[i].Valor_Jan);
                $scope.Total_Fev += DoubleVal(pPrevisao[i].Valor_Fev);
                $scope.Total_Mar += DoubleVal(pPrevisao[i].Valor_Mar);
                $scope.Total_Abr += DoubleVal(pPrevisao[i].Valor_Abr);
                $scope.Total_Mai += DoubleVal(pPrevisao[i].Valor_Mai);
                $scope.Total_Jun += DoubleVal(pPrevisao[i].Valor_Jun);
                $scope.Total_Jul += DoubleVal(pPrevisao[i].Valor_Jul);
                $scope.Total_Ago += DoubleVal(pPrevisao[i].Valor_Ago);
                $scope.Total_Set += DoubleVal(pPrevisao[i].Valor_Set);
                $scope.Total_Out += DoubleVal(pPrevisao[i].Valor_Out);
                $scope.Total_Nov += DoubleVal(pPrevisao[i].Valor_Nov);
                $scope.Total_Dez += DoubleVal(pPrevisao[i].Valor_Dez);
                $scope.Total_Negociado += DoubleVal(pPrevisao[i].Valor_Negociado);
                //$scope.Total_Previsao += DoubleVal(pPrevisao[i].Valor_Total);

                _Total_Previsao = _Total_Previsao + DoubleVal(pPrevisao[i].Valor_Jan) +
                    DoubleVal(pPrevisao[i].Valor_Fev) +
                    DoubleVal(pPrevisao[i].Valor_Mar) +
                    DoubleVal(pPrevisao[i].Valor_Abr) +
                    DoubleVal(pPrevisao[i].Valor_Mai) +
                    DoubleVal(pPrevisao[i].Valor_Jun) +
                    DoubleVal(pPrevisao[i].Valor_Jul) +
                    DoubleVal(pPrevisao[i].Valor_Ago) +
                    DoubleVal(pPrevisao[i].Valor_Set) +
                    DoubleVal(pPrevisao[i].Valor_Out) +
                    DoubleVal(pPrevisao[i].Valor_Nov) +
                    DoubleVal(pPrevisao[i].Valor_Dez);
                pPrevisao[i].Valor_Total = MoneyFormat(_Total_Previsao,true);
                $scope.Total_Previsao += DoubleVal(pPrevisao[i].Valor_Total);
            };
        };
    };
    
    //==========================Ajustar a Previsao
    $scope.AjustarPrevisao = function (pPrevisao, pAjuste) {
        if (!pAjuste.TipoAjuste) {
            return;
        }
        var _valido = false;
        var _valor_Previsao = 0;
        var _valor_Ajuste = 0;
        var _novo_Valor = 0
        var _pct = 0;
        var _anomes = 0;
        for (var i = 0; i < pPrevisao.length; i++) {
            if (pPrevisao[i].Cod_Veiculo) {
                for (var x = 1; x < 13; x++) {
                    _valor_Previsao = 0;
                    _valor_Ajuste = 0;
                    _novo_Valor = 0
                    _pct = 0;
                    _anomes = 0;

                    _anomes = pPrevisao[i].Ano.toString() + ("0000" + x.toString()).slice(-2);
                    _valido = false;
                    if (!pAjuste.Competencia_Inicial && !pAjuste.Competencia_Final) {
                        _valido = true;
                    }
                    if (_anomes >= CompetenciaToInt(pAjuste.Competencia_Inicial) && _anomes <= CompetenciaToInt(pAjuste.Competencia_Final)) {
                        _valido = true;
                    }
                    if (_valido) {
                        //console.log(_anomes);
                        if (x == 1) { _valor_Previsao = DoubleVal(pPrevisao[i].Valor_Jan); };
                        if (x == 2) { _valor_Previsao = DoubleVal(pPrevisao[i].Valor_Fev); };
                        if (x == 3) { _valor_Previsao = DoubleVal(pPrevisao[i].Valor_Mar); };
                        if (x == 4) { _valor_Previsao = DoubleVal(pPrevisao[i].Valor_Abr); };
                        if (x == 5) { _valor_Previsao = DoubleVal(pPrevisao[i].Valor_Mai); };
                        if (x == 6) { _valor_Previsao = DoubleVal(pPrevisao[i].Valor_Jun); };
                        if (x == 7) { _valor_Previsao = DoubleVal(pPrevisao[i].Valor_Jul); };
                        if (x == 8) { _valor_Previsao = DoubleVal(pPrevisao[i].Valor_Ago); };
                        if (x == 9) { _valor_Previsao = DoubleVal(pPrevisao[i].Valor_Set); };
                        if (x == 10) { _valor_Previsao = DoubleVal(pPrevisao[i].Valor_Out); };
                        if (x == 11) { _valor_Previsao = DoubleVal(pPrevisao[i].Valor_Nov); };
                        if (x == 12) { _valor_Previsao = DoubleVal(pPrevisao[i].Valor_Dez); };

                        _valor_Ajuste = DoubleVal(pAjuste.Valor_Vendas);
                        _pct = DoubleVal(pAjuste.Percentual_Vendas);

                        if (_pct) {
                            _valor_Ajuste = _valor_Previsao * (_pct / 100);
                        }

                        if (_valor_Ajuste && pPrevisao[i].Tipo_Linha == 1) {
                            if (pAjuste.TipoAjuste == 2) {
                                _valor_Ajuste = _valor_Ajuste * -1;
                            }
                            _novo_Valor = _valor_Previsao + _valor_Ajuste;
                        };
                        if (x == 1) { pPrevisao[i].Valor_Jan = MoneyFormat(_novo_Valor,true) };
                        if (x == 2) { pPrevisao[i].Valor_Fev = MoneyFormat(_novo_Valor, true) };
                        if (x == 3) { pPrevisao[i].Valor_Mar = MoneyFormat(_novo_Valor, true) };
                        if (x == 4) { pPrevisao[i].Valor_Abr = MoneyFormat(_novo_Valor, true) };
                        if (x == 5) { pPrevisao[i].Valor_Mai = MoneyFormat(_novo_Valor, true) };
                        if (x == 6) { pPrevisao[i].Valor_Jun = MoneyFormat(_novo_Valor, true) };
                        if (x == 7) { pPrevisao[i].Valor_Jul = MoneyFormat(_novo_Valor, true) };
                        if (x == 8) { pPrevisao[i].Valor_Ago = MoneyFormat(_novo_Valor, true) };
                        if (x == 9) { pPrevisao[i].Valor_Set = MoneyFormat(_novo_Valor, true) };
                        if (x == 10) { pPrevisao[i].Valor_Out = MoneyFormat(_novo_Valor, true) };
                        if (x == 11) { pPrevisao[i].Valor_Nov = MoneyFormat(_novo_Valor, true) };
                        if (x == 12) { pPrevisao[i].Valor_Dez = MoneyFormat(_novo_Valor, true) };
                    };
                };
            }
        };
        $scope.AtualizaTotal(pPrevisao);
    };
    //======================Copiar Valores
    $scope.CopiarValores = function (pPrevisao, pTipo) {
        if (pTipo == 1) {
            for (var i = 0; i < pPrevisao.length; i++) {
                if (pPrevisao[i].Valor_Negociado) {
                    pPrevisao[i].Valor_Previsao = pPrevisao[i].Valor_Negociado;
                };
            };
        };
        if (pTipo == 2) {
            pPrevisao.Valor_Previsao = pPrevisao.Valor_Negociado;
        };
        $scope.AtualizaTotal($scope.Previsao)
    };
           
    $scope.PrevisaoExcluirVeiculo = function (pPrevisao) {

        swal({
            title: "Tem certeza que deseja Excluir essa Previsão Veiculo?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim,Excluir",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post('PrevisaoExcluirVeiculo', pPrevisao).then(function (response) {
                if (response) {
                    var _index = -1;
                    if (response.data[0].Status) {
                        for (var i = 0; i < $scope.Previsao.length; i++) {
                            if ($scope.Previsao[i].Cod_Veiculo.trim() == pPrevisao.Cod_Veiculo.trim() ) {
                                _index = i;
                                break;
                            }
                        }
                        if (_index > -1) {
                            $scope.Previsao.splice(_index, 1);
                            $scope.AtualizaTotal($scope.Previsao);
                        };

                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    };
                }
            });
        });
    };
    //==========================Salvar
    $scope.SalvarPrevisaoVendasVeiculo = function (pPrevisao, ) {
        
        httpService.Post("SalvarPrevisaoVendasVeiculo", pPrevisao).then(function (response) {
            if (response) {
                if (response.data) {
                    $scope.Results = response.data;
                    $scope.ShowResult = true;
                }
            }
        })
    };
    //=========Adicionar nova linha
    $scope.AdicionarLinha = function (pFiltro, pPrevisao) {
        console.log(pPrevisao);
        httpService.Post('PrevisaVendaNewVeiculo', pFiltro).then(function (response) {
            $scope.Previsao.unshift(response.data);
            if (isNaN(pPrevisao[1].Valor_Jan)) {

                $scope.pPrevisao[1].Valor_Jan = MoneyFormat(pPrevisao[1].Valor_Jan,true);

            }
            else {

                $scope.Previsao[1].Valor_Jan = MoneyFormat(pPrevisao[1].Valor_Jan,true);

            }

            if (isNaN(pPrevisao[1].Valor_Fev)) {

                $scope.pPrevisao[1].Valor_Fev = MoneyFormat(pPrevisao[1].Valor_Fev,true);

            }
            else {

                $scope.Previsao[1].Valor_Fev = MoneyFormat(pPrevisao[1].Valor_Fev, true);

            }

            if (isNaN(pPrevisao[1].Valor_Mar)) {

                $scope.pPrevisao[1].Valor_Mar = MoneyFormat(pPrevisao[1].Valor_Mar, true);

            }
            else {

                $scope.Previsao[1].Valor_Mar = MoneyFormat(pPrevisao[1].Valor_Mar, true);
                console.log($scope.Previsao[1].Valor_Mar);
            }

            if (isNaN(pPrevisao[1].Valor_Abr)) {

                $scope.pPrevisao[1].Valor_Abr = MoneyFormat(pPrevisao[1].Valor_Abr, true);

            }
            else {

                $scope.Previsao[1].Valor_Abr = MoneyFormat(pPrevisao[1].Valor_Abr, true);
                console.log($scope.Previsao[1].Valor_Abr);
            }

            if (isNaN(pPrevisao[1].Valor_Mai)) {

                $scope.pPrevisao[1].Valor_Mai = MoneyFormat(pPrevisao[1].Valor_Mai, true);

            }
            else {

                $scope.Previsao[1].Valor_Mai = MoneyFormat(pPrevisao[1].Valor_Mai, true);
                console.log($scope.Previsao[1].Valor_Mai);
            }

            if (isNaN(pPrevisao[1].Valor_Jun)) {

                $scope.pPrevisao[1].Valor_Jun = MoneyFormat(pPrevisao[1].Valor_Jun, true);

            }
            else {

                $scope.Previsao[1].Valor_Jun = MoneyFormat(pPrevisao[1].Valor_Jun, true);
                
            }


            if (isNaN(pPrevisao[1].Valor_Jul)) {

                $scope.pPrevisao[1].Valor_Jul = MoneyFormat(pPrevisao[1].Valor_Jul, true);

            }
            else {

                $scope.Previsao[1].Valor_Jul = MoneyFormat(pPrevisao[1].Valor_Jul, true);
            }

            if (isNaN(pPrevisao[1].Valor_Ago)) {

                $scope.pPrevisao[1].Valor_Ago = MoneyFormat(pPrevisao[1].Valor_Ago, true);

            }
            else {

                $scope.Previsao[1].Valor_Ago = MoneyFormat(pPrevisao[1].Valor_Ago, true);
                
            }

            if (isNaN(pPrevisao[1].Valor_Set)) {

                $scope.pPrevisao[1].Valor_Set = MoneyFormat(pPrevisao[1].Valor_Set);

            }
            else {

                $scope.Previsao[1].Valor_Set = MoneyFormat(pPrevisao[1].Valor_Set);

            }

            if (isNaN(pPrevisao[1].Valor_Out)) {

                $scope.pPrevisao[1].Valor_Out = MoneyFormat(pPrevisao[1].Valor_Out, true);

            }
            else {

                $scope.Previsao[1].Valor_Out = MoneyFormat(pPrevisao[1].Valor_Out, true);

            }

            if (isNaN(pPrevisao[1].Valor_Nov)) {

                $scope.pPrevisao[1].Valor_Nov = MoneyFormat(pPrevisao[1].Valor_Nov, true);

            }
            else {

                $scope.Previsao[1].Valor_Nov = MoneyFormat(pPrevisao[1].Valor_Nov, true);

            }

            if (isNaN(pPrevisao[1].Valor_Dez)) {

                $scope.pPrevisao[1].Valor_Dez = MoneyFormat(pPrevisao[1].Valor_Dez, true);

            }
            else {

                $scope.Previsao[1].Valor_Dez = MoneyFormat(pPrevisao[1].Valor_Dez, true);

            }
        });
    };
    //===============Selecionar Veiculo
    $scope.SelecionaVeiculo = function (pPrevisao) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        $scope.PesquisaTabelas.Items = [];
        $scope.PesquisaTabelas.PreFiltroTexto = "";
        $scope.PesquisaTabelas.PreFilter = false;
        $scope.PesquisaTabelas.Titulo = "Seleção de Veículo";
        $scope.PesquisaTabelas.MultiSelect = false;
        httpService.Get('ListarTabela/Veiculo').then(function (response) {
            $scope.PesquisaTabelas.Items = response.data;
        });
        $scope.PesquisaTabelas.ClickCallBack = function (value) { pPrevisao.Cod_Veiculo = value.Codigo, pPrevisao.Nome_Veiculo = value.Descricao };
        //$scope.PesquisaTabelas.LoadCallBack = function (pFilter) {
        //    httpService.Get('ListarTabela/Veiculo/' + pFilter).then(function (response) {
        //        $scope.PesquisaTabelas.Items = response.data;
        //    });
        //}
        $("#modalTabela").modal(true);
    };
 
}]);

