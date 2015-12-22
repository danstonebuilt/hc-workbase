using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Hcrp.Framework.Classes
{    
    /// <summary>
    /// Entidade pedido atendimento
    /// </summary>
    public class PedidoAtendimento
    {
        #region Membros Privados

        private bool _estaHabilitadoCarregamentoDoBD { get; set; }
        private Classes.TipoProtocoloAtendimento _tipoProtocoloAtendimento { get; set; }
        private Hcrp.Framework.Classes.Municipio _municipioPedido { get; set; }
        private Hcrp.Framework.Classes.Municipio _municipio { get; set; }
        private Hcrp.Framework.Classes.ServicoSadt _servicoSadt { get; set; }
        private Hcrp.Framework.Classes.Logradouro  _logradouro { get; set; }
        private Hcrp.Framework.Classes.Instituicao _postoMedico { get; set; }
        private Hcrp.Framework.Classes.Profissional _medicoSolicitante { get; set; }
        private Hcrp.Framework.Classes.GrauInstrucao _grauInstrucaoPaciente { get; set; }
        private MotivoEncaminhamento _motivoEncaminhamento  { get; set; }
        
        #endregion

        #region Membros Públicos

        public int _codGrauInstrucao { get; set; }
        public string _idfUfRg { get; set; }
        public string _codTipoLogradouro { get; set; }
        public int _codServicoSadt { get; set; }
        public string _chaveMunicipio { get; set; }
        public string _chaveMunicipioPedido { get; set; }
        public int _codProcedimentoHc { get; set; }
        public int _codPostoMedico { get; set; }
        public string _CnesPostoMedico { get; set; }
        public int _codMedicoSolicitante { get; set; }
        public int _codMotivoEncaminhamento { get; set; }
        public int _CodProtocoloAtendimento { get; set; }   
        public string _NomeMunicipioPedido { get; set; }
        public Hcrp.Framework.Classes.Instituicao _Instituicao { get; set; }
        public string _NomeFormularioVisualizacao { get; set; }
        public int _CodDir { get; set; }

        #endregion

        #region Construtor

        /// <summary> 
        /// Construtor PedidoAtendimento
        /// </summary>
        public PedidoAtendimento()
        {

        }

        /// <summary>
        /// Construtor PedidoAtendimento
        /// </summary>
        /// <param name="habilitarCarregamentoDoBD">
        /// Caso opte por (true) nesta opção, todas as propriedades de relacionamento irão carregar os objetos diretamente do banco de dados.
        /// Não é recomendado utilizar esta opção se for carregada uma lista de PedidoAtendimento, várias chamadas ao banco podem comprometer a perfomance do aplicativo.
        /// </param>
        public PedidoAtendimento(bool habilitarCarregamentoDoBD)
        {
            _estaHabilitadoCarregamentoDoBD = habilitarCarregamentoDoBD;
        }

        #endregion

        #region Propriedades

        /// <summary>
        /// Seq da entidade
        /// </summary>
        public int Seq { get; set; }

        /// <summary>
        /// Tipo do pedido
        /// </summary>
        public ETipoPedido TipoPedido { get; set; }

        /// <summary>
        /// Tipo do protocolo de atedimento
        /// </summary>
        public Classes.TipoProtocoloAtendimento TipoProtocoloAtendimento
        {
            get
            {
                try
                {
                    if (this._estaHabilitadoCarregamentoDoBD)
                    {
                        if (_tipoProtocoloAtendimento == null)
                        {
                            if (_CodProtocoloAtendimento > 0)
                            {
                                _tipoProtocoloAtendimento = new Classes.TipoProtocoloAtendimento().BuscarTipoProtocoloAtendimentoCodigo(_CodProtocoloAtendimento);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    else
                    {
                        if (_tipoProtocoloAtendimento == null)
                        {
                            _tipoProtocoloAtendimento =  new TipoProtocoloAtendimento();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return _tipoProtocoloAtendimento;
            }
            set
            {
                _tipoProtocoloAtendimento = value;
            }
        }

        /// <summary>
        /// Municipio do pedido
        /// </summary>
        public Hcrp.Framework.Classes.Municipio MunicipioPedido
        {
            get
            {
                try
                {
                    if (this._estaHabilitadoCarregamentoDoBD)
                    {
                        if (_municipioPedido == null)
                        {
                            if (!string.IsNullOrWhiteSpace(_chaveMunicipioPedido))
                            {
                                _municipioPedido = new Hcrp.Framework.Classes.Municipio().BuscaMunicipiosChave(_chaveMunicipioPedido);
                            }
                            else
                            {
                                return null;
                                //throw new Exception("A propriedade _chaveMunicipioPedido deve ser informada.");
                            }
                        }
                    }
                    else
                    {
                        if (_municipioPedido == null)
                        {
                            _municipioPedido = new Municipio();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return _municipioPedido;
            }
            set
            {
                _municipioPedido = value;
            }
        }

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

        /// <summary>
        /// Serviço SADT
        /// </summary>
        public Hcrp.Framework.Classes.ServicoSadt ServicoSadt
        {
            get
            {
                try
                {
                    if (this._estaHabilitadoCarregamentoDoBD)
                    {
                        if (_servicoSadt == null)
                        {
                            if (this.Seq > 0)
                            {
                                _servicoSadt = new Hcrp.Framework.Classes.ServicoSadt().BuscaServicoSadtPedAtendimento(Seq);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    else
                    {
                        if (_servicoSadt == null)
                        {
                            _servicoSadt = new ServicoSadt();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return _servicoSadt;
            }
            set
            {
                _servicoSadt = value;
            }
        }

        /// <summary>
        /// Usuário solicitante
        /// </summary>
        public Hcrp.Framework.Classes.Usuario UsuarioSolicitante { get; set; }

        /// <summary>
        /// Logradouro
        /// </summary>
        public Hcrp.Framework.Classes.Logradouro Logradouro
        {
            get
            {
                try
                {
                    if (this._estaHabilitadoCarregamentoDoBD)
                    {
                        if (_logradouro == null)
                        {
                            if (!string.IsNullOrWhiteSpace(_codTipoLogradouro))
                            {
                                _logradouro = new Hcrp.Framework.Classes.Logradouro().BuscaLogradouroCodigo(_codTipoLogradouro);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    else
                    {
                        if (_logradouro == null)
                        {
                            _logradouro = new Logradouro();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return _logradouro;
            }
            set
            {
                _logradouro = value;
            }
        }

        /// <summary>
        /// Posto médico
        /// </summary>
        public Hcrp.Framework.Classes.Instituicao PostoMedico
        {
            get
            {
                try
                {
                    if (this._estaHabilitadoCarregamentoDoBD)
                    {
                        if (_postoMedico == null)
                        {
                            if (this._codPostoMedico > 0)
                            {
                                _postoMedico = new Hcrp.Framework.Classes.Instituicao().BuscaInstituicaoCodigo(Convert.ToString(this._codPostoMedico));
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    else
                    {
                        if (_postoMedico == null)
                        {
                            _postoMedico = new Instituicao();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return _postoMedico;
            }
            set
            {
                _postoMedico = value;
            }
        }

        /// <summary>
        /// Código de registro do paciente
        /// </summary>
        public string RegistroPaciente { get; set; }

        /// <summary>
        /// Nome do paciente
        /// </summary>
        public string NomePaciente { get; set; }

        /// <summary>
        /// Sobrenome do paciente
        /// </summary>
        public string SobrenomePaciente { get; set; }

        /// <summary>
        /// Nome completo do paciente
        /// </summary>
        public string NomeCompletoPaciente {
            get {
                return this.NomePaciente + " " + this.SobrenomePaciente;
            }
        }

        /// <summary>
        /// Data de nascimento do paciente
        /// </summary>
        public DateTime DataNascimento { get; set; }

        /// <summary>
        /// Idade aparente
        /// </summary>
        public string IdadeAparentePaciente { get; set; }

        /// <summary>
        /// Sexo paciente
        /// </summary>
        public string SexoPaciente { get; set; }

        /// <summary>
        /// Cor paciente
        /// </summary>
        public ECorPaciente CorPaciente { get; set; }

        /// <summary>
        /// Nome da mãe
        /// </summary>
        public string MaePaciente { get; set; }

        /// <summary>
        /// Prioridade do pedido
        /// </summary>
        public EPrioridadePedido PrioridadePedido { get; set; }

        /// <summary>
        /// Data da emissão
        /// </summary>
        public DateTime DataEmissao { get; set; }

        /// <summary>
        /// Medico solicitante
        /// </summary>
        public Hcrp.Framework.Classes.Profissional MedicoSolicitante {
            get
            {
                try
                {
                    if (this._estaHabilitadoCarregamentoDoBD)
                    {
                        if (_medicoSolicitante == null)
                        {
                            if (this._codMedicoSolicitante > 0)
                            {
                                _medicoSolicitante =  new Hcrp.Framework.Classes.Profissional().BuscarProfissionalCodigo(this._codMedicoSolicitante);
                            }
                            else
                            {
                                return null;
                                //throw new Exception("A propriedade _codMedicoSolicitante deve ser informada.");
                            }
                        }
                    }
                    else
                    {
                        if (_medicoSolicitante == null)
                        {
                            _medicoSolicitante = new Profissional();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return _medicoSolicitante;
            }
            set
            {
                _medicoSolicitante = value;
            }
        }

        /// <summary>
        /// CID 1
        /// </summary>
        public Hcrp.Framework.Classes.Cid Cid1 { get; set; }
        
        /// <summary>
        /// CID 2
        /// </summary>
        public Hcrp.Framework.Classes.Cid Cid2 { get; set; }

        /// <summary>
        /// Cep do paciente
        /// </summary>
        public string CepPaciente { get; set; }

        /// <summary>
        /// Nro logradouro
        /// </summary>
        public string NumeroEnderecoPaciente { get; set; }

        /// <summary>
        /// Descrição do logradouro
        /// </summary>
        public string DescricaoLogradouroPaciente { get; set; }

        /// <summary>
        /// Complemento do logradouro
        /// </summary>
        public string ComplementoEnderecoPaciente { get; set; }

        /// <summary>
        /// Bairro do paciente
        /// </summary>
        public string BairroPaciente { get; set; }

        /// <summary>
        /// Endereço do paciente
        /// </summary>
        public string EnderecoPaciente { get; set; }

        /// <summary>
        /// Diagnostico
        /// </summary>
        public string Diagnostico { get; set; }

        /// <summary>
        /// Conduta Terapeutica 
        /// </summary>
        public string CondutaTerapeutica { get; set; }

        /// <summary>
        /// Quadro Clinico
        /// </summary>
        public string QuadroClinico { get; set; }

        /// <summary>
        /// Exames Realizados
        /// </summary>
        public string ExamesRealizados { get; set; }

        /// <summary>
        /// Justificativa Devolucao
        /// </summary> 
        public string JustificativaDevolucao { get; set; }

        /// <summary>
        /// Tipo Contato
        /// </summary>
        public ETipoContato TipoContato { get; set; }

        /// <summary>
        /// CNS Paciente
        /// </summary>
        public string CnsPaciente { get; set; }

        /// <summary>
        /// Celular Paciente
        /// </summary>
        public string CelularPaciente { get; set; }

        /// <summary>
        /// Fone Paciente
        /// </summary>
        public string FonePaciente { get; set; }

        /// <summary>
        /// Fone Contato
        /// </summary>
        public string FoneContato { get; set; }

        /// <summary>
        /// Email Paciente
        /// </summary>
        public string EmailPaciente { get; set; }

        /// <summary>
        /// Num Prontuario Municipio
        /// </summary>
        public string NumProntuarioMunicipio { get; set; }

        /// <summary>
        /// Prioridade Emissao
        /// </summary>
        public EPrioridadeEmissao PrioridadeEmissao { get; set; }

        /// <summary>
        /// Visualizado Guia Externa
        /// </summary>
        public string VisualizadoGuiaExterna { get; set; }

        /// <summary>
        /// CPF Paciente
        /// </summary>
        public string CpfPaciente { get; set; }

        /// <summary>
        /// RG Paciente
        /// </summary>
        public string RgPaciente { get; set; }

        /// <summary>
        /// Cod Orgao Emissor RG
        /// </summary>
        public int CodOrgaoEmissor { get; set; }

        /// <summary>
        /// UF RG
        /// </summary>
        public string UfRg { get; set; }

        /// <summary>
        /// Observacao
        /// </summary>
        public string Observacao { get; set; }

        /// <summary>
        /// Descricao Formulario Visualizacao
        /// </summary>
        public string DescricaoFormularioVisualizacao { get; set; }

        /// <summary>
        /// Grau de instrução do paciente
        /// </summary>
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

        /// <summary>
        /// Item pedido atendimento
        /// </summary>
        public ItemPedidoAtendimento ItemPedidoAtendimento { get; set; }

        /// <summary>
        /// Lista de itens de pedido atendimento
        /// </summary>
        public List<Hcrp.Framework.Classes.ItemPedidoAtendimento> ItemsPedido
        {
            get
            {
                return new Hcrp.Framework.Classes.ItemPedidoAtendimento().BuscarItemsAtendimento(this.Seq);
            }
        }

        /// <summary>
        /// Motivo de encaminhamento
        /// </summary>
        public MotivoEncaminhamento MotivoEncaminhamento
        {
            get
            {
                try
                {
                    if (this._estaHabilitadoCarregamentoDoBD)
                    {
                        if (_motivoEncaminhamento == null)
                        {
                            if (this._codMotivoEncaminhamento > 0)
                            {
                                _motivoEncaminhamento = new Hcrp.Framework.Classes.MotivoEncaminhamento().BuscarMotivoEncaminhamentoCodigo(this._codMotivoEncaminhamento);
                            }
                            else
                            {
                                return null;
                                //throw new Exception("A propriedade _codGrauInstrucao deve ser informada.");
                            }
                        }
                    }
                    else
                    {
                        if (_motivoEncaminhamento == null)
                        {
                            _motivoEncaminhamento = new MotivoEncaminhamento();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return _motivoEncaminhamento;
            }
            set
            {
                _motivoEncaminhamento = value;
            }
        }

        #endregion

        #region Enuns

        /// <summary>
        /// Enum sexo
        /// </summary>
        public enum ESexo
        {
            [DescriptionAttribute("Masculino")]
            Masculino = 'M',

            [DescriptionAttribute("Feminino")]
            Feminino = 'F',

            [DescriptionAttribute("Desconhecido")]
            Desconhecido = 'D'
        }

        /// <summary>
        /// Enum prioridade de emissão
        /// </summary>
        public enum EPrioridadeEmissao 
        { 
            Alta = 0,
            Media = 1,
            Baixa = 2
        }

        /// <summary>
        /// Enum tipo do pedido
        /// </summary>
        public enum ETipoPedido
        {
            [DescriptionAttribute("Consulta")]
            Consulta = 'C',

            [DescriptionAttribute("Exames com Agenda")]
            ExamesComAgenda = 'E',
            
            [DescriptionAttribute("Exames Laboratoriais")]
            ExamesLaboratoriais = 'L'
        }

        /// <summary>
        /// Enum tipo de protocolo
        /// </summary>
        public enum ETipoProtocolo
        {
            ConsultaGeral = 1,
            ProtocoloCardio = 2,
            ExameComAgenda = 3,
            ConsultaHERP = 4,
            ConsultaCir1 = 5,
            ConsultaCir2 = 6,
            ConsultaCir3 = 7
        }

        /// <summary>
        /// Enum cor do paciente
        /// </summary>
        public enum ECorPaciente
        {
            [DescriptionAttribute("Não informada")]
            NaoInformada = 0,

            [DescriptionAttribute("Branco")]
            Branco = 1,

            [DescriptionAttribute("Preto")]
            Preto = 2,

            [DescriptionAttribute("Amarelo")]
            Amarelo = 3,

            [DescriptionAttribute("Vermelho")]
            Vermelho = 4,

            [DescriptionAttribute("Mulato")]
            Mulato = 5
        }

        /// <summary>
        /// Enum Prioridade
        /// </summary>
        public enum EPrioridadePedido
        {
            [DescriptionAttribute("Alta")]
            Alta = 0,

            [DescriptionAttribute("Média")]
            Media = 1,

            [DescriptionAttribute("Baixa")]
            Baixa = 2
        }

        /// <summary>
        /// Enum tipo de contato
        /// </summary>
        public enum ETipoContato
        {
            [DescriptionAttribute("Pessoa")]
            Pessoal = 'P',

            [DescriptionAttribute("Contato")]
            Contato = 'C',

            [DescriptionAttribute("Unidade de Saúde")]
            UnidadeSaude = 'U'
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Obter lista de pacientes vinculados ao pedido de atendimento
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<Hcrp.Framework.Classes.PedidoAtendimento> BuscarPacientes(List<Hcrp.Framework.Classes.ParametrosOracle> filtros)
        {
            List<Hcrp.Framework.Classes.Paciente> LPaciente = new List<Hcrp.Framework.Classes.Paciente>();            
            List<Hcrp.Framework.Classes.PedidoAtendimento> LRetorno = new List<Hcrp.Framework.Classes.PedidoAtendimento>();

            if (filtros.Count == 1)
            {
                //Se for apenas Registro buscar na classe Paciente
                //Se for apenas Prontuario Municipio buscar na Pedido Atendimento
                if (filtros[0].CampoOracle == "COD_PACIENTE")
                    LPaciente = new Hcrp.Framework.Dal.Paciente().BuscarPacientes(filtros);
                else
                    LRetorno = new Hcrp.Framework.Dal.PedidoAtendimento().BuscarPacientes(filtros);
            }
            else
            { 
                LPaciente = new Hcrp.Framework.Dal.Paciente().BuscarPacientes(filtros);
                LRetorno = new Hcrp.Framework.Dal.PedidoAtendimento().BuscarPacientes(filtros);           
            }

            foreach (var item in LPaciente)
            {
                Hcrp.Framework.Classes.PedidoAtendimento pedidoAtendimento = new Hcrp.Framework.Classes.PedidoAtendimento(true);

                pedidoAtendimento.RegistroPaciente = item.RegistroPaciente;
                pedidoAtendimento.NomePaciente = item.NomePaciente;
                pedidoAtendimento.SobrenomePaciente = item.SobrenomePaciente;
                pedidoAtendimento.SexoPaciente = item.SexoPaciente;
                pedidoAtendimento.DataNascimento = item.DataNascimento;
                pedidoAtendimento.MaePaciente = item.MaePaciente;
                pedidoAtendimento.IdadeAparentePaciente = item.Idade;
                pedidoAtendimento.CorPaciente = (ECorPaciente)(int)item.Cor;
                pedidoAtendimento.CepPaciente = item.CepEndereco;
                pedidoAtendimento.NumeroEnderecoPaciente = item.NumeroEndereco;
                pedidoAtendimento.ComplementoEnderecoPaciente = item.ComplementoEndereco;
                pedidoAtendimento.BairroPaciente = item.NomeBairro;
                pedidoAtendimento.EnderecoPaciente = item.DescricaoEndereco;
                pedidoAtendimento.CnsPaciente = item.NumeroCNS;
                pedidoAtendimento.FoneContato = item.TelefoneContato;
                pedidoAtendimento.EmailPaciente = item.Email;
                pedidoAtendimento.CpfPaciente = item.CPFPaciente;
                pedidoAtendimento.RgPaciente = item.RGPaciente;
                pedidoAtendimento.UfRg = item.IdfUfRG;
                pedidoAtendimento.CodOrgaoEmissor = item.CodOrgaoEmissor;
                pedidoAtendimento._codGrauInstrucao = item._codGrauInstrucao;
                pedidoAtendimento._chaveMunicipio = item._chaveMunicipio;

                LRetorno.Add(pedidoAtendimento);
            }            

            //Senão trazer das duas
            return LRetorno;
        }

        /// <summary>
        /// Obter lista de pedidos por paciente
        /// </summary>
        /// <param name="pa"></param>
        /// <returns></returns>
        public List<Hcrp.Framework.Classes.PedidoAtendimento> BuscarPedidos(Hcrp.Framework.Classes.PedidoAtendimento pa)
        {
            return new Hcrp.Framework.Dal.PedidoAtendimento().BuscarPedidos(pa);
        }

        /// <summary>
        /// Gravar pedido atendimento
        /// </summary>
        /// <returns></returns>
        public long Gravar()
        {
            return new Hcrp.Framework.Dal.PedidoAtendimento().Gravar(this);
        }

        /// <summary>
        /// Obter pedido item
        /// </summary>
        /// <param name="seq_item"></param>
        /// <returns></returns>
        public Hcrp.Framework.Classes.PedidoAtendimento BuscarPedidoItem(int seq_item)
        {
            return new Hcrp.Framework.Dal.PedidoAtendimento().BuscarPedidoItem(seq_item);
        }

        #endregion 
    }
}
