angular.module('App').controller('Simulacao_List_Controller', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location) {

    //====================Inicializa scopes
    $scope.CurrentShow = "Filtro";
    $scope.ListStatus = "";
    $scope.gridheaders = [{ 'title': 'Edit', 'visible': true, 'searchable': false, 'config': false, 'sortable': false },
                            { 'title': 'Id', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Identificação', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Emp.Venda', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Tipo de Mídia', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Agência', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Cliente', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Contato', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Período Inicio', 'visible': true, 'searchable': false, 'config': true, 'sortable': true },
                            { 'title': 'Valor Tabela', 'visible': true, 'searchable': false, 'config': true, 'sortable': true },
                            { 'title': 'Valor Negociado', 'visible': true, 'searchable': false, 'config': true, 'sortable': true },
                            { 'title': 'Desconto Real.', 'visible': true, 'searchable': false, 'config': true, 'sortable': true },
                            { 'title': 'Autor', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Status', 'visible': true, 'searchable': true, 'config': true, 'sortable': true },
                            { 'title': 'Delete', 'visible': true, 'searchable': false, 'config': false, 'sortable': false }
    ];
    $scope.MesAnoKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    //====================Check se é Simulacao ou Proposta
    if ($rootScope.routeId == 'Proposta@Index') {
        $scope.Processo = 'P'
        $scope.Descricao_Processo = 'Proposta'
        $scope.NovoProcesso = 'Nova Proposta'
    }
    else {
        $scope.Processo = 'S'
        $scope.Descricao_Processo = 'Simulacao'
        $scope.NovoProcesso = 'Novo Modelo'
    }
    //====================Inicializa o Filtro
    $scope.NewFiltro = function () {
        if ($scope.Processo == 'P')
            localStorage.removeItem('PropostaFilter');
        else {
            localStorage.removeItem('SimulacaoFilter');
        }
        return {
            'Id_Simulacao': '',
            'Id_Status': '',
            'Validade_Inicio': '',
            'Validade_Termino': '',
            'Cod_Empresa_Venda': '',
            'Nome_Empresa_Venda': '',
            'Agencia': '',
            'Cliente': '',
            'Contato': '',
        }
    }

    //===========================Se ja tiver filtro anterior gravado
    if ($scope.Processo == 'P') {
        var _Filter = JSON.parse(localStorage.getItem('PropostaFilter'));
    } else {
        var _Filter = JSON.parse(localStorage.getItem('SimulacaoFilter'));
    }

    if (_Filter) {
        $scope.Filtro = _Filter;
    }
    else {
        $scope.Filtro = $scope.NewFiltro();
    }

    //====================Carrega Lista De Stutus
    httpService.Get("ListarTabela/Status_" + $scope.Descricao_Processo).then(function (response) {
        $scope.ListStatus = response.data;
    });

    //====================Permissoes
    $scope.PermissaoExcluir = 'false';
    $scope.PermissaoEditar = 'false';
    httpService.Get("credential/" + $scope.Descricao_Processo + "@Edit").then(function (response) {
        $scope.PermissaoEditar = response.data;
    });
    httpService.Get("credential/" + $scope.Descricao_Processo + "@Destroy").then(function (response) {
        $scope.PermissaoExcluir = response.data;
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
    $scope.CarregarSimulacao = function (pFiltro) {
        $scope.Simulacoes = [];
        if ($scope.Processo == 'P') {
            localStorage.setItem('PropostaFilter', JSON.stringify($scope.Filtro));
        }
        else {
            localStorage.setItem('SimulacaoFilter', JSON.stringify($scope.Filtro));
        }
        $scope.CurrentShow = '';
        $('#dataTable').dataTable().fnDestroy();
        var _url = 'ListSimulacao';
        _url += '?Id_Simulacao=' + pFiltro.Id_Simulacao;
        _url += '&Processo=' + $scope.Processo;
        _url += '&Id_Status=' + pFiltro.Id_Status;
        _url += '&Validade_Inicio=' + pFiltro.Validade_Inicio;
        _url += '&Validade_Termino=' + pFiltro.Validade_Termino;
        _url += '&Cod_Empresa_Venda=' + pFiltro.Cod_Empresa_Venda;
        _url += '&Agencia=' + pFiltro.Agencia;
        _url += '&Cliente=' + pFiltro.Cliente;
        _url += '&Contato=' + pFiltro.Contato;
        _url += '&';
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Simulacoes = response.data;
                if ($scope.Simulacoes.length == 0) {
                    $scope.RepeatFinished();
                }
            }
        });
    };

    //==================== Edicao da Simulacao
    $scope.EditarSimulacao = function (pIdSimulacao) {
        $location.path("/SimulacaoCadastro/Edit/" + pIdSimulacao + '/' + $scope.Processo)
    }
    //==================== Exclusao da Simulacao
    $scope.ExcluirSimulacao = function (pIdSimulacao) {
        swal({
            title: "Tem certeza que deseja Excluir essa Simulação?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim,Excluir",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post('SimulacaoDestroy', { 'Id_Simulacao': pIdSimulacao }).then(function (response) {
                if (response) {
                    $scope.CarregarSimulacao($scope.Filtro);
                    $scope.CurrentShow = "Grid";
                }
            });
        });
    };

    //====================Configuracao do Grid
    $scope.ConfiguraGrid = function () {
        param = {};
        param.language = fnDataTableLanguage();
        param.lengthMenu = [[7, 10, 25, 50, -1], [7, 10, 25, 50, "Todos"]];

        param.scrollCollapse = true;
        param.paging = true;
        param.dom = "<'row'<'col-sm-6'B><'col-sm-3'l><'col-sm-3'f>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        param.buttons = [
            { text: $scope.NovoProcesso + '<span class="fa fa-file margin-left-10"></span>', className: 'btn btn-primary btnNew', action: function (e, dt, button, config) { $('#btnNovaSimulacao').click(); } },
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
        var buttons = table.buttons([1]);
        if (table.rows({ selected: true }).indexes().length === 0) {
            buttons.disable();
        }
        else {
            buttons.enable();
        }
    };

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
                $scope.CarregarSimulacao(_Filter);
            }
        }
    });

    $scope.NovaSimulacao = function () {
        $location.path("/SimulacaoCadastro/New/0/" + $scope.Processo);
    }
    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });
}]);


