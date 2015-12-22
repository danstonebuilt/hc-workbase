using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.IO;
using System.Web;

namespace Hcrp.Framework.Dal
{
    public class RevistaArtigo : Hcrp.Framework.Classes.RevistaArtigo
    {
        private const string dataInicio = "10/02/2012"; 

        public List<Hcrp.Framework.Classes.RevistaArtigo> BuscarArtigos(List<ETipoSituacaoRevista> lTipoSituacao)
        {
            List<Hcrp.Framework.Classes.RevistaArtigo> l = new List<Hcrp.Framework.Classes.RevistaArtigo>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.SEQ_REVISTA_ARTIGO, NVL(A.SEQ_REVISTA_EDICAO,0) SEQ_REVISTA_EDICAO, A.NUM_USER_AUTOR, " + Environment.NewLine);
                    sb.Append(" A.DTA_SUBMISSAO, A.DSC_TITULO, " + Environment.NewLine);
                    sb.Append(" A.COD_SITUACAO, A.SEQ_REVISTA, A.COD_EIXO_TEMATICO, " + Environment.NewLine);
                    sb.Append(" ( SELECT COUNT(*) FROM REVISTA_ARTIGO_REVISAO X WHERE X.DTA_FINALIZACAO IS NOT NULL AND X.SEQ_REVISTA_ARTIGO = A.SEQ_REVISTA_ARTIGO ) REVISADO, " + Environment.NewLine);
                    sb.Append(" ( SELECT Count(*) FROM REVISTA_ARTIGO_REVISAO S WHERE S.SEQ_REVISTA_ARTIGO = A.SEQ_REVISTA_ARTIGO ) TRIAGENS " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_ARTIGO A " + Environment.NewLine);
                    string situacoes = "";
                    foreach (var sit in lTipoSituacao)
                    {
                        if (situacoes == "")
                            situacoes = situacoes + Convert.ToString((int)sit);
                        else situacoes = situacoes + "," + Convert.ToString((int)sit);
                    }
                    if ((situacoes != "") && (situacoes != "-1"))
                    {
                        sb.Append(" WHERE A.COD_SITUACAO IN (" + situacoes + ")" + Environment.NewLine);
                        sb.Append(" AND TRUNC(A.DTA_SUBMISSAO) > TRUNC(TO_DATE('" + dataInicio + "','DD/MM/YYYY'))" + Environment.NewLine);
                    }
                    else
                    {
                        sb.Append(" AND TRUNC(A.DTA_SUBMISSAO) > TRUNC(TO_DATE('" + dataInicio + "','DD/MM/YYYY'))" + Environment.NewLine);
                    }
                    sb.Append(" ORDER BY A.COD_SITUACAO, A.SEQ_REVISTA_ARTIGO" + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.RevistaArtigo dra = new Hcrp.Framework.Classes.RevistaArtigo();
                        dra.SeqRevista = Convert.ToInt32(dr["SEQ_REVISTA"]);
                        dra.NumArtigo = Convert.ToInt32(dr["SEQ_REVISTA_ARTIGO"]);
                        dra.Titulo = Convert.ToString(dr["DSC_TITULO"]).ToUpper();
                        dra.DataUltimaSubmissao = Convert.ToDateTime(dr["DTA_SUBMISSAO"]);
                        if (Convert.ToInt32(dr["SEQ_REVISTA_EDICAO"]) > 0)
                            dra.SeqRevistaEdicao = Convert.ToInt32(dr["SEQ_REVISTA_EDICAO"]);
                        dra.NumUserAutor = Convert.ToInt32(dr["NUM_USER_AUTOR"]);
                        dra.CodSituacao = Convert.ToInt32(dr["COD_SITUACAO"]);
                        dra.Triagens = Convert.ToString(dr["TRIAGENS"]);
                        if ((dr["COD_EIXO_TEMATICO"] != DBNull.Value))
                            dra.CodEixoTematico = Convert.ToInt32(dr["COD_EIXO_TEMATICO"]);
                        if ((Convert.ToInt32(dr["REVISADO"]) > 0) && (Convert.ToInt32(dr["COD_SITUACAO"]) == (int)Hcrp.Framework.Classes.RevistaArtigo.ETipoSituacaoRevista.EmTriagem))
                            dra.Revisado = @"http://10.165.5.50/InterfaceHC/imagens/Liberado.gif";
                        else dra.Revisado = "";
                        l.Add(dra);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void BuscarPorCodigo(Hcrp.Framework.Classes.RevistaArtigo artigo, int id)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.SEQ_REVISTA_ARTIGO, NVL(A.SEQ_REVISTA_EDICAO,0) SEQ_REVISTA_EDICAO, A.NUM_USER_AUTOR, " + Environment.NewLine);
                    sb.Append(" A.DTA_SUBMISSAO, A.DSC_TITULO, " + Environment.NewLine);
                    sb.Append(" A.COD_SITUACAO, A.SEQ_REVISTA, A.COD_EIXO_TEMATICO " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_ARTIGO A " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_REVISTA_ARTIGO = " + Convert.ToString(id) + Environment.NewLine);
                    sb.Append(" AND TRUNC(A.DTA_SUBMISSAO) > TRUNC(TO_DATE('" + dataInicio + "','DD/MM/YYYY'))" + Environment.NewLine);
                    sb.Append(" ORDER BY A.SEQ_REVISTA_ARTIGO" + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        artigo.SeqRevista = Convert.ToInt32(dr["SEQ_REVISTA"]);
                        artigo.NumArtigo = Convert.ToInt32(dr["SEQ_REVISTA_ARTIGO"]);
                        artigo.Titulo = Convert.ToString(dr["DSC_TITULO"]);
                        artigo.DataUltimaSubmissao = Convert.ToDateTime(dr["DTA_SUBMISSAO"]);
                        if (Convert.ToInt32(dr["SEQ_REVISTA_EDICAO"]) > 0)
                            artigo.SeqRevistaEdicao = Convert.ToInt32(dr["SEQ_REVISTA_EDICAO"]);
                        artigo.NumUserAutor = Convert.ToInt32(dr["NUM_USER_AUTOR"]);
                        artigo.CodSituacao = Convert.ToInt32(dr["COD_SITUACAO"]);
                        if ((dr["COD_EIXO_TEMATICO"] != DBNull.Value))
                            artigo.CodEixoTematico = Convert.ToInt32(dr["COD_EIXO_TEMATICO"]);
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public String CarregarEmHtml(Hcrp.Framework.Classes.RevistaArtigo artigo, int id, char ShowAutor)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.SEQ_REVISTA_ARTIGO, NVL(A.SEQ_REVISTA_EDICAO,0) SEQ_REVISTA_EDICAO, A.NUM_USER_AUTOR, " + Environment.NewLine);
                    sb.Append(" A.DTA_SUBMISSAO, A.DSC_TITULO, " + Environment.NewLine);
                    sb.Append(" A.COD_SITUACAO, A.SEQ_REVISTA, A.COD_EIXO_TEMATICO  " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_ARTIGO A " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_REVISTA_ARTIGO = " + Convert.ToString(id) + Environment.NewLine);
                    sb.Append(" AND TRUNC(A.DTA_SUBMISSAO) > TRUNC(TO_DATE('" + dataInicio + "','DD/MM/YYYY'))" + Environment.NewLine);
                    sb.Append(" ORDER BY A.SEQ_REVISTA_ARTIGO" + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        artigo.SeqRevista = Convert.ToInt32(dr["SEQ_REVISTA"]);
                        artigo.NumArtigo = Convert.ToInt32(dr["SEQ_REVISTA_ARTIGO"]);
                        artigo.Titulo = Convert.ToString(dr["DSC_TITULO"]);
                        artigo.DataUltimaSubmissao = Convert.ToDateTime(dr["DTA_SUBMISSAO"]);
                        if (Convert.ToInt32(dr["SEQ_REVISTA_EDICAO"]) > 0)
                            artigo.SeqRevistaEdicao = Convert.ToInt32(dr["SEQ_REVISTA_EDICAO"]);
                        artigo.NumUserAutor = Convert.ToInt32(dr["NUM_USER_AUTOR"]);
                        artigo.CodSituacao = Convert.ToInt32(dr["COD_SITUACAO"]);
                        if ((dr["COD_EIXO_TEMATICO"] != DBNull.Value))
                            artigo.CodEixoTematico = Convert.ToInt32(dr["COD_EIXO_TEMATICO"]);
                    }

                    StringBuilder sbArtigo = new StringBuilder();
                    //Título
                    sbArtigo.Append("<table cellpadding=\"0\" class=\"tabelaArtigo\">" + Environment.NewLine);
                    sbArtigo.Append("    <tr>" + Environment.NewLine);
                    sbArtigo.Append("        <td class=\"artigoCapition\">" + Environment.NewLine);
                    sbArtigo.Append("            <strong>Título</strong></td>" + Environment.NewLine);
                    sbArtigo.Append("        <td class=\"artigoBtItalico\">" + Environment.NewLine);
                    sbArtigo.Append("            &nbsp;</td>" + Environment.NewLine);
                    sbArtigo.Append("    </tr>" + Environment.NewLine);
                    sbArtigo.Append("    <tr>" + Environment.NewLine);
                    sbArtigo.Append("        <td class=\"artigoTextBox\" colspan=\"2\">" + Environment.NewLine);
                    sbArtigo.Append("            " + artigo.Titulo + " " + Environment.NewLine);
                    sbArtigo.Append("            </td>" + Environment.NewLine);
                    sbArtigo.Append("    </tr>" + Environment.NewLine);
                    sbArtigo.Append(" </table>" + Environment.NewLine);
                    sbArtigo.Append("<br />" + Environment.NewLine);

                    //Autores
                    if (ShowAutor == 'S')
                    {

                        List<Hcrp.Framework.Classes.AutorRevistaArtigo> lAutor = new Hcrp.Framework.Dal.AutorRevistaArtigo().BuscarAutoresDoArtigo((Convert.ToInt32(id)));

                        sbArtigo.Append("<table cellpadding=\"0\" class=\"tabelaArtigo\">" + Environment.NewLine);
                        sbArtigo.Append("    <tr>" + Environment.NewLine);
                        sbArtigo.Append("        <td class=\"artigoCapition\">" + Environment.NewLine);
                        sbArtigo.Append("            <strong>Autores</strong></td>" + Environment.NewLine);
                        sbArtigo.Append("        <td class=\"artigoBtItalico\">" + Environment.NewLine);
                        sbArtigo.Append("            &nbsp;</td>" + Environment.NewLine);
                        sbArtigo.Append("    </tr>" + Environment.NewLine);
                        sbArtigo.Append("    <tr>" + Environment.NewLine);
                        sbArtigo.Append("        <td class=\"artigoTextBox\" colspan=\"2\">" + Environment.NewLine);

                        //tabela de autores
                        sbArtigo.Append("<table class=\"GridView\" cellspacing=\"0\" rules=\"all\" border=\"1\" id=\"gv_autores\" style=\"border-collapse:collapse;\">" + Environment.NewLine);
                        sbArtigo.Append("<tr>" + Environment.NewLine);
                        sbArtigo.Append("<th scope=\"col\">Nome</th><th class=\"oculta\" scope=\"col\">&nbsp;</th>" + Environment.NewLine);
                        sbArtigo.Append("<th scope=\"col\"><center>Área</center></th>" + Environment.NewLine);
                        sbArtigo.Append("<th scope=\"col\"><center>Autor Principal</center></th>" + Environment.NewLine);
                        sbArtigo.Append("</tr>" + Environment.NewLine);

                        foreach (Hcrp.Framework.Classes.AutorRevistaArtigo Autor in lAutor)
                        {
                            string sCorrespondente = string.Empty;
                            Usuario U = new Usuario();
                            U.BuscarUsuarioCodigo(artigo.NumUserAutor);
                            if ((Autor.TipoDocumento == U.TipoDocumento) && (Autor.Documento == U.Documento))
                            {
                                sCorrespondente = " (CORRESPONDENTE)";   
                            }
                            sbArtigo.Append("<tr>");
                            sbArtigo.Append("  <td>" + Autor.Nome + "&nbsp;" + sCorrespondente + "</td>");
                            sbArtigo.Append("  <td><center>" + Autor.Area + "</center></td>");
                            sbArtigo.Append("  <td><center><input type='radio' disabled='true' " + (Autor.AutorPrincipal ? " checked='true'" : "") + "></input></center></td>");
                            sbArtigo.Append("</tr>");
                        }

                        sbArtigo.Append("</table>" + Environment.NewLine);

                        sbArtigo.Append("            </td>" + Environment.NewLine);
                        sbArtigo.Append("    </tr>" + Environment.NewLine);
                        sbArtigo.Append(" </table>" + Environment.NewLine);
                        sbArtigo.Append("<br />" + Environment.NewLine);
                    }

                    //Anexos
                    sbArtigo.Append("<table cellpadding=\"0\" class=\"tabelaArtigo\">" + Environment.NewLine);
                    sbArtigo.Append("    <tr>" + Environment.NewLine);
                    sbArtigo.Append("        <td class=\"artigoCapition\">" + Environment.NewLine);
                    sbArtigo.Append("            <strong>Arquivos</strong></td>" + Environment.NewLine);
                    sbArtigo.Append("        <td class=\"artigoBtItalico\">" + Environment.NewLine);
                    sbArtigo.Append("            &nbsp;</td>" + Environment.NewLine);
                    sbArtigo.Append("    </tr>" + Environment.NewLine);
                    foreach (var item in artigo.Anexos)
                    {
                        sbArtigo.Append("    <tr>" + Environment.NewLine);
                        sbArtigo.Append("        <td class=\"artigoTextBox\" colspan=\"2\">" + Environment.NewLine);
                        sbArtigo.Append("            " + @"<a href='..\Uploads\Artigos\" + Convert.ToString(artigo.NumArtigo) + @"\Anexos\" + item.CaminhoAnexo + "' target='_blank' >" + item.Descricao + " </a>" + Environment.NewLine);
                        sbArtigo.Append("            </td>" + Environment.NewLine);
                        sbArtigo.Append("    </tr>" + Environment.NewLine);

                    }
                    sbArtigo.Append(" </table>" + Environment.NewLine);
                    sbArtigo.Append("<br />" + Environment.NewLine);


                    return sbArtigo.ToString();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Hcrp.Framework.Classes.RevistaArtigo> BuscarMeusArtigos(int num_user_banco, int seq_revista)
        {
            List<Hcrp.Framework.Classes.RevistaArtigo> l = new List<Hcrp.Framework.Classes.RevistaArtigo>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.SEQ_REVISTA_ARTIGO, A.SEQ_REVISTA_EDICAO, A.NUM_USER_AUTOR, " + Environment.NewLine);
                    sb.Append(" A.DTA_SUBMISSAO, A.DSC_TITULO, " + Environment.NewLine);
                    sb.Append(" A.COD_SITUACAO, A.SEQ_REVISTA, A.COD_EIXO_TEMATICO " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_ARTIGO A " + Environment.NewLine);
                    sb.Append(" WHERE A.NUM_USER_AUTOR = " + Convert.ToString(num_user_banco) + Environment.NewLine);
                    sb.Append(" AND A.SEQ_REVISTA = " + Convert.ToString(seq_revista) + Environment.NewLine);
                    sb.Append(" AND TRUNC(A.DTA_SUBMISSAO) > TRUNC(TO_DATE('" + dataInicio + "','DD/MM/YYYY'))" + Environment.NewLine);
                    sb.Append(" ORDER BY A.SEQ_REVISTA_ARTIGO DESC" + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.RevistaArtigo artigo = new Hcrp.Framework.Classes.RevistaArtigo();
                        artigo.NumArtigo = Convert.ToInt32(dr["SEQ_REVISTA_ARTIGO"]);
                        if ((dr["SEQ_REVISTA_EDICAO"] != DBNull.Value))
                            artigo.SeqRevistaEdicao = Convert.ToInt32(dr["SEQ_REVISTA_EDICAO"]);
                        artigo.NumUserAutor = Convert.ToInt32(dr["NUM_USER_AUTOR"]);
                        artigo.DataUltimaSubmissao = Convert.ToDateTime(dr["DTA_SUBMISSAO"]);
                        artigo.Titulo = Convert.ToString(dr["DSC_TITULO"]);
                        artigo.CodSituacao = Convert.ToInt32(dr["COD_SITUACAO"]);
                        artigo.SeqRevista = Convert.ToInt32(dr["SEQ_REVISTA"]);
                        if ((dr["COD_EIXO_TEMATICO"] != DBNull.Value))
                            artigo.CodEixoTematico = Convert.ToInt32(dr["COD_EIXO_TEMATICO"]);
                        l.Add(artigo);
                    }
                    return l;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Hcrp.Framework.Classes.RevistaArtigo> BuscarMeusArtigosARevisar(int num_user_banco, int seq_revista)
        {
            List<Hcrp.Framework.Classes.RevistaArtigo> l = new List<Hcrp.Framework.Classes.RevistaArtigo>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.SEQ_REVISTA_ARTIGO, A.SEQ_REVISTA_EDICAO, A.NUM_USER_AUTOR, " + Environment.NewLine);
                    sb.Append(" A.DTA_SUBMISSAO, A.DSC_TITULO, " + Environment.NewLine);
                    sb.Append(" A.COD_SITUACAO, A.SEQ_REVISTA, A.COD_EIXO_TEMATICO " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_ARTIGO A, REVISTA_ARTIGO_REVISAO B " + Environment.NewLine);
                    sb.Append(" WHERE B.NUM_USER_REVISAO = " + Convert.ToString(num_user_banco) + Environment.NewLine);
                    sb.Append(" AND A.SEQ_REVISTA_ARTIGO = B.SEQ_REVISTA_ARTIGO " + Environment.NewLine);
                    sb.Append(" AND A.SEQ_REVISTA = " + Convert.ToString(seq_revista) + Environment.NewLine);
                    sb.Append(" AND TRUNC(A.DTA_SUBMISSAO) > TRUNC(TO_DATE('" + dataInicio + "','DD/MM/YYYY'))" + Environment.NewLine);
                    sb.Append(" AND (B.IDF_ACEITACAO <> 0 OR B.IDF_ACEITACAO IS NULL)" + Environment.NewLine);
                    sb.Append(" AND B.IDF_AVALIACAO IS NULL " + Environment.NewLine);
                    sb.Append(" ORDER BY A.SEQ_REVISTA_ARTIGO DESC" + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.RevistaArtigo artigo = new Hcrp.Framework.Classes.RevistaArtigo();
                        artigo.SeqRevista = Convert.ToInt32(dr["SEQ_REVISTA"]);
                        artigo.NumArtigo = Convert.ToInt32(dr["SEQ_REVISTA_ARTIGO"]);
                        artigo.Titulo = Convert.ToString(dr["DSC_TITULO"]);
                        artigo.DataUltimaSubmissao = Convert.ToDateTime(dr["DTA_SUBMISSAO"]);
                        if (!String.IsNullOrEmpty(Convert.ToString(dr["SEQ_REVISTA_EDICAO"])))
                        {
                            artigo.SeqRevistaEdicao = Convert.ToInt32(dr["SEQ_REVISTA_EDICAO"]);
                        }
                        artigo.NumUserAutor = Convert.ToInt32(dr["NUM_USER_AUTOR"]);
                        artigo.CodSituacao = Convert.ToInt32(dr["COD_SITUACAO"]);
                        if ((dr["COD_EIXO_TEMATICO"] != DBNull.Value))
                            artigo.CodEixoTematico = Convert.ToInt32(dr["COD_EIXO_TEMATICO"]);
                        l.Add(artigo);
                    }
                    return l;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Hcrp.Framework.Classes.RevistaArtigo.ETipoSituacaoRevista BuscaSituacaoAtual(long codArtigo)
        {
            ETipoSituacaoRevista situacao = new ETipoSituacaoRevista();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.COD_SITUACAO " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_ARTIGO A " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_REVISTA_ARTIGO = :SEQ_REVISTA_ARTIGO " + Environment.NewLine);
                    sb.Append(" AND TRUNC(A.DTA_SUBMISSAO) > TRUNC(TO_DATE('" + dataInicio + "','DD/MM/YYYY'))" + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_REVISTA_ARTIGO"] = codArtigo;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        situacao = (ETipoSituacaoRevista)Convert.ToInt32(dr["COD_SITUACAO"]);
                    }
                }
                return situacao;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public long InserirAtualizar(Hcrp.Framework.Classes.RevistaArtigo Artigo)
        {
            if ((Artigo.NumArtigo == null) || (Artigo.NumArtigo == 0)) //Inserir
            {
                try
                {
                    long retorno = 0;
                    using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                    {
                        // Abrir conexão
                        ctx.Open();

                        // Preparar o comando
                        Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("REVISTA_ARTIGO");
                        if (Artigo.Edicao.SeqEdicao > 0)
                            comando.Params["SEQ_REVISTA_EDICAO"] = Artigo.Edicao.SeqEdicao;

                        comando.Params["NUM_USER_AUTOR"] = new Hcrp.Framework.Classes.UsuarioConexao().NumUserBanco;

                        comando.Params["DTA_SUBMISSAO"] = DateTime.Now;

                        if (!String.IsNullOrWhiteSpace(Artigo.Titulo))
                            comando.Params["DSC_TITULO"] = Artigo.Titulo.Replace("–", "&#8212;").Replace("“", "\"").Replace("”", "\"");

                        if (Artigo.SituacaoAtual.CodSituacao > 0)
                            comando.Params["COD_SITUACAO"] = Artigo.SituacaoAtual.CodSituacao;

                        if (Artigo.SeqRevista > 0)
                            comando.Params["SEQ_REVISTA"] = Artigo.Revista.SeqRevista;

                        // Executar o insert
                        ctx.ExecuteInsert(comando);

                        // Pegar o último ID
                        retorno = ctx.GetSequenceValue("GENERICO.SEQ_REVISTA_ARTIGO", false);

                        if ((Artigo.AnexosExcluir != null) && (Artigo.AnexosExcluir.Count > 0))
                        {
                            Artigo.ExcluirAnexosArtigo();
                        }
                        foreach (var ItemAnexo in Artigo.Anexos)
                        {
                            ItemAnexo.InserirAtualizarComArtigo(retorno);
                        }

                        Artigo.ExcluirAutoresArtigo();
                        foreach (var item in Artigo.Autores)
                        {
                            item.InserirAtualizarComArtigo(retorno);
                        }
                    }
                    return retorno;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else //Atualizar
            {
                try
                {
                    long retorno = 0;
                    using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                    {
                        // Abrir conexão
                        ctx.Open();

                        // Preparar o comando
                        Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REVISTA_ARTIGO");
                        //comando.Params["SEQ_REVISTA_EDICAO"] = Artigo.Edicao.SeqEdicao;
                        //comando.Params["NUM_USER_AUTOR"] = new Hcrp.Framework.Classes.UsuarioConexao().NumUserBanco;
                        comando.Params["DTA_SUBMISSAO"] = DateTime.Now;

                        if (!String.IsNullOrWhiteSpace(Artigo.Titulo))
                            comando.Params["DSC_TITULO"] = Artigo.Titulo.Replace("–", "&#8212;").Replace("“", "\"").Replace("”", "\"");

                        comando.Params["COD_SITUACAO"] = Artigo.SituacaoAtual.CodSituacao;
                        comando.Params["SEQ_REVISTA"] = Artigo.Revista.SeqRevista;

                        comando.FilterParams["SEQ_REVISTA_ARTIGO"] = Artigo.NumArtigo;

                        // Pegar o último ID
                        retorno = Artigo.NumArtigo;

                        if ((BuscaSituacaoAtual(retorno) == ETipoSituacaoRevista.EmEdicao) || (BuscaSituacaoAtual(retorno) == ETipoSituacaoRevista.AprovadoComCorrecoes))
                        {
                            if ((Artigo.AnexosExcluir != null) && (Artigo.AnexosExcluir.Count > 0))
                            {
                                Artigo.ExcluirAnexosArtigo();
                            }
                            foreach (var ItemAnexo in Artigo.Anexos)
                            {
                                ItemAnexo.InserirAtualizarComArtigo(retorno);
                            }

                            Artigo.ExcluirAutoresArtigo();
                            foreach (var item in Artigo.Autores)
                            {
                                item.InserirAtualizarComArtigo(retorno);
                            }
                        }

                        // Executar o update
                        ctx.ExecuteUpdate(comando);
                    }
                    return retorno;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        public void Excluir(Hcrp.Framework.Classes.RevistaArtigo Artigo)
        {
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                try
                {
                    ctx.Open();

                    ctx.BeginTransaction();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("REVISTA_AUTOR_ARTIGO");
                    // Filtrar o registro a ser atualizado
                    comando.Params["SEQ_REVISTA_ARTIGO"] = Artigo.NumArtigo;
                    // Executar a exclusão
                    ctx.ExecuteDelete(comando);

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando2 = new Hcrp.Infra.AcessoDado.CommandConfig("REVISTA_ARTIGO_ANEXO");
                    // Filtrar o registro a ser atualizado
                    comando2.Params["SEQ_REVISTA_ARTIGO"] = Artigo.NumArtigo;
                    // Executar a exclusão
                    ctx.ExecuteDelete(comando2);

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando3 = new Hcrp.Infra.AcessoDado.CommandConfig("REVISTA_ARTIGO");
                    // Filtrar o registro a ser atualizado
                    comando3.Params["SEQ_REVISTA_ARTIGO"] = Artigo.NumArtigo;
                    // Executar a exclusão
                    ctx.ExecuteDelete(comando3);

                    ctx.Commit();

                }
                catch (Exception ex)
                {
                    string erro = ex.Message;
                    ctx.Rollback();
                }
        }


        public long AtualizarStatus(Hcrp.Framework.Classes.RevistaArtigo Artigo)
        {
            try
            {
                long retorno = 0;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REVISTA_ARTIGO");
                    if ((Artigo.CodEixoTematico != null) && (Artigo.CodEixoTematico > 0))
                        comando.Params["COD_EIXO_TEMATICO"] = Artigo.CodEixoTematico;

                    comando.Params["COD_SITUACAO"] = Artigo.SituacaoAtual.CodSituacao;

                    comando.FilterParams["SEQ_REVISTA_ARTIGO"] = Artigo.NumArtigo;

                    // Executar o insert
                    ctx.ExecuteUpdate(comando);

                    // Pegar o último ID
                    retorno = Artigo.NumArtigo;
                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public long AtualizarEdicao(Hcrp.Framework.Classes.RevistaArtigo Artigo)
        {
            try
            {
                long retorno = 0;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REVISTA_ARTIGO");
                    comando.Params["SEQ_REVISTA_EDICAO"] = Artigo.SeqRevistaEdicao;

                    comando.FilterParams["SEQ_REVISTA_ARTIGO"] = Artigo.NumArtigo;

                    // Executar o insert
                    ctx.ExecuteUpdate(comando);

                    // Pegar o último ID
                    retorno = Artigo.NumArtigo;
                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int QtdRevisoresSelecionados(Hcrp.Framework.Classes.RevistaArtigo Artigo)
        {
            int retorno = 0;
            List<Hcrp.Framework.Classes.UsuarioRevisorRevista> lu = new List<Hcrp.Framework.Classes.UsuarioRevisorRevista>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(@" SELECT COUNT(*) QTD ");
                    sb.Append(@" FROM REVISTA_ARTIGO_REVISAO A ");
                    sb.Append(@" WHERE A.SEQ_REVISTA_ARTIGO = :SEQ_REVISTA_ARTIGO ");
                    sb.Append(@"   AND ((A.IDF_ACEITACAO IS NULL) OR (A.IDF_ACEITACAO = 1)) ");

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_REVISTA_ARTIGO"] = Artigo.NumArtigo;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        retorno = Convert.ToInt32(dr["QTD"]);
                    }
                }
                return retorno;
            }
            catch (Exception)
            {
                return retorno;
            }
        }


        public Boolean ExcluirAutoresArtigo(Hcrp.Framework.Classes.RevistaArtigo Artigo)
        {
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                try
                {
                    Boolean r = false;
                    {
                        ctx.Open();
                        Hcrp.Infra.AcessoDado.UpdateCommandConfig comandoDelete = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REVISTA_AUTOR_ARTIGO");
                        comandoDelete.Params["SEQ_REVISTA_ARTIGO"] = Artigo.NumArtigo;
                        ctx.AllowUnqualifiedUpdates = true;
                        ctx.ExecuteDelete(comandoDelete);
                        r = true;
                    }
                    return r;
                }
                catch (Exception ex)
                {
                    string erro = ex.Message;
                    throw;
                }
        }


        public Boolean ExcluirAnexosArtigo(Hcrp.Framework.Classes.RevistaArtigo Artigo)
        {
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                try
                {
                    Boolean r = false;
                    foreach (var itemAnexoExcluir in Artigo.AnexosExcluir)
                    {
                        if (Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("..\\Uploads\\Artigos\\" + Convert.ToString(Artigo.NumArtigo)) + "\\Anexos\\"))
                        {
                            string[] arqDelete = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath("..\\Uploads\\Artigos\\" + Convert.ToString(Artigo.NumArtigo)) + "\\Anexos\\", itemAnexoExcluir.CaminhoAnexo);
                            foreach (string f in arqDelete)
                            {
                                File.Delete(f);
                            }
                        }
                        ctx.Open();
                        Hcrp.Infra.AcessoDado.UpdateCommandConfig comandoDelete = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REVISTA_ARTIGO_ANEXO");
                        comandoDelete.Params["SEQ_REVISTA_ARTIGO"] = Artigo.NumArtigo;
                        comandoDelete.Params["DSC_CAMINHO_ANEXO"] = itemAnexoExcluir.CaminhoAnexo;
                        ctx.AllowUnqualifiedUpdates = true;
                        ctx.ExecuteDelete(comandoDelete);
                        r = true;
                    }
                    return r;
                }
                catch (Exception ex)
                {
                    string erro = ex.Message;
                    return false;
                    //throw;
                }
        }


        public List<Hcrp.Framework.Classes.RevistaArtigo> BuscarPorEdicao(Hcrp.Framework.Classes.RevistaEdicao edicao)
        {
            List<Hcrp.Framework.Classes.RevistaArtigo> l = new List<Hcrp.Framework.Classes.RevistaArtigo>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.SEQ_REVISTA_ARTIGO, NVL(A.SEQ_REVISTA_EDICAO,0) SEQ_REVISTA_EDICAO, A.NUM_USER_AUTOR, " + Environment.NewLine);
                    sb.Append(" A.DTA_SUBMISSAO, A.DSC_TITULO, " + Environment.NewLine);
                    sb.Append(" A.COD_SITUACAO, A.SEQ_REVISTA, A.COD_EIXO_TEMATICO " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_ARTIGO A " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_REVISTA_EDICAO = :SEQ_REVISTA_EDICAO " + Environment.NewLine);
                    sb.Append(" ORDER BY fnc_tira_acentos(dbms_lob.substr(trim(a.dsc_titulo), 50, 1))" + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_REVISTA_EDICAO"] = edicao.SeqEdicao;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.RevistaArtigo dra = new Hcrp.Framework.Classes.RevistaArtigo();
                        dra.SeqRevista = Convert.ToInt32(dr["SEQ_REVISTA"]);
                        dra.NumArtigo = Convert.ToInt32(dr["SEQ_REVISTA_ARTIGO"]);
                        dra.Titulo = Convert.ToString(dr["DSC_TITULO"]);
                        dra.DataUltimaSubmissao = Convert.ToDateTime(dr["DTA_SUBMISSAO"]);
                        if (Convert.ToInt32(dr["SEQ_REVISTA_EDICAO"]) > 0)
                            dra.SeqRevistaEdicao = Convert.ToInt32(dr["SEQ_REVISTA_EDICAO"]);
                        dra.NumUserAutor = Convert.ToInt32(dr["NUM_USER_AUTOR"]);
                        dra.CodSituacao = Convert.ToInt32(dr["COD_SITUACAO"]);
                        if ((dr["COD_EIXO_TEMATICO"] != DBNull.Value))
                            dra.CodEixoTematico = Convert.ToInt32(dr["COD_EIXO_TEMATICO"]);
                        l.Add(dra);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public long AtualizarRevisao(Hcrp.Framework.Classes.RevistaArtigo Artigo)
        {
            try
            {
                long retorno = 0;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    if (Convert.ToInt32(Artigo.NumArtigo) > 0)
                    {
                        // Preparar o comando
                        Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REVISTA_ARTIGO");
                        comando.Params["COD_SITUACAO"] = Artigo.SituacaoAtual.CodSituacao;
                        comando.FilterParams["SEQ_REVISTA_ARTIGO"] = Artigo.NumArtigo;

                        // Executar o update
                        ctx.ExecuteUpdate(comando);
                    }

                    // Pegar o ID
                    retorno = Artigo.NumArtigo;

                    if ((Artigo.AnexosExcluir != null) && (Artigo.AnexosExcluir.Count > 0))
                    {
                        Artigo.ExcluirAnexosArtigo();
                    }
                    foreach (var ItemAnexo in Artigo.Anexos)
                    {
                        ItemAnexo.InserirAtualizarComArtigo(retorno);
                    }

                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
