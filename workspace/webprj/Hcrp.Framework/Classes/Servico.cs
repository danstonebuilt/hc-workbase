using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Servico
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }

        public Servico()
        { }

        public Servico BuscaServicoCodigo(int codServico)
        {
            return new Hcrp.Framework.Dal.Servico().BuscaServicoCodigo(codServico);
        }
    }
}
