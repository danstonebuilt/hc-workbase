using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.Entity
{
    public class AtendimentoPaciente 
    {
        #region Propriedades

        public Int64 SeqAtendimento { get; set; }

        public string CodPaciente { get; set; }

        public string NomePaciente { get; set; }

        #endregion
    }
}
