using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Security;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao
{
    public partial class Default1 : System.Web.UI.Page
    {
        #region ViewState

        /// <summary>
        /// Carregar viewstate.
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            //if (this.ViewState["numUserBanco"] != null)
            //    this.numUserBanco = Convert.ToInt32(this.ViewState["numUserBanco"]);

        }

        /// <summary>
        /// Salvar ViewState
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            //this.ViewState["numUserBanco"] = this.numUserBanco;

            return base.SaveViewState();
        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {   
                
            }
        }

        #endregion

        protected void Button1_Click(object sender, EventArgs e)
        {
            
        }

        #region Métodos

        #endregion
    }
}