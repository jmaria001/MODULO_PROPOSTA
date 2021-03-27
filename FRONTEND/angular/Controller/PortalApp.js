angular.module('App').controller('PortalAppController', ['$scope', '$rootScope', 'tokenApi', 'httpService', '$location', function ($scope, $rootScope, tokenApi, httpService, $location) {
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
    $scope.ShortMenus = [
        {
            'Title': 'DashBoard',
            'SubItens': [
                { 'Title': 'Evolução de Vendas', 'Url': $rootScope.pageUrl + 'indexDash.html#/EvolucaoVendas' },
                { 'Title': 'Funil de Vendas', 'Url': $rootScope.pageUrl + 'indexDash.html#/FunilVendas' },
                { 'Title': 'Gráfico de Vendas', 'Url': $rootScope.pageUrl + 'indexDash.html#/GraficoVendas' },
                { 'Title': 'Power-Bi', 'Url': 'https://app.powerbi.com/view?r=eyJrIjoiYWU0MjA5YjktYWI0YS00YzU1LTg2OGQtNmEwNzA0YWEyYTk1IiwidCI6ImIyMWNiNTRlLTM1YzEtNDkwZi05OWRkLTBkNmQ4ODYwZTAyYSJ9&pageName=ReportSection294b1d6666875dd21759' }
            ],
        },
        {
            'Title': 'Administração',
            'SubItens': [
                { 'Title': 'Usuários', 'Url': $rootScope.pageUrl + 'IndexAdm.html#/usuario' },
                { 'Title': 'Parâmetros Gerais', 'Url': $rootScope.pageUrl + 'IndexAdm.html#/Parametro' },
                { 'Title': 'Parâmetros de Valoração', 'Url': $rootScope.pageUrl + 'IndexAdm.html#/ParametroValoracao' },
                { 'Title': 'Cadastros', 'Url': $rootScope.pageUrl + 'IndexAdm.html#/cadastro' },
            ],
        },
        {
            'Title': 'Opec',
            'SubItens': [
                { 'Title': 'Manutenção de Mapa Reserva', 'Url': $rootScope.pageUrl + 'IndexOpec.html#/MapaReserva' },
                { 'Title': 'Determinação de Títulos', 'Url': $rootScope.pageUrl + 'IndexOpec.html#/Determinacao' },
                { 'Title': 'Importar Propostas', 'Url': $rootScope.pageUrl + 'IndexOpec.html#/MapaReservaImport' },
                { 'Title': 'Consulta de Veiculações', 'Url': $rootScope.pageUrl + 'IndexOpec.html#/ConsultaVeiculacoes' },
            ],
        },
        {
            'Title': 'Vendas',
            'SubItens': [
                { 'Title': 'Modelo de Vendas', 'Url': $rootScope.pageUrl + 'IndexVendas.html#/Simulacao' },
                { 'Title': 'Propostas   ', 'Url': $rootScope.pageUrl + 'IndexVendas.html#/Proposta' },
                { 'Title': 'Negociações', 'Url': $rootScope.pageUrl + 'IndexVendas.html#/Negociações' },
                { 'Title': "Manutenção de Am's", 'Url': $rootScope.pageUrl + 'IndexVendas.html#/ConsultaAM' },
                { 'Title': "Pacote de Descontos", 'Url': $rootScope.pageUrl + 'IndexVendas.html#/pacote' },
                { 'Title': "Regras de Aprovação", 'Url': $rootScope.pageUrl + 'IndexVendas.html#/regraaprovacao' },
            ],
        },
        {
            'Title': 'Programação',
            'SubItens': [
                { 'Title': 'Manutenção da Grade', 'Url': $rootScope.pageUrl + 'IndexProg.html#/Grade' },
                { 'Title': 'Consulta da Disponibilidade', 'Url': $rootScope.pageUrl + 'IndexProg.html#/ConsultaProgramacaoDiaria' },
                { 'Title': 'De-Para da Programação(Período)', 'Url': $rootScope.pageUrl + 'IndexProg.html#/DeParaPeriodo' },
                { 'Title': 'De-Para da Programação(Data)', 'Url': $rootScope.pageUrl + 'IndexProg.html#/DeParaData' },
                { 'Title': 'Confirmação Horário da Programação', 'Url': $rootScope.pageUrl + 'IndexProg.html#/HorarioExibicao' },
            ],
        },
        {
            'Title': 'Roteiro',
            'SubItens': [
                { 'Title': 'Parâmetros do Roteiro', 'Url': $rootScope.pageUrl + 'IndexRoteiro.html#/ParamRoteiro' },
                { 'Title': 'Parâmetros de Retorno da Play-List', 'Url': $rootScope.pageUrl + 'IndexRoteiro.html#/ParRetorPlayList' },
                { 'Title': 'Depositórios', 'Url': $rootScope.pageUrl + 'IndexRoteiro.html#/DepositorioFitas' },
                { 'Title': 'Materiais', 'Url': $rootScope.pageUrl + 'IndexRoteiro.html#/MateriaisFitas' },
                { 'Title': 'Numeração de Fitas', 'Url': $rootScope.pageUrl + 'IndexRoteiro.html#/NumeracaoFitas' },
                { 'Title': 'Numeração de Fitas Patrocínio', 'Url': $rootScope.pageUrl + 'IndexRoteiro.html#/FitaPatrocinio' },
                { 'Title': 'Consulta de Veiculações', 'Url': $rootScope.pageUrl + 'IndexRoteiro.html#/ConsultaVeiculacoes' },
                { 'Title': 'Pré-Ordenação', 'Url': $rootScope.pageUrl + 'IndexRoteiro.html#/PreOrdenacao' },
                { 'Title': 'Ordenação', 'Url': $rootScope.pageUrl + 'IndexRoteiro.html#/Roteiro' },
                { 'Title': 'Composição de Breaks', 'Url': $rootScope.pageUrl + 'IndexRoteiro.html#/Breaks' },
                { 'Title': 'Consulta de Fitas Ordenadas', 'Url': $rootScope.pageUrl + 'IndexRoteiro.html#/ConsultaFitasOrdenadas' },
                { 'Title': 'Envio Play-List', 'Url': $rootScope.pageUrl + 'IndexRoteiro.html#/EnvioPlayList' },
            ],
        },
        {
            'Title': 'Checking',
            'SubItens': [
                { 'Title': 'Baixa por Veiculação', 'Url': $rootScope.pageUrl + 'IndexChecking.html#/BaixaVeiculacao' },
                { 'Title': 'Baixa por Contrato', 'Url': $rootScope.pageUrl + 'IndexChecking.html#/BaixaContrato' },
                { 'Title': 'Baixa por Roteiro', 'Url': $rootScope.pageUrl + 'IndexChecking.html#/BaixaRoteiro' },
                { 'Title': 'Confirmação do Roteiro', 'Url': $rootScope.pageUrl + 'IndexChecking.html#/ConfirmacaoRoteiro' },
                { 'Title': 'Retorno da Play-List', 'Url': $rootScope.pageUrl + 'IndexChecking.html#/RetornoPlayList' },
                { 'Title': 'Consulta de Veiculações', 'Url': $rootScope.pageUrl + 'IndexChecking.html#/ConsultaVeiculacoes' },
                { 'Title': 'Geração do Comprovante', 'Url': $rootScope.pageUrl + 'IndexChecking.html#/GeracaoCE' },
                { 'Title': 'Impressão do Comprovante', 'Url': $rootScope.pageUrl + 'IndexChecking.html#/ImpressaoCe' },
                { 'Title': 'Reabrir Comprovante', 'Url': $rootScope.pageUrl + 'IndexChecking.html#/ReabreCE' },
            ],
        },
        {
            'Title': 'Faturamento',
            'SubItens': [
                { 'Title': 'Complemento de Contratos', 'Url': $rootScope.pageUrl + 'IndexFaturamento.html#/ComplementoContrato/1' },
                { 'Title': 'Complemento de Antecipados', 'Url': $rootScope.pageUrl + 'IndexFaturamento.html#/ComplementoContrato/0' },
                { 'Title': 'Outras Receitas', 'Url': $rootScope.pageUrl + 'IndexFaturamento.html#/ComplementoOutrasReceitas' },
                { 'Title': 'Pesquisa de Complementos', 'Url': $rootScope.pageUrl + 'IndexFaturamento.html#/ComplementoContratoPesquisa' },
                { 'Title': 'Geração de Faturas', 'Url': $rootScope.pageUrl + 'IndexFaturamento.html#/FaturaGeracao' },
                { 'Title': 'Pesquisa de Faturas', 'Url': $rootScope.pageUrl + 'IndexFaturamento.html#/FaturasPesquisa' },

            ],
        },
    ]
}]);





