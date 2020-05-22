angular.module('App').controller('ProdutoCadastroController', ['$scope', '$rootScope', '$location', 'httpService', '$location', '$routeParams', function ($scope, $rootScope, $location, httpService, $location, $routeParams) {

    //====================Inicializa scopes
    $scope.Parameters = $routeParams;
    $scope.PesquisaTabelas = { "Items": [], 'FiltroTexto': '', ClickCallBack: '', 'Titulo': '', 'MultiSelect': false };
    $scope.ListaProdutos = {};
    $scope.Produto = "";
    $scope.EditSegmento = false;
    $scope.EditSetor = false;
    $scope.EditProduto = false;
    $scope.SelectSegmento = false;
    $scope.SelectSetor = false;
    //========================Verifica Permissoes
    $scope.PermissaoDelete= false;
    httpService.Get("credential/Produto@Destroy").then(function (response) {
        $scope.PermissaoDelete = response.data;
    });

    //==========================Busca dados do Produto
    var _url = "GetProdutoData/" + $scope.Parameters.Id;;
    httpService.Get(_url).then(function (response) {
        if (response) {
            $scope.Produto = response.data;
            $scope.Produto.Operacao = $scope.Parameters.Action;
        }
    });
    //===========================Controle dos campos enabled disabel
    if ($scope.Parameters.Action=='EditSegmento') {
        $scope.EditSegmento = true;
        $scope.EditSetor = false;
        $scope.EditProduto = false;
        $scope.SelectSegmento = false;
        $scope.SelectSetor = false;
    }
    if ($scope.Parameters.Action == 'EditSetor') {
        $scope.EditSegmento = false;
        $scope.EditSetor = true;
        $scope.EditProduto = false;
        $scope.SelectSegmento = true;
        $scope.SelectSetor = false;
    }
    if ($scope.Parameters.Action == 'EditProduto') {
        $scope.EditSegmento = false;
        $scope.EditSetor = false;
        $scope.EditProduto = true;
        $scope.SelectSegmento = true;
        $scope.SelectSetor = true;
    }
    if ($scope.Parameters.Action == 'New') {
        $scope.EditSegmento = true;
        $scope.EditSetor = true;
        $scope.EditProduto = true;
        $scope.SelectSegmento = true;
        $scope.SelectSetor = true;
    }

    //===========================Pesquisa Segmento
    $scope.PesquisaSegmento = function () {
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
                    if ($scope.Parameters.Action!='EditSetor') {
                        $scope.Produto.Cod_Setor = null;
                        $scope.Produto.Setor= "";
                    }
                };
                $("#modalTabela").modal(true);
            };
        });
    };
    //===========================Pesquisa Setor
    $scope.PesquisaSetor = function () {
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
    $scope.SalvarProduto = function (pProduto) {
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
    $scope.ExcluirProduto = function (pOpcao, pProduto) {
        var _text = "";
        switch (pOpcao) {
            case 'Segmento':
                _text = 'Essa Operação excluirá todos os setores e produtos vinculados.'
                break;
            case 'Setor':
                _text = 'Essa Operação excluirá todos os produtos vinculados.'
                break;
            case 'Produto':
                _text = 'Essa Operação excluirá todos a composição do Produto nos Clientes.';
                break;
            default:
                break;

        }
        swal({
            title: "Tem certeza que deseja excluir esse " + pOpcao + " ?",
            text: _text,
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Não,Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("ExcluirProduto", pProduto).then(function (response) {
                if (response) {
                    if (response.data[0].Status== 1) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/Produto");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });
    };
}]);
