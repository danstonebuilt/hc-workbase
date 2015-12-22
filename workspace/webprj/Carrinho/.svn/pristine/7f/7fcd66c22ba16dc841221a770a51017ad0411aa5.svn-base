<%@ Page Title="" Language="C#" MasterPageFile="~/Master_Antiga/Modal_Antiga.master"
    AutoEventWireup="true" CodeBehind="BuscaPatrimonio.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Comum.BuscaPatrimonio" %>

<%@ Register Assembly="ASPnetPagerV2_8" Namespace="ASPnetControls" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function setarValor(id_campo_codigo, valor_do_campo_codigo, id_campo_descricao, valor_do_campo_descricao) {

            window.parent.document.getElementById(id_campo_codigo).value = valor_do_campo_codigo;

            if (id_campo_descricao != '')
                window.parent.document.getElementById(id_campo_descricao).value = valor_do_campo_descricao;

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCabecalho" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title">
                Informe os filtros para pesquisa
            </h3>
        </div>
        <div class="panel-body">
            <asp:Panel runat="server" ID="pnlFocus" DefaultButton="ibtnBusca">
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                                Nº Patrimônio:</label>
                    <div class="col-sm-7">
                        <asp:TextBox ID="txtPatrimonio" runat="server"  MaxLength="8" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                                Descrição complementar:</label>
                    <div class="col-sm-7">
                        <asp:TextBox ID="txtDscComplementar" runat="server"  MaxLength="30"
                            CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-12">
                        <asp:Button ID="ibtnBusca" runat="server" Text="Pesquisar"
                                CssClass="btn btn-success" Width="100px" OnClick="btBuscar_Click">
                            </asp:Button>
                    </div>
                </div>
            </asp:Panel>
            <br />
            <i>Clique sob o equipamento para selecioná-lo.</i>
            <asp:GridView runat="server" ID="grvDado" Width="100%" AutoGenerateColumns="False"
                BorderWidth="0" AllowSorting="True" CssClass="table table-bordered table-condensed"
                EmptyDataText="Nenhum registro foi localizado.">
                <Columns>
                    <asp:TemplateField HeaderText="Nº Patrimônio">
                        <ItemTemplate>
                            <%# Eval("DscModelo")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Descrição complementar">
                        <ItemTemplate>
                            <%# Eval("DscComplementar")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Localização">
                        <ItemTemplate>
                            <%# Eval("DscComplementoLocalizacao")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div style="text-align: right; margin-top: 5px;">
                <asp:Label runat="server" ID="lblTotalRegistro"></asp:Label>
            </div>
            <cc2:PagerV2_8 Width="100%" ID="paginador" runat="server" BackToFirstClause="Ir para última Página"
                BackToPageClause="Voltar para Página" GoClause="Ir" GoToLastClause="Voltar para última Página"
                NextToPageClause="Próxima Página" GenerateGoToSection="false" GeneratePagerInfoSection="false"
                ShowingResultClause="Mostrando Resultados" ShowResultClause="Mostrar Resultado"
                OnCommand="paginador_Command" ToClause="para" Visible="false" />
        </div>
    </div>
</asp:Content>
