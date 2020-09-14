angular.module('App').controller('RoteiroController', ['$scope', '$rootScope', '$location', 'httpService', '$location', function ($scope, $rootScope, $location, httpService, $location, $routeParams) {

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
    $scope.Critica= "";
    $scope.ShowGuiaProgramas = false;
    $scope.ShowFiltro = true;
    $scope.CurrentTab = 'Midia'
    $scope.Consistencia={'Concorrencia':true,'Outros':true,'Rotativo':true};
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
        var _Url = 'Roteiro/CarregarRoteiro';
        httpService.Post(_Url, pFiltro).then(function (response) {
            if (response.data.length == 0) {
                ShowAlert("Não existe Roteiro para esse Veiculo/Data");
            }
            else {
                $scope.Roteiro = response.data;
                $scope.ShowGuiaProgramas = false;
                $scope.ShowFiltro = false;
                httpService.Post("Roteiro/CarregarComerciais", pFiltro).then(function (responseComercial) {
                    if (responseComercial.data) {
                        $scope.Comerciais = responseComercial.data;
                        $scope.RenumeraItens($scope.Roteiro);
                    }
                });
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
                console.log(_total_Encaixe_Programa);
                console.log(_total_Duracao_Programa);
                _totalBreak = 0;
                _totalArtistico = 0;
                _totalArtistico;
                _totalIntervalo;
                var _total_Encaixe_Programa = 0;
                var _total_Duracao_Programa = 0
            };
        };
    };
    //=================Consiste a Ordenacao
    $scope.ConsisteOrdenacao = function (Index_Origem, Index_Destino) {
        var _Mensagem = ""
        if (!Index_Destino) {
            return;
        }
        //----------------Tipos de Breaks Diferente nao pode soltar
        if ($scope.Comerciais[Index_Origem].Tipo_Break != null && $scope.Roteiro[Index_Destino].Tipo_Break != 3 && $scope.Comerciais[Index_Origem].Tipo_Break != $scope.Roteiro[Index_Destino].Tipo_Break) {
            _Mensagem += 'Comercial ' + $scope.Comerciais[Index_Origem].Nome_Tipo_Break;
            _Mensagem += ' não pode ser ordenado em um Intervalo ' + $scope.Roteiro[Index_Destino].Nome_Tipo_Break;
            ShowAlert(_Mensagem)
            return;
        };
        if ($scope.Comerciais[Index_Origem].Tipo_Break != null && $scope.Roteiro[Index_Destino].Tipo_Break == 3 && $scope.Comerciais[Index_Origem].Tipo_Break == 1) {
            _Mensagem = 'Comercial ' + $scope.Comerciais[Index_Origem].Nome_Tipo_Break;
            _Mensagem += ' não pode ser ordenado em um Intervalo ' + $scope.Roteiro[Index_Destino].Nome_Tipo_Break;
            _Mensagem += '.'
            ShowAlert(_Mensagem)
            return;
        };
        //---------------------Choque de Concorrencia
        for (var i = 0; i < $scope.Roteiro.length; i++) {
            if ($scope.Roteiro[i].Indica_Comercial && $scope.Roteiro[i].Id_Break == $scope.Roteiro[Index_Destino].Id_Break) {
                if ($scope.Roteiro[i].Cod_Produto.substr(0, 6) == $scope.Comerciais[Index_Origem].Cod_Produto.substr(0, 6) && $scope.Consistencia.Concorrencia && $scope.Comerciais[Index_Origem].Cod_Produto.trim()!="") {
                    _Mensagem += "Existe um produto concorrente nesse break."
                    break;
                };
            };
        };
        //---------------------Programas diferentes
        if (($scope.Roteiro[Index_Destino].Cod_Programa != $scope.Comerciais[Index_Origem].Cod_Programa) && ($scope.Comerciais[Index_Origem].Pasta == 'Midia' || $scope.Comerciais[Index_Origem].Pasta == 'Outros')) {
            if ($scope.Consistencia.Outros) {
                _Mensagem += "\n Comercial não pertence a esse Programa."
            }
        }
        //---------------------Horarios de Rotativos
        if ($scope.Comerciais[Index_Origem].Pasta == 'Rotativo') {
            var _valido = true;
            if ($scope.Comerciais[Index_Origem].Hora_Fim_Programa < $scope.Roteiro[Index_Destino].Hora_Inicio_Programa
                || $scope.Comerciais[Index_Origem].Hora_Inicio_Programa > $scope.Roteiro[Index_Destino].Hora_Fim_Programa) {
                if ($scope.Consistencia.Rotativo) {
                    _Mensagem += "\n Horario do Programa Rotativo está fora do Horário do Programa."
                }
            };
        };
        //---------------------Confirma a Ordenacao
        if (_Mensagem) {
            _Mensagem += "\n Deseja ordenar mesmo assim ? "
            swal({
                title: _Mensagem,
                fontsize: 11,
                showCancelButton: true,
                confirmButtonClass: "btn-warning",
                confirmButtonText: "Sim, Ordenar",
                cancelButtonText: "Não,Cancelar",
                closeOnConfirm: true
            }, function () {
                $scope.Ordenacao(Index_Origem, Index_Destino);
            });
        }
        else {
            $scope.Ordenacao(Index_Origem, Index_Destino);
        }
    }
    //=================Ordenacao / Drag and Drop de Comercial para Roteiro
    $scope.Ordenacao = function (Index_Origem, Index_Destino) {
        //----------------Cria o item para o comercial que foi arrastado
        var newItem = {};
        angular.copy($scope.Roteiro[Index_Destino], newItem);
        newItem.Indica_Titulo_Programa = false;
        newItem.Indica_Titulo_Break = false;
        newItem.Indica_Titulo_Intervalo = false;
        newItem.Indica_Comercial = true;
        newItem.Titulo_Comercial = $scope.Comerciais[Index_Origem].Titulo_Comercial;
        newItem.Cod_Comercial = $scope.Comerciais[Index_Origem].Cod_Comercial;
        newItem.Duracao = $scope.Comerciais[Index_Origem].Duracao;
        newItem.Cod_Produto = $scope.Comerciais[Index_Origem].Cod_Produto;
        newItem.Nome_Produto = $scope.Comerciais[Index_Origem].Nome_Produto;
        newItem.Numero_Fita = $scope.Comerciais[Index_Origem].Numero_Fita;
        newItem.Id_Fita = $scope.Comerciais[Index_Origem].Id_Fita;
        newItem.Origem = $scope.Comerciais[Index_Origem].Origem;
        newItem.Cod_Empresa = $scope.Comerciais[Index_Origem].Cod_Empresa;
        newItem.Numero_Mr = $scope.Comerciais[Index_Origem].Numero_Mr;
        newItem.Sequencia_Mr = $scope.Comerciais[Index_Origem].Sequencia_Mr;
        newItem.Cod_Programa_Origem = $scope.Comerciais[Index_Origem].Cod_Programa;
        newItem.Cod_Veiculo_Origem = $scope.Comerciais[Index_Origem].Cod_Veiculo;
        newItem.Cod_Data_Exibicao = $scope.Comerciais[Index_Origem].Data_Exibicao;
        newItem.Chave_Acesso = $scope.Comerciais[Index_Origem].Chave_Acesso;
        //----------------Insere Novo Comercial no Roteiro

        $scope.Roteiro.splice(Index_Destino + 1, 0, newItem);

        //----------------Remove Comercial do grid de comerciais
        if ($scope.Comerciais[Index_Origem].Numero_Mr) {
            $scope.Comerciais[Index_Origem].Indica_Ordenado = true;
        }
        //----------------Renumera Itens
        $scope.RenumeraItens($scope.Roteiro)
        //----------------Atualiza scope
        $scope.$digest();
    }
    //===========================Desordenar Comercial do Roteiro / Devolve para table de comerciais
    $scope.Desordenar = function (pItem) {
        if (pItem.Numero_Mr) {
            //------------------------Devolve comercial
            for (var i = 0; i < $scope.Comerciais.length; i++) {
                if ($scope.Comerciais[i].Cod_Veiculo == pItem.Cod_Veiculo_Origem && $scope.Comerciais[i].Cod_Programa == pItem.Cod_Programa_Origem && $scope.Comerciais[i].Chave_Acesso == pItem.Chave_Acesso) {
                    $scope.Comerciais[i].Indica_Ordenado = false;
                    break;
                };
            };
        }
        //------------------------Exclui da Ordenacao
        $scope.Roteiro.splice(pItem.Id_Item, 1);
        //----------------Renumera Itens
        $scope.RenumeraItens($scope.Roteiro)
    };
    //===========================Cortar Comercial do Roteiro
    $scope.CortarRoteiro = function (pComercial) {
        swal({
            title: "Tem certeza que deseja Excluir com Baixa esse Comercial?",
            //type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir e Baixar",
            cancelButtonText: "Não,Cancelar",
            closeOnConfirm: true
        }, function () {
            httpService.Post("Roteiro/BaixarVeiculacao", pComercial).then(function (response) {
                if (response.data[0].Status == 1) {
                    $scope.Comerciais.splice(pComercial.Id_Item, 1)
                }
                ShowAlert(response.data[0].Mensagem);
            })
        });
    };
    //===========================Mudar Comercial de Posicao no Roteiro
    $scope.MoverComercial = function (pRoteiro, pIndex, pDirection) {
        var _NewComercial = {};
        var _NewIndex = -1;
        var _Id_Break = pRoteiro[pIndex].Id_Break;
        var _Break = pRoteiro[pIndex].Break;
        var _Sequencia_Faixa = pRoteiro[pIndex].Sequencia_Faixa;
        var _Tipo_Break = pRoteiro[pIndex].Tipo_Break;
        var _Id_Intervalo = pRoteiro[pIndex]._Id_Intervalo;
        if (pDirection == 1) {
            for (var i = pIndex + 1; i < pRoteiro.length; i++) {
                if (pRoteiro[i].Indica_Titulo_Programa) {
                    break;
                };

                if (pRoteiro[i].Indica_Titulo_Intervalo && pRoteiro[i].Tipo_Break != pRoteiro[pIndex].Tipo_Break && pRoteiro[pIndex].Origem != 'Artistico') {
                    break;
                };

                if (pRoteiro[i].Indica_Titulo_Intervalo && pRoteiro[i].Tipo_Break != 2) {
                    _Id_Break = pRoteiro[i].Id_Break;
                    _Break = pRoteiro[i].Break;
                    _Id_Intervalo = pRoteiro[i].Id_Intervalo;
                    _Sequencia_Faixa = pRoteiro[i].Sequencia_Faixa;
                    _Tipo_Break = pRoteiro[i].Tipo_Break
                    _NewIndex = i + 1;
                    break;
                };
                if (pRoteiro[i].Indica_Comercial) {
                    _Id_Break = pRoteiro[i].Id_Break;
                    _Id_Intervalo = pRoteiro[i].Id_Intervalo;
                    _Break = pRoteiro[i].Break;
                    _Sequencia_Faixa = pRoteiro[i].Sequencia_Faixa;
                    _Tipo_Break = pRoteiro[i].Tipo_Break
                    _NewIndex = i + 1;
                    break;
                };

            };
        };
        if (pDirection == -1) {
            for (var i = pIndex - 1; i > 0; i--) {
                if (pRoteiro[i].Indica_Titulo_Programa) {
                    break;
                };

                if (pRoteiro[i].Tipo_Break != pRoteiro[pIndex].Tipo_Break && pRoteiro[pIndex].Origem != 'Artistico') {
                    break;
                };

                if (pRoteiro[i].Indica_Titulo_Intervalo && pRoteiro[i].Tipo_Break != 2 && pRoteiro[i].Id_Break != pRoteiro[pIndex].Id_Break) {
                    _Id_Break = pRoteiro[i].Id_Break;
                    _Id_Intervalo = pRoteiro[i].Id_Intervalo;
                    _Tipo_Break = pRoteiro[i].Tipo_Break;
                    Break = pRoteiro[i].Break;
                    _Sequencia_Faixa = pRoteiro[i].Sequencia_Faixa;
                    _NewIndex = i + 1;
                    break;
                };
                if (pRoteiro[i].Indica_Comercial) {
                    _Id_Break = pRoteiro[i].Id_Break;
                    _Id_Intervalo = pRoteiro[i].Id_Intervalo;
                    _Tipo_Break = pRoteiro[i].Tipo_Break
                    _Break = pRoteiro[i].Break;
                    _Sequencia_Faixa = pRoteiro[i].Sequencia_Faixa;
                    if (pRoteiro[i].Id_Intervalo == pRoteiro[pIndex].Id_Intervalo) {
                        _NewIndex = i;

                    }
                    else {
                        _NewIndex = i + 1;
                    }
                    break;
                };

            };
        };

        if (_NewIndex == -1) {
            ShowAlert("Mudança  de Posição Inválida");
            return;
        }

        //-------------------Se Mudou de brak, testa choque de concorrencia novamente
        _Mensagem = "";
        if (pRoteiro[pIndex].Id_Break != _Id_Break) {
            for (var i = 0; i < pRoteiro.length; i++) {
                if (pRoteiro[i].Indica_Comercial && pRoteiro[i].Id_Break == _Id_Break) {
                    if (pRoteiro[i].Cod_Produto.substr(0, 6) == pRoteiro[pIndex].Cod_Produto.substr(0, 6) && pRoteiro[i].Cod_Produto) {
                        _Mensagem += "Existe um produto concorrente nesse break ."
                        break;
                    };
                };
            };
        };

        if (_Mensagem) {
            _Mensagem += "\n Deseja mudar de posição mesmo assim ? "
            swal({
                title: _Mensagem,
                fontsize: 11,
                showCancelButton: true,
                confirmButtonClass: "btn-warning",
                confirmButtonText: "Sim, Mudar",
                cancelButtonText: "Não,Cancelar",
                closeOnConfirm: true
            }, function () {
                //-------------------Insere o comercial na nova sequencia
                angular.copy(pRoteiro[pIndex], _NewComercial);
                _NewComercial.Id_Break = _Id_Break;
                _NewComercial.Id_Intervalo = _Id_Intervalo;
                _NewComercial.Break = _Break;
                _NewComercial.Sequencia_Faixa = _Sequencia_Faixa;
                _NewComercial.Tipo_Break = _Tipo_Break;
                pRoteiro.splice(_NewIndex, 0, _NewComercial);
                //-------------------Remove  comercial da antiga sequencia
                if (pDirection == 1) {
                    pRoteiro.splice(pIndex, 1);
                }
                else {
                    pRoteiro.splice(pIndex + 1, 1);
                };
                $scope.RenumeraItens(pRoteiro);
                $scope.$digest();
            });
        }
        else {
            //-------------------Insere o comercial na nova sequencia
            angular.copy(pRoteiro[pIndex], _NewComercial);
            _NewComercial.Id_Break = _Id_Break
            _NewComercial.Id_Intervalo = _Id_Intervalo;
            _NewComercial.Break = _Break;
            _NewComercial.Tipo_Break = _Tipo_Break;
            _NewComercial.Sequencia_Faixa = _Sequencia_Faixa;
            pRoteiro.splice(_NewIndex, 0, _NewComercial);
            //-------------------Remove  comercial da antiga sequencia
            if (pDirection == 1) {
                pRoteiro.splice(pIndex, 1);
            }
            else {
                pRoteiro.splice(pIndex + 1, 1);
            }
            $scope.RenumeraItens(pRoteiro);
        };
    };

    //===========================Excluir o Roteiro
    $scope.ExcluirRoteiro = function (pFiltro) {
        var _data = { 'Cod_Veiculo': pFiltro.Cod_Veiculo, 'Data_Exibicao': pFiltro.Data_Exibicao, 'Cod_Programa': pFiltro.Cod_Programa }
        swal({
            title: "Essa operação excluir o Roteiro total nesse dia. Tem certeza que deseja continuar ?",
            //type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "Sim, Excluir",
            cancelButtonText: "Não,Cancelar",
            closeOnConfirm: true
            
        }, function () {
            httpService.Post("Roteiro/Excluir", _data).then(function (response) {
                if (response) {
                    ShowAlert("Roteiro excluido com Sucesso")
                    $scope.CarregarRoteiro(pFiltro);
                }
            });
        });
    };
    //===========================Salvar  o Roteiro
    $scope.SalvarRoteiro = function (pRoteiro) {
        httpService.Post("Roteiro/Salvar", pRoteiro).then(function (response) {
            if (response) {
                $scope.Critica = response.data;
            }
        });
    };
    
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        $rootScope.routeloading = false;
    });
}]);


