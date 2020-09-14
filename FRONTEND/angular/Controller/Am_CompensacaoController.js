angular.module('App').controller('Am_CompensacaoController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {
    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.PermissaoEdit = false;
    $scope.PermissaoExcluir = 'false';
    $scope.Parameters = $routeParams;
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.MesAnoKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.Solucao = [];
    //========================Recebe Parametro
    //httpService.Get("credential/MapaReservaCompensacaoFalhas@New").then(function (response) {
    //    $scope.PermissaoNew = response.data;
    //});

    
    //httpService.Get("credential/" + "MapaReservaCompensacaoFalhas@Edit").then(function (response) {
    //    $scope.PermissaoEdit = response.data;
    //});


    //httpService.Get("credential/MapaReservaCompensacaoFalhas@Destroy").then(function (response) {
    //    $scope.PermissaoExcluir = response.data;
    //});

    //====================Inicializa scopes
    $scope.ShowGrid = false;
    $scope.gridheaders = [{ 'title': '', 'visible': true, 'searchable': false, 'config': true, 'sortable': false },
        { 'title': 'Data', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Programa', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Comercial', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Duração', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Qualidade', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
        { 'title': 'Valor', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    ];

    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.CurrentShow = 'Grid';
        $scope.ShowGrid = true;
        //setTimeout(function () {
        //    $("#dataTable").dataTable().fnAdjustColumnSizing();
        //}, 10)
    };

    $scope.NewCompensacao = function () {
        return {
            'Competencia': '',
            'Cod_Programa': '',
            'Data_Exibicao': '',
            'Cod_Comercial': '',
            'Duracao': '',
            'Quantidade': '',
            'Valor': '',
        }
    }
    $scope.Compensacao_Temp = $scope.NewCompensacao();


    $scope.CancelaEdicao = function () {
        $scope.Operacao = "";
        $scope.ShowInput = false;
        $scope.Compensacao_Temp = $scope.NewCompensacao();
    };


    //====================Carrega o Grid

    $scope.CarregarFalhasCompensacao = function () {
        $rootScope.routeloading = true;
        $scope.FalhasCompensacao = [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();

        var _url = "AMFalhas"
         _url+= "?Cod_Empresa="  +  $scope.Parameters.Cod_Empresa;
        _url += "&Numero_Mr=" + $scope.Parameters.Numero_Mr;
        _url += "&Sequencia_Mr=" + $scope.Parameters.Sequencia_Mr;
        _url += "&Documento_Para=" + $scope.Parameters.Numero_Docto;
        _url += "&";
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.FalhasCompensacao = response.data;
                if ($scope.FalhasCompensacao.length == 0) {
                    $scope.RepeatFinished();
                }
            }
        });
    }


    //=======================Selecao de Tipo de Comercial
    $scope.PesquisaComerciais = function () {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        $scope.listaTipoComercial = ""
        var _url = 'AM/PesquisaComerciais'
        _url += "?Cod_Empresa=" + $scope.Parameters.Cod_Empresa;
        _url += "&Numero_Mr=" + $scope.Parameters.Numero_Mr;
        _url += "&Sequencia_Mr=" + $scope.Parameters.Sequencia_Mr;
        _url += "&";
        $scope.PesquisaTabelas = NewPesquisaTabela()

        httpService.Get(_url).then(function (response) {
            $scope.PesquisaTabelas.Items = response.data;
            $scope.PesquisaTabelas.FiltroTexto = ""
            $scope.PesquisaTabelas.PreFilter = false;
            $scope.PesquisaTabelas.Titulo = "Seleção de Tipos de Comerciais"
            $scope.PesquisaTabelas.MultiSelect = false;
            $scope.PesquisaTabelas.ClickCallBack = function (value) {
                $scope.Compensacao_Temp.Cod_Comercial = value.Codigo;
                $scope.Compensacao_Temp.Duracao = value.Duracao;
            },
                $("#modalTabela").modal(true);
        });
    };

    //=======================Validacao de Comerciais
    $scope.ValidarTipoComercial = function (pParam) {
        var _url = 'AM/PesquisaComerciais'
        _url += "?Cod_Empresa=" + $scope.Parameters.Cod_Empresa;
        _url += "&Numero_Mr=" + $scope.Parameters.Numero_Mr;
        _url += "&Sequencia_Mr=" + $scope.Parameters.Sequencia_Mr;
        _url += "&Cod_Comercial=" + pParam.Cod_Comercial;
        _url += "&";
        httpService.Get(_url).then(function (response) {
            if (response.data.length == 0) {
                pParam.Cod_Comercial = "";
                pParam.Duracao= "";
                ShowAlert("Código do Comercial não existe");
            }
            else {
                pParam.Duracao = response.data[0].Duracao;
            }
        });
    };





    //=======================Selecao de Programas
    $scope.PesquisaPrograma = function (pParam) {
        if (!pParam.Competencia) {
            pParam.Cod_Programa = "";
            ShowAlert("Informe a Competência antes de selecionar o Programa");
            return;
        }
        $scope.PesquisaTabelas = NewPesquisaTabela();
        $scope.listaPrograma = ""
        var _url = 'AM/ListarGrade'
        _url += "?Cod_Veiculo=" + $scope.Parameters.Cod_Veiculo;
        _url += "&Competencia=" + pParam.Competencia;
        _url += "&Cod_Empresa=" + $scope.Parameters.Cod_Empresa;
        _url += "&Numero_Mr=" + $scope.Parameters.Numero_Mr;
        _url += "&Sequencia_Mr=" + $scope.Parameters.Sequencia_Mr;
        _url += "&Cod_Programa=" + $scope.Compensacao_Temp.Cod_Programa.trim();
        _url += "&";
       httpService.Get(_url).then(function (response) {
            $scope.PesquisaTabelas.Items = response.data;
            $scope.PesquisaTabelas.FiltroTexto = ""
            $scope.PesquisaTabelas.PreFilter = false;
            $scope.PesquisaTabelas.Titulo = "Seleção de Grade"
            $scope.PesquisaTabelas.MultiSelect = false;
            $scope.PesquisaTabelas.ClickCallBack = function (value) {
                $scope.Compensacao_Temp.Cod_Programa = value.Cod_Programa;
                $scope.Compensacao_Temp.Data_Exibicao= value.Data_Exibicao;
            },
                $("#modalGradeCompensacao").modal(true);
        });
    };
    //=======================Validacao de Programas
    $scope.ValidarPrograma = function (pParam) {
        if (!pParam.Competencia) {
            ShowAlert("Informe a Competência antes de selecionar o Programa");
            return;
        }
        var _url = 'AM/ListarGrade'
        _url += "?Cod_Veiculo=" + $scope.Parameters.Cod_Veiculo;
        _url += "&Competencia=" + pParam.Competencia;
        _url += "&Cod_Empresa=" + $scope.Parameters.Cod_Empresa;
        _url += "&Numero_Mr=" + $scope.Parameters.Numero_Mr;
        _url += "&Sequencia_Mr=" + $scope.Parameters.Sequencia_Mr;
        _url += "&Cod_Programa=" + pParam.Cod_Programa;
        _url += "&";
        httpService.Get(_url).then(function (response) {
            if (response.data.length==0) {
                pParam.Cod_Programa = "";
                ShowAlert("Não existe Grade para esse Programa");
            }
        });
    };
          

    //=======================Salvar a Compensação
    $scope.SalvarCompensacao = function (pParam) {
        console.log(pParam);
        var _url = "AM/SalvarCompensacao"
        var _data = {
            'Cod_Empresa': $scope.Parameters.Cod_Empresa,
            'Numero_Mr': $scope.Parameters.Numero_Mr,
            'Sequencia_Mr': $scope.Parameters.Sequencia_Mr,
            'Documento_Para': $scope.Parameters.Numero_Docto,
            'Cod_Veiculo': $scope.Parameters.Cod_Veiculo,
            'Competencia': pParam.Competencia,
            'Cod_Programa': pParam.Cod_Programa,
            'Data_Exibicao': pParam.Data_Exibicao,
            'Cod_Comercial': pParam.Cod_Comercial,
            'Duracao': pParam.Duracao,
            'Qtd_Compensacao': pParam.Quantidade
        };
        httpService.Post(_url, _data).then(function (response) {
            if (response) {
                if (response.data[0].Status == 0) {
                    ShowAlert(response.data[0].Mensagem)
                }
                else {
                    $scope.Compensacao_Temp = $scope.NewCompensacao();
                    //$scope.ShowInput = false;
                    // carregar p grid de compensacao
                    $scope.CarregarFalhasCompensacao();
                }
            }
        });
    };

    //=======================Salvar a Compensação
    $scope.GravarSolucao = function (pParam) {
        var _url = "AM/Solucao"
        var _data = {
            'Cod_Empresa': $scope.Parameters.Cod_Empresa,
            'Numero_Mr': $scope.Parameters.Numero_Mr,
            'Sequencia_Mr': $scope.Parameters.Sequencia_Mr,
            'Documento_Para': $scope.Parameters.Numero_Docto,
            'Cod_Veiculo': $scope.Parameters.Cod_Veiculo,
            'Qtd_Compensacao': pParam.Qtd_Total_Compensacao,
            'Solucao':pParam.Solucao
        };
        httpService.Post(_url, _data).then(function (response) {
            if (response) {
                if (response.data[0].Status == 1) {
                    ShowAlert(response.data[0].Mensagem);
                    $('.modal-backdrop').remove();
                    $location.path("/ConsultaAM");
                }
                else {
                    ShowAlert(response.data[0].Mensagem);
                    
                }
            }
        });
    };

    //======================Excluir Compensações
    $scope.ExcluirCompensacao = function (pCompensacao) {
        //$scope.pCompensacao.Cod_Veiculo = $scope.Parameters.Cod_Veiculo;
        swal({
            title: "Tem certeza que deseja Excluir esta Compensação ?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
                pCompensacao.Cod_Veiculo = $scope.Parameters.Cod_Veiculo;
                pCompensacao.Cod_Empresa = $scope.Parameters.Cod_Empresa;
                pCompensacao.Documento_Para = $scope.Parameters.Numero_Docto;
                pCompensacao.Numero_Mr = $scope.Parameters.Numero_Mr;
                pCompensacao.Sequencia_Mr = $scope.Parameters.Sequencia_Mr;
            httpService.Post("ExcluirCompensacao", pCompensacao).then(function (response) {
                if (response) {

                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $scope.Compensacao_Temp = $scope.NewCompensacao();
                        $scope.CarregarFalhasCompensacao();
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });

    };
    //=============================Encerrarmento da Am
    $scope.EncerrarAm = function (pCompensacao) {
        $scope.Solucao = [];
        if ($scope.FalhasCompensacao.Qtd_Total_Compensacao>0) {
            $scope.Solucao.push({ 'Id': 0, 'Descricao': 'Ponderar', 'Letra': 'P' });
            $scope.Solucao.push({ 'Id': 3, 'Descricao': 'Faturar', 'Letra': 'F' });
        }
        
        $scope.Solucao.push({ 'Id': 1, 'Descricao': 'Deduzir', 'Letra': 'D' });
        $scope.Solucao.push({ 'Id': 2, 'Descricao': 'Creditar', 'Letra': 'C' });
        
        if ($scope.FalhasCompensacao.Qtd_Total_Compensacao == 0) {
            $scope.Solucao.push({ 'Id': 4, 'Descricao': 'Faturar Sem Crédito', 'Letra': 'S' });
        }
        $("#ModalEncerramentoAm").modal(true);
    };
    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {

        $scope.CarregarFalhasCompensacao();
    });


}]);

