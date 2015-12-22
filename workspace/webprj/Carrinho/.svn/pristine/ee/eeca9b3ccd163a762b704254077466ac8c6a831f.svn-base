using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao.Relatorio
{
    public partial class ViewPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ResponseRelatorio"] == null)
                {
                    Response.Write("Não é possível abrir o relatório.");
                    return;
                }

                try
                {
                    var byteArray = Session["ResponseRelatorio"] as byte[];

                    var response = HttpContext.Current.Response;

                    response.Clear();
                    response.ClearHeaders();
                    response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", byteArray.Length.ToString());
                    response.BinaryWrite(byteArray);
                    response.Flush();
                    response.Close();
                    Session.Remove("ResponseRelatorio");
                }
                catch (Exception)
                {
                    Response.Write("Não é possível abrir o relatório.");
                    return;
                }
            }
        }
    }
}