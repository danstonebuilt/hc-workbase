using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text;
using System.Data;
using System.Configuration;

namespace Hcrp.Framework.UI
{
    /// <summary>
    /// Classe base utilizada para implementação de métodos e propriedades comuns à todas as outras páginas
    /// </summary>
    public class BaseMasterPage : System.Web.UI.MasterPage
    {
        #region Fields
        
        public enum TipoMensagem { Alerta, Sucesso, Erro, Informacao };

        # endregion 

        #region Propriedade

        /// <summary>
        /// Usuário logado na aplicação
        /// </summary>
        protected Hcrp.Framework.Classes.Usuario LoggedUser
        {
            get { return (Hcrp.Framework.Classes.Usuario)this.Session["LoggedUser"] ?? new Hcrp.Framework.Classes.Usuario(); }
            set { this.Session["LoggedUser"] = value; }
        }

        /// <summary>
        /// Guarda o número do usuário logado.
        /// </summary>
        public long NumeroUsuarioBancoLogado
        {
            get
            {
                if (this.Session["NumeroUsuarioBancoLogadoG"] == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(this.Session["NumeroUsuarioBancoLogadoG"]);
                }
            }
            set
            {
                this.Session["NumeroUsuarioBancoLogadoG"] = value;
            }
        }

        /// <summary>
        /// Guarda as roles do usuário logado.
        /// </summary>
        public List<string> _listRoleUsuarioLogado
        {
            get
            {
                if (this.Session["_listRoleUsuarioLogadoG"] == null)
                {
                    return null;
                }
                else
                {
                    return (List<string>)this.Session["_listRoleUsuarioLogadoG"];
                }
            }
            set
            {
                this.Session["_listRoleUsuarioLogadoG"] = value;
            }
        }

        #endregion Propriedades

        #region Eventos

        protected void Page_Init(object sender, EventArgs e)
        {
            this.RegistrarCss();

            this.OcultarMensagem();

            if (this.Session["MensagemOutraPagina"] != null)
            {
                System.Collections.Generic.Dictionary<TipoMensagem, string> msg = this.Session["MensagemOutraPagina"] as System.Collections.Generic.Dictionary<TipoMensagem, string>;
                this.ExibirMensagem((TipoMensagem)msg.Keys.FirstOrDefault(), msg.Values.FirstOrDefault().ToString());
                Session.Remove("MensagemOutraPagina");
            }

            if (HttpContext.Current.Session["conn"] == null)
            {
                // Se a session que armazena a conectionstring estiver limpa, redirecionamenot
                String url_solicitada = this.Page.Request.FilePath;
                url_solicitada = url_solicitada.Remove(0, url_solicitada.LastIndexOf("/") + 1);

                if (url_solicitada.ToLower() != "login.aspx" &&
                    url_solicitada.ToLower() != "default.aspx" &&
                    url_solicitada.ToLower() != "menu.aspx" &&
                    url_solicitada.ToLower() != "eventoinesperado.aspx" &&
                    url_solicitada.ToLower() != "acessonegado.aspx")
                {
                    Response.Redirect("~/Login.aspx", false);
                    return;
                }


            }

            //if (!Page.IsPostBack)
            //{
            //    // Este não terá validação de acesso a páginas.
            //    this.ValidaAcesso();
            //}
            //else
            //{
            //    this.ValidarSeASessaoDoUsuarioEstaValida();
            //}
        }

        #endregion Eventos

        #region Métodos

        /// <summary>
        /// Retorna o nome do sistema para masterpage [FB]
        /// </summary>
        /// <returns></returns>
        public string getNomeSistema()
        {
            string result = string.Empty;

            result = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["NomeSistema"]);

            if (string.IsNullOrEmpty(result))
            {
                result = "NomeSistema deve ser definido no AppSettings";
            }

            return result;

        }

        private void RegistrarCss()
        {
            

            HtmlHead head = (HtmlHead)Page.Header;
            //            HtmlLink link;

            LiteralControl ltrStyle = new LiteralControl();
            ltrStyle.Text = string.Empty;

            ltrStyle.Text += "<link href='" + this.ResolveClientUrl("~/") + "InterfaceHC/css/jquery-ui-1.8.22.custom.css' type='text/css' rel='stylesheet' />";
            ltrStyle.Text += "<link href='" + this.ResolveClientUrl("~/") + "InterfaceHC/css/Global_estilo.css' type='text/css' rel='stylesheet' />";
            ltrStyle.Text += "<link href='" + this.ResolveClientUrl("~/") + "InterfaceHC/css/Global_fontes.css' type='text/css' rel='stylesheet' />";
            ltrStyle.Text += "<link href='" + this.ResolveClientUrl("~/") + "InterfaceHC/css/Tabela_principal.css' type='text/css' rel='stylesheet' />";
            ltrStyle.Text += "<link href='" + this.ResolveClientUrl("~/") + "InterfaceHC/css/jquery.fancybox-1.3.4.css' type='text/css' rel='stylesheet' />";
            ltrStyle.Text += "<link href='" + this.ResolveClientUrl("~/") + "InterfaceHC/css/PadraoHC.css' type='text/css' rel='stylesheet' />";
            ltrStyle.Text += "<link href='" + this.ResolveClientUrl("~/") + "InterfaceHC/css/Menu.css' type='text/css' rel='stylesheet' />";



            ltrStyle.Text += "<style type='text/css'>";

            ltrStyle.Text += "    .notification { ";
            ltrStyle.Text += "       margin-bottom: 10px; position: fixed; top: 0; left: 0; z-index: 999; text-align:center; font-weight:bold; width:99.9%; ";
            ltrStyle.Text += "    } ";
            ltrStyle.Text += "    .notification .messages { ";
            ltrStyle.Text += "      margin: 1px; padding: 13px 0px 13px 50px; color: rgb(43, 43, 43); font-size: 14px; text-align:center;  ";            
            ltrStyle.Text += "    } ";
            ltrStyle.Text += "    .erro { ";
            ltrStyle.Text += "      border: 1px solid rgb(197, 143, 117); color:Red;";
            ltrStyle.Text += "    } ";
            ltrStyle.Text += "    .erro .messages { ";
            ltrStyle.Text += "       background: url('" + this.ResolveClientUrl("~/") + "InterfaceHC/imagens/error.png') no-repeat 10px 3px rgb(242, 187, 160); background-position-y: center;";
            ltrStyle.Text += "    } ";
            ltrStyle.Text += "    .sucesso { ";
            ltrStyle.Text += "       border: 1px solid rgb(154, 201, 51); ";
            ltrStyle.Text += "    } ";
            ltrStyle.Text += "    .sucesso .messages { ";
            ltrStyle.Text += "       background: url('" + this.ResolveClientUrl("~/") + "InterfaceHC/imagens/success.png') no-repeat 10px 3px rgb(206, 231, 144); background-position-y: center; ";
            ltrStyle.Text += "    } ";
            ltrStyle.Text += "    .alerta { ";
            ltrStyle.Text += "       border: 1px solid rgb(230, 210, 96); ";
            ltrStyle.Text += "    } ";
            ltrStyle.Text += "    .alerta .messages { ";
            ltrStyle.Text += "       background: url('" + this.ResolveClientUrl("~/") + "InterfaceHC/imagens/warning.png') no-repeat 10px 3px rgb(249, 237, 170); background-position-y: center;";
            ltrStyle.Text += "    } ";
            ltrStyle.Text += "    .informacao { ";
            ltrStyle.Text += "       border: 1px solid rgb(172, 219, 239); ";
            ltrStyle.Text += "    } ";
            ltrStyle.Text += "    .informacao .messages { ";
            ltrStyle.Text += "       background: url('" + this.ResolveClientUrl("~/") + "InterfaceHC/imagens/info.png') no-repeat 10px 3px rgb(218, 239, 251); background-position-y: center;";
            ltrStyle.Text += "    }    ";


            ltrStyle.Text += "    #topo{margin:auto; width:100%; margin-bottom:10px; background:url('" + this.ResolveClientUrl("~/") + "InterfaceHC/Imagens/cabecalho.jpg') repeat-x top;} ";
            ltrStyle.Text += "    .fundo-faixa{ margin:auto; width:730px; height:77px;} ";
            ltrStyle.Text += "    /* Paginador */";
            ltrStyle.Text += "    .PagerCurrentPageCell, .PagerOtherPageCells{ background-color:#f7f7f7;}";
            ltrStyle.Text += "    .PagerCurrentPageCell{padding:5px; background-color:#f4f4f4}";
            ltrStyle.Text += "    .PagerOtherPageCells a{padding:5px;}	";
            ltrStyle.Text += "    .PagerOtherPageCells a:hover{padding:5px; background-color:#333; color:#FFF; text-decoration:none}    ";
            ltrStyle.Text += "    .Grid_linha:hover{background-color: #ffe7a2; border-bottom: 1px solid #e8ecf0; color: #000000; cursor: hand; font-size: 8pt; padding: 1 3 1 5; }";
            ltrStyle.Text += "    .legendaForm { background-color: #EDEFF1; color: #349802; font-weight: bold; padding-top:10px;  }";
            
            ltrStyle.Text += "    .warning {";
            ltrStyle.Text += "        color: #9F6000;";
            ltrStyle.Text += "        background-color: #FEEFB3;";
            ltrStyle.Text += "        background-image: url('" + this.ResolveClientUrl("~/") + "InterfaceHC/Imagens/warning.png');";
            ltrStyle.Text += "        background-repeat:no-repeat;";
            ltrStyle.Text += "        background-position:center;";
            ltrStyle.Text += "        border: 1px solid #9F6000;";
            ltrStyle.Text += "        width: 700px;";
            ltrStyle.Text += "        text-align:left;";
            ltrStyle.Text += "        border: 1px solid;";
            ltrStyle.Text += "        margin: 0px 0px;";
            ltrStyle.Text += "        padding:10px 5px 10px 50px;";
            ltrStyle.Text += "        background-repeat: no-repeat;";
            ltrStyle.Text += "        background-position: 10px center;    ";
            ltrStyle.Text += "    }    ";
            
            ltrStyle.Text += "</style> ";
            head.Controls.Add(ltrStyle);

         
        }

        /// <summary>
        /// Exibir mensagem.
        /// </summary>
        /// <param name="tipoMensagem"></param>
        /// <param name="mensagem"></param>
        public void ExibirMensagem(TipoMensagem tipoMensagem, string mensagem, string pPnlMensagemName = "", Panel pnlMensagem = null)
        {
            if (string.IsNullOrEmpty(pPnlMensagemName))
            {
                pPnlMensagemName = "pnlMensagem";
            }

            if (pnlMensagem == null)
            {

                if (FindControl(pPnlMensagemName) is Panel)
                {
                    pnlMensagem = (FindControl(pPnlMensagemName) as Panel);
                }
                else if ((Master != null) && (Master.FindControl(pPnlMensagemName) is Panel))
                {
                    pnlMensagem = (Master.FindControl(pPnlMensagemName) as Panel);
                }
                else if ((Master != null) && (Master.Master != null) && (Master.Master.FindControl(pPnlMensagemName) is Panel))
                {
                    pnlMensagem = (Master.Master.FindControl(pPnlMensagemName) as Panel);
                }
            }

            if (pnlMensagem != null )
            {

                if (pnlMensagem == null)
                    pnlMensagem = FindControl(pPnlMensagemName) as Panel;

                Label lblMensagem = (FindControl("lblMensagem") as Label);

                if (lblMensagem == null)
                    lblMensagem = FindControl("lblMensagem") as Label;

                UpdatePanel updMensagem = FindControl("updMensagem") as UpdatePanel;

                if (updMensagem == null)
                    updMensagem = FindControl("updMensagem") as UpdatePanel;

                switch (tipoMensagem)
                {
                    case TipoMensagem.Alerta:
                        pnlMensagem.CssClass = "notification alerta";
                        break;
                    case TipoMensagem.Erro:
                        pnlMensagem.CssClass = "notification erro";
                        break;
                    case TipoMensagem.Informacao:
                        pnlMensagem.CssClass = "notification informacao";
                        break;
                    case TipoMensagem.Sucesso:
                        pnlMensagem.CssClass = "notification sucesso";
                        break;
                }

                lblMensagem.Text = mensagem;

                pnlMensagem.Visible = true;

                string x = this.ID;

                //ScriptManager.RegisterStartupScript(this, typeof(string), "msg", "$(\"#" + pnlMensagem.ClientID + "\").slideDown(500);", true);
                ScriptManager.RegisterStartupScript(pnlMensagem.Parent.Page, typeof(string), "msg", "$(\"#" + pnlMensagem.ClientID + "\").slideDown(500); $(\"#" + pnlMensagem.ClientID + "\").click(function(){$(this).slideUp(500);});", true);
                 

                updMensagem.Update();
            }
        }

        public void ExibirMensagem(TipoMensagem tipoMensagem, string mensagem, int timeout)
        {
            this.ExibirMensagem(tipoMensagem, mensagem);
            ScriptManager.RegisterStartupScript(this, typeof(string), "msgTimeout", "setTimeout('$(\"#" + (FindControl("pnlMensagem") as Panel).ClientID + "\").slideUp(500);','" + timeout + "');", true);
        }

        /// <summary>
        /// Exibir mensagem.
        /// </summary>
        /// <param name="tipoMensagem"></param>
        /// <param name="mensagem"></param>
        public void ExibirMensagemEmOutraPagina(TipoMensagem tipoMensagem, string mensagem)
        {
            System.Collections.Generic.Dictionary<TipoMensagem, string> msg = new Dictionary<TipoMensagem, string>();
            msg.Add(tipoMensagem, mensagem);
            this.Session["MensagemOutraPagina"] = msg;
        }

        /// <summary>
        /// Ocultar mensagem.
        /// </summary>
        public void OcultarMensagem()
        {
            if (this.FindControl("pnlMensagem") != null)
            {
                var pnlMensagem = FindControl("pnlMensagem") as Panel;
                var updMensagem = FindControl("updMensagem") as UpdatePanel;

                pnlMensagem.Visible = false;
                updMensagem.Update();
            }
        }

        public void AbrirModal(string url, int width, int height)
        {
            StringBuilder str = new StringBuilder();

            str.Append(" $(document).ready(function () { ");
            str.Append("    $.fancybox({ ");
            str.Append("        'width': " + width + ", ");
            str.Append("        'height': " + height + ", ");
            str.Append("        'autoScale': false, ");
            str.Append("        'transitionIn': 'elastic', ");
            str.Append("        'transitionOut': 'none', ");
            str.Append("        'type': 'iframe', ");
            str.Append("        'href': '" + url + "', ");
            str.Append("        'overlayColor' : '#F1F1F1', ");
            str.Append("        'modal' : false, ");
            str.Append("        'centerOnScroll' : true, ");
            str.Append("        'hideOnOverlayClick' : false, 'padding' : 0 ");
            str.Append("    }); ");
            str.Append(" });  ");

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "janela", str.ToString(), true);
        }

        /// <summary>
        /// Método que chamada jquery - fancybox
        /// </summary>
        /// <param name="url">Pagina que será aberta dentro da modal</param>
        /// <param name="width">Largura da modal </param>
        /// <param name="height">Tamanho da modal</param>
        /// <param name="acao">Método javascript a ser executado assim que a modal é fechada(onClosed)..Exemplo: Post()</param>
        public void AbrirModal(string url, int width, int height, string acao = "", bool modal = false)
        {
            StringBuilder str = new StringBuilder();

            str.Append(" $(document).ready(function () { ");
            str.Append("    $.fancybox({ ");
            str.Append("        'width': " + width + ", ");
            str.Append("        'height': " + height + ", ");
            str.Append("        'autoScale': false, ");
            str.Append("        'transitionIn': 'elastic', ");
            str.Append("        'transitionOut': 'none', ");
            str.Append("        'type': 'iframe', ");
            str.Append("        'href': '" + url + "', ");
            str.Append("        'overlayColor' : '#F1F1F1', ");
            str.Append("        'modal' : false, ");
            str.Append("        'centerOnScroll' : true, ");
            str.Append("        'enableEscapeButton' : false,");

            if (!string.IsNullOrWhiteSpace(acao))
                str.Append("        'onClosed'        :     function() { " + acao + "; },  ");

            str.Append("        'hideOnOverlayClick' : false, 'padding' : 0 ");
            str.Append("    }); ");
            str.Append(" });  ");



            ScriptManager.RegisterStartupScript(Page, typeof(Page), "janela", str.ToString(), true);
        }

        public void DesabilitarControlesDaPagina(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is DropDownList)
                    ((DropDownList)(c)).Enabled = false;
                else if (c is GridView)
                    ((GridView)(c)).Enabled = false;
                else if (c is TextBox)
                    ((TextBox)(c)).Enabled = false;
                else if (c is Panel)
                    ((Panel)(c)).Enabled = false;
                else if (c is CheckBox)
                    ((CheckBox)(c)).Enabled = false;
                else if (c is CheckBoxList)
                    ((CheckBoxList)(c)).Enabled = false;
                else if (c is RadioButtonList)
                    ((RadioButtonList)(c)).Enabled = false;
                else if (c is Button)
                    ((Button)(c)).Enabled = false;
                else if (c is ImageButton)
                    ((ImageButton)(c)).Enabled = false;
                else if (c is GridView)
                    ((GridView)(c)).Enabled = false;
                else if (c is LinkButton)
                    ((LinkButton)(c)).Enabled = false;
                else if (c is FileUpload)
                    ((FileUpload)(c)).Enabled = false;

                DesabilitarControlesDaPagina(c);
            }
        }

        protected void ValidaAcesso()
        {

            // 0 = AcessoPermitido
            // 1 = AcessoNegado ou Erro ao validar acesso
            // 2 = Sessão Expirada
            Int32 ResultadoAcesso = 0;
            String url_solicitada = this.Page.Request.FilePath;
            url_solicitada = url_solicitada.Remove(0, url_solicitada.LastIndexOf("/") + 1);

            try
            {

                if (this.NumeroUsuarioBancoLogado > 0)
                {

                    if (url_solicitada.ToLower() != "login.aspx" &&
                        url_solicitada.ToLower() != "default.aspx" &&
                        url_solicitada.ToLower() != "menu.aspx" &&
                        url_solicitada.ToLower() != "eventoinesperado.aspx" &&
                        url_solicitada.ToLower() != "acessonegado.aspx")
                    {

                        //List<Framework.Seguranca.Entidade.VisaoEntitie> listVisaoUsuario = Session["listVisaoUsuario"] as List<Framework.Seguranca.Entidade.VisaoEntitie>;
                        if (this._listRoleUsuarioLogado != null && this._listRoleUsuarioLogado.Count > 0)
                        {

                            if (Hcrp.Framework.Infra.Util.Parametrizacao.CodSistema <= 0)
                            {
                                ResultadoAcesso = 1;
                            }
                            else if (this.PaginaInformadaPodeAcessar(this._listRoleUsuarioLogado, url_solicitada, Hcrp.Framework.Infra.Util.Parametrizacao.CodSistema))
                            {
                                ResultadoAcesso = 0;
                            }
                            else
                            {
                                ResultadoAcesso = 1;
                            }

                        }
                        else
                        {
                            ResultadoAcesso = 2;
                        }

                    }
                }
                else
                {
                    ResultadoAcesso = 2;
                }

            }
            catch
            {
                ResultadoAcesso = 1;
            }

            if (ResultadoAcesso == 1)
            {
                if (this.NumeroUsuarioBancoLogado > 0 && url_solicitada.ToLower() != "acessonegado.aspx" && url_solicitada.ToLower() != "eventoinesperado.aspx")
                {
                    Response.Redirect("~/AcessoNegado.aspx", false);
                }
            }
            else if (ResultadoAcesso == 2)
            {
                if (url_solicitada.ToLower() != "login.aspx" && url_solicitada.ToLower() != "acessonegado.aspx" && url_solicitada.ToLower() != "eventoinesperado.aspx")
                {
                    // se o número de usuário de banco está zerado e o usuário está vindo da tela de acesso negado, então quer dizer que o mesmo já foi autenticado
                    // mas não possui permissão para a aplicação, caso contrário envia-o para login, para autenticá-lo.
                    Response.Redirect("~/login.aspx", false);
                }
            }
        }

        protected bool PaginaInformadaPodeAcessar(List<string> _listRoles, string urlPagina, int idCodSistema)
        {
            bool podeAcessar = false;

            try
            {
                if (!string.IsNullOrEmpty(urlPagina))
                {
                    //List<Hcrp.Acesso.Dominio.Entidade.Perfil> _listPerfil = new Hcrp.Acesso.Infra.AcessoDado.Perfil().ObterListaDePerfilPorSistema(idCodSistema);

                    //if (_listPerfil != null && _listPerfil.Count > 0)
                    //{

                    //    foreach (string itemRoleUsuario in _listRoles)
                    //    {
                    //        if (!string.IsNullOrWhiteSpace(itemRoleUsuario) && !string.IsNullOrWhiteSpace(urlPagina))
                    //        {

                    //            //var conta = (from contaL in _listPerfil
                    //            //             where contaL.GrupoPodeAcessar.ToUpper().Contains(itemRoleUsuario.ToUpper()) &&
                    //            //                   contaL.NomePaginaASPX.ToUpper().Contains(urlPagina.ToUpper())
                    //            //             select contaL).Count();


                    //            Hcrp.Acesso.Dominio.Entidade.Perfil perfilVerifica =_listPerfil.Where( q => q.Nome.ToUpper().Trim() == itemRoleUsuario.ToUpper().Trim() ).FirstOrDefault();

                    //            if (perfilVerifica != null)
                    //            {
                    //                foreach (var ppItem in new Hcrp.Acesso.Infra.AcessoDado.PerfilPrograma().ObterListaDeProgramaDoPerfilComId( perfilVerifica.IdPerfil ))
                    //                {
                    //                    Hcrp.Acesso.Dominio.Entidade.Programa prog = new Hcrp.Acesso.Infra.AcessoDado.Programa().ObterProgramaComOId(Convert.ToInt32(ppItem.IdPrograma));

                    //                    //podeAcessar = (prog.PaginaWeb.ToUpper().Contains(urlPagina.ToUpper()));
                    //                    podeAcessar = true;
                    //                    if (podeAcessar) 
                    //                    {
                    //                        break;
                    //                    }

                    //                }

                    //            }

                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception)
            {
                throw;
            }

            return podeAcessar;
        }

        protected void ValidarSeASessaoDoUsuarioEstaValida()
        {
            if (this.NumeroUsuarioBancoLogado == 0)
            {
                String url_solicitada = this.Page.Request.FilePath;
                url_solicitada = url_solicitada.Remove(0, url_solicitada.LastIndexOf("/") + 1);

                if (url_solicitada.ToLower() != "login.aspx" && url_solicitada.ToLower() != "acessonegado.aspx" && url_solicitada.ToLower() != "eventoinesperado.aspx")
                {
                    // se o número de usuário no banco está zerado e o usuário está vindo da tela de acesso negado, então quer dizer que o mesmo já foi autenticado
                    // mas não possui permissão para a aplicação, caso contrário envia-o para login, para autenticá-lo.
                    Response.Redirect("~/login.aspx", false);
                }
            }

        }

        /// <summary>
        /// Método utilizado para mostrar uma mensagem ao usuário
        /// </summary>
        /// <param name="title">Título da mensagem</param>
        /// <param name="message">Mensagem</param>
        protected void ShowMessage(String title, String message)
        {
            String jsMessage = "alert('{0}');";

            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), title, String.Format(jsMessage, message), true);
        }

        public void ExibirAlerta(string titulo, string msg)
        {
            StringBuilder str = new StringBuilder();

            str.Append(" showNotification({ ");
            str.Append("     type : \"warning\", ");
            str.Append("     message: \"" + msg + "\"");
            str.Append(" }); ");

            //str.Append(" $(document).ready(function () { ");
            //str.Append("   jAlert('" + msg + "', '" + titulo + "');  ");
            //str.Append(" });  ");

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alerta", str.ToString(), true);
        }

        #endregion Métodos
    }
}