<%@ Page Title="" Language="C#" MasterPageFile="~/Master/HcrpMaster.Master" AutoEventWireup="true"
    CodeBehind="VisualizarConferencia.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Conferencia.VisualizarConferencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphCabecalho" runat="server">
    Lista conferência / Controle de lacre
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphConteudo" runat="server">
    <asp:HiddenField runat="server" ID="hdnseqLacreRepositorio" ClientIDMode="Static"/>
    <asp:HiddenField runat="server" ID="hdnseqRepositorio" ClientIDMode="Static"/>
    
    
    
    <div class="col-sm-6" id="divLacreComp" style="display: none;">
        <div class="table-responsive">
            <asp:GridView runat="server" ID="grvLacreComplemento" Width="100%" AutoGenerateColumns="False"
            BorderWidth="0" AllowSorting="false" CssClass="table table-hover table-bordered"
            EmptyDataText="Nenhum lacre complemento registrado">
            <Columns>
                <asp:TemplateField HeaderText="Descrição" HeaderStyle-Width="60%">
                    <ItemTemplate>
                        <%# Eval("DSC_COMPLEMENTO")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Número do lacre">
                    <ItemTemplate>
                        <%# Eval("NUM_LACRE")%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerTemplate>
                <div id="page-selection" class="pagination"></div>
            </PagerTemplate>
        </asp:GridView>
        </div>
    </div>

    <div class="modal fade" id="modalItensDaLista" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content ">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h3 class="modal-title">
                        Itens no repositório</h3>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table id="tblItensListaControleModal" class="table table-bordered table-hover table-condensed">
                            <thead style="background-color: #dddddd">
                                <tr>
                                    <th>
                                        Alínea
                                    </th>
                                    <th>
                                        Código
                                    </th>
                                    <th>
                                        Item
                                    </th>
                                    <th>
                                        Lote
                                    </th>
                                    <th>
                                        Quantidade necessária
                                    </th>
                                    <th>
                                        Quantidade disponível
                                    </th>
                                    <th>
                                        Validade
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="panel panel-group" id="accordion1">
        <div class="panel panel-default">
            <div class="panel-heading" data-toggle="collapse" data-parent="#accordion1" href="#InfRepositorio"
                style="cursor: pointer">
                <h3 class="panel-title">
                    Informações sobre o repositório</h3>
            </div>
            <div id="InfRepositorio" class="panel-collapse collapse in">
                <div class="panel-body ">
                    <fieldset >
                        <div class="form-group">
                            <label class="col-sm-2 control-label">
                                Instituto:</label>
                            <div class="col-sm-3" >
                                <asp:TextBox ID="lblInstituto" disabled runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label">
                                Situação:</label>
                            <div class="col-sm-3" >
                                <asp:TextBox ID="lblSituacao" disabled runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">
                                Patrimônio:</label>
                            <div class="col-sm-3" >
                                <asp:TextBox ID="lblPatrimonio" runat="server" CssClass="form-control input-sm" disabled></asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label">
                                Tipo da Lista:</label>
                            <div class="col-sm-3" >
                                <asp:TextBox disabled ID="lblTipoDaLista" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group" visible="false" id="lblMotivRompimento" clientidmode="Static"
                            runat="server">
                            <label class="col-sm-2 control-label">
                                Motivo do Rompimento:</label>
                            <div class="col-sm-3" >
                                <asp:TextBox disabled ID="lblMotivRompimentoDsc" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        
                            <label class="col-sm-2 control-label">
                                Lacre carro:</label>
                            <div class="col-sm-3" >
                                <asp:TextBox disabled ID="lblLacresDsc" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label">
                                lacre caixa de intubação:</label>
                            <div class="col-sm-3">
                                <button id="popComplemento" class="btn btn-default btn-sm" data-toggle="tooltip" data-placement="right" >
                                    <span class="glyphicon glyphicon-paperclip" aria-hidden="true"></span>
                                </button>
                            </div>
                        
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-group" id="accordion2">
        <div class="panel panel-default">
            <div class="panel-heading" data-toggle="collapse" data-parent="#accordion2" href="#HistLacracao"
                style="cursor: pointer">
                <h3 class="panel-title">
                    Histórico de Lacração&nbsp;<span class="text-warning" style="font-size: 11px;">(clique
                        para expandir/fechar)</span></h3>
            </div>
            <div id="HistLacracao" class="panel-collapse collapse in">
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="grvHistLacracao" Width="100%" AutoGenerateColumns="False"
                            BorderWidth="0" AllowSorting="False" EmptyDataText="Nenhum registro foi localizado."
                            class="table table-bordered table-condensed">
                            <Columns>
                                <asp:TemplateField HeaderText="Data Histórico">
                                    <ItemTemplate>
                                        <%# Eval("DataCadastro", "{0: dd/MM/yyyy HH:mm}")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Responsável">
                                    <ItemTemplate>
                                        <%# Eval("UsuarioCadastro.Nome")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Situação do lacre">
                                    <ItemTemplate>
                                        <%# Eval("TipoSituacaoHc.NomSituacao")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-group" id="accordion3">
        <div class="panel panel-default">
            <div class="panel-heading" data-toggle="collapse" data-parent="#accordion3" href="#HistChecagem"
                style="cursor: pointer">
                <h3 class="panel-title">
                    Histórico de Conferência&nbsp;<span class="text-warning" style="font-size: 11px;">(clique
                        para expandir/fechar)</span></h3>
            </div>
            <div id="HistChecagem" class="panel-collapse collapse in">
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:GridView runat="server" ID="grvHistChecagem" Width="100%" AutoGenerateColumns="False"
                            BorderWidth="0" AllowSorting="False" EmptyDataText="Nenhum registro foi localizado."
                            class="table table-bordered table-condensed">
                            <Columns>
                                <asp:TemplateField HeaderText="Data Checagem">
                                    <ItemTemplate>
                                        <%# Eval("DataCadastro", "{0: dd/MM/yyyy HH:mm}")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Responsável">
                                    <ItemTemplate>
                                        <%# Eval("UsuarioCadastro.Nome")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Situação do lacre">
                                    <ItemTemplate>
                                        <%# Eval("TipoSituacaoHc.NomSituacao")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-group" id="accordion4">
        <div class="panel panel-default">
            <div class="panel-heading" data-toggle="collapse" data-parent="#accordion4" href="#HistOcorrencia"
                style="cursor: pointer">
                <h3 class="panel-title">
                    Histórico de Ocorrência&nbsp;<span class="text-warning" style="font-size: 11px;">(clique
                        para expandir/fechar)</span></h3>
            </div>
            <div id="HistOcorrencia" class="panel-collapse collapse in" style="overflow: auto;
                max-height: 300px;">
                <div class="panel-body">
                    <div class="table-responsive">
                        <div>
                            <asp:Repeater runat="server" ID="rptLacreOcorrenciaRegistradas" OnItemDataBound="rptLacreOcorrenciaRegistradas_ItemDataBound">
                                <HeaderTemplate>
                                    <div class="col-sm-12">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <h4>
                                        <asp:Label ID="lblDataOcorrenciaRegistrada" runat="server" Text="-" class="label label-default"></asp:Label>
                                        <asp:TextBox ID="txtDescOcorrenciaRegistradaVisualizacao" runat="server" CssClass="form-control"
                                            TextMode="MultiLine" Text='<%# Eval("DscOcorrencia") %>' ReadOnly="true"></asp:TextBox>
                                    </h4>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-group" id="pnItens" clientidmode="Static" runat="server">
        <div class="panel panel-default">
            <div class="panel-heading" data-toggle="collapse" data-parent="#pnItens" href="#ItensReposit"
                style="cursor: pointer">
                <h3 class="panel-title">
                    Itens no repositório&nbsp;<span class="text-warning" style="font-size: 11px;">(clique
                        para expandir/fechar)</span></h3>
            </div>
            <div id="ItensReposit" class="panel-collapse collapse in">
                <div class="panel-body">
                    <div class="form-group" >
                        <div class="col-sm-12" runat="server" id="divItensFaltantes">
                            <a class="text-danger" onclick="listarItensFaltantes();" style="cursor: pointer">Existem <span
                                class="badge" runat="server" id="lblContadorItensFaltantes"></span> itens que estão
                                configurados na lista mas não estão no repositório.Clique aqui para visualizar.
                            </a>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12" runat="server" id="div2">
                            <a class="text-danger" onclick="listarQtdDiferente();" style="cursor: pointer">
                                Existem <span class="badge" runat="server" id="lblContadorQtdsDiferentes"></span>
                                &nbsp;itens que estão com qtd. disponível diferente da qtd. necessária. Clique aqui
                                para visualizar. </a>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12" runat="server" id="div3">
                            <a class="text-danger" onclick="listarVencidos();" style="cursor: pointer">Existem
                                <span class="badge" runat="server" id="lblContadorVencidos"></span>&nbsp;MEDICAMENTO(S) que
                                estão dentro do prazo de validade. Clique aqui para visualizar. </a>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-12" runat="server" id="div4">
                            <a class="text-danger" onclick="mostrarMateriaisVencidos();" style="cursor: pointer">Existem
                                <span class="badge" runat="server" id="lblContadorMateriaisVencidos"></span>&nbsp;MATERIAL(AIS) que
                                estão dentro do prazo de validade. Clique aqui para visualizar. </a>
                        </div>
                    </div>
                    <div class="form-group" >
                        <div class="col-sm-10">
                            <a onclick="mostrarItensDaLista();" style="cursor: pointer">Clique aqui para visualizar
                                os itens do repositório em tela cheia</a>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <div style="overflow: auto; width: 100%; max-height: 300px;" runat="server" id="divItens">
                            <asp:GridView runat="server" ID="grvItem" Width="97%" AutoGenerateColumns="False"
                                class="table table-bordered table-condensed" EmptyDataText="Nenhum registro foi localizado."
                                OnRowDataBound="grvItem_RowDataBound" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Alínea">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAlinea" runat="server" Text='<%# Eval("ItensListaControle.Alinea.Nome")%>'></asp:Label>
                                            <asp:HiddenField ID="hdfSeqLacreRepositorioItens" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Código">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCodigoMaterial" runat="server" Text='<%# Eval("Material.Codigo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nome">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNomeMaterial" runat="server" Text='<%# Eval("Material.Nome")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unidade">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnidade" runat="server" Text='<%# Eval("ItensListaControle.Unidade.Nome")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qtd.<br />Necessária">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQtdNecessaria" runat="server" Text='<%# Eval("ItensListaControle.QuantidadeNecessaria")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qtd.<br />Disponível">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQtdDisponivel" runat="server" Text='<%# Eval("QtdDisponivel")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lote">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLote" runat="server" Text='<%# Eval("Lote.NumLoteFabricante")%>'></asp:Label>
                                            <itemstyle horizontalalign="Center" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Validade">
                                        <ItemTemplate>
                                            <asp:Label ID="lblValidade" runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="panel panel-group" id="pnItensConsumidos" clientidmode="Static" runat="server">
        <div class="panel panel-default">
            <div class="panel-heading" data-toggle="collapse" data-parent="#pnItensConsumidos" href="#divItensConsumidos"
                style="cursor: pointer">
                <h3 class="panel-title">Itens que saíram do repositório&nbsp;<span class="text-warning" style="font-size: 11px;">(clique para expandir/fechar)</span></h3>
            </div>
            <div id="divItensConsumidos" class="panel-collapse collapse in">
                <div class="panel-body">
                    <div class="table-responsive">
                        <div style="overflow: auto; width: 100%; max-height: 300px;" runat="server" id="div9">
                            <asp:GridView runat="server" ID="grdItensConsumidos" Width="97%" AutoGenerateColumns="False"
                                class="table table-bordered table-condensed" EmptyDataText="Nenhum registro foi localizado.">
                                     <Columns>
                                        <asp:TemplateField HeaderText="Código">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodigoMaterial" runat="server" Text='<%# Eval("LacreRepositorioItens.Material.Codigo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nome">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNomeMaterial" runat="server" Text='<%# Eval("LacreRepositorioItens.Material.Nome")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qtd.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQtdConsumida" runat="server" Text='<%# Eval("QtdUtilizada")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Lote">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLote" runat="server" Text='<%# Eval("LacreRepositorioItens.NumLoteFabricante")%>'></asp:Label>
                                                <itemstyle horizontalalign="Center" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <div class="panel panel-group" id="pnItensPassado" clientidmode="Static" runat="server">
        <div class="panel panel-default">
            <div class="panel-heading" data-toggle="collapse" data-parent="#pnItensPassado" href="#divItensPassado"
                style="cursor: pointer">
                <h3 class="panel-title">Itens que estavam no repositório no momento da lacração&nbsp;<span class="text-warning" style="font-size: 11px;">(clique para expandir/fechar)</span></h3>
            </div>
            <div id="divItensPassado" class="panel-collapse collapse in">
                <div class="panel-body">
                    <div class="table-responsive">
                        <div style="overflow: auto; width: 100%; max-height: 300px;" runat="server" id="div5">
                            <asp:GridView runat="server" ID="grdItensPassado" Width="97%" AutoGenerateColumns="False" OnRowDataBound="grdItensPassado_RowDataBound"
                                class="table table-bordered table-condensed" EmptyDataText="Nenhum registro foi localizado.">
                                     <Columns>
                                    <asp:TemplateField HeaderText="Alínea">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAlinea" runat="server" Text='<%# Eval("ItensListaControle.Alinea.Nome")%>'></asp:Label>
                                            <asp:HiddenField ID="hdfSeqLacreRepositorioItens" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Código">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCodigoMaterial" runat="server" Text='<%# Eval("Material.Codigo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nome">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNomeMaterial" runat="server" Text='<%# Eval("Material.Nome")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unidade">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnidade" runat="server" Text='<%# Eval("ItensListaControle.Unidade.Nome")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qtd.<br />Necessária">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQtdNecessaria" runat="server" Text='<%# Eval("ItensListaControle.QuantidadeNecessaria")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qtd.<br />Disponível">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQtdDisponivel" runat="server" Text='<%# Eval("QtdDisponivel")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lote">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLote" runat="server" Text='<%# Eval("Lote.NumLoteFabricante")%>'></asp:Label>
                                            <itemstyle horizontalalign="Center" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Validade">
                                        <ItemTemplate>
                                            <asp:Label ID="lblValidade" runat="server" Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    

    <div class="form-group">
        <div class="col-sm-3" runat="server" id="divRegistrarConferencia">
            <asp:UpdatePanel ID="upp" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:Button type="button" class="btn btn-success" ID="btnRegistrarConferencia" runat="server"
                        OnClick="btnRegistrarConferencia_Click" Text="Registrar conferência"></asp:Button>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="col-sm-3">
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" CssClass="btn btn-success"
                Width="100px" OnClick="btnVoltar_Click"></asp:Button>
        </div>
    </div>
    
    <div class="modal fade" id="modalItensFaltantes" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h3 class="modal-title">
                        Itens em falta no repositório</h3>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table id="tblItensFaltantes" class="table table-bordered table-hover table-condensed">
                            <thead style="background-color: #dddddd">
                                <tr>
                                    <th>
                                        Código
                                    </th>
                                    <th>
                                        Item
                                    </th>
                                    <th>
                                        Quantidade necessária
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalItensQtdDiferente" tabindex="-3" role="dialog" aria-labelledby="myModalLabel1"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h3 class="modal-title">
                        Itens com qtds. diferentes no repositório</h3>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table id="tblItensQtdDiferente" class="table table-bordered table-hover table-condensed">
                            <thead style="background-color: #dddddd">
                                <tr>
                                    <th>
                                        Código
                                    </th>
                                    <th>
                                        Item
                                    </th>
                                    <th>
                                        Quantidade necessária
                                    </th>
                                    <th>
                                        Quantidade disponível
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalItensVencidos" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h3 class="modal-title">
                        Itens dentro do prazo de validade no repositório</h3>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table id="tblItensVencidos" class="table table-bordered table-hover table-condensed">
                            <thead style="background-color: #dddddd">
                                <tr>
                                    <th>
                                        Código
                                    </th>
                                    <th>
                                        Item
                                    </th>
                                    <th>
                                        Data de vencimento
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <div class="modal fade" id="modalMateriaisVencidos" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h3 class="modal-title">
                        Materiais dentro do prazo de validade no repositório</h3>
                </div>
                <div class="modal-body">
                    <div class="table-responsive">
                        <table id="tblMateriaisVencidos" class="table table-bordered table-hover table-condensed">
                            <thead style="background-color: #dddddd">
                                <tr>
                                    <th>
                                        Código
                                    </th>
                                    <th>
                                        Item
                                    </th>
                                    <th>
                                        Data de vencimento
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        setInterval(function () { destacarGridItemVencimento(); }, 800);

        $(document).ready(function () {
            $('.exibirAlerta').tooltip({ placement: 'left', container: 'body' });

            //Popover lacre complemento
            $("#popComplemento").popover({
                html: true,
                container: "body",
                trigger: "click",
                content: function () {
                    return $("#divLacreComp").html();
                }
            });

            //Fechar popover ao clicar em qualquer área da tela
            $('body').on('click', function (e) {
                $("#popComplemento").each(function () {
                    if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                        $(this).popover('hide');
                    }
                });
            });

            $("#popComplemento").on("click", function (e) {
                e.preventDefault();
            });

            $('#popComplemento').tooltip({
                title: "Clique aqui para exibir os lacres complementares"
            });
        });

        function destacarGridItemVencimento() {
            $('#<%= grvItem.ClientID %> tbody').find(".exibirAlerta").animateHighlightItemValidade();
        }

        $.fn.animateHighlightItemValidade = function () {

            //alert-warning

            this.toggleClass("alert-warning");
        };

        function mostrarItensDaLista() {
            listarItensListaControleModal();
        }

        function mostrarMateriaisVencidos() {
            listarMateriaisVencidos();
        }

        function listarMateriaisVencidos() 
        {
            $.ajax({
                type: "POST",
                url: "../Conferencia/VisualizarConferencia.aspx/ObterMateriaisVencidos",
                data: JSON.stringify({ "seqLacreRepositorio": $("#hdnseqLacreRepositorio").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //beforeSend: 
                success:
                    function (msg) {

                        if ($("#tblMateriaisVencidos  > tbody tr").size() > 0) {
                            $("#tblMateriaisVencidos  > tbody").empty();
                        }

                        $.each(jQuery.parseJSON(msg.d), function () {

                            $("#tblMateriaisVencidos  > tbody").append(
                                "<tr>" +
                                    "<td> " + (this['Material']['Codigo'] == null ? "" : this['Material']['Codigo']) + "</td>" +
                                    "<td>" + this['Material']['Nome'] + "</td>" +
                                    "<td>" + this['Lote']['DataValidadeLote'] + "</td>" +
                                    "</tr>");
                        });
                        $('#modalMateriaisVencidos').modal('show');
                    }
            });
            }
        
        function listarItensListaControleModal() {
            $.when($.ajax({
                    type: "POST",
                    url: "VisualizarConferencia.aspx/ObterItensPorListaControle",
                    data: JSON.stringify({ "seqLacreRepositorio": $("#hdnseqLacreRepositorio").val() }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //beforeSend: 
                    success:
                        function (msg) {

                            if ($("#tblItensListaControleModal > tbody tr").size() > 0) {
                                $("#tblItensListaControleModal > tbody").empty();
                            }

                            $.each(jQuery.parseJSON(msg.d), function () {

                                $("#tblItensListaControleModal > tbody").append(
                                    "<tr>" +
                                        "<td>" + this['ItensListaControle']['Alinea']['Nome'] + "</td>" +
                                        "<td> " + (this['Material']['Codigo'] != null ? this['Material']['Codigo'] : "") + "</td>" +
                                        "<td>" + this['Material']['Nome'] + "</td>" +
                                        "<td>" + (this['Lote']['NumLoteFabricante'] != null ? this['Lote']['NumLoteFabricante'] : "") + "</td>" +
                                        "<td>" + this['ItensListaControle']['QuantidadeNecessaria'] + " / " + this['ItensListaControle']['Unidade']['Nome'] + "</td>" +
                                        "<td>" + this['QtdDisponivel'] + " / " + this['ItensListaControle']['Unidade']['Nome'] + "</td>" +
                                        "<td>" + this['Lote']['DataValidadeLote'] + "</td>" +
                                        "</tr>");
                            });

                        }
                }).done(function () {
                    $('#modalItensDaLista').modal('show');
                }));
        }

        function listarItensFaltantes() {
            $.ajax({
                type: "POST",
                url: "../Conferencia/VisualizarConferencia.aspx/ObterItensEmFaltaNoRepositorio",
                data: JSON.stringify({ "seqRepositorio": $("#hdnseqRepositorio").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //beforeSend: 
                success:
                    function (msg) {

                        if ($("#tblItensFaltantes  > tbody tr").size() > 0) {
                            $("#tblItensFaltantes  > tbody").empty();
                        }

                        $.each(jQuery.parseJSON(msg.d), function () {

                            $("#tblItensFaltantes  > tbody").append(
                                "<tr>" +
                                    "<td> " + this['Material']['Codigo'] + "</td>" +
                                    "<td>" + this['Material']['Nome'] + "</td>" +
                                    "<td>" + this['QuantidadeNecessaria'] + " / " + this['Unidade']['Nome'] + "</td>" +
                                    "</tr>");
                        });
                        $('#modalItensFaltantes').modal('show');
                    }
            });
        }

        function listarItensComQuantidadesDiferentes() {
            $.ajax({
                type: "POST",
                url: "../Conferencia/VisualizarConferencia.aspx/ObterItensEmFaltaNoRepositorio",
                data: JSON.stringify({ "seqRepositorio": $("#hdnseqRepositorio").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //beforeSend: 
                success:
                    function (msg) {

                        if ($("#tblItensFaltantes  > tbody tr").size() > 0) {
                            $("#tblItensFaltantes  > tbody").empty();
                        }

                        $.each(jQuery.parseJSON(msg.d), function () {

                            $("#tblItensFaltantes  > tbody").append(
                                "<tr>" +
                                    "<td> " + this['Material']['Codigo'] + "</td>" +
                                    "<td>" + this['Material']['Nome'] + "</td>" +
                                    "<td>" + this['QuantidadeNecessaria'] + " / " + this['Unidade']['Nome'] + "</td>" +
                                    "</tr>");
                        });
                        $('#modalItensFaltantes').modal('show');
                    }
            });
        }

        function listarQtdDiferente() {
            $.ajax({
                type: "POST",
                url: "../Conferencia/VisualizarConferencia.aspx/ObterItensComQtdDiferente",
                data: JSON.stringify({ "seqLacreRepositorio": $("#hdnseqLacreRepositorio").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //beforeSend: 
                success:
                    function (msg) {

                        if ($("#tblItensQtdDiferente > tbody tr").size() > 0) {
                            $("#tblItensQtdDiferente > tbody").empty();
                        }

                        $.each(jQuery.parseJSON(msg.d), function () {

                            $("#tblItensQtdDiferente > tbody").append(
                                "<tr>" +
                                    "<td> " + this['Material']['Codigo'] + "</td>" +
                                    "<td>" + this['Material']['Nome'] + "</td>" +
                                    "<td>" + this['ItensListaControle']['QuantidadeNecessaria'] + " / " + this['ItensListaControle']['Unidade']['Nome'] + "</td>" +
                                    "<td>" + this['QtdDisponivel'] + " / " + this['ItensListaControle']['Unidade']['Nome'] + "</td>" +
                                    "</tr>");
                        });
                        $('#modalItensQtdDiferente').modal('show');
                    }
            });
        }

        function listarVencidos() {
            $.ajax({
                type: "POST",
                url: "../Conferencia/VisualizarConferencia.aspx/ObterItensVencidos",
                data: JSON.stringify({ "seqLacreRepositorio": $("#hdnseqLacreRepositorio").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //beforeSend: 
                success:
                    function (msg) {

                        if ($("#tblItensVencidos > tbody tr").size() > 0) {
                            $("#tblItensVencidos > tbody").empty();
                        }

                        $.each(jQuery.parseJSON(msg.d), function () {

                            $("#tblItensVencidos  > tbody").append(
                                "<tr>" +
                                    "<td> " + (this['Material']['Codigo'] == null ? "" : this['Material']['Codigo']) + "</td>" +
                                    "<td>" + this['Material']['Nome'] + "</td>" +
                                    "<td>" + this['Lote']['DataValidadeLote'] + "</td>" +
                                    "</tr>");
                        });
                        $('#modalItensVencidos').modal('show');
                    }
            });
        }

    </script>
</asp:Content>
