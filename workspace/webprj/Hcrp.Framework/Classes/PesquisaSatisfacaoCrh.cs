using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class PesquisaSatisfacaoCrh
    {
        public int seq { get; set; }
        public DateTime DataParticipacao { get; set; }
        public EClasseProfissional ClasseProfissional { get; set; }
        public int AnosServico { get; set; }
        public int MesesServico { get; set; }
        public bool CargoConfianca { get; set; }
        public string DescricaoCargoConfianca { get; set; }

        public EClasseProfissionalProfissao ClasseProfissionalProfissao { get; set; }
        public string DescricaoDemaisProfissoes { get; set; }
        
        public ETipoUtilizacaoCRH UtilizacaoCrh { get; set; }
        public bool ManualServidor { get; set; }
        public ERepostasPossiveis AtendimentoPessoal { get; set; }
        public ERepostasPossiveis AtendimentoFone { get; set; }
        public ERepostasPossiveis AtendimentoEmail { get; set; }
        public EConsultaInfTecnica ConsultaInformacoesTecnicas { get; set; }
        public EPrazoAtendimento PrazoAtendimento { get; set; }
        public bool ParticipouCursos { get; set; }
        public ESobreCursos SobreCursos { get; set; }
        public ERepostasPossiveis ReconhecimentoTalentos { get; set; }
        public ERepostasPossiveis AtracaoTalentos { get; set; }
        public ERepostasPossiveis OportunidadeCrescimento { get; set; }
        public ERepostasPossiveis QualidadeVida { get; set; }
        public ERepostasPossiveis AvaliacaoFinal { get; set; }
        public string ElogioCritica { get; set; }

        public List<PesquisaSatisfacaoCrhQualidade> PerguntasQualidade { get; set; }

        public enum EClasseProfissional
        { 
            Basico = 0,
            Medio = 1,
            Universitario = 2
        }
        public enum EClasseProfissionalProfissao
        { 
            Enfermagem = 0,
            Médica = 1,
            DemaisFuncoes = 2
        }
        public enum ETipoUtilizacaoCRH
        { 
            SomenteContratacao = 0,
            ContratacaoTreinamento = 1,
            VariasVezes = 2
        }
        public enum ERepostasPossiveis
        {
            MuitoBom = 4,
            Bom = 3,
            Regular = 2,
            Ruim = 1,
            MuitoRuim = 0,
            NaoUtilizei = 5
        }
        public enum EConsultaInfTecnica
        {
            ResponderamAdequadamente = 0,
            ResponderamParcialmente = 1,
            NaoResponderam = 2,
            ResponderamDuvidasAuxiliaramResolveProblemas = 3,
            NaoUtilizei = 4
        }
        public enum EPrazoAtendimento
        {
            DocumentoEntreguePrazo = 0,
            AtrasoEntregaDocumento = 1,
            DemoraEntregaComPrejuizo = 2,
            NuncaSoliciteiDocumentoCRH = 3
        }
        public enum ESobreCursos
        { 
            AtenderamExpectativasConteudoAplicavel = 0,
            AtenderamExpectativasConteudoPacialmenteAplicavel = 1,
            AtenderamExpectativasConteudoNaoAplicavel = 2,
            NaoAtenderamExpectativasConteudoNaoAplicavel = 3,            
            CursoConteudoSuperaramExpectativas = 4,
            AreaFuncaoNaoSaoRealizadosCursos = 5        
        }

        public PesquisaSatisfacaoCrh(){ }

        public bool Gravar()
        {
            return new Hcrp.Framework.Dal.PesquisaSatisfacaoCrh().Gravar(this);
        }
    }
}
