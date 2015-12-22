using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Dal
{
    public class ProtocoloConsultaOftalmoExame
    {
        #region variáveis / construtor

        internal Hcrp.Infra.AcessoDado.TransacaoDinamica transacao;

        public ProtocoloConsultaOftalmoExame()
        {

        }

        public ProtocoloConsultaOftalmoExame(Hcrp.Infra.AcessoDado.TransacaoDinamica _trans)
        {
            transacao = _trans;
        }

        #endregion

        /// <summary>
        /// Obter protocolo consulta oftalmo exame por sequencia de item pedido atendimento.
        /// </summary>
        /// <param name="seqitemPedidoAtendimento"></param>
        /// <returns></returns>
        public List<Framework.Classes.ProtocoloConsultaOftalmoExame> ObterListaDeProtocoloConsultaOftalmoExamePorSequenciaItemPedidoAtendimento(int seqitemPedidoAtendimento)
        {
            Framework.Classes.ProtocoloConsultaOftalmoExame item = null;
            List<Framework.Classes.ProtocoloConsultaOftalmoExame> listRetorno = new List<Classes.ProtocoloConsultaOftalmoExame>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT P.SEQ_PROT_CONS_OFTALMO_EXAMES, P.SEQ_ITEM_PEDIDO_ATENDIMENTO,");
                    str.AppendLine(" P.IDF_LADO, P.IDF_TIPO_EXAME, P.DSC_RESULTADO ");
                    str.AppendLine(" FROM PROTOCOLO_CONS_OFTALMO_EXAMES P ");
                    str.AppendLine(" WHERE P.SEQ_ITEM_PEDIDO_ATENDIMENTO = :SEQ_ITEM_PEDIDO_ATENDIMENTO ");

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = seqitemPedidoAtendimento;

                    // Abrir conexão
                    ctx.Open();

                    // Executar a query
                    ctx.ExecuteQuery(query);

                    // Preparar o retorno
                    while (ctx.Reader.Read())
                    {
                        item = new Classes.ProtocoloConsultaOftalmoExame();

                        if (ctx.Reader["SEQ_PROT_CONS_OFTALMO_EXAMES"] != DBNull.Value)
                            item.NumSeqProtConsOftalmoExame = Convert.ToInt64(ctx.Reader["SEQ_PROT_CONS_OFTALMO_EXAMES"]);

                        if (ctx.Reader["SEQ_ITEM_PEDIDO_ATENDIMENTO"] != DBNull.Value)
                            item.NumSeqItemPedidoAtendimento = Convert.ToInt32(ctx.Reader["SEQ_ITEM_PEDIDO_ATENDIMENTO"]);

                        if (ctx.Reader["IDF_LADO"] != DBNull.Value)
                            item.IdfLado = ctx.Reader["IDF_LADO"].ToString();

                        if (ctx.Reader["IDF_TIPO_EXAME"] != DBNull.Value)
                            item.IdfTipoExame = Convert.ToInt16(ctx.Reader["IDF_TIPO_EXAME"]);

                        if (ctx.Reader["DSC_RESULTADO"] != DBNull.Value)
                            item.DscResultado = ctx.Reader["DSC_RESULTADO"].ToString();

                        listRetorno.Add(item);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return listRetorno;
        }

        /// <summary>
        /// Inserir novo Protocolo de Consulta Oftalmo Exames.
        /// </summary>
        /// <param name="protoc"></param>
        public void Inserir(Framework.Classes.ProtocoloConsultaOftalmoExame protoc)
        {
            try
            {

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PROTOCOLO_CONS_OFTALMO_EXAMES");
                    comando.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = protoc.NumSeqItemPedidoAtendimento;
                    comando.Params["IDF_LADO"] = protoc.IdfLado;
                    comando.Params["IDF_TIPO_EXAME"] = protoc.IdfTipoExame;
                    comando.Params["DSC_RESULTADO"] = protoc.DscResultado;

                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inserir novo Protocolo de Consulta Oftalmo Exames transacionado.
        /// </summary>        
        public void InserirTrans(Framework.Classes.ProtocoloConsultaOftalmoExame protoc)
        {
            try
            {
                // Obter contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                // Preparar o comando
                Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PROTOCOLO_CONS_OFTALMO_EXAMES");
                comando.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = protoc.NumSeqItemPedidoAtendimento;
                comando.Params["IDF_LADO"] = protoc.IdfLado;
                comando.Params["IDF_TIPO_EXAME"] = protoc.IdfTipoExame;
                comando.Params["DSC_RESULTADO"] = protoc.DscResultado;

                // Executar o insert
                ctx.ExecuteInsert(comando);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Excluir um Protocolo de Consulta Oftalmo Exames.
        /// </summary>
        /// <param name="protoc"></param>
        /// <returns></returns>
        public bool Excluir(Int64 numSeqProtocoloConsultaOftalmoExame)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PROTOCOLO_CONS_OFTALMO_EXAMES");

                    comando.Params["SEQ_PROT_CONS_OFTALMO_EXAMES"] = numSeqProtocoloConsultaOftalmoExame;

                    // Executar o Delete
                    ctx.ExecuteDelete(comando);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Excluir Protocolo de Consulta Oftalmo Exames por sequencia de item pedido atendimento.
        /// </summary>
        /// <param name="protoc"></param>
        /// <returns></returns>
        public bool ExcluirPorNumSeqItemPedidoAtendimento(int numSeqItemPedidoAtendimento)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PROTOCOLO_CONS_OFTALMO_EXAMES");

                    comando.Params["SEQ_ITEM_PEDIDO_ATENDIMENTO"] = numSeqItemPedidoAtendimento;

                    // Executar o Delete
                    ctx.ExecuteDelete(comando);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
