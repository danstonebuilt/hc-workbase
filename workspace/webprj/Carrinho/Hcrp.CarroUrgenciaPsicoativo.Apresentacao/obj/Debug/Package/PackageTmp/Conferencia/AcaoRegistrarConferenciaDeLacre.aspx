<%@ Page Title="" Language="C#" MasterPageFile="~/Master_Antiga/Modal_antiga.Master"
    AutoEventWireup="true" CodeBehind="AcaoRegistrarConferenciaDeLacre.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Conferencia.AcaoRegistrarConferenciaDeLacre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">

        function retornoOperacao(comando, idCamporeceberValor) {
            window.parent.document.getElementById(idCamporeceberValor).value = comando;
            window.parent.document.getElementById("btnPost").click();
            //window.parent.post();
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCabecalho" runat="server">
    <h3>Selecione a opção</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upp" runat="server" ClientIDMode="Static" UpdateMode="Always">
        <ContentTemplate>
            <div class="form-group" id="trNovaLacracao" runat="server" visible="false">
                <asp:LinkButton ID="lnkBtnNovaLacracao" runat="server" comando="NOVA_LACRACAO" ToolTip="Realizar nova lacração." class="btn btn-success btn-lg btn-block"
                    OnClick="lnkBtnAcao_Click">Nova lacração
                </asp:LinkButton>
            </div>
            <div class="form-group" id="trExcluir" runat="server" visible="false">
                <asp:LinkButton ID="lnkBtnExcluir" runat="server" comando="EXCLUIR" ToolTip="Exclui o carrinho." class="btn btn-success btn-lg btn-block"
                    OnClick="lnkBtnAcao_Click">Excluir
                </asp:LinkButton>
            </div>
            <div class="form-group" id="trVisualizar" runat="server" visible="false">
                <asp:LinkButton ID="lnkBtnVisualizar" runat="server" comando="VISUALIZAR" ToolTip="Visualizar do Carrinho." class="btn btn-success btn-lg btn-block"
                    OnClick="lnkBtnAcao_Click">Visualizar
                </asp:LinkButton>
            </div>
            <div class="form-group" id="trConferencia" runat="server" visible="false">
                <asp:LinkButton ID="lnkBtnConferencia" runat="server" comando="CONFERENCIA" ToolTip="Conferência do Carrinho." class="btn btn-success btn-lg btn-block"
                    OnClick="lnkBtnAcao_Click">Conferência
                </asp:LinkButton>
            </div>
            <div class="form-group" id="trEditar" runat="server" visible="false">
                <asp:LinkButton ID="lnkBtnEditar" runat="server" comando="EDITAR" ToolTip="Edição do Carrinho." class="btn btn-success btn-lg btn-block"
                    OnClick="lnkBtnAcao_Click">Editar
                </asp:LinkButton>
            </div>
            <div class="form-group" id="trRegistrarOcorrencia" runat="server" visible="false">
                <asp:LinkButton ID="lnkBtnRegistrarOcorrencia" runat="server" comando="REGISTRAR_OCORRENCIA" class="btn btn-success btn-lg btn-block"
                    ToolTip="Registrar ocorrência do Carrinho." OnClick="lnkBtnAcao_Click">Registrar Ocorrência
                </asp:LinkButton>
            </div>
            <div class="form-group" id="trQuebrarLacre" runat="server" visible="false">
                <asp:LinkButton ID="lnkBtnQuebrarLacre" runat="server" comando="QUEBRAR_LACRE" ToolTip="Quebrar lacre do carrinho." class="btn btn-success btn-lg btn-block"
                    OnClick="lnkBtnAcao_Click">Quebrar Lacre
                </asp:LinkButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="form-group">
        <div class="col-sm-12 pull-right">
            <asp:Button ID="btnSair" runat="server" Text="Sair" CausesValidation="true" CssClass="btn btn-success"
                Width="70px" OnClientClick="window.parent.$.fancybox.close(); return false;"
                ToolTip="Desistir da operação e retornar a tela anterior."></asp:Button>
        </div>
    </div>
</asp:Content>
