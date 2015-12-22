using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    [Serializable]
    public class AutorRevistaArtigo
    {

        public string Nome { get; set; }
        public string Documento { get; set; }
        public Usuario.ETipoDocumento TipoDocumento { get; set; }
        public string Area { get; set; }
        public bool AutorPrincipal { get; set; }

        
        public AutorRevistaArtigo()
        { }

        public Boolean InserirAtualizar()
        {
            return new Hcrp.Framework.Dal.AutorRevistaArtigo().InserirAtualizar(this);
        }

        public Boolean InserirAtualizarComArtigo(long seqArtigo)
        {
            return new Hcrp.Framework.Dal.AutorRevistaArtigo().InserirAtualizarComArtigo(this, seqArtigo);       
        }
        
    }
}
