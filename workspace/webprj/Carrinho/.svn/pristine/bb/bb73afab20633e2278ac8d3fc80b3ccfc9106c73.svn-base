<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Modal.Master" AutoEventWireup="true"
    CodeBehind="ImportaItensOutraLista.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Comum.ImportaItensOutraLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecalho" runat="server">
    Importar Lista
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCorpoPagina" runat="server">
    <input type="hidden" id="hdnListaPara"/>
    <div class="form-group">
        <label class="col-sm-4 control-label">
            Instituto</label>
        <div class="col-sm-8">
            <select  ID="ddlInstitutoModal" class="form-control" data-toggle="dropdown"
                Style="max-width: 250px" >
            </select>
        </div>
    </div>
    <div class="form-group">
        <label class="col-sm-4 control-label">
            Tipo de lista</label>
        <div class="col-sm-8">
            <select ID="ddlTipoListaModal" class="form-control" data-toggle="dropdown"
                Style="max-width: 250px" >
            </select>
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-12 ">
            <button type="button" class="btn btn-success pull-right" id="btnImportarItens">Importar</button>
        </div>
    </div>
    <p class="alert-info">Os itens que já existem na lista alvo, não serão importados.</p>
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
                        Quantidade necessária
                    </th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </div>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            // Carregando combo Instituto
            $.ajax({
                type: "POST",
                url: "../Comum/ImportaItensOutraLista.aspx/CarregarComboInstituto",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                beforeSend: function (e) {
                    $("#ddlInstitutoModal").empty().append($("<option></option>").val("0").html("Carregando..."));
                },
                success:
                    function (msg) {
                        $("#ddlInstitutoModal").empty().append($("<option></option>").val("0").html("Selecione..."));
                        $.each(jQuery.parseJSON(msg.d), function () {
                            $("#ddlInstitutoModal").append($("<option></option>").val(this['CodInstituto']).html(this['NomeInstituto']));
                        });
                    }
            });

            $('#btnImportarItens').on("click", function () {
                ImportarItens();
            });

            function CarregaComboTipoListaModal(val) {
                // Carregando combo Instituto
                $.ajax({
                    type: "POST",
                    url: "../Comum/ImportaItensOutraLista.aspx/CarregarComboTipoLista",
                    data: JSON.stringify({ instituto: val }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $("#ddlTipoListaModal").empty().append($("<option></option>").val("0").html("Carregando..."));

                        if ($("#tblItensListaControleModal > tbody tr").size() > 0) {
                            $("#tblItensListaControleModal > tbody").empty();
                        }
                    },
                    success:
                        function (msg) {
                            $("#ddlTipoListaModal").empty().append($("<option></option>").val("0").html("Selecione..."));
                            $.each(jQuery.parseJSON(msg.d), function () {
                                $("#ddlTipoListaModal").append($("<option></option>").val(this['SeqListaControle']).html(this['NomeListaControle']));
                            });
                        }
                });
            }

            // IndexChanging dropdownlist Instituto
            $('#ddlInstitutoModal').change(function () {
                CarregaComboTipoListaModal($('#ddlInstitutoModal option:selected').val());
                //$("#divConteudo").fadeOut("show");
            });

            // IndexChanging dropdownlist TipoLista
            $('#ddlTipoListaModal').change(function () {
                if ($(this).val() != 0) {
                    //$("#divConteudo").fadeIn("slow");
                    ListarItensListaControleModal();
                } else {
                    if ($("#tblItensListaControleModal > tbody tr").size() > 0) {
                        $("#tblItensListaControleModal > tbody").empty();
                    }
                }
            });

            function ListarItensListaControleModal() {

                $.ajax({
                    type: "POST",
                    url: "../Comum/ImportaItensOutraLista.aspx/ObterItensPorListaControle",
                    data: JSON.stringify({ "seqlistaControle": $('#ddlTipoListaModal option:selected').val() }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //beforeSend: 
                    success:
                        function (msg) {

                            if ($("#tblItensListaControleModal > tbody tr").size() > 0) {
                                $("#tblItensListaControleModal > tbody").empty();
                            }

                            $.each(jQuery.parseJSON(msg.d), function () {

                                var btnAtivoInativo;
                                var iconeAtivoInativo;
                                var linha;
                                var idlinha = this['SeqItensListaControle'];
                                var funcaoAtivaInativa;
                                if (this['IdfAtivo'] == 'S') {
                                    //btnAtivoInativo = "class='btn btn-danger btn-sm AtivarInativarLinha' data-toggle='tooltip' title='Inativar'";
                                    //iconeAtivoInativo = "glyphicon glyphicon-remove-circle";
                                    funcaoAtivaInativa = 'INATIVAR';
                                    linha = "notdanger";
                                } else {
                                    //btnAtivoInativo = "class='btn btn-primary btn-sm AtivarInativarLinha' data-toggle='tooltip' title='Ativar'";
                                    //iconeAtivoInativo = "glyphicon glyphicon-ok-circle";
                                    linha = "danger";
                                    funcaoAtivaInativa = 'ATIVAR';
                                }

                                //teste = teste + "<a href=''>Alterar</a></br><span class='Inativar'>Inativar" + idlinha + "</span>";

                                $("#tblItensListaControleModal > tbody").append(
                                        "<tr class='clickLinha " + linha + "' id='row" + idlinha + "'>" +
                                            "<td>" + this['Alinea']['Nome'] + "</td>" +
                                            "<td> " + this['Material']['Codigo'] + "</td>" +
                                            "<td>" + this['Material']['Nome'] + "</td>" +
                                            "<td>" + this['QuantidadeNecessaria'] + " / " + this['Unidade']['Nome'] + "</td>" +
                                        "</tr>");
                            });
                        }
                });
            }

            function ImportarItens() {

                if ($('#ddlInstitutoModal option:selected').val() == 0) {
                    toastr.options = { 'timeOut': 3500, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                    toastr.warning("Selecione o instituto !");
                    return false;
                }

                if ($('#ddlTipoListaModal option:selected').val() == 0) {
                    toastr.options = { 'timeOut': 3500, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                    toastr.warning("Selecione o tipo de lista para importação !");
                    return false;
                }

                

                $.ajax({
                    type: "POST",
                    url: "../Comum/ImportaItensOutraLista.aspx/ImportarItens",
                    data: JSON.stringify({ "seqListaDe": $('#ddlTipoListaModal option:selected').val(), "seqListaPara": $('#hdnListaPara').val() }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {

                    },
                    success:
                        function (msg) {
                            //$.each(jQuery.parseJSON(msg.d), function () {
                            toastr.options = { 'timeOut': 3500, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                            toastr.success("Itens importados com sucesso !");
                            //});

                            // Fecha o modal
                            $('#modalImportarItens').modal('hide');
                        }
                });

            }
        });
    </script>
</asp:Content>
