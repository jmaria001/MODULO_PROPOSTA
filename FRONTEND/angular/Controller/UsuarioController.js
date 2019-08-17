angular.module('App').controller('UsuarioController', ['$scope', '$rootScope', 'httpService', '$location','$timeout', function ($scope, $rootScope, httpService, $location,$timeout) {

    //====================Inicializa scopes
    $scope.CurrentShow = "Grid";
    $scope.CurrentTab = "Perfil";
    $scope.checkBoxEmpresa = false;
    $scope.checkBoxPerfil = false;
    $scope.Ctrl = {};
    $scope.NivelAcesso = [{ 'Id': 1, 'Descricao': 'Padrão' }, { 'Id': 2, 'Descricao': 'Administrador' }]
    $scope.gridheaders = [{ 'title': '#ID', 'visible': true, 'searchable': false, 'sortable': true },
                            { 'title': 'Login', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Nome', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Email', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Telefone', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Nivel de Acesso', 'visible': true, 'searchable': true, 'sortable': true },
                            { 'title': 'Status', 'visible': true, 'searchable': true, 'sortable': true },
    ];

    //====================Permissoes
    $scope.PermissaoNew = 'false';
    $scope.PermissaoEdit = 'false';
    $scope.PermissaoDesativar = 'false';
    $scope.PermissaoExcluir= 'false';
    httpService.Get("credential/Usuario@New").then(function (response) {
        $scope.PermissaoNew = response.data;
    });
    httpService.Get("credential/Usuario@Edit").then(function (response) {
        $scope.PermissaoEdit = response.data;
    });
    httpService.Get("credential/Usuario@Destroy").then(function (response) {
        $scope.PermissaoExcluir = response.data;
    });
    httpService.Get("credential/Usuario@Activate").then(function (response) {
        $scope.PermissaoDesativar = response.data;
    });
    
    //====================Quando terminar carga do grid, torna view do grid visible
    $scope.RepeatFinished = function () {
        $rootScope.routeloading = false;
        $scope.ConfiguraGrid();
        $scope.CurrentShow = 'Grid';
        setTimeout(function () {
            $("#dataTable").dataTable().fnAdjustColumnSizing();
        }, 10)


    };

    //====================Carrega o Grid
    $scope.CarregarUsuario = function () {
        $rootScope.routeloading = true;
        $scope.Usuarios = [];
        $scope.CurrentShow = '';
        $('#dataTable').dataTable().fnDestroy();
        httpService.Get('UsuarioListar').then(function (response) {
            if (response) {
                $scope.Usuarios = response.data;
                if ($scope.Usuarios.length == 0) {
                    $scope.RepeatFinished();
                }
            }
        });
    };
    //====================Novo Usuario/ Editar Usuario
    $scope.EditarUsuario = function (pIdUsuario) {
        httpService.Get('GetUsuario/'+pIdUsuario).then(function (response) {
            if (response) {
                $scope.Ctrl = response.data;
            }
            $scope.CurrentShow = 'Dados';
        });
    };
    //====================Desativar Usuario
    $scope.DesativarReativar = function (pIdUsuario, pAction) {

        swal({
            title: "Tem certeza que deseja " + (pAction ? "reativar" : "desativar") + " esse Usuário ?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim," + (pAction ? "Reativar" : "Desativar"),
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            _Data = { 'Id_Usuario': pIdUsuario, 'Status': pAction }
            httpService.Post('DesativarReativar', _Data).then(function (response) {
                $scope.CarregarUsuario();
                $scope.CurrentShow = 'Dados';
            });
        });
    };
    //====================Excluir Usuario
    $scope.ExcluirUsuario = function (pIdUsuario) {
        swal({
            title: "Tem certeza que deseja Excluir esse Usuário ?",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim,Excluir" ,
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            _Data = { 'Id_Usuario': pIdUsuario}
            httpService.Post('ExcluirUsuario', _Data).then(function (response) {
                $scope.CarregarUsuario();
                $scope.CurrentShow = 'Dados';
            });
        });
    };

    
    //=====================Marcou uma funcao do Perfil
    $scope.CheckPerfil = function (pPerfil) {
        for (var i = 0; i < $scope.Ctrl.Perfil.length; i++) {
            if ($scope.Ctrl.Perfil[i].Id_Funcao_Root == pPerfil.Id_Funcao) {
                $scope.Ctrl.Perfil[i].Selected = pPerfil.Selected;
            }
        }
    }
    //=====================Marcar / Desmarcar todos os perfis
    $scope.MarcarPerfil = function () {
        for (var i = 0; i < $scope.Ctrl.Perfil.length; i++) {
            $scope.Ctrl.Perfil[i].Selected = $scope.checkBoxPerfil;
        }
    }
    //=====================Marcar / Desmarcar todos as Empresas do Perfil
    $scope.MarcarEmpresa = function () {
        for (var i = 0; i < $scope.Ctrl.Empresas.length; i++) {
            $scope.Ctrl.Empresas[i].Selected = $scope.checkBoxEmpresa;
        }
    }
    //===========================Cancela Edicao
    $scope.CancelaEdicao = function () {
        $scope.CurrentShow = 'Grid';
        $scope.CurrentTab = 'Perfil';
        $scope.Ctrl = {};
        $scope.checkBoxEmpresa = false;
        $scope.checkBoxPerfil = false;
    }
    //===========================Salvar o Usuario
    $scope.SalvarUsuario = function () {
        httpService.Post('SalvarUsuario',$scope.Ctrl).then(function (response) {
            if (response.data[0].Status==0) {
                ShowAlert(response.data[0].Mensagem, 'error');
            }
            else {
                ShowAlert('Dados Gravados com Sucesso', 'success');
                $scope.CarregarUsuario();
                $scope.CancelaEdicao();
            }
        });
    }
    //====================Funcao para configurar o Grid
    $scope.ConfiguraGrid = function () {
        param = {};
        param.language = fnDataTableLanguage();
        param.lengthMenu = [[7, 10, 25, 50, -1], [7, 10, 25, 50, "Todos"]];
        param.scrollCollapse = true;
        param.paging = true;
        param.dom = "<'row'<'col-sm-3'l><'col-sm-4'f><'col-sm-5'B>>" + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-5'i><'col-sm-7'p>>",
        param.buttons = [
            //{ text: 'Novo Usuário<span class="fa fa-file margin-left-10"></span>', className: 'btn btn-primary btnNew', action: function (e, dt, button, config) { $('#btnNovoUsuarioUsuario').click(); } },
            {
                text: 'Exportar<span class="fa fa-file-excel-o margin-left-10" style="color:white"></span>', type: 'excel', className: 'btn btn-warning HideButton', extend: 'excel', exportOptions: {
                    columns: ':visible:not(:first-child)'
                }
            }
        ];
        param.order = [[0, 'asc']];
        param.autoWidth = false;

        param.columns = [];
        for (var i = 0; i < $scope.gridheaders.length; i++) {
            param.columns.push({ "visible": $scope.gridheaders[i].visible, "searchable": $scope.gridheaders[i].searchable, "sortable": $scope.gridheaders[i].sortable });
        }
        $('#dataTable').DataTable(param);

    };
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $scope.ConfiguraGrid();
        $scope.CarregarUsuario();
    });
    //===========================Evento chamado ao fim do ngrepeat ao carregar grid 
    $scope.$on('ngRepeatFinished', function (ngRepeatFinishedEvent) {
        $scope.RepeatFinished();
    });
}]);

