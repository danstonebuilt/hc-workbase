using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Basico
{
    public partial class TipoLista : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            this.grvItem.RowDataBound += new GridViewRowEventHandler(grvItem_RowDataBound);

            if (!IsPostBack)
            {
                this.CarregarCombo();
            }
        }

        void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var item = e.Row.DataItem as Entity.ListaControle;
                //var lblCaixaIntubacao = e.Row.FindControl("lblCaixaIntubacao") as Label;
                var lblAtivo = e.Row.FindControl("lblAtivo") as Label;
                var lnkAtivarOuInativar = e.Row.FindControl("lnkAtivarOuInativar") as LinkButton;

                //if (item.IdfCaixaIntubacao.Equals(0))
                //    lblCaixaIntubacao.Text = "Não";
                //else
                //    lblCaixaIntubacao.Text = "Sim";

                if (item.IdfAtivo.Equals("S"))
                {
                    lnkAtivarOuInativar.Text = "<span class='glyphicon glyphicon-remove'></span>";
                    lnkAtivarOuInativar.ToolTip = "Inativar";
                    lblAtivo.Text = "Sim";
                    
                }
                else
                {
                    e.Row.Attributes["class"] = "danger";
                    lnkAtivarOuInativar.Text = "<span class='glyphicon glyphicon-ok'></span>";
                    lnkAtivarOuInativar.ToolTip = "Ativar";
                    lblAtivo.Text = "Não";
                }
            }
        }

        protected void ddlInstituto_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ExibirDadosParaOInstitutoSelecionado();
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidaCamposObrigatorios())
                {
                    Entity.ListaControle _listaControle = new Entity.ListaControle();
                    _listaControle.UsuarioCadastro = new Entity.Usuario();
                    _listaControle.Instituto = new Entity.Instituto();

                    _listaControle.NomeListaControle = this.txtNome.Text.Trim();
                    _listaControle.IdfCaixaIntubacao = (this.chkPossuiCaixa.Checked ? 1 : 0);
                    _listaControle.UsuarioCadastro.NumUserBanco =
                        Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;
                    _listaControle.Instituto.CodInstituto = Convert.ToInt32(this.ddlInstituto.SelectedValue);

                    // enviar para o bd
                    bool naoInseriu = false;

                    new BLL.ListaControle().Adicionar(_listaControle, out naoInseriu);

                    // emitir msg caso nao tenha conseguido inserir
                    if (naoInseriu)
                    {
                        base.ExibirMensagem(TipoMensagem.Alerta,
                                            "O nome informado já esta cadastrado. Por favor informe outro.");
                    }

                    this.txtNome.Text = "";
                    this.chkPossuiCaixa.Checked = false;

                    // recarregar dados
                    this.ExibirDadosParaOInstitutoSelecionado();
                }
            }
            catch (Exception err)
            {
                ErrorSignal.FromCurrentContext().Raise(err);
            }
        }

        protected void lnkAtivarOuInativar_Click(object sender, EventArgs e)
        {
            try
            {
                var seqListaControle = Convert.ToInt64(((LinkButton)sender).CommandArgument);
                var hdfAtivo = ((LinkButton)sender).Parent.FindControl("hdfAtivo") as HiddenField;

                if (hdfAtivo != null)
                {
                    new BLL.ListaControle().AtivarOuInativar((hdfAtivo.Value.Equals("N") ? true : false), seqListaControle, Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado);
                    this.ExibirDadosParaOInstitutoSelecionado();
                }
            }
            catch (Exception err)
            {
                ErrorSignal.FromCurrentContext().Raise(err);
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Menu.aspx", false);

        }

        #endregion

        #region Métodos

        private bool ValidaCamposObrigatorios()
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                ExibirMensagem(TipoMensagem.Erro,"O campo nome é obrigatório !");
                txtNome.Focus();
                return false;
            }

            return true;
        }

        private void CarregarCombo()
        {
            try
            {
                int codInstituicao = Framework.Infra.Util.Parametrizacao.Instancia().CodInstituicao;
                

                this.ddlInstituto.DataValueField = "CodInstituto";
                this.ddlInstituto.DataTextField = "NomeInstituto";
                this.ddlInstituto.DataSource = new Framework.Classes.Instituto().BuscarInstitutosPorInstituicao(codInstituicao);
                this.ddlInstituto.DataBind();
                this.ddlInstituto.Items.Insert(0, new ListItem("SELECIONE ", "0"));

            }
            catch (Exception err)
            {
                ErrorSignal.FromCurrentContext().Raise(err);
            }
        }

        private void ExibirDadosParaOInstitutoSelecionado()
        {
            try
            {
                this.pnlTipoLista.Visible = false;

                if (!string.IsNullOrWhiteSpace(this.ddlInstituto.SelectedValue) && !string.Equals(this.ddlInstituto.SelectedValue, "0"))
                {
                    this.pnlTipoLista.Visible = true;

                    // carregar o grid
                    this.grvItem.AllowPaging = false;
                    this.grvItem.DataSource = new BLL.ListaControle().ObterPorInstituto(Convert.ToInt32(this.ddlInstituto.SelectedValue),chkFiltraInativo.Checked ? "N" : "S");
                    this.grvItem.DataBind();
                }
                else
                {
                    this.grvItem.DataBind();
                }
            }
            catch (Exception err)
            {
                ErrorSignal.FromCurrentContext().Raise(err);
            }
        }

        #endregion

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            
            this.pnlTipoLista.Visible = true;

            // carregar o grid
            this.grvItem.AllowPaging = false;
                    
            this.grvItem.DataSource = new BLL.ListaControle().ObterPorInstituto(
                Convert.ToInt32((sender as LinkButton).CommandArgument));
                    
            this.grvItem.DataBind();


         
            
        }

        protected void chkFiltraInativo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.pnlTipoLista.Visible = false;

                if (!string.IsNullOrWhiteSpace(this.ddlInstituto.SelectedValue) && !string.Equals(this.ddlInstituto.SelectedValue, "0"))
                {
                    this.pnlTipoLista.Visible = true;

                    // carregar o grid
                    this.grvItem.AllowPaging = false;
                    this.grvItem.DataSource = new BLL.ListaControle().ObterPorInstituto(Convert.ToInt32(this.ddlInstituto.SelectedValue), chkFiltraInativo.Checked ? "N" : "S");
                    this.grvItem.DataBind();
                }
                else
                {
                    this.grvItem.DataBind();
                }
            }
            catch (Exception err)
            {
                ErrorSignal.FromCurrentContext().Raise(err);
            }
        }
    }
}