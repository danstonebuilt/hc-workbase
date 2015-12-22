using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class SiteMenuHorizontal
    {
        public int Codigo { get; set; }
        public int _Site { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public string Url { get; set; }
        public int Ordem { get; set; }
        public string Imagem { get; set; }


        public SiteMenuHorizontal CarregarMenuHorizontal(int CodMenuV)
        {
            return new Hcrp.Framework.Dal.SiteMenuHorizontal().CarregarMenuHorizontal(CodMenuV);
        }

        public List<Hcrp.Framework.Classes.SiteMenuVertical> ItensMenuVertical
        {
            get
            {
                return new Hcrp.Framework.Dal.SiteMenuVertical().BuscarMenuVertical(this.Codigo);
            }

        }

        public Site Site
        {
            get
            {
                if (this.Site == null)
                {
                    return new Hcrp.Framework.Dal.Site().BuscarSite(_Site);
                }
                else return this.Site;
            }
        }

        public List<Hcrp.Framework.Classes.SiteMenuHorizontal> BuscarMenuHorizontal()
        {
            return new Hcrp.Framework.Dal.SiteMenuHorizontal().BuscarMenuHorizontal(_Site);
        }

    }
}
