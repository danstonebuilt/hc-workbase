using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.Entity
{
    public class Lacre_reposit_complemento
    {
        public Int64 SEQ_REPOSITORIO_COMPLEMENTO { get; set; }

        public string DSC_COMPLEMENTO { get; set; }

        public string NUM_LACRE { get; set; }

        public Int64 SEQ_LACRE_REPOSITORIO { get; set; }

        public DateTime? DTA_HOR_CADASTRO { get; set; }

        public Int64 NUM_USER_CADASTRO { get; set; }

        public string IDF_ATIVO { get; set; }
    }
}
