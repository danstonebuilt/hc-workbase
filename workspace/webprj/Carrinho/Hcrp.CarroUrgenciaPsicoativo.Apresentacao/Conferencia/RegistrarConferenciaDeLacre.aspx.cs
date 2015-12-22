
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Hcrp.CarroUrgenciaPsicoativo.BLL;
using Hcrp.CarroUrgenciaPsicoativo.Entity;
using Newtonsoft.Json;
using Parametrizacao = Hcrp.Framework.Infra.Util.Parametrizacao;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Conferencia
{
    public partial class RegistrarConferenciaDeLacre : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
    {
        Int64 seqLacreRepositorio = 0;
        Int32 totalregGrv = 0;

        #region ViewState

        /// <summary>
        /// Carregar viewstate.
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            this.seqLacreRepositorio = Convert.ToInt64(ViewState["seqLacreRepositorio"]);
            this.totalregGrv = Convert.ToInt32(ViewState["totalregGrv"]);
        }

        /// <summary>
        /// Salvar ViewState
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            this.ViewState["seqLacreRepositorio"] = this.seqLacreRepositorio;
            this.ViewState["totalregGrv"] = this.totalregGrv;
            return base.SaveViewState();
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            this.paginador.PageSize = Hcrp.CarroUrgenciaPsicoativo.BLL.Parametrizacao.Instancia().QuantidadeRegistroPagina;
            this.paginador.Visible = false;
            this.paginador.ItemCount = this.totalregGrv;

            if (!Page.IsPostBack)
            {
                this.ConfigurarPeriodos();
                this.CarregarComboInstituto();
                this.CarregarComboPatrimonio();
            }
        }

        protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio lacreRep = (Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio)e.Row.DataItem;

                    if (lacreRep != null)
                    {
                        Label lblNumLacre = (Label)e.Row.FindControl("lblNumLacre");

                        if (lacreRep.NumLacre != null)
                            lblNumLacre.Text = lacreRep.NumLacre.Value.ToString();

                        Label lblNumLacreIntubacao = (Label)e.Row.FindControl("lblNumLacreIntubacao");

                        if (!string.IsNullOrWhiteSpace(lacreRep.NumCaixaIntubacao))
                            lblNumLacreIntubacao.Text = lacreRep.NumCaixaIntubacao;
                        else
                        {
                            var compLacre = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioComplemento().ObterComplemento(lacreRep.SeqLacreRepositorio);
                            if (compLacre.Count > 0)
                            {
                                lblNumLacreIntubacao.Text = "<a id=\"popComp_" + lacreRep.SeqLacreRepositorio + "\" type=\"button\" class=\"btn btn-default btn-sm exibeToolTip \"><span class=\"glyphicon glyphicon-paperclip\" aria-hidden=\"true\"></span></a>";
                                GridView grvLacreComplemento = (GridView)e.Row.FindControl("grvLacreComplemento");
                                grvLacreComplemento.DataSource = compLacre;
                                grvLacreComplemento.DataBind();
                            }
                        }

                        Label lblSeqLacreRepositorio = (Label)e.Row.FindControl("lblSeqLacreRepositorio");
                        if (lacreRep.SeqLacreRepositorio > 0)
                            lblSeqLacreRepositorio.Text = lacreRep.SeqLacreRepositorio.ToString();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void ddlPatrimonio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.paginador.CurrentIndex = 1;
                this.CarregarLacreRepositorio();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();

                if (!string.IsNullOrWhiteSpace(this.hdfTipoComando.Value))
                {
                    if (this.hdfTipoComando.Value == "NOVA_LACRACAO")
                    {
                        this.GerarNovaLacracao();
                    }
                    else if (this.hdfTipoComando.Value == "EXCLUIR")
                    {
                        this.Excluir();
                    }
                    else if (this.hdfTipoComando.Value == "EDITAR")
                    {
                        this.Editar();
                    }
                    else if (this.hdfTipoComando.Value == "VISUALIZAR")
                    {
                        this.VisualizarCarrinho();
                    }
                    else if (this.hdfTipoComando.Value == "REGISTRAR_OCORRENCIA")
                    {
                        this.RegistrarOcorrenciaCarrinho();
                    }
                    else if (this.hdfTipoComando.Value == "OCORRENCIA_REGISTRADA")
                    {
                        this.SinalizarOcorrenciaCarrinhoRegistrada();
                    }
                    else if (this.hdfTipoComando.Value == "CONFERENCIA")
                    {
                        this.RegistrarConferenciaChecagem();
                    }
                    else if (this.hdfTipoComando.Value == "QUEBRAR_LACRE")
                    {
                        this.QuebrarLacreDoCarrinho();
                    }   
                    else if (this.hdfTipoComando.Value == "LACRE_ROMPIDO")
                    {
                        this.SinalizarLacreRompido();
                    }
                    else if(this.hdfTipoComando.Value == "SALVAR_OCORRENCIA")
                    {
                        AdicionarOcorrencia();
                    }
                    else if (this.hdfTipoComando.Value == "SALVAR_QUEBRA_LACRE")
                    {
                        QuebrarLacre_Click();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void grvItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SEL")
                {
                    this.seqLacreRepositorio = Convert.ToInt64(e.CommandArgument);

                    ConfiguraAcoesParaOCarrinho();

                    ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), "modal", "$('#modalAcoes').modal('show')", true);


                    uppOpcoes.Update();
                    //Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                    //query.Parametros.Add("idCampoRetornoComando", this.hdfTipoComando.ClientID);
                    //query.Parametros.Add("seqLacreRepositorio", this.seqLacreRepositorio.ToString());
                    //query.Url = Page.ResolveUrl("~/Conferencia/AcaoRegistrarConferenciaDeLacre.aspx");

                    //this.AbrirModal_NEW(query.ObterUrlCriptografada(), 300, 193,"post()");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void ddlInstituto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.CarregarComboPatrimonio();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                this.paginador.CurrentIndex = 1;
                this.CarregarLacreRepositorio();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void ddlTipoOcorrencia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Métodos

        protected void paginador_Command(object sender, CommandEventArgs e)
        {
            int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
            this.paginador.CurrentIndex = currnetPageIndx;
            this.CarregarLacreRepositorio();
        }

        /// <summary>
        /// Quebrar lacre do carrinho.
        /// </summary>
        protected void QuebrarLacreDoCarrinho()
        {
            try
            {
                if (this.seqLacreRepositorio > 0)
                {
                    Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                    query.Parametros.Add("seqLacreRepositorio", this.seqLacreRepositorio.ToString());
                    query.Parametros.Add("idCampoRetornoComando", this.hdfTipoComando.ClientID);
                    query.Url = Page.ResolveUrl("~/Conferencia/QuebrarLacre.aspx");
                    this.AbrirModal(query.ObterUrlCriptografada(), 600, 350);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Sinalizar lacre rompido.
        /// </summary>
        protected void SinalizarLacreRompido()
        {
            try
            {
                this.ExibirMensagem(TipoMensagem.Sucesso, "Lacre rompido.");

                this.paginador.CurrentIndex = 1;
                this.CarregarLacreRepositorio();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Registrar conferencia/checagem.
        /// </summary>
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

                        this.ExibirMensagem(TipoMensagem.Sucesso, "Conferência registrada com sucesso.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Sinalizar ocorrência registrada.
        /// </summary>
        protected void SinalizarOcorrenciaCarrinhoRegistrada()
        {
            try
            {
                this.ExibirMensagem(TipoMensagem.Sucesso, "Ocorrência registrada.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Registrar ocorrência carrinho.
        /// </summary>
        protected void RegistrarOcorrenciaCarrinho()
        {
            try
            {
                if (this.seqLacreRepositorio > 0)
                {
                    Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                    query.Parametros.Add("seqLacreRepositorio", this.seqLacreRepositorio.ToString());
                    query.Parametros.Add("idCampoRetornoComando", this.hdfTipoComando.ClientID);
                    query.Url = Page.ResolveUrl("~/Conferencia/RegistrarOcorrencia.aspx");
                    this.AbrirModal(query.ObterUrlCriptografada(), 600, 250);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Visualizar carrinho.
        /// </summary>
        protected void VisualizarCarrinho()
        {
            try
            {
                if (this.seqLacreRepositorio > 0)
                {
                    Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                    query.Parametros.Add("seqLacreRepositorio", this.seqLacreRepositorio.ToString());
                    query.Url = "~/Conferencia/VisualizarConferencia.aspx";
                    Response.Redirect(query.ObterUrlCriptografada(), false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Editar carrinho.
        /// </summary>
        protected void Editar()
        {
            try
            {
                if (this.seqLacreRepositorio > 0)
                {
                    Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                    query.Parametros.Add("seqLacreRepositorio", this.seqLacreRepositorio.ToString());
                    query.Url = "~/Conferencia/GerenciarConferenciaDeLacre.aspx";
                    Response.Redirect(query.ObterUrlCriptografada(), false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Excluir carrinho.
        /// </summary>
        protected void Excluir()
        {
            try
            {
                // Transação iniciar
                Hcrp.Infra.AcessoDado.TransacaoDinamica transacao = new Hcrp.Infra.AcessoDado.TransacaoDinamica();

                // historico
                new Hcrp.CarroUrgenciaPsicoativo.BLL.HistoricoLacreRepositorio().ExcluirPorlacreRepositorioTrans(transacao, this.seqLacreRepositorio);

                // lacre equipamento
                new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioEquipamento().ExcluirPorLacreRepositorioTrans(transacao, this.seqLacreRepositorio);

                // carrinho
                new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorio().ExcluirTrans(transacao, this.seqLacreRepositorio);

                transacao.ComitarTransacao();

                this.paginador.CurrentIndex = 1;
                this.CarregarLacreRepositorio();

                this.ExibirMensagem(TipoMensagem.Sucesso, "Carro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower().Contains("constraint"))
                {
                    base.ExibirMensagem(TipoMensagem.Erro, "Este registro possui relacionamento e não poderá ser excluído.");
                    return;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Gerar Nova lacração.
        /// </summary>
        protected void GerarNovaLacracao()
        {
            try
            {
                long seqGerado = 0;

                // Lacrando um repositorio rompido
                if (seqLacreRepositorio > 1)
                {
                    Hcrp.Infra.AcessoDado.TransacaoDinamica transacao = new Hcrp.Infra.AcessoDado.TransacaoDinamica();

                    seqGerado = new BLL.LacreRepositorio().GerarLacreProvisorio(transacao, this.seqLacreRepositorio);

                    Entity.LacreRepositorio _lacreRep = new BLL.LacreRepositorio().ObterPorIdTrans(transacao,seqGerado);

                    if (_lacreRep != null)
                    {
                        Entity.HistoricoLacreRepositorio histLacreRep = new Entity.HistoricoLacreRepositorio();

                        histLacreRep.LacreRepositorio = _lacreRep;
                        histLacreRep.DataCadastro = DateTime.Now;

                        histLacreRep.IdfConferencia = "N";

                        Entity.TipoSituacaoHc sit = new TipoSituacaoHc()
                        {
                            CodSituacao = _lacreRep.TipoSituacaoHc.CodSituacao
                        };

                        histLacreRep.TipoSituacaoHc = sit;

                        histLacreRep.UsuarioCadastro = new Entity.Usuario();

                        histLacreRep.UsuarioCadastro.NumUserBanco = Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;

                        new BLL.HistoricoLacreRepositorio().AdicionarTrans(transacao, histLacreRep);
                    }

                    transacao.ComitarTransacao();
                }
                else
                {
                    // Gravar lacre repositorio
                    Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio lacreRepositorio =
                        new Entity.LacreRepositorio();

                    lacreRepositorio.RepositorioListaControle = new Entity.RepositorioListaControle();
                    lacreRepositorio.RepositorioListaControle.SeqRepositorio =
                        Convert.ToInt64(this.ddlPatrimonio.SelectedValue);

                    lacreRepositorio.UsuarioCadastro = new Entity.Usuario();
                    lacreRepositorio.UsuarioCadastro.NumUserBanco =
                        Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;

                    lacreRepositorio.DataCadastro = DateTime.Now;

                    lacreRepositorio.TipoSituacaoHc = new Entity.TipoSituacaoHc();
                    lacreRepositorio.TipoSituacaoHc.CodSituacao =
                        Hcrp.CarroUrgenciaPsicoativo.BLL.Parametrizacao.Instancia().CodigoDaSituacaoProvisorio;

                    lacreRepositorio.LacreTipoOcorrencia = new Entity.LacreTipoOcorrencia();
                    lacreRepositorio.LacreTipoOcorrencia.SeqLacreTipoOCorrencia =
                        Hcrp.CarroUrgenciaPsicoativo.BLL.Parametrizacao.Instancia().SeqTipoOcorrenciaNovaLacracao;

                    seqGerado =
                        new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorio().Adicionar(lacreRepositorio);

                    Entity.LacreRepositorio _lacreRep = new BLL.LacreRepositorio().ObterPorId(seqGerado);

                    if (_lacreRep != null)
                    {
                        Entity.HistoricoLacreRepositorio histLacreRep = new Entity.HistoricoLacreRepositorio();

                        histLacreRep.LacreRepositorio = _lacreRep;
                        histLacreRep.DataCadastro = DateTime.Now;

                        histLacreRep.IdfConferencia = "N";

                        Entity.TipoSituacaoHc sit = new TipoSituacaoHc()
                        {
                            CodSituacao = _lacreRep.TipoSituacaoHc.CodSituacao
                        };

                        histLacreRep.TipoSituacaoHc = sit;

                        histLacreRep.UsuarioCadastro = new Entity.Usuario();

                        histLacreRep.UsuarioCadastro.NumUserBanco = Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;

                        new BLL.HistoricoLacreRepositorio().Adicionar(histLacreRep);
                    }

                }

                // direcionar
                Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                query.Url = "~/Conferencia/GerenciarConferenciaDeLacre.aspx";
                query.Parametros.Add("seqLacreRepositorio", seqGerado.ToString());

                Response.Redirect(query.ObterUrlCriptografada(), false);
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        public string GetPostGrid()
        {
            PostBackOptions options = new PostBackOptions(btnPost);
            Page.ClientScript.RegisterForEventValidation(options);
            return Page.ClientScript.GetPostBackEventReference(options);
        }

        /// <summary>
        /// Configurar períodos.
        /// </summary>
        protected void ConfigurarPeriodos()
        {
            try
            {
                this.txtPeriodoDe.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
                this.txtPeriodoAte.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar lacre repositório.
        /// </summary>
        protected void CarregarLacreRepositorio()
        {
            try
            {
                if (this.ddlPatrimonio.SelectedValue == "0" ||
                    this.ddlPatrimonio.SelectedValue == "")
                {
                    this.ExibirMensagem(TipoMensagem.Alerta, "Informe o patrimônio");
                    return;
                }

                DateTime? periodoInicial = null;
                DateTime? periodoFinal = null;
                DateTime periodoAux = DateTime.MinValue;

                //if (DateTime.TryParse(this.txtPeriodoDe.Text, out periodoAux) == true)
                //{
                //    periodoInicial = periodoAux;
                //}
                //else
                //{
                //    this.ExibirMensagem(TipoMensagem.Alerta, "Período inicial inválido.");
                //    return;
                //}

                //if (DateTime.TryParse(this.txtPeriodoAte.Text, out periodoAux) == true)
                //{
                //    periodoFinal = periodoAux.AddHours(23).AddMinutes(59).AddSeconds(59);
                //}
                //else
                //{
                //    this.ExibirMensagem(TipoMensagem.Alerta, "Período final inválido.");
                //    return;
                //}

                //if (periodoInicial > periodoFinal)
                //{
                //    this.ExibirMensagem(TipoMensagem.Alerta, "Período inicial não pode maior que o periodo final.");
                //    return;
                //}


                if (this.ddlPatrimonio.SelectedValue != "0" &&
                    this.ddlPatrimonio.SelectedValue != "")
                {

                    
                    

                    int totalRegistro = 0;

                    List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio> listLacreRep = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorio().ObterPorFiltroPaginado(null,
                                                                                                                                                                            Convert.ToInt64(this.ddlPatrimonio.SelectedValue),
                                                                                                                                                                            periodoInicial,
                                                                                                                                                                            periodoFinal,
                                                                                                                                                                            this.paginador.CurrentIndex,
                                                                                                                                                                            out totalRegistro);
                    this.paginador.ItemCount = totalRegistro;
                    this.totalregGrv = totalRegistro;

                    

                    //if (listLacreRep != null)
                    //{
                    //    foreach (Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio item in listLacreRep)
                    //    {
                    //        listFinal.Add(item);
                    //    }
                    //}


                    // Adiciona uma linha em branco quando a consulta nao retornar registro.
                    // Para a consulta nao retornar registro o usuário estará fazendo a primeira lacração do carrinho.
                    if (listLacreRep.Count == 0)
                    {
                        // Sempre adicionar uma linha em branco na grid com a opção de gerar novo carrinho.
                        List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio> listFinal =
                            new List<Entity.LacreRepositorio>();

                        Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio lacreNovo = new Entity.LacreRepositorio();
                        lacreNovo.SeqLacreRepositorio = -1;

                        listFinal.Add(lacreNovo);

                        this.grvItem.DataSource = listFinal;
                    }
                    else
                    {
                        this.grvItem.DataSource = listLacreRep;
                    }

                    this.grvItem.AllowPaging = false;

                    if (totalRegistro > 0)
                        this.paginador.Visible = true;
                    else
                        this.paginador.Visible = false;

                    this.grvItem.DataBind();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar combo instituto.
        /// </summary>
        protected void CarregarComboInstituto()
        {
            try
            {
                int codInstituicao = Framework.Infra.Util.Parametrizacao.Instancia().CodInstituicao;

                this.ddlInstituto.DataValueField = "CodInstituto";
                this.ddlInstituto.DataTextField = "NomeInstituto";

                this.ddlInstituto.DataSource = new Framework.Classes.Instituto().BuscarInstitutosPorInstituicao(codInstituicao);
                
                this.ddlInstituto.DataBind();
                this.ddlInstituto.Items.Insert(0, new ListItem("SELECIONE", "0"));


                 ddlInstituto.SelectedValue = Parametrizacao.Instancia().CodInstituto.ToString();

                 //ddlInstituto_SelectedIndexChanged(null,null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar combo patrimonio.
        /// </summary>
        protected void CarregarComboPatrimonio()
        {
            try
            {
                this.ddlPatrimonio.Items.Clear();

                if (this.ddlInstituto.SelectedValue != "" &&
                    this.ddlInstituto.SelectedValue != "0")
                {
                    this.ddlPatrimonio.DataValueField = "SeqRepositorio";
                    this.ddlPatrimonio.DataTextField = "DscIdentificacao";

                    string roles = BLL.Parametrizacao.Instancia().CodigoDaRoleUsuario.ToString() + "," +
                                   BLL.Parametrizacao.Instancia().CodigoDaRoleEnfermeiro.ToString();

                    // 
                    if (TipoUsuarioLogado.Any(x=> x.Nome == "RL_CAR_EMERGENCIA_ADM"))
                    {
                        this.ddlPatrimonio.DataSource =
                        new Hcrp.CarroUrgenciaPsicoativo.BLL.RepositorioListaControle().ObterPorInstituto(
                            Convert.ToInt32(this.ddlInstituto.SelectedValue));
                    }
                    else
                    {
                        this.ddlPatrimonio.DataSource = new Hcrp.CarroUrgenciaPsicoativo.BLL.RepositorioListaControle().
                            ObterPorInstitutoCentroCustoUsuarioLogado(Convert.ToInt32(this.ddlInstituto.SelectedValue),
                                                                      roles);
                    }

                    this.ddlPatrimonio.DataBind();

                }

                if (ddlPatrimonio.Items.Count == 0)
                {
                    ExibirMensagem(TipoMensagem.Alerta,"Você não está associado a nenhum centro de custo do instituto.");
                    return;
                }

                this.ddlPatrimonio.Items.Insert(0, new ListItem("SELECIONE", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ConfiguraAcoesParaOCarrinho()
        {
            try
            {
                trConferencia.Visible = false;
                trEditar.Visible = false;
                trExcluir.Visible = false;
                trNovaLacracao.Visible = false;
                trQuebrarLacre.Visible = false;
                trRegistrarOcorrencia.Visible = false;
                trVisualizar.Visible = false;


                if (this.seqLacreRepositorio > 0)
                {

                    Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio lacreRep = null;
                    List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio> listLacreRep =
                        new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorio().ObterPorFiltro(this.seqLacreRepositorio, null, null, null);

                    

                    if (listLacreRep != null && listLacreRep.Count > 0)
                        lacreRep = listLacreRep[0];

                    if (lacreRep != null)
                    {
                        Int64? ultimoLacre =
                            new BLL.LacreRepositorio().ObterUltimoLacreRepositorio(
                                lacreRep.RepositorioListaControle.SeqRepositorio);

                        if (!string.IsNullOrWhiteSpace(lacreRep.ExisteLancamentoDeMaterial) &&
                            lacreRep.ExisteLancamentoDeMaterial == "N")
                        {
                            this.trExcluir.Visible = true;
                        }

                        if (lacreRep.TipoSituacaoHc != null)
                        {
                            this.trVisualizar.Visible = true;
                            //this.trConferencia.Visible = true;

                            if (lacreRep.TipoSituacaoHc.CodSituacao == (Int64)BLL.Parametrizacao.Instancia().CodigoDaSituacaoProvisorio)
                            {
                                this.trEditar.Visible = true;
                            }

                            if (lacreRep.TipoSituacaoHc.CodSituacao == (Int64)BLL.Parametrizacao.Instancia().CodigoDaSituacaoProvisorio ||
                                lacreRep.TipoSituacaoHc.CodSituacao == (Int64)BLL.Parametrizacao.Instancia().CodigoDaSituacaoLacrado)
                            {
                                this.trRegistrarOcorrencia.Visible = true;
                            }

                            if (lacreRep.TipoSituacaoHc.CodSituacao == (Int64)BLL.Parametrizacao.Instancia().CodigoDaSituacaoLacrado)
                            {
                                this.trQuebrarLacre.Visible = true;
                            }

                            if (lacreRep.TipoSituacaoHc.CodSituacao == (Int64)BLL.Parametrizacao.Instancia().CodigoDaSituacaoRompido && (lacreRep.SeqLacreRepositorio == ultimoLacre))
                            {
                                this.trNovaLacracao.Visible = true;
                            }
                        }
                    }
                }
                else if (this.seqLacreRepositorio == -1)
                {
                    this.trNovaLacracao.Visible = true;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AdicionarOcorrencia()
        {
            try
            {
                if (Page.IsValid)
                {
                    Entity.LacreOcorrencia lacreOcorrencia = new Entity.LacreOcorrencia();

                    lacreOcorrencia.LacreRepositorio = new Entity.LacreRepositorio();
                    lacreOcorrencia.LacreRepositorio.SeqLacreRepositorio = this.seqLacreRepositorio;

                    if (!string.IsNullOrWhiteSpace(this.txtDscOcorrencia.Text))
                        lacreOcorrencia.DscOcorrencia = this.txtDscOcorrencia.Text.ToUpper().ToString();

                    lacreOcorrencia.DataCadastro = DateTime.Now;

                    lacreOcorrencia.UsuarioCadastro = new Entity.Usuario();
                    lacreOcorrencia.UsuarioCadastro.NumUserBanco = Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;

                    new BLL.LacreOcorrencia().Adicionar(lacreOcorrencia);

                    ExibirMensagem(TipoMensagem.Sucesso,"Ocorrência registrada com sucesso.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar tipo de ocorrência.
        /// </summary>
        [WebMethod]
        public static string CarregarTipoOcorrencia()
        {
            try
            {
                List<Entity.LacreTipoOcorrencia> result = new BLL.LacreTipoOcorrencia().ObterTodosAtivos();

                return JsonConvert.SerializeObject(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void EnviarEmailParaOsGestores(List<Entity.Usuario> listUsuarioGestor, string identificacaoCarro)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                string nomeUsuarioLogado = string.Empty;

                Framework.Classes.Usuario usuarioLogado = 
                    new Framework.Classes.Usuario().BuscarUsuarioCodigo(Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado);
                
                if (usuarioLogado != null && !string.IsNullOrWhiteSpace(usuarioLogado.NomeCompleto))
                    nomeUsuarioLogado = usuarioLogado.NomeCompleto;

                sb.AppendLine("<br /> ");
                sb.AppendLine("<br /> ");
                sb.AppendLine("Foi rompido um lacre de repositório sem a geração de um lacre provisório.");
                sb.AppendLine("<br /> ");
                sb.AppendLine("<br /> ");
                sb.AppendLine("Carro: ");
                sb.AppendLine(identificacaoCarro);
                sb.AppendLine("<br /> ");
                sb.AppendLine("<br /> ");
                sb.AppendLine("Usuário responsável: ");
                sb.AppendLine(nomeUsuarioLogado);
                sb.AppendLine("<br /> ");
                sb.AppendLine("<br /> ");
                sb.AppendLine("Data: ");
                sb.AppendLine(DateTime.Now.ToString("d/MM/yyyy HH:mm"));
                sb.AppendLine("<br /> ");
                sb.AppendLine("<br /> ");
                sb.AppendLine("Id do repositório: ");
                sb.AppendLine(this.seqLacreRepositorio.ToString());
                sb.AppendLine("<br /> ");
                sb.AppendLine("<br /> ");
                sb.AppendLine("Desrição da Ocorrência: ");
                sb.AppendLine(this.txtDscOcorrenciaQuebraLacre.InnerText.ToUpper().Trim());


                new BLL.Email().Enviar(sb.ToString(), listUsuarioGestor, "Ocorrência de lacração carro emergência - Lacre rompido");

            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void QuebrarLacre_Click()
        {
            try
            {
                // Verificar se carrinho ainda continua lacrado.
                Entity.LacreRepositorio lacreRep = new BLL.LacreRepositorio().ObterPorId(this.seqLacreRepositorio);

                if (lacreRep != null &&
                    lacreRep.TipoSituacaoHc != null &&
                    lacreRep.TipoSituacaoHc.CodSituacao == BLL.Parametrizacao.Instancia().CodigoDaSituacaoLacrado)
                {

                    // Sinalizar a quebra de lacre. e registra o historico
                    new BLL.LacreRepositorio().QuebrarLacreCarrinho(this.seqLacreRepositorio, Convert.ToInt64(hdnTipoOcorrencia.Value));

                    // Se foi informando ocorrencia, então gravar.
                    if (!string.IsNullOrWhiteSpace(this.txtDscOcorrenciaQuebraLacre.InnerText))
                    {
                        Entity.LacreOcorrencia lacreOcorrencia = new Entity.LacreOcorrencia();

                        lacreOcorrencia.DataCadastro = DateTime.Now;
                        lacreOcorrencia.DscOcorrencia = this.txtDscOcorrenciaQuebraLacre.InnerText.ToUpper().Trim();

                        lacreOcorrencia.LacreRepositorio = new Entity.LacreRepositorio();
                        lacreOcorrencia.LacreRepositorio.SeqLacreRepositorio = this.seqLacreRepositorio;

                        lacreOcorrencia.UsuarioCadastro = new Entity.Usuario();
                        lacreOcorrencia.UsuarioCadastro.NumUserBanco = Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;

                        new BLL.LacreOcorrencia().Adicionar(lacreOcorrencia);

                        // Enviar email para os gestores com a descrição feita pelo usuário.

                        List<Entity.Usuario> listUsuarioGestor = new BLL.Usuario().ObterOsUsuarioGestoresDoRepositorio(BLL.Parametrizacao.Instancia().CodigoDaRoleGestor, lacreRep.RepositorioListaControle.SeqRepositorio);
                        
                        if (listUsuarioGestor != null)
                        {
                            this.EnviarEmailParaOsGestores(listUsuarioGestor, lacreRep.RepositorioListaControle.TipoRepositorioListaControle.DscTipoRepositorio);
                        }
                    }

                    // Se foi checado para gerar lacre provisório, então gerar.
                    // Transação iniciar
                    if (this.chkgerarLacreProv.Checked == true)
                    {
                        Hcrp.Infra.AcessoDado.TransacaoDinamica transacao = new Hcrp.Infra.AcessoDado.TransacaoDinamica();

                        long seqGerado =  new BLL.LacreRepositorio().GerarLacreProvisorio(transacao, this.seqLacreRepositorio);
                        transacao.ComitarTransacao();

                        Entity.LacreRepositorio _lacreRep = new BLL.LacreRepositorio().ObterPorId(seqGerado);

                        if (_lacreRep != null)
                        {
                            Entity.HistoricoLacreRepositorio histLacreRep = new Entity.HistoricoLacreRepositorio();

                            histLacreRep.LacreRepositorio = _lacreRep;
                            histLacreRep.DataCadastro = DateTime.Now;

                            histLacreRep.IdfConferencia = "N";

                            Entity.TipoSituacaoHc sit = new TipoSituacaoHc()
                            {
                                CodSituacao = _lacreRep.TipoSituacaoHc.CodSituacao
                            };

                            histLacreRep.TipoSituacaoHc = sit;

                            histLacreRep.UsuarioCadastro = new Entity.Usuario();

                            histLacreRep.UsuarioCadastro.NumUserBanco = Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;

                            new BLL.HistoricoLacreRepositorio().Adicionar(histLacreRep);
                        }

                        Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                        query.Parametros.Add("seqLacreRepositorio", seqGerado.ToString());
                        query.Url = "~/Conferencia/GerenciarConferenciaDeLacre.aspx";
                        Response.Redirect(query.ObterUrlCriptografada(), false);
                    }
                    else
                    {
                        Response.Redirect("~/Conferencia/RegistrarConferenciaDeLacre.aspx", false);
                    }
                }

                // Page.ClientScript.RegisterStartupScript(typeof(string), "fecha", "retornoOperacao('LACRE_ROMPIDO','" + this.idCampoRetornoComando + "'); window.parent.$.fancybox.close();", true);
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        #endregion
    }
}