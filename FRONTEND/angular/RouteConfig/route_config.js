angular.module('App').config(function ($routeProvider) {
    $routeProvider
        .when('/portal', {
            templateUrl: 'view/portal.html',
            authorize: false,
            routeName: 'Portal',
            RouteId:0
        })
        .when('/cadastro', {
            templateUrl: 'view/UnderConstrution.html',
            authorize: false,
            routeName: 'Cadastros',
            RouteId: 0
        })
        .when('/blank', {
            templateUrl: 'view/blank.html',
            authorize: false,
            routeName: 'SIM - Módulo Propostas',
            RouteId:0
        })
        .when('/login', {
            templateUrl: 'view/login.html',
            authorize: false,
            routeName: 'Autenticação',
            RouteId: 0
        })
        .when('/newpassword', {
            templateUrl: 'view/newpassword.html',
            controller: 'newPasswordController',
            authorize: false,
            routeName: 'Solicitação de Alteração de Senha',
            RouteId: 0
        })
        .when('/error', {
            templateUrl: 'view/error.html',
            authorize: false,
            routeName: 'Mensagens de Erro',
            RouteId: 0
        })
        .when('/unlogged', {
            templateUrl: 'view/unlogged.html',
            authorize: false,
            routeName: 'Login',
            RouteId: 0
        })
        .when('/unauthorized', {
            templateUrl: 'view/unauthorized.html',
            authorize: false,
            routeName: 'Acesso não autorizado',
           RouteId: 0
        })
        .when('/dashboard', {
            templateUrl: 'view/UnderConstrution.html',
            authorize: true,
            routeName: 'Dashboard',
            RouteId: 0
        })
        .when('/usuario', {
            templateUrl: 'view/Usuario.html',
            authorize: true,
            controller:'UsuarioController',
            routeName: 'Cadastro de Usuários',
            RouteId: 'Usuario@Index'
        })

        .when('/veiculo', {
            templateUrl: 'view/Veiculo.html',
            authorize: true,
            controller: 'VeiculoController',
            routeName: 'Cadastro de Veículos',
            RouteId: 'Veiculo@Index'
        })

        .when('/mercado', {
            templateUrl: 'view/Mercado.html',
            authorize: true,
            controller: 'MercadoController',
            routeName: 'Cadastro de Mercado',
            RouteId: 'Mercado@Index'
        })
        .when('/VeiculoCadastro/:Action/:Id', {
            templateUrl: 'view/VeiculoCadastro.html',
            authorize: true,
            controller: 'VeiculoCadastroController',
            routeName: 'Inclusao de Veículos',
            RouteId: 'Veiculo@New'
        })

        .when('/MercadoCadastroEdit/:Action/:Id', {
            templateUrl: 'view/MercadoCadastro.html',
            authorize: true,
            controller: 'MercadoCadastroController',
            routeName: 'Edição de Mercados',
            RouteId: 'Mercado@Edit'
        })

        .when('/MercadoCadastroNew/:Action/:Id', {
            templateUrl: 'view/MercadoCadastro.html',
            authorize: true,
            controller: 'MercadoCadastroController',
            routeName: 'Inclusao de Mercados',
            RouteId: 'Mercado@New'
        })


        .when('/Simulacao', {
            //templateUrl: 'view/simulacao.html',
            templateUrl: 'view/simulacao_List.html',
            controller: 'Simulacao_List_Controller',
            authorize: true,
            routeName: 'Simulação',
            RouteId: 'Simulacao@Index'
        })
        .when('/SimulacaoCadastro/:Action/:Id', {
            templateUrl: 'view/simulacao.html',
            controller: 'SimulacaoController',
            authorize: true,
            routeName: 'Simulação',
            RouteId: 'Simulacao@Edit'
        })

        .when('/Proposta', {
            templateUrl: 'view/UnderConstrution.html',
            authorize: true,
            routeName: 'Proposta',
            RouteId: 'Proposta@Index'
        })

    .when('/Politica', {
        templateUrl: 'view/UnderConstrution.html',
        authorize: true,
        routeName: 'Politica de Descontos',
        RouteId: 'Politica@Index'
    })


    .otherwise({ redirectTo: "/blank" })

});

angular.module('App')
    .run(['$rootScope', '$location', 'httpService', 'tokenApi', function ($rootScope, $location, httpService, tokenApi) {
        $rootScope.$on('$routeChangeStart', function (route, next) {
            $rootScope.routeloading = true;
            if (next.authorize == true) {
                httpService.Get('Credential/' + next.RouteId)
                    .then(function (response) {
                        if (response) {
                            if (response.data==false) {
                                 $location.path("/unauthorized")
                            }
                        }
                    })
                .catch(function (ex) {
                    console.log(ex);
                });
            }
        });

        $rootScope.$on('$routeChangeSuccess', function (route, current) {
            $rootScope.routeloading = false;
            $rootScope.routeName = current.$$route.routeName;
            $rootScope.RouteTitle = current.$$route.routeTitle;
        });
    }]);

angular.module('App').config(['$locationProvider', function ($locationProvider) {
    $locationProvider.hashPrefix("");
}]);


