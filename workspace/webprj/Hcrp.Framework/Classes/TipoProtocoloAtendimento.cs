using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class TipoProtocoloAtendimento
    {
        public int Seq { get; set; }
        public string Nome { get; set; }
        public string FormularioSolicitacao { get; set; }
        public string FormularioVisualizacao { get; set; }
        public bool Ativo { get; set; }
        public string IdfSexo { get; set; }
        public Int16? ValorIdadeInicial { get; set; }
        public Int16? ValorIdadeFinal { get; set; }

        public TipoProtocoloAtendimento()
        { }

        public TipoProtocoloAtendimento BuscarTipoProtocoloAtendimentoCodigo(int codigo)
        {
            return new Hcrp.Framework.Dal.TipoProtocoloAtendimento().BuscarTipoProtocoloAtendimentoCodigo(codigo);
        }

    }
}
