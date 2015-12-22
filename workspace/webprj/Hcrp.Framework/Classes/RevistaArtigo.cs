using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    [Serializable]
    public class RevistaArtigo
    {
        private Revista _Revista;
        private RevistaEdicao _Edicao;
        private Usuario _Autor;
        private SituacaoHc _SituacaoAtual;
        private IList<AutorRevistaArtigo> _Autores;
        private List<RevistaArtigoAnexo> _Anexos;
        private EixoTematico _EixoTematico;
        
        public Boolean ComParecerista { get; set; }

        public string Revisado { get; set; }
        public int NumArtigo { get; set; }
        public int SeqRevistaEdicao { get; set; }
        public int NumUserAutor{ get; set; }
        public int CodSituacao{ get; set; }
        public int SeqRevista { get; set; }
        public int CodEixoTematico { get; set; }
        public string Titulo { get; set; }
        public string Triagens { get; set; }
        public DateTime DataUltimaSubmissao { get; set; }
        public string DownloadPdf {
            get {
                return "<a href='uploads/Artigos/" + Convert.ToString(this.NumArtigo) + "/" + Convert.ToString(this.NumArtigo) + ".pdf' target='_blank'><img src='http://10.165.5.50/InterfaceHC/imagens/pdf.gif' /></a>";
            }
        }
        public int QtdRevisoresSelecionados {
            get {
                return new Hcrp.Framework.Dal.RevistaArtigo().QtdRevisoresSelecionados(this);
            }        
        }
        public Revista Revista {
            get
            {
                if (_Revista == null)
                {
                    _Revista = new Hcrp.Framework.Dal.Revista().BuscarRevistaCodigo(this.SeqRevista);
                }
                return _Revista;
            }
        }
        public RevistaEdicao Edicao {
            get
            {
                if (_Edicao == null)
                {
                    _Edicao = new Hcrp.Framework.Dal.RevistaEdicao().BuscarEdicaoCodigo(this.SeqRevistaEdicao);
                }
                return _Edicao;
            }
        }
        public Usuario Autor { 
            get
            {
                if (_Autor == null)
                {
                    _Autor = new Hcrp.Framework.Dal.Usuario().BuscarUsuarioCodigo(this.NumUserAutor);
                }
                return _Autor;
            }
            set { _Autor = value; }
        }
        public SituacaoHc SituacaoAtual
        {
            get
            {
                if (_SituacaoAtual == null)
                {
                    _SituacaoAtual = new Hcrp.Framework.Dal.SituacaoHc().BuscarSituacaoCodigo(this.CodSituacao);
                }
                return _SituacaoAtual;
            }
        }
        public IList<MovimentacaoRevista> DatasSubmissao { get; set; }
        public IList<AutorRevistaArtigo> Autores
        {
            get
            {
                if (_Autores == null)
                    return new Hcrp.Framework.Dal.AutorRevistaArtigo().BuscarAutoresDoArtigo(this.NumArtigo);
                else return _Autores;
            }
            set{ _Autores = value; }
        }
        public IList<MovimentacaoRevista> DatasCorrecao { get; set; }
        public IList<MovimentacaoRevista> DatasDiagramacao { get; set; }
        public IList<RevisaoRevistaArtigo> Revisoes { get; set; }
        public IList<ParecerRevistaArtigo> Pareceres {
            get
            {
                return new Hcrp.Framework.Dal.ParecerRevistaArtigo().BuscarPareceres(this.NumArtigo);
            }
        }
        public IList<ParecerRevistaArtigo> PareceresFinais
        {
            get
            {
                if (ComParecerista == null)
                    ComParecerista = false;
                return new Hcrp.Framework.Dal.ParecerRevistaArtigo().BuscarParecerFinal(this.NumArtigo,this.ComParecerista);
            }
        }
        public IList<SituacaoHc> Situacoes { get; set; }
        public List<RevistaArtigoAnexo> Anexos
        {
            get
            {
                if (_Anexos == null)
                    return new Hcrp.Framework.Dal.RevistaArtigoAnexo().BuscarAnexosPorArtigo(this.NumArtigo);
                else return _Anexos;
            }
            set { _Anexos = value; }
        }
        public List<RevistaArtigoAnexo> AnexosExcluir{get;set;}
        public EixoTematico EixoTematico
        {
            get
            {
                if (_EixoTematico == null)
                {
                    _EixoTematico = new Hcrp.Framework.Dal.EixoTematico().BuscarEixoCodigo(this.CodEixoTematico);
                }
                return _EixoTematico;
            }
            set { _EixoTematico = value; }
        }
        public string TituloColorido {
            get 
            { 
                switch (this.CodSituacao)
	            {
		            case (int)ETipoSituacaoRevista.Aprovado  :
                        return "<span style=\"font-color:green\">" + this.Titulo + "</span>";
                        break;                    
                    case (int)ETipoSituacaoRevista.Rejeitado :
                        return "<span style=\"font-color:red\">" + this.Titulo + "</span>";
                        break;
                    default :
                        return this.Titulo;
                        break;
	            }
            }
        }

        public enum ETipoSituacaoRevista
        {
            EmEdicao = 112,
            EmTriagem = 116,
            EmRevisao = 117,
            Revisado = 118,
            AprovadoComCorrecoes = 119,
            Aprovado = 52,
            Rejeitado = 120,
            CorrigidoOrtograficamente = 121,
            Diagramado = 122,
            Publicado = 123,
            ValidacaoCorrecaoOrtografica = 124,
            Todos = -1
        }

        public RevistaArtigo()
        { }
        ///Métodos
    public void BuscarPorCodigo(int id)
        {
            new Hcrp.Framework.Dal.RevistaArtigo().BuscarPorCodigo(this, id);
        }
    public List<Hcrp.Framework.Classes.RevistaArtigo> BuscarPorEdicao(Hcrp.Framework.Classes.RevistaEdicao edicao)
    {
        return new Hcrp.Framework.Dal.RevistaArtigo().BuscarPorEdicao(edicao);
    }
    public String CarregarEmHtml(int id, char ShowAutor = 'S')
        {
            return new Hcrp.Framework.Dal.RevistaArtigo().CarregarEmHtml(this, id, ShowAutor);
        }
    public long InserirAtualizar()
    {
        return new Hcrp.Framework.Dal.RevistaArtigo().InserirAtualizar(this);
    }
    public void Excluir()
        {
            new Hcrp.Framework.Dal.RevistaArtigo().Excluir(this);
        }
    public long AtualizarStatus()
    {
        return new Hcrp.Framework.Dal.RevistaArtigo().AtualizarStatus(this);
    }
    public long AtualizarEdicao()
    {
        return new Hcrp.Framework.Dal.RevistaArtigo().AtualizarEdicao(this);
    }
    public Boolean ExcluirAutoresArtigo()
    {
        return new Hcrp.Framework.Dal.RevistaArtigo().ExcluirAutoresArtigo(this);
    }
    public Boolean ExcluirAnexosArtigo()
    {
        return new Hcrp.Framework.Dal.RevistaArtigo().ExcluirAnexosArtigo(this);
    }
    public long AtualizarRevisao()
    {
        return new Hcrp.Framework.Dal.RevistaArtigo().AtualizarRevisao(this);
    }

    }   

}
