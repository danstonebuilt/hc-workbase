using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

namespace Hcrp.Framework.Infra.Util
{
    public class TratamentoDeErro : System.Web.IHttpModule
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

                string caminhoArquivoLogErro = System.Web.HttpContext.Current.Server.MapPath("~/Erro");

                if (!Directory.Exists(caminhoArquivoLogErro))
                {
                    Directory.CreateDirectory(string.Concat(System.Web.HttpContext.Current.Server.MapPath("~/"), "Erro"));
                }
                
                StreamWriter swArquivoLog;

                caminhoArquivoLogErro = string.Concat(caminhoArquivoLogErro, "/log.txt");

                if (File.Exists(caminhoArquivoLogErro))
                {
                    swArquivoLog = File.AppendText(caminhoArquivoLogErro);
                }
                else
                {
                    swArquivoLog = File.CreateText(caminhoArquivoLogErro);
                }

                HttpContext.Current.Session["erro"] = ex.InnerException.Message;

                // Grava as informações do erro no LOG
                swArquivoLog.WriteLine("\r\n");
                swArquivoLog.WriteLine("################################################################################");
                swArquivoLog.WriteLine(String.Format("{0:dd/MM/yyyy HH:mm:ss}", DateTime.Now) + ": " + ex.Message.ToString() + "\r\n");
                swArquivoLog.WriteLine("Método atual: " + ex.InnerException.TargetSite.Name + "\r\n");
                swArquivoLog.WriteLine("Host: " + _application.Request.UserHostAddress.ToString() + "\r\n");
                swArquivoLog.WriteLine("Local: " + _application.Request.Path.ToString() + "\r\n");
                swArquivoLog.WriteLine("--------------------------------------------------------------------------------");
                swArquivoLog.WriteLine("Detalhes: " + "\r\n\r\n" + ex.InnerException.Message + "\r\n");
                swArquivoLog.WriteLine("################################################################################");

                swArquivoLog.Flush();
                swArquivoLog.Close();
                swArquivoLog.Dispose();

            }





        }

        #region IHttpModule Members

        public void Dispose()
        {

        }

        #endregion
    }
}
