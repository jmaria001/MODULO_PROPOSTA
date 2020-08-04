using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Security;
using System.Xml.Serialization;
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;
using CLASSDB;

namespace PROPOSTA
{
    public partial class SimLib
    {
        public void NewParameter(SqlDataAdapter adp,String Parameter,String value)
        {
            
            if (!String.IsNullOrEmpty(value))
            {
                adp.SelectCommand.Parameters.AddWithValue(Parameter, value.TrimEnd());
            }
            else
            {
                adp.SelectCommand.Parameters.AddWithValue(Parameter, DBNull.Value);
            }
        }
        public void NewParameter(SqlDataAdapter adp, String Parameter, Int32 value)
        {
            
            adp.SelectCommand.Parameters.AddWithValue(Parameter, value);
            
        }
        public void NewParameter(SqlDataAdapter adp, String Parameter, Int32 value,Boolean ConvertZeroToNull)
        {
            if (value==0 )
            {
                if (ConvertZeroToNull)
                {
                    adp.SelectCommand.Parameters.AddWithValue(Parameter, DBNull.Value);
                }
                else
                {
                    adp.SelectCommand.Parameters.AddWithValue(Parameter, value);
                }
            }
            else
            {
                adp.SelectCommand.Parameters.AddWithValue(Parameter, value);
            }
        }
        public void NewParameter(SqlDataAdapter adp, String Parameter, Boolean value,Boolean ConvertBooleanToTinyint)
        {
            if (ConvertBooleanToTinyint)
            {
                adp.SelectCommand.Parameters.AddWithValue(Parameter, (value ? 1 : 0));
            }
            else
            {
                adp.SelectCommand.Parameters.AddWithValue(Parameter, value);
            }

        }
        public void NewParameter(SqlDataAdapter adp, String Parameter, Boolean value )
        {

            adp.SelectCommand.Parameters.AddWithValue(Parameter, value);

        }
        public void NewParameter(SqlDataAdapter adp, String Parameter, Byte value)
        {
            adp.SelectCommand.Parameters.AddWithValue(Parameter, value);
        }
        public void NewParameter(SqlDataAdapter adp, String Parameter, Byte value, Boolean ConvertZeroToNull)
        {
            if (value == 0)
            {
                if (ConvertZeroToNull)
                {
                    adp.SelectCommand.Parameters.AddWithValue(Parameter, DBNull.Value);
                }
                else
                {
                    adp.SelectCommand.Parameters.AddWithValue(Parameter, value);
                }
            }
            else
            {
                adp.SelectCommand.Parameters.AddWithValue(Parameter, value);
            }
        }
        public void NewParameter(SqlDataAdapter adp, String Parameter, DateTime value)
        {
            if (value == DateTime.MinValue)
            {
                adp.SelectCommand.Parameters.AddWithValue(Parameter, DBNull.Value);
            }
            else
            {
                adp.SelectCommand.Parameters.AddWithValue(Parameter, value);
            }
            
        }
        public  String SerializeToString(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj,ns);

                return writer.ToString();
            }
        }
        public  String GenereteSecretKey()
        {
            String key = "";
            Random rnd = new Random();
            Random step  = new Random();
            for (int i = 0; i < 20; i++)
            {
                switch (step.Next(1,4))
                {
                    case 1:
                        key += (char)rnd.Next(97, 122);
                        break;
                    case 2:
                        key += (char)rnd.Next(49, 57);
                        break;
                    case 3:
                        key += (char)rnd.Next(65, 90);
                        break;
                    case 4:
                        key += (char)rnd.Next(58, 74);
                        break;
                }
            }
            return key.Substring(0, 5) + "-" + key.Substring(5, 5) + "-" + key.Substring(10, 5) + "-" + key.Substring(15,5);
        }
        public String Criptografa(String Par_Campo)
        {
            
                int _byte1 = 0;
                int _byte2 = 0;
                int _byte3 = 0;
                var _byte4 = "";
                var _byte5 = "";
                Random rnd = new Random();

            for (int i = 0; i < Par_Campo.Length; i++)
            {
                //_byte1 = Int32.Parse(Par_Campo.Substring(i, 1));

                _byte1 = (int)Par_Campo[i];

                _byte2 = rnd.Next(1,700);
                _byte3 = _byte1 + _byte2;
                    _byte4 += ("0000" + _byte3.ToString()).Right(3) + ("0000" + _byte2.ToString()).Right(3);
                }
                for (var x = 0; x < _byte4.Length; x += 2)
                {
                    _byte5 += _byte4.Substring(x + 1, 1) + _byte4.Substring(x, 1);
                }
                return _byte5;
            }
        public  String Decriptografa(string Par_Campo)
        {
            String Var_Senha = "";
            String Var_Criptografa = "";
            Int32 bt1 = 0;
            Int32 bt2 = 0;
            Int32 bt3 = 0;
            try
            {
                for (int Var_Contador = 0; Var_Contador < Par_Campo.Length; Var_Contador += 2)
                {
                    Var_Criptografa += Par_Campo.Substring(Var_Contador + 1, 1) + Par_Campo.Substring(Var_Contador, 1);
                }
                for (int Var_Contador = 0; Var_Contador < @Var_Criptografa.Length; Var_Contador += 6)
                {
                    bt1 = Int16.Parse(Var_Criptografa.Substring(Var_Contador, 3));
                    bt2 = Int16.Parse(Var_Criptografa.Substring(Var_Contador + 3, 3));
                    bt3 = bt1 - bt2;
                    Var_Senha += (Char)bt3;
                }
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
            finally
            {
            }
            return Var_Senha.TrimEnd();
            
        }
        public  String GetJsonItem(String pJsonString,String pItem)
        {
            var obj = "";
            if (!String.IsNullOrEmpty( pJsonString))
            {
                JObject json = JObject.Parse(pJsonString);
                obj = json[pItem].Value<string>();
            }
            return obj;                            
        }
        public   Int32 CompetenciaInt(String pCompetenciaChar)
        {
            Int32 retorno = 0;
            if (!String.IsNullOrEmpty(pCompetenciaChar))
            {
                String Mes = pCompetenciaChar.Substring(0, 2);
                String Ano = pCompetenciaChar.Substring(3, 4);
                retorno =Int32.Parse(Ano + Mes);
            }
            return retorno;
        }
        public String CompetenciaString(Int32 pcompetencia)
        {
            String retorno = "";
            if (pcompetencia >0 )
            {
                retorno = pcompetencia.ToString().Substring(4,2) + '/' +pcompetencia.ToString().Substring(0, 4);
            }
            return retorno;
        }
        public DateTime LastDay(Int32 Month,Int32 Year)
        {
            return new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));
        }
        public DateTime FirstDay(Int32 Month, Int32 Year)
        {
            return new DateTime(Year, Month, 1);
        }
        public String CompetenciaExtenso(Int32 pCompetencia)
        {
            String strMes= pCompetencia.ToString().Substring(4,2);
            switch (pCompetencia.ToString().Substring(4, 2))
            {
                case "01":strMes = "Janeiro";break;
                case "02": strMes = "Fevereiro"; break;
                case "03": strMes = "Março"; break;
                case "04": strMes = "Abril"; break;
                case "05": strMes = "Maio"; break;
                case "06": strMes = "Junho"; break;
                case "07": strMes = "Julho"; break;
                case "08": strMes = "Agosto"; break;
                case "09": strMes = "Setembro"; break;
                case "10": strMes = "Outubro"; break;
                case "11": strMes = "Novembro"; break;
                case "12": strMes = "Dezembro"; break;
                default:break;
            }
            return strMes+'/' + pCompetencia.ToString().Substring(0, 4);
        }
    }
}
