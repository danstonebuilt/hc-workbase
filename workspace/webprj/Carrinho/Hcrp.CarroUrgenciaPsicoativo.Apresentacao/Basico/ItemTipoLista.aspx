<%@ Page Title="" Language="C#" MasterPageFile="~/Master/HcrpMaster.Master" AutoEventWireup="true" 
    CodeBehind="ItemTipoLista.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Basico.ItemTipoLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphCabecalho" runat="server">
    Itens do tipo de lista
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphConteudo" runat="server">
    <div class="modal fade " id="modalAlterarDados" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h3 class="modal-title">
                        Alterar dados</h3>
                </div>
                <div class="modal-body">
                    <h4>
                    </h4>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Qtd. necessária</label>
                        <div class="col-sm-4">
                            <input type="hidden" id="hdnidLinha" />
                            <input type="text" id="txtAlteracaoQuantidadeNecessaria" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Unidade de medida</label>
                        <div class="col-sm-8">
                            <input type="text" id="txtUnidadeAlteracao" class="form-control" disabled />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success btn-lg btn-block" id="btnSalvarAlteracaoDados">
                        Salvar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade " id="modalAcoes" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h3 class="modal-title" id="myModalLabel">
                        Ações</h3>
                </div>
                <div class="modal-body">
                    <h4>
                    </h4>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-success btn-lg btn-block" id="btnAtivarInativar">
                        Inativar</button>
                    <button type="button" class="btn btn-success btn-lg btn-block" id="btnAlterar">
                        Alterar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
            </div>
        </div>
    </div>
    <div class="modal fade" id="modalImportarItens" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
            </div>
        </div>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">
            Instituto</label>
        <div class="col-sm-3">
            <asp:DropDownList runat="server" ID="ddlInstituto" class="form-control" data-toggle="dropdown"
                Style="max-width: 250px" ClientIDMode="Static">
            </asp:DropDownList>
        </div>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">
            Tipo de lista</label>
        <div class="col-sm-7">
            <asp:DropDownList runat="server" ID="ddlTipoLista" class="form-control" data-toggle="dropdown" ClientIDMode="Static">
            </asp:DropDownList>
        </div>
    </div>
    <div id="divConteudo" style="display: none">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    Adicionar itens</h3>
            </div>
            <div id="divItens" class="panel-body">
                <div class="form-group">
                    <div class="col-sm-10">
                        <div id="divBotoes" class="btn-group" data-toggle="buttons">
                            <label class="btn btn-primary active" id="rItemComCodigo">
                                <input type="radio" name="options">
                                Item com código
                            </label>
                            <label class="btn btn-primary" id="rItemSemCodigo">
                                <input type="radio" name="options">
                                Item sem código
                            </label>
                            <label class="btn btn-primary" id="rEquipamento">
                                <input type="radio" name="options">
                                Equipamento
                            </label>
                        </div>
                    </div>
                </div>
                <div id="divMaterialComCodigo" class="panel panel-default">
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-sm-4">
                                <div class="input-group input-group-sm">
                                    <span class="input-group-addon">Cod. Material</span>
                                    <input type="hidden" id="hdnCodMaterial" data-rule-required="true" />
                                    <input type="text" id="txtCodigoMaterial" class="form-control" data-placement="top"
                                        data-content="Digite o código do material para pesquisar e pressione Enter" />
                                    <span class="input-group-btn">
                                        <button type="button" class="btn btn-default" data-toggle="modal" data-target="#myModal"
                                            href="../Comum/BuscaMaterialListaControleItem.aspx">
                                            <span class="glyphicon glyphicon-search"></span>
                                        </button>
                                    </span>
                                </div>
                            </div>
                            <div class="col-sm-8">
                                <div class="input-group input-group-sm">
                                    <span class="input-group-addon">Descrição material</span>
                                    <input type="text" id="txtNomeMaterial" class="form-control input-sm" disabled />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4">
                                <div class="input-group input-group-sm">
                                    <span class="input-group-addon">Quantidade</span>
                                    <input type="text" id="txtQuantidadeMaterialComCodigo" class="form-control" />
                                </div>
                            </div>
                            <div class="col-sm-8">
                                <div class="input-group input-group-sm">
                                    <span class="input-group-addon">Unidade de medida</span>
                                    <input type="text" id="txtUnidade" class="form-control" disabled="disabled" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divMaterialSemCodigo" class="panel panel-default" style="display: none">
                    <div class="panel-body">
                        <div class="form-group">
                            <label class="col-sm-1 control-label">
                                Descrição</label>
                            <div class="col-sm-8">
                                <input type="text" id="txtDescricaoMaterialSemCodigo" class="form-control input-sm" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-1 control-label">
                                Alinea</label>
                            <div class="col-sm-3">
                                <asp:DropDownList runat="server" ID="ddlAlineaMaterialSemCodigo" class="form-control input-sm"
                                    data-toggle="dropdown" Style="max-width: 250px" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                            <label class="col-sm-2 control-label">
                                Unidade de med.</label>
                            <div class="col-sm-3">
                                <asp:DropDownList runat="server" ID="ddlUnidadeDeMedidaMaterialSemCodigo" class="form-control input-sm"
                                    data-toggle="dropdown" Style="max-width: 250px" ClientIDMode="Static">
                                </asp:DropDownList>
                            </div>
                            <label class="col-sm-1 control-label">
                                Quantidade.</label>
                            <div class="col-sm-2">
                                <input type="text" id="txtQuantidadeMaterialSemCodigo" class="form-control input-sm" />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divEquipamento" class="panel panel-default" style="display: none">
                    <div class="panel-body">
                        <div class="form-group">
                            <div class="col-sm-4">
                                <div class="input-group input-group-sm">
                                    <span class="input-group-addon">Tipo Bem</span>
                                    <input type="text" id="txtCodigoTipoBem" class="form-control" data-placement="top"
                                        data-content="Digite o código do bem para pesquisar" />
                                    <span class="input-group-btn">
                                        <button type="button" class="btn btn-default" data-toggle="modal" data-target="#myModal"
                                            href="../Comum/BuscaTipoBem.aspx">
                                            <span class="glyphicon glyphicon-search"></span>
                                        </button>
                                    </span>
                                </div>
                            </div>
                            <div class="col-sm-8">
                                <div class="input-group input-group-sm">
                                    <span class="input-group-addon">Descrição Bem</span>
                                    <input type="text" id="txtNomeTipoBem" class="form-control input-sm" disabled />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4">
                                <div class="input-group input-group-sm">
                                    <span class="input-group-addon">Quantidade</span>
                                    <input type="text" id="txtQuantidadeBem" class="form-control" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-group" style="text-align: right">
                    <div class="col-sm-12">
                        <input type="button" class="btn btn-success" id="btnAdicionarMaterial" value="Adicionar"
                            title="Adicionar material" data-toggle="tooltip" data-loading-text="Processando..." />
                        <input type="button" class="btn btn-success" id="btnImportarLista" value="Importar lista"
                            title="Importar itens de outra lista" data-toggle="modal" data-target="#modalImportarItens"
                            href="../Comum/ImportaItensOutraLista.aspx" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <label class="checkbox">
        <asp:CheckBox runat="server" ID="chkFiltraInativo" Text="Listar itens inativos ?"
            ClientIDMode="Static" />
    </label>
    <div class="table-responsive">
        <table id="tblItensListaControle" class="table table-bordered table-condensed">
            <thead style="background-color: #dddddd">
                <tr>
                    <th >
                        Alínea
                    </th>
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
                        Ações
                    </th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <div id="divNenhumRegistro" class="panel panel-info" style="display: none">
        <div class="panel-heading">
            <h3 class="panel-title">
                Nenhum registro encontrado.
            </h3>
        </div>
    </div>
    <div style="margin-top: 15px">
        <asp:Button ID="btnVoltar" runat="server" Text="Voltar" ClientIDMode="Static" CssClass="btn btn-success"
            Width="100px" OnClick="btnVoltar_Click" data-toggle="tooltip" title="Voltar para o menu">
        </asp:Button>
    </div>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            
             $('#form1').validate();

            $(document).keypress(function (e) {
                if (e.which == 13) {
                    e.preventDefault();
                    if (document.activeElement.id == "txtCodigoMaterial" || document.activeElement.id == "txtQuantidadeMaterialComCodigo") {
                        ObterMaterial();
                    }

                    
                }
            });

            $("body").tooltip({ selector: '[data-toggle="tooltip"]' });

            $("#modalImportarItens").on('hidden.bs.modal', function () {
                $(this).data('bs.modal', null);
            });

            $("#modalAlterarDados").on('hidden.bs.modal', function () {
                $(this).data('bs.modal', null);
            });

            $("#modalAlterarDados").on('hidden.bs.modal', function () {
                $(this).data('bs.modal', null);
            });

            $("#modalAcoes").on('hidden.bs.modal', function () {
                $(this).data('bs.modal', null);
            });

            $("#modalImportarItens").on('show.bs.modal', function () {
                $(this).find(".modal-dialog").addClass("lg");
            });
            
            $("#modalImportarItens").on('hidden.bs.modal', function () {
                $(this).data('bs.modal', null);

                ListarItensListaControle();
            });

            $('#chkFiltraInativo').change(function () {
                ListarItensListaControle();
            });


            //$("#btnVoltar").tooltip();
            //$("#btnAdicionarMaterialcomCodigo").tooltip();
            //$("#btnImportarLista").tooltip();
            //$("#btnAdicionarMaterial").tooltip();

            $("#txtCodigoMaterial").on("focus", function () {
                $("#txtCodigoMaterial").popover('show');
            }).on('focusout', function () {
                $("#txtCodigoMaterial").popover('hide');
            });

            $("#txtCodigoMaterial").popover({ "trigger": "hover" });

            $('.btn').button();

            // Trocando panel de pesquisa de item com/sem código e equipamento
            $("#rItemComCodigo,#rItemSemCodigo,#rEquipamento").on("click", function (handler) {
                if (this.id == "rItemComCodigo" && !$(this).hasClass("active")) {
                    $("#divMaterialSemCodigo").fadeOut(function () {
                        $("#divEquipamento").fadeOut(function () {
                            $("#divMaterialComCodigo").fadeIn();
                        });
                    });
                }
                else if (this.id == "rItemSemCodigo" && !$(this).hasClass("active")) {
                    $("#divMaterialComCodigo").fadeOut(function () {
                        $("#divEquipamento").fadeOut(function () {
                            $("#divMaterialSemCodigo").fadeIn();
                        });
                    });

                    carregarComAlinea();

                    carregarComboUnidade();
                } else {
                    if ((!$(this).hasClass("active"))) {
                        $("#divMaterialComCodigo").fadeOut(function () {
                            $("#divMaterialSemCodigo").fadeOut(function () {
                                $("#divEquipamento").fadeIn();
                            });
                        });
                    }
                }
            });

            // Carregando combo Instituto
            $.ajax({
                type: "POST",
                url: "ItemTipoLista.aspx/CarregarComboInstituto",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function (e) {
                    $("#ddlInstituto").empty().append($("<option></option>").val("0").html("Carregando..."));
                },
                success:
                    function (msg) {
                        $("#ddlInstituto").empty().append($("<option></option>").val("0").html("Selecione..."));
                        $.each(jQuery.parseJSON(msg.d), function () {
                            $("#ddlInstituto").append($("<option></option>").val(this['CodInstituto']).html(this['NomeInstituto']));
                        });
                    }
            });

            function CarregaComboTipoLista(val) {
                // Carregando combo Instituto
                $.ajax({
                    type: "POST",
                    url: "ItemTipoLista.aspx/CarregarComboTipoLista",
                    data: JSON.stringify({ instituto: val }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#ddlTipoLista").empty().append($("<option></option>").val("0").html("Carregando..."));

                        if ($("#tblItensListaControle > tbody tr").size() > 0) {
                            $("#tblItensListaControle > tbody").empty();
                        }
                    },
                    success:
                        function (msg) {
                            $("#ddlTipoLista").empty().append($("<option></option>").val("0").html("Selecione..."));
                            $.each(jQuery.parseJSON(msg.d), function () {
                                $("#ddlTipoLista").append($("<option></option>").val(this['SeqListaControle']).html(this['NomeListaControle']));
                            });
                            
                            $("#ddlTipoLista").chosen({
                                    disable_search_threshold: 10,
                                    no_results_text: "Lista não encontrada!",
                                    width: "95%"
                                });
                        }
                });
            }

            function ObterMaterial() {

                var txt = $("#txtCodigoMaterial");

                if (txt.val().length >= 5) {
                    // Obter Material
                    $.ajax({
                        type: "POST",
                        url: "ItemTipoLista.aspx/ObterMaterial",
                        data: JSON.stringify({ codigoMaterial: txt.val() }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function (e) {
                            $("#hdnCodMaterial").val('');
                            $("#txtNomeMaterial").val('');
                            $("#txtUnidade").val('');
                        },
                        success:
                            function (msg) {
                                if (jQuery.parseJSON(msg.d) == 0) {
                                    toastr.options = { 'timeOut': 5000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                                    toastr.warning("Nenhum material foi encontrado !");
                                }
                                else {
                                    $.each(jQuery.parseJSON(msg.d), function () {
                                        $("#hdnCodMaterial").val(this['Codigo']);
                                        $("#txtNomeMaterial").val(this['Nome']);
                                        $("#txtUnidade").val(this['Unidade']['Nome']);
                                    });
                                }
                            }
                    });
                } else if ($("#txtNomeMaterial").val().length > 0) {
                    $("#txtCodigoMaterial,#txtNomeMaterial,#txtUnidade,#hdnCodMaterial").val('');
                }
            }

            // Pesquisa material
            //$("#txtCodigoMaterial").on("blur", function () {

                
            //});

            function carregarComAlinea() {
                // Carregando combo Alinea
                $.ajax({
                    type: "POST",
                    url: "ItemTipoLista.aspx/CarregarComboAlinea",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#ddlAlineaMaterialSemCodigo").empty().append($("<option></option>").val("").html("CARREGANDO..."));
                    },
                    success:
                    function (msg) {
                        $("#ddlAlineaMaterialSemCodigo").empty().append($("<option></option>").val("0").html("SELECIONE..."));
                        $.each(jQuery.parseJSON(msg.d), function () {
                            $("#ddlAlineaMaterialSemCodigo").append($("<option></option>").val(this['Codigo']).html(this['Nome']));
                        });
                    }
                });
            }

            function carregarComboUnidade() {
                // Carregando combo Alinea
                $.ajax({
                    type: "POST",
                    url: "ItemTipoLista.aspx/CarregarComboUnidadeDeMedida",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#ddlUnidadeDeMedidaMaterialSemCodigo").empty().append($("<option></option>").val("").html("CARREGANDO..."));
                    },
                    success:
                    function (msg) {
                        $("#ddlUnidadeDeMedidaMaterialSemCodigo").empty().append($("<option></option>").val("0").html("SELECIONE..."));
                        $.each(jQuery.parseJSON(msg.d), function () {
                            $("#ddlUnidadeDeMedidaMaterialSemCodigo").append($("<option></option>").val(this['Codigo']).html(this['Nome']));
                        });
                    }
                });
            }

            function InserirMaterial(btn, codigoMaterial, quantidadeNecessaria, codigoUnidade, descricaoMaterialSemCodigo, codigoAlinea, codigotipoBem) {

                var itemMaterial;

                $(btn).button('loading');

                if (codigoMaterial != null) {
                    // Montando o material com código
                    itemMaterial = {
                        "QuantidadeNecessaria": quantidadeNecessaria,
                        "DescricaoMaterial": "",
                        "Unidade": { "Codigo": codigoUnidade },
                        "Alinea": { "Codigo": codigoAlinea },
                        "TipoBem": { "CodigoTipoBem": codigotipoBem },
                        "Material": { "Codigo": codigoMaterial },
                        "ListaControle": { "SeqListaControle": $('#ddlTipoLista option:selected').val() }
                    };
                }
                else if (codigotipoBem == null) //Material sem código
                {
                    itemMaterial = {
                        "QuantidadeNecessaria": quantidadeNecessaria,
                        "DescricaoMaterial": descricaoMaterialSemCodigo,
                        "Unidade": { "Codigo": codigoUnidade },
                        "Alinea": { "Codigo": codigoAlinea },
                        "TipoBem": { "CodigoTipoBem": codigotipoBem },
                        //"Material": { "Codigo": codigoMaterial },
                        "ListaControle": { "SeqListaControle": $('#ddlTipoLista option:selected').val() }
                    };

                } else { //Equipamento
                    itemMaterial = {
                        "QuantidadeNecessaria": quantidadeNecessaria,
                        //"DescricaoMaterial": descricaoMaterialSemCodigo,
                        //"Unidade": { "Codigo": codigoUnidade },
                        //"Alinea": { "Codigo": codigoAlinea },
                        "TipoBem": { "CodigoTipoBem": codigotipoBem },
                        //"Material": { "Codigo": codigoMaterial },
                        "ListaControle": { "SeqListaControle": $('#ddlTipoLista option:selected').val() }
                    };
                }


                $.ajax({
                    type: "POST",
                    url: "ItemTipoLista.aspx/InserirMaterial",
                    data: JSON.stringify({ "item": itemMaterial }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $(btn).button('Processando');
                    },
                    success:
                        function (msg) {
                            
                            if (jQuery.parseJSON(msg.d) == 0) {
                                
                                    toastr.options = { 'timeOut': 5000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                                    toastr.warning("Este material ja existe na lista !");
                                } else {
                                    toastr.options = { 'timeOut': 5000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                                    toastr.success("Item inserido com sucesso !");
                            }
                            
                        },
                    
                }).always(function () {
                    $(btn).button('reset');
                });
            }

            function AtivarInativarItem(_seqItemListaControle) {

                var acao;
                if ($("#btnAtivarInativar").text().toUpperCase() == 'ATIVAR') {
                    acao = true;

                } else {
                    acao = false;
                }

                //bool ehPraAtivar,long seqItemListaControle
                $.ajax({
                    type: "POST",
                    url: "ItemTipoLista.aspx/AtivarInativarItem",
                    data: JSON.stringify({ "ehPraAtivar": acao, "seqItemListaControle": _seqItemListaControle }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {

                    },
                    success:
                        function (msg) {

                            $("#" + _seqItemListaControle).toggleClass('danger').toggleClass('notdanger');

                            toastr.options = { 'timeOut': 2000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                            toastr.success("Item alterado com sucesso !");
                        }
                });

                // Esconde o modal de opções
                $('#modalAcoes').modal('hide');
            }

            function ListarItensListaControle() {

                $.ajax({
                    type: "POST",
                    url: "ItemTipoLista.aspx/ObterItensPorListaControle",
                    data: JSON.stringify({ "seqlistaControle": $('#ddlTipoLista option:selected').val(), "status": $('#chkFiltraInativo').is(':checked') }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
//                        if ($("#tblItensListaControle > tbody tr").size() > 0) {
//                            $("#tblItensListaControle > tbody").empty();
//                        }
                    },
                    success:
                        function (msg) {
                            
                            if ($("#tblItensListaControle > tbody tr").size() > 0) {
                                $("#tblItensListaControle > tbody").empty();
                            }

                            $.each(jQuery.parseJSON(msg.d), function () {

                                var btnAtivoInativo;
                                var iconeAtivoInativo;
                                var linha;
                                var idlinha = this['SeqItensListaControle'];
                                var funcaoAtivaInativa;
                                if (this['IdfAtivo'] == 'S') {
                                    funcaoAtivaInativa = 'INATIVAR';
                                    linha = "notdanger";
                                } else {
                                    linha = "danger";
                                    funcaoAtivaInativa = 'ATIVAR';
                                }


                                $("#tblItensListaControle > tbody").append(
                                        "<tr class='clickLinha " + linha + "' id='" + idlinha + "'>" +
                                            "<td>" + this['Alinea']['Nome'] + "</td>" +
                                            "<td> " + this['Material']['Codigo'] + "</td>" +
                                            "<td>" + this['Material']['Nome'] + "</td>" +
                                            "<td>" + this['QuantidadeNecessaria'] + " / " + this['Unidade']['Nome'] + "</td>" +
                                            "<td>" +
                                            "   <button type='button' class='btn btn-default btn-sm' data-toggle='modal' data-target='#modalAcoes'><span class='glyphicon glyphicon-list'></span></button>" +
                                            "</td>" +
                                        "</tr>");
                            });
                        }
                });
                
                
            }

            // Configurando a modal de opções
            $('#modalAcoes').on('show.bs.modal', function (e) {
                //var btn = $(e.relatedTarget);
                var idLinha = $(e.relatedTarget).closest("tr").attr("id");

                // Se a linha tiver a class "danger" quer dizer o registro esta inativo
                if ($(e.relatedTarget).closest("tr").hasClass('danger')) {
                    $("#btnAtivarInativar").text('Ativar');
                } else {
                    $("#btnAtivarInativar").text('Inativar');
                }

                // Alinea + Medicamento
                var info = $($(e.relatedTarget).closest("tr").find("td")[0]).text() + " - " + $($(e.relatedTarget).closest("tr").find("td")[2]).text();

                // Recuperar a coluna quandidade necessária composta  : "Quant. / Unidade"
                var arrquantidadeNecUnidadeMedida = $($(e.relatedTarget).closest("tr").find("td")[3]).text().split('/'); ;

                // Recupera informações da linha do grid para mostrar na modal - unidade + item
                $('#modalAcoes h4').html(info);

                // atribui o onclick par ao botão inativar/ativar
                $("#btnAtivarInativar").off().on("click", function () {
                    AtivarInativarItem(idLinha);
                    //alert(idLinha);
                });

                // Atribui o onclick para o botão alterar dados
                $("#btnAlterar").off().on("click", function () {
                    //Fecha a modal de ações e abre a modal de alteração de dados
                    $('#modalAcoes').modal('hide');
                    $('#modalAlterarDados').modal('show');

                    // Info
                    $('#modalAlterarDados h4').html(info);
                    $('#txtAlteracaoQuantidadeNecessaria').val(arrquantidadeNecUnidadeMedida[0].trim());
                    $('#txtUnidadeAlteracao').val(arrquantidadeNecUnidadeMedida[1].trim());

                    //var btn = $(e.relatedTarget);
                    //var idLinha = $("#hdnidLinha").val();

                    // Atribuindo o evento click para o botão salvar
                    $("#btnSalvarAlteracaoDados").off().on("click", function () {
                        AlterarDadosItem(idLinha);
                        //alert(idLinha);
                    });
                });
            });

            function AlterarDadosItem(idLinha) {
                //bool ehPraAtivar,long seqItemListaControle
                $.ajax({
                    type: "POST",
                    url: "ItemTipoLista.aspx/AlterarDadosItem",
                    data: JSON.stringify({ "seqItem": idLinha, "quantidade": $("#txtAlteracaoQuantidadeNecessaria").val() }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {

                    },
                    success:
                        function (msg) {

                            toastr.options = { 'timeOut': 2000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                            toastr.success("Item alterado com sucesso !");

                            ListarItensListaControle();
                        }
                });

                // Esconde o modal de opções
                $('#modalAlterarDados').modal('hide');

            }

            // IndexChanging dropdownlist Instituto
            $('#ddlInstituto').change(function () {
                CarregaComboTipoLista($('#ddlInstituto option:selected').val());
                $("#divConteudo").fadeOut("show");
            });

            // IndexChanging dropdownlist TipoLista
            $('#ddlTipoLista').change(function () {
                if ($(this).val() != 0) {
                    $("#divConteudo").fadeIn("slow");
                    ListarItensListaControle();
                } else {
                    $("#divConteudo").fadeOut("slow");
                }
            });

            // Quando o modal mostrar na tela ..setar o foco para o primeiro input da tela
            $("#myModal").on('shown.bs.modal', function () {
                $(this).find(".modal-body :input:visible").first().focus();
            });

            $("#modalAlterarDados").on('shown.bs.modal', function () {
                $(this).find(".modal-body :input:visible").first().focus();

            });

            $("#modalImportarItens").on('shown.bs.modal', function () {
                $(this).find(".modal-body :input:visible").first().focus();
                $(this).find("#hdnListaPara").val($('#ddlTipoLista option:selected').val());

                //alert((this).find("#hdnListaPara").val());
            });

            // Evento disparado quando modal é fechado
            // Necessario destruir o modal
            $("#myModal").on('hidden.bs.modal', function () {
                $(this).data('bs.modal', null);
            });


            $("#btnAdicionarMaterial").on("click", function () {

                if ($("#rItemComCodigo").hasClass("active")) {

                    if ($("#hdnCodMaterial").val() == "") {
                        ExibirMensagemAlerta("Pesquise um material");

                        $("#txtCodigoMaterial").focus();
                        return;
                    }
                    
                    if ($("#txtQuantidadeMaterialComCodigo").val() == "" || $("#txtQuantidadeMaterialComCodigo").val() == 0) {
                        ExibirMensagemAlerta("Digite uma quantidade");

                        $("#txtQuantidadeMaterialComCodigo").focus();
                        return;
                    }

                    InserirMaterial(this, $("#hdnCodMaterial").val(), $("#txtQuantidadeMaterialComCodigo").val(), 0, null, 0, null);

                    LimparCampos(["#txtCodigoMaterial", "#txtNomeMaterial", "#txtUnidade", "#txtQuantidadeMaterialComCodigo"]);
                    
                    ListarItensListaControle();

                } else if ($("#rItemSemCodigo").hasClass("active")) {
                    //Valida campos obrigatório
                    if ($("#txtDescricaoMaterialSemCodigo").val() == "") {
                        ExibirMensagemAlerta("Digite uma descrição para o material");

                        $("#txtDescricaoMaterialSemCodigo").focus();
                        return;
                    }
                    
                    if ($('#ddlAlineaMaterialSemCodigo option:selected').val() == "0") {
                        ExibirMensagemAlerta("Selecione a alinea !");

                        $("#ddlAlineaMaterialSemCodigo").focus();
                        return;
                    }
                    
                    if ($('#ddlUnidadeDeMedidaMaterialSemCodigo option:selected').val() == "0") {
                        ExibirMensagemAlerta("Selecione uma unidade !");

                        $("#ddlUnidadeDeMedidaMaterialSemCodigo").focus();
                        return;
                    }
                    
                    if ($("#txtQuantidadeMaterialSemCodigo").val() == "" || $("#txtQuantidadeMaterialSemCodigo").val() == 0) {
                        ExibirMensagemAlerta("Digite uma quantidade");

                        $("#txtQuantidadeMaterialSemCodigo").focus();
                        return;
                    }


                    InserirMaterial(this, null, $("#txtQuantidadeMaterialSemCodigo").val(),
                        $('#ddlUnidadeDeMedidaMaterialSemCodigo option:selected').val(), $("#txtDescricaoMaterialSemCodigo").val(),
                        $('#ddlAlineaMaterialSemCodigo option:selected').val(), null);

                    LimparCampos(["#txtQuantidadeMaterialSemCodigo", "#ddlUnidadeDeMedidaMaterialSemCodigo", "#ddlAlineaMaterialSemCodigo", "#txtDescricaoMaterialSemCodigo"]);

                    ListarItensListaControle();

                    //$(this).button('reset');
                }
                else {
                    
                    if ($("#txtCodigoTipoBem").val() == "" || $("#txtCodigoTipoBem").val() == 0) {
                        ExibirMensagemAlerta("Pesquise um equipamento !");

                        $("#txtCodigoTipoBem").focus();
                        return;
                    }
                    
                    if ($("#txtQuantidadeBem").val() == "" || $("#txtQuantidadeBem").val() == 0) {
                        ExibirMensagemAlerta("Digite uma quantidade !");

                        $("#txtQuantidadeBem").focus();
                        return;
                    }

                    InserirMaterial(this, null, $("#txtQuantidadeBem").val(),
                        null, null,
                        null, $("#txtCodigoTipoBem").val());

                    LimparCampos(["#txtQuantidadeBem", "#txtCodigoTipoBem", "#txtNomeTipoBem"]);

                    ListarItensListaControle();
                }


            });
        });

    </script>
</asp:Content>
