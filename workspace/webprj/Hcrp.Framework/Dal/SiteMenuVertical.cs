using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class SiteMenuVertical : Hcrp.Framework.Classes.SiteMenuVertical
    {

        public SiteMenuVertical CarregarMenuVertical(int CodMenuV)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    string sql  =   " SELECT A.COD_MENU_VERTICAL, A.DSC_URL, A.NOM_MENU_VERTICAL, A.IDF_ATIVO \n" +
                                    " FROM SITE_MENU_VERTICAL A \n" +
                                    " WHERE A.COD_MENU_VERTICAL = :COD_MENU_VERTICAL";


                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);
                    query.Params["COD_MENU_VERTICAL"] = CodMenuV;
                    ctx.ExecuteQuery(query);
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        this.Codigo = Convert.ToInt32(dr["COD_MENU_VERTICAL"]);
                        this.Nome = Convert.ToString(dr["NOM_MENU_VERTICAL"]);
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
        public List<Hcrp.Framework.Classes.SiteMenuVertical> BuscarMenuVertical(int CodMenuHorizontal)
        {
            try
            {
                List<Hcrp.Framework.Classes.SiteMenuVertical> L = new List<Hcrp.Framework.Classes.SiteMenuVertical>();
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();


                    string sql =    " SELECT V.COD_MENU_VERTICAL, V.NOM_MENU_VERTICAL, \n" +
                                    "       V.DSC_URL, V.IDF_ATIVO, HV.COD_MENU_VERT_PAI, \n" +
                                    "       HV.NUM_ORDEM \n" +
                                    " FROM SITE_MENU_VERTICAL V, SITE_MENU_HOR_VERTICAL HV \n" +
                                    " WHERE V.COD_MENU_VERTICAL = HV.COD_MENU_VERTICAL \n" +
                                    " AND HV.COD_MENU_HORIZONTAL = :COD_MENU_HORIZONTAL \n" +
                                    " ORDER BY HV.NUM_ORDEM";


                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);
                    query.Params["COD_MENU_HORIZONTAL"] = CodMenuHorizontal;
                    ctx.ExecuteQuery(query);
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.SiteMenuVertical I = new Hcrp.Framework.Classes.SiteMenuVertical();
                        I.Codigo = Convert.ToInt32(dr["COD_MENU_VERTICAL"]);
                        I.Nome = Convert.ToString(dr["NOM_MENU_VERTICAL"]);
                        I.Url = Convert.ToString(dr["DSC_URL"]);
                        I.Ativo = (Convert.ToString(dr["IDF_ATIVO"]) == "S");
                        I.CodigoPai = Convert.ToInt32(dr["COD_MENU_VERT_PAI"]);
                        I.Ordem = Convert.ToInt32(dr["NUM_ORDEM"]);
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
        public List<Hcrp.Framework.Classes.SiteMenuVertical> BuscarMenuVerticalInformacao(int SeqInformacao)
        {
            try
            {
                List<Hcrp.Framework.Classes.SiteMenuVertical> L = new List<Hcrp.Framework.Classes.SiteMenuVertical>();
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();


                    string sql = " SELECT A.COD_MENU_VERTICAL, A.NOM_MENU_VERTICAL, " +
                                 "        A.DSC_URL, A.IDF_ATIVO " +
                                 " FROM SITE_MENU_VERTICAL A, SITE_MENU_VERTICAL_INFORMACAO B " +
                                 " WHERE A.COD_MENU_VERTICAL = B.COD_MENU_VERTICAL " +
                                 " AND B.SEQ_SITE_INFORMACAO = :SEQ_SITE_INFORMACAO " +
                                 " ORDER BY A.NOM_MENU_VERTICAL ";
                                 
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);
                    query.Params["SEQ_SITE_INFORMACAO"] = SeqInformacao;
                    ctx.ExecuteQuery(query);
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.SiteMenuVertical I = new Hcrp.Framework.Classes.SiteMenuVertical();                        
                        I.Codigo = Convert.ToInt32(dr["COD_MENU_VERTICAL"]);
                        I.Nome = Convert.ToString(dr["NOM_MENU_VERTICAL"]);
                        I.Url = Convert.ToString(dr["DSC_URL"]);
                        I.Ativo = (Convert.ToString(dr["IDF_ATIVO"]) == "S");
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
