angular.module('App').controller('NegociacaoCadastroController', ['$scope', '$rootScope', '$location', 'httpService', '$location', '$routeParams', function ($scope, $rootScope, $location, httpService, $location, $routeParams) {

    //====================Inicializa scopes
    $scope.Forma_Pgto = [{ 'Id': 0, 'Descricao': 'Espécie' }, { 'Id': 1, 'Descricao': 'Permuta' }]
    $scope.Parameters = $routeParams;
    $scope.Negociacao = "";
    $scope.PeriodoInicioKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.PeriodoFimKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.TabelaPrecoKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.PesquisaTabelas = { "Items": [], 'FiltroTexto': '', ClickCallBack: '', 'Titulo': '', 'MultiSelect': false };
    $scope.Tipo_Intermediario = [{ 'Tipo_Intermediario': 'C', 'Nome_Tipo_Intermediario': 'Corretor' }, { 'Tipo_Intermediario': 'E', 'Nome_Tipo_Intermediario': 'Terceiro' }, { 'Tipo_Intermediario': 'F', 'Nome_Tipo_Intermediario': 'Afiliada' }, { 'Tipo_Intermediario': 'P', 'Nome_Tipo_Intermediario': 'Parceiro' }]
    $scope.Tipo_Comissao = [{ 'Tipo_Comissao': 'B', 'Nome_Tipo_Comissao': 'Bruto' }, { 'Tipo_Comissao': 'L', 'Nome_Tipo_Comissao': 'Liquido' }]
    $scope.ShowNewIntermediario = false;
    $scope.NewIntermediario = function () {
        return { 'Cod_Intermediario': '', 'Nome_Intermediario': '', 'Comissao': '', 'Tipo_Intermediario': {'Tipo_Intermediario':'','Nome_Tipo_Intermediario':''},  'Sequencia': '', 'Tipo_Comissao': {'Tipo_Comissao':'','Nome_Tipo_Comissao':''}}
    }
    $scope.Intermediario_Temp = $scope.NewIntermediario();

    //==========================Busca dados da Negociacao
    var _url = "Negociacao/Get";
    _url += "?Numero_Negociacao=" + $scope.Parameters.Id;
    _url += "&";
    httpService.Get(_url).then(function (response) {
        if (response) {
            $scope.Negociacao = response.data;
            if ($scope.Parameters.Action == 'New') {
                $scope.Negociacao.Sequencia_Tabela = "";
                $scope.Negociacao.Sequencia_Tabela_Reaplicacao = "";
            }
        }
    });
    //==========================Evento - Mudou a Competencia Inicio da Negociacao
    $scope.$watch('Negociacao.Competencia_Inicial', function (newValue, oldValue) {
        if (newValue != oldValue && newValue) {
            var _year = newValue.substr(3, 4);
            var_month = newValue.substr(0, 2);
            var _first = _year + var_month
            $scope.PeriodoFimKeys = { 'Year': parseInt(_year), 'First': parseInt(_first), 'Last': "" }
        }
    });
    //===============================Selecao de Empresas de Venda
    $scope.SelecionarEmpresaVenda = function () {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        var _url = 'ListarTabela/Empresa_Usuario'
        httpService.Get(_url).then(function (response) {
            $scope.PesquisaTabelas = {}
            if (response.data) {
                $scope.ListaEmpresaVenda = response.data;
                for (var i = 0; i < $scope.Negociacao.Empresas_Venda.length ; i++) {
                    for (var y = 0; y < $scope.ListaEmpresaVenda.length ; y++) {
                        if ($scope.Negociacao.Empresas_Venda[i].Cod_Empresa == $scope.ListaEmpresaVenda[y].Codigo) {
                            $scope.ListaEmpresaVenda[y].Selected = true;
                        }
                    };
                };
                $scope.PesquisaTabelas.Items = $scope.ListaEmpresaVenda;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção de Empresas";
                $scope.PesquisaTabelas.MultiSelect = true;
                $scope.PesquisaTabelas.ClickCallBack = function () {
                    $scope.Negociacao.Empresas_Venda = [];
                    for (var i = 0; i < $scope.ListaEmpresaVenda.length; i++) {
                        if ($scope.ListaEmpresaVenda[i].Selected) {
                            $scope.Negociacao.Empresas_Venda.push({ 'Cod_Empresa': $scope.ListaEmpresaVenda[i].Codigo, 'Nome_Empresa': $scope.ListaEmpresaVenda[i].Descricao });
                        }
                    };
                };
                $("#modalTabela").modal(true);
            };
        });
    }
    //=====================Remover Empresa De Venda
    $scope.RemoverEmpresaVenda = function (pCod_Empresa) {
        for (var i = 0; i < $scope.Negociacao.Empresas_Venda.length; i++) {
            if ($scope.Negociacao.Empresas_Venda[i].Cod_Empresa == pCod_Empresa) {
                $scope.Negociacao.Empresas_Venda.splice(i, 1);
                break;
            }
        }
    }
    //===============================Selecao de Empresas de Faturamento
    $scope.SelecionarEmpresaFaturamento = function () {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        var _url = 'ListarTabela/Empresa_Usuario'
        httpService.Get(_url).then(function (response) {
            $scope.PesquisaTabelas = {}
            if (response.data) {
                $scope.ListaEmpresaFaturamento = response.data;
                for (var i = 0; i < $scope.Negociacao.Empresas_Faturamento.length ; i++) {
                    for (var y = 0; y < $scope.ListaEmpresaFaturamento.length ; y++) {
                        if ($scope.Negociacao.Empresas_Faturamento[i].Cod_Empresa == $scope.ListaEmpresaFaturamento[y].Codigo) {
                            $scope.ListaEmpresaFaturamento[y].Selected = true;
                        }
                    };
                };
                $scope.PesquisaTabelas.Items = $scope.ListaEmpresaFaturamento;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção de Empresas";
                $scope.PesquisaTabelas.MultiSelect = true;
                $scope.PesquisaTabelas.ClickCallBack = function () {
                    $scope.Negociacao.Empresas_Faturamento = [];
                    for (var i = 0; i < $scope.ListaEmpresaFaturamento.length; i++) {
                        if ($scope.ListaEmpresaFaturamento[i].Selected) {
                            $scope.Negociacao.Empresas_Faturamento.push({ 'Cod_Empresa': $scope.ListaEmpresaFaturamento[i].Codigo, 'Nome_Empresa': $scope.ListaEmpresaFaturamento[i].Descricao });
                        }
                    };
                };
                $("#modalTabela").modal(true);
            };
        });
    };
    //=====================Remover Empresa De Faturamento
    $scope.RemoverEmpresaFaturamento = function (pCod_Empresa) {
        for (var i = 0; i < $scope.Negociacao.Empresas_Faturamento.length; i++) {
            if ($scope.Negociacao.Empresas_Faturamento[i].Cod_Empresa == pCod_Empresa) {
                $scope.Negociacao.Empresas_Faturamento.splice(i, 1);
                break;
            }
        }
    };
    //=====================Botão Adicionar Intermediario
    $scope.AdicionarIntermediarios = function () {
        $scope.Intermediario_Temp = $scope.NewIntermediario();
        $scope.ShowNewIntermediario = true;
    };
    $scope.ValidarIntermediario = function (pCodIntermediario) {
        for (var i = 0; i < $scope.Negociacao.Intermediarios.length; i++) {
            if (pCodIntermediario.trim() == $scope.Negociacao.Intermediarios[i].Cod_Intermediario.trim()) {
                ShowAlert("Intermediário já existe na Negociação", "warning")
                break;
            }
        }
        var _url = "ValidarTabela/Terceiro/" + pCodIntermediario.trim()
        httpService.Get(_url).then(function (response) {
            if (response.data[0].Status == 0) {
                $scope.Intermediario_Temp = $scope.NewIntermediario();
                ShowAlert(response.data[0].Mensagem, 'warning', 2000);
            }
            else {
                $scope.Intermediario_Temp.Nome_Intermediario = response.data[0].Descricao;
            }
        })
    };
    //=========================Cancela Inclusao de Intermediario
    $scope.CancelarIntermediario = function () {
        $scope.Intermediario_Temp = $scope.NewIntermediario();
        $scope.ShowNewIntermediario = false;
    };
    //=========================Salvar Inclusao de Intermediario
    $scope.SalvarIntermediario = function () {

        $scope.Negociacao.Intermediarios.push($scope.Intermediario_Temp);
        $scope.Intermediario_Temp = $scope.NewIntermediario();
        $scope.ShowNewIntermediario = false;
    };
    //===============Clicou na lupa da inclusao de intermediario
    $scope.PesquisaIntermediario = function () {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        $scope.PesquisaTabelas.Items = "";
        $scope.PesquisaTabelas.FiltroTexto = ""
        $scope.PesquisaTabelas.PreFilter = true;
        $scope.PesquisaTabelas.Titulo = "Seleção de Intermediários"
        $scope.PesquisaTabelas.MultiSelect = false;
        $scope.PesquisaTabelas.ClickCallBack = function (value) {
            $scope.Intermediario_Temp.Cod_Intermediario = value.Codigo, $scope.Intermediario_Temp.Nome_Intermediario = value.Descricao
        },
        $scope.PesquisaTabelas.LoadCallBack = function (pFilter) {
            httpService.Get('ListarTabela/Terceiro/' + pFilter.trim()).then(function (response) {
                $scope.PesquisaTabelas.Items = response.data
            });
        },
        $("#modalTabela").modal(true);
    };
    //===============Remover Intermediario
    $scope.RemoverIntermediario = function (pCod_Intermediario) {
        swal({
            title: "Tem certeza que deseja Excluir esse Intermedipario?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Não,Cancelar",
            closeOnConfirm: true
        }, function () {
            for (var i = 0; i < $scope.Negociacao.Intermediarios.length; i++) {
                if (pCod_Intermediario.trim() == $scope.Negociacao.Intermediarios[i].Cod_Intermediario.trim()) {
                    $scope.Negociacao.Intermediarios.splice(i, 1);
                    $scope.$digest();
                    break;
                }
            };
        });
    };
    //===============================Selecao de Apresentadores
    $scope.SelecionarApresentadores = function () {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        var _url = 'ListarTabela/Apresentador_Codigo'
        httpService.Get(_url).then(function (response) {
            $scope.PesquisaTabelas = {}
            if (response.data) {
                $scope.ListaApresentadores = response.data;
                for (var i = 0; i < $scope.Negociacao.Apresentadores.length ; i++) {
                    for (var y = 0; y < $scope.ListaApresentadores.length ; y++) {
                        if ($scope.Negociacao.Apresentadores[i].Cod_Apresentador.trim()== $scope.ListaApresentadores[y].Codigo.trim()) {
                            $scope.ListaApresentadores[y].Selected = true;
                        }
                    };
                };
                $scope.PesquisaTabelas.Items = $scope.ListaApresentadores;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção de Apresentadores";
                $scope.PesquisaTabelas.MultiSelect = true;
                $scope.PesquisaTabelas.ClickCallBack = function () {
                    $scope.Negociacao.Apresentadores = [];
                    for (var i = 0; i < $scope.ListaApresentadores.length; i++) {
                        if ($scope.ListaApresentadores[i].Selected) {
                            $scope.Negociacao.Apresentadores.push({ 'Cod_Apresentador': $scope.ListaApresentadores[i].Codigo, 'Nome_Apresentador': $scope.ListaApresentadores[i].Descricao });
                        }
                    };
                };
                $("#modalTabela").modal(true);
            };
        });
    };
    //=====================Remover Apresentadores
    $scope.RemoverApresentador = function (pCod_Apresentador) {
        for (var i = 0; i < $scope.Negociacao.Apresentadores.length; i++) {
            if ($scope.Negociacao.Apresentadores[i].Cod_Apresentador.trim() == pCod_Apresentador.trim()) {
                $scope.Negociacao.Apresentadores.splice(i, 1);
                break;
            }
        }
    };
}]);
