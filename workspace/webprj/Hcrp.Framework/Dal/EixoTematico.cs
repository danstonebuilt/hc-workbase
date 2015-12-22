using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class EixoTematico : Hcrp.Framework.Classes.EixoTematico
    {
        public Hcrp.Framework.Classes.EixoTematico BuscarEixoCodigo(int codEixoTematico)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT COD_EIXO_TEMATICO, DSC_EIXO_TEMATICO " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_EIXO_TEMATICO " + Environment.NewLine);
                    sb.Append(" WHERE COD_EIXO_TEMATICO = :COD_EIXO_TEMATICO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_EIXO_TEMATICO"] = codEixoTematico;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        this.Codigo = Convert.ToInt32(dr["COD_EIXO_TEMATICO"]);
                        this.Descricao = Convert.ToString(dr["DSC_EIXO_TEMATICO"]);
                    }
                }
                return this;
            }
            catch (Exception)
            {
                throw;
            }            
        }
        public List<Hcrp.Framework.Classes.EixoTematico> BuscarEixos()
        {
            try
            {
                List<Hcrp.Framework.Classes.EixoTematico> l = new List<Hcrp.Framework.Classes.EixoTematico>();
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT -1 COD_EIXO_TEMATICO, ' ' DSC_EIXO_TEMATICO FROM DUAL " + Environment.NewLine);
                    sb.Append(" UNION ALL " + Environment.NewLine);
                    sb.Append(" SELECT COD_EIXO_TEMATICO, DSC_EIXO_TEMATICO " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_EIXO_TEMATICO " + Environment.NewLine);
                    sb.Append(" ORDER BY DSC_EIXO_TEMATICO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.EixoTematico E = new Hcrp.Framework.Classes.EixoTematico();
                        E.Codigo = Convert.ToInt32(dr["COD_EIXO_TEMATICO"]);
                        E.Descricao = Convert.ToString(dr["DSC_EIXO_TEMATICO"]);
                        l.Add(E);
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
