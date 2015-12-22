using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class SiteMenuHorizontal : Hcrp.Framework.Classes.SiteMenuHorizontal
    {

        public SiteMenuHorizontal CarregarMenuHorizontal(int CodMenuV)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    string sql =    " SELECT A.COD_MENU_HORIZONTAL, A.NOM_MENU_HORIZONTAL, A.DSC_URL, A.IDF_ATIVO \n" +
                                    " FROM SITE_MENU_HORIZONTAL A \n" +
                                    " WHERE /*A.COD_SITE = 1 \n" + 
                                    " AND*/ A.COD_MENU_HORIZONTAL = :COD_MENU_HORIZONTAL";



                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);
                    query.Params["COD_MENU_HORIZONTAL"] = CodMenuV;
                    ctx.ExecuteQuery(query);
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        this.Codigo = Convert.ToInt32(dr["COD_MENU_HORIZONTAL"]);
                        this.Nome = Convert.ToString(dr["NOM_MENU_HORIZONTAL"]);
                        this.Url = Convert.ToString(dr["DSC_URL"]);
                        this.Ativo = (Convert.ToString(dr["IDF_ATIVO"]) == "S");
                    }

                    return this;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<Hcrp.Framework.Classes.SiteMenuHorizontal> BuscarMenuHorizontal(int CodSite)
        {
            try
            {
                List<Hcrp.Framework.Classes.SiteMenuHorizontal> L = new List<Hcrp.Framework.Classes.SiteMenuHorizontal>();
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    string sql = " SELECT H.COD_SITE, H.COD_MENU_HORIZONTAL, H.NOM_MENU_HORIZONTAL, H.DSC_URL, H.NUM_ORDEM, H.IDF_ATIVO, H.DSC_IMAGEM FROM SITE_MENU_HORIZONTAL H\n" +
                                       " WHERE H.COD_SITE = :COD_SITE ORDER BY NUM_ORDEM";

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);
                    query.Params["COD_SITE"] = CodSite;
                    ctx.ExecuteQuery(query);
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.SiteMenuHorizontal I = new Hcrp.Framework.Classes.SiteMenuHorizontal();
                        I.Codigo = Convert.ToInt32(dr["COD_MENU_HORIZONTAL"]);
                        I.Nome = Convert.ToString(dr["NOM_MENU_HORIZONTAL"]);
                        I.Ativo = (Convert.ToString(dr["IDF_ATIVO"]) == "S");
                        I.Url = Convert.ToString(dr["DSC_URL"]);
                        I.Ordem = Convert.ToInt32(dr["NUM_ORDEM"]);
                        I.Imagem = Convert.ToString(dr["DSC_IMAGEM"]);
                        L.Add(I);
                    }

                    return L;

                }
            }
            catch (Exception)
            {      
                throw;
            }


        }


        public List<Hcrp.Framework.Classes.SiteMenuHorizontal> BuscarMenuHorizontalPorMenuVertical(int CodMenuV)
        {
            try
            {
                List<Hcrp.Framework.Classes.SiteMenuHorizontal> L = new List<Hcrp.Framework.Classes.SiteMenuHorizontal>();
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    string sql =    " SELECT H.COD_SITE, H.COD_MENU_HORIZONTAL, H.NOM_MENU_HORIZONTAL, H.DSC_URL, H.NUM_ORDEM, H.IDF_ATIVO, H.DSC_IMAGEM \n" +
                                    " FROM SITE_MENU_HORIZONTAL H, SITE_MENU_HOR_VERTICAL HV \n" +
                                    " WHERE H.COD_MENU_HORIZONTAL = HV.COD_MENU_HORIZONTAL \n" +
                                    " AND HV.COD_MENU_VERTICAL = :COD_MENU_VERTICAL ";


                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);
                    query.Params["COD_MENU_VERTICAL"] = CodMenuV;
                    ctx.ExecuteQuery(query);
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.SiteMenuHorizontal I = new Hcrp.Framework.Classes.SiteMenuHorizontal();
                        I.Codigo = Convert.ToInt32(dr["COD_MENU_HORIZONTAL"]);
                        I.Nome = Convert.ToString(dr["NOM_MENU_HORIZONTAL"]);
                        I.Ativo = (Convert.ToString(dr["IDF_ATIVO"]) == "S");
                        I.Url = Convert.ToString(dr["DSC_URL"]);
                        I.Ordem = Convert.ToInt32(dr["NUM_ORDEM"]);
                        I.Imagem = Convert.ToString(dr["DSC_IMAGEM"]);
                        L.Add(I);
                    }

                    return L;

                }
            }
            catch (Exception)
            {
                throw;
            }


        }


        public List<Hcrp.Framework.Classes.SiteMenuHorizontal> BuscarDestaques(int CodMenuH)
        {
            try
            {
                List<Hcrp.Framework.Classes.SiteMenuHorizontal> L = new List<Hcrp.Framework.Classes.SiteMenuHorizontal>();
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    string sql = " SELECT H.COD_SITE, H.COD_MENU_HORIZONTAL, H.NOM_MENU_HORIZONTAL, H.DSC_URL, H.NUM_ORDEM, H.IDF_ATIVO, H.DSC_IMAGEM FROM SITE_MENU_HORIZONTAL H\n" +
                                       " WHERE H.COD_SITE = :COD_SITE";

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);
                    query.Params["COD_SITE"] = CodMenuH;
                    ctx.ExecuteQuery(query);
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.SiteMenuHorizontal I = new Hcrp.Framework.Classes.SiteMenuHorizontal();
                        I.Codigo = Convert.ToInt32(dr["COD_MENU_HORIZONTAL"]);
                        I.Nome = Convert.ToString(dr["NOM_MENU_HORIZONTAL"]);
                        I.Ativo = (Convert.ToString(dr["IDF_ATIVO"]) == "S");
                        I.Url = Convert.ToString(dr["DSC_URL"]);
                        I.Ordem = Convert.ToInt32(dr["NUM_ORDEM"]);
                        I.Imagem = Convert.ToString(dr["DSC_IMAGEM"]);
                        L.Add(I);
                    }

                    return L;

                }
            }
            catch (Exception)
            {
                throw;
            }


        }

    }
}
