using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Atendimento
    {
        public int SeqAtendimento { get; set; }

        public int SeqMovimentacaoPaciente { get; set; }
        
        public DateTime? DataAberturaAtendimento { get; set; }
        public DateTime? DataFechamentoAtendimento { get; set; }
        private Especialidade _Especialidade;

        public int _codEspecialidade { get; set; }
        private TipoAtendimento _TipoAtendimento;
        public int _codTipoAtendimento { get; set; }

        public Hcrp.Framework.Classes.TipoAtendimento TipoAtendimento
        {
            get
            {
                if (this._TipoAtendimento == null)
                    _TipoAtendimento = new Hcrp.Framework.Classes.TipoAtendimento().BuscarTipoAtendimentoCodigo(_codTipoAtendimento);
                return this._TipoAtendimento;
            }
            set { _TipoAtendimento = value; }
        }

        public Hcrp.Framework.Classes.MovimentacaoPaciente UltimaMovimentacaoPaciente
        {
            get
            {
                return new Hcrp.Framework.Classes.MovimentacaoPaciente().BuscaUltimaMovimentacaoPorAtendimento(this);
            }
        }

        public Hcrp.Framework.Classes.MovimentacaoPaciente UltimaMovimentacaoDoAtendimento
        {
            get
            {
                return new Hcrp.Framework.Classes.MovimentacaoPaciente().BuscaMovimentacaoDoAtendimento(this);
            }
        }
        
        //TIPO_ATENDIMENTO_HC

        public Hcrp.Framework.Classes.Atendimento BuscaAtendimentoEmAberto(Hcrp.Framework.Classes.Paciente paciente)
        {
            return new Hcrp.Framework.Dal.Atendimento().BuscaAtendimentoEmAberto(paciente);
        }

        public Hcrp.Framework.Classes.Atendimento BuscaAtendimentoPorCodigo(int seqAtendimento)
        {
            return new Hcrp.Framework.Dal.Atendimento().BuscaAtendimentoPorCodigo(seqAtendimento);
        }

        public Hcrp.Framework.Classes.Especialidade Especialidade
        {
            get
            {
                if (this._Especialidade == null)
                    _Especialidade = new Hcrp.Framework.Classes.Especialidade().BuscaEspecialidadeCodigo(_codEspecialidade);
                return this._Especialidade;
            }
            set { _Especialidade = value; }
        }


    }
}
