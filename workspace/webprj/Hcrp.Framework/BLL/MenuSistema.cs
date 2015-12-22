using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.BLL
{
    public class MenuSistema
    {
        public static string sessionObjetoSelecaoMenu = "ObjetoSelecaoMenuSistema";
        public static string sessionListagemMenuSistema = "ListagemMenuSistema";

        public static List<Hcrp.Framework.Entity.MenuSistema> getMenuSistema(long pNumeroUsuarioBanco, int pCodigoInstituto, int pCodigoSistema, bool pListarAtivos = true, bool pListarInativos = false, bool pListarNaoMenus = false)
        {
            List<Hcrp.Framework.Entity.MenuSistema> result = Dal.MenuSistema.getMenuSistema(pNumeroUsuarioBanco, pCodigoInstituto, pCodigoSistema, pListarAtivos, pListarInativos, pListarNaoMenus).ToList();

            return result;

        }
    }
}
