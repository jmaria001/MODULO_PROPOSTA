"use strict";
function GetDictionary() {
    //=======dicionario
    var _dicionario = [
                        { 'Id': 'Simulacao_Identificacao', 'label': 'Identificação', 'caption': 'Identificação', 'atributos': { 'maxlength': '50', 'type': 'text', 'keymode': 'text', } },
                        { 'Id': 'Data', 'label': 'Data', 'icon': 'calendarDay', 'atributos': { 'maxlength': '10', 'type': 'text', 'keymode': 'numeric', 'placeholder': 'ddmmyyyy' } },
                        { 'Id': 'Competencia', 'label': 'Competência', 'icon': 'calendarMonth', 'atributos': { 'maxlength': '7', 'type': 'text', 'keymode': 'numeric', 'placeholder': 'mes/ano' } },
                        { 'Id': 'Money', 'label': '', 'icon': '', 'atributos': { 'maxlength': '15', 'type': 'text', 'keymode': '', 'placeholder': '/ano' } },
                        { 'Id': 'Empresa_Usuario', 'label': 'Empresa', 'icon': 'search', 'atributos': { 'maxlength': '3', 'type': 'text', 'keymode': 'numeric'} },
                        { 'Id': 'Mercado', 'label': 'Mercado', 'icon': 'search', 'atributos': { 'maxlength': '5', 'type': 'text' } },
                        { 'Id': 'Pacote_Desconto', 'label': 'Pacote de Desconto', 'icon': 'search', 'atributos': { 'maxlength': '30', 'type': 'text', 'keymode': 'numeric' } },
                        { 'Id': 'Agencia', 'label': 'Agência', 'icon': 'search', 'atributos': { 'maxlength': '6', 'type': 'text', 'keymode': 'numeric', 'filter': true } },
                        { 'Id': 'Cliente', 'label': 'Cliente', 'icon': 'search', 'atributos': { 'maxlength': '6', 'type': 'text', 'keymode': 'numeric', 'filter': true } },
                        { 'Id': 'Contato', 'label': 'Contato', 'icon': 'search', 'atributos': { 'maxlength': '6', 'type': 'text', 'keymode': 'numeric'} },

    ];

    //==============Config html
    var elements = document.getElementsByClassName("dictionary");
    for (var i = 0, len = elements.length; i < len; i++) {
        
        //======================Obtem o dicionario
        var _dictionaryName = elements[i].attributes.dictionary;
        var _def = GetDicionarioDef(_dictionaryName.value);
        var _text_name = 'txt' + _dictionaryName.value + '_' + i.toString();
        var _caption = elements[i].attributes.caption;
        var _required = elements[i].attributes.required;
        var _control_group = 'group_' + +i.toString();
        var _icon = true
        
        if (elements[i].attributes.icon ) {
            _icon =  elements[i].attributes.icon.value==true;
        } 
        
        var _inputAttributes = elements[i].getElementsByTagName('input')[0];
        //=======================form-group
        var _formgroup = document.createElement("div");
        _formgroup.setAttribute("class", "form-group");
        elements[i].appendChild(_formgroup);

        //==================Label  dentro do form-group
        var _label = document.createElement("label");
        _label.setAttribute("style","white-space:nowrap")
        _label.setAttribute("for", _text_name);

        if (_required) {
            _label.setAttribute("class", 'fieldrequired field-x');
        }
        else
        {
            _label.setAttribute("class", 'field-x');
        }
        
        if (_caption) {
            _label.innerHTML = _caption.value;
        }
        else {
            _label.innerHTML = _def.label;
        }
        _formgroup.appendChild(_label);

        //==================Input-group dentro do form-group
        var _inputgroup = document.createElement("div");
        //_inputgroup.setAttribute("class", "input-group input-group-sm");
        _inputgroup.setAttribute("class", "input-group");
        _formgroup.appendChild(_inputgroup);
        if (_def.icon && _icon ) {
            
            //==================Input-group add on dentro do inputgroup
            var _inputgroupaddon = document.createElement("div");
            _inputgroupaddon.setAttribute("class", "input-group-addon");
            _inputgroup.appendChild(_inputgroupaddon);

            //==================Span dentro do  Input-group add 
            var _span = document.createElement("span")
            switch (_def.icon) {
                case 'calendarDay':
                    _span.setAttribute("class", "fa fa-calendar dropdown-toggle");
                    _span.setAttribute("data-toggle", "dropdown");
                    _span.setAttribute("control_group", 'icon_' +_control_group);
                    break;
                case 'calendarMonth':
                    _span.setAttribute("class", "fa fa-calendar dropdown-toggle");
                    _span.setAttribute("data-toggle", "dropdown");
                    _span.setAttribute("control_group", 'icon_' + _control_group);
                    break;
                case 'search':
                        _span.setAttribute("class", "fa fa-search dropdown-toggle");
                        _span.setAttribute("onclick", "ShowSelectItem('" + _text_name + "'," + 'false' + ")");
                        _span.setAttribute("control_group", 'icon_' + _control_group);
                    break;
                default:
            }
            
            _span.setAttribute("style", "cursor: pointer");
            _inputgroupaddon.appendChild(_span);

            //inclui a diretiva Mes ano na Input-group add 
            var _uiCompetencia = elements[i].getElementsByClassName('dictionary-ui-competencia')[0];
            if (_uiCompetencia) {
                _inputgroupaddon.appendChild(_uiCompetencia);
            }
            //inclui a diretiva Data na Input-group add 
            var _calendar = elements[i].getElementsByClassName('dictionary-ui-calendar')[0];
            if (_calendar) {
                _inputgroupaddon.appendChild(_calendar);
            }''
        }
        //==================Input text  dentro do inputgroup
        var _input = elements[i].getElementsByTagName('input')[0];
        _input.setAttribute("name", _text_name);
        _input.setAttribute("dictionary", _dictionaryName.value);
        _input.setAttribute("class", "form-control");
        _input.setAttribute("onkeypress", "return DicionarioKeyPress(this,event)");
        _input.setAttribute("onkeyup", "DicionarioKeyUp(this)");
        _input.setAttribute("control_group", 'input_' + _control_group);
        for (var key in _def.atributos) {
            _input.setAttribute(key, _def.atributos[key]);
        }
        //append final
        _inputgroup.appendChild(_input);
        elements[i].appendChild(_formgroup);
    }

    //===Format
    //$("input[Format='Decimal']").on("blur", function () {
    //    var _integer = '';
    //    var _dec = '';
    //    var _scale = this.getAttribute('Scale');
    //    if (!_scale) {
    //        _scale = 2;
    //    }

    //    if (this.value.indexOf(",")==-1) {
    //        this.value += ',' + '0'.repeat(_scale);
    //    }

    //    _integer = this.value.substr(0, this.value.length - (_scale) - 1);
    //    _dec = this.value.substr(this.value.indexOf(",")+1, _scale);
    //    var _format = ''
    //    var z = 0;
    //    for (var i = _integer.length-1; i > -1; i--) {
    //        z++
    //        if (z == 4) {
    //            _format = '.' + _format;
    //            z = 1
    //        }
    //        _format = _integer.substr(i, 1) + _format
    //    }
    //    this.value = _format+ ',' + _dec;
        
    //});
    //$("input[Format='Decimal']").on("focus", function () {
    //    this.value = this.value.replace('.', ',');
        
    //});
    $("input[Format='Decimal']").attr("onkeypress", "return DecimalOnly(event)")
    $("input[Format='Numeric']").attr("onkeypress", "return NumericOnly(event)")
    
    function GetDicionarioDef(pName) {
        for (var i = 0; i < _dicionario.length; i++) {
            if (_dicionario[i].Id==pName) {
                return (_dicionario[i])
            }
        }

        var _ix = _dicionario.findIndex(x => x.Id === pName);
        if (_ix == -1) {
            alert('Adicionar ' + pName + ' em dictionary.js')
        }
        return _dicionario[_ix];
    }

    $(".decimal, .money .percent").attr("onkeypress", "return DecimalOnly(event)")

    $(".numeric,.cnpj calendar-date").attr("onkeypress", "return NumericOnly(event)")
    $(".money").blur(function () {
        this.value = MoneyFormat(this.value);
    });
    $(".decimal").blur(function () {
        this.value = DecimalFormat(this.value);
    });
    $(".percent").blur(function () {
        this.value = PercentFormat(this.value);
    });

    $(".money").focus(function () {
        this.value = DecimalUnformat(this.value);
    });
    $(".cnpj").focus(function () {
        this.value = cnpjUnformat(this.value);
    });
    $(".cnpj").blur(function () {
        this.value = cnpjFormat(this.value);
    });
}


function DicionarioKeyPress(elm, e) {

    if (e.charCode == 8 || e.charCode == 9 || e.charCode == 0) {
        return true;
    }
    switch (elm.getAttribute("keymode")) {  
        case 'numeric':
            return (e.charCode >= 48 && e.charCode <= 57);
            break;
        case 'upper':
            return true;
            break;
        case 'lower':
            return true;
            break;
        case 'text':
            return true;
            break;
        case 'decimal':
            return ((e.charCode >= 48 && e.charCode <= 57) || e.charCode == 44 || e.charCode == 46);
            break;
        default:
            return true;
            break
    }
}
function DicionarioKeyUp(elm) {
    
    switch (elm.getAttribute("keymode")) {
        case 'numeric':
            break;Ffo
        case 'upper':
            elm.value = elm.value.toUpperCase();
            break;
        case 'lower':
            elm.value = elm.value.toLowerCase();
            break;
        case 'text':
            break;
        case 'decimal':
            break;
        default:
            return true;
            break
    }
}
function ShowSelectItem(elm, pMultiSelect) {
    var _element = document.getElementsByName(elm);
    if ($(_element).is(':disabled')) {
        return;
    }
    var _filter = false;
    if (_element[0].attributes.filter) {
        _filter =_element[0].attributes.filter.value;
    }
    var _table = _element[0].attributes.dictionary.value;
    var _elemendId = _element[0].attributes.id.value;
    $("#modal-Table").modal();
    angular.element(document.getElementById('modal-Table')).scope().DicionaryLoadTable(_table, _elemendId, pMultiSelect,_filter);
}

