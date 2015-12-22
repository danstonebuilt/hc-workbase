using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Master
{
    public partial class Modal : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.AdicionarLinks();
        }

        private void AdicionarLinks()
        {

            //HtmlHead head = (HtmlHead)Page.Header;
            //HtmlLink link;

            //// Global estilo
            //link = new HtmlLink();
            //link.Attributes.Add("href", Page.ResolveUrl("~/InterfaceHC/css/Global_estilo.css"));
            //link.Attributes.Add("type", "text/css");
            //link.Attributes.Add("rel", "stylesheet");
            //head.Controls.Add(link);

            //// Global fontes
            //link = new HtmlLink();
            //link.Attributes.Add("href", Page.ResolveUrl("~/InterfaceHC/css/Global_fontes.css"));
            //link.Attributes.Add("type", "text/css");
            //link.Attributes.Add("rel", "stylesheet");
            //head.Controls.Add(link);

            //// Tabela principal
            //link = new HtmlLink();
            //link.Attributes.Add("href", Page.ResolveUrl("~/InterfaceHC/css/Tabela_principal.css"));
            //link.Attributes.Add("type", "text/css");
            //link.Attributes.Add("rel", "stylesheet");
            //head.Controls.Add(link);

            //// Padrao HC
            //link = new HtmlLink();
            //link.Attributes.Add("href", Page.ResolveUrl("~/InterfaceHC/css/PadraoHC.css"));
            //link.Attributes.Add("type", "text/css");
            //link.Attributes.Add("rel", "stylesheet");
            //head.Controls.Add(link);

            //// Menu
            //link = new HtmlLink();
            //link.Attributes.Add("href", Page.ResolveUrl("~/InterfaceHC/css/Menu.css"));
            //link.Attributes.Add("type", "text/css");
            //link.Attributes.Add("rel", "stylesheet");
            //head.Controls.Add(link);
        }
    }
}