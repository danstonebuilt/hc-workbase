using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ParametroMonitorizacao
    {

        public int Codigo;
        public String Descricao;
        public double ValorMinimo;
        public double ValorMaximo;
        public Boolean Ativo;

        public enum EParametroMonitorizacao
        {
            Altura = 12,
            Peso = 11,
            //
            Fugulin = 153,
            ScorePQU = 154,
            ScoreBinomio = 155,
            ScoreDini = 156,
            NAS = 183,
            Braden = 158,
            BradenQ = 250,
            SCIM = 254
        }

        public ParametroMonitorizacao BuscarParametroMonitorizacaoCodigo(EParametroMonitorizacao paramMonit)
        {
            return BuscarParametroMonitorizacaoCodigo((int)paramMonit);
        }

        public ParametroMonitorizacao BuscarParametroMonitorizacaoCodigo(int codigo)
        {
            return new Hcrp.Framework.Dal.ParametroMonitorizacao().BuscarParametroMonitorizacaoCodigo(codigo);
        }
    }
}
