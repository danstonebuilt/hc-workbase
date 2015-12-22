using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.Entity
{
    public class RepositorioListaControle
    {
        #region Propriedades

        public Int64 SeqRepositorio { get; set; }
        public string DscIdentificacao { get; set; }
        public ListaControle ListaControle  { get; set; }
        public TipoRepositorioListaControle TipoRepositorioListaControle { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataUltimaAlteracao { get; set; }
        public Usuario UsuarioCadastro { get; set; }
        public Usuario UsuarioUltimaAlteracao { get; set; }        
        public string IdfAtivo { get; set; }
        public BemPatrimonial BemPatrimonial { get; set; }

        public List<Entity.RepositorioCentroCusto> listRepositorioCentroCusto { get; set; }
        #endregion
    }
}
