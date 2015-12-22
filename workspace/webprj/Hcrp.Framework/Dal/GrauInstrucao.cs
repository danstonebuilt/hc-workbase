using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class GrauInstrucao : Hcrp.Framework.Classes.GrauInstrucao
    {
        public Hcrp.Framework.Classes.GrauInstrucao BuscaGrauInstrucaoCodigo(int codigo)
        {
            Hcrp.Framework.Classes.GrauInstrucao g = new Hcrp.Framework.Classes.GrauInstrucao();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.COD_GRAU_INSTRUCAO, A.IDF_ATIVO, A.DSC_GRAU_INSTRUCAO, A.COD_GRAU_INSTRUCAO_SUS " + Environment.NewLine);
                    sb.Append(" FROM GRAU_INSTRUCAO A " + Environment.NewLine);
                    sb.Append(" WHERE A.COD_GRAU_INSTRUCAO = :COD_GRAU_INSTRUCAO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_GRAU_INSTRUCAO"] = codigo;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        g.Codigo = Convert.ToInt32(dr["COD_GRAU_INSTRUCAO"]);
                        g.CodigoSus = Convert.ToString(dr["COD_GRAU_INSTRUCAO_SUS"]);
                        g.Ativo = Convert.ToString(dr["IDF_ATIVO"]) == "S";
                        g.Descricao = Convert.ToString(dr["DSC_GRAU_INSTRUCAO"]);
                    }
                }
                return g;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public List<Hcrp.Framework.Classes.GrauInstrucao> BuscaGrauInstrucao()
        {
            List<Hcrp.Framework.Classes.GrauInstrucao> l = new List<Hcrp.Framework.Classes.GrauInstrucao>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.COD_GRAU_INSTRUCAO, A.IDF_ATIVO, A.DSC_GRAU_INSTRUCAO, A.COD_GRAU_INSTRUCAO_SUS " + Environment.NewLine);
                    sb.Append(" FROM GRAU_INSTRUCAO A " + Environment.NewLine);
                    sb.Append(" WHERE A.IDF_ATIVO = 'S' " + Environment.NewLine);
                    //sb.Append(" ORDER BY A.DSC_GRAU_INSTRUCAO " + Environment.NewLine);
                    sb.Append(" ORDER BY NUM_ORDEM ");

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.GrauInstrucao g = new Hcrp.Framework.Classes.GrauInstrucao();
                        g.Codigo = Convert.ToInt32(dr["COD_GRAU_INSTRUCAO"]);
                        g.CodigoSus = Convert.ToString(dr["COD_GRAU_INSTRUCAO_SUS"]);
                        g.Ativo = Convert.ToString(dr["IDF_ATIVO"]) == "S";
                        g.Descricao = Convert.ToString(dr["DSC_GRAU_INSTRUCAO"]);
                        l.Add(g);
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
