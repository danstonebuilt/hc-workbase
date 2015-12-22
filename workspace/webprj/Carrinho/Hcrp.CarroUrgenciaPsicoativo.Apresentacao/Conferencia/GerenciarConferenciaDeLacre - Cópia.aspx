<%@ Page Title="" Language="C#" MasterPageFile="~/Master_Antiga/HcrpMaster.Master"
    AutoEventWireup="true" CodeBehind="GerenciarConferenciaDeLacre.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Conferencia.GerenciarConferenciaDeLacre" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style>
        .ajax__tab_tab
        {
            height: 21px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphCabecalho" runat="server">
    Lista conferência / Controle de lacre
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphConteudo" runat="server">
    <asp:UpdatePanel runat="server" ID="upp">
        <ContentTemplate>
            <asp:HiddenField ID="hdfComando" runat="server" />
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
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">
                                Motivo do Rompimento:</label>
                            <div class="col-sm-3">
                                <asp:TextBox ID="lblMotivoRompimento" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </div>
            
            <ul class="nav nav-tabs">
              <li class="active"><a href="#home" data-toggle="tab">Home</a></li>
              <li><a href="#profile" data-toggle="tab">Profile</a></li>
              <li><a href="#dropdown1" data-toggle="tab">Messages</a></li>
            </ul>
            <div id="myTabContent" class="tab-content">
              <div class="tab-pane fade" id="home">
                <p>Raw denim you probably haven't heard of them jean shorts Austin. Nesciunt tofu stumptown aliqua, retro synth master cleanse. Mustache cliche tempor, williamsburg carles vegan helvetica. Reprehenderit butcher retro keffiyeh dreamcatcher synth. Cosby sweater eu banh mi, qui irure terry richardson ex squid. Aliquip placeat salvia cillum iphone. Seitan aliquip quis cardigan american apparel, butcher voluptate nisi qui.</p>
              </div>
              <div class="tab-pane fade" id="profile">
                <p>Food truck fixie locavore, accusamus mcsweeney's marfa nulla single-origin coffee squid. Exercitation +1 labore velit, blog sartorial PBR leggings next level wes anderson artisan four loko farm-to-table craft beer twee. Qui photo booth letterpress, commodo enim craft beer mlkshk aliquip jean shorts ullamco ad vinyl cillum PBR. Homo nostrud organic, assumenda labore aesthetic magna delectus mollit. Keytar helvetica VHS salvia yr, vero magna velit sapiente labore stumptown. Vegan fanny pack odio cillum wes anderson 8-bit, sustainable jean shorts beard ut DIY ethical culpa terry richardson biodiesel. Art party scenester stumptown, tumblr butcher vero sint qui sapiente accusamus tattooed echo park.</p>
              </div>
              <div class="tab-pane fade" id="dropdown1">
                <p>Etsy mixtape wayfarers, ethical wes anderson tofu before they sold out mcsweeney's organic lomo retro fanny pack lo-fi farm-to-table readymade. Messenger bag gentrify pitchfork tattooed craft beer, iphone skateboard locavore carles etsy salvia banksy hoodie helvetica. DIY synth PBR banksy irony. Leggings gentrify squid 8-bit cred pitchfork. Williamsburg banh mi whatever gluten-free, carles pitchfork biodiesel fixie etsy retro mlkshk vice blog. Scenester cred you probably haven't heard of them, vinyl craft beer blog stumptown. Pitchfork sustainable tofu synth chambray yr.</p>
              </div>
            </div>

            <cc1:TabContainer ID="TabContainerAba" runat="server" ActiveTabIndex="0" Width="100%"
                Visible="true" OnActiveTabChanged="TabContainerAba_ActiveTabChanged" AutoPostBack="true" >
                <cc1:TabPanel runat="server" HeaderText="Registrar Consumo/Saída" ID="tbConsumoSaida">
                    <ContentTemplate>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    Atendimento paciente</h3>
                            </div>
                            <div id="divPaciente" class="panel-body">
                                <div class="form-group">
                                    <div class="col-sm-4">
                                        <div class="input-group input-group-sm">
                                            <span class="input-group-addon">* Registro:</span>
                                            <asp:TextBox ID="txtRegCliente" runat="server" class="form-control" MaxLength="8"
                                                AutoPostBack="True" OnTextChanged="txtRegCliente_TextChanged"></asp:TextBox>
                                            <%--<asp:ImageButton ID="ibtnBuscaPaciente"
                                                    runat="server" AlternateText="Clique aqui para realizar a pesquisa." ImageAlign="AbsMiddle"
                                                    CssClass="Hand" ImageUrl="~/InterfaceHC/Imagens/Bt_lupa.gif" Width="15px" Height="15px"
                                                    CausesValidation="False" Style="margin-left: 3px; border: 0px; padding: 0px"
                                                    OnClick="ibtnBuscaPaciente_Click"></asp:ImageButton>--%>
                                            <span class="input-group-btn">
                                                <button type="button" class="btn btn-default" runat="server" onclick="ibtnBuscaPaciente_Click">
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
                                            <asp:TextBox ID="lblAtendimento" runat="server" class="form-control"
                                                disabled></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="alert alert-info" runat="server" id="divlblMsgOperacaoConsumoSaida" clientidmode="Static">
                                    <asp:Label ID="lblMsgOperacaoConsumoSaida" runat="server" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    Itens do repositório</h3>
                            </div>
                            <div class="panel-body">
                                <div style="overflow: auto; width: 100%; height: 450px;" class="table-responsive">
                                    <asp:GridView runat="server" ID="grvItemConsumoSaida" Width="97%" AutoGenerateColumns="False"
                                        class="table table-bordered table-condensed" BorderWidth="0px" EmptyDataText="Nenhum registro foi localizado."
                                        OnRowDataBound="grvItemConsumoSaida_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Alínea">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAlinea" runat="server" Text='<%# Eval("ItensListaControle.Alinea.Nome")%>'></asp:Label><asp:HiddenField
                                                        ID="hdfSeqLacreRepositorioItens" runat="server" Value='<%# Eval("SeqLacreRepositorioItens")%>' />
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
                                                    <asp:TextBox ID="txtQtdNecessaria" runat="server" Enabled="false" Text='<%# Eval("ItensListaControle.QuantidadeNecessaria")%>'
                                                        Width="50px" CssClass="form-control input-sm"></asp:TextBox></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qtd.<br />Disponível">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQtdDisponivel" runat="server" Text='<%# Eval("QtdDisponivel")%>'></asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qtd.<br />Utilizada">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtUtilizada" runat="server" Text='<%# Eval("QtdUtilizada")%>' Width="50px"
                                                        CssClass="form-control input-sm" MaxLength="5"></asp:TextBox></ItemTemplate>
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
                                                    <tr id="trJustificativaSemAtendimento" runat="server" class="alert alert-warning">
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
                                                    </tr>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <br />
                        <span class="Conteudo_legenda_campos1">
                            <asp:CheckBox ID="chkGerarRequisicaoDeReposicaoAutomaticamente" runat="server" Text="Gerar requisição de reposição automaticamente"
                                class="hidden" />
                        </span>
                        <br />
                        <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CssClass="btn btn-success"
                            Width="100px" OnClick="btnImprimir_Click"></asp:Button>
                        <asp:Button ID="btnSalvarConsumoSaida" ValidationGroup="regConsumoSaida" runat="server"
                            Text="Salvar" CssClass="btn btn-success" Width="100px" OnClick="btnSalvarConsumoSaida_Click">
                        </asp:Button>
                        <asp:Button ID="btnVoltarRegConsSaida" runat="server" Text="Voltar" CausesValidation="False"
                            CssClass="btn btn-success" Width="100px" OnClick="btnVoltarRegConsSaida_Click">
                        </asp:Button>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" HeaderText="Informar Reposição" ID="tbInformarReposicao">
                    <ContentTemplate>
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
                                                <asp:TextBox ID="txtCodMaterial" runat="server" MaxLength="8" AutoPostBack="True"
                                                    CssClass="form-control" OnTextChanged="txtCodMaterial_TextChanged"></asp:TextBox>
                                                <%--<asp:ImageButton ID="ibtnBuscaPaciente"
                                                        runat="server" AlternateText="Clique aqui para realizar a pesquisa." ImageAlign="AbsMiddle"
                                                        CssClass="Hand" ImageUrl="~/InterfaceHC/Imagens/Bt_lupa.gif" Width="15px" Height="15px"
                                                        CausesValidation="False" Style="margin-left: 3px; border: 0px; padding: 0px"
                                                        OnClick="ibtnBuscaPaciente_Click"></asp:ImageButton>--%>
                                                <span class="input-group-btn">
                                                    <button id="Button1" type="button" class="btn btn-default" runat="server" onclick="imgBtnBuscaMaterial_Click">
                                                        <span class="glyphicon glyphicon-search"></span>
                                                    </button>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-8">
                                            <div class="input-group input-group-sm">
                                                <span class="input-group-addon">Nome</span>
                                                <asp:TextBox ID="txtNomeMaterialComCodigo" runat="server" class="form-control" disabled></asp:TextBox>
                                                <asp:HiddenField ID="hdfSeqItemListaControle" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-7">
                                            <div class="input-group input-group-sm">
                                                <span class="input-group-addon">Unidade de Medida:</span>
                                                <asp:TextBox ID="txtUnidMedMaterialComCodigo" runat="server" disabled CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <label class="col-sm-1 control-label">
                                            Lote:</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="ddlLoteMatComCodigo" runat="server" class="form-control input-sm"
                                                data-toggle="dropdown">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqFldLoteMatComCod" runat="server" ControlToValidate="ddlLoteMatComCodigo"
                                                Display="Dynamic" ErrorMessage="Lote requerido." ForeColor="Red" InitialValue="0"
                                                SetFocusOnError="True" ValidationGroup="validacao" Enabled="False">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnMaterialSemCodigo" runat="server" Visible="False">
                                    <div class="form-group">
                                        <label class="col-sm-1 control-label">
                                            Item:</label>
                                        <div class="col-sm-8">
                                            <asp:DropDownList ID="ddlItemMatSemCodigo" runat="server" class="form-control input-sm"
                                                data-toggle="dropdown">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="reqFldMatSemCod" runat="server" ControlToValidate="ddlItemMatSemCodigo"
                                                Display="Dynamic" ErrorMessage="Material requerido." ForeColor="Red" InitialValue="0"
                                                SetFocusOnError="True" ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                        </div>
                                </asp:Panel>
                                <div class="form-group">
                                    <label class="col-sm-1 control-label">
                                        Quantidade:</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtQtdMat" runat="server" MaxLength="5" Width="70px" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:CompareValidator ID="compVldQtdMatSemCodigo" runat="server" ControlToValidate="txtQtdMat"
                                            Display="Dynamic" ErrorMessage="Quantidade inválido." ForeColor="Red" Operator="DataTypeCheck"
                                            SetFocusOnError="True" Type="Integer" ValidationGroup="validacao">*</asp:CompareValidator>
                                        <asp:RequiredFieldValidator ID="reqFldQtdMatSemCodigo" runat="server" ControlToValidate="txtQtdMat"
                                            Display="Dynamic" ErrorMessage="Quantidade requerido." ForeColor="Red" SetFocusOnError="True"
                                            ValidationGroup="validacao">*</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-5">
                                        <asp:Button ID="btnAdicionaMaterial" runat="server" Text="Adicionar" CssClass="btn btn-success"
                                            Width="100px" OnClick="btnAdicionaMaterial_Click" ValidationGroup="validacao">
                                        </asp:Button>
                                    </div>
                                </div>
                                <div style="overflow: auto; width: 100%; height: 450px;" class="table-responsive">
                                    <asp:GridView runat="server" ID="grvItemInformarReposicao" Width="100%" AutoGenerateColumns="False"
                                        class="table table-bordered table-condensed" BorderWidth="0px" EmptyDataText="Nenhum registro foi localizado."
                                        OnRowCommand="grvItemInformarReposicao_RowCommand" OnRowDataBound="grvItemInformarReposicao_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Alínea">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAlinea" runat="server" Text='<%# Eval("ItensListaControle.Alinea.Nome")%>'></asp:Label><asp:HiddenField
                                                        ID="hdfSeqLacreRepositorioItens" runat="server" />
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
                                                    <asp:Label ID="lblValidade" runat="server" Visible="false"></asp:Label></ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
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
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" HeaderText="Equipamento" ID="tbEquipamento">
                    <ContentTemplate>
                        <div class="form-group">
                            <label class="control-label col-sm-2">
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
                                    <asp:TextBox ID="txtDscPatrimonio" runat="server" disabled Height="30px" CssClass="form-control"></asp:TextBox>
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
                                            <asp:Label ID="txtDescricao" runat="server" 
                                                Text='<%# Eval("BemPatrimonial.DscComplementar")%>'></asp:Label></ItemTemplate>
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
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" HeaderText="Registrar Ocorrência" ID="tbRegOcorrencia">
                    <ContentTemplate>
                        <div class="form-group">
                            <h3 class="col-sm-4">
                                Registrar Ocorrência</h3>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12">
                                <asp:TextBox ID="txtDscOcorrencia" runat="server" TextMode="MultiLine" CssClass="form-control"
                                    ClientIDMode="Static" MaxLength="500"></asp:TextBox><asp:RequiredFieldValidator ID="reqFldOcorrencia"
                                        runat="server" ErrorMessage="Descrição da ocorrência requerida." ValidationGroup="ocorrencia"
                                        Text="*" ForeColor="Red" ControlToValidate="txtDscOcorrencia"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-12">
                                <asp:Button ID="btnAdicionaOcorrencia" runat="server" Text="Adicionar" CssClass="btn btn-success"
                                    ValidationGroup="ocorrencia" CausesValidation="true" Width="100px" OnClick="btnAdicionaOcorrencia_Click">
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
                </cc1:TabPanel>
                <cc1:TabPanel runat="server" HeaderText="Lacrar" ID="tbLacrar">
                    <ContentTemplate>
                        <div visible="False" class="col-sm-12 alert alert-danger" style="height: 33px; padding-top: 5px;"
                            runat="server" id="divInfoPodeLacrar" clientidmode="Static">
                            <strong>Voce não possui permissão para lacrar !</strong>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">
                                Número do lacre:</label>
                            <div class="col-sm-3 ">
                                <asp:TextBox ID="txtNumLacre" runat="server" Width="80px" MaxLength="8" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">
                                Número do lacre da caixa de intubação:</label>
                            <div class="col-sm-3 ">
                                <asp:TextBox ID="txtNumCaixaIntubacao" runat="server" Width="80px" MaxLength="8"
                                    CssClass="form-control"></asp:TextBox>
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
                        <br />
                        <div class="alert alert-warning" runat="server" runat="server" id="tbDscOcorrenciaLacrarComValidadeForaPeriodo"
                            visible="False">
                            <label>
                                Motivo de lacração com validade dos itens à vencer
                            </label>
                            <asp:TextBox ID="txtDscOcorrenciaLancarComItensAVencer" runat="server" CssClass="form-control"
                                MaxLength="2000" TextMode="MultiLine"></asp:TextBox>
                        </div>
                        <br />
                        <asp:Button ID="btnLacrar" runat="server" Text="Lacrar" CssClass="btn btn-success"
                            Width="100px" OnClick="btnLacrar_Click"></asp:Button>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <script type="text/javascript" language="javascript">

                function pageLoad() {
                    function post() {
                        <%=GetPostGrid() %>
                    }

                    $('#myTab a').click(function(e) {
                        e.preventDefault()
                        $(this).tab('show')
                    });
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
