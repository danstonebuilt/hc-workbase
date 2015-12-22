using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.Entity
{
    public class ItensListaControle
    {
        #region Propriedades

        public Int64? SeqItensListaControle { get; set; }

        public ListaControle ListaControle { get; set; }

        public Material Material { get; set; }

        public int? QuantidadeNecessaria { get; set; }

        public string DescricaoMaterial { get; set; }

        public Unidade Unidade { get; set; }

        public Alinea Alinea { get; set; }

        public TipoBem TipoBem { get; set; }

        public DateTime? DataCadastro { get; set; }

        public Usuario UsuarioCadastro { get; set; }

        public string IdfAtivo { get; set; }

        #endregion
    }
}
