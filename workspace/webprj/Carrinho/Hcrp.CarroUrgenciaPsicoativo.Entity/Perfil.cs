using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.Entity
{
    [Serializable]
    public class Perfil
    {
        public Int64 IdPerfil { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Int64? IdPerfilPai { get; set; }
        public string IdfRepassaDiretos { get; set; }
        public string IdfInformaCentroDeCusto { get; set; }
        public string Resumo { get; set; }
        public string IdfPerfilGerencial { get; set; }
        public string IdfAtivo { get; set; }
        public long IdInstituicao { get; set; }
        public Int32 Level { get; set; }

        public Perfil()
        { }

    }
}
