using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.Entity
{
    public class ListaControle
    {
        #region Propriedades

        public Int64 SeqListaControle { get; set; }
        public string NomeListaControle { get; set; }
        /// <summary>
        /// Ativo S - SIM | N - Não
        /// </summary>
        public string IdfAtivo { get; set; }

        public int IdfCaixaIntubacao { get; set; }
        public Instituto Instituto { get; set; }
        public DateTime DataCadastro { get; set; }
        public Usuario UsuarioCadastro { get; set; }
        public DateTime? DataUltimaAlteracao { get; set; }
        public Usuario UsuarioUltimaAlteracao { get; set; }       

        #endregion
    }
}
