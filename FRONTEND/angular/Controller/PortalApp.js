angular.module('App').controller('PortalAppController', ['$scope', '$rootScope', 'tokenApi', 'httpService',  '$location', function ($scope, $rootScope, tokenApi, httpService, $location) {
    $scope.AppModulos = [
        {
            'Id': 1,
            'Name': 'SIM-Dashboard',
            'Text': 'Informações Gerenciais',
            'bgcolor': '#e79500',
            'color': 'black',
            'url': 'indexDash.html'
        },
        {
            'Id': 2,
            'Name': 'SIM-Administração',
            'Text': 'Gestão de Usuários,Tabelas de Apoios, Parâmetros, etc.',
            'bgcolor': '#38b58b',
            'color': 'black',
            'url': 'indexAdm.html'
        },
        {
            'Id': 2, 'Name': 'SIM-Opec',
            'Text': 'Entrada de Mapas, Determinação de Títulos, etc.',
            'bgcolor': 'antiquewhite',
            'color': 'black',
            'url': 'indexOpec.html'
        },
        {
            'Id': 3,
            'Name': 'SIM-Vendas',
            'Text': 'Simulação de Vendas, Elaboração de Propostas, Negociações, Compensações.',
            'bgcolor': '#9091e1',
            'color': 'white',
            'url': 'indexVendas.html'
        },
        {
            'Id': 4,
            'Name': 'SIM-Programação',
            'Text': 'Gestão da Disponibilidade, Manutenção da Grade.',
            'bgcolor': '#f78595',
            'color': 'black',
            'url': 'indexProg.html'
        },
        {
            'Id': 5,
            'Name': 'SIM-Roteiro',
            'Text': 'Elaboração do Roteiro,Envio e Retorno de Play-Lists',
            'bgcolor': '#d9cd6c',
            'color': 'black',
            'url': 'indexRoteiro.html'
        },
        {
            'Id': 6,
            'Name': 'SIM-Checking',
            'Text': 'Apontamento de Falhas,Horários, Conciliação da Play-List',
            'bgcolor': 'rgb(208, 16, 57)',
            'color': 'white',
            'url': 'indexChecking.html'
        },
        {
            'Id': 7, 'Name': 'SIM-Faturamento',
            'Text': 'Complemento de Contratos, Geração de Pedidos, Integração ERP',
            'bgcolor': 'rgb(199, 197, 213)',
            'color': 'black',
            'url': 'indexFaturamento.html'
        },
    ];
}]);





