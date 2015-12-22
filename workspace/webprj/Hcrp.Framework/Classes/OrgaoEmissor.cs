using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class OrgaoEmissor
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        public OrgaoEmissor()
        { }

        public List<Hcrp.Framework.Classes.OrgaoEmissor> BuscarOrgaoEmissor()
        {
            return new Hcrp.Framework.Dal.OrgaoEmissor().BuscarOrgaoEmissor();
        }

        public Hcrp.Framework.Classes.OrgaoEmissor BuscarOrgaoEmissorCodigo(int cod)
        {
            return new Hcrp.Framework.Dal.OrgaoEmissor().BuscarOrgaoEmissorCodigo(cod);       
        }
    }
}
    