using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ItemPedidoAtendimento
    {
        public int _CodProcedimentoHc { get; set; }
        public int _NumSeqLocalAtendimento { get; set; }
        public int _NumUserDevolucao { get; set; }
        public int _CodSituacao { get; set; }
        public int _CodMotivoCancelamento { get; set; }
        public long _NumSeqPedidoAtendimento { get; set; }
        public int _CodInstituto { get; set; }
        public int _CodEspecialidadeHc { get; set; }
        public int _SeqConfProcAgenda { get; set; }
        public string _EspecialidadeHc { get; set; }
        public Instituto _Instituto { get; set; }

        /// <summary>
        /// IDF_CONTRASTE_EXAME
        /// </summary>
        public string _IdfContrasteExame { get; set; }


        public List<Hcrp.Framework.Classes.ObservacaoItemAtendimento> _Observacoes { get; set; }
        public int Seq { get; set; }
        public Hcrp.Framework.Classes.MapeamentoLocal LocalAtendimento 
        {
            get
            {
                return new Hcrp.Framework.Classes.MapeamentoLocal().BuscarLocalCodigo(this._NumSeqLocalAtendimento);
            }        
        }
        public DateTime DataAtendimento { get; set; }
        public Hcrp.Framework.Classes.Usuario UsuarioDevolucao
        {
            get
            {
                return new Hcrp.Framework.Classes.Usuario().BuscarUsuarioCodigo(this._NumUserDevolucao);
            }
            set { }
        }
        public SituacaoHc Situacao
        {
            get
            {
                return new Hcrp.Framework.Classes.SituacaoHc().BuscarSituacaoCodigo(this._CodSituacao);
            }
        }
        public string DescricaoSituacaoHC { get; set; }
        public Motivo MotivoCancelamento 
        {
            get
            {
                return new Hcrp.Framework.Classes.Motivo().BuscarMotivoCodigo(this._CodMotivoCancelamento);
            }
        }
        public EOrigemAgendamento OrigemAgendamento { get; set; }
        /// <summary>
        /// 1 - Radiologia 2 :
        ///     NUM_ORIGEM_AGENDAMENTO = PEDIDO_EXAME_ITEM_HC.NUM_PEDIDO_EXAME_ITEM
        /// 2 - Endoscopia:
        ///     NUM_ORIGEM_AGENDAMENTO = PEDIDO_EXAME_ENDOSCOPIA.NUM_PEDIDO
        /// 3 - Cardiologia (Antigo):
        /// 4 - Cardiologia (novo)
        ///     NUM_ORIGEM_AGENDAMENTO = PEDIDO_EXAME_ITEM_HC.NUM_PEDIDO_EXAME_ITEM
        /// 5 - Broncofibroscopia:
        ///     NUM_ORIGEM_AGENDAMENTO = PEDIDO_EXAME_ITEM_HC.NUM_PEDIDO_EXAME_ITEM 
        /// </summary>
        public int NumeroOrigem { get; set; }
        public ETipoSituacaoItemPedidoAtendimento TipoSituacao { get; set; }
        public Hcrp.Framework.Classes.PedidoAtendimento PedidoAtendimento
        {
            get
            {
                return new Hcrp.Framework.Classes.PedidoAtendimento().BuscarPedidoItem(this.Seq) ;
            }
        }
        public ProcedimentoHc ProcedimentoHc
        {
            get
            {
                if (this._CodProcedimentoHc > 0)
                    return new Hcrp.Framework.Classes.ProcedimentoHc().BuscaProcedimentoCodigo(this._CodProcedimentoHc);
                else return null;
            }
        }
        public List<Hcrp.Framework.Classes.ObservacaoItemAtendimento> Observacoes { 
            get{
                _Observacoes = new Hcrp.Framework.Classes.ObservacaoItemAtendimento().BuscarObservacoesItem(Seq);
                return _Observacoes;
            } 
            set{
                _Observacoes = value;
            } 
        }
        public Classes.Instituto Instituto
        {
            get
            {                
                return new Hcrp.Framework.Classes.Instituto().BuscarInstitutoCodigo(this._CodInstituto);
            }
        }
        public Classes.Especialidade Especialidade
        {
            get
            {
                return new Hcrp.Framework.Classes.Especialidade().BuscaEspecialidadeCodigo(this._CodEspecialidadeHc);
            }
        }
        public Classes.ConfigProcedimentoSadtComAgenda ConfigProcedimentoSadtComAgenda
        {
            get
            {
                return new Hcrp.Framework.Classes.ConfigProcedimentoSadtComAgenda().BuscaConfigCodigo(this._SeqConfProcAgenda);
            }
        }
        public Int64 NumeroOrigemAgendamento { get; set; }

        public string IdfNivelAtendimento { get; set; }
        public string IdfAdequacaoAtendimento { get; set; }        

        public enum EOrigemAgendamento
        {
            Radiologia2 = 1,
            Endoscopia = 2,
            Cardiologia1 = 3,
            Cardiologia2 = 4,
            Broncofibroscopia = 5,
            Consultas = 6
        }
        public enum ETipoSituacaoItemPedidoAtendimento
        {
            Solicitado = 26,
            Cancelado = 2,
            Triado = 44,
            Atendido = 10,
            Notificado = 27,
            Devolvido = 35,
            SolicitadoExtracota = 125,
            Reagendado = 126,
            PreTriagem = 92,
            DevolvidoDRS = 65,
            EncaminhamentoExterno = 103,
            NotificadoHC = 66,
            TriadoHC = 64
        }
        
        public ItemPedidoAtendimento()
        { }

        public List<Hcrp.Framework.Classes.ItemPedidoAtendimento> BuscarItemsAtendimento(int seqPedidoAtendimento)
        {
            return new Hcrp.Framework.Dal.ItemPedidoAtendimento().BuscarItemsAtendimento(seqPedidoAtendimento);
        }
        public Hcrp.Framework.Classes.ItemPedidoAtendimento BuscarItemAtendimento(int seqItemPedidoAtendimento)
        {
            return new Hcrp.Framework.Dal.ItemPedidoAtendimento().BuscarItemAtendimento(seqItemPedidoAtendimento);
        }
        public List<Hcrp.Framework.Classes.ItemPedidoAtendimento> BuscarPedidosControle(List<Hcrp.Framework.Classes.ParametrosOracle> filtros, Hcrp.Framework.Classes.ItemPedidoAtendimento.ETipoSituacaoItemPedidoAtendimento situacao, int codServicoSadt, int codDrs)
        {
            List<Hcrp.Framework.Classes.ItemPedidoAtendimento> LRetorno = new List<Hcrp.Framework.Classes.ItemPedidoAtendimento>();
            LRetorno = new Hcrp.Framework.Dal.ItemPedidoAtendimento().BuscarPedidosControle(filtros, situacao, codServicoSadt, codDrs);
            return LRetorno;
        }
        public long Gravar(long seqPedidoAtendimento)
        {
            return new Hcrp.Framework.Dal.ItemPedidoAtendimento().Gravar(this,seqPedidoAtendimento);
        }
        public void AlterarStatus(ETipoSituacaoItemPedidoAtendimento NovoStatus)
        {
            new Hcrp.Framework.Dal.ItemPedidoAtendimento().AlterarStatus(this, NovoStatus);
        }
    }
}