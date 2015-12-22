using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Comum
{
    public partial class BuscaTipoBem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string PesquisarTipoBem(string codigoTipoBem, string nomeTipoBem)
        {
            List<Entity.TipoBem> listAlinea = new List<Entity.TipoBem>();

            BLL.TipoBem bllTipoBem = new BLL.TipoBem();

            listAlinea = bllTipoBem.ObterListaTipoBem(codigoTipoBem, nomeTipoBem);

            return JsonConvert.SerializeObject(listAlinea);
        }
    }
}