using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.Entity
{
    public class Lote 
    {
        #region Propriedades

        public Int64 NumLote { get; set; }
        public Material Material { get; set; }
        public string NumLoteFabricante { get; set; }
        public DateTime DataValidadeLote { get; set; }
        public Int32 QtdLote { get; set; }
        public Int64 SeqNotaFiscal { get; set; }
        public Int64? SeqLote { get; set; }

        //public string DescricaoLoteFormatada 
        //{
        //    get
        //    {
        //        return string.Format("Nº Lote {0} - Qtd {1} - Validade {2}", this.NumLote, this.QtdLote, this.DataValidadeLote.ToString("dd/MM/yyyy"));
        //    }
        //}

        #endregion

        
    }
}
