using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class ParecerRevistaArtigo : Hcrp.Framework.Classes.ParecerRevistaArtigo
    {
        public List<Hcrp.Framework.Classes.ParecerRevistaArtigo> BuscarPareceres(int NumArtigo)
        {
            List<Hcrp.Framework.Classes.ParecerRevistaArtigo> l = new List<Hcrp.Framework.Classes.ParecerRevistaArtigo>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style1\">Enviado por: '||U.NOM_USUARIO||' '||U.SBN_USUARIO||' - '||C.DSC_EMAIL||'</span><br>'|| " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style3\">AO AUTOR:</span><br />'|| " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style4\">'||A.DSC_PARECER_AUTOR||'</span>' " + Environment.NewLine);
                    sb.Append("  PARECER, " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style1\">Enviado por: '||U.NOM_USUARIO||' '||U.SBN_USUARIO||' - '||C.DSC_EMAIL||'</span><br>'|| " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style3\">AO AUTOR:</span><br />'|| " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style4\">'||A.DSC_PARECER_AUTOR||'</span><br />'|| " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style5\"><strong>AO TRIADOR:</strong></span><br />'|| " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style6\">'||A.DSC_PARECER_TRIADOR||' <br />' || " + Environment.NewLine);
                    sb.Append(" 'Avaliação: ' || DECODE(A.IDF_AVALIACAO,'0','Rejeitado','1','Aprovado com Restrições','2','Aprovado','') || '</span>' PARECER_COMPLETO, " + Environment.NewLine);
                    sb.Append(" A.DTA_FINALIZACAO, A.DSC_PARECER_TRIADOR, A.DSC_PARECER_AUTOR, A.IDF_AVALIACAO " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_ARTIGO_REVISAO A, USUARIO U, COMPLEMENTO_USUARIO C " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_REVISTA_ARTIGO = :SEQ_REVISTA_ARTIGO " + Environment.NewLine);
                    sb.Append(" AND A.NUM_USER_REVISAO = U.NUM_USER_BANCO " + Environment.NewLine);
                    sb.Append(" AND U.NUM_USER_BANCO = C.NUM_USER_BANCO(+) " + Environment.NewLine);
                    sb.Append(" AND A.DTA_FINALIZACAO IS NOT NULL " + Environment.NewLine);
                    sb.Append(" ORDER BY A.DTA_FINALIZACAO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_REVISTA_ARTIGO"] = NumArtigo;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.ParecerRevistaArtigo dra = new Hcrp.Framework.Classes.ParecerRevistaArtigo();
//                        if (string.IsNullOrEmpty(Convert.ToString(dr["DSC_PARECER_TRIADOR"])))
//                        {
//                            dra.ParecerFormatadoAutor = "";
//                            dra.ParecerCompleto = "";
//                        }
//                        else
 //                       {
                            dra.ParecerFormatadoAutor = Convert.ToString(dr["PARECER"]);
                            dra.ParecerCompleto = Convert.ToString(dr["PARECER_COMPLETO"]);
//                        }
                        dra.DataFinalizacao = Convert.ToDateTime(dr["DTA_FINALIZACAO"]);
                        if (!string.IsNullOrEmpty(Convert.ToString(dr["IDF_AVALIACAO"])))
                            dra.Situacao = (Hcrp.Framework.Classes.RevisaoRevistaArtigo.EAvaliacaoRevisao)Convert.ToInt32(dr["IDF_AVALIACAO"]);
                        l.Add(dra);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Hcrp.Framework.Classes.ParecerRevistaArtigo> BuscarParecerFinal(int NumArtigo, Boolean comParecerista = false)
        {
            List<Hcrp.Framework.Classes.ParecerRevistaArtigo> l = new List<Hcrp.Framework.Classes.ParecerRevistaArtigo>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT " + Environment.NewLine);
                    if (comParecerista)
                        sb.Append(" '<span class=\"parecer_style1\">Enviado por: '||U.NOM_USUARIO||' '||U.SBN_USUARIO||' - '||C.DSC_EMAIL||'</span><span class=\"parecer_style2\"><br>'|| " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style3\">PARECER FINAL:</span><br />'|| " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style4\">'||A.DSC_PARECER||'<br />' " + Environment.NewLine);
                    sb.Append(" || 'Avaliação: ' || DECODE(A.IDF_AVALIACAO,'0','Rejeitado','1','Aprovado com Restrições','2','Aprovado','') || '</span><br />' PARECER " + Environment.NewLine);
                    sb.Append(" ,A.DTA_PARECER " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_PARECER_FINAL A, USUARIO U, COMPLEMENTO_USUARIO C " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_REVISTA_ARTIGO = :SEQ_REVISTA_ARTIGO " + Environment.NewLine);
                    sb.Append(" AND A.NUM_USER_BANCO = U.NUM_USER_BANCO " + Environment.NewLine);
                    sb.Append(" AND U.NUM_USER_BANCO = C.NUM_USER_BANCO " + Environment.NewLine);
                    sb.Append(" AND A.DTA_PARECER IS NOT NULL " + Environment.NewLine);
//                    sb.Append(" AND A.DSC_PARECER IS NOT NULL " + Environment.NewLine);
                    sb.Append(" ORDER BY A.DTA_PARECER " + Environment.NewLine);
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query2 = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query2.Params["NumArtigo"] = NumArtigo;
                    ctx.ExecuteQuery(query2);
                    // Cria objeto de material
                    OracleDataReader dr2 = ctx.Reader as OracleDataReader;
                    while (dr2.Read())
                    {
                        Hcrp.Framework.Classes.ParecerRevistaArtigo dra = new Hcrp.Framework.Classes.ParecerRevistaArtigo();
                        dra.ParecerFinal = Convert.ToString(dr2["PARECER"]);
                        dra.DataFinalizacao = Convert.ToDateTime(dr2["DTA_PARECER"]);
                        l.Add(dra);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
