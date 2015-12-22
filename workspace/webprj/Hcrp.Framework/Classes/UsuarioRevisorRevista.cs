using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class UsuarioRevisorRevista : Hcrp.Framework.Classes.Usuario
    {
        public int QtdEnviado
        {
            get
            {
                return new Hcrp.Framework.Dal.UsuarioRevisorRevista().RetornaQtdEnviado(this);
            }
        }
        public int QtdRevisando
        { get 
            { 
                return new Hcrp.Framework.Dal.UsuarioRevisorRevista().RetornaQtdRevisando(this);
            } 
        }
        public int QtdRevisado
        { 
            get 
            {
                return new Hcrp.Framework.Dal.UsuarioRevisorRevista().RetornaQtdRevisado(this);
            } 
        }
        public int QtdRecusado
        { 
            get 
            {
                return new Hcrp.Framework.Dal.UsuarioRevisorRevista().RetornaQtdRecusado(this);
            } 
        }
        public string StatusJuntoArtigo
        {
            get 
            {
                return new Hcrp.Framework.Dal.UsuarioRevisorRevista().BuscaStatusJuntoArtigo(Convert.ToInt32(System.Web.HttpContext.Current.Session["NumArtigo"]), this);
            }
        }
    }
}
