<%@ Page Title="" Language="C#" MasterPageFile="~/Master_Antiga/Modal_Antiga.Master" AutoEventWireup="true" CodeBehind="QuebrarLacre.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Conferencia.QuebrarLacre" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">

        function retornoOperacao(comando, idCamporeceberValor) {
            window.parent.document.getElementById(idCamporeceberValor).value = comando;
            window.parent.post();
        }
    
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphCabecalho" runat="server">

    Motivo da quebra de lacre

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   <table cellpadding="0" cellspacing="3" width="100%">
        <tr>
            <td  class="Conteudo_legenda_campos1" style="width: 90px">                            
                Tipo Ocorrência:
            </td> 
            <td>
                <asp:DropDownList ID="ddlTipoOcorrencia" runat="server" Width="350px">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="reqFldTipoOcorrencia" 
                                            runat="server" 
                                            ErrorMessage="Tipo da ocorrência requerido." 
                                            Text="*"
                                            ControlToValidate="ddlTipoOcorrencia"
                                            InitialValue="0"
                                            ForeColor="Red"
                                            ValidationGroup="validacao"
                                            ></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
          <td colspan="2">
              <asp:CheckBox ID="chkgerarLacreProv" runat="server" 
                  Text="Gerar Lacre Provisório" Checked="true" AutoPostBack="True" 
                  oncheckedchanged="chkgerarLacreProv_CheckedChanged" />
          </td>
        </tr>
   </table>
   
   <asp:UpdatePanel ID="updPnTbOcorrencia" runat="server" UpdateMode="Conditional">
   <ContentTemplate>     
   
   <table cellpadding="0" cellspacing="3" width="100%" runat="server" id="tbOcorrencia" visible="false">
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
    </table>

    </ContentTemplate>
       <Triggers>
           <asp:AsyncPostBackTrigger ControlID="chkgerarLacreProv" 
               EventName="CheckedChanged" />
       </Triggers>
    </asp:UpdatePanel>

    <br />
    
    <asp:Button ID="btnSalvar" 
                        runat="server" 
                        Text="Salvar" 
                        CssClass="btn btn-success"
                        ValidationGroup="validacao"
                        CausesValidation="true"
                        Width="100px" onclick="btnSalvar_Click"  ></asp:Button>

</asp:Content>
