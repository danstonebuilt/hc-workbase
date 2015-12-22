using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    [Serializable]
    public class ParecerRevistaArtigo
    {
        public int NumParecer { get; set; }
        public Usuario Parecerista { get; set; }
        public EAvaliacaoParecer Avaliacao { get; set; }
        public string ParecerAutor { get; set; }
        public string ParecerTriador { get; set; }
        public string ParecerFinal { get; set; }
        public DateTime DataParecer { get; set; }
        public string ParecerCompleto{ get; set; }
        public string ParecerFormatadoAutor{get; set;}
        public DateTime DataFinalizacao { get; set; }
        public enum EAvaliacaoParecer
        {
            Rejeitado = 0,
            AprovadoRestricoes = 1,
            Aprovado = 2
        }
        public ParecerRevistaArtigo()
        { }
        public Hcrp.Framework.Classes.RevisaoRevistaArtigo.EAvaliacaoRevisao Situacao { get; set; }
    }
}
