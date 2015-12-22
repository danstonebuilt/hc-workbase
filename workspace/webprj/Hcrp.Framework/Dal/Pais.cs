using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class Pais : Hcrp.Framework.Classes.Pais
    {
        public Hcrp.Framework.Classes.Pais BuscaPaisSigla(string sigla)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT SGL_PAIS, DSC_PAIS " + Environment.NewLine);
                    sb.Append(" FROM PAIS " + Environment.NewLine);
                    sb.Append(" WHERE SGL_PAIS = :SGL_PAIS " + Environment.NewLine);
                    


                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SGL_PAIS"] = sigla;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        this.Sigla = Convert.ToString(dr["SGL_PAIS"]);
                        this.Nome = Convert.ToString(dr["DSC_PAIS"]);
                    }
                }
                return this;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Hcrp.Framework.Classes.Pais> BuscaPais()
        {

            List<Hcrp.Framework.Classes.Pais> p = new List<Hcrp.Framework.Classes.Pais>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT SGL_PAIS, DSC_PAIS " + Environment.NewLine);
                    sb.Append(" FROM PAIS " + Environment.NewLine);
                    sb.Append(" ORDER BY DSC_PAIS");
                    
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.Pais a = new Hcrp.Framework.Classes.Pais();
                        a.Sigla = Convert.ToString(dr["SGL_PAIS"]);
                        a.Nome = Convert.ToString(dr["DSC_PAIS"]);
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
