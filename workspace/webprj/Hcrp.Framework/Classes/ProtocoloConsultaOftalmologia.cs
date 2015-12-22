using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ProtocoloConsultaOftalmologia
    {
        public Int64 NumSeqProtocoloConsultaOftalmologia { get; set; }
        public int NumSeqItemPedidoAtendimento { get; set; }
        
        public string IdfEspecialidadeMedico { get; set; }
        public string IdfEspecialidadeMedicoExtenso
        {
            get
            {
                //1 - OFTALMOLOGIA
                //2 - MEDICO DA FAMILIA
                //3 - CLINICA MÉDICA
                //9 - OUTRA
                
                if (!string.IsNullOrWhiteSpace(this.IdfEspecialidadeMedico))
                {
                    if (this.IdfEspecialidadeMedico == "1")
                        return "OFTALMOLOGIA";
                    else if (this.IdfEspecialidadeMedico == "2")
                        return "MÉDICO DA FAMILIA";
                    else if (this.IdfEspecialidadeMedico == "3")
                        return "CLINICA MÉDICA";
                    else if (this.IdfEspecialidadeMedico == "9" && !string.IsNullOrWhiteSpace(this.DscOutraEspecialidadeMedico))
                        return this.DscOutraEspecialidadeMedico;
                    else
                        return "";
                }
                else
                {
                    return "";
                }
            }
        }
        
        public string DscOutraEspecialidadeMedico { get; set; }
        public string IdfNecessidadeExameComplementar { get; set; }
        public string IdfNecessidadeAvaliacaoDiagnostica { get; set; }
        public string IdfNecessidadeSeguimentoClinico { get; set; }
        public string IdfNecessidadeAvaliacaoEspecializadaCirurgica { get; set; }
        public string IdfNecessidadeProcedimento { get; set; }
        public List<Framework.Classes.ProtocoloConsultaOftalmologiaExame> listProtocoloConsultaOftalmologiaExame { get; set; }
        public List<Framework.Classes.ProtocoloConsultaOftalmoProc> listProtocoloConsultaOftalmologiaProcedimento { get; set; }

        public ProtocoloConsultaOftalmologia()
        {
            this.listProtocoloConsultaOftalmologiaExame = new List<ProtocoloConsultaOftalmologiaExame>();
            this.listProtocoloConsultaOftalmologiaProcedimento = new List<ProtocoloConsultaOftalmoProc>();
        }

        /// <summary>
        /// Obter protocolo consulta oftalmologia por sequencia de item pedido atendimento.
        /// </summary>
        /// <param name="seqitemPedidoAtendimento"></param>
        /// <returns></returns>
        public Hcrp.Framework.Classes.ProtocoloConsultaOftalmologia ObterProtocoloConsultaOftalmologiaPorSequenciaItemPedidoAtendimento(int numSeqitemPedidoAtendimento)
        {
            return new Hcrp.Framework.Dal.ProtocoloConsultaOftalmologia().ObterProtocoloConsultaOftalmologiaPorSequenciaItemPedidoAtendimento(numSeqitemPedidoAtendimento);
        }

        /// <summary>
        /// Inserir protocolo consulta oftalmologia.
        /// </summary>
        /// <param name="seqitemPedidoAtendimento"></param>
        /// <returns></returns>
        public void Inserir(Framework.Classes.ProtocoloConsultaOftalmologia protoc)
        {
            new Hcrp.Framework.Dal.ProtocoloConsultaOftalmologia().Inserir(protoc);
        }

        /// <summary>
        /// Inserir protocolo consulta oftalmologia.
        /// </summary>       
        public void InserirTrans(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, Framework.Classes.ProtocoloConsultaOftalmologia protoc)
        {
            new Hcrp.Framework.Dal.ProtocoloConsultaOftalmologia(transacao).InserirTrans(protoc);
        }
    }
}
