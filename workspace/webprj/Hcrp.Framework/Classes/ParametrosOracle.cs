using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ParametrosOracle
    {
        public string CampoOracle { get; set; }
        public object ValorOracle { get; set; }
        public eTipoAssociacao TipoAssociacao { get; set; }
        public ParametrosOracle()
        { }

        public enum eTipoAssociacao
        {
            Igual,
            Like,
            Is,
            IsNot,
            IsNull,
            Between,
            In
        }
    }
}
