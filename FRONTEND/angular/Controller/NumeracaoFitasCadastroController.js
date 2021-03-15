angular.module('App').controller('NumeracaoFitasCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {
    //====================Inicializa scopes
    $scope.ShowGrid = false;
    $scope.NumeroFitasS = "";
    //$scope.NumeroFitas = "";
    $scope.NumeroFitas = [];

    $scope.NumeracaoFitas = "";
    $scope.NumeracaoFitasS = "";
        
    //====================Carrega o Grid de fitas por Veiculo / Numeracao
    $scope.CarregarNumerarFitas = function (pFita) {
        $rootScope.routeloading = true;
        $scope.NumeracaoFitasS = [];
        $scope.ShowGrid = '';
        $('#dataTable').dataTable().fnDestroy();
        var _url = "ExibirVeiculosFitas"
        _url += "?Cod_Empresa=" + $scope.Parameters.Cod_Empresa;
        _url += "&Numero_Mr=" + $scope.Parameters.Numero_Mr;
        _url += "&Sequencia_Mr=" + $scope.Parameters.Sequencia_Mr;
        _url += "&Cod_Comercial=" + $scope.Parameters.Cod_Comercial;
        _url += "&Cod_Tipo_Comercial=" + $scope.Parameters.Cod_Tipo_Comercial;
        _url += "&Cod_Tipo_Midia=" + $scope.Parameters.Cod_Tipo_Midia;
        _url += "&";
        httpService.Get(_url).then(function (response) {
            if (response) {
                //$scope.NumeroFitasS = response.data;
                $scope.NumeracaoFitasS = response.data;
                
                $scope.CurrentShow = 'Grid';
              //  $scope.NumeroFitasS = response.data;
            }
        });
    };
    //====================Formata o Numero da Fita
    $scope.FormataNumeroFita = function (pNumeroFita) {
        if (pNumeroFita.Numero_Fita) {
            pNumeroFita.Numero_Fita = pNumeroFita.Numero_Fita.replace(/[^0-9]/g, '')
            pNumeroFita.Numero_Fita = '000000' + pNumeroFita.Numero_Fita;
            pNumeroFita.Numero_Fita = pNumeroFita.Numero_Fita.slice(pNumeroFita.Numero_Fita.length - 6);
            pNumeroFita.Numero_Fita = 'CO' + pNumeroFita.Numero_Fita;
        };
    };
    //=====================Carregar Numero de Fita 
    $scope.CarregarNumero = function (pNumero_Fita) {
        var vCod_Veiculo = pNumero_Fita.Cod_Veiculo;
        var tipo_fita = 'CO';
        if (pNumero_Fita.Cod_Veiculo == null && pNumero_Fita.Cod_Veiculo == undefined) {
            ShowAlert("Para utilizar numeração automática, primeiro selecione um veiculo");
            return;
        }
        var _data = {
            'Cod_Veiculo': pNumero_Fita.Cod_Veiculo,
            'Tipo_Fita': tipo_fita,
            'Cod_Tipo_Midia': pNumero_Fita.Cod_Tipo_Midia,
            'Cod_Tipo_Comercial': pNumero_Fita.Cod_Tipo_Comercial
        };
        httpService.Post("RangeFitaNumeracao", _data).then(function (response) {
            if (response) {
                if (response.data[0].Status == 0) {
                    ShowAlert('Veículo não esta parametrizado corretamente em Paramêtros de Numeração de Fitas');
                    return;
                }
                else {
                    for (var i = 0; i < $scope.NumeracaoFitasS.length; i++) {

                        if (vCod_Veiculo == $scope.NumeracaoFitasS[i].Cod_Veiculo) {
                            $scope.NumeracaoFitasS[i].Numero_Fita = response.data[0].Numero_Fita;
                        };
                    };
                };
            };
        });
    };

    //=======================Validacao de Apresentadores
    $scope.ValidarApresentador = function (pParam) {
        
        var _url = 'NumeracaoFitasValidarApresentador'
        _url += "?Cod_Apresentador=" + pParam.Cod_Apresentador;
        _url += "&";
        httpService.Get(_url).then(function (response) {
            if (response.data.length == 0) {
                pParam.Cod_Programa = "";
                ShowAlert("Não existe Apresentador");
            }
        });
    };
    //=======================Selecao de Apresentadores
    $scope.PesquisaApresentador = function (pParam) {
        var vCod_Veiculo = pParam.Cod_Veiculo;
        $scope.PesquisaTabelas = NewPesquisaTabela();
        $scope.listaApresentador = ""
        var _url = 'NumeracaoFitasApresentador'
        _url += "?Cod_Apresentador=" + $scope.Parameters.Cod_Apresentador;
        _url += "&";
        httpService.Get(_url).then(function (response) {
            $scope.PesquisaTabelas.Items = response.data;
            $scope.PesquisaTabelas.FiltroTexto = ""
            $scope.PesquisaTabelas.PreFilter = false;
            $scope.PesquisaTabelas.Titulo = "Seleção de Apresentador"
            $scope.PesquisaTabelas.MultiSelect = false;
            $scope.PesquisaTabelas.ClickCallBack = function (value) {
                for (var i = 0; i < $scope.NumeracaoFitasS.length; i++) {
                    if (vCod_Veiculo == $scope.NumeracaoFitasS[i].Cod_Veiculo) {
                        $scope.NumeracaoFitasS[i].Cod_Apresentador = value.Codigo;
                    };
                };
            },
                $("#modalTabela").modal(true);
        });
    };
    //======================= Gravar as fitas numeradas
    $scope.SalvarFitasNumeradas = function (pParam) {
        for (var i = 0; i < pParam.length; i++) {
            if (pParam[i].Numero_Fita == null && pParam[i].Numero_Fita == undefined) {
                ShowAlert("Veículo:" + pParam[i].Cod_Veiculo + " Nenhum Numero de Fita foi Informado");
                return;
            };
        };
        httpService.Post("SalvarNumeracaoFitas", pParam).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/NumeracaoFitas");
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                };
            };
        })
    };
    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $rootScope.routeloading = false;
                $scope.CarregarNumerarFitas();
    });

}]);



