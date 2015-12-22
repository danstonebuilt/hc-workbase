using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Elmah;
using Hcrp.CarroUrgenciaPsicoativo.BLL;
using Hcrp.CarroUrgenciaPsicoativo.Entity;
using Newtonsoft.Json;
using RepositorioListaControle = Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioListaControle;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Basico
{
    public partial class Repositorio : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string CarregarComboInstituto()
        {
            try
            {
                
                int codInstituicao = Framework.Infra.Util.Parametrizacao.Instancia().CodInstituicao;

                //int codInstituicao = 1;



                //this.ddlInstituto.DataValueField = "CodInstituto";
                //this.ddlInstituto.DataTextField = "NomeInstituto";
                //this.ddlInstituto.DataSource = new Framework.Classes.Instituto().BuscarInstitutosPorInstituicao(codInstituicao);
                //this.ddlInstituto.DataBind();
                //this.ddlInstituto.Items.Insert(0, new ListItem("SELECIONE", "0"));

                List<Framework.Classes.Instituto> result = new Framework.Classes.Instituto().BuscarInstitutosPorInstituicao(codInstituicao);

                return JsonConvert.SerializeObject(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {

            //url is in pattern "~myblog/mypage.aspx"
            string redirectURL = Page.ResolveClientUrl("~/") + "Menu.aspx";
            //string script = "window.location = '" + redirectURL + "';";
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "RedirectTo", script, true);

            this.Response.Redirect(redirectURL, false);
        }

        [WebMethod]
        public static string ListarRepositorios(int seqInstituto, bool status)
        {
            List<Entity.RepositorioListaControle> itens = new List<RepositorioListaControle>();

            BLL.RepositorioListaControle bllRepositorioListaControle = new BLL.RepositorioListaControle();

            itens = bllRepositorioListaControle.ObterPorInstituto(seqInstituto, status);

            return JsonConvert.SerializeObject(itens, Formatting.Indented, new JsonSerializerSettings { });
        }

        [WebMethod]
        public static string ListarTipoPatrimonio()
        {
            BLL.TipoPatrimonio bllTipoPatrimonio = new BLL.TipoPatrimonio();

            return JsonConvert.SerializeObject(bllTipoPatrimonio.ObterTiposDePatrimonio(), Formatting.Indented);
        }

        [WebMethod]
        public static string ListarTipoRepositorio()
        {
            BLL.TipoRepositorioListaControle bllTipoPatrimonio = new BLL.TipoRepositorioListaControle();

            return JsonConvert.SerializeObject(bllTipoPatrimonio.ListarTiposDeRepositorio(), Formatting.Indented);
        }

        [WebMethod]
        public static string CarregarComboTipoLista(int instituto)
        {
            try
            {
                int codInstituicao = Framework.Infra.Util.Parametrizacao.Instancia().CodInstituicao;

                //todo configurar no web config a lista padrao
                // Não pode deixar que a lista padrao fique associada a um repositório
                List<Entity.ListaControle> result = new BLL.ListaControle().ObterPorInstituto(instituto).Where(x => x.SeqListaControle != 4 && x.SeqListaControle != 17).ToList();

                return JsonConvert.SerializeObject(result, Formatting.Indented);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [WebMethod]
        public static string ObterCentroDeCusto(string codigoCC, int codigoInstituto)
        {
            BLL.CentroDeCusto bllCC = new BLL.CentroDeCusto();

            return JsonConvert.SerializeObject(bllCC.ObterCentroDeCusto(codigoCC, codigoInstituto), Formatting.Indented);
        }

        [WebMethod]
        public static string ObterPatrimonioPorNumeroETipo(Int64? numPatrimonio, Int64? tipoPatrimonio)
        {
            BLL.BemPatrimonial bllBemPat = new BLL.BemPatrimonial();

            return JsonConvert.SerializeObject(bllBemPat.ObterPatrimonioPorNumeroETipo(numPatrimonio, tipoPatrimonio), Formatting.Indented);
        }

        [WebMethod]
        public static void AdicionarRepositorio(Entity.RepositorioListaControle item)
        {
            BLL.RepositorioListaControle bllReposit = new BLL.RepositorioListaControle();

            bllReposit.AdicionarRepositorio(item);
        }

        [WebMethod]
        public static void AtivarInativarItem(bool ehPraAtivar, long seqRepositorio)
        {
            BLL.RepositorioListaControle bllRepositorioListaControle = new BLL.RepositorioListaControle();

            bllRepositorioListaControle.AtivarOuInativar(ehPraAtivar, seqRepositorio);
        }

        [WebMethod]
        public static void AlterarDadosItem(Entity.RepositorioListaControle repositorio)
        {
            BLL.RepositorioListaControle bllRepositorioListaControle = new BLL.RepositorioListaControle();
            bllRepositorioListaControle.AtualizarItem(repositorio);
        }

        [WebMethod]
        public static string ObterRepositorio(Int64 seqRepositorio)
        {
            BLL.RepositorioListaControle bllRepositorioListaControle = new BLL.RepositorioListaControle();

            return JsonConvert.SerializeObject(bllRepositorioListaControle.ObterPorId(seqRepositorio), Formatting.Indented);
        }

        [WebMethod]
        public static string ObterCentrosCustoDoRepositorio(Int64 seqRepositorio)
        {
            BLL.RepositorioListaControle bllRepositorioListaControle = new BLL.RepositorioListaControle();

            return JsonConvert.SerializeObject(bllRepositorioListaControle.ObterCentrosDeCustoDoRepositorio(seqRepositorio), Formatting.Indented);
        }
    }
}