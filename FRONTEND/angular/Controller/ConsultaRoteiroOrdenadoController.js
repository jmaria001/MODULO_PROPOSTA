angular.module('App').controller('ConsultaRoteiroOrdenadoController', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location, $routeParams) {

    //Tipo_Break
    //0 = local
    //1 = Net
    //2 = Artistico
    //3= Pe 

    //===========================Inicializa Scopes
    $scope.NewFiltro = function () {
        return { 'Cod_Veiculo': '', 'Nome_Veiculo': '', 'Data_Exibicao': '', 'Programas': [] }
    }
    $scope.Filtro = $scope.NewFiltro();
    $scope.checkMarcar = false;
    $scope.Roteiro = "";
    $scope.Critica = "";
    $scope.ShowGuiaProgramas = false;
    $scope.ShowFiltro = true;
    $scope.CurrentTab = 'Midia'
    $scope.Consistencia = { 'Concorrencia': true, 'Outros': true, 'Rotativo': true };
    //===========================Carregar Guia de Programas
    $scope.CarregarGuiaProgramas = function (pFiltro) {
        if (!pFiltro.Cod_Veiculo || !pFiltro.Data_Exibicao) {
            ShowAlert("Favor Informar o Veículo e Data de Exibição");
            return;
        }
        var _Url = 'Roteiro/GuiaProgramacao';
        httpService.Post(_Url, pFiltro).then(function (response) {
            $scope.checkMarcar = false;
            $scope.Filtro.Programas = "";
            $scope.Roteiro = "";
            if (response.data.length == 0) {
                $scope.ShowGuiaProgramas = false;
                ShowAlert("Não existe Composição de Breaks  para esse Veiculo/Data")
            }
            else {
                $rootScope.routeName = "Roteiro Comercial ( Veiculo:" + pFiltro.Cod_Veiculo + "  Data:" + pFiltro.Data_Exibicao + " )";
                $scope.Filtro.Programas = response.data;
                $scope.ShowGuiaProgramas = true;
            }
        });
    };
    //===========================Carregar Roteiro
    $scope.CarregarRoteiro = function (pFiltro) {
        var Marcado = false
        for (var i = 0; i < $scope.Filtro.Programas.length; i++) {
            if ($scope.Filtro.Programas[i].Selected) {
                Marcado = true;
            }
        };
        if (!Marcado) {
            ShowAlert("Nenhum programa foi Selecionado");
            return;
        };
        var _Url = 'ConsultaRoteiroOrdenado/CarregarRoteiro';
        httpService.Post(_Url, pFiltro).then(function (response) {
            if (response.data.length == 0) {
                ShowAlert("Não existe Roteiro para esse Veiculo/Data");
            }
            else {
                $scope.Roteiro = response.data;
                $scope.ShowGuiaProgramas = false;
                $scope.ShowFiltro = false;

            }
        });
    };
    //===========================Mostra/Oculta Roteiro do programa
    $scope.fnShowHide = function (pId_Programa) {
        for (var i = 0; i < $scope.Roteiro.length; i++) {
            if ($scope.Roteiro[i].Id_Programa == pId_Programa) {
                $scope.Roteiro[i].Show = !$scope.Roteiro[i].Show;
            }
        }
    }
    //===========================Marcar/Desmarcar Guia de Progrmas
    $scope.MarcarProgramas = function (pValue) {
        for (var i = 0; i < $scope.Filtro.Programas.length; i++) {
            $scope.Filtro.Programas[i].Selected = pValue;
        }
    };
    //===========================CancelaRoteiro
    $scope.CancelarRoteiro = function () {
        $scope.Roteiro = "";
        $scope.Comerciais = "";
        $scope.Filtro = $scope.NewFiltro();
        $scope.ShowFiltro = true;
        $scope.ShowGuiaProgramas = false;
    };
    //===========================Mostrar todos os programas
    $scope.MostrarTodos = function (pRoteiro, pValue) {
        for (var i = 0; i < pRoteiro.length; i++) {
            pRoteiro[i].Show = pValue;
        }
    };
    //=================Renumera Itens do Roteiro e Comercial
    $scope.RenumeraItens = function (pRoteiro) {
        for (var i = 0; i < pRoteiro.length; i++) {
            pRoteiro[i].Id_Item = i;
        }
        for (var x = 0; x < $scope.Comerciais.length; x++) {
            $scope.Comerciais[x].Id_Item = x;
        }

        var _totalIntervalo = 0;
        var _totalBreak = 0
        var _totalArtistico = 0
        var _total_Encaixe_Programa = 0;
        var _total_Duracao_Programa = 0
        for (var i = pRoteiro.length - 1; i >= 0; i--) {
            if (pRoteiro[i].Indica_Comercial) {
                if (pRoteiro[i].Origem == 'Artistico') {
                    _totalArtistico += pRoteiro[i].Duracao;
                }
                else {
                    _totalIntervalo += pRoteiro[i].Duracao;
                }
                _totalBreak += pRoteiro[i].Duracao;
                _total_Encaixe_Programa += pRoteiro[i].Duracao;
            };
            if (pRoteiro[i].Indica_Titulo_Intervalo) {
                if (pRoteiro[i].Tipo_Break == '2') {
                    pRoteiro[i].Encaixe = _totalArtistico;
                    _totalArtistico = 0;
                }
                else {
                    pRoteiro[i].Encaixe = _totalIntervalo;
                    _totalIntervalo = 0;
                }
            };
            if (pRoteiro[i].Indica_Titulo_Break) {
                pRoteiro[i].Encaixe = _totalBreak;
                _total_Duracao_Programa += pRoteiro[i].Duracao;
                _totalBreak = 0;
                _totalArtistico = 0;
                _totalArtistico;
                _totalIntervalo
            };
            if (pRoteiro[i].Indica_Titulo_Programa) {
                pRoteiro[i].Encaixe = _total_Encaixe_Programa;
                pRoteiro[i].Duracao = _total_Duracao_Programa;
                _totalBreak = 0;
                _totalArtistico = 0;
                _totalArtistico;
                _totalIntervalo;
                var _total_Encaixe_Programa = 0;
                var _total_Duracao_Programa = 0
            };
        };
    };
 



    //===========================Imprimir Roteiro
    $scope.ImprimirRoteiro = function (pRoteiro) {
        var Marcado = false
        for (var i = 0; i < $scope.Filtro.Programas.length; i++) {
            if ($scope.Filtro.Programas[i].Selected) {
                Marcado = true;
            }
        };
        if (!Marcado) {
            ShowAlert("Nenhum programa foi Selecionado");
            return;
        };
        httpService.Post("Roteiro/ImprimirRoteiro/", pRoteiro).then(function (response) {
            if (response.data) {
                url = $rootScope.baseUrl + "PDFFILES/ROTEIRO/" + $rootScope.UserData.Login.trim() + "/" + response.data;
                var win = window.open(url, '_blank');
                win.focus();
            }
            else {
                ShowAlert("Não existe Roteiro a ser Impresso", "warning")
            }
        });
    };
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $rootScope.routeloading = false;
    });
}]);


