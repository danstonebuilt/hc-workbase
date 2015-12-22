using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class SiteInformacao : Hcrp.Framework.Classes.SiteInformacao
    {
        public Hcrp.Framework.Classes.SiteInformacao BuscaSuperDestaque()
        {
            try
            {
                Hcrp.Framework.Classes.SiteInformacao I = new Hcrp.Framework.Classes.SiteInformacao();
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT * FROM " + Environment.NewLine);
                    sb.Append(" ( " + Environment.NewLine);
                    sb.Append(" SELECT ROWNUM LINHA, X.* FROM " + Environment.NewLine);
                    sb.Append(" ( " + Environment.NewLine);
                    sb.Append(" SELECT A.* FROM SITE_INFORMACAO A " + Environment.NewLine);
                    sb.Append(" WHERE SYSDATE BETWEEN A.DTA_HOR_PUBLICACAO AND A.DTA_HOR_EXPIRACAO " + Environment.NewLine);
                    sb.Append(" ORDER BY A.DTA_HOR_PUBLICACAO DESC " + Environment.NewLine);
                    sb.Append(" )X " + Environment.NewLine);
                    sb.Append(" )Y " + Environment.NewLine);
                    sb.Append(" WHERE LINHA <= 3 " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        if (dr["NUM_USER_PUBLICACAO"] != DBNull.Value)
                            I._NumUserPublicacao = Convert.ToInt32(dr["NUM_USER_PUBLICACAO"]);
                        if (dr["SEQ_SITE_INFORMACAO"] != DBNull.Value)
                            I.Seq = Convert.ToInt32(dr["SEQ_SITE_INFORMACAO"]);
                        if (dr["DTA_HOR_CADASTRO"] != DBNull.Value)
                            I.DataCadastro = Convert.ToDateTime(dr["DTA_HOR_CADASTRO"]);
                        if (dr["DTA_HOR_PUBLICACAO"] != DBNull.Value)
                            I.DataPublicacao = Convert.ToDateTime(dr["DTA_HOR_PUBLICACAO"]);
                        if (dr["DTA_HOR_EXPIRACAO"] != DBNull.Value)
                            I.DataExpiracao = Convert.ToDateTime(dr["DTA_HOR_EXPIRACAO"]);
                        if (dr["DTA_INFORMACAO"] != DBNull.Value)
                            I.DataInformacao = Convert.ToDateTime(dr["DTA_INFORMACAO"]);
                        I.Titulo = Convert.ToString(dr["DSC_TITULO"]);
                        I.Resumo = Convert.ToString(dr["DSC_RESUMO"]);
                        I.Corpo = Convert.ToString(dr["DSC_INFORMACAO"]);
                        I.PalavrasChave = Convert.ToString(dr["DSC_PALAVRA_CHAVE"]);
                        if (dr["SEQ_GALERIA_IMG_CHAMADA"] != DBNull.Value)
                            I._SeqSiteInformacaoGaleria = Convert.ToInt32(dr["SEQ_GALERIA_IMG_CHAMADA"]);
                    }
                }
                return I;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Hcrp.Framework.Classes.SiteInformacao> BuscaDestaques()
        {
            try
            {
                List<Hcrp.Framework.Classes.SiteInformacao> L = new List<Hcrp.Framework.Classes.SiteInformacao>();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT * FROM " + Environment.NewLine);
                    sb.Append(" ( " + Environment.NewLine);
                    sb.Append(" SELECT ROWNUM LINHA, X.* FROM " + Environment.NewLine);
                    sb.Append(" ( " + Environment.NewLine);
                    sb.Append(" SELECT A.* FROM SITE_INFORMACAO A " + Environment.NewLine);
                    sb.Append(" WHERE SYSDATE BETWEEN A.DTA_HOR_PUBLICACAO AND A.DTA_HOR_EXPIRACAO " + Environment.NewLine);
                    sb.Append(" ORDER BY A.DTA_HOR_PUBLICACAO DESC " + Environment.NewLine);
                    sb.Append(" )X " + Environment.NewLine);
                    sb.Append(" )Y " + Environment.NewLine);
                    sb.Append(" WHERE LINHA BETWEEN 2 AND 5 " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.SiteInformacao I = new Hcrp.Framework.Classes.SiteInformacao();
                        if (dr["NUM_USER_PUBLICACAO"] != DBNull.Value)
                            I._NumUserPublicacao = Convert.ToInt32(dr["NUM_USER_PUBLICACAO"]);
                        if (dr["SEQ_SITE_INFORMACAO"] != DBNull.Value)
                            I.Seq = Convert.ToInt32(dr["SEQ_SITE_INFORMACAO"]);
                        if (dr["DTA_HOR_CADASTRO"] != DBNull.Value)
                            I.DataCadastro = Convert.ToDateTime(dr["DTA_HOR_CADASTRO"]);
                        if (dr["DTA_HOR_PUBLICACAO"] != DBNull.Value)
                            I.DataPublicacao = Convert.ToDateTime(dr["DTA_HOR_PUBLICACAO"]);
                        if (dr["DTA_HOR_EXPIRACAO"] != DBNull.Value)
                            I.DataExpiracao = Convert.ToDateTime(dr["DTA_HOR_EXPIRACAO"]);
                        if (dr["DTA_INFORMACAO"] != DBNull.Value)
                            I.DataInformacao = Convert.ToDateTime(dr["DTA_INFORMACAO"]);
                        I.Titulo = Convert.ToString(dr["DSC_TITULO"]);
                        I.Resumo = Convert.ToString(dr["DSC_RESUMO"]);
                        I.Corpo = Convert.ToString(dr["DSC_INFORMACAO"]);
                        I.PalavrasChave = Convert.ToString(dr["DSC_PALAVRA_CHAVE"]);
                        if (dr["SEQ_GALERIA_IMG_CHAMADA"] != DBNull.Value)
                            I._SeqSiteInformacaoGaleria = Convert.ToInt32(dr["SEQ_GALERIA_IMG_CHAMADA"]);
                        L.Add(I);
                    }
                }
                return L;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Hcrp.Framework.Classes.SiteInformacao> BuscaMiniDestaques()
        {
            try
            {
                List<Hcrp.Framework.Classes.SiteInformacao> L = new List<Hcrp.Framework.Classes.SiteInformacao>();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT * FROM " + Environment.NewLine);
                    sb.Append(" ( " + Environment.NewLine);
                    sb.Append(" SELECT ROWNUM LINHA, X.* FROM " + Environment.NewLine);
                    sb.Append(" ( " + Environment.NewLine);
                    sb.Append(" SELECT A.* FROM SITE_INFORMACAO A " + Environment.NewLine);
                    sb.Append(" WHERE SYSDATE BETWEEN A.DTA_HOR_PUBLICACAO AND A.DTA_HOR_EXPIRACAO " + Environment.NewLine);
                    sb.Append(" ORDER BY A.DTA_HOR_PUBLICACAO DESC " + Environment.NewLine);
                    sb.Append(" )X " + Environment.NewLine);
                    sb.Append(" )Y " + Environment.NewLine);
                    sb.Append(" WHERE LINHA BETWEEN 6 AND 15 " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.SiteInformacao I = new Hcrp.Framework.Classes.SiteInformacao();
                        if (dr["NUM_USER_PUBLICACAO"] != DBNull.Value)
                            I._NumUserPublicacao = Convert.ToInt32(dr["NUM_USER_PUBLICACAO"]);
                        if (dr["SEQ_SITE_INFORMACAO"] != DBNull.Value)
                            I.Seq = Convert.ToInt32(dr["SEQ_SITE_INFORMACAO"]);
                        if (dr["DTA_HOR_CADASTRO"] != DBNull.Value)
                            I.DataCadastro = Convert.ToDateTime(dr["DTA_HOR_CADASTRO"]);
                        if (dr["DTA_HOR_PUBLICACAO"] != DBNull.Value)
                            I.DataPublicacao = Convert.ToDateTime(dr["DTA_HOR_PUBLICACAO"]);
                        if (dr["DTA_HOR_EXPIRACAO"] != DBNull.Value)
                            I.DataExpiracao = Convert.ToDateTime(dr["DTA_HOR_EXPIRACAO"]);
                        if (dr["DTA_INFORMACAO"] != DBNull.Value)
                            I.DataInformacao = Convert.ToDateTime(dr["DTA_INFORMACAO"]);
                        I.Titulo = Convert.ToString(dr["DSC_TITULO"]);
                        I.Resumo = Convert.ToString(dr["DSC_RESUMO"]);
                        I.Corpo = Convert.ToString(dr["DSC_INFORMACAO"]);
                        I.PalavrasChave = Convert.ToString(dr["DSC_PALAVRA_CHAVE"]);
                        if (dr["SEQ_GALERIA_IMG_CHAMADA"] != DBNull.Value)
                            I._SeqSiteInformacaoGaleria = Convert.ToInt32(dr["SEQ_GALERIA_IMG_CHAMADA"]);
                        L.Add(I);
                    }
                }
                return L;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Hcrp.Framework.Classes.SiteInformacao BuscaNoticiaId(int id)
        {
            try
            {
                Hcrp.Framework.Classes.SiteInformacao I = new Hcrp.Framework.Classes.SiteInformacao();
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT A.* FROM SITE_INFORMACAO A " + Environment.NewLine);
                    sb.Append(" WHERE SEQ_SITE_INFORMACAO = :SEQ_SITE_INFORMACAO " + Environment.NewLine);
                    sb.Append(" ORDER BY A.DTA_HOR_PUBLICACAO DESC " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_SITE_INFORMACAO"] = id;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        if (dr["NUM_USER_PUBLICACAO"] != DBNull.Value)
                            I._NumUserPublicacao = Convert.ToInt32(dr["NUM_USER_PUBLICACAO"]);
                        if (dr["SEQ_SITE_INFORMACAO"] != DBNull.Value)
                            I.Seq = Convert.ToInt32(dr["SEQ_SITE_INFORMACAO"]);
                        if (dr["DTA_HOR_CADASTRO"] != DBNull.Value)
                            I.DataCadastro = Convert.ToDateTime(dr["DTA_HOR_CADASTRO"]);
                        if (dr["DTA_HOR_PUBLICACAO"] != DBNull.Value)
                            I.DataPublicacao = Convert.ToDateTime(dr["DTA_HOR_PUBLICACAO"]);
                        if (dr["DTA_HOR_EXPIRACAO"] != DBNull.Value)
                            I.DataExpiracao = Convert.ToDateTime(dr["DTA_HOR_EXPIRACAO"]);
                        if (dr["DTA_INFORMACAO"] != DBNull.Value)
                            I.DataInformacao = Convert.ToDateTime(dr["DTA_INFORMACAO"]);
                        I.Titulo = Convert.ToString(dr["DSC_TITULO"]);
                        I.Resumo = Convert.ToString(dr["DSC_RESUMO"]);
                        I.Corpo = Convert.ToString(dr["DSC_INFORMACAO"]);
                        I.PalavrasChave = Convert.ToString(dr["DSC_PALAVRA_CHAVE"]);
                        if (dr["SEQ_GALERIA_IMG_CHAMADA"] != DBNull.Value)
                            I._SeqSiteInformacaoGaleria = Convert.ToInt32(dr["SEQ_GALERIA_IMG_CHAMADA"]);
                    }
                }
                return I;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Hcrp.Framework.Classes.SiteInformacao> BuscaTodasNoticias()
        {
            try
            {
                List<Hcrp.Framework.Classes.SiteInformacao> L = new List<Hcrp.Framework.Classes.SiteInformacao>();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT A.* FROM SITE_INFORMACAO A " + Environment.NewLine);
                    sb.Append(" WHERE SYSDATE BETWEEN A.DTA_HOR_PUBLICACAO AND A.DTA_HOR_EXPIRACAO " + Environment.NewLine);
                    sb.Append(" ORDER BY A.DTA_HOR_PUBLICACAO DESC " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.SiteInformacao I = new Hcrp.Framework.Classes.SiteInformacao();
                        if (dr["NUM_USER_PUBLICACAO"] != DBNull.Value)
                            I._NumUserPublicacao = Convert.ToInt32(dr["NUM_USER_PUBLICACAO"]);
                        if (dr["SEQ_SITE_INFORMACAO"] != DBNull.Value)
                            I.Seq = Convert.ToInt32(dr["SEQ_SITE_INFORMACAO"]);
                        if (dr["DTA_HOR_CADASTRO"] != DBNull.Value)
                            I.DataCadastro = Convert.ToDateTime(dr["DTA_HOR_CADASTRO"]);
                        if (dr["DTA_HOR_PUBLICACAO"] != DBNull.Value)
                            I.DataPublicacao = Convert.ToDateTime(dr["DTA_HOR_PUBLICACAO"]);
                        if (dr["DTA_HOR_EXPIRACAO"] != DBNull.Value)
                            I.DataExpiracao = Convert.ToDateTime(dr["DTA_HOR_EXPIRACAO"]);
                        I.Titulo = Convert.ToString(dr["DSC_TITULO"]);
                        I.Resumo = Convert.ToString(dr["DSC_RESUMO"]);
                        I.Corpo = Convert.ToString(dr["DSC_INFORMACAO"]);
                        I.PalavrasChave = Convert.ToString(dr["DSC_PALAVRA_CHAVE"]);
                        if (dr["SEQ_GALERIA_IMG_CHAMADA"] != DBNull.Value)
                            I._SeqSiteInformacaoGaleria = Convert.ToInt32(dr["SEQ_GALERIA_IMG_CHAMADA"]);
                        L.Add(I);
                    }
                }
                return L;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Hcrp.Framework.Classes.SiteInformacao> BuscarInformacoesRelacionadas(int id)
        {
            try
            {
                List<Hcrp.Framework.Classes.SiteInformacao> L = new List<Hcrp.Framework.Classes.SiteInformacao>();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();


                    sb.Append(" SELECT * FROM ( " + Environment.NewLine);
                    sb.Append(" SELECT B.SEQ_INF_SITE_LINK SEQ_REL FROM SITE_INFORMACAO_LINK B " + Environment.NewLine);
                    sb.Append(" WHERE B.SEQ_INFORMACAO_SITE = :SEQ " + Environment.NewLine);
                    sb.Append(" UNION " + Environment.NewLine);
                    sb.Append(" SELECT B.SEQ_INFORMACAO_SITE SEQ_REL FROM SITE_INFORMACAO_LINK B " + Environment.NewLine);
                    sb.Append(" WHERE B.SEQ_INF_SITE_LINK = :SEQ) X, SITE_INFORMACAO Y " + Environment.NewLine);
                    sb.Append(" WHERE X.SEQ_REL = Y.SEQ_SITE_INFORMACAO " + Environment.NewLine);
                    sb.Append(" ORDER BY Y.DSC_TITULO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query.Params["SEQ"] = id;
                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.SiteInformacao I = new Hcrp.Framework.Classes.SiteInformacao();
                        if (dr["NUM_USER_PUBLICACAO"] != DBNull.Value)
                            I._NumUserPublicacao = Convert.ToInt32(dr["NUM_USER_PUBLICACAO"]);
                        if (dr["SEQ_SITE_INFORMACAO"] != DBNull.Value)
                            I.Seq = Convert.ToInt32(dr["SEQ_SITE_INFORMACAO"]);
                        if (dr["DTA_HOR_CADASTRO"] != DBNull.Value)
                            I.DataCadastro = Convert.ToDateTime(dr["DTA_HOR_CADASTRO"]);
                        if (dr["DTA_HOR_PUBLICACAO"] != DBNull.Value)
                            I.DataPublicacao = Convert.ToDateTime(dr["DTA_HOR_PUBLICACAO"]);
                        if (dr["DTA_HOR_EXPIRACAO"] != DBNull.Value)
                            I.DataExpiracao = Convert.ToDateTime(dr["DTA_HOR_EXPIRACAO"]);
                        I.Titulo = Convert.ToString(dr["DSC_TITULO"]);
                        I.Resumo = Convert.ToString(dr["DSC_RESUMO"]);
                        I.Corpo = Convert.ToString(dr["DSC_INFORMACAO"]);
                        I.PalavrasChave = Convert.ToString(dr["DSC_PALAVRA_CHAVE"]);
                        if (dr["SEQ_GALERIA_IMG_CHAMADA"] != DBNull.Value)
                            I._SeqSiteInformacaoGaleria = Convert.ToInt32(dr["SEQ_GALERIA_IMG_CHAMADA"]);
                        L.Add(I);
                    }
                }
                return L;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AtualizarImagemCapa(long seqSiteInformacao, long seqInformacaoSiteGaleria)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    var comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_INFORMACAO");
                    if (seqInformacaoSiteGaleria > 0)
                    {
                        comando.Params["SEQ_GALERIA_IMG_CHAMADA"] = seqInformacaoSiteGaleria;
                    }
                    else
                    {
                        comando.Params["SEQ_GALERIA_IMG_CHAMADA"] = DBNull.Value;
                    }
                    comando.FilterParams["SEQ_SITE_INFORMACAO"] = seqSiteInformacao;
                    ctx.ExecuteUpdate(comando);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long InserirAtualizar(Hcrp.Framework.Classes.SiteInformacao iSite, int codSite)
        {
            if ((iSite.Seq == null) || (iSite.Seq == 0)) //Inserir
            {
                try
                {
                    long retorno = 0;
                    using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                    {
                        // Abrir conexão
                        ctx.Open();

                        // Preparar o comando
                        Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("SITE_INFORMACAO");

                        comando.Params["DTA_HOR_CADASTRO"] = System.DateTime.Now;
                        comando.Params["DTA_HOR_PUBLICACAO"] = iSite.DataPublicacao;
                        comando.Params["DTA_HOR_EXPIRACAO"] = iSite.DataExpiracao;
                        comando.Params["DTA_INFORMACAO"] = iSite.DataInformacao;
                        comando.Params["DSC_TITULO"] = iSite.Titulo.Replace("–", "&#8212;").Replace("“", "\"").Replace("”", "\"");
                        comando.Params["DSC_RESUMO"] = iSite.Resumo.Replace("–", "&#8212;").Replace("“", "\"").Replace("”", "\"");
                        comando.Params["DSC_INFORMACAO"] = iSite.Corpo.Replace("–", "&#8212;").Replace("“", "\"").Replace("”", "\"");
                        comando.Params["NUM_USER_PUBLICACAO"] = new Hcrp.Framework.Classes.UsuarioConexao().NumUserBanco;
                        string TagCloud = iSite.PalavrasChave.Replace("–", "&#8212;").Replace("“", "\"").Replace("”", "\"");
                        comando.Params["DSC_PALAVRA_CHAVE"] = TagCloud.ToUpper();

                        // Executar o insert
                        ctx.ExecuteInsert(comando);

                        // Pegar o último ID
                        retorno = ctx.GetSequenceValue("GENERICO.SEQ_SITE_INFORMACAO", false);
                        //retorno = 0;
                        foreach (var item in iSite.GaleriaImagens)
                        {
                            long retorno_capa = item.InserirAtualizar(retorno, codSite);
                            if (item._FlagCapa)
                            {
                                Hcrp.Infra.AcessoDado.UpdateCommandConfig comandoA = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_INFORMACAO");
                                comandoA.Params["SEQ_GALERIA_IMG_CHAMADA"] = retorno_capa;
                                comandoA.FilterParams["SEQ_SITE_INFORMACAO"] = retorno;
                                ctx.ExecuteUpdate(comandoA);
                            }
                        }

                        foreach (var item in iSite.ItemsMenuVertical)
                        {
                            Hcrp.Infra.AcessoDado.CommandConfig comandoC = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_MENU_VERTICAL_INFORMACAO");
                            comandoC.Params["COD_MENU_VERTICAL"] = item.Codigo;
                            comandoC.Params["SEQ_SITE_INFORMACAO"] = retorno;
                            ctx.ExecuteInsert(comandoC);
                        }

                        foreach (var item in iSite.InformacoesRelacionadas)
                        {
                            Hcrp.Infra.AcessoDado.CommandConfig comandoD = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_INFORMACAO_LINK");
                            comandoD.Params["SEQ_INFORMACAO_SITE"] = retorno;
                            comandoD.Params["SEQ_INF_SITE_LINK"] = item.Seq;
                            ctx.ExecuteInsert(comandoD);
                        }

                        // Tag Cloud
                        montaTagCloud(TagCloud, ctx);

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
                        Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_INFORMACAO");
                        comando.Params["DTA_HOR_CADASTRO"] = System.DateTime.Now;
                        comando.Params["DTA_HOR_PUBLICACAO"] = iSite.DataPublicacao;
                        comando.Params["DTA_HOR_EXPIRACAO"] = iSite.DataExpiracao;
                        comando.Params["DTA_INFORMACAO"] = iSite.DataInformacao;
                        comando.Params["DSC_TITULO"] = iSite.Titulo.Replace("–", "&#8212;").Replace("“", "\"").Replace("”", "\"");
                        comando.Params["DSC_RESUMO"] = iSite.Resumo.Replace("–", "&#8212;").Replace("“", "\"").Replace("”", "\"");
                        comando.Params["DSC_INFORMACAO"] = iSite.Corpo.Replace("–", "&#8212;").Replace("“", "\"").Replace("”", "\"");
                        comando.Params["NUM_USER_PUBLICACAO"] = new Hcrp.Framework.Classes.UsuarioConexao().NumUserBanco;
                        string TagCloud = iSite.PalavrasChave.Replace("–", "&#8212;").Replace("“", "\"").Replace("”", "\"");
                        comando.Params["DSC_PALAVRA_CHAVE"] = TagCloud.ToUpper();
                        comando.FilterParams["SEQ_SITE_INFORMACAO"] = iSite.Seq;


                        if ((int)Hcrp.Framework.Classes.Site.ECodSite.SiteHC == codSite)
                        {
                            Hcrp.Infra.AcessoDado.UpdateCommandConfig comandoA = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_INFORMACAO");
                            comandoA.Params["SEQ_GALERIA_IMG_CHAMADA"] = DBNull.Value;
                            comandoA.FilterParams["SEQ_SITE_INFORMACAO"] = iSite.Seq;
                            ctx.ExecuteUpdate(comandoA);

                            iSite.ApagarGaleria(iSite.Seq);

                            foreach (var item in iSite.GaleriaImagens)
                            {
                                long retorno_capa = item.InserirAtualizar(iSite.Seq, codSite);
                                if (item._FlagCapa)
                                    comando.Params["SEQ_GALERIA_IMG_CHAMADA"] = retorno_capa;
                            }
                        }

                        // Executar o update
                        ctx.ExecuteUpdate(comando);

                        // Pegar o ID
                        retorno = iSite.Seq;

                        Hcrp.Infra.AcessoDado.CommandConfig comandoB = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_MENU_VERTICAL_INFORMACAO");
                        comandoB.Params["SEQ_SITE_INFORMACAO"] = iSite.Seq;
                        ctx.ExecuteDelete(comandoB);

                        foreach (var item in iSite.ItemsMenuVertical)
                        {
                            Hcrp.Infra.AcessoDado.CommandConfig comandoC = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_MENU_VERTICAL_INFORMACAO");
                            comandoC.Params["COD_MENU_VERTICAL"] = item.Codigo;
                            comandoC.Params["SEQ_SITE_INFORMACAO"] = iSite.Seq;
                            ctx.ExecuteInsert(comandoC);
                        }

                        Hcrp.Infra.AcessoDado.CommandConfig comandoD = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_INFORMACAO_LINK");
                        comandoD.Params["SEQ_INFORMACAO_SITE"] = retorno;
                        ctx.ExecuteDelete(comandoD);

                        comandoD = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_INFORMACAO_LINK");
                        comandoD.Params["SEQ_INF_SITE_LINK"] = retorno;
                        ctx.ExecuteDelete(comandoD);

                        foreach (var item in iSite.InformacoesRelacionadas)
                        {
                            Hcrp.Infra.AcessoDado.CommandConfig comandoC = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_INFORMACAO_LINK");
                            comandoC.Params["SEQ_INFORMACAO_SITE"] = retorno;
                            comandoC.Params["SEQ_INF_SITE_LINK"] = item.Seq;
                            ctx.ExecuteInsert(comandoC);
                        }


                        // Tag Cloud
                        montaTagCloud(TagCloud, ctx);

                    }
                    return retorno;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void montaTagCloud(string TagCloud, Hcrp.Infra.AcessoDado.Contexto ctx)
        {
            string[] words = TagCloud.Split(',');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Trim();
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < words.Length; i++)
            {
                sb.Clear();
                sb.Append(" SELECT * FROM SITE_TAG_CLOUD A WHERE A.NOM_TAG = :NOM_TAG ");
                Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                query.Params["NOM_TAG"] = words[i];
                ctx.ExecuteQuery(query);
                OracleDataReader dr = ctx.Reader as OracleDataReader;
                if (!dr.HasRows)
                {
                    Hcrp.Infra.AcessoDado.CommandConfig comandoE = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_TAG_CLOUD");
                    comandoE.Params["NOM_TAG"] = words[i];
                    comandoE.Params["NUM_PONTUACAO"] = 0;
                    ctx.ExecuteInsert(comandoE);
                }
            }
        }


        public void VotaTagCloud(string TagCloud)
        {
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                ctx.Open();

                string[] words = TagCloud.Split(',');
                for (int i = 0; i < words.Length; i++)
                {
                    words[i] = words[i].Trim();
                }

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < words.Length; i++)
                {
                    sb.Clear();
                    sb.Append(" SELECT * FROM SITE_TAG_CLOUD A WHERE A.NOM_TAG = :NOM_TAG ");
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query.Params["NOM_TAG"] = words[i];
                    ctx.ExecuteQuery(query);
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    int NumPontos = 0;
                    while (dr.Read())
                    {
                        NumPontos = Convert.ToInt32(dr["NUM_PONTUACAO"]);
                    }
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_TAG_CLOUD");
                    comando.Params["NUM_PONTUACAO"] = NumPontos + 1;
                    comando.FilterParams["NOM_TAG"] = words[i];
                    ctx.ExecuteUpdate(comando);
                }
            }
        }

        public string[] ListaTagClouds()
        {
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                ctx.Open();
                string[] words = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13" };
                int i = 0;
                StringBuilder sb = new StringBuilder();
                sb.Clear();
                sb.Append(" select x.* from ( ");
                sb.Append(" SELECT * FROM SITE_TAG_CLOUD A  ");
                sb.Append(" ORDER BY A.NUM_PONTUACAO DESC, A.NOM_TAG ASC) x ");
                sb.Append(" where rownum <= 14 ");
                Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                ctx.ExecuteQuery(query);
                OracleDataReader dr = ctx.Reader as OracleDataReader;
                while (dr.Read())
                {
                    words[i] = Convert.ToString(dr["NOM_TAG"]);
                    i++;
                }
                return words;
            }
        }


        public List<Hcrp.Framework.Classes.SiteInformacao> BuscaTodasNoticiasFiltro(string busca, int codSite)
        {
            try
            {
                List<Hcrp.Framework.Classes.SiteInformacao> L = new List<Hcrp.Framework.Classes.SiteInformacao>();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append("select x.*" + Environment.NewLine);
                    sb.Append("  from site_informacao x" + Environment.NewLine);
                    sb.Append("  join (select a.seq_site_informacao" + Environment.NewLine);
                    sb.Append("          from site                          b," + Environment.NewLine);
                    sb.Append("               site_menu_horizontal          c," + Environment.NewLine);
                    sb.Append("               site_menu_hor_vertical        d," + Environment.NewLine);
                    sb.Append("               site_menu_vertical            e," + Environment.NewLine);
                    sb.Append("               site_menu_vertical_informacao f," + Environment.NewLine);
                    sb.Append("               site_informacao               a" + Environment.NewLine);
                    sb.Append("         where b.cod_site = :SITE" + Environment.NewLine);
                    sb.Append("           and b.cod_site = c.cod_site" + Environment.NewLine);
                    sb.Append("           and c.cod_menu_horizontal = d.cod_menu_horizontal" + Environment.NewLine);
                    sb.Append("           and d.cod_menu_vertical = e.cod_menu_vertical" + Environment.NewLine);
                    sb.Append("           and e.cod_menu_vertical = f.cod_menu_vertical" + Environment.NewLine);
                    sb.Append("           and f.seq_site_informacao = a.seq_site_informacao" + Environment.NewLine);
                    sb.Append("           and ((upper(a.dsc_titulo) like :BUSCA) or" + Environment.NewLine);
                    sb.Append("               (upper(a.dsc_palavra_chave) like :BUSCA) or" + Environment.NewLine);
                    sb.Append("               (upper(a.dsc_informacao) like :BUSCA))" + Environment.NewLine);
                    sb.Append("         group by a.seq_site_informacao) y" + Environment.NewLine);
                    sb.Append("    on x.seq_site_informacao = y.seq_site_informacao" + Environment.NewLine);
                    sb.Append(" order by x.dsc_titulo desc" + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SITE"] = codSite;
                    query.Params["BUSCA"] = "%" + busca.ToUpper() + "%";
                    query.Params["BUSCA"] = "%" + busca.ToUpper() + "%";
                    query.Params["BUSCA"] = "%" + busca.ToUpper() + "%";

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.SiteInformacao I = new Hcrp.Framework.Classes.SiteInformacao();
                        if (dr["NUM_USER_PUBLICACAO"] != DBNull.Value)
                            I._NumUserPublicacao = Convert.ToInt32(dr["NUM_USER_PUBLICACAO"]);
                        if (dr["SEQ_SITE_INFORMACAO"] != DBNull.Value)
                            I.Seq = Convert.ToInt32(dr["SEQ_SITE_INFORMACAO"]);
                        if (dr["DTA_HOR_CADASTRO"] != DBNull.Value)
                            I.DataCadastro = Convert.ToDateTime(dr["DTA_HOR_CADASTRO"]);
                        if (dr["DTA_HOR_PUBLICACAO"] != DBNull.Value)
                            I.DataPublicacao = Convert.ToDateTime(dr["DTA_HOR_PUBLICACAO"]);
                        if (dr["DTA_HOR_EXPIRACAO"] != DBNull.Value)
                            I.DataExpiracao = Convert.ToDateTime(dr["DTA_HOR_EXPIRACAO"]);
                        if (dr["DTA_INFORMACAO"] != DBNull.Value)
                            I.DataInformacao = Convert.ToDateTime(dr["DTA_INFORMACAO"]);
                        I.Titulo = Convert.ToString(dr["DSC_TITULO"]);
                        I.Resumo = Convert.ToString(dr["DSC_RESUMO"]);
                        I.Corpo = Convert.ToString(dr["DSC_INFORMACAO"]);
                        I.PalavrasChave = Convert.ToString(dr["DSC_PALAVRA_CHAVE"]);
                        if (dr["SEQ_GALERIA_IMG_CHAMADA"] != DBNull.Value)
                            I._SeqSiteInformacaoGaleria = Convert.ToInt32(dr["SEQ_GALERIA_IMG_CHAMADA"]);
                        L.Add(I);
                    }
                }
                return L;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Hcrp.Framework.Classes.SiteInformacao> BuscarInformacoesMenuV(int CodMenuV)
        {
            try
            {
                List<Hcrp.Framework.Classes.SiteInformacao> L = new List<Hcrp.Framework.Classes.SiteInformacao>();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    string sql = " SELECT A.SEQ_SITE_INFORMACAO, \n" +
                                    "       A.DTA_HOR_CADASTRO, \n" +
                                    "       A.DTA_HOR_PUBLICACAO, \n" +
                                    "       A.DTA_HOR_EXPIRACAO, \n" +
                                    "       A.DSC_TITULO, \n" +
                                    "       A.DSC_RESUMO, \n" +
                                    "       A.DSC_INFORMACAO, \n" +
                                    "       A.NUM_USER_PUBLICACAO, \n" +
                                    "       A.DSC_PALAVRA_CHAVE, \n" +
                                    "       A.SEQ_GALERIA_IMG_CHAMADA, \n" +
                                    "       A.DTA_INFORMACAO \n" +
                                    " FROM SITE_INFORMACAO A, SITE_MENU_VERTICAL_INFORMACAO B \n" +
                                    " WHERE A.SEQ_SITE_INFORMACAO = B.SEQ_SITE_INFORMACAO \n" +
                                    " AND SYSDATE BETWEEN A.DTA_HOR_PUBLICACAO AND A.DTA_HOR_EXPIRACAO \n" +
                                    " AND B.COD_MENU_VERTICAL = :COD_MENU_VERTICAL ORDER BY A.DTA_HOR_CADASTRO DESC ";


                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);
                    query.Params["COD_MENU_VERTICAL"] = CodMenuV;
                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.SiteInformacao I = new Hcrp.Framework.Classes.SiteInformacao();
                        if (dr["NUM_USER_PUBLICACAO"] != DBNull.Value)
                            I._NumUserPublicacao = Convert.ToInt32(dr["NUM_USER_PUBLICACAO"]);
                        if (dr["SEQ_SITE_INFORMACAO"] != DBNull.Value)
                            I.Seq = Convert.ToInt32(dr["SEQ_SITE_INFORMACAO"]);
                        if (dr["DTA_HOR_CADASTRO"] != DBNull.Value)
                            I.DataCadastro = Convert.ToDateTime(dr["DTA_HOR_CADASTRO"]);
                        if (dr["DTA_HOR_PUBLICACAO"] != DBNull.Value)
                            I.DataPublicacao = Convert.ToDateTime(dr["DTA_HOR_PUBLICACAO"]);
                        if (dr["DTA_HOR_EXPIRACAO"] != DBNull.Value)
                            I.DataExpiracao = Convert.ToDateTime(dr["DTA_HOR_EXPIRACAO"]);
                        if (dr["DTA_INFORMACAO"] != DBNull.Value)
                            I.DataInformacao = Convert.ToDateTime(dr["DTA_INFORMACAO"]);
                        I.Titulo = Convert.ToString(dr["DSC_TITULO"]);
                        I.Resumo = Convert.ToString(dr["DSC_RESUMO"]);
                        I.Corpo = Convert.ToString(dr["DSC_INFORMACAO"]);
                        I.PalavrasChave = Convert.ToString(dr["DSC_PALAVRA_CHAVE"]);
                        if (dr["SEQ_GALERIA_IMG_CHAMADA"] != DBNull.Value)
                            I._SeqSiteInformacaoGaleria = Convert.ToInt32(dr["SEQ_GALERIA_IMG_CHAMADA"]);
                        L.Add(I);
                    }
                }
                return L;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
