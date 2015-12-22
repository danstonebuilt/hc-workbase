<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="BuscaMaterialListaControleItem.aspx.cs" MasterPageFile="~/Master/Modal.master"
    Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Comum.BuscaMaterialListaControleItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecalho" runat="server">
    Pesquisa de materiais
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphCorpoPagina" runat="server">
    <div class="form-group">
    <label class="col-sm-2 control-label">
        Código</label>
    <div id="divCodigoMaterial" class="col-sm-5" data-toggle="popover" data-placement="right"
        data-content="8 caracteres">
        <input type="text" class="form-control input-sm" id="txtCodigoMaterialFilho" />
    </div>
</div>
<div class="form-group">
    <label class="col-sm-2 control-label">
        Descrição</label>
    <div id="divDescricaoMaterial" class="col-sm-8" data-toggle="popover" data-placement="right"
        data-content="Minímo 5 caracteres">
        <input type="text" class="form-control input-sm" id="txtDescricaoMaterial" />
    </div>
</div>
<div class="form-group">
    <label class="col-sm-2 control-label">
        Alínea</label>
    <div class="col-sm-8">
        <select  id="ddlAlinea" class="form-control input-sm" data-toggle="dropdown"
            clientidmode="Static">
            </select>
    </div>
</div>
<div class="form-group">
    <div class="col-sm-offset-2 col-sm-10" style="text-align: right">
        <input type="button" id="btnPesquisar" value="Pesquisar" class="btn btn-success" />
    </div>
</div>
<br />
<i>Clique sob a descrição do material para selecioná-lo.</i>
<table id="tblMaterial" class="table table-bordered table-hover table-condensed">
    <thead>
        <tr>
            <th>
                Alínea
            </th>
            <th>
                Unidade
            </th>
            <th>
                Código
            </th>
            <th>
                Item
            </th>
        </tr>
    </thead>
    <tbody>
    </tbody>
</table>
<div style="text-align: right; margin-top: 5px;">
    <asp:label runat="server" id="lblTotalRegistro"></asp:label>
</div>
<script type="text/javascript">
    $(document).ready(function () {

        // Mostrar popover// Mostrar popover// Mostrar popover// Mostrar popover
        $("#txtCodigoMaterialFilho").on("focus", function () {
            $("#divCodigoMaterial").popover('show');
        }).on('focusout', function () {
            $("#divCodigoMaterial").popover('hide');
        });
        $("#txtDescricaoMaterial").on("focus", function () {
            $("#divDescricaoMaterial").popover('show');
        }).on('focusout', function () {
            $("#divDescricaoMaterial").popover('hide');
        });

        $("#divCodigoMaterial").popover({ "trigger": "hover" });
        $("#divDescricaoMaterial").popover({ "trigger": "hover" });
        // Mostrar popover// Mostrar popover// Mostrar popover// Mostrar popover

        // Carregando combo Alinea
        $.ajax({
            type: "POST",
            url: "../Comum/BuscaMaterialListaControleItem.aspx/CarregarComboAlinea",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                $("#ddlAlinea").empty().append($("<option></option>").val("").html("CARREGANDO..."));
            },
            success:
                    function (msg) {
                        $("#ddlAlinea").empty().append($("<option></option>").val("0").html("SELECIONE..."));
                        $.each(jQuery.parseJSON(msg.d), function () {
                            $("#ddlAlinea").append($("<option></option>").val(this['Codigo']).html(this['Nome']));
                        });
                    }
        });

        $("#btnPesquisar").on("click",
                function () {
                    var _nomeMaterial = $("#txtDescricaoMaterial").val();
                    var _codigoMaterial = $("#txtCodigoMaterialFilho").val();
                    var _codigoAlinea = $("#ddlAlinea option:selected").val();

                    // Se codigo do material for digitado valida
                    if (_codigoMaterial.length != 0) {
                        if (_codigoMaterial.length != 8) {
                            toastr.options = { 'timeOut': 11000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                            toastr.info("O código do material é composto por 8 caracteres, digite corretamente !");
                            return false;
                        }
                    }
                    else {
                        if (_codigoAlinea == "0" || _nomeMaterial.length < 5) {
                            toastr.options = { 'timeOut': 11000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                            toastr.info("Pelo menos alinea e a descrição do material(minímo 5 caracteres) devem ser preenchidos para realizar a pesquisa !");
                            return false;
                        }
                    }

                    $.ajax({
                        type: "POST",
                        url: "../Comum/BuscaMaterialListaControleItem.aspx/PesquisarMaterial",
                        data: JSON.stringify({ codigoMaterial: _codigoMaterial, DescricaoMaterial: _nomeMaterial, CodigoAlinea: _codigoAlinea }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            // Limpa a table antes do bind
                            if ($("#tblMaterial > tbody tr").size() > 0) {
                                $("#tblMaterial > tbody").empty();
                            }
                        },
                        success:
                            function (msg) {
                                // Carrega a tabela com os dados
                                $.each(jQuery.parseJSON(msg.d), function () {

                                    //console.log(msg.d);

                                    $("#tblMaterial > tbody").append(
                                        "<tr class='clickLinha' style='cursor:pointer'>" +
                                            "<td>" + this['Alinea']['Nome'] + "</td>" +
                                            "<td> " + this['Unidade']['Nome'] + "</td>" +
                                            "<td>" + this['Codigo'] + "</td>" +
                                            "<td>" + this['Nome'] + "</td>" +
                                        "</tr>");
                                });


                                // Recupera os valores da linha da table
                                // [0]=Alínea [1]=Unidade [2]=Código [3]DescricaoItem
                                $('#tblMaterial > tbody > tr').click(function () {
                                    var arrayLinha = $(this).find('td').map(function () {
                                        return $(this).text();
                                    });

                                    //Retorna os valores para os campos da pagina pai
                                    $("#txtCodigoMaterial").val(arrayLinha[2]);
                                    $("#txtNomeMaterial").val(arrayLinha[3]);
                                    $("#txtUnidade").val(arrayLinha[1]);

                                    // Fecha o modal
                                    $('#myModal').modal('hide');
                                });
                            }
                    });
                }
            );
    });
</script>
</asp:Content>
