angular.module('App').controller('Composicao_Break_Controller', ['$scope', '$rootScope', '$location', 'httpService', '$filter', function ($scope, $rootScope, $location, httpService, $filter, $routeParams) {


    //====================Inicializa scopes
    $scope.ShowFilter = true;
    $scope.ShowGrid = false;
    $scope.ShowEdit = false;
    $scope.ShowGravacao = false;
    $scope.Filtro = "";
    $scope.Breaks= []; 
    $scope.Temp_Break = {};
    $scope.Intervalo = [{ 'Codigo': 0, 'Descricao': 'Local' }, { 'Codigo': 1, 'Descricao': 'Net' }, { 'Codigo': 2, 'Descricao': 'Artistico' }, { 'Codigo': 3, 'Descricao': 'Local PE' }];
    $scope.ListaBreaks = [];
    $scope.ListaSequencias = [];
    $scope.Critica = "";
    $scope.MaxSequenciaFaixa = 0;
    $scope.Operacao = "";


    $scope.NewFiltro = function(){
        return { 'Cod_Veiculo': '011', 'Nome_Veiculo': '', 'Data_Exibicao': '2020-09-15', 'Cod_Programa': 'JMEI', 'Nome_Programa': '', 'Data_Inicio': '', 'Data_Fim': '' }
    };
    $scope.Filtro = $scope.NewFiltro();
    //====================Carrega Table de Breaks
    $scope.CarregarListaBreaks = function (pFiltro) {
        $rootScope.routeloading = true;
        $scope.Breaks.Composicao = [];
        $scope.ShowGrid = false;
        var _url = 'Roteiro/ListarBreak';
        _url += '?Cod_Veiculo=' + pFiltro.Cod_Veiculo;
        _url += '&Data_Exibicao=' + pFiltro.Data_Exibicao;
        _url += '&Cod_Programa=' + pFiltro.Cod_Programa;
        _url += '&';
        httpService.Get(_url).then(function (response) {
            if (response) {
                $scope.ShowFilter = false;
                $scope.ShowGrid = true;
                $scope.ShowEdit = false;
                $scope.ShowGravacao = false;
                $scope.ShowStatus= false;
                $scope.Breaks= response.data; 
                $scope.RenumeraId($scope.Breaks.Composicao);
                for (var i = 0; i < $scope.Breaks.Composicao.length; i++) {
                    $scope.MaxSequenciaFaixa = Math.max($scope.MaxSequenciaFaixa, $scope.Breaks.Composicao[i].Sequencia_Faixa);
                };
            };
        });
    };
    //===============================Selecionar programa
    $scope.SelecionarPrograma = function (pFiltro) {
        if (!pFiltro.Cod_Veiculo || !pFiltro.Data_Exibicao) {
            ShowAlert("Informe o Veiculo e Data");
            pFiltro.Cod_Programa = "";
            pFiltro.Nome_Programa = ""
            return
        }
        $scope.PesquisaTabelas = NewPesquisaTabela();
        var _url = 'Roteiro/ProgramasBreak'
        _url += '?Cod_Veiculo=' + pFiltro.Cod_Veiculo;
        _url += '&Data_Exibicao=' + pFiltro.Data_Exibicao;
        _url += '&';

        httpService.Get(_url).then(function (response) {
            $scope.PesquisaTabelas.Titulo = "Selecionar Programas"
            if (response.data) {
                for (var i = 0; i < response.data.length; i++) {
                    if (response.data[i].Indica_Rotativo == 0) {
                        $scope.PesquisaTabelas.Items.push({ 'Codigo': response.data[i].Cod_Programa, 'Descricao': response.data[i].Nome_Programa });
                    };
                };

                $scope.PesquisaTabelas.FiltroTexto = ""
                $scope.PesquisaTabelas.MultiSelect = false;
                $scope.PesquisaTabelas.ClickCallBack = function (value) {
                    pFiltro.Cod_Programa = value.Codigo;
                    pFiltro.Nome_Programa = value.Descricao;
                };
            }
            $("#modalTabela").modal(true);
        });
    };
    
    //===============================Validar programa
    $scope.ProgramaChange = function (pFiltro) {
        if (!pFiltro.Cod_Veiculo || !pFiltro.Data_Exibicao) {
            ShowAlert("Informe o Veiculo e Data");
            pFiltro.Cod_Programa = "";
            pFiltro.Nome_Programa = ""
            return
        }
        var _url = 'Roteiro/ProgramasBreak'
        _url += '?Cod_Veiculo=' + pFiltro.Cod_Veiculo;
        _url += '&Data_Exibicao=' + pFiltro.Data_Exibicao;
        _url += '&Cod_Programa=' + pFiltro.Cod_Programa;
        _url += '&';

        httpService.Get(_url).then(function (response) {
            if (response.data.length==0) {
                ShowAlert("Não existe grade para esse Programa");
                pFiltro.Cod_Programa = "";
                pFiltro.Nome_Programa = ""
            }
            else {
                pFiltro.Nome_Programa = response.data[0].Nome_Programa;
            }
        });
    };
    //===========================Botao Adicionar Break
    $scope.AdicionarBreak = function (pFiltro) {
        $scope.ListaBreaks = [];
        if ($scope.Breaks.Composicao.length > 0) {
            var _lastBreak = $scope.Breaks.Composicao[($scope.Breaks.Composicao.length) - 1].Breaks;
            _lastBreak++; //para acrescentar um break
            for (var i = 0; i <= _lastBreak ; i++) {
                $scope.ListaBreaks.push(i);
            };
        } else {
            $scope.ListaBreaks.push(0);
            $scope.ListaBreaks.push(1);
        }
        $scope.Temp_Break = {};
        $scope.Operacao= "Inclusao"
        $scope.ShowGrid = false;
        $scope.ShowEdit = true;
        $scope.ChangeBreak($scope.Temp_Break);
    };
    //===========================Botao Cancela Edicao
    $scope.CancelaEdicao = function () {
        $scope.Temp_Break = {};
        $scope.ListaBreaks = [];
        $scope.ListaSequencias = [];
        $scope.ShowEdit = false;
        $scope.ShowGrid = true;
        $scope.ShowGravacao = false;
        $scope.ShowStatus= false;
    };
    //=============================Renumera Id_Composicao
    $scope.RenumeraId = function (pBreak) {
        var _seq = 0;
        var _break_ant = -1;
        for (var i = 0; i < pBreak.length; i++) {
            if (_break_ant != pBreak[i].Breaks) {
                _seq = 0;
                _break_ant = pBreak[i].Breaks;
            }
            _seq++;
            pBreak[i].Id_Composicao = i + 1;
            pBreak[i].Sequencia = _seq;
        }
    };
    //===========================Botao Insert Break
    $scope.InsertBreak = function (pBreak) {
        if (!pBreak.Breaks && pBreak.Breaks!=0) {
            ShowAlert("Numero do Break não foi Informado.");
            return;
        };
        if (!pBreak.Duracao) {
            ShowAlert("Duração do Intervalo não foi Informada.");
            return;
        };
        if (!pBreak.Tipo_Break) {
            ShowAlert("Intervalo não foi Informado.");
            return;
        };
        //-----Localiza onde inserir o break
        var _index = -1;
        var _lastindex = 0;
        for (var i = 0; i < $scope.Breaks.Composicao.length; i++) {
            if (pBreak.Breaks == $scope.Breaks.Composicao[i].Breaks && pBreak.Sequencia == $scope.Breaks.Composicao[i].Sequencia) {
                _index = i;
                break;
            };
            if ($scope.Breaks.Composicao[i].Breaks > pBreak.Breaks) {
                _index = i
                break;
            };
            if (pBreak.Breaks == $scope.Breaks.Composicao[i].Breaks) {
                _index = i + 1; ///para guardar a ultima posicao do break quando for nova sequencia
            };
        };
        if (_index == -1) {
            if (pBreak.Breaks == 0) {
                _index = 0;
            }
            else {
                _index = $scope.Breaks.Composicao.length;
            }
        };
        $scope.MaxSequenciaFaixa++;
        pBreak.Sequencia_Faixa = $scope.MaxSequenciaFaixa;
        $scope.Breaks.Composicao.splice(_index, 0, pBreak);
        $scope.RenumeraId($scope.Breaks.Composicao);
        $scope.CancelaEdicao();
    };
    $scope.UpdateBreak = function (pBreak) {
        var _index = -1;
        for (var i = 0; i < $scope.Breaks.Composicao.length; i++) {
            if (pBreak.Id_Composicao == $scope.Breaks.Composicao[i].Id_Composicao) {
                _index = i;
                break
            }
        };
        if (_index > -1) {
            angular.forEach(pBreak, function (value, key) {
                $scope.Breaks.Composicao[_index][key] = value;
            });
        };
        $scope.CancelaEdicao();
    };
    //===========================Alteracao
    $scope.EditarBreak = function (pBreak) {
        $scope.Operacao= 'Alteracao'
        $scope.ListaBreaks.push(pBreak.Breaks);
        $scope.ListaSequencias.push(pBreak.Sequencia);
        angular.copy(pBreak, $scope.Temp_Break);
        $scope.Temp_Break.Tipo_Break = $scope.Intervalo[pBreak.Tipo_Break.Codigo];
        $scope.ShowGrid = false;
        $scope.ShowEdit = true;
        $scope.ShowGravacao = false;
        $scope.ShowStatus= false;

    };
    //===========================Confirmar a exclusao do Break
    $scope.ExcluirBreak = function (pBreak) {
        swal({
            title: 'Confirma a Exlusão do Break ?',
            showCancelButton: true,
            confirmButtonClass: "btn-warning",
            confirmButtonText: "Sim, Excluir",
            cancelButtonText: "Não,Cancelar",
            closeOnConfirm: true
        }, function () {
            for (var i = 0; i < $scope.Breaks.Composicao.length; i++) {
                if ($scope.Breaks.Composicao[i].Id_Item == pBreak.Id_Item) {
                    $scope.Breaks.Composicao.splice(i, 1);
                    $scope.RenumeraId($scope.Breaks.Composicao);
                    $scope.$digest();
                    break;
                };
            };
        });
    };
    //===========================Mudou o Break
    $scope.ChangeBreak = function (pBreak) {
        $scope.ListaSequencias = [];
        var _seq = 0;
        for (var i = 0; i < $scope.Breaks.Composicao.length; i++) {
            if (pBreak.Breaks == $scope.Breaks.Composicao[i].Breaks) {
                _seq = $scope.Breaks.Composicao[i].Sequencia;
                if ($scope.ListaSequencias.indexOf(_seq) == -1) {
                    $scope.ListaSequencias.push(_seq);
                }
                if ($scope.Breaks.Composicao[i].Hora_Inicio) {
                    pBreak.Hora_Inicio = $filter('hhmm')($scope.Breaks.Composicao[i].Hora_Inicio)
                }
            };
        };
        _seq++;
        $scope.ListaSequencias.push(_seq);
        pBreak.Sequencia = _seq;
    };
    //===========================Atualiza data da propagacao
    $scope.RefreshData = function (pBreak) {
        pBreak.Data_Inicio_Propagacao = $scope.Filtro.Data_Exibicao;
        pBreak.Data_Fim_Propagacao = pBreak.Ultimo_Dia_Break;
    };
    //===========================Limpa Filtro e scopes
    $scope.NovoFiltro= function () {
        $scope.ShowFilter = true;
        $scope.ShowGrid = false;
        $scope.ShowEdit = false;
        $scope.ShowGravacao = false;
        $scope.ShowStatus= false;
        $scope.Breaks=[]; 
        $scope.Temp_Break = {};
        $scope.ListaBreaks = [];
        $scope.ListaSequencias = [];
        $scope.MaxSequenciaFaixa = 0;
        $scope.Operacao = "";
        $scope.Filtro = $scope.NewFiltro();
    };
    //===========================Mover Up /Down
    $scope.MoverIntervalo = function (pBreak, pDirection)
    {
        var _oldIndex = -1;
        var _newIndex = -1;
        for (var i = 0; i < $scope.Breaks.Composicao.length; i++) {
            if ($scope.Breaks.Composicao[i].Id_Composicao == pBreak.Id_Composicao) {
                _oldIndex = i;
                break;
            };
        };
        _newIndex = _oldIndex + pDirection;
        if (pDirection < 0 &&  _newIndex <0) {
            ShowAlert("Mudança de Intervalo Inválida")
            return;
        }
        if (pDirection > 0 && _newIndex > $scope.Breaks.Composicao.length-1) {
            ShowAlert("Mudança de Intervalo Inválida")
            return;
        }
        
        if ($scope.Breaks.Composicao[_newIndex].Breaks != pBreak.Breaks) {
            ShowAlert("Mudança de Intervalo Inválida")
            return;
        }
        
        $scope.Breaks.Composicao.splice(_oldIndex, 1);
        $scope.Breaks.Composicao.splice(_newIndex, 0,pBreak);
        $scope.RenumeraId($scope.Breaks.Composicao);
    }
    //===========================Confirmar Gravacado do Break
    $scope.GravarBreak = function (pBreak) {
        httpService.Post("Roteiro/GravarBreak", pBreak).then(function (response) {
                if (response.data[0].Status==1) {
                    ShowAlert('Dados Gravados com Sucesso !')
                    $scope.CarregarListaBreaks($scope.Filtro);
                }
                else {
                    $scope.Critica = response.data;
                    $scope.ShowGravacao = false;
                    $scope.ShowStatus = true;
                };
        });
    };
    //===========================Botao Ok Critca
    $scope.CriticaOk = function () {
        $scope.ShowGravacao = false;
        $scope.ShowStatus = false;
        $scope.ShowGrid = true;
        $scope.Critica = "";

    };
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        
    });
}]);


