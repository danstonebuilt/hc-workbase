using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class AutorRevistaArtigo : Hcrp.Framework.Classes.AutorRevistaArtigo
    {
        public List<Hcrp.Framework.Classes.AutorRevistaArtigo> BuscarAutorPorNomeCpf(string nomeCpf, string tipoPesquisa)
        {
            List<Hcrp.Framework.Classes.AutorRevistaArtigo> l = new List<Hcrp.Framework.Classes.AutorRevistaArtigo>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT DISTINCT A.NUM_DOC_AUTOR, A.NOM_AUTOR, A.IDF_DOC, B.DSC_AREA " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_AUTOR A, REVISTA_AUTOR_ARTIGO B " + Environment.NewLine);
                    if (tipoPesquisa == "c")
                        sb.Append(" WHERE A.NUM_DOC_AUTOR LIKE '%" + nomeCpf + "%' " + Environment.NewLine);
                    else sb.Append(" WHERE UPPER(A.NOM_AUTOR) LIKE '%" + nomeCpf.ToUpper() + "%' " + Environment.NewLine);
                    sb.Append("   AND A.NUM_DOC_AUTOR = B.NUM_DOC_AUTOR(+) " + Environment.NewLine);
                    sb.Append(" ORDER BY A.NOM_AUTOR, B.DSC_AREA " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.AutorRevistaArtigo a = new Hcrp.Framework.Classes.AutorRevistaArtigo();
                        a.Nome = Convert.ToString(dr["NOM_AUTOR"]);
                        a.Documento = Convert.ToString(dr["NUM_DOC_AUTOR"]);
                        a.TipoDocumento = (Usuario.ETipoDocumento)Convert.ToInt32(dr["IDF_DOC"]);
                        a.Area = Convert.ToString(dr["DSC_AREA"]);
                        a.AutorPrincipal = false;
                        l.Add(a);
                    }

                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }

	
	public List<Hcrp.Framework.Classes.AutorRevistaArtigo> BuscarAutoresDoArtigo(int NumArtigo)
        {
            List<Hcrp.Framework.Classes.AutorRevistaArtigo> l = new List<Hcrp.Framework.Classes.AutorRevistaArtigo>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.NUM_DOC_AUTOR, A.NOM_AUTOR, A.IDF_DOC, B.DSC_AREA, B.NUM_ORDEM " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_AUTOR A, REVISTA_AUTOR_ARTIGO B " + Environment.NewLine);
                    sb.Append(" WHERE B.SEQ_REVISTA_ARTIGO = " + NumArtigo + Environment.NewLine);
                    sb.Append(" AND A.NUM_DOC_AUTOR = B.NUM_DOC_AUTOR " + Environment.NewLine);
                    sb.Append(" ORDER BY A.NOM_AUTOR " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.AutorRevistaArtigo a = new Hcrp.Framework.Classes.AutorRevistaArtigo();
                        a.Nome = Convert.ToString(dr["NOM_AUTOR"]);
                        a.Documento = Convert.ToString(dr["NUM_DOC_AUTOR"]);
                        a.TipoDocumento = (Usuario.ETipoDocumento)Convert.ToInt32(dr["IDF_DOC"]);
                        a.Area = Convert.ToString(dr["DSC_AREA"]);
                        a.AutorPrincipal = Convert.ToInt32(dr["NUM_ORDEM"]) == 0 ? true : false;
                        l.Add(a);
                    }

                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }
	

        public Boolean InserirAtualizar(Hcrp.Framework.Classes.AutorRevistaArtigo Autor)
        {
            Boolean r = false;

            return r;
        }

        public Boolean InserirAtualizarComArtigo(Hcrp.Framework.Classes.AutorRevistaArtigo Autor, long seqArtigo)
        {
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            try
            {
                Boolean r = false;
                {
                    //Gravar na tabela Autor
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig cmdAtualizaAutor = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REVISTA_AUTOR");
                    ctx.Open();
                    if (!String.IsNullOrWhiteSpace(Autor.Nome))
                        cmdAtualizaAutor.Params["NOM_AUTOR"] = Autor.Nome.ToUpper();
                    cmdAtualizaAutor.Params["IDF_DOC"] = (int)Autor.TipoDocumento;
                    cmdAtualizaAutor.FilterParams["NUM_DOC_AUTOR"] = Autor.Documento;
                    ctx.ExecuteUpdate(cmdAtualizaAutor);
                    if (ctx.RowsAffected == 0)
                    {
                        Hcrp.Infra.AcessoDado.CommandConfig cmdInsereAutor = new Hcrp.Infra.AcessoDado.CommandConfig("REVISTA_AUTOR");
                        if (!String.IsNullOrWhiteSpace(Autor.Documento))
                            cmdInsereAutor.Params["NUM_DOC_AUTOR"] = Autor.Documento;
                        if (!String.IsNullOrWhiteSpace(Autor.Nome))
                            cmdInsereAutor.Params["NOM_AUTOR"] = Autor.Nome.ToUpper();
                        cmdInsereAutor.Params["IDF_DOC"] = (int)Autor.TipoDocumento;
                        ctx.ExecuteInsert(cmdInsereAutor);
                    }

                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comandoDelete = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REVISTA_AUTOR_ARTIGO");
                    comandoDelete.Params["SEQ_REVISTA_ARTIGO"] = seqArtigo;
                    comandoDelete.Params["NUM_DOC_AUTOR"] = Autor.Documento;
                    ctx.AllowUnqualifiedUpdates = true;
                    ctx.ExecuteDelete(comandoDelete);
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("REVISTA_AUTOR_ARTIGO");
                    comando.Params["SEQ_REVISTA_ARTIGO"] = seqArtigo;
                    if (!String.IsNullOrWhiteSpace(Autor.Documento))
                        comando.Params["NUM_DOC_AUTOR"] = Autor.Documento;
                    if (!String.IsNullOrWhiteSpace(Autor.Area))
                        comando.Params["DSC_AREA"] = Autor.Area.Replace("–", "&#8212;").Replace("“", "\"").Replace("”", "\"");
                    comando.Params["NUM_ORDEM"] = Autor.AutorPrincipal ? 0 : 1;
                    // Executar o insert
                    ctx.ExecuteInsert(comando);
                    r = true;
                }
                ctx.Commit();
                return r;
            }
            catch (Exception ex)
            {
                ctx.Rollback();
                string erro = ex.Message;
                throw;
            }            
        }
    }
}
