using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
 

namespace Hcrp.Framework.Dal
{
    public class Site : Hcrp.Framework.Classes.Site
    {

        public Hcrp.Framework.Classes.Site BuscarSite(int CodSite)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    string sql = " SELECT S.COD_SITE, S.NOM_SITE, S.IDF_ATIVO, S.DSC_URL_ATIVO, S.DSC_URL_INATIVO FROM SITE S\n" +
                                       " WHERE S.COD_SITE = :COD_SITE ";

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);
                    query.Params["COD_SITE"] = CodSite;
                    ctx.ExecuteQuery(query);
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        this.Codigo = Convert.ToInt32(dr["COD_SITE"]);
                        this.Nome = Convert.ToString(dr["NOM_SITE"]);
                        this.Ativo = (Convert.ToString(dr["IDF_ATIVO"]) == "S");
                        this.UrlAtivo = Convert.ToString(dr["DSC_URL_ATIVO"]);
                        this.UrlInativo = Convert.ToString(dr["DSC_URL_INATIVO"]);
                    }

                    return this;

                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }



    
    }
}
