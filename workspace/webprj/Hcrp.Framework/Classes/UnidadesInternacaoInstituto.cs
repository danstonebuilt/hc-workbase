using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class UnidadesInternacaoInstituto
    {
        public string Numero { get; set; }
        public string Nome { get; set; }

        public UnidadesInternacaoInstituto()
        { }

        public List<UnidadesInternacaoInstituto> BuscaUnidadesInternacaoInstituto(string CodInstituto)
        {
            return new Hcrp.Framework.Dal.UnidadesInternacaoInstituto().BuscaUnidadesInternacaoInstituto(CodInstituto);
        }

        public List<UnidadesInternacaoInstituto> BuscaAgrupamentoUnidadesInternacaoInstituto(string CodInstituto)
        {
            return new Hcrp.Framework.Dal.UnidadesInternacaoInstituto().BuscaAgrupamentoUnidadesInternacaoInstituto(CodInstituto);
        }

        public List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto> BuscaUnidades_Andar_Ala(string CodInstituto)
        {
            return new Hcrp.Framework.Dal.UnidadesInternacaoInstituto().BuscaUnidades_Andar_Ala(CodInstituto);
        }

        public List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto> BuscaEnfermaria_Andar_Ala(string CodInstituto, string NomeAndar)
        {
            return new Hcrp.Framework.Dal.UnidadesInternacaoInstituto().BuscaEnfermaria_Andar_Ala(CodInstituto, NomeAndar);
        }

        public List<Hcrp.Framework.Classes.UnidadesInternacaoInstituto> BuscaAgrupamentoVisualizacaoPainel(string CodInstituto, ref long NumSeqLocal_Enfermaria)
        {
            return new Hcrp.Framework.Dal.UnidadesInternacaoInstituto().BuscaAgrupamentoVisualizacaoPainel(CodInstituto, ref NumSeqLocal_Enfermaria);
        }

        
    }
}
