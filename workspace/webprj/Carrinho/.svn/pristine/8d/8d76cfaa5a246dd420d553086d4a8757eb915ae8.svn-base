using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao
{
    public partial class DownLoadArquivo : System.Web.UI.Page
    {
        Int64 seqLacreRepositorio = 0;
        Int64? seqAtendimento = null;
        Int32 codInstituto = 0;
        Int64 codRepositorio = 0;
        string tipoArquivo = string.Empty;

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                Int64 auxSeqAtendimento = 0;

                Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                Int64.TryParse(query.ObterOValorDoParametro("seqLacreRep"), out this.seqLacreRepositorio);
                Int64.TryParse(query.ObterOValorDoParametro("seqAtendimento"), out auxSeqAtendimento);
                Int32.TryParse(query.ObterOValorDoParametro("codInstituto"), out codInstituto);
                Int64.TryParse(query.ObterOValorDoParametro("codRepositorio"), out codRepositorio);

                if (auxSeqAtendimento > 0)
                    this.seqAtendimento = auxSeqAtendimento;

                this.tipoArquivo = query.ObterOValorDoParametro("tipo_arquivo");

                if (!string.IsNullOrWhiteSpace(tipoArquivo))
                {
                    this.RenderizarDocumento();
                }
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Renderizar documento.
        /// </summary>
        protected void RenderizarDocumento()
        {
            try
            {
                string mensagemRetorno = string.Empty;

                byte[] arquivo = null;

                string _nomeArquivo = "";

                if (tipoArquivo == "REG_CONSUMO_SAIDA" && seqLacreRepositorio > 0)
                {
                    arquivo = this.ObterArquivoDeRegistroDeConsumoSaida();
                    _nomeArquivo = "ConsumoSaida.pdf";
                }
                else if (tipoArquivo == "TROCA_MATERIAL" && codInstituto > 0)
                {
                    arquivo = this.ObterArquivoDeTrocaDeMaterial();
                    _nomeArquivo = "TrocaMaterial.pdf";
                }

                if (arquivo != null && arquivo.Length > 0)
                {
                    // Limpa o conteúdo de saída atual do buffer
                    Page.Response.Clear();

                    // Adiciona um cabeçalho que especifica o nome default para a caixa de diálogos Salvar Como...
                    Page.Response.ContentType = "application/octet-stream";

                    Page.Response.AddHeader("Content-Disposition", "attachment; filename=" + _nomeArquivo);

                    // Adiciona ao cabeçalho o tamanho do arquivo para que o browser possa exibir o progresso do download
                    Page.Response.AddHeader("Content-Length", arquivo.Length.ToString());

                    // configurar o enconding do header
                    Page.Response.HeaderEncoding = System.Text.Encoding.ASCII;

                    Page.Response.Flush();

                    Page.Response.BinaryWrite(arquivo);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter arquivo de registro consumo saida.
        /// </summary>        
        protected byte[] ObterArquivoDeRegistroDeConsumoSaida()
        {
            byte[] arquivo = null;

            try
            {
                // Carregar as propriedades do report com os dados.
                ReportViewer reportViewer = null;
                reportViewer = new ReportViewer();

                reportViewer.LocalReport.EnableExternalImages = true;

                //reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

                // Montar datatable.
                DataTable DT_REG_CONSUMO_SAIDA = new DataTable();
                DT_REG_CONSUMO_SAIDA.Columns.Add("ALINEA", typeof(string));
                DT_REG_CONSUMO_SAIDA.Columns.Add("CODIGO", typeof(string));
                DT_REG_CONSUMO_SAIDA.Columns.Add("NOME", typeof(string));
                DT_REG_CONSUMO_SAIDA.Columns.Add("UNIDADE", typeof(string));
                DT_REG_CONSUMO_SAIDA.Columns.Add("QTD_NECESSARIA", typeof(string));
                DT_REG_CONSUMO_SAIDA.Columns.Add("QTD_DISPONIVEL", typeof(string));
                DT_REG_CONSUMO_SAIDA.Columns.Add("QTD_UTILIZADA", typeof(string));
                DT_REG_CONSUMO_SAIDA.Columns.Add("LOTE", typeof(string));
                DT_REG_CONSUMO_SAIDA.Columns.Add("VALIDADE", typeof(string));

                #region Carregar datatable

                // Carregar o datatable
                DataRow dtRow = null;
                List<Entity.LacreRepositorioItens> listLacreRepItens = new BLL.LacreRepositorioItens().ObterPorLacreRepositorio(this.seqLacreRepositorio, this.seqAtendimento);
                if (listLacreRepItens != null)
                {
                    foreach (Entity.LacreRepositorioItens item in listLacreRepItens)
                    {
                        dtRow = DT_REG_CONSUMO_SAIDA.NewRow();

                        if (item.ItensListaControle != null &&
                            item.ItensListaControle.Alinea != null &&
                            !string.IsNullOrWhiteSpace(item.ItensListaControle.Alinea.Nome))
                        {
                            dtRow["ALINEA"] = item.ItensListaControle.Alinea.Nome;
                        }

                        if (item.Material != null &&
                            !string.IsNullOrWhiteSpace(item.Material.Codigo))
                        {
                            dtRow["CODIGO"] = item.Material.Codigo;
                        }

                        if (item.Material != null &&
                            !string.IsNullOrWhiteSpace(item.Material.Nome))
                        {
                            dtRow["NOME"] = item.Material.Nome;
                        }

                        if (item.ItensListaControle != null &&
                            item.ItensListaControle.Unidade != null &&
                            !string.IsNullOrWhiteSpace(item.ItensListaControle.Unidade.Nome))
                        {
                            dtRow["UNIDADE"] = item.ItensListaControle.Unidade.Nome;
                        }

                        if (item.ItensListaControle != null)
                            dtRow["QTD_NECESSARIA"] = item.ItensListaControle.QuantidadeNecessaria;

                        dtRow["QTD_DISPONIVEL"] = item.QtdDisponivel;

                        dtRow["QTD_UTILIZADA"] = item.QtdUtilizada;

                        if (item.Lote != null &&
                            !string.IsNullOrWhiteSpace(item.Lote.NumLoteFabricante))
                        {
                            dtRow["LOTE"] = item.Lote.NumLoteFabricante;
                        }

                        if (item.Lote != null &&
                            item.Lote.DataValidadeLote.Year > 1900)
                        {
                            dtRow["VALIDADE"] = item.Lote.DataValidadeLote.ToString("dd/MM/yyyy");
                        }

                        DT_REG_CONSUMO_SAIDA.Rows.Add(dtRow);
                    }
                }

                // Dados do atendimento.
                DataTable DT_ATENDIMENTO_PACIENTE = new DataTable();
                DT_ATENDIMENTO_PACIENTE.Columns.Add("NUM_ANO_ATENDIMENTO", typeof(string));
                DT_ATENDIMENTO_PACIENTE.Columns.Add("NOME_PACIENTE", typeof(string));

                if (this.seqAtendimento != null)
                {
                    DataView dtViewAtendimento = new BLL.AtendimentoPaciente().ObterDadosDoAtendimentoPorSeqAtendimento(this.seqAtendimento.Value);
                    if (dtViewAtendimento != null && dtViewAtendimento.Count > 0)
                    {
                        dtRow = DT_ATENDIMENTO_PACIENTE.NewRow();

                        if (dtViewAtendimento[0]["NUM_ANO_ATENDIMENTO"] != DBNull.Value)
                            dtRow["NUM_ANO_ATENDIMENTO"] = dtViewAtendimento[0]["NUM_ANO_ATENDIMENTO"].ToString();

                        if (dtViewAtendimento[0]["NOME_PACIENTE"] != DBNull.Value)
                            dtRow["NOME_PACIENTE"] = dtViewAtendimento[0]["NOME_PACIENTE"].ToString();

                        DT_ATENDIMENTO_PACIENTE.Rows.Add(dtRow);
                    }
                }

                if (DT_ATENDIMENTO_PACIENTE.Rows.Count == 0)
                {
                    dtRow = DT_ATENDIMENTO_PACIENTE.NewRow();
                    dtRow["NUM_ANO_ATENDIMENTO"] = "SEM ATENDIMENTO";
                    dtRow["NOME_PACIENTE"] = "SEM PACIENTE";
                    DT_ATENDIMENTO_PACIENTE.Rows.Add(dtRow);
                }

                #endregion

                ReportDataSource rptds = null;

                rptds = new ReportDataSource("DsRegConsumoSaida", DT_REG_CONSUMO_SAIDA);
                reportViewer.LocalReport.DataSources.Add(rptds);

                rptds = new ReportDataSource("DsAtendimentoPaciente", DT_ATENDIMENTO_PACIENTE);
                reportViewer.LocalReport.DataSources.Add(rptds);

                Warning[] warn = null;
                string[] streamids = null;
                string mimeType = "application/pdf";
                string encoding = string.Empty;
                string extension = string.Empty;

                reportViewer.LocalReport.ReportPath = @"Relatorio\\rdlc\\PreviewRegistroConsumoSaida.rdlc";

                arquivo = reportViewer.LocalReport.Render("pdf", null, out mimeType, out encoding, out extension, out streamids, out warn);
            }
            catch (Exception)
            {
                throw;
            }

            return arquivo;
        }

        /// <summary>
        /// Obter arquivo de registro consumo saida.
        /// </summary>        
        protected byte[] ObterArquivoDeTrocaDeMaterial()
        {
            byte[] arquivo = null;

            try
            {
                // Carregar as propriedades do report com os dados.
                ReportViewer reportViewer = null;
                reportViewer = new ReportViewer();

                reportViewer.LocalReport.EnableExternalImages = true;

                //reportViewer.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);

                // Montar datatable.
                var DT_TROCA_MATERIAL = new DataTable();
                DT_TROCA_MATERIAL.Columns.Add("NUM_PATRIMONIO", typeof(string));
                DT_TROCA_MATERIAL.Columns.Add("IDENTIFICACAO_CARRINHO", typeof(string));
                DT_TROCA_MATERIAL.Columns.Add("COD_MATERIAL", typeof(string));
                DT_TROCA_MATERIAL.Columns.Add("DSC_MATERIAL", typeof(string));
                DT_TROCA_MATERIAL.Columns.Add("LOTE", typeof(string));
                DT_TROCA_MATERIAL.Columns.Add("QTD_NECESSARIA", typeof(string));
                DT_TROCA_MATERIAL.Columns.Add("QTD_REQ_VENCENDO", typeof(string));
                DT_TROCA_MATERIAL.Columns.Add("VALIDADE", typeof(string));

                #region Carregar datatable

                // Carregar o datatable
                DataRow dtRow = null;

                int qtdDiasVencer = BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMedicamento;

                // Verificar se há itens a serem exibidos.
                var listLacreRepItens = new Hcrp.CarroUrgenciaPsicoativo.
                    BLL.LacreRepositorioItens().ObterParaTrocaDematerial(codInstituto, qtdDiasVencer, codRepositorio);

                if (listLacreRepItens != null)
                {
                    foreach (Entity.LacreRepositorioItens item in listLacreRepItens)
                    {
                        dtRow = DT_TROCA_MATERIAL.NewRow();

                        if (item.LacreRepositorio != null && item.LacreRepositorio.RepositorioListaControle != null)
                        {
                            if (item.LacreRepositorio.RepositorioListaControle.BemPatrimonial != null &&
                                !string.IsNullOrWhiteSpace(item.LacreRepositorio.RepositorioListaControle.
                                BemPatrimonial.DscTipoPatrimonio))
                            {
                                dtRow["NUM_PATRIMONIO"] =
                                    item.LacreRepositorio.RepositorioListaControle.BemPatrimonial.DscTipoPatrimonio;
                            }

                            if (!string.IsNullOrWhiteSpace(item.LacreRepositorio.RepositorioListaControle.DscIdentificacao))
                            {
                                dtRow["IDENTIFICACAO_CARRINHO"] = item.LacreRepositorio.RepositorioListaControle.DscIdentificacao;
                            }
                        }

                        if (item.ItensListaControle != null && item.ItensListaControle.Material != null)
                        {
                            if (!string.IsNullOrWhiteSpace(item.ItensListaControle.Material.Codigo))
                            {
                                dtRow["COD_MATERIAL"] = item.ItensListaControle.Material.Codigo;
                            }

                            if (!string.IsNullOrWhiteSpace(item.ItensListaControle.Material.Nome))
                            {
                                dtRow["DSC_MATERIAL"] = item.ItensListaControle.Material.Nome;
                            }

                            dtRow["QTD_NECESSARIA"] = item.ItensListaControle.QuantidadeNecessaria;
                        }

                        dtRow["QTD_REQ_VENCENDO"] = item.QtdVencendo;


                        if (!string.IsNullOrWhiteSpace(item.NumLoteFabricante))
                            dtRow["LOTE"] = item.NumLoteFabricante;

                        if (item.DataValidadeLote.HasValue && item.DataValidadeLote > DateTime.MinValue)
                            dtRow["VALIDADE"] = item.DataValidadeLote.Value.ToString("dd/MM/yyyy");


                        DT_TROCA_MATERIAL.Rows.Add(dtRow);
                    }
                }
                #endregion

                ReportDataSource rptds = null;

                rptds = new ReportDataSource("DsTrocaDeMaterial", DT_TROCA_MATERIAL);
                reportViewer.LocalReport.DataSources.Add(rptds);

                Warning[] warn = null;
                string[] streamids = null;
                string mimeType = "application/pdf";
                string encoding = string.Empty;
                string extension = string.Empty;

                reportViewer.LocalReport.ReportPath = @"Relatorio\\rdlc\\PreviewTrocaDeMaterial.rdlc";

                reportViewer.LocalReport.SetParameters(new ReportParameter("DataReferencia", DateTime.Now.ToString("dd/MM/yyyy")));
                reportViewer.LocalReport.SetParameters(new ReportParameter("Vencimento", qtdDiasVencer + " DIA" + (qtdDiasVencer < 1 ? "" : "S")));

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