<%@ Page Title="" Language="C#" MasterPageFile="~/Master_Antiga/Modal_Antiga.master"
    AutoEventWireup="true" CodeBehind="AcaoListaConferenciaEquipamento.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Conferencia.AcaoListaConferenciaEquipamento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">

        function retornoOperacao(comando, idCamporeceberValor) {
            window.parent.document.getElementById(idCamporeceberValor).value = comando;
            window.parent.post();
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCabecalho" runat="server">
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success">
        <div class="panel-heading">
            <h3 class="panel-title">
                Opções
            </h3>
        </div>
        <div class="panel-body">
            <div class="form-group" runat="server" Visible="False" id="trTesteEquipamento">
                <div class="col-sm-10">
                    <asp:Button ID="lnkBtnTesteEquipamento" runat="server" comando="INFORMAR_TESTE_EQUIPAMENTO" class="btn btn-success btn-lg btn-block" Text="Informar teste"
                        ToolTip="Registrar o teste do equipamento." OnClick="lnkBtnAcao_Click">
                    </asp:Button>
                </div>
            </div>
            
            <div class="form-group" id="trExcluir" runat="server" visible="true">
                <div class="col-sm-10">
                    <asp:Button ID="lnkBtnExcluir" runat="server" comando="EXCLUIR_EQUIPAMENTO" ToolTip="Exclui o equipamento." class="btn btn-success btn-lg btn-block" Text="Excluir"
                            OnClick="lnkBtnAcao_Click">
                        </asp:Button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
