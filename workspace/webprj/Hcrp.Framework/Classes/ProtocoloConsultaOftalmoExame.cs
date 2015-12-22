using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ProtocoloConsultaOftalmoExame
    {
        public Int64 NumSeqProtConsOftalmoExame { get; set; }
        public int NumSeqItemPedidoAtendimento { get; set; }
        public string IdfLado { get; set; }
        
        public Int16 IdfTipoExame { get; set; }
        public string IdfTipoExameExtenso
        {
            get
            {
                // 1 - Acuidade Visual Corrigida
                // 2 - Refração
                // 3 - Tonometria
                // 4 - Biomicroscopia
                // 5 - Fundo de Olho
                // 6 - Refração Estática 
                // 7 - Mobilidade ocular 
                // 8 - Papila Corada
                // 9 - Papila Pálida
                // 10 - Papila com Edema
                // 11 - Malformação de Papila
                // 12 - Escavação Fisiológica
                // 13 - Aumento de Escavação
                // 14 - Defeitos Localizados de Escavação
                // 15 - Mácula sem Alterações
                // 16 - Máciça com Alterações
                // 17 - Vasos sem Alterações
                // 18 - Alterações Vasculares
                // 19 - Retina sem Alaterações
                // 20 - Alterações na Retina
                // 99 - Outros exames



                if (this.IdfTipoExame == 1)
                    return "Acuidade Visual Corrigida";
                else if (this.IdfTipoExame == 2)
                    return "Refração";
                else if (this.IdfTipoExame == 3)
                    return "Tonometria";
                else if (this.IdfTipoExame == 4)
                    return "Biomicroscopia";
                else if (this.IdfTipoExame == 5)
                    return "Fundo de Olho";
                else if (this.IdfTipoExame == 6)
                    return "Refração Estática";
                else if (this.IdfTipoExame == 7)
                    return "Motilidade ocular";
                else if (this.IdfTipoExame == 8)
                    return "Papila Corada";
                else if (this.IdfTipoExame == 9)
                    return "Papila Pálida";
                else if (this.IdfTipoExame == 10)
                    return "Papila com Edema";
                else if (this.IdfTipoExame == 11)
                    return "Malformação de Papila";
                else if (this.IdfTipoExame == 12)
                    return "Escavação Fisiológica";
                else if (this.IdfTipoExame == 13)
                    return "Aumento de Escavação";
                else if (this.IdfTipoExame == 14)
                    return "Defeitos Localizados de Escavação";
                else if (this.IdfTipoExame == 15)
                    return "Mácula sem Alterações";
                else if (this.IdfTipoExame == 16)
                    return "Máciça com Alterações";
                else if (this.IdfTipoExame == 17)
                    return "Vasos sem Alterações";
                else if (this.IdfTipoExame == 18)
                    return "Alterações Vasculares";
                else if (this.IdfTipoExame == 19)
                    return "Retina sem Alterações";
                else if (this.IdfTipoExame == 20)
                    return "Alterações na Retina";
                else if (this.IdfTipoExame == 99)
                    return "Outros exames";
                else
                    return "";

            }
        }
        
        public string DscResultado { get; set; }

        /// <summary>
        /// Obter protocolo consulta oftalmo exame por sequencia de item pedido atendimento.
        /// </summary>
        /// <param name="seqitemPedidoAtendimento"></param>
        /// <returns></returns>
        public List<Hcrp.Framework.Classes.ProtocoloConsultaOftalmoExame> ObterListaDeProtocoloConsultaOftalmoExamePorSequenciaItemPedidoAtendimento(int seqitemPedidoAtendimento)
        {
            return new Hcrp.Framework.Dal.ProtocoloConsultaOftalmoExame().ObterListaDeProtocoloConsultaOftalmoExamePorSequenciaItemPedidoAtendimento(seqitemPedidoAtendimento);
        }

        /// <summary>
        /// Inserir novo Protocolo de Consulta Oftalmo Exames.
        /// </summary>
        /// <param name="protoc"></param>
        public void Inserir(Framework.Classes.ProtocoloConsultaOftalmoExame protoc)
        {
            new Hcrp.Framework.Dal.ProtocoloConsultaOftalmoExame().Inserir(protoc);
        }

        /// <summary>
        /// Inserir novo Protocolo de Consulta Oftalmo Exames.
        /// </summary>        
        public void InserirTrans(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, Framework.Classes.ProtocoloConsultaOftalmoExame protoc)
        {
            new Hcrp.Framework.Dal.ProtocoloConsultaOftalmoExame(transacao).InserirTrans(protoc);
        }

        /// <summary>
        /// Excluir um Protocolo de Consulta Oftalmo Exames.
        /// </summary>
        /// <param name="protoc"></param>
        /// <returns></returns>
        public void Excluir(Int64 numSeqProtocoloConsultaOftalmoExame)
        {
            new Hcrp.Framework.Dal.ProtocoloConsultaOftalmoExame().Excluir(numSeqProtocoloConsultaOftalmoExame);
        }

        /// <summary>
        /// Excluir Protocolo de Consulta Oftalmo Exames por sequencia de item pedido atendimento.
        /// </summary>
        /// <param name="protoc"></param>
        /// <returns></returns>
        public void ExcluirPorNumSeqItemPedidoAtendimento(int numSeqItemPedidoAtendimento)
        {
            new Hcrp.Framework.Dal.ProtocoloConsultaOftalmoExame().ExcluirPorNumSeqItemPedidoAtendimento(numSeqItemPedidoAtendimento);
        }
    }
}
