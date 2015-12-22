using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Conferencia
{
    public partial class AcaoListaConferenciaEquipamento : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
    {
        string idCampoRetornoComando = string.Empty;
        Int64 seqLacreRepEquipamento = 0;

        #region ViewState

        /// <summary>
        /// Carregar viewstate.
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            this.idCampoRetornoComando = this.ViewState["idCampoRetornoComando"].ToString();
            this.seqLacreRepEquipamento = Convert.ToInt64(this.ViewState["seqLacreRepEquipamento"]);
        }

        /// <summary>
        /// Salvar ViewState
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            this.ViewState["idCampoRetornoComando"] = this.idCampoRetornoComando;
            this.ViewState["seqLacreRepEquipamento"] = this.seqLacreRepEquipamento;
            return base.SaveViewState();
        }

        #endregion

        #region eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                this.idCampoRetornoComando = query.ObterOValorDoParametro("idCampoRetornoComando");
                Int64.TryParse(query.ObterOValorDoParametro("seqLacreRepositorioEquipamento"), out this.seqLacreRepEquipamento);

                if (string.IsNullOrWhiteSpace(idCampoRetornoComando) || this.seqLacreRepEquipamento == 0)
                    Response.End();

                if (this.seqLacreRepEquipamento > 0)
                    this.CarregarOpcao();
            }
        }

        protected void lnkBtnAcao_Click(object sender, EventArgs e)
        {
            Button lnkBtnAcao = (Button)sender;

            if (lnkBtnAcao.Attributes["comando"] == "INFORMAR_TESTE_EQUIPAMENTO")
            {
                // validar a data.
                //DateTime dataAux;

                //if (DateTime.TryParse(this.txtDataDoTeste.Text, out dataAux) == false)
                //{
                //    this.ExibirMensagem(TipoMensagem.Alerta, "Data inválida.");
                //    return;
                //}
                //else
                //{
                //    if (dataAux.Year < 1900)
                //    {
                //        this.ExibirMensagem(TipoMensagem.Alerta, "Data inválida.");
                //        return;
                //    }
                //}

                Page.ClientScript.RegisterStartupScript(typeof(string), "fecha", "retornoOperacao('" + lnkBtnAcao.Attributes["comando"] + "|" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "','" + this.idCampoRetornoComando + "'); window.parent.$.fancybox.close();", true);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "fecha", "retornoOperacao('" + lnkBtnAcao.Attributes["comando"] + "','" + this.idCampoRetornoComando + "'); window.parent.$.fancybox.close();", true);
            }
            
        }

        #endregion

        #region métodos

        /// <summary>
        /// Carregar opções.
        /// </summary>
        protected void CarregarOpcao()
        {
            try
            {
                if (this.seqLacreRepEquipamento > 0)
                {
                    Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioEquipamento lacreRepEquipamento = null;
                    lacreRepEquipamento = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioEquipamento().ObterPorId(this.seqLacreRepEquipamento);

                    if (lacreRepEquipamento != null && lacreRepEquipamento.SeqLacreRepositorioEquipamento > 0)
                    {
                        // Se o teste já foi realizado, não pode realizar novamente.
                        if (lacreRepEquipamento.DataTeste == null)
                            this.trTesteEquipamento.Visible = true;
                    }
                }
            }
            catch (Exception err)
            {
                ErrorSignal.FromCurrentContext().Raise(err);
            }
        }


        #endregion
    }
}