using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using PROPOSTA;
using System.Data;
using Newtonsoft.Json;


namespace webapi

{
    public class AuthorizationServerProvider:OAuthAuthorizationServerProvider
    {
        public override async Task   ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
             context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Acess-Control-Allow-Origin", new[] { "*" });
            try
            {
                apiCredential cls = new apiCredential();
                SimLib clsLib = new SimLib();
                var user = context.UserName;
                var password = context.Password;

                user = clsLib.Criptografa(user); //Criptogradar aqui por causa do Genexus que nao criptografa por javascript
                password = clsLib.Criptografa(password); //Criptogradar aqui por causa do Genexus  por javascript

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                xUser xUser = new xUser();
                xUser.Name = user;
                xUser.Password = password;
                String json = JsonConvert.SerializeObject(xUser);
                identity.AddClaim(new Claim(ClaimTypes.Name, json));

                DataTable dtbLogin = cls.Login(json);
                if (dtbLogin.Rows[0]["Valido"].ToString()!="1")
                {
                    context.Response.StatusCode = 201;
                    context.SetError(dtbLogin.Rows[0]["Mensagem"].ToString());
                    return;
                }
                              
                var roles = new List<string>();
                roles.Add("Admin");
                roles.Add("Users");
                foreach (var role in roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
                GenericPrincipal principal = new GenericPrincipal(identity, roles.ToArray());
                Thread.CurrentPrincipal = principal;
                context.Validated(identity);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 201;
                context.SetError(ex.Message.ToString());
            }
        }
        public class xUser
        {
            public String Name;
            public String Password;
            public string Autenticacao;
            public String Sistema;
            public String SecretKey;
        }
    }
}