using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Hcrp.Framework.Classes
{
    public class ConfiguracaoSistema
    {
        public string ServidorBancoDados
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["ServidorWeb"] == null)
                    throw new Exception("Nenhum Servidor Web foi informado. Verificar no inicio da aplicação se o mesmo foi definido.");
                else return Convert.ToString(System.Web.HttpContext.Current.Session["ServidorWeb"]);
            }
            set
            {
                System.Web.HttpContext.Current.Session["ServidorWeb"] = value.ToString();
            }
        }
        public string IpComputador
        {
        get
        {
            if (System.Web.HttpContext.Current.Session["IpComputador"] == null)
            {
                System.Web.HttpContext.Current.Session["IpComputador"] = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                return System.Web.HttpContext.Current.Request.UserHostAddress.ToString();                    
            }
            else 
            { 
                return Convert.ToString(System.Web.HttpContext.Current.Session["IpComputador"]);
            }
        }
        }
        public string NomTerminal {
            get
            {
                    if (System.Web.HttpContext.Current.Session["NomTerminal"] == null)
                    {
                        throw new Exception("Nenhum Nome de Terminal foi informado. Verificar no inicio da aplicação se o mesmo foi definido.");
                    }
                    else return Convert.ToString(System.Web.HttpContext.Current.Session["NomTerminal"]);
            }
            set
            {
                System.Web.HttpContext.Current.Session["NomTerminal"] = value.ToString();
            }        
        }
        public string SistemaOperacional
        {
            get
            {
                    if (System.Web.HttpContext.Current.Session["SistemaOperacional"]==null)
                        throw new Exception("Nenhum Sistema Operacional foi informado. Verificar no inicio da aplicação se o mesmo foi definido.");
                    else return Convert.ToString(System.Web.HttpContext.Current.Session["SistemaOperacional"]);
            }
            set
            {
                System.Web.HttpContext.Current.Session["SistemaOperacional"] = value.ToString();
            }
        }
        public string BrowserUsuario {
            get
            {
                if (System.Web.HttpContext.Current.Session["BrowserUsuario"] == null)
                        throw new Exception("Nenhum Browser foi informado. Verificar no inicio da aplicação se o mesmo foi definido.");
                else return Convert.ToString(System.Web.HttpContext.Current.Session["BrowserUsuario"]);
            }
            set
            {
                System.Web.HttpContext.Current.Session["BrowserUsuario"] = value.ToString();
            }        
        }
        public int CodInstituto {
            get
            {
                    if (System.Web.HttpContext.Current.Session["CodInstituto"] == null)
                    {
                        Hcrp.Framework.Classes.Instituto _instituto = new Hcrp.Framework.Dal.Instituto().BuscarInstituto(this.IpComputador);
                        System.Web.HttpContext.Current.Session["CodInstituto"] = _instituto.CodInstituto;
                        return _instituto.CodInstituto;
                    }
                    else
                        return Convert.ToInt32(System.Web.HttpContext.Current.Session["CodInstituto"]);
            }
        }        
        public int CodInstituicaoSistema {
            get
            {
                if (System.Web.HttpContext.Current.Session["CodInstituicaoSistema"] == null)
                    {
                        Hcrp.Framework.Classes.Instituto _instituto = new Hcrp.Framework.Dal.Instituto().BuscarInstituto(this.IpComputador);
                        System.Web.HttpContext.Current.Session["CodInstituicaoSistema"] = _instituto.CodInstSistema;
                        return _instituto.CodInstSistema;
                    }
                    else
                        return Convert.ToInt32(System.Web.HttpContext.Current.Session["CodInstituicaoSistema"]);
            }
        }        
        public int CodSistema {
            get
            {
                if (System.Web.HttpContext.Current.Session["CodSistema"] == null)
                        throw new Exception("Nenhum Sistema foi informado. Verificar no inicio da aplicação se o mesmo foi definido.");
                else return Convert.ToInt32(System.Web.HttpContext.Current.Session["CodSistema"]);
            }
            set
            {
                System.Web.HttpContext.Current.Session["CodSistema"] = value.ToString();
            }                      
        }
        public string ConnectionString
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["ConexaoUsuario"] == null)
                        throw new Exception("Nenhuma Connection String foi informada ou o método GerarConexaoUsuario() não foi executado. Verificar no inicio da aplicação se o mesmo foi definido.");
                else return Convert.ToString(System.Web.HttpContext.Current.Session["ConexaoUsuario"]);
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
        public Boolean GerarConexaoUsuario()
        {
            Boolean ret;            
            UsuarioConexao u = new UsuarioConexao();
            string conn = "Data Source=" + this.ServidorBancoDados + "; User Id=" + u.Login + ";Password=" + u.Senha + ";DataProvider=OracleClient;";
            System.Web.HttpContext.Current.Session["ConexaoUsuario"] = conn;
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                try
                {
                    ctx.Open();
                    ret = true;
                }
                catch (Exception ex)
                {
                    string erro = ex.Message;
                    ret = false;
                    //throw;
                }                
            }
            return ret;
        }
        public int RevistaSite {
            get
            {
                if (System.Web.HttpContext.Current.Session["Revista"] == null)
                {
                    throw new Exception("Nenhuma Revista foi informada. Verificar no inicio da aplicação se a mesma foi definida.");
                }
                else return Convert.ToInt32(System.Web.HttpContext.Current.Session["Revista"]);
            }
            set
            {
                System.Web.HttpContext.Current.Session["Revista"] = value.ToString();
            }                
        }

        public ConfiguracaoSistema() { }
    }
}
