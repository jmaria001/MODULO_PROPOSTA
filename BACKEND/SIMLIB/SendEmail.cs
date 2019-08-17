using OpenPop.Mime;
using OpenPop.Pop3;
using PROPOSTA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Pop3;

namespace PROPOSTA
{
    public partial class SimLib
    {
        public String EmailErrorToSuporte(String pCredentialUser, String pMensagemErro,String pSource,String pTrace)
        {
            
            //apiCredential Cls = new apiCredential(pCredentialUser);
            //DataTable dtb = new DataTable();
            //String strDestinatarioSuporte = System.Configuration.ConfigurationManager.AppSettings["EmailSuporte"];
            //String strDestinatarioUsuario = "";
            //String UserName = Decriptografa(GetJsonItem(pCredentialUser, "Name"));

            //String strMensagemUsuario = "<html><head></head><body>";
            //strMensagemUsuario += "<h3>Olá " + UserName + "</h3>";
            //strMensagemUsuario += "<br>";
            //strMensagemUsuario += "<p>Notamos que ocorreu um erro durante a utilização do Sistema Módulo Proposta.</p>";
            //strMensagemUsuario += "<p>Não se preocupe que nossa Equipe de Suporte já está verificando.</p>";
            //strMensagemUsuario += "<p>Mensagem de erro</p>";
            //strMensagemUsuario += "<p>" + pMensagemErro + "</p>";
            ////strMensagemUsuario += "<p>Aplication:<p>";
            ////strMensagemUsuario += "<p>" + pSource + "</p>";
            ////strMensagemUsuario += "<p>Trace:</p>";
            ////strMensagemUsuario += "<p>"+pTrace+"<p>";
            //strMensagemUsuario += "<p style='font-size:12px;font- amily:Verdana;font- tyle:italic'>Favor não responder a esse email pois foi enviado automaticamente pelo sistema </p>";
            //strMensagemUsuario += "</body></html>";

            //String strMensagemSuporte  = "<html><head></head><body>";
            //strMensagemSuporte += "<h3>Erro na utilizacao do Sistema Módulo Proposta.</h3>";
            //strMensagemSuporte += "<br>";
            //strMensagemSuporte += "<p>Foi identificado um erro durante a utilização do Módulo Proposta.</p>";
            //strMensagemSuporte += "<p>Usuário:" + UserName  +"</p>";
            //strMensagemSuporte += "<p>Detalhes do Erro:</p>";
            //strMensagemSuporte += "<p>" + pMensagemErro + "</p>";
            //strMensagemSuporte += "<p>Aplication:<p>";
            //strMensagemSuporte += "<p>" + pSource + "</p>";
            //strMensagemSuporte += "<p>Trace:</p>";
            //strMensagemSuporte += "<p>" + pTrace + "<p>";
            //strMensagemSuporte += "<p style='font-size:12px;font- amily:Verdana;font- tyle:italic'>Favor não responder a esse email pois foi enviado automaticamente pelo sistema </p>";
            //strMensagemSuporte += "</body></html>";

            //try
            //{
            //    dtb = Cls.GetUserData();
            //    if (dtb.Rows.Count > 0)
            //    {
            //        strDestinatarioUsuario = dtb.Rows[0]["Email"].ToString();
            //    }

            //    if (!String.IsNullOrEmpty(strDestinatarioUsuario))
            //    {
            //        EnviaEmail(strDestinatarioUsuario, "", "", "Notificação de Erro Módulo Proposta", strMensagemUsuario);
            //    }

            //    if (!String.IsNullOrEmpty(strMensagemSuporte))
            //    {
            //        EnviaEmail(strDestinatarioSuporte, "", "", "Notificação de Erro Módulo Proposta", strMensagemSuporte);
            //    }

            //}
            //catch (Exception Ex)
            //{

            //    throw new Exception(Ex.Message);
            //}



            return "";
        }
        public void EnviaEmail(String emailDestinatario, String emailComCopia, String emailComCopiaOculta, String assuntoMensagem, String conteudoMensagem)
        {
            //======================Obtem valores do Config
            String emailRemetente = System.Configuration.ConfigurationManager.AppSettings["EmailFrom"];
            String server = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"];
            String User = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            String Password = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
            String porta = System.Configuration.ConfigurationManager.AppSettings["SmtpPorta"];
            if (porta == "")
            {
                porta = "25";
            }

            
            //=========================Cria objeto com dados do e-mail.
            System.Net.Mail.MailMessage objEmail = new System.Net.Mail.MailMessage();

            //========================Config Email
            objEmail.From = new System.Net.Mail.MailAddress(emailRemetente);
            objEmail.To.Add(emailDestinatario);
            if (!String.IsNullOrEmpty(emailComCopia))
            {
                objEmail.CC.Add(emailComCopia);
            }
            if (!String.IsNullOrEmpty(emailComCopiaOculta))
            {
                objEmail.Bcc.Add(emailComCopiaOculta);
            }

            objEmail.Priority = System.Net.Mail.MailPriority.Normal;
            objEmail.IsBodyHtml = true;
            objEmail.Subject = assuntoMensagem;
            objEmail.Body = conteudoMensagem;
            objEmail.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

            //================================Anexo
            // Caso queira enviar um arquivo anexo
            //Caminho do arquivo a ser enviado como anexo
            //string arquivo = Server.MapPath("arquivo.jpg");
            // Ou especifique o caminho manualmente
            //string arquivo = @"e:\home\LoginFTP\Web\arquivo.jpg";
            // Cria o anexo para o e-mail
            //Attachment anexo = new Attachment(arquivo, System.Net.Mime.MediaTypeNames.Application.Octet);
            // Anexa o arquivo a mensagemn+
            //objEmail.Attachments.Add(anexo);

            //===================================Cliente do emailComCopia 
            System.Net.Mail.SmtpClient objSmtp = new System.Net.Mail.SmtpClient();
            objSmtp.Host = server;
            objSmtp.Credentials = new NetworkCredential(User, this.Decriptografa(Password));
            objSmtp.Port = int.Parse(porta);

            
              

            try
            {
                objSmtp.Send(objEmail);
            }
            catch (Exception Rx)
            {
                throw new Exception(Rx.Message);
            }
            finally
            {
                objEmail.Dispose();
            }

        }
        //public void TesteEmail()
        //{
        //    try
        //    {
        //        Pop3Client client = new Pop3Client();
        //        client.Connect("pop.cartv.com.br");
        //        client.SendAuthUser("suporte@cartv.com.br");
        //        client.SendAuthPass("sancorp2");
        //        uint em = client.GetEmailCount();
        //        string str = client.GetEmailRaw(1);
        //        for (uint i = 0; i < em; i++)
        //        {

        //            string body = client.GetEmail(1).Body ;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {

        //        throw new Exception(Ex.Message);
        //    }


        //}
        public List<EmailMessage> TesteEmail()
        {
            System.Net.Pop3.Pop3Client client = new System.Net.Pop3.Pop3Client();
            client.Connect("pop.cartv.com.br");
            client.SendAuthUser("suporte@cartv.com.br");
            client.SendAuthPass("sancorp2");
            var emails = new List<Pop3Message>();
            List <EmailMessage> Emails = new List<EmailMessage>();

            try
            {
                var intEmails = client.GetEmailCount();
                                for (uint i = intEmails; i > 750; i--)
                    {
                    var email = client.GetEmail(i);
                    Emails.Add(new EmailMessage() {From=email.From,To=email.To,Subject=email.Subject,Attachaments=email.Attachments.Count,Body=email.Body,CC=email.ContentType});
                }
            }
            catch (Exception Ex)
            {

                throw new Exception(Ex.Message);
            }
            finally
            {
                client.Disconnect();
            }
            return Emails;
        }
        //public  List<Message> FetchAllMessages()
        //{
        //    // The client disconnects from the server when being disposed
        //    string hostname = "pop.cartv.com.br";
        //    int port = 995;
        //    bool useSsl = false;
        //        string username = "suporte@cartv.com.br";
        //        string password = "sancorp2";
        //    Pop3Client client = new Pop3Client();

        //        // Connect to the server
        //        client.Connect(hostname,port,useSsl);

        //        // Authenticate ourselves towards the server
        //        client.Authenticate(username, password);

        //        // Get the number of messages in the inbox
        //        int messageCount = client.GetMessageCount();

        //        // We want to download all messages
        //        List<Message> allMessages = new List<Message>(messageCount);

        //        // Messages are numbered in the interval: [1, messageCount]
        //        // Ergo: message numbers are 1-based.
        //        // Most servers give the latest message the highest number
        //        for (int i = messageCount; i > 0; i--)
        //        {
        //            allMessages.Add(client.GetMessage(i));
        //        }

        //        // Now return the fetched messages
        //        return allMessages;

        //}
        public class EmailMessage
        {
            public string From;
            public DateTime Date;
            public String To;
            public string CC;
            public string Bcc;
            public string Subject;
            public string Body;
            public Int32 Attachaments;


        }
    }
}

