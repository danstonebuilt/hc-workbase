using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class UnidadesInternacaoInstituto : Hcrp.Framework.Classes.UnidadesInternacaoInstituto
    {
        public List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto> BuscaUnidadesInternacaoInstituto(string CodInstituto)
        {

            List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto> p = new List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    //sb.Append("SELECT DISTINCT LOC.NUM_SEQ_LOCAL " + Environment.NewLine);
                    //sb.Append("      ,LOC.NOM_LOCAL " + Environment.NewLine);
                    //sb.Append("  FROM MAPEAMENTO_LOCAL LOC " + Environment.NewLine);
                    //sb.Append("      ,IDENTIFICACAO_MAP_LOCAL IML " + Environment.NewLine);
                    //sb.Append(" WHERE IML.NUM_ID_LOCAL = 11 " + Environment.NewLine);
                    //sb.Append("   AND LOC.COD_INSTITUTO = :COD_INSTITUTO " + Environment.NewLine);
                    //sb.Append("   AND LOC.NUM_SEQ_LOCAL = IML.NUM_SEQ_LOCAL " + Environment.NewLine);


                    sb.Append("SELECT DISTINCT NUM_SEQ_LOCAL_ENFERMARIA NUM_SEQ_LOCAL, " + Environment.NewLine);
                    sb.Append("      MLF.NOM_LOCAL " + Environment.NewLine);
                    sb.Append("FROM (SELECT  " + Environment.NewLine);
                    sb.Append("      NVL(FCN_NIVEL_ACIMA_ID_LOCAL(CL.NUM_SEQ_LOCAL, 5), " + Environment.NewLine);
                    sb.Append("      CL.NUM_SEQ_LOCAL) NUM_SEQ_LOCAL_ENFERMARIA " + Environment.NewLine);
                    sb.Append("FROM CONFIGURACAO_LEITO CL, MAPEAMENTO_LOCAL ML " + Environment.NewLine);
                    sb.Append("WHERE CL.NUM_SEQ_LOCAL IN " + Environment.NewLine);
                    sb.Append("      (SELECT NUM_SEQ_LOCAL " + Environment.NewLine);
                    sb.Append("      FROM LEITO_TIPO_ATENDIMENTO " + Environment.NewLine);
                    sb.Append("      WHERE NUM_SEQ_LOCAL = CL.NUM_SEQ_LOCAL " + Environment.NewLine);
                    sb.Append("      AND COD_TIPO_ATENDIMENTO IN (2, 5)) " + Environment.NewLine);
                    sb.Append("      AND CL.NUM_SEQ_LOCAL = ML.NUM_SEQ_LOCAL " + Environment.NewLine);
                    sb.Append("      AND ML.COD_INSTITUTO = :COD_INSTITUTO " + Environment.NewLine);
                    sb.Append("      AND ML.IDF_ATIVO = 'S' " + Environment.NewLine);
                    sb.Append("      AND CL.IDF_LEITO_EXTRA = 'N' " + Environment.NewLine);
                    sb.Append("UNION ALL " + Environment.NewLine);
                    sb.Append("SELECT  " + Environment.NewLine);
                    sb.Append("      NVL(FCN_NIVEL_ACIMA_ID_LOCAL(MP.NUM_SEQ_LOCAL, 5), " + Environment.NewLine);
                    sb.Append("      MP.NUM_SEQ_LOCAL) NUM_SEQ_LOCAL_ENFERMARIA                " + Environment.NewLine);
                    sb.Append("FROM PACIENTE_EM_ATENDIMENTO AP, MOVIMENTACAO_PACIENTE_ABERTA MP " + Environment.NewLine);
                    sb.Append("WHERE AP.COD_TIPO_ATENDIMENTO IN (2, 5) " + Environment.NewLine);
                    sb.Append("      AND AP.COD_INSTITUTO = :COD_INSTITUTO " + Environment.NewLine);
                    sb.Append("      AND AP.SEQ_ATENDIMENTO = MP.SEQ_ATENDIMENTO " + Environment.NewLine);
                    sb.Append("      AND EXISTS " + Environment.NewLine);
                    sb.Append("(SELECT LTC.NUM_SEQ_LOCAL " + Environment.NewLine);
                    sb.Append("      FROM LEITO_TIPO_CONVENIO LTC " + Environment.NewLine);
                    sb.Append("      WHERE LTC.NUM_SEQ_LOCAL = MP.NUM_SEQ_LOCAL " + Environment.NewLine);
                    sb.Append("      AND LTC.IDF_TIPO_CONVENIO in (0, 1, 2, 3, 4))) X, " + Environment.NewLine);
                    sb.Append("      MAPEAMENTO_LOCAL MLF, " + Environment.NewLine);
                    sb.Append("      TIPO_ATENDIMENTO_HC TA, " + Environment.NewLine);
                    sb.Append("      INSTITUTO I " + Environment.NewLine);
                    sb.Append(" WHERE X.NUM_SEQ_LOCAL_ENFERMARIA = MLF.NUM_SEQ_LOCAL " + Environment.NewLine);
                    sb.Append("      AND I.COD_INST_SISTEMA = :COD_INSTITUTO " + Environment.NewLine);
                    //sb.Append("GROUP BY MLF.DSC_ORDENACAO, " + Environment.NewLine);
                    //sb.Append("      NUM_SEQ_LOCAL_ENFERMARIA, " + Environment.NewLine);
                    //sb.Append("      NOM_LOCAL " + Environment.NewLine);
                    //sb.Append("ORDER BY NOM_LOCAL " + Environment.NewLine);


                    sb.Append(" UNION ALL " + Environment.NewLine);
                    sb.Append("SELECT DISTINCT PAI.NUM_SEQ_LOCAL " + Environment.NewLine);
                    sb.Append("      ,PAI.NOM_LOCAL  " + Environment.NewLine);
                    sb.Append("  FROM MAPEAMENTO_LOCAL LOC " + Environment.NewLine);
                    sb.Append("      ,MAPEAMENTO_LOCAL PAI " + Environment.NewLine);
                    sb.Append("      ,IDENTIFICACAO_MAP_LOCAL IML " + Environment.NewLine);
                    sb.Append(" WHERE IML.NUM_ID_LOCAL IN (28,29) " + Environment.NewLine);
                    sb.Append("   AND LOC.COD_INSTITUTO = :COD_INSTITUTO " + Environment.NewLine);
                    sb.Append("   AND LOC.NUM_SEQ_LOCAL_PAI = PAI.NUM_SEQ_LOCAL " + Environment.NewLine);
                    sb.Append("   AND LOC.NUM_SEQ_LOCAL = IML.NUM_SEQ_LOCAL " + Environment.NewLine);

                    if (CodInstituto.Equals("2"))
                    {
                        sb.Append(" UNION ALL " + Environment.NewLine); //UNIDADES DE ISOLAMENTO
                        sb.Append("SELECT DISTINCT LOC_ISO.NUM_SEQ_LOCAL " + Environment.NewLine);
                        sb.Append("      ,LOC_ISO.NOM_LOCAL " + Environment.NewLine);
                        sb.Append("  FROM MAPEAMENTO_LOCAL LOC_ISO " + Environment.NewLine);
                        sb.Append(" WHERE " + Environment.NewLine);
                        sb.Append("    LOC_ISO.NUM_SEQ_LOCAL IN (4831,5982,701) " + Environment.NewLine);
                    }

                    sb.Append(" ORDER BY NOM_LOCAL");

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_INSTITUTO"] = Convert.ToInt32(CodInstituto);

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.UnidadesInternacaoInstituto a = new Hcrp.Framework.Classes.UnidadesInternacaoInstituto();
                        a.Numero = Convert.ToString(dr["NUM_SEQ_LOCAL"]);
                        a.Nome = Convert.ToString(dr["NOM_LOCAL"]);
                        p.Add(a);
                    }
                }
                return p;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto> BuscaAgrupamentoUnidadesInternacaoInstituto(string CodInstituto)
        {

            List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto> p = new List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT ' ' NOM_AGRUPAMENTO, " + Environment.NewLine);
                    sb.Append("  -1 NUM_SEQ_LOCAL " + Environment.NewLine);
                    sb.Append("   FROM DUAL " + Environment.NewLine);
                    sb.Append(" UNION ALL " + Environment.NewLine);
                    sb.Append("SELECT NOM_AGRUPAMENTO, NUM_SEQ_LOCAL " + Environment.NewLine);
                    sb.Append("  FROM V_MAPEAMENTO_LOCAL_GRUPO " + Environment.NewLine);
                    sb.Append("   WHERE COD_INSTITUTO = :COD_INSTITUTO " + Environment.NewLine);
                    sb.Append(" ORDER BY NOM_AGRUPAMENTO");

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_INSTITUTO"] = Convert.ToInt32(CodInstituto);

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.UnidadesInternacaoInstituto a = new Hcrp.Framework.Classes.UnidadesInternacaoInstituto();
                        a.Numero = Convert.ToString(dr["NUM_SEQ_LOCAL"]);
                        a.Nome = Convert.ToString(dr["NOM_AGRUPAMENTO"]);
                        p.Add(a);
                    }
                }
                return p;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto> BuscaUnidades_Andar_Ala(string CodInstituto)
        {

            List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto> p = new List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT ROWNUM, A1.* FROM (SELECT X.ANDAR_LEITO " + Environment.NewLine);
                    sb.Append(" FROM ( " + Environment.NewLine);
                    sb.Append(" SELECT  " + Environment.NewLine);
                    sb.Append(" (LPAD(A.IDF_ANDAR, 2, '0') || ' ANDAR  -> ALA: '|| C.IDF_ALA) ANDAR_LEITO, " + Environment.NewLine);
                    sb.Append(" A.NUM_SEQ_LOCAL, A.IDF_ATIVO, A.SGL_LOCAL, A.NOM_LOCAL, " + Environment.NewLine);
                    sb.Append(" A.NUM_ID_LOCAL, B.DSC_ID_LOCAL,  C.IDF_ALA, " + Environment.NewLine);
                    sb.Append(" A.NUM_SEQ_LOCAL_PAI " + Environment.NewLine);
                    sb.Append(" FROM MAPEAMENTO_LOCAL A, " + Environment.NewLine);
                    sb.Append(" IDENTIFICACAO_LOCAL B, " + Environment.NewLine);
                    sb.Append(" CONFIGURACAO_LEITO C " + Environment.NewLine);
                    sb.Append(" WHERE A.COD_INSTITUTO = :COD_INSTITUTO " + Environment.NewLine);
                    sb.Append(" AND   A.NUM_ID_LOCAL = 1 " + Environment.NewLine);
                    sb.Append(" AND   A.IDF_ATIVO = 'S' " + Environment.NewLine);
                    sb.Append(" AND   B.NUM_ID_LOCAL = A.NUM_ID_LOCAL " + Environment.NewLine);
                    sb.Append(" AND   C.NUM_SEQ_LOCAL = A.NUM_SEQ_LOCAL       " + Environment.NewLine);
                    sb.Append(" ) X " + Environment.NewLine);
                    sb.Append(" GROUP BY X.ANDAR_LEITO ) A1" + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_INSTITUTO"] = Convert.ToInt32(CodInstituto);

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.UnidadesInternacaoInstituto a = new Hcrp.Framework.Classes.UnidadesInternacaoInstituto();
                        a.Numero = Convert.ToString(dr["ROWNUM"]);
                        a.Nome = Convert.ToString(dr["ANDAR_LEITO"]);
                        p.Add(a);
                    }
                }
                return p;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto> BuscaEnfermaria_Andar_Ala(string CodInstituto, string NomeAndar)
        {

            List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto> p = new List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT X.ANDAR_LEITO, X.NUM_SEQ_LOCAL_ENFERM " + Environment.NewLine);
                    sb.Append("     FROM ( " + Environment.NewLine);
                    sb.Append("       SELECT  " + Environment.NewLine);
                    sb.Append("         (LPAD(A.IDF_ANDAR, 2, '0') || ' ANDAR  -> ALA: '|| C.IDF_ALA) ANDAR_LEITO, " + Environment.NewLine);
                    sb.Append("         A.NUM_SEQ_LOCAL, A.IDF_ATIVO, A.SGL_LOCAL, A.NOM_LOCAL, " + Environment.NewLine);
                    sb.Append("         A.NUM_ID_LOCAL, B.DSC_ID_LOCAL,  C.IDF_ALA, " + Environment.NewLine);
                    sb.Append("         A.NUM_SEQ_LOCAL_PAI, " + Environment.NewLine);
                    
                    //sb.Append("         (SELECT MAX(ML.NUM_SEQ_LOCAL) NUM_SEQ_LOCAL_ENFERM " + Environment.NewLine);
                    //sb.Append("         FROM MAPEAMENTO_LOCAL ML " + Environment.NewLine);
                    //sb.Append("         WHERE ML.NUM_ID_LOCAL = 5 " + Environment.NewLine);
                    //sb.Append("         AND   ML.COD_INSTITUTO = A.COD_INSTITUTO " + Environment.NewLine);
                    //sb.Append("         AND   LPAD(ML.IDF_ANDAR, 2, '0') = LPAD(A.IDF_ANDAR, 2, '0') ) NUM_SEQ_LOCAL_ENFERM " + Environment.NewLine);

                    sb.Append("        NVL(FCN_NIVEL_ACIMA_ID_LOCAL(A.NUM_SEQ_LOCAL, 5), A.NUM_SEQ_LOCAL) NUM_SEQ_LOCAL_ENFERM " + Environment.NewLine);

                    sb.Append("       FROM MAPEAMENTO_LOCAL A, " + Environment.NewLine);
                    sb.Append("         IDENTIFICACAO_LOCAL B, " + Environment.NewLine);
                    sb.Append("         CONFIGURACAO_LEITO C " + Environment.NewLine);
                    sb.Append("       WHERE A.COD_INSTITUTO = :COD_INSTITUTO " + Environment.NewLine);
                    sb.Append("       AND   A.NUM_ID_LOCAL = 1 " + Environment.NewLine);
                    sb.Append("       AND   A.IDF_ATIVO = 'S' " + Environment.NewLine);
                    sb.Append("       AND   B.NUM_ID_LOCAL = A.NUM_ID_LOCAL " + Environment.NewLine);
                    sb.Append("       AND   C.NUM_SEQ_LOCAL = A.NUM_SEQ_LOCAL       " + Environment.NewLine);
                    sb.Append("   ) X " + Environment.NewLine);
                    sb.Append(string.Format(" WHERE X.ANDAR_LEITO = '{0}' ", NomeAndar.Trim().ToUpper()) + Environment.NewLine);
                    sb.Append(" GROUP BY X.ANDAR_LEITO, X.NUM_SEQ_LOCAL_ENFERM " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_INSTITUTO"] = Convert.ToInt32(CodInstituto);
                    //query.Params["NOM_ANDAR"] = "'" +  + "'";

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.UnidadesInternacaoInstituto a = new Hcrp.Framework.Classes.UnidadesInternacaoInstituto();
                        a.Numero = Convert.ToString(dr["NUM_SEQ_LOCAL_ENFERM"]);
                        a.Nome = Convert.ToString(dr["ANDAR_LEITO"]);
                        p.Add(a);
                    }
                }
                return p;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto> BuscaAgrupamentoVisualizacaoPainel(string CodInstituto, ref long NumSeqLocal_Enfermaria)
        {

            List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto> p = new List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.SEQ_VISUAL_PAINEL_VAGA, A.DSC_VISUAL_PAINEL_VAGA, A.NUM_SEQ_LOCAL_ENFERM " + Environment.NewLine);
                    sb.Append(" FROM GENERICO.V_VISUALIZACAO_PAINEL_VAGA A " + Environment.NewLine);
                    sb.Append(" WHERE A.COD_INSTITUTO = :COD_INSTITUTO " + Environment.NewLine);
                    sb.Append(" AND   A.IDF_ATIVO = 'S' " + Environment.NewLine);                    

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_INSTITUTO"] = Convert.ToInt32(CodInstituto);

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.UnidadesInternacaoInstituto a = new Hcrp.Framework.Classes.UnidadesInternacaoInstituto();
                        a.Numero = Convert.ToString(dr["SEQ_VISUAL_PAINEL_VAGA"]);
                        a.Nome = Convert.ToString(dr["DSC_VISUAL_PAINEL_VAGA"]);

                        NumSeqLocal_Enfermaria = Convert.ToInt64(dr["NUM_SEQ_LOCAL_ENFERM"].ToString());

                        p.Add(a);
                    }
                }
                return p;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
