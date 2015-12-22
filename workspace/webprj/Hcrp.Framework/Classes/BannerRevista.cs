using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    [Serializable]
    public class BannerRevista
    {
        public int SeqBanner { get; set; }
        public string Nome { get; set; }
        public string Link { get; set; }

        public BannerRevista()
        { }

        public Boolean Inserir()
        { 
            return new Hcrp.Framework.Dal.BannerRevista().Inserir(this);
        }
    }
}
