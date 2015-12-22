using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ParametroMonitorizacaoPaciente
    {
        public int Codigo { get; set; }

        private ParametroMonitorizacao _ParametroMonitorizacao;
        public int _seqParametroMonitorizacao { get; set; }
        public Hcrp.Framework.Classes.ParametroMonitorizacao ParametroMonitorizacao
        {
            get
            {
                if (this._ParametroMonitorizacao == null)
                    _ParametroMonitorizacao = new Hcrp.Framework.Classes.ParametroMonitorizacao().BuscarParametroMonitorizacaoCodigo(_seqParametroMonitorizacao);
                return this._ParametroMonitorizacao;
            }
            set { _ParametroMonitorizacao = value; }
        }

        public Double valor { get; set; }
        public DateTime DataHora { get; set; }

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

        private Atendimento _Atendimento;
        public int _seqAtendimento { get; set; }
        public Hcrp.Framework.Classes.Atendimento Atendimento
        {
            get
            {
                if (this._Atendimento == null)
                    _Atendimento = new Hcrp.Framework.Classes.Atendimento().BuscaAtendimentoPorCodigo(_seqAtendimento);
                return this._Atendimento;
            }
            set { _Atendimento = value; }
        }

        private Usuario _Usuario;
        public int _numUserBanco { get; set; }
        public Hcrp.Framework.Classes.Usuario Usuario
        {
            get
            {
                if (this._Usuario == null)
                    _Usuario = new Hcrp.Framework.Classes.Usuario().BuscarUsuarioCodigo(_numUserBanco);
                return this._Usuario;
            }
            set { _Usuario = value; }
        }


        public Hcrp.Framework.Classes.ParametroMonitorizacao BuscarParametroMonitorizacaoCodigo(int seqParametroMonitorizacao) {
            return new Hcrp.Framework.Dal.ParametroMonitorizacao().BuscarParametroMonitorizacaoCodigo(seqParametroMonitorizacao);
        }

        public List<Classes.ParametroMonitorizacaoPaciente> BuscaMonitorizacaoPaciente(Classes.Paciente paciente, Classes.Atendimento atendimento, Classes.ParametroMonitorizacao parametroMonitorizacao, DateTime dataInicial, DateTime dataFinal)
        {
            return new Hcrp.Framework.Dal.ParametroMonitorizacaoPaciente().BuscaMonitorizacaoPaciente(paciente, atendimento, parametroMonitorizacao, dataInicial, dataFinal);
        }

        public List<ParametroMonitorizacaoPaciente> BuscaMonitorizacaoPacienteRecente(Paciente paciente)
        {
            return new Hcrp.Framework.Dal.ParametroMonitorizacaoPaciente().BuscaMonitorizacaoPacienteRecente(paciente);
        }

        public ParametroMonitorizacaoPaciente ProcuraValor(List<ParametroMonitorizacaoPaciente> l, ParametroMonitorizacao.EParametroMonitorizacao eParametroMonitorizacao)
        {
            List<Classes.ParametroMonitorizacaoPaciente> lfiltrado = l.FindAll(delegate(Hcrp.Framework.Classes.ParametroMonitorizacaoPaciente p) { return ((ParametroMonitorizacao.EParametroMonitorizacao)p._seqParametroMonitorizacao).Equals(eParametroMonitorizacao); });
            if (lfiltrado.Count > 0)
                return lfiltrado[0];
            else
                return null;
        }

        public void Incluir()
        {
            this.Codigo = new Hcrp.Framework.Dal.ParametroMonitorizacaoPaciente().IncluirParametroMonitorizacaoPaciente(this);
        }

        /// <summary>
        /// Realiza a busca dos valores padroes de monitorizacao, sendo eles: valor minimo, valor maximo e valor atual.
        /// </summary>
        /// <param name="codigoAtendimento">Codigo do atendimento para identificacao dos valores padroes</param>
        /// <returns>Lista com valores padroes de monitorizacao</returns>
        public List<Classes.ParametroMonitorizacaoPaciente> BuscarMonitorizacaoPadrao(long codigoAtendimento)
        {
            return new Dal.ParametroMonitorizacaoPaciente().BuscarMonitorizacaoPadrao(codigoAtendimento);
        }

    }
}
