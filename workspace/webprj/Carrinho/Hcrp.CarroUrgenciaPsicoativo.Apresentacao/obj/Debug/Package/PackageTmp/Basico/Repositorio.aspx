<%@ Page Title="" Language="C#" MasterPageFile="~/Master/HcrpMaster.Master" AutoEventWireup="true"
    CodeBehind="Repositorio.aspx.cs" Inherits="Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Basico.Repositorio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphCabecalho" runat="server">
    Manutenção do repositório
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphConteudo" runat="server">
    <div class="modal fade " id="modalAdicionarRepositorio" tabindex="-1" role="dialog"
        aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h3 class="modal-title" id="H1">
                        Adicionar repositório</h3>
                </div>
                <div class="modal-body">
                    <input type="hidden" id="hdnNumBem" />
                    <input type="hidden" id="hdnSeqRepositorio" />
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Tipo patrimônio</label>
                        <div class="col-sm-5">
                            <select id="ddlTipoPatrimonio" class="form-control" data-toggle="dropdown">
                            </select>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Número patrimônio</label>
                        <div class="col-sm-4">
                            <div class="input-group input-group-sm">
                                <input type="text" id="txtNumeroPatrimonio" class="form-control" data-placement="top"
                                    data-content="Clique na lupa para pesquisa o patrimônio" />
                                <span class="input-group-btn">
                                    <button type="button" class="btn btn-default" id="btnPesquisarBemPatrimonio">
                                        <span class="glyphicon glyphicon-search"></span>
                                    </button>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div id="divBemEncontrado" class="hidden">
                                <span class="label label-success"><span class="glyphicon glyphicon-ok-circle"></span>
                                    Patrimônio encontrado</span>
                            </div>
                            <div id="divNaoBemEncontrado" class="hidden">
                                <span class="label label-danger"><span class="glyphicon glyphicon-remove-circle"></span>
                                    Patrimônio não encontrado</span>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Tipo reposítório</label>
                        <div class="col-sm-8">
                            <select id="ddlTipoRepositorio" class="form-control" data-toggle="dropdown">
                            </select>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Identificação</label>
                        <div class="col-sm-8">
                            <input type="text" id="txtIdentificacao" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Tipo de Lista</label>
                        <div class="col-sm-8">
                            <select id="ddlTipoLista" class="form-control" tabindex="1">
                            </select>
                        </div>
                    </div>
                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Associar centros de custo</h3>
                        </div>
                        <div class="panel-body">
                            <div class="form-group">
                                <div class="col-sm-4">
                                    <div class="input-group input-group-sm">
                                        <span class="input-group-addon">Cod. C/C</span>
                                        <input type="text" id="txtCodigoCentroCusto" class="form-control" data-placement="top"
                                            maxlength="9" data-content="Digite o código do centro de custo para pesquisar e pressione Enter" />
                                        <span class="input-group-btn">
                                            <button type="button" class="btn btn-default" id="btnPesquisarCentroCusto">
                                                <span class="glyphicon glyphicon-search"></span>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="input-group input-group-sm">
                                        <span class="input-group-addon">Centro de custo</span>
                                        <input type="text" id="txtNomeCentroDeCusto" class="form-control input-sm" disabled />
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <button type="button" class="btn btn-success" id="btnAssociarCentroDeCusto">
                                        Adicionar</button>
                                </div>
                            </div>
                            <%-- <div class="form-group pull-right">
                                <div class="col-sm-12">
                                    <button type="button" class="btn btn-success" id="btnAssociarCentroDeCusto">
                                        Adicionar</button>
                                </div>
                            </div>--%>
                            <div class="table-responsive" style="max-height: 120px; overflow: auto;">
                                <table id="tblCentroDeCusto" class="table table-bordered table-condensed">
                                    <thead style="background-color: #dddddd">
                                        <tr>
                                            <th style='width: 100px'>
                                                Código
                                            </th>
                                            <th>
                                                Nome c/c
                                            </th>
                                            <th>
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
                <div class="modal-footer">
                    <button type="button" class="btn btn-success btn-lg btn-block" id="btnAdicionarRepositorio">
                        Salvar</button>
                    <button type="button" class="btn btn-success btn-lg btn-block" id="btnCancelar" data-dismiss="modal">
                        Cancelar</button>
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
    <div class="form-group">
        <label class="col-sm-2 control-label">
            Instituto</label>
        <div class="col-sm-3">
            <select id="ddlInstituto" class="form-control" data-toggle="dropdown" style="max-width: 250px">
            </select>
            <label class="checkbox">
                <asp:CheckBox runat="server" ID="chkFiltraInativo" Text="Listar inativos ?" ClientIDMode="Static" />
            </label>
        </div>
    </div>
    <div id="corpoPagina" style="display: none">
        <div class="form-group">
            <div class="col-sm-12">
                <button type="button" class="btn btn-success" id="btnAbrirModalAdicionarRep">
                    Adicionar</button>
            </div>
        </div>
        <br />
        <div class="table-responsive">
            <table id="tblRepositorios" class="table table-bordered table-condensed">
                <thead style="background-color: #dddddd">
                    <tr>
                        <th>
                            Patrimônio
                        </th>
                        <th>
                            Tipo Reposit.
                        </th>
                        <th>
                            Identificação
                        </th>
                        <th>
                            Tipo Lista
                        </th>
                        <th>
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>
    <br />
    <div class="form-group">
        <div class="col-sm-12">
            <asp:Button ID="btnVoltar" runat="server" Text="Voltar" ClientIDMode="Static" CssClass="btn btn-success"
                Width="100px" OnClick="btnVoltar_Click" data-toggle="tooltip" title="Voltar para o menu">
            </asp:Button>
        </div>
    </div>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            
            $('#chkFiltraInativo').change(function () {
                carregaTableRepositorios();
            });

            $("#ddlInstituto").on("change", function () {
                if ($("#ddlInstituto option:selected").val() == 0) {
                    $("#corpoPagina").fadeOut();
                }
                else
                    carregaTableRepositorios();
                    $("#corpoPagina").fadeIn();
            });

            // Carregando combo Instituto
            $.ajax({
                type: "POST",
                url: "Repositorio.aspx/CarregarComboInstituto",
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

            function carregaTableRepositorios() {

                $.ajax({
                    type: "POST",
                    url: "Repositorio.aspx/ListarRepositorios",
                    data: JSON.stringify({ "seqInstituto": $('#ddlInstituto option:selected').val(), "status": $('#chkFiltraInativo').is(':checked') }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success:
                        function (msg) {

                            if ($("#tblRepositorios > tbody tr").size() > 0) {
                                $("#tblRepositorios > tbody").empty();
                            }

                            $.each(jQuery.parseJSON(msg.d), function () {

                                var linha;
                                var idlinha = this['SeqRepositorio'];
                                var funcaoAtivaInativa;
                                if (this['IdfAtivo'] == 'S') {
                                    funcaoAtivaInativa = 'INATIVAR';
                                    linha = "notdanger";
                                } else {
                                    linha = "danger";
                                    funcaoAtivaInativa = 'ATIVAR';
                                }

                                $("#tblRepositorios > tbody").append(
                                "<tr class='clickLinha " + linha + "' id='" + idlinha + "'>" +
                                    "<td>" + this['BemPatrimonial']['NumeroPatrimonio'] + "/" +this['BemPatrimonial']['DscTipoPatrimonio'] + "</td>" +
                                    "<td>" + this['TipoRepositorioListaControle']['DscTipoRepositorio'] + "</td>" +
                                    "<td>" + this['DscIdentificacao'] + "</td>" +
                                    "<td>" + this['ListaControle']['NomeListaControle'] + "</td>" +
                                    "<td style='width:50px'>" +
                                    "   <button type='button' class='btn btn-default btn-sm' data-toggle='modal' data-target='#modalAcoes'><span class='glyphicon glyphicon-list'></span></button>" +
                                    "</td>" +
                                "</tr>");
                            });
                        }
                });
            }

            $("#btnAbrirModalAdicionarRep").on("click", function () {
                $("#modalAdicionarRepositorio").modal('show');
            });

            $("#modalAdicionarRepositorio").on('show.bs.modal', function () {
                carregarDadosModalAdicionar();

                $("#txtCodigoCentroCusto").popover({ "trigger": "focus" });
                $("#txtNumeroPatrimonio").popover({ "trigger": "focus" });

            });

            $("#modalAdicionarRepositorio").on('hidden.bs.modal', function () {
                $.LimparCamposModal(this);

                $("#divBemEncontrado").addClass("hidden");
                $("#divNaoBemEncontrado").addClass("hidden");
            });

            $("#btnAssociarCentroDeCusto").on('click', function () {
                associarCentroDeCusto();
            });

            $(document).keypress(function (e) {
                if (e.which == 13) {
                    e.preventDefault();
                    if (document.activeElement.id == "txtCodigoCentroCusto") {
                        obterCentroDeCusto();
                    }
                }
            });

            $("#btnPesquisarCentroCusto").on("click", function () {
                obterCentroDeCusto();
            });

            $("#btnPesquisarBemPatrimonio").off().on("click", function () {
                
//                if ($("#ddlTipoPatrimonio option:selected").val() == "0") {

//                    toastr.options = { 'timeOut': 5000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
//                    toastr.warning("Selecione o tipo de patrimônio !");
//                    $("#ddlTipoPatrimonio").focus();

//                    return false;
//                }

//                if ($("#txtNumeroPatrimonio").val() == "0") {
//                    toastr.options = { 'timeOut': 5000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
//                    toastr.warning("Digite o numero do patrimônio !");
//                    $("#txtNumeroPatrimonio").focus();

//                    return false;
//                }
                
                if ($("#ddlTipoPatrimonio option:selected").val() != "0") {
                    obterPatrimonioPorNumeroETipo();
                }

                
            });

            $("#btnAssociarCentroDeCusto").off().on("click", function () {
                associarCentroDeCusto();
            });
            
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

                
                var info = $($(e.relatedTarget).closest("tr").find("td")[0]).text() + " - " + $($(e.relatedTarget).closest("tr").find("td")[2]).text();

                // Recupera informações da linha do grid para mostrar na modal - unidade + item
                $('#modalAcoes h4').html(info);

                // atribui o onclick para o botão inativar/ativar
                $("#btnAtivarInativar").off().on("click", function () {
                    ativarInativarItem(idLinha);
                    //alert(idLinha);
                });

                // Atribui o onclick para o botão alterar dados
                $("#btnAlterar").off().on("click", function () {
                   //Fecha a modal de ações e abre a modal de alteração de dados
                    $('#modalAcoes').modal('hide');
                    $('#modalAdicionarRepositorio').modal('show');
                    carregarDadosModalAdicionar(true, idLinha);

                    // Atribuindo o evento click para o botão salvar
                    $("#btnSalvarAlteracaoDados").off().on("click", function () {
                        alterarDadosItem(idLinha);
                        //alert(idLinha);
                    });
                });
            });

            function preencherObjetoTela() {
                
                var hdnNumBem = $("#hdnNumBem");

//                if ($(hdnNumBem).val().length == 0) {
//                    toastr.options = { 'timeOut': 5000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
//                    toastr.warning("Nenhum patrimônio foi encontrado, por favor digite o número e tipo de patrimônio corretamente !");
//                    return null;
//                }
                
                if ($("#ddlTipoRepositorio option:selected").val() == 0) {
                    toastr.options = { 'timeOut': 5000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                    toastr.warning("Selecione o tipo do repositório!");
                    
                    $("#ddlTipoRepositorio").focus();
                    return null;
                }
                
                if ( $('#txtIdentificacao').val() == "") {
                    ExibirMensagemAlerta("Digite uma identificação para o repositório !");

                    $("#txtIdentificacao").focus();
                    return null;
                }
     
                if ($("#ddlTipoLista option:selected").val() == 0) {
                    toastr.options = { 'timeOut': 5000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                    toastr.warning("Selecione o tipo de lista para o repositório!");
                    
                    $("#ddlTipoLista").focus();

                    return null;
                }

                // Motando a entidade RepositorioListaControle
                var repositorioCC = {};
                var centroCustos;

                repositorioCC.ListaControle = {};
                repositorioCC.TipoRepositorioListaControle = {};
                repositorioCC.BemPatrimonial = {};
                repositorioCC.listRepositorioCentroCusto = [];

                repositorioCC.DscIdentificacao = $("#txtIdentificacao").val().toUpperCase();
                repositorioCC.ListaControle.SeqListaControle = $("#ddlTipoLista option:selected").val();
                repositorioCC.TipoRepositorioListaControle.SeqTipoRepositorioLstControl = $("#ddlTipoRepositorio option:selected").val();
                //repositorioCC.BemPatrimonial.NumBem = $("#hdnNumBem").val();

                $.each($("#tblCentroDeCusto tbody > tr"), function () {
                    centroCustos = { };

                    centroCustos.codigoCentroCusto = $('td:first', this).text(); //$(this).find("td:first").text();

                    repositorioCC.listRepositorioCentroCusto.push(centroCustos);
                });

                return repositorioCC;
            }

            function adicionarRepositorio(btn) {
                /*var hdnNumBem = $("#hdnNumBem");
                var seqLista = $("#ddlTipoLista option:selected").val();

                if ($(hdnNumBem).val().length == 0) {
                    toastr.options = { 'timeOut': 5000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                    toastr.warning("Nenhum patrimônio foi encontrado, por favor digite o número e tipo de patrimônio corretamente !");
                }

                // Motando a entidade RepositorioListaControle
                var repositorioCC = {};
                var centroCustos;

                repositorioCC.ListaControle = {};
                repositorioCC.TipoRepositorioListaControle = {};
                repositorioCC.BemPatrimonial = {};
                repositorioCC.listRepositorioCentroCusto = [];

                repositorioCC.DscIdentificacao = $("#txtIdentificacao").val();
                repositorioCC.ListaControle.SeqListaControle = $("#ddlTipoLista option:selected").val();
                repositorioCC.TipoRepositorioListaControle.SeqTipoRepositorioLstControl = $("#ddlTipoRepositorio option:selected").val();
                repositorioCC.BemPatrimonial.NumBem = $("#hdnNumBem").val();

                $.each($("#tblCentroDeCusto tbody > tr"), function () {
                    centroCustos = { };

                    centroCustos.codigoCentroCusto = $('td:first', this).text(); //$(this).find("td:first").text();

                    repositorioCC.listRepositorioCentroCusto.push(centroCustos);
                });*/

                var repositorioCC = preencherObjetoTela();

                if(repositorioCC == null)
                    return;

                $.ajax({
                    type: "POST",
                    url: "Repositorio.aspx/AdicionarRepositorio",
                    data: JSON.stringify({ "item": repositorioCC }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {
                        $(btn).button('Processando');
                    },
                    success:
                        function (msg) {
                            toastr.options = { 'timeOut': 5000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                            toastr.success("Repositório inserido com sucesso !");
                            
                            $('#modalAdicionarRepositorio').modal('hide');
                            
                            carregaTableRepositorios();
                        },
                    
                }).always(function () {
                    $(btn).button('reset');
                });

            }
            
            function carregarDadosModalAdicionar(telaAlteracao,idLinha) {
                
                carregarTipoPatrimonio();
                
                carregarTipoRepositorio();
                
                carregaComboTipoLista();
                
                if (telaAlteracao) {
                    obterRepositorio(idLinha);

                    $("#btnPesquisarBemPatrimonio").click();

                    $("#hdnSeqRepositorio").val(idLinha);
                    
                    $("#btnAdicionarRepositorio").off().on('click', function () {
                        alterarDadosItem(this);
                    });
                } else {
                    $("#btnAdicionarRepositorio").off().on('click', function () {
                        adicionarRepositorio(this);
                    });
                }
            }

            function obterCentroDeCusto() {
                var txt = $("#txtCodigoCentroCusto");

                //if (txt.val().length >= 8) {
                // Obter Material
                $.ajax({
                    type: "POST",
                    url: "Repositorio.aspx/ObterCentroDeCusto",
                    data: JSON.stringify({ codigoCC: txt.val().toUpperCase(), codigoInstituto: $("#ddlInstituto option:selected").val() }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function (e) {
                        $("#txtCodigoCentroCusto").val('');
                        $("#txtNomeCentroDeCusto").val('');
                    },
                    success:
                        function (msg) {
                            if (jQuery.parseJSON(msg.d) == null) {
                                toastr.options = { 'timeOut': 5000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                                toastr.warning("Nenhum centro de custo foi encontrado !");
                            }
                            else {
                                //$.each(jQuery.parseJSON(msg.d), function () {
                                var result = jQuery.parseJSON(msg.d);
                                $("#txtCodigoCentroCusto").val(result.Codigo);
                                $("#txtNomeCentroDeCusto").val(result.Nome);
                                //});
                            }
                        }
                });
            }

            function associarCentroDeCusto() {

                if ($('#txtCodigoCentroCusto').val().length == 0 || $('#txtNomeCentroDeCusto').val().length == 0) {
                    toastr.options = { 'timeOut': 5000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                    toastr.warning("Pesquise um centro de custo !");
                    return false;
                }

                if (!verificaCentroCustoJaAdicionado()) {
                    return false;
                }

                montarGridCentroCusto('[{ "codigoCentroCusto": "' + $('#txtCodigoCentroCusto').val() + '", "nomeCentroCusto": "' + $('#txtNomeCentroDeCusto').val() + '"}]');

                /*$('#tblCentroDeCusto > tbody').append(
                    '<tr><td>' + $('#txtCodigoCentroCusto').val() + '</td>' +
                        '<td>' + $('#txtNomeCentroDeCusto').val() + '</td>' +
                        "<td style='width:50px' >" +
                        "<button type='button' class='btn btn-default btn-sm excluirCC'><span class='glyphicon glyphicon-remove'></span></button>" + '</td>' +
                    '</tr>');

                //Limpa os campos
                $('#txtCodigoCentroCusto').val('');
                $('#txtNomeCentroDeCusto').val('');

                $('.excluirCC').off().on("click", function (e) {
                    excluirCentroDeCusto(this);
                });*/
                
                //Limpa os campos
                $('#txtCodigoCentroCusto').val('');
                $('#txtNomeCentroDeCusto').val('');
            }
            
            function montarGridCentroCusto(infos) {

                $.each(jQuery.parseJSON(infos), function() {

                    var codigoCc = this['codigoCentroCusto'];
                    var nomeCc = this['nomeCentroCusto'];

                    $('#tblCentroDeCusto > tbody').append(
                        '<tr><td>' + codigoCc + '</td>' +
                            '<td>' + nomeCc + '</td>' +
                            "<td style='width:50px' >" +
                            "<button type='button' class='btn btn-default btn-sm excluirCC'><span class='glyphicon glyphicon-remove'></span></button>" + '</td>' +
                            '</tr>');
                });

                $('.excluirCC').off().on("click", function (e) {
                    excluirCentroDeCusto(this);
                });
            }

            function verificaCentroCustoJaAdicionado() {
                if ($('#tblCentroDeCusto tr > td:first').text().toUpperCase() == $('#txtCodigoCentroCusto').val().toUpperCase()) {
                    toastr.options = { 'timeOut': 5000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                    toastr.warning("Centro de custo já esta adicionado !");
                    return false;
                }
                return true;
            }

            function excluirCentroDeCusto(btn) {
                var row = btn.parentNode.parentNode;
                row.parentNode.removeChild(row);
            }

            function carregarTipoPatrimonio() {
                // Carregando combo Instituto
                $.ajax({
                    type: "POST",
                    url: "Repositorio.aspx/ListarTipoPatrimonio",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    beforeSend: function (e) {
                        $("#ddlTipoPatrimonio").empty().append($("<option></option>").val("0").html("Carregando..."));
                    },
                    success:
                    function (msg) {
                        $("#ddlTipoPatrimonio").empty().append($("<option></option>").val("0").html("Selecione..."));
                        $.each(jQuery.parseJSON(msg.d), function () {
                            $("#ddlTipoPatrimonio").append($("<option></option>").val(this['CodigoTipoPatrimonio']).html(this['Descricao']));
                        });
                    }
                });
            }

            function carregarTipoRepositorio() {
                // Carregando combo Instituto
                $.ajax({
                    type: "POST",
                    url: "Repositorio.aspx/ListarTipoRepositorio",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    beforeSend: function (e) {
                        $("#ddlTipoRepositorio").empty().append($("<option></option>").val("0").html("Carregando..."));
                    },
                    success:
                    function (msg) {
                        $("#ddlTipoRepositorio").empty().append($("<option></option>").val("0").html("Selecione..."));
                        $.each(jQuery.parseJSON(msg.d), function () {
                            $("#ddlTipoRepositorio").append($("<option></option>").val(this['SeqTipoRepositorioLstControl']).html(this['DscTipoRepositorio']));
                        });
                    }
                });
            }

            function carregaComboTipoLista(val) {
                // Carregando combo Instituto
                $.ajax({
                    type: "POST",
                    url: "Repositorio.aspx/CarregarComboTipoLista",
                    data: JSON.stringify({ instituto: $('#ddlInstituto option:selected').val() }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: false,
                    beforeSend: function () {
                        $("#ddlTipoLista").empty().append($("<option></option>").val("0").html("Carregando..."));
                    },
                    success:
                        function (msg) {
                            $("#ddlTipoLista").empty().append($("<option></option>").val("0").html("Selecione..."));
                            $.each(jQuery.parseJSON(msg.d), function () {
                                $("#ddlTipoLista").append($("<option></option>").val(this['SeqListaControle']).html(this['NomeListaControle']));
                            });


                        }
                });
            }

            function obterPatrimonioPorNumeroETipo() {
                $.ajax({
                    type: "POST",
                    async: false,
                    url: "Repositorio.aspx/ObterPatrimonioPorNumeroETipo",
                    data: JSON.stringify({ numPatrimonio: $('#txtNumeroPatrimonio').val(), tipoPatrimonio: $('#ddlTipoPatrimonio option:selected').val() }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {

                    },
                    success:
                        function (msg) {
                            if (jQuery.parseJSON(msg.d) == null) {
                                toastr.options = { 'timeOut': 5000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                                toastr.warning("Nenhum patrimônio foi encontrado, por favor digite o número e tipo de patrimônio corretamente !");

                                //$("#divNaoBemEncontrado").toggleClass("hidden");
                                $("#divBemEncontrado").addClass("hidden").removeClass("show");
                                $("#divNaoBemEncontrado").addClass("show").removeClass("hidden");
                            }
                            else {
                                //$.each(jQuery.parseJSON(msg.d), function () {
                                var result = jQuery.parseJSON(msg.d);
                                $("#hdnNumBem").val(result.NumBem);

                                $("#divBemEncontrado").addClass("show").removeClass("hidden");
                                $("#divNaoBemEncontrado").addClass("hidden").removeClass("show");
                                //});
                            }
                        }
                });
            }
            
            function ativarInativarItem(_seqRepositorio) {

                var acao;
                if ($("#btnAtivarInativar").text().toUpperCase() == 'ATIVAR') {
                    acao = true;

                } else {
                    acao = false;
                }

                //bool ehPraAtivar,long seqItemListaControle
                $.ajax({
                    type: "POST",
                    url: "Repositorio.aspx/AtivarInativarItem",
                    data: JSON.stringify({ "ehPraAtivar": acao, "seqRepositorio": _seqRepositorio }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {

                    },
                    success:
                        function (msg) {

                            $("#" + _seqRepositorio).toggleClass('danger').toggleClass('notdanger');

                            toastr.options = { 'timeOut': 2000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                            toastr.success("Item alterado com sucesso !");
                        }
                });

                // Esconde o modal de opções
                $('#modalAcoes').modal('hide');
            }
            
            function alterarDadosItem(idLinha) {

//                if ($("#hdnNumBem").val() == "") {
//                    ExibirMensagemAlerta("Necessário um patrimônio para o repostório.");

//                    $("#ddlTipoPatrimonio").focus();
//                    return;
//                }
                
                if ( $('#ddlTipoRepositorio option:selected').val() == "0") {
                    ExibirMensagemAlerta("Selecione o tipo repositório !");

                    $("#ddlTipoRepositorio").focus();
                    return;
                }
                
                if ( $('#txtIdentificacao').val() == "") {
                    ExibirMensagemAlerta("Digite uma identificação !");

                    $("#txtIdentificacao").focus();
                    return;
                }

                if ( $('#ddlTipoLista option:selected').val() == "0") {
                    ExibirMensagemAlerta("Selecione uma lista para o repositório !");

                    $("#ddlTipoLista").focus();

                    return;
                }
                
                var repositorioCC = preencherObjetoTela();

                if(repositorioCC == null)
                    return;

                repositorioCC.SeqRepositorio = $("#hdnSeqRepositorio").val();

                $.ajax({
                    type: "POST",
                    url: "Repositorio.aspx/AlterarDadosItem",
                    data: JSON.stringify({ "repositorio": repositorioCC }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {

                    },
                    success:
                        function (msg) {

                            toastr.options = { 'timeOut': 2000, 'positionClass': 'toast-top-full-width', 'fadeIn': 200, 'fadeOut': 600 };
                            toastr.success("Item alterado com sucesso !");

                            carregaTableRepositorios();
                        }
                });

                // Esconde o modal de opções
                $('#modalAdicionarRepositorio').modal('hide');

            }
            
            function obterRepositorio(seqRepositorio) {
                $.ajax({
                    type: "POST",
                    url: "Repositorio.aspx/ObterRepositorio",
                    data: JSON.stringify({ "seqRepositorio": seqRepositorio }),
                    contentType: "application/json; charset=utf-8",
                    async: false,
                    dataType: "json",
                    beforeSend: function () {

                    },
                    success:
                        function (msg) {

                            var repositorio = jQuery.parseJSON(msg.d);

                            $("#txtIdentificacao").val(repositorio.DscIdentificacao);
                            $("#txtNumeroPatrimonio").val(repositorio.BemPatrimonial.NumeroPatrimonio);
                            $("#hdnNumBem").val(repositorio.NumBem);
                            $("#ddlTipoLista").val(repositorio.ListaControle.SeqListaControle);
                            $("#ddlTipoRepositorio").val(repositorio.TipoRepositorioListaControle.SeqTipoRepositorioLstControl);
                            $("#ddlTipoPatrimonio").val(repositorio.BemPatrimonial.CodTipoPatrimonio);

                            carregarCentroDeCustos(seqRepositorio);
                        }
                });
            }
            
            function carregarCentroDeCustos(seqRepositorio) {
                $.ajax({
                    type: "POST",
                    url: "Repositorio.aspx/ObterCentrosCustoDoRepositorio",
                    data: JSON.stringify({ "seqRepositorio": seqRepositorio }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {

                    },
                    success:
                        function (msg) {
                        montarGridCentroCusto(msg.d);
                        }
                });
            }
        });
    </script>
</asp:Content>
