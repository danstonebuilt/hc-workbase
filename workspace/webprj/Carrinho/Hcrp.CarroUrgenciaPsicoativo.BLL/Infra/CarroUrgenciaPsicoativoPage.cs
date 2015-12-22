using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao
{
    public class CarroUrgenciaPsicoativoPage : System.Web.UI.Page
    {
        #region Enum
        public enum Toast
        {
            [Description("toast-bottom-right")]
            BottomRight,
            [Description("toast-bottom-left")]
            BottomLeft,
            [Description("toast-top-right")]
            TopRight,
            [Description("toast-top-left")]
            TopLeft,
            [Description("toast-top-full-width")]
            TopFullWidth,
            [Description("toast-bottom-full-width")]
            BottomFullWidth
        }

        public enum TipoMensagem
        {
            [Description("success")]
            Sucesso,
            [Description("info")]
            Info,
            [Description("warning")]
            Alerta,
            [Description("error")]
            Erro
        }
        #endregion


        #region Propriedades

        /// <summary>
        /// Guarda a senha do usuário logado.
        /// </summary>
        public string SenhaUsuarioBancoLogado
        {
            get
            {
                if (this.Session["SenhaUsuarioBancoLogadoG"] == null)
                {
                    return "";
                }
                else
                {
                    return this.Session["SenhaUsuarioBancoLogadoG"].ToString();
                }
            }
            set
            {
                this.Session["SenhaUsuarioBancoLogadoG"] = value;
            }
        }

        /// <summary>
        /// Guarda o login do usuário logado.
        /// </summary>
        public string LoginUsuarioBancoLogado
        {
            get
            {
                if (this.Session["LoginUsuarioBancoLogadoG"] == null)
                {
                    return "";
                }
                else
                {
                    return this.Session["LoginUsuarioBancoLogadoG"].ToString();
                }
            }
            set
            {
                this.Session["LoginUsuarioBancoLogadoG"] = value;
            }
        }
        
        /// <summary>
        /// Guarda o número do usuário logado.
        /// </summary>
        public Int32 NumeroUsuarioBancoLogado
        {
            get
            {
                if (this.Session["NumeroUsuarioBancoLogadoG"] == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(this.Session["NumeroUsuarioBancoLogadoG"]);
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


        // Propriedade que guarda
        public List<Entity.Perfil> TipoUsuarioLogado
        {
            get { return (Session["TipoUsuarioLogado"] as List<Entity.Perfil>); }
            set
            {
                Session["TipoUsuarioLogado"] = value;
            }
        }

        #endregion

        #region variáveis

        //public enum TipoMensagem { Alerta, Sucesso, Erro, Informacao };

        #endregion

        #region eventos

        protected void Page_Init(object sender, EventArgs e)
        {

            this.RegistrarJs();
            
            if (this.Session["MensagemOutraPagina"] != null)
            {
                System.Collections.Generic.Dictionary<TipoMensagem, string> msg = this.Session["MensagemOutraPagina"] as System.Collections.Generic.Dictionary<TipoMensagem, string>;
                this.ExibirMensagem((TipoMensagem)msg.Keys.FirstOrDefault(), msg.Values.FirstOrDefault().ToString());
                Session.Remove("MensagemOutraPagina");
            }

            //if (!Page.IsPostBack)
            //{
            //    this.ValidaAcesso();
            //}
            //else
            //{
            //    this.ValidarSeASessaoDoUsuarioEstaValida();
            //}
        }

        #endregion

        #region métodos

        /// <summary>
        /// Exibir mensagem.
        /// </summary>
        /// <param name="tipoMensagem"></param>
        /// <param name="mensagem"></param>
        //public void ExibirMensagem(TipoMensagem tipoMensagem, string mensagem)
        //{
        //    Panel pnlMensagem = (Master.FindControl("pnlMensagem") as Panel);

        //    if (pnlMensagem == null)
        //        pnlMensagem = Master.Master.FindControl("pnlMensagem") as Panel;

        //    Label lblMensagem = (Master.FindControl("lblMensagem") as Label);

        //    if (lblMensagem == null)
        //        lblMensagem = Master.Master.FindControl("lblMensagem") as Label;

        //    UpdatePanel updMensagem = Master.FindControl("updMensagem") as UpdatePanel;

        //    if (updMensagem == null)
        //        updMensagem = Master.Master.FindControl("updMensagem") as UpdatePanel;

        //    switch (tipoMensagem)
        //    {
        //        case TipoMensagem.Alerta:
        //            pnlMensagem.CssClass = "notification alerta";
        //            break;
        //        case TipoMensagem.Erro:
        //            pnlMensagem.CssClass = "notification erro";
        //            break;
        //        case TipoMensagem.Informacao:
        //            pnlMensagem.CssClass = "notification info";
        //            break;
        //        case TipoMensagem.Sucesso:
        //            pnlMensagem.CssClass = "notification success";
        //            break;
        //    }


        //    lblMensagem.Text = mensagem;

        //    pnlMensagem.Visible = true;

        //    ScriptManager.RegisterStartupScript(this, typeof(string), "msg", "$(\"#" + pnlMensagem.ClientID + "\").slideDown(500); $(\"#" + pnlMensagem.ClientID + "\").click(function(){$(this).slideUp(500);});", true);           
            
        //    updMensagem.Update();
        //}

        /// <summary>
        /// Retorna a Descrição do Enum
        /// </summary>
        /// <param name="_value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum _value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = _value.GetType().GetField(_value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return _value.ToString();
            }
        }

        /// <summary>
        /// Método responsavel por exibir mensagens na tela
        /// </summary>
        /// <param name="timeOut">tempo que a mensagem será exibida na tela, se valor 0, assumirá valores default</param>
        /// <param name="direcao">Direção da mesangem : Esquerda(top ou bottom), Direita(top ou bottom), Encima, Embaixo</param>
        /// <param name="info">Tipo da mensagem : Sucesso,Info, Alerta, Erro</param>
        /// <param name="msg">Mensagem a ser exibida</param>
        public void ExibirMensagem(TipoMensagem tipomensagem = TipoMensagem.Sucesso, string msg = "", int timeOut = 0, Toast direcao = Toast.TopFullWidth)
        {
            //debug	            boleano	false	            Ativa modo depuração do componente
            //positionClass	    string	"toast-top-right"	Insere uma classe CSS para posicionamento da notificação toast-top-right | toast-bottom-right | toast-bottom-left | toast-top-left | toast-top-full-width | toast-bottom-full-width
            //fadeIn	        integer	300	                Tempo de efeito de esmaecer (aparecer)
            //fadeOut	        integer	1000	            Tempo de efeito de esmaecer (sumir)
            //timeOut	        integer	5000	            Tempo total de exibição da notificação
            //extendedTimeOut	integer	0	                Tempo adicional de exibição da notificação

            if (timeOut == 0)
            {
                switch (tipomensagem)
                {
                    case TipoMensagem.Alerta:
                        timeOut = 4000;
                        break;
                    case TipoMensagem.Erro:
                        timeOut = 0;
                        break;
                    case TipoMensagem.Info:
                        timeOut = 4000;
                        break;
                    case TipoMensagem.Sucesso:
                        timeOut = 4000;
                        break;
                }
            }

            StringBuilder str = new StringBuilder();

            str.Append(" $(document).ready(function () {  ");
            str.Append(" toastr.options = { 'timeOut': " + timeOut + ", 'positionClass': '" + GetEnumDescription(direcao) + "', 'fadeIn': 200, 'fadeOut': 600 };");
            str.Append(" toastr." + GetEnumDescription(tipomensagem) + "('" + msg + "');");
            str.Append("});");

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "notificacao", str.ToString(), true);

            UpdatePanel updMensagem = Master.FindControl("updMensagem") as UpdatePanel;

            updMensagem.Update();
        }


        //public void ExibirMensagem(TipoMensagem tipoMensagem, string mensagem)
        //{

            //Panel pnlMensagem = (Master.FindControl("pnlMensagem") as Panel);

            //int tempoMensagem = 0;

            //if (pnlMensagem == null)
            //    pnlMensagem = Master.Master.FindControl("pnlMensagem") as Panel;

            //Label lblMensagem = (Master.FindControl("lblMensagem") as Label);

            //if (lblMensagem == null)
            //    lblMensagem = Master.Master.FindControl("lblMensagem") as Label;

            //UpdatePanel updMensagem = Master.FindControl("updMensagem") as UpdatePanel;

            //if (updMensagem == null)
            //    updMensagem = Master.Master.FindControl("updMensagem") as UpdatePanel;

            //switch (tipoMensagem)
            //{
            //    case TipoMensagem.Alerta:
            //        pnlMensagem.CssClass = "notification alerta";
            //        tempoMensagem = 10000;
            //        break;
            //    case TipoMensagem.Erro:
            //        pnlMensagem.CssClass = "notification erro";
            //        tempoMensagem = 0;
            //        break;
            //    case TipoMensagem.Informacao:
            //        pnlMensagem.CssClass = "notification informacao";
            //        tempoMensagem = 11000;
            //        break;
            //    case TipoMensagem.Sucesso:
            //        pnlMensagem.CssClass = "notification sucesso";
            //        tempoMensagem = 6000;
            //        break;
            //}

            //lblMensagem.Text = mensagem;

            //pnlMensagem.Visible = true;


            ////ScriptManager.RegisterStartupScript(this, typeof(string), "msg", "$(\"#" + pnlMensagem.ClientID + "\").slideDown(500).click(function(){$(this).slideUp(500);});", true);

            //ScriptManager.RegisterStartupScript(this, typeof(string), "msg", "$(\"#" + pnlMensagem.ClientID + "\").slideDown(500);", true);

            //if (tempoMensagem != 0)
            //    ScriptManager.RegisterStartupScript(this, typeof(string), "msgTimeout", "setTimeout('$(\"#" + (Master.FindControl("pnlMensagem") as Panel).ClientID + "\").fadeOut(1500);','" + tempoMensagem + "');", true);

            ////ScriptManager.RegisterStartupScript(pnlMensagem.Parent.Page, typeof(string), "msg", "$(\"#" + pnlMensagem.ClientID + "\").slideDown(500); $(\"#" + pnlMensagem.ClientID + "\").click(function(){$(this).slideUp(500);})", true);

            //updMensagem.Update();
        //}

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
            (Master.FindControl("pnlMensagem") as Panel).Visible = false;
            (Master.FindControl("updMensagem") as UpdatePanel).Update();
        }

        private void ValidaAcesso()
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

                            if (this.PaginaInformadaPodeAcessar(this._listRoleUsuarioLogado, url_solicitada) == true)
                                ResultadoAcesso = 0;
                            else
                                ResultadoAcesso = 1;

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

        protected bool PaginaInformadaPodeAcessar(List<string> _listRoles, string urlPagina)
        {
            bool podeAcessar = false;

            try
            {

                //List<Infra.Classes.PermissaoPedidoExameExterno> _listPaginas = new Classes.PermissaoPedidoExameExterno().ObterTodasPermissoes();

                //if (_listPaginas != null && _listPaginas.Count > 0)
                //{
                //    foreach (string itemRoleUsuario in _listRoles)
                //    {
                //        if (!string.IsNullOrWhiteSpace(itemRoleUsuario) && !string.IsNullOrWhiteSpace(urlPagina))
                //        {

                //            var conta = (from contaL in _listPaginas
                //                         where contaL.GrupoPodeAcessar.ToUpper().Contains(itemRoleUsuario.ToUpper()) &&
                //                               contaL.NomePaginaASPX.ToUpper().Contains(urlPagina.ToUpper())
                //                         select contaL).Count();

                //            if (conta > 0)
                //            {
                //                podeAcessar = true;
                //                break;
                //            }

                //        }
                //    }
                //}

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

                DesabilitarControlesDaPagina(c);
            }
        }

        private void RegistrarJs()
        {
            //ScriptManager.RegisterClientScriptInclude(this, typeof(string), "jquery", Page.ResolveClientUrl("~/") + "JsAux/jquery-ui-1.8.21.custom/js/jquery-1.7.2.min.js");
            //ScriptManager.RegisterClientScriptInclude(this, typeof(string), "format", Page.ResolveClientUrl("~/InterfaceHC/js/FormataCamposLib.js"));
            //ScriptManager.RegisterStartupScript(this, typeof(string), "lib", "setTimeout('aplicacarFormatacaoCampos(document.forms[0]);', 2000);", true);
            //ScriptManager.RegisterClientScriptInclude(this, typeof(string), "jquery-mousewheel", Page.ResolveClientUrl("~/") + "InterfaceHC/Js/fancybox/jquery.mousewheel-3.0.4.pack.js");
            //ScriptManager.RegisterClientScriptInclude(this, typeof(string), "jquery-fancybox", Page.ResolveClientUrl("~/") + "InterfaceHC/Js/fancybox/jquery.fancybox-1.3.4.pack.js");
            //ScriptManager.RegisterClientScriptInclude(this, typeof(string), "jquery-ui", Page.ResolveClientUrl("~/") + "JsAux/jquery-ui-1.8.21.custom/js/jquery-ui-1.8.22.custom.min.js");
            //ScriptManager.RegisterClientScriptInclude(this, typeof(string), "jquery-mask", Page.ResolveClientUrl("~/") + "Jsaux/jquery.maskedinput-1.3.min.js");
            //ScriptManager.RegisterClientScriptInclude(this, typeof(string), "jmNotify", Page.ResolveClientUrl("~/") + "Jsaux/jquery.jmNotify.js");

            //ScriptManager.RegisterClientScriptInclude(this, typeof(string), "nicescroll", Page.ResolveClientUrl("~/") + "Jsaux/jquery.nicescroll.js");

            

            //HtmlHead head = (HtmlHead)Page.Header;
            //HtmlLink link;

            //// Estilo fancybox
            //link = new HtmlLink();
            //link.Attributes.Add("href", Page.ResolveClientUrl("~/") + "InterfaceHC/css/jquery.fancybox-1.3.4.css");
            //link.Attributes.Add("type", "text/css");
            //link.Attributes.Add("rel", "stylesheet");
            //head.Controls.Add(link);

            //// Estilo jquery datapiker
            //link = new HtmlLink();
            //link.Attributes.Add("href", Page.ResolveClientUrl("~/") + "Jsaux/jquery-ui-1.8.21.custom/css/custom-theme/jquery-ui-1.10.3.custom.css");
            //link.Attributes.Add("type", "text/css");
            //link.Attributes.Add("rel", "stylesheet");
            //head.Controls.Add(link);            
            
        }
        
        public void AbrirModal(string url, int width, int height)
        {
            //Está sendo registrado na master da framework.
            
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
        /// <param name="acao">Método javascript a ser executado assim que a modal é fechada(onClosed)~Exemplo: Post()</param>
        public void AbrirModal_NEW(string url, int width, int height, string acao = "", bool modal = false)
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
            str.Append("        'overlayColor' : '#182E3B', ");
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

        /// <summary>
        /// Ocultar botão início da master page~
        /// </summary>
        public void OcultarBotaoInicioDaMaster()
        {
            (Master.FindControl("ibtnInicio") as HyperLink).Visible = false;            
        }

        #endregion
    
    }
}
