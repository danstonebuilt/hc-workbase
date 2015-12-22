using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Hcrp.Framework.Classes
{
    public class RevistaArtigoAnexo
    {
        public string Descricao { get; set; }
        public string CaminhoAnexo { get; set; }
        public System.Web.UI.WebControls.FileUpload Arquivo { get; set; }

        public RevistaArtigoAnexo()
        { }
        
        public Boolean InserirAtualizarComArtigo(long seqArtigo)
        {
            return new Hcrp.Framework.Dal.RevistaArtigoAnexo().InserirAtualizarComArtigo(this, seqArtigo);
        }

    }
}
