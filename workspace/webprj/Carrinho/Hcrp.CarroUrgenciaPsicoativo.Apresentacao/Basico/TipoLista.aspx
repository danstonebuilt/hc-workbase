<%@ Page Title="" Language="C#" MasterPageFile="~/Master/HcrpMaster.Master" AutoEventWireup="true"
    CodeBehind="TipoLista.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Basico.TipoLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphCabecalho" runat="server">
    Tipo de lista para conferência
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphConteudo" runat="server">
    
    <asp:UpdatePanel runat="server" ID="upp">
        <ContentTemplate>
            
            <div class="form-group">
                <label class="col-sm-2 control-label">
                    Instituto</label>
                <div class="col-sm-3">
                    <asp:DropDownList runat="server" ID="ddlInstituto" class="form-control" data-toggle="dropdown" style="max-width: 250px"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlInstituto_SelectedIndexChanged">
                    </asp:DropDownList>
                    
                    <label class="checkbox">
                        <asp:CheckBox runat="server" ID="chkFiltraInativo" Text="Listar inativas ?" AutoPostBack="True" 
                        oncheckedchanged="chkFiltraInativo_CheckedChanged" />
                    </label>
                </div>
            </div>

            <asp:Panel runat="server" ID="pnlTipoLista" Visible="false">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Adicionar</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group" >
                            <label class="col-sm-2 control-label" id="lnome" for="nome">Nome</label>
                            <div class="col-sm-10">
                                <asp:TextBox runat="server" type="text" class="form-control" ID="txtNome" name="nome" style="max-width: 400px"
                                    MaxLength="50" placeholder="Digite o nome da lista" ClientIDMode="Static" > </asp:TextBox>
                            </div>    
                        
                            <div class="col-sm-offset-2 col-sm-10 ">
                                <label class="checkbox" style="display: none">
                                    <asp:CheckBox runat="server" ID="chkPossuiCaixa" Text="Possui caixa de intubação" />
                                </label>
                                <asp:Button ID="btnAdicionar" CausesValidation="true" runat="server" Text="Adicionar" validar
                                    ToolTip="Adicionar" class="btn btn-success" Width="100px" 
                                     OnClick="btnAdicionar_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="table-responsive">
                <asp:GridView runat="server" ID="grvItem" Width="100%" AutoGenerateColumns="False"
                    BorderWidth="0" AllowSorting="false" CssClass="table table-hover table-bordered"
                    EmptyDataText="Nenhum registro foi localizado.">
                    <Columns>
                        <asp:TemplateField HeaderText="Nome" HeaderStyle-Width="40%">
                            <ItemTemplate>
                                <%# Eval("NomeListaControle")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Instituto">
                            <ItemTemplate>
                                <%# Eval("Instituto.NomeInstituto")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Ativo">
                            <ItemTemplate>
                                <asp:Label ID="lblAtivo" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ação">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkAtivarOuInativar" OnClick="lnkAtivarOuInativar_Click" CssClass="btn btn-default btn-sm pegatooltip" data-toggle="tooltip" data-placement="top"
                                    CommandArgument='<%# Eval("SeqListaControle")%>' ClientIDMode="Static"></asp:LinkButton>
                                <asp:HiddenField runat="server" ID="hdfAtivo" Value='<%# Eval("IdfAtivo")%>' />
                            </ItemTemplate>
                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerTemplate>
                        <div id="page-selection" class="pagination"></div>
                    </PagerTemplate>
                </asp:GridView>
                </div>
            </asp:Panel>
            <div style="margin-top: 15px">
                <asp:Button ID="btnVoltar" CausesValidation="false" runat="server" Text="Voltar" ClientIDMode="Static" data-toggle="tooltip" title="Voltar para o menu"
                    CssClass="btn btn-success cancel" Width="100px" OnClick="btnVoltar_Click"></asp:Button>
                    <%--Para que o validate de campo obrigatórios na valide o postback do botão voltar é ´necessario colocar a classe:cancel no botão--%>
            </div>
            <script type="text/javascript" language="javascript">

                function pageLoad() {

                    $('.pegatooltip').tooltip();

                    // inicializa a dropdown
                    $('.dropdown-toggle').dropdown();

                    // inicializa a validacao
                    //$("#formIsValid").val("false");
                    $('#form1').validate();

                    // Inicializa o tooltip
                    $("#btnVoltar").tooltip();
                }
            
                // Necessario para para parar o postback quando algum campo obrigatório nao estiver preenchido
                Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(CheckValidationStatus);
            
                function CheckValidationStatus(sender, args) {
                    var isValid = $("#form1").valid();
                    //if ($("#formIsValid").val() == "false") { 
                    if (!isValid && ($(args._postBackElement).attr('validar') != null)) {
                        args.set_cancel(true); 
                        return false;
                    } else {
                        var validator = $("#form1").validate();
                        validator.resetForm();
                    }
                }

            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
