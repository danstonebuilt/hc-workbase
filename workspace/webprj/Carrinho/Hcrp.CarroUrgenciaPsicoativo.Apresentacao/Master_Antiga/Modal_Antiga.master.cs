using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Master_Antiga
{
    public partial class Modal_Antiga : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AdicionarLinks();
        }
        
	    private void AdicionarLinks()
        {
            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "jquery", Page.ResolveClientUrl("~/") + "Framework/jquery/jQuery-1.11.0.min.js");

            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "1", Page.ResolveClientUrl("~/") + "Framework/jquery-ui-1.8.21.custom/js/jquery-ui-1.8.22.custom.min.js");
            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "2", Page.ResolveClientUrl("~/") + "Framework/jquery-ui-1.8.21.custom/js/jquery.ui.core.js");
            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "3", Page.ResolveClientUrl("~/") + "Framework/jquery-ui-1.8.21.custom/js/jquery.ui.widget.js");
            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "4", Page.ResolveClientUrl("~/") + "Framework/jquery-ui-1.8.21.custom/js/jquery.ui.effect.js");

            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "5", Page.ResolveClientUrl("~/") + "Framework/FancyBox/lib/jquery.mousewheel-3.0.6.pack.js");
            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "6", Page.ResolveClientUrl("~/") + "Framework/FancyBox/source/jquery.fancybox.pack.js");
            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "7", Page.ResolveClientUrl("~/") + "Framework/FancyBox/source/helpers/jquery.fancybox-buttons.js");

            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "8", Page.ResolveClientUrl("~/") + "Framework/FancyBox/source/jquery.fancybox.css");
            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "9", Page.ResolveClientUrl("~/") + "Framework/bootstrap3.0.3/js/bootstrap.js");
            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "10", Page.ResolveClientUrl("~/") + "Framework/toastr/js/jquery-toastr.js");
            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "11", Page.ResolveClientUrl("~/") + "Framework/Jsaux/JsCustomizado.js");

            HtmlHead head = (HtmlHead)Page.Header;
            HtmlLink link;

            // Estilo fancybox
            link = new HtmlLink();
            link.Attributes.Add("href", Page.ResolveClientUrl("~/") + "Framework/bootstrap3.0.3/css/bootstrap.css");
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);

            // Estilo jquery datapiker
            link = new HtmlLink();
            link.Attributes.Add("href", Page.ResolveClientUrl("~/") + "Framework/bootstrap3.0.3/css/bootstrap-theme.css");
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);

            // Estilo jquery datapiker
            link = new HtmlLink();
            link.Attributes.Add("href", Page.ResolveClientUrl("~/") + "Framework/toastr/css/toastr.css");
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);

            link = new HtmlLink();
            link.Attributes.Add("href", Page.ResolveClientUrl("~/") + "Framework/jquery-ui-1.8.21.custom/css/custom-theme/jquery-ui.css");
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);

            link = new HtmlLink();
            link.Attributes.Add("href", Page.ResolveClientUrl("~/") + "Framework/jquery-ui-1.8.21.custom/css/custom-theme/jquery.ui.all.css");
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);

            link = new HtmlLink();
            link.Attributes.Add("href", Page.ResolveClientUrl("~/") + "Framework/jquery-ui-1.8.21.custom/css/custom-theme/jquery.ui.core.css");
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);

            link = new HtmlLink();
            link.Attributes.Add("href", Page.ResolveClientUrl("~/") + "Framework/jquery-ui-1.8.21.custom/css/custom-theme/jquery.ui.dialog.css");
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);

            // Estilo jquery datapiker
            link = new HtmlLink();
            link.Attributes.Add("href", Page.ResolveClientUrl("~/") + "Framework/Cssaux/carroUrgenciaPsicoativo-customizado.css");
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);
        }
    }
}