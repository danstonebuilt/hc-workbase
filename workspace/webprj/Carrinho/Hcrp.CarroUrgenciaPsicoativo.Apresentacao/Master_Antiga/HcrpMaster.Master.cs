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
    public partial class HcrpMaster : System.Web.UI.MasterPage
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            AdicionarLinks();

            if (!Page.IsPostBack)
            {
                

                try
                {
                    //Hcrp.Framework.Classes.UsuarioConexao u = new Hcrp.Framework.Classes.UsuarioConexao();
                    //Hcrp.Framework.Classes.ConfiguracaoSistema Config = new Framework.Classes.ConfiguracaoSistema();

                    //Hcrp.Framework.Classes.Instituto Instituto_ = new Hcrp.Framework.Classes.Instituto();
                    //Instituto_ = new Hcrp.Framework.Classes.Instituto().BuscarInstitutoCodigo(Config.CodInstituto);

                    //Hcrp.Framework.Classes.Instituicao Instituicao_ = new Framework.Classes.Instituicao();
                    //Instituicao_ = new Framework.Classes.Instituicao().BuscaInstituicaoCodigo(Config.CodInstituicaoSistema.ToString());

                    //string numVersao = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

                    //lblVersao.Text = Config.IpComputador + " | " +
                    //Config.ServidorBancoDados + " | " + Instituto_.NomeInstituto + " | " + Instituicao_.Nome + " | " + numVersao; // +" | " + u.NomeCompleto;


                    Hcrp.Framework.Classes.UsuarioConexao u = new Hcrp.Framework.Classes.UsuarioConexao();
                    Hcrp.Framework.Classes.ConfiguracaoSistema Config = new Framework.Classes.ConfiguracaoSistema();
                    var instituto = new Framework.Classes.Instituto().BuscarInstituto(HttpContext.Current.Request.UserHostAddress.ToString());
                    var instituicao = new Framework.Classes.Instituicao().BuscaInstituicaoCodigo((instituto.CodInstSistema > 0 ? instituto.CodInstSistema.ToString() : ""));

                    lblVersao.Text = Config.IpComputador + " | " +
                                     Config.ServidorBancoDados + " | " + u.NomeCompleto + " | " + instituto.NomeInstituto + " | " + instituicao.Nome;
                }
                catch { }
            }
        }

        protected void imgInicio_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("menu.aspx");
        }

        protected void imgSair_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        protected void ibtnInicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Menu.aspx", false);
        }

        #endregion

        #region Métodos

        private void AdicionarLinks()
        {



    




    //<script src="<%=Page.ResolveClientUrl("~/") %>Framework/Jsaux/jquery-validate.js" type="text/javascript"></script>
    //<script src="<%=Page.ResolveClientUrl("~/") %>Framework/Jsaux/jquery-validate-pt-br.js" type="text/javascript"></script>

            ScriptManager.RegisterClientScriptInclude(this, typeof(string), "jquery", Page.ResolveClientUrl("~/") + "Framework/jquery/jQuery-1.11.0.min.js");

            //ScriptManager.RegisterClientScriptInclude(this, typeof(string), "jquery", Page.ResolveClientUrl("~/") + "Framework/jquery-ui-1.8.21.custom/js/jquery-1.7.2.min.js");

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

            //ScriptManager.RegisterClientScriptInclude(this, typeof(string), "11", Page.ResolveClientUrl("~/") + "Framework/jquery-ui-1.8.21.custom/js/jquery.ui.datepicker.js");

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
            //link = new HtmlLink();
            //link.Attributes.Add("href", Page.ResolveClientUrl("~/") + "Framework/toastr/css/toastr-responsive.css");
            //link.Attributes.Add("type", "text/css");
            //link.Attributes.Add("rel", "stylesheet");
            //head.Controls.Add(link);

            // Estilo jquery datapiker
            link = new HtmlLink();
            link.Attributes.Add("href", Page.ResolveClientUrl("~/") + "Framework/Cssaux/carroUrgenciaPsicoativo-customizado.css");
            link.Attributes.Add("type", "text/css");
            link.Attributes.Add("rel", "stylesheet");
            head.Controls.Add(link);

            //link = new HtmlLink();
            //link.Attributes.Add("href", Page.ResolveClientUrl("~/") + "Framework/jquery-ui-1.8.21.custom/css/custom-theme/jquery.ui.datepicker.css");
            //link.Attributes.Add("type", "text/css");
            //link.Attributes.Add("rel", "stylesheet");
            //head.Controls.Add(link);
            
        }

        #endregion
    }
}