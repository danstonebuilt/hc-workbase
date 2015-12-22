using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class DireitosUsuario
    {
        public int Codigo { get; set; }
        public string Modulo { get; set; }
        public string Descricao { get; set; }

        public DireitosUsuario() { }

        public List<DireitosUsuario> BuscarModulos(int sistema, Hcrp.Framework.Classes.UsuarioConexao usuario) {
            return new Hcrp.Framework.Dal.DireitosUsuario().BuscarModulos(sistema, usuario);
        }
    }
}
