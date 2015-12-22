using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Conferencia
{
    public partial class QuebrarLacre : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
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
                this.CarregarTipoOcorrencia();

                Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                Int64.TryParse(query.ObterOValorDoParametro("seqLacreRepositorio"), out this.seqLacreRepositorio);
                this.idCampoRetornoComando = query.ObterOValorDoParametro("idCampoRetornoComando");

                if (this.seqLacreRepositorio == 0)
                    Response.End();
            }
        }

        protected void chkgerarLacreProv_CheckedChanged(object sender, EventArgs e)
        {
            this.tbOcorrencia.Visible = false;

            if (this.chkgerarLacreProv.Checked == false)
            {
                this.tbOcorrencia.Visible = true;
            }
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    // Verificar se carrinho ainda continua lacrado.
                    Entity.LacreRepositorio lacreRep = new BLL.LacreRepositorio().ObterPorId(this.seqLacreRepositorio);

                    if (lacreRep != null &&
                        lacreRep.TipoSituacaoHc != null &&
                        lacreRep.TipoSituacaoHc.CodSituacao == BLL.Parametrizacao.Instancia().CodigoDaSituacaoLacrado)
                    {

                        // Sinalizar a quebra de lacre.
                        new BLL.LacreRepositorio().QuebrarLacreCarrinho(this.seqLacreRepositorio, Convert.ToInt64(this.ddlTipoOcorrencia.SelectedValue));

                        // Se foi informando ocorrencia, então gravar.
                        if (this.tbOcorrencia.Visible == true && !string.IsNullOrWhiteSpace(this.txtDscOcorrencia.Text))
                        {
                            Entity.LacreOcorrencia lacreOcorrencia = new Entity.LacreOcorrencia();

                            lacreOcorrencia.DataCadastro = DateTime.Now;
                            lacreOcorrencia.DscOcorrencia = this.txtDscOcorrencia.Text.ToUpper().Trim();

                            lacreOcorrencia.LacreRepositorio = new Entity.LacreRepositorio();
                            lacreOcorrencia.LacreRepositorio.SeqLacreRepositorio = this.seqLacreRepositorio;

                            lacreOcorrencia.UsuarioCadastro = new Entity.Usuario();
                            lacreOcorrencia.UsuarioCadastro.NumUserBanco = Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;

                            new BLL.LacreOcorrencia().Adicionar(lacreOcorrencia);

                            // Enviar email para os gestores com a descrição feita pelo usuário.
                            List<Entity.Usuario> listGestores = new BLL.Usuario().ObterOsUsuarioGestoresDoRepositorio(BLL.Parametrizacao.Instancia().CodigoDaRoleGestor,
                                lacreRep.RepositorioListaControle.SeqRepositorio);

                            if (listGestores != null)
                            {
                                this.EnviarEmailParaOsGestores(listGestores);
                            }
                        }

                        // Se foi checado para gerar lacre provisório, então gerar.
                        // Transação iniciar
                        if (this.chkgerarLacreProv.Checked == true)
                        {
                            Hcrp.Infra.AcessoDado.TransacaoDinamica transacao = new Hcrp.Infra.AcessoDado.TransacaoDinamica();

                            new BLL.LacreRepositorio().GerarLacreProvisorio(transacao, this.seqLacreRepositorio);

                            transacao.ComitarTransacao();
                        }
                    }

                    Page.ClientScript.RegisterStartupScript(typeof(string), "fecha", "retornoOperacao('LACRE_ROMPIDO','" + this.idCampoRetornoComando + "'); window.parent.$.fancybox.close();", true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Enviar email para o usuário
        /// </summary>
        protected void EnviarEmailParaOsGestores(List<Entity.Usuario> listGestores)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                string nomeUsuarioLogado = string.Empty;
                Framework.Classes.Usuario usuarioLogado = new Framework.Classes.Usuario().BuscarUsuarioCodigo(Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado);
                if (usuarioLogado != null && !string.IsNullOrWhiteSpace(usuarioLogado.NomeCompleto))
                    nomeUsuarioLogado = usuarioLogado.NomeCompleto;

                sb.AppendLine("<br /> ");
                sb.AppendLine("<br /> ");
                sb.AppendLine("Foi rompido um lacre de repositorio sem a geração de um lacre provisório.");
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
                sb.AppendLine(this.txtDscOcorrencia.Text.ToUpper().Trim());

                new BLL.Email().Enviar(sb.ToString(), listGestores, "Repositório de Medicamento - Lacre rompido");

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar tipo de ocorrência.
        /// </summary>
        protected void CarregarTipoOcorrencia()
        {
            try
            {
                this.ddlTipoOcorrencia.DataValueField = "SeqLacreTipoOCorrencia";
                this.ddlTipoOcorrencia.DataTextField = "DscTipoOcorrencia";
                this.ddlTipoOcorrencia.DataSource = new BLL.LacreTipoOcorrencia().ObterTodosAtivos();
                this.ddlTipoOcorrencia.DataBind();
                this.ddlTipoOcorrencia.Items.Insert(0, new ListItem("SELECIONE", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}