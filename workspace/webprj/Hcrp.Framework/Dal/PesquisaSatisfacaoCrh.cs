using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Dal
{
    public class PesquisaSatisfacaoCrh
    {
        public bool Gravar(Hcrp.Framework.Classes.PesquisaSatisfacaoCrh p)
        {
            try
            {
                long Seq = 0;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {                        
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("GENERICO.CRH_PESQUISA_SATISFACAO");                        
                    comando.Params["SEQ_FORM_PESQUISA"] = 1;
                    comando.Params["DTA_HOR_PARTICIPACAO"] = p.DataParticipacao;
                    comando.Params["IDF_CLASSE_PROFISSIONAL"] = (int)p.ClasseProfissional;
                    comando.Params["QTD_ANOS_SERVICO"] = p.AnosServico;
                    comando.Params["QTD_MESES_SERVICO"] = p.MesesServico;
                    if (p.CargoConfianca)
                        comando.Params["IDF_CARGO_CONFIANCA"] = "S";
                    else comando.Params["IDF_CARGO_CONFIANCA"] = "N";
                    if (!String.IsNullOrWhiteSpace(p.DescricaoCargoConfianca))
                        comando.Params["DSC_CARGO_CONFIANCA"] = p.DescricaoCargoConfianca;
                    comando.Params["IDF_PROFISSAO"] = (int)p.ClasseProfissionalProfissao;
                    if (!String.IsNullOrWhiteSpace(p.DescricaoDemaisProfissoes))
                        comando.Params["DSC_PROFISSAO"] = p.DescricaoDemaisProfissoes;
                    comando.Params["IDF_UTILIZACAO_CRH"] = (int)p.UtilizacaoCrh;
                    if (p.ManualServidor)
                        comando.Params["IDF_TEM_MANUAL_SERVIDOR"] = "S";
                    else comando.Params["IDF_TEM_MANUAL_SERVIDOR"] = "N";
                    comando.Params["IDF_RH_ATEND_PESSOAL"] = (int)p.AtendimentoPessoal;
                    comando.Params["IDF_RH_ATEND_FONE"] = (int)p.AtendimentoFone;
                    comando.Params["IDF_RH_ATEND_EMAIL"] = (int)p.AtendimentoEmail;
                    comando.Params["IDF_CONSULTA_INF_TECNICA"] = (int)p.ConsultaInformacoesTecnicas;
                    comando.Params["IDF_PRAZO_ATENDIMENTO"] = (int)p.PrazoAtendimento;
                    if (p.ParticipouCursos)
                        comando.Params["IDF_PARTICIPOU_CURSO"] = "S";
                    else comando.Params["IDF_PARTICIPOU_CURSO"] = "N";
                    comando.Params["IDF_SOBRE_CURSOS"] = (int)p.SobreCursos;
                    comando.Params["IDF_RECONHEC_TALENTOS"] = (int)p.ReconhecimentoTalentos;
                    comando.Params["IDF_ATRACAO_TALENTOS"] = (int)p.AtracaoTalentos;
                    comando.Params["IDF_OPORTUNIDADE_CRESCIMENTO"] = (int)p.OportunidadeCrescimento;
                    comando.Params["IDF_QUALIDADE_VIDA"] = (int)p.QualidadeVida;
                    comando.Params["IDF_AVALIACAO_FINAL"] = (int)p.AvaliacaoFinal;
                    if (!String.IsNullOrWhiteSpace(p.ElogioCritica))
                        comando.Params["DSC_ELOGIO_CRITICA"] = p.ElogioCritica;
                        
                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                    // Pegar o último ID
                    Seq = ctx.GetSequenceValue("GENERICO.SEQ_CRH_PESQUISA_SATISFACAO", false);
                    
                    //Gravar os Items
                    foreach (var Item in p.PerguntasQualidade)
                    {
                        Item.Gravar(Seq);
                    }                                           

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }                
        }
    }
}
