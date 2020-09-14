angular.module('App').filter('percentage', ['$filter', function ($filter) {
    return function (input, decimals) {
        if (input) {
            input = DoubleVal(input);
            return $filter('number')(input, decimals) + '%';
        }
        else {
            return '';
        }

    };
}]);

angular.module('App').filter('MesExtenso', [function () {
    return function (input) {
        pdata = input;
        var _mes = "";
        var _ano = "";
        if (!pdata) {
            return 'Sem Data';
        }
        pdata = pdata.replace(/[^0-9]+/g, "");
        if (pdata.length != 4 && pdata.length != 6) {
            return 'Sem Data';
        }
        var _mes = parseInt(pdata.substring(0, 2));
        if (pdata.length==4) {
            _ano = parseInt(pdata.substr(2, 2));
            _ano = '20' + _ano;
        }
        else {
            _ano = parseInt(pdata.substr(2, 4));
        }
        
        if (_mes < 1 || _mes > 12) {
            return 'Sem Data';
        }
        else{
            return ['', 'Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'][_mes] + '/' + _ano
        }
    };
}]);
angular.module('App').filter('hhmm', [function () {
    return function (input) {
        pdata = input;
        if (!pdata) {
            return 
        }
        pdata = pdata.replace(/[^0-9]+/g, "");
        return pdata.substr(0, 2) + ":" + pdata.substr(2, 2);
    };
}]);

angular.module('App').filter('range', function () {
    return function (input, min, max) {
        min = parseInt(min); //Make string input int
        max = parseInt(max);
        for (var i = min; i <= max; i++)
        input.push(i);
        return input;
    };
});

angular.module('App').filter('rangeDiaInicio', function () {
    return function (input, PeriodoInicio, PeriodoFim, Competencia) {
        var min = 0;
        var max = 0;
        if (Competencia) {
            
            var _year = parseInt(Competencia.substring(3, 7))
            var _month = parseInt(Competencia.substring(0, 2))
            
            var _lastDay = new Date(_year, _month , 0);
            var _firstDay = new Date(_year, _month - 1, 1);
            var from = PeriodoInicio.split("/")
            var _DatabaseInicio = new Date(from[2], from[1] - 1, from[0])

            var from = PeriodoFim.split("/")
            var _DatabaseFim = new Date(from[2], from[1] - 1, from[0])
            min = _firstDay.getDate();
            if (_DatabaseInicio > _firstDay) {

                min = _DatabaseInicio.getDate();
            }
            max = _lastDay.getDate();
            if (_DatabaseFim < _lastDay) {
                max = _DatabaseFim.getDate();
            }
        }

        for (var i = min; i <= max; i++)
            input.push(i);
        return input;
    };
});
angular.module('App').filter('rangeDiaFim', function () {
    return function (input, PeriodoInicio, PeriodoFim, Competencia,Dia_Inicio) {
        var min = 0;
        var max = 0;
        if (Competencia) {

            var _year = parseInt(Competencia.substring(3, 7))
            var _month = parseInt(Competencia.substring(0, 2))

            var _lastDay = new Date(_year, _month , 0);
            var _firstDay = new Date(_year, _month - 1, 1);

            var from = PeriodoInicio.split("/")
            var _DatabaseInicio = new Date(from[2], from[1] - 1, from[0])

            var from = PeriodoFim.split("/")
            var _DatabaseFim = new Date(from[2], from[1] - 1, from[0])
            min = _firstDay.getDate();
            if (_DatabaseInicio > _firstDay) {

                min = _DatabaseInicio.getDate();
            }
            if (min < Dia_Inicio) {
                min=Dia_Inicio
            }


            max = _lastDay.getDate();
            if (_DatabaseFim < _lastDay) {
                max = _DatabaseFim.getDate();
            }
        }

        for (var i = min; i <= max; i++)
            input.push(i);
        return input;
    };
});