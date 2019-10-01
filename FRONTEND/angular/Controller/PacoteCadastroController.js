angular.module('App').controller('PacoteCadastroController', ['$scope', '$rootScope', '$filter', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope,$filter, httpService, $location, $timeout, $routeParams) {

    //========================Inicializa Scopes
    $scope.Parameters = $routeParams;
    $scope.Pacote = "";
    $scope.OpcoesDesconto = [];
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

    $scope.newDigitacaoDesconto = function () {
        return { 'Tipo': { 'Codigo': '', 'Descricao': '' }, 'Conteudo': '', 'Data_Inicio': '', 'Data_Termino': '', 'Chave': '', Desconto: '' }
    }
    $scope.DigitacaoDesconto = $scope.newDigitacaoDesconto();
    $scope.TipoDescontoFilter = "";
    //==========================Busca dados do Pacote
    var _url = "GetPacoteData/" + $scope.Parameters.Id.trim();
    httpService.Get(_url).then(function (response) {
        if (response) {
            $scope.Pacote = response.data;
        }
    });
    //==========================Carregar Itens de Desconto
    $scope.CarregarOpcoesDesconto = function (pDesconto) {
        $scope.TipoDescontoFilter = "";
        if (pDesconto.Tipo.Codigo != 1) {
            httpService.Get("GetOpcoesDesconto/" + pDesconto.Tipo.Codigo).then(function (response) {
                if (response.data) {
                    $scope.OpcoesDesconto = response.data;
                }
            });
        }
    }
    //==========================Adicionar Itens no Grid de Descontos
    $scope.AdicionarDesconto = function (pDesconto) {
        if (!pDesconto.Tipo.Codigo) {
            ShowAlert("Favor Informar o Tipo de Desconto")
            return;
        }
        if (!pDesconto.Desconto || pDesconto.Desconto == 0) {
            ShowAlert("Favor Informar o Desconto")
            return;
        }
        if (pDesconto.Tipo.Codigo == 1) {
            if (!pDesconto.Data_Inicio || !pDesconto.Data_Termino) {
                ShowAlert("Favor Informar os data de Início e Término para o Desconto",'warning')
                return
            }
            else {
                $scope.Pacote.DescontoDetalhe.push({
                    'id_Pacote_Detalhe': $scope.Pacote.Max_Id_Desconto + 1,
                    'Cod_Desconto': pDesconto.Tipo.Codigo,
                    'Descricao':pDesconto.Tipo.Descricao,
                    'Conteudo': pDesconto.Data_Inicio + ' a ' + pDesconto.Data_Termino,
                    'Data_Inicio': StringToDate(pDesconto.Data_Inicio,"dd/mm/yyyy"),
                    'Data_Termino': StringToDate(pDesconto.Data_Termino,"dd/mm/yyyy"),
                    'Chave': '',
                    'Desconto': pDesconto.Desconto
                })
                $scope.DigitacaoDesconto = $scope.newDigitacaoDesconto();
                $scope.TipoDescontoFilter = "";
                $scope.Pacote.Max_Id_Desconto++;
            }
        }
        if (pDesconto.Tipo.Codigo != 1) {
            var _qtd_Selecionado = 0;
            for (var i = 0; i < $scope.OpcoesDesconto.length; i++) {
                if ($scope.OpcoesDesconto[i].Selecionado) {
                    _qtd_Selecionado++
                    $scope.Pacote.DescontoDetalhe.push({
                        'id_Pacote_Detalhe': $scope.Pacote.Max_Id_Desconto + 1,
                        'Cod_Desconto': pDesconto.Tipo.Codigo,
                        'Descricao': pDesconto.Tipo.Descricao,
                        'Conteudo': $scope.OpcoesDesconto[i].Descricao,
                        'Data_Inicio': null,
                        'Data_Termino': null,
                        'Chave': $scope.OpcoesDesconto[i].Codigo,
                        'Desconto': pDesconto.Desconto
                    })
                    $scope.Pacote.Max_Id_Desconto++;
                }
            }
            if (_qtd_Selecionado == 0) {
                ShowAlert('Nenhum Item foi selecionado', 'warning');
                return
            }
            $scope.DigitacaoDesconto = $scope.newDigitacaoDesconto();
            $scope.TipoDescontoFilter = "";
        }
    };
    //==========================Remover Itens no Grid de Descontos
    $scope.RemoverDesconto = function (pId) {
        swal({
            title: "Tem certeza que deseja Excluir esse Desconto ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            for (var i = 0; i < $scope.Pacote.DescontoDetalhe.length; i++) {
                if ($scope.Pacote.DescontoDetalhe[i].id_Pacote_Detalhe==pId) {
                    $scope.Pacote.DescontoDetalhe.splice(i, 1);
                    $scope.$digest();
                    break;
                }
            }
        });
    };
    //===========================Salvar Pacote
    $scope.SalvarPacote = function (pPacote) {
        httpService.Post("SalvarPacote" , pPacote).then(function (response) {
            if (response.data) {
                if (response.data[0].Status) {
                    $scope.Pacote.Id_Pacote = response.data[0].Id_Pacote;
                    ShowAlert(response.data[0].Mensagem, 'success');
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'error');
                }
            }
        });
    };
}
]);

