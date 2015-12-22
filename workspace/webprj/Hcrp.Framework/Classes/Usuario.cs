using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    [Serializable]
    public class Usuario
    {
        public int NumUserBanco { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string NomeAcesso { get; set; }
        public string Documento { get; set; }
        public ETipoDocumento TipoDocumento { get; set; }
        public Int16 IdfTipoUsuario { get; set; }

        public string NomeCompleto
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.Nome)) new Hcrp.Framework.Dal.Usuario().PreencherUsuario(this); 
                return this.Nome + " " + this.SobreNome;
            }
        }


        public Usuario()
        {
        }

        public Usuario BuscarUsuarioCodigo(Int32 numUserBanco)
        {
            return new Hcrp.Framework.Dal.Usuario().BuscarUsuarioCodigo(numUserBanco);
        }

        public enum ETipoDocumento
        {
            Outros = 0,
            CPF = 1
        }

    }
}
