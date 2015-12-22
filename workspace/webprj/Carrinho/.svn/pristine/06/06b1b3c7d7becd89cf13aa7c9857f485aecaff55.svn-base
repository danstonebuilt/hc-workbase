using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Elmah;
using Hcrp.CarroUrgenciaPsicoativo.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ItensListaControle = Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle;
using LacreRepositorioItens = Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens;
using Material = Hcrp.CarroUrgenciaPsicoativo.Entity.Material;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Conferencia
{
    public partial class GerenciarConferenciaDeLacre : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
    {
        static Int64 seqLacreRepositorio = 0;
        Int64 seqAtendimento = 0;
        Int64 seqLacreRepositorioEquipamento = 0;

        long seqRepositorio = 0;

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
                seqLacreRepositorio = Convert.ToInt64(this.ViewState["seqLacreRepositorio"]);

            if (this.ViewState["seqLacreRepositorioEquipamento"] != null)
                this.seqLacreRepositorioEquipamento = Convert.ToInt64(this.ViewState["seqLacreRepositorioEquipamento"]);

            if (this.ViewState["seqRepositorio"] != null)
                this.seqRepositorio = Convert.ToInt32(this.ViewState["seqRepositorio"]);
        }

        /// <summary>
        /// Salvar ViewState
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            this.ViewState["seqAtendimento"] = this.seqAtendimento;
            this.ViewState["seqLacreRepositorio"] = seqLacreRepositorio;
            this.ViewState["seqLacreRepositorioEquipamento"] = this.seqLacreRepositorioEquipamento;

            this.ViewState["seqRepositorio"] = this.seqRepositorio;

            return base.SaveViewState();
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                seqLacreRepositorio = 0;

                Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                if (Int64.TryParse(query.ObterOValorDoParametro("seqLacreRepositorio"), out seqLacreRepositorio) == true)
                {
                    //this.seqLacreRepositorio = 8;

                    if (seqLacreRepositorio > 0)
                    {
                        this.CarregarInterface();
                    }
                    else
                    {
                        Response.End();
                    }

                    //ScriptManager.RegisterClientScriptBlock(this.Page,typeof(string),"teste","$('#myTab a:first').tab('show')",true);
                }
            }
        }

        protected void txtCodMaterial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.CarregarMaterialComCodigoPorCodigoMaterial();

                txtCodMaterial.Focus();
            }
            catch (Exception err)  
            {
                ErrorSignal.FromCurrentContext().Raise(err);
            }
        }

        protected void btnLacrar_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                Description
                //Validar se a lista de itens do carrinho esta completa(QTD necessária = QTD. Disponivel), caso contrario mostrar mensagem na tela.
                O usuário pode lacrar, porem ele registra a ocorrencia(campo texto aberto)(O sistema envia emails para os gestores com a descrição da ocorrencia.)
                --------------------------------------------------------------------------------------------------------------------------------------------------------
                Validar se na lista de itens do carrinho possui itens dentro do prazo de validade (ITENS com a data de validade dentro de 4 meses)  

                O usuário pode lacrar, porem ele registra a ocorrencia(campo texto aberto)(O sistema envia emails para os gestores com a descrição da ocorrencia.)
                -------------------------------------------------------------------------------------------------------------------------------------------------------
                Somente usuários ENFERMEIROS podem lacrar.
                */

                Int64 numLacre = 0;
                string numCaixaIntubacao = null;
                //Int64 numCaixaIntubacaoAux = 0;
                //Int64 numCaixaIntubacaoAux2 = 0;

                this.tbDscOcorrenciaLacrarComQtdDiferente.Visible = false;

                #region validar número de lacre e número de caixa de intubação

                // Validar número de lacre.
                if (Int64.TryParse(this.txtNumLacre.Text, out numLacre) == false)
                {
                    this.ExibirMensagem(TipoMensagem.Alerta, "Número do lacre inválido.");
                    return;
                }

                // Validar número de caixa de intubação.
                //if (string.IsNullOrWhiteSpace(this.txtNumCaixaIntubacao.Text) && divCaixaIntubacao.Visible)
                //{
                //    this.ExibirMensagem(TipoMensagem.Alerta, "Número da caixa de intubação inválido.");
                //    return;

                //}
                //if (string.IsNullOrWhiteSpace(this.txtNumCaixaIntubacao2.Text) && divCaixaIntubacao.Visible)
                //{
                //    this.ExibirMensagem(TipoMensagem.Alerta, "Número da caixa de intubação inválido.");
                //    return;
                //}

                //numCaixaIntubacao = !String.IsNullOrEmpty(txtNumCaixaIntubacao.Text) ? (txtNumCaixaIntubacao.Text.ToUpper() + " | " + txtNumCaixaIntubacao2.Text.ToUpper()) : "";

                #endregion

                // Obter os itens do repostório.
                List<Entity.LacreRepositorioItens> listRepItens =
                    new BLL.LacreRepositorioItens().ObterPorLacreRepositorio(seqLacreRepositorio, null);

                if (listRepItens == null || listRepItens.Count == 0)
                {
                    this.ExibirMensagem(TipoMensagem.Alerta, "Não foram encontrados os itens do repositório para realizar a lacração.");
                    return;
                }

                #region validar qtd necessário com qtd disponível

                // Validar se qtd necessário é igual a qtd disponível.  
                bool temItensComQtdDiferentes = false;

                var queryLastNames =
                    (from student in listRepItens
                     group student by new
                     {
                         student.Material.Codigo,
                         student.Material.Nome
                     } into newGroup
                     //orderby newGroup.Key
                     select new Entity.LacreRepositorioItens()
                                {
                                    Material = new Material()
                                                   {
                                                       Codigo = Convert.ToString(newGroup.Key)
                                                   },
                                    ItensListaControle = new ItensListaControle()
                                                             {
                                                                 QuantidadeNecessaria = newGroup.Max(x => x.ItensListaControle.QuantidadeNecessaria)
                                                             },
                                    QtdDisponivel = newGroup.Sum(x => x.QtdDisponivel)
                                }).ToList();

                var contaItensComQtdDifrente = (from contaItensComQtdDifrenteL in queryLastNames
                                                where contaItensComQtdDifrenteL.QtdDisponivel != contaItensComQtdDifrenteL.ItensListaControle.QuantidadeNecessaria
                                                select contaItensComQtdDifrenteL).Count();

                #endregion


                #region validar data de validade do item

                // Verificar se o prazo de validade dos itens se encontra dentro do período.
                Int32 numDiasPeriodo = BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMedicamento;

                bool temItensComValidadeForaDoPeriodoEQuantidadesDiferentes = false;
                bool temItensComValidadeForaDoPeriodo = false;
                TimeSpan timeSpan;

                var contaItensDataValidaAVencer = (from item in listRepItens
                                                   where ((item.Lote.DataValidadeLote - DateTime.Now.Date).Days <= numDiasPeriodo)
                                                            && item.QtdDisponivel > 0
                                                        && item.ItensListaControle.Alinea.IdfClasse == 2
                                                   select item).Count();

                if (contaItensDataValidaAVencer > 0 && contaItensComQtdDifrente > 0)
                {
                    //if (this.tbDscOcorrenciaLacrarComValidadeForaPeriodo.Visible &&
                    //    string.IsNullOrWhiteSpace(this.txtDscOcorrenciaLancarComItensAVencer.Text))
                    if (
                        string.IsNullOrWhiteSpace(this.txtItensVencerQuantidadeDiferente.Text))
                    {
                        this.ExibirMensagem(TipoMensagem.Alerta, "Uma justificativa deve ser informada para o lacração com itens que estão com a data de " +
                                                                 "validade a vencer e com quantidades diferentes das quantidades disponíveis.");
                        txtItensVencerQuantidadeDiferente.Focus();

                        return;
                    }

                    temItensComValidadeForaDoPeriodoEQuantidadesDiferentes = true;
                }

                // Precisa informar justificativa.
                if (contaItensDataValidaAVencer > 0 && !temItensComValidadeForaDoPeriodoEQuantidadesDiferentes)
                {
                    //if (this.tbDscOcorrenciaLacrarComValidadeForaPeriodo.Visible &&
                    //    string.IsNullOrWhiteSpace(this.txtDscOcorrenciaLancarComItensAVencer.Text))
                    if (
                        string.IsNullOrWhiteSpace(this.txtDscOcorrenciaLancarComItensAVencer.Text))
                    {
                        this.ExibirMensagem(TipoMensagem.Alerta, "Uma justificativa deve ser informada para o lacração com itens que estão com a data de validade a vencer.");
                        txtDscOcorrenciaLancarComItensAVencer.Focus();
                        return;
                    }

                    temItensComValidadeForaDoPeriodo = true;
                }

                // Precisa informar justificativa.
                if (contaItensComQtdDifrente > 0 && !temItensComValidadeForaDoPeriodoEQuantidadesDiferentes && !temItensComValidadeForaDoPeriodo)
                {
                    //if (this.tbDscOcorrenciaLacrarComQtdDiferente.Visible == false ||
                    //    string.IsNullOrWhiteSpace(this.txtDscOcorrenciaLancarComQtdDiferente.Text))
                    if (
                        string.IsNullOrWhiteSpace(this.txtDscOcorrenciaLancarComQtdDiferente.Text))
                    {
                        this.ExibirMensagem(TipoMensagem.Alerta, "Uma justificativa deve ser informada para o lacração com itens que estão com quantidades necessários " +
                                                                 "diferentes das quantidades disponíveis.");

                        txtDscOcorrenciaLancarComQtdDiferente.Focus();

                        return;
                    }

                    temItensComQtdDiferentes = true;
                }

                #endregion

                // Lacrar carrinho.
                new BLL.LacreRepositorio().LacrarCarrinho(seqLacreRepositorio, numLacre, numCaixaIntubacao, DateTime.Now);

                #region enviar email com as ocorrências
                // Se exitem itens com qtd diferentes ou data de validade fora do período, 
                // enviar emails para os gestores.
                List<Entity.Usuario> listGestores = null;
                if (temItensComQtdDiferentes || temItensComValidadeForaDoPeriodo || temItensComValidadeForaDoPeriodoEQuantidadesDiferentes == true)
                {
                    if (temItensComValidadeForaDoPeriodoEQuantidadesDiferentes)
                    {
                        temItensComQtdDiferentes = true;
                        temItensComValidadeForaDoPeriodo = true;
                    }

                    Entity.LacreRepositorio lacreRep = new BLL.LacreRepositorio().ObterPorId(seqLacreRepositorio);

                    // Precisa pegar o gestor do centro de custo que esta associado ao repositório !!!!!!
                    listGestores = new BLL.Usuario().ObterOsUsuarioGestoresDoRepositorio(BLL.Parametrizacao.Instancia().CodigoDaRoleGestor,
                        lacreRep.RepositorioListaControle.SeqRepositorio);


                    // Se os gestores foram obtidos enviar os emails.
                    if (listGestores != null)
                    {
                        Framework.Classes.Usuario usuarioLogado = new Framework.Classes.Usuario().
                            BuscarUsuarioCodigo(Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado);

                        //if (
                        //    lacreRep.SeqLacreRepositorio > 0 &&
                        //    //lacreRep.NumLacre != null &&
                        //    lacreRep.RepositorioListaControle != null &&
                        //    lacreRep.RepositorioListaControle.TipoRepositorioListaControle != null &&
                        //    !string.IsNullOrWhiteSpace(lacreRep.RepositorioListaControle.TipoRepositorioListaControle.DscTipoRepositorio) &&
                        //    !string.IsNullOrWhiteSpace(lacreRep.RepositorioListaControle.DscIdentificacao) &&
                        //    lacreRep.DataCadastro.HasValue &&
                        //    usuarioLogado != null &&
                        //    !string.IsNullOrWhiteSpace(usuarioLogado.NomeCompleto))
                        //{
                        //foreach (Entity.Usuario usuario in listGestores)
                        //{
                        //    if (!string.IsNullOrWhiteSpace(usuario.Email))
                        //    {
                                this.EnviarEmailParaGestorNotificandoSobreLacracaoComOcorrencia(
                                                                                    listGestores,
                                                                                    numLacre,
                                                                                    numCaixaIntubacao,
                                                                                    lacreRep.SeqLacreRepositorio,
                                                                                    lacreRep.RepositorioListaControle.TipoRepositorioListaControle.DscTipoRepositorio,
                                                                                    lacreRep.RepositorioListaControle.DscIdentificacao,
                                                                                    lacreRep.DataCadastro.Value,
                                                                                    usuarioLogado.NomeCompleto,
                                                                                    temItensComQtdDiferentes,
                                                                                    temItensComValidadeForaDoPeriodo,
                                                                                    ListaItensComQtdDiferente(),
                                                                                    ListaItensVencidos(),
                                                                                    ListaItensEmFalta(seqRepositorio));

                                // ExibirMensagem(TipoMensagem.Sucesso, usuario.Email);
                        //    }
                        //}
                        //}
                        //else { ExibirMensagem(TipoMensagem.Sucesso, "lacreRep.SeqLacreRepositorio = " + lacreRep.SeqLacreRepositorio +
                        //    "lacreRep.NumLacre=" + lacreRep.NumLacre + ("lacreRep.RepositorioListaControle=" + lacreRep.RepositorioListaControle != null ? "1":"0") +
                        //        "lacreRep.RepositorioListaControle.TipoRepositorioListaControle.DscTipoRepositorio=" + lacreRep.RepositorioListaControle.TipoRepositorioListaControle.DscTipoRepositorio +
                        //        "lacreRep.RepositorioListaControle.DscIdentificacao=" + lacreRep.RepositorioListaControle.DscIdentificacao + "usuarioLogado.NomeCompleto=" + usuarioLogado.NomeCompleto
                        //    ); }

                    }
                }

                #endregion

                ExibirMensagemEmOutraPagina(TipoMensagem.Sucesso, "Lacração realizada com sucesso !");


                Response.Redirect("RegistrarConferenciaDeLacre.aspx",false);

            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);

                ErrorSignal.FromCurrentContext().Raise(ex);
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
            catch (Exception err)
            {
                ErrorSignal.FromCurrentContext().Raise(err);
            }
        }

        protected void btnAdicionaOcorrencia_Click(object sender, EventArgs e)
        {
            try
            {

                Entity.LacreOcorrencia lacreOcorrencia = new Entity.LacreOcorrencia();

                lacreOcorrencia.LacreRepositorio = new Entity.LacreRepositorio();
                lacreOcorrencia.LacreRepositorio.SeqLacreRepositorio = seqLacreRepositorio;

                if (!string.IsNullOrWhiteSpace(this.txtDscOcorrencia.Text))
                    lacreOcorrencia.DscOcorrencia = this.txtDscOcorrencia.Text.ToUpper().ToString();

                lacreOcorrencia.DataCadastro = DateTime.Now;

                lacreOcorrencia.UsuarioCadastro = new Entity.Usuario();
                lacreOcorrencia.UsuarioCadastro.NumUserBanco = Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;

                new BLL.LacreOcorrencia().Adicionar(lacreOcorrencia);

                this.CarregarLacreOcorrencia();

                txtDscOcorrencia.Text = string.Empty;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "clear", "$('#txtDscOcorrencia').val('');", true);

                this.ExibirMensagem(TipoMensagem.Sucesso, "Ocorrência registrada com sucesso.");
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
                        List<Entity.LacreRepositorioEquipamento> listLacreEquip = new BLL.LacreRepositorioEquipamento().ObterPorLacreRepositorio(seqLacreRepositorio);

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
                        lacreRepEquipamento.LacreRepositorio.SeqLacreRepositorio = seqLacreRepositorio;

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
            catch (Exception err)
            {
                ErrorSignal.FromCurrentContext().Raise(err);
            }
        }

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
                    /*System.Web.UI.HtmlControls.HtmlTableRow trJustificativaSemAtendimento = e.Row.FindControl("trJustificativaSemAtendimento") as System.Web.UI.HtmlControls.HtmlTableRow;

                    if (trJustificativaSemAtendimento != null)
                    {
                        // Quanto não há atendimento, uma justificatica deverá ser informada para o lançamento de consumo.
                        if (this.seqAtendimento == 0)
                        {
                            trJustificativaSemAtendimento.Visible = true;
                        }
                    }*/

                    Entity.LacreRepositorioItens lacreRepItens = (Entity.LacreRepositorioItens)e.Row.DataItem;

                    //if (lacreRepItens.QtdDisponivel == 0)
                    //{
                    //    // Nota ao desenvolvedor : Quando um item estiver com a coluna "Qtd. Disponivel = 0 " pintar a linha do grid de vermelho (claro).
                    //    //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FC6D71"); // #339966  
                    //    e.Row.CssClass = "danger";
                    //}


                    Label lblValidade = (Label)e.Row.FindControl("lblValidade");

                    if (lacreRepItens.Lote.DataValidadeLote.Year > 1900)
                        lblValidade.Text = lacreRepItens.Lote.DataValidadeLote.ToString("dd/MM/yyyy");

                    Int32 numDiasPeriodo = BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMedicamento;
                    
                    if (lacreRepItens.QtdDisponivel > 0 && lacreRepItens.ItensListaControle.Alinea.IdfClasse == 2 && (
                        ((lacreRepItens.Lote.DataValidadeLote - DateTime.Now.Date).Days <= numDiasPeriodo)))
                    {
                        e.Row.Cells[8].Attributes.Add("data-toggle", "tooltip");
                        e.Row.Cells[8].Attributes.Add("title", "Item dentro do prazo de validade !");
                        e.Row.Cells[8].CssClass = "alert alert-warning exibirAlerta";
                    }
                    else if (lacreRepItens.QtdDisponivel > 0 && lacreRepItens.ItensListaControle.Alinea.IdfClasse != 2 && (
                        ((lacreRepItens.Lote.DataValidadeLote - DateTime.Now.Date).Days <= BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMaterial)))
                    {
                        e.Row.Cells[8].Attributes.Add("data-toggle", "tooltip");
                        e.Row.Cells[8].Attributes.Add("title", "Item dentro do prazo de validade !");
                        e.Row.Cells[8].CssClass = "alert alert-warning exibirAlerta";
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void grvItemInformarReposicao_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Int32 numDiasPeriodo = BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMedicamento;

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
                    else if (lacreRepItens.QtdDisponivel > 0 && lacreRepItens.ItensListaControle.Alinea.IdfClasse == 2 && (
                        ((lacreRepItens.Lote.DataValidadeLote - DateTime.Now.Date).Days <= numDiasPeriodo)))
                    {
                        e.Row.Cells[7].Attributes.Add("data-toggle", "tooltip");
                        e.Row.Cells[7].Attributes.Add("title", "Item dentro do prazo de validade !");
                        e.Row.Cells[7].CssClass = "alert alert-warning exibirAlerta";
                    }
                    else if (lacreRepItens.QtdDisponivel > 0 && lacreRepItens.ItensListaControle.Alinea.IdfClasse != 2 && (
                        ((lacreRepItens.Lote.DataValidadeLote - DateTime.Now.Date).Days <= BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMaterial)))
                    {
                        e.Row.Cells[7].Attributes.Add("data-toggle", "tooltip");
                        e.Row.Cells[7].Attributes.Add("title", "Item dentro do prazo de validade !");
                        e.Row.Cells[7].CssClass = "alert alert-warning exibirAlerta";
                    }

                    Label lblValidade = (Label)e.Row.FindControl("lblValidade");
                    if (lacreRepItens.Lote != null && lacreRepItens.Lote.DataValidadeLote.Year > 1900)
                    {
                        lblValidade.Text = lacreRepItens.Lote.DataValidadeLote.ToString("dd/MM/yyyy");
                        lblValidade.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
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
                this.AdicionarMaterialEmInformarReposicao();

                this.CarregarGridInformarReposicao();

                divLoteManual.Visible = false;

                divLoteAutomatico.Visible = true;
            }
            catch (Exception err)
            {
                ErrorSignal.FromCurrentContext().Raise(err);
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
                else
                {
                    divConsumoSemAtendimento.Visible = true;
                }

                this.CarregarRegistroConsumoSaida();
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);
            }

            uppPaciente.Update();
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
            List<Entity.LacreRepositorioItens> listLacreRepItem = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioItens().ObterPorLacreRepositorio(seqLacreRepositorio, null);

            if (listLacreRepItem == null || listLacreRepItem.Count == 0)
            {
                this.ExibirMensagem(TipoMensagem.Alerta, "Não há itens a serem visualizados.");
                return;
            }

            // Gerar um PDF para download !!!(Não precisa abrir o relatório em TELA);
            Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
            query.Url = "~/DownLoadArquivo.aspx";
            query.Parametros.Add("seqLacreRep", seqLacreRepositorio.ToString());
            query.Parametros.Add("seqAtendimento", this.seqAtendimento.ToString());
            query.Parametros.Add("tipo_arquivo", "REG_CONSUMO_SAIDA");
            Response.Redirect(query.ObterUrlCriptografada(), false);
        }

        protected void btnSalvarConsumoSaida_Click(object sender, EventArgs e)
        {
            try
            {
                this.GravarItensConsumoSaida();

                uppBuscaMaterial.Update();
            }
            catch (Exception err)
            {
                ErrorSignal.FromCurrentContext().Raise(err);
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
                        uppEquipamento.Update();
                    }
                    else if (this.hdfComando.Value.Contains("INFORMAR_TESTE_EQUIPAMENTO"))
                    {
                        this.InformarTesteDeEquipamento();
                        uppEquipamento.Update();
                    }
                    else if (this.hdfComando.Value == "EXCLUIR_EQUIPAMENTO")
                    {
                        this.InativarEquipamento();
                        uppEquipamento.Update();
                    }
                    else if (this.hdfComando.Value == "ATUALIZAR_GRID_ITENS_REPOSITORIO")
                    {
                        CarregarGridInformarReposicao();

                        UpdatePanel2.Update();
                    }
                    else if (this.hdfComando.Value == "ATUALIZAR_GRID_ITENS")
                    {
                        CarregarRegistroConsumoSaida();
                        UpdatePanel2.Update();
                    }
                    else if (this.hdfComando.Value == "ATUALIZAR_DADOS_LACRACAO")
                    {
                        ValidaLacracao();
                        uppLacracao.Update();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
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
                query.Parametros.Add("valor", this.txtNumPatrimonio.ClientID);
                query.Parametros.Add("codTipoPatrimonio", this.ddlTipoPatrimonio.SelectedValue);

                query.Parametros.Add("seqItemListaControle", hdnSeqTipoListaControle.Value);

                base.AbrirModal(query.ObterUrlCriptografada(), 740, 450);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnBuscarMaterial_Click(object sender, EventArgs e)
        {
            var material = new BLL.Material();

            var codMaterial = string.Empty;
            Int64 numLote = 0;

            bool achouMaterial = false;

            txtCodigoMaterial.Text = txtCodigoMaterial.Text.ToUpper();

            material.ObtemInsumos(txtCodigoMaterial.Text, out codMaterial, out numLote);

            if (numLote < 1)
            {
                ExibirMensagem(TipoMensagem.Alerta, "Este material não está codificado para consumo via código de barras. Informe manualmente.");
                txtCodigoMaterial.Text = string.Empty;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "highlight", string.Format("destacarGridConsumo('{0}');", codMaterial), true);
                return;
            }

            foreach (GridViewRow row in this.grvItemConsumoSaida.Rows)
            {
                var lblCodigoMaterial = (Label)row.FindControl("lblCodigoMaterial");

                if (lblCodigoMaterial.Text == codMaterial)
                {
                    var blLote = (HiddenField)row.FindControl("hdnLoteInsumoUnitario");

                    if (blLote.Value == numLote.ToString())
                    {
                        var txtUtilizada = (TextBox)row.FindControl("txtUtilizada");
                        txtUtilizada.Text = (Convert.ToDecimal(txtUtilizada.Text) + 1).ToString();
                        achouMaterial = true;

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "highlight", string.Format("destacarGridConsumo('{0}');", codMaterial), true);
                    }
                }
            }

            if (!achouMaterial)
            {
                ExibirMensagem(TipoMensagem.Alerta, "Este material não esta no repositório.");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "manterScrool", string.Format("manterScrool('{0}');", txtCodigoMaterial.ID), true);
            }

            //ExibirMensagem(TipoMensagem.Sucesso, "Item registrado com sucesso. Cod.:" + txtCodigoMaterial.Text,2000);
            txtCodigoMaterial.Text = string.Empty;


            txtCodigoMaterial.Focus();

        }

        protected void btnBuscaMaterialRep_Click(object sender, EventArgs e)
        {
            var material = new BLL.Material();

            var codMaterial = string.Empty;
            Int64 numLote = 0;

            txtCodMaterial.Text = txtCodMaterial.Text.ToUpper();

            material.ObtemInsumos(txtCodMaterial.Text, out codMaterial, out numLote);

            txtCodMaterial.Text = codMaterial;

            if (!string.IsNullOrWhiteSpace(txtCodMaterial.Text))
            {
                if (this.CarregarMaterialComCodigoPorCodigoMaterial())
                {
                    if (numLote > 0)
                    {
                        ddlLoteMatComCodigo.SelectedValue = numLote.ToString();

                        txtQtdMat.Text = "1";

                        var itensReposicao = new Hcrp.CarroUrgenciaPsicoativo.BLL.
                            LacreRepositorioItens().ObterPorLacreRepositorio(seqLacreRepositorio, null);

                        var materialRepositorio = itensReposicao.FindAll(x => x.Material.Codigo == codMaterial);

                        // Material já existe, só atualizar
                        if (materialRepositorio.Count > 0)
                        {
                            var totalNecessario = materialRepositorio.First().ItensListaControle.QuantidadeNecessaria;
                            var totalDisponivel = materialRepositorio.Sum(x => x.QtdDisponivel);

                            /*if (totalDisponivel == 0)
                            {
                                this.ExibirMensagem(TipoMensagem.Alerta, "Não é possível inserir o material pois ultrapassará a quantidade disponível. ");
                                return;
                            }*/

                            if (totalNecessario < (totalDisponivel + 1))
                            {
                                this.ExibirMensagem(TipoMensagem.Alerta,
                                                    "Não é possível inserir o item pois ultrapassará a quantidade necessária. ");

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "highlightReposicao",
                                                                        string.Format("destacarGridReposicao('{0}');",
                                                                                      codMaterial), true);

                                this.txtCodMaterial.Text = string.Empty;
                                this.txtNomeMaterialComCodigo.Text = string.Empty;
                                this.hdfSeqItemListaControle.Value = string.Empty;
                                this.txtUnidMedMaterialComCodigo.Text = string.Empty;
                                this.txtQtdMat.Text = string.Empty;

                                ddlLoteMatComCodigo.Items.Clear();

                                //txtCodMaterial.Focus();

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "foco1",
                                                                        " setTimeout(function(){ $('#txtCodMaterial').focus();}, 100); ",
                                                                        true);
                                return;
                            }

                            var itemRepositorio = materialRepositorio.Find(x => x.Lote.NumLote == numLote);

                            if (itemRepositorio == null)
                            {
                                AdicionarMaterialEmInformarReposicao();
                            }
                            else
                            {
                                itemRepositorio.QtdDisponivel = ++itemRepositorio.QtdDisponivelInserida;


                                new BLL.LacreRepositorioItens().AtualizarQuantidadeUtilizada(itemRepositorio);

                                this.txtCodMaterial.Text = string.Empty;
                                this.txtNomeMaterialComCodigo.Text = string.Empty;
                                this.hdfSeqItemListaControle.Value = string.Empty;
                                this.txtUnidMedMaterialComCodigo.Text = string.Empty;
                                this.txtQtdMat.Text = string.Empty;

                                ddlLoteMatComCodigo.Items.Clear();
                            }
                        }
                        else
                        {
                            // material não existe, inclui um novo
                            AdicionarMaterialEmInformarReposicao();
                        }

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "highlightReposicao",
                                                                string.Format("destacarGridReposicao('{0}');",
                                                                              codMaterial), true);
                    }

                    CarregarGridInformarReposicao();
                }
            }
            else
            {
                ExibirMensagem(TipoMensagem.Alerta, "Digite o código material corretamente/leitura errada do código de barras !");
            }
            //txtCodMaterial.Text = "";
            //txtCodMaterial.Focus();
            //uppInformarReposicao.Update();

            ScriptManager.RegisterClientScriptBlock(this, typeof(string), "teste", "setTimeout(function(){ $('#txtCodMaterial').focus();}, 200);", true);
            //txtCodMaterial.Focus();
        }

        protected void btnRequisicao_Click(object sender, EventArgs e)
        {
            try
            {
                //this.hdfComando.Value = "BUSCA_PATRIMONIO_EQUIPAMENTO";

                Hcrp.Infra.Util.QueryStringSegura query = new Hcrp.Infra.Util.QueryStringSegura();
                query.Url = Page.ResolveUrl("~/TrocaMaterial/RequisitarMaterial.aspx");
                query.Parametros.Add("seqRepositorio", seqRepositorio.ToString());
                //query.Parametros.Add("valor", this.txtNumPatrimonio.ClientID);
                //query.Parametros.Add("codTipoPatrimonio", this.ddlTipoPatrimonio.SelectedValue);



                base.AbrirModal(query.ObterUrlCriptografada(), 740, 450);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void lblInformelote_Click(object sender, EventArgs e)
        {
            if (divLoteManual.Visible)
            {
                divLoteManual.Visible = false;
                divLoteAutomatico.Visible = true;
            }
            else
            {
                divLoteManual.Visible = true;
                divLoteAutomatico.Visible = false;
            }

            //$("#divLoteManual").toggleClass("hidden");

            //        $("#divLoteAutomatico").toggleClass("hidden");

            //        if ($("#divLoteManual").hasClass("hidden")) {
            //            $("#lblInformelote").text("Clique para informar o lote manualmente");
            //        } else {
            //            $("#lblInformelote").text("Clique aqui para selecionar um lote");
            //        }
        }

        protected void btnInserirComplemento_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescricaoComplemento.Text))
            {
                this.ExibirMensagem(TipoMensagem.Alerta, "Digite a descrição do lacre");
                txtDescricaoComplemento.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNumComplemento.Text))
            {
                this.ExibirMensagem(TipoMensagem.Alerta, "Digite o número do lacre.");
                txtNumComplemento.Focus();
                return;
            }

            Entity.Lacre_reposit_complemento item = new Entity.Lacre_reposit_complemento();

            item.DSC_COMPLEMENTO = txtDescricaoComplemento.Text.ToUpper().Trim();

            item.NUM_LACRE = txtNumComplemento.Text.ToUpper().Trim();

            item.SEQ_LACRE_REPOSITORIO = seqLacreRepositorio;

            item.NUM_USER_CADASTRO = NumeroUsuarioBancoLogado;

            BLL.LacreRepositorioComplemento bll = new LacreRepositorioComplemento();

            bll.Adicionar(item);

            CarregarLacresComplementos();

            txtNumComplemento.Text = string.Empty;
            txtDescricaoComplemento.Text = string.Empty;
        }

        protected void grvLacreComplemento_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Int64 seq = Convert.ToInt64(e.CommandArgument);

                if (e.CommandName == "EXCLUIR")
                {
                    this.ExcluirLacreComplemento(seq);

                    this.CarregarLacresComplementos();

                    this.ExibirMensagem(TipoMensagem.Sucesso, "Item excluído com sucesso.");
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
        protected void EnviarEmailParaGestorNotificandoSobreLacracaoComOcorrencia(List<Entity.Usuario> usuarios , Int64 numLacre, string numCaicaIntubacao, Int64 seqLacreRepositorio, string dscTipoRepositorio,
                                                                                                string repositorioIdentificacao, DateTime dataCadastroCarro, string nomeusuarioResponsavel,
                                                                                                bool temQtdDiferentes, bool temValidadeForaPeriodo, List<Entity.LacreRepositorioItens> itensComQtdDiferente,
                                                                                                List<Entity.LacreRepositorioItens> itensVencidos, List<Entity.ItensListaControle> itensEmFalta)
        {
            //try
            //{
            StringBuilder corpo = new StringBuilder();
            corpo.AppendLine("Lacração com ocorrências.");
            corpo.AppendLine("<br /> ");
            corpo.AppendLine("<br /> ");

            if (temQtdDiferentes == true && temValidadeForaPeriodo == true)
                corpo.AppendLine("Lacração com quantidade necessárias diferentes das quantidades disponíveis e itens com validade à vencer.");
            else if (temQtdDiferentes == true && temValidadeForaPeriodo == false)
                corpo.AppendLine("Lacração com quantidade necessárias diferentes das quantidades disponíveis.");
            else if (temQtdDiferentes == false && temValidadeForaPeriodo == true)
                corpo.AppendLine("Itens com validade à vencer.");

            corpo.AppendLine("<br /> ");
            corpo.AppendLine("<br /> ");
            corpo.AppendLine("Justificativa do usuário:");

            if (!string.IsNullOrWhiteSpace(this.txtItensVencerQuantidadeDiferente.Text))
            {
                corpo.AppendLine("<br />");
                corpo.AppendLine(this.txtItensVencerQuantidadeDiferente.Text);
            }

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
            corpo.AppendLine("Número lacre carro: ");
            corpo.AppendLine(numLacre.ToString());

            if (numCaicaIntubacao != null)
            {
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("Número lacre caixa de intubação: ");
                corpo.AppendLine(numCaicaIntubacao);
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
            corpo.AppendLine("Responsável pela lacração: ");
            corpo.AppendLine(nomeusuarioResponsavel);

            #region Tabela de itens com qtd. diferente no repositório
            
            if (itensComQtdDiferente.Count > 0)
            {
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("<br /> ");
                
                //Header
                corpo.Append("<table border=\"1\">");
                corpo.Append("<tr>");
                corpo.Append("<td colspan=\"4\">");
                corpo.AppendLine("<b>Itens com qtd. diferente no repositório</b>");
                corpo.Append("</td>");
                corpo.Append("</tr>");
                corpo.Append("<tr>");
                corpo.Append("<td>");
                corpo.Append("<b>Código</b>");
                corpo.Append("</td>");
                corpo.Append("<td>");
                corpo.Append("<b>Item</b>");
                corpo.Append("</td>");
                corpo.Append("<td>");
                corpo.Append("<b>Qtd. necessária</b>");
                corpo.Append("</td>");
                corpo.Append("<td>");
                corpo.Append("<b>Qtd. disponível</b>");
                corpo.Append("</td>");
                corpo.Append("</tr>"); 

                foreach (var item in itensComQtdDiferente)
                {
                    corpo.Append("<tr>");
                    corpo.Append("<td>");
                    corpo.Append(item.Material.Codigo);
                    corpo.Append("</td>");
                    corpo.Append("<td>");
                    corpo.Append(item.Material.Nome);
                    corpo.Append("</td>");
                    corpo.Append("<td>");
                    corpo.Append((item.ItensListaControle.QuantidadeNecessaria > 0 ? (item.ItensListaControle.QuantidadeNecessaria + " / " + item.ItensListaControle.Unidade.Nome) : ""));
                    corpo.Append("</td>");
                    corpo.Append("<td>");
                    corpo.Append((item.QtdDisponivel > 0 ? (item.QtdDisponivel + " / " + item.ItensListaControle.Unidade.Nome) : ""));
                    corpo.Append("</td>");
                    corpo.Append("</tr>"); 
                }
                
                corpo.Append("</table>");
            }

            #endregion

            #region Tabela de itens dentro do prazo de validade no repositório

            if (itensVencidos.Count > 0)
            {
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("<br /> ");

                //Header
                corpo.Append("<table border=\"1\">");
                corpo.Append("<tr>");
                corpo.Append("<td colspan=\"3\">");
                corpo.AppendLine("<b>Itens dentro do prazo de validade no repositório</b>");
                corpo.Append("</td>");
                corpo.Append("</tr>");
                corpo.Append("<tr>");
                corpo.Append("<td>");
                corpo.Append("<b>Código</b>");
                corpo.Append("</td>");
                corpo.Append("<td>");
                corpo.Append("<b>Item</b>");
                corpo.Append("</td>");
                corpo.Append("<td>");
                corpo.Append("<b>Data de vencimento</b>");
                corpo.Append("</td>");
                corpo.Append("</tr>");

                foreach (var item in itensVencidos)
                {
                    corpo.Append("<tr>");
                    corpo.Append("<td>");
                    corpo.Append(item.Material.Codigo);
                    corpo.Append("</td>");
                    corpo.Append("<td>");
                    corpo.Append(item.Material.Nome);
                    corpo.Append("</td>");
                    corpo.Append("<td>");
                    corpo.Append(item.Lote.DataValidadeLote.ToString("dd/MM/yyyy"));
                    corpo.Append("</td>");
                    corpo.Append("</tr>");
                }

                corpo.Append("</table>");
            }

            #endregion

            #region Tabela de itens em falta no repositório

            if (itensEmFalta.Count > 0)
            {
                corpo.AppendLine("<br /> ");
                corpo.AppendLine("<br /> ");

                //Header
                corpo.Append("<table border=\"1\">");
                corpo.Append("<tr>");
                corpo.Append("<td colspan=\"3\">");
                corpo.AppendLine("<b>Itens em falta no repositório</b>");
                corpo.Append("</td>");
                corpo.Append("</tr>");
                corpo.Append("<tr>");
                corpo.Append("<td>");
                corpo.Append("<b>Código</b>");
                corpo.Append("</td>");
                corpo.Append("<td>");
                corpo.Append("<b>Item</b>");
                corpo.Append("</td>");
                corpo.Append("<td>");
                corpo.Append("<b>Qtd. necessária</b>");
                corpo.Append("</td>");
                corpo.Append("</tr>");

                foreach (var item in itensEmFalta)
                {
                    corpo.Append("<tr>");
                    corpo.Append("<td>");
                    corpo.Append(item.Material.Codigo);
                    corpo.Append("</td>");
                    corpo.Append("<td>");
                    corpo.Append(item.Material.Nome);
                    corpo.Append("</td>");
                    corpo.Append("<td>");
                    corpo.Append(item.QuantidadeNecessaria + " / " + item.Unidade.Nome);
                    corpo.Append("</td>");
                    corpo.Append("</tr>");
                }

                corpo.Append("</table>");
            }

            #endregion

            //try
            //{
            new BLL.Email().Enviar(corpo.ToString(), usuarios, "Ocorrência de lacração carro emergência");
            //}
            //catch
            //{
            //}
            //Literal1.Text = email + " <br>"+ corpo.ToString();
            //}
            //catch //()
            //{
            //    //ExibirMensagem(TipoMensagem.Erro, ex.Message + " <br/>" + ex.InnerException);
            //}
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
                    //this.txtNumCaixaIntubacao.Enabled = false;
                    divInfoPodeLacrar.Visible = true;

                    // Somente enfermagem pode requisitar material
                    btnRequisicao.Visible = false;
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
                this.rptLacreOcorrenciaRegistradas.DataSource = new BLL.LacreOcorrencia().ObterPorSeqLacreRepositorio(seqLacreRepositorio);
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

                            this.ExibirMensagem(TipoMensagem.Sucesso, "Teste de equipamento informado com sucesso.");

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
                this.grvItemRepLacreEquipamento.DataSource = new BLL.LacreRepositorioEquipamento().ObterPorLacreRepositorio(seqLacreRepositorio);
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
                    this.ddlItemMatSemCodigo.DataSource = new Hcrp.CarroUrgenciaPsicoativo.BLL.ItensListaControle().ObterListaDeListaControleItemComMaterialSemCodigo(seqLacreRepositorio);
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
        protected bool CarregarMaterialComCodigoPorCodigoMaterial()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(this.txtCodMaterial.Text))
                {
                    this.txtCodMaterial.Text = this.txtCodMaterial.Text.ToUpper();

                    this.txtNomeMaterialComCodigo.Text = string.Empty;
                    this.txtUnidMedMaterialComCodigo.Text = string.Empty;
                    this.hdfSeqItemListaControle.Value = string.Empty;

                    List<Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle> listItemListaControle = 
                        new Hcrp.CarroUrgenciaPsicoativo.BLL.ItensListaControle().ObterListaDeListaControleItemComMaterialComCodigo(seqLacreRepositorio);

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
                        else
                        {
                            ExibirMensagem(TipoMensagem.Alerta, "Este material não está configurado na lista do repositório");
                            return false;
                        }
                    }
                    else
                    {
                        ExibirMensagem(TipoMensagem.Alerta, "Não foi possível obter os itens do repositório");
                        return false;
                    }
                }
                return true;
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
                    pnMaterialComCodigo.Visible = true;
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
        public void CarregarGridInformarReposicao()
        {
            try
            {
                this.grvItemInformarReposicao.DataSource = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioItens().ObterPorLacreRepositorio(seqLacreRepositorio, null);
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
                Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens lacreRepItens =
                    new Entity.LacreRepositorioItens();

                lacreRepItens.LacreRepositorio = new Entity.LacreRepositorio();
                lacreRepItens.LacreRepositorio.SeqLacreRepositorio = seqLacreRepositorio;

                lacreRepItens.Material = new Entity.Material();
                lacreRepItens.Lote = new Entity.Lote();

                lacreRepItens.ItensListaControle = new Entity.ItensListaControle();

                lacreRepItens.UsuarioCadastro = new Entity.Usuario();

                lacreRepItens.UsuarioCadastro.NumUserBanco =
                    Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;


                if (!string.IsNullOrWhiteSpace(txtQtdMat.Text))
                {
                    lacreRepItens.QtdDisponivel = Convert.ToInt32(this.txtQtdMat.Text);
                }
                else
                {
                    this.ExibirMensagem(TipoMensagem.Alerta,
                                            "Digite uma quantidade.");
                    return;
                }

                // Verificar se o material tem código HC ou não.
                if (pnMaterialComCodigo.Visible)
                {
                    if (!string.IsNullOrWhiteSpace(this.txtCodMaterial.Text))
                        lacreRepItens.Material.Codigo = this.txtCodMaterial.Text;
                    else
                    {
                        this.ExibirMensagem(TipoMensagem.Alerta,
                                            "Digite um código material.");
                        return;
                    }

                    DateTime dataValidadeLoteManual = new DateTime();

                    if (divLoteManual.Visible)
                    {
                        //if (string.IsNullOrWhiteSpace(txtLoteManual.Text))
                        //{
                        //    this.ExibirMensagem(TipoMensagem.Alerta,
                        //                    "Digite um lote para o item.");
                        //    return;
                        //}

                        if (string.IsNullOrWhiteSpace(txtValidadeManual.Text))
                        {
                            this.ExibirMensagem(TipoMensagem.Alerta,
                                            "Digite uma validade para o item.");
                            return;
                        }

                        if (!DateTime.TryParse(txtValidadeManual.Text, out dataValidadeLoteManual))
                        {
                            this.ExibirMensagem(TipoMensagem.Alerta,
                                            "Digite uma data válida.");
                            return;
                        }
                    }

                    // Lote selecionado pela dropdownlist divLoteManual escondido
                    if (!divLoteManual.Visible)
                    {
                        if (this.ddlLoteMatComCodigo.SelectedIndex > 0)
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
                        else
                        {
                            this.ExibirMensagem(TipoMensagem.Alerta,
                                                "Não foi possível localizar o lote para este material.");
                            return;
                        }
                    }
                    else
                    {
                        lacreRepItens.NumLoteFabricante = txtLoteManual.Text.ToUpper().Trim();
                        lacreRepItens.DataValidadeLote = dataValidadeLoteManual;
                    }

                    // Obter a seq itens lista controle pelo código do material.
                    Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle itemListaControle =
                        new Hcrp.CarroUrgenciaPsicoativo.BLL.ItensListaControle().
                            ObterItensPorCodMaterialESeqLacreRespositorio(seqLacreRepositorio,
                                                                            lacreRepItens.Material.Codigo);

                    if (!string.IsNullOrWhiteSpace(this.hdfSeqItemListaControle.Value))
                    {
                        lacreRepItens.ItensListaControle.SeqItensListaControle =
                            itemListaControle.SeqItensListaControle;
                    }
                    else
                    {
                        this.ExibirMensagem(TipoMensagem.Alerta,
                                            "Não foi possível localizar o item da lista de controle.");
                        return;
                    }
                }
                else
                {
                    // Material sem código.

                    if (this.ddlItemMatSemCodigo.SelectedValue != "0")
                        lacreRepItens.ItensListaControle.SeqItensListaControle =
                            Convert.ToInt64(this.ddlItemMatSemCodigo.SelectedValue);

                    if (string.IsNullOrWhiteSpace(txtValidadeItemSemCodigo.Text))
                    {
                        this.ExibirMensagem(TipoMensagem.Alerta,
                                        "Digite uma validade para o item.");
                        return;
                    }

                    DateTime dataValidadeItemSemcodigo;
                    if (!DateTime.TryParse(txtValidadeItemSemCodigo.Text, out dataValidadeItemSemcodigo))
                    {
                        this.ExibirMensagem(TipoMensagem.Alerta,
                                        "Digite uma data válida.");
                        return;
                    }

                    lacreRepItens.Material.Nome = ddlItemMatSemCodigo.SelectedItem.Text;
                    lacreRepItens.DataValidadeLote = dataValidadeItemSemcodigo;
                }

                // Verificar a quantidade de itens.
                // Não deixar o usuário colocar mais item do que o permitido na configuração da lista(ITENS_LISTA_CONTROLE.QTD_NECESSARIA).
                if (lacreRepItens.ItensListaControle != null &&
                    lacreRepItens.ItensListaControle.SeqItensListaControle > 0)
                {
                    Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle itemListControleAux =
                        new Hcrp.CarroUrgenciaPsicoativo.BLL.ItensListaControle().ObterItensPorId(
                            lacreRepItens.ItensListaControle.SeqItensListaControle.Value);

                    if (lacreRepItens.QtdDisponivel > itemListControleAux.QuantidadeNecessaria)
                    {
                        this.ExibirMensagem(TipoMensagem.Alerta,
                                            "Quantidade informada não pode ser maior que a quantidade necessária (" +
                                            itemListControleAux.QuantidadeNecessaria + "). ");
                        return;
                    }
                }

                #region Comentado
                // Verificar se o item já foi adicionado.    
                //Int64? numLoteAux = null;

                //if (lacreRepItens.Lote != null && lacreRepItens.Lote.NumLote > 0)
                //    numLoteAux = lacreRepItens.Lote.NumLote;

                //if (new BLL.LacreRepositorioItens().VerificarSeOMaterialJahFoiAdicionadoParaLacreRepositorioItens(this.seqLacreRepositorio, lacreRepItens.ItensListaControle.SeqItensListaControle.Value, numLoteAux) == true)
                //{
                //    this.ExibirMensagem(TipoMensagem.Alerta, "Material já adicionado, informe outro. ");
                //    return;
                //}
                #endregion

                var itensReposicao = new Hcrp.CarroUrgenciaPsicoativo.BLL.
                    LacreRepositorioItens().ObterPorLacreRepositorio(seqLacreRepositorio, null);

                var materialRepositorio =
                    itensReposicao.FindAll(x => (lacreRepItens.Material.Codigo == null ? 
                        (x.Material.Nome.ToUpper() == lacreRepItens.Material.Nome.ToUpper()) : 
                        (x.Material.Codigo == lacreRepItens.Material.Codigo)));

                // Material já existe, só atualizar
                if (materialRepositorio.Count > 0)
                {
                    var totalNecessario = materialRepositorio.First().ItensListaControle.QuantidadeNecessaria;
                    var totalDisponivel = materialRepositorio.Sum(x => x.QtdDisponivel);

                    if (totalNecessario < (totalDisponivel + lacreRepItens.QtdDisponivel))
                    {
                        this.ExibirMensagem(TipoMensagem.Alerta,
                                            "Não é possível inserir o material pois ultrapassará a quantidade necessária. ");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "highlightReposicao",
                                                                string.Format("destacarGridReposicao('{0}');",
                                                                                this.txtCodMaterial.Text), true);

                        return;
                    }
                }

                Entity.LacreRepositorioItens itemRepositorio = new Entity.LacreRepositorioItens();

                if (divLoteManual.Visible)
                {
                    itemRepositorio =
                    materialRepositorio.Find(
                        x => x.Lote.NumLoteFabricante == lacreRepItens.NumLoteFabricante && x.Lote.DataValidadeLote == lacreRepItens.DataValidadeLote);
                }
                else if (!chkMatComSemCodigo.Checked)
                {
                    itemRepositorio =
                    materialRepositorio.Find(
                        x => x.Lote.NumLote == Convert.ToInt64(ddlLoteMatComCodigo.SelectedValue));
                }
                else
                    itemRepositorio = null; // Item sem codigo material


                if (itemRepositorio != null)
                {
                    itemRepositorio.QtdDisponivelInserida += lacreRepItens.QtdDisponivel;
                    new BLL.LacreRepositorioItens().AtualizarQuantidadeUtilizada(itemRepositorio);
                }
                else
                {
                    // Adicionar
                    new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioItens().Adicionar(lacreRepItens);
                }

                // Limpar campos.
                this.txtNomeMaterialComCodigo.Text = string.Empty;
                this.hdfSeqItemListaControle.Value = string.Empty;
                this.txtUnidMedMaterialComCodigo.Text = string.Empty;
                this.txtQtdMat.Text = string.Empty;
                txtValidadeItemSemCodigo.Text = string.Empty;

                //if (this.ddlLoteMatComCodigo.Items.Contains(this.ddlLoteMatComCodigo.Items.FindByValue("0")))
                //    this.ddlLoteMatComCodigo.SelectedValue = "0";

                ddlLoteMatComCodigo.Items.Clear();

                if (this.ddlItemMatSemCodigo.Items.Contains(this.ddlItemMatSemCodigo.Items.FindByValue("0")))
                    this.ddlItemMatSemCodigo.SelectedValue = "0";

                this.ExibirMensagem(TipoMensagem.Sucesso, "Item adicionado com sucesso.");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "highlightReposicao",
                                                        string.Format("destacarGridReposicao('{0}');",
                                                                        txtCodMaterial.Text.Trim().ToUpper()), true);

                this.txtCodMaterial.Text = string.Empty;

                txtLoteManual.Text = string.Empty;
                txtValidadeManual.Text = string.Empty;

            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, ex.Message);

                ErrorSignal.FromCurrentContext().Raise(ex);
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

                this.seqAtendimento = new Hcrp.CarroUrgenciaPsicoativo.BLL.AtendimentoPaciente().ObterAtendimentoDoPacienteParaAData(codPaciente, DateTime.Now);

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
                HiddenField hdnQtdUtilizada = null;
                Label lblQtdDisponivel = null;
                //TextBox txtJustificativa = null;

                string resultadoValidacao = string.Empty;
                int qtdUtilizada = 0;
                int qtdDisponivel = 0;
                int qtdUtilizadaComAtendimento = 0;
                Int64 seqLacreRepItens = 0;
                List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens> listLacreRepItens = new List<Entity.LacreRepositorioItens>();
                Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens lacreRepItens = null;

                // Obter as quantidades dísponíveis novamente para saber se ainda continuam com as mesmas quantidades.
                List<Entity.QuantidadeRegistroConsumoSaida> listQtdDisponivelComAtendimento =
                    new BLL.LacreRepositorioItens().ObterQuantidadeDisponivelParaConsumoSaida(seqLacreRepositorio, this.seqAtendimento);

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
                    //txtJustificativa = (TextBox)item.FindControl("txtJustificativa");

                    // Recupero o valor do hidden pra saber se o usuário esta alterando a quantidade utilizada
                    hdnQtdUtilizada = (HiddenField)item.FindControl("hdnQtdUtilizada");

                    // Obter chave.
                    seqLacreRepItens = Convert.ToInt64(hdfSeqLacreRepositorioItens.Value);

                    // Obter q qtd disponivel com atendimento para o item.
                    qtdDisponivel = 0;
                    qtdUtilizadaComAtendimento = 0;

                    if (Int32.TryParse(txtUtilizada.Text, out qtdUtilizada) == false)
                    {
                        resultadoValidacao =
                            string.Format("O item {0} {1} - {2} {3} está com a quantidade utilizada inválida.",
                                          lblAlinea.Text, lblCodigoMaterial.Text, lblNomeMaterial.Text, lblLote.Text);

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "highlight",
                                                                string.Format("destacarGridConsumo('{0}');",
                                                                              seqLacreRepItens), true);

                        break;
                    }


                    if (Convert.ToInt32(hdnQtdUtilizada.Value) != qtdUtilizada)
                    {
                        var qtdRegConsumoSaida = (from qtdRegConsumoSaidaL in listQtdDisponivelComAtendimento
                                                  where qtdRegConsumoSaidaL.SeqLacreRepositorioItens == seqLacreRepItens
                                                  select qtdRegConsumoSaidaL).FirstOrDefault();
                        if (qtdRegConsumoSaida != null)
                        {
                            qtdDisponivel = qtdRegConsumoSaida.QtdDisponivelComTodoAtendimento;
                            qtdUtilizadaComAtendimento = qtdRegConsumoSaida.QtdUtilizadaComAtendimento;
                        }


                        if ((qtdUtilizada > (qtdDisponivel - qtdUtilizadaComAtendimento)) && !(qtdUtilizada < Convert.ToInt32(hdnQtdUtilizada.Value)))
                        // se a quantidade for zero, pode ser que a pessoa informou item errado no consumo!!
                        {
                            // Se a quantidade utilizada for maior que zero, então já foi utilizada deste item, devolve o que foi consumido para fazer a comparação.                        

                            // Não deixar o usuário informar a coluna "Qtd. utilizada" maior que a coluna "Qtd. Disponivel".                        
                            resultadoValidacao =
                                string.Format(
                                    "O item {0} {1} - {2} {3} está com a quantidade utilizada maior que a quantidade disponível. </br>",
                                    lblAlinea.Text, lblCodigoMaterial.Text, lblNomeMaterial.Text, lblLote.Text);

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "highlight",
                                                                    string.Format("destacarGridConsumo('{0}');",
                                                                                  lblCodigoMaterial.Text), true);

                            break;
                        }
                        else
                        {
                            // Armazena na lista para depois mandar para o banco.
                            lacreRepItens = new Entity.LacreRepositorioItens();
                            lacreRepItens.SeqLacreRepositorioItens = seqLacreRepItens;
                            lacreRepItens.QtdUtilizada = Convert.ToInt32(txtUtilizada.Text);

                            /*if (!string.IsNullOrWhiteSpace(txtJustificativa.Text))
                            lacreRepItens.DscJustificativaConsumoSemAtendimento = txtJustificativa.Text.ToUpper().Trim();*/

                            //lacreRepItens.Lote = new Entity.Lote();
                            //DateTime.TryParse(txtValidade.Text, out auxData);
                            //if (auxData.Year > 1900)
                            //    lacreRepItens.Lote.DataValidadeLote = auxData;

                            listLacreRepItens.Add(lacreRepItens);
                        }
                    }
                }

                if (seqAtendimento == 0 && txtJustificativaConsumoSemAtendimento.Text.Length == 0)
                {
                    // Não deixar o usuário informar um texto maior que 200 caracteres.                      
                    resultadoValidacao = resultadoValidacao + string.Format("É necessário informar uma justificativa para a utilização dos itens sem atendimento.");
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

                    if (seqAtendimento == 0)
                    {
                        listLacreRepItens.ForEach(x => x.DscJustificativaConsumoSemAtendimento = txtJustificativaConsumoSemAtendimento.Text);
                    }

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
            catch (Exception err)
            {
                ErrorSignal.FromCurrentContext().Raise(err);
            }
        }

        /// <summary>
        /// Carregar materiais para registro de consumo/saida.
        /// </summary>
        protected void CarregarRegistroConsumoSaida()
        {
            try
            {
                if (seqLacreRepositorio > 0)
                {
                    Int64? seqAtendimentoP = null;

                    if (this.seqAtendimento > 0)
                        seqAtendimentoP = this.seqAtendimento;

                    List<Entity.LacreRepositorioItens> list =
                        new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioItens().ObterPorLacreRepositorio(
                            seqLacreRepositorio, seqAtendimentoP);

                    this.grvItemConsumoSaida.DataSource = list;

                    this.grvItemConsumoSaida.DataBind();

                    // Se touxe registros e não tem atendimento do paciente, informar usuário.
                    if (grvItemConsumoSaida.Rows.Count > 0 && this.seqAtendimento == 0)
                    {
                        //this.lblMsgOperacaoConsumoSaida.Text = "Consumo de itens sem atendimento do paciente, informe um paciente em atendimento, caso contrário uma justificativa deveré ser informada para o consumo.";
                        this.lblMsgOperacaoConsumoSaida.Text = "Informe um paciente em atendimento, " +
                                                               "caso contrário uma justificativa deveré ser informada para o consumo !";

                        txtJustificativaConsumoSemAtendimento.Text = list[0].DscJustificativaConsumoSemAtendimento;

                        uppBuscaMaterial.Update();
                    }

                    uppBuscaMaterial.Update();
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

                    divConsumoSemAtendimento.Visible = false;
                }
                else
                    divConsumoSemAtendimento.Visible = true;
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

                List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio> listLacreRep = new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorio().
                    ObterPorFiltro(seqLacreRepositorio,
                                                    null,
                                                    null,
                                                    null);

                if (listLacreRep != null &&
                    listLacreRep.Count > 0)
                {
                    lacreRep = listLacreRep[0];

                    seqRepositorio = lacreRep.RepositorioListaControle.SeqRepositorio;

                    hdnSeqRepositorio.Value = seqRepositorio.ToString();

                    if (lacreRep.RepositorioListaControle != null &&
                        lacreRep.RepositorioListaControle.ListaControle != null &&
                        lacreRep.RepositorioListaControle.ListaControle.Instituto != null &&
                        !string.IsNullOrWhiteSpace(lacreRep.RepositorioListaControle.ListaControle.Instituto.NomeInstituto))
                    {
                        this.lblInstituto.Text = lacreRep.RepositorioListaControle.ListaControle.Instituto.NomeInstituto;

                        //if (lacreRep.RepositorioListaControle.ListaControle.IdfCaixaIntubacao == 0)
                        //{
                        //    divCaixaIntubacao.Visible = false;
                        //}
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
                        hdnSeqTipoListaControle.Value = lacreRep.RepositorioListaControle.ListaControle.SeqListaControle.ToString();
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

                CarregarLacresComplementos();

                // Forçar a tabindex para 0
                //this.TabContainerAba.ActiveTabIndex = 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ValidaLacracao()
        {
            try
            {
                // Obter os itens do repostório.
                List<Entity.LacreRepositorioItens> listRepItens =
                    new BLL.LacreRepositorioItens().ObterPorLacreRepositorio(seqLacreRepositorio, null);

                if (listRepItens == null || listRepItens.Count == 0)
                {
                    this.ExibirMensagem(TipoMensagem.Alerta,
                                        "Não foram encontrados os itens do repositório para realizar a lacração.");
                    return;
                }

                List<Entity.ItensListaControle> itens = new List<ItensListaControle>();

                BLL.ItensListaControle bllItensListaControle = new BLL.ItensListaControle();

                itens = bllItensListaControle.ObterItensEmFaltaNoRepositorio(Convert.ToInt64(hdnSeqRepositorio.Value));

                if (itens.Count == 0)
                {
                    divItensFaltantes.Visible = false;
                }
                else
                    lblContadorItensFaltantes.InnerText = itens.Count.ToString();

                #region validar qtd necessário com qtd disponível

                var queryLastNames =
                    (from student in listRepItens
                     group student by new {student.Material.Codigo,
                        student.Material.Nome
                     } into newGroup
                         //orderby newGroup.Key
                         select new Entity.LacreRepositorioItens()
                                    {
                                        Material = new Material()
                                                       {
                                                           Codigo = Convert.ToString(newGroup.Key)
                                                       },
                                        ItensListaControle = new ItensListaControle()
                                                                 {
                                                                     QuantidadeNecessaria =
                                                                         newGroup.Max(
                                                                             x => x.ItensListaControle.QuantidadeNecessaria)
                                                                 },
                                        QtdDisponivel = newGroup.Sum(x => x.QtdDisponivel)
                                    }).ToList();

                //foreach (var nameGroup in queryLastNames)
                //{
                //    t =  (from item in listRepItens
                //        where item.Material.Codigo == nameGroup.Key
                //                select item.QtdDisponivel).Sum();
                //}

                //var contaItensComQtdDifrente = (from contaItensComQtdDifrenteL in listRepItens
                //                                where contaItensComQtdDifrenteL.QtdDisponivel != contaItensComQtdDifrenteL.ItensListaControle.QuantidadeNecessaria
                //                                select contaItensComQtdDifrenteL).Count()

                var contaItensComQtdDifrente = (from contaItensComQtdDifrenteL in queryLastNames
                                                where
                                                    contaItensComQtdDifrenteL.QtdDisponivel !=
                                                    contaItensComQtdDifrenteL.ItensListaControle.QuantidadeNecessaria
                                                select contaItensComQtdDifrenteL).Count();

                lblContadorQtdsDiferentes.InnerText = contaItensComQtdDifrente.ToString();
                #endregion

                #region validar data de validade do item

                // Verificar se o prazo de validade dos itens se encontra dentro do período.
                Int32 numDiasPeriodo = BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMedicamento;
                TimeSpan timeSpan;

                var contaItensDataValidaAVencer = (from item in listRepItens
                                                   where
                                                       ((item.Lote.DataValidadeLote - DateTime.Now.Date).Days <=
                                                        numDiasPeriodo)
                                                        && item.QtdDisponivel > 0
                                                        && item.ItensListaControle.Alinea.IdfClasse == 2
                                                   select item).Count();

                lblContadorVencidos.InnerText = contaItensDataValidaAVencer.ToString();

                lblContadorMateriaisVencidos.InnerText = ListaItensVencidos(false).Count.ToString();

                #endregion

                if (contaItensComQtdDifrente > 0 && contaItensDataValidaAVencer > 0)
                {
                    divItensVencerQuantidadeDiferente.Visible = true;
                    return;
                }
                else
                {
                    divItensVencerQuantidadeDiferente.Visible = false;
                }

                // Precisa informar justificativa.
                if (contaItensComQtdDifrente > 0)
                {
                    tbDscOcorrenciaLacrarComQtdDiferente.Visible = true;
                    return;
                }
                else
                {
                    tbDscOcorrenciaLacrarComQtdDiferente.Visible = false;
                }

                // Precisa informar justificativa.
                if (contaItensDataValidaAVencer > 0)
                {
                    tbDscOcorrenciaLacrarComValidadeForaPeriodo.Visible = true;
                    return;
                }
                else
                {
                    tbDscOcorrenciaLacrarComValidadeForaPeriodo.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ExibirMensagem(TipoMensagem.Erro, "ERRO : " + ex.Message);
            }


        }

        public static List<Entity.ItensListaControle> ListaItensEmFalta(long seqRepositorio)
        {
            //Na tela de importação de itens somente listar os ATIVOS
            List<Entity.ItensListaControle> itens = new List<ItensListaControle>();

            BLL.ItensListaControle bllItensListaControle = new BLL.ItensListaControle();

            itens = bllItensListaControle.ObterItensEmFaltaNoRepositorio(seqRepositorio);

            return itens;
        }

        [WebMethod]
        public static string ObterItensEmFaltaNoRepositorio(long seqRepositorio)
        {
            return JsonConvert.SerializeObject(ListaItensEmFalta(seqRepositorio), Formatting.Indented, new JsonSerializerSettings { });
        }

        public static List<Entity.LacreRepositorioItens> ListaItensComQtdDiferente()
        {
            // Obter os itens do repostório.
            List<Entity.LacreRepositorioItens> listRepItens =
                new BLL.LacreRepositorioItens().ObterPorLacreRepositorio(seqLacreRepositorio, null);

            var grupoMateriais =
                (from student in listRepItens
                 group student by new {student.Material.Codigo,
                     student.Material.Nome
                 } into newGroup
                     //orderby newGroup.Key
                     select new Entity.LacreRepositorioItens()
                     {
                         Material = new Material()
                         {
                             Codigo = Convert.ToString(newGroup.Key.Codigo),
                             Nome = newGroup.Key.Nome//newGroup.Select(x => x.Material.Nome).FirstOrDefault(),

                         },
                         ItensListaControle = new ItensListaControle()
                         {
                             QuantidadeNecessaria =
                                 newGroup.Max(x => x.ItensListaControle.QuantidadeNecessaria),
                             Unidade = newGroup.Select(x => x.ItensListaControle.Unidade).FirstOrDefault(),
                         },
                         QtdDisponivel = newGroup.Sum(x => x.QtdDisponivel)
                     }).ToList();

            var itensComQtdDiferente = (from contaItensComQtdDifrenteL in grupoMateriais
                                       where
                                           contaItensComQtdDifrenteL.QtdDisponivel !=
                                           contaItensComQtdDifrenteL.ItensListaControle.QuantidadeNecessaria
                                       select contaItensComQtdDifrenteL).ToList();

            return itensComQtdDiferente;
        }
        
        [WebMethod]
        public static string ObterItensComQtdDiferente()
        {
            return JsonConvert.SerializeObject(ListaItensComQtdDiferente(), Formatting.Indented, new JsonSerializerSettings { });
        }
        
        /// <summary>
        /// Método que lista os itens do carrinho dentro do prazo para troca(a vencer)
        /// </summary>
        /// <param name="exibirMedicamentos">
        /// Se passado o valor true o sistema mostra somente os medicamentos a vencer, caso contrario, mostrar somente os materiais
        /// </param>
        /// <returns></returns>
        public static List<Entity.LacreRepositorioItens> ListaItensVencidos(bool exibirMedicamentos = true)
        {
            // Obter os itens do repostório.
            List<Entity.LacreRepositorioItens> listRepItens =
                new BLL.LacreRepositorioItens().ObterPorLacreRepositorio(seqLacreRepositorio, null);

            // Verificar se o prazo de validade dos itens se encontra dentro do período.
            Int32 numDiasPeriodo = BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMedicamento;

            TimeSpan timeSpan;

            var itensDataValidaAVencer = new List<LacreRepositorioItens>();

            if (exibirMedicamentos)
            {
                itensDataValidaAVencer = (from item in listRepItens
                                              where
                                                  ((item.Lote.DataValidadeLote - DateTime.Now.Date).Days <=
                                                   numDiasPeriodo)
                                                   && item.QtdDisponivel > 0
                                                   && item.ItensListaControle.Alinea.IdfClasse == 2
                                              select item).ToList();
            }
            else
            {
                itensDataValidaAVencer = (from item in listRepItens
                                          where
                                              ((item.Lote.DataValidadeLote - DateTime.Now.Date).Days <=
                                               BLL.Parametrizacao.Instancia().NumeroDeDiasParaVencimentoDaTrocaDeMaterial)
                                               && item.QtdDisponivel > 0
                                               && item.ItensListaControle.Alinea.IdfClasse != 2
                                          select item).ToList();
            }
           

            return itensDataValidaAVencer;
        }

        [WebMethod]
        public static string ObterItensVencidos()
        {
            return JsonConvert.SerializeObject(ListaItensVencidos(), Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
        }

        [WebMethod]
        public static string ObterMateriaisVencidos()
        {
            return JsonConvert.SerializeObject(ListaItensVencidos(false), Formatting.Indented, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" });
        }

        private void CarregarLacresComplementos()
        {
            try
            {
                this.grvLacreComplemento.DataSource = new BLL.LacreRepositorioComplemento().ObterComplemento(seqLacreRepositorio);
                this.grvLacreComplemento.DataBind();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private void ExcluirLacreComplemento(Int64 seq)
        {
            try
            {
                new Hcrp.CarroUrgenciaPsicoativo.BLL.LacreRepositorioComplemento().AtivarOuInativar(false, seq);

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}