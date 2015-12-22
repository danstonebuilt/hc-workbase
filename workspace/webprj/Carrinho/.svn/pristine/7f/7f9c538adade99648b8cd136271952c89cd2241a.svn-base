using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{

    // Antes de adicionar qualquer parametrização, verificar se já nao existe em "Framework.Infra.Util.Parametrizacao.Instancia".
    
    public class Parametrizacao
    {   
        private static Parametrizacao instancia = null;
        private Parametrizacao() { }

        /// <summary>
        /// Gerencia a instancia da classe.
        /// </summary>
        /// <returns></returns>
        public static Parametrizacao Instancia()
        {
            if (instancia == null) instancia = new Parametrizacao();
            return instancia;
        }        
       
        public string UrlSistema
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["UrlSistema"].ToString();
            }
        }

        public Int64 TamanhoMaximoUpload
        {
            get
            {
                return Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["TamanhoMaximoUpload"]);
            }
        }

        public string UrlTermoConsentimento
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["urlTermoConsentimento"].ToString();
            }
        }

        public bool EstaEmAmbienteDeDesenvolvimento
        {
            get
            {
                return Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["AmbienteDeDesenvolvimento"]);
            }
        }
        
        public string CaminhoGerarEmailQuandoEmDesenvolvimento
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["caminhoGerarEmailQuandoDev"].ToString();
            }
        }

        public string smtp
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["smtp"].ToString();
            }
        }

        public string UrlMenuSistemas
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["urlMenuSistemas"].ToString();
            }
        }

        /// <summary>
        /// Obter o código do sistema.
        /// </summary>
        public int CodigoDoSistema
        {
            get
            {
                return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CodigoSistema"]);
            }
        }

        /// <summary>
        /// Retorna quantidade de registro por página
        /// </summary>
        public int QuantidadeRegistroPagina
        {
            get
            {
                return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["QuantidadeRegPagina"]);
            }
        }

        /// <summary>
        /// Código da situação do carrinho "Provisório"
        /// </summary>
        public Int64 CodigoDaSituacaoProvisorio
        {
            get
            {
                return Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["codSituacaoProvisorio"]);
            }
        }

        /// <summary>
        /// Código da situação do carrinho "Lacrado"
        /// </summary>
        public Int64 CodigoDaSituacaoLacrado
        {
            get
            {
                return Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["codSituacaoLacrado"]);
            }
        }

        /// <summary>
        /// Código da situação do carrinho "Rompido"
        /// </summary>
        public Int64 CodigoDaSituacaoRompido
        {
            get
            {
                return Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["codSituacaoRompido"]);
            }
        }

        /// <summary>
        /// Código da situação do carrinho "lacre conferido".
        /// </summary>
        public Int64 CodigoDaSituacaoLacreConferido
        {
            get
            {
                return Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["codSituacaoLacreConferido"]);
            }
        }

        /// <summary>
        /// Número de dias de vencimento para a troca de medicamento.
        /// </summary>
        public Int32 NumeroDeDiasParaVencimentoDaTrocaDeMedicamento
        {
            get
            {
                return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["numDiaVencimentoTrocaMedicamento"]);
            }
        }

        /// <summary>
        /// Número de dias de vencimento para a troca de material.
        /// </summary>
        public Int32 NumeroDeDiasParaVencimentoDaTrocaDeMaterial
        {
            get
            {
                return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["numDiaVencimentoTrocaMaterial"]);
            }
        }

        /// <summary>
        /// Seq lacre tipo ocorrencia nova lacração.
        /// </summary>
        public Int64 SeqTipoOcorrenciaNovaLacracao
        {
            get
            {
                return Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["seqLacreTipoOcorrenciaAtendimento"]);
            }
        }

        /// <summary>
        /// Obter o código da role enfermeiro.
        /// </summary>
        public Int64 CodigoDaRoleEnfermeiro
        {
            get
            {
                return Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["codRoleEnfermeiro"]);
            }
        }

        /// <summary>
        /// Obter o código da role gestor.
        /// </summary>
        public Int64 CodigoDaRoleGestor
        {
            get
            {
                return Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["codRoleGestor"]);
            }
        }

        public Int64 CodigoDaRoleUsuario
        {
            get
            {
                return Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["codRoleUsuario"]);
            }
        }

        /// <summary>
        /// Obter o código da role enfermeiro.
        /// </summary>
        public Int64 CodigoDaRoleAdministrador
        {
            get
            {
                return Convert.ToInt64(System.Configuration.ConfigurationManager.AppSettings["codRoleAdministrador"]);
            }
        }

        /// <summary>
        /// Endereço do servidor onde fica o serviço hcrp
        /// </summary>
        public string EnderecoServico
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["EnderecoServico"].ToString();
            }
        }

        /// <summary>
        /// nome do diretorio onde fica o serviço de consulta de materiais
        /// </summary>
        public string ServicoConsultaMaterial
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ServicoConsultaMaterial"].ToString();
            }
        }
    }
}
