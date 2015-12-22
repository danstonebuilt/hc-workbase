<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Modal.Master" AutoEventWireup="true"
    CodeBehind="BuscaTipoBem.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Comum.BuscaTipoBem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphCabecalho" runat="server">
    Pesquisa de equipamento
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCorpoPagina" runat="server">
    <div class="form-group">
        <label class="col-sm-2 control-label">
            Código</label>
        <div id="divCodigoTipoBem" class="col-sm-5">
            <input type="text" class="form-control input-sm" id="txtCodigoTipoBemFilho" />
        </div>
    </div>
    <div class="form-group">
        <label class="col-sm-2 control-label">
            Descrição</label>
        <div id="divDescricaoMaterial" class="col-sm-8" >
            <input type="text" class="form-control input-sm" id="txtDescricaoTipoBemFilho" />
        </div>
    </div>
    <div class="form-group">
        <div class="col-sm-offset-2 col-sm-10" style="text-align: right">
            <input type="button" id="btnPesquisar" value="Pesquisar" class="btn btn-success" />
        </div>
    </div>
    <br />
    <i>Clique sob a descrição do material para selecioná-lo.</i>
    <table id="tblTipoBem" class="table table-bordered table-hover table-condensed">
        <thead>
            <tr>
                <th>
                    Código
                </th>
                <th>
                    Nome
                </th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
    <div style="text-align: right; margin-top: 5px;">
        <asp:Label runat="server" ID="lblTotalRegistro"></asp:Label>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {

            // Mostrar popover// Mostrar popover// Mostrar popover// Mostrar popover
            //            $("#txtCodigoMaterialFilho").on("focus", function () {
            //                $("#divCodigoMaterial").popover('show');
            //            }).on('focusout', function () {
            //                $("#divCodigoMaterial").popover('hide');
            //            });
            //            $("#txtDescricaoMaterial").on("focus", function () {
            //                $("#divDescricaoMaterial").popover('show');
            //            }).on('focusout', function () {
            //                $("#divDescricaoMaterial").popover('hide');
            //            });

            //            $("#divCodigoMaterial").popover({ "trigger": "hover" });
            //            $("#divDescricaoMaterial").popover({ "trigger": "hover" });
            // Mostrar popover// Mostrar popover// Mostrar popover// Mostrar popover

            $("#btnPesquisar").on("click",
                function () {
                    var _nomeTipoBem = $("#txtDescricaoTipoBemFilho").val();
                    var _codigoTipoBem = $("#txtCodigoTipoBemFilho").val();


                    if (_codigoTipoBem.length == 0 && _nomeTipoBem.length == 0) {
                        toastr.options = { 'timeOut': 11000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                        toastr.info("Digite o código ou o nome do Bem que deseja pesquisar !");
                        return false;
                    }

                    if (_codigoTipoBem.length == 0 && _nomeTipoBem.length <= 2) {
                        toastr.options = { 'timeOut': 11000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                        toastr.info("Digite pelo menos 3 caracteres do nome para pesquisar !");
                        return false;
                    }


                    $.ajax({
                        type: "POST",
                        url: "../Comum/BuscaTipoBem.aspx/PesquisarTipoBem",
                        data: JSON.stringify({ codigoTipoBem: _codigoTipoBem, nomeTipoBem: _nomeTipoBem }),
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        beforeSend: function () {
                            // Limpa a table antes do bind
                            if ($("#tblTipoBem > tbody tr").size() > 0) {
                                $("#tblTipoBem > tbody").empty();
                            }
                        },
                        success:
                            function (msg) {
                                // Carrega a tabela com os dados
                                $.each(jQuery.parseJSON(msg.d), function () {

                                    console.log(jQuery.parseJSON(msg.d));
                                    $("#tblTipoBem > tbody").append(
                                        "<tr class='clickLinha' style='cursor:pointer'>" +
                                            "<td>" + this['CodigoTipoBem'] + "</td>" +
                                            "<td> " + this['NomeTipoBem'] + "</td>" +
                                        "</tr>");
                                });


                                // Recupera os valores da linha da table
                                // [0]=Alínea [1]=Unidade [2]=Código [3]DescricaoItem
                                $('#tblTipoBem > tbody > tr').click(function () {
                                    var arrayLinha = $(this).find('td').map(function () {
                                        return $(this).text();
                                    });

                                    //Retorna os valores para os campos da pagina pai
                                    $("#txtCodigoTipoBem").val(arrayLinha[0]);
                                    $("#txtNomeTipoBem").val(arrayLinha[1]);

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
