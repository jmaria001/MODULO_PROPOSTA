angular.module('App').controller('ReabreCEController', ['$scope', '$rootScope', 'httpService', '$location', '$timeout', function ($scope, $rootScope, httpService, $location, $timeout) {
    

    //====================Inicializa scopes
    $scope.Filtro = {};
    $scope.NewFilter = function () {
        $scope.Filtro = { 'Empresa': '', 'Contrato': '', 'Sequencia': '', 'Veiculo': '', 'MotivoReabertura': '' };
    };
    $scope.NewFilter();
    //====================Reabre o Comprovante
    $scope.ReaberturaExecutar = function (pFiltro) {
        swal({
            title: "Deseja Realmente Reabrir os Comprovantes de Exibição para o Contrato Informado?",
            //text: "Essa opcação desabilita o acesso ao sistema para esse usuário",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Reabrir?",
            cancelButtonText: "Cancelar",
            closeOnConfirm: true
        }, function () {
            //=====Empresa==============================
            if (pFiltro.Empresa == "" || pFiltro.Empresa == undefined) {
                ShowAlert("Filtro empresa é obrigatório");
                return;
            }
            //=====Contrato=============================
            if (pFiltro.Contrato == "" || pFiltro.Contrato == undefined) {
                ShowAlert("Filtro cotrato é obrigatório");
                return;
            }
            //=====Sequência============================
            if (pFiltro.Sequencia == "" || pFiltro.Sequencia == undefined) {
                ShowAlert("Filtro sequência é obrigatório");
                return;
            }
            ////=====Veículo==============================
            //if (pFiltro.Veiculo == "" || pFiltro.Veiculo == undefined) {
            //    ShowAlert("Filtro veículo é obrigatório");
            //    return;
            //}
            //=====MotivoBixa===========================
            if (pFiltro.MotivoReabertura == "" || pFiltro.MotivoReabertura == undefined) {
                ShowAlert("Filtro motivo da reabertura é obrigatório");
                return;
            }
            $rootScope.routeloading = true;
            $scope.ReabreCE = "";
            $scope.ShowGrid = false;
            $('#dataTable').dataTable().fnDestroy();
            //var _Competencia = CompetenciaToInt(pFiltro.Competencia);
            var _url = 'ExecutarReabreCE'
            _url += '?Empresa=' + pFiltro.Empresa;
            _url += '&Contrato=' + pFiltro.Contrato;
            _url += '&Sequencia=' + pFiltro.Sequencia;
            _url += '&Veiculo=' + pFiltro.Veiculo;
            _url += '&MotivoReabertura=' + pFiltro.MotivoReabertura;
            _url += '&';

            httpService.Get(_url).then(function (response) {
                if (response) {
                    $scope.ReabreCE = response.data[0];
                    if ($scope.ReabreCE.length == 0) {
                        ShowAlert("Não existe dados cadastrado p/ este Filtro");
                    } else {
                        ShowAlert(response.data[0].Mensagem);
                        if (response.data[0].Status) {
                            $scope.NewFilter();
                        }
                    };
                };
            });
        });
    };
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
    });

}]);

