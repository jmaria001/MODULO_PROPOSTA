angular.module('App').controller('MenuCadastroController', ['$scope', '$rootScope', 'httpService', '$location', function ($scope, $rootScope, httpService, $location) {

    //===================Inicializa Scopes
    $scope.Cadastros = [
        {   'Link':'CondPgto','Title':'Condições de Pagamento'},
        {   'Link':'Empresa','Title':'Empresas'},
        {   'Link':'usuario','Title':'Usuários'},
        {   'Link':'veiculo','Title':'Veículos'},
        {   'Link':'mercado','Title':'Mercados'},
        {   'Link':'TipoComercial','Title':'Tipo de Comercial'},
        {   'Link':'CaracVeicul','Title':'Característica da Veiculacão'},
        { 'Link': 'CategoriaCliente', 'Title': 'Categoria do Cliente' },
        { 'Link': 'MotivoFalha', 'Title': 'Motivo da Falha' },
        { 'Link': 'MotivoAlterNegoc', 'Title': 'Motivo da Alteração da Negociação' },
        { 'Link': 'Contato', 'Title': 'Contatos' },
        { 'Link': 'TipoMidia', 'Title': 'Tipo de Mídia' },
        { 'Link': 'Qualidade', 'Title': 'Qualidades da Veiculaçao' },
        { 'Link': 'Rede', 'Title': 'Redes' },
        { 'Link': 'MotivoCancelamento', 'Title': 'Motivo de Cancelamento' },

    ];
    //===========================fim do load da pagina
    $scope.$watch('$viewContentLoaded', function () {
        
    });

}]);

