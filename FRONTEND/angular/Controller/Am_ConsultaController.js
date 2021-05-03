angular.module('App').controller('Am_ConsultaController', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location) {

    //====================Inicializa scopes
    // $scope.PermissaoNew = false;

    $scope.PosSit = [
        { 'id': 1, 'nome': 'Somente Pendente' },
        { 'id': 2, 'nome': 'Somente encerradas' },
        { 'id': 3, 'nome': 'Pendentes e Encerradas' }
    ]

    $scope.CurrentShow = "Filtro";

    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }

    $scope.gridheaders = [{ 'title': 'Botao1', 'visible': true, 'searchable': false, 'config': false, 'sortable': false },
    { 'title': 'Botao2', 'visible': true, 'searchable': false, 'config': false, 'sortable': false },
    { 'title': 'AM', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Negociação', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Agencia', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Cliente', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Produto', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Veiculo', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Período Campanha', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Contrato', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Tipo de Mídia', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    { 'title': 'Solução', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
    ];
    //====================Inicializa o Filtro
    $scope.Filtro = {};
    $scope.NewFiltro = function () {
        localStorage.removeItem('AmFilter');
        return {
            'Competencia': '',
            'Cod_Nucleo': '',
            'Cod_Contato': '',
            'Agencia': '',
            'Cliente': '',
            'Cod_Veiculo': '',
            'Cod_Programa': '',
            'Cod_Red_Produto': '',
            'Cod_Empresa_Venda': '',
            'Numero_Mr': '',
            'Sequencia': '',
            'Numero_Negociacao': '',
            'Situacao': 3
        }
    }
    //===========================Se ja tiver filtro anterior gravado
    var _Filter = JSON.parse(localStorage.getItem('AmFilter'));

    if (_Filter) {
        $scope.Filtro = _Filter;
    }
    else {
        $scope.Filtro = $scope.NewFiltro();
    }

    //====================Permissoes
    $scope.PermissaoReabrir = false;
    $scope.PermissaoReencaixe= false;
    httpService.Get("credential/AM@Reabrir").then(function (response) {
        $scope.PermissaoReabrir = response.data;
    });
    httpService.Get("credential/Am@Reencaixar").then(function (response) {
        $scope.PermissaoReencaixe = response.data;
    });

    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.CurrentShow = 'Grid';
        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        }, 1000)
    };

    //====================Carrega o Grid

    $scope.CarregarMapaReservaCompensacao = function (pFiltro) {
        
        if (!pFiltro.Competencia){
            ShowAlert("Competência é um filtro obrigatório");
            return ;
        }
        $scope.Ams = [];
        localStorage.setItem('AmFilter', JSON.stringify($scope.Filtro));
        $scope.CurrentShow = '';
        $('#dataTable').dataTable().fnDestroy();
        var _url = 'AM/List';
        _url += '?Competencia=' + pFiltro.Competencia;
        _url += '&Nucleo=' + pFiltro.Cod_Nucleo;
        _url += '&Contato=' + pFiltro.Cod_Contato;
        _url += '&Cliente=' + pFiltro.Cliente;
        _url += '&Agencia=' + pFiltro.Agencia;
        _url += '&Veiculo=' + pFiltro.Cod_Veiculo;
        _url += '&Programa=' + pFiltro.Cod_Programa;
        _url += '&Produto=' + pFiltro.Cod_Red_Produto;
        _url += '&Cod_Empresa_Venda=' + pFiltro.Cod_Empresa;
        _url += '&Numero_Mr=' + pFiltro.Numero_Mr;
        _url += '&Sequencia=' + pFiltro.Sequencia;
        _url += '&Numero_Negociacao=' + pFiltro.Numero_Negociacao;
        _url += '&Situacao=' + pFiltro.Situacao;
        _url += '&';
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Ams = response.data;
                if ($scope.Ams.length == 0) {
                    $scope.RepeatFinished();
                }
            }
        });
    };
    //==================== Novo MapaReserva Compensação
    $scope.NovaMapaReservaCompensacao = function () {
        $location.path("/MapaReservaCompensacaoCadastro/New/0")
    }
    //==================== Edicao da MapaReserva Compensação
    $scope.NovaMapaReservaCompensacao = function (pIdMapaReservaCompensacao) {
        $location.path("/MapaReservaCompensacaoCadastro/Edit/" + pIdMapaReservaCompensacao)
    }
    //====================Configuracao do Grid
    $scope.ConfiguraGrid = function () {
        param = {};
        param.language = fnDataTableLanguage();
        param.lengthMenu = [[7, 10, 25, 50, -1], [7, 10, 25, 50, "Todos"]];

        //param.scrollCollapse = true;
        param.paging = true;
        param.dom = "<'row'<'col-sm-6'B><'col-sm-3'l><'col-sm-3'f>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
            param.buttons = [
                //{ text: 'Novo Mapa Reserva', className: 'btn btn-primary btnNew', action: function (e, dt, button, config) { $('#btnNovoMapaReservaCompensacao').click(); } },
                {
                    text: 'Abrir no Excel<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning', extend: 'excel', exportOptions: {
                        columns: ':visible:not(:first-child)'
                    }
                },
                { text: 'Novo Filtro' + '<span class="fa fa-filter margin-left-10"></span>', className: 'btn btn-info', action: function (e, dt, button, config) { $('#btnNovoFiltro').click(); } },
            ];
        param.order = [[1, 'desc']];
        param.autoWidth = false;
        param.columns = [];
        for (var i = 0; i < $scope.gridheaders.length; i++) {
            param.columns.push({ "visible": $scope.gridheaders[i].visible, "searchable": $scope.gridheaders[i].searchable, "sortable": $scope.gridheaders[i].sortable });
        }
        $('#dataTable').DataTable(param);
        var table = $('#dataTable').DataTable();
        var buttons = table.buttons([0]);
        if (table.rows({ selected: true }).indexes().length === 0) {
            buttons.disable();
        }
        else {
            buttons.enable();
        }
    };


    //=======================Selecao de Tipo de Comercial
    $scope.PesquisaTipoComercial = function () {
        $scope.PesquisaTabelas = NewPesquisaTabela();
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
                $scope.TipoComercial_Temp.Descricao = response.data[0].Descricao;
            }
        })
    };
    //===========================Reabrir Am
    $scope.ReabrirAm = function (pCompensacao) {
        var _data = {
            'Cod_Empresa': pCompensacao.Cod_Empresa,
            'Numero_Mr': pCompensacao.Numero_MR,
            'Sequencia_Mr': pCompensacao.Sequencia_MR,
            'Documento_Para': pCompensacao.Numero_Docto,
        };

        swal({
            title: "Tem certeza que deseja reabrir essa AM ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Reabrir",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
                httpService.Post("AM/ReabrirAM", _data).then(function (response) {
                    if (response) {
                        $scope.Aviso = response.data[0];
                        if (response.data[0].Status == 1) {
                            ShowAlert(response.data[0].Mensagem);
                            $scope.CarregarMapaReservaCompensacao($scope.Filtro);
                        }
                        else {
                            ShowAlert(response.data[0].Mensagem, 'warning');
                        }
                    }
                })    
        });
    };
    $scope.Reencaixar = function (param) {
        if (param.Tem_Compensacao==1) {
            ShowAlert("Não é possivel reencaixar essa AM porque já tem Compensação !")
            return;
        }
        var _ulr = "/Am_Reencaixe/" + param.Cod_Empresa + "/" + param.Numero_MR + "/" + param.Sequencia_MR + "/" + param.Numero_Docto + "/" + param.Competencia + "/" + param.Cod_Veiculo;
        $location.path(_ulr)
    };
    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });

    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();

        if (_Filter) {
            var _Carregar = false
            angular.forEach(_Filter, function (value, key) {
                if (value) {
                    _Carregar = true;
                }
            });
            if (_Carregar) {
                $scope.CarregarMapaReservaCompensacao(_Filter);
            }
        }
    });

}]);


