using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Conferencia
{
    public partial class RegistrarOcorrencia : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
    {
        Int64 seqLacreRepositorio = 0;
        string idCampoRetornoComando = string.Empty;
        
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

            if (this.ViewState["idCampoRetornoComando"] != null)
                this.idCampoRetornoComando = this.ViewState["idCampoRetornoComando"].ToString();
        }

        /// <summary>
        /// Salvar ViewState
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {   
            this.ViewState["seqLacreRepositorio"] = this.seqLacreRepositorio;
            this.ViewState["idCampoRetornoComando"] = this.idCampoRetornoComando;
            return base.SaveViewState();
        }

        #endregion
        
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                Int64.TryParse(query.ObterOValorDoParametro("seqLacreRepositorio"), out this.seqLacreRepositorio);
                this.idCampoRetornoComando = query.ObterOValorDoParametro("idCampoRetornoComando");

                if (this.seqLacreRepositorio == 0)
                    Response.End();
            }
        }

        protected void btnAdicionaOcorrencia_Click(object sender, EventArgs e)
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

                    Page.ClientScript.RegisterStartupScript(typeof(string), "fecha", "retornoOperacao('OCORRENCIA_REGISTRADA','" + this.idCampoRetornoComando + "'); window.parent.$.fancybox.close();", true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Métodos

        #endregion
    }
}