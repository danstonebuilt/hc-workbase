<%@ Page Title="" Language="C#" MasterPageFile="~/Master/HcrpMaster.Master" AutoEventWireup="true"
    CodeBehind="menu.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCabecalho" runat="server">
    Menu
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphConteudo" runat="server">
    <div class="alert alert-warning" role="alert">
        <span class="glyphicon glyphicon-list-alt"></span>
        <a href="Manual.pdf" target="_blank">Manual do sistema</a>
    </div>

  

    
    <ul class="list-group">    
        <asp:Repeater runat="server" ID="RpMenuRepeater" EnableViewState="true" OnItemDataBound="RpSubMenuRepeate_ItemDataBound">
            <ItemTemplate>
                <li class="list-group-item HeaderMenu" style="color: #48AF46;font-weight: bolder">
                    <span class="glyphicon glyphicon-home"></span>
                        <%# DataBinder.Eval(Container.DataItem, "nom_exibicao_programa") %>
                </li>
                <asp:Repeater runat="server" ID="RpSubMenuRepeate" OnItemDataBound="RpSubMenuRepeate_ItemDataBound">
                    <ItemTemplate>
                        <a style="padding-left: 30px" class="list-group-item" href="<%# DataBinder.Eval(Container.DataItem, "dsc_pagina_web") %>">
                            <span class="glyphicon glyphicon-hand-right" style="color: black"></span>
                            <%# DataBinder.Eval(Container.DataItem, "nom_exibicao_programa") %>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
    
    
</asp:Content>
