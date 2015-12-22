using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    [Serializable]
    public class RevistaEdicao
    {
        public int SeqEdicao { get; set; }
        public string Numero { get; set; }
        public int Ano { get; set; }
        public string Nome { get; set; }
        public DateTime DataPublicacao { get; set; }
        public string ImagemCapa { get; set; }
        public List<RevistaArtigo> Artigos {
            get 
            {
                return new Hcrp.Framework.Classes.RevistaArtigo().BuscarPorEdicao(this);
            }        
        }

        public RevistaEdicao()
        { }

        public long InserirAtualizar(int SeqRevista)
        {
            return new Hcrp.Framework.Dal.RevistaEdicao().InserirAtualizar(this, SeqRevista);
        }

        public Hcrp.Framework.Classes.RevistaEdicao BuscarEdicaoCodigo(int seq)
        {
            return new Hcrp.Framework.Dal.RevistaEdicao().BuscarEdicaoCodigo(seq);       
        }
    }
}
