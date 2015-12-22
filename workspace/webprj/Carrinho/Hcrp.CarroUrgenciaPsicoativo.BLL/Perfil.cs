using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class Perfil
    {
        public List<Entity.Perfil> ObterOPerfilDoUsuarioLogado()
        {
            return new DAL.Perfil().ObterOPerfilDoUsuarioLogado();
        }
    }
}
