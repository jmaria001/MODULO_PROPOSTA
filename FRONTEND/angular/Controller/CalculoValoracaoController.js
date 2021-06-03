angular.module('App').controller('CalculoValoracaoController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {

    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.PermissaoEdit = false;
    $scope.PermissaoNego = true;
    $scope.CalculoValoracaoS = [];
 
    httpService.Get("credential/CalculoValoracao@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/CalculoValoracao@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });
    $scope.PermissaoExcluir = 'false';
    httpService.Get("credential/CalculoValoracao@Destroy").then(function (response) {
        $scope.PermissaoExcluir = response.data;
    });


    //====================Inicializa scopes
    $scope.CalculoValoracaoS = [];

    $scope.NewCalculoValoracao = function () {
        return {
            'Cod_Empresa': '',
            'Numero_Mr': '',
            'Sequencia_Mr': ''
        }
    }
    $scope.CalculoValoracaos = $scope.NewCalculoValoracao();
 
    $scope.Operacao = "";

    //===========================Adicionar Linhas de Comercial
    $scope.AdicionarCalculoValoracao = function () {
        $scope.CalculoValoracaoS.push({});
    }

       //=======================Selecao de Tipo de Comercial
    $scope.PesquisaEmpresa = function () {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        $scope.listaEmpresa = ""
        var _url = 'ListarTabela/Empresa'
        httpService.Get(_url).then(function (response) {
            $scope.PesquisaTabelas.Items = response.data;
            $scope.PesquisaTabelas.FiltroTexto = ""
            $scope.PesquisaTabelas.PreFilter = false;
            $scope.PesquisaTabelas.Titulo = "Seleção de Tipos de Empresa"
            $scope.PesquisaTabelas.MultiSelect = false;
            $scope.PesquisaTabelas.ClickCallBack = function (value) {
                $scope.CalculoValoracao.Cod_Empresa = value.Codigo, $scope.CalculoValoracao.Nome_Empresa = value.Descricao;
            },
                $("#modalTabela").modal(true);
        });
    };
    //======================Validar Empresa 
    $scope.ValidarEmpresa = function (pCodigo) {
        pCodigo = pCodigo.toUpperCase();
        $scope.CalculoValoracao.Cod_Empresa = pCodigo.toUpperCase();
        for (var i = 0; i < $scope.ParametroValoracao.Tipo_Comercial.length; i++) {
            if ($scope.CalculoValoracaol[i].Cod_Empresa == pCodigo) {
                ShowAlert("Código já Cadastrado", "warning")
                $scope.CalculoValoracao.Cod_Empresa = "";
                return;
            };
        };
        var _url = "ValidarTabela/Empresa/" + pCodigo.trim()
        httpService.Get(_url).then(function (response) {
            if (response.data[0].Status == 0) {
                ShowAlert(response.data[0].Mensagem, 'warning', 2000);
                $scope.CalculoValoracao.Cod_Empresa = "";

            }
            else {
                $scope.CalculoValoracao.Nome_Empresa = response.data[0].Descricao;
            }
        })
    };

    //===============Clicou na lupa Tpo do Comercial 
    $scope.PesquisaEmpresa = function (pEmpresa) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        httpService.Get('ListarTabela//Empresa_Usuario').then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção de Empresas";
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pEmpresa.Cod_Empresa = value.Codigo; pEmpresa.Nome_Empresa = value.Descricao };
                $("#modalTabela").modal(true);
            }
        });
    }

    //===============Validar Empresa
    $scope.ValidarEmpresa = function (pEmpresa) {
        if (!pEmpresa.Cod_Empresa) {
            pEmpresa.Nome_Empresa = "";
            return;
        }
        httpService.Get('ValidarTabela/Empresa_Usuario/' + pEmpresa.Cod_Empresa).then(function (response) {
            if (response.data[0].Status == 0) {
                ShowAlert(response.data[0].Mensagem)
                pEmpresa.Cod_Empresa = "";
                pEmpresa.Nome_Empresa = "";
            }
            else {
                pEmpresa.Nome_Empresa = response.data[0].Descricao
            }
        });
    }


    //====================Carrega Contrato            
    $scope.ValidarContrato = function (pContrato) {
        if (pContrato.Cod_Empresa == "") {
            ShowAlert("Empresa é obrigatória!");
            return;
        }

        if (pContrato.Sequencia_Mr == "") {
            ShowAlert("Dado Sequencia do contrato é obrigatória!");
            return;
        }
        httpService.Post('ValidarContrato', pContrato).then(function (response) {
            if (response) {

                if (response.data.length > 0) {
                    return;
                }
                else {
                    ShowAlert('Não existe  contrato Informado!')
                    return;

                }
            }
        });
    };

    //====================Carrega Contrato            
    $scope.ValidarNegociacao = function (pNegociacao) {

        httpService.Post('ValidarNegociacao', pNegociacao).then(function (response) {
            if (response) {
                if (response.data.length > 0) {
                    return;
                }
                else {
                    ShowAlert('Não existe negociação no contrato Informado!')
                    return;
                }
            }

        });
    };



    //=======================Salvar Calculo de Valoracao
    $scope.SalvarCalculoValoracao = function (pCalculoValoracaoS, pCalculoValoracao) {
        var i = pCalculoValoracaoS.length;
        if (pCalculoValoracaoS == 0 && pCalculoValoracao == undefined) {
            ShowAlert("Valoração de Contrato ou Negociação/Pendentes não foram definidos !");
            return
        }

        if (pCalculoValoracao != undefined) {

            for (var i = 0; i < pCalculoValoracaoS.length; i++) {
                pCalculoValoracaoS[i].Numero_Negociacao = pCalculoValoracao.Numero_Negociacao;
                pCalculoValoracaoS[i].Competencia = pCalculoValoracao.Competencia;
                pCalculoValoracaoS[i].Indica_Valoracao = pCalculoValoracao.Indica_Valoracao;

            }



            if (pCalculoValoracao.Competencia != "" && pCalculoValoracao.Numero_Negociacao == "") {
                ShowAlert("Numero de Negociação é obrigatória!");
                return;
            }

            httpService.Post("ValoracaoPendentes", pCalculoValoracao).then(function (response) {

                if (response) {
                    if (response.data[0].Retorno != "OK") {
                        ShowAlert(response.data[0].Retorno, 'success');
                        return;
                    }

                }
            });
        }

        if (pCalculoValoracaoS.length != 0) {


            httpService.Post("ValoracaoContratos", pCalculoValoracaoS).then(function (response) {
                if (response) {

                    if (response.data[response.data.length-1].nValorados != 0) {

                        ShowAlert(response.data[response.data.length -1].nValorados + ' Contrato(s) foram colocado(s) na fila de valoração.');
                       
                    }

                    else {
                        ShowAlert('Nenhum Contrato foi colocado na fila de valoração.')
                        
                    }
                    pCalculoValoracao.Competencia = "";
                    pCalculoValoracao.Numero_Negociacao = "";
                    pCalculoValoracao.Indica_Valoracao = false;
                    $scope.CalculoValoracaoS.splice({});
                    return;
                }

            });
        }
        else {
            if (pCalculoValoracao.Indica_Valoracao == undefined) { pCalculoValoracao.Indica_Valoracao = 0 };

            httpService.Post("ValoracaoContratosNego", pCalculoValoracao).then(function (response) {
                if (response) {

                    if (response.data[0].nValorados != 0) {

                        ShowAlert(response.data[0].nValorados + ' Contrato(s) foram colocado(s) na fila de valoração.');
                       
                    }

                    else {
                        ShowAlert('Nenhum Contrato foi colocado na fila de valoração.')
                       
                    }
                    pCalculoValoracao.Competencia       = "";
                    pCalculoValoracao.Numero_Negociacao = "";
                    pCalculoValoracao.Indica_Valoracao  = false;
                    $scope.CalculoValoracaoS.splice({});
                    return;

                }

            });

        }
 
    };

    $scope.CalculoValoracaoExcluir = function (pContrato) {

        for (var i = 0; i < $scope.CalculoValoracaoS.length; i++) {

            if ($scope.CalculoValoracaoS[i].Cod_Empresa == pContrato.Cod_Empresa && $scope.CalculoValoracaoS[i].Numero_Mr == pContrato.Numero_Mr && $scope.CalculoValoracaoS[i].Sequencia_Mr == pContrato.Sequencia_Mr) {
                $scope.CalculoValoracaoS.splice(i, 1);
                break;
            }

        }
    };

}]);

