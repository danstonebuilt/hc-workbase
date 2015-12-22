using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Hcrp.Framework.Infra.Util;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.IO;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Master
{
    public partial class RelConsumoCarrinho : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.ConfigurarPeriodos();
                this.CarregarComboInstituto();
                this.CarregarComboPatrimonio();
            }
        }

        protected void ddlInstituto_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.CarregarComboPatrimonio();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (ddlInstituto.SelectedIndex == 0)
                ExibirMensagem(TipoMensagem.Alerta, "Favor informar o Instituto!", 0, Toast.TopFullWidth);
            else if (ddlPatrimonio.SelectedIndex == 0)
                ExibirMensagem(TipoMensagem.Alerta, "Favor informar o Repositório!", 0, Toast.TopFullWidth);
            else if (String.IsNullOrWhiteSpace(txtPeriodoDe.Text))
                ExibirMensagem(TipoMensagem.Alerta, "Favor informar o Período Inicial!", 0, Toast.TopFullWidth);
            else if (String.IsNullOrWhiteSpace(txtPeriodoAte.Text))
                ExibirMensagem(TipoMensagem.Alerta, "Favor informar o Período Final!", 0, Toast.TopFullWidth);
            else
            {
                Session["ResponseRelatorio"] = ObterConsumoCarrinho();
                rtpViewer.Attributes.Add("src", ResolveClientUrl("ViewPDF.aspx"));
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            byte[] exp = null;

            if (Session["ResponseRelatorio"] != null)
                exp = (byte[])Session["ResponseRelatorio"];
            else
                exp = ObterConsumoCarrinho();

            Response.Clear();
            MemoryStream ms = new MemoryStream(exp);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + string.Format("{0}.{1}", ddlPatrimonio.SelectedItem.ToString().Replace(" ", "") + "_" + txtPeriodoDe.Text + "_" + txtPeriodoAte.Text, "pdf") + ";");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Configurar períodos.
        /// </summary>
        protected void ConfigurarPeriodos()
        {
            try
            {
                this.txtPeriodoDe.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
                this.txtPeriodoAte.Text = DateTime.Now.ToString("dd/MM/yyyy");
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

                ddlInstituto_SelectedIndexChanged(null, null);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar combo patrimonio.
        /// </summary>
        protected void CarregarComboPatrimonio()
        {
            try
            {
                this.ddlPatrimonio.Items.Clear();

                if (this.ddlInstituto.SelectedValue != "" &&
                    this.ddlInstituto.SelectedValue != "0")
                {
                    this.ddlPatrimonio.DataValueField = "SeqRepositorio";
                    this.ddlPatrimonio.DataTextField = "DscIdentificacao";

                    string roles = BLL.Parametrizacao.Instancia().CodigoDaRoleUsuario.ToString() + "," +
                                   BLL.Parametrizacao.Instancia().CodigoDaRoleEnfermeiro.ToString();

                    if (TipoUsuarioLogado.Any(x => x.Nome == "RL_CAR_EMERGENCIA_ADM"))
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

        /// <summary>
        /// Obter consumo carrinho.
        /// </summary>        
        protected byte[] ObterConsumoCarrinho()
        {
            byte[] arquivo = null;

            try
            {
                // Carregar as propriedades do report com os dados.
                ReportViewer reportViewer = null;
                reportViewer = new ReportViewer();

                reportViewer.LocalReport.EnableExternalImages = true;

                // Montar datatable.
                var dtConsumoMaterial = new DataTable();

                dtConsumoMaterial.Columns.Add("SEQ_LACRE_REPOSITORIO", typeof(string));


                dtConsumoMaterial.Columns.Add("NUM_LACRE", typeof(string));
                dtConsumoMaterial.Columns.Add("NOM_SITUACAO", typeof(string));
                dtConsumoMaterial.Columns.Add("NOM_MATERIAL", typeof(string));
                dtConsumoMaterial.Columns.Add("DTA_VALIDADE_LOTE", typeof(string));
                dtConsumoMaterial.Columns.Add("NUM_LOTE_FABRICANTE", typeof(string));
                dtConsumoMaterial.Columns.Add("QTD_UTILIZADA", typeof(string));

                //dtConsumoMaterial.Columns.Add("SEQ_ATENDIMENTO", typeof(string));
                dtConsumoMaterial.Columns.Add("NOM_PACIENTE", typeof(string));

                dtConsumoMaterial.Columns.Add("DSC_JUSTIFICATIVA", typeof(string));
                dtConsumoMaterial.Columns.Add("NOM_USUARIO_BANCO", typeof(string));

                #region Carregar datatable

                // Carregar o datatable
                DataRow dtRow = null;

                long seqRepositorio = !string.IsNullOrWhiteSpace(ddlPatrimonio.SelectedValue) ? Convert.ToInt32(ddlPatrimonio.SelectedValue) : 0;
                DateTime periodoInicial = Convert.ToDateTime(txtPeriodoDe.Text);
                DateTime periodoFinal = Convert.ToDateTime(txtPeriodoAte.Text);

                // Verificar se há itens a serem exibidos.
                var listLacreRepUtilizacao = new Hcrp.CarroUrgenciaPsicoativo.
                    BLL.LacreRepositorioUtilizacao().ObterConsumoPorLacre(Parametrizacao.Instancia().CodInstituto, seqRepositorio, periodoInicial, periodoFinal);

                if (listLacreRepUtilizacao != null)
                {
                    foreach (Entity.LacreRepositorioUtilizacao item in listLacreRepUtilizacao)
                    {
                        dtRow = dtConsumoMaterial.NewRow();

                        if (item.LacreRepositorioItens != null)
                        {
                            if (item.LacreRepositorioItens.LacreRepositorio != null)
                            {
                                dtRow["SEQ_LACRE_REPOSITORIO"] =
                                    item.LacreRepositorioItens.LacreRepositorio.SeqLacreRepositorio;

                                dtRow["NUM_LACRE"] =
                                    item.LacreRepositorioItens.LacreRepositorio.NumLacre;

                                if (item.LacreRepositorioItens.LacreRepositorio.TipoSituacaoHc != null)
                                    dtRow["NOM_SITUACAO"] =
                                        item.LacreRepositorioItens.LacreRepositorio.TipoSituacaoHc.NomSituacao;
                            }

                            if (item.LacreRepositorioItens.Material != null)
                                dtRow["NOM_MATERIAL"] =
                                    item.LacreRepositorioItens.Material.Nome;

                            if (item.LacreRepositorioItens.DataValidadeLote.HasValue &&
                                item.LacreRepositorioItens.DataValidadeLote > DateTime.MinValue)
                                dtRow["DTA_VALIDADE_LOTE"] =
                                    item.LacreRepositorioItens.DataValidadeLote.Value.ToString("dd/MM/yyyy");

                            dtRow["NUM_LOTE_FABRICANTE"] =
                                item.LacreRepositorioItens.NumLoteFabricante;

                            if (item.AtendimentoPaciente != null && !string.IsNullOrWhiteSpace(item.AtendimentoPaciente.NomePaciente))
                                dtRow["NOM_PACIENTE"] =
                                    item.AtendimentoPaciente.NomePaciente;
                        }

                        dtRow["QTD_UTILIZADA"] =
                                item.QtdUtilizada;

                        dtRow["DSC_JUSTIFICATIVA"] =
                                item.DscJustificativa;

                        if (item.UsuarioCadastro != null)
                            dtRow["NOM_USUARIO_BANCO"] =
                                item.UsuarioCadastro.NomeAcesso;

                        dtConsumoMaterial.Rows.Add(dtRow);
                    }
                }
                #endregion

                ReportDataSource rptds = null;

                rptds = new ReportDataSource("DsConsumoMaterial", dtConsumoMaterial);
                reportViewer.LocalReport.DataSources.Add(rptds);

                Warning[] warn = null;
                string[] streamids = null;
                string mimeType = "application/pdf";
                string encoding = string.Empty;
                string extension = string.Empty;

                reportViewer.LocalReport.ReportPath = @"Relatorio\\rdlc\\PreviewConsumoCarrinho.rdlc";

                reportViewer.LocalReport.SetParameters(new ReportParameter("DataInicio", periodoInicial.ToString("dd/MM/yyyy")));
                reportViewer.LocalReport.SetParameters(new ReportParameter("DataFim", periodoFinal.ToString("dd/MM/yyyy")));
                reportViewer.LocalReport.SetParameters(new ReportParameter("Carrinho", ddlPatrimonio.SelectedItem.Text));

                arquivo = reportViewer.LocalReport.Render("pdf", null, out mimeType, out encoding, out extension, out streamids, out warn);
            }
            catch (Exception)
            {
                throw;
            }

            return arquivo;
        }

        #endregion

    }
}