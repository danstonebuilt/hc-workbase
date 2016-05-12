<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/HcrpMaster.Master"
    CodeBehind="MovimentacaoRepositorio.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Movimentacao.MovimentacaoRepositorio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
    <style type="text/css">
        .remove-space
        {
            margin: 0 auto !important;
            text-align: center;
        }
        
        fieldset.scheduler-border
        {
            border: 1px groove #ddd !important;
            padding: 0 1.4em 1.4em 1.4em !important;
            margin: 0 0 1.5em 0 !important;
            -webkit-box-shadow: 0px 0px 0px 0px #000;
            box-shadow: 0px 0px 0px 0px #000;
        }
        
        legend.scheduler-border
        {
            font-size: 1.2em !important;
            font-weight: bold !important;
            text-align: left !important;
            width: inherit; /* Or auto */
            padding: 0 10px; /* To give a bit of padding on the left and right */
            border-bottom: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphCabecalho" runat="server">
    Movimentação de Repositório
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphConteudo" runat="server">
    <input type="hidden" id="hdcodparsed" runat="server" />
    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
            </div>
        </div>
    </div>
    <fieldset class="scheduler-border">
        <legend class="scheduler-border">Repositório</legend>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Cod.Bar repositório</label>
            <div class="col-sm-4">
                <div class="input-group input-group-sm">
                    <input type="text" id="txtCodBarRepositorio" class="form-control" data-placement="top"
                        data-content="Código barra repositório" name="txtCodBarRepositorio" />
                    <span class="input-group-btn">
                        <button type="button" class="btn btn-default" href="../Pesquisa/Repositorio.aspx?acao=codbar"
                            data-toggle="modal" data-target="#myModal">
                            <span class="glyphicon glyphicon-search"></span>
                        </button>
                    </span>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Desc. repositório</label>
            <div class="col-sm-8">
                <input type="text" id="txtRepositorio" class="form-control " data-placement="top"
                    data-content="Código barra repositório" name="txtRepositorio" readonly="readonly" />
            </div>
        </div>
    </fieldset>
    <fieldset class="scheduler-border">
        <legend class="scheduler-border">Centro Custo de Origem</legend>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Cod.C/C orig.</label>
            <div class="col-sm-4">
                <div class="input-group input-group-sm">
                    <input type="text" id="txtCodCustOrigem" class="form-control" data-placement="top"
                        data-content="Digite o código do Centro de custo" name="txtCodCustOrigem" pattern="^[A-Z]{4}\d{5}$"  />
                    <span class="input-group-btn">
                        <button type="button" class="btn btn-default" href="../Pesquisa/CentrodeCusto.aspx?acao=origem"
                            data-toggle="modal" data-target="#myModal">
                            <span class="glyphicon glyphicon-search"></span>
                        </button>
                    </span>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Desc. C/C orig.</label>
            <div class="col-sm-8">
                <input type="text" id="txtCentCustOrigem" class="form-control" data-placement="top"
                    data-content="Centro custo de origem" name="txtCentCustOrigem" readonly="readonly" />
            </div>
        </div>
    </fieldset>
    <fieldset class="scheduler-border">
        <legend class="scheduler-border">Centro Custo de Destino</legend>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Cod.C/C dest.</label>
            <div class="col-sm-4">
                <div class="input-group input-group-sm">
                    <input type="text" id="txtCodCentCustDest" class="form-control" data-placement="top"
                        data-content="Centro Custo de destino" name="txtCodCentCustDest" pattern="^[A-Z]{4}\d{5}$" />
                    <span class="input-group-btn">
                        <button type="button" class="btn btn-default" href="../Pesquisa/CentrodeCusto.aspx?acao=destino"
                            data-toggle="modal" data-target="#myModal">
                            <span class="glyphicon glyphicon-search"></span>
                        </button>
                    </span>
                </div>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Desc. C/C dest.</label>
            <div class="col-sm-8">
                <input type="text" id="txtCentCustDest" class="form-control" data-placement="top"
                    data-content="Centro Custo de destino" name="txtCentCustDest" readonly="readonly" />
            </div>
        </div>
    </fieldset>
    <div class="remove-space">
        <asp:Button ID="btDispensar" class="btn btn-success" runat="server" Text="Dispensar"
            BackColor="#4CA44C" Font-Bold="True" ForeColor="White" OnClick="btDispensar_Click" />
    </div>
    <script type="text/javascript" language="javascript">
        $(function () {
            var $myModal = $("#myModal");
            var $txtCodBarRepositorio = $('#txtCodBarRepositorio');
            var $txtRepositorio = $('#txtRepositorio');
            var $txtCodCustOrigem = $('#txtCodCustOrigem');
            var $txtCentCustOrigem = $('#txtCentCustOrigem');
            var $txtCodCentCustDest = $('#txtCodCentCustDest');
            var $txtCentCustDest = $('#txtCentCustDest');

            // Quando o modal mostrar na tela ..setar o foco para o primeiro input da tela
            $myModal.on('shown.bs.modal', function () {
                $(this).find(".modal-body :input:visible").first().focus();
            });

            // Evento disparado quando modal é fechado
            // Necessario destruir o modal
            $myModal.on('hidden.bs.modal', function () {
                $(this).data('bs.modal', null);
            });

            $txtCodBarRepositorio.on('keypress', function (evt) {


                if (evt.which == '13') {
                    if (validateFields($(this)) > 0) {
                        return false;
                    }
                    else {
                        var sequencia = parseCodBarReposit($(this).val());
                        carregarRepositorioPorCodigoBarra(sequencia);
                    }
                    evt.preventDefault();
                }                
            });

            $txtCodCustOrigem.on('keypress', function (evt) {


                if (evt.which == '13')
                {
                    if (validateFields($(this)) > 0) {
                        return false;
                    }
                    else {                        
                        $txtCentCustOrigem.val(carregarCentroCustoOrg($(this).val()));
                    }
                    evt.preventDefault();
                }
            });

            $txtCodCentCustDest.on('keypress', function (evt) {

                if (evt.which == '13')
                {
                    if (validateFields($(this), $txtCodCustOrigem) > 0) {
                        return false;
                    }
                    else {
                        $txtCentCustDest.val(carregarCentroCustoDst($(this).val()));
                    }
                    evt.preventDefault();
                }
            });

            function carregarRepositorioPorCodigoBarra(codBar) {

                $.ajax({
                    type: "POST",
                    url: "MovimentacaoRepositorio.aspx/PesquisarRepositorio",
                    data: JSON.stringify({
                        codBar: codBar,
                        descricao: null
                    }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",

                    success: function (json) {
                        $.each(jQuery.parseJSON((json.d)), function () {

                            $txtRepositorio.val(this["DscIdentificacao"]);

                            if (empty($txtRepositorio.val())) {

                                ExibirMensagemAlerta("Repositório não encontrado");
                            }

                        });
                    }
                });
            }

            function carregarCentroCustoOrg(codCC) {

                var jsonObj;

                $.ajax({
                    type: "POST",
                    url: "MovimentacaoRepositorio.aspx/ObterCentroCustoLocalDestino",
                    data: JSON.stringify({ codigoCentroCusto: codCC }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",

                    success: function (json) {
                        jsonObj = jQuery.parseJSON(json.d);
                        $.each((jsonObj), function () {

                            $txtCentCustOrigem.val(jsonObj.NomeCentroCusto);
                        });

                        if (empty($txtCentCustOrigem.val())) {

                            ExibirMensagemAlerta("Centro de Custo Origem não encontrado");
                        }
                    }
                });
            }

            function carregarCentroCustoDst(codCC) {

                var jsonObj;

                $.ajax({
                    type: "POST",
                    url: "MovimentacaoRepositorio.aspx/ObterCentroCustoLocalDestino",
                    data: JSON.stringify({ codigoCentroCusto: codCC }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",

                    success: function (json) {
                        jsonObj = jQuery.parseJSON(json.d);
                        $.each((jsonObj), function () {

                            $txtCentCustDest.val(jsonObj.NomeCentroCusto);
                        });

                        if (empty($txtCentCustDest.val())) {
                            ExibirMensagemAlerta("Centro de Custo Destino não encontrado");
                        }
                    }
                });
            }

        });
        //---------------------------------------------------
        /*Objects*/
        var validateEmpty = {

            validar: function (field) {
                if (typeof field == "object") {
                    if (field.val() == "")
                        return true;
                }
            },

            motivo: function () {
                ExibirMensagemAlerta("Campo não pode ser vazio.");                
            }
        }

        var validateFormatCodBar = {

            validar: function (field) {
                if (typeof field == "object") {
                    var er = /^[A-Z]{2}\d{8}$/;

                    if (!er.test(field.val()))
                        return true;
                }
            },

            motivo: function () {

                ExibirMensagemAlerta("Código Repositório não esta no Formato correto.");                
            }
        }

        var validateFormatCodCC = {

            validar: function (field) {
                if (typeof field == "object") {
                    var er = /^[A-Z]{4}\d{5}$/;

                    if (!er.test(field.val()))
                        return true;
                }
            },

            motivo: function () {

                ExibirMensagemAlerta("Código Centro de Custo não esta no Formato correto.");
            }
        }


        var validateEqualityCodDest = {

            validar: function (field, _opc) {              
                    if (field.val() == _opc.val())
                        return true;        

            },

            motivo: function () {
                ExibirMensagemAlerta("Código Centro de Custo Destino não pode ser igual origem.");
            }
        }
        //---------------------------------------------------
        /*Common functions*/

        function validateFields(input, _opcInput)
        {

            var validations = [];

            if (input.attr("id") == "txtCodBarRepositorio") 
                validations = new Array(validateEmpty, validateFormatCodBar);
            else if (input.attr("id") == "txtCodCustOrigem")
                validations = new Array(validateEmpty, validateFormatCodCC);
            else if (input.attr("id") == "txtCodCentCustDest")
                validations = new Array(validateEmpty, validateFormatCodCC, validateEqualityCodDest);

            var exceptions = 0;

            $.each(validations, function (index, val) {
                if (val.validar(input, _opcInput)) {
                    val.motivo();
                    exceptions++;
                    return false;
                }                
            });

            return exceptions;
        }

        function empty(v) {
            if (v == null || v == 0 || v == '' || v == "")
                return true
            else
                return false
        }

        function parseCodBarReposit(sequencia) {

            return $.grep(sequencia.slice(2, 10), function (indice) {
                return indice != '0';
            }).toString().replace(/,/g, "");
        }                      
       
    </script>
</asp:Content>
