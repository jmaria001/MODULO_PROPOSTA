angular.module('App').controller('DeterminacaoTituloController', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location) {

    //============================Inicializar Scopes
    $scope.NewFilter = function(){
        return {'Cod_Empresa':'','Numero_Mr':'','Sequencia_Mr':''}
    }
    $scope.Filtro = $scope.NewFilter();
    $scope.NewComercial = function () {
        return {
            'Cod_Empresa': '',
            'Numero_Mr': '',
            'Sequencia_Mr': '',
            'Cod_Comercial': '',
            'Titulo_Comercial': '',
            'Duracao': '',
            'Cod_Tipo_Comercial': '',
            'Nome_Tipo_Comercial': '',
            'Cod_Red_Produto': '',
            'Nome_Produto': '',
            'Observacoes': '',
            'Indica_Titulo_Determinar':false
        };
    };
    $scope.Comercial = $scope.NewComercial();
    $scope.Determinacao = "";
    $scope.AnaliseRotate= "";
    $scope.ShowDados = true;
    $scope.ShowComercial = false;
    $scope.ShowAnalise= false;
    $scope.CountDe = 0;
    $scope.CountPara = 0;
    $scope.Competencia_Text = ""
    $scope.RodizioConfirmado = false;
    //============================Carregar Comerciais
    $scope.CarregarComerciais = function (pFiltro) {
        httpService.Post("Determinacao/CarregarDados", pFiltro).then(function (response) {
            if (response.data) {
                if (!response.data.Id_Contrato) {
                    ShowAlert("Não existem dados para esse filtro")
                }
                $scope.Determinacao = response.data;
                $scope.ShowDados = true;
                $scope.CountDe = 0;
                $scope.CountPara = 0;
                $scope.Competencia_Text = MesExtenso($scope.Determinacao.Competencia)
            };
        });
    };
    //===============Selecao de Veiculos
    $scope.SelecionarVeiculos = function (pDeterminacao) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        $scope.PesquisaTabelas.Items = pDeterminacao.Veiculos;
        $scope.PesquisaTabelas.Titulo = "Seleção de Veículos"
        $scope.PesquisaTabelas.MultiSelect = true;
        $scope.PesquisaTabelas.ClickCallBack = function () { };
        $("#modalTabela").modal(true);
    };
    //===============Selecao de Programas
    $scope.SelecionarProgramas = function (pDeterminacao) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        $scope.PesquisaTabelas.Items = pDeterminacao.Programas;
        $scope.PesquisaTabelas.Titulo = "Seleção de Programas"
        $scope.PesquisaTabelas.ClickCallBack = function () { };
        $scope.PesquisaTabelas.MultiSelect = true;
        $("#modalTabela").modal(true);
    };
    //===============Cancelar a Determinacao
    $scope.CancelarDeterminacao = function () {
        $scope.Filtro = $scope.NewFilter();
        $scope.Determinacao = "";
        $scope.CountDe = 0;
        $scope.CountPara = 0;
        $scope.Competencia_Text = ""
        $scope.RodizioConfirmado = false;
        $scope.ShowAnalise = false;
        $scope.ShowDados = true;


    };
    //===============Adicionar Comercial
    $scope.AdicionarComercial = function (pDeterminacao) {
        $scope.ShowDados = false;
        $scope.ShowComercial = true;
        $scope.Comercial = $scope.NewComercial();
        $scope.Comercial.Cod_Empresa = pDeterminacao.Cod_Empresa;
        $scope.Comercial.Numero_Mr = pDeterminacao.Numero_Mr;
        $scope.Comercial.Sequencia_Mr = pDeterminacao.Sequencia_Mr;
    };
    //===============Clicou na lupa Produto 
    $scope.PesquisaProduto = function (pDeterminacao,pComercial) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        $scope.PesquisaTabelas.Items = [];
        $scope.PesquisaTabelas.PreFiltroTexto = "";
        $scope.PesquisaTabelas.PreFilter = true;
        $scope.PesquisaTabelas.Titulo = "Seleção de Produtos";
        $scope.PesquisaTabelas.MultiSelect = false;
        if (pDeterminacao.Cod_Cliente) {
            $scope.PesquisaTabelas.ButtonText = "Mostrar Produtos do Cliente " + pDeterminacao.Cod_Cliente
            $scope.PesquisaTabelas.ButtonCallBack = function () {
                httpService.Get('MapaReserva/ListarProdutoCliente/' + pDeterminacao.Cod_Cliente).then(function (response) {
                    $scope.PesquisaTabelas.Items = response.data;
                });
            };
        };
        $scope.PesquisaTabelas.ClickCallBack = function (value) { pComercial.Cod_Red_Produto = value.Codigo; pComercial.Nome_Produto = value.Descricao };
        $scope.PesquisaTabelas.LoadCallBack = function (pFilter) {
            httpService.Get('ListarTabela/Produto/' + pFilter).then(function (response) {
                $scope.PesquisaTabelas.Items = response.data;
            });
        }
        $("#modalTabela").modal(true);
    };
    ////==========================Validar o Produto
    $scope.ProdutoChange = function (pComercial) {
        if (!pComercial.Cod_Red_Produto) {
            pComercial.Nome_Produto = "";
            return;
        };
        httpService.Get("ValidarTabela/Produto/" + pComercial.Cod_Red_Produto).then(function (response) {
            if (!response.data[0].Status) {
                ShowAlert("Produto Inválido");
                pComercial.Cod_Red_Produto = "";
                pComercial.Nome_Produto = "";
            }
            else {
                pComercial.Nome_Produto = response.data[0].Descricao;
            }
        });
    };
    ////==========================Salvar Comercial
    $scope.SalvarComercial = function (pComercial) {
        httpService.Post("Determinacao/SalvarComercial", pComercial).then(function (response) {
            if (response.data) {
                if (response.data[0].Status) {
                    $scope.Determinacao.Comerciais.push(pComercial);
                    $scope.ShowComercial= false;
                    $scope.ShowDados = true;
                }
                else {
                    ShowAlert(response.data[0].Mensagem)
                };
            };
        }); 
    };
    ////==========================Clique em comercial de 
    $scope.ComercialDeClick = function (pDeterminacao, pComercial) {
        $scope.CountDe = 0;
        for (var i = 0; i < pDeterminacao.Comerciais.length; i++) {
            if (pDeterminacao.Comerciais[i].Cod_Comercial != pComercial.Cod_Comercial) {
                pDeterminacao.Comerciais[i].Selected_De = false;
            };
            if (pDeterminacao.Comerciais[i].Selected_De) {
                $scope.CountDe++;
            };
        };
    };
    ////==========================Clique em comercial Para
    $scope.ComercialParaClick = function (pDeterminacao, pComercial) {
        $scope.CountPara = 0;
        for (var i = 0; i < pDeterminacao.Comerciais.length; i++) {
            if (pDeterminacao.Comerciais[i].Selected_Para) {
                $scope.CountPara++;
            };
        };
    };
    ////==========================Confirmar Rotate
    $scope.ConfirmarRotate = function (pDeterminacao, pComercial) {
        
        for (var i = 0; i < pDeterminacao.Comerciais.length; i++) {
            if (pDeterminacao.Comerciais[i].Selected_Para) {
               if (pDeterminacao.Comerciais[i].Duracao != pComercial.Duracao) {
                    ShowAlert("Os comerciais De e Para devem ter a mesma duração");
                    return;
                };
            };
        };

        pComercial.Rotate = [];
        for (var i = 0; i < pDeterminacao.Comerciais.length; i++) {
            if (pDeterminacao.Comerciais[i].Selected_Para) {
            
                if (pDeterminacao.Comerciais[i].Duracao != pComercial.Duracao) {
                    ShowAlert("Os comerciais De e Para devem ter a mesma duração");
                    return;
                };
                pComercial.Rotate.push({ 'Cod_Comercial_De': pComercial.Cod_Comercial, 'Cod_Comercial_Para': pDeterminacao.Comerciais[i].Cod_Comercial })
            };
            pDeterminacao.Comerciais[i].Selected_De = false;
            pDeterminacao.Comerciais[i].Selected_Para = false;
        };
        $scope.CountDe= 0;
        $scope.CountPara = 0;
        $scope.RodizioConfirmado = $scope.CheckRodizioConfirmado(pDeterminacao);
    };
    ////==========================Desfazer Rotate
    $scope.DesfazerRotate = function (pDeterminacao, pComercial) {
        pComercial.Rotate = [];
        $scope.RodizioConfirmado = $scope.CheckRodizioConfirmado(pDeterminacao);
    };
    ////==========================Verifica se tem Rodizio Confirmado
    $scope.CheckRodizioConfirmado = function (pDeterminacao) {
        var _TemRodizio = false;
        for (var i = 0; i < pDeterminacao.Comerciais.length; i++) {
            if (pDeterminacao.Comerciais[i].Rotate.length>0) {
                _TemRodizio = true;
                break;
            };
        };
        return _TemRodizio;
    }
    ////==========================Analisar Rotate
    $scope.AnalisarRotate = function (pDeterminacao) {
        if ($scope.CountDe && $scope.CountPara) {
            ShowAlert("Favor Confirmar o De-Para antes de analisar");
            return;
        }
        httpService.Post('Determinacao/AnalisarRotate',pDeterminacao).then(function(response){
            if (response.data) {
                if (response.data[0].Status) {
                    $scope.AnaliseRotate= response.data;
                    $scope.ShowDados = false;
                    $scope.ShowAnalise = true;
                }
                else {
                    ShowAlert(response.data[0].Mensagem);
                };
            };
        });
    };
    ////==========================Salvar  Rotate
    $scope.SalvarRotate = function (pDeterminacao) {
        httpService.Post('Determinacao/SalvarDeterminacao', pDeterminacao).then(function (response) {
            if (response.data) {
                if (!response.data[0].Critica) {
                    ShowAlert('Determinação concluida com sucesso.');
                    $scope.CancelarDeterminacao()
                }
                else {
                    ShowAlert(response.data[0].Critica);
                };
            };
        });
    };
}]);


