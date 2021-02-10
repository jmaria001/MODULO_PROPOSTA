angular.module('App').controller('EnvioPlayListController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {

    //------------------- Inicializa Scopes --------------------
    $scope.ShowFiltro = true;
    $scope.ShowDados = false;
    $scope.Parametros = "";
    $scope.ParametrosDados = "";
    $scope.DownloadUrl = "";
    //------------------- Novo Filtro ------------------------
    $scope.NewFilter = function () {
        $scope.Parametros = { 'Cod_Veiculo': '', 'Nome_Veiculo': '', 'Data_Programacao': '', 'Exibidor': '', 'Nome_Arquivo': '' };
        $scope.ParametrosDados = "{'Exibidor': '', 'Nome_Arquivo': '','Posicao':'','Tamanho':''}";
        $scope.ShowFiltro = true;
        $scope.ShowDados = false;
        $scope.DownloadUrl = "";
    };
    $scope.NewFilter();

    //------------------- Carrega os Dados ------------------------
    $scope.EnvioPlayListCarregar = function (pParam) {
        //-----Consistencias
        if (!pParam.Cod_Veiculo || !pParam.Data_Programacao) {
            //ShowAlert("Código do Veículo e Data da Programação são Obrigatórios");
            return;
        };
        $scope.DownloadUrl = "";
        $scope.Parametros.Exibidor = "";
        $scope.Parametros.Nome_Arquivo = "";
        $scope.ShowDados = false;
        //-----Faz Consulta
        var _url = "EnvioPlayListFiltrar";
        _url += "?Cod_Veiculo=" + pParam.Cod_Veiculo;
        _url += "&Nome_Veiculo=" + pParam.Nome_Veiculo;
        _url += "&Data_Programacao=" + pParam.Data_Programacao;
        _url += "&=";
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Parametros = response.data;
                $scope.ShowFiltro = true;
                $scope.ShowDados = false;
            }
        });
    };


    //-------------------- Alterar Parametros ---------------------------
    $scope.AlterarParametros = function (pParam) {
        //-----Consistencias
        if (!pParam.Cod_Veiculo) {
            ShowAlert("Código do Veículo é Obrigatório");
            return;
        }
        $scope.ShowParametros = '';
        //-----Faz Consulta
        var _url = "EnvioPlayListFiltrarParametros";
        _url += "?Cod_Veiculo=" + pParam.Cod_Veiculo;
        _url += "&=";
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.ParametrosDados = response.data;
                $scope.ShowFiltro = false;
                $scope.ShowDados = true;
            }
        });
    };

    //-------------------- Salvar Parametros -----------------------
    $scope.SalvarParametros = function (pParametro) {
        httpService.Post("EnvioPlayListSalvarParametros", pParametro).then(function (response) {
            if (response.data) {
                ShowAlert(response.data[0].Mensagem)
                if (response.data[0].Status == 1) {
                    $scope.ParametrosDados = "{'Exibidor': '', 'Nome_Arquivo': '','Posicao':'','Tamanho':''}";
                    $scope.ShowFiltro = true;
                    $scope.ShowDados = false;
                    $scope.EnvioPlayListCarregar($scope.Parametros);
                }
            }
        });
    };

    //-------------------- Cancelar Parametros -----------------------
    $scope.Cancelar = function () {
        $scope.ParametrosDados = "{'Exibidor': '', 'Nome_Arquivo': '','Posicao':'','Tamanho':''}";
        $scope.ShowFiltro = true;
        $scope.ShowDados = false;
        $scope.DownloadUrl = "";
    };

    //-------------------------Gerar os arquivos
    $scope.GerarArquivos = function (pParam) {
        if (!pParam.Data_Programacao || !pParam.Cod_Veiculo) {
            ShowAlert("Favor informar o veiculo e a data da Programação");
            return;
        };
        if (!pParam.Exibidor) {
            ShowAlert("Sistema Exibidor não está parametrizado para esse Veículo.");
            return;
        }
        httpService.Post('EnvioPlayGerarArquivo', pParam).then(function (response) {
            if (response) {
                if (!response.data.Status) {
                    ShowAlert(response.data.Mensagem);
                }
                else {
                    $scope.DownloadUrl = $rootScope.baseUrl + response.data.Url;
                    //var strUrl = $rootScope.baseUrl + response.data.Url;
                    //var win = window.open(strUrl, '_blank');
                    //win.focus();
                    
                }
            };
        });
    };
    //---------Quando mudar o veiculo ou data, carrega os parametros abaixo desses campos 
    $scope.$watch('[Parametros.Cod_Veiculo,Parametros.Data_Programacao]', function (newValue, oldValue) {
        if (newValue != oldValue) {
            $scope.DownloadUrl = "";
            $scope.EnvioPlayListCarregar($scope.Parametros)
        }
    });
}]);



