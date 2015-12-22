using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Hcrp.Framework.Classes
{
    [Serializable]
    public class Revista
    {
        public int SeqRevista { get; set; }
        public string Nome { get; set; }
        public string UrlImagemTopo { get; set; }
        public string Apresentacao { get; set; }
        public string CorpoEditorial { get; set; }
        public string Missao { get; set; }
        public string InstrucoesSubmissao { get; set; }
        public string RegrasSubmissao { get; set; }
        public string ChecklistSubmissao { get; set; }
        public int QtdMinimaRevisores { get; set; }
        public int QtdMaximaRevisores { get; set; }
        public int QtdMaxDiasAceite { get; set; }
        public EOpcaoAceite OpcaoPadraoAceite { get; set; }
        public int QtdMaxDiasRevisao { get; set; }
        public EOpcaoRevisao OpcaoPadraoRevisao { get; set; }
        public IList<BannerRevista> Banners { get; set; }
        public IList<RevistaEdicao> Edicoes { get; set; }
        public int QtdMaxDiasRevisaoOrtografica { get; set; }
        public int QtdDiasAlertaRevisor { get; set; }
        public DateTime DataInicioPublicacoes { get; set; }
        public DateTime DataFinalPublicacoes { get; set; }

        public enum EOpcaoAceite
        {
            Aceita = 1,
            Rejeita = 2
        }

        public enum EOpcaoRevisao
        {
            Conclui = 1,
            Rejeita = 2
        }

        public Revista() 
        {
        
        }

        public long Atualizar(int SeqRevista)
        {
            return new Hcrp.Framework.Dal.Revista().Atualizar(this, SeqRevista);
        }

        ///Métodos
        public Revista BuscarRevista()
        {            
            return new Hcrp.Framework.Dal.Revista().BuscarRevistaCodigo(new Hcrp.Framework.Classes.ConfiguracaoSistema().RevistaSite);
        }

    }
}
