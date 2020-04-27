angular.module('App').controller('ProgramaCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Recebe Parametro
    $scope.PesquisaTabelas = { "Items": [], 'FiltroTexto': '', ClickCallBack: '', 'Titulo': '', 'MultiSelect': false };
    $scope.Parameters = $routeParams;
    $scope.Programa = "";
    $scope.ListadeApresentadores = [];
    $scope.Programa.Veiculos = [];
    $scope.Programa.Apresentador = [];
    $scope.currentApresentador = -1;
    $scope.currentVeiculos = 0;

    //========================Verifica Permissoes
    $scope.PermissaoDelete= false;
    httpService.Get("credential/Programa@Destroy").then(function (response) {
        $scope.PermissaoDelete = response.data;
    });
    //==========================Busca dados das Programa

    var _url = "GetProgramaData/" + $scope.Parameters.Id;
    httpService.Get(_url).then(function (response) {
        if (response) {
            $scope.Programa = response.data;
            if ($scope.Parameters.Action=="New") {
                $scope.Programa.RedeId = "";
                $scope.Programa.Cod_N_JOVE = "";
                $scope.Programa.Qtd_Cotas = "";
            }
        }
    });

    //==========================Salvar
    $scope.SalvarPrograma = function (pPrograma) {
        $scope.Programa.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';
        httpService.Post("SalvarPrograma", pPrograma).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/Programa");    
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };
    //======================Excluir
    $scope.ExcluirPrograma = function (pPrograma) {

        swal({
            title: "Tem certeza que deseja Excluir esta  Programa ?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("ExcluirPrograma", pPrograma).then(function (response) {
                if (response) {

                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/Programa");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });

    };
// Aqui foi definindo funções para apresentadores
    $scope.SelecionarApresentadores = function () {
        var _url = 'ListarTabela/Apresentador'
        //_url += '?Abrangencia=' + $scope.Simulacao.Esquemas[$scope.currentEsquema].Abrangencia;
        //_url += '&Mercado=' + NullToString($scope.Simulacao.Esquemas[$scope.currentEsquema].Cod_Mercado);
        //_url += '&Empresa=' + NullToString($scope.Simulacao.Cod_Empresa_Venda);
        //_url += '&Empresa_Faturamento=' + NullToString($scope.Simulacao.Esquemas[$scope.currentEsquema].Cod_Empresa_Faturamento);
        httpService.Get(_url).then(function (response) {
            $scope.PesquisaTabelas = {}
            if (response.data) {
                $scope.ListadeApresentadores = response.data;
                if ($scope.Programa.Apresentadores == null && $scope.Programa.Apresentadores == undefined) {
                    $scope.Programa.Apresentadores = 0;
                    }

               // console.log($scope.Programa.Apresentadores.length);
                for (var i = 0; i < $scope.Programa.Apresentadores.length ; i++) {
                    for (var y = 0;y < $scope.ListadeApresentadores.length ; y++) {
                        if ($scope.Programa.Apresentadores[i].Id_Apresentador == $scope.ListadeApresentadores[y].Codigo) {
                            $scope.ListadeApresentadores[y].Selected = true;
                        }
                    };
                };
                $scope.PesquisaTabelas.Items = $scope.ListadeApresentadores ;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção de Apresentadores";
                $scope.PesquisaTabelas.MultiSelect = true;
                $scope.PesquisaTabelas.ClickCallBack = function () {
                    $scope.Programa.Apresentadores = [];
                    for (var i = 0; i < $scope.ListadeApresentadores.length; i++) {
                        if ($scope.ListadeApresentadores[i].Selected) {
                            $scope.Programa.Apresentadores.push({ 'Id_Apresentador': $scope.ListadeApresentadores[i].Codigo, 'Nome_Apresentador': $scope.ListadeApresentadores[i].Descricao });
                        }
                    };
                };
                $("#modalTabela").modal(true);
            };
        });
    }

    //=====================Clicou no X da lista de apresentadores selecionados- remover apresentadores
    $scope.RemoverApresentador = function (pId_Apresentador) {
        for (var i = 0; i < $scope.Programa.Apresentadores.length; i++) {
           // console.log($scope.Programa.Apresentadores.splice(i, 1));
            if ($scope.Programa.Apresentadores[i].Id_Apresentador == pId_Apresentador) {
                $scope.Programa.Apresentadores.splice(i, 1);
                break;
            }
        }
    }

    // Aqui foi definindo funções para veículos
    $scope.SelecionarVeiculos = function () {
        var _url = 'ListarTabela/Veiculo'
        httpService.Get(_url).then(function (response) {
            if (response.data) {
                $scope.ListadeVeiculos = response.data;
                $scope.ListadeApresentadores = response.data;
                if ($scope.Programa.Veiculos == null && $scope.Programa.Veiculos == undefined) {
                    $scope.Programa.Veiculos = 0;
                }

                for (var i = 0; i < $scope.Programa.Veiculos.length; i++) {
                    for (var y = 0; y < $scope.ListadeVeiculos.length; y++) {
                        if ($scope.Programa.Veiculos[i].Cod_Veiculo == $scope.ListadeVeiculos[y].Codigo) {
                            $scope.ListadeVeiculos[y].Selected = true;
                        }
                    };
                };
                $scope.PesquisaTabelas.Items = $scope.ListadeVeiculos;
                $scope.PesquisaTabelas.FiltroTexto = "";
                $scope.PesquisaTabelas.Titulo = "Seleção de Veiculos";
                $scope.PesquisaTabelas.MultiSelect = true;
                $scope.PesquisaTabelas.ClickCallBack = function () {
                    $scope.Programa.Veiculos = [];
                    for (var i = 0; i < $scope.ListadeVeiculos.length; i++) {
                        if ($scope.ListadeVeiculos[i].Selected) {
                            $scope.Programa.Veiculos.push({ 'Cod_Veiculo': $scope.ListadeVeiculos[i].Codigo, 'Nome_Veiculo': $scope.ListadeVeiculos[i].Descricao });
                        }
                    };
                };
                $("#modalTabela").modal(true);
            };
        });
    }

    //=====================Clicou no X da lista de Veiculos selecionados- remover Veiculos
    $scope.RemoverVeiculos = function (pCod_Veiculo) {
        for (var i = 0; i < $scope.Programa.Veiculos.length; i++) {
            // console.log($scope.Programa.Apresentadores.splice(i, 1));
            if ($scope.Programa.Veiculos[i].Cod_Veiculo == pCod_Veiculo) {
                $scope.Programa.Veiculos.splice(i, 1);
                break;
            }
        }
    }




}]);

