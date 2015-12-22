using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hcrp.Framework.Infra.Util;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.TrocaMaterial
{
    public partial class TrocaDeMaterial : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
    {
        Int32 totalregGrv = 0;

        #region ViewState

        /// <summary>
        /// Carregar viewstate.
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            this.totalregGrv = Convert.ToInt32(ViewState["totalregGrv"]);
        }

        /// <summary>
        /// Salvar ViewState
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
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
                this.CarregarComboInstituto();

                CarregarComboPatrimonio();

                this.CarregarQtdDiasVencimento();
            }
        }

        protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Entity.LacreRepositorioItens lacreRepItens = (Entity.LacreRepositorioItens)e.Row.DataItem;

                    Label lblDataVldLote = (Label)e.Row.FindControl("lblDataVldLote");

                    if (lacreRepItens.DataValidadeLote != null)
                        lblDataVldLote.Text = lacreRepItens.DataValidadeLote.Value.ToString("dd/MM/yyyy");
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
                CarregarComboPatrimonio();//CarregarMateriais();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/menu.aspx", false);
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            /*int qtdDiasVencer = BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMaterial;

            // Verificar se há itens a serem exibidos.
            var listLacreRepItem = new Hcrp.CarroUrgenciaPsicoativo.
                BLL.LacreRepositorioItens().ObterParaTrocaDematerial(
                Convert.ToInt32(this.ddlInstituto.SelectedValue), qtdDiasVencer);

            if (listLacreRepItem == null || listLacreRepItem.Count == 0)
            {
                this.ExibirMensagem(TipoMensagem.Alerta, "Não há itens a serem visualizados.");
                return;
            }

            */
            if (this.ddlInstituto.SelectedValue == "0" ||
                    this.ddlInstituto.SelectedValue == "")
            {
                this.ExibirMensagem(TipoMensagem.Alerta, "Informe o instituto");
                return;
            }

            if (this.ddlPatrimonio.SelectedValue == "0" ||
                    this.ddlPatrimonio.SelectedValue == "")
            {
                this.ExibirMensagem(TipoMensagem.Alerta, "Informe o repositório");
                return;
            }

            // Gerar um PDF para download !!!(Não precisa abrir o relatório em TELA);
            Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
            query.Url = "~/DownLoadArquivo.aspx";
            query.Parametros.Add("codInstituto", Convert.ToInt32(this.ddlInstituto.SelectedValue).ToString());
            query.Parametros.Add("codRepositorio", Convert.ToInt32(this.ddlPatrimonio.SelectedValue).ToString());
            query.Parametros.Add("tipo_arquivo", "TROCA_MATERIAL");
            Response.Redirect(query.ObterUrlCriptografada(), false);
        }

        #endregion

        #region Métodos

        protected void paginador_Command(object sender, CommandEventArgs e)
        {
            int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
            this.paginador.CurrentIndex = currnetPageIndx;
            this.CarregarMateriais();
        }

        /// <summary>
        /// Carregar materiais.
        /// </summary>
        protected void CarregarMateriais()
        {
            try
            {
                if (this.ddlInstituto.SelectedValue == "0" ||
                    this.ddlInstituto.SelectedValue == "")
                {
                    this.ExibirMensagem(TipoMensagem.Alerta, "Informe o instituto");
                    return;
                }

                int totalRegistro = 0;

                int qtdDiasVencer = BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMedicamento;


                List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens> listLacreRepItens = 
                    new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioItens().ObterParaTrocaDematerialPaginado(Convert.ToInt32(this.ddlInstituto.SelectedValue),
                                                                                                                Convert.ToInt64(this.ddlPatrimonio.SelectedValue),
                                                                                                                qtdDiasVencer,
                                                                                                                this.paginador.CurrentIndex,
                                                                                                                out totalRegistro);
                this.paginador.ItemCount = totalRegistro;
                this.totalregGrv = totalRegistro;

                this.grvItem.AllowPaging = false;

                this.grvItem.DataSource = listLacreRepItens;

                if (totalRegistro > 0)
                    this.paginador.Visible = true;
                else
                    this.paginador.Visible = false;

                this.grvItem.DataBind();

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar a quantidade de dias de vencimento.
        /// </summary>
        protected void CarregarQtdDiasVencimento()
        {
            try
            {
                this.lblDiasvencimento.Text = BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMedicamento.ToString();
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



                //ddlInstituto_SelectedIndexChanged(null, null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void CarregarComboPatrimonio()
        {
            try
            {
                this.ddlPatrimonio.Items.Clear();

                if (this.ddlInstituto.SelectedValue != "" &&
                    this.ddlInstituto.SelectedValue != "0")
                {

                    string roles = BLL.Parametrizacao.Instancia().CodigoDaRoleUsuario.ToString() + "," +
                                   BLL.Parametrizacao.Instancia().CodigoDaRoleEnfermeiro.ToString();

                    this.ddlPatrimonio.DataValueField = "SeqRepositorio";
                    this.ddlPatrimonio.DataTextField = "DscIdentificacao";

                    // Usuário LUCASEDU - LAMarques - MOALVES
                    // Possuem acesso a todos os carrinhos
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
                    ExibirMensagem(TipoMensagem.Alerta, "Você não está associado a nenhum centro de custo do instituto.");
                    return;
                }

                this.ddlPatrimonio.Items.Insert(0, new ListItem("SELECIONE", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        protected void ddlPatrimonio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CarregarMateriais();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnRequisitarMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                //this.hdfComando.Value = "BUSCA_PATRIMONIO_EQUIPAMENTO";

                Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                query.Url = Page.ResolveUrl("~/TrocaMaterial/RequisitarMaterial.aspx");
                query.Parametros.Add("seqRepositorio", ddlPatrimonio.SelectedValue.ToString());
                //query.Parametros.Add("valor", this.txtNumPatrimonio.ClientID);
                //query.Parametros.Add("codTipoPatrimonio", this.ddlTipoPatrimonio.SelectedValue);



                base.AbrirModal(query.ObterUrlCriptografada(), 740, 450);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}