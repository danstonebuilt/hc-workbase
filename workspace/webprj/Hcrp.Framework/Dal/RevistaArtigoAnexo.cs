using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using System.Web;
using System.IO;

namespace Hcrp.Framework.Dal
{
    public class RevistaArtigoAnexo : Hcrp.Framework.Classes.RevistaArtigoAnexo
    {
        public List<Hcrp.Framework.Classes.RevistaArtigoAnexo> BuscarAnexosPorArtigo(int numArtigo)
        {
            List<Hcrp.Framework.Classes.RevistaArtigoAnexo> l = new List<Hcrp.Framework.Classes.RevistaArtigoAnexo>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT A.SEQ_ANEXO,  A.DSC_CAMINHO_ANEXO, A.NOM_ANEXO " + Environment.NewLine);
                    sb.Append("  FROM REVISTA_ARTIGO_ANEXO A " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_REVISTA_ARTIGO = :SEQ_REVISTA_ARTIGO " + Environment.NewLine);
                    sb.Append(" ORDER BY A.SEQ_ANEXO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_REVISTA_ARTIGO"] = numArtigo;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.RevistaArtigoAnexo ra = new Hcrp.Framework.Classes.RevistaArtigoAnexo();
                        ra.CaminhoAnexo = Convert.ToString(dr["DSC_CAMINHO_ANEXO"]);
                        ra.Descricao = Convert.ToString(dr["NOM_ANEXO"]);
                        l.Add(ra);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Boolean InserirAtualizarComArtigo(Hcrp.Framework.Classes.RevistaArtigoAnexo Anexo, long seqArtigo)
        {
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                try
                {
                    Boolean r = false;
                    {
                        if (Anexo.Arquivo.HasFile)
                        {
                            if (Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("..\\Uploads\\Artigos\\" + Convert.ToString(seqArtigo)) + "\\Anexos\\"))
                            {
                                string[] arqDelete = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("..\\Uploads\\Artigos\\" + Convert.ToString(seqArtigo)) + "\\Anexos\\", Anexo.CaminhoAnexo);
                                foreach (string f in arqDelete)
                                {
                                    File.Delete(f);
                                }
                            }

                            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("..\\Uploads\\Artigos\\" + Convert.ToString(seqArtigo) + "\\Anexos\\")))
                                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("..\\Uploads\\Artigos\\" + Convert.ToString(seqArtigo) + "\\Anexos\\"));
                            Anexo.Arquivo.SaveAs(System.Web.HttpContext.Current.Server.MapPath("..\\Uploads\\Artigos\\" + Convert.ToString(seqArtigo)) + "\\Anexos\\" + Anexo.CaminhoAnexo);
                            
                            ctx.Open();

                            Hcrp.Infra.AcessoDado.UpdateCommandConfig comandoDelete = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REVISTA_ARTIGO_ANEXO");
                            comandoDelete.Params["SEQ_REVISTA_ARTIGO"] = seqArtigo;
                            comandoDelete.Params["DSC_CAMINHO_ANEXO"] = Anexo.CaminhoAnexo;
                            ctx.AllowUnqualifiedUpdates = true;
                            ctx.ExecuteDelete(comandoDelete);
                        
                            StringBuilder sb2 = new StringBuilder();

                            sb2.Append(" SELECT NVL(MAX(SEQ_ANEXO),0) + 1 QTD " + Environment.NewLine);
                            sb2.Append(" FROM REVISTA_ARTIGO_ANEXO A " + Environment.NewLine);
                            sb2.Append(" WHERE A.SEQ_REVISTA_ARTIGO = :SEQ_REVISTA_ARTIGO " + Environment.NewLine);

                            Hcrp.Infra.AcessoDado.QueryCommandConfig query2 = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb2.ToString());

                            query2.Params["SEQ_REVISTA_ARTIGO"] = seqArtigo;

                            DataTable dt = ctx.GetDataTable(query2);

                            // Cria objeto de material
                            int numOrdem = Convert.ToInt32(dt.Rows[0]["QTD"]);
                            Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("REVISTA_ARTIGO_ANEXO");
                            comando.Params["SEQ_REVISTA_ARTIGO"] = seqArtigo;
                            if (!String.IsNullOrWhiteSpace(Anexo.CaminhoAnexo))                            
                                comando.Params["DSC_CAMINHO_ANEXO"] = Anexo.CaminhoAnexo;
                            if (!String.IsNullOrWhiteSpace(Anexo.Descricao))
                                comando.Params["NOM_ANEXO"] = Anexo.Descricao;
                            comando.Params["SEQ_ANEXO"] = numOrdem;
                            // Executar o insert
                            ctx.ExecuteInsert(comando);
                            r = true;
                        }
                    }
                    return r;
                }
                catch (Exception ex)
                {
                    string erro = ex.Message;
                    //throw;
                    return false;
                }
        }

    }
}
