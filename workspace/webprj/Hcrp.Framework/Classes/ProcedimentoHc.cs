using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ProcedimentoHc
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public string _CodMaterial { get; set; }
        public string _CodCC { get; set; }
        public DateTime MatCCDataCadastro;
        public string MatCCUsuarioCad;
        public DateTime MatCCDataExc;
        public string MatCCUsuarioExc;
        public int MatCCSeq;

        public Hcrp.Framework.Classes.Material Material
        {
            get
            {
                return new Hcrp.Framework.Dal.Material().BuscaMaterialCodigo(_CodMaterial);
            }
        }
        public Hcrp.Framework.Classes.CentroDeCusto CentroCusto
        {
            get
            {
                return new Hcrp.Framework.Dal.CentroDeCusto().BuscaCentroCustoCodigo(_CodCC);

            }
        }

        public ProcedimentoHc()
        { }

        public ProcedimentoHc BuscaProcedimentoCodigo(int codProcedimento)
        {
            return new Hcrp.Framework.Dal.ProcedimentoHc().BuscaProcedimentoCodigo(codProcedimento);
        }

        public double InserirProcedMatCC(Framework.Classes.ProcedimentoHc Proc)
        {
            return new Hcrp.Framework.Dal.ProcedimentoHc().InserirProcedMatCC(Proc);
        }

        public List<Hcrp.Framework.Classes.ProcedimentoHc> BuscaProcedimentoMatCC(string CodMaterial, string CodCenCusto)
        {
            return new Hcrp.Framework.Dal.ProcedimentoHc().BuscaProcedimentoMatCC(CodMaterial, CodCenCusto);
        }

        public double AlterarProcedMatCC(Framework.Classes.ProcedimentoHc Proc)
        {
            return new Hcrp.Framework.Dal.ProcedimentoHc().AlterarProcedMatCC(Proc);
        }

        public List<Hcrp.Framework.Classes.ProcedimentoHc> ObterProcedimentoHCEmVigencia()
        {
            return new Hcrp.Framework.Dal.ProcedimentoHc().ObterProcedimentoHCEmVigencia();
        }

        /// <summary>
        /// Método utilizado para buscar os institutos pelos filtros informados
        /// </summary>
        /// <param name="codigoInstituicao">Código da instituição</param>
        /// <param name="codigoProcedimento">Código do procedimento</param>
        /// <param name="descricaoProcedimento">Descrição do procedimento</param>
        /// <returns>Procedimentos listados</returns>
        public List<Hcrp.Framework.Classes.ProcedimentoHc> ObterProcedimentoHC(Int32? codigoInstituicao, Int32? codigoProcedimento, String descricaoProcedimento)
        {
            return new Hcrp.Framework.Dal.ProcedimentoHc().ObterProcedimentoHC(codigoInstituicao, codigoProcedimento, descricaoProcedimento);
        }

    }
}
