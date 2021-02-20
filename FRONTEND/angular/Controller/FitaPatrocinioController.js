angular.module('App').controller('FitaPatrocinioController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {

    //====================Inicializa scopes
    $scope.CompetenciaKeys = { 'Year': new Date().getFullYear(), 'First': '', 'Last': '' }
    $scope.Filtro = {};
    $scope.ShowGrid = false;
    $scope.ShowDados = false;
    $scope.ShowContratos = false
    $scope.ShowFilter = true;
    $scope.FitasPatrocinio = [];
    $scope.FitasContratos = {};
    $scope.Contrato = {};
    $scope.Numeracao = {};
    $scope.Action = "";
    $scope.NewFiltro = function () {
        $scope.Filtro = {
            'CompetenciaInicial': CurrentMMYYYY(),  
            'CompetenciaFinal': CurrentMMYYYY(),
            'Cod_Programa': '',
            'Nome_Programa': '',
            'Cod_Veiculo': '',
            'Nome_Veiculo': '',
            'Indica_Pendente': true,
            'Indica_Numerada': true
        };
        $scope.ShowFilter = true;
        $scope.ShowGrid = false;    
    }
    $scope.NewFiltro();

    //========================Verifica Permissoes
    $scope.PermissaoNumerar= false;
    httpService.Get("credential/FitaPatrocino@Numerar").then(function (response) {
        $scope.PermissaoNumerar = response.data;
    });
    //========================Parametros do Grid
    $scope.ShowFilter = true;
    $scope.gridheaders = [{ 'Numerar': '', 'visible': true, 'searchable': false, 'sortable': false },
        { 'Competência': '', 'visible': true, 'searchable': true, 'sortable': true },
        { 'Veiculo': '', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Programa', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Tipo Comercial', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Periodo Exibição', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'N.Fita', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Duração.', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Dur. Cabeça', 'visible': true, 'searchable': true, 'sortable': true },
        //{ 'title': 'Dur. Total', 'visible': true, 'searchable': true, 'sortable': true },
        { 'title': 'Status', 'visible': true, 'searchable': true, 'sortable': true },
        { 'botoes': '', 'visible': true, 'searchable': false, 'sortable': false },
        //{ 'Excluir': '', 'visible': true, 'searchable': false, 'sortable': false },
        //{ 'Desativar': '', 'visible': true, 'searchable': false, 'sortable': false}
    ];

    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.ShowGrid = true;
        $scope.ShowFilter = false;
        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        }, 10)
    };
    //====================Carrega Fitas 
    $scope.CarregaFitasPatrocinio = function (pFiltro) {
        if (!pFiltro.CompetenciaFinal || !pFiltro.CompetenciaFinal) {
            ShowAlert("Competência Inicio e Término são filtros obrigatórios")
            return
        }
        $rootScope.routeloading = true;
        $scope.FitasPatrocinio= [];
        $scope.ShowGrid = false;
        $('#dataTable').dataTable().fnDestroy();
        
        httpService.Post('FitaPatrocinioListar', pFiltro).then(function (response) {
            if (response) {
                $scope.FitasPatrocinio = response.data;
                if ($scope.FitasPatrocinio.length == 0) {
                    $scope.RepeatFinished();
                };
            };
        });
    };
    //=======================Ver Contratos
    $scope.VerContratos = function (pfita) {
        $scope.Contrato = angular.copy(pfita);
        httpService.Post('FitaPatrocinioContratos', pfita).then(function (response) {
            if (response) {
                $scope.FitasContratos = response.data;
                $scope.ShowGrid = false;
                $scope.ShowContratos = true;
            };
        });
    };
    //====================Click em Numerar Fita
    $scope.NumerarFita = function (pFita, pAction) {
        $scope.Action = pAction;
        $scope.ShowFilter = false;
        $scope.ShowGrid = false;
        $scope.ShowDados= true;
        $scope.Numeracao = angular.copy(pFita);
    };
    //====================Formata o Numero da Fita
    $scope.FormataNumeroFita = function (pNumeroFita) {
        if (pNumeroFita) {
            pNumeroFita = pNumeroFita.replace(/[^0-9]/g, '')
            pNumeroFita = '000000' + pNumeroFita;
            pNumeroFita = pNumeroFita.slice(pNumeroFita.length - 6);
            pNumeroFita = 'CO' + pNumeroFita;
            $scope.Numeracao.Numero_Fita = pNumeroFita;
        };
    };
    //====================Procurar Numero de Fita
    $scope.ProcurarNumero = function (pNumeracao) {   
        httpService.Post('FitaPatrocinioProcurarFita', pNumeracao).then(function (response) {
            if (response.data) {
                if (response.data[0].Numero_Fita) {
                    pNumeracao.Numero_Fita= response.data[0].Numero_Fita;
                }
                else {
                    ShowAlert("Não foi encontrado Número de Fita Disponivel");
                };
            };
        });
    };
    //====================Cancelar Operacao de Numeracao
    $scope.CancelarNumeracao = function () {
        $scope.Numeracao = {}
        $scope.ShowDados = false;
        $scope.ShowGrid = true; 
    };
    //====================Confirmar a Numeracao 
    $scope.ConfirmarNumeracao = function (pNumeracao) {
        httpService.Post('FitaPatrocinioGravar', pNumeracao).then(function (response) {
            if (response.data) {
                if (response.data[0].Status) {
                    ShowAlert('Dados Gravados com Sucesso.');
                    $scope.CarregaFitasPatrocinio($scope.Filtro);
                    $scope.CancelarNumeracao();
                }
                else {
                    ShowAlert(response.data[0].Mensagem);
                };
            };
        });
    };
    //====================Desativar a Fita
    $scope.DesativarFita = function (pFita) {
        swal({
            title: "Confirma a Desativação da Fita "  + pFita.Numero_Fita,
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Desativar",
            cancelButtonText: "Não,Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post('FitaPatrocinioDesativar', pFita).then(function (response) {
                if (response.data) {
                    pFita.Indica_Desativada = true;
                };
            });
        });
    };
    //====================Excluir a Fita
    $scope.ExcluirFita = function (pFita) {
        swal({
            title: "Confirma a Exclusão  da Fita " + pFita.Numero_Fita,
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir",
            cancelButtonText: "Não,Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post('FitaPatrocinioExcluir', pFita).then(function (response) {
                if (response.data) {
                    $scope.CarregaFitasPatrocinio($scope.Filtro);
                };
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
            {
                text: 'Abrir no Excel<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-primary', extend: 'excel', exportOptions: {
                    columns: ':visible:not(:first-child)'
                }
            },
            { text: 'Retornar', className: 'btn btn-warning', action: function (e, dt, button, config) { $('#btnRetornar').click(); } },
        ];
        param.order = [[0, 'desc']];
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
    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.ConfiguraGrid();
    });

}]);

