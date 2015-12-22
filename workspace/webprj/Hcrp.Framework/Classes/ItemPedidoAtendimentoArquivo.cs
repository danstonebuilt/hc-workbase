using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ItemPedidoAtendimentoArquivo
    {
        public int Codigo { get; set; }
        public long CodigoItemPedidoAtendimento { get; set; }
        public byte[] Arquivo { get; set; }
        public string ExtensaoArquivo { get; set; }
        public string NomeOriginalArquivo { get; set; }
    }
}
