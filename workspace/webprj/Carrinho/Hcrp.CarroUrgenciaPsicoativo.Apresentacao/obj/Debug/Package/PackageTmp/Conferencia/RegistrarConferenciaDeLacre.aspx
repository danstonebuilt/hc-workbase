<%@ Page Title="" Language="C#" MasterPageFile="~/Master/HcrpMaster.Master" AutoEventWireup="true"
    CodeBehind="RegistrarConferenciaDeLacre.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Conferencia.RegistrarConferenciaDeLacre" %>

<%@ Register Assembly="ASPnetPagerV2_8" Namespace="ASPnetControls" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <script language="javascript" type="text/javascript">
        
        function post() {
            <%=GetPostGrid() %>
        }
             
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphCabecalho" runat="server">
    Lacrações / Conferência
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphConteudo" runat="server">
    <asp:UpdatePanel runat="server" ID="uppOpcoes" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal fade " id="modalAcoes" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h3 class="modal-title">
                                Ações</h3>
                        </div>
                        <div class="modal-body">
                            <div id="divOpcoes">
                                <div id="trNovaLacracao" runat="server" visible="false">
                                    <button type="button" class="btn btn-success btn-lg btn-block" id="btnNovaLacracao"
                                        comando="NOVA_LACRACAO">
                                        Nova Lacração</button>
                                    <br />
                                </div>
                                <div id="trExcluir" runat="server" visible="false">
                                    <button type="button" class="btn btn-success btn-lg btn-block" id="btnExcluir" comando="EXCLUIR">
                                        Excluir</button>
                                    <br />
                                </div>
                                <div id="trVisualizar" runat="server" visible="false">
                                    <button type="button" class="btn btn-success btn-lg btn-block" id="btnVisualizar"
                                        comando="VISUALIZAR">
                                        Visualizar/Conferência</button>
                                    <br />
                                </div>
                                <div id="trConferencia" runat="server" visible="false">
                                    <button type="button" class="btn btn-success btn-lg btn-block" id="btnConferencia"
                                        comando="CONFERENCIA">
                                        Conferência</button>
                                    <br />
                                </div>
                                <div id="trEditar" runat="server" visible="false">
                                    <button type="button" class="btn btn-success btn-lg btn-block" id="btnEditar" comando="EDITAR">
                                        Editar</button>
                                    <br />
                                </div>
                                <div id="trRegistrarOcorrencia" runat="server" visible="false">
                                    <button type="button" class="btn btn-success btn-lg btn-block" id="btnRegistrarOcorrencia"
                                        comando="REGISTRAR_OCORRENCIA">
                                        Registrar Ocorrência</button>
                                    <br />
                                </div>
                                <div id="trQuebrarLacre" runat="server" visible="false">
                                    <button type="button" class="btn btn-success btn-lg btn-block" id="btnQuerLacre"
                                        comando="QUEBRAR_LACRE">
                                        Quebrar lacre</button>
                                </div>
                            </div>
                            <div id="divRegistrarOcorrencia" style="display: none">
                                <h4>
                                    Digite a ocorrência
                                </h4>
                                <asp:TextBox ID="txtDscOcorrencia" Style="min-height: 150px" runat="server" TextMode="MultiLine"
                                    CssClass="form-control" ClientIDMode="Static" MaxLength="500"></asp:TextBox>
                                <br />
                                <button type="button" class="btn btn-success btn-lg btn-block" id="btnSalvarOcorrencia"
                                    comando="SALVAR_OCORRENCIA">
                                    Salvar</button>
                            </div>
                            <div id="divQuebrarLacre" style="display: none">
                                <h4>
                                    Motivo da quebra de lacre
                                </h4>
                                <div class="form-group">
                                    <label class="col-sm-3 control-label">
                                        Tipo ocorrência</label>
                                    <div class="col-sm-9">
                                        <asp:DropDownList ID="ddlTipoOcorrencia" data-toggle="dropdown" 
                                            class="form-control" runat="server" ClientIDMode="Static"></asp:DropDownList>
                                        
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-12">
                                        <asp:CheckBox CssClass="checkbox" ID="chkgerarLacreProv" runat="server" Text="Gerar Lacre Provisório" Checked="true" ClientIDMode="Static" />
                                    </div>
                                </div>
                                <div class="form-group" id="divRegistrarOcorrenciaLacreSemProvisorio" style="display: none">
                                    <div class="col-sm-12">
                                        <span>Obrigatório informar uma justificativa ao quebrar o lacre e não gerar um provisório.</span>
                                        <textarea  ID="txtDscOcorrenciaQuebraLacre" class="form-control" runat="server" clientidmode="Static"></textarea>
                                    </div>
                                </div>
                                <br />
                                <Button type="button" class="btn btn-success btn-lg btn-block" id="btnSALVAR_QUEBRA_LACRE"
                                    comando="SALVAR_QUEBRA_LACRE">Quebrar</Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upp" runat="server" ClientIDMode="Static" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnPost" EventName="Click" />
        </Triggers>
        <ContentTemplate>
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
                <div class="col-sm-7">
                    <asp:UpdatePanel ID="updPnPatrimonio" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlPatrimonio" runat="server"  AutoPostBack="True" ClientIDMode="Static"
                                class="form-control" data-toggle="dropdown" OnSelectedIndexChanged="ddlPatrimonio_SelectedIndexChanged" data-placeholder="Selecione" >
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlInstituto" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="ddlPatrimonio" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group hidden">
                <div class="col-lg-offset-2 col-sm-2">
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
                        CssClass="btn btn-success" Width="100px" OnClick="btnPesquisar_Click"></asp:Button>
                    <asp:Button ID="btnVoltar" CausesValidation="true" runat="server" Text="Voltar" OnClientClick="window.location.href='../Menu.aspx';return false"
                        ToolTip="Voltar a tela principal." CssClass="btn btn-success" Width="100px">
                    </asp:Button>
                </div>
            </div>
            <div class="table-responsive">
                <asp:GridView runat="server" ID="grvItem" Width="100%" AutoGenerateColumns="False"
                    class="table table-bordered table-condensed" BorderWidth="0" AllowSorting="False"
                    EmptyDataText="Nenhum registro foi localizado." OnRowCommand="grvItem_RowCommand"
                    OnRowDataBound="grvItem_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Id">
                            <ItemTemplate>
                                <asp:Label ID="lblSeqLacreRepositorio" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Motivo">
                            <ItemTemplate>
                                <%--<%# Eval("DataCadastro", "{0: dd/MM/yyyy HH:mm}")%>--%>
                                <%# Eval("LacreTipoOcorrencia.DscTipoOcorrencia")%>
                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Data Lacração">
                            <ItemTemplate>
                                <%# Eval("DataLacracao", "{0: dd/MM/yyyy HH:mm}")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nº lacre carro">
                            <ItemTemplate>
                                <asp:Label ID="lblNumLacre" runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Lacre complemento">
                            <ItemTemplate>
                                <asp:Label ID="lblNumLacreIntubacao" runat="server"></asp:Label>
                                <%-- Grid lacre complemento --%>
                                <div class="col-sm-6" id="divLacreComp" runat="server" style="display: none;">
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
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Responsável Lacração">
                            <ItemTemplate>
                                <%# Eval("UsuarioResponsavelLacracao.Nome")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Situação do lacre atual">
                            <ItemTemplate>
                                <%# Eval("TipoSituacaoHc.NomSituacao")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Data situação do lacre">
                            <ItemTemplate>
                                <%# Eval("DataDaSituacao", "{0: dd/MM/yyyy HH:mm}")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:UpdatePanel runat="server" ID="uppvisualizacoes" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ibtnVisualizarOpcoes" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <%--<button type='submit' value="<%# Eval("SeqLacreRepositorio")%>" class='btn btn-default btn-sm' runat="server"
                                    data-toggle='modal' data-target='#modalAcoes'>
                                    <span class='glyphicon glyphicon-list'></span>
                                </button>--%>
                                        <asp:LinkButton runat="server" ID="ibtnVisualizarOpcoes" type="button" class="btn btn-default btn-sm"
                                            Text="<span class='glyphicon glyphicon-list'></span>" CommandArgument='<%# Eval("SeqLacreRepositorio")%>'
                                            CommandName="SEL" />
                                        <%--<asp:LinkButton data-toggle='modal' data-target='#modalAcoes' runat="server" ID="ibtnVisualizarOpcoes" type="button" class="btn btn-default btn-sm" Text="<span class='glyphicon glyphicon-list'></span>"
                                        CommandName="SEL" CommandArgument='<%# Eval("SeqLacreRepositorio")%>'>        
                                    </asp:LinkButton>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <cc2:PagerV2_8 Width="100%" ID="paginador" runat="server" BackToFirstClause="Ir para última Página"
                BackToPageClause="Voltar para Página" GoClause="Ir" GoToLastClause="Voltar para última Página"
                NextToPageClause="Próxima Página" GenerateGoToSection="false" GeneratePagerInfoSection="false"
                ShowingResultClause="Mostrando Resultados" ShowResultClause="Mostrar Resultado"
                OnCommand="paginador_Command" ToClause="para" Visible="false" />
            <br />
            <asp:HiddenField ID="hdfTipoComando" runat="server" ClientIDMode="Static" />
            <input type="hidden" id="hdnTipoOcorrencia" runat="server" ClientIDMode="Static"/>
            <asp:Button runat="server" ID="btnPost" OnClick="btnPost_Click" Style="display: none"
                ClientIDMode="Static" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script language="javascript" type="text/javascript">
        function pageLoad() {

            $("#ddlPatrimonio").chosen({
                disable_search_threshold: 10,
                no_results_text: "Repositório não encontrado!",
                width: "95%"
            });


            $(".data").datepicker({
                changeMonth: true,
                changeYear: true
            });

            $('#chkgerarLacreProv').on("change",function () {
                if (!$('#chkFiltraInativo').is(':checked')) {
                    $("#divRegistrarOcorrenciaLacreSemProvisorio").fadeIn();
                }
                else {
                    $("#divRegistrarOcorrenciaLacreSemProvisorio").fadeOut();
                }
            });

            $("#ddlTipoOcorrencia").on("change", function () {
                if ($("#ddlTipoOcorrencia option:selected").val() != "-1") {
                    $("#hdnTipoOcorrencia").val($("#ddlTipoOcorrencia option:selected").val());
                }
            });

            $("#modalAcoes").on('hidden.bs.modal', function () {
                $.LimparCamposModal(this);

                //$("#chkgerarLacreProv").attr('checked', 'checked');
                $("#chkgerarLacreProv").prop('checked', true);
            });

            $("#btnDataDe").on("click", function () {
                $(this).datepicker();
            });

            //$(".data").mask("99/99/9999");
            
            //Hack IE 9 ou <
            if (ie <= 9) {
                if ($('#modalAcoes').is(":visible")) {
                    bindButtonModal();
                }
            }
            else {
                $('#modalAcoes').on('shown.bs.modal', function (e) {
                    //var btn = $(e.relatedTarget);
                    //var idLinha = $(e.relatedTarget).attr("value");
                    bindButtonModal();
                });
            }

            function bindButtonModal() {
                $("#modalAcoes .modal-body button").each(function () {
                    $(this).off().on("click", function () {
                        selecionarOpcao(this);
                    });
                });
            }

            function selecionarOpcao(btn) {

                var acao = $(btn).attr("comando");

                $("#hdfTipoComando").val(acao);

                if (acao == "REGISTRAR_OCORRENCIA") {
                    //$("#divOpcoes").toggleClass("hidden");

                    $("#divOpcoes").fadeOut(function () {
                        $("#divRegistrarOcorrencia").fadeIn(function () {
                            //$(this).toggleClass("invisible");
                        });
                    });

                    return;
                }

                if (acao == "QUEBRAR_LACRE") {
                    //$("#divOpcoes").toggleClass("hidden");

                    $("#divOpcoes").fadeOut(function () {
                        $("#divQuebrarLacre").fadeIn();
                    });

                    CarregarTipoOcorrencia();
                    return;
                }

                if (acao == "SALVAR_QUEBRA_LACRE") {
                    
                    if ($("#ddlTipoOcorrencia option:selected").val() == "0") {
                        ExibirMensagemAlerta("Selecione o tipo de ocorrência !");
                        $("#ddlTipoOcorrencia").focus();
                        return;
                    }

                    if (!$('#chkgerarLacreProv').is(':checked') && $("#txtDscOcorrenciaQuebraLacre").val().length <= 0) {
                            ExibirMensagemAlerta("Uma justificativa deve ser preenchida !");
                            $("#ddlTipoOcorrencia").focus();
                            return;
                        }
                    }

                    if (acao == "SALVAR_OCORRENCIA") {

                        if ($("#txtDscOcorrencia").val().length == 0) {
                            ExibirMensagemAlerta("Descreva a ocorrência !");
                            $("#txtDscOcorrencia").focus();
                            return;
                        }
                    }


                $("#modalAcoes").modal('hide');

                $("#btnPost").click();
            }


            function CarregarTipoOcorrencia() {
                
                // Carregando combo Instituto
                $.ajax({
                    type: "POST",
                    url: "RegistrarConferenciaDeLacre.aspx/CarregarTipoOcorrencia",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function (e) {
                        $("#ddlTipoOcorrencia").empty().append($("<option></option>").val("0").html("Carregando..."));
                    },
                    success:
                function (msg) {
                    $("#ddlTipoOcorrencia").empty().append($("<option></option>").val("0").html("Selecione..."));
                    
                    $.each(jQuery.parseJSON(msg.d), function () {
                        $("#ddlTipoOcorrencia").append($("<option></option>").val(this['SeqLacreTipoOCorrencia']).html(this['DscTipoOcorrencia']));
                    });
                }
                });
            }

            //Popover lacre complemento
            $("a .glyphicon-paperclip").parent().each(function() {
                $(this).popover({
                    html: true,
                    trigger: "click",
                    content: function () {
                        return $(this).parent().next().html();
                    }
                });
                
            });

            //Fechar popover ao clicar em qualquer área da tela
            $('body').on('click', function (e) {
                $("a .glyphicon-paperclip").parent().each(function () {
                    if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
                        $(this).popover('hide');
                    }
                });
            });

            $(".exibeToolTip").tooltip({
                title: "Clique aqui para exibir os lacres complementares"
            });
        }

        var ie = (function () {

            var undef,
                v = 3,
                div = document.createElement('div'),
                all = div.getElementsByTagName('i');

            while (
                    div.innerHTML = '<!--[if gt IE ' + (++v) + ']><i></i><![endif]-->',
                    all[0]
                );

            return v > 4 ? v : undef;

        } ());
    </script>
</asp:Content>
