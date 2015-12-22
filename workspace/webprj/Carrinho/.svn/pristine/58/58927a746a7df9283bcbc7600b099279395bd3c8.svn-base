<%@ Page Title="" Language="C#" MasterPageFile="~/Master/HcrpMaster.Master" AutoEventWireup="true"
    CodeBehind="TrocaDeMaterial.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.TrocaMaterial.TrocaDeMaterial" %>

<%@ Register Assembly="ASPnetPagerV2_8" Namespace="ASPnetControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphCabecalho" runat="server">
    Troca de Materiais
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphConteudo" runat="server">
    <div class="alert alert-warning">
        <strong>Vencimento dos itens:&nbsp;<asp:Label ID="lblDiasvencimento" runat="server" Style="color: red;"></asp:Label>&nbsp;dias</strong>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">
            Instituto</label>
        <div class="col-sm-3">
            <asp:DropDownList ID="ddlInstituto" runat="server" Width="250px" class="form-control" disabled
                data-toggle="dropdown" AutoPostBack="True" OnSelectedIndexChanged="ddlInstituto_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">
            Repositório</label>
        <div class="col-sm-10">
            <asp:UpdatePanel ID="updPnPatrimonio" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlPatrimonio" runat="server" AutoPostBack="True" ClientIDMode="Static"
                        class="form-control" data-toggle="dropdown" OnSelectedIndexChanged="ddlPatrimonio_SelectedIndexChanged">
                    </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlInstituto" EventName="SelectedIndexChanged" />
                    <asp:PostBackTrigger ControlID="ddlPatrimonio" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:Button ID="btnImprimir" runat="server" Text="Imprimir" CssClass="btn btn-success"
        Width="140px" OnClick="btnImprimir_Click"></asp:Button>
    <asp:Button ID="btnRequisitarMaterial" runat="server" Text="Requisitar medicamentos"
        CssClass="btn btn-success hidden"  
        onclick="btnRequisitarMaterial_Click"></asp:Button>
    <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="btn btn-success"
        Width="140px" OnClick="btnVoltar_Click"></asp:Button>
    <br />
    <br />
    <asp:GridView runat="server" ID="grvItem" Width="100%" AutoGenerateColumns="False"
        CssClass="table table-bordered table-condensed" BorderWidth="0" AllowSorting="False"
        EmptyDataText="Nenhum registro foi localizado." OnRowDataBound="grvItem_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="Patrimônio">
                <ItemTemplate>
                    <%# Eval("LacreRepositorio.RepositorioListaControle.BemPatrimonial.DscTipoPatrimonio")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Identif. Carrinho">
                <ItemTemplate>
                    <%# Eval("LacreRepositorio.RepositorioListaControle.DscIdentificacao")%>
                </ItemTemplate>
            </asp:TemplateField>
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
            <asp:TemplateField HeaderText="Lote">
                <ItemTemplate>
                    <%# Eval("NumLoteFabricante")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Qtd. Necessária">
                <ItemTemplate>
                    <%# Eval("ItensListaControle.QuantidadeNecessaria")%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Qtd. Vencendo">
                <ItemTemplate>
                    <%# Eval("QtdVencendo")%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Validade">
                <ItemTemplate>
                    <asp:Label ID="lblDataVldLote" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <cc2:PagerV2_8 Width="100%" ID="paginador" runat="server" BackToFirstClause="Ir para última Página"
        BackToPageClause="Voltar para Página" GoClause="Ir" GoToLastClause="Voltar para última Página"
        NextToPageClause="Próxima Página" GenerateGoToSection="false" GeneratePagerInfoSection="false"
        ShowingResultClause="Mostrando Resultados" ShowResultClause="Mostrar Resultado"
        OnCommand="paginador_Command" ToClause="para" Visible="false" />
        
    <script language="javascript" type="text/javascript">
        function pageLoad() {

            $("#ddlPatrimonio").chosen({
                disable_search_threshold: 10,
                no_results_text: "Repositório não encontrado!",
                width: "95%"
            });
        }
    </script>
</asp:Content>
