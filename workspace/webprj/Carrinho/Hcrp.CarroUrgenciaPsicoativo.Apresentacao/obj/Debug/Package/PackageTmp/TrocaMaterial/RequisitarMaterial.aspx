<%@ Page Title="" Language="C#" MasterPageFile="~/Master_Antiga/Modal_Antiga.master"
    AutoEventWireup="true" CodeBehind="RequisitarMaterial.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.TrocaMaterial.RequisitarMaterial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCabecalho" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title">
                Requisição de Material
            </h3>
        </div>
        <div class="panel-body">
            <div class="form-group">
               REPOSITORIO
            </div>
        </div>
    </div>--%>
    <div class="modal-header">
        <h3 class="modal-title">
            Requisitar itens para repositório</h3>
    </div>
    <asp:UpdatePanel runat="server" ID="uppPaciente" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rblTipoItem" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
            <div class="modal-body">
                <asp:Panel runat="server" ID="pnlItens">
                    <div class="form-group">
                        <div class="col-sm-10">
                            <asp:RadioButtonList runat="server" ID="rblTipoItem" AutoPostBack="True" OnSelectedIndexChanged="rblTipoItem_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="1">Medicamentos</asp:ListItem>
                                <asp:ListItem Value="2">Soro</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12">
                            <asp:GridView runat="server" ID="grvItem" Width="100%" AutoGenerateColumns="False"
                                BorderWidth="0" CssClass="table table-bordered table-condensed" AllowSorting="False"
                                EmptyDataText="Nenhum registro foi localizado.">
                                <Columns>
                                    <%-- <asp:TemplateField HeaderText="Patrimônio">
                                <ItemTemplate>                                                
                                    <%# Eval("LacreRepositorio.RepositorioListaControle.BemPatrimonial.DscTipoPatrimonio")%>
                                </ItemTemplate>                                
                            </asp:TemplateField>--%>
                                    <%-- <asp:TemplateField HeaderText="Identif. Carrinho">
                                <ItemTemplate>                                                
                                    <%# Eval("LacreRepositorio.RepositorioListaControle.DscIdentificacao")%>
                                </ItemTemplate>                                
                            </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Código Material">
                                        <ItemTemplate>
                                            <%# Eval("ItensListaControle.Material.Codigo")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descrição">
                                        <ItemTemplate>
                                            <%# Eval("ItensListaControle.Material.Nome")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Lote">
                                    <ItemTemplate>                                                
                                         <%# Eval("NumLoteFabricante")%>
                                    </ItemTemplate>                
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Qtd.<br /> Necessária">
                                        <ItemTemplate>
                                            <%# Eval("ItensListaControle.QuantidadeNecessaria")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qtd. a requisitar">
                                        <ItemTemplate>
                                            <%# Eval("QtdVencendo")%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Validade">
                                    <ItemTemplate>                                                                    
                                        <asp:Label ID="lblDataVldLote" runat="server" ></asp:Label>
                                    </ItemTemplate>                     
                                </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlRequisicaGerada" Visible="False" Height="200px">
                    <h2><asp:Label Height="16px" runat="server" ID="lblRequisicaoGerada" ></asp:Label></h2> 
                </asp:Panel>
                <br />
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnRequisitar" CausesValidation="true" runat="server" Text="Requisitar"
                    CssClass="btn btn-success" Width="100px" OnClick="btnRequisitar_Click"></asp:Button>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
