

using System;
using System.Collections.Generic;


namespace PROPOSTA
{
    public partial class MapaReserva
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public MapaReserva(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }

        public class ContratoModel
        {
            public String Operacao { get; set; }
            public String Cod_Empresa_Venda { get; set; }
            public String Nome_Empresa_Venda { get; set; }
            public Int32 Numero_Mr { get; set; }
            public Int32 Numero_Negociacao { get; set; }
            public Int32 Sequencia_Mr { get; set; }
            public String Cod_Programa { get; set; }
            public String Cod_Empresa_Faturamento { get; set; }
            public String Nome_Empresa_Faturamento { get; set; }
            public String Caracteristica_Contrato { get; set; }
            public String Cod_Contato { get; set; }
            public String Nome_Contato { get; set; }
            public String Cod_Nucleo { get; set; }
            public String Nome_Nucleo { get; set; }
            public String Cod_Tipo_Midia { get; set; }
            public Boolean Indica_Midia_Online { get; set; }
            public String Data_Recepcao_Reserva { get; set; }
            public String Numero_PI { get; set; }
            public String Obs_Roteiro { get; set; }
            public String Periodo_Campanha_Inicio { get; set; }
            public String Periodo_Campanha_Termino { get; set; }  
            public Int32 Competencia { get; set; }
            public Int32 Indica_Grade { get; set; }
            public String Cod_Cliente { get; set; }
            public String Nome_Cliente { get; set; }
            public String Cod_Agencia { get; set; }
            public String Nome_Agencia { get; set; }
            public Boolean Indica_Por_Credito { get; set; }
            public String Vlr_Informado { get; set; }
            public String Desconto{ get; set; }
            public Boolean Indica_Apoio { get; set; }
            public String Cod_Mercado { get; set; }
            public Int32 Id_Contrato { get; set; }
            public String Obs_Contrato { get; set; }
            public String Campanha { get; set; }
            public String Codigo_Projeto { get; set; }
            public Int32 Versao_Projeto { get; set; }
            public Boolean Criar_Negociacao { get; set; }
            public Boolean Editar_Negociacao { get; set; } = true;
            public Boolean Editar_Cliente { get; set; } = true;
            public Boolean Editar_Agencia { get; set; } = true;
            public Boolean Editar_Contato { get; set; } = true;
            public Boolean Editar_Nucleo { get; set; } = true;
            public Boolean Editar_Empresa_Venda { get; set; } = true;
            public Boolean Editar_Empresa_Faturamento { get; set; } = true;
            public Boolean Editar_Tipo_Midia { get; set; } = true;
            public Boolean Editar_Mercado { get; set; } = true;
            public Boolean Editar_Abrangencia { get; set; } = true;
            public Boolean Editar_Periodo_Campanha { get; set; } = true;
            public Boolean Editar_Valor_Informado { get; set; } = true;
            public Boolean Editar_Midia_Apoio{ get; set; } = true;
            public Boolean Editar_Conta_Credito{ get; set; } = true;
            public Boolean Editar_Caracteristica_Contrato{ get; set; } = true;
            public Boolean Editar_Midia_OnLine{ get; set; } = true;
            public Int32 Id_Simulacao { get; set; }
            public Int32 Id_Esquema{ get; set; }
            public Boolean Indica_Tqp { get; set; }
            public Int32 Sequenciador_Veiculacao { get; set; } = 0;
            public Boolean Tem_Fatura { get; set; }
            public Boolean Comprovado { get; set; }
            public List<ComercialModel> Comerciais { get; set; }
            public List<VeiculacacaoModel> Veiculacoes{ get; set; }
            public List<VeiculacaoOnLineModel> VeiculacoesOnLine { get; set; }
            public List<VeiculoModel> Veiculos{ get; set; }

        }

        public class ComercialModel
        {
            public String Cod_Comercial { get; set; }
            public String Cod_Tipo_Comercial { get; set; }
            public String Nome_Tipo_Comercial { get; set; }
            public Int32 Duracao { get; set; }
            public Boolean Indica_Titulo_Determinar { get; set; }
            public Int32 Cod_Red_Produto { get; set; }
            public String Nome_Produto { get; set; }
            public String Numero_Fita { get; set; }
            public String Titulo_Comercial { get; set; }
            public Boolean Tem_Veiculacao { get; set; }
            public String Cod_Tipo_Comercializacao { get; set; }
            public String Nome_Tipo_Comercializacao { get; set; }

        }
        public class VeiculacacaoModel
        {
            public Int32 Id_Veiculacao{ get; set; }
            public String Cod_Programa { get; set; }
            public String Cod_Caracteristica { get; set; }
            public String Cod_Comercial    { get; set; }
            public Int32 Qtd_Total{ get; set; }
            public Boolean Permite_Editar { get; set; }
            public String Cod_Tipo_Comercializacao{ get; set; }
            public String Nome_Tipo_Comercializacao { get; set; }
            public List<InsercoesModel> Insercoes { get; set; }
        }
        public class VeiculacaoOnLineModel
        {
            public String Data_Inicio { get; set; }
            public String Data_Fim{ get; set; }
            public String Cod_Caracteristica { get; set; }
            public String Cod_Comercial { get; set; }
            public String Cod_Programa{ get; set; }
            public String Qtd{ get; set; }
        }
        public class InsercoesModel
        {
            public Int32 Id_Veiculacao { get; set; }
            public DateTime Data_Exibicao { get; set; }
            public byte Dia { get; set; }
            public String Dia_Semana  { get; set; }
            public Int32? Qtd { get; set; }
            public Boolean Tem_Grade { get; set; }
            public Boolean Valido{ get; set; }
        }
        public class VeiculoModel
        {
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }

        }

        public class MapaReservaFiltroModel
        {
            public Int32 Numero_Negociacao { get; set; }
            public Int32 Numero_Mr { get; set; }
            public String Numero_Pi { get; set; }
            public String Competencia_Inicio { get; set; }
            public String Competencia_Fim { get; set; }
            public String Agencia { get; set; }
            public String Cliente { get; set; }
            public String Cod_Empresa_Venda { get; set; }
            public String Cod_Empresa_Faturamento { get; set; }
            public String Contato { get; set; }
        }
        public class MapaReservaMidiaFiltroModel
        {
            public Int32 Id_Contrato { get; set; }
            public String Competencia { get; set; }
            public String Cod_Veiculo { get; set; }
            public Byte Display { get; set; }
        }
        public class MapaReservaMidiaModel
        {
            public String Cod_Veiculo { get; set; }
            public Int32 Tipo_Linha { get; set; }
            public String Cod_Programa { get; set; }
            public String Cod_Caracteristica { get; set; }
            public String Cod_Comercial { get; set; }
            public Boolean Indica_Exibido { get; set; }
            public Int32 Qtd_Total { get; set; }
            public Double Valor_Tabela { get; set; }
            public Double Valor_Negociado { get; set; }
            public Double Desconto { get; set; }
            public List<MapaReservaInsercoesModel> Insercoes { get; set; }
        }
        public class MapaReservaInsercoesModel
        {
            public String Data_Exibicao { get; set; }
            public String Dia { get; set; }
            public String Dia_Semana { get; set; }
            public Int32 Qtd { get; set; }

        }
        public class GetTerceirosNegociacaoModel
        {
            public Int32 Numero_Negociacao { get; set; }
            public String Tabela { get; set; }
            public String Codigo { get; set; }
        }
        public  class ParamNewMidiaModel
        {
            public String Inicio_Campanha{ get; set; }
            public String Fim_Campanha{ get; set; }
            public String    Cod_Programa { get; set; }
            public Boolean Indica_Midia_Online { get; set; }
            public List<VeiculoModel> Veiculos { get; set; }
        }
    }
}