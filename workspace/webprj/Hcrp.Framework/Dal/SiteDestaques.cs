using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class SiteDestaques : Hcrp.Framework.Classes.SiteDestaques
    {

        public List<Hcrp.Framework.Classes.SiteDestaques> BuscarDestaque(int CodSite)
        {
            try
            {
                List<Hcrp.Framework.Classes.SiteDestaques> L = new List<Hcrp.Framework.Classes.SiteDestaques>();
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();


                    //string sql =    " SELECT * FROM ( \n" +
                    //                " SELECT * FROM \n" + 
                    //                "   (SELECT DISTINCT D.IDF_LOCAL, \n" + 
                    //                "          I.SEQ_SITE_INFORMACAO, \n" + 
                    //                "          I.DTA_HOR_PUBLICACAO, \n" + 
                    //                "          I.DTA_HOR_EXPIRACAO, \n" + 
                    //                "          D.COD_SITE, \n" + 
                    //                "          D.COD_MENU_HORIZONTAL, \n" + 
                    //                "          D.COD_MENU_VERTICAL \n" + 
                    //                "   FROM SITE_MENU_VERTICAL_DESTAQUES D, \n" + 
                    //                "        SITE_MENU_HORIZONTAL H, \n" + 
                    //                "        SITE_MENU_HOR_VERTICAL HV, \n" + 
                    //                "        SITE_MENU_VERTICAL V, \n" + 
                    //                "        SITE_MENU_VERTICAL_INFORMACAO VI, \n" + 
                    //                "        SITE_INFORMACAO I \n" + 
                    //                "   WHERE D.COD_MENU_HORIZONTAL = H.COD_MENU_HORIZONTAL \n" + 
                    //                "   AND H.COD_MENU_HORIZONTAL = HV.COD_MENU_HORIZONTAL \n" + 
                    //                "   AND HV.COD_MENU_VERTICAL = V.COD_MENU_VERTICAL \n" + 
                    //                "   AND V.COD_MENU_VERTICAL = VI.COD_MENU_VERTICAL \n" + 
                    //                "   AND VI.SEQ_SITE_INFORMACAO = I.SEQ_SITE_INFORMACAO \n" + 
                    //                "   UNION \n" + 
                    //                "   SELECT DISTINCT D.IDF_LOCAL, \n" + 
                    //                "          I.SEQ_SITE_INFORMACAO, \n" + 
                    //                "          I.DTA_HOR_PUBLICACAO, \n" + 
                    //                "          I.DTA_HOR_EXPIRACAO, \n" +
                    //                "          D.COD_SITE, \n" +
                    //                "          D.COD_MENU_HORIZONTAL, \n" +
                    //                "          D.COD_MENU_VERTICAL \n" +  
                    //                "   FROM SITE_MENU_VERTICAL_DESTAQUES D, \n" + 
                    //                "        SITE_MENU_VERTICAL V, \n" + 
                    //                "        SITE_MENU_VERTICAL_INFORMACAO VI, \n" + 
                    //                "        SITE_INFORMACAO I \n" + 
                    //                "   WHERE D.COD_MENU_VERTICAL = V.COD_MENU_VERTICAL \n" + 
                    //                "   AND V.COD_MENU_VERTICAL = VI.COD_MENU_VERTICAL \n" + 
                    //                "   AND VI.SEQ_SITE_INFORMACAO = I.SEQ_SITE_INFORMACAO \n" + 
                    //                "   UNION\n" + 
                    //                "   SELECT DISTINCT D.IDF_LOCAL, \n" + 
                    //                "          I.SEQ_SITE_INFORMACAO, \n" + 
                    //                "          I.DTA_HOR_PUBLICACAO, \n" + 
                    //                "          I.DTA_HOR_EXPIRACAO, \n" +
                    //                "          D.COD_SITE, \n" +
                    //                "          D.COD_MENU_HORIZONTAL, \n" +
                    //                "          D.COD_MENU_VERTICAL \n" + 
                    //                "   FROM SITE_MENU_VERTICAL_DESTAQUES D, \n" + 
                    //                "        SITE_INFORMACAO I \n" + 
                    //                "   WHERE D.SEQ_SITE_INFORMACAO = I.SEQ_SITE_INFORMACAO) X \n" +
                    //                " WHERE X.COD_SITE = :COD_SITE \n" + 
                    //                " AND SYSDATE BETWEEN X.DTA_HOR_PUBLICACAO AND X.DTA_HOR_EXPIRACAO \n" + 
                    //                " ORDER BY DBMS_RANDOM.RANDOM ) ";




                    string sql = "select * from\n" +
                    "\n" +
                    "(\n" +
                    "(\n" +
                    "SELECT * FROM\n" +
                    "   (SELECT DISTINCT D.IDF_LOCAL,\n" +
                    "          I.SEQ_SITE_INFORMACAO,\n" +
                    "          I.DTA_HOR_PUBLICACAO,\n" +
                    "          I.DTA_HOR_EXPIRACAO,\n" +
                    "          D.COD_SITE,\n" +
                    "          D.COD_MENU_HORIZONTAL,\n" +
                    "          D.COD_MENU_VERTICAL\n" +
                    "   FROM SITE_MENU_VERTICAL_DESTAQUES D,\n" +
                    "        SITE_MENU_HORIZONTAL H,\n" +
                    "        SITE_MENU_HOR_VERTICAL HV,\n" +
                    "        SITE_MENU_VERTICAL V,\n" +
                    "        SITE_MENU_VERTICAL_INFORMACAO VI,\n" +
                    "        SITE_INFORMACAO I\n" +
                    "   WHERE D.COD_MENU_HORIZONTAL = H.COD_MENU_HORIZONTAL\n" +
                    "   AND H.COD_MENU_HORIZONTAL = HV.COD_MENU_HORIZONTAL\n" +
                    "   AND HV.COD_MENU_VERTICAL = V.COD_MENU_VERTICAL\n" +
                    "   AND V.COD_MENU_VERTICAL = VI.COD_MENU_VERTICAL\n" +
                    "   AND VI.SEQ_SITE_INFORMACAO = I.SEQ_SITE_INFORMACAO\n" +
                    "   UNION\n" +
                    "   SELECT DISTINCT D.IDF_LOCAL,\n" +
                    "          I.SEQ_SITE_INFORMACAO,\n" +
                    "          I.DTA_HOR_PUBLICACAO,\n" +
                    "          I.DTA_HOR_EXPIRACAO,\n" +
                    "          D.COD_SITE,\n" +
                    "          D.COD_MENU_HORIZONTAL,\n" +
                    "          D.COD_MENU_VERTICAL\n" +
                    "   FROM SITE_MENU_VERTICAL_DESTAQUES D,\n" +
                    "        SITE_MENU_VERTICAL V,\n" +
                    "        SITE_MENU_VERTICAL_INFORMACAO VI,\n" +
                    "        SITE_INFORMACAO I\n" +
                    "   WHERE D.COD_MENU_VERTICAL = V.COD_MENU_VERTICAL\n" +
                    "   AND V.COD_MENU_VERTICAL = VI.COD_MENU_VERTICAL\n" +
                    "   AND VI.SEQ_SITE_INFORMACAO = I.SEQ_SITE_INFORMACAO\n" +
                    "   UNION\n" +
                    "   SELECT DISTINCT D.IDF_LOCAL,\n" +
                    "          I.SEQ_SITE_INFORMACAO,\n" +
                    "          I.DTA_HOR_PUBLICACAO,\n" +
                    "          I.DTA_HOR_EXPIRACAO,\n" +
                    "          D.COD_SITE,\n" +
                    "          D.COD_MENU_HORIZONTAL,\n" +
                    "          D.COD_MENU_VERTICAL\n" +
                    "   FROM SITE_MENU_VERTICAL_DESTAQUES D,\n" +
                    "        SITE_INFORMACAO I\n" +
                    "   WHERE D.SEQ_SITE_INFORMACAO = I.SEQ_SITE_INFORMACAO) X\n" +
                    " WHERE X.COD_SITE = :COD_SITE\n" +
                    " AND SYSDATE BETWEEN X.DTA_HOR_PUBLICACAO AND X.DTA_HOR_EXPIRACAO\n" +
                    " )\n" +
                    " MINUS\n" +
                    " (\n" +
                    " select * from (\n" +
                    "\n" +
                    "\n" +
                    " SELECT * FROM\n" +
                    "   (SELECT DISTINCT D.IDF_LOCAL,\n" +
                    "          I.SEQ_SITE_INFORMACAO,\n" +
                    "          I.DTA_HOR_PUBLICACAO,\n" +
                    "          I.DTA_HOR_EXPIRACAO,\n" +
                    "          D.COD_SITE,\n" +
                    "          D.COD_MENU_HORIZONTAL,\n" +
                    "          D.COD_MENU_VERTICAL\n" +
                    "   FROM SITE_MENU_VERTICAL_DESTAQUES D,\n" +
                    "        SITE_MENU_HORIZONTAL H,\n" +
                    "        SITE_MENU_HOR_VERTICAL HV,\n" +
                    "        SITE_MENU_VERTICAL V,\n" +
                    "        SITE_MENU_VERTICAL_INFORMACAO VI,\n" +
                    "        SITE_INFORMACAO I\n" +
                    "   WHERE D.COD_MENU_HORIZONTAL = H.COD_MENU_HORIZONTAL\n" +
                    "   AND H.COD_MENU_HORIZONTAL = HV.COD_MENU_HORIZONTAL\n" +
                    "   AND HV.COD_MENU_VERTICAL = V.COD_MENU_VERTICAL\n" +
                    "   AND V.COD_MENU_VERTICAL = VI.COD_MENU_VERTICAL\n" +
                    "   AND VI.SEQ_SITE_INFORMACAO = I.SEQ_SITE_INFORMACAO\n" +
                    "   UNION\n" +
                    "   SELECT DISTINCT D.IDF_LOCAL,\n" +
                    "          I.SEQ_SITE_INFORMACAO,\n" +
                    "          I.DTA_HOR_PUBLICACAO,\n" +
                    "          I.DTA_HOR_EXPIRACAO,\n" +
                    "          D.COD_SITE,\n" +
                    "          D.COD_MENU_HORIZONTAL,\n" +
                    "          D.COD_MENU_VERTICAL\n" +
                    "   FROM SITE_MENU_VERTICAL_DESTAQUES D,\n" +
                    "        SITE_MENU_VERTICAL V,\n" +
                    "        SITE_MENU_VERTICAL_INFORMACAO VI,\n" +
                    "        SITE_INFORMACAO I\n" +
                    "   WHERE D.COD_MENU_VERTICAL = V.COD_MENU_VERTICAL\n" +
                    "   AND V.COD_MENU_VERTICAL = VI.COD_MENU_VERTICAL\n" +
                    "   AND VI.SEQ_SITE_INFORMACAO = I.SEQ_SITE_INFORMACAO\n" +
                    "   UNION\n" +
                    "   SELECT DISTINCT D.IDF_LOCAL,\n" +
                    "          I.SEQ_SITE_INFORMACAO,\n" +
                    "          I.DTA_HOR_PUBLICACAO,\n" +
                    "          I.DTA_HOR_EXPIRACAO,\n" +
                    "          D.COD_SITE,\n" +
                    "          D.COD_MENU_HORIZONTAL,\n" +
                    "          D.COD_MENU_VERTICAL\n" +
                    "   FROM SITE_MENU_VERTICAL_DESTAQUES D,\n" +
                    "        SITE_INFORMACAO I\n" +
                    "   WHERE D.SEQ_SITE_INFORMACAO = I.SEQ_SITE_INFORMACAO) X\n" +
                    " WHERE X.COD_SITE = :COD_SITE\n" +
                    " AND SYSDATE BETWEEN X.DTA_HOR_PUBLICACAO AND X.DTA_HOR_EXPIRACAO\n" +
                    "\n" +
                    " ) Z WHERE (Z.COD_MENU_HORIZONTAL IS NOT NULL OR Z.COD_MENU_VERTICAL IS NOT NULL)\n" +
                    "     AND Z.SEQ_SITE_INFORMACAO IN\n" +
                    "\n" +
                    "     (\n" +
                    "\n" +
                    "     select Y.SEQ_SITE_INFORMACAO from (\n" +
                    "\n" +
                    "\n" +
                    " SELECT * FROM\n" +
                    "   (SELECT DISTINCT D.IDF_LOCAL,\n" +
                    "          I.SEQ_SITE_INFORMACAO,\n" +
                    "          I.DTA_HOR_PUBLICACAO,\n" +
                    "          I.DTA_HOR_EXPIRACAO,\n" +
                    "          D.COD_SITE,\n" +
                    "          D.COD_MENU_HORIZONTAL,\n" +
                    "          D.COD_MENU_VERTICAL\n" +
                    "   FROM SITE_MENU_VERTICAL_DESTAQUES D,\n" +
                    "        SITE_MENU_HORIZONTAL H,\n" +
                    "        SITE_MENU_HOR_VERTICAL HV,\n" +
                    "        SITE_MENU_VERTICAL V,\n" +
                    "        SITE_MENU_VERTICAL_INFORMACAO VI,\n" +
                    "        SITE_INFORMACAO I\n" +
                    "   WHERE D.COD_MENU_HORIZONTAL = H.COD_MENU_HORIZONTAL\n" +
                    "   AND H.COD_MENU_HORIZONTAL = HV.COD_MENU_HORIZONTAL\n" +
                    "   AND HV.COD_MENU_VERTICAL = V.COD_MENU_VERTICAL\n" +
                    "   AND V.COD_MENU_VERTICAL = VI.COD_MENU_VERTICAL\n" +
                    "   AND VI.SEQ_SITE_INFORMACAO = I.SEQ_SITE_INFORMACAO\n" +
                    "   UNION\n" +
                    "   SELECT DISTINCT D.IDF_LOCAL,\n" +
                    "          I.SEQ_SITE_INFORMACAO,\n" +
                    "          I.DTA_HOR_PUBLICACAO,\n" +
                    "          I.DTA_HOR_EXPIRACAO,\n" +
                    "          D.COD_SITE,\n" +
                    "          D.COD_MENU_HORIZONTAL,\n" +
                    "          D.COD_MENU_VERTICAL\n" +
                    "   FROM SITE_MENU_VERTICAL_DESTAQUES D,\n" +
                    "        SITE_MENU_VERTICAL V,\n" +
                    "        SITE_MENU_VERTICAL_INFORMACAO VI,\n" +
                    "        SITE_INFORMACAO I\n" +
                    "   WHERE D.COD_MENU_VERTICAL = V.COD_MENU_VERTICAL\n" +
                    "   AND V.COD_MENU_VERTICAL = VI.COD_MENU_VERTICAL\n" +
                    "   AND VI.SEQ_SITE_INFORMACAO = I.SEQ_SITE_INFORMACAO\n" +
                    "   UNION\n" +
                    "   SELECT DISTINCT D.IDF_LOCAL,\n" +
                    "          I.SEQ_SITE_INFORMACAO,\n" +
                    "          I.DTA_HOR_PUBLICACAO,\n" +
                    "          I.DTA_HOR_EXPIRACAO,\n" +
                    "          D.COD_SITE,\n" +
                    "          D.COD_MENU_HORIZONTAL,\n" +
                    "          D.COD_MENU_VERTICAL\n" +
                    "   FROM SITE_MENU_VERTICAL_DESTAQUES D,\n" +
                    "        SITE_INFORMACAO I\n" +
                    "   WHERE D.SEQ_SITE_INFORMACAO = I.SEQ_SITE_INFORMACAO) X\n" +
                    " WHERE X.COD_SITE = :COD_SITE\n" +
                    " AND SYSDATE BETWEEN X.DTA_HOR_PUBLICACAO AND X.DTA_HOR_EXPIRACAO\n" +
                    "\n" +
                    " ) Y WHERE Y.COD_MENU_HORIZONTAL IS NULL AND Y.COD_MENU_VERTICAL IS NULL\n" +
                    "\n" +
                    "     )\n" +
                    " )\n" +
                    ")\n" +
                    " ORDER BY DBMS_RANDOM.RANDOM";




                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);
                    query.Params["COD_SITE"] = CodSite;
                    ctx.ExecuteQuery(query);
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    this.IdLocal = 0;
                    this._IdInformacao = 0;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.SiteDestaques I = new Hcrp.Framework.Classes.SiteDestaques(-1);
                        I.IdLocal = Convert.ToInt32(dr["IDF_LOCAL"]);
                        I._IdInformacao = Convert.ToInt32(dr["SEQ_SITE_INFORMACAO"]);
                        if ((dr["COD_MENU_HORIZONTAL"] != DBNull.Value))
                            I._IdMenuHorizontal = Convert.ToInt32(dr["COD_MENU_HORIZONTAL"]);
                        if ((dr["COD_MENU_VERTICAL"] != DBNull.Value))
                            I._IdMenuVertical = Convert.ToInt32(dr["COD_MENU_VERTICAL"]);
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




        public void BuscarConfigDestaque(Hcrp.Framework.Classes.SiteDestaques DestaqueX)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();


                    string sql = " SELECT * FROM SITE_MENU_VERTICAL_DESTAQUES A WHERE A.SEQ_MENU_VERT_DEST = :SEQ ";

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);
                    query.Params["SEQ"] = DestaqueX.Seq;
                    ctx.ExecuteQuery(query);
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        DestaqueX.IdLocal = Convert.ToInt32(dr["IDF_LOCAL"]);
                        if ((dr["SEQ_SITE_INFORMACAO"] != DBNull.Value))
                            DestaqueX._IdInformacao = Convert.ToInt32(dr["SEQ_SITE_INFORMACAO"]);
                        if ((dr["COD_MENU_HORIZONTAL"] != DBNull.Value))
                            DestaqueX._IdMenuHorizontal = Convert.ToInt32(dr["COD_MENU_HORIZONTAL"]);
                        if ((dr["COD_MENU_VERTICAL"] != DBNull.Value))
                            DestaqueX._IdMenuVertical = Convert.ToInt32(dr["COD_MENU_VERTICAL"]);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        } //fim buscar


        public int BuscarSeqDestaque(int codSite, int idfLocal, int CodMenuHorizontal)
        {
            try
            {
                int Sequence = -1;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    
                    string sql =    " SELECT A.SEQ_MENU_VERT_DEST FROM SITE_MENU_VERTICAL_DESTAQUES A "+
                                    " WHERE A.COD_SITE  = :COD_SITE "+
                                    "   AND A.IDF_LOCAL = :IDF_LOCAL "+
                                    "   AND A.COD_MENU_HORIZONTAL = :COD_MENU_HORIZONTAL ";

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);
                    query.Params["COD_SITE"] = codSite;
                    query.Params["IDF_LOCAL"] = idfLocal;
                    query.Params["COD_MENU_HORIZONTAL"] = CodMenuHorizontal;
                    ctx.ExecuteQuery(query);
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        if ((dr["SEQ_MENU_VERT_DEST"] != DBNull.Value))
                            Sequence = Convert.ToInt32(dr["SEQ_MENU_VERT_DEST"]);
                        else
                        {
                            Sequence = -1;
                        }
                    }
                }
                return Sequence;
            }
            catch (Exception)
            {
                throw;
            }
        } //fim buscarSeq1

        public int BuscarSeqDestaque(int codSite, int idfLocal)
        {
            try
            {
                int Sequence = -1;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();


                    string sql = " SELECT A.SEQ_MENU_VERT_DEST FROM SITE_MENU_VERTICAL_DESTAQUES A " +
                                    " WHERE A.COD_SITE  = :COD_SITE " +
                                    "   AND A.IDF_LOCAL = :IDF_LOCAL ";

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);
                    query.Params["COD_SITE"] = codSite;
                    query.Params["IDF_LOCAL"] = idfLocal;
                    ctx.ExecuteQuery(query);
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        if ((dr["SEQ_MENU_VERT_DEST"] != DBNull.Value))
                            Sequence = Convert.ToInt32(dr["SEQ_MENU_VERT_DEST"]);
                        else
                        {
                            Sequence = -1;
                        }
                    }
                }
                return Sequence;
            }
            catch (Exception)
            {
                throw;
            }
        } //fim buscarSeq1


        public bool GravarConfiguracoesDestaque(Hcrp.Framework.Classes.SiteDestaques DestaqueX)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();
                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_MENU_VERTICAL_DESTAQUES");
                    

                    if (DestaqueX._IdMenuHorizontal == 0)
                        comando.Params["COD_MENU_HORIZONTAL"] = DBNull.Value;
                    else
                        comando.Params["COD_MENU_HORIZONTAL"] = DestaqueX._IdMenuHorizontal;

                    if (DestaqueX._IdMenuVertical == 0)
                        comando.Params["COD_MENU_VERTICAL"] = DBNull.Value;
                    else
                        comando.Params["COD_MENU_VERTICAL"] = DestaqueX._IdMenuVertical;

                    if (DestaqueX._IdInformacao == 0)
                        comando.Params["SEQ_SITE_INFORMACAO"] = DBNull.Value;
                    else
                        comando.Params["SEQ_SITE_INFORMACAO"] = DestaqueX._IdInformacao;


                    comando.FilterParams["SEQ_MENU_VERT_DEST"] = DestaqueX.Seq;

                    // Executar o update
                    ctx.ExecuteUpdate(comando);

                    // Pegar o ID
                    return true;

                }

            }
            catch (Exception)
            {
                return false;
            }
        } //fim Gravação

    }
}
