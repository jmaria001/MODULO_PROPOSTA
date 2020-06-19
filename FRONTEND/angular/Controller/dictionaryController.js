angular.module('App').controller('dictionaryController', ['$scope', 'httpService', '$document', function ($scope, httpService, $document) {
    $scope.elementToBind = undefined;
    $scope.dictionaryTable = undefined;
    $scope.MultiSelect = false;
    $scope.ShowFilter = false;
    $scope.DicionaryValidate = function (elmId,pValidate) {
        if (pValidate == undefined) {
            pValidate = true;
        }
        var _element = $document[0].getElementById(elmId);
        var _bind = undefined;
        var _extrabind = undefined;
        if (_element.hasAttribute("secondarybind")) {
            _bind = _element.attributes['secondarybind'].value;
        }
        if (_element.hasAttribute("extrabind")) {
            _extrabind = _element.attributes['extrabind'].value;
        }
        var _key = _element.value;
        var _dictionary = _element.attributes['dictionary'].value;

        if (_dictionary && _key && pValidate) {
            var _url = "ValidarTabela/" + _dictionary.trim() + "/" + _key.trim()
            httpService.Get(_url).then(function (response) {
                if (response.data[0].Status == 0) {
                    $scope.DicionarySetValue($document[0].getElementById(_bind), "");
                    $scope.DicionarySetValue($document[0].getElementById(_extrabind), "");
                    ShowAlert(response.data[0].Mensagem, 'warning', 2000);
                    $scope.DicionarySetValue(_element, "");
                    _element.focus();
                }
                else {
                    $scope.DicionarySetValue($document[0].getElementById(_bind), response.data[0].Descricao);
                    $scope.DicionarySetValue($document[0].getElementById(_extrabind), response.data[0].Extra);
                }
            })
        }
        else {
            $scope.DicionarySetValue($document[0].getElementById(_bind), "");
            $scope.DicionarySetValue($document[0].getElementById(_extrabind), "");
        }
    };
    $scope.DicionaryLoadTable = function (pTable, pElement, pMultiSelect, pShowFilter) {
        $scope.MultiSelect = pMultiSelect;
        $scope.ShowFilter = pShowFilter;
        $scope.elementToBind = pElement;
        $scope.TableName = pTable;
        var _url = "ListarTabela" + "/" + pTable;
        if (!$scope.ShowFilter) {
            httpService.Get("ListarTabela/" + pTable).then(function (response) {
                $scope.dictionaryTable = response.data;
            })
        }
        else {
            $scope.dictionaryTable = [];
        }
        $scope.$digest();
    };
    $scope.DicionaryLoadWithFilter = function (pFilter) {
        httpService.Get("ListarTabela/" + $scope.TableName + "/" + pFilter).then(function (response) {
            if (response.data) {
                $scope.dictionaryTable = response.data;
                $scope.filtrotexto = "";
            }
        })
    };
    $scope.DicionaryLoadFromScope = function (pScopeFrom, pElement, pMultiSelect) {
        $scope.MultiSelect = pMultiSelect;
        $scope.elementToBind = pElement;
        $scope.dictionaryTable = pScopeFrom;
    };

    $scope.ItemSelectClickId = function (pCodigo, pDescricao, pExtra) {
        $("#modal-Table").modal('toggle');
        $scope.filtrotexto = "";
        $scope.dictionaryTable = undefined;
        var _element = $document[0].getElementById($scope.elementToBind);
        var _primarybind = _element.attributes['primarybind'].value;
        var _secondarybind = undefined;
        var _extrabind = undefined;

        if (_element.attributes['secondarybind']) {
            _secondarybind = _element.attributes['secondarybind'].value;
        };
        if (_element.attributes['extrabind']) {
                _extrabind = _element.attributes['extrabind'].value;
        };

        if (_primarybind) {
            var _elm = $document[0].getElementById(_primarybind);
            $scope.DicionarySetValue(_elm, pCodigo)
        }
        if (_secondarybind) {
            var _elm = $document[0].getElementById(_secondarybind);
            $scope.DicionarySetValue(_elm, pDescricao)
        }
        if (_extrabind) {
            var _elm = $document[0].getElementById(_extrabind);
            $scope.DicionarySetValue(_elm, pExtra)
        }
    };

    $scope.CloseSelect = function () {
        $scope.MultiSelect = "";
        $scope.ShowFilter = false;
        $scope.elementToBind = "";
        $scope.TableName = "";
        $scope.dictionaryTable = undefined;
        $("#modal-Table").modal('toggle');
    };

    $scope.ItemSelectClickOk = function () {
        
        var _Target = $scope.dictionaryTable.filter(function (el) {
            return (el.Value === true);
        });
        var _element = $document[0].getElementById($scope.elementToBind);
        var _primarybind = _element.attributes['primarybind'].value;
        $scope.DicionarySetValue(_element, _Target)
        $scope.CloseSelect()
    };


    $scope.Marcar = function (pValue) {
        for (var i = 0; i < $scope.filteredSelect.length; i++) {
            $scope.filteredSelect[i].Value = pValue;
        }
    };
    $scope.DicionarySetValue = function (element, value) {
        if (element) {

        
        element.value = value
        var ctrl = angular.element(element).data('$ngModelController');
        ctrl.$setViewValue(value);
        ctrl.$commitViewValue();
        }
    };
    $scope.PesquisaTabelasMarcarTodos = function (pValue)
    {
        for (var i = 0; i < $scope.PesquisaTabelas.Items.length; i++) {
            $scope.PesquisaTabelas.Items[i].Selected = pValue;
        }
    }
}]);
