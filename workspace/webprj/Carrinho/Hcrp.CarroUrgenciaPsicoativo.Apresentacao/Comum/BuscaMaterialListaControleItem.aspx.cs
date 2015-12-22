using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Elmah;
using Hcrp.Framework.Classes;
using Newtonsoft.Json;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Comum
{
    public partial class BuscaMaterialListaControleItem : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
    {
        string id = "", valor = "";
        Int64 seqLacreRepositorio = 0;

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
            this.seqLacreRepositorio = Convert.ToInt64(this.ViewState["seqLacreRepositorio"]);
        }

        /// <summary>
        /// Salvar ViewState
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            this.ViewState["id"] = this.id;
            this.ViewState["valor"] = this.valor;
            this.ViewState["seqLacreRepositorio"] = this.seqLacreRepositorio;
            return base.SaveViewState();
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            //Configurar numero de registros a serem exibidos.
            //this.paginador.PageSize = Hcrp.CarroUrgenciaPsicoativo.BLL.Parametrizacao.Instancia().QuantidadeRegistroPagina;
            //this.paginador.Visible = false;
            //this.grvDado.RowDataBound += new GridViewRowEventHandler(grvDado_RowDataBound);

            //if (!IsPostBack)
            //{
            //    Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();

            //    this.id = query.ObterOValorDoParametro("id");

            //    this.valor = query.ObterOValorDoParametro("valor");

            //    Int64.TryParse(query.ObterOValorDoParametro("seqLacreRepositorio"), out this.seqLacreRepositorio);

            //    this.txtDescricao.Focus();
            //}
        }

        Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle itemListaControle;
        void grvDado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType == DataControlRowType.DataRow))
            {
                itemListaControle = e.Row.DataItem as Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle;

                if (itemListaControle != null)
                {
                    e.Row.Attributes.Add("onMouseOver", "this.className = 'Grid_linha_hover'");
                    e.Row.Attributes.Add("onMouseOut", "this.className = 'Grid_linha'");
                    e.Row.ToolTip = "Clique para selecionar este registro.";
                    e.Row.Attributes.Add("onclick", "setarValor('" + this.id + "','" + itemListaControle.SeqItensListaControle + "','" + this.valor + "','" + itemListaControle.Material.Nome + "'); window.parent.post(); window.parent.$.fancybox.close();");
                }
            }
        }

        protected void btBuscar_Click(object sender, EventArgs e)
        {
            //this.paginador.CurrentIndex = 1;
            this.CarregarGrid();
        }

        protected void paginador_Command(object sender, CommandEventArgs e)
        {
            int currnetPageIndx = Convert.ToInt32(e.CommandArgument);
            //this.paginador.CurrentIndex = currnetPageIndx;
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
                /*
                if (!string.IsNullOrWhiteSpace(this.txtDescricao.Text) &&
                    this.txtDescricao.Text.Length < 3)
                {
                    base.ExibirMensagem(TipoMensagem.Alerta, "Informe pelo menos 3 caracteres na dscrição para realizar a busca.");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(this.txtCodigo.Text) &&
                    this.txtCodigo.Text.Length < 3)
                {
                    base.ExibirMensagem(TipoMensagem.Alerta, "Informe pelo menos 3 caracteres no código para realizar a busca.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(this.txtCodigo.Text) &&
                    string.IsNullOrWhiteSpace(this.txtDescricao.Text))
                {
                    base.ExibirMensagem(TipoMensagem.Alerta, "Informe pelo menos 3 caracteres em algum dos campos de pesquisa para realizar a busca.");
                    return;
                }
                 */

                //if (!string.IsNullOrWhiteSpace(this.txtCodigo.Text))
                //    this.txtCodigo.Text = this.txtCodigo.Text.ToUpper();

                //if (!string.IsNullOrWhiteSpace(this.txtDescricao.Text))
                //    this.txtDescricao.Text = this.txtDescricao.Text.ToUpper();

                //Int32 totalRegistro = 0;

                //this.grvDado.AllowPaging = false;

                //// Busca lista 
                ////this.grvDado.DataSource = new Hcrp.CarroUrgenciaPsicoativo.BLL.ItensListaControle().ObterListaDeListaControleItemComMaterialComCodigoPaginado(this.seqLacreRepositorio,
                ////                                                                                                                                              this.txtCodigo.Text, 
                ////                                                                                                                                            this.txtDescricao.Text, 
                ////                                                                                                                                            paginador.CurrentIndex, 
                ////                                                                                                                                            out totalRegistro);

                //this.paginador.ItemCount = totalRegistro;

                //if (totalRegistro > 0)
                //    this.paginador.Visible = true;
                //else
                //    this.paginador.Visible = false;

                //this.grvDado.DataBind();
            }
            catch (Exception err)
            {
                ErrorSignal.FromCurrentContext().Raise(err);
            }
        }

        [WebMethod]
        public static string CarregarComboAlinea()
        {
            List<Framework.Classes.Alinea> listAlinea = new List<Alinea>();

            Framework.Dal.Alinea DalAlinea = new Framework.Dal.Alinea();

            listAlinea = DalAlinea.ObterListaDeAlinea();

            return JsonConvert.SerializeObject(listAlinea);
        }

        [WebMethod]
        public static string PesquisarMaterial(string codigoMaterial, string DescricaoMaterial, int? CodigoAlinea)
        {
            List<Entity.Material> listAlinea = new List<Entity.Material>();

            BLL.Material bllMaterial = new BLL.Material();

            listAlinea = bllMaterial.ObterListaMaterias(codigoMaterial, DescricaoMaterial, CodigoAlinea);

            return JsonConvert.SerializeObject(listAlinea);
        }

        #endregion
    }
}