

using System;
using System.Collections.Generic;


namespace PROPOSTA
{
    public partial class Simulacao
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public Simulacao(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class SimulacaoModel
        {
            public Int32 Id_Simulacao { get; set; }
            public String Identificacao { get; set; }
            public String Tipo  { get; set; }
            public String Validade_Inicio { get; set; }
            public String Validade_Termino{ get; set; }
            public String Cod_Empresa_Venda { get; set; }
            public String Nome_Empresa_Venda { get; set; }
            public String Cod_Tipo_Midia { get; set; }
            public String Nome_Tipo_Midia { get; set; }
            public String Cod_Agencia { get; set; }
            public String Nome_Agencia { get; set; }
            public String Cnpj_Agencia { get; set; }
            public String Cod_Cliente { get; set; }
            public String Nome_Cliente { get; set; }
            public String Cnpj_Cliente{ get; set; }
            public String Cod_Contato { get; set; }
            public String Nome_Contato { get; set; }
            public String Cod_Nucleo { get; set; }
            public String Nome_Nucleo { get; set; }
            public Int32? Forma_Pgto{ get; set; }
            public String  Condicao_Pagamento { get; set; }
            public String Tabela_Preco { get; set; }
            public String Desconto_Padrao { get; set; }
            public String Comissao_Agencia { get; set; }
            public Int32? Id_Pacote{ get; set; }
            public String Descricao_Pacote{ get; set; }
            public String Valor_Informado { get; set; }
            public String Valor_Total_Negociado { get; set; }
            public String Valor_Total_Tabela { get; set; }
            public String Desconto_Real { get; set; }
            public Boolean Fixar_Desconto { get; set; }
            public Boolean Fixar_Valor { get; set; }
            public Int32 Id_Usuario { get; set; }
            public String Nome_Usuario { get; set; }
            public Int32 ContadorEsquema{ get; set; }
            public Int32 ContadorMidia{ get; set; }
            public Int32  Id_Status{ get; set; }
            public String Descricao_Status { get; set; }
            public String BackColorStatus { get; set; }
            public String ForecolorStatus { get; set; }
            public Boolean Indica_Valoracao { get; set; }
            public Boolean PendenteCalculo { get; set; }
            public Boolean Requer_Aprovacao { get; set; }
            public Boolean Permite_Aprovacao { get; set; }
            public Boolean Permite_Envio_Aprovacao { get; set; }
            public Boolean Permite_Gerar{ get; set; }
            public Boolean Permite_Editar{ get; set; }
            public Boolean Permite_Confirmar_Venda{ get; set; }
            public Boolean Permite_Reabrir { get; set; }
            public String Observacao{ get; set; }
            public String Motivo_Recusa{ get; set; }
            public List<EsquemaModel> Esquemas { get; set; }
            public String Critica { get; set; }
            public Boolean Indica_Inconsistencia{ get; set; }
            public Boolean Indica_Sem_Midia{ get; set; }
            public Int32 Termometro_Venda { get; set; }
            public String Cod_Programa_Patrocinado{ get; set; }
            public List<TotalizadorModel> Totalizadores { get; set; }
        }
        public class TotalizadorModel
        {
            public Int32 RedeId { get; set; }
            public String Nome_Rede { get; set; }
            public Double Valor_Total_Tabela { get; set; }
            public Double Valor_Total_Negociado { get; set; }
            public Double Desconto_Real { get; set; }
            
        }
        public class EsquemaModel
        {
            public Int32 Id_Esquema { get; set; }
            public Int32 Id_Simulacao { get; set; }
            public String Competencia { get; set; }
            public Int32 RedeId { get; set; }
            public String Nome_Rede{ get; set; }
            public Byte Abrangencia { get; set; }
            public String Cod_Mercado { get; set; }
            public String Valor_Total_Negociado { get; set; }
            public String Valor_Total_Tabela { get; set; }
            public String Desconto_Padrao { get; set; }
            public Boolean Fixar_Desconto { get; set; }
            public Boolean Fixar_Valor { get; set; }
            public String Cod_Empresa_Faturamento { get; set; }
            public String Caracteristica_Contrato { get; set; }
            public String Cod_Programa_Patrocinado { get; set; }
            public List<MidiaModel> Midias { get; set; }
            public List<VeiculoModel> Veiculos { get; set; }
            public String BackColorTab { get; set; }
        }
        public class MidiaModel
        {
            public Int32 Id_Midia { get; set; }
            public Int32 Id_Esquema { get; set; }
            public String Cod_Programa { get; set; }
            public String Nome_Programa { get; set; }
            public String Cod_Caracteristica { get; set; }
            public String Nome_Caracteristica { get; set; }
            public String Cod_Comercial { get; set; }
            public String Cod_Tipo_Comercial { get; set; }
            public String Nome_Tipo_Comercial { get; set; }
            public String Titulo_Comercial { get; set; }
            public String Cod_Red_Produto { get; set; }
            public String Nome_Produto { get; set; }
            public Int32 Duracao { get; set; }
            public Int32 Dia_Inicio { get; set; }
            public Int32 Dia_Fim { get; set; }
            public Int32 Qtd_Insercoes { get; set; }
            public String Distribuicao { get; set; } = "D";
            public Int32 Qtd_Total_Insercoes { get; set; }
            public String Valor_Tabela_Unitario { get; set; }
            public String Valor_Tabela_Total { get; set; }
            public String Valor_Negociado_Total { get; set; }
            public String Valor_Informado{ get; set; }
            public String Desconto_Informado { get; set; }
            public String Desconto_Real{ get; set; }
            public String Critica { get; set; }
            public List<InsercaoModel> Insercoes { get; set; }
            public Boolean  IsValid { get; set; }
        }
        public class InsercaoModel
        {
            public Int32 Id_Insercao { get; set; }
            public Int32 Id_Midia { get; set; }
            public DateTime Data_Exibicao { get; set; }
            public String Dia { get; set; }
            public String Dia_Semana{ get; set; }
            public String Qtd { get; set; }
            public String Valor_Tabela_Unitario { get; set; }
            public String Valor_Negociado_Unitario { get; set; }
            public String Valor_Negociado_Total{ get; set; }
            public String Desconto_Aplicado { get; set; }
            public String Tipo_Desconto { get; set; }
            public Boolean Tem_Grade { get; set; }
            public String Critica { get; set; } //mensagem na distribuicao das insercoes
            public Boolean Status{ get; set; } //retorno de sucesso ou nao na distribuicao das insercoes
        }
        public class DescontoModel
        {
            public Int32 Id_Desconto { get; set; }
            public Int32 Id_Simulacao { get; set; }
            public Int32 Cod_Desconto { get; set; }
            public String Conteudo { get; set; }
            public String Chave { get; set; }
            public DateTime Data_Inicio { get; set; }
            public DateTime Data_Termino { get; set; }
            public String Desconto { get; set; }

        }
        public class VeiculoModel
        {
            public Int32 Id_Esquema { get; set; }
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }
        }

        public class GetVeiculoParam
        {
            public Int32 Abrangencia { get; set; }
            public String Cod_Mercado { get; set; }
            public String Cod_Empresa { get; set; }
            public String Cod_Empresa_Faturamento { get; set; }
            public Int32 RedeId{ get; set; }
            public Boolean Indica_Midia_Online { get; set; }
        }
        public class GetProgramasGradeParam
        {
            public List<VeiculoModel> Veiculos;
            public String Competencia;
        }
        public class DistribuicaoInsecoesParam
        {
            public Int32 Id_Midia { get; set; }
            public String Competencia { get; set; }
            public String Cod_Programa { get; set; }
            public String Cod_Tipo_Comercial { get; set; }
            public String Cod_Caracteristica { get; set; }
            public Int32 Qtd_Insercoes { get; set; }
            public String Distribuicao { get; set; }
            public  Byte Dia_Inicio { get; set; }
            public Byte Dia_Fim { get; set; }
            public String Validade_Inicio { get; set; }
            public String Validade_Termino { get; set; }
            public String Cod_Empresa_Faturamento { get; set; }
            public Int32 Duracao { get; set; }
            public List<VeiculoModel> Veiculos { get; set; }
        }
        public class Param_Aprovacao_Model
        {
            public Int32 Id_Simulacao { get; set; }
            public String url { get; set; }
            public String Token{ get; set; }
            public String Action { get; set; }
            public String Motivo{ get; set; }
        }
        public class Param_Geracao_Model
        {
            public Int32 Id_Simulacao { get; set; }
            public String Nome_Contato { get; set; }
            public String Email_Contato{ get; set; }
            public String Email_Copia{ get; set; }
            public String Observacao{ get; set; }
        }
        public class SimulacaoFiltroParam
        {
            public Int32 Id_Simulacao { get; set; }
            public String Processo { get; set; }
            public Int32 Id_Status { get; set; }
            public String Cod_Empresa_Venda{ get; set; }
            public String Validade_Inicio { get; set; }
            public String Validade_Termino{ get; set; }
            public String Agencia{ get; set; }
            public String Cliente{ get; set; }
            public String Contato{ get; set; }
        }
        public class ParamSelecionarPacote
        {
            public Int32 Id_Pacote { get; set; }
            public String Validade_Inicio{ get; set; }
            public String Validade_Termino{ get; set; }
        }
    }
}