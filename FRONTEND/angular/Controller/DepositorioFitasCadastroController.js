angular.module('App').controller('DepositorioFitasCadastroController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', '$routeParams', function ($scope, $rootScope, httpService, $location, $timeout, $routeParams) {

    //========================Recebe Parametro
    $scope.PesquisaTabelas = { "Items": [], 'FiltroTexto': '', ClickCallBack: '', 'Titulo': '', 'MultiSelect': false };
    $scope.Parameters = $routeParams;
    $scope.ListadeVeiculos = [];
    $scope.DepositorioFitas = "";
    $scope.DepositorioFitas.Veiculos = [];
    $scope.currentVeiculos = 0;
    $scope.PosTipoFita = [
        { 'id': 1, 'nome': 'Avulso' },
        { 'id': 2, 'nome': 'Artistico' }
    ]

    //========================Verifica Permissoes

    $scope.PermissaoDelete = false;


    //$scope.PermiteAvulso = function (pAvulso) {
    //    if (pAvulso.Tipo_Fita == 1) {
    //        $scope.PermissaoAvulso = false;
    //    }
    //}




    httpService.Get("credential/DepositorioFitas@Destroy").then(function (response) {
        $scope.PermissaoDelete = response.data;
    });

    //==========================Busca dados dos Depositorio Fitas

    var _url = "GetDepositorioFitasData/" + $scope.Parameters.Id;
    httpService.Get(_url).then(function (response) {

        if (response) {
            $scope.DepositorioFitas = response.data;
            if ($scope.Parameters.Action == "New") {
                $scope.DepositorioFitas.Tipo_Fita = "";
                $scope.DepositorioFitas.Quantidade = "";
                $scope.DepositorioFitas.Duracao = "";
                $scope.DepositorioFitas.Cod_Red_Produto = "";

            }

            if ($scope.Parameters.Action == "Edit") {
                if (response.data.Tipo_Fita == "Avulso") {
                    $scope.DepositorioFitas.Tipo_Fita = 1;
                }
                else {
                    $scope.DepositorioFitas.Tipo_Fita = 2;
                }

                if ($scope.DepositorioFitas.Cod_Red_Produto == 0)
                {
                    $scope.DepositorioFitas.Cod_Red_Produto = ""
                }


            }


        }
    });

    ////==========================Salvar
    $scope.SalvarDepositorioFitas = function (pDepositorioFitas) {
        $scope.DepositorioFitas.id_operacao = $scope.Parameters.Action == "New" ? 'I' : 'E';

        if (pDepositorioFitas.Data_Inicio > pDepositorioFitas.Data_Final) {

            ShowAlert("Data Inicio da Fita não pode ser maior que a Data Final!");
            return;
        }

        if (pDepositorioFitas.Data_Inicio == null && pDepositorioFitas.Data_Inicio == undefined) {

            ShowAlert("Data Inicio da Fita não pode ficar em branco");
            return;
        }


        if (pDepositorioFitas.Data_Final == null && pDepositorioFitas.Data_Final == undefined) {

            ShowAlert("Data Final da Fita não pode ficar em branco");
            return;
        }


        if (pDepositorioFitas.Numero_Fita == null && pDepositorioFitas.Numero_Fita == undefined) {

            ShowAlert("Número de Fita não pode ficar em branco");
            return;
        }


        if (pDepositorioFitas.Titulo_Comercial == null && pDepositorioFitas.Titulo_Comercial == undefined) {

            ShowAlert("Titulo  de Fita não pode ficar em branco");
            return;
        }

        if (pDepositorioFitas.Quantidade=="") {

            ShowAlert("Quantidade  de Fita não pode ficar em branco");
            return;
        }

        if (pDepositorioFitas.Duracao=="") {

            ShowAlert("Duração  de Fita não pode ficar em branco");
            return;
        }

        if (pDepositorioFitas.Cod_Tipo_Comercial == null && pDepositorioFitas.Cod_Tipo_Comercial == undefined) {

            ShowAlert("Código de tipo Comercial  de Fita não pode ficar em branco");
            return;
        }

        if (pDepositorioFitas.Cod_Veiculo == null && pDepositorioFitas.Cod_Veiculo == undefined) {

            ShowAlert("Código de Veículo  de Fita não pode ficar em branco");
            return;
        }

        var ct_Arquivo_Midia = document.getElementById('txtArquivoMidia').value;
        pDepositorioFitas.Arquivo_Midia = ct_Arquivo_Midia;

        if (pDepositorioFitas.Tipo_Fita == 1) {
            pDepositorioFitas.Tipo_Fita = 'CO';
            tipo_fita_n = 1;

        }
        else {
            pDepositorioFitas.Tipo_Fita = 'AR'
            tipo_fita_n = 2;
        }

        //pDepositorioFitas.Cod_Veiculo = $scope.DepositorioFitas.Veiculos[0].Cod_Veiculo

        httpService.Post("SalvarDepositorioFitas", pDepositorioFitas).then(function (response) {
            if (response) {

                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                    $location.path("/DepositorioFitas");
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            }
        })
    };


    //======================Excluir
    $scope.ExcluirDepositorioFitas = function (pDepositorioFitas) {

        swal({
            title: "Tem certeza que deseja Excluir esta  Fita ?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {

           if (pDepositorioFitas.Tipo_Fita == 1) {
               pDepositorioFitas.Tipo_Fita = 'CO';
           }
           else {
                 pDepositorioFitas.Tipo_Fita = 'AR'
           }

            httpService.Post("ExcluirDepositorioFitas", pDepositorioFitas).then(function (response) {
                if (response) {

                    if (response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem, 'success');
                        $location.path("/DepositorioFitas");
                    }
                    else {
                        ShowAlert(response.data[0].Mensagem, 'warning');
                    }
                }
            })
        });

    };
 

    // Aqui foi definindo funções para veículos
    //$scope.SelecionarVeiculos = function () {
    //    //$scope.PesquisaTabelas = NewPesquisaTabela();
    //    var _url = 'ListarTabela/Veiculo'
    //    httpService.Get(_url).then(function (response) {
    //        $scope.PesquisaTabelas = {}
    //        if (response.data) {
    //            $scope.ListadeVeiculos = response.data;
    //            if ($scope.DepositorioFitas.Veiculos == null && $scope.DepositorioFitas.Veiculos == undefined) {
    //                $scope.DepositorioFitas.Veiculos = 0;
    //            }

    //            for (var i = 0; i < $scope.DepositorioFitas.Veiculos.length; i++) {
    //                for (var y = 0; y < $scope.ListadeVeiculos.length; y++) {
    //                    if ($scope.DepositorioFitas.Veiculos[i].Cod_Veiculo == $scope.ListadeVeiculos[y].Codigo) {
    //                        $scope.ListadeVeiculos[y].Selected = true;
    //                    }
    //                };
    //            };
    //            $scope.PesquisaTabelas.Items = $scope.ListadeVeiculos;
    //            $scope.PesquisaTabelas.FiltroTexto = "";
    //            $scope.PesquisaTabelas.Titulo = "Seleção de Veiculos";
    //            $scope.PesquisaTabelas.MultiSelect = true;
    //            $scope.PesquisaTabelas.ClickCallBack = function () {
    //                $scope.DepositorioFitas.Veiculos = [];
    //                for (var i = 0; i < $scope.ListadeVeiculos.length; i++) {
    //                    if ($scope.ListadeVeiculos[i].Selected) {
    //                        $scope.DepositorioFitas.Veiculos.push({ 'Cod_Veiculo': $scope.ListadeVeiculos[i].Codigo, 'Nome_Veiculo': $scope.ListadeVeiculos[i].Descricao });
    //                    }
    //                };
    //            };
    //            $("#modalTabela").modal(true);
    //        };
    //    });
    //}

    ////=====================Clicou no X da lista de Veiculos selecionados- remover Veiculos
    //$scope.RemoverVeiculos = function (pCod_Veiculo) {
    //    var n = 0;
    //    for (var i = 0; i < $scope.DepositorioFitas.Veiculos.length; i++) {
    //        if ($scope.DepositorioFitas.Veiculos[i].Cod_Veiculo == pCod_Veiculo) {
    //            $scope.DepositorioFitas.Veiculos.splice(i, 1);
    //            break;
    //        }
    //    }
    //}

    //=====================Carregar Numero de Fita 
    $scope.CarregarNumeroFita = function (pNumero_Fita) {
        //var n = 0;
        var tipo_fita_n = 0;

        if ($scope.DepositorioFitas.Tipo_Fita == "") {
            ShowAlert("Para utilizar numeração automática, primeiro selecione Tipo de Fita");
            return;
           
        }


        if ($scope.DepositorioFitas.Cod_Veiculo == null && $scope.DepositorioFitas.Cod_Veiculo == undefined) {

            ShowAlert("Para utilizar numeração automática, primeiro selecione um veiculo");
            return;
        }
        //else {
        //    for (var i = 0; i < $scope.DepositorioFitas.Veiculos.length; i++) {
        //        n = n + 1;

        //    }
        //}
        //if (n > 1) {

        //    ShowAlert("Numeração automática somente é permitida quando selecionado apenas um veiculo");
        //    return;
        //}

        if (pNumero_Fita.Tipo_Fita == 1) {
            pNumero_Fita.Tipo_Fita = 'CO'
            tipo_fita_n=1
        }
        else {
            pNumero_Fita.Tipo_Fita = 'AR'
            tipo_fita_n=2
        }


        var _data = {
            'Cod_Veiculo': pNumero_Fita.Cod_Veiculo,
           // 'Cod_Veiculo': pNumero_Fita.Veiculos[0].Cod_Veiculo,
            'Tipo_Fita': pNumero_Fita.Tipo_Fita,
            'Tipo_Midia': '',
            'Cod_Tipo_Comercial': pNumero_Fita.Cod_Tipo_Comercial
        };

        httpService.Post("RangeFita", _data).then(function (response) {
            if (response) {
               
                if (response.data[0].Status == 0) {


                    ShowAlert('Veículo não esta parametrizado corretamente em Paramêtros de Numeração de Fitas');
                   //Atribuir dado ao campo numeracao de fita 
                }
                else {
                    $scope.DepositorioFitas.Tipo_Fita = tipo_fita_n
                    $scope.DepositorioFitas.Numero_Fita = response.data[0].Numero_Fita;

                  

                }
            }
        })    
    }


    //=====================Marcar / Desmarcar todos os Reencaixe
    $scope.MarcaDepositorioFitas = function () {
        if ($scope.DepositorioFitas.MarcarDesmarcar == true) {
            $scope.DepositorioFitas.Indica_DiaDom = true;
            $scope.DepositorioFitas.Indica_DiaSeg = true;
            $scope.DepositorioFitas.Indica_DiaTer = true;
            $scope.DepositorioFitas.Indica_DiaQua = true;
            $scope.DepositorioFitas.Indica_DiaQui = true;
            $scope.DepositorioFitas.Indica_DiaSex = true;
            $scope.DepositorioFitas.Indica_DiaSab = true;
        }
        else {
            $scope.DepositorioFitas.Indica_DiaDom = false;
            $scope.DepositorioFitas.Indica_DiaSeg = false;
            $scope.DepositorioFitas.Indica_DiaTer = false;
            $scope.DepositorioFitas.Indica_DiaQua = false;
            $scope.DepositorioFitas.Indica_DiaQui = false;
            $scope.DepositorioFitas.Indica_DiaSex = false;
            $scope.DepositorioFitas.Indica_DiaSab = false;
        }

    }



}]);

