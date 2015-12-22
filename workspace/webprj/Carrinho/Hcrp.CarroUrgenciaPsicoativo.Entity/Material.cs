using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hcrp.Infra.AcessoDado;

namespace Hcrp.CarroUrgenciaPsicoativo.Entity
{
    public class Material : Framework.Classes.Material
    {
        public Framework.Classes.Alinea Alinea;

        public Framework.Classes.Unidade Unidade;
    }
}
