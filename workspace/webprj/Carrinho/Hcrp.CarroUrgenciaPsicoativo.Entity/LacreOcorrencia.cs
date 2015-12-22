using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.Entity
{
    public class LacreOcorrencia
    {
        #region Propriedades

        public Int64 SeqLacreOcorrencia { get; set; }
        public LacreRepositorio LacreRepositorio { get; set; }
        public string DscOcorrencia { get; set; }
        public DateTime DataCadastro { get; set; }
        public Usuario UsuarioCadastro { get; set; }

        #endregion
    }
}
