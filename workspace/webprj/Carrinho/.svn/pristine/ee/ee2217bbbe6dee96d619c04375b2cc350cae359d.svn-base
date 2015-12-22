<%@ Page Title="" Language="C#" MasterPageFile="~/Master/HcrpMaster.Master" AutoEventWireup="True" CodeBehind="login.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCabecalho" runat="server">
    AUTENTICAÇÃO NO SISTEMA
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphConteudo" runat="server">
    <center>
        <div id="div_listagem" runat="server" class="divConteudo" style="width:97%;">
			<h1 style="WIDTH: 97%; HEIGHT: 33px">
			    <ASP:Label id="lblTitulo" runat="server"></ASP:Label>
            </h1>
	        <br />
            <div style="width:300px;">
            <center>       
                <table width="99%">
                    <tr>
                        <td align="right">
                            Usuário:</td>
                        <td align="left">
                            <asp:TextBox ID="txt_login" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUsuario" runat="server" 
                                ControlToValidate="txt_login" Display="Dynamic" 
                                ErrorMessage="O nome do usuário é obrigatório" SetFocusOnError="True" 
                                ValidationGroup="validacao" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Senha:</td>
                        <td align="left">
                            <asp:TextBox ID="txt_senha" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSenha" runat="server" 
                                ControlToValidate="txt_senha" Display="Dynamic" 
                                ErrorMessage="A senha é obrigatória" SetFocusOnError="True" 
                                ValidationGroup="validacao" ForeColor="Red">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
		    
                <asp:Label ID="lbErro" runat="server" ForeColor="Red" 
                    Text="Usuário ou Senha incorreto. Verifique." Visible="False"></asp:Label>
                <br />
		    
                <br />
                <asp:Button ID="bt_Conectar" runat="server" CssClass="btn btn-success" 
                    Text="Conectar" onclick="bt_Conectar_Click" ValidationGroup="validacao" />  
                    <br /> 
                    <br /> 
               
                <br />
                </center> 
            </div>
		</div>
    </center>
</asp:Content>
