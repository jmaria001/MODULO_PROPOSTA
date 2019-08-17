using System;
namespace PROPOSTA
{
    public partial class apiCredential
    {
        private String Credential;
        private String CurrentUser;
        private SimLib clsLib = new SimLib();
        public apiCredential(String pUser)
        {
            this.Credential = pUser;
            this.CurrentUser = clsLib.Decriptografa(clsLib.GetJsonItem(this.Credential, "Name"));
        }
        public apiCredential()
        {
        }

        public class ParamCredential
        {

            public Int32 RouteId{ get; set; }
            public String UserName{ get; set; }
            public String Password{ get; set; }

        }
        public class RememberPassword
        {
            public String Email;
            public String Login;
            public Boolean EsqueceuLogin;
            public String Url;
            public String Senha;
            public String Token;
        }
    }
}