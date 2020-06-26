angular.module('App').controller('ProdutoCadastroController', ['$scope', '$rootScope', '$location', 'httpService', '$location', '$routeParams', function ($scope, $rootScope, $location, httpService, $location, $routeParams) {

    //====================Inicializa scopes
    $scope.Parameters = $routeParams;
    $scope.PesquisaTabelas = { "Items": [], 'FiltroTexto': '', ClickCallBack: '', 'Titulo': '', 'MultiSelect': false };
    $scope.ListaProdutos = { "Items": [], 'FiltroTexto': '', ClickCallBack: '', 'Titulo': '', 'MultiSelect': false };
    $scope.Produto = "";
    $scope.Operacoes = [{ 'Id': 1, 'Descricao': 'Novo Segmento', 'Action': 'New', 'value': false },
        { 'Id': 2, 'Descricao': 'Novo Setor', 'Action': 'New', 'value': false },
        { 'Id': 3, 'Descricao': 'Novo Produto', 'Action': 'New', 'value': false },
        { 'Id': 4, 'Descricao': 'Edição do Segmento', 'Action': 'Edit', 'value': false },
        { 'Id': 5, 'Descricao': 'Edição do Setor', 'Action': 'Edit', 'value': false },
        { 'Id': 6, 'Descricao': 'Edição do Produto', 'Action': 'Edit', 'value': false }];

    $scope.EditSegmento = false;
    $scope.EditSetor = false;
    $scope.EditProduto = false;
    $scope.SelectSegmento = false;
    $scope.SelectSetor = false;

    //===============================Controle dos Campos Enabled/Disable/Show
    $scope.ControleCampos = function () {
        $scope.EditSegmento = $scope.Operacoes[0].value || $scope.Operacoes[3].value;
        $scope.EditSetor = $scope.Operacoes[1].value || $scope.Operacoes[4].value;
        $scope.EditProduto = $scope.Operacoes[2].value || $scope.Operacoes[5].value;
        $scope.SelectSegmento = $scope.Operacoes[1].value || $scope.Operacoes[2].value;
        $scope.SelectSetor = $scope.Operacoes[2].value;
    };
    //==========================Busca dados do Produto
    var _url = "GetProdutoData/" + $scope.Parameters.Id;;
    httpService.Get(_url).then(function (response) {
        if (response) {
            $scope.Produto = response.data;
            if ($scope.Parameters.Action=='New') {
                $scope.Operacoes[2].value = true
                $scope.Produto.Operacao = $scope.Operacoes[2].Id;
            }
            else {
                $scope.Operacoes[5].value = true
                $scope.Produto.Operacao = $scope.Operacoes[5].Id;
            }
            $scope.ControleCampos();
        }
    });
    
    //===============================Seta Operacao 
    $scope.SetaOperacao = function (pOperacao) {
        if (pOperacao.value) {
            $scope.Produto.Operacao = pOperacao.Id;
            for (var i = 0; i < $scope.Operacoes.length; i++) {
                if ($scope.Operacoes[i].Id != pOperacao.Id) {
                    $scope.Operacoes[i].value = false;
                };
            };
        };
        $scope.ControleCampos();
    };
    //===========================Pesquisa Segmento
    $scope.PesquisaSegmento = function () {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        var _url = 'ListarTabela/Segmento';
        httpService.Get(_url).then(function (response) {
            $scope.PesquisaTabelas = {}
            if (response.data) {
                $scope.ListaProdutos = response.data;
                $scope.PesquisaTabelas.Items = $scope.ListaProdutos;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção de Segmentos de Produto";
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) {
                    $scope.Produto.Cod_Segmento = value.Codigo;
                    $scope.Produto.Segmento = value.Descricao;
                };
                $("#modalTabela").modal(true);
            };
        });
    };
    //===========================Pesquisa Setor
    $scope.PesquisaSetor = function () {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        var _url = 'SetorListar/' + $scope.Produto.Cod_Segmento;
        httpService.Get(_url).then(function (response) {
            $scope.PesquisaTabelas = {}
            if (response.data) {
                $scope.ListaProdutos = response.data;
                $scope.PesquisaTabelas.Items = $scope.ListaProdutos;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção de Setores de Produto";
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) {
                    $scope.Produto.Cod_Setor = value.Codigo;
                    $scope.Produto.Setor = value.Descricao;
                };
                $("#modalTabela").modal(true);
            };
        });
    };
    //===========================Salvar Produto
    $scope.SalvarProduto= function (pProduto) {
        httpService.Post("SalvarProduto", pProduto).then(function (response) {
            if (response) {
                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/Produto");
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
                
            }
        })
    };
}]);
