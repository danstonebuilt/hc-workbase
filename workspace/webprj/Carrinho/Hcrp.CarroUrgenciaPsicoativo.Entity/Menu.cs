using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.Entity
{
    public class Menu
    {
        #region Propriedades

        public Int32 NumSequencia { get; set; }
        public string NomeMenuPai { get; set; }
        public Int64 CodPrograma { get; set; }
        public string NomeMenu { get; set; }
        public string NomeMenuExibicao { get; set; }
        public string DscUrl { get; set; }
        public int IdfMenu { get; set; }
        public int QtdItens { get; set; }
        public Int64 SeqElementoProntuario { get; set; }
        public Int64 SeqElementoProntuarioPaciente { get; set; }        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.Menu> listaDeFilhos { get; set; }

        #endregion
    }
}
