<%@ Page Title="" Language="C#" MasterPageFile="~/Master_Antiga/Modal_Antiga.master" AutoEventWireup="true" CodeBehind="RegistrarOcorrencia.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Conferencia.RegistrarOcorrencia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script language="javascript" type="text/javascript">

         function retornoOperacao(comando, idCamporeceberValor) {
             window.parent.document.getElementById(idCamporeceberValor).value = comando;
             window.parent.post();
         }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCabecalho" runat="server">

    Registrar Ocorrência

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table cellpadding="0" cellspacing="3" width="100%">
        <tr>
            <td>                            
                <span><strong>Registrar Ocorrência:</strong></span>
            </td>  
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtDscOcorrencia" 
                                runat="server" Width="450px" 
                                Height="70px" 
                                TextMode="MultiLine" 
                                CssClass="textarea"
                                ClientIDMode="Static"
                                MaxLength="500"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="reqFldOcorrencia" 
                                                runat="server" 
                                                ErrorMessage="Descrição da ocorrência requerida." 
                                                ValidationGroup="validacao" 
                                                Text="*"
                                                ForeColor="Red"
                                                ControlToValidate="txtDscOcorrencia"></asp:RequiredFieldValidator>
                             
                             
            </td>  
        </tr>   
        <tr>
            <td>
                <asp:Button ID="btnAdicionaOcorrencia" 
                            runat="server" 
                            Text="Registrar" 
                            CssClass="btn btn-success"
                            ValidationGroup="validacao"
                            CausesValidation="true"
                            Width="100px" onclick="btnAdicionaOcorrencia_Click" ></asp:Button>
            </tr>             
    </table>

</asp:Content>
