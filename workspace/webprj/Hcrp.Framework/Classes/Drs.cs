using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Drs
    {
        public int Codigo { get; set; }
        public bool Ativa { get; set; }
        public string Numero { get; set; }
        public string Nome { get; set; }        
        public bool FazParteComplexoHc { get; set; }        

        public Drs()
        {
        }

        public Hcrp.Framework.Classes.Drs BuscarDrsCodigo(int codigo)
        {
            return new Hcrp.Framework.Dal.Drs().BuscarDrsCodigo(codigo);
        }

        public List<Hcrp.Framework.Classes.Drs> BuscarDrsUsuario(Hcrp.Framework.Classes.UsuarioConexao u)
        {
            return new Hcrp.Framework.Dal.Drs().BuscarDrsUsuario(u);
        }
    }
}
