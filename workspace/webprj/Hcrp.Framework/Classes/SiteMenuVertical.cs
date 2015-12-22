using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class SiteMenuVertical
    {

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Url { get; set; }
        public bool Ativo { get; set; }
        public int CodigoPai { get; set; }
        public int Ordem { get; set; }

        public SiteMenuVertical()
        {
            
        }

        public Hcrp.Framework.Dal.SiteMenuVertical CarregarMenuVertical(int CodMenuV)
        {
            return new Hcrp.Framework.Dal.SiteMenuVertical().CarregarMenuVertical(CodMenuV);
        }

        public List<Hcrp.Framework.Classes.SiteMenuVertical> BuscarMenuVertical(int CodMenuHorizontal)
        {
            return new Hcrp.Framework.Dal.SiteMenuVertical().BuscarMenuVertical(CodMenuHorizontal);
        }

        public List<Hcrp.Framework.Classes.SiteMenuHorizontal> BuscarMenuHorizontal(int CodMenuVertical)
        {
            return new Hcrp.Framework.Dal.SiteMenuHorizontal().BuscarMenuHorizontalPorMenuVertical(CodMenuVertical);
        }


    }
}
