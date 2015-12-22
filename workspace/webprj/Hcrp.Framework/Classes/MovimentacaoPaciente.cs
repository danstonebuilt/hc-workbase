using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class MovimentacaoPaciente
    {
        public int Codigo { get; set; }

        private MapeamentoLocal _MapeamentoLocal; 
        public int _numSeqLocal { get; set; }

        public Hcrp.Framework.Classes.MapeamentoLocal MapeamentoLocal
        {
            get
            {
                if (this._MapeamentoLocal == null)
                    _MapeamentoLocal = new Hcrp.Framework.Classes.MapeamentoLocal().BuscarLocalCodigo(_numSeqLocal);
                return this._MapeamentoLocal;
            }
            set { _MapeamentoLocal = value; }
        }

        internal MovimentacaoPaciente BuscaUltimaMovimentacaoPorAtendimento(Atendimento atendimento)
        {
            return new Hcrp.Framework.Dal.MovimentacaoPaciente().BuscaUltimaMovimentacaoPorAtendimento(atendimento);
        }

        public MovimentacaoPaciente BuscaMovimentacaoDoAtendimento(Hcrp.Framework.Classes.Atendimento Atendimento)
        {
            return new Hcrp.Framework.Dal.MovimentacaoPaciente().BuscaMovimentacaoDoAtendimento(Atendimento);
        }
    }
}
