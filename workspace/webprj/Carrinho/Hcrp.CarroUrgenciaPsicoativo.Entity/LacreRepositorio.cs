using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.Entity
{
    public class LacreRepositorio
    {
        #region Propriedades
        
        public Int64 SeqLacreRepositorio { get; set; }        
        public RepositorioListaControle RepositorioListaControle { get; set; }
        public Usuario UsuarioCadastro { get; set; }
        public Usuario UsuarioUltimaAlteracao { get; set; }
	    public DateTime? DataCadastro { get; set; }
        public DateTime? DataHoraUltimaAlteracao { get; set; }
        public Int64? NumLacre { get; set; }
        public string NumCaixaIntubacao { get; set; }
        public TipoSituacaoHc TipoSituacaoHc { get; set; }
        public LacreTipoOcorrencia LacreTipoOcorrencia { get; set; }
        public DateTime? DataLacracao { get; set; }
        public Usuario UsuarioResponsavelLacracao { get; set; }
        public DateTime? DataDaSituacao { get; set; }
        public string ExisteLancamentoDeMaterial { get; set; }

        #endregion
    }
}
