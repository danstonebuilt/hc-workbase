using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class GrauInstrucao
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string CodigoSus { get; set; }
        public bool Ativo { get; set; }

        public GrauInstrucao()
        { }

        public List<Hcrp.Framework.Classes.GrauInstrucao> BuscaGrauInstrucao()
        {
            return new Hcrp.Framework.Dal.GrauInstrucao().BuscaGrauInstrucao();
        }
        public Hcrp.Framework.Classes.GrauInstrucao BuscaGrauInstrucaoCodigo(int codigo)
        {
            return new Hcrp.Framework.Dal.GrauInstrucao().BuscaGrauInstrucaoCodigo(codigo);
        }
    }
}
