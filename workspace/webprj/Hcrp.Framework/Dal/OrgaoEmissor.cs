using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class OrgaoEmissor : Hcrp.Framework.Classes.OrgaoEmissor
    {
        public List<Hcrp.Framework.Classes.OrgaoEmissor> BuscarOrgaoEmissor()
        {
            List<Hcrp.Framework.Classes.OrgaoEmissor> l = new List<Hcrp.Framework.Classes.OrgaoEmissor>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COD_ORGAO, DSC_ORGAO, IDF_ATIVO " + Environment.NewLine);
                    sb.Append(" FROM ORGAO_EMISSOR " + Environment.NewLine);
                    sb.Append(" ORDER BY DSC_ORGAO " + Environment.NewLine);
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.OrgaoEmissor o = new Hcrp.Framework.Classes.OrgaoEmissor();
                        o.Codigo = Convert.ToInt32(dr["COD_ORGAO"]);
                        o.Descricao = Convert.ToString(dr["DSC_ORGAO"]);
                        o.Ativo = Convert.ToString(dr["IDF_ATIVO"])=="S";
                        l.Add(o);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }        
        }    

        public Hcrp.Framework.Classes.OrgaoEmissor BuscarOrgaoEmissorCodigo(int cod)
        {
            Hcrp.Framework.Classes.OrgaoEmissor o = new Hcrp.Framework.Classes.OrgaoEmissor();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COD_ORGAO, DSC_ORGAO, IDF_ATIVO " + Environment.NewLine);
                    sb.Append(" FROM ORGAO_EMISSOR " + Environment.NewLine);
                    sb.Append(" WHERE COD_ORGAO = :COD_ORGAO " + Environment.NewLine);
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_ORGAO"] = cod;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        o.Codigo = Convert.ToInt32(dr["COD_ORGAO"]);
                        o.Descricao = Convert.ToString(dr["DSC_ORGAO"]);
                        o.Ativo = Convert.ToString(dr["IDF_ATIVO"]) == "S";
                    }
                }
                return o;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
