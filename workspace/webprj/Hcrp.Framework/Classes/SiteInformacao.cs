using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hcrp.Framework.Classes
{
    [Serializable]
    public class SiteInformacao
    {
        private List<Hcrp.Framework.Classes.SiteMenuVertical> _ItemsMenuVertical;
        private List<SiteInformacaoGaleria> _GaleriaImagens;
        private List<SiteInformacao> _InformacoesRelacionadas;

        public int _NumUserPublicacao { get; set; }
        public int _SeqSiteInformacaoGaleria { get; set; }

        public int Seq { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataPublicacao { get; set; }
        public DateTime DataExpiracao { get; set; }
        public DateTime DataInformacao { get; set; }
        public string Titulo { get; set; }
        public string Resumo { get; set; }
        public string Corpo  { get; set; }
        public Usuario UsuarioPublicacao
        {
            get
            {
                if (this.UsuarioPublicacao == null)
                {
                    return new Hcrp.Framework.Dal.Usuario().BuscarUsuarioCodigo(this._NumUserPublicacao);
                }
                else return this.UsuarioPublicacao;
            }
            set{}
        }
        public string PalavrasChave { get; set; }

        public SiteInformacaoGaleria ImagemChamada {
            get
            {
                return new Hcrp.Framework.Dal.SiteInformacaoGaleria().BuscaImagemChamada(_SeqSiteInformacaoGaleria);
            }
            set{}
        }
        public List<SiteInformacaoGaleria> GaleriaImagens {
            get 
            {
                if (this.Seq == null)
                    return null;
                else
                {   
                    if (this._GaleriaImagens==null)
                        this._GaleriaImagens = new Hcrp.Framework.Dal.SiteInformacaoGaleria().BuscaGaleria(this.Seq);
                    return this._GaleriaImagens;
                }
            }
            set { _GaleriaImagens = value; }
        }


        public List<SiteInformacao> InformacoesRelacionadas
        {
            get
            {
                if (this.Seq == null)
                    return null;
                else
                {
                    if (this._InformacoesRelacionadas == null)
                        this._InformacoesRelacionadas = new Hcrp.Framework.Dal.SiteInformacao().BuscarInformacoesRelacionadas(this.Seq);
                    return this._InformacoesRelacionadas;
                }
            }
            set { _InformacoesRelacionadas = value; }
        }



        public List<Hcrp.Framework.Classes.SiteMenuVertical> ItemsMenuVertical
        {
            get
            {
                if (this.Seq == null)
                    return null;
                else
                {
                    if (this._ItemsMenuVertical == null)
                        this._ItemsMenuVertical = new Hcrp.Framework.Dal.SiteMenuVertical().BuscarMenuVerticalInformacao(this.Seq);
                    return this._ItemsMenuVertical;
                }
            }
            set 
            {
                this._ItemsMenuVertical = value;
            }
        }

      /*  public List<Hcrp.Framework.Classes.SiteMenuVertical> ItensMenuV {
            get
            {
                if (this.ItensMenuV == null)
                    return new Hcrp.Framework.Dal.SiteMenuVertical().BuscarMenuVerticalInf(this.Seq);
                else return this.ItensMenuV;
            }
            set { }
        }*/

        public SiteInformacao()
        { }

        public Hcrp.Framework.Classes.SiteInformacao BuscaSuperDestaque()
        {
            return new Hcrp.Framework.Dal.SiteInformacao().BuscaSuperDestaque();
        }
        public List<Hcrp.Framework.Classes.SiteInformacao> BuscaDestaques()
        { 
            return new Hcrp.Framework.Dal.SiteInformacao().BuscaDestaques();
        }
        public List<Hcrp.Framework.Classes.SiteInformacao> BuscaMiniDestaques()
        { 
            return new Hcrp.Framework.Dal.SiteInformacao().BuscaMiniDestaques();
        }
        public Hcrp.Framework.Classes.SiteInformacao BuscaNoticiaId(int id)
        {
            return new Hcrp.Framework.Dal.SiteInformacao().BuscaNoticiaId(id);
        }
        public List<Hcrp.Framework.Classes.SiteInformacao> BuscaTodasNoticias()
        {
            return new Hcrp.Framework.Dal.SiteInformacao().BuscaTodasNoticias();
        }
        public List<Hcrp.Framework.Classes.SiteInformacao> BuscaTodasNoticiasFiltro(string busca, int codSite)
        {
            if (!string.IsNullOrWhiteSpace(busca))
                return new Hcrp.Framework.Dal.SiteInformacao().BuscaTodasNoticiasFiltro(busca, codSite);
            else return null;
        }
        
        /// <summary>
        /// Atualiza a seq da capa da notícia. Setar zero para gravar null no banco de dados
        /// </summary>
        /// <param name="seqSiteInformacao"></param>
        /// <param name="seqInformacaoSiteGaleria"></param>
        public void AtualizarImagemCapa(long seqSiteInformacao, long seqInformacaoSiteGaleria)
        {
            new Hcrp.Framework.Dal.SiteInformacao().AtualizarImagemCapa(seqSiteInformacao, seqInformacaoSiteGaleria);
        }
        
        public long InserirAtualizar(int codSite)
        {
            return new Hcrp.Framework.Dal.SiteInformacao().InserirAtualizar(this, codSite);
        }
        public List<Hcrp.Framework.Classes.SiteInformacao> BuscarInformacoesMenuV(int CodMenuV)
        {
            return new Hcrp.Framework.Dal.SiteInformacao().BuscarInformacoesMenuV(CodMenuV);
        }

        public void VotaTagCloud()
        {
            new Hcrp.Framework.Dal.SiteInformacao().VotaTagCloud(this.PalavrasChave);
        }

        public string[] ListaTagClouds()
        {
            return new Hcrp.Framework.Dal.SiteInformacao().ListaTagClouds();
        }


        // CONVERTER PARA URL CURTA
        protected string ObterUrlCurta(string url)
        {
            try
            {
                if (!url.ToLower().StartsWith("http") && !url.StartsWith("ftp"))
                {
                    url = "http://" + url;
                }

                WebRequest request = WebRequest.Create("http://tinyurl.com/api-create.php?url=" + url);

                WebResponse response = request.GetResponse();

                string urlCurta = string.Empty;

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    urlCurta = reader.ReadToEnd();
                }
                // se tudo der certo, devolve a url resumida
                return urlCurta;
            }
            catch (Exception)
            {
                // se ocorrer erro devolve a mesma url
                return url;
            }
        }
        
        // FACEBOOK
        public string ObterUrlFacebook(string urlParaCompartilhar, string tituloPagina, int cod_site)
        {
            Hcrp.Framework.Classes.Site site = new Hcrp.Framework.Dal.Site().BuscarSite(cod_site);

            StringBuilder url = new StringBuilder(string.Format("http://www.facebook.com/sharer.php?u={0}&t={1}",

                urlParaCompartilhar, site.Nome+": "+tituloPagina));

            return url.ToString();

        }

        // TWITTER
        public string ObterUrlTwitter(string urlParaCompartilhar, string tweet, int cod_site)
        {

            Hcrp.Framework.Classes.Site site = new Hcrp.Framework.Dal.Site().BuscarSite(cod_site);

            StringBuilder url = new StringBuilder(string.Format("http://twitter.com/home?status={0} {1}",

                site.Nome+": "+tweet+" >>> ", ObterUrlCurta(urlParaCompartilhar) ));


            return url.ToString();

        }

        //ORKUT
        public string ObterUrlOrkut(string urlParaCompartilhar, string tituloPagina, int cod_site)
        {
            Hcrp.Framework.Classes.Site site = new Hcrp.Framework.Dal.Site().BuscarSite(cod_site);

            StringBuilder url = new StringBuilder(string.Format("http://promote.orkut.com/preview?nt=orkut.com&tt={0}&du={1}",


                site.Nome+": "+tituloPagina, urlParaCompartilhar));

            return url.ToString();

        }

        public bool ApagarGaleria(long seqSiteInformacao)
        {
            return new Hcrp.Framework.Dal.SiteInformacaoGaleria().Apagar(seqSiteInformacao);
        }



    }
}
