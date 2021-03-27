angular.module('App').controller('MateriaisFitasCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Recebe Parametro
    $scope.PesquisaTabelas = { "Items": [], 'FiltroTexto': '', ClickCallBack: '', 'Titulo': '', 'MultiSelect': false };
    $scope.Parameters = $routeParams;
    $scope.ListadeVeiculos = [];
    $scope.MateriaisFitas = "";
    $scope.DepositorioFitas = "";
    $scope.DepositorioFitas.Veiculos = [];
    $scope.currentVeiculos = 0;
    //========================Verifica Permissoes
    $scope.PermissaoDelete = false;

    httpService.Get("credential/MateriaisFita@Destroy").then(function (response) {
        $scope.PermissaoDelete = response.data;
    });

    //==========================Busca dados dos Materiais Fitas

    var _url = "GetMateriaisFitasData/" + $scope.Parameters.Id;
    httpService.Get(_url).then(function (response) {
        if (response) {
            $scope.MateriaisFitas = response.data;
            if ($scope.Parameters.Action == "New") {
                $scope.MateriaisFitas.Cod_Tipo_Comercial = "";
                $scope.MateriaisFitas.Duracao = "";
                $scope.MateriaisFitas.Cod_Red_Produto = "";

            }
        }
    });
    //===============Clicou na lupa Produto 
    $scope.PesquisaProduto = function (pMaterial) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        $scope.PesquisaTabelas.Items = [];
        $scope.PesquisaTabelas.PreFiltroTexto = "";
        $scope.PesquisaTabelas.PreFilter = true;
        $scope.PesquisaTabelas.Titulo = "Seleção de Produtos";
        $scope.PesquisaTabelas.MultiSelect = false;
        if (pMaterial.Cod_Cliente) {
            $scope.PesquisaTabelas.ButtonText = "Mostrar Produtos do Cliente " + pMaterial.Cod_Cliente
            $scope.PesquisaTabelas.ButtonCallBack = function () {
                httpService.Get('MapaReserva/ListarProdutoCliente/' + pMaterial.Cod_Cliente).then(function (response) {
                    $scope.PesquisaTabelas.Items = response.data;
                });
            };
        };
        $scope.PesquisaTabelas.ClickCallBack = function (value) { pMaterial.Cod_Red_Produto = value.Codigo; pMaterial.Descricao_Produto = value.Descricao };
        $scope.PesquisaTabelas.LoadCallBack = function (pFilter) {
            httpService.Get('ListarTabela/Produto/' + pFilter).then(function (response) {
                $scope.PesquisaTabelas.Items = response.data;
            });
        }

        $("#modalTabela").modal(true);
    };
    ////==========================Validar o Produto
    $scope.ProdutoChange = function (pMaterial) {
        if (!pMaterial.Cod_Red_Produto) {
            pMaterial.Descricao_Produto = "";
            return;
        };
        httpService.Get("ValidarTabela/Produto/" + pMaterial.Cod_Red_Produto).then(function (response) {
            if (!response.data[0].Status) {
                ShowAlert("Produto Inválido");
                pMaterial.Cod_Red_Produto = "";
                pMaterial.Descricao_Produto = "";
            }
            else {
                pMaterial.Descricao_Produto = response.data[0].Descricao;
            }
        });
    };
    ////==========================Salvar
    $scope.SalvarMateriaisFitas = function (pMateriaisFitas) {
        $scope.MateriaisFitas.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        
        httpService.Post("SalvarMateriaisFitas", pMateriaisFitas).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/MateriaisFitas");
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };


    //======================Excluir
    $scope.ExcluirMateriaisFitas = function (pMateriaisFitas) {

        swal({
            title: "Tem certeza que deseja Excluir esta  Fita ?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {


            pMateriaisFitas.Tipo_Fita = 'CO';


                httpService.Post("ExcluirMateriaisFitas", pMateriaisFitas).then(function (response) {
                if (response) {

                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/MateriaisFitas");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });

    };


    //=====================Carregar Numero de Fita 
    $scope.CarregarNumeroFita = function (pNumero_Fita) {
        var n = 0;
        var tipo_fita_n = 0;
        var Tipo_Fita;
        Tipo_Fita='CO'

        if (pNumero_Fita.Cod_Veiculo == null && pNumero_Fita.Cod_Veiculo == undefined) {

            ShowAlert("Para utilizar numeração automática, primeiro selecione um veiculo");
            return;
        }

        var _data = {
            //'Cod_Veiculo': $scope.DepositorioFitas.Veiculos[0].Cod_Veiculo,
            'Cod_Veiculo': pNumero_Fita.Cod_Veiculo,
            'Tipo_Fita': Tipo_Fita,
            'Tipo_Midia': pNumero_Fita.Tipo_Midia,
            'Cod_Tipo_Comercial': pNumero_Fita.Cod_Tipo_Comercial
        };

        httpService.Post("RangeFitaMateriais", _data).then(function (response) {
            if (response) {

                if (response.data[0].Status == 0) {

                    ShowAlert('Veículo não esta parametrizado corretamente em Paramêtros de Numeração de Fitas');
                    //Atribuir dado ao campo numeracao de fita 
                }
                else {
                    
                    $scope.MateriaisFitas.Numero_Fita = response.data[0].Numero_Fita;

                }
            }
        })
    }




}]);

