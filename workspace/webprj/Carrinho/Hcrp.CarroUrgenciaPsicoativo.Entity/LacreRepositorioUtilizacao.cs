using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.Entity
{
    public class LacreRepositorioUtilizacao
    {
        #region Propriedades

        public Int64 SeqLacreRepositUtil { get; set; }
        public AtendimentoPaciente AtendimentoPaciente { get; set; }
        public LacreRepositorioItens LacreRepositorioItens { get; set; }
        public Int32? QtdUtilizada { get; set; }
        public string DscJustificativa { get; set; }
        public DateTime DataCadastro { get; set; }
        public Usuario UsuarioCadastro { get; set; }

        #endregion
    }
}
