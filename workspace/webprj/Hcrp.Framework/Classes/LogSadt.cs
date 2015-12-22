using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class LogSadt
    {
        public int _NumUsuario { get; set; }
        public int _CodSituacao { get; set; }

        public string Operacao { get; set; }
        public DateTime DataLog { get; set; }
        public string Chave { get; set; }
        public string Tabela { get; set; }
        public string Coluna { get; set; }
        public Usuario UsuarioLog
        {
            get
            {
                if (this._NumUsuario > 0)
                    return new Hcrp.Framework.Classes.Usuario().BuscarUsuarioCodigo(this._NumUsuario);
                else return null;
            }
        }
        public SituacaoHc Situacao
        {
            get
            {
                if (this._CodSituacao > 0)
                    return new Hcrp.Framework.Classes.SituacaoHc().BuscarSituacaoCodigo(this._CodSituacao);
                else return null;
            }
        }

        public LogSadt()
        { }

        public List<Hcrp.Framework.Classes.LogSadt> BuscaLogs(int seqItemAtendimento)
        {
            return new Hcrp.Framework.Dal.LogSadt().BuscaLogs(seqItemAtendimento);       
        }

    }
}