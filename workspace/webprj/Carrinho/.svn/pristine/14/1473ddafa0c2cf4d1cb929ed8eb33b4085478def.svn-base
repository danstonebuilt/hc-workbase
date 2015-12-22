using System;
using System.Collections.Generic;
using System.Data;
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
using ItensListaControle = Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Basico
{
    public partial class ItemTipoLista : Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao.CarroUrgenciaPsicoativoPage
    {
        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            string redirectURL = Page.ResolveClientUrl("~/") + "Menu.aspx";
            
            this.Response.Redirect(redirectURL, false);
        }

        #endregion

        #region Métodos

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

        [WebMethod]
        public static string CarregarComboTipoLista(int instituto)
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

                List<Entity.ListaControle> result = new BLL.ListaControle().ObterPorInstituto(instituto);


                //int zero = 0;

                //return (10 / zero);

                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [WebMethod]

        public static string InserirMaterial(Entity.ItensListaControle item)
        {
            BLL.ItensListaControle bllItensListaControle = new BLL.ItensListaControle();

            return JsonConvert.SerializeObject(bllItensListaControle.Adicionar(item));
        }

        [WebMethod]
        public static string ObterMaterial(string codigoMaterial)
        {
            List<Entity.Material> entMaterial = new List<Entity.Material>();

            BLL.Material bllMaterial = new BLL.Material();

            string codMaterial = string.Empty;
            long numLote = 0;

            bllMaterial.ObtemInsumos(codigoMaterial, out codMaterial, out numLote);

            entMaterial = bllMaterial.ObterMaterial(codMaterial);

            /*if(numLote > 0)
            {
                var novo = entMaterial.FirstOrDefault();
            }*/
            
            return JsonConvert.SerializeObject(entMaterial);
        }

        [WebMethod]
        public static string CarregarComboAlinea()
        {
            List<Framework.Classes.Alinea> listAlinea = new List<Framework.Classes.Alinea>();

            Framework.Dal.Alinea DalAlinea = new Framework.Dal.Alinea();

            listAlinea = DalAlinea.ObterListaDeAlinea();

            return JsonConvert.SerializeObject(listAlinea);
        }

        [WebMethod]
        public static string CarregarComboUnidadeDeMedida()
        {
            List<Framework.Classes.Unidade> listAlinea = new List<Framework.Classes.Unidade>();

            listAlinea = new Framework.Classes.Unidade().BuscarListaDeUnidade();

            return JsonConvert.SerializeObject(listAlinea);
        }

        [WebMethod]
        public static string ObterItensPorListaControle(long seqlistaControle, bool status)
        {
            List<Entity.ItensListaControle> itens = new List<ItensListaControle>();

            BLL.ItensListaControle bllItensListaControle = new BLL.ItensListaControle();

            itens = bllItensListaControle.ObterItensPorListaControle(seqlistaControle, status);

            return JsonConvert.SerializeObject(itens, Formatting.Indented, new JsonSerializerSettings { });
        }

        [WebMethod]
        public static void AtivarInativarItem(bool ehPraAtivar, long seqItemListaControle)
        {
            BLL.ItensListaControle bllItensListaControle = new BLL.ItensListaControle();

            bllItensListaControle.AtivarOuInativar(ehPraAtivar, seqItemListaControle);
        }

        [WebMethod]
        public static void AlterarDadosItem(long seqItem, int quantidade)
        {
            BLL.ItensListaControle bllItensListaControle = new BLL.ItensListaControle();

            bllItensListaControle.AtualizarItem(seqItem, quantidade);
        }

        #endregion
    }
}