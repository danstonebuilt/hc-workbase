using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Hcrp.Framework.Classes
{
    public class ConfiguracaoSistema
    {
        #region Membros privados

        //public string _servidorDeBancoDeDados
        //{
        //    get
        //    {
        //        //if (this.CookieHcrp["sbd"] != null)
        //        //    return this.CookieHcrp["sbd"].ToString();
        //        //else
        //        //{
        //            //this.CookieHcrp.Values["sbd"] = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["Servidor"]);
        //            //this.CookieHcrp.Expires = DateTime.Now.AddHours(8);
        //            //return this.CookieHcrp["sbd"].ToString();

        //       //}

        //        if (System.Web.HttpContext.Current.Session["BaseDados"] != null)
        //        {
        //            return System.Web.HttpContext.Current.Session["BaseDados"].ToString();
        //        }
        //        else
        //        {
        //            return Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["Servidor"]);
        //        }
        //    }

        //}
        private string _login { get; set; }
        private string _senha { get; set; }
        private string _numUserBanco { get; set; }
        protected HttpCookie CookieHcrp;
        //protected HttpSessionStateBase SessionHCRP;

        #endregion

        public ConfiguracaoSistema()
        {
            //if (System.Web.HttpContext.Current.Request.Cookies["hcrp"] != null)
            //{
            //    CookieHcrp = System.Web.HttpContext.Current.Request.Cookies["hcrp"];
            //}
            //else
            //{
            //    CookieHcrp = new HttpCookie("hcrp");
            //}
            //HttpContext.Current.Session.Add("hcrp",SessionHCRP);
        }

        /// <summary>
        /// Servido de banco de dados
        /// </summary>
        public string ServidorBancoDados
        {
            get
            {

                //if (this.CookieHcrp["sbd"] != null)
                //    return this.CookieHcrp["sbd"].ToString();
                //else
                //{
                //    this.CookieHcrp.Values["sbd"] = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["Servidor"]);
                //    this.CookieHcrp.Expires = DateTime.Now.AddHours(8);
                //    return this.CookieHcrp["sbd"].ToString();
                //return Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["Servidor"]);

                if (System.Web.HttpContext.Current.Session["BaseDados"] != null)
                {
                    return System.Web.HttpContext.Current.Session["BaseDados"].ToString();
                }
                else
                {
                    return Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["Servidor"]);
                }
                //}
            }
            set { System.Web.HttpContext.Current.Session["BaseDados"] = value; }
        }

        /// <summary>
        /// Ip do computador cliente
        /// </summary>
        public string IpComputador
        {
            get
            {
                string ip = string.Empty;

                ip =
                    String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"])
                        ? System.Web.HttpContext.Current.Request.UserHostAddress.ToString()
                        : System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                // todo: RETIRAR
                if (ip == "::1" || ip == "127.0.0.1")
                {
                    ip = "10.165.100.76";
                }

                if (ip.Contains(","))
                    ip = ip.Split(',').First().Trim();

                if (ip.Contains(":"))
                    ip = ip.Split(':').First().Trim();

                //ip = System.Web.HttpContext.Current.Request.UserHostAddress;

                //if (System.Web.HttpContext.Current.Request.UserHostAddress != null)
                //    ip = System.Web.HttpContext.Current.Request.UserHostAddress;
                //else
                //ip =
                //    System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Remove(
                //        System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(":"));


                return ip;
            }
        }

        /// <summary>
        /// Nome da maquina cliente
        /// </summary>
        public string NomeTerminal
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];
            }
        }

        /// <summary>
        /// Sistema operacional
        /// </summary>
        public string SistemaOperacional
        {
            get
            {
                if (this.CookieHcrp["so"] != null)
                    return this.CookieHcrp["so"].ToString();
                else
                    throw new Exception("Nenhum sistema operacional foi informado. Verificar no inicio da aplicação se o mesmo foi definido.");
            }
            set
            {
                this.CookieHcrp.Values["so"] = value.ToString();
                this.CookieHcrp.Expires = DateTime.Now.AddHours(8);
            }
        }

        /// <summary>
        /// Browser do usuário
        /// </summary>
        public string BrowserUsuario
        {
            get
            {
                return HttpContext.Current.Request.Browser.Browser;
            }
        }

        /// <summary>
        /// Código do instituto
        /// </summary>
        public int CodInstituto
        {
            get
            {
                if (HttpContext.Current.Session["codInst"] == null)
                {
                    Hcrp.Framework.Classes.Instituto _instituto = new Hcrp.Framework.Dal.Instituto().BuscarInstituto(this.IpComputador);
                    HttpContext.Current.Session["codInst"] = _instituto.CodInstituto.ToString();
                    return _instituto.CodInstituto;
                }
                else
                    return Convert.ToInt32(HttpContext.Current.Session["codInst"]);
            }
        }

        /// <summary>
        /// Código da instituição sistema
        /// </summary>
        public int CodInstituicaoSistema
        {
            get
            {
                if (HttpContext.Current.Session["codInstSistema"] == null)
                {
                    Hcrp.Framework.Classes.Instituto _instituto = new Hcrp.Framework.Dal.Instituto().BuscarInstituto(this.IpComputador);
                    HttpContext.Current.Session["codInstSistema"] = _instituto.CodInstSistema.ToString();
                    return _instituto.CodInstSistema;
                }
                else
                    return Convert.ToInt32(HttpContext.Current.Session["codInstSistema"]);
            }
        }

        /// <summary>
        /// Retorna o código do sistema
        /// </summary>
        public int CodSistema
        {
            get
            {
                if (HttpContext.Current.Session["codSistema"] != null)
                    return Convert.ToInt32(HttpContext.Current.Session["codSistema"]);
                else
                    throw new Exception("Nenhum Sistema foi informado. Verificar no inicio da aplicação se o mesmo foi definido.");
            }
            set
            {
                HttpContext.Current.Session["codSistema"] = value.ToString();
            }
        }

        public string ConnectionString
        {
            get
            {

                string con = "";

                if (HttpContext.Current.Session["conn"] != null)
                {
                    try
                    {
                        con = Infra.Util.Encryption.DecryptText(HttpContext.Current.Session["conn"].ToString());
                    }
                    catch (Exception)
                    {
                        con = HttpContext.Current.Session["conn"].ToString();
                    }
                    //return TUtil.CryptHC(null, "D", this.CookieHcrp["conn"].ToString());
                }

                return con;

            }
        }

        public void LimparSession()
        {
            try
            {
                System.Web.HttpContext.Current.Session.Abandon();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LimparCookieHcrp()
        {
            if (this.CookieHcrp != null)
            {
                System.Web.HttpContext.Current.Response.Cookies.Remove("hcrp");
            }
        }



        /// <summary>
        /// Login do usuario no banco
        /// </summary>
        public string Login
        {
            get
            {
                string _nomeDoUsuario = "";

                if (HttpContext.Current.Session["r"] != null)
                {
                    _nomeDoUsuario = Infra.Util.Encryption.DecryptText(HttpContext.Current.Session["r"].ToString());
                }

                return _nomeDoUsuario;

            }
            set
            {
                this._login = value;
                HttpContext.Current.Session["r"] = Infra.Util.Encryption.EncryptText(value.ToString());
            }
        }

        /// <summary>
        /// Senha de banco do usuário
        /// </summary>
        public string Senha
        {
            get
            {
                string _senhaDoUsuario = "";
                if (HttpContext.Current.Session["p"] != null)
                {
                    _senhaDoUsuario = Infra.Util.Encryption.DecryptText(HttpContext.Current.Session["p"].ToString());
                }
                return _senhaDoUsuario;

            }
            set
            {
                this._senha = value;
                HttpContext.Current.Session["p"] = Infra.Util.Encryption.EncryptText(value.ToString());
            }
        }

        public Boolean GerarConexaoUsuario(string erro1 = "")
        {
            // Variaveis de apoio
            Boolean ret;

            erro1 = "";

            string servidorDeBancoDeDados = this.ServidorBancoDados, login = this._login, senha = this._senha;

            // Validar parâmetros para montagem da connectionstring
            if (string.IsNullOrWhiteSpace(servidorDeBancoDeDados))
                throw new ApplicationException("Nenhum servidor de banco de dados foi informado. Verificar no inicio da aplicação se o mesmo foi definido.");

            if (string.IsNullOrWhiteSpace(login))
                throw new ApplicationException("Nenhum login foi informado. Verificar no inicio da aplicação se o mesmo foi definido.");

            if (string.IsNullOrWhiteSpace(senha))
                throw new ApplicationException("Nenhuma senha foi informada. Verificar no inicio da aplicação se o mesmo foi definido.");

            // Montar a string de conexão
            string conn = "Data Source=" + servidorDeBancoDeDados + "; User Id=" + login + ";Password=" + senha + ";DataProvider=OracleClient;Min Pool Size=0;Max Pool Size=40;Incr Pool Size=2;Decr Pool Size=2;";                        

            // Configurar cookie da connection string
            HttpContext.Current.Session["conn"] = Infra.Util.Encryption.EncryptText(conn);

            // Testar a conexão
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto(conn))
            {
                try
                {
                    ctx.Open();
                    ret = true;
                }
                catch (Exception ex)
                {
                    HttpContext.Current.Session.Remove("hcrp");
                    string erro = ex.Message;

                    ret = false;

                    erro1 = erro;
                }
            }

            if (ret)
            {
                this._numUserBanco = Convert.ToString(new Hcrp.Framework.Dal.Usuario().BuscarUsuarioLogin(this._login));
                HttpContext.Current.Session["numuserbanco"] = _numUserBanco;
                Hcrp.Framework.Dal.Usuario.InstanciarSessoesDoUsuario(_numUserBanco);
                //    HttpContext.Current.Response.Cookies.Add(this.CookieHcrp); // Mandamos o cookie para o cliente
                //    this.CookieHcrp.Values["numuserbanco"] = Infra.Util.Encryption.EncryptText(Convert.ToString(new Hcrp.Framework.Dal.Usuario().BuscarUsuarioLogin(login)));
            }

            return ret;

        }

        public int RevistaSite
        {
            get
            {
                return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SeqRevista"]);
            }
        }

        public string NumUserBanco
        {
            get
            {
                return _numUserBanco;
            }
        }
    }
}
