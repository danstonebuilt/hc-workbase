using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Conferencia
{
    public partial class GerenciarConferenciaDeLacre : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
    {
        Int64 seqLacreRepositorio = 0;
        Int64 seqAtendimento = 0;
        Int64 seqLacreRepositorioEquipamento = 0;

        #region ViewState

        /// <summary>
        /// Carregar viewstate.
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            if (this.ViewState["seqAtendimento"] != null)
                this.seqAtendimento = Convert.ToInt64(this.ViewState["seqAtendimento"]);

            if (this.ViewState["seqLacreRepositorio"] != null)
                this.seqLacreRepositorio = Convert.ToInt64(this.ViewState["seqLacreRepositorio"]);

            if (this.ViewState["seqLacreRepositorioEquipamento"] != null)
                this.seqLacreRepositorioEquipamento = Convert.ToInt64(this.ViewState["seqLacreRepositorioEquipamento"]);
        }

        /// <summary>
        /// Salvar ViewState
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            this.ViewState["seqAtendimento"] = this.seqAtendimento;
            this.ViewState["seqLacreRepositorio"] = this.seqLacreRepositorio;
            this.ViewState["seqLacreRepositorioEquipamento"] = this.seqLacreRepositorioEquipamento;
            return base.SaveViewState();
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                if (Int64.TryParse(query.ObterOValorDoParametro("seqLacreRepositorio"), out this.seqLacreRepositorio) == true)
                {
                    
                    //this.seqLacreRepositorio = 8;

                    if (this.seqLacreRepositorio > 0)
                    {
                        this.CarregarInterface();
                    }
                    else
                    {
                        Response.End();
                    }
                }
            }
        }

        protected void txtCodMaterial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.CarregarMaterialComCodigoPorCodigoMaterial();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnLacrar_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                Description
                Validar se a lista de itens do carrinho esta completa(QTD necessária = QTD. Disponivel), caso contrario mostrar mensagem na tela.
                O usuário pode lacrar, porem ele registra a ocorrencia(campo texto aberto)(O sistema envia emails para os gestores com a descrição da ocorrencia.)
                --------------------------------------------------------------------------------------------------------------------------------------------------------
                Validar se na lista de itens do carrinho possui itens dentro do prazo de validade (ITENS com a data de validade dentro de 4 meses)  

                O usuário pode lacrar, porem ele registra a ocorrencia(campo texto aberto)(O sistema envia emails para os gestores com a descrição da ocorrencia.)
                -------------------------------------------------------------------------------------------------------------------------------------------------------
                Somente usuários ENFERMEIROS podem lacrar.
                */
                
                Int64 numLacre = 0;
                Int64? numCaixaIntubacao = null;
                Int64 numCaixaIntubacaoAux = 0;

                this.tbDscOcorrenciaLacrarComQtdDiferente.Visible = false;

                #region validar número de lacre e número de caixa de intubação

                // Validar número de lacre.
                if (Int64.TryParse(this.txtNumLacre.Text, out numLacre) == false)
                {
                    this.ExibirMensagem(TipoMensagem.Alerta, "Número do lacre inválido.");
                    return;
                }

                // Validar número de caixa de intubação.
                if (!string.IsNullOrWhiteSpace(this.txtNumCaixaIntubacao.Text))
                {
                    if (Int64.TryParse(this.txtNumCaixaIntubacao.Text, out numCaixaIntubacaoAux) == true)
                    {
                        numCaixaIntubacao = numCaixaIntubacaoAux;
                    }
                    else
                    {
                        this.ExibirMensagem(TipoMensagem.Alerta, "Número da caixa de intubação inválido.");
                        return;
                    }
                }

                #endregion

                // Obter os itens do repostório.
                List<Entity.LacreRepositorioItens> listRepItens = new BLL.LacreRepositorioItens().ObterPorLacreRepositorio(this.seqLacreRepositorio, null);

                if (listRepItens == null || listRepItens.Count == 0)
                {
                    this.ExibirMensagem(TipoMensagem.Alerta, "Não foram encontrados os itens do repositório para realizar a lacração.");                    
                    return;
                }

                #region validar qtd necessário com qtd disponível

                // Validar se qtd necessário é igual a qtd disponível.  
                bool temItensComQtdDiferentes = false;                
                var contaItensComQtdDifrente = (from contaItensComQtdDifrenteL in listRepItens
                                                where contaItensComQtdDifrenteL.QtdDisponivelInserida != contaItensComQtdDifrenteL.ItensListaControle.QuantidadeNecessaria
                                                select contaItensComQtdDifrenteL).Count();

                // Precisa informar justificativa.
                if (contaItensComQtdDifrente > 0)
                {
                    //if (this.tbDscOcorrenciaLacrarComQtdDiferente.Visible == false ||
                //    string.IsNullOrWhiteSpace(this.txtDscOcorrenciaLancarComQtdDiferente.Text))
                    if (
                        string.IsNullOrWhiteSpace(this.txtDscOcorrenciaLancarComQtdDiferente.Text))
                    {
                        this.ExibirMensagem(TipoMensagem.Alerta, "Uma justificativa deve ser informada para o lacração com itens que estão com quantidades necessários diferentes das quantidades disponíveis.");
                        this.tbDscOcorrenciaLacrarComQtdDiferente.Visible = true;
                        return;
                    }    
                
                    temItensComQtdDiferentes = true;
                }

                #endregion


                #region validar data de validade do item

                // Verificar se o prazo de validade dos itens se encontra dentro do período.
                Int32 numDiasPeriodo = BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMaterial;
                bool temItensComValidadeForaDoPeriodo = false;
                TimeSpan timeSpan;

                foreach (Entity.LacreRepositorioItens item in listRepItens)
                {
                    //if (item.DataValidadeLote != null &&
                    //    item.DataValidadeLote.Value > DateTime.Now)
                    if (item.Lote.DataValidadeLote != null)
                    {
                        timeSpan = item.Lote.DataValidadeLote - DateTime.Now.Date;

                        if (timeSpan.Days <= numDiasPeriodo)
                        {
                            temItensComValidadeForaDoPeriodo = true;
                        }
                    }                    
                }

                // Precisa informar justificativa.
                if (temItensComValidadeForaDoPeriodo == true)
                {
                    //if (this.tbDscOcorrenciaLacrarComValidadeForaPeriodo.Visible &&
                    //    string.IsNullOrWhiteSpace(this.txtDscOcorrenciaLancarComItensAVencer.Text))
                    if (
                        string.IsNullOrWhiteSpace(this.txtDscOcorrenciaLancarComItensAVencer.Text))
                    {
                        this.ExibirMensagem(TipoMensagem.Alerta, "Uma justificativa deve ser informada para o lacração com itens que estão com a data de validade a vencer.");
                        this.tbDscOcorrenciaLacrarComValidadeForaPeriodo.Visible = true;
                        return;
                    }
                }

                #endregion

                
                // Lacrar carrinho.
                new BLL.LacreRepositorio().LacrarCarrinho(this.seqLacreRepositorio, numLacre, numCaixaIntubacao, DateTime.Now);


                #region enviar email com as ocorrências

                // Se exitem itens com qtd diferentes ou data de validade fora do período, 
                // enviar emails para os gestores.
                List<Entity.Usuario> listGestores = null;
                if (temItensComQtdDiferentes == true || temItensComValidadeForaDoPeriodo == true)
                {
                    listGestores = new BLL.Usuario().ObterOsUsuarioDaRole(BLL.Parametrizacao.Instancia().CodigoDaRoleGestor);

                    // Se os gestores foram obtidos enviar os emails.
                    if (listGestores != null)
                    {
                        Entity.LacreRepositorio lacreRep = new BLL.LacreRepositorio().ObterPorId(this.seqLacreRepositorio);
                        Framework.Classes.Usuario usuarioLogado = new Framework.Classes.Usuario().BuscarUsuarioCodigo(Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado);

                        if (lacreRep != null && 
                            lacreRep.SeqLacreRepositorio > 0 &&
                            lacreRep.NumLacre != null &&
                            lacreRep.RepositorioListaControle != null &&
                            lacreRep.RepositorioListaControle.TipoRepositorioListaControle != null &&
                            !string.IsNullOrWhiteSpace(lacreRep.RepositorioListaControle.TipoRepositorioListaControle.DscTipoRepositorio) &&
                            !string.IsNullOrWhiteSpace(lacreRep.RepositorioListaControle.DscIdentificacao) &&
                            lacreRep.DataCadastro != null &&
                            usuarioLogado != null &&
                            !string.IsNullOrWhiteSpace(usuarioLogado.NomeCompleto))
                        {
                            foreach (Entity.Usuario usuario in listGestores)
                            {
                                if (!string.IsNullOrWhiteSpace(usuario.Email))
                                {
                                    this.EnviarEmailParaGestorNotificandoSobreLacracaoComOcorrenciaComQtdDiferente(usuario.Email,
                                                                                                                    lacreRep.NumLacre.Value,
                                                                                                                    lacreRep.NumCaixaIntubacao,
                                                                                                                    lacreRep.SeqLacreRepositorio,
                                                                                                                    lacreRep.RepositorioListaControle.TipoRepositorioListaControle.DscTipoRepositorio,
                                                                                                                    lacreRep.RepositorioListaControle.DscIdentificacao,
                                                                                                                    lacreRep.DataCadastro.Value,
                                                                                                                    usuarioLogado.NomeCompleto,
                                                                                                                    temItensComQtdDiferentes,
                                                                                                                    temItensComValidadeForaDoPeriodo);
                                }
                            }
                        }
                    }
                }

                #endregion

            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void rptLacreOcorrenciaRegistradas_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    Entity.LacreOcorrencia lacreOcorrencia = (Entity.LacreOcorrencia)e.Item.DataItem;

                    if (lacreOcorrencia != null)
                    {
                        Label lblDataOcorrenciaRegistrada = (Label)e.Item.FindControl("lblDataOcorrenciaRegistrada");

                        if (lacreOcorrencia.UsuarioCadastro != null &&
                            !string.IsNullOrWhiteSpace(lacreOcorrencia.UsuarioCadastro.Nome))
                        {
                            lblDataOcorrenciaRegistrada.Text = string.Format("{0} - {1}", lacreOcorrencia.UsuarioCadastro.Nome, lacreOcorrencia.DataCadastro.ToString());
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
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

                    this.CarregarLacreOcorrencia();

                    this.ExibirMensagem(TipoMensagem.Sucesso, "Ocorrência registrada com sucesso.");

                    this.txtDscOcorrencia.Text = string.Empty;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void ddlTipoPatrimonio_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.CarregarPatrimonioParaEquipamentosPorNumeroPatrimonio();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void grvItemRepLacreEquipamento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "SEL")
                {
                    this.hdfComando.Value = string.Empty;
                    this.seqLacreRepositorioEquipamento = Convert.ToInt64(e.CommandArgument);

                    Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                    query.Parametros.Add("idCampoRetornoComando", this.hdfComando.ClientID);
                    query.Parametros.Add("seqLacreRepositorioEquipamento", this.seqLacreRepositorioEquipamento.ToString());
                    query.Url = Page.ResolveUrl("~/Conferencia/AcaoListaConferenciaEquipamento.aspx");
                    this.AbrirModal(query.ObterUrlCriptografada(), 300, 200);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnAdicionaPatrimonio_Click(object sender, EventArgs e)
        {
            try
            {
                Int64 numBem = 0;

                if (Int64.TryParse(this.hdfNumBemEquipamento.Value, out numBem) == true)
                {
                    Entity.BemPatrimonial bemPatrimonio = null;

                    // verificar se o bem é válido.
                    bemPatrimonio = new BLL.BemPatrimonial().ObterPatrimonioPorNumeroETipoVinculadoItemListaControle(numBem, null, null);

                    if (bemPatrimonio != null && bemPatrimonio.NumBem > 0)
                    {
                        // Verificar se já não foi adicionado.
                        List<Entity.LacreRepositorioEquipamento> listLacreEquip = new BLL.LacreRepositorioEquipamento().ObterPorLacreRepositorio(this.seqLacreRepositorio);

                        if (listLacreEquip != null && listLacreEquip.Count > 0)
                        {
                            var conta = (from contaL in listLacreEquip
                                         where contaL.BemPatrimonial.NumBem == bemPatrimonio.NumBem
                                         select contaL).Count();

                            if (conta > 0)
                            {
                                this.ExibirMensagem(TipoMensagem.Alerta, "Equipamento já adicionado.");
                                return;
                            }
                        }

                        Entity.LacreRepositorioEquipamento lacreRepEquipamento = new Entity.LacreRepositorioEquipamento();

                        lacreRepEquipamento.LacreRepositorio = new Entity.LacreRepositorio();
                        lacreRepEquipamento.LacreRepositorio.SeqLacreRepositorio = this.seqLacreRepositorio;

                        lacreRepEquipamento.IdfAtivo = "S";
                        lacreRepEquipamento.DataCadastro = DateTime.Now;

                        lacreRepEquipamento.BemPatrimonial = bemPatrimonio;

                        new BLL.LacreRepositorioEquipamento().Adicionar(lacreRepEquipamento);

                        this.CarregarLacreRepositorioEquipamento();

                        this.ExibirMensagem(TipoMensagem.Sucesso, "Equipamento adicionado com sucesso.");

                        // Limpar campos de patrimonio.
                        this.txtNumPatrimonio.Text = string.Empty;
                        this.txtDscPatrimonio.Text = string.Empty;
                        this.hdfNumBemEquipamento.Value = string.Empty;

                    }
                    else
                    {
                        this.ExibirMensagem(TipoMensagem.Alerta, "Patrimônio inválido.");
                    }
                }
                else
                {
                    this.ExibirMensagem(TipoMensagem.Alerta, "Patrimônio inválido.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //protected void imgBtnBuscapatrimonio_Click(object sender, ImageClickEventArgs e)
        //{
            //try
            //{
            //    if (this.ddlTipoPatrimonio.SelectedValue == "0" ||
            //        this.ddlTipoPatrimonio.SelectedValue == "")
            //    {
            //        this.ExibirMensagem(TipoMensagem.Alerta, "Informe o tipo de patrimônio.");
            //        return;
            //    }

            //    this.hdfComando.Value = "BUSCA_PATRIMONIO_EQUIPAMENTO";

            //    Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
            //    query.Url = Page.ResolveUrl("~/Comum/BuscaPatrimonio.aspx");
            //    query.Parametros.Add("id", this.hdfNumBemEquipamento.ClientID);
            //    query.Parametros.Add("valor", this.txtDscPatrimonio.ClientID);
            //    query.Parametros.Add("codTipoPatrimonio", this.ddlTipoPatrimonio.SelectedValue);
            //    base.AbrirModal(query.ObterUrlCriptografada(), 740, 450);
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        //}

        protected void txtNumPatrimonio_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.CarregarPatrimonioParaEquipamentosPorNumeroPatrimonio();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void grvItemConsumoSaida_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    System.Web.UI.HtmlControls.HtmlTableRow trJustificativaSemAtendimento = e.Row.FindControl("trJustificativaSemAtendimento") as System.Web.UI.HtmlControls.HtmlTableRow;

                    if (trJustificativaSemAtendimento != null)
                    {
                        // Quanto não há atendimento, uma justificatica deverá ser informada para o lançamento de consumo.
                        if (this.seqAtendimento == 0)
                        {
                            trJustificativaSemAtendimento.Visible = true;
                        }
                    }

                    Entity.LacreRepositorioItens lacreRepItens = (Entity.LacreRepositorioItens)e.Row.DataItem;
                    Label lblValidade = (Label)e.Row.FindControl("lblValidade");

                    if (lacreRepItens.Lote.DataValidadeLote.Year > 1900)
                        lblValidade.Text = lacreRepItens.Lote.DataValidadeLote.ToString("dd/MM/yyyy");                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void grvItemInformarReposicao_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Entity.LacreRepositorioItens lacreRepItens = (Entity.LacreRepositorioItens)e.Row.DataItem;

                    if (lacreRepItens.QtdDisponivel == 0)
                    {
                        // Nota ao desenvolvedor : Quando um item estiver com a coluna "Qtd. Disponivel = 0 " pintar a linha do grid de vermelho (claro).
                        //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FC6D71"); // #339966  
                        e.Row.CssClass = "danger";
                    }

                    Label lblValidade = (Label)e.Row.FindControl("lblValidade");
                    if (lacreRepItens.Lote != null && lacreRepItens.Lote.DataValidadeLote.Year > 1900)
                    {
                        lblValidade.Text = lacreRepItens.Lote.DataValidadeLote.ToString("dd/MM/yyyy");
                        lblValidade.Visible = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void grvItemInformarReposicao_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Int64 seqLacreRepItem = Convert.ToInt64(e.CommandArgument);

                if (e.CommandName == "EXCLUIR")
                {
                    this.ExcluirMaterialDeLacreRepositorioItem(seqLacreRepItem);

                    this.CarregarGridInformarReposicao();

                    this.ExibirMensagem(TipoMensagem.Sucesso, "Item excluído com sucesso.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void imgBtnBuscaMaterial_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.hdfComando.Value = "BUSCA_MATERIAL_LISTA_CONTROLE_ITEM";
                this.hdfSeqItemListaControle.Value = string.Empty;

                Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                query.Url = Page.ResolveUrl("~/Comum/BuscaMaterialListaControleItem.aspx");
                query.Parametros.Add("id", this.hdfSeqItemListaControle.ClientID);
                query.Parametros.Add("valor", this.txtNomeMaterialComCodigo.ClientID);
                query.Parametros.Add("seqLacreRepositorio", this.seqLacreRepositorio.ToString());
                base.AbrirModal(query.ObterUrlCriptografada(), 740, 450);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void chkMatComSemCodigo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.chkMatComSemCodigo.Checked == true)
                    this.ModoMaterialComCodigo(false);
                else
                    this.ModoMaterialComCodigo(true);

                this.CarregarMaterialSemCodigoParaInformarReposicao();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnAdicionaMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    this.AdicionarMaterialEmInformarReposicao();
                    this.CarregarGridInformarReposicao();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void txtRegCliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.LimparDadosDoPaciente();

                if (!string.IsNullOrWhiteSpace(this.txtRegCliente.Text) &&
                    this.txtRegCliente.Text.Length == 8)
                {
                    this.CarregarPacienteParaRegistrarConsumoSaida(this.txtRegCliente.Text);
                }                

                this.CarregarRegistroConsumoSaida();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnVoltarRegConsSaida_Click(object sender, EventArgs e)
        {
            try
            {
                this.IrParaRegistrarConferenciaDeLacre();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            // Verificar se há itens a serem exibidos.
            List<Entity.LacreRepositorioItens> listLacreRepItem = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioItens().ObterPorLacreRepositorio(this.seqLacreRepositorio, null);

            if (listLacreRepItem == null || listLacreRepItem.Count == 0)
            {
                this.ExibirMensagem(TipoMensagem.Alerta, "Não há itens a serem visualizados.");
                return;
            }

            // Gerar um PDF para download !!!(Não precisa abrir o relatório em TELA);
            Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
            query.Url = "~/DownLoadArquivo.aspx";
            query.Parametros.Add("seqLacreRep", this.seqLacreRepositorio.ToString());
            query.Parametros.Add("seqAtendimento", this.seqAtendimento.ToString());
            query.Parametros.Add("tipo_arquivo", "REG_CONSUMO_SAIDA");
            Response.Redirect(query.ObterUrlCriptografada(), false);
        }

        protected void btnSalvarConsumoSaida_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    this.GravarItensConsumoSaida();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void ibtnBuscaPaciente_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.hdfComando.Value = "BUSCA_PACIENTE_REG_CONS_SAIDA";

                Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();                
                query.Url = Page.ResolveUrl("~/Comum/BuscaPaciente.aspx");
                query.Parametros.Add("id", this.hdfRetornoValor.ClientID);
                query.Parametros.Add("valor", this.txtNomePaciente.ClientID);
                base.AbrirModal(query.ObterUrlCriptografada(), 740, 450);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(this.hdfComando.Value))
                {
                    if (this.hdfComando.Value == "BUSCA_PACIENTE_REG_CONS_SAIDA")
                    {
                        if (!string.IsNullOrWhiteSpace(this.hdfRetornoValor.Value))
                        {
                            this.CarregarPacienteParaRegistrarConsumoSaida(this.hdfRetornoValor.Value);
                            this.CarregarRegistroConsumoSaida();
                        }
                    }
                    else if (this.hdfComando.Value == "BUSCA_MATERIAL_LISTA_CONTROLE_ITEM")
                    {
                        this.CarregarMaterialComCodigoPorSeqListaControleItem();
                    }
                    else if (this.hdfComando.Value == "BUSCA_PATRIMONIO_EQUIPAMENTO")
                    {
                        if (!string.IsNullOrWhiteSpace(this.hdfNumBemEquipamento.Value))
                        {
                            this.CarregarPatrimonioParaEquipamentosPorNumeroBem();
                        }
                    }
                    else if (this.hdfComando.Value.Contains("INFORMAR_TESTE_EQUIPAMENTO"))
                    {
                        this.InformarTesteDeEquipamento();
                    }
                    else if (this.hdfComando.Value == "EXCLUIR_EQUIPAMENTO")
                    {
                        this.InativarEquipamento();
                    }
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
        /// Enviar email para os gestores notificando sobre lacração de repositório com ocorrência.
        /// </summary>
        protected void EnviarEmailParaGestorNotificandoSobreLacracaoComOcorrenciaComQtdDiferente(string email,
                                                                                                Int64 numLacre,
                                                                                                Int64? numCaicaIntubacao,
                                                                                                Int64 seqLacreRepositorio,
                                                                                                string dscTipoRepositorio,
                                                                                                string repositorioIdentificacao,
                                                                                                DateTime dataCadastroCarro,
                                                                                                string nomeusuarioResponsavel,
                                                                                                bool temQtdDiferentes,
                                                                                                bool temValidadeForaPeriodo)
        {
            try
            {
                StringBuilder corpo = new StringBuilder();
                corpo.AppendLine("Lacração com ocorrências.");
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("<br /> ");

                if (temQtdDiferentes == true && temValidadeForaPeriodo == true)
                    corpo.AppendLine("Lacração com quantidade necessárias diferentes das quantidades disponíveis e itens com validade  vencer.");
                else if (temQtdDiferentes == true && temValidadeForaPeriodo == false)
                    corpo.AppendLine("Lacração com quantidade necessárias diferentes das quantidades disponíveis.");
                else if (temQtdDiferentes == false && temValidadeForaPeriodo == true)
                    corpo.AppendLine("Itens com validade a vencer.");

                corpo.AppendLine("<br /> ");
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("Justificativa do usuário:");

                if (!string.IsNullOrWhiteSpace(this.txtDscOcorrenciaLancarComQtdDiferente.Text))
                {
                    corpo.AppendLine("<br />");
                    corpo.AppendLine(this.txtDscOcorrenciaLancarComQtdDiferente.Text);
                }

                if (!string.IsNullOrWhiteSpace(this.txtDscOcorrenciaLancarComItensAVencer.Text))
                {
                    corpo.AppendLine("<br />");
                    corpo.AppendLine(this.txtDscOcorrenciaLancarComItensAVencer.Text);
                }

                corpo.AppendLine("<br /> ");
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("Número lacre: ");
                corpo.AppendLine(numLacre.ToString());

                if (numCaicaIntubacao != null)
                {
                    corpo.AppendLine("<br /> ");
                    corpo.AppendLine("<br /> ");
                    corpo.AppendLine("Emitido por: ");
                    corpo.AppendLine(numCaicaIntubacao.Value.ToString());
                }

                corpo.AppendLine("<br /> ");
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("Id repositório: ");
                corpo.AppendLine(seqLacreRepositorio.ToString());
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("Tipo de repositório: ");
                corpo.AppendLine(dscTipoRepositorio);
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("Identificação repositório: ");
                corpo.AppendLine(repositorioIdentificacao);
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("Data do cadastro: ");
                corpo.AppendLine(dataCadastroCarro.ToString("dd/MM/yyyy"));
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("Responsável: ");
                corpo.AppendLine(nomeusuarioResponsavel);

                new BLL.Email().Enviar(corpo.ToString(), email, "Ocorrência de Lacração Carro Urgência Psicoativo");

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Verificar se usuário logado pode lacrar.
        /// </summary>
        protected void VerifircarSeUsuarioLogadoPodeLacrar()
        {
            try
            {
                bool usuarioEstaNaRole = false;

                usuarioEstaNaRole = new BLL.Usuario().UsuarioLogadoEstaNaRole(BLL.Parametrizacao.Instancia().CodigoDaRoleEnfermeiro);

                if (usuarioEstaNaRole == false)
                {
                    this.btnLacrar.Enabled = false;
                    this.txtNumLacre.Enabled = false;
                    this.txtNumCaixaIntubacao.Enabled = false;
                    divInfoPodeLacrar.Visible = true;

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lacre ocorrências.
        /// </summary>
        protected void CarregarLacreOcorrencia()
        {
            try
            {
                this.rptLacreOcorrenciaRegistradas.DataSource = new BLL.LacreOcorrencia().ObterPorSeqLacreRepositorio(this.seqLacreRepositorio);
                this.rptLacreOcorrenciaRegistradas.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inativar equipamento.
        /// </summary>
        protected void InativarEquipamento()
        {
            try
            {
                if (this.seqLacreRepositorioEquipamento > 0)
                {
                    new BLL.LacreRepositorioEquipamento().InativarEquipamento(this.seqLacreRepositorioEquipamento);

                    this.ExibirMensagem(TipoMensagem.Sucesso, "Equipamento inativado com sucesso.");

                    this.CarregarLacreRepositorioEquipamento();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Informar teste de equipamento.
        /// </summary>
        protected void InformarTesteDeEquipamento()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(this.hdfComando.Value))
                {
                    // Obter a data selecionada.
                    string[] chaves = this.hdfComando.Value.Split('|');

                    if (chaves.Length == 2 &&
                        !string.IsNullOrWhiteSpace(chaves[1]))
                    {

                        if (this.seqLacreRepositorioEquipamento > 0)
                        {
                            new BLL.LacreRepositorioEquipamento().AtualizarDadosDeTesteDoEquipamento(this.seqLacreRepositorioEquipamento, Convert.ToDateTime(chaves[1]), Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado);

                            this.ExibirMensagem(TipoMensagem.Sucesso, "Teste de equipamento informando com sucesso.");

                            this.CarregarLacreRepositorioEquipamento();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar lacre repositório equipamentos.
        /// </summary>
        protected void CarregarLacreRepositorioEquipamento()
        {
            try
            {
                this.grvItemRepLacreEquipamento.DataSource = new BLL.LacreRepositorioEquipamento().ObterPorLacreRepositorio(this.seqLacreRepositorio);
                this.grvItemRepLacreEquipamento.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar tipo de patrimonio.
        /// </summary>
        protected void CarregarTipoPatrimonio()
        {
            try
            {
                this.ddlTipoPatrimonio.DataValueField = "CodigoTipoPatrimonio";
                this.ddlTipoPatrimonio.DataTextField = "Descricao";
                this.ddlTipoPatrimonio.DataSource = new BLL.TipoPatrimonio().ObterTiposDePatrimonio();
                this.ddlTipoPatrimonio.DataBind();
                this.ddlTipoPatrimonio.Items.Insert(0, new ListItem("SELECIONE", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar patrimônios para equipamentos por número bem.
        /// </summary>
        protected void CarregarPatrimonioParaEquipamentosPorNumeroBem()
        {
            try
            {
                Int64 numBem = 0;

                Int64.TryParse(this.hdfNumBemEquipamento.Value, out numBem);

                if (numBem > 0)
                {
                    this.hdfNumBemEquipamento.Value = string.Empty;
                    this.txtNumPatrimonio.Text = string.Empty;
                    this.txtDscPatrimonio.Text = string.Empty;

                    Entity.BemPatrimonial bemPatrimonial = new BLL.BemPatrimonial().ObterPatrimonioPorNumeroETipoVinculadoItemListaControle(numBem, null, null);

                    if (bemPatrimonial != null && bemPatrimonial.NumBem > 0)
                    {
                        if (bemPatrimonial.NumeroPatrimonio != null)
                            this.txtNumPatrimonio.Text = bemPatrimonial.NumeroPatrimonio.Value.ToString();

                        if (!string.IsNullOrWhiteSpace(bemPatrimonial.DscModelo))
                            this.txtDscPatrimonio.Text = bemPatrimonial.DscModelo;

                        this.hdfNumBemEquipamento.Value = bemPatrimonial.NumBem.ToString();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar patrimônios para equipamentos por número patrimonio.
        /// </summary>
        protected void CarregarPatrimonioParaEquipamentosPorNumeroPatrimonio()
        {
            try
            {
                if (this.ddlTipoPatrimonio.SelectedValue != "" &&
                    this.ddlTipoPatrimonio.SelectedValue != "0")
                {
                    Int64 numPatrimonio = 0;

                    this.txtDscPatrimonio.Text = string.Empty;
                    this.hdfNumBemEquipamento.Value = string.Empty;

                    if (!string.IsNullOrWhiteSpace(this.txtNumPatrimonio.Text))
                    {
                        if (Int64.TryParse(this.txtNumPatrimonio.Text, out numPatrimonio) == true)
                        {
                            Entity.BemPatrimonial bemPatrimonial = new BLL.BemPatrimonial().ObterPatrimonioPorNumeroETipoVinculadoItemListaControle(null, numPatrimonio, Convert.ToInt64(this.ddlTipoPatrimonio.SelectedValue));

                            if (bemPatrimonial != null && bemPatrimonial.NumBem > 0)
                            {
                                if (bemPatrimonial.NumeroPatrimonio != null)
                                    this.txtNumPatrimonio.Text = bemPatrimonial.NumeroPatrimonio.ToString();

                                if (!string.IsNullOrWhiteSpace(bemPatrimonial.DscModelo))
                                    this.txtDscPatrimonio.Text = bemPatrimonial.DscModelo;

                                this.hdfNumBemEquipamento.Value = bemPatrimonial.NumBem.ToString();
                            }
                            else
                            {
                                this.ExibirMensagem(TipoMensagem.Alerta, "Equipamento não encontrado.");
                            }
                        }
                        else
                        {
                            this.ExibirMensagem(TipoMensagem.Alerta, "Número de patrimônio inválido.");
                        }
                    }
                }
                else
                {
                    this.ExibirMensagem(TipoMensagem.Alerta, "O tipo de patrimônio deve ser selecionado.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Excluir material de lacre repositorio item.
        /// </summary>
        protected void ExcluirMaterialDeLacreRepositorioItem(Int64 seqLacreRepItem)
        {
            try
            {
                // Verificar se o material já foi utilizado.
                List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioUtilizacao> listLacreRepUtilizacao = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioUtilizacao().ObterPorSeqLacreRepositorioItem(seqLacreRepItem);

                if (listLacreRepUtilizacao != null &&
                    listLacreRepUtilizacao.Count > 0)
                {
                    this.ExibirMensagem(TipoMensagem.Alerta, "Este material já foi utilizado, o mesmo não poderá ser removido.");
                    return;
                }

                new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioItens().Excluir(seqLacreRepItem);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar material sem código para informar reposição.
        /// </summary>
        protected void CarregarMaterialSemCodigoParaInformarReposicao()
        {
            try
            {
                if (this.ddlItemMatSemCodigo.Items.Count == 0)
                {

                    this.ddlItemMatSemCodigo.DataValueField = "SeqItensListaControle";
                    this.ddlItemMatSemCodigo.DataTextField = "DescricaoMaterial";
                    this.ddlItemMatSemCodigo.DataSource = new Hcrp.CarroUrgenciaPsicoativo.BLL.ItensListaControle().ObterListaDeListaControleItemComMaterialSemCodigo(this.seqLacreRepositorio);
                    this.ddlItemMatSemCodigo.DataBind();

                    this.ddlItemMatSemCodigo.Items.Insert(0, new ListItem("SELECIONE", "0"));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Informar lote do material para informar reposição.
        /// </summary>
        protected void CarregarLoteParaInformarResposicao(string codMaterial)
        {
            try
            {
                this.ddlLoteMatComCodigo.Items.Clear();

                if (!string.IsNullOrWhiteSpace(codMaterial))
                {
                    this.ddlLoteMatComCodigo.DataValueField = "NumLote";
                    this.ddlLoteMatComCodigo.DataTextField = "NumLoteFabricante";
                    this.ddlLoteMatComCodigo.DataSource = new Hcrp.CarroUrgenciaPsicoativo.BLL.Lote().ObterPorCodMaterial(codMaterial);
                    this.ddlLoteMatComCodigo.DataBind();
                }

                this.ddlLoteMatComCodigo.Items.Insert(0, new ListItem("SELECIONE", "0"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar material com código por código de material.
        /// </summary>
        protected void CarregarMaterialComCodigoPorCodigoMaterial()
        {
            try
            {   
                if (!string.IsNullOrWhiteSpace(this.txtCodMaterial.Text))
                {
                    this.txtCodMaterial.Text = this.txtCodMaterial.Text.ToUpper();

                    this.txtNomeMaterialComCodigo.Text = string.Empty;
                    this.txtUnidMedMaterialComCodigo.Text = string.Empty;
                    this.hdfSeqItemListaControle.Value = string.Empty;
                    
                    List<Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle> listItemListaControle = new Hcrp.CarroUrgenciaPsicoativo.BLL.ItensListaControle().ObterListaDeListaControleItemComMaterialComCodigo(this.seqLacreRepositorio);

                    if (listItemListaControle != null)
                    {
                        // Obter pelo código.
                        var itemListControle = (from itemListControleL in listItemListaControle
                                                where itemListControleL.Material.Codigo == this.txtCodMaterial.Text
                                                select itemListControleL).FirstOrDefault();

                        if (itemListControle != null)
                        {
                            if (itemListControle.Material != null)
                            {
                                if (!string.IsNullOrWhiteSpace(itemListControle.Material.Codigo))
                                    this.txtCodMaterial.Text = itemListControle.Material.Codigo;

                                if (!string.IsNullOrWhiteSpace(itemListControle.Material.Nome))
                                    this.txtNomeMaterialComCodigo.Text = itemListControle.Material.Nome;
                            }

                            if (itemListControle.Unidade != null &&
                                !string.IsNullOrWhiteSpace(itemListControle.Unidade.Nome))
                            {
                                this.txtUnidMedMaterialComCodigo.Text = itemListControle.Unidade.Nome;
                            }

                            this.hdfSeqItemListaControle.Value = itemListControle.SeqItensListaControle.ToString();

                            this.CarregarLoteParaInformarResposicao(itemListControle.Material.Codigo);
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar material com código por seq lista controle item.
        /// </summary>
        protected void CarregarMaterialComCodigoPorSeqListaControleItem()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(this.hdfSeqItemListaControle.Value))
                {
                    Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle itemListaControle = new Hcrp.CarroUrgenciaPsicoativo.BLL.ItensListaControle().ObterItensPorId(Convert.ToInt64(this.hdfSeqItemListaControle.Value));

                    if (itemListaControle != null &&
                        itemListaControle.SeqItensListaControle > 0)
                    {
                        if (itemListaControle.Material != null)
                        {
                            if (!string.IsNullOrWhiteSpace(itemListaControle.Material.Codigo))
                                this.txtCodMaterial.Text = itemListaControle.Material.Codigo;

                            if (!string.IsNullOrWhiteSpace(itemListaControle.Material.Nome))
                                this.txtNomeMaterialComCodigo.Text = itemListaControle.Material.Nome;
                        }

                        if (itemListaControle.Unidade != null &&
                            !string.IsNullOrWhiteSpace(itemListaControle.Unidade.Nome))
                        {
                            this.txtUnidMedMaterialComCodigo.Text = itemListaControle.Unidade.Nome;
                        }

                        this.CarregarLoteParaInformarResposicao(itemListaControle.Material.Codigo);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Modo de material com código ou sem código.
        /// </summary>
        protected void ModoMaterialComCodigo(bool comCodigo)
        {
            try
            {
                this.txtCodMaterial.Text = string.Empty;
                this.txtNomeMaterialComCodigo.Text = string.Empty;

                if (this.ddlLoteMatComCodigo.Items.Contains(this.ddlLoteMatComCodigo.Items.FindByValue("0")))
                    this.ddlLoteMatComCodigo.SelectedValue = "0";
                
                this.txtUnidMedMaterialComCodigo.Text = string.Empty;

                if (this.ddlItemMatSemCodigo.Items.Contains(this.ddlItemMatSemCodigo.Items.FindByValue("0")))
                    this.ddlItemMatSemCodigo.SelectedValue = "0";
                
                this.txtQtdMat.Text = string.Empty;

                this.pnMaterialComCodigo.Visible = false;
                this.pnMaterialSemCodigo.Visible = false;

                if (comCodigo == true)
                    this.pnMaterialComCodigo.Visible = true;
                else
                    this.pnMaterialSemCodigo.Visible = true;

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar grid informar reposição.
        /// </summary>
        protected void CarregarGridInformarReposicao()
        {
            try
            {
                this.grvItemInformarReposicao.DataSource = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioItens().ObterPorLacreRepositorio(this.seqLacreRepositorio, null);
                this.grvItemInformarReposicao.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Adicionar material na grid de informar reposição.
        /// </summary>
        protected void AdicionarMaterialEmInformarReposicao()
        {
            try
            {

                Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens lacreRepItens = new Entity.LacreRepositorioItens();

                lacreRepItens.LacreRepositorio = new Entity.LacreRepositorio();
                lacreRepItens.LacreRepositorio.SeqLacreRepositorio = this.seqLacreRepositorio;

                lacreRepItens.Material = new Entity.Material();
                lacreRepItens.Lote = new Entity.Lote();

                lacreRepItens.ItensListaControle = new Entity.ItensListaControle();

                lacreRepItens.UsuarioCadastro = new Entity.Usuario();
                lacreRepItens.UsuarioCadastro.NumUserBanco = Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;

                lacreRepItens.QtdDisponivel = Convert.ToInt32(this.txtQtdMat.Text);

                // Verificar se o material tem código ou não.
                if (this.pnMaterialComCodigo.Visible == true)
                {
                    if (!string.IsNullOrWhiteSpace(this.txtCodMaterial.Text))
                        lacreRepItens.Material.Codigo = this.txtCodMaterial.Text;

                    if (this.ddlLoteMatComCodigo.SelectedValue != "0")
                        lacreRepItens.Lote.NumLote = Convert.ToInt64(this.ddlLoteMatComCodigo.SelectedValue);

                    // Obter dados do lote.
                    Hcrp.CarroUrgenciaPsicoativo.Entity.Lote lote = null;
                    if (lacreRepItens.Lote.NumLote > 0)
                        lote = new Hcrp.CarroUrgenciaPsicoativo.BLL.Lote().ObterPorId(lacreRepItens.Lote.NumLote);

                    if (lote != null &&
                        lote.NumLote > 0)
                    {
                        //lacreRepItens.QtdDisponivel = lote.QtdLote;
                        lacreRepItens.NumLoteFabricante = lote.NumLoteFabricante;
                        lacreRepItens.DataValidadeLote = lote.DataValidadeLote;
                    }
                    //else
                    //{
                    //    this.ExibirMensagem(TipoMensagem.Alerta, "Não foi possível localizar o lote para este material.");
                    //    return;
                    //}

                    // Obter a seq itens lista controle pelo código do material.
                    Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle itemListaControle = new Hcrp.CarroUrgenciaPsicoativo.BLL.ItensListaControle().ObterItensPorCodMaterialESeqLacreRespositorio(this.seqLacreRepositorio, lacreRepItens.Material.Codigo);

                    if (!string.IsNullOrWhiteSpace(this.hdfSeqItemListaControle.Value))
                    {
                        lacreRepItens.ItensListaControle.SeqItensListaControle = itemListaControle.SeqItensListaControle;
                    }
                    else
                    {
                        this.ExibirMensagem(TipoMensagem.Alerta, "Não foi possível localizar o item da lista de controle.");
                        return;
                    }
                }
                else
                {
                    // Material sem código.

                    if (this.ddlItemMatSemCodigo.SelectedValue != "0")
                        lacreRepItens.ItensListaControle.SeqItensListaControle = Convert.ToInt64(this.ddlItemMatSemCodigo.SelectedValue);
                }

                // Verificar a quantidade de itens.
                // Não deixar o usuário colocar mais item do que o permitido na configuração da lista(ITENS_LISTA_CONTROLE.QTD_NECESSARIA).
                if (lacreRepItens != null &&
                    lacreRepItens.ItensListaControle != null &&
                    lacreRepItens.ItensListaControle.SeqItensListaControle > 0)
                {
                    Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle itemListControleAux = new Hcrp.CarroUrgenciaPsicoativo.BLL.ItensListaControle().ObterItensPorId(lacreRepItens.ItensListaControle.SeqItensListaControle.Value);
                    if (lacreRepItens.QtdDisponivel > itemListControleAux.QuantidadeNecessaria)
                    {
                        this.ExibirMensagem(TipoMensagem.Alerta, "Quantidade informada não pode ser maior que a quantidade necessária (" + itemListControleAux.QuantidadeNecessaria + "). ");
                        return;
                    }
                }

                // Verificar se o item já foi adicionado.    
                Int64? numLoteAux = null;

                if (lacreRepItens.Lote != null && lacreRepItens.Lote.NumLote > 0)
                    numLoteAux = lacreRepItens.Lote.NumLote;

                if (new BLL.LacreRepositorioItens().VerificarSeOMaterialJahFoiAdicionadoParaLacreRepositorioItens(this.seqLacreRepositorio, lacreRepItens.ItensListaControle.SeqItensListaControle.Value, numLoteAux) == true)
                {
                    this.ExibirMensagem(TipoMensagem.Alerta, "Material já adicionado, informe outro. ");
                    return;
                }

                // Adicionar
                new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioItens().Adicionar(lacreRepItens);

                // Limpar campos.
                this.txtCodMaterial.Text = string.Empty;
                this.txtNomeMaterialComCodigo.Text = string.Empty;
                this.hdfSeqItemListaControle.Value = string.Empty;
                this.txtUnidMedMaterialComCodigo.Text = string.Empty;
                this.txtQtdMat.Text = string.Empty;

                if (this.ddlLoteMatComCodigo.Items.Contains(this.ddlLoteMatComCodigo.Items.FindByValue("0")))
                    this.ddlLoteMatComCodigo.SelectedValue = "0";

                if (this.ddlItemMatSemCodigo.Items.Contains(this.ddlItemMatSemCodigo.Items.FindByValue("0")))
                    this.ddlItemMatSemCodigo.SelectedValue = "0";

                this.ExibirMensagem(TipoMensagem.Sucesso, "Item adicionado com sucesso.");

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Limpar dados do paciente.
        /// </summary>
        protected void LimparDadosDoPaciente()
        {
            try
            {
                this.txtIdadePaciente.Text = string.Empty;
                this.txtNomePaciente.Text = string.Empty;
                this.lblSexo.Text = string.Empty;
                this.lblCor.Text = string.Empty;

                this.seqAtendimento = 0;
                this.lblAtendimento.Text = string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Ir para tela de registrar conferencia de lacre.
        /// </summary>
        protected void IrParaRegistrarConferenciaDeLacre()
        {
            try
            {
                Response.Redirect("~/Conferencia/RegistrarConferenciaDeLacre.aspx", false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter o atendimento do paciente.
        /// </summary>
        protected void ObterAtendimentoDoPaciente(string codPaciente)
        {
            try
            {
                this.seqAtendimento = 0;
                this.lblMsgOperacaoConsumoSaida.Text = "";
                divlblMsgOperacaoConsumoSaida.Visible = false;

                // TODO: (bruno) ver com lucas a forma de busca atendimento do paciente.
                // 12/11/2008 data para teste.
                this.seqAtendimento = new Hcrp.CarroUrgenciaPsicoativo.BLL.AtendimentoPaciente().ObterAtendimentoDoPacienteParaAData(codPaciente, DateTime.Now);

                // TODO: (bruno) remover.
                // para o paciente 0011084E forçar o atendimento 6382
                if (codPaciente == "0011084E")
                    this.seqAtendimento = 6382;

                if (this.seqAtendimento == 0)
                {
                    divlblMsgOperacaoConsumoSaida.Visible = true;
                    this.ExibirMensagem(TipoMensagem.Alerta, "Não foi possível encontrar o atendimento do paciente. O registro de consumo e saída não será lançado para um atendimento de paciente.");
                }
                else
                {
                    // Obter numero e ano de atendimento.
                    DataView dtViewDadosAtendimento = new Hcrp.CarroUrgenciaPsicoativo.BLL.AtendimentoPaciente().ObterDadosDoAtendimentoPorSeqAtendimento(this.seqAtendimento);
                    if (dtViewDadosAtendimento != null && dtViewDadosAtendimento.Count > 0)
                    {
                        this.lblAtendimento.Text = dtViewDadosAtendimento[0]["NUM_ANO_ATENDIMENTO"].ToString();
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Validar itens de Consumo/Saida
        /// </summary>
        protected void GravarItensConsumoSaida()
        {
            try
            {
                Label lblAlinea = null;
                Label lblCodigoMaterial = null;
                Label lblNomeMaterial = null;
                Label lblLote = null;
                TextBox txtUtilizada = null;                
                HiddenField hdfSeqLacreRepositorioItens = null;
                Label lblQtdDisponivel = null;
                TextBox txtJustificativa = null;

                string resultadoValidacao = string.Empty;
                int qtdUtilizada = 0;
                int qtdDisponivel = 0;
                int qtdUtilizadaComAtendimento = 0;
                Int64 seqLacreRepItens = 0;                
                List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens> listLacreRepItens = new List<Entity.LacreRepositorioItens>();
                Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens lacreRepItens = null;

                // Obter as quantidades dísponíveis novamente para saber se ainda continuam com as mesmas quantidades.
                List<Entity.QuantidadeRegistroConsumoSaida> listQtdDisponivelComAtendimento = new BLL.LacreRepositorioItens().ObterQuantidadeDisponivelParaConsumoSaida(this.seqLacreRepositorio, this.seqAtendimento);
                if (listQtdDisponivelComAtendimento == null)
                    return;

                foreach (GridViewRow item in this.grvItemConsumoSaida.Rows)
                {   
                    lblAlinea = (Label)item.FindControl("lblAlinea");
                    lblCodigoMaterial = (Label)item.FindControl("lblCodigoMaterial");
                    lblNomeMaterial = (Label)item.FindControl("lblNomeMaterial");
                    lblLote = (Label)item.FindControl("lblLote");
                    txtUtilizada = (TextBox)item.FindControl("txtUtilizada");                    
                    hdfSeqLacreRepositorioItens = (HiddenField)item.FindControl("hdfSeqLacreRepositorioItens");
                    lblQtdDisponivel = (Label)item.FindControl("lblQtdDisponivel");
                    txtJustificativa = (TextBox)item.FindControl("txtJustificativa");

                    // Obter chave.
                    seqLacreRepItens = Convert.ToInt64(hdfSeqLacreRepositorioItens.Value);

                    // Obter q qtd disponivel com atendimento para o item.
                    qtdDisponivel = 0;
                    qtdUtilizadaComAtendimento = 0;
                    var qtdRegConsumoSaida = (from qtdRegConsumoSaidaL in listQtdDisponivelComAtendimento
                                              where qtdRegConsumoSaidaL.SeqLacreRepositorioItens == seqLacreRepItens
                                              select qtdRegConsumoSaidaL).FirstOrDefault();
                    if (qtdRegConsumoSaida != null)
                    {
                        qtdDisponivel = qtdRegConsumoSaida.QtdDisponivelComTodoAtendimento;
                        qtdUtilizadaComAtendimento = qtdRegConsumoSaida.QtdUtilizadaComAtendimento;
                    }

                    if (Int32.TryParse(txtUtilizada.Text, out qtdUtilizada) == false)
                    {
                        resultadoValidacao = string.Format("O item {0} {1} - {2} {3} está com a quantidade utilizada inválida.", 
                            lblAlinea.Text, lblCodigoMaterial.Text, lblNomeMaterial.Text, lblLote.Text);
                        break;
                    }
                    else if (qtdUtilizada > (qtdDisponivel + qtdUtilizadaComAtendimento))
                    {
                        // Se a quantidade utilizada for maior que zero, então já foi utilizada deste item, devolve o que foi consumido para fazer a comparação.                        
                        
                        // Não deixar o usuário informar a coluna "Qtd. utilizada" maior que a coluna "Qtd. Disponivel".                        
                        resultadoValidacao = string.Format("O item {0} {1} - {2} {3} está com a quantidade utilizada maior que a quantidade disponível.", 
                            lblAlinea.Text, lblCodigoMaterial.Text, lblNomeMaterial.Text, lblLote.Text);
                        break;
                    }
                    else if (!string.IsNullOrWhiteSpace(txtJustificativa.Text) && txtJustificativa.Text.Length > 200)
                    {
                        // Não deixar o usuário informar um texto maior que 200 caracteres.                      
                        resultadoValidacao = string.Format("O item {0} {1} - {2} {3} está com o texto da justificativa muito extenso, informe uma justificativa com até 200 caracteres.",
                            lblAlinea.Text, lblCodigoMaterial.Text, lblNomeMaterial.Text, lblLote.Text);
                        break;
                    }
                    else
                    {
                        // Armazena na lista para depois mandar para o banco.
                        lacreRepItens = new Entity.LacreRepositorioItens();
                        lacreRepItens.SeqLacreRepositorioItens = seqLacreRepItens;
                        lacreRepItens.QtdUtilizada = Convert.ToInt32(txtUtilizada.Text);

                        if (!string.IsNullOrWhiteSpace(txtJustificativa.Text))
                            lacreRepItens.DscJustificativaConsumoSemAtendimento = txtJustificativa.Text.ToUpper().Trim();

                        //lacreRepItens.Lote = new Entity.Lote();
                        //DateTime.TryParse(txtValidade.Text, out auxData);
                        //if (auxData.Year > 1900)
                        //    lacreRepItens.Lote.DataValidadeLote = auxData;

                        listLacreRepItens.Add(lacreRepItens);
                    }
                }

                if (!string.IsNullOrWhiteSpace(resultadoValidacao))
                {
                    this.ExibirMensagem(TipoMensagem.Alerta, resultadoValidacao);
                }
                else if (listLacreRepItens.Count > 0)
                {
                    // Realizar gravação.
                    int numUserLogado = Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;

                    Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioItens adLacreRepItem = new BLL.LacreRepositorioItens();
                    Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioUtilizacao adLacreRepUtilizacao = new BLL.LacreRepositorioUtilizacao();

                    Hcrp.Infra.AcessoDado.TransacaoDinamica transacao = new Hcrp.Infra.AcessoDado.TransacaoDinamica();

                    foreach (Entity.LacreRepositorioItens item in listLacreRepItens)
                    {
                        //adLacreRepItem.AtualizarDataVencimentoDoLote(transacao, item.SeqLacreRepositorioItens, item.Lote.DataValidadeLote);
                        adLacreRepUtilizacao.AtualizarOuAdicionarQuantidadeDeUtilizacao(transacao, item.SeqLacreRepositorioItens, item.QtdUtilizada, this.seqAtendimento, numUserLogado,
                            item.DscJustificativaConsumoSemAtendimento);
                    }

                    transacao.ComitarTransacao();

                    this.CarregarRegistroConsumoSaida();

                    this.ExibirMensagem(TipoMensagem.Sucesso, "Dados atualizados com sucesso.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar materiais para registro de consumo/saida.
        /// </summary>
        protected void CarregarRegistroConsumoSaida()
        {
            try
            {
                if (this.seqLacreRepositorio > 0)
                {
                    Int64? seqAtendimentoP = null;

                    if (this.seqAtendimento > 0)
                        seqAtendimentoP = this.seqAtendimento;

                    this.grvItemConsumoSaida.DataSource = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioItens().ObterPorLacreRepositorio(this.seqLacreRepositorio, seqAtendimentoP);
                    this.grvItemConsumoSaida.DataBind();

                    // Se touxe registros e não tem atendimento do paciente, informar usuário.
                    if (grvItemConsumoSaida.Rows.Count > 0 && this.seqAtendimento == 0)
                    {
                        this.lblMsgOperacaoConsumoSaida.Text = "Consumo de itens sem atendimento do paciente, informe um paciente em atendimento, caso contrário uma justificativa deveré ser informada para o consumo.";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetPostGrid()
        {
            PostBackOptions options = new PostBackOptions(btnPost);
            Page.ClientScript.RegisterForEventValidation(options);
            return Page.ClientScript.GetPostBackEventReference(options);
        }

        /// <summary>
        /// Carregar paciente para registrar consumo/saida.
        /// </summary>
        protected void CarregarPacienteParaRegistrarConsumoSaida(string codigoPaciente)
        {
            try
            {
                this.LimparDadosDoPaciente();

                if (!string.IsNullOrWhiteSpace(codigoPaciente))
                    codigoPaciente = codigoPaciente.ToUpper().Trim();

                Framework.Classes.Paciente paciente = new Framework.Classes.Paciente().BuscarPacienteRegistro(codigoPaciente);

                if (paciente != null)
                {
                    if (!string.IsNullOrWhiteSpace(paciente.RegistroPaciente))
                        this.txtRegCliente.Text = paciente.RegistroPaciente;

                    if (!string.IsNullOrWhiteSpace(paciente.NomeCompletoPaciente))
                        this.txtNomePaciente.Text = paciente.NomeCompletoPaciente;

                    if (!string.IsNullOrWhiteSpace(paciente.Idade))
                        this.txtIdadePaciente.Text = new Hcrp.CarroUrgenciaPsicoativo.BLL.Paciente().IdadeFormatadaEmAnoMesDia(paciente.Idade);

                    if (!string.IsNullOrWhiteSpace(paciente.SexoPaciente))
                        this.lblSexo.Text = paciente.SexoPaciente;

                    this.lblCor.Text = Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.EnumUtil.DescricaoEnum(paciente.Cor);

                    if (!string.IsNullOrWhiteSpace(this.lblCor.Text))
                        this.lblCor.Text = this.lblCor.Text.ToUpper();

                    // Buscar atendimento.
                    this.ObterAtendimentoDoPaciente(codigoPaciente);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Carregar interface.
        /// </summary>
        protected void CarregarInterface()
        {
            try
            {
                Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio lacreRep = null;

                List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio> listLacreRep = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorio().ObterPorFiltro(this.seqLacreRepositorio,
                                                                                                                                                                null,
                                                                                                                                                                null,
                                                                                                                                                                null);

                if (listLacreRep != null &&
                    listLacreRep.Count > 0)
                {
                    lacreRep = listLacreRep[0];

                    if (lacreRep.RepositorioListaControle != null &&
                        lacreRep.RepositorioListaControle.ListaControle != null &&
                        lacreRep.RepositorioListaControle.ListaControle.Instituto != null &&
                        !string.IsNullOrWhiteSpace(lacreRep.RepositorioListaControle.ListaControle.Instituto.NomeInstituto))
                    {
                        this.lblInstituto.Text = lacreRep.RepositorioListaControle.ListaControle.Instituto.NomeInstituto;
                    }

                    if (lacreRep.TipoSituacaoHc != null &&
                        !string.IsNullOrWhiteSpace(lacreRep.TipoSituacaoHc.NomSituacao))
                    {
                        this.lblSituacao.Text = lacreRep.TipoSituacaoHc.NomSituacao;
                    }

                    if (lacreRep.RepositorioListaControle != null &&
                        !string.IsNullOrWhiteSpace(lacreRep.RepositorioListaControle.DscIdentificacao))
                    {
                        this.lblPatrimonio.Text = lacreRep.RepositorioListaControle.DscIdentificacao;
                    }

                    if (lacreRep.LacreTipoOcorrencia != null &&
                        !string.IsNullOrWhiteSpace(lacreRep.LacreTipoOcorrencia.DscTipoOcorrencia))
                    {
                        this.lblMotivoRompimento.Text = lacreRep.LacreTipoOcorrencia.DscTipoOcorrencia;
                    }

                    if (lacreRep.RepositorioListaControle != null &&
                        lacreRep.RepositorioListaControle.ListaControle != null &&
                        !string.IsNullOrWhiteSpace(lacreRep.RepositorioListaControle.ListaControle.NomeListaControle))
                    {
                        this.lblTipoDaLista.Text = lacreRep.RepositorioListaControle.ListaControle.NomeListaControle;
                    }

                }

                

                // Informações utilizadas na aba "registrar consumo/saida".
                this.CarregarRegistroConsumoSaida();
                
                // Informações utilizadas na aba "Informar reposição".
                this.CarregarLoteParaInformarResposicao("");
                this.CarregarGridInformarReposicao();
                
                // Informações utilizadas na aba "Equipamento"
                this.CarregarTipoPatrimonio();
                this.CarregarLacreRepositorioEquipamento();

                // Informações utilizadas na aba "Resgitrar Ocorrências"
                this.CarregarLacreOcorrencia();
                
                this.VerifircarSeUsuarioLogadoPodeLacrar();

                // Forçar a tabindex para 0
                this.TabContainerAba.ActiveTabIndex = 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        protected void TabContainerAba_ActiveTabChanged(object sender, EventArgs e)
        {
            if (TabContainerAba.ActiveTabIndex == 1) // Informar reposicao
            {
                CarregarGridInformarReposicao();
            }
            //else if(TabContainerAba.ActiveTabIndex == 0) // Registrar consumo/ saida
            //{
            //    CarregarRegistroConsumoSaida();
            //}
        }

        protected void imgBtnBuscapatrimonio_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ddlTipoPatrimonio.SelectedValue == "0" ||
                    this.ddlTipoPatrimonio.SelectedValue == "")
                {
                    this.ExibirMensagem(TipoMensagem.Alerta, "Informe o tipo de patrimônio.");
                    return;
                }

                this.hdfComando.Value = "BUSCA_PATRIMONIO_EQUIPAMENTO";

                Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                query.Url = Page.ResolveUrl("~/Comum/BuscaPatrimonio.aspx");
                query.Parametros.Add("id", this.hdfNumBemEquipamento.ClientID);
                query.Parametros.Add("valor", this.txtDscPatrimonio.ClientID);
                query.Parametros.Add("codTipoPatrimonio", this.ddlTipoPatrimonio.SelectedValue);
                base.AbrirModal(query.ObterUrlCriptografada(), 740, 450);
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}