<%@ Page Title="" Language="C#" MasterPageFile="~/Master/HcrpMaster.Master" MaintainScrollPositionOnPostback="true"
    AutoEventWireup="true" CodeBehind="GerenciarConferenciaDeLacre.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Conferencia.GerenciarConferenciaDeLacre" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        li.active > a
        {
            background-color: #428bca !important;
            color: #fff !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphCabecalho" runat="server">
    Lista conferência / Controle de lacre
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphConteudo" runat="server">
    <asp:HiddenField ID="hdfComando" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdfRetornoValor" runat="server" />
    <asp:Button runat="server" ID="btnPost" OnClick="btnPost_Click" Style="display: none" />
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                Informações sobre o repositório</h3>
        </div>
        <div id="div1" class="panel-body">
            <fieldset disabled>
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                        Instituto:</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="lblInstituto" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                    <label class="col-sm-2 control-label">
                        Situação:</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="lblSituacao" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                        Patrimônio:</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="lblPatrimonio" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                    <label class="col-sm-2 control-label">
                        Tipo da Lista:</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="lblTipoDaLista" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hdnSeqTipoListaControle" ClientIDMode="Static" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                        Motivo do Rompimento:</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="lblMotivoRompimento" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                    <div class="col-md-offset-2 col-sm-4">
                        <a onclick="mostrarItensDaLista();" style="cursor: pointer">Clique aqui para visualizar
                            os itens da lista</a>
                    </div>
                </div>
            </fieldset>
            <div class="form-group ">
                <div class="col-sm-12">
                    <asp:Button ID="btnVoltarRegConsSaida" runat="server" Text="Voltar" CausesValidation="False"
                        CssClass="btn btn-success" Width="100px" OnClick="btnVoltarRegConsSaida_Click">
                    </asp:Button>
                </div>
            </div>
        </div>
    </div>
    <ul class="nav nav-tabs" id="myTab">
        <li class="active"><a href="#RegistrarConsumoSaida" data-toggle="tab">Registrar Consumo/Saída</a></li>
        <li><a href="#InformarReposicao" data-toggle="tab">Informar Reposição</a></li>
        <li><a href="#Equipamento" data-toggle="tab">Equipamento</a></li>
        <li><a href="#RegistrarOcorrencia" data-toggle="tab">Registrar Ocorrência</a></li>
        <li><a href="#Lacrar" data-toggle="tab">Lacrar</a></li>
    </ul>
    <div class="panel panel-default">
        <div class="panel-body">
            <div id="myTabContent" class="tab-content">
                <div class="tab-pane fade active in" id="RegistrarConsumoSaida">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Atendimento paciente</h3>
                        </div>
                        <asp:UpdatePanel runat="server" ID="uppPaciente" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtRegCliente" EventName="TextChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <div id="divPaciente" class="panel-body">
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <div id="divlblMsgOperacaoConsumoSaida" runat="server" style="padding-bottom: 3px;">
                                                <asp:Label ID="lblMsgOperacaoConsumoSaida" runat="server" CssClass="label label-primary" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="input-group input-group-sm">
                                                <span class="input-group-addon">* Registro:</span>
                                                <asp:TextBox ID="txtRegCliente" runat="server" class="form-control" MaxLength="8"
                                                    AutoPostBack="True" OnTextChanged="txtRegCliente_TextChanged" ClientIDMode="Static"></asp:TextBox>
                                                <%--<asp:ImageButton ID="ibtnBuscaPaciente"
                                                runat="server" AlternateText="Clique aqui para realizar a pesquisa." ImageAlign="AbsMiddle"
                                                CssClass="Hand" ImageUrl="~/InterfaceHC/Imagens/Bt_lupa.gif" Width="15px" Height="15px"
                                                CausesValidation="False" Style="margin-left: 3px; border: 0px; padding: 0px"
                                                OnClick="ibtnBuscaPaciente_Click"></asp:ImageButton>--%>
                                                <span class="input-group-btn">
                                                    <button id="Button2" type="button" class="btn btn-default" runat="server" onclick="ibtnBuscaPaciente_Click">
                                                        <span class="glyphicon glyphicon-search"></span>
                                                    </button>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-8">
                                            <div class="input-group input-group-sm">
                                                <span class="input-group-addon">Nome</span>
                                                <asp:TextBox ID="txtNomePaciente" runat="server" class="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-3">
                                            <div class="input-group input-group-sm">
                                                <span class="input-group-addon">Idade:</span>
                                                <asp:TextBox ID="txtIdadePaciente" runat="server" disabled Width="190px" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="input-group input-group-sm">
                                                <span class="input-group-addon">Sexo:</span>
                                                <asp:TextBox ID="lblSexo" runat="server" disabled Width="190px" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="input-group input-group-sm">
                                                <span class="input-group-addon">Cor:</span>
                                                <asp:TextBox ID="lblCor" runat="server" disabled Width="190px" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="input-group input-group-sm">
                                                <span class="input-group-addon">Atendimento:</span>
                                                <asp:TextBox ID="lblAtendimento" runat="server" class="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="alert alert-warning" runat="server" id="divConsumoSemAtendimento" visible="True">
                                        <label>
                                            Motivo de consumo sem atendimento ao paciente.
                                        </label>
                                        <asp:TextBox ID="txtJustificativaConsumoSemAtendimento" runat="server" CssClass="form-control"
                                            Height="80px" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:UpdatePanel runat="server" ID="uppBuscaMaterial" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscarMaterial" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="panel panel-group" id="accordion">
                                <div class="panel panel-default">
                                    <div class="panel-heading" style="cursor: pointer" data-toggle="collapse" data-parent="#accordion"
                                        href="#divCodBarMaterial">
                                        <h3 class="panel-title">
                                            Informar consumo por código de barras (somente item rastreado)
                                        </h3>
                                    </div>
                                    <div id="divCodBarMaterial" class="panel-collapse collapse in">
                                        <div class="panel-body">
                                            <div class="col-sm-4">
                                                <div class="input-group input-group-sm">
                                                    <span class="input-group-addon">Cod. Material</span>
                                                    <asp:TextBox runat="server" ID="txtCodigoMaterial" class="form-control" runat="server"
                                                        ClientIDMode="Static"></asp:TextBox>
                                                    <%-- 
                                                        data-placement="right"
                                                        data-container="body" 
                                                        data-content="Mantenha o foco neste controle para o leitor de código de barras funcionar."

                                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtCodigoMaterial" class="form-control"
                                                                data-placement="top" data-content="Digite o código do material para pesquisar e pressione Enter"></asp:TextBox>--%>
                                                    <%--<asp:HiddenField runat="server" ID="hdnCodMaterial" ClientIDMode="Static" />--%>
                                                    <asp:Button runat="server" ID="btnBuscarMaterial" Style="display: none;" ClientIDMode="Static"
                                                        OnClick="btnBuscarMaterial_Click" />
                                                    <%--<span class="input-group-btn">
                                                                <button type="button" class="btn btn-default" data-toggle="modal" data-target="#myModal"
                                                                    href="../Comum/BuscaMaterialListaControleItem.aspx">
                                                                    <span class="glyphicon glyphicon-search"></span>
                                                                </button>
                                                            </span>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                            <div class="form-group">
                                <div class="col-sm-2">
                                    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CssClass="btn btn-success"
                                        Width="100px" OnClick="btnImprimir_Click"></asp:Button>
                                </div>
                                <div class="col-sm-2">
                                    <asp:UpdatePanel runat="server" ID="uppSalvarConsumoSaida" UpdateMode="Conditional"
                                        CssClass="pull-right">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnSalvarConsumoSaida" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Button ID="btnSalvarConsumoSaida" runat="server" ClientIDMode="Static" Text="Salvar consumo"
                                                CssClass="btn btn-success" OnClick="btnSalvarConsumoSaida_Click"></asp:Button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-sm-3">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btnRequisicao" EventName="Click"></asp:AsyncPostBackTrigger>
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Button ID="btnRequisicao" runat="server" Text="Requisição de medicamentos" CssClass="btn btn-success"
                                                OnClick="btnRequisicao_Click"></asp:Button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div class="panel panel-default" id="ai">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        Itens do repositório</h3>
                                </div>
                                <asp:Panel runat="server" ID="Panel1">
                                    <div class="panel-body">
                                        <div id="divGridItensConsumidos" style="overflow: auto; width: 100%; max-height: 450px;
                                            position: relative" class="table-responsive">
                                            <%--<asp:UpdatePanel runat="server" ID="uppMaterial" UpdateMode="Conditional">
                                            <Triggers>
                                                
                                            </Triggers>
                                            <ContentTemplate>--%>
                                            <asp:GridView runat="server" ID="grvItemConsumoSaida" Width="100%" AutoGenerateColumns="False"
                                                ClientIDMode="Static" class="table table-bordered table-condensed" EmptyDataText="Nenhum registro foi localizado."
                                                OnRowDataBound="grvItemConsumoSaida_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Alínea">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAlinea" runat="server" Text='<%# Eval("ItensListaControle.Alinea.Nome")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Código">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCodigoMaterial" runat="server" Text='<%# Eval("Material.Codigo")%>'></asp:Label>
                                                            <asp:HiddenField ID="hdfSeqLacreRepositorioItens" runat="server" Value='<%# Eval("SeqLacreRepositorioItens")%>' />
                                                            <asp:HiddenField ID="hdnLoteInsumoUnitario" runat="server" Value='<%# Eval("Lote.NumLote")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nome">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNomeMaterial" runat="server" Text='<%# Eval("Material.Nome")%>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unidade">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnidade" runat="server" Text='<%# Eval("ItensListaControle.Unidade.Nome")%>'></asp:Label></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qtd.<br />Necessária">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQtdNecessaria" runat="server" Enabled="false" Text='<%# Eval("ItensListaControle.QuantidadeNecessaria")%>'
                                                                Width="50px" CssClass="form-control input-sm "></asp:TextBox></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qtd.<br />Disponível">
                                                        <ItemTemplate>
                                                            <strong>
                                                                <asp:Label ID="lblQtdDisponivel" runat="server" Text='<%# Eval("QtdDisponivel")%>'></asp:Label></strong></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qtd.<br />Utilizada">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtUtilizada" runat="server" Text='<%# Eval("QtdUtilizada")%>' Width="50px"
                                                                CssClass="form-control input-sm" MaxLength="5"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnQtdUtilizada" runat="server" Value='<%# Eval("QtdUtilizada")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Lote">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLote" runat="server" Text='<%# Eval("Lote.NumLoteFabricante")%>'></asp:Label><itemstyle
                                                                horizontalalign="Center" /></ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Validade">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValidade" runat="server"></asp:Label>
                                                            <%--<tr id="trJustificativaSemAtendimento" runat="server" class="alert alert-warning">
                                                                    <td colspan="99">
                                                                        <label>
                                                                            Justificativa
                                                                        </label>
                                                                        <asp:TextBox ID="txtJustificativa" runat="server" TextMode="MultiLine" CssClass="form-control input-sm"
                                                                            MaxLength="200" Text='<%# Eval("DscJustificativaConsumoSemAtendimento")%>'>
                                                                        </asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="reqFldJustificativaSemAtendimento" runat="server"
                                                                            ControlToValidate="txtJustificativa" Text="*" ForeColor="Red" ValidationGroup="regConsumoSaida"
                                                                            ErrorMessage="Justificatica requerido."></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>--%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <span class="Conteudo_legenda_campos1">
                        <asp:CheckBox ID="chkGerarRequisicaoDeReposicaoAutomaticamente" runat="server" Text="Gerar requisição de reposição automaticamente"
                            class="hidden" />
                    </span>
                </div>
                <div class="tab-pane fade" id="InformarReposicao">
                    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnPost" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lblInformelote" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Button runat="server" ID="btnBuscaMaterialRep" Style="display: none;" ClientIDMode="Static"
                                OnClick="btnBuscaMaterialRep_Click" />
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        Repor itens</h3>
                                    <asp:CheckBox ID="chkMatComSemCodigo" CssClass="checkbox" runat="server" Text="Item sem código"
                                        AutoPostBack="True" OnCheckedChanged="chkMatComSemCodigo_CheckedChanged" />
                                </div>
                                <div id="divItens" class="panel-body">
                                    <asp:Panel ID="pnMaterialComCodigo" runat="server">
                                        <div class="form-group">
                                            <div class="col-sm-4">
                                                <div class="input-group input-group-sm">
                                                    <span class="input-group-addon">Cod. Material:</span>
                                                    <asp:TextBox runat="server" ID="txtCodMaterial" class="form-control" ClientIDMode="Static"
                                                        data-placement="top" data-content="Digite o código do material e pressione Enter para pesquisar ou utilize o leitor de código de barras"></asp:TextBox>
                                                    <%--<asp:ImageButton ID="ibtnBuscaPaciente"
                                                        runat="server" AlternateText="Clique aqui para realizar a pesquisa." ImageAlign="AbsMiddle"
                                                        CssClass="Hand" ImageUrl="~/InterfaceHC/Imagens/Bt_lupa.gif" Width="15px" Height="15px"
                                                        CausesValidation="False" Style="margin-left: 3px; border: 0px; padding: 0px"
                                                        OnClick="ibtnBuscaPaciente_Click"></asp:ImageButton>--%>
                                                    <span class="input-group-btn"></span>
                                                </div>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="input-group input-group-sm">
                                                    <span class="input-group-addon">Nome</span>
                                                    <asp:TextBox ID="txtNomeMaterialComCodigo" runat="server" class="form-control" disabled
                                                        ClientIDMode="Static"></asp:TextBox>
                                                    <asp:HiddenField ID="hdfSeqItemListaControle" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="col-sm-5">
                                                <div class="input-group input-group-sm">
                                                    <span class="input-group-addon">Unidade de Medida:</span>
                                                    <asp:TextBox ID="txtUnidMedMaterialComCodigo" runat="server" disabled CssClass="form-control"
                                                        ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div id="divLoteAutomatico" runat="server" clientidmode="Static">
                                                <label class="col-sm-1 control-label">
                                                    Lote:</label>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddlLoteMatComCodigo" runat="server" class="form-control input-sm"
                                                        data-placeholder="Selecione" ClientIDMode="Static" data-toggle="dropdown">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-5" id="divLoteManual" runat="server" clientidmode="Static" visible="False">
                                                <div class="input-group input-group-sm">
                                                    <span class="input-group-addon">Lote:</span>
                                                    <asp:TextBox ID="txtLoteManual" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="input-group input-group-sm">
                                                    <span class="input-group-addon">Validade:</span>
                                                    <asp:TextBox ID="txtValidadeManual" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:LinkButton runat="server" ID="lblInformelote" Text="Clique
                                                    para informar o lote manualmente" OnClick="lblInformelote_Click"></asp:LinkButton>
                                                <%--<a onclick="informarLoteManual();" style="cursor: pointer" id="lblInformelote" runat="server"></a>--%>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnMaterialSemCodigo" runat="server" Visible="False">
                                        <div class="form-group">
                                            <label class="col-sm-1 control-label">
                                                Item:</label>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlItemMatSemCodigo" runat="server" class="form-control input-sm"
                                                    data-toggle="dropdown">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-1 control-label">
                                                Validade:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtValidadeItemSemCodigo" runat="server" CssClass="form-control input-sm"
                                                    ClientIDMode="Static"></asp:TextBox>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                    <div class="form-group">
                                        <label class="col-sm-1 control-label">
                                            Quantidade:</label>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtQtdMat" runat="server" MaxLength="5" Width="70px" CssClass="form-control input-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-5">
                                            <asp:Button ID="btnAdicionaMaterial" runat="server" Text="Adicionar" CssClass="btn btn-success"
                                                Width="100px" OnClick="btnAdicionaMaterial_Click"></asp:Button>
                                        </div>
                                    </div>
                                    <div style="overflow: auto; width: 100%; max-height: 450px; position: relative" class="table-responsive"
                                        id="divRegistroReposicao">
                                        <%--<asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnPost" EventName="Click"></asp:AsyncPostBackTrigger>
                                                        <asp:AsyncPostBackTrigger ControlID="btnBuscaMaterialRep" EventName="Click"></asp:AsyncPostBackTrigger>
                                                    </Triggers>
                                                    <ContentTemplate>--%>
                                        <asp:GridView runat="server" ID="grvItemInformarReposicao" Width="100%" AutoGenerateColumns="False"
                                            class="table table-bordered table-condensed" BorderWidth="0px" EmptyDataText="Nenhum registro foi localizado."
                                            OnRowCommand="grvItemInformarReposicao_RowCommand" OnRowDataBound="grvItemInformarReposicao_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Alínea">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAlinea" runat="server" Text='<%# Eval("ItensListaControle.Alinea.Nome")%>'></asp:Label>
                                                        <asp:HiddenField ID="hdfSeqLacreRepositorioItens" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Código">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCodigoMaterial" runat="server" Text='<%# Eval("Material.Codigo")%>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nome">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNomeMaterial" runat="server" Text='<%# Eval("Material.Nome")%>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unidade">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnidade" runat="server" Text='<%# Eval("ItensListaControle.Unidade.Nome")%>'></asp:Label></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qtd.<br />Necessária">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQtdNecessaria" runat="server" CssClass="form-control input-sm"
                                                            Enabled="false" Text='<%# Eval("ItensListaControle.QuantidadeNecessaria")%>'
                                                            Width="50px"></asp:TextBox></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qtd.<br />Disponível">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQtdDisponivel" runat="server" Enabled="false" Text='<%# Eval("QtdDisponivel")%>'
                                                            CssClass="form-control input-sm" Width="50px"></asp:TextBox></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lote">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLote" runat="server" Text='<%# Eval("Lote.NumLoteFabricante")%>'></asp:Label><itemstyle
                                                            horizontalalign="Center" /></ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Validade">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblValidade" runat="server" Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="ibtnViimgBtnExcluirItem" type="button" class="btn btn-default btn-sm"
                                                            Text="<span class='glyphicon glyphicon-remove'></span>" CommandArgument='<%# Eval("SeqLacreRepositorioItens")%>'
                                                            CommandName="EXCLUIR" OnClientClick="return confirm('Deseja realmente remover este item?');" />
                                                        <itemstyle horizontalalign="Center" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <%--</ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="tab-pane fade" id="Equipamento">
                    <asp:UpdatePanel runat="server" ID="uppEquipamento" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlTipoPatrimonio" EventName="SelectedIndexChanged">
                            </asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="btnAdicionaPatrimonio" EventName="Click"></asp:AsyncPostBackTrigger>
                            <asp:AsyncPostBackTrigger ControlID="txtNumPatrimonio" EventName="TextChanged"></asp:AsyncPostBackTrigger>
                        </Triggers>
                        <ContentTemplate>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Tipo patrimônio:</label>
                                <div class="col-sm-8">
                                    <asp:DropDownList ID="ddlTipoPatrimonio" runat="server" Width="300px" AutoPostBack="True"
                                        class="form-control input-sm" data-toggle="dropdown" OnSelectedIndexChanged="ddlTipoPatrimonio_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-4">
                                    <div class="input-group input-group-sm">
                                        <span class="input-group-addon">Número Patrimônio:</span>
                                        <asp:TextBox ID="txtNumPatrimonio" runat="server" MaxLength="8" AutoPostBack="True"
                                            OnTextChanged="txtNumPatrimonio_TextChanged" CssClass="form-control"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton runat="server" ID="imgBtnBuscapatrimonio" type="button" class="btn btn-default"
                                                OnClick="imgBtnBuscapatrimonio_Click" Text="<span class='glyphicon glyphicon-search'></span>" />
                                        </span>
                                    </div>
                                </div>
                                <div class="col-sm-8">
                                    <div class="input-group input-group-sm">
                                        <span class="input-group-addon">Descrição</span>
                                        <asp:TextBox ID="txtDscPatrimonio" runat="server" disabled CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <asp:Button ID="btnAdicionaPatrimonio" runat="server" Text="Adicionar" CssClass="btn btn-success"
                                        Width="100px" OnClick="btnAdicionaPatrimonio_Click"></asp:Button>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdfNumBemEquipamento" runat="server" />
                            <br />
                            <div style="overflow: auto; width: 100%; height: 450px;">
                                <asp:GridView runat="server" ID="grvItemRepLacreEquipamento" Width="97%" AutoGenerateColumns="False"
                                    class="table table-bordered table-condensed" BorderWidth="0px" EmptyDataText="Nenhum registro foi localizado."
                                    OnRowCommand="grvItemRepLacreEquipamento_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Patrimônio">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPatrimonio" runat="server" Text='<%# Eval("BemPatrimonial.DscModelo")%>'></asp:Label></ItemTemplate>
                                            <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descrição">
                                            <ItemTemplate>
                                                <asp:Label ID="txtDescricao" runat="server" Text='<%# Eval("BemPatrimonial.DscComplementar")%>'></asp:Label></ItemTemplate>
                                            <ItemStyle Width="355px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Teste">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTeste" runat="server" Text='<%# Eval("DataTeste", "{0: dd/MM/yyyy HH:mm}")%>'></asp:Label></ItemTemplate>
                                            <ItemStyle Width="115px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Resp. Teste">
                                            <ItemTemplate>
                                                <asp:Label ID="lblResponsavelTeste" runat="server" Text='<%# Eval("UsuarioResponsavelTeste.Nome")%>'></asp:Label></ItemTemplate>
                                            <ItemStyle Width="125px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="ibtnVisualizarOpcoes" type="button" class="btn btn-default btn-sm"
                                                    Text="<span class='glyphicon glyphicon-list'></span>" CommandName="SEL" CommandArgument='<%# Eval("SeqLacreRepositorioEquipamento")%>'>        
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="35px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="tab-pane fade" id="RegistrarOcorrencia">
                    <div class="form-group">
                        <h3 class="col-sm-4">
                            Registrar Ocorrência</h3>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <asp:TextBox ID="txtDscOcorrencia" runat="server" TextMode="MultiLine" CssClass="form-control"
                                ClientIDMode="Static" MaxLength="500"></asp:TextBox>
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server" ID="uppOcorrencias" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAdicionaOcorrencia" EventName="Click"></asp:AsyncPostBackTrigger>
                        </Triggers>
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="col-sm-12">
                                    <asp:Button ID="btnAdicionaOcorrencia" runat="server" Text="Adicionar" CssClass="btn btn-success"
                                        CausesValidation="true" Width="100px" OnClick="btnAdicionaOcorrencia_Click">
                                    </asp:Button>
                                </div>
                            </div>
                            <div style="overflow: auto; width: 100%; height: 450px;">
                                <asp:Repeater runat="server" ID="rptLacreOcorrenciaRegistradas" OnItemDataBound="rptLacreOcorrenciaRegistradas_ItemDataBound">
                                    <HeaderTemplate>
                                        <div class="col-sm-12">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <h4>
                                            <asp:Label ID="lblDataOcorrenciaRegistrada" runat="server" Text="-" class="label label-default"></asp:Label></h4>
                                        <asp:TextBox ID="txtDescOcorrenciaRegistradaVisualizacao" runat="server" CssClass="form-control"
                                            TextMode="MultiLine" Text='<%# Eval("DscOcorrencia") %>' ReadOnly="true"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </div>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="tab-pane fade" id="Lacrar">
                    <asp:UpdatePanel ID="uppLacracao" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnLacrar" EventName="Click"></asp:AsyncPostBackTrigger>
                        </Triggers>
                        <ContentTemplate>
                            <div visible="False" class="col-sm-12 alert alert-danger" style="height: 33px; padding-top: 5px;"
                                runat="server" id="divInfoPodeLacrar" clientidmode="Static">
                                <strong>Voce não possui permissão para lacrar !</strong>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12" runat="server" id="divItensFaltantes">
                                    <a class="text-danger" onclick="mostrarItensFaltantes();" style="cursor: pointer">Existem
                                        <span class="badge" runat="server" id="lblContadorItensFaltantes"></span>&nbsp;itens
                                        que estão configurados na lista mas não estão no repositório. Clique aqui para visualizar.
                                    </a>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12" runat="server" id="div2">
                                    <a class="text-danger" onclick="mostrarItensQtdDiferente();" style="cursor: pointer">
                                        Existem <span class="badge" runat="server" id="lblContadorQtdsDiferentes"></span>
                                        &nbsp;itens que estão com qtd. disponível diferente da qtd. necessária. Clique aqui
                                        para visualizar. </a>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12" runat="server" id="div3">
                                    <a class="text-danger" onclick="mostrarItensVencidos();" style="cursor: pointer">Existe(m)
                                        <span class="badge" runat="server" id="lblContadorVencidos"></span>&nbsp;MEDICAMENTO(S) que
                                        estão dentro do prazo de validade. Clique aqui para visualizar. </a>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-12" runat="server" id="div4">
                                    <a class="text-danger" onclick="mostrarMateriaisVencidos();" style="cursor: pointer">Existe(m)
                                        <span class="badge" runat="server" id="lblContadorMateriaisVencidos"></span>&nbsp;MATERIAL(AIS) que
                                        estão dentro do prazo de validade. Clique aqui para visualizar. </a>
                                </div>
                            </div>
                            <div class="alert alert-warning" runat="server" id="tbDscOcorrenciaLacrarComQtdDiferente"
                                visible="False">
                                <label>
                                    Motivo de lacração com quantidades necessárias diferentes das quantidades disponíveis
                                </label>
                                <asp:TextBox ID="txtDscOcorrenciaLancarComQtdDiferente" runat="server" CssClass="form-control"
                                    Height="80px" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            
                            <div class="alert alert-warning" runat="server" runat="server" id="tbDscOcorrenciaLacrarComValidadeForaPeriodo"
                                visible="False">
                                <label>
                                    Motivo de lacração com validade dos itens a vencer
                                </label>
                                <asp:TextBox ID="txtDscOcorrenciaLancarComItensAVencer" runat="server" CssClass="form-control"
                                    MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            
                            <div class="alert alert-warning" runat="server" id="divItensVencerQuantidadeDiferente"
                                visible="False">
                                <label>
                                    Motivo de lacração com quantidades necessárias diferentes das quantidades disponíveis
                                    e itens a vencer
                                </label>
                                <asp:TextBox ID="txtItensVencerQuantidadeDiferente" runat="server" CssClass="form-control"
                                    Height="80px" MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                            
                            <div class="form-group" runat="server" id="divCaixaIntubacao">
                                <label class="col-sm-3 control-label">
                                    Lacres complemento(Ex.:Caixa intubação):</label>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtDescricaoComplemento" runat="server" placeholder="Digite o nome" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-sm-3 ">
                                    <asp:TextBox ID="txtNumComplemento" placeholder="Digite o numero do lacre" runat="server" MaxLength="20" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                               <div  class="col-sm-2 ">
                                   <asp:Button ID="btnInserirComplemento" runat="server" Text="Adicionar" CssClass="btn btn-success"
                                Width="100px" onclick="btnInserirComplemento_Click" ></asp:Button>
                               </div>

                               <div  class="col-sm-6 ">
                                   <div class="table-responsive">
                                        <asp:GridView runat="server" ID="grvLacreComplemento" Width="100%" AutoGenerateColumns="False"
                                        BorderWidth="0" AllowSorting="false" CssClass="table table-hover table-bordered"
                                        EmptyDataText="Nenhum lacre complemento registrado" 
                                            onrowcommand="grvLacreComplemento_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Descrição" HeaderStyle-Width="60%">
                                                <ItemTemplate>
                                                    <%# Eval("DSC_COMPLEMENTO")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Número do lacre">
                                                <ItemTemplate>
                                                    <%# Eval("NUM_LACRE")%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="ibtnViimgBtnExcluirItem" type="button" class="btn btn-default btn-sm"
                                                            Text="<span class='glyphicon glyphicon-remove'></span>" CommandArgument='<%# Eval("SEQ_REPOSITORIO_COMPLEMENTO")%>'
                                                            CommandName="EXCLUIR" OnClientClick="return confirm('Deseja realmente remover este item?');" />
                                                        <itemstyle horizontalalign="Center" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                        </Columns>
                                        <PagerTemplate>
                                            <div id="page-selection" class="pagination"></div>
                                        </PagerTemplate>
                                    </asp:GridView>
                                   </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Número do lacre do carrinho:</label>
                                <div class="col-sm-3 ">
                                    <asp:TextBox ID="txtNumLacre" placeholder="Digite o número do lacre"  runat="server" MaxLength="8" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <div class="col-sm-3 ">
                                    <asp:Button ID="btnLacrar" runat="server" Text="Lacrar carrinho" CssClass="btn btn-success"
                                     OnClick="btnLacrar_Click"></asp:Button>
                                </div>
                            </div>
                                                       
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <br />
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalItensDaLista" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h3 class="modal-title">
                        Itens configurados para a lista</h3>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table id="tblItensListaControleModal" class="table table-bordered table-hover table-condensed">
                            <thead style="background-color: #dddddd">
                                <tr>
                                    <th>
                                        Alínea
                                    </th>
                                    <th>
                                        Código
                                    </th>
                                    <th>
                                        Item
                                    </th>
                                    <th>
                                        Quantidade necessária
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hdnSeqRepositorio" ClientIDMode="Static" />
    <div class="modal fade" id="modalItensFaltantes" tabindex="-2" role="dialog" aria-labelledby="myModalLabel2"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h3 class="modal-title">
                        Itens em falta no repositório</h3>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table id="tblItensFaltantes" class="table table-bordered table-hover table-condensed">
                            <thead style="background-color: #dddddd">
                                <tr>
                                    <th>
                                        Código
                                    </th>
                                    <th>
                                        Item
                                    </th>
                                    <th>
                                        Quantidade necessária
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalItensQtdDiferente" tabindex="-3" role="dialog" aria-labelledby="myModalLabel1"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h3 class="modal-title">
                        Itens com qtds. diferentes no repositório</h3>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table id="tblItensQtdDiferente" class="table table-bordered table-hover table-condensed">
                            <thead style="background-color: #dddddd">
                                <tr>
                                    <th>
                                        Código
                                    </th>
                                    <th>
                                        Item
                                    </th>
                                    <th>
                                        Quantidade necessária
                                    </th>
                                    <th>
                                        Quantidade disponível
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalItensVencidos" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h3 class="modal-title">
                        Medicamentos dentro do prazo de validade no repositório</h3>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table id="tblItensVencidos" class="table table-bordered table-hover table-condensed">
                            <thead style="background-color: #dddddd">
                                <tr>
                                    <th>
                                        Código
                                    </th>
                                    <th>
                                        Item
                                    </th>
                                    <th>
                                        Data de vencimento
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <div class="modal fade" id="modalMateriaisVencidos" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h3 class="modal-title">
                        Materiais dentro do prazo de validade no repositório</h3>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table id="tblMateriaisVencidos" class="table table-bordered table-hover table-condensed">
                            <thead style="background-color: #dddddd">
                                <tr>
                                    <th>
                                        Código
                                    </th>
                                    <th>
                                        Item
                                    </th>
                                    <th>
                                        Data de vencimento
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript" language="javascript">
        
        setInterval(function() { destacarGridItemVencimento(); }, 800);
                
        function destacarGridConsumo(textoAProcurar) {
            $('#<%= grvItemConsumoSaida.ClientID %> td').filter(function () { return $.trim($(this).text()) == textoAProcurar; }).closest("tr").animateHighlightConsumo();
        }

        function destacarGridReposicao(textoAProcurar) {
            $('#<%= grvItemInformarReposicao.ClientID %> td').filter(function () { return $.trim($(this).text()) == textoAProcurar; }).closest("tr").animateHighlightReposicao();
        }

        function manterScrool(idObjeto) {
                    
            var x =  $('#'+ idObjeto).offset();

            $('html,body').animate({scrollTop: (x.top)}, 1000);
        }
                
        function destacarGridItemVencimento() {
            
            $('#<%= grvItemConsumoSaida.ClientID %> tbody').find(".exibirAlerta").animateHighlightItemValidade();
            
            $('#<%= grvItemInformarReposicao.ClientID %> tbody').find(".exibirAlerta").animateHighlightItemValidade();
            
        }
                
        $.fn.animateHighlightItemValidade = function () {
                    
            //alert-warning

            this.toggleClass("alert-warning");
        };

                
        $.fn.animateHighlightConsumo = function () {
                    
            //var t =  $('#<%= grvItemConsumoSaida.ClientID %> td').position();

            var t = $(this).position();

            //$("#divGridItensConsumidos").scrollTo(2500);
            var x =  $('#divCodBarMaterial').offset();

            $('html,body').animate({scrollTop: (x.top)}, 1000);
                    
            $('#divGridItensConsumidos').animate({scrollTop: t.top }, 1000);

            var highlightBg = "#F7C595";
            var animateMs = 400;
            var originalBg = this.css("backgroundColor");
                    
            this.stop().animate({ backgroundColor: highlightBg }, animateMs)
                .animate({ backgroundColor: originalBg }, animateMs)
                .animate({ backgroundColor: highlightBg }, animateMs)
                .animate({ backgroundColor: originalBg }, animateMs)
                .animate({ backgroundColor: highlightBg }, animateMs)
                .animate({ backgroundColor: originalBg }, animateMs)
                .animate({ backgroundColor: originalBg }, animateMs)
                .animate({ backgroundColor: originalBg }, animateMs)
                .animate({ backgroundColor: originalBg }, animateMs)
                .animate({ backgroundColor: originalBg }, animateMs);
        };
                
        $.fn.animateHighlightReposicao = function () {
                                       
            var t = $(this).position();
                    
            var x =  $('#txtCodMaterial').offset();

            $('html,body').animate({scrollTop: (x.top-50)}, 1000);
                    
            $('#divRegistroReposicao').animate({scrollTop: t.top}, 1000);

            var highlightBg = "#F7C595";
            var animateMs = 400;
            var originalBg = this.css("backgroundColor");
                    
            this.stop().animate({ backgroundColor: highlightBg }, animateMs)
                .animate({ backgroundColor: originalBg }, animateMs)
                .animate({ backgroundColor: highlightBg }, animateMs)
                .animate({ backgroundColor: originalBg }, animateMs)
                .animate({ backgroundColor: highlightBg }, animateMs)
                .animate({ backgroundColor: originalBg }, animateMs)
                .animate({ backgroundColor: originalBg }, animateMs)
                .animate({ backgroundColor: originalBg }, animateMs)
                .animate({ backgroundColor: originalBg }, animateMs)
                .animate({ backgroundColor: originalBg }, animateMs);
        };
                
        function mostrarItensFaltantes() {
            listarItensFaltantes();
        }
                
        function mostrarItensQtdDiferente() {
            listarQtdDiferente();
        }
                
        function mostrarItensVencidos() {
            listarVencidos();
        }

        function mostrarMateriaisVencidos() {
            listarMateriaisVencidos();
        }

        function mostrarItensDaLista() {
            listarItensListaControleModal();
        }

        $('#modalItensDaLista').on('hidden.bs.modal', function(e) {
            $.LimparCamposModal(this);
        });

        $('#modalItensFaltantes').on('hidden.bs.modal', function(e) {
            $.LimparCamposModal(this);
        });

        $('#modalItensQtdDiferente').on('hidden.bs.modal', function(e) {
            $.LimparCamposModal(this);
        });

        $('#modalItensVencidos').on('hidden.bs.modal', function(e) {
            $.LimparCamposModal(this);
        });

        function listarItensListaControleModal() {
            $.ajax({
                type: "POST",
                url: "../Comum/ImportaItensOutraLista.aspx/ObterItensPorListaControle",
                data: JSON.stringify({ "seqlistaControle": $("#hdnSeqTipoListaControle").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //beforeSend: 
                success:
                    function(msg) {
                                
                        $.each(jQuery.parseJSON(msg.d), function() {
                            $("#tblItensListaControleModal> tbody").append(
                                "<tr>" +
                                    "<td>" + this['Alinea']['Nome'] + "</td>" +
                                    "<td> " + this['Material']['Codigo'] + "</td>" +
                                    "<td>" + this['Material']['Nome'] + "</td>" +
                                    "<td>" + this['QuantidadeNecessaria'] + " / " + this['Unidade']['Nome'] + "</td>" +
                                    "</tr>");
                        });
                        $('#modalItensDaLista').modal('show');
                    }                        
            });
        }

        function listarItensFaltantes() {
            $.ajax({
                type: "POST",
                url: "../Conferencia/GerenciarConferenciaDeLacre.aspx/ObterItensEmFaltaNoRepositorio",
                data: JSON.stringify({ "seqRepositorio": $("#hdnSeqRepositorio").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //beforeSend: 
                success:
                    function (msg) {

                        if ($("#tblItensFaltantes  > tbody tr").size() > 0) {
                            $("#tblItensFaltantes  > tbody").empty();
                        }

                        $.each(jQuery.parseJSON(msg.d), function () {
                                    
                            $("#tblItensFaltantes  > tbody").append(
                                "<tr>" +
                                    "<td> " + this['Material']['Codigo'] + "</td>" +
                                    "<td>" + this['Material']['Nome'] + "</td>" +
                                    "<td>" + this['QuantidadeNecessaria'] + " / " + this['Unidade']['Nome'] + "</td>" +
                                    "</tr>");
                        });
                        $('#modalItensFaltantes').modal('show');
                    }
            });
        }
                
        function listarQtdDiferente() {
            $.ajax({
                type: "POST",
                url: "../Conferencia/GerenciarConferenciaDeLacre.aspx/ObterItensComQtdDiferente",
                //data: JSON.stringify({ "seqRepositorio": $("#hdnSeqRepositorio").val() }),
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //beforeSend: 
                success:
                    function (msg) {

                        if ($("#tblItensQtdDiferente  > tbody tr").size() > 0) {
                            $("#tblItensQtdDiferente  > tbody").empty();
                        }

                        $.each(jQuery.parseJSON(msg.d), function () {
                                    
                            $("#tblItensQtdDiferente  > tbody").append(
                                "<tr>" +
                                    "<td> " + this['Material']['Codigo'] + "</td>" +
                                    "<td>" + this['Material']['Nome'] + "</td>" +
                                    "<td>" + this['ItensListaControle']['QuantidadeNecessaria'] + " / " + this['ItensListaControle']['Unidade']['Nome'] + "</td>" +
                                    "<td>" + this['QtdDisponivel'] + " / " + this['ItensListaControle']['Unidade']['Nome'] + "</td>" +
                                    "</tr>");
                        });
                        $('#modalItensQtdDiferente').modal('show');
                    }
            });
        }
                
        function listarVencidos() {
            $.ajax({
                type: "POST",
                url: "../Conferencia/GerenciarConferenciaDeLacre.aspx/ObterItensVencidos",
                data: JSON.stringify({ "seqRepositorio": $("#hdnSeqRepositorio").val() }),
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //beforeSend: 
                success:
                    function (msg) {

                        if ($("#tblItensVencidos  > tbody tr").size() > 0) {
                            $("#tblItensVencidos  > tbody").empty();
                        }

                        $.each(jQuery.parseJSON(msg.d), function () {
                                    
                            $("#tblItensVencidos  > tbody").append(
                                "<tr>" +
                                    "<td> " + (this['Material']['Codigo'] == null ? "" : this['Material']['Codigo']) + "</td>" +
                                    "<td>" + this['Material']['Nome'] + "</td>" +
                                    "<td>" + this['Lote']['DataValidadeLote'] + "</td>" +
                                    "</tr>");
                        });
                        $('#modalItensVencidos').modal('show');
                    }
            });
        }
        
        function listarMateriaisVencidos() {
            $.ajax({
                type: "POST",
                url: "../Conferencia/GerenciarConferenciaDeLacre.aspx/ObterMateriaisVencidos",
                data: JSON.stringify({ "seqRepositorio": $("#hdnSeqRepositorio").val() }),
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //beforeSend: 
                success:
                    function (msg) {

                        if ($("#tblMateriaisVencidos  > tbody tr").size() > 0) {
                            $("#tblMateriaisVencidos  > tbody").empty();
                        }

                        $.each(jQuery.parseJSON(msg.d), function () {
                                    
                            $("#tblMateriaisVencidos  > tbody").append(
                                "<tr>" +
                                    "<td> " + (this['Material']['Codigo'] == null ? "" : this['Material']['Codigo']) + "</td>" +
                                    "<td>" + this['Material']['Nome'] + "</td>" +
                                    "<td>" + this['Lote']['DataValidadeLote'] + "</td>" +
                                    "</tr>");
                        });
                        $('#modalMateriaisVencidos').modal('show');
                    }
            });
        }
                
        function post() {
            <%=GetPostGrid() %>
        }

        $('a[data-toggle="tab"]').on('show.bs.tab', function(e) {
            if ( $(e.target).attr("href") == '#InformarReposicao') {
                setTimeout(function(){ $('#txtCodMaterial').focus();}, 500);
                $("#hdfComando").val("ATUALIZAR_GRID_ITENS_REPOSITORIO");
                post();
            }
                    
            if ( $(e.target).attr("href") == '#RegistrarConsumoSaida') {
                $("#hdfComando").val("ATUALIZAR_GRID_ITENS");
                post();
            }
                    
            if ( $(e.target).attr("href") == '#Lacrar') {
                $("#hdfComando").val("ATUALIZAR_DADOS_LACRACAO");
                post();
            }
        });

        $('#myTab a').click(function(e) {
            e.preventDefault();
            $(this).tab('show');
        });

        function pageLoad() {
                    
            $("#ddlLoteMatComCodigo").chosen({
                disable_search_threshold: 10,
                no_results_text: "Lote não encontrado!",
                width: "95%"
            });

            $("#txtCodMaterial").popover({
                html: true,
                trigger: 'hover'
            });
                    
            $("#txtCodigoMaterial").popover({
                html: true,
                trigger: 'hover'
            });

//                    $('#txtRegCliente').popover('destroy');
//                    
//                    $("#txtRegCliente").popover({
//                        html: true,
//                        trigger: 'hover'
//                    });
                    
            $("#txtValidadeManual").mask("99/99/9999");
            $("#txtValidadeItemSemCodigo").mask("99/99/9999");

            $("#txtCodMaterial").on("keyup", function(event) {
                if (event.keyCode == 8 || event.keyCode == 46) {
                    $("#txtNomeMaterialComCodigo").val("");
                    $("#txtUnidMedMaterialComCodigo").val("");
                    $("#txtUnidMedMaterialComCodigo").val("");

                    $("#ddlLoteMatComCodigo").find('option').remove();
                            
                    $("#hdfSeqItemListaControle").val("");
                }
            });


            $('.exibirAlerta').tooltip({placement:'left',container: 'body'});
        }
                
        $(document).keypress(function (e) {
            if (e.which == 13) {
                        
                if (document.activeElement.id == "txtCodigoMaterial" || document.activeElement.id == "txtQuantidadeMaterialComCodigo") {
                    // $("#hdnCodMaterial").val($("#txtCodigoMaterial").val());
                    $("#<%= btnBuscarMaterial.ClientID %>").click();
                }
                        
                if (document.activeElement.id == "txtCodMaterial") {
                    // $("#hdnCodMaterial").val($("#txtCodigoMaterial").val());
                    $("#<%= btnBuscaMaterialRep.ClientID %>").click();
                            
                    //setTimeout(function () {$('#txtCodigoMaterial').focus();},2000);
                            
                    //setTimeout(function(){ $('#txtCodMaterial').focus();}, 2000);
                    //setTimeout(function(){ alert('fdsfsdfsd');}, 2000);
                }
                e.preventDefault();
            }
        });

//                function informarLoteManual() {
//                    $("#divLoteManual").toggleClass("hidden");
//                    
//                    $("#divLoteAutomatico").toggleClass("hidden");

//                    if ($("#divLoteManual").hasClass("hidden")) {
//                        $("#lblInformelote").text("Clique para informar o lote manualmente");
//                    } else {
//                        $("#lblInformelote").text("Clique aqui para selecionar um lote");
//                    }
//                    
//                }
    </script>
</asp:Content>
