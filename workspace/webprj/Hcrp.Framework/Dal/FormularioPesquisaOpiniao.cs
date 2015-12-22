using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class FormularioPesquisaOpiniao
    {
        public Hcrp.Framework.Classes.FormularioPesquisaOpiniao BuscarDadosForm(Hcrp.Framework.Classes.FormularioPesquisaOpiniao.ETipoFormulario Tipo)
        {            
            try
            {
                Hcrp.Framework.Classes.FormularioPesquisaOpiniao formPesquisa = new Hcrp.Framework.Classes.FormularioPesquisaOpiniao();
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT SEQ_FORM_PESQUISA, DTA_HOR_INI, DTA_HOR_FIM, DSC_PESQUISA, DSC_URL, DSC_SQL_EXPORTACAO " + Environment.NewLine);
                    sb.Append(" FROM GENERICO.FORM_PESQUISA " + Environment.NewLine);
                    sb.Append(" WHERE SEQ_FORM_PESQUISA = :SEQ_FORM_PESQUISA " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_FORM_PESQUISA"] = (int)Tipo;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        formPesquisa.Seq = Convert.ToInt32(dr["SEQ_FORM_PESQUISA"]);
                        formPesquisa.DataInicio = Convert.ToDateTime(dr["DTA_HOR_INI"]);
                        formPesquisa.DataFim = Convert.ToDateTime(dr["DTA_HOR_FIM"]);
                        formPesquisa.Descricao = Convert.ToString(dr["DSC_PESQUISA"]);
                        formPesquisa.Url = Convert.ToString(dr["DSC_URL"]);
                        formPesquisa.SqlExportacao =  Convert.ToString(dr["DSC_SQL_EXPORTACAO"]);
                    }
                   return formPesquisa;
                }
            }
            catch (Exception)
            {
                throw;
            }            
        }
        public List<Hcrp.Framework.Classes.FormularioPesquisaOpiniao> BuscarFormularios()
        {
            try
            {
                List<Hcrp.Framework.Classes.FormularioPesquisaOpiniao> l = new List<Hcrp.Framework.Classes.FormularioPesquisaOpiniao>();                
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT SEQ_FORM_PESQUISA, DTA_HOR_INI, DTA_HOR_FIM, DSC_PESQUISA, DSC_URL, DSC_SQL_EXPORTACAO " + Environment.NewLine);
                    sb.Append(" FROM GENERICO.FORM_PESQUISA " + Environment.NewLine);
                    sb.Append(" ORDER BY DSC_PESQUISA " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());                    

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.FormularioPesquisaOpiniao formPesquisa = new Hcrp.Framework.Classes.FormularioPesquisaOpiniao();
                        formPesquisa.Seq = Convert.ToInt32(dr["SEQ_FORM_PESQUISA"]);
                        formPesquisa.DataInicio = Convert.ToDateTime(dr["DTA_HOR_INI"]);
                        formPesquisa.DataFim = Convert.ToDateTime(dr["DTA_HOR_FIM"]);
                        formPesquisa.Descricao = Convert.ToString(dr["DSC_PESQUISA"]);
                        formPesquisa.Url = Convert.ToString(dr["DSC_URL"]);
                        formPesquisa.SqlExportacao = Convert.ToString(dr["DSC_SQL_EXPORTACAO"]);
                        l.Add(formPesquisa);
                    }
                    return l;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
