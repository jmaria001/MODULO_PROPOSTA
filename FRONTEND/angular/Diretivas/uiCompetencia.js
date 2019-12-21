angular.module("App").directive("uiCompetencia", function () {
    return {

        templateUrl: "template/uiMesAno.html",
        replace: true,
        restrict: "AE",
        scope: {
            typmesano: "=",
            params: "=",
        },
        transclude: true,
        link: function (scope, element, attrs, ctrls) {
            scope.addYear = function (x) {
                scope.params.Year = scope.params.Year + x;
            };
            scope.ClickOnDate = function (m, y) {
                scope.typmesano = LeftZero(m, 2) + '/' + y.toString();
            };
            scope.MesValido = function (m) {
                var _ret = true;
                if (scope.params) {
                    var _x = parseInt(scope.params.Year + LeftZero(m, 2));
                    if (scope.params.First) {
                        if (_x < scope.params.First) {
                            _ret = false;
                        }
                    }
                    if (scope.params.Last) {
                        if (_x > scope.params.Last){
                            _ret = false;
                        }
                    }
                }
                return _ret;
            };
            scope.NextYear = function () {
                var _ret = true;
                if (scope.params) {
                    var _x = parseInt(scope.params.Year + '12')
                    if (scope.params.Last) {
                        if (_x >= scope.params.Last) {
                            _ret = false;
                        }
                    }
                    return _ret;
                }
            };
            scope.PreviousYear = function () {
                var _ret = true;
                if (scope.params) {
                    var _x = parseInt(scope.params.Year + '01')
                    if (scope.params.First) {
                        if (_x <= scope.params.First) {
                            _ret = false;
                        }
                    }
                    return _ret;
                }
            };
            $('.CalendarMesAnocontent').on({
                "click": function (e) {
                    e.stopPropagation();
                }
            });
        }
    };
});

