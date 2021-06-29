angular.module('App').controller('ApresentadoresCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Recebe Parametro
    $scope.PesquisaTabelas = { "Items": [], 'FiltroTexto': '', ClickCallBack: '', 'Titulo': '', 'MultiSelect': false };
    $scope.Parameters = $routeParams;
    $scope.Apresentador = "";
    $scope.ListadeProgramas = [];
    $scope.Apresentador.Programas = [];

    //$scope.Negociacao.Apresentadores = [];
    //$scope.ListaApresentadores = [];


    $scope.currentProgramas = 0;

    //========================Verifica Permissoes
    $scope.PermissaoDelete = false;
    httpService.Get("credential/Apresentador@Destroy").then(function (response) {
        $scope.PermissaoDelete = response.data;
    });
    //==========================Busca dados dos Apresentadores

    var _url = "GetApresentadoresData/" + $scope.Parameters.Id;
    httpService.Get(_url).then(function (response) {
        if (response) {
            $scope.Apresentador = response.data;
            if ($scope.Parameters.Action == "New") {
                $scope.Apresentador.Salario = "";
            }
        }
    });
           


    //==========================Salvar
    $scope.SalvarApresentador = function (pApresentador) {
        $scope.Apresentador.Id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("SalvarApresentadores", pApresentador).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/Apresentadores");
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };
    //======================Excluir
    $scope.ExcluirApresentador = function (pApresentador) {

        swal({
            title: "Tem certeza que deseja Excluir este  Apresentador ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("ExcluirApresentadores", pApresentador).then(function (response) {
                if (response) {

                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/Apresentadores");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });

    };
    // Aqui foi definindo funções para apresentadores
    $scope.SelecionarProgramas = function () {
        var _url = 'ListarTabela/Programa'
        httpService.Get(_url).then(function (response) {
            //$scope.PesquisaTabelas = {}
            if (response.data) {
                $scope.ListadeProgramas = response.data;
                if ($scope.Apresentador.Programas == null && $scope.Apresentador.Programas == undefined) {
                    $scope.Apresentador.Programas = 0;
                }
                for (var i = 0; i < $scope.Apresentador.Programas.length; i++) {
                    for (var y = 0; y < $scope.ListadeProgramas.length; y++) {
                        if ($scope.Apresentador.Programas[i].Cod_Programa == $scope.ListadeProgramas[y].Codigo) {
                            $scope.ListadeProgramas[y].Selected = true;
                        }
                    };
                };
                $scope.PesquisaTabelas.Items = $scope.ListadeProgramas;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção de Programas";
                $scope.PesquisaTabelas.MultiSelect = true;
                $scope.PesquisaTabelas.ClickCallBack = function () {
                    $scope.Apresentador.Programas = [];
                    for (var i = 0; i < $scope.ListadeProgramas.length; i++) {
                        if ($scope.ListadeProgramas[i].Selected) {
                            $scope.Apresentador.Programas.push({ 'Cod_Programa': $scope.ListadeProgramas[i].Codigo, 'Titulo': $scope.ListadeProgramas[i].Descricao });
                        }
                    };
                };
                $("#modalTabela").modal(true);
            };
        });


    }

    //=====================Clicou no X da lista de programas selecionados- remover programas
    $scope.RemoverProgramas = function (pCod_Programa) {
        for (var i = 0; i < $scope.Apresentador.Programas.length; i++) {
            if ($scope.Apresentador.Programas[i].Cod_Programa == pCod_Programa) {
                $scope.Apresentador.Programas.splice(i, 1);
                break;
            }
        }
    }



}]);


