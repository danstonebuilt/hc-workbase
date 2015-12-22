using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.TrocaMaterial
{
    public partial class RequisitarMaterial : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
    {
        int codInstituto = 0;
        private long seqRepositorio = 0;
        
        #region ViewState

        /// <summary>
        /// Carregar viewstate.
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            if (this.ViewState["codInstituto"] != null)
                this.codInstituto = Convert.ToInt32(this.ViewState["codInstituto"]);

            if (this.ViewState["seqRepositorio"] != null)
                this.seqRepositorio = Convert.ToInt32(this.ViewState["seqRepositorio"]);    
        }

        /// <summary>
        /// Salvar ViewState
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {   
            this.ViewState["codInstituto"] = this.codInstituto;
            this.ViewState["seqRepositorio"] = this.seqRepositorio;
            return base.SaveViewState();
        }

        #endregion
        
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                Int32.TryParse(query.ObterOValorDoParametro("codInstituto"), out this.codInstituto);

                Int64.TryParse(query.ObterOValorDoParametro("seqRepositorio"), out this.seqRepositorio);

               //this.CarregarComboRepositorio();

                CarregarMaterial();
            }
        }

        protected void rblTipoItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarMaterial();
        }

        protected void btnRequisitar_Click(object sender, EventArgs e)
        {
            try
            {
                string retorno = new BLL.LacreRepositorioItens().RequisitarItensParaORepositorio(seqRepositorio, rblTipoItem.SelectedIndex != 0);

                if (!retorno.Contains("ERRO"))
                {
                    lblRequisicaoGerada.Text = "Requisição gerada n° : " + retorno;
                }
                else
                {
                    lblRequisicaoGerada.Text = "Erro ao gerar requisição : " + retorno;
                }

                pnlItens.Visible = false;
                btnRequisitar.Visible = false;
                pnlRequisicaGerada.Visible = true;
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Carregar combo repositorio.
        /// </summary>
        protected void CarregarComboRepositorio()
        {
            //try
            //{
            //    this.ddlRepositorio.DataValueField = "SeqRepositorio";
            //    this.ddlRepositorio.DataTextField = "DscIdentificacao";
            //    this.ddlRepositorio.DataSource = new BLL.RepositorioListaControle().ObterRepositorioComMaterialAVencer(codInstituto, BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMaterial);
            //    this.ddlRepositorio.DataBind();
            //    this.ddlRepositorio.Items.Insert(0, new ListItem("SELECIONE", "0"));
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }

        /// <summary>
        /// Carregar material.
        /// </summary>
        protected void CarregarMaterial()
        {
            try
            {
                this.grvItem.DataSource = new BLL.LacreRepositorioItens().ObterParaRequisicaoDeMaterial(seqRepositorio, rblTipoItem.SelectedIndex != 0);
                this.grvItem.DataBind();

                if (grvItem.Rows.Count == 0)
                {
                    btnRequisitar.Visible = false;
                }
                else
                {
                    btnRequisitar.Visible = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}