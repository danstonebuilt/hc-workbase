using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao
{   
    public partial class HcrpMaster : System.Web.UI.MasterPage
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Expires = -1500;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "no-cache");

            this.AdicionarLinks();

            if (!Page.IsPostBack)
            {
                //if (HttpContext.Current.User.Identity.IsAuthenticated)
                //{
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

                        var instituto = new Framework.Classes.Instituto().BuscarInstituto(Config.IpComputador);

                        var instituicao =
                            new Framework.Classes.Instituicao().BuscaInstituicaoCodigo((instituto.CodInstSistema > 0
                                                                                            ? instituto.CodInstSistema.
                                                                                                  ToString()
                                                                                            : ""));

                        lblVersao.Text = Config.IpComputador + " | " +
                                         Config.ServidorBancoDados + " | " + u.NomeCompleto + " | " +
                                         instituto.NomeInstituto + " | " + instituicao.Nome;
                    }
                    catch
                    {
                    }
                }
            //}
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

        //    HtmlHead head = (HtmlHead)Page.Header;
        //    HtmlLink link;

        //    // Global estilo
        //    link = new HtmlLink();
        //    link.Attributes.Add("href", Page.ResolveUrl("~/InterfaceHC/css/Global_estilo.css"));
        //    link.Attributes.Add("type", "text/css");
        //    link.Attributes.Add("rel", "stylesheet");
        //    head.Controls.Add(link);

        //    // Global fontes
        //    link = new HtmlLink();
        //    link.Attributes.Add("href", Page.ResolveUrl("~/InterfaceHC/css/Global_fontes.css"));
        //    link.Attributes.Add("type", "text/css");
        //    link.Attributes.Add("rel", "stylesheet");
        //    head.Controls.Add(link);

        //    // Tabela principal
        //    link = new HtmlLink();
        //    link.Attributes.Add("href", Page.ResolveUrl("~/InterfaceHC/css/Tabela_principal.css"));
        //    link.Attributes.Add("type", "text/css");
        //    link.Attributes.Add("rel", "stylesheet");
        //    head.Controls.Add(link);

        //    // Padrao HC
        //    link = new HtmlLink();
        //    link.Attributes.Add("href", Page.ResolveUrl("~/InterfaceHC/css/PadraoHC.css"));
        //    link.Attributes.Add("type", "text/css");
        //    link.Attributes.Add("rel", "stylesheet");
        //    head.Controls.Add(link);

        //    // Menu
        //    link = new HtmlLink();
        //    link.Attributes.Add("href", Page.ResolveUrl("~/InterfaceHC/css/Menu.css"));
        //    link.Attributes.Add("type", "text/css");
        //    link.Attributes.Add("rel", "stylesheet");
        //    head.Controls.Add(link);
        }

        #endregion
    }
}