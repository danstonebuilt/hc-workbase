using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class PesquisaSatisfacaoCrhQualidade
    {
        public ETipoQualidade TipoQualidade { get; set; }
        public ERepostasPossiveis RapidezAtendimento { get; set; }
        public ERepostasPossiveis AtendimentoCordial { get; set; }
        public ERepostasPossiveis CapacidadeResolverProblemas { get; set; }
        public ERepostasPossiveis ConhecimentoTecnico { get; set; }
        public ERepostasPossiveis ClarezaExplicacao { get; set; }
        public ERepostasPossiveis ConfiabilidadeInformacoes { get; set; }

        public enum ERepostasPossiveis
        { 
            MuitoBom = 4,
            Bom = 3,
            Regular = 2,
            Ruim = 1,
            MuitoRuim = 0,
            NaoUtilizei = 5
        }
        public enum ETipoQualidade
        { 
            ProcessoSeletivo = 0,
            ProcessoAdmissao = 1,
            Pagamento = 2,
            Ferias = 3,
            CentralBenfcios = 4,
            Frequencia = 5
        }
        public PesquisaSatisfacaoCrhQualidade() { }

        public bool Gravar(long seqPesquisaCrh)
        {
            return new Hcrp.Framework.Dal.PesquisaSatisfacaoCrhQualidade().Gravar(this, seqPesquisaCrh);
        }

    }
}
