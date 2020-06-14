using CLASSDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PROPOSTA
{
    public partial class Grade

    {
        public GradeListModel GradeList(GradeFiltroModel Filtro)
        {
            clsConexao cnn = new clsConexao(this.Credential);
            cnn.Open();
            SqlDataAdapter Adp = new SqlDataAdapter();
            DataTable dtb = new DataTable("dtb");
            SimLib clsLib = new SimLib();
            GradeListModel Grades = new GradeListModel();
            List<GradeListDiaModel>  Dias = new List<GradeListDiaModel>();
            List<GradeListProgramaModel> Programas = new List<GradeListProgramaModel>();

            try
            {
                SqlCommand cmd = cnn.Procedure(cnn.Connection, "Pr_Proposta_Grade_List");
                Adp.SelectCommand = cmd;
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Login", this.CurrentUser);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Cod_Veiculo", Filtro.Cod_Veiculo);
                Adp.SelectCommand.Parameters.AddWithValue("@Par_Competencia", clsLib.CompetenciaInt(Filtro.Competencia));
                Adp.Fill(dtb);
                DateTime dtUltimoDia = DateTime.MinValue;
                if (dtb.Rows.Count>0)
                {
                    dtUltimoDia = dtb.Rows[0]["Data_Exibicao"].ToString().ConvertToDatetime();
                }
                foreach (DataRow drw in dtb.Rows)
                {
                    if (drw["Data_Exibicao"].ToString().ConvertToDatetime() != dtUltimoDia)
                    {
                        Dias.Add(new GradeListDiaModel() { Data_Exibicao = dtUltimoDia, Programas = Programas ,Dia_Semana=dtUltimoDia.ToString("ddd").ToUpper()});
                        Programas = new List<GradeListProgramaModel>();
                        dtUltimoDia = drw["Data_Exibicao"].ToString().ConvertToDatetime();
                    }
                    Programas.Add(new GradeListProgramaModel()
                    {
                        Cod_Programa = drw["Cod_Programa"].ToString(),
                        Nome_Programa = drw["Nome_Programa"].ToString(),
                        Hora_Inicio = drw["Hora_Inicio"].ToString(),
                        Hora_Termino = drw["Hora_Termino"].ToString(),
                        Nome_Genero = drw["Nome_Genero"].ToString(),
                    });
                }
                if (dtUltimoDia != DateTime.MinValue)
                {
                    Dias.Add(new GradeListDiaModel() { Data_Exibicao = dtUltimoDia, Programas = Programas, Dia_Semana = dtUltimoDia.ToString("ddd").ToUpper() });
                }
                
                Grades.Dias = Dias;
            }
            
            catch (Exception)
            {
                throw;
            }
            finally
            {
                cnn.Close();
            }
            return Grades;
        }

    }
}
