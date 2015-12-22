using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class AgendaPaciente
    {
        #region propriedades privadas
        private bool _estaHabilitadoCarregamentoDoBD { get; set; }
        private string _especialidadeServico { get; set; }
        private string _tipoConsultaProcedimento { get; set; }
        private Especialidade _especialidade { get; set; }
        private TipoConsulta _tipoConsulta { get; set; }
        private Servico _servico { get; set; }
        public ProcedimentoHc _procedimentoHc { get; set; }
        #endregion

        #region propriedades públicas
        public Int64 NumeroAgendaProcedimento { get; set; }
        public Usuario Usuario { get; set; }
        public int? CodigoSituacaoAtendimento { get; set; }
        public string _CodPaciente { get; set; }
        public int _CodEspecialidade { get; set; }
        public int _CodTipoConsulta { get; set; }
        public int _CodProfissional { get; set; }
        public int _CodInstituto { get; set; }
        public int _CodProcedimentoHc { get; set; }
        public int _CodServico { get; set; }
        public int _CodInstituicao { get; set; }
        public int _NumConfigAgenda { get; set; }
        public int _NumSeqLocal { get; set; }
        public int _CodPacienteSemRegistro { get; set; }
        public string TipoAgendamento { get; set; }
        public DateTime DataAgendamento { get; set; }

        public string EspecialidadeServico
        {
            get
            {
                if (this._estaHabilitadoCarregamentoDoBD == true)
                {
                    if (_CodEspecialidade > 0)
                        return this.Especialidade.Sigla + " - " + Especialidade.Nome;
                    else if (_CodServico > 0)
                        return this.Servico.Descricao;
                    else return "";
                }
                else
                {
                    return this._especialidadeServico;
                }
            }
            set
            {
                this._especialidadeServico = value;
            }
        }

        public string TipoConsultaProcedimento
        {
            get
            {
                if (this._estaHabilitadoCarregamentoDoBD == true)
                {
                    if (_CodTipoConsulta > 0)
                        return this.TipoConsulta.Descricao;
                    else if (_CodProcedimentoHc > 0)
                        return this.ProcedimentoHc.Descricao;
                    else return "";
                }
                else
                {
                    return this._tipoConsultaProcedimento;
                }
            }
            set
            {
                this._tipoConsultaProcedimento = value;
            }
        }

        public Paciente Paciente { get; set; }

        public Especialidade Especialidade
        {
            get
            {
                if (this._estaHabilitadoCarregamentoDoBD == true)
                {
                    return new Hcrp.Framework.Classes.Especialidade().BuscaEspecialidadeCodigo(this._CodEspecialidade);
                }
                else
                {
                    return this._especialidade;
                }
            }
            set
            {
                this._especialidade = value;
            }
        }

        public TipoConsulta TipoConsulta
        {
            get
            {
                if (this._estaHabilitadoCarregamentoDoBD == true)
                {
                    return new Hcrp.Framework.Classes.TipoConsulta().BuscaTipoConsultaCodigo(this._CodTipoConsulta);
                }
                else
                {
                    return this._tipoConsulta;
                }
            }
            set
            {
                this._tipoConsulta = value;
            }
        }

        public Profissional ProfissionalSolicitante { get; set; }
        public Instituto Instituto { get; set; }

        public Servico Servico
        {
            get
            {
                if (this._estaHabilitadoCarregamentoDoBD == true)
                {
                    return new Hcrp.Framework.Classes.Servico().BuscaServicoCodigo(this._CodServico);
                }
                else
                {
                    return this._servico;
                }
            }
            set
            {
                this._servico = value;
            }
        }

        public ProcedimentoHc ProcedimentoHc
        {
            get
            {
                if (this._estaHabilitadoCarregamentoDoBD == true)
                {
                    return new Hcrp.Framework.Classes.ProcedimentoHc().BuscaProcedimentoCodigo(this._CodProcedimentoHc);
                }
                else
                {
                    return this._procedimentoHc;
                }
            }
            set
            {
                this._procedimentoHc = value;
            }
        }

        public string Situacao { get; set; }
        #endregion

        #region construtores
        public AgendaPaciente()
        {
            this._estaHabilitadoCarregamentoDoBD = true;
        }

        /// <summary>
        /// Construtor PedidoAtendimento
        /// </summary>
        /// <param name="habilitarCarregamentoDoBD">
        /// Caso opte por (true) nesta opção, todas as propriedades de relacionamento irão carregar os objetos diretamente do banco de dados.
        /// Não é recomendado utilizar esta opção se for carregada uma lista de PedidoAtendimento, várias chamadas ao banco podem comprometer a perfomance do aplicativo.
        /// </param>
        public AgendaPaciente(bool habilitarCarregamentoDoBD)
        {
            this._estaHabilitadoCarregamentoDoBD = habilitarCarregamentoDoBD;
        }

        #endregion

        #region métodos
        public List<Hcrp.Framework.Classes.AgendaPaciente> BuscaAgendaPaciente(String codPaciente)
        {
            return new Hcrp.Framework.Dal.AgendaPaciente().BuscaAgendaPaciente(codPaciente);
        }
        #endregion
    }
}
