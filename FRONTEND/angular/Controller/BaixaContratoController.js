angular.module('App').controller('BaixaContratoController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

        
    //====================Inicializa scopes
    $scope.Tipo_Operacao = [{ 'Descricao': 'Baixa'},{'Descricao':'Cancelamento'}];
    $scope.Filtro = {};
    $scope.NewFiltro = function () {
        $scope.Filtro = { 'Cod_Empresa': '', 'Numero_Mr': '', 'Sequencia_Mr': '' ,'Cod_Programa':'','Cod_Comercial':''};
    }
    $scope.NewFiltro();
    $scope.BaixaContrato = "";
    $scope.Veiculos = [];
    $scope.Agencias = "";
    $scope.Clientes = "";
    $scope.Programas = "";
    //====================Carrega Contrato            
    $scope.CarregarContratoBaixa = function (pFiltro) {
        if (pFiltro.Cod_Empresa == undefined) {
            ShowAlert("Cod_Empresa é de seleção obrigatória!");
            return
        }
        if (pFiltro.Numero_Mr == undefined) {
            ShowAlert("Numero_Mr é de seleção obrigatória!");
            return
        }
        if (pFiltro.Sequencia_Mr == undefined) {
            ShowAlert("Sequencia_Mr é de seleção obrigatória!");
            return
        }
        $scope.ShowFilter = false;
        httpService.Post("BaixaContrato/GetContratoBaixa", pFiltro).then(function (response) {
            if (response) {
                $scope.BaixaContrato = response.data;
                if (!$scope.BaixaContrato.Loaded) {
                    ShowAlert("Não existe dados cadastrado p/ este Filtro");
                };
            };
        });
    };

    //===============Pesquisa Programas do Contrato
    $scope.PesquisaProgramaContrato = function (pFiltro, pContrato) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        pFiltro.cod = "";
        httpService.Post('BaixaContrato/GetProgramaContrato', pFiltro).then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção de Programas";
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pContrato.Cod_Programa = value.Codigo; pContrato.Titulo = value.Descricao };
                $("#modalTabela").modal(true);
            }
        });
    };
    //===============Validar Programas do Contrato
    $scope.ValidarPrograma = function (pFiltro, pContrato) {
        if (pContrato.Cod_Programa == "") {
            pContrato.Titulo = "";
            return;
        }
        pFiltro.Cod_Programa = pContrato.Cod_Programa;
        httpService.Post('BaixaContrato/GetProgramaContrato', pFiltro).then(function (response) {
            if (response.data) {
                if (response.data.length == 0) {
                    ShowAlert("Programa Inválido para esse Contrato.")
                    pContrato.Cod_Programa = "";
                    pContrato.Titulo = "";
                    pFiltro.Cod_Programa = "";
                }
                else {
                    pContrato.Titulo = response.data[0].Descricao;
                }
            };
        });
    };
    //===============Pesquisa Comercial do Contrato
    $scope.PesquisaComercialContrato = function (pFiltro, pContrato) {
        pFiltro.Cod_Comercial = "";
        $scope.PesquisaTabelas = NewPesquisaTabela();
        httpService.Post('BaixaContrato/GetComercialContrato', pFiltro).then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção de Comercial";
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pContrato.Cod_Comercial = value.Codigo; pContrato.Titulo_Comercial = value.Descricao };
                $("#modalTabela").modal(true);
            }
        });
    };
    //===============Validar Comercial do Contrato
    $scope.ValidarComercial = function (pFiltro, pContrato) {
        if (pContrato.Cod_Comercial == "") {
            pContrato.Titulo_Comercial = "";
            return;
        }
        pFiltro.Cod_Comercial = pContrato.Cod_Comercial;
        httpService.Post('BaixaContrato/GetComercialContrato', pFiltro).then(function (response) {
            if (response.data) {
                if (response.data.length == 0) {
                    ShowAlert("Comercial Inválido para esse Contrato.")
                    pContrato.Cod_Comercial = "";
                    pContrato.Titulo_Comercial = "";
                    pFiltro.Cod_Comercial = "";
                }
                else {
                    pContrato.Titulo_Comercial = response.data[0].Descricao;
                }
            };
        });
    };
    //====================Salva Contrato
    $scope.SalvarContratoBaixa = function (pBaixaContrato) {
        if (pBaixaContrato.Tipo_Operacao == "" || pBaixaContrato.Tipo_Operacao == undefined) {
            ShowAlert("Favor Selecionar 'Tipo de Operação'!");
            return
        }

        if (pBaixaContrato.Tipo_Operacao == "Cancelamento") {
            pBaixaContrato.Domingo = true;
            pBaixaContrato.Segunda = true;
            pBaixaContrato.Terca = true;
            pBaixaContrato.Quarta = true;
            pBaixaContrato.Quinta = true;
            pBaixaContrato.Sexta = true;
            pBaixaContrato.Sabado = true;
            pBaixaContrato.Cod_Programa = "";
            pBaixaContrato.Titulo = "";
            pBaixaContrato.Cod_Comercial = "";
            pBaixaContrato.Titulo_Comercial = "";
        }
        
        $scope._varSemana = ",";
        for (ix = 0; ix <= 6; ix++) {
            if (pBaixaContrato.Domingo == true && ix == 0) {
                $scope._varSemana += (ix + 1) + ",";
            } else if (pBaixaContrato.Segunda == true && ix == 1) {
                $scope._varSemana += (ix + 1) + ",";
            } else if (pBaixaContrato.Terca == true && ix == 2) {
                $scope._varSemana += (ix + 1) + ",";
            } else if (pBaixaContrato.Quarta == true && ix == 3) {
                $scope._varSemana += (ix + 1) + ",";
            } else if (pBaixaContrato.Quinta == true && ix == 4) {
                $scope._varSemana += (ix + 1) + ",";
            } else if (pBaixaContrato.Sexta == true && ix == 5) {
                $scope._varSemana += (ix + 1) + ",";
            } else if (pBaixaContrato.Sabado == true && ix == 6) {
                $scope._varSemana += (ix + 1) + ",";
            }
        }
        //==================================Consistência
        if (pBaixaContrato.Tipo_Operacao == "Baixa") {
            var bolTemVeiculo = false;
            for (var i = 0; i < $scope.BaixaContrato.Veiculos.length; i++) {
                if (true) {
                    if ($scope.BaixaContrato.Veiculos[i].Selected) {
                        bolTemVeiculo = true;
                        break;
                    };
                };
            };
            if (!bolTemVeiculo) {
                ShowAlert("Nenhum Veiculo foi Selecionado!");
                return
            };
        };

        if ($scope._varSemana == ",") {
            ShowAlert("Selecione pelo menos um dia da semana!");
            return
        }
        else {
            pBaixaContrato.DiaSemana = $scope._varSemana;
        };

        if (pBaixaContrato.Tipo_Operacao == undefined || pBaixaContrato.Tipo_Operacao == "") {
            ShowAlert("'Tipo de Operação', é de seleção obrigatória!");
            return
        };

        if (pBaixaContrato.Data_Inicial == undefined || pBaixaContrato.Data_Inicial == "") {
            ShowAlert("Data, 'A Partir de', é de seleção obrigatória!");
            return
        };

        if (pBaixaContrato.Cod_Qualidade == undefined || pBaixaContrato.Cod_Qualidade == "") {
            ShowAlert("'Qualidade' é de seleção obrigatória!");
            return
        };

        httpService.Post("BaixaContrato/Baixar", pBaixaContrato).then(function (response) {
            if (response) {
                if (response.data) {
                    var msg = 'Baixa Concluida Com Sucesso\n';
                    msg += "Qtd Veiculacões Baixadas: " + response.data[0].Qtd_Baixa + '\n';
                    msg += "Qtd Veiculacões Rejeitadas: " + response.data[0].Qtd_Rejeitado;
                    ShowAlert(msg);
                    $scope.NewFiltro();
                    $scope.BaixaContrato = "";
                }
                else {
                    ShowAlert('Houve erro na Baixa, Verifique.');
                }
            }
        })
    };
}]);

