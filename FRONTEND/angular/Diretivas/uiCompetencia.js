angular.module("App").directive("uiCompetencia", function () {
    return {
        
        templateUrl: "template/uiMesAno.html",
        replace: true,
        restrict: "AE",
        scope: {
            typmesano: "="
        },
        transclude: true,
        link: function (scope, element, attrs, ctrls) {
            scope.Year = new Date().getFullYear();
            scope.Months= ['Janeiro','Fevereiro','Marco','Abril','Maio','Junho','Julho','Agosto','Setembro','Outubro','Novembro','Dezembro']
            scope.addYear= function (x) {
                scope.Year = scope.Year + x;
            };
            scope.ClickOnDate = function (m, y) {
                scope.typmesano = LeftZero(m, 2) + '/' + y.toString();
                }
            $('.CalendarMesAnocontent').on({
                "click": function (e) {
                    e.stopPropagation();
                }
            });
        }
    };
});

