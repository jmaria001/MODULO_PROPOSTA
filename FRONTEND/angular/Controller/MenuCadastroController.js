angular.module('App').controller('MenuCadastroController', ['$scope', '$rootScope', 'httpService', '$location', function ($scope, $rootScope, httpService, $location) {

    //===================Inicializa Scopes
    $scope.Cadastros = [
        { 'Link': 'CondPgto', 'Title': 'Condições de Pagamento' },
        { 'Link': 'Empresa', 'Title': 'Empresas' },
        { 'Link': 'usuario', 'Title': 'Usuários' },
        { 'Link': 'veiculo', 'Title': 'Veículos' },
        { 'Link': 'mercado', 'Title': 'Mercados' },
        { 'Link': 'TipoComercial', 'Title': 'Tipo de Comercial'},
        { 'Link': 'CaracVeicul', 'Title': 'Característica da Veiculacão'},
        { 'Link': 'CategoriaCliente', 'Title': 'Categoria do Cliente' },
        { 'Link': 'MotivoFalha', 'Title': 'Motivo da Falha'},
        { 'Link': 'MotivoAlterNegoc', 'Title': 'Motivo da Alteração da Negociação' },
        { 'Link': 'Contato', 'Title': 'Contatos' },
        { 'Link': 'TipoMidia', 'Title': 'Tipo de Mídia' },
        { 'Link': 'Qualidade', 'Title': 'Qualidades da Veiculaçao' },
        { 'Link': 'MotivoCancelamento', 'Title': 'Motivo de Cancelamento' },
        { 'Link': 'TabelaPrecos', 'Title': 'Tabela de Preços' },
        { 'Link': 'Programa', 'Title': 'Programas' },
        { 'Link': 'Terceiro', 'Title': 'Terceiros' },
        { 'Link': 'Produto', 'Title': 'Produtos' },
        { 'Link': 'Genero', 'Title': 'Generos' },
        { 'Link': 'Parametro', 'Title': 'Parâmetros Gerais' },
        { 'Link': 'ParametroValoracao', 'Title': 'Parâmetros de Valoração' },
        { 'Link': 'Grade', 'Title': 'Grade da Programação' },
        { 'Link': 'Rede', 'Title': 'Redes' },
    ];
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
    });
}]);

