using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ImpressoraRede
    {
        public long SeqImpressoraRede { get; set; }
        public string EnderecoIP { get; set; }
        public string NomeMaquina { get; set; }
        public string NomeImpressora { get; set; }
        public string NomeConcatenado { get { return string.Concat(this.NomeMaquina, "\\", this.NomeImpressora); } }

        /// <summary>
        /// Obter lista de impressoras na rede
        /// </summary>
        /// <returns></returns>
        public List<Classes.ImpressoraRede> ObterListaDeImpressoraNaRede()
        {
            return new Dal.ImpressoraRede().ObterListaDeImpressoraNaRede();
        }
    }
}
