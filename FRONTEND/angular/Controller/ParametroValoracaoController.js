angular.module('App').controller('ParametroValoracaoController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {

    //========================Verifica Permissoes
    $scope.PermissaoNew = false;
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.PermissaoEdit = false;
    $scope.ExcluirTipoComercial = [];
    httpService.Get("credential/ParametroValoracao@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/ParametroValoracao@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });
    $scope.PermissaoExcluir = 'false';
    httpService.Get("credential/ParametroValoracao@Destroy").then(function (response) {
        $scope.PermissaoExcluir = response.data;
    });
    $scope.PosTipo = [
        { 'id': 'Normal', 'nome': 'Normal' },
        { 'id': 'Merchandising', 'nome': 'Merchandising' },
        { 'id': 'MidiaOnline', 'nome': 'MidiaOnline' }
    ]



    //====================Inicializa scopes
    $scope.Filtro = {};
    $scope.NewFiltro = function () {
        $scope.Filtro = { 'Competencia': '','Programa': '','TipoParametroValoracao': ''};
        localStorage.removeItem('ParametroValoracaoFilter');
    }
    $scope.TipoValoracao = [{ 'Codigo': 1, 'Nome': 'Por Duração' }, { 'Codigo': 2, 'Nome': 'Proporcional' }];
    $scope.TipoParametroValoracao = [{ 'Codigo': 1, 'Nome': 'Normal' }, { 'Codigo': 2, 'Nome': 'Merchandising' }, { 'Codigo': 3, 'Nome': 'Mida Online' }];

    $scope.NewTipoComercial = function () {
        return {
            'Cod_Tipo_Comercial' :'',
            'Descricao': '',
            'Vlr_Parametro': '',
            'Indica_Vlr_Duracao': '',
            'Indica_Vlr_Proporcional': '',
            'Tipo_Valoracao': '',
            'Indica_Desativado': false
        }
    }
    $scope.TipoComercial_Temp = $scope.NewTipoComercial();
    //Definindo Duracao
    $scope.NewDuracao = function () {
        return {
            'Duracao': '',
            'Vlr_Parametro': '',
            'Indica_Desativado': false
        }
    }
    $scope.Duracao_Temp = $scope.NewDuracao();



    //Fim 
    $scope.Operacao = "";
    $scope.PesquisaTabelas = { "Items": [], 'FiltroTexto': '', ClickCallBack: '', 'Titulo': '', 'MultiSelect': false };
    //======================Verifica se tem filtro anterior
    var _Filter = JSON.parse(localStorage.getItem('ParametroValoracaoFilter'));
    if (!_Filter) {
        $scope.NewFiltro()
    }

    $scope.ShowGrid = false;
    //========================Parametros do Grid
    $scope.ShowFilter = true;
    //$scope.gridheaders = [{ 'title': '', 'visible': true, 'searchable': false, 'sortable': false },
    //{ 'title': 'Competencia', 'visible': true, 'searchable': true, 'sortable': true },
    //{ 'title': 'Cod_Empresa', 'visible': true, 'searchable': true, 'sortable': true },
    //{ 'title': 'Cod_Programa', 'visible': true, 'searchable': true, 'sortable': true },
    ////{ 'title': 'Titulo', 'visible': true, 'searchable': true, 'sortable': true },
    //];


    //====================Quando terminar carga do grid, torna view do grid visible
    //$scope.RepeatFinished = function () {
    //    $rootScope.routeloading = false;
    //    $scope.ConfiguraGrid();
    //    $scope.ShowGrid = true;
    //    setTimeout(function () {
    //        $("#dataTable").dataTable().fnAdjustColumnSizing();
    //    }, 10)
    //};

    //====================Carrega o Grid
    $scope.CarregarParametroValoracao = function (pFiltro) {
        if (!pFiltro.Competencia) {
            ShowAlert("Filtro Competência é obrigatório");
            return;
        }
            if (!pFiltro.TipoParametroValoracao) {
                ShowAlert("Filtro tipo de parâmetro de valoração é obrigatório");
                return;
        }

        $rootScope.routeloading = true;
        $scope.ParametroValoracao = [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();
        var _url = 'ParametroValoracaoListar'
        _url += '?Competencia=' + pFiltro.Competencia;
        _url += '&Programa=' + pFiltro.Programa;
        _url += '&TipoParametroValoracao=' + pFiltro.TipoParametroValoracao;
        _url += '&';
        
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.ParametroValoracao= response.data;
                $scope.ShowGrid = true;
                $scope.ShowFilter   = false;
            }
            localStorage.setItem('ParametroValoracaoFilter', JSON.stringify($scope.Filtro));
        });
    };

    $scope.CancelaEdicao = function () {
        $scope.Operacao = "";
        $scope.TipoComercial_Temp = $scope.NewTipoComercial();
        $scope.Duracao_Temp = $scope.NewDuracao();
    };
    //=======================Selecao de Tipo de Comercial
    $scope.PesquisaTipoComercial = function () {
        $scope.listaTipoComercial = ""
        var _url = 'ListarTabela/Tipo_Comercial'
        httpService.Get(_url).then(function (response) {
            $scope.PesquisaTabelas.Items = response.data;
            $scope.PesquisaTabelas.FiltroTexto = ""
            $scope.PesquisaTabelas.PreFilter = false;
            $scope.PesquisaTabelas.Titulo = "Seleção de Tipos de Comerciais"
            $scope.PesquisaTabelas.MultiSelect = false;
            $scope.PesquisaTabelas.ClickCallBack = function (value) {
                $scope.TipoComercial_Temp.Cod_Tipo_Comercial = value.Codigo, $scope.TipoComercial_Temp.Descricao = value.Descricao;
            },
                $("#modalTabela").modal(true);
        });
    };
    //======================Validar Tipo de Comercial
    $scope.ValidarTipoComercial = function (pCodigo) {
        pCodigo = pCodigo.toUpperCase();
        $scope.TipoComercial_Temp.Cod_Tipo_Comercial = pCodigo.toUpperCase();
        for (var i = 0; i < $scope.ParametroValoracao.Tipo_Comercial.length; i++) {
            if ($scope.ParametroValoracao.Tipo_Comercial[i].Cod_Tipo_Comercial == pCodigo) {
                ShowAlert("Tipo de Comercial já Cadastrado", "warning")
                $scope.TipoComercial_Temp.Cod_Tipo_Comercial = "";
                return;
            };
        };
        var _url = "ValidarTabela/Tipo_Comercial/" + pCodigo.trim()
        httpService.Get(_url).then(function (response) {
            if (response.data[0].Status == 0) {
                ShowAlert(response.data[0].Mensagem, 'warning', 2000);
                $scope.TipoComercial_Temp.Cod_Tipo_Comercial = "";

            }
            else {
                $scope.TipoComercial_Temp.Descricao= response.data[0].Descricao;
            }
        })
    };

    //////======================Validar Duracao
    $scope.ValidarDuracao = function (pDuracao) {
        $scope.Duracao_Temp.Duracao = pDuracao;
        for (var i = 0; i < $scope.ParametroValoracao.Duracao.length; i++) {
            if ($scope.ParametroValoracao.Duracao[i].Duracao == pDuracao) {
                ShowAlert("Duração já Cadastrado", "warning")
                $scope.Duracao_Temp.Duracao = "";
                return;
            };
        };
     };

    //=======================Salvar Tipo de Comercial 
    $scope.SalvarParametro = function (pTipoComercial) {
        //pTipoComercial.Cod_Empresa = $scope.ParametroValoracao.Cod_Empresa;

        pTipoComercial.Cod_Empresa = "999";
        pTipoComercial.Cod_Programa = $scope.ParametroValoracao.Cod_Programa;
        pTipoComercial.Competencia = $scope.ParametroValoracao.Competencia;
        pTipoComercial.TipoParametroValoracao = $scope.ParametroValoracao.TipoParametroValoracao;

        httpService.Post("SalvarParametroValoracao", pTipoComercial).then(function (response) {

            if (response.data[0].Status) {
                ShowAlert(response.data[0].Mensagem, 'success');
                $scope.Operacao = "";
                $scope.TipoComercial_Temp = $scope.NewTipoComercial();

                $scope.CarregarParametroValoracao(pTipoComercial);
            }
            else {
                ShowAlert(response.data[0].Mensagem, "warn+ing");
            }
        });
      
    };


    //=======================Salvar Duracao
    $scope.SalvarParametroDuracao = function (pDuracao) {
        //pTipoComercial.Cod_Empresa = $scope.ParametroValoracao.Cod_Empresa;

        pDuracao.Cod_Empresa = "999";
        pDuracao.Cod_Programa = $scope.ParametroValoracao.Cod_Programa;
        pDuracao.Competencia = $scope.ParametroValoracao.Competencia;
        pDuracao.TipoParametroValoracao = $scope.ParametroValoracao.TipoParametroValoracao;
        httpService.Post("SalvarParametroValoracaoDuracao", pDuracao).then(function (response) {

            if (response.data[0].Status) {
                ShowAlert(response.data[0].Mensagem, 'success');
                $scope.Operacao = "";
                $scope.Duracao_Temp = $scope.NewDuracao();

                $scope.CarregarParametroValoracao(pDuracao);
               

            }
            else {
                ShowAlert(response.data[0].Mensagem, "warn+ing");
            }
        });

    };

    $scope.ParametroExcluirTipoComercial = function (pCod_Comercial) {
        pCod_Comercial.Cod_Empresa = "999";
        pCod_Comercial.Cod_Programa = $scope.ParametroValoracao.Cod_Programa;
        pCod_Comercial.Competencia = $scope.ParametroValoracao.Competencia;
        pCod_Comercial.TipoParametroValoracao = $scope.ParametroValoracao.TipoParametroValoracao;

        swal({
            title: "Tem certeza que deseja Excluir esse Tipo Comercial?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim,Excluir",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
          

        }, function () {
                httpService.Post('ParametroExcluirTipoComercial', pCod_Comercial ).then(function (response) {
                    if (response) {

                        if (response.data[0].Status) {

                            ShowAlert(response.data[0].Mensagem, 'success');
                            $scope.CarregarParametroValoracao(pCod_Comercial);
                        }
                        else {
                            ShowAlert(response.data[0].Mensagem, 'warning');
                        }
                    }
                });
        });
    };

    $scope.ParametroExcluirDuracao = function (pDuracao) {
        pDuracao.Cod_Empresa = "999";
        pDuracao.Cod_Programa = $scope.ParametroValoracao.Cod_Programa;
        pDuracao.Competencia = $scope.ParametroValoracao.Competencia;
        pDuracao.TipoParametroValoracao = $scope.ParametroValoracao.TipoParametroValoracao;
        swal({
            title: "Tem certeza que deseja Excluir essa Duração?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim,Excluir",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true


        }, function () {
                httpService.Post('ParametroExcluirDuracao', pDuracao).then(function (response) {
                if (response) {

                    if (response.data[0].Status) {

                        ShowAlert(response.data[0].Mensagem, 'success');
                        $scope.CarregarParametroValoracao(pDuracao);
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            });
        });
    };



}]);

