<%@ Page Title="" Language="C#" MasterPageFile="~/Master_Antiga/Modal_Antiga.master" AutoEventWireup="true" CodeBehind="BuscaPaciente.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Comum.BuscaPaciente" %>

<%@ Register assembly="ASPnetPagerV2_8" namespace="ASPnetControls" tagprefix="cc2" %>

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

    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="Tab_conteudo">
	    <tr>
		    <td class="Conteudo_legenda_grupo2">Informe o nome do paciente para pesquisa</td>
	    </tr>        
    </table>

    <asp:Panel runat="server" ID="pnlFocus" DefaultButton="ibtnBusca">
        <table width="500" border="0" cellpadding="0" cellspacing="0" class="Tab_conteudo">
            <tr>
                <td class="Conteudo_legenda_campos1"><asp:Label runat="server" ID="Label2" Text="Nome:"></asp:Label></td>
                <td><asp:TextBox id="txtNome" runat="server" Width="260" MaxLength="30" ></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td class="Conteudo_legenda_campos1"><asp:Label runat="server" ID="Label3" Text="Sobrenome:"></asp:Label></td>
                <td>
                    <asp:TextBox id="txtSobrenome" runat="server" Width="260px" MaxLength="30"></asp:TextBox>
                    <asp:ImageButton id="ibtnBusca" OnClick="btBuscar_Click" runat="server" alternatetext="Clique aqui para realizar a pesquisa" imagealign="AbsMiddle" cssclass="Hand" ImageUrl="~/InterfaceHC/Imagens/Bt_lupa.gif" width="15px" height="15px" style="margin-left: 3px; border:0px; padding:0px"></ASP:ImageButton>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <br />
    
    <i>Clique sob o nome do paciente para selecioná-lo.</i>

    <asp:GridView runat="server"
                ID="grvDado" Width="100%" 
                AutoGenerateColumns="False" 
                BorderWidth="0"
                AllowSorting="True"
                HeaderStyle-CssClass="Grid_cabecalho" 
                EmptyDataRowStyle-CssClass="Grid_linha" RowStyle-CssClass="Grid_linha"
                EmptyDataText="Nenhum registro foi localizado.">

        <Columns>

            <asp:TemplateField HeaderText="Registro Paciente">
                <ItemTemplate>
                    <%# Eval("RegistroPaciente")%>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Nome Completo">
                <ItemTemplate>                            
                    <%# Eval("NomeCompletoPaciente")%>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        
    </asp:GridView> 

    <div style="text-align:right; margin-top:5px;">
        <asp:Label runat="server" ID="lblTotalRegistro"></asp:Label>
    </div>

    <cc2:pagerv2_8 Width="100%" ID="paginador" runat="server" 
                    BackToFirstClause="Ir para última Página" BackToPageClause="Voltar para Página" 
                    GoClause="Ir" GoToLastClause="Voltar para última Página" 
                    NextToPageClause="Próxima Página" GenerateGoToSection="false" GeneratePagerInfoSection="false"
                    ShowingResultClause="Mostrando Resultados" 
                    ShowResultClause="Mostrar Resultado"
                    OnCommand="paginador_Command"
                    ToClause="para" Visible="false" /> 

</asp:Content>
