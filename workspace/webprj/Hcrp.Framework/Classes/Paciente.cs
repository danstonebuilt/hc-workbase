using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Hcrp.Framework.Classes
{
    public class Paciente
    {
        #region Membros Privados

        private bool _estaHabilitadoCarregamentoDoBD { get; set; }
        private Hcrp.Framework.Classes.GrauInstrucao _grauInstrucaoPaciente { get; set; }
        private Hcrp.Framework.Classes.Municipio _municipio { get; set; }

        #endregion

        #region Construtor

        /// <summary> 
        /// Construtor PedidoAtendimento
        /// </summary>
        public Paciente()
        {

        }

        /// <summary>
        /// Construtor PedidoAtendimento
        /// </summary>
        /// <param name="habilitarCarregamentoDoBD">
        /// Caso opte por (true) nesta opção, todas as propriedades de relacionamento irão carregar os objetos diretamente do banco de dados.
        /// Não é recomendado utilizar esta opção se for carregada uma lista de PedidoAtendimento, várias chamadas ao banco podem comprometer a perfomance do aplicativo.
        /// </param>
        public Paciente(bool habilitarCarregamentoDoBD)
        {
            _estaHabilitadoCarregamentoDoBD = habilitarCarregamentoDoBD;
        }

        #endregion

        #region Membros Públicos

        public int _codGrauInstrucao { get; set; }
        public string _chaveMunicipio { get; set; }

        #endregion

        public enum ECorPaciente
        {
            [DescriptionAttribute("Branca")]
            Branca = 1,

            [DescriptionAttribute("Preta")]
            Preta = 2,

            [DescriptionAttribute("Amarela")]
            Amarela = 3,

            [DescriptionAttribute("Vermelha")]
            Vermelha = 4,

            [DescriptionAttribute("Mulato")]
            Mulato = 5
        }

        #region Propriedades

        public string RegistroPaciente { get; set; }

        public string NomePaciente { get; set; }

        public string SobrenomePaciente { get; set; }

        public string NomeCompletoPaciente
        {
            get
            {
                return this.NomePaciente + " " + this.SobrenomePaciente;
            }
        }

        public string SexoPaciente { get; set; }

        public DateTime DataNascimento { get; set; }

        public string MaePaciente { get; set; }

        public string Idade { get; set; }

        public ECorPaciente Cor { get; set; }

        public Hcrp.Framework.Classes.Atendimento AtendimentoEmAberto
        {
            get
            {
                return new Hcrp.Framework.Classes.Atendimento().BuscaAtendimentoEmAberto(this);
            }
        }
        
        public Hcrp.Framework.Classes.Atendimento AtendimentoEmUso { get; set; }

        public string NumeroCNS { get; set; }

        public string CPFPaciente { get; set; }

        public string RGPaciente { get; set; }

        public int CodOrgaoEmissor { get; set; }

        public string IdfUfRG { get; set; }

        public string PaiPaciente { get; set; }

        public string NomeCidade { get; set; }

        public string TelefoneContato { get; set; }

        public string TelefoneCelular { get; set; }

        public string DescricaoEndereco { get; set; }

        public string NumeroEndereco { get; set; }

        public string ComplementoEndereco { get; set; }

        public string NomeBairro { get; set; }

        public string UFEndereco { get; set; }

        public string CepEndereco { get; set; }

        public string SiglaPais { get; set; }

        public string CodigoLocalidadeEndereco { get; set; }

        public string TipoLogradouro { get; set; }

        public string Email { get; set; }

        public Hcrp.Framework.Classes.GrauInstrucao GrauInstrucaoPaciente
        {
            get
            {
                try
                {
                    if (this._estaHabilitadoCarregamentoDoBD)
                    {
                        if (_grauInstrucaoPaciente == null)
                        {
                            if (this._codGrauInstrucao > 0)
                            {
                                _grauInstrucaoPaciente = new Hcrp.Framework.Classes.GrauInstrucao().BuscaGrauInstrucaoCodigo(_codGrauInstrucao);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    else
                    {
                        if (_grauInstrucaoPaciente == null)
                        {
                            _grauInstrucaoPaciente = new GrauInstrucao();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return _grauInstrucaoPaciente;
            }
            set
            {
                _grauInstrucaoPaciente = value;
            }
        }

        public string EstadoCivil { get; set; }

        public string Profissao { get; set; }

        public DateTime? DataHoraObito { get; set; }

        /// <summary>
        /// Municipio
        /// </summary>
        public Hcrp.Framework.Classes.Municipio Municipio
        {
            get
            {
                try
                {
                    if (this._estaHabilitadoCarregamentoDoBD)
                    {
                        if (_municipio == null)
                        {
                            if (!string.IsNullOrWhiteSpace(_chaveMunicipio))
                            {
                                _municipio = new Hcrp.Framework.Classes.Municipio().BuscaMunicipiosChave(_chaveMunicipio);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    else
                    {
                        if (_municipio == null)
                        {
                            _municipio = new Municipio();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return _municipio;
            }
            set
            {
                _municipio = value;
            }
        }

        #endregion

        #region Métodos

        public Hcrp.Framework.Classes.Paciente BuscarPacienteRegistro(String codigoPaciente)
        {
            return new Hcrp.Framework.Dal.Paciente().BuscarPacienteRegistro(codigoPaciente);
        }

        public List<Hcrp.Framework.Classes.Paciente> BuscaPacientesEspecialidade(Hcrp.Framework.Classes.Especialidade especialidade)
        {
            return new Hcrp.Framework.Dal.Paciente().BuscaPacientesEspecialidade(especialidade);
        }

        public List<Framework.Classes.Paciente> BuscarPacientes(String codigoPaciente, String nomePaciente, String sobrenomePaciente, Boolean pesquisaFonetica)
        {
            return new Hcrp.Framework.Dal.Paciente().BuscarPacientes(codigoPaciente, nomePaciente, sobrenomePaciente, pesquisaFonetica);

        }

        public List<Hcrp.Framework.Classes.Paciente> ObterDadosAtendimentoAtual(Hcrp.Framework.Classes.Especialidade especialidade)
        {
            return new Hcrp.Framework.Dal.Paciente().ObterDadosAtendimentoAtual(especialidade);
        }

        /// <summary>
        /// Realiza a busca pelo valor padrao de medicamentos para o paciente
        /// </summary>
        /// <param name="codigoPaciente">Codigo do paciente</param>
        /// <param name="dataAtendimento">Data em que o atendimento medico esta sendo efetuado</param>
        /// <param name="mostrarPosologia">Flag que indica se o resultado deve mostrar posologia</param>
        /// <param name="formatarHtml">Flag que indica se o resultado deve estar formatado em HTML</param>
        /// <returns>Texto com valor padrao de medicamentos para o paciente</returns>
        public string ObterChecoMedicamentoPadrao(string codigoPaciente, DateTime dataAtendimento, bool? mostrarPosologia, bool? formatarHtml)
        {
            return new Dal.Paciente().ObterChecoMedicamentoPadrao(codigoPaciente, dataAtendimento, mostrarPosologia, formatarHtml);
        }


        /// <summary>
        /// Realiza a busca pelo valor padrao de exames para o paciente
        /// </summary>
        /// <param name="codigoPaciente">Codigo do paciente</param>
        /// <returns>Texto com valor padrao de exame para o paciente</returns>
        public string ObterChecoExamePadrao(string codigoPaciente)
        {
            return new Dal.Paciente().ObterChecoExamePadrao(codigoPaciente);
        }


        #endregion
    }
}

