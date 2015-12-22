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
using ItensListaControle = Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Comum
{
    public partial class ImportaItensOutraLista : System.Web.UI.Page
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

                List<Entity.ListaControle> result = new BLL.ListaControle().ObterPorInstituto(instituto);

                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [WebMethod]
        public static string ObterItensPorListaControle(long seqlistaControle)
        {
            //Na tela de importação de itens somente listar os ATIVOS
            List<Entity.ItensListaControle> itens = new List<ItensListaControle>();

            BLL.ItensListaControle bllItensListaControle = new BLL.ItensListaControle();

            itens = bllItensListaControle.ObterItensPorListaControle(seqlistaControle,false);

            return JsonConvert.SerializeObject(itens, Formatting.Indented, new JsonSerializerSettings { });
        }

        [WebMethod]
        public static void ImportarItens(long seqListaPara, long seqListaDe)
        {
            try
            {
                BLL.ItensListaControle bllItensListaControle = new BLL.ItensListaControle();

                bllItensListaControle.ImportarItens(seqListaPara, seqListaDe);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}