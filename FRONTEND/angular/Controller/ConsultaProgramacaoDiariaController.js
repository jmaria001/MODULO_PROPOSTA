angular.module('App').controller('ConsultaProgramacaoDiariaController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout','$routeParams', function ($scope, $rootScope, httpService, $location, $timeout) {
    //====================Inicializa scopes
    $scope.Filtro = {};
    $scope.NewFiltro = function () {
        $scope.Filtro = {
            'Data Inicial': '', 'Data Final': '', 'Veiculo': '', 'Nome_Veiculo': '', 'Programa': '', 'Titulo': '', 'Indica_Local': true, 'Indica_Net': true, 'Indica_Progs_Saldo_Zero': true, 'Indica_Progs_Saldo_Posit': true, 'Indica_Progs_Saldo_Estou': true, 'Indica_Progs_Desativados':true };
        localStorage.removeItem('ConsultaProgramacaoDiariaFilter');
    }
    //---Verifica se tem filtro anterior
    var _Filter = JSON.parse(localStorage.getItem('ConsultaProgramacaoDiariaFilter'));
    if (!_Filter) {
        $scope.NewFiltro()
    }
    //--
    $scope.ShowGrid = false;
    $scope.ShowFilter = true;
    //---Grid
    $scope.gridheaders = [
        { 'title': 'Veiculo', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Programa', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Horario_Inicial', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Horario_Final', 'visible': true, 'searchable': false, 'sortable': false },
        { 'title': 'Data_Exibicao', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Indica_Grade', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Dispo', 'visible': true, 'searchable': false, 'sortable': false },
        { 'title': 'Absorvido', 'visible': true, 'searchable': true, 'sortable': true },
        //{ 'title': 'Reservado', 'visible': true, 'searchable': true, 'sortable': true },
        //{ 'title': 'Invasao', 'visible': true, 'searchable': false, 'sortable': false },
        { 'title': 'Saldo', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Detalhe', 'visible': true, 'searchable': true, 'sortable': true },
    ];

    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.ShowGrid = true;
        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        }, 10)
    };

    //====================Carrega o Grid
    $scope.CarregarConsultaProgramacaoDiaria = function (pFiltro) {
        
        //====Consistencias
        if (!pFiltro.Data_Inicial) {
            ShowAlert("Filtro Data Inicial é obrigatório");
            return;
        }
        if (!pFiltro.Data_Final) {
            ShowAlert("Filtro Data Final é obrigatório");
            return;
        }
        if (!pFiltro.Veiculo) {
            ShowAlert("Filtro Veículo é obrigatório");
            return;
        }
        if (!pFiltro.Indica_Progs_Saldo_Zero && !pFiltro.Indica_Progs_Saldo_Posit && !pFiltro.Indica_Progs_Saldo_Estou) {
            ShowAlert("Marcar pelo menos uma das opções de Saldo");
            return;
        }

        var _dia = parseInt(pFiltro.Data_Inicial.substr(0, 2));
        var _mes = parseInt(pFiltro.Data_Inicial.substr(3, 2));
        var _ano = parseInt(pFiltro.Data_Inicial.substr(6, 4));
        var _diaInicio = new Date(_ano, _mes - 1, _dia)

        _dia = parseInt(pFiltro.Data_Final.substr(0, 2));
        _mes = parseInt(pFiltro.Data_Final.substr(3, 2));
        _ano = parseInt(pFiltro.Data_Final.substr(6, 4));
        var _diaFim = new Date(_ano, _mes - 1, _dia)
        var _diaFim = new Date(_ano, _mes - 1, _dia)

        if (_diaFim < _diaInicio) {
            ShowAlert("Data Final está menor que a Data Inicial");
            return;
        }

        const diffTime = Math.abs(_diaFim - _diaInicio);
        const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));

        if (diffDays > 120) {
            ShowAlert("O período de datas não pode ser maior que 120 dias");
            return;
        }

        //====
        $rootScope.routeloading = true;
        $scope.ConsultaProgramacaoDiarias = [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();

        var _url = 'ConsultaProgramacaoDiariaListar'
        _url += '?Veiculo=' + pFiltro.Veiculo;
        _url += '&Data_Inicial=' + pFiltro.Data_Inicial;
        _url += '&Data_Final=' + pFiltro.Data_Final;
        _url += '&Programa=' + pFiltro.Programa;
        _url += '&Indica_Progs_Saldo_Zero=' + pFiltro.Indica_Progs_Saldo_Zero;
        _url += '&Indica_Progs_Saldo_Estou=' + pFiltro.Indica_Progs_Saldo_Estou;
        _url += '&Indica_Progs_Saldo_Posit=' + pFiltro.Indica_Progs_Saldo_Posit;
        _url += '&Par_Indica_Sem_Disponibilidade=true';
        _url += '&Par_Indica_Invasao_Espaco=true';
        _url += '&Indica_Progs_Desativados=' + pFiltro.Indica_Progs_Desativados;
        _url += '&Par_Programa_chkVendaSP=false';
        _url += '&Indica_Local=' + pFiltro.Indica_Local;
        _url += '&Indica_Net=' + pFiltro.Indica_Net;
        _url += '&Par_Tipo_Retorno=false';
        _url += '&';
        $scope.ShowFilter = false;
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.ConsultaProgramacaoDiarias = response.data;
                if ($scope.ConsultaProgramacaoDiarias.length == 0) {
                    $scope.RepeatFinished();
                }
            }
            localStorage.setItem('ConsultaProgramacaoDiariaFilter', JSON.stringify($scope.Filtro));
        });
    };

    //====================Funcao para configurar o Grid
    $scope.ConfiguraGrid = function () {
        param = {};
        param.language = fnDataTableLanguage();
        param.lengthMenu = [[7, 10, 25, 50, -1], [7, 10, 25, 50, "Todos"]];
        param.pageLength = 7;
        param.scrollCollapse = true;
        param.paging = true;

        param.dom = "<'row'<'col-sm-3'l><'col-sm-4'f><'col-sm-5'B>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
            param.buttons = [
                {
                    text: 'Exportar<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning HideButton', extend: 'excel', exportOptions: {
                        columns: ':visible:not(:first-child)'
                    }
                }
            ];
        param.order = [[4, 'asc'],[0,'asc'],[5,'asc']];
        param.autoWidth = false;
        param.columns = [];
        for (var i = 0; i < $scope.gridheaders.length; i++) {
            param.columns.push({ "visible": $scope.gridheaders[i].visible, "searchable": $scope.gridheaders[i].searchable, "sortable": $scope.gridheaders[i].sortable });
        }
        $('#dataTable').DataTable(param);
    };
    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });

    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.ConfiguraGrid();
        if (_Filter) {
            $scope.Filtro = _Filter;
            $scope.CarregarConsultaProgramacaoDiaria($scope.Filtro);
        }
    });
}]);

