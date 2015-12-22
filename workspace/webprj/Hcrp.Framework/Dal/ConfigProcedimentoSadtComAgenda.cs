using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class ConfigProcedimentoSadtComAgenda : Hcrp.Framework.Classes.ConfigProcedimentoSadtComAgenda
    {
        public List<Hcrp.Framework.Classes.ConfigProcedimentoSadtComAgenda> BuscaConfiguracoesVigentes(int CodServicoSadt, int CodDir, string SglPais, string SglUf, string CodLocalidade, int CodInstSolicitante)
        {
            List<Hcrp.Framework.Classes.ConfigProcedimentoSadtComAgenda> l = new List<Hcrp.Framework.Classes.ConfigProcedimentoSadtComAgenda>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.SEQ_CONF_PROC_AGENDA, A.COD_PROCEDIMENTO_HC, A.DTA_INICIO_VIGENCIA, A.DTA_FIM_VIGENCIA, " + Environment.NewLine);
                    sb.Append(" A.COD_DIR, A.SGL_PAIS, A.SGL_UF, A.COD_LOCALIDADE, A.COD_INST_SOLICITANTE, A.COD_ESPEC_TRIAGEM, " + Environment.NewLine);
                    sb.Append(" A.COD_INST_SISTEMA_TRI, A.NOM_FORM_SOLICITACAO " + Environment.NewLine);
                    sb.Append(" FROM SADT_CONF_PROC_AGENDA A, PROCEDIMENTO_HC B " + Environment.NewLine);
                    sb.Append(" WHERE SYSDATE BETWEEN A.DTA_INICIO_VIGENCIA AND A.DTA_FIM_VIGENCIA " + Environment.NewLine);
                    sb.Append("   AND B.COD_SERVICO_SADT = :COD_SERVICO_SADT " + Environment.NewLine);
                    if (CodDir != -1)
                        sb.Append("   AND ((A.COD_DIR IS NULL) OR (A.COD_DIR = :COD_DIR)) " + Environment.NewLine);
                    if (SglPais != null)
                        sb.Append("   AND ((A.SGL_PAIS IS NULL) OR (A.SGL_PAIS = :SGL_PAIS)) " + Environment.NewLine);
                    if (SglUf != null)
                        sb.Append("   AND ((A.SGL_UF IS NULL) OR (A.SGL_UF = :SGL_UF)) " + Environment.NewLine);
                    if (CodLocalidade != null)
                        sb.Append("   AND ((A.COD_LOCALIDADE IS NULL) OR (A.COD_LOCALIDADE = :COD_LOCALIDADE)) " + Environment.NewLine);
                    if (CodInstSolicitante != -1)
                        sb.Append("   AND ((A.COD_INST_SOLICITANTE IS NULL) OR (A.COD_INST_SOLICITANTE = :COD_INST_SOLICITANTE)) " + Environment.NewLine);                    
                    sb.Append("   AND A.COD_PROCEDIMENTO_HC = B.COD_PROCEDIMENTO_HC " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    if (CodServicoSadt != -1)
                        query.Params["COD_SERVICO_SADT"] = CodServicoSadt;
                    if (CodDir != -1 )
                        query.Params["COD_DIR"] = CodDir;
                    if (SglPais != null)
                        query.Params["SGL_PAIS"] = SglPais;
                    if (SglUf != null)
                        query.Params["SGL_UF"] = SglUf;
                    if (CodLocalidade != null)
                        query.Params["COD_LOCALIDADE"] = CodLocalidade;
                    if (CodInstSolicitante != -1)
                        query.Params["COD_INST_SOLICITANTE"] = CodInstSolicitante;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.ConfigProcedimentoSadtComAgenda c = new Hcrp.Framework.Classes.ConfigProcedimentoSadtComAgenda();
                        c.Seq = Convert.ToInt32(dr["SEQ_CONF_PROC_AGENDA"]);
                        if (dr["COD_PROCEDIMENTO_HC"] != DBNull.Value)
                            c._CodProcedimento = Convert.ToInt32(dr["COD_PROCEDIMENTO_HC"]);
                        if (dr["COD_DIR"] != DBNull.Value)
                            c._CodDir = Convert.ToInt32(dr["COD_DIR"]);
                        c._SglPais = Convert.ToString(dr["SGL_PAIS"]);
                        c._SglUf = Convert.ToString(dr["SGL_UF"]);
                        c._CodLocalidade = Convert.ToString(dr["COD_LOCALIDADE"]);
                        if (dr["COD_INST_SOLICITANTE"] != DBNull.Value)
                            c._CodInstSolicitante = Convert.ToInt32(dr["COD_INST_SOLICITANTE"]);
                        if (dr["COD_ESPEC_TRIAGEM"] != DBNull.Value)
                            c._CodEspecTriagem = Convert.ToInt32(dr["COD_ESPEC_TRIAGEM"]);
                        if (dr["COD_INST_SISTEMA_TRI"] != DBNull.Value)
                            c._CodInstSistemaTri = Convert.ToInt32(dr["COD_INST_SISTEMA_TRI"]);
                        c.DataInicioVigencia = Convert.ToDateTime(dr["DTA_INICIO_VIGENCIA"]);
                        c.DataFinalVigencia = Convert.ToDateTime(dr["DTA_FIM_VIGENCIA"]);
                        c.FormularioSolicitacao = Convert.ToString(dr["NOM_FORM_SOLICITACAO"]);
                        l.Add(c);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }       
        }
        public string BuscaFormularioSolicitacao(int seq)
        {
            string retorno = "";
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT NOM_FORM_SOLICITACAO " + Environment.NewLine);
                    sb.Append(" FROM SADT_CONF_PROC_AGENDA " + Environment.NewLine);
                    sb.Append(" WHERE COD_PROCEDIMENTO_HC = :COD_PROCEDIMENTO_HC " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query.Params["COD_PROCEDIMENTO_HC"] = seq;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        retorno = Convert.ToString(dr["NOM_FORM_SOLICITACAO"]);
                    }
                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }               
        }
        public Hcrp.Framework.Classes.ConfigProcedimentoSadtComAgenda BuscaConfigCodigo(int cod)
        {
            Hcrp.Framework.Classes.ConfigProcedimentoSadtComAgenda c = new Hcrp.Framework.Classes.ConfigProcedimentoSadtComAgenda();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.SEQ_CONF_PROC_AGENDA, A.COD_PROCEDIMENTO_HC, A.DTA_INICIO_VIGENCIA, A.DTA_FIM_VIGENCIA, " + Environment.NewLine);
                    sb.Append(" A.COD_DIR, A.SGL_PAIS, A.SGL_UF, A.COD_LOCALIDADE, A.COD_INST_SOLICITANTE, A.COD_ESPEC_TRIAGEM, " + Environment.NewLine);
                    sb.Append(" A.COD_INST_SISTEMA_TRI, A.NOM_FORM_SOLICITACAO, A.NOM_FORM_VISUALIZACAO, A.IDF_SEXO, A.VLR_IDADE_INICIAL, A.VLR_IDADE_FINAL " + Environment.NewLine);
                    sb.Append(" FROM SADT_CONF_PROC_AGENDA A " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_CONF_PROC_AGENDA = :SEQ_CONF_PROC_AGENDA " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_CONF_PROC_AGENDA"] = cod;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        c.Seq = Convert.ToInt32(dr["SEQ_CONF_PROC_AGENDA"]);
                        if (dr["COD_PROCEDIMENTO_HC"] != DBNull.Value)
                            c._CodProcedimento = Convert.ToInt32(dr["COD_PROCEDIMENTO_HC"]);
                        if (dr["COD_DIR"] != DBNull.Value)
                            c._CodDir = Convert.ToInt32(dr["COD_DIR"]);
                        c._SglPais = Convert.ToString(dr["SGL_PAIS"]);
                        c._SglUf = Convert.ToString(dr["SGL_UF"]);
                        c._CodLocalidade = Convert.ToString(dr["COD_LOCALIDADE"]);
                        if (dr["COD_INST_SOLICITANTE"] != DBNull.Value)
                            c._CodInstSolicitante = Convert.ToInt32(dr["COD_INST_SOLICITANTE"]);
                        if (dr["COD_ESPEC_TRIAGEM"] != DBNull.Value)
                            c._CodEspecTriagem = Convert.ToInt32(dr["COD_ESPEC_TRIAGEM"]);
                        if (dr["COD_INST_SISTEMA_TRI"] != DBNull.Value)
                            c._CodInstSistemaTri = Convert.ToInt32(dr["COD_INST_SISTEMA_TRI"]);
                        c.DataInicioVigencia = Convert.ToDateTime(dr["DTA_INICIO_VIGENCIA"]);
                        c.DataFinalVigencia = Convert.ToDateTime(dr["DTA_FIM_VIGENCIA"]);
                        c.FormularioSolicitacao = Convert.ToString(dr["NOM_FORM_SOLICITACAO"]);

                        c.IdfSexo = Convert.ToString(dr["IDF_SEXO"]);

                        if (dr["VLR_IDADE_INICIAL"] != DBNull.Value)
                            c.ValorIdadeInicial = Convert.ToInt32(dr["VLR_IDADE_INICIAL"]);

                        if (dr["VLR_IDADE_FINAL"] != DBNull.Value)
                            c.ValorIdadeFinal = Convert.ToInt32(dr["VLR_IDADE_FINAL"]);

                        if (dr["NOM_FORM_VISUALIZACAO"] != DBNull.Value)
                            c.FormularioVisualizacao = Convert.ToString(dr["NOM_FORM_VISUALIZACAO"]);
                    }
                }
                return c;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
