using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class MapeamentoLocal
    {
        public int _CodInstituto { get; set; }
        public int Seq { get; set; }
        public bool Ativo { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public Hcrp.Framework.Classes.Instituto Instituto {
            get {
                return new Hcrp.Framework.Dal.Instituto().BuscarInstitutoCodigo(_CodInstituto);
            }
        }

        public MapeamentoLocal()
        { }

        public Hcrp.Framework.Classes.MapeamentoLocal BuscarLocalCodigo(int codLocal) {
            return new Hcrp.Framework.Dal.MapeamentoLocal().BuscarLocalCodigo(codLocal);
        }
    }
}
