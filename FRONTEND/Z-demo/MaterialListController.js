angular.module('App').controller('MaterialListController', ['$scope', '$rootScope', 'GenericApi', 'AccountApi', 'MaterialApi', '$location', '$document', function ($scope, $rootScope, GenericApi, AccountApi, MaterialApi, $location, $document) {
    $scope.names = ["Emil", "Tobias", "Linus"];
    //====================Inicializa scopes
    $scope.DownloadUrl = $rootScope.baseUrl + 'ANEXOS\\';
    $scope.CurrentShow = "";
    $scope.ShowFilter = "true";
    $scope.gridheaders = [{ 'title': 'Id#', 'visible': true, 'searchable': false, 'sortable': true },
                            { 'title': 'Agencia', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Cliente', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Executivo', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Tipo', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Status', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Enviado Po', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Arquivos', 'visible': true, 'searchable': false, 'sortable': false },
                            { 'title': 'Acoes', 'visible': true, 'searchable': false, 'sortable': false },
    ];

    $scope.VeiculosDaRede = "";
    $scope.PesquisaTabelas = { 'FiltroTexto': '', cback: '', 'Items': [] }
    $scope.filtro = {
        'Id_Rede': '',
        'Cod_Veiculo': '',
        'Cod_Cliente': '',
        'Id_Tipo_Material': '',
        'Id_Material_Status': '',
        'Cod_Tipo_Midia': '',
        'Numero_Negociacao': '',
        'Periodo_Inicial': '',
        'Periodo_Final': '',
        'Id_Material_Status': '',
    };


    $scope.Materiais = "";
    $scope.StatusMaterial = "";
    $scope.TipoMidia = "";
    $scope.TipoMaterial = "";
    $scope.TipoVenda = "";

    //====================Controla Permissoes
    $scope.PermissaoAprovar = false;
    $scope.PermissaoDesaprovar = false;
    $scope.PermissaoUpload = false;
    $scope.PermissaoDownload = false;
    AccountApi.Credential("media@approve").then(function (response) {
        $scope.PermissaoAprovar = response.data;
    });
    AccountApi.Credential("media@disapprove").then(function (response) {
        $scope.PermissaoDesaprovar = response.data;
    });
    AccountApi.Credential("media@upload").then(function (response) {
        $scope.PermissaoUpload = response.data;
    });
    AccountApi.Credential("media@download").then(function (response) {
        $scope.PermissaoDownload = response.data;
    });
    //====================Carrega A Lista de Redes
    GenericApi.ListarTabela('rede').then(function (response) {
        if (response) {
            $scope.Redes = response.data;
        }
    });
    //====================Carrega A Lista de Status
    GenericApi.ListarTabela('Status_Material').then(function (response) {
        if (response) {
            $scope.StatusMaterial = response.data;
        }
    });
    //====================Carrega A Lista de Tipo de Midia
    GenericApi.ListarTabela('Tipo_Midia').then(function (response) {
        if (response) {
            $scope.TipoMidia = response.data;
        }
    });
    //====================Carrega A Lista de Tipo Material
    GenericApi.ListarTabela('Tipo_Material').then(function (response) {
        if (response) {
            $scope.TipoMaterial = response.data;
        }
    });
    //====================Carrega A Lista de Tipo de Venda
    GenericApi.ListarTabela('Tipo_Venda').then(function (response) {
        if (response) {
            $scope.TipoVenda = response.data;
        }
    });

    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.CurrentShow = 'Grid';
        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        })
    };

    //====================Carrega o Grid
    $scope.CarregarMaterial = function () {
        $scope.Materiais = [];
        $scope.CurrentShow = '';
        $('#dataTable').dataTable().fnDestroy();
        setTimeout(function () {
            MaterialApi.ListarMaterial($scope.filtro).then(function (response) {
                if (response) {
                    $scope.Materiais = response.data;
                    if ($scope.Materiais.length == 0) {
                        $scope.RepeatFinished();
                    }
                }
            });
        }, 100);
    };

    //====================Funcao para configurar o Grid
    $scope.ConfiguraGrid = function () {
        param = {};
        param.language = fnDataTableLanguage();
        param.lengthMenu = [[5, 7, 10, 25, 50, -1], [5, 7, 10, 25, 50, "Todos"]];
        param.responsive = true;
        //param.scrollY = "500px",
        param.scrollCollapse = true;
        param.paging = true;
        //param.scrollX = false;
        //var ctrl = $document[0].getElementById('dataTable');
        //$(ctrl).addClass('nowrap')

        //if ($scope.GridModo.ScroolY) {
        //    param.scrollY = "500px",
        //    param.scrollCollapse = true;
        //    param.paging = false;
        //};
        //if ($scope.GridModo.ScroolX) {
        //    param.scrollX = true;
        //    var ctrl = $document[0].getElementById('dataTable');
        //    $(ctrl).addClass('nowrap')
        //}
        //else {
        //    var ctrl = $document[0].getElementById('dataTable');
        //    $(ctrl).removeClass('nowrap')
        //};
        param.dom = "<'row'<'col-sm-3'l><'col-sm-2'f> >" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        //param.buttons = [
        //    { text: 'Novo Material<span class="fa fa-file margin-left-10"></span>', className: 'btn btn-primary', action: function (e, dt, button, config) { $('#btnNovoMaterial').click(); } },
        //    {
        //        text: 'Exportar<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning', extend: 'excel', exportOptions: {
        //            columns: ':visible:not(:first-child)'
        //        }
        //    }
        //];
        param.order = [[1, 'asc']];
        param.autoWidth = false;

        param.columns = [];
        for (var i = 0; i < $scope.gridheaders.length; i++) {
            param.columns.push({ "visible": $scope.gridheaders[i].visible, "searchable": $scope.gridheaders[i].searchable, "sortable": $scope.gridheaders[i].sortable });
        }

        $('#dataTable').DataTable(param);
        var table = $('#dataTable').DataTable();
    };

    //===========================Clicou em pesquisar
    $scope.Pesquisar = function () {
        $scope.CarregarMaterial();
        localStorage.setItem('MaterialFilter', JSON.stringify($scope.filtro));
    };
    //===========================================Escolheu uma rede/ Carrega os Veiculos da Rede
    $scope.RedeChange = function () {
        if ($scope.filtro.Id_Rede) {
            GenericApi.ListarVeiculosRede($scope.filtro.Id_Rede).then(function (response) {
                if (response.data) {
                    $scope.VeiculosDaRede = response.data;
                    if ($scope.VeiculosDaRede.length == 1) {
                        $scope.filtro.Cod_Veiculo = $scope.VeiculosDaRede[0].Cod_Veiculo;
                    }
                }
            });
        }
    };
    //===========================================Clicou no download do Material
    $scope.DownloadMaterial = function (pId_Material, pFile, pStatus) {
        var _url = $scope.DownloadUrl + '\MAT_' + pId_Material + '/' + pFile.FileName;
        window.open(_url, 'download');
        if (pStatus == 1) {
            MaterialApi.UpdateStatus(pId_Material, 2).then(function (response) {
                if (response) {
                    if (response.data[0].Valido == 1) {
                        for (var i = 0; i < $scope.Materiais.length; i++) {
                            if ($scope.Materiais[i].Id_Material == pId_Material) {
                                $scope.Materiais[i].Id_Material_Status = response.data[0].NewStatus;
                                $scope.Materiais[i].Nome_Status = response.data[0].NewNomeStatus;
                            }
                        }
                    }
                }
            });
        }
    };

    //====================Seta Valores padrao do Usuario 
    AccountApi.GetUserData().then(function (response) {
        if (response) {
            $scope.filtro.Id_Rede = response.data.Id_Rede_Padrao
            if (response.data.Id_Rede_Padrao) {
                $scope.RedeChange();
            }
        }
    });

    //===========================Se ja tiver filtro anterior gravado
    var _MaterialFilter = JSON.parse(localStorage.getItem('MaterialFilter'));
    if (_MaterialFilter) {
        $scope.filtro = _MaterialFilter;
        if ($scope.filtro.Id_Rede) {
            $scope.RedeChange()
        }
    }
    //===========================Limpar Filtros
    $scope.LimparFiltro = function () {
        localStorage.removeItem('MaterialFilter');
        $scope.Materiais = "";
        angular.forEach($scope.filtro, function (value, key) {
            $scope.filtro[key] = "";
        });
        $scope.VeiculosDaRede = [];
        $('#dataTable').dataTable().fnDestroy();
        $scope.CurrentShow = '';

    };
    $scope.SetaStatus = function (pIdMaterial, pId_Status, pNome_Status) {
        for (var i = 0; i < $scope.Materiais.length; i++) {
            if ($scope.Materiais[i].Id_Material == pIdMaterial) {
                $scope.Materiais[i].Id_Material_Status = pId_Status;
                $scope.Materiais[i].Nome_Status = pNome_Status;
                if (pId_Status==3) {
                    $scope.Materiais[i].checkPronto = !$scope.Materiais[i].checkPronto
                }
                break;
            }
        }
    }
    //====================Aprovar o Material
    $scope.AprovarMaterial = function (pMaterial) {

        var _valido = true

        for (var i = 0; i < pMaterial.Anexos.length; i++) {
            if (!pMaterial.Anexos[i].Numero_Fita && pMaterial.Id_Tipo_Material==1) { 
                _valido = false;
                break
            }
        }
        if (! _valido) {
            ShowAlert("Favor Informar o Numero de Aúdio para todos os Anexos antes de Liberar o Material");
            return;
        }


        swal({
            title: "Tem certeza que deseja Aprovar esse Material",
            type: "warning",
            showCancelButton: true,
            closeOnConfirm: true,
            confirmButtonText: "Sim,Aprovar",
            cancelButtonText: "Não, Cancelar",
            inputPlaceholder: ""
        }, function () {
            MaterialApi.UpdateStatus(pMaterial.Id_Material, 2).then(function (response) {
                if (response) {
                    if (response.data[0].Valido) {
                        $scope.SetaStatus(pMaterial.Id_Material, response.data[0].NewStatus, response.data[0].NewNomeStatus)
                    }
                }
            });
        });
    }
    //====================Finalizar  o Material
    $scope.FinalizarMaterial = function (pMaterial) {
        swal({
            title: "Tem certeza que deseja Finalizar esse Material",
            type: "warning",
            showCancelButton: true,
            closeOnConfirm: true,
            confirmButtonText: "Sim,Finalizar",
            cancelButtonText: "Não, Cancelar",
            inputPlaceholder: ""
        }, function () {
            MaterialApi.UpdateStatus(pMaterial.Id_Material, 3).then(function (response) {
                if (response) {
                    if (response.data[0].Valido) {
                        $scope.SetaStatus(pMaterial.Id_Material, response.data[0].NewStatus, response.data[0].NewNomeStatus)

                    }
                }
            });
        });
    }
    //====================Rejeitar o Material
    $scope.RecusarMaterial = function (pId_Material) {
        swal({
            title: "Tem certeza que deseja Recusar esse Material ?",
            text: 'Informe o Motivo da Recusa.',
            type: "input",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim,Recusar",
            cancelButtonText: "Não,Cancelar",
            closeOnConfirm: true
        }, function (inputValue) {
            if (inputValue === false) {
                return false;
            }
            MaterialApi.UpdateStatus(pId_Material, 4, inputValue).then(function (response) {
                if (response) {
                    if (response.data[0].Valido) {
                        $scope.SetaStatus(pId_Material, response.data[0].NewStatus, response.data[0].NewNomeStatus)
                    }
                }
            });
        });
    }
    //====================Update Numero Fita
    $scope.UpdateNumeroFita = function (pAnexo) {
        pAnexo.Numero_Fita = pAnexo.Numero_Fita_Temp;
        MaterialApi.UpdateNumeroFita(pAnexo).then(function (response) {
            pAnexo.ShowOkFita = false;
        });
    };

    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        if (_MaterialFilter) {
            $scope.CarregarMaterial();
        }
        else {

            $scope.filtro.Periodo_Inicial = CurrentDate();
            $scope.filtro.Periodo_Final = CurrentDate();
            if ($rootScope.UserData.Id_Nivel_Acesso == 5) {
                $scope.filtro.Id_Material_Status = 2; 
                $scope.CarregarMaterial();
            }
        }
    });
    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });
}]);


