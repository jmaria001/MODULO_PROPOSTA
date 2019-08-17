angular.module("App").directive("uiDate", function ($filter) {
    return {
        require: "ngModel",
        link: function (scope, element, attrs, ctrl) {

            element.bind("keyup", function () {
                if (ctrl.$viewValue) {

                
                var _data = ctrl.$viewValue.replace(/[^0-9]+/g, "");
                if (_data.length > 8)
                {
                    _data = _data.substring(0, 8);
                }
                ctrl.$setViewValue(_data);
                ctrl.$render();
                }
            });
            
            element.bind("focus", function () {
                if (ctrl.$viewValue) {
                    var _data = ctrl.$viewValue.replace(/[^0-9]+/g, "");
                    ctrl.$setViewValue(_data);
                    ctrl.$render();
                }
            });

            element.bind('blur', function () {
                var _date = ctrl.$viewValue
                var _dia = "";
                var _mes = "";
                var _ano = "";
                var _newdata = "";
                if (_date) {
                    switch (_date.length) {
                        case 6:
                            _dia = _date.substring(0, 2);
                            _mes = _date.substring(2, 4);
                            _ano = '20' + _date.substring(4, 6);
                            break
                        case 8:
                            _dia = _date.substring(0, 2);
                            _mes = _date.substring(2, 4);
                            _ano = _date.substring(4, 8);
                            break
                        default:
                            //swal('', '[' + _newdata + ']' + 'Data Inválida', 'warning');
                            ShowAlert('Data Inválida', 'error', 2000);
                            ctrl.$setViewValue("");
                            ctrl.$render();
                            return
                            break;
                    }
                    _newdata = _dia + '/' + _mes + '/' + _ano
                    if (!IsDate(_newdata)) {
                        //swal('', '[' + _newdata + ']' + 'Data Inválida', 'warning');
                        ShowAlert('Data Inválida', 'error', 2000);
                        _newdata = '';
                        ctrl.$setViewValue(_newdata);
                        ctrl.$render();
                    }
                    else{
                        ctrl.$setViewValue(_newdata);
                        ctrl.$render();
                    }
                }
            });
        }
    };
});

angular.module("App").directive("uiMesano", function ($filter) {
    return {
        require: "ngModel",
        link: function (scope, element, attrs, ctrl) {
            
            element.bind("keyup", function () {
                if (ctrl.$viewValue) {

                var _data = ctrl.$viewValue.replace(/[^0-9]+/g, "");
                if (_data.length > 6) {
                    _data = _data.substring(0, 6);
                }
                ctrl.$setViewValue(_data);
                ctrl.$render();
                }
            });

            element.bind("focus", function () {
                if (ctrl.$viewValue) {
                var _data = ctrl.$viewValue.replace(/[^0-9]+/g, "");
                ctrl.$setViewValue(_data);
                ctrl.$render();
                }
            });

            element.bind('blur', function () {
                var _date = ctrl.$viewValue
                var _mes = "";
                var _ano = "";
                var _newdata = "";
                if (_date) {
                    switch (_date.length) {
                        case 4:
                            _mes = _date.substring(0, 2);
                            _ano = '20' + _date.substring(2, 4);

                            break
                        case 6:
                            _mes = _date.substring(0, 2);
                            _ano = _date.substring(2, 6);
                            break
                        default:
                            //swal('', 'Competência Inválida', 'warning');
                            ShowAlert('Competência Inválida', 'error', 2000);
                            return
                            break;
                    }
                    _newdata = _mes + '/' + _ano
                    if (parseInt(_mes) < 1 || parseInt(_mes) > 12) 
                    {
                        ShowAlert('Competência Inválida', 'error', 2000);
                        //swal('', '[' + _newdata + ']' + 'Competência Inválida', 'warning');
                        ctrl.$setViewValue('');
                        ctrl.$render();
                    }
                    else {
                        
                        ctrl.$setViewValue(_newdata);
                        ctrl.$render();
                    }
                }
            });
        }
    };
});


function IsDate(pData) //Valida e formata uma pData
{
    //=========================================Consiste o tamanho 
    pData= pData.replace(/[^0-9]+/g, "");
    if (pData.length != 6 && pData.length != 8 )
    {
        return false;
    }
    //=========================================Consiste o Conteudo
    
    var dia = pData.substring(0, 2);
    var mes = pData.substring(2, 4);
    var ano = pData.substring(4, 6);
    var ultimo_dia = 0;
    var valido = true;
    if (pData.length = 8) {
        ano = pData.substring(4, 8);
    }
    if (ano < 100) {
        ano = "20" + ano;
    }
    if (parseInt(mes, 10) < 1 || parseInt(mes, 10) > 12) {
        valido = false;
    }
    if (parseInt(ano, 10) < 0 || parseInt(ano, 10) > 2999) {
        valido = false;
    }
    switch (mes) {
        case "01":
        case "03":
        case "05":
        case "07":
        case "08":
        case "10":
        case "12":
            {
                ultimo_dia = 31;
                break
            }
        case "04":
        case "06":
        case "09":
        case "11":
            {
                ultimo_dia = 30;
                break
            }

        case "02":
            {
                ultimo_dia = 28;
                if ((ano % 4 == 0) && ((ano % 100 != 0) || (ano % 400 == 0))) {
                    ultimo_dia = 29;
                }
                else {
                    ultimo_dia = 28;
                }
            }
    }
    if (parseInt(dia, 10) < 0 || parseInt(dia, 10) > ultimo_dia) {
        valido = false;
    }
    return valido;
    
}