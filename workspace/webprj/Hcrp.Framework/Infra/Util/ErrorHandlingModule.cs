using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.SessionState;
using System.Web;
using System.Net.Mail;
using System.Configuration;


namespace Hcrp.Framework.Infra.Util         
{
    public class ErrorHandlingModule : System.Web.IHttpModule
    {
        private HttpApplication _application;

        public void Init(HttpApplication context)
        {
            _application = context;
            _application.Error += new EventHandler(ErrorHandler);
        }


        private void ErrorHandler(object sender, EventArgs e)
        {
            Exception ex = _application.Server.GetLastError();
            this.CreateFileLog(ex);
        }

        private void CreateFileLog(Exception ex)
        {
            if (ex.InnerException != null)
            {
                string caminhoArquivoLogErro = System.Web.HttpContext.Current.Server.MapPath(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["caminhoArquivoLogErro"]));

                StreamWriter swArquivoLog;

                if (File.Exists(caminhoArquivoLogErro))
                {
                    swArquivoLog = File.AppendText(caminhoArquivoLogErro);
                }
                else
                {
                    swArquivoLog = File.CreateText(caminhoArquivoLogErro);
                }

                // Grava as informações do erro no LOG
                HttpSessionState sessao = HttpContext.Current.Session;

                swArquivoLog.WriteLine("\r\n");
                swArquivoLog.WriteLine("################################################################################");
                swArquivoLog.WriteLine(String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now) + ": " + ex.Message.ToString() + "\r\n");
                swArquivoLog.WriteLine("Método atual: " + ex.InnerException.TargetSite.Name + "\r\n");
                swArquivoLog.WriteLine("Host: " + _application.Request.UserHostAddress.ToString() + "\r\n");
                swArquivoLog.WriteLine("Local: " + _application.Request.Path.ToString() + "\r\n");
                swArquivoLog.WriteLine("--------------------------------------------------------------------------------");
                swArquivoLog.WriteLine("Detalhes: " + "\r\n\r\n" + ex.InnerException.Message + "\r\n");

                if (sessao != null)
                {
                    swArquivoLog.WriteLine("--------------------------------------------------------------------------------");

                    swArquivoLog.WriteLine("Variáveis de Sessão: " + "\r\n\r\n");

                    for (var i = 0; i < sessao.Count; i++)
                    {
                        swArquivoLog.WriteLine(string.Format("{0} = {1} \r\n ", sessao.Keys[i].ToString(), sessao[i]));
                    }

                    swArquivoLog.WriteLine("--------------------------------------------------------------------------------");

                }

                swArquivoLog.WriteLine("################################################################################");

                swArquivoLog.Flush();
                swArquivoLog.Close();
                swArquivoLog.Dispose();

                // Verificar se precisa ser disparado por email
                bool notificaPorEmail = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["notificaPorEmail"]);

                if (notificaPorEmail)
                {
                    EnviarLogDeErro();
                }
            }
        }

        public static string EnviarLogDeErro()
        {
            string erro = string.Empty;

            try
            {

                string fromLogEmail = Convert.ToString( ConfigurationManager.AppSettings["fromLogEmail"] );
                if (string.IsNullOrEmpty(fromLogEmail))
                {
                    fromLogEmail = "sistemashcrp@hcrp.fmrp.usp.br";
                }


                string fromLogDisplayName = Convert.ToString( ConfigurationManager.AppSettings["fromLogDisplayName"]);
                if (string.IsNullOrEmpty(fromLogDisplayName))
                {
                    fromLogDisplayName = "Sistemas HCRP";
                }

                string subjectLogErro = Convert.ToString( ConfigurationManager.AppSettings["logErroSubject"] );
                if (string.IsNullOrEmpty(subjectLogErro))
                {
                    subjectLogErro = "Relatório de erro dos sistemas HCRP";
                }

                string toEmail = Convert.ToString( ConfigurationManager.AppSettings["emailASerNotificado"]);

                if (!string.IsNullOrEmpty(toEmail))
                {


                    string toNotificarDisplayName = Convert.ToString(ConfigurationManager.AppSettings["emailASerNotificadoDisplayName"]);
                    string relatorioErroCorpoEmail = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["relatorioErroCorpoEmail"]);

                    if (string.IsNullOrEmpty(relatorioErroCorpoEmail))
                    {
                        relatorioErroCorpoEmail = "Log de erro.";
                    }

                    MailMessage mensagem = new MailMessage();
                    SmtpClient smtpSender;

                    MailAddress From = new MailAddress(fromLogEmail, fromLogDisplayName);
                    MailAddress to = new MailAddress(toEmail, toNotificarDisplayName);

                    mensagem.From = From;
                    mensagem.To.Add(to);

                    //mensagem.Bcc.Add("scsomera@hcpr.fmrp.usp.br");
                    //mensagem.Bcc.Add("alecrim@hcrp.fmrp.usp.br");
                    //mensagem.Bcc.Add("rsantana@hcrp.fmrp.usp.br");
                    //mensagem.Bcc.Add("qualidadehcrp@gmail.com");
                    mensagem.Bcc.Add("fbarbosa@hcrp.fmrp.usp.br");
                    
                    mensagem.Subject = subjectLogErro;
                    mensagem.SubjectEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");
                    mensagem.Priority = MailPriority.High;
                    mensagem.IsBodyHtml = true;
                    mensagem.Body = relatorioErroCorpoEmail;
                    mensagem.BodyEncoding = System.Text.Encoding.GetEncoding("ISO-8859-1");

                    string arquivoLog = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["caminhoArquivoLogErro"]);

                    if (!String.IsNullOrEmpty(arquivoLog)) 
                    {
                        if ((HttpContext.Current != null) && (HttpContext.Current.Server != null))
                        {

                            arquivoLog = HttpContext.Current.Server.MapPath(arquivoLog);

                            if (File.Exists(arquivoLog))
                            {
                                Attachment anexoArquivoLog = new Attachment(arquivoLog);
                                mensagem.Attachments.Add(anexoArquivoLog);
                            }
                        }
                    }

                    smtpSender = new SmtpClient(ConfigurationManager.AppSettings["smtp"].ToString());

                    smtpSender.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpSender.Send(mensagem);

                    mensagem.Dispose();

                }


            }
            catch (Exception err)
            {
                erro = err.Message;
            }

            return erro;
        }


        #region IHttpModule Members

        public void Dispose()
        {

        }

        #endregion
    }
}
