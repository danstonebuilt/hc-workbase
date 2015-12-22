using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;
using System.IO;

namespace Hcrp.Framework.Classes
{
    public class SiteInformacaoGaleria
    {
        private SiteInformacao _SiteInformacao;
        public int _seqSiteInformacao { get; set; }

        public System.Web.UI.WebControls.FileUpload Arquivo { get; set; }
        public byte[] ArquivoArrayOfBytes  { get; set; }
        public int Seq { get; set; }
        public SiteInformacao SiteInformacao
        {
            get
            {
                if (this._SiteInformacao == null)
                    _SiteInformacao = SiteInformacao.BuscaNoticiaId(_seqSiteInformacao);
                return this._SiteInformacao;
            }
            set { _SiteInformacao = value; }
        }
        public string Rotulo { get; set; }
        public string Caminho { get; set; }
        public ETipoObjeto TipoObjeto { get; set; }
        public bool _FlagCapa { get; set; }
        public string Capa
        {
            get {
                if (this._FlagCapa)
                    return "http://10.165.5.50/InterfaceHC/Imagens/Liberado.gif";
                else return "";
            }
            set { }
        }

        public enum ETipoObjeto
        { 
            Imagem = 0,
            VideoEmbedded = 1,
            Arquivo = 2,
            ImagemMosaico = 3,
            ImagemAninhada = 4
        }

        public SiteInformacaoGaleria()
        {         
        }

        public SiteInformacaoGaleria BuscaImagemChamada(int seqSiteInformacaoGaleria)
        {
            return new Hcrp.Framework.Dal.SiteInformacaoGaleria().BuscaImagemChamada(seqSiteInformacaoGaleria);
        }

        public List<SiteInformacaoGaleria> BuscaGaleria(int seqSiteInformacao)
        {
            return new Hcrp.Framework.Dal.SiteInformacaoGaleria().BuscaGaleria(seqSiteInformacao);
        }

        public long InserirAtualizar(long seqSiteInformacao, int codSite)        
        {
            return new Hcrp.Framework.Dal.SiteInformacaoGaleria().InserirAtualizar(this, seqSiteInformacao, codSite);
        }

        public bool ApagarGaleria(long seqSiteInformacao)
        {
            return new Hcrp.Framework.Dal.SiteInformacaoGaleria().Apagar(seqSiteInformacao);
        }

        public bool ApagarUmItem(long pSeqInformacao, long pSeqInformacaoSiteGaleria)
        {
            // se o item apagado for a capa, entao remove a capa da noticia
            var info = new Classes.SiteInformacao().BuscaNoticiaId((int)pSeqInformacao);
            if (info.ImagemChamada.Seq == pSeqInformacaoSiteGaleria)
            {
                info.AtualizarImagemCapa(pSeqInformacao, 0);
            }

            // apagar o arquivo físico
            var item = BuscaImagemChamada((int)pSeqInformacaoSiteGaleria);

            string pathPrefix = HttpContext.Current.Server.MapPath("..\\");
            File.Delete(pathPrefix + item.Caminho);
            File.Delete(pathPrefix + item.Caminho.Insert(item.Caminho.Length - Path.GetExtension(item.Caminho).Length, "_mini"));

            return new Hcrp.Framework.Dal.SiteInformacaoGaleria().ApagarUmItem(pSeqInformacao, pSeqInformacaoSiteGaleria);
        }
    }
}
