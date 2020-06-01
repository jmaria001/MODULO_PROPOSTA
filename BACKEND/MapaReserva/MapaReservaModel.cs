

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

        public class MapaReservaModel
        {
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
            public String Cod_Veiculo{ get; set; }
            public Byte Display{ get; set; }
        }
        public class MapaReservaMidiaModel
        {
            public String Cod_Veiculo { get; set; }
            public Int32 Tipo_Linha { get; set; }
            public String Cod_Programa { get; set; }
            public String Cod_Caracteristica{ get; set; }
            public String Cod_Comercial{ get; set; }
            public Boolean Indica_Exibido{ get; set; }
            public Int32 Qtd_Total{ get; set; }
            public Double Valor_Tabela{ get; set; }
            public Double Valor_Negociado{ get; set; }
            public Double Desconto { get; set; }
            public List<MapaReservaInsercoesModel> Insercoes { get; set; }
        }
        public class MapaReservaInsercoesModel
        {
            public String Data_Exibicao{ get; set; }
            public String Dia { get; set; }
            public String Dia_Semana { get; set; }
            public  Int32 Qtd { get; set; }

        }
    }
}