using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Site
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string UrlAtivo { get; set; }
        public string UrlInativo { get; set; }


        public void BuscarSite(int CodSite)
        {
            new Hcrp.Framework.Dal.Site().BuscarSite(CodSite);
        }

        public enum ECodSite
        {
            SiteHC = 1,
            SiteHEAB = 2
        }
    }
}
