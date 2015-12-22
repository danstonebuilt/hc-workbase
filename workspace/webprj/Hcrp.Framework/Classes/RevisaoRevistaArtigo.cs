using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    [Serializable]
    public class RevisaoRevistaArtigo
    {
        public int NumRevisao { get; set; }
        public int NumUserRevisor { get; set; }
        public int NumUserParecerFinal { get; set; }
        public EAvaliacaoRevisao Avaliacao { get; set; }
        public string ParecerAutor { get; set; }
        public string ParecerTriador { get; set; }
        public string ParecerCompleto { get; set; }
        public string ParecerFormatadoAutor { get; set; }
        public string ParecerFinal { get; set; }
        public DateTime DataParecerFinal { get; set; }
        public int NumItemParecerFinal { get; set; }
        public DateTime DataConvite { get; set; }
        public EAceiteRevisao Aceite { get; set; }
        public DateTime DataAceite { get; set; }
        public DateTime DataFinalizacao { get; set; }
        public string IdfAvaliacao { get; set; }
        public string IdfAvaliacaoFinal { get; set; }
        public string IdfAceite { get; set; }

        public enum EAvaliacaoRevisao
        {
            Rejeitado = 0,
            AprovadoRestricoes = 1,
            Aprovado = 2
        }

        public enum EAceiteRevisao
        {
            Rejeitou = 0,
            Aceitou = 1
        }

        public RevisaoRevistaArtigo()
        { }

        public long SetAceite(int NumArtigo, string Aceite)
        {
            return new Hcrp.Framework.Dal.RevisaoRevistaArtigo().SetAceite(this, NumArtigo, Aceite);
        }
        public long SetParecer(int NumArtigo, int Parecer)
        {
            return new Hcrp.Framework.Dal.RevisaoRevistaArtigo().SetParecer(this, NumArtigo, Parecer);
        }
        public long SetParecerFinal(int NumArtigo, int Parecer)
        {
            return new Hcrp.Framework.Dal.RevisaoRevistaArtigo().SetParecerFinal(this, NumArtigo, Parecer);
        }
        public Boolean InserirUsuarioRevisao(int NumArtigo)
        {
            return new Hcrp.Framework.Dal.RevisaoRevistaArtigo().InserirUsuarioRevisao(this, NumArtigo);
        }
    }
}
