angular.module('App').controller('ParamNumFitasController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {

    //----------------------Inicializa scopes
    $scope.newFilter = function () {
        return { 'Cod_Veiculo': '', 'Nome_Veiculo': '' }
    };
    $scope.NewParametros = function () {
        return {
            'Cod_Veiculo': '', 'Nome_Veiculo': '',
            'Rg_Comerc_De': '', 'Rg_Comerc_Ate': '', 'Rg_Artist_De': '', 'Rg_Artist_Ate': '', 'Rg_Reserv_De': '', 'Rg_Reserv_Ate': '',
            'Indica_Numerac_Compart': false, 'Indica_Numerac_Propria': false,
            'Regras': []
        };
    };
    $scope.Filtro = $scope.newFilter();
    $scope.Parametros = $scope.NewParametros();

    //--------------------- Controle do Seta Flag -------------------
    $scope.SetaFlag = function (pTipo) {
        if (pTipo == 'C') {
            if ($scope.Parametros.Indica_Numerac_Compart) {
                $scope.Parametros.Indica_Numerac_Propria = false;
            };
        };
        if (pTipo == 'P') {
            if ($scope.Parametros.Indica_Numerac_Propria) {
                $scope.Parametros.Indica_Numerac_Compart = false;
            };
        };
    };

    //--------------------Carregar Parâmetros-------------------------
    $scope.CarregaParametros = function (pParam) {
        var _url = ""
        if (pParam.Cod_Veiculo) {
            _url = "CarregarParamFita/" + pParam.Cod_Veiculo;
        }
        else {
            _url = "CarregarParamFita"
        }
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.Parametros = response.data;
            };
        });
    };

    //------------------Quando mudar o veiculo, carrega os parametros----------------
    $scope.$watch('[Filtro.Cod_Veiculo]', function (newValue, oldValue) {
        if (newValue != oldValue) {
            $scope.CarregaParametros($scope.Filtro);
        };
    });

    //------------------- Adicionar linha no grid ----------------------
    $scope.AdicionarLinha = function () {
        $scope.Parametros.Max_Id_Linha++;
        $scope.Parametros.Regras.push({
            'Id_Linha': $scope.Parametros.Max_Id_Linha,
            'Tipo_Midia': '',
            'Tipo_Comercial': '',
            'Num_Fita_De': '',
            'Num_Fita_Ate': ''
        });
    };

    //------------------- Remover linha no grid ----------------------
    $scope.RemoverLinha = function (pId_Linha) {
        for (var i = 0; i < $scope.Parametros.Regras.length; i++) {
            if ($scope.Parametros.Regras[i].Id_Linha == pId_Linha) {
                $scope.Parametros.Regras.splice(i, 1);
                break;
            };
        };
    };

    //------------------- Selecionar Tipo de Midia----------------------
    $scope.SelecionarTipoMidia = function (pNumerFita) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        httpService.Get('ListarTabela/TipoMidias').then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data;
                $scope.PesquisaTabelas.Titulo = "Seleção de Tipo de Mídias"
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pNumerFita.Tipo_Midia = value.Codigo }
                $("#modalTabela").modal(true);
            }
        });
    };

    //------------------- Validar Tipo de Midia----------------------
    $scope.ValidarTipoMidia = function (pNumerFita) {
        httpService.Get("ValidarTabela/TipoMidias/" + pNumerFita.Tipo_Midia).then(function (response) {
            if (!response.data[0].Status) {
                ShowAlert(response.data[0].Mensagem + " - " + pNumerFita.Tipo_Midia + " - Linha " + pNumerFita.Id_Linha);
                pNumerFita.Tipo_Midia = "";
            }
        });
    };

    //------------------- Selecionar Tipo de Comercial----------------------
    $scope.SelecionarTipoComercial = function (pNumerFita) {
        $scope.PesquisaTabelas = NewPesquisaTabela();
        httpService.Get('ListarTabela/Tipo_Comercial').then(function (response) {
            if (response.data) {
                $scope.PesquisaTabelas.Items = response.data;
                $scope.PesquisaTabelas.Titulo = "Seleção de Tipo de Comerciais"
                $scope.PesquisaTabelas.ClickCallBack = function (value) { pNumerFita.Tipo_Comercial = value.Codigo }
                $("#modalTabela").modal(true);
            }
        });
    };

    //------------------- Validar Tipo de Comercial----------------------
    $scope.ValidarTipoComercial = function (pNumerFita) {
        httpService.Get("ValidarTabela/Tipo_Comercial/" + pNumerFita.Tipo_Comercial).then(function (response) {
            if (!response.data[0].Status) {
                ShowAlert(response.data[0].Mensagem + " - " + pNumerFita.Tipo_Comercial + " - Linha " + pNumerFita.Id_Linha);
                pNumerFita.Tipo_Comercial = "";
            }
        });
    };

    //--------------------- Salvar Parametros --------------------------
    $scope.SalvarParametros = function (pFiltro, pParam, pRegra) {
        //----Consistencia do Filtro
        if (!pFiltro.Cod_Veiculo) {
            ShowAlert("Veículo é obrigatório");
            return;
        };

        //----Consistencias dos Campos de Parametros
        if (pParam.Rg_Comerc_De == "" || pParam.Rg_Comerc_De == 0
            || pParam.Rg_Comerc_Ate == "" || pParam.Rg_Comerc_Ate == 0
            || pParam.Rg_Artist_De == "" || pParam.Rg_Artist_De == 0
            || pParam.Rg_Artist_Ate == "" || pParam.Rg_Artist_Ate == 0) {
            ShowAlert("As Ranges Comercial e Artístico são obrigatórias. \n Se for usar uma única Range, repita a numeração nos dois paramêtros");
            return;
        };
        var _Comerc_De = parseInt(pParam.Rg_Comerc_De.toString().substr(0, 6));
        var _Comerc_Ate = parseInt(pParam.Rg_Comerc_Ate.toString().substr(0, 6));
        var _Artist_De = parseInt(pParam.Rg_Artist_De.toString().substr(0, 6));
        var _Artist_Ate = parseInt(pParam.Rg_Artist_Ate.toString().substr(0, 6));
        var _Reserv_De = parseInt(pParam.Rg_Reserv_De.toString().substr(0, 6));
        var _Reserv_Ate = parseInt(pParam.Rg_Reserv_Ate.toString().substr(0, 6));
        if (_Comerc_De >= _Comerc_Ate) {
            ShowAlert("Número inicial do Comercial deve ser menor que o final");
            return;
        };
        if (_Artist_De >= _Artist_Ate) {
            ShowAlert("Número inicial do Artístico deve ser menor que o final");
            return;
        };
        if ((pParam.Rg_Reserv_De != "" && pParam.Rg_Reserv_De != 0) || (pParam.Rg_Reserv_Ate != "" && pParam.Rg_Reserv_Ate != 0)) {
            if (pParam.Rg_Reserv_De == "" || pParam.Rg_Reserv_De == 0) {
                ShowAlert("Número inicial Reservado deve ser um número válido");
                return;
            }
            if (pParam.Rg_Reserv_Ate == "" || pParam.Rg_Reserv_Ate == 0) {
                ShowAlert("Número final Reservado deve ser um número válido");
                return;
            }
            if (_Reserv_De >= _Reserv_Ate) {
                ShowAlert("Número inicial Reservado deve ser menor que o final");
                return;
            };
        };
        if (!pParam.Indica_Numerac_Compart && !pParam.Indica_Numerac_Propria) {
            ShowAlert("Selecione uma opção entre Numeração compartilhada e Numeração Própria");
            return;
        };

        //----Consistencias do Grid
        var bolPreenchido = false;
        for (var i = 0; i < pRegra.length; i++) {
            if (pRegra[i].Tipo_Midia || pRegra[i].Tipo_Comercial || pRegra[i].Num_Fita_De || pRegra[i].Num_Fita_Ate) {
                bolPreenchido = true;
                break;
            };
        };
        if (bolPreenchido) {
            for (var i = 0; i < pRegra.length; i++) {
                if (!pRegra[i].Tipo_Midia) {
                    ShowAlert("Tipo de Mídia é obrigatório. Linha " + pRegra[i].Id_Linha.toString());
                    return;
                };
                httpService.Get("ValidarTabela/TipoMidias/" + pRegra[i].Tipo_Midia).then(function (response) {
                    if (!response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem + " - " + pRegra[i].Tipo_Midia + " - Linha " + pRegra[i].Id_Linha);
                        pRegra[i].Tipo_Midia = "";
                    }
                });
                if (!pRegra[i].Tipo_Comercial) {
                    ShowAlert("Tipo de Comercial é obrigatório. Linha " + pRegra[i].Id_Linha.toString());
                    return;
                };
                httpService.Get("ValidarTabela/Tipo_Comercial/" + pRegra[i].Tipo_Comercial).then(function (response) {
                    if (!response.data[0].Status) {
                        ShowAlert(response.data[0].Mensagem + " - " + pRegra[i].Tipo_Comercial + " - Linha " + pRegra[i].Id_Linha);
                        pRegra[i].Tipo_Comercial = "";
                    }
                });
                if (pRegra[i].Num_Fita_De == "" || pRegra[i].Num_Fita_De == 0) {
                    ShowAlert("Numero Inicial da Fita é obrigatório. Linha " + pRegra[i].Id_Linha);
                    return;
                };
                if (pRegra[i].Num_Fita_Ate == "" || pRegra[i].Num_Fita_Ate == 0) {
                    ShowAlert("Numero Final da Fita é obrigatório. Linha " + pRegra[i].Id_Linha);
                    return;
                };
                var _Fita_De = parseInt(pRegra[i].Num_Fita_De.toString().substr(0, 6));
                var _Fita_Ate = parseInt(pRegra[i].Num_Fita_Ate.toString().substr(0, 6));
                if (_Fita_De >= _Fita_Ate) {
                    ShowAlert("Número inicial da Fita deve ser menor que o final. Linha " + pRegra[i].Id_Linha);
                    return;
                };
            };
        };

        //----Chama a rotina de Salvar Parametros
        var _data = {
            'Cod_Veiculo': pFiltro.Cod_Veiculo,
            'Rg_Comerc_De': pParam.Rg_Comerc_De,
            'Rg_Comerc_Ate': pParam.Rg_Comerc_Ate,
            'Rg_Artist_De': pParam.Rg_Artist_De,
            'Rg_Artist_Ate': pParam.Rg_Artist_Ate,
            'Rg_Reserv_De': pParam.Rg_Reserv_De,
            'Rg_Reserv_Ate': pParam.Rg_Reserv_Ate,
            'Indica_Numerac_Compart': pParam.Indica_Numerac_Compart,
            'Indica_Numerac_Propria': pParam.Indica_Numerac_Propria,
            'Regras': pRegra
        };
        httpService.Post('SalvaParametros', _data).then(function (response) {
            if (response) {
                if (response.data[0].Status) {
                    ShowAlert(response.data[0].Mensagem, 'success');
                }
                else {
                    ShowAlert(response.data[0].Mensagem, 'warning');
                }
            };
        });
    };



}]);






