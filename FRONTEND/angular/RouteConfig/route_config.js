angular.module('App').config(function ($routeProvider) {
    $routeProvider
        .when('/PortalApp', {
            templateUrl: 'view/PortalApp.html',
            authorize: false,
            routeName: 'Portal de Aplicações',
            controller: 'PortalAppController',
            RouteId: 0
        })
    .when('/portal', {
        templateUrl: 'view/portal.html',
        authorize: false,
        routeName: 'Mural',
        RouteId: 0
    })
    .when('/cadastro', {
        templateUrl: 'view/UnderConstrution.html',
        authorize: false,
        routeName: 'Cadastros',
        RouteId: 0
    })
    .when('/UnderConstrution', {
        templateUrl: 'view/UnderConstrution.html',
        authorize: false,
        routeName: 'Pagina em Construção',
        RouteId: 0
    })
    .when('/blank', {
        templateUrl: 'view/Portal.html',
        authorize: false,
        routeName: 'Mural',
        RouteId: 0
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
    .when('/MenuDashboard', {
        templateUrl: 'view/MenuDashboard.html',
        controller: 'MenuDashboardController',
        authorize: false,
        routeName: 'Menu Dashboard',
        RouteId: 0
    })
    .when('/GraficoVendas', {
        templateUrl: 'view/DashGraficoVendas.html',
        authorize: true,
        controller: 'DashGraficoVendasController',
        routeName: 'Gráfico de Vendas',
        RouteId: 'GraficoVendas@index'
    })
    .when('/FunilVendas', {
        templateUrl: 'view/DashFunilVendas.html',
        authorize: true,
        controller: 'DashFunilVendasController',
        routeName: 'Funil de Vendas',
        RouteId: 'FunilVendas@index'
    })
         .when('/EvolucaoVendas', {
             templateUrl: 'view/DashEvolucaoVendas.html',
             authorize: true,
             controller: 'DashEvolucaoVendasController',
             routeName: 'Dashboard Evolução Vendas',
             RouteId: 'EvolucaoVendas@Index'
         })
    .when('/cadastro', {
        templateUrl: 'view/MenuCadastro.html',
        controller: 'MenuCadastroController',
        authorize: false,
        routeName: 'Portal de Cadastros',
        RouteId: 0
    })
    .when('/CondPgto', {
        templateUrl: 'view/CondPgto.html',
        authorize: true,
        controller: 'CondPgtoController',
        routeName: 'Cadastro de Condições de Pagamentos',
        RouteId: 'CondPgto@Index'
    })
    .when('/CondPgtoCadastro/:Action/:Id', {
        templateUrl: 'view/CondPgtoCadastro.html',
        authorize: true,
        controller: 'CondPgtoCadastroController',
        routeName: 'Inclusao de Condições de Pagamentos',
        RouteId: 'CondPgto@New'
    })
    .when('/Empresa', {
        templateUrl: 'view/Empresa.html',
        authorize: true,
        controller: 'EmpresaController',
        routeName: 'Cadastro de Empresa',
        RouteId: 'Empresa@Index'
    })
    .when('/EmpresaCadastro/:Action/:Id', {
        templateUrl: 'view/EmpresaCadastro.html',
        authorize: true,
        controller: 'EmpresaCadastroController',
        routeName: 'Inclusao de Empresa',
        RouteId: 'Empresa@New'
    })
    .when('/usuario', {
        templateUrl: 'view/Usuario.html',
        authorize: true,
        controller: 'UsuarioController',
        routeName: 'Cadastro de Usuários',
        RouteId: 'Usuario@Index'
    })
    .when('/mercado', {
        templateUrl: 'view/Mercado.html',
        authorize: true,
        controller: 'MercadoController',
        routeName: 'Cadastro de Mercado',
        RouteId: 'Mercado@Index'
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
    .when('/TipoComercial', {
        templateUrl: 'view/TipoComercial.html',
        authorize: true,
        controller: 'TipoComercialController',
        routeName: 'Cadastro de Tipo Comercial',
        RouteId: 'TipoComercial@Index'
    })
    .when('/TipoComercialCadastro/:Action/:Id', {
        templateUrl: 'view/TipoComercialCadastro.html',
        authorize: true,
        controller: 'TipoComercialCadastroController',
        routeName: 'Inclusao de Tipo Comercial',
        RouteId: 'TipoComercial@New'
    })
    .when('/CaracVeicul', {
        templateUrl: 'view/CaracVeicul.html',
        authorize: true,
        controller: 'CaracVeiculController',
        routeName: 'Cadastro de Características de Veiculação',
        RouteId: 'CaracVeicul@Index'
    })
    .when('/CaracVeiculCadastroNew/:Action/:Id', {
        templateUrl: 'view/CaracVeiculCadastro.html',
        authorize: true,
        controller: 'CaracVeiculCadastroController',
        routeName: 'Inclusão de Característica de Veiculação',
        RouteId: 'CaracVeicul@New'
    })
    .when('/CaracVeiculCadastroEdit/:Action/:Id', {
        templateUrl: 'view/CaracVeiculCadastro.html',
        authorize: true,
        controller: 'CaracVeiculCadastroController',
        routeName: 'Alteração de Característica de Veiculação',
        RouteId: 'CaracVeicul@Edit'
    })
    .when('/CategoriaCliente', {
        templateUrl: 'view/CategoriaCliente.html',
        authorize: true,
        controller: 'CategoriaClienteController',
        routeName: 'Cadastro de Categorias de Cliente',
        RouteId: 'CategoriaCliente@Index'
    })
    .when('/CategoriaClienteCadastroNew/:Action/:Id', {
        templateUrl: 'view/CategoriaClienteCadastro.html',
        authorize: true,
        controller: 'CategoriaClienteCadastroController',
        routeName: 'Inclusão de Categoria de Cliente',
        RouteId: 'CategoriaCliente@New'
    })
    .when('/CategoriaClienteCadastroEdit/:Action/:Id', {
        templateUrl: 'view/CategoriaClienteCadastro.html',
        authorize: true,
        controller: 'CategoriaClienteCadastroController',
        routeName: 'Alteração de Categoria de Cliente',
        RouteId: 'CategoriaCliente@Edit'
    })

    .when('/MotivoAlterNegoc', {
        templateUrl: 'view/MotivoAlterNegoc.html',
        authorize: true,
        controller: 'MotivoAlterNegocController',
        routeName: 'Cadastro de Motivo de Alteração da Negociação',
        RouteId: 'MotivoAlterNegoc@Index'
    })
    .when('/MotivoAlterNegocCadastro/:Action/:Id', {
        templateUrl: 'view/MotivoAlterNegocCadastro.html',
        authorize: true,
        controller: 'MotivoAlterNegocCadastroController',
        routeName: 'Inclusao de Motivo de Alteração da Negociação',
        RouteId: 'MotivoAlterNegoc@New'
    })

    .when('/MotivoFalha', {
        templateUrl: 'view/MotivoFalha.html',
        authorize: true,
        controller: 'MotivoFalhaController',
        routeName: 'Cadastro de Motivo de Falha',
        RouteId: 'MotivoFalha@Index'
    })
    .when('/MotivoFalhaCadastro/:Action/:Id', {
        templateUrl: 'view/MotivoFalhaCadastro.html',
        authorize: true,
        controller: 'MotivoFalhaCadastroController',
        routeName: 'Inclusão de Motivo de Falha',
        RouteId: 'MotivoFalha@New'
    })
    .when('/TipoMidia', {
        templateUrl: 'view/TipoMidia.html',
        authorize: true,
        controller: 'TipoMidiaController',
        routeName: 'Cadastro de Tipo Midia',
        RouteId: 'TipoMidia@Index'
    })

    .when('/TipoMidiaCadastro/:Action/:Id', {
        templateUrl: 'view/TipoMidiaCadastro.html',
        authorize: true,
        controller: 'TipoMidiaCadastroController',
        routeName: 'Inclusao de Tipo Midia',
        RouteId: 'TipoMidia@New'
    })
    .when('/Contato', {
        templateUrl: 'view/Contato.html',
        authorize: true,
        controller: 'ContatoController',
        routeName: 'Cadastro de Contato',
        RouteId: 'Contato@Index'
    })

    .when('/ContatoCadastro/:Action/:Id', {
        templateUrl: 'view/ContatoCadastro.html',
        authorize: true,
        controller: 'ContatoCadastroController',
        routeName: 'Inclusao de Contato',
        RouteId: 'Contato@New'
    })

    .when('/Qualidade', {
        templateUrl: 'view/Qualidade.html',
        authorize: true,
        controller: 'QualidadeController',
        routeName: 'Cadastro de Qualidade',
        RouteId: 'Qualidade@Index'
    })

    .when('/QualidadeCadastro/:Action/:Id', {
        templateUrl: 'view/QualidadeCadastro.html',
        authorize: true,
        controller: 'QualidadeCadastroController',
        routeName: 'Inclusao de Qualidade',
        RouteId: 'Qualidade@New'
    })
    .when('/Rede', {
        templateUrl: 'view/Rede.html',
        authorize: true,
        controller: 'RedeController',
        routeName: 'Cadastro de Rede',
        RouteId: 'Rede@Index'
    })
    .when('/RedeCadastro/:Action/:Id', {
        templateUrl: 'view/RedeCadastro.html',
        authorize: true,
        controller: 'RedeCadastroController',
        routeName: 'Inclusao de Rede',
        RouteId: 'Rede@New'
    })

    .when('/MotivoCancelamento', {
        templateUrl: 'view/MotivoCancelamento.html',
        authorize: true,
        controller: 'MotivoCancelamentoController',
        routeName: 'Cadastro Motivo de Cancelamento',
        RouteId: 'MotivoCancelamento@Index'
    })

    .when('/MotivoCancelamentoCadastro/:Action/:Id', {
        templateUrl: 'view/MotivoCancelamentoCadastro.html',
        authorize: true,
        controller: 'MotivoCancelamentoCadastroController',
        routeName: 'Inclusao de Motivo de Cancelamento',
        RouteId: 'MotivoCancelamento@New'
    })
    .when('/MotivoFalha', {
        templateUrl: 'view/MotivoFalha.html',
        authorize: true,
        controller: 'MotivoFalhaController',
        routeName: 'Cadastro de Motivo de Falha',
        RouteId: 'MotivoFalha@Index'
    })
    .when('/MotivoFalhaCadastro/:Action/:Id', {
        templateUrl: 'view/MotivoFalhaCadastro.html',
        authorize: true,
        controller: 'MotivoFalhaCadastroController',
        routeName: 'Inclusão de Motivo de Falha',
        RouteId: 'MotivoFalha@New'
    })
    .when('/TabelaPrecos', {
        templateUrl: 'view/TabelaPrecos.html',
        authorize: true,
        controller: 'TabelaPrecosController',
        routeName: 'Cadastro de Tabela de Preços',
        RouteId: 'TabelaPrecos@Index'
    })

    .when('/TabelaPrecosCadastroNew/:Action/:Id', {
        templateUrl: 'view/TabelaPrecosCadastro.html',
        authorize: true,
        controller: 'TabelaPrecosCadastroController',
        routeName: 'Inclusao de Tabela de Preço',
        RouteId: 'TabelaPrecos@New'
    })


    .when('/TabelaPrecosCadastroEdit/:Action/:Id', {
        templateUrl: 'view/TabelaPrecosCadastro.html',
        authorize: true,
        controller: 'TabelaPrecosCadastroController',
        routeName: 'Alteração de Tabela de Preço',
        RouteId: 'TabelaPrecos@Edit'
    })
    .when('/Programa', {
        templateUrl: 'view/Programa.html',
        authorize: true,
        controller: 'ProgramaController',
        routeName: 'Cadastro de Programas',
        RouteId: 'Programa@Index'
    })

    .when('/ProgramaCadastro/:Action/:Id', {
        templateUrl: 'view/ProgramaCadastro.html',
        authorize: true,
        controller: 'ProgramaCadastroController',
        routeName: 'Inclusao de Programa',
        RouteId: 'Programa@New'
    })
    .when('/veiculo', {
        templateUrl: 'view/Veiculo.html',
        authorize: true,
        controller: 'VeiculoController',
        routeName: 'Cadastro de Veículos',
        RouteId: 'Veiculo@Index'
    })

    .when('/VeiculoCadastroNew/:Action/:Id', {
        templateUrl: 'view/VeiculoCadastro.html',
        authorize: true,
        controller: 'VeiculoCadastroController',
        routeName: 'Inclusao de Veiculo',
        RouteId: 'Veiculo@New'
    })

    .when('/VeiculoCadastroEdit/:Action/:Id', {
        templateUrl: 'view/VeiculoCadastro.html',
        authorize: true,
        controller: 'VeiculoCadastroController',
        routeName: 'Alteração de Veiculo',
        RouteId: 'Veiculo@Edit'
    })
    .when('/Terceiro', {
        templateUrl: 'view/Terceiro.html',
        authorize: true,
        controller: 'TerceiroController',
        routeName: 'Cadastro de Terceiro',
        RouteId: 'Terceiro@Index'
    })

    .when('/TerceiroCadastro/:Action/:Id', {
        templateUrl: 'view/TerceiroCadastro.html',
        authorize: true,
        controller: 'TerceiroCadastroController',
        routeName: 'Inclusao de Terceiro',
        RouteId: 'Terceiro@New'
    })
    .when('/TerceiroConsulta/:Action/:Id', {
        templateUrl: 'view/TerceiroCadastro.html',
        authorize: true,
        controller: 'TerceiroCadastroController',
        routeName: 'Inclusao de Terceiro',
        RouteId: 'Terceiro@New'
    })
.when('/Produto', {
    templateUrl: 'view/Produto.html',
    authorize: true,
    controller: 'ProdutoController',
    routeName: 'Cadastro de Produtos',
    RouteId: 'Produto@Index'
})
    .when('/ProdutoCadastro/:Action/:Id', {
        templateUrl: 'view/ProdutoCadastro.html',
        authorize: true,
        controller: 'ProdutoCadastroController',
        routeName: 'Manutenção de Produtos',
        RouteId: 'Produto@New'
    })
         .when('/Genero', {
             templateUrl: 'view/Genero.html',
             authorize: true,
             controller: 'GeneroController',
             routeName: 'Tabela de Genero',
             RouteId: 'Genero@Index'
         })

        .when('/GeneroCadastro/:Action/:Id', {
            templateUrl: 'view/GeneroCadastro.html',
            authorize: true,
            controller: 'GeneroCadastroController',
            routeName: 'Inclusao de Genero',
            RouteId: 'Genero@New'
        })

        .when('/Parametro', {
            templateUrl: 'view/Parametro.html',
            authorize: true,
            controller: 'ParametroController',
            routeName: 'Parametros do Sistema',
            RouteId: 'Parametro@Index'
        })

        .when('/ParametroCadastro/:Action/:Id', {
            templateUrl: 'view/ParametroCadastro.html',
            authorize: true,
            controller: 'ParametroCadastroController',
            routeName: 'Inclusao de Parametro',
            RouteId: 'Parametro@New'
        })
                .when('/ParametroValoracao', {
                    templateUrl: 'view/ParametroValoracao.html',
                    authorize: true,
                    controller: 'ParametroValoracaoController',
                    routeName: 'Cadastro de Parametro de Valoração',
                    RouteId: 'ParametroValoracao@Index'
                })
        .when('/ParametroValoracaoCadastroNew/:Action/:Id', {
            templateUrl: 'view/ParametroValoracaoCadastro.html',
            authorize: true,
            controller: 'ParametroValoracaoCadastroController',
            routeName: 'Inclusão de Parametro de Valoração ',
            RouteId: 'ParametroValoracao@New'
        })
        .when('/ParametroValoracaoCadastroEdit/:Action/:Id', {
            templateUrl: 'view/ParametroValoracaoCadastro.html',
            authorize: true,
            controller: 'ParametroValoracaoCadastroController',
            routeName: 'Alteração de Parametro de Valoração',
            RouteId: 'ParametroValoracao@Edit'
        })
    .when('/Grade', {
        templateUrl: 'view/Grade.html',
        controller: 'GradeController',
        authorize: true,
        routeName: 'Grade da Programação',
        RouteId: 'Grade@Index'
    })
    .when('/GradeCadastro/:Action/:Veiculo/:Data/:Programa', {
        templateUrl: 'view/GradeCadastro.html',
        controller: 'GradeCadastroController',
        authorize: true,
        routeName: 'Edição da Grade de Programação',
        RouteId: 'Grade@New'
    })
    .when('/PropagacaoGrade', {
        templateUrl: 'view/PropagacaoGrade.html',
        authorize: true,
        controller: 'PropagacaoGradeController',
        routeName: 'Propagação da Grade',
        RouteId: 'Grade@Replicar'
    })
.when('/ConsultaProgramacaoDiaria', {
    templateUrl: 'view/ConsultaProgramacaoDiaria.html',
    authorize: true,
    controller: 'ConsultaProgramacaoDiariaController',
    routeName: 'Consulta da Programação Prevista',
    RouteId: 'ConsultaProgramacaoDiaria@Index'
})
        .when('/ConsultaProgramacaoDiariaDetalhe/:Veiculo/:Data/:Programa/:Grade', {
            templateUrl: 'view/ConsultaProgramacaoDiariaDetalhe.html',
            authorize: true,
            controller: 'ConsultaProgramacaoDiariaDetalheController',
            routeName: 'Consulta da Programação Prevista - Detalhe',
            RouteId: 'ConsultaProgramacaoDiariaDetalhe@Edit'
        })
    //----------------------------------------------------
    .when('/Simulacao', {
        templateUrl: 'view/simulacao_List.html',
        controller: 'Simulacao_List_Controller',
        authorize: true,
        routeName: 'Modelo de Vendas',
        RouteId: 'Simulacao@Index'
    })
    .when('/SimulacaoCadastro/:Action/:Id/:Processo', {
        templateUrl: 'view/simulacao.html',
        controller: 'SimulacaoController',
        authorize: true,
        routeName: 'Modelo de Vendas',
        RouteId: 'Simulacao@Edit'
    })

    .when('/Proposta', {
        templateUrl: 'view/simulacao_List.html',
        controller: 'Simulacao_List_Controller',
        authorize: true,
        routeName: 'Proposta',
        RouteId: 'Proposta@Index'
    })
    .when('/pacote', {
        templateUrl: 'view/PacoteDesconto_List.html',
        controller: 'PacoteDesconto_List_Controller',
        authorize: true,
        routeName: 'Pacote de Descontos',
        RouteId: 'Pacote@Index'
    })
    .when('/PacoteNew/:Action/:Id', {
        templateUrl: 'view/PacoteCadastro.html',
        controller: 'PacoteCadastroController',
        authorize: true,
        routeName: 'Novo Pacote de Descontos',
        RouteId: 'Pacote@New'
    })
    .when('/PacoteEdit/:Action/:Id', {
        templateUrl: 'view/PacoteCadastro.html',
        controller: 'PacoteCadastroController',
        authorize: true,
        routeName: 'Edição de Pacotes',
        RouteId: 'Pacote@New'
    })
    .when('/PacoteShow/:Action/:Id', {
        templateUrl: 'view/PacoteCadastro.html',
        controller: 'PacoteCadastroController',
        routeName: 'Visualização do Pacote ',
    })
    .when('/regraaprovacao', {
        templateUrl: 'view/RegraAprovacao.html',
        controller: 'RegraAprovacao_Controller',
        authorize: true,
        routeName: 'Regras de Aprovação de Descontos',
        RouteId: 'Aprovacao@Index'
    })
    .when('/PendenciaAprovacao', {
        templateUrl: 'view/PendenciaAprovacao.html',
        controller: 'PendenciaAprovacaoController',
        authorize: true,
        routeName: 'Pendencias de Aprovação',
        RouteId: 'Proposta@Approve'
    })
    .when('/PropostaAprovacao/:Id/:From', {
        templateUrl: 'view/PropostaPendenteAprovacao.html',
        controller: 'AprovarPendentes_Controller',
        authorize: true,
        routeName: 'Aprovação da Proposta',
        RouteId: 'Proposta@Approve'
    })
    .when('/regracadastro/:Action/:Id', {
        templateUrl: 'view/RegraAprovacao_Cadastro.html',
        controller: 'RegraAprovacaoCadastro_Controller',
        authorize: true,
        routeName: 'Regra de Aprovação de Desconto',
        RouteId: 'Pacote@New'
    })
    .when('/Negociacao', {
        templateUrl: 'view/Negociacao.html',
        controller: 'NegociacaoController',
        authorize: true,
        routeName: 'Negociacoes',
        RouteId: 'Negociacao@Index'
    })
    .when('/NegociacaoCadastro/:Action/:Id', {
        templateUrl: 'view/Negociacao_Cadastro.html',
        //templateUrl: 'view/UnderConstrution.html',
        controller: 'NegociacaoCadastroController',
        authorize: true,
        routeName: 'Manutenção de Negociações',
        RouteId: 'Negociacao@New'
    })
    .when('/NegociacaoDelhalhe/:Id', {
        templateUrl: 'view/NegociacaoDetalhe.html',
        controller: 'NegociacaoDetalheController',
        //authorize: true,
        routeName: 'Mapas Reservas da Negociação',
        RouteId: ''
    })
        .when('/MapaReserva', {
            templateUrl: 'view/MapaReserva.html',
            controller: 'MapaReservaController',
            authorize: true,
            routeName: 'Mapas Reserva',
            RouteId: 'MapaReserva@Index'
        })
        .when('/MapaReservaDetalhe/:Id', {
            templateUrl: 'view/MapaReservaDetalhe.html',
            controller: 'MapaReservaDetalheController',
            authorize: true,
            routeName: 'Mapas Reserva',
            RouteId: 'MapaReserva@Index'
        })
    .when('/MapaReservaCadastro/:Action/:Id', {
        templateUrl: 'view/MapaReservaCadastro.html',
        controller: 'MapaReservaCadastroController',
        authorize: true,
        routeName: 'Manutencão de Mapa Reserva',
        RouteId: 'MapaReserva@New'
    })
    .when('/MapaReservaImport', {
        templateUrl: 'view/MapaReservaImport.html',
        controller: 'MapaReservaImportController',
        authorize: true,
        routeName: 'Importação de Propostas para Mapa Reserva',
        RouteId: 'MapaReserva@Import'
    })
    .when('/ConsultaVeiculacoes', {
        templateUrl: 'view/ConsultaVeiculacao.html',
        controller: 'ConsultaVeiculacaoController',
        authorize: true,
        routeName: "Consulta de Veiculaçoes",
        RouteId: 'Veiculacao@Consulta'
    })
    .when('/ConsultaAM', {
        templateUrl: 'view/Am_Consulta.html',
        controller: 'Am_ConsultaController',
        authorize: true,
        routeName: "Consulta de AM's",
        RouteId: 'AM@Index'
    })
    .when('/Am_Compensacao/:Cod_Empresa/:Numero_Mr/:Sequencia_Mr/:Numero_Docto/:Cod_Veiculo', {
        templateUrl: 'view/Am_Compensacao.html',
        controller: 'Am_CompensacaoController',
        authorize: true,
        routeName: 'Compensação de Falhas',
        RouteId: 'Am@Compensacao'
    })
        .when('/Am_Reencaixe/:Cod_Empresa/:Numero_Mr/:Sequencia_Mr/:Numero_Docto/:Competencia/:Cod_Veiculo', {
            templateUrl: 'view/Am_Reencaixe.html',
            controller: 'Am_ReencaixeController',
            authorize: true,
            routeName: 'Manutenção de Am',
            RouteId: 'Am@Reencaixar'
        })
    .when('/Roteiro', {
        templateUrl: 'view/Roteiro.html',
        controller: 'RoteiroController',
        authorize: true,
        routeName: 'Roteiro Comercial',
        RouteId: 'Roteiro@Index'
    })
    .when('/PreOrdenacao', {
        templateUrl: 'view/PreOrdenacao.html',
        authorize: true,
        controller: 'PreOrdenacaoController',
        routeName: 'Pré-Ordenação do Roteiro',
        RouteId: 'Roteiro@Pre_Ordenacao'
    })

    .when('/Breaks', {
        templateUrl: 'view/Composicao_Break.html',
        controller: 'Composicao_Break_Controller',
        authorize: true,
        routeName: 'Composicao de Breaks',
        RouteId: 'Roteiro@Break'
    })

    .when('/ParamRoteiro', {
        templateUrl: 'view/ParamRoteiro.html',
        authorize: true,
        controller: 'ParamRoteiroController',
        routeName: 'Parâmetros do Roteiro',
        RouteId: 'Roteiro@Parametros'
    })
    .when('/ParRetorPlayList', {
        templateUrl: 'view/ParRetorPlayList.html',
        authorize: true,
        controller: 'ParRetorPlayListController',
        routeName: 'Parametrização do Retorno da PlayList',
        RouteId: 'Roteiro@Par_Ret_Play'
    })
    .when('/EnvioPlayList', {
        templateUrl: 'view/EnvioPlayList.html',
        authorize: true,
        controller: 'EnvioPlayListController',
        routeName: 'Envio da PlayList',
        RouteId: 'Roteiro@EnvioPlayList'
    })
    .when('/ComplementoContrato/:Origem', {
        templateUrl: 'view/ComplementoContrato.html',
        authorize: true,
        controller: 'ComplementoContratoController',
        routeName: 'Complemento de Faturas',
        RouteId: 'Faturamento@Complemento'
    })
    .when('/ComplementoOutrasReceitas', {
        templateUrl: 'view/ComplementoOutrasReceitas.html',
        authorize: true,
        controller: 'ComplementoOutrasReceitas',
        routeName: 'Complemento de Outras Receitas',
        RouteId: 'Faturamento@OutrasReceitas'
    })
    .when('/ComplementoContratoPesquisa', {
        templateUrl: 'view/ComplementoContratoPesquisa.html',
        authorize: true,
        controller: 'ComplementoContratoPesquisaController',
        routeName: 'Pesquisa de Complementos',
        RouteId: 'Complemento@Pesquisa'
    })
    .when('/FaturasPesquisa', {
        templateUrl: 'view/FaturasPesquisa.html',
        authorize: true,
        controller: 'FaturasPesquisaController',
        routeName: 'Pesquisa de Faturas',
        RouteId: 'Fatura@Pesquisa'
    })
    .when('/FaturaGeracao', {
        templateUrl: 'view/GeracaoFatura.html',
        authorize: true,
        controller: 'GeracaoFaturaController',
        routeName: 'Geração de Faturas',
        RouteId: 'Fatura@Geracao'
    })
    .when('/GeracaoCE', {
        templateUrl: 'view/GeracaoCE.html',
        authorize: true,
        controller: 'GeracaoCEController',
        routeName: 'Geração do Comprovante de Exibição',
        RouteId: 'Geracao@Ce'
    })
    .when('/DepositorioFitas', {
        templateUrl: 'view/DepositorioFitas.html',
        controller: 'DepositorioFitasController',
        authorize: true,
        routeName: 'Deposítório de Fitas',
        RouteId: 'Fitas@Depositorio'
    })
        .when('/DepositorioFitasCadastro/:Action/:Id', {
            templateUrl: 'view/DepositorioFitasCadastro.html',
            authorize: true,
            controller: 'DepositorioFitasCadastroController',
            routeName: 'Alteração de Fita',
            RouteId: 'Fitas@Depositorio'
        })
    .when('/MateriaisFitas', {
        templateUrl: 'view/MateriaisFitas.html',
        controller: 'MateriaisFitasController',
        authorize: true,
        routeName: 'Cadastro de Materiais',
        RouteId: 'Fitas@Material'
    })
    .when('/MateriaisFitasCadastro/:Action/:Id', {
        templateUrl: 'view/MateriaisFitasCadastro.html',
        authorize: true,
        controller: 'MateriaisFitasCadastroController',
        routeName: 'Manutenção do Cadastro de materiais',
        RouteId: 'Fitas@Material'
    })
        .when('/NumeracaoFitas', {
            templateUrl: 'view/NumeracaoFitas.html',
            controller: 'NumeracaoFitasController',
            authorize: true,
            routeName: 'Numeração de Fitas',
            RouteId: 'Fitas@Numeracao'
        })
    .when('/NumeracaoFitasCadastro/:Cod_Empresa/:Numero_Mr/:Sequencia_Mr/:Cod_Comercial/:Cod_Tipo_Comercial/:Cod_Tipo_Midia', {
        templateUrl: 'view/NumeracaoFitasCadastro.html',
        authorize: true,
        controller: 'NumeracaoFitasCadastroController',
        routeName: 'Numeração de Fita',
        RouteId: 'Fitas@Numeracao'
    })
    .when('/BaixaVeiculacao', {
        templateUrl: 'view/BaixaVeiculacoes.html',
        controller: 'BaixaVeiculacoesController',
        authorize: true,
        routeName: 'Baixa de Veiculações',
        RouteId: 'Baixa@Veiculacao'
    })
        .when('/FitaPatrocinio', {
            templateUrl: 'view/FitaPatrocinio.html',
            controller: 'FitaPatrocinioController',
            authorize: true,
            routeName: 'Controle de Fitas de Patrocínio',
            RouteId: 'FitaPatrocino@Numerar'
        })
    .when('/RetornoPlayList', {
    templateUrl: 'view/RetornoPlayList.html',
    authorize: true,
    controller: 'RetornoPlayListController',
    routeName: 'Parametrização do Retorno da PlayList',
    RouteId: 'Retorno@PlayList'
    })
         .when('/BaixaContrato', {
             templateUrl: 'view/BaixaContrato.html',
             authorize: true,
             controller: 'BaixaContratoController',
             routeName: 'Baixa por Contrato',
             RouteId: 'baixa@contrato'
         })

    .when('/NaturezadeServico', {
        templateUrl: 'view/NaturezadeServico.html',
        authorize: true,
        controller: 'NaturezadeServicoController',
        routeName: 'Cadastro de Natureza de Serviço',
        RouteId: 'Natureza@Index'
    })


    .when('/NaturezadeServicoCadastro/:Action/:Cod_Natureza/:Cod_Empresa', {
        templateUrl: 'view/NaturezadeServicoCadastro.html',
        authorize: true,
        controller: 'NaturezadeServicoCadastroController',
        routeName: 'Manutenção de Natureza de Serviço',
        RouteId: 'Natureza@Index'
    })

    .when('/NaturezadeServicoCadastro', {
        templateUrl: 'view/NaturezadeServicoCadastro.html',
        authorize: true,
        controller: 'NaturezadeServicoCadastroController',
        routeName: 'Manutenção de Natureza de Serviço',
        RouteId:  'Natureza@Index'
    })
    .when('/ConfirmacaoRoteiro', {
        templateUrl: 'view/ConfirmacaoRoteiro.html',
        authorize: true,
        controller: 'ConfirmacaoRoteiroController',
        routeName: 'Confirmacao do Roteiro',
        RouteId: 'Roteiro@Confirmacao'
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
                    if (response.data == false) {
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
        $rootScope.routeId = current.$$route.RouteId;
    });
}]);

angular.module('App').config(['$locationProvider', function ($locationProvider) {
    $locationProvider.hashPrefix("");
}]);


