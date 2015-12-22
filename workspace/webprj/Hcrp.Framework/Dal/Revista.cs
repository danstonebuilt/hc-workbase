using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class Revista : Hcrp.Framework.Classes.Revista
    {
        public Revista BuscarRevistaCodigo(Int32 seqRevista)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT SEQ_REVISTA, NOM_REVISTA, URL_IMAGEM_TOPO, DSC_APRESENTACAO, " + Environment.NewLine);
                    sb.Append(" DSC_CORPO_EDITORIAL, DSC_MISSAO, DSC_REGRAS_SUBMISSAO, DSC_INSTRUCOES_SUBMISSAO, " + Environment.NewLine);
                    sb.Append(" DSC_CHECKLIST_SUBMISSAO, NUM_MIN_REVISORES, NUM_MAX_REVISORES, NUM_MAX_DIAS_ACEITE, " + Environment.NewLine);
                    sb.Append(" IDF_OP_ACEITE, NUM_MAX_DIAS_REVISAO, IDF_OP_REVISAO, " + Environment.NewLine);
                    sb.Append(" NUM_MAX_VALIDACAO_ORTOGR, NUM_DIAS_ALERTA_REVISOR, DTA_INI_INSCRICAO, DTA_FIM_INSCRICAO " + Environment.NewLine);
                    sb.Append(" FROM REVISTA " + Environment.NewLine);
                    sb.Append(" WHERE SEQ_REVISTA = :SEQ_REVISTA " + Environment.NewLine);
                    sb.Append(" ORDER BY NOM_REVISTA " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_REVISTA"] = seqRevista;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        this.SeqRevista = Convert.ToInt32(dr["SEQ_REVISTA"]);
                        this.Nome = Convert.ToString(dr["NOM_REVISTA"]);
                        this.UrlImagemTopo = Convert.ToString(dr["URL_IMAGEM_TOPO"]);
                        this.Apresentacao = Convert.ToString(dr["DSC_APRESENTACAO"]);
                        this.CorpoEditorial = Convert.ToString(dr["DSC_CORPO_EDITORIAL"]);
                        this.Missao = Convert.ToString(dr["DSC_MISSAO"]);
                        this.InstrucoesSubmissao = Convert.ToString(dr["DSC_INSTRUCOES_SUBMISSAO"]);
                        this.RegrasSubmissao = Convert.ToString(dr["DSC_REGRAS_SUBMISSAO"]);
                        this.ChecklistSubmissao = Convert.ToString(dr["DSC_CHECKLIST_SUBMISSAO"]);
                        this.QtdMinimaRevisores = Convert.ToInt32(dr["NUM_MIN_REVISORES"]);
                        this.QtdMaximaRevisores = Convert.ToInt32(dr["NUM_MAX_REVISORES"]);
                        this.QtdMaxDiasAceite = Convert.ToInt32(dr["NUM_MAX_DIAS_ACEITE"]);
                        this.OpcaoPadraoAceite = (EOpcaoAceite)Convert.ToInt32(dr["IDF_OP_ACEITE"]);
                        this.QtdMaxDiasRevisao = Convert.ToInt32(dr["NUM_MAX_DIAS_REVISAO"]);
                        this.OpcaoPadraoRevisao = (EOpcaoRevisao)Convert.ToInt32(dr["IDF_OP_REVISAO"]);
                        this.QtdMaxDiasRevisaoOrtografica = Convert.ToInt32(dr["NUM_MAX_VALIDACAO_ORTOGR"]); 
                        this.QtdDiasAlertaRevisor = Convert.ToInt32(dr["NUM_DIAS_ALERTA_REVISOR"]);
                        this.DataInicioPublicacoes = Convert.ToDateTime(dr["DTA_INI_INSCRICAO"]);
                        this.DataFinalPublicacoes = Convert.ToDateTime(dr["DTA_FIM_INSCRICAO"]);
                    }
                }
                return this;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long Atualizar(Hcrp.Framework.Classes.Revista Revista, int SeqRevista)
        {
            try
            {
                long retorno = 0;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REVISTA");
                    //comando.Params["SEQ_REVISTA_EDICAO"] = Artigo.Edicao.SeqEdicao;
                    if (!String.IsNullOrWhiteSpace(Revista.Nome))
                        { comando.Params["NOM_REVISTA"] = Revista.Nome; }
                    if (!String.IsNullOrWhiteSpace(Revista.UrlImagemTopo))
                        { comando.Params["URL_IMAGEM_TOPO"] = Revista.UrlImagemTopo; }
                    if (!String.IsNullOrWhiteSpace(Revista.Apresentacao))
                        { comando.Params["DSC_APRESENTACAO"] = Revista.Apresentacao; }
                    if (!String.IsNullOrWhiteSpace(Revista.CorpoEditorial))
                        { comando.Params["DSC_CORPO_EDITORIAL"] = Revista.CorpoEditorial; }
                    if (!String.IsNullOrWhiteSpace(Revista.Missao))
                        { comando.Params["DSC_MISSAO"] = Revista.Missao; }
                    if (!String.IsNullOrWhiteSpace(Revista.InstrucoesSubmissao))
                        { comando.Params["DSC_INSTRUCOES_SUBMISSAO"] = Revista.InstrucoesSubmissao; }
                    if (!String.IsNullOrWhiteSpace(Revista.RegrasSubmissao))
                        { comando.Params["DSC_REGRAS_SUBMISSAO"] = Revista.RegrasSubmissao; }
                    if (!String.IsNullOrWhiteSpace(Revista.ChecklistSubmissao))
                        { comando.Params["DSC_CHECKLIST_SUBMISSAO"] = Revista.ChecklistSubmissao; }

                        comando.Params["NUM_MIN_REVISORES"] = Revista.QtdMinimaRevisores; 

                        comando.Params["NUM_MAX_REVISORES"] = Revista.QtdMaximaRevisores; 

                        comando.Params["NUM_MAX_DIAS_ACEITE"] = Revista.QtdMaxDiasAceite; 

                        comando.Params["IDF_OP_ACEITE"] = (int)Revista.OpcaoPadraoAceite; 

                        comando.Params["NUM_MAX_DIAS_REVISAO"] = Revista.QtdMaxDiasRevisao; 

                        comando.Params["IDF_OP_REVISAO"] = (int)Revista.OpcaoPadraoRevisao;

                        comando.Params["NUM_MAX_VALIDACAO_ORTOGR"] = Revista.QtdMaxDiasRevisaoOrtografica;
                        comando.Params["NUM_DIAS_ALERTA_REVISOR"] = Revista.QtdDiasAlertaRevisor;
                        comando.Params["DTA_INI_INSCRICAO"] = Revista.DataInicioPublicacoes;
                        comando.Params["DTA_FIM_INSCRICAO"] = Revista.DataFinalPublicacoes; 


                    comando.FilterParams["SEQ_REVISTA"] = SeqRevista;

                    // Executar o insert
                    ctx.ExecuteUpdate(comando);

                    // Pegar o último ID
                    retorno = SeqRevista;
                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

    }
}
