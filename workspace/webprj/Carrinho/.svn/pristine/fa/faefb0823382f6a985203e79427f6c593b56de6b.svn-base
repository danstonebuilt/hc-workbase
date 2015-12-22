using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Comum
{
    public partial class BuscaPatrimonio : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
    {
        string id = "", valor = "";
        Int64 codTipoPatrimonio = 0;
        Int64 seqItemListaControle = 0;

        #region ViewState

        /// <summary>
        /// Carregar viewstate.
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            this.id = this.ViewState["id"].ToString();
            this.valor = this.ViewState["valor"].ToString();
            this.codTipoPatrimonio = Convert.ToInt64(this.ViewState["codTipoPatrimonio"]);
            this.seqItemListaControle = Convert.ToInt64(this.ViewState["seqItemListaControle"]);
        }

        /// <summary>
        /// Salvar ViewState
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            this.ViewState["id"] = this.id;
            this.ViewState["valor"] = this.valor;
            this.ViewState["codTipoPatrimonio"] = this.codTipoPatrimonio;
            this.ViewState["seqItemListaControle"] = this.seqItemListaControle;

            return base.SaveViewState();
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            //Configurar numero de registros a serem exibidos.
            this.paginador.PageSize = Hcrp.CarroUrgenciaPsicoativo.BLL.Parametrizacao.Instancia().QuantidadeRegistroPagina;
            this.paginador.Visible = false;
            this.grvDado.RowDataBound += new GridViewRowEventHandler(grvDado_RowDataBound);

            if (!IsPostBack)
            {
                Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();

                this.id = query.ObterOValorDoParametro("id");

                this.valor = query.ObterOValorDoParametro("valor");

                Int64.TryParse(query.ObterOValorDoParametro("codTipoPatrimonio"), out this.codTipoPatrimonio);

                Int64.TryParse(query.ObterOValorDoParametro("seqItemListaControle"), out this.seqItemListaControle);
                

                this.txtPatrimonio.Focus();

                if (this.codTipoPatrimonio == 0)
                    Response.End();
            }
        }

        Entity.BemPatrimonial bemPatrimonia;
        void grvDado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                bemPatrimonia = e.Row.DataItem as Entity.BemPatrimonial;

                if (bemPatrimonia != null)
                {
                    e.Row.Attributes.Add("onMouseOver", "this.className = 'Grid_linha_hover'");
                    e.Row.Attributes.Add("onMouseOut", "this.className = 'Grid_linha'");
                    e.Row.ToolTip = "Clique para selecionar este registro.";
                    e.Row.Attributes.Add("onclick", "setarValor('" + this.id + "','" + bemPatrimonia.NumBem.ToString() + "','" + this.valor + "','" + bemPatrimonia.DscModelo + "'); window.parent.post(); window.parent.$.fancybox.close();");
                }
            }
        }

        protected void btBuscar_Click(object sender, EventArgs e)
        {
            this.paginador.CurrentIndex = 1;
            this.CarregarGrid();
        }

        protected void paginador_Command(object sender, CommandEventArgs e)
        {
            int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
            this.paginador.CurrentIndex = currnetPageIndx;
            this.CarregarGrid();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Carregar grid.
        /// </summary>
        private void CarregarGrid()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.txtPatrimonio.Text) && string.IsNullOrWhiteSpace(this.txtDscComplementar.Text))
                {
                    base.ExibirMensagem(TipoMensagem.Alerta, "Informe pelo menos 3 caracteres em algum dos campos de pesquisa para realizar a busca.");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(this.txtDscComplementar.Text) && this.txtDscComplementar.Text.Length < 3)
                {
                    base.ExibirMensagem(TipoMensagem.Alerta, "Informe pelo menos 3 caracteres na desrição complementar para realizar a busca.");
                    return;
                }                
                

                Int64 numPatrimonioAux = 0;
                Int64? numPatrimonio = null;
                if (!string.IsNullOrWhiteSpace(this.txtPatrimonio.Text))
                {
                    if (Int64.TryParse(this.txtPatrimonio.Text, out numPatrimonioAux) == false)
                    {
                        base.ExibirMensagem(TipoMensagem.Alerta, "Número de patrimônio inválido.");
                        return;
                    }
                    else
                    {
                        numPatrimonio = numPatrimonioAux;
                    }
                }

                if (!string.IsNullOrWhiteSpace(this.txtDscComplementar.Text))
                    this.txtDscComplementar.Text = this.txtDscComplementar.Text.ToUpper().Trim();

                Int32 totalRegistro = 0;

                this.grvDado.AllowPaging = false;

                // Busca lista 
                this.grvDado.DataSource = new Hcrp.CarroUrgenciaPsicoativo.BLL.BemPatrimonial().ObterListaDeBensPatrimoniais(numPatrimonio,
                    this.txtDscComplementar.Text, this.codTipoPatrimonio, seqItemListaControle, paginador.CurrentIndex, out totalRegistro);

                this.paginador.ItemCount = totalRegistro;

                if (totalRegistro > 0)
                    this.paginador.Visible = true;
                else
                    this.paginador.Visible = false;

                this.grvDado.DataBind();
            }
            catch (Exception err)
            {
                ErrorSignal.FromCurrentContext().Raise(err);
            }
        }

        #endregion
    }
}