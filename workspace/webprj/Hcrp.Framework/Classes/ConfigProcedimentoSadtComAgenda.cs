using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ConfigProcedimentoSadtComAgenda
    {
        public int _CodProcedimento { get; set; }
        public int _CodDir { get; set; }
        public string _SglPais { get; set; }
        public string _SglUf { get; set; }
        public string _CodLocalidade { get; set; }
        public int _CodInstSolicitante { get; set; }
        public int _CodEspecTriagem { get; set; }
        public int _CodInstSistemaTri { get; set; }

        public string IdfSexo { get; set; }
        public int? ValorIdadeInicial { get; set; }
        public int? ValorIdadeFinal { get; set; }

        public int Seq { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime DataFinalVigencia { get; set; }
        public string FormularioSolicitacao { get; set; }
        public string FormularioVisualizacao { get; set; }
        
        public Hcrp.Framework.Classes.ProcedimentoHc ProcedimentoHc {
            get {
                return new Hcrp.Framework.Classes.ProcedimentoHc().BuscaProcedimentoCodigo(_CodProcedimento);
            }            
        }
        public Hcrp.Framework.Classes.Drs Drs {
            get {
                return new Hcrp.Framework.Dal.Drs().BuscarDrsCodigo(_CodDir);
            }
        }
        public Hcrp.Framework.Classes.Municipio Municipio { get; set; }
        public Hcrp.Framework.Classes.Instituicao InstituicaoSolicitant { get; set; }
        public Hcrp.Framework.Classes.Especialidade EspecialidadeTriagem { get; set; }
        public Hcrp.Framework.Classes.Instituicao IntituicaoTriagem { get; set; }

        public ConfigProcedimentoSadtComAgenda() { }

        public List<Hcrp.Framework.Classes.ConfigProcedimentoSadtComAgenda> BuscaConfiguracoesVigentes(int CodServicoSadt, int CodDir, string SglPais, string SglUf, string CodLocalidade, int CodInstSolicitante) 
        {
            return new Hcrp.Framework.Dal.ConfigProcedimentoSadtComAgenda().BuscaConfiguracoesVigentes(CodServicoSadt, CodDir, SglPais, SglUf, CodLocalidade, CodInstSolicitante);
        }
        public string BuscaFormularioSolicitacao(int seq)
        {
            return new Hcrp.Framework.Dal.ConfigProcedimentoSadtComAgenda().BuscaFormularioSolicitacao(seq);
        }
        public ConfigProcedimentoSadtComAgenda BuscaConfigCodigo(int cod)
        { 
            return new Dal.ConfigProcedimentoSadtComAgenda().BuscaConfigCodigo(cod);
        }
    }
}
