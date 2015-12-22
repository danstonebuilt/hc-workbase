using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Hcrp.Infra;

namespace Hcrp.Framework.Infra.Util
{
    public class Parametrizacao
    {
        private static Parametrizacao instancia = null;
        private Parametrizacao() { }

        /// <summary>
        /// Gerencia a instancia da classe.
        /// </summary>
        /// <returns></returns>
        public static Parametrizacao Instancia()
        {
            if (instancia == null) instancia = new Parametrizacao();
            return instancia;
        }

        /// <summary>
        /// Enum status
        /// </summary>
        //public enum StatusNotificacao
        //{
        //    AguardandoInvestigacao = 113,
        //    EmProcessoDeInvestigacao = 114,
        //    InvestigacaoConcluida = 115
        //}


        /// <summary>
        /// Retorna/Seta id do instituto
        /// </summary>
        public int CodInstituto
        {
            get
            {
                Hcrp.Infra.AcessoDado.RepositorioUtil _repositorio = new Hcrp.Infra.AcessoDado.RepositorioUtil();
                return _repositorio.ObterInstituto();
            }
        }

        /// <summary>
        /// Retorna/Seta id do instituto
        /// </summary>
        public int NumeroNoBancoDoUsuarioLogado
        {
            get
            {
                Hcrp.Infra.AcessoDado.RepositorioUtil _repositorio = new Hcrp.Infra.AcessoDado.RepositorioUtil();
                return _repositorio.ObterNumeroNoBancoDoUsuarioLogado();
            }
        }

        /// <summary>
        /// Retorna quantidade de registro por página
        /// </summary>
        public int QuantidadeRegistroPagina
        {
            get
            {
                int ret = 10;

                if (System.Configuration.ConfigurationManager.AppSettings["QuantidadeRegPagina"] != null)
                    return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["QuantidadeRegPagina"]);
                else
                    return ret;
            }
        }

        /// <summary>
        /// Retorna o codigo do sistema [FB 19/09/2012]
        /// </summary>
        public static int CodSistema
        {
            get
            {
                int ret = 0;

                if (System.Configuration.ConfigurationManager.AppSettings["CodigoSistema"] != null)
                    return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CodigoSistema"]);
                else
                    return ret;
            }
        }

        /// <summary>
        /// Retorna o codigo do sistema [FB 19/09/2012]
        /// </summary>
        public int CodigoSistema
        {
            get
            {
                int ret = 0;

                if (System.Configuration.ConfigurationManager.AppSettings["CodigoSistema"] != null)
                    return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CodigoSistema"]);
                else
                    return ret;
            }
        }


        /// <summary>
        /// Retorna código da instituição
        /// </summary>
        public int CodInstituicao
        {
            get
            {
                Hcrp.Infra.AcessoDado.RepositorioUtil _repositorio = new Hcrp.Infra.AcessoDado.RepositorioUtil();
                int codInstituicao = _repositorio.ObterInstituto(false);
                return codInstituicao;
            }
        }


        /// <summary>
        /// Obter código da notificação
        /// </summary>
        public long CodNotificacao
        {
            get
            {
                if (System.Web.HttpContext.Current.Request.Cookies["usuario"] == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(System.Web.HttpContext.Current.Request.Cookies["usuario"]["codNotificacao"]);
                }
            }
            set
            {
                HttpCookie aCookie;

                if (System.Web.HttpContext.Current.Request.Cookies["usuario"] != null)
                {
                    aCookie = System.Web.HttpContext.Current.Request.Cookies["usuario"];
                }
                else
                {
                    aCookie = new HttpCookie("usuario");
                }

                aCookie.Values["codNotificacao"] = value.ToString();
                aCookie.Expires = DateTime.Now.AddHours(4);
                HttpContext.Current.Response.Cookies.Add(aCookie);
            }
        }

        public void LimparCookie()
        {
            try
            {
                HttpCookie aCookie = System.Web.HttpContext.Current.Request.Cookies["usuario"];

                if (aCookie != null)
                {
                    aCookie.Expires = DateTime.Now.AddDays(-1);
                    HttpContext.Current.Response.Cookies.Add(aCookie);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter Ip do Usuario.
        /// </summary>
        public string ObterIpDoUsuario
        {
            get
            {

                string ip =
                    String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"])
                        ? System.Web.HttpContext.Current.Request.UserHostAddress.ToString()
                        : System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                ip = ip == "::1" ? "127.0.0.1" : ip;

                if (ip.Contains(","))
                    ip = ip.Split(',').First().Trim();

                if (ip.Contains(":"))
                    ip = ip.Split(':').First().Trim();

                return ip;


                return System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            }
        }


    }
}
