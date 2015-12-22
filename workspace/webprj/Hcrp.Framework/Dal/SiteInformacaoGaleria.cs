using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Hcrp.Framework.Dal
{
    public class SiteInformacaoGaleria : Hcrp.Framework.Classes.SiteInformacaoGaleria
    {
        public Hcrp.Framework.Classes.SiteInformacaoGaleria BuscaImagemChamada(int seqSiteInformacaoGaleria)
        {
            try
            {
                Hcrp.Framework.Classes.SiteInformacaoGaleria I = new Hcrp.Framework.Classes.SiteInformacaoGaleria();
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT A.SEQ_INFORMACAO_SITE_GALERIA, A.SEQ_SITE_INFORMACAO, A.DSC_ROTULO, A.DSC_CAMINHO, A.IDF_TIPO_OBJETO " + Environment.NewLine);
                    sb.Append(" FROM SITE_INFORMACAO_GALERIA A " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_INFORMACAO_SITE_GALERIA = :SEQ_INFORMACAO_SITE_GALERIA " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_INFORMACAO_SITE_GALERIA"] = seqSiteInformacaoGaleria;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        if (dr["SEQ_SITE_INFORMACAO"] != DBNull.Value)
                            I._seqSiteInformacao = Convert.ToInt32(dr["SEQ_SITE_INFORMACAO"]);
                        if (dr["SEQ_INFORMACAO_SITE_GALERIA"] != DBNull.Value)
                            I.Seq = Convert.ToInt32(dr["SEQ_INFORMACAO_SITE_GALERIA"]);
                        I.Rotulo = Convert.ToString(dr["DSC_ROTULO"]);
                        I.Caminho = Convert.ToString(dr["DSC_CAMINHO"]);
                        I.TipoObjeto = (ETipoObjeto)Convert.ToInt32(dr["IDF_TIPO_OBJETO"]);
                    }
                }
                return I;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Hcrp.Framework.Classes.SiteInformacaoGaleria> BuscaGaleria(int seqSiteInformacao)
        {
            try
            {
                List<Hcrp.Framework.Classes.SiteInformacaoGaleria> l = new List<Hcrp.Framework.Classes.SiteInformacaoGaleria>();
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT A.SEQ_INFORMACAO_SITE_GALERIA, A.SEQ_SITE_INFORMACAO, A.DSC_ROTULO, A.DSC_CAMINHO, A.IDF_TIPO_OBJETO " + Environment.NewLine);
                    sb.Append(" FROM SITE_INFORMACAO_GALERIA A " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_SITE_INFORMACAO = :SEQ_SITE_INFORMACAO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_SITE_INFORMACAO"] = seqSiteInformacao;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.SiteInformacaoGaleria I = new Hcrp.Framework.Classes.SiteInformacaoGaleria();
                        if (dr["SEQ_SITE_INFORMACAO"] != DBNull.Value)
                            I._seqSiteInformacao = Convert.ToInt32(dr["SEQ_SITE_INFORMACAO"]);
                        if (dr["SEQ_INFORMACAO_SITE_GALERIA"] != DBNull.Value)
                            I.Seq = Convert.ToInt32(dr["SEQ_INFORMACAO_SITE_GALERIA"]);
                        I.Rotulo = Convert.ToString(dr["DSC_ROTULO"]);
                        I.Caminho = Convert.ToString(dr["DSC_CAMINHO"]);
                        I.TipoObjeto = (ETipoObjeto)Convert.ToInt32(dr["IDF_TIPO_OBJETO"]);
                        l.Add(I);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static System.Drawing.Bitmap miniaturaDoUpload(object arquivo, string nomeArquivo, int maxHeight, int maxWidth)
        {
            if (Path.GetExtension(nomeArquivo).Equals(".jpg", StringComparison.InvariantCultureIgnoreCase) ||
                Path.GetExtension(nomeArquivo).Equals(".gif", StringComparison.InvariantCultureIgnoreCase) ||
                Path.GetExtension(nomeArquivo).Equals(".png", StringComparison.InvariantCultureIgnoreCase) ||
                Path.GetExtension(nomeArquivo).Equals(".bmp", StringComparison.InvariantCultureIgnoreCase) ||
                Path.GetExtension(nomeArquivo).Equals(".jpeg", StringComparison.InvariantCultureIgnoreCase))
            {
                System.Drawing.Bitmap imageToBeResized = null;

                if (arquivo.GetType().IsArray)
                {
                    MemoryStream ms = new MemoryStream((byte[])arquivo);
                    imageToBeResized = new System.Drawing.Bitmap(System.Drawing.Image.FromStream(ms));
                }
                else
                {
                    imageToBeResized = new System.Drawing.Bitmap(((FileUpload)arquivo).PostedFile.InputStream);
                }

                int imageHeight = imageToBeResized.Height;
                int imageWidth = imageToBeResized.Width;

                imageHeight = (imageHeight * maxWidth) / imageWidth;
                imageWidth = maxWidth;

                if (imageHeight > maxHeight)
                {
                    imageWidth = (imageWidth * maxHeight) / imageHeight;
                    imageHeight = maxHeight;
                }
                if ((imageToBeResized.Height > maxHeight) || (imageToBeResized.Width > maxWidth))
                {
                    return new System.Drawing.Bitmap(imageToBeResized, imageWidth, imageHeight);
                }
                else
                {
                    return imageToBeResized;
                }
            }
            else
            {
                return null;
            }
        }

        public long InserirAtualizar(Hcrp.Framework.Classes.SiteInformacaoGaleria iSiteGaleria, long seqSiteInformacao, int codSite)
        {

            if ((iSiteGaleria.Seq == 0) ||
                // Se for HC insere sempre, senha for HEAB insere e atualizada, dependendo do estado do registro                                             
               (codSite == (int)Hcrp.Framework.Classes.Site.ECodSite.SiteHC))  //Inserir
            {
                try
                {
                    long retorno = 0;
                    using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                    {
                        // Abrir conexão
                        ctx.Open();

                        // Pegar a sequence
                        retorno = ctx.GetSequenceValue("GENERICO.SEQ_INFORMACAO_SITE_GALERIA", true);

                        // Preparar o comando
                        Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("SITE_INFORMACAO_GALERIA");
                        comando.Params["SEQ_INFORMACAO_SITE_GALERIA"] = retorno;
                        comando.Params["SEQ_SITE_INFORMACAO"] = seqSiteInformacao;
                        comando.Params["DSC_ROTULO"] = iSiteGaleria.Rotulo;
                        comando.Params["IDF_TIPO_OBJETO"] = (int)iSiteGaleria.TipoObjeto;
                        comando.Params["DSC_CAMINHO"] = iSiteGaleria.Caminho;

                        // se for HEAB salva o arquivo em disco deferentemente
                        if (codSite == (int)Hcrp.Framework.Classes.Site.ECodSite.SiteHEAB)
                        {
                            // se for imagem, cria miniatura
                            if ((iSiteGaleria.TipoObjeto == SiteInformacaoGaleria.ETipoObjeto.Imagem) ||
                                (iSiteGaleria.TipoObjeto == SiteInformacaoGaleria.ETipoObjeto.ImagemMosaico) ||
                                (iSiteGaleria.TipoObjeto == SiteInformacaoGaleria.ETipoObjeto.ImagemAninhada))
                            {
                                string path = GravarImgEmDisco(iSiteGaleria, seqSiteInformacao);
                                comando.Params["DSC_CAMINHO"] = path;
                            }
                            else
                            {
                                string path = GetNovoPathMaisNomeArquivo(iSiteGaleria, seqSiteInformacao);
                                Directory.CreateDirectory(Path.GetDirectoryName(path));
                                System.IO.File.WriteAllBytes(path, iSiteGaleria.ArquivoArrayOfBytes);
                                path = @"upload/" + seqSiteInformacao + "/" + Path.GetFileName(path);
                                comando.Params["DSC_CAMINHO"] = path;
                            }
                        }
                        else
                        #region gravação de arquivos em disco do HC
                        {

                            if (iSiteGaleria.TipoObjeto == ETipoObjeto.VideoEmbedded)
                            {

                            }
                            else if (iSiteGaleria.TipoObjeto == ETipoObjeto.ImagemMosaico)
                            {
                                if (iSiteGaleria.ArquivoArrayOfBytes != null)
                                {
                                    comando.Params["DSC_CAMINHO"] = GravarImgEmDisco(iSiteGaleria, seqSiteInformacao);
                                }
                            }
                            else
                            {
                                string compl = "";
                                string path = "";
                                if (iSiteGaleria.Arquivo != null && iSiteGaleria.Arquivo.HasFile)
                                {
                                    if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("..\\upload\\" + iSiteGaleria.Caminho)))
                                    {
                                        compl = "X_";
                                        //File.Delete(System.Web.HttpContext.Current.Server.MapPath("..\\upload\\" + iSiteGaleria.Arquivo.FileName));
                                    }

                                    if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("..\\upload\\")))
                                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("..\\upload\\"));
                                    iSiteGaleria.Arquivo.SaveAs(System.Web.HttpContext.Current.Server.MapPath("..\\upload\\" + compl + iSiteGaleria.Arquivo.FileName));

                                    path = "upload\\" + compl + iSiteGaleria.Arquivo.FileName;
                                    comando.Params["DSC_CAMINHO"] = path;
                                }
                            }
                        }
                        #endregion
                        // Executar o insert
                        ctx.ExecuteInsert(comando);
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
                    long retorno = iSiteGaleria.Seq;
                    using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                    {
                        ctx.Open();
                        // Preparar o comando
                        Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_INFORMACAO_GALERIA");
                        comando.Params["DSC_ROTULO"] = iSiteGaleria.Rotulo;
                        comando.Params["DSC_CAMINHO"] = iSiteGaleria.Caminho;
                        comando.Params["IDF_TIPO_OBJETO"] = (int)iSiteGaleria.TipoObjeto;
                        comando.FilterParams["SEQ_INFORMACAO_SITE_GALERIA"] = iSiteGaleria.Seq;

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

        private string GetNovoPathMaisNomeArquivo(Hcrp.Framework.Classes.SiteInformacaoGaleria iSiteGaleria, long seqSiteInformacao)
        {
            string destinoArquivo = HttpContext.Current.Server.MapPath("..\\upload\\" + seqSiteInformacao + "\\" + iSiteGaleria.Caminho);
            if (File.Exists(destinoArquivo))
            {
                destinoArquivo = destinoArquivo.Insert(destinoArquivo.Length - Path.GetExtension(destinoArquivo).Length, "_x");
            }
            return destinoArquivo;
        }

        /// <summary>
        /// redimensiona para um tamanho máximo, converte para jpeg, cria a miniatura e retorna o path completo do arquivo salvo
        /// </summary>
        /// <param name="iSiteGaleria"></param>
        /// <param name="seqSiteInformacao"></param>
        /// <returns></returns>
        private string GravarImgEmDisco(Hcrp.Framework.Classes.SiteInformacaoGaleria iSiteGaleria, long seqSiteInformacao)
        {
            string path = null;
            string destinoArquivo = GetNovoPathMaisNomeArquivo(iSiteGaleria, seqSiteInformacao);

            Directory.CreateDirectory(Path.GetDirectoryName(destinoArquivo));
            System.Drawing.Bitmap tamanhoMax;
            tamanhoMax = miniaturaDoUpload(iSiteGaleria.ArquivoArrayOfBytes, iSiteGaleria.Caminho, 700, 900);
            tamanhoMax.Save(destinoArquivo, System.Drawing.Imaging.ImageFormat.Jpeg);

            System.Drawing.Bitmap miniatura;
            miniatura = miniaturaDoUpload(iSiteGaleria.ArquivoArrayOfBytes, iSiteGaleria.Caminho, 200, 200);

            string mini = destinoArquivo.Remove(destinoArquivo.Length - Path.GetExtension(destinoArquivo).Length);
            miniatura.Save(mini + "_mini.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

            path = @"upload/" + seqSiteInformacao + "/" + Path.GetFileName(destinoArquivo);
            return path;
        }

        public bool Apagar(long seqSiteInformacao)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comandoD = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_INFORMACAO_GALERIA");
                    comandoD.Params["SEQ_SITE_INFORMACAO"] = seqSiteInformacao;
                    ctx.ExecuteDelete(comandoD);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ApagarUmItem(long pSeqInformacao, long pSeqInformacaoSiteGaleria)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comandoD = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("SITE_INFORMACAO_GALERIA");
                    comandoD.Params["SEQ_INFORMACAO_SITE_GALERIA"] = pSeqInformacaoSiteGaleria;
                    ctx.ExecuteDelete(comandoD);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
