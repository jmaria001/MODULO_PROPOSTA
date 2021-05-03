angular.module('App').controller('TabelaPrecosMolCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {
   
    //========================Recebe Parametro
    $scope.Parameters = $routeParams;
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.TabelaPrecosMolCad = "";

    //========================Verifica Permissoes
    $scope.PermissaoDelete = false;
    httpService.Get("credential/TabelaPrecosMol@Destroy").then(function (response) {
        $scope.PermissaoDelete = response.data;
    });

    //==========================Busca dados do Tabela de Preços MOL
    var _url = "GetTabelaPrecosMolData/" + $scope.Parameters.Id;
    httpService.Get(_url).then(function (response) {
        if (response) {
            $scope.TabelaPrecosMolCad = response.data;
        }
    });
    //------------------- Adicionar linha no grid ----------------------
    $scope.AdicionarLinha = function () {
        $scope.TabelaPrecosMolCad.Max_Id_Linha++;
        $scope.TabelaPrecosMolCad.ValoresMol.push({
            'Id_Linha': $scope.TabelaPrecosMolCad.Max_Id_Linha,
            'Cod_Tipo_Comercializacao': '',
            'Nome_Comercializacao': '',
            'Valor': '0,00'
        });
    };

    //------------------- Remover linha no grid ----------------------
    $scope.RemoverLinha = function (pId_Linha) {
        for (var i = 0; i < $scope.TabelaPrecosMolCad.ValoresMol.length; i++) {
            if ($scope.TabelaPrecosMolCad.ValoresMol[i].Id_Linha == pId_Linha) {
                $scope.TabelaPrecosMolCad.ValoresMol.splice(i, 1);
                break;
            };
        };
    };
    //------------------- Selecionar Tipo Comercializacao----------------------
    $scope.SelecionarTipoComercializacao = function (pValoresMol) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        httpService.Get('ListarTabela/Tipo_Comercializacao').then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data;
                $scope.PesquisaTabelas.Titulo = "Seleção de Tipo de Comercializacao"
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pValoresMol.Cod_Tipo_Comercializacao = value.Codigo; pValoresMol.Nome_Comercializacao = value.Descricao }
                $("#modalTabela").modal(true);
            }
        });
    };

    //------------------- Validar Tipo Comercializacao----------------------
    $scope.ValidarTipoComercializacao = function (pValoresMol) {
        httpService.Get("ValidarTabela/Tipo_Comercializacao/" + pValoresMol.Cod_Tipo_Comercializacao).then(function (response) {
            if (!response.data[0].Status) {
                ShowAlert(response.data[0].Mensagem + " - " + pValoresMol.Cod_Tipo_Comercializacao + " - Linha " + pValoresMol.Id_Linha);
                pValoresMol.Cod_Tipo_Comercializacao.Tipo_Midia = "";
            }
            else {
                pValoresMol.Nome_Comercializacao = response.data[0].Descricao;
            }
        });
    };

    //==========================Salvar
    $scope.SalvarTabelaPrecosMol = function (pDados, pValoresMol) {
        if (!pDados.Competencia) {
            ShowAlert("Competência é obrigatória");
            return;
        }
        if (!pDados.Cod_Veiculo_Mercado) {
            ShowAlert("Veículo/Mercado é obrigatório");
            return;
        }
        if (!pDados.Cod_Programa) {
            ShowAlert("Programa é obrigatório");
            return;
        }
        //--Percorre grid para verificar se está preenchido
        var bolPreenchido = false;
        for (var i = 0; i < pValoresMol.length; i++) {
            if (pValoresMol[i].Cod_Tipo_Comercializacao && DoubleVal(pValoresMol[i].Valor) > 0) {
                bolPreenchido = true;
                break;
            };
        };
        if (!bolPreenchido) {
            ShowAlert("Favor digitar pelo menos um Tipo de Comercialização e Valor");
            return;
        }
        //--Percorre grid para verificar duplicidade de tipo_comercializacao
        var bolDuplicado = false;
        var strTipoComerc = "";
        var iId_Linha = 0;
        for (var i = 0; i < pValoresMol.length; i++) {
            strTipoComerc = pValoresMol[i].Cod_Tipo_Comercializacao;
            iId_Linha = pValoresMol[i].Id_Linha;

            for (var x = 0; x < pValoresMol.length; x++) {
                if (pValoresMol[x].Cod_Tipo_Comercializacao == strTipoComerc && pValoresMol[x].Id_Linha != iId_Linha) {
                    bolDuplicado = true;
                    break;
                };
            };
            if (bolDuplicado) {
                ShowAlert("Existe duplicidade de Tipo de Comercialização, favor corrigir");
                return;
            };
        };
        //--Salva
        $scope.TabelaPrecosMolCad.Id_Operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        var _data = {
            'Id_Operacao': pDados.Id_Operacao,
            'Competencia': pDados.Competencia,
            'Cod_Programa': pDados.Cod_Programa,
            'Cod_Veiculo_Mercado': pDados.Cod_Veiculo_Mercado,
            'ValoresMol': pValoresMol
        };
        httpService.Post("SalvarTabelaPrecosMol", _data).then(function (response) {
            if (response) {
                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/TabelaPrecosMol");
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };

    //======================Excluir
    $scope.ExcluirTabelaPrecosMol = function (pTabelaPrecosMol) {
        swal({
            title: "Tem certeza que deseja Excluir esta Tabela de Preços MOL ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("ExcluirTabelaPrecosMol", pTabelaPrecosMol).then(function (response) {
                if (response) {
                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/TabelaPrecosMol");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });
    };

}]);




