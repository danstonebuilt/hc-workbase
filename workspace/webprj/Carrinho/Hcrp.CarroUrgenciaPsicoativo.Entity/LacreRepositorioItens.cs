using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.Entity
{
    public class LacreRepositorioItens
    {
        #region Propriedades
        
        public Int64 SeqLacreRepositorioItens { get; set; }
        public LacreRepositorio LacreRepositorio { get; set; }                
        public Int32 QtdDisponivel { get; set; }
        public Material Material { get; set; }
        public Lote Lote { get; set; }
        public ItensListaControle ItensListaControle { get; set; }
        public string NumLoteFabricante { get; set; }
        public DateTime? DataValidadeLote { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataUltimaAlteracao { get; set; }
        public Usuario UsuarioCadastro { get; set; }
        public Usuario UsuarioUltimaAlteracao { get; set; }
        public Int32 QtdUtilizada { get; set; }
        public string DscJustificativaConsumoSemAtendimento { get; set; }
        public Int32 QtdDisponivelInserida { get; set; }
        public Int32 QtdVencendo { get; set; }

        #endregion
    }

    public class QuantidadeRegistroConsumoSaida
    {
        #region Propriedades

        public Int64 SeqLacreRepositorioItens { get; set; }
        public int QtdDisponivel { get; set; }
        public int QtdDisponivelComTodoAtendimento { get; set; }
        public int QtdUtilizadaComAtendimento { get; set; }

        #endregion
    }
}
