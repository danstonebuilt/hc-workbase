using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Conferencia
{
    public partial class AcaoRegistrarConferenciaDeLacre : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
    {
        string idCampoRetornoComando = string.Empty;
        Int64 seqLacreRepositorio = 0;

        #region ViewState

        /// <summary>
        /// Carregar viewstate.
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            this.idCampoRetornoComando = this.ViewState["idCampoRetornoComando"].ToString();
            this.seqLacreRepositorio = Convert.ToInt64(this.ViewState["seqLacreRepositorio"]);
        }

        /// <summary>
        /// Salvar ViewState
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            this.ViewState["idCampoRetornoComando"] = this.idCampoRetornoComando;
            this.ViewState["seqLacreRepositorio"] = this.seqLacreRepositorio;
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
                Int64.TryParse(query.ObterOValorDoParametro("seqLacreRepositorio"), out this.seqLacreRepositorio);

                if (string.IsNullOrWhiteSpace(idCampoRetornoComando) || this.seqLacreRepositorio == 0)
                    Response.End();

                if (this.seqLacreRepositorio > 0 || this.seqLacreRepositorio == -1)
                    this.ConfiguraAcoesParaOCarrinho();
            }
        }

        protected void lnkBtnAcao_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtnAcao = (LinkButton)sender;
            
            //Page.ClientScript.RegisterStartupScript(typeof(string), "fecha", "retornoOperacao('" + lnkBtnAcao.Attributes["comando"] + "','" + this.idCampoRetornoComando + "'); window.parent.$.fancybox.close();", true);

            //ScriptManager.RegisterStartupScript(Page, typeof(string), "fecha",
            //                                        "retornoOperacao('" + lnkBtnAcao.Attributes["comando"] + "','" +
            //                                        this.idCampoRetornoComando + "'); window.parent.$.fancybox.close();",
            //                                        true);

            //ScriptManager.RegisterStartupScript(Page, typeof(string), "fecha",
            //                                        "window.parent.$.fancybox.close();",
            //                                        true);

            string func = "retornoOperacao('" + lnkBtnAcao.Attributes["comando"] + "','" + this.idCampoRetornoComando +
                          "');";

            //ScriptManager.RegisterStartupScript(this, typeof(string), "fecha",
            //                                        " setTimeout(function(){" + func + " window.parent.$.fancybox.close();}, 100);",
            //                                        true);

            //ScriptManager.RegisterClientScriptBlock(this, typeof(string), "fecha",
            //                                        func + " window.parent.$.fancybox.close();",
            //                                        true);

            ScriptManager.RegisterStartupScript(this, typeof(string), "fecha",
                                                     func + " setTimeout(function(){ window.parent.$.fancybox.close();}, 500);",
                                                    true);
        }

        #endregion

        #region métodos

        /// <summary>
        /// Configurar ações para carrinho.
        /// </summary>
        protected void ConfiguraAcoesParaOCarrinho()
        {
            try
            {
                if (this.seqLacreRepositorio > 0)
                {

                    Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio lacreRep = null;
                    List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio> listLacreRep = 
                        new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorio().ObterPorFiltro(this.seqLacreRepositorio, null, null, null);

                    if (listLacreRep != null && listLacreRep.Count > 0)
                        lacreRep = listLacreRep[0];

                    if (lacreRep != null)
                    {
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
                        }
                    }
                }
                else if (this.seqLacreRepositorio == -1)
                {
                    this.trNovaLacracao.Visible = true;
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