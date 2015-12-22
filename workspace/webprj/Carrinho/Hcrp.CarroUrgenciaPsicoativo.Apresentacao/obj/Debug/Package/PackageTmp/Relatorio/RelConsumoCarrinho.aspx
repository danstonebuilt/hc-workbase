<%@ Page Title="" Language="C#" MasterPageFile="~/Master/HcrpMaster.Master" AutoEventWireup="true" CodeBehind="RelConsumoCarrinho.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Master.RelConsumoCarrinho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCabecalho" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphConteudo" runat="server">
    
    <link type="text/css" rel="stylesheet" href="../Framework/jquery-ui-1.8.21.custom/css/custom-theme/minified/jquery.ui.core.min.css" />
    <link type="text/css" rel="stylesheet" href="../Framework/jquery-ui-1.8.21.custom/css/custom-theme/minified/jquery.ui.theme.min.css" />
    <link type="text/css" rel="stylesheet" href="../Framework/jquery-ui-1.8.21.custom/css/custom-theme/minified/jquery.ui.datepicker.min.css" />
    
    <style type="text/css">
        .ui-datepicker-title {
            color: black;
        }
    </style>
    
    <div class="form-group">
        <label class="col-sm-2 control-label">
            Instituto</label>
        <div class="col-sm-3">
            <asp:DropDownList ID="ddlInstituto" runat="server" Width="300px" AutoPostBack="True" disabled
                class="form-control disabled" data-toggle="dropdown" OnSelectedIndexChanged="ddlInstituto_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">
            Repositório</label>
        <div class="col-sm-3">
            <asp:UpdatePanel ID="updPnPatrimonio" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlPatrimonio" runat="server" Width="300px" class="form-control" data-toggle="dropdown">
                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ID="reqddlPatrimonio" runat="server" ControlToValidate="ddlPatrimonio" InitialValue="0"
                        Text="*" ForeColor="Red" ValidationGroup="IsValid" ErrorMessage="Repositório requerido."></asp:RequiredFieldValidator>--%>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlInstituto" EventName="SelectedIndexChanged" />
                    <asp:PostBackTrigger ControlID="ddlPatrimonio" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">
            Data utilização</label>
        <div class="col-sm-2">
            <div class="input-group input-group-sm">
                <span class="input-group-addon">De</span>
                <asp:TextBox ID="txtPeriodoDe" runat="server" class="form-control data"></asp:TextBox>
                <span class="input-group-btn">
                    <button id="btnDataDe" type="button" class="btn btn-default">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </button>
                </span>
            </div>
        </div>
        <div class="col-sm-2">
            <div class="input-group input-group-sm">
                <span class="input-group-addon">até</span>
                <asp:TextBox ID="txtPeriodoAte" runat="server" class="form-control data"></asp:TextBox>
                <span class="input-group-btn">
                    <button id="btnDataAte" type="button" class="btn btn-default">
                        <span class="glyphicon glyphicon-calendar"></span>
                    </button>
                </span>
            </div>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-12">
            <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" ToolTip="Pesquisar para os filtros informados."
                CssClass="btn btn-success" Width="100px" onclick="btnPesquisar_Click" ValidationGroup="IsValid" CausesValidation="True"></asp:Button>
            <asp:Button ID="btnVoltar" CausesValidation="true" runat="server" Text="Voltar" OnClientClick="window.location.href='../Menu.aspx'; return false;"
                ToolTip="Voltar a tela principal." CssClass="btn btn-success" Width="100px">
            </asp:Button>
        </div>
    </div>
    
    <hr class="clearfix" />
    
    <div class="panel">
        
        <div class="form-inline">
            <asp:Button ID="btnExportar" class="btn" runat="server" Text="Exportar" 
                ValidationGroup="IsValid" CausesValidation="True" onclick="btnExportar_Click" />
        </div>
        
        <iframe runat="server" id="rtpViewer" style="width: 925px; height: 650px; margin-top: 6px;"></iframe>
        <br />
        É necessário que tenha o <a href="http://get.adobe.com/br/reader/" target="_blank">Adobe&nbsp;Reader</a>&nbsp;instalado.
        
    </div>
    
    <script type="text/javascript">

        $(document).ready(function () {

            $(".data").mask("99/99/9999");

            $(".data").datepicker({
                changeMonth: true,
                changeYear: true
            });

            $("#btnDataDe").click(function () {
                $("#<%=txtPeriodoDe.ClientID%>").datepicker('show');
            });

            $("#btnDataAte").click(function () {
                $("#<%=txtPeriodoAte.ClientID%>").datepicker('show');
            });
        });        

    </script>
    
    <script type="text/javascript" src="../Framework/jquery-ui-1.8.21.custom/js/jquery.ui.datepicker.js"></script>

</asp:Content>
