using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    [Serializable]
    public class EixoTematico
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }

        public EixoTematico()
        { }

        public List<Hcrp.Framework.Classes.EixoTematico> BuscarEixos()
        {
            return new Hcrp.Framework.Dal.EixoTematico().BuscarEixos();
        }
    }
}
