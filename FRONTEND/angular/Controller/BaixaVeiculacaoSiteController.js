angular.module('App').controller('BaixaVeiculacaoSiteController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //===========================Inicializa scopes
    $scope.ShowFilter = true;
    $scope.ShowGrid = false;
    $scope.Filtro = ""
    $scope.Veiculacoes = [];
    $scope.NewFilter = function () {
        $scope.Filtro = { 'Cod_Veiculo': '', 'Nome_Veiculo': '', 'Data_Exibicao': '', 'Cod_Programa': '', 'Nome_Programa': '', 'Cod_Empresa': '', 'Numero_Mr': '', 'Sequencia"mr': '' };
        $scope.Veiculacoes = [];
    };
    $scope.NewFilter();

    //===========================Carrega Veiculacoes 
    $scope.CarregarVeiculacoesSite = function (pFiltro) {
        httpService.Post('BaixaSite/CarregarVeiculacao', pFiltro).then(function (response) {
            if (response.data) {
                if (response.data.length == 0) {
                    ShowAlert('Nenhum registro foi selecionado com esse filtro');
                    return
                };

                if (!response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem);
                    return;
                }
                else {
                    $scope.Veiculacoes = response.data;
                    $scope.ShowFilter = false;
                    $scope.ShowGrid = true;
                };
            };
        });
    };
    //================================-=Mudou a Qtd Exibido
    $scope.QtdExibidoChange = function (pVeiculacao) {
        var _falhas =  pVeiculacao.Qtd_Previsto - pVeiculacao.Qtd_Exibido; 
        if (_falhas > 0) {
            pVeiculacao.Qtd_Falha = _falhas;
            pVeiculacao.Cod_Qualidade = pVeiculacao.Cod_Qualidade_Padrao;
        }
        else {
            pVeiculacao.Qtd_Falha = 0;
            pVeiculacao.Cod_Qualidade = "";
        };
    };

    $scope.ConfirmarTodos = function (pVeiculacao) {
        swal({
            title: "Confirma a exibição de Todos os comerciais pela Qtd Prevista?",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            cancelButtonText: "Não,Cancelar",
            confirmButtonText: "Sim, Confirmar",
            closeOnConfirm: true
        }, function () {
            for (var i = 0; i < pVeiculacao.length; i++) {
                pVeiculacao[i].Qtd_Exibido = pVeiculacao[i].Qtd_Previsto;
                pVeiculacao[i].Qtd_Falha = 0;
                pVeiculacao[i].Cod_Qualidade = "";
            };
            $scope.$digest();
        });
    };
    //===============================Cancelar Baixa
    $scope.CancelarBaixa = function () {
        $scope.Veiculacoes = [];
        $scope.ShowGrid = false;
        $scope.ShowFilter = true;
    };
    //===============Clicou na lupa de qualidade
    $scope.PesquisaQualidade = function (pVeiculacao) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        httpService.Get('ListarTabela/Qualidade').then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção de Qualidade";
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pVeiculacao.Cod_Qualidade = value.Codigo};
                $("#modalTabela").modal(true);
            }
        });
    }
    //===========================Validar Código de Qualidade
    $scope.ValidarQualidade = function (pVeiculacao) {
        if (pVeiculacao.Cod_Qualidade == "") {
            return;
        }
        httpService.Post('BaixaVeiculacoes/ValidarQualidade', pVeiculacao).then(function (response) {
            if (response.data) {
                if (response.data[0].Status == 0) {
                    ShowAlert(response.data[0].Mensagem)
                    pVeiculacao.Cod_Qualidade = "";
                    return;
                }
            }
        });
    };
    //===========================Salvar a Baixa
    $scope.SalvarBaixa = function (pVeiculacao) {
        swal({
            title: "Confirma a Baixa das Veiculações ?",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            cancelButtonText: "Não,Cancelar",
            confirmButtonText: "Sim, Confirmar",
            closeOnConfirm: true
        }, function () {
            httpService.Post('BaixaSite/BaixarVeiculacao', pVeiculacao).then(function (response) {
                $scope.Veiculacoes = response.data
            });
        });
    };

    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
    });

}]);

