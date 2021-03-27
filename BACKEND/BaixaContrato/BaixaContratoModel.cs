using System;
using System.Collections.Generic;

namespace PROPOSTA
{
    public partial class BaixaContrato
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public BaixaContrato(String pCredential)
        {
            this.Credential = pCredential;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }


        public class BaixaContratoModel
        {
            public String Id_Operacao { get; set; }
            public String Tipo_Operacao { get; set; }
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public int Sequencia_Mr { get; set; }
            public String Data_Inicial { get; set; }
            public String Data_Final { get; set; }
            public String Cod_Qualidade { get; set; }
            public String Cod_Programa { get; set; }
            public String Titulo { get; set; }
            public String Cod_Comercial { get; set; }
            public String Titulo_Comercial { get; set; }
            public String Cod_Agencia { get; set; }
            public String Nome_Agencia { get; set; }
            public String Cod_Cliente { get; set; }
            public String Nome_Cliente { get; set; }
            public Boolean Domingo { get; set; }
            public Boolean Segunda { get; set; }
            public Boolean Terca { get; set; }
            public Boolean Quarta { get; set; }
            public Boolean Quinta { get; set; }
            public Boolean Sexta{ get; set; }
            public Boolean Sabado { get; set; }
            public String Data_Help { get; set; }
            public String Cod_Usuario { get; set; }
            public Boolean Indica_Cancelamento { get; set; }
            public Boolean Indica_Cancelar_Am { get; set; }
            public String Motivo_Cancelamento { get; set; }
            public String Cod_Veiculo { get; set; }
            public String DiaSemana { get; set; }
            public Boolean Loaded { get; set; }
            public String Cod_Qualidade_Cancelamento { get; set; }
            public String Descricao_Qualidade_Cancelamento { get; set; }
            public List<BaixaContratoVeiculoModel> Veiculos { get; set; }
        }

        public class FiltroModel
        {
            public String Cod_Empresa { get; set; }
            public Int32 Numero_Mr { get; set; }
            public int Sequencia_Mr { get; set; }
            public String Cod_Programa { get; set; }
            public String Cod_Comercial{ get; set; }
        }

        public class BaixaContratoVeiculoModel
        {
            public String Cod_Veiculo { get; set; }
            public String Nome_Veiculo { get; set; }
            public Boolean Selected { get; set; }
        }
    }
}