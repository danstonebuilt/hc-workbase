using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Hcrp.CarroUrgenciaPsicoativo.BLL.FrameworkAplicacao;

namespace Hcrp.CarroUrgenciaPsicoativo.Apresentacao
{
    
    public partial class menu : CarroUrgenciaPsicoativoPage
    {
        public List<Hcrp.Framework.Entity.MenuSistema> Lista
        {
            get { return (List<Hcrp.Framework.Entity.MenuSistema>)Session[Hcrp.Framework.BLL.MenuSistema.sessionListagemMenuSistema]; }
            set { Session[Hcrp.Framework.BLL.MenuSistema.sessionListagemMenuSistema] = value; }
        }

        #region Métodos

        private void CarregarMenu()
        {
            try
            {
                //Definir chamada de acesso para:
                //p_num_user_banco   := 6802
                //p_cod_sistema      :=  182 
                //p_cod_inst_sistema :=    1

                Hcrp.Framework.Entity.MenuSistema a = new Hcrp.Framework.Entity.MenuSistema();

                int cod_inst_sistema = 1;

                int cod_sistema = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CodigoSistema"]);

                //numero do sistema;
                long num_user_banco = Convert.ToInt64(Convert.ToString(NumeroUsuarioBancoLogado));

                Lista = Hcrp.Framework.BLL.MenuSistema.getMenuSistema(num_user_banco, cod_inst_sistema, cod_sistema, true, false);

                if (Lista.Count == 0)
                {
                    this.ExibirMensagem(TipoMensagem.Erro, "O usuário não possui nenhum menu para exibição !");
                    return;
                }

                var menu = (from item in Lista where item.level == 1 && item.idf_menu != "N" orderby item.num_ordem ascending select new { nom_exibicao_programa = item.nom_exibicao_programa, cod_programa = item.cod_programa }).ToList();

                RpMenuRepeater.DataSource = menu;
                RpMenuRepeater.DataBind();
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (TipoUsuarioLogado != null)
                {
                    CarregarMenu();
                }
                else
                {
                    ExibirMensagem(TipoMensagem.Erro, "O usuário não possui acesso ao sistema !" + "<BR> SERVIDOR :" + System.Environment.MachineName);
                }
            }
        }

        protected void RpSubMenuRepeate_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Para cada grupo de menu, carregar seus itens de grupo quais são filhos.            
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (Lista != null)
                {
                    var MenuLista = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "nom_exibicao_programa"));

                    int MenuListaPai = 0;

                    // Montar os submenus dentro do menu pai
                    try
                    {
                        MenuListaPai = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "cod_programa"));
                    }
                    catch { MenuListaPai = 0; }

                    if (!string.IsNullOrWhiteSpace(MenuLista) && MenuListaPai > 0)
                    {
                        //Carga do Submenu no Controle.
                        var submenu = (from l in Lista
                                       where l.level == 2 && l.idf_menu != "N" && l.cod_programa_pai == MenuListaPai
                                       orderby l.num_ordem ascending

                                       select new
                                       {
                                           nom_exibicao_programa = l.nom_exibicao_programa,
                                           dsc_pagina_web = l.dsc_pagina_web
                                       });

                        Repeater RpSubRepeate = e.Item.FindControl("RpSubMenuRepeate") as Repeater;

                        if (RpSubRepeate != null)
                        {
                            RpSubRepeate.DataSource = submenu;
                            RpSubRepeate.DataBind();
                        }
                    }
                }
            }
        }
        #endregion
    }
}