using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Hcrp.Framework.Infra.Util
{
    public class CustomPage : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            HtmlHead head = (HtmlHead)Page.Header;
            HtmlLink link;

            // Global estilo
            link = new HtmlLink();
            link.Attributes.Add("href", String.Concat(System.Configuration.ConfigurationManager.AppSettings["urlSistema"], "Global_estilo.css"));
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);

            // Global fontes
            link = new HtmlLink();
            link.Attributes.Add("href", String.Concat(System.Configuration.ConfigurationManager.AppSettings["urlSistema"], "Global_fontes.css"));
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);

            // Tabela principal
            link = new HtmlLink();
            link.Attributes.Add("href", String.Concat(System.Configuration.ConfigurationManager.AppSettings["urlSistema"], "Tabela_principal.css"));
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);

            // grs
            link = new HtmlLink();
            link.Attributes.Add("href", String.Concat(System.Configuration.ConfigurationManager.AppSettings["urlSistema"], "grs.css"));
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);

            // menu
            link = new HtmlLink();
            link.Attributes.Add("href", String.Concat(System.Configuration.ConfigurationManager.AppSettings["urlSistema"], "menu.css"));
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);

            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "jquery", Page.ResolveClientUrl("~/") + "js/jquery-1.4.2.min.js");
            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "format", Page.ResolveClientUrl("~/") + "js/FormataCamposLib.js");
            ScriptManager.RegisterStartupScript(this, typeof(string), "lib", "setTimeout('aplicacarFormatacaoCampos(document.forms[0]);', 2000);", true);
            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "functions", Page.ResolveClientUrl("~/") + "js/functions.js");
            ScriptManager.RegisterClientScriptInclude(this.Page, typeof(string), "ext-base", Page.ResolveClientUrl("~/") + "frameworks/extjs/adapter/ext/ext-base.js");
            ScriptManager.RegisterClientScriptInclude(this.Page, typeof(string), "ext-all", Page.ResolveClientUrl("~/") + "frameworks/extjs/ext-all.js");
            ScriptManager.RegisterClientScriptInclude(this.Page, typeof(string), "ext-lang-pt_br-min", Page.ResolveClientUrl("~/") + "frameworks/extjs/build/locale/ext-lang-pt_br-min.js");


            if (this.Session["MensagemOutraPagina"] != null)
            {
                System.Collections.Generic.Dictionary<TipoMensagem, string> msg = this.Session["MensagemOutraPagina"] as System.Collections.Generic.Dictionary<TipoMensagem, string>;
                this.ExibirMensagem((TipoMensagem)msg.Keys.FirstOrDefault(), msg.Values.FirstOrDefault().ToString());
                Session.Remove("MensagemOutraPagina");
            }

            //Registrar scripts da modal na página.
            this.RegistraModal();

        }

        /// <summary>
        /// Abrir janela modal.
        /// </summary>
        /// <param name="url"></param>
        public void AbrirModal(string url, string width, string height, string tituloJanela)
        {
            string operador = "";

            if (url.Contains("?"))
            {
                operador = "&";
            }
            else
            {
                operador = "?";
            }

            System.Text.StringBuilder inicializaShadow = new System.Text.StringBuilder();

            inicializaShadow.AppendLine("var winTPesquisa2; function AbreModal(){");
            inicializaShadow.AppendLine("if (!winTPesquisa2) {");
            inicializaShadow.AppendLine(" winTPesquisa2 = new Ext.Window({");
            inicializaShadow.AppendLine("el: 'janelaModal',");
            inicializaShadow.AppendLine("layout: 'fit',");
            inicializaShadow.AppendLine(string.Concat("width: ", width, ","));
            inicializaShadow.AppendLine(string.Concat("height: ", height, ","));
            inicializaShadow.AppendLine("closeAction: 'hide',");
            inicializaShadow.AppendLine("modal: true,");
            inicializaShadow.AppendLine("plain: true,");
            inicializaShadow.AppendLine("items: new Ext.TabPanel({");
            inicializaShadow.AppendLine("el: 'janelaModal_tabs',");
            inicializaShadow.AppendLine("autoTabs: true,");
            inicializaShadow.AppendLine("activeTab: 0,");
            inicializaShadow.AppendLine("deferredRender: false,");
            inicializaShadow.AppendLine("border: false");
            inicializaShadow.AppendLine("}),");
            inicializaShadow.AppendLine(" buttons: [{");
            inicializaShadow.AppendLine(" text: 'Fechar',");
            inicializaShadow.AppendLine("id: 'btn_fecha_iframe_TPesquisa2',");
            inicializaShadow.AppendLine("handler: function() {");
            inicializaShadow.AppendLine("winTPesquisa2.hide();");
            inicializaShadow.AppendLine("}");
            inicializaShadow.AppendLine("}]");
            inicializaShadow.AppendLine("});");
            inicializaShadow.AppendLine("}else{");
            inicializaShadow.AppendLine("winTPesquisa2.setHeight(" + height + ");winTPesquisa2.setWidth(" + width + "); winTPesquisa2.doLayout();");
            inicializaShadow.AppendLine("};");

            // Mudando chamada do IFRAME
            inicializaShadow.AppendLine("x = window.frames['IFramePesquisa']; x.document.open(); x.document.close(); x.focus()");
            inicializaShadow.AppendLine(string.Concat("$('#IFramePesquisa').attr('src','", System.Configuration.ConfigurationManager.AppSettings["url"] + url + operador + "r=" + System.Guid.NewGuid().ToString(), "');"));
            inicializaShadow.AppendLine(string.Concat("$('#IFramePesquisa').attr('width','" + width + "');"));
            inicializaShadow.AppendLine(string.Concat("$('#IFramePesquisa').attr('height','" + height + "');"));
            inicializaShadow.AppendLine("winTPesquisa2.show();};");
            inicializaShadow.AppendLine("setTimeout('AbreModal()', 1)");
            inicializaShadow.AppendLine("function closeIframe() {winTPesquisa2.hide();}");

            //ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), "shadow", inicializaShadow.ToString(), true);

            //string urlCompleta = "setTimeout(\"ModalBox('" + Request.Url.GetLeftPart(UriPartial.Authority) + url + "'," + width + "," + height + ",'" + tituloJanela + "');\"" + ",1000);";
            string urlCompleta = "setTimeout(\"ModalBox('" + System.Configuration.ConfigurationManager.AppSettings["url"] + url + "'," + width + "," + height + ",'" + tituloJanela + "');\"" + ",1000);";
            ScriptManager.RegisterStartupScript(this, typeof(string), "janelaModal", inicializaShadow.ToString(), true);
        }

        public enum TipoMensagem { Alerta, Sucesso, Erro, Informacao };

        /// <summary>
        /// Exibir mensagem.
        /// </summary>
        /// <param name="tipoMensagem"></param>
        /// <param name="mensagem"></param>
        public void ExibirMensagem(TipoMensagem tipoMensagem, string mensagem)
        {

            switch (tipoMensagem)
            {
                case TipoMensagem.Alerta:
                    (Master.FindControl("pnlMensagem") as Panel).CssClass = "warning";
                    break;
                case TipoMensagem.Erro:
                    (Master.FindControl("pnlMensagem") as Panel).CssClass = "error";
                    break;
                case TipoMensagem.Informacao:
                    (Master.FindControl("pnlMensagem") as Panel).CssClass = "info";
                    break;
                case TipoMensagem.Sucesso:
                    (Master.FindControl("pnlMensagem") as Panel).CssClass = "success";
                    break;
            }

            Label lblMensagem = (Master.FindControl("lblMensagem") as Label);
            lblMensagem.Text = mensagem;

            (Master.FindControl("pnlMensagem") as Panel).Visible = true;
            ScriptManager.RegisterStartupScript(this, typeof(string), "msg", "$(\"#" + (Master.FindControl("pnlMensagem") as Panel).ClientID + "\").slideDown(500);", true);

            (Master.FindControl("updMensagem") as UpdatePanel).Update();

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
            (Master.FindControl("pnlMensagem") as Panel).Visible = false;
            (Master.FindControl("updMensagem") as UpdatePanel).Update();
        }

        public void RegistraModal()
        {
            
        }
    }
}
