angular.module('App').controller('dictionaryController', ['$scope', 'httpService', '$document', function ($scope, httpService, $document) {
    $scope.elementToBind = undefined;
    $scope.dictionaryTable = undefined;
    $scope.MultiSelect = false;
    $scope.DicionaryValidate = function (elmId,pValidate) {
        if (pValidate == undefined) {
            pValidate = true;
        }
        var _element = $document[0].getElementById(elmId);
        if (_element.hasAttribute("secondarybind")) {
            _bind = _element.attributes['secondarybind'].value;
        }
        var _key = _element.value;
        var _dictionary = _element.attributes['dictionary'].value;

        if (_dictionary && _key && pValidate) {
            var _url = "ValidarTabela/" + _dictionary.trim() + "/" + _key.trim()
            httpService.Get(_url).then(function (response) {
                if (response.data[0].Status == 0) {
                    if (_bind) {
                        $scope.DicionarySetValue($document[0].getElementById(_bind), "");
                    }
                    ShowAlert(response.data[0].Mensagem, 'error', 2000);
                    $scope.DicionarySetValue(_element, "");
                    _element.focus();
                }
                else {
                    if (_bind) {
                        $scope.DicionarySetValue($document[0].getElementById(_bind), response.data[0].Descricao);
                    }
                }
            })
        }
        else {
            if (_bind) {
                $scope.DicionarySetValue($document[0].getElementById(_bind), "");
            }
        }
    };
    $scope.DicionaryLoadTable = function (pTable, pElement, pMultiSelect) {
        $scope.MultiSelect = pMultiSelect;
        $scope.elementToBind = pElement;
        var _url = "ListarTabela" + "/" + pTable;
        httpService.Get("ListarTabela/"+ pTable).then(function (response) {
            $scope.dictionaryTable = response.data;
        })
    };

    $scope.DicionaryLoadFromScope = function (pScopeFrom, pElement, pMultiSelect) {
        $scope.MultiSelect = pMultiSelect;
        $scope.elementToBind = pElement;
        $scope.dictionaryTable = pScopeFrom;
    };

    $scope.ItemSelectClickId = function (pCodigo, pDescricao) {
        $("#modal-Table").modal('toggle');
        $scope.filtrotexto = "";
        $scope.dictionaryTable = undefined;
        var _element = $document[0].getElementById($scope.elementToBind);
        var _primarybind = _element.attributes['primarybind'].value;
        _secondarybind = undefined;
        if (_element.attributes['secondarybind']) {
            _secondarybind = _element.attributes['secondarybind'].value;
        };

        if (_primarybind) {
            var _elm = $document[0].getElementById(_primarybind);
            $scope.DicionarySetValue(_elm, pCodigo)
        }
        if (_secondarybind) {
            var _elm = $document[0].getElementById(_secondarybind);
            $scope.DicionarySetValue(_elm, pDescricao)
        }
    };

    $scope.ItemSelectClickOk = function () {
        $("#modal-Table").modal('toggle');
        var _Target = $scope.dictionaryTable.filter(function (el) {
            return (el.Value === true);
        });
        var _element = $document[0].getElementById($scope.elementToBind);
        var _primarybind = _element.attributes['primarybind'].value;
        $scope.DicionarySetValue(_element, _Target)
        $scope.filtrotexto = "";
        $scope.dictionaryTable = undefined;
    };
    $scope.Marcar = function (pValue) {
        for (var i = 0; i < $scope.filteredSelect.length; i++) {
            $scope.filteredSelect[i].Value = pValue;
        }
    };
    $scope.DicionarySetValue = function (element, value) {
        element.value = value
        var ctrl = angular.element(element).data('$ngModelController');
        ctrl.$setViewValue(value);
        ctrl.$commitViewValue();
    };

}]);
