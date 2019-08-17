
angular.module("App").directive("uiCalendar", function () {
    return {

        templateUrl: "template/uiCalendar.html",
        replace: true,
        restrict: "AE",
        scope: {
            typdate: "=",
        },
        transclude: true,
        link: function (scope, element, attrs, ctrls) {
            scope.aMonth = ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'];

            var d = new Date();
            scope.year = d.getFullYear();
            scope.month = d.getMonth();
            scope.MonthTitle = scope.aMonth[scope.month] + '/' + scope.year;
            scope.CalendarDays = SetDay(scope.year, scope.month);

            scope.addMonth = function (x) {
                var d = new Date(scope.year, scope.month, 1);
                d.setMonth(d.getMonth() + x);
                scope.year = d.getFullYear();
                scope.month = d.getMonth();
                scope.MonthTitle = scope.aMonth[scope.month] + '/' + scope.year;
                scope.CalendarDays = SetDay(scope.year, scope.month);
            };

            scope.ClickOnDate = function (d)
            {
                var _ret = '000' + d
                var dia = _ret.substring(_ret.length - 2, _ret.length);
                var _ret = '000' + (scope.month+1)
                var mes = _ret.substring(_ret.length - 2, _ret.length);
                scope.typdate = dia + '/' + mes    + '/' + scope.year;

            }
            $('.calendarcontent').on({
                "click": function (e) {
                e.stopPropagation();
                }
            });
        }
    };

    function SetDay (y,m) {
        var aDays= [{'Dom':'','Seg':'','Ter':'','Qua':'','Qui':'','Sex':'','Sab':''},
                    { 'Dom': '', 'Seg': '', 'Ter': '', 'Qua': '', 'Qui': '', 'Sex': '', 'Sab': '' },
                    { 'Dom': '', 'Seg': '', 'Ter': '', 'Qua': '', 'Qui': '', 'Sex': '', 'Sab': '' },
                    { 'Dom': '', 'Seg': '', 'Ter': '', 'Qua': '', 'Qui': '', 'Sex': '', 'Sab': '' },
                    { 'Dom': '', 'Seg': '', 'Ter': '', 'Qua': '', 'Qui': '', 'Sex': '', 'Sab': '' },
                    { 'Dom': '', 'Seg': '', 'Ter': '', 'Qua': '', 'Qui': '', 'Sex': '', 'Sab': '' },
                    
        ];
        var grid = [];
        var semana=0
        for (var i = 1; i < 32; i++) {
            var d = new Date(y, m, i,12  ,0,0)
            var wk = d.getDay();
            if (d.getMonth() != m) {
                break;
            }
            switch (wk) {
                case 0:
                    aDays[semana].Dom = i;
                    break;
                case 1:
                    aDays[semana].Seg = i;
                    break;
                case 2:
                    aDays[semana].Ter = i;
                case 3:
                    aDays[semana].Qua = i;
                    break;
                case 4:
                    aDays[semana].Qui = i;
                    break;
                case 5:
                    aDays[semana].Sex = i;
                    break;
                case 6:
                    aDays[semana].Sab = i;
                    semana++
                    break;
          }

        }
        for (var i = 0; i < aDays.length; i++) {

            if (!aDays[i].Dom && !aDays[i].Seg && !aDays[i].Ter && !aDays[i].Qua && !aDays[i].Qui && !aDays[i].Sex && !aDays[i].Sab) {
                
                aDays.splice(i, 1);
            }
        }
        return aDays;
    }

        
});

