﻿

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
            public String Validade_Inicio { get; set; }
            public String Validade_Termino{ get; set; }
            public String Cod_Empresa_Venda { get; set; }
            public String Nome_Empresa_Venda { get; set; }
            public String Tabela_Preco { get; set; }
            public String Desconto_Padrao { get; set; }
            public String Valor_Informado { get; set; }
            public String Valor_Total_Negociado { get; set; }
            public String Valor_Total_Tabela { get; set; }
            public String Desconto_Real { get; set; }
            public Boolean Fixar_Desconto { get; set; }
            public Boolean Fixar_Valor { get; set; }
            public Int32 Id_Usuario { get; set; }
            public Int32 ContadorEsquema{ get; set; }
            public Int32 ContadorMidia{ get; set; }
            public Int32  Id_Status{ get; set; }
            public Boolean PendenteCalculo { get; set; }
            public List<EsquemaModel> Esquemas { get; set; }

        }
        public class EsquemaModel
        {
            public Int32 Id_Esquema { get; set; }
            public Int32 Id_Simulacao { get; set; }
            public String Competencia { get; set; }
            public Byte Abrangencia { get; set; }
            public String Cod_Mercado { get; set; }
            public String Valor_Total_Negociado { get; set; }
            public String Valor_Total_Tabela { get; set; }
            public String Desconto_Padrao { get; set; }
            public Boolean Fixar_Desconto { get; set; }
            public Boolean Fixar_Valor { get; set; }
            public String Cod_Empresa_Faturamento { get; set; }
            public List<MidiaModel> Midias { get; set; }
            public List<VeiculoModel> Veiculos { get; set; }
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
            public String Mercado { get; set; }
            public String Empresa { get; set; }
            public String EmpresaFaturamento { get; set; }
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
            public  Byte Dia_Inicio { get; set; }
            public Byte Dia_Fim { get; set; }
            public List<VeiculoModel> Veiculos { get; set; }
        }

    }
}