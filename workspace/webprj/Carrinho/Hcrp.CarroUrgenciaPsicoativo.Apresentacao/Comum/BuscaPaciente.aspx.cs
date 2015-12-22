using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Comum
{
    public partial class BuscaPaciente : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
    {
        string id = "", valor = "";

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
        }

        /// <summary>
        /// Salvar ViewState
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            this.ViewState["id"] = this.id;
            this.ViewState["valor"] = this.valor;
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

                this.txtNome.Focus();
            }
        }

        Framework.Classes.Paciente paciente;
        void grvDado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                paciente = e.Row.DataItem as Framework.Classes.Paciente;

                if (paciente != null)
                {
                    e.Row.Attributes.Add("onMouseOver", "this.className = 'Grid_linha_hover'");
                    e.Row.Attributes.Add("onMouseOut", "this.className = 'Grid_linha'");
                    e.Row.ToolTip = "Clique para selecionar este registro.";
                    e.Row.Attributes.Add("onclick", "setarValor('" + this.id + "','" + paciente.RegistroPaciente + "','" + this.valor + "','" + paciente.NomeCompletoPaciente + "'); window.parent.post(); window.parent.$.fancybox.close();");
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
                if ((string.IsNullOrWhiteSpace(this.txtNome.Text) || this.txtNome.Text.Length < 3) ||
                    (string.IsNullOrWhiteSpace(this.txtSobrenome.Text) || this.txtSobrenome.Text.Length < 3))
                {
                    base.ExibirMensagem(TipoMensagem.Alerta, "Informe pelo menos 3 caracteres em algum dos campos de pesquisa para realizar a busca.");
                    return;
                }

                Int32 totalRegistro = 0;

                this.grvDado.AllowPaging = false;

                // Busca lista 
                this.grvDado.DataSource = new Hcrp.CarroUrgenciaPsicoativo.BLL.Paciente().ObterListaDePaciente(this.txtNome.Text, this.txtSobrenome.Text, paginador.CurrentIndex, out totalRegistro);

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