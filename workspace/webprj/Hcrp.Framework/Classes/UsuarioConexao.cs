using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;

namespace Hcrp.Framework.Classes
{
    public class UsuarioConexao : Usuario
    {
        private IList<EPerfilRevista> _Direitos;
        private List<Hcrp.Framework.Classes.Drs> _Drs;
        private List<Hcrp.Framework.Classes.DireitosUsuario> _PerfilPrograma;

        public int NumUserBanco {
            get
            {
                if (HttpContext.Current.Session["numuserbanco"] != null)
                    return Convert.ToInt32(HttpContext.Current.Session["numuserbanco"].ToString());
                else throw new Exception("Nenhum Usuário foi informado. Verificar no inicio da aplicação se o mesmo foi definido.");
            }
        }
        public string Login {
            get
            {
                if (HttpContext.Current.Session["r"] != null)
                    return Infra.Util.Encryption.DecryptText(HttpContext.Current.Session["r"].ToString());
                else throw new Exception("Nenhum Usuário foi informado. Verificar no inicio da aplicação se o mesmo foi definido.");
            }
            set
            {
                HttpContext.Current.Session["Login"] = value.ToString();
            }   
        }
        public string Senha {
            get
            {
                if (HttpContext.Current.Session["p"] != null)
                    return Infra.Util.Encryption.DecryptText(HttpContext.Current.Session["p"].ToString());
                else throw new Exception("Nenhuma Senha foi informada. Verificar no inicio da aplicação se a mesma foi definida.");
            }
            set
            {
                HttpContext.Current.Session["Senha"] = value.ToString();
            }
        }
        public List<Hcrp.Framework.Classes.Drs> Drs
        {
            get
            {
                {
                    if (_Drs == null)
                    {
                        _Drs = new Hcrp.Framework.Classes.Drs().BuscarDrsUsuario(this);
                        return _Drs;
                    }
                    else
                    {
                        return _Drs;
                    }
                }
            }
        }

        public IList<EPerfilRevista> Direitos
        {
            get {
                if (this._Direitos == null)
                    _Direitos = new Hcrp.Framework.Dal.DireitosUsuario().BuscarRoles(new ConfiguracaoSistema().CodSistema, this);
                return _Direitos;
            } 
        }
        
        public List<Hcrp.Framework.Classes.DireitosUsuario> PerfilPrograma
        {
            get
            {
                if (this._PerfilPrograma == null)
                    _PerfilPrograma = new Hcrp.Framework.Dal.DireitosUsuario().BuscarPerfilPrograma(new ConfiguracaoSistema().CodSistema, this);
                return _PerfilPrograma;
            }
        }


        public UsuarioConexao() {

        }

        public string NomeCompleto
        {
            get
            {
                if (HttpContext.Current.Session["NomeCompleto"] == null)
                    HttpContext.Current.Session["NomeCompleto"] = Infra.Util.Encryption.EncryptText(new Hcrp.Framework.Dal.DireitosUsuario().BuscarNomeSobrenome(this));
                return Infra.Util.Encryption.DecryptText(HttpContext.Current.Session["NomeCompleto"].ToString());
            }
        }

        public string Cpf
        {
            get
            {
                if (HttpContext.Current.Session["CPF"] == null)
                    HttpContext.Current.Session["CPF"] = Infra.Util.Encryption.EncryptText(new Hcrp.Framework.Dal.DireitosUsuario().BuscarCpf(this));
                return Infra.Util.Encryption.DecryptText(HttpContext.Current.Session["CPF"].ToString());
            }
        }

        public string UltimaArea
        {
            get
            {
                if (HttpContext.Current.Session["UltimaArea"] == null)
                    HttpContext.Current.Session["UltimaArea"] = Infra.Util.Encryption.EncryptText(new Hcrp.Framework.Dal.DireitosUsuario().BuscarUltimaArea(this));
                return Infra.Util.Encryption.DecryptText(HttpContext.Current.Session["UltimaArea"].ToString());
            }
        }

        public string Email
        {
            get
            {
                if (HttpContext.Current.Session["Email"] == null)
                    HttpContext.Current.Session["Email"] = Infra.Util.Encryption.EncryptText(new Hcrp.Framework.Dal.DireitosUsuario().BuscarEmail(this));
                return Infra.Util.Encryption.DecryptText(HttpContext.Current.Session["Email"].ToString());
            }
        }

        public void SalvarEmail(string email, Int64 numUserBanco = 0)
        {
            new Hcrp.Framework.Dal.DireitosUsuario().SalvarEmail(this, email, numUserBanco);
        }

        // foi removido pelo franscico em 11/03/2011 - colocado de volta, usado no SiteHC
        public bool AdministraSite(int cod_site)
        {
            return new Hcrp.Framework.Dal.DireitosUsuario().AdministraSite(this, cod_site);
        }

        public enum EPerfilRevista 
        {
            //DSV
            /*
            PerfilAutor = 696,
            PerfilTriador = 697,
            PerfilRevisor = 698,
            PerfilCorretor = 700,
            PerfilDiagramador = 702,
            PerfilAdministrador = 703
            
            
            //HML
            PerfilAutor = 805,
            PerfilTriador = 806,
            PerfilRevisor = 807,
            PerfilCorretor = 808,
            PerfilDiagramador = 810,
            PerfilAdministrador = 811                        
            */
            //VEGA
            
            PerfilAutor = 822,
            PerfilTriador = 823,
            PerfilRevisor = 824,
            PerfilCorretor = 825,
            PerfilDiagramador = 826,
            PerfilAdministrador = 827
            
        }
    }
}
