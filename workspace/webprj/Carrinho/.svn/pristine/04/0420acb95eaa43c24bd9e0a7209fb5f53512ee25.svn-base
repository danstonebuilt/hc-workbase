using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Hcrp.CarroUrgenciaPsicoativo.Entity;
using Newtonsoft.Json.Converters;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Conferencia
{
    public partial class VisualizarConferencia : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
    {
        Int64 seqLacreRepositorio = 0;        

        #region ViewState

        /// <summary>
        /// Carregar viewstate.
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            if (this.ViewState["seqLacreRepositorio"] != null)
                this.seqLacreRepositorio = Convert.ToInt64(this.ViewState["seqLacreRepositorio"]);
        }

        /// <summary>
        /// Salvar ViewState
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {   
            this.ViewState["seqLacreRepositorio"] = this.seqLacreRepositorio;
            
            return base.SaveViewState();
        }

        #endregion
        
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                if (Int64.TryParse(query.ObterOValorDoParametro("seqLacreRepositorio"), out this.seqLacreRepositorio) == true)
                {
                    hdnseqLacreRepositorio.Value = seqLacreRepositorio.ToString();

                    //this.seqLacreRepositorio = 8;

                    if (this.seqLacreRepositorio > 0)
                    {
                        this.CarregarInterface();

                        CarregarGridItensConsumidos();

                        CarregarGridLacresComplemento();
                    }
                    else
                    {
                        Response.End();
                    }
                }
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Conferencia/RegistrarConferenciaDeLacre.aspx", false);
        }

        protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Entity.LacreRepositorioItens lacreRepItens = (Entity.LacreRepositorioItens)e.Row.DataItem;
                   
                    Label lblValidade = (Label)e.Row.FindControl("lblValidade");
                    if (lacreRepItens.Lote != null && lacreRepItens.Lote.DataValidadeLote.Year > 1900)
                    {
                        lblValidade.Text = lacreRepItens.Lote.DataValidadeLote.ToString("dd/MM/yyyy");
                        lblValidade.Visible = true;
                    }

                    Int32 numDiasPeriodo = BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMedicamento;

                    
                    // Medicamentos que estao dentro do prazo de validade 
                    if (lacreRepItens.QtdDisponivel > 0 && lacreRepItens.ItensListaControle.Alinea.IdfClasse == 2 && (
                        ((lacreRepItens.Lote.DataValidadeLote - DateTime.Now.Date).Days <= numDiasPeriodo)))
                    {
                        e.Row.Cells[7].Attributes.Add("data-toggle", "tooltip");
                        e.Row.Cells[7].Attributes.Add("title", "Item dentro do prazo de validade !");
                        e.Row.Cells[7].CssClass = "alert alert-warning exibirAlerta";

                    }
                        // Materiais que estao dentro do prazo de validade
                    else if (lacreRepItens.QtdDisponivel > 0 && lacreRepItens.ItensListaControle.Alinea.IdfClasse != 2 && (
                        ((lacreRepItens.Lote.DataValidadeLote - DateTime.Now.Date).Days <= BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMaterial)))
                    {
                        e.Row.Cells[7].Attributes.Add("data-toggle", "tooltip");
                        e.Row.Cells[7].Attributes.Add("title", "Item dentro do prazo de validade !");
                        e.Row.Cells[7].CssClass = "alert alert-warning exibirAlerta";
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void rptLacreOcorrenciaRegistradas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Entity.LacreOcorrencia lacreOcorrencia = (Entity.LacreOcorrencia)e.Item.DataItem;

                    if (lacreOcorrencia != null)
                    {
                        Label lblDataOcorrenciaRegistrada = (Label)e.Item.FindControl("lblDataOcorrenciaRegistrada");

                        if (lacreOcorrencia.UsuarioCadastro != null &&
                            !string.IsNullOrWhiteSpace(lacreOcorrencia.UsuarioCadastro.Nome))
                        {
                            lblDataOcorrenciaRegistrada.Text = string.Format("{0} - {1}", lacreOcorrencia.UsuarioCadastro.Nome, lacreOcorrencia.DataCadastro.ToString("dd/MM/yyyy HH:mm"));
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnRegistrarConferencia_Click(object sender, EventArgs e)
        {
            RegistrarConferenciaChecagem();
        }

        protected void grdItensPassado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Entity.LacreRepositorioItens lacreRepItens = (Entity.LacreRepositorioItens)e.Row.DataItem;

                    Label lblValidade = (Label)e.Row.FindControl("lblValidade");
                    if (lacreRepItens.Lote != null && lacreRepItens.Lote.DataValidadeLote.Year > 1900)
                    {
                        lblValidade.Text = lacreRepItens.Lote.DataValidadeLote.ToString("dd/MM/yyyy");
                        lblValidade.Visible = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Carregar histórico de lacração.
        /// </summary>
        protected void CarregarHistoricoDeLacracao()
        {
            try
            {
                this.grvHistLacracao.DataSource = new BLL.HistoricoLacreRepositorio().ObterPorSeqLacreRepositorio(this.seqLacreRepositorio, false);
                this.grvHistLacracao.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar histórico de checagem.
        /// </summary>
        protected void CarregarHistoricoDeChecagem()
        {
            try
            {
                this.grvHistChecagem.DataSource = new BLL.HistoricoLacreRepositorio().ObterPorSeqLacreRepositorio(this.seqLacreRepositorio, true);
                this.grvHistChecagem.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar grid de itens.
        /// </summary>
        protected void CarregarGridItens()
        {
            try
            {
                this.grvItem.DataSource = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioItens().ObterItensNoLacreRepositorio(this.seqLacreRepositorio);
                this.grvItem.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lacre ocorrências.
        /// </summary>
        protected void CarregarLacreOcorrencia()
        {
            try
            {
                this.rptLacreOcorrenciaRegistradas.DataSource = new BLL.LacreOcorrencia().ObterPorSeqLacreRepositorio(this.seqLacreRepositorio);
                this.rptLacreOcorrenciaRegistradas.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar interface.
        /// </summary>
        protected void CarregarInterface()
        {
            try
            {
                Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio lacreRep = null;

                List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio> listLacreRep = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorio().ObterPorFiltro(this.seqLacreRepositorio,
                                                                                                                                                                null,
                                                                                                                                                                null,
                                                                                                                                                                null);
                if (listLacreRep != null &&
                    listLacreRep.Count > 0)
                {
                    lacreRep = listLacreRep[0];

                    List<Entity.ItensListaControle> itens = new List<Entity.ItensListaControle>();

                    BLL.ItensListaControle bllItensListaControle = new BLL.ItensListaControle();

                    hdnseqRepositorio.Value = lacreRep.RepositorioListaControle.SeqRepositorio.ToString();

                    itens = bllItensListaControle.ObterItensEmFaltaNoRepositorio(Convert.ToInt64(hdnseqRepositorio.Value));

                    if (itens.Count == 0)
                    {
                        divItensFaltantes.Visible = false;
                    }
                    else
                        lblContadorItensFaltantes.InnerText = itens.Count.ToString();

                    if (lacreRep.RepositorioListaControle != null &&
                        lacreRep.RepositorioListaControle.ListaControle != null &&
                        lacreRep.RepositorioListaControle.ListaControle.Instituto != null &&
                        !string.IsNullOrWhiteSpace(lacreRep.RepositorioListaControle.ListaControle.Instituto.NomeInstituto))
                    {
                        this.lblInstituto.Text = lacreRep.RepositorioListaControle.ListaControle.Instituto.NomeInstituto;
                    }

                    if (lacreRep.TipoSituacaoHc != null &&
                        !string.IsNullOrWhiteSpace(lacreRep.TipoSituacaoHc.NomSituacao))
                    {
                        this.lblSituacao.Text = lacreRep.TipoSituacaoHc.NomSituacao;

                        if (lacreRep.TipoSituacaoHc.CodSituacao == (Int64)BLL.Parametrizacao.Instancia().CodigoDaSituacaoRompido)
                        {
                            divRegistrarConferencia.Visible = false;
                        }
                    }

                    if (lacreRep.RepositorioListaControle != null &&
                        !string.IsNullOrWhiteSpace(lacreRep.RepositorioListaControle.DscIdentificacao))
                    {
                        this.lblPatrimonio.Text = lacreRep.RepositorioListaControle.DscIdentificacao;
                    }

                    if (lacreRep.RepositorioListaControle != null &&
                        lacreRep.RepositorioListaControle.ListaControle != null &&
                        !string.IsNullOrWhiteSpace(lacreRep.RepositorioListaControle.ListaControle.NomeListaControle))
                    {
                        this.lblTipoDaLista.Text = lacreRep.RepositorioListaControle.ListaControle.NomeListaControle;
                    }

                    if (lacreRep.LacreTipoOcorrencia != null &&
                        !string.IsNullOrWhiteSpace(lacreRep.LacreTipoOcorrencia.DscTipoOcorrencia))
                    {
                        this.lblMotivRompimentoDsc.Text = lacreRep.LacreTipoOcorrencia.DscTipoOcorrencia;
                    }

                    if (lacreRep.NumLacre != null)
                        this.lblLacresDsc.Text = lacreRep.NumLacre.Value.ToString();

                    //if (!string.IsNullOrWhiteSpace(lacreRep.NumCaixaIntubacao))
                    //    this.txtCaixaIntubacao.Text = lacreRep.NumCaixaIntubacao;

                    
                    this.CarregarLacreOcorrencia();
                    this.CarregarHistoricoDeLacracao();
                    this.CarregarHistoricoDeChecagem();

                    // se a situação for "rompido" trata exibição.
                    if (lacreRep.TipoSituacaoHc != null &&
                        lacreRep.TipoSituacaoHc.CodSituacao == BLL.Parametrizacao.Instancia().CodigoDaSituacaoRompido)
                    {
                        this.ExibirCamposParaSituacaoRompido();

                        
                    }
                    else
                    {
                        this.CarregarGridItens();
                    }

                    if (lacreRep.TipoSituacaoHc.CodSituacao != BLL.Parametrizacao.Instancia().CodigoDaSituacaoLacrado)
                    {
                        CarregarGridItensPassado();
                    }

                    if (lacreRep.TipoSituacaoHc.CodSituacao == BLL.Parametrizacao.Instancia().CodigoDaSituacaoLacrado)
                    {
                        pnItensPassado.Visible = false;
                    }


                    // Obter os itens do repositório para contador.
                    List<Entity.LacreRepositorioItens> listRepItens =
                        new BLL.LacreRepositorioItens().ObterPorLacreRepositorio(seqLacreRepositorio, null);
                    
                    #region validar qtd necessário com qtd disponível

                    var queryLastNames =
                        (from student in listRepItens
                         where student.QtdDisponivel > 0
                         group student by new
                         {
                             student.Material.Codigo,
                             student.Material.Nome
                         } into newGroup
                         //orderby newGroup.Key
                         select new Entity.LacreRepositorioItens()
                         {
                             Material = new Material()
                             {
                                 Codigo = Convert.ToString(newGroup.Key.Codigo),
                                 Nome = newGroup.Key.Nome
                             },
                             ItensListaControle = new ItensListaControle()
                             {
                                 QuantidadeNecessaria =
                                     newGroup.Max(
                                         x => x.ItensListaControle.QuantidadeNecessaria)
                             },
                             QtdDisponivel = newGroup.Sum(x => x.QtdDisponivel)
                         }).ToList();

                    var contaItensComQtdDifrente = (from contaItensComQtdDifrenteL in queryLastNames
                                                    where (contaItensComQtdDifrenteL.QtdDisponivel != contaItensComQtdDifrenteL.ItensListaControle.QuantidadeNecessaria)
                                                    && (contaItensComQtdDifrenteL.QtdDisponivel > 0)
                                                    select contaItensComQtdDifrenteL).Count();

                    lblContadorQtdsDiferentes.InnerText = contaItensComQtdDifrente.ToString();
                    #endregion

                    #region validar data de validade do item

                    // Verificar se o prazo de validade dos itens se encontra dentro do período.
                    Int32 numDiasPeriodo = BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMedicamento;
                    TimeSpan timeSpan;

                    var contaItensDataValidaAVencer = (from item in listRepItens
                                                       where ((item.Lote.DataValidadeLote - DateTime.Now.Date).Days <= numDiasPeriodo) 
                                                       && (item.QtdDisponivel > 0)
                                                       && item.ItensListaControle.Alinea.IdfClasse == 2
                                                       select item).Count();

                    var contaMateriaisDataValidaAVencer = (from item in listRepItens
                                                       where ((item.Lote.DataValidadeLote - DateTime.Now.Date).Days <= BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMaterial)
                                                       && (item.QtdDisponivel > 0)
                                                       && item.ItensListaControle.Alinea.IdfClasse != 2
                                                       select item).Count();

                    lblContadorVencidos.InnerText = contaItensDataValidaAVencer.ToString();

                    lblContadorMateriaisVencidos.InnerText = contaMateriaisDataValidaAVencer.ToString();

                    #endregion
                }
               
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Exibir campos para situação rompido.
        /// </summary>
        protected void ExibirCamposParaSituacaoRompido()
        {
            this.pnItens.Visible = false;
            this.lblMotivRompimento.Visible = true;
            this.lblMotivRompimentoDsc.Visible = true;

            //this.lblLacres.Visible = false;
            this.lblLacresDsc.Visible = false;
        }

        protected void RegistrarConferenciaChecagem()
        {
            try
            {
                if (this.seqLacreRepositorio > 0)
                {
                    Entity.LacreRepositorio lacreRep = new BLL.LacreRepositorio().ObterPorId(this.seqLacreRepositorio);

                    if (lacreRep != null)
                    {
                        Entity.HistoricoLacreRepositorio histLacreRep = new Entity.HistoricoLacreRepositorio();

                        histLacreRep.LacreRepositorio = lacreRep;
                        histLacreRep.DataCadastro = DateTime.Now;
                        histLacreRep.IdfConferencia = "S";
                        histLacreRep.TipoSituacaoHc = lacreRep.TipoSituacaoHc;

                        histLacreRep.UsuarioCadastro = new Entity.Usuario();
                        histLacreRep.UsuarioCadastro.NumUserBanco = Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;

                        new BLL.HistoricoLacreRepositorio().Adicionar(histLacreRep);

                        ExibirMensagemEmOutraPagina(TipoMensagem.Sucesso, "Conferência registrada com sucesso.");

                        Response.Redirect("RegistrarConferenciaDeLacre.aspx");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [WebMethod]
        public static string ObterItensPorListaControle(long seqLacreRepositorio)
        {
            //Na tela de importação de itens somente listar os ATIVOS
            List<Entity.LacreRepositorioItens> itens = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioItens().ObterItensNoLacreRepositorio(seqLacreRepositorio);

            return JsonConvert.SerializeObject(itens, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
        }

        [WebMethod]
        public static string ObterItensEmFaltaNoRepositorio(long seqRepositorio)
        {
            //Na tela de importação de itens somente listar os ATIVOS
            List<Entity.ItensListaControle> itens = new List<Entity.ItensListaControle>();

            BLL.ItensListaControle bllItensListaControle = new BLL.ItensListaControle();

            itens = bllItensListaControle.ObterItensEmFaltaNoRepositorio(seqRepositorio);

            return JsonConvert.SerializeObject(itens, Formatting.Indented, new JsonSerializerSettings { });
        }

        [WebMethod]
        public static string ObterItensComQuantidadeDisponivelDiferentes(long seqLacreRepositorio)
        {
            //Na tela de importação de itens somente listar os ATIVOS
            List<Entity.LacreRepositorioItens> itens = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioItens().ObterItensNoLacreRepositorio(seqLacreRepositorio);

            return JsonConvert.SerializeObject(itens);
        }
        
        [WebMethod]
        public static string ObterItensComQtdDiferente(int seqLacreRepositorio)
        {
            // Obter os itens do repostório.
            List<Entity.LacreRepositorioItens> listRepItens =
                new BLL.LacreRepositorioItens().ObterPorLacreRepositorio(seqLacreRepositorio, null);

            var grupoMateriais =
                (from student in listRepItens
                 group student by new {student.Material.Codigo,
                     student.Material.Nome
                 } into newGroup
                     //orderby newGroup.Key
                     select new Entity.LacreRepositorioItens()
                     {
                         Material = new Material()
                         {
                             Codigo = Convert.ToString(newGroup.Key.Codigo),
                             //Nome = newGroup.Select(x => x.Material.Nome).FirstOrDefault()
                             Nome = newGroup.Key.Nome
                         },
                         ItensListaControle = new ItensListaControle()
                         {
                             QuantidadeNecessaria = newGroup.Max(x => x.ItensListaControle.QuantidadeNecessaria),
                             Unidade = newGroup.Select(x => x.ItensListaControle.Unidade).FirstOrDefault()
                         },
                         QtdDisponivel = newGroup.Sum(x => x.QtdDisponivel)
                     }).ToList();

            var itensComQtdDifrente = (from contaItensComQtdDifrenteL in grupoMateriais
                                       where (contaItensComQtdDifrenteL.QtdDisponivel != contaItensComQtdDifrenteL.ItensListaControle.QuantidadeNecessaria)
                                       && (contaItensComQtdDifrenteL.QtdDisponivel > 0)
                                       select contaItensComQtdDifrenteL).ToList();

            return JsonConvert.SerializeObject(itensComQtdDifrente, Formatting.Indented, new JsonSerializerSettings { });
        }

        [WebMethod]
        public static string ObterItensVencidos(int seqLacreRepositorio)
        {
            // Obter os itens do repostório.
            List<Entity.LacreRepositorioItens> listRepItens =
                new BLL.LacreRepositorioItens().ObterPorLacreRepositorio(seqLacreRepositorio, null);


            #region validar data de validade do item

            // Verificar se o prazo de validade dos itens se encontra dentro do período.
            Int32 numDiasPeriodo = BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMedicamento;
            TimeSpan timeSpan;

            var itensDataValidaAVencer = (from item in listRepItens
                                          where ((item.Lote.DataValidadeLote - DateTime.Now.Date).Days <= numDiasPeriodo)
                                          && (item.QtdDisponivel > 0)
                                          && item.ItensListaControle.Alinea.IdfClasse == 2
                                          select item).ToList();

            #endregion

            return JsonConvert.SerializeObject(itensDataValidaAVencer, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
        }

        [WebMethod]
        public static string ObterMateriaisVencidos(int seqLacreRepositorio)
        {
            // Obter os itens do repostório.
            List<Entity.LacreRepositorioItens> listRepItens =
                new BLL.LacreRepositorioItens().ObterPorLacreRepositorio(seqLacreRepositorio, null);

            // Verificar se o prazo de validade dos itens se encontra dentro do período.
            Int32 numDiasPeriodo = BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMaterial;
            TimeSpan timeSpan;

            var itensDataValidaAVencer = (from item in listRepItens
                                          where ((item.Lote.DataValidadeLote - DateTime.Now.Date).Days <= numDiasPeriodo)
                                          && (item.QtdDisponivel > 0)
                                          && item.ItensListaControle.Alinea.IdfClasse != 2
                                          select item).ToList();

            return JsonConvert.SerializeObject(itensDataValidaAVencer, Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
        }

        private void CarregarGridItensConsumidos()
        {
            grdItensConsumidos.DataSource =
                new CarroUrgenciaPsicoativo.BLL.LacreRepositorioUtilizacao().ObterConsumoPorLacre(seqLacreRepositorio);

            grdItensConsumidos.DataBind();
        }

        private void CarregarGridItensPassado()
        {
            try
            {
                this.grdItensPassado.DataSource = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioItens().
                    ObterItensNoLacreRepositorio(this.seqLacreRepositorio);

                this.grdItensPassado.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void CarregarGridLacresComplemento()
        {

            var compLacre = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioComplemento().ObterComplemento(seqLacreRepositorio);

            if (compLacre.Count > 0)
            {
                grvLacreComplemento.DataSource = compLacre;
                grvLacreComplemento.DataBind();
            }

        }

        #endregion        
    }
}