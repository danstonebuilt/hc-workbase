using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;
using Oracle.DataAccess.Client;

namespace Hcrp._Pep.DAL
{
    public class ExameHC
    {
        #region Métodos

        /// <summary>
        /// Retorna uma lista de exame com base nos parametros informados na procedure proc_exame_paciente_hc.         
        /// </summary>        
        /// <returns></returns>
        public DataView ObterDadosExamesProcExamePacienteHC(sbyte pTipoPaciente,
            string pCodPaciente, DateTime? pDataInicial, DateTime? pDataFinal, sbyte? pServico, sbyte pTipoOrdenacao)
        {
            try
            {
                using (Contexto ctx = new Contexto())
                {
                    // Abrir conexão                
                    ctx.Open();

                    OracleCommand com = new OracleCommand();

                    com.Connection = ctx.Conn as OracleConnection;
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "PROC_EXAME_PACIENTE_HC";

                    com.Parameters.Add("v_tipo_paciente", OracleDbType.Int16, ParameterDirection.Input).Value = pTipoPaciente; //0 - Paciente COM REGISTRO HC / 1- Paciente SEM REGISTRO HC
                    com.Parameters.Add("v_cod_paciente", OracleDbType.Varchar2, ParameterDirection.Input).Value = pCodPaciente;
                    com.Parameters.Add("v_dta_inicial", OracleDbType.Date, ParameterDirection.Input).Value = pDataInicial;
                    com.Parameters.Add("v_dta_final", OracleDbType.Date, ParameterDirection.Input).Value = pDataFinal;
                    com.Parameters.Add("v_servico", OracleDbType.Int32, ParameterDirection.Input).Value = pServico;                    
                    //v_tipo_ordenacao = 1 - Serviço   2 - Procedimento HC 3 - Situação 4 - Data 5 - Liberação
                    com.Parameters.Add("v_tipo_ordenacao", OracleDbType.Int16, ParameterDirection.Input).Value = pTipoOrdenacao;
                    com.Parameters.Add("cs_menu", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    // Obter a lista de registros
                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(com.ExecuteReader());

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Retorna a quantidade de exame criticos
        /// </summary>        
        /// <returns></returns>
        public Int16 ObterQtdExamesCriticosPorPedidoExameItem(Int64 pNumPedidoExameItem)
        {
            Int16 intQtdExames = 0;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine("SELECT COUNT(*) QTDE FROM NOTIFICACAO_EXAME_CRITICO N WHERE N.NUM_PEDIDO_EXAME_ITEM = " + pNumPedidoExameItem.ToString());
                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        dr.Read();
                        intQtdExames = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<Int16>(dr, "QTDE");
                    }
                    finally
                    {
                        dr.Close();
                        dr.Dispose();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

            return intQtdExames;
        }


        /// <summary>
        /// Retorna uma lista de exame com base nos parametros informados na procedure proc_exame_paciente_hc.         
        /// </summary>        
        /// <returns></returns>
        public Int16 ObterQtdImagensExameRadiologia(Int32 pNumPedidoExameItem)
        {
            Int16 intQtdImagens = 0;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine("SELECT COUNT(*) QTDE FROM ");
                    str.AppendLine("GO.PEDIDO_EXAME_ITEM_IMAGEM ");
                    str.AppendLine("WHERE NUM_PEDIDO_EXAME_ITEM = " + pNumPedidoExameItem);
                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        dr.Read();
                        intQtdImagens = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<Int16>(dr, "QTDE");
                    }
                    finally
                    {
                        dr.Close();
                        dr.Dispose();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

            return intQtdImagens;
        }

        /// <summary>
        /// Obter dados de exame hc para visualização de exames.
        /// </summary>        
        public DataView ObterDadosDoExameHCParaVisualizacaoDeExames(Int64 numPedidoExameItem)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT FCN_RESULTADO_EXAME_PEP(A.NUM_PEDIDO_EXAME_ITEM) DSC_RESULTADO_EXAME, ");
                    str.AppendLine("        B.SEQ_EXAME_HC_ARQUIVO, ");
                    str.AppendLine("        B.DSC_TIPO_ARQUIVO, ");
                    str.AppendLine("        SUBSTR(FNC_CALCULA_TEMPO(A.DTA_HOR_EXAME,D.DTA_NASCIMENTO,'aammddhhmiss'),1,240) IDADE, ");
                    str.AppendLine("        A.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" FROM EXAME_HC A, ");
                    str.AppendLine("      EXAME_HC_ARQUIVO B, ");
                    str.AppendLine("      ELEMENTO_PRONTUARIO_PACIENTE C, ");
                    str.AppendLine(" PACIENTE D ");
                    str.AppendLine(string.Format(" WHERE A.NUM_PEDIDO_EXAME_ITEM = {0} ", numPedidoExameItem));
                    str.AppendLine(" AND A.NUM_EXAME = B.NUM_EXAME(+) ");
                    str.AppendLine(" AND C.SEQ_ELEMENTO_PRONTUARIO = 3 ");
                    str.AppendLine(" AND A.NUM_PEDIDO_EXAME_ITEM = C.NUM_ORIGEM ");
                    str.AppendLine(" AND C.COD_PACIENTE = D.COD_PACIENTE ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;

                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter o número do exame pelo número do laudo.
        /// </summary>        
        public Int64 ObterNumeroDoExamePeloNumeroDoLaudo(long numLaudo)
        {
            Int64 numExame = 0;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT NUM_EXAME  ");
                    str.AppendLine(" FROM EXAME_HC ");
                    str.AppendLine(string.Format(" WHERE NUM_PEDIDO_EXAME_ITEM = {0} ", numLaudo));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        dr.Read();

                        numExame = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<Int64>(dr, "NUM_EXAME");

                    }
                    finally
                    {
                        dr.Close();
                        dr.Dispose();

                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

            return numExame;
        }

        /// <summary>
        /// Obter dados de exame hc para visualização de laudo eletrocardiograma.
        /// </summary>        
        public DataView ObterDadosDoExameHCParaVisualizacaoDeLaudoEletrocardiograma(Int64 numPedidoExameItem)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT A.NUM_EXAME, ");
                    str.AppendLine("    A.NUM_ID_EXAME, ");
                    str.AppendLine("    'IMAGEM DO EXAME' DSC_TITULO_RELATORIO, ");
                    str.AppendLine("    A.DTA_HOR_EXAME, ");
                    str.AppendLine("    SUBSTR(FNC_CALCULA_TEMPO(A.DTA_HOR_EXAME, ");
                    str.AppendLine("    E.DTA_NASCIMENTO,'aammddhhmiss'),1,240) IDADE, ");
                    str.AppendLine("    C.SEQ_EXAME_HC_ARQUIVO, ");
                    str.AppendLine("    A.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" FROM EXAME_HC A, ");
                    str.AppendLine("    PEDIDO_EXAME_ITEM_HC B, ");
                    str.AppendLine("    EXAME_HC_ARQUIVO C, ");
                    str.AppendLine("    PEDIDO_EXAME_HC D, ");
                    str.AppendLine("    PACIENTE E ");
                    str.AppendLine(string.Format(" WHERE A.NUM_PEDIDO_EXAME_ITEM = {0} ", numPedidoExameItem));
                    str.AppendLine("    AND A.NUM_EXAME = C.NUM_EXAME ");
                    str.AppendLine("    AND A.NUM_PEDIDO_EXAME_ITEM = B.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine("    AND B.NUM_PEDIDO = D.NUM_PEDIDO  ");
                    str.AppendLine("    AND D.COD_PACIENTE = E.COD_PACIENTE ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter dados de diagnostico ECG para visualização de laudo eletrocardiograma.
        /// </summary>        
        public DataView ObterDadosDeDiagnosticoECGParaVisualizacaoDeLaudoEletrocardiograma(Int64 numPedidoExameItem)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT DISTINCT ");
                    str.AppendLine("     D.COD_DIAGNOSTICO_ECG, ");
                    str.AppendLine("     D.DSC_DIAGNOSTICO_ECG ");
                    str.AppendLine(" FROM EXAME_HC A, ");
                    str.AppendLine("    GENERICO.EXAME_DIAGNOSTICO_ECG_ITEM B, ");
                    str.AppendLine("    GENERICO.DIAGNOSTICO_ECG_ITEM C, ");
                    str.AppendLine("    GENERICO.DIAGNOSTICO_ECG D ");
                    str.AppendLine(string.Format(" WHERE A.NUM_PEDIDO_EXAME_ITEM = {0} ", numPedidoExameItem));
                    str.AppendLine("    AND A.IDF_SITUACAO IN (3,4) ");
                    str.AppendLine("    AND A.NUM_EXAME = B.NUM_EXAME ");
                    str.AppendLine("    AND B.SEQ_DIAGNOSTICO_ECG_ITEM= C.SEQ_DIAGNOSTICO_ECG_ITEM ");
                    str.AppendLine("    AND C.COD_DIAGNOSTICO_ECG = D.COD_DIAGNOSTICO_ECG ");
                    str.AppendLine(" ORDER BY D.DSC_DIAGNOSTICO_ECG ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter dados de item diagnostico ECG para visualização de laudo eletrocardiograma.
        /// </summary>        
        public DataView ObterDadosDeItemDiagnosticoECGParaVisualizacaoDeLaudoEletrocardiograma(Int64 numPedidoExameItem, Int64 codDiagnosticoEcg)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT C.DSC_DIAGNOSTICO_ECG_ITEM, ");
                    str.AppendLine("        COD_DIAGNOSTICO_ECG ");
                    str.AppendLine(" FROM EXAME_HC A, ");
                    str.AppendLine(" GENERICO.EXAME_DIAGNOSTICO_ECG_ITEM B, ");
                    str.AppendLine(" GENERICO.DIAGNOSTICO_ECG_ITEM C ");
                    str.AppendLine(string.Format(" WHERE A.NUM_PEDIDO_EXAME_ITEM = {0} ", numPedidoExameItem));
                    str.AppendLine(" AND A.IDF_SITUACAO IN (3,4) ");
                    str.AppendLine(string.Format(" AND C.COD_DIAGNOSTICO_ECG = {0} ", codDiagnosticoEcg));
                    str.AppendLine(" AND A.NUM_EXAME = B.NUM_EXAME ");
                    str.AppendLine(" AND B.SEQ_DIAGNOSTICO_ECG_ITEM= C.SEQ_DIAGNOSTICO_ECG_ITEM ");
                    str.AppendLine(" ORDER BY C.DSC_DIAGNOSTICO_ECG_ITEM ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter descrição do exame laudo para visualização de laudo eletrocardiograma.
        /// </summary>        
        public DataView ObterDesricaoDoExameLaudoParaVisualizacaoDeLaudoEletrocardiograma(Int64 numPedidoExameItem)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT B.DSC_EXAME_LAUDO ");
                    str.AppendLine(" FROM EXAME_HC A, ");
                    str.AppendLine("      EXAME_LAUDO_HC B ");
                    str.AppendLine(string.Format(" WHERE A.NUM_PEDIDO_EXAME_ITEM = {0} ", numPedidoExameItem));
                    str.AppendLine(" AND A.IDF_SITUACAO IN (3,4) ");
                    str.AppendLine(" AND A.NUM_EXAME = B.NUM_EXAME ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter atuação para visualização de laudo eletrocardiograma.
        /// </summary>        
        public DataView ObterAtuacaoParaVisualizacaoDeLaudoEletrocardiograma(Int64 numPedidoExameItem)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT '<b>' || D.NOM_PRIVILEGIO || ':</b> ' || C.NOM_PROFISSIONAL || ' ' || C.SBN_PROFISSIONAL || DECODE(C.NUM_DOC_PROFISSIONAL,NULL,'',' - CRM: ') || C.NUM_DOC_PROFISSIONAL ATUACAO ");
                    str.AppendLine(" FROM EXAME_HC A, ");
                    str.AppendLine("      CLASSIF_PROF_EXAME_HC B, ");
                    str.AppendLine("      PROFISSIONAL C, ");
                    str.AppendLine("      PRIVILEGIO_HC D ");
                    str.AppendLine(string.Format(" WHERE A.NUM_PEDIDO_EXAME_ITEM = {0} ", numPedidoExameItem));
                    str.AppendLine(" AND A.IDF_SITUACAO IN (3,4) ");
                    str.AppendLine(" AND A.NUM_EXAME = B.NUM_EXAME ");
                    str.AppendLine(" AND B.COD_PROFISSIONAL = C.COD_PROFISSIONAL ");
                    str.AppendLine(" AND B.COD_PRIVILEGIO = D.COD_PRIVILEGIO ");
                    str.AppendLine(" ORDER BY D.COD_PRIVILEGIO ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter de exame hc para visualização de laudo exame ecofetal.
        /// </summary>        
        public DataView ObterDadosExameHCParaVisualizacaoDeLaudoExameEcofetal(Int64 numPedidoExameItem)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT A.NUM_EXAME, ");
                    str.AppendLine("    A.NUM_ID_EXAME, ");
                    str.AppendLine("    'RELATÓRIO DE ECOCARDIOGRAMA FETAL' DSC_TITULO_RELATORIO, ");
                    str.AppendLine("    A.DTA_HOR_EXAME, ");
                    str.AppendLine("    G.NOM_PROCEDIMENTO_HC, ");
                    str.AppendLine("    A.COD_INDICACAO, ");
                    str.AppendLine("    SUBSTR(FNC_CALCULA_TEMPO(A.DTA_HOR_EXAME, ");
                    str.AppendLine("    F.DTA_NASCIMENTO,'aammddhhmiss'),1,240) IDADE, ");
                    str.AppendLine("    A.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" FROM EXAME_HC A, ");
                    str.AppendLine("    EXAME_ITEM_HC B, ");
                    str.AppendLine("    PEDIDO_EXAME_ITEM_AGENDA_HC H, ");
                    str.AppendLine("    PEDIDO_EXAME_ITEM_HC D, ");
                    str.AppendLine("    PEDIDO_EXAME_HC E, ");
                    str.AppendLine("    PACIENTE F, ");
                    str.AppendLine("    PROCEDIMENTO_HC G ");
                    str.AppendLine(" WHERE ");
                    str.AppendLine(string.Format(" A.NUM_PEDIDO_EXAME_ITEM = {0} ", numPedidoExameItem));
                    str.AppendLine(" AND A.NUM_EXAME = B.NUM_EXAME ");
                    str.AppendLine(" AND B.NUM_ORDEM = 1 ");
                    str.AppendLine(" AND A.NUM_PEDIDO_EXAME_ITEM = H.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" AND H.NUM_PEDIDO_EXAME_ITEM = D.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" AND D.NUM_PEDIDO = E.NUM_PEDIDO ");
                    str.AppendLine(" AND E.COD_PACIENTE = F.COD_PACIENTE ");
                    str.AppendLine(" AND B.COD_PROCEDIMENTO_HC =G.COD_PROCEDIMENTO_HC ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter dados do laudo para visualização de laudo exame ecofetal.
        /// </summary>        
        public DataView ObterDadosDoLaudoParaVisualizacaoDeLaudoExameEcofetal(Int64 numExame, Int64 feto)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT A.NUM_EXAME, ");
                    str.AppendLine("    A.DTA_HOR_EXAME, ");
                    str.AppendLine("    A.NUM_ID_EXAME, ");
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=302 AND NUM_GRUPO_INFORMACAO = {0}) POSICAO_CARDIACA, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=303 AND NUM_GRUPO_INFORMACAO = {0}) SITUS, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=304 AND NUM_GRUPO_INFORMACAO = {0}) JUNCAO_VENO_ATRIAL, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=305 AND NUM_GRUPO_INFORMACAO = {0}) TIPO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=306 AND NUM_GRUPO_INFORMACAO = {0}) MODO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=307 AND NUM_GRUPO_INFORMACAO = {0}) JUNCAO_VENTRICULO_ARTERIAL, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=308 AND NUM_GRUPO_INFORMACAO = {0}) ATRIO_DIREITO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=309 AND NUM_GRUPO_INFORMACAO = {0}) ATRIO_ESQUERDO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=310 AND NUM_GRUPO_INFORMACAO = {0}) SEPTO_INTERATRIAL, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=311 AND NUM_GRUPO_INFORMACAO = {0}) VENTRICULO_ESQUERDO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=312 AND NUM_GRUPO_INFORMACAO = {0}) VENTRICULO_DIREITO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=313 AND NUM_GRUPO_INFORMACAO = {0}) SEPTO_INTERVENTRICULAR, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=314 AND NUM_GRUPO_INFORMACAO = {0}) VALVA_MITRAL, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=315 AND NUM_GRUPO_INFORMACAO = {0}) VALVA_TRICUSPIDE, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=317 AND NUM_GRUPO_INFORMACAO = {0}) VALVA_AORTICA, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=318 AND NUM_GRUPO_INFORMACAO = {0}) VALVA_PULMONAR, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=319 AND NUM_GRUPO_INFORMACAO = {0}) VALVA_ARCO_DUCTAL, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=321 AND NUM_GRUPO_INFORMACAO = {0}) VALVA_ARCO_AORTICO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=322 AND NUM_GRUPO_INFORMACAO = {0}) RITMO_CARDIACO, ", feto));
                    str.AppendLine("    NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 88 AND NUM_GRUPO_INFORMACAO = 1), ");
                    str.AppendLine("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 88 AND NUM_GRUPO_INFORMACAO = 1 )) CONCLUSAO, ");
                    str.AppendLine("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 1647 AND NUM_GRUPO_INFORMACAO = 1) OBSERVACAO_GO ");
                    str.AppendLine(" FROM EXAME_HC A ");
                    str.AppendLine(string.Format(" WHERE A.NUM_EXAME = {0} ", numExame));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter dados de exame hc para visualização de laudo exame gemelar.
        /// </summary>        
        public DataView ObterDadosDeExameHCParaVisualizacaoDeLaudoExameGemear(Int64 numPedidoExameItem)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT A.NUM_EXAME, ");
                    str.AppendLine("    A.NUM_ID_EXAME, 'LAUDO DE ULTRA-SONOGRAFIA GEMELAR' DSC_TITULO_RELATORIO, ");
                    str.AppendLine("    A.DTA_HOR_EXAME, ");
                    str.AppendLine("    G.NOM_PROCEDIMENTO_HC, ");
                    str.AppendLine("    SUBSTR(FNC_CALCULA_TEMPO(A.DTA_HOR_EXAME ,F.DTA_NASCIMENTO,'aammddhhmiss'),1,240) IDADE, ");
                    str.AppendLine("    A.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" FROM EXAME_HC A, ");
                    str.AppendLine("    EXAME_ITEM_HC B, ");
                    str.AppendLine("    PEDIDO_EXAME_ITEM_AGENDA_HC H, ");
                    str.AppendLine("    PEDIDO_EXAME_ITEM_HC D, ");
                    str.AppendLine("    PEDIDO_EXAME_HC E, ");
                    str.AppendLine("    PACIENTE F, ");
                    str.AppendLine("    PROCEDIMENTO_HC G ");
                    str.AppendLine(string.Format(" WHERE A.NUM_PEDIDO_EXAME_ITEM = {0} ", numPedidoExameItem));
                    str.AppendLine(" AND A.NUM_EXAME = B.NUM_EXAME ");
                    str.AppendLine(" AND B.NUM_ORDEM = 1 ");
                    str.AppendLine(" AND A.NUM_PEDIDO_EXAME_ITEM = H.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" AND H.NUM_PEDIDO_EXAME_ITEM = D.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" AND D.NUM_PEDIDO = E.NUM_PEDIDO ");
                    str.AppendLine(" AND E.COD_PACIENTE = F.COD_PACIENTE ");
                    str.AppendLine(" AND B.COD_PROCEDIMENTO_HC = G.COD_PROCEDIMENTO_HC ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter dados complementar de exame hc para visualização de laudo exame gemelar.
        /// </summary>        
        public DataView ObterDadosComplementarDeExameHCParaVisualizacaoDeLaudoExameGemear(Int64 numPedidoExame)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT A.NUM_EXAME, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 12','|') dum, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 13','|') ta_semanas, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 14','|') ta_dias, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 16','|') qtd_fetos, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 17','|') espessura, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 27','|') grau, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 28','|') liq_amniot, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 29','|') ila, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 30','|') arterias, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 31','|') veias, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 33','|') dbp, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 35','|') cc, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 36','|') ca, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 37','|') umero, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 38','|') radio, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 39','|') ulna, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 40','|') femur, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 41','|') tibia, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 42','|') fibula, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 43','|') cerebelo, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 44','|') dbo, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 45','|') dio, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 46','|') valor_do, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 47','|') ic, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 48','|') cc_ca, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 49','|') f_ca, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 50','|') peso, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 88','|') par_conclusao_morfologico, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 125','|') par_morf, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 298','|') placenta, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 299','|') posicao, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 300','|') amnio, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 301','|') ecocardiografia, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 491','|') situacao, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 492','|') apresentacao, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 493','|') mov_cardiaco, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 494','|') tronco, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 496','|') mov_respirat, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 497','|') tonus, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 498','|') pbf, ");
                    str.AppendLine("    FCN_LINHA_COLUNA('SELECT CTU_INFORMACAO ");
                    str.AppendLine("    FROM EXAME_INFORMACAO_HC ");
                    str.AppendLine("    WHERE NUM_EXAME = ' || A.NUM_EXAME || ' AND COD_TIPO_INFORMACAO = 499','|') sexo_provavel ");
                    str.AppendLine(" FROM EXAME_HC A ");
                    str.AppendLine(string.Format(" WHERE  A.NUM_EXAME = {0} ", numPedidoExame));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter dados exame hc para visualização de laudo exame morfologico.
        /// </summary>        
        public DataView ObterDadosExameHCParaVisualizacaoDeLaudoExameMorfologico(Int64 numPedidoExame)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();                   

                    str.AppendLine(" SELECT A.NUM_EXAME, ");
                    str.AppendLine(" A.NUM_ID_EXAME, ");
                    str.AppendLine(" 'RELATÓRIO DE ULTRA-SONOGRAFIA MORFOLÓGICA FETAL' DSC_TITULO_RELATORIO, ");
                    str.AppendLine(" A.DTA_HOR_EXAME, ");
                    str.AppendLine(" G.NOM_PROCEDIMENTO_HC, ");
                    str.AppendLine(" A.COD_INDICACAO, ");
                    str.AppendLine(" SUBSTR(FNC_CALCULA_TEMPO(A.DTA_HOR_EXAME ,F.DTA_NASCIMENTO,'aammddhhmiss'),1,240) IDADE, ");
                    str.AppendLine(" A.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" FROM EXAME_HC A, ");
                    str.AppendLine(" EXAME_ITEM_HC B, ");
                    str.AppendLine(" PEDIDO_EXAME_ITEM_AGENDA_HC H, ");
                    str.AppendLine(" PEDIDO_EXAME_ITEM_HC D, ");
                    str.AppendLine(" PEDIDO_EXAME_HC E, ");
                    str.AppendLine(" PACIENTE F, ");
                    str.AppendLine(" PROCEDIMENTO_HC G ");
                    str.AppendLine(string.Format(" WHERE A.NUM_PEDIDO_EXAME_ITEM = {0} ", numPedidoExame));
                    str.AppendLine(" AND A.NUM_EXAME = B.NUM_EXAME ");
                    str.AppendLine(" AND B.NUM_ORDEM = 1 ");
                    str.AppendLine(" AND A.NUM_PEDIDO_EXAME_ITEM = H.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" AND H.NUM_PEDIDO_EXAME_ITEM = D.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" AND D.NUM_PEDIDO = E.NUM_PEDIDO ");
                    str.AppendLine(" AND E.COD_PACIENTE = F.COD_PACIENTE ");
                    str.AppendLine(" AND B.COD_PROCEDIMENTO_HC =G.COD_PROCEDIMENTO_HC ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter dados complementar de exame hc para visualização de laudo exame.
        /// </summary>        
        public DataView ObterDadosComplementarDeExameHCParaVisualizacaoDeLaudoExame(Int64 numExame, Int64 feto)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();                   

                    str.AppendLine(" SELECT A.NUM_EXAME, A.DTA_HOR_EXAME, A.NUM_ID_EXAME, ");
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=12 AND NUM_GRUPO_INFORMACAO = {0}) DUM, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=13 AND NUM_GRUPO_INFORMACAO = {0}) TA_SEM, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=14 AND NUM_GRUPO_INFORMACAO = {0}) TA_DIAS, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=15 AND NUM_GRUPO_INFORMACAO = {0}) SITUACAO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=16 AND NUM_GRUPO_INFORMACAO = {0}) QTDE_FETOS, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=17 AND NUM_GRUPO_INFORMACAO = {0}) ESP_PLACENTA, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=19 AND NUM_GRUPO_INFORMACAO = {0}) APRESENTACAO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=20 AND NUM_GRUPO_INFORMACAO = {0}) POSICAO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=21 AND NUM_GRUPO_INFORMACAO = {0}) MOV_CARDIACOS, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=22 AND NUM_GRUPO_INFORMACAO = {0}) MOV_RESPIRAT, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=23 AND NUM_GRUPO_INFORMACAO = {0}) TRONCO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=24 AND NUM_GRUPO_INFORMACAO = {0}) TONUS, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=25 AND NUM_GRUPO_INFORMACAO = {0}) PBF, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=27 AND NUM_GRUPO_INFORMACAO = {0}) GRAU, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=28 AND NUM_GRUPO_INFORMACAO = {0}) VOLUME_US, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=29 AND NUM_GRUPO_INFORMACAO = {0}) ILA, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=30 AND NUM_GRUPO_INFORMACAO = {0}) ARTERIAS, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=31 AND NUM_GRUPO_INFORMACAO = {0}) VEIAS, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=32 AND NUM_GRUPO_INFORMACAO = {0}) SEXO_PROVAVEL, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=33 AND NUM_GRUPO_INFORMACAO = {0}) DBP, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=34 AND NUM_GRUPO_INFORMACAO = {0}) DOF, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=35 AND NUM_GRUPO_INFORMACAO = {0}) CC, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=36 AND NUM_GRUPO_INFORMACAO = {0}) CA, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=37 AND NUM_GRUPO_INFORMACAO = {0}) UMERO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=38 AND NUM_GRUPO_INFORMACAO = {0}) RADIO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=39 AND NUM_GRUPO_INFORMACAO = {0}) ULNA, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=40 AND NUM_GRUPO_INFORMACAO = {0}) FEMUR, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=41 AND NUM_GRUPO_INFORMACAO = {0}) TIBIA, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=42 AND NUM_GRUPO_INFORMACAO = {0}) FIBULA, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=43 AND NUM_GRUPO_INFORMACAO = {0}) CEREBELO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=44 AND NUM_GRUPO_INFORMACAO = {0}) DBO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=45 AND NUM_GRUPO_INFORMACAO = {0}) DIO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=46 AND NUM_GRUPO_INFORMACAO = {0}) DO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=47 AND NUM_GRUPO_INFORMACAO = {0}) IC, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=48 AND NUM_GRUPO_INFORMACAO = {0}) CC_CA, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=49 AND NUM_GRUPO_INFORMACAO = {0}) F_CA, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=50 AND NUM_GRUPO_INFORMACAO = {0}) PESO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=298 AND NUM_GRUPO_INFORMACAO = {0}) NRO_PLACENTA, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=300 AND NUM_GRUPO_INFORMACAO = {0}) NRO_AMNIO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=520 AND NUM_GRUPO_INFORMACAO = {0}) MAIOR_BOLSAO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=467 AND NUM_GRUPO_INFORMACAO = {0}) ART_UTERINA_DIR_INC, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=468 AND NUM_GRUPO_INFORMACAO = {0}) ART_UTERINA_DIR_IR, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=469 AND NUM_GRUPO_INFORMACAO = {0}) ART_UTERINA_DIR_IP, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=470 AND NUM_GRUPO_INFORMACAO = {0}) ART_UTERINA_ESQ_INC, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=471 AND NUM_GRUPO_INFORMACAO = {0}) ART_UTERINA_ESQ_IR, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=472 AND NUM_GRUPO_INFORMACAO = {0}) ART_UTERINA_ESQ_IP, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=473 AND NUM_GRUPO_INFORMACAO = {0}) ART_UMBILICAL, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=474 AND NUM_GRUPO_INFORMACAO = {0}) ART_CEREBRAL_IR, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=475 AND NUM_GRUPO_INFORMACAO = {0}) ART_CEREBRAL_PVS, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=476 AND NUM_GRUPO_INFORMACAO = {0}) DUCTO_VENOSO_PULSAT, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=477 AND NUM_GRUPO_INFORMACAO = {0}) SISTOLEATRIAL, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=478 AND NUM_GRUPO_INFORMACAO = {0}) PULMAO_FIGADO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=480 AND NUM_GRUPO_INFORMACAO = {0}) PROXIMAL_UMERO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=481 AND NUM_GRUPO_INFORMACAO = {0}) DISTAL_FEMUR, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=482 AND NUM_GRUPO_INFORMACAO = {0}) PROXIMAL_TIBIA, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=483 AND NUM_GRUPO_INFORMACAO = {0}) MECONIO, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1641 AND NUM_GRUPO_INFORMACAO = {0}) DT_ULT_US, ", feto));
                    str.AppendLine(string.Format(" NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 88 AND NUM_GRUPO_INFORMACAO = {0}), ", feto));
                    str.AppendLine(string.Format(" (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 88 AND NUM_GRUPO_INFORMACAO = {0})) CONCLUSAO, ", feto));
                    str.AppendLine(string.Format(" NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 125 AND NUM_GRUPO_INFORMACAO = {0}), ", feto));
                    str.AppendLine(string.Format("(SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 125 AND NUM_GRUPO_INFORMACAO = {0})) MORFOLOGIA, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1643 AND NUM_GRUPO_INFORMACAO = {0}) ID_GEST_INF_SEM, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1644 AND NUM_GRUPO_INFORMACAO = {0}) ID_GEST_INF_DIAS, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1645 AND NUM_GRUPO_INFORMACAO = {0}) ID_GEST_CALC_SEM, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1646 AND NUM_GRUPO_INFORMACAO = {0}) ID_GEST_CALC_DIAS, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1884 AND NUM_GRUPO_INFORMACAO = {0}) FOSSA_POSTERIOR, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1885 AND NUM_GRUPO_INFORMACAO = {0}) PREGA_NUCAL, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1886 AND NUM_GRUPO_INFORMACAO = {0}) ATRIO_VENTRICULAR, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1887 AND NUM_GRUPO_INFORMACAO = {0}) OSSO_NASAL, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1888 AND NUM_GRUPO_INFORMACAO = {0}) GOLF_BALL, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1889 AND NUM_GRUPO_INFORMACAO = {0}) PELVE_RENAL_D, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1890 AND NUM_GRUPO_INFORMACAO = {0}) PELVE_RENAL_E, ", feto));
                    str.AppendLine(string.Format(" (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1891 AND NUM_GRUPO_INFORMACAO = {0}) INTESTINO, ", feto));
                    str.AppendLine(" NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 1647 AND NUM_GRUPO_INFORMACAO = 1), ");
                    str.AppendLine(" (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 1647 AND NUM_GRUPO_INFORMACAO = 1)) OBSERVACAO_GO ");

                    str.AppendLine(" FROM EXAME_HC A ");
                    str.AppendLine(string.Format(string.Format(" WHERE A.NUM_EXAME = {0} ", numExame)));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter dados exame hc para visualização de laudo exame obstetrico.
        /// </summary>        
        public DataView ObterDadosExameHCParaVisualizacaoDeLaudoExameObstetrico(Int64 numPedidoExameItem)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();                    

                    str.AppendLine(" SELECT A.NUM_EXAME, ");
                    str.AppendLine(" A.NUM_ID_EXAME, 'RELATÓRIO DE ULTRA-SONOGRAFIA OBSTÉTRICA' DSC_TITULO_RELATORIO, ");
                    str.AppendLine(" A.DTA_HOR_EXAME, ");
                    str.AppendLine(" G.NOM_PROCEDIMENTO_HC, ");
                    str.AppendLine(" A.COD_INDICACAO, ");
                    str.AppendLine(" SUBSTR(FNC_CALCULA_TEMPO(A.DTA_HOR_EXAME ,F.DTA_NASCIMENTO,'aammddhhmiss'),1,240) IDADE, ");
                    str.AppendLine(" A.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" FROM EXAME_HC A, ");
                    str.AppendLine(" EXAME_ITEM_HC B, ");
                    str.AppendLine(" PEDIDO_EXAME_ITEM_AGENDA_HC H, ");
                    str.AppendLine(" PEDIDO_EXAME_ITEM_HC D, ");
                    str.AppendLine(" PEDIDO_EXAME_HC E, ");
                    str.AppendLine(" PACIENTE F, ");
                    str.AppendLine(" PROCEDIMENTO_HC G ");
                    str.AppendLine(string.Format(" WHERE A.NUM_PEDIDO_EXAME_ITEM = {0} ", numPedidoExameItem));
                    str.AppendLine(" AND A.NUM_EXAME = B.NUM_EXAME ");
                    str.AppendLine(" AND B.NUM_ORDEM = 1 ");
                    str.AppendLine(" AND A.NUM_PEDIDO_EXAME_ITEM = H.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" AND H.NUM_PEDIDO_EXAME_ITEM = D.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" AND D.NUM_PEDIDO = E.NUM_PEDIDO ");
                    str.AppendLine(" AND E.COD_PACIENTE = F.COD_PACIENTE ");
                    str.AppendLine(" AND B.COD_PROCEDIMENTO_HC =G.COD_PROCEDIMENTO_HC ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter dados exame hc para visualização de laudo exame pelvico.
        /// </summary>        
        public DataView ObterDadosExameHCParaVisualizacaoDeLaudoExamePelvico(Int64 numPedidoExameItem)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();                    

                    str.AppendLine(" SELECT A.NUM_EXAME, ");
                    str.AppendLine("    A.NUM_ID_EXAME, ");
                    str.AppendLine("    'LAUDO DE ULTRA-SOM PÉLVICO' DSC_TITULO_RELATORIO, ");
                    str.AppendLine("    A.DTA_HOR_EXAME, ");
                    str.AppendLine("    G.NOM_PROCEDIMENTO_HC, ");
                    str.AppendLine("    SUBSTR(FNC_CALCULA_TEMPO(A.DTA_HOR_EXAME, ");
                    str.AppendLine("    F.DTA_NASCIMENTO,'aammddhhmiss'),1,240) IDADE, ");
                    str.AppendLine("    A.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" FROM   EXAME_HC A, ");
                    str.AppendLine("    EXAME_ITEM_HC B, ");
                    str.AppendLine("    PEDIDO_EXAME_ITEM_AGENDA_HC H, ");
                    str.AppendLine("    PEDIDO_EXAME_ITEM_HC D, ");
                    str.AppendLine("    PEDIDO_EXAME_HC E, ");
                    str.AppendLine("    PACIENTE F, ");
                    str.AppendLine("    PROCEDIMENTO_HC G ");
                    str.AppendLine(string.Format(" WHERE A.NUM_PEDIDO_EXAME_ITEM = {0} ", numPedidoExameItem));
                    str.AppendLine(" AND A.NUM_EXAME = B.NUM_EXAME ");
                    str.AppendLine(" AND B.NUM_ORDEM = 1 ");
                    str.AppendLine(" AND A.NUM_PEDIDO_EXAME_ITEM = H.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" AND H.NUM_PEDIDO_EXAME_ITEM = D.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" AND D.NUM_PEDIDO = E.NUM_PEDIDO ");
                    str.AppendLine(" AND E.COD_PACIENTE = F.COD_PACIENTE ");
                    str.AppendLine(" AND B.COD_PROCEDIMENTO_HC =G.COD_PROCEDIMENTO_HC ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter dados exame hc complementares para visualização de laudo exame pelvico.
        /// </summary>        
        public DataView ObterDadosExameHCComplementaresParaVisualizacaoDeLaudoExamePelvico(Int64 numExame)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();                   

                    str.AppendLine(" SELECT A.NUM_EXAME, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 12) DTA_ULT_MENS, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 54) EXM_BIDIM, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 55) FREQUENCIA, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 56) UTERO, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 93) CONTORNOS, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 94) MEDIDAT_MENS, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 95) NORMAL_UTERO, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 107) MED_ANT_POST_UTERO, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 108) MED_TRANSV_UTERO, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 115) MED_LONG_OVA_DIR, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 116) MED_ANT_POST_OVA_DIR, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 117) MED_TRANS_OVA_DIR, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 118) MED_LONG_OVA_ESQ, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 119) MED_ANT_POST_OVA_ESQ, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 120) MED_TRANS_OVA_ESQ, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 123) NORMAL_OVA_DIR, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 124) NORMAL_OVA_ESQ, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 232) VOL_UTERO, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 233) VOL_OVA_DIR, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 234) VOL_OVA_ESQ, ");

                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 1875) ANO_ULT_MENS, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 1876) OV_DIR_LOCAL, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 1877) OV_DIR_TEXTURA, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 1878) OV_ESQ_LOCAL, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 1879) OV_ESQ_TEXTURA, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 468) ART_UT_DIR_IR, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 469) ART_UT_DIR_IP, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 471) ART_UT_ESQ_IR, ");
                    str.AppendLine("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 472) ART_UT_ESQ_IP, ");

                    str.AppendLine("    NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 57), ");
                    str.AppendLine("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 57)) COLO_UTERINO, ");
                    str.AppendLine("    NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 82), ");
                    str.AppendLine("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 82)) BEXIGA, ");
                    str.AppendLine("    NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 83), ");
                    str.AppendLine("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 83)) VAGINA, ");
                    str.AppendLine("    NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 84), ");
                    str.AppendLine("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 84)) TEXTURA_MIOMETRIAL, ");
                    str.AppendLine("    NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 85), ");
                    str.AppendLine("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 85)) CARAC_ENDOMETRIO, ");
                    str.AppendLine("    NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 86), ");
                    str.AppendLine("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 86)) OVARIO_DIREITO, ");
                    str.AppendLine("    NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 87), ");
                    str.AppendLine("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 87)) OVARIO_ESQUERDO, ");
                    str.AppendLine("    NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 96), ");
                    str.AppendLine("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 96)) ECOGENECIDADE_PELVICA, ");
                    str.AppendLine("    NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 231), ");
                    str.AppendLine("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 231)) FUNDO_SACO_DOUGLAS, ");
                    str.AppendLine("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 89) OBSERVACAO, ");
                    str.AppendLine("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 235) CONCLUSAO ");
                    str.AppendLine(" FROM   EXAME_HC A ");
                    str.AppendLine(string.Format(" WHERE  A.NUM_EXAME = {0} ", numExame));                    

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter dados exame hc para visualização de laudo exame primeiro trimestre.
        /// </summary>        
        public DataView ObterDadosExameHCParaVisualizacaoDeLaudoExamePrimeiroTrimestre(Int64 numPedidoExameItem)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();                   

                    str.AppendLine(" SELECT A.NUM_EXAME, ");
                    str.AppendLine("    A.NUM_ID_EXAME, 'RELATÓRIO DE ULTRA-SONOGRAFIA PRIMEIRO TRIMESTRE' DSC_TITULO_RELATORIO, ");
                    str.AppendLine("    A.DTA_HOR_EXAME, ");
                    str.AppendLine("    G.NOM_PROCEDIMENTO_HC, ");
                    str.AppendLine("    A.COD_INDICACAO, ");
                    str.AppendLine("    SUBSTR(FNC_CALCULA_TEMPO(A.DTA_HOR_EXAME, ");
                    str.AppendLine("    F.DTA_NASCIMENTO,'aammddhhmiss'),1,240) IDADE ");
                    str.AppendLine(" FROM EXAME_HC A, ");
                    str.AppendLine("    EXAME_ITEM_HC B, ");
                    str.AppendLine("    PEDIDO_EXAME_ITEM_AGENDA_HC H, ");
                    str.AppendLine("    PEDIDO_EXAME_ITEM_HC D, ");
                    str.AppendLine("    PEDIDO_EXAME_HC E, ");
                    str.AppendLine("    PACIENTE F, ");
                    str.AppendLine("    PROCEDIMENTO_HC G ");
                    str.AppendLine(string.Format(" WHERE A.NUM_PEDIDO_EXAME_ITEM = {0} ", numPedidoExameItem));
                    str.AppendLine("    AND A.NUM_EXAME = B.NUM_EXAME ");
                    str.AppendLine("    AND B.NUM_ORDEM = 1 ");
                    str.AppendLine("    AND A.NUM_PEDIDO_EXAME_ITEM = H.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine("    AND H.NUM_PEDIDO_EXAME_ITEM = D.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine("    AND D.NUM_PEDIDO = E.NUM_PEDIDO ");
                    str.AppendLine("    AND E.COD_PACIENTE = F.COD_PACIENTE ");
                    str.AppendLine("    AND B.COD_PROCEDIMENTO_HC =G.COD_PROCEDIMENTO_HC ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter dados exame hc complementares para visualização de laudo exame primeiro trimestre.
        /// </summary>        
        public DataView ObterDadosExameHCComplementaresParaVisualizacaoDeLaudoExamePrimeiroTrimestre(Int64 numExame, Int64 feto)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();                  

                    str.AppendLine(" SELECT A.NUM_EXAME, ");
                    str.AppendLine("        A.DTA_HOR_EXAME, ");
                    str.AppendLine("        A.NUM_ID_EXAME, ");
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=12 AND NUM_GRUPO_INFORMACAO = {0}) DUM, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=13 AND NUM_GRUPO_INFORMACAO = {0}) TA_SEM, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=14 AND NUM_GRUPO_INFORMACAO = {0}) TA_DIAS, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=16 AND NUM_GRUPO_INFORMACAO = {0}) QTDE_FETOS, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=273 AND NUM_GRUPO_INFORMACAO = {0}) PRE_NATAL, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=274 AND NUM_GRUPO_INFORMACAO = {0}) MODO, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=275 AND NUM_GRUPO_INFORMACAO = {0}) EQUIPAMENTO, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=287 AND NUM_GRUPO_INFORMACAO = {0}) FREQUENCIA, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=276 AND NUM_GRUPO_INFORMACAO = {0}) UTERO, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=277 AND NUM_GRUPO_INFORMACAO = {0}) COLO_UTERINO, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=278 AND NUM_GRUPO_INFORMACAO = {0}) OVARIOS, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=279 AND NUM_GRUPO_INFORMACAO = {0}) UTERO_GRAVIDICO, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=280 AND NUM_GRUPO_INFORMACAO = {0}) UTERO_VILOSIDADES, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=281 AND NUM_GRUPO_INFORMACAO = {0}) CCN, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=282 AND NUM_GRUPO_INFORMACAO = {0}) BATIMENTO_CARDIACO, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=286 AND NUM_GRUPO_INFORMACAO = {0}) PROCEDIMENTO_COMPLEMENTAR, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=298 AND NUM_GRUPO_INFORMACAO = {0}) NRO_PLACENTA, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=300 AND NUM_GRUPO_INFORMACAO = {0}) NRO_AMNIO, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=484 AND NUM_GRUPO_INFORMACAO = {0}) ENCAMINHADO, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=485 AND NUM_GRUPO_INFORMACAO = {0}) COMPRIMENTO_COLO, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=486 AND NUM_GRUPO_INFORMACAO = {0}) OVARIOS_UTERO, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=487 AND NUM_GRUPO_INFORMACAO = {0}) OSSO_NASAL, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=488 AND NUM_GRUPO_INFORMACAO = {0}) DUCTO_VENOSO, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=489 AND NUM_GRUPO_INFORMACAO = {0}) PROCEDIMENTO, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=490 AND NUM_GRUPO_INFORMACAO = {0}) TRANSLUCENCIA_NUCAL, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1641 AND NUM_GRUPO_INFORMACAO = {0}) DT_ULT_US, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1643 AND NUM_GRUPO_INFORMACAO = {0}) ID_GEST_INF_SEM, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1644 AND NUM_GRUPO_INFORMACAO = {0}) ID_GEST_INF_DIAS, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1645 AND NUM_GRUPO_INFORMACAO = {0}) ID_GEST_CALC_SEM, ", feto));
                    str.AppendLine(string.Format("    (SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO=1646 AND NUM_GRUPO_INFORMACAO = {0}) ID_GEST_CALC_DIAS, ", feto));
                    str.AppendLine(string.Format("    NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 285 AND NUM_GRUPO_INFORMACAO = {0}), ", feto));
                    str.AppendLine(string.Format("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 285 AND NUM_GRUPO_INFORMACAO = {0})) OUTRAS_MEDIDAS, ", feto));
                    str.AppendLine(string.Format("    NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 89 AND NUM_GRUPO_INFORMACAO = {0}), ", feto));
                    str.AppendLine(string.Format("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 89 AND NUM_GRUPO_INFORMACAO = {0})) OBSERVACAO, ", feto));
                    str.AppendLine(string.Format("    NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 88 AND NUM_GRUPO_INFORMACAO = {0}), ", feto));
                    str.AppendLine(string.Format("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 88 AND NUM_GRUPO_INFORMACAO = {0})) CONCLUSAO, ", feto));

                    str.AppendLine("    NVL((SELECT CTU_INFORMACAO FROM EXAME_INFORMACAO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 1647 AND NUM_GRUPO_INFORMACAO = 1), ");
                    str.AppendLine("    (SELECT DBMS_LOB.SUBSTR(DSC_EXAME_LAUDO, DBMS_LOB.GETLENGTH(DSC_EXAME_LAUDO), 1) FROM EXAME_LAUDO_HC WHERE NUM_EXAME = A.NUM_EXAME AND COD_TIPO_INFORMACAO = 1647 AND NUM_GRUPO_INFORMACAO = 1)) OBSERVACAO_GO ");
                    str.AppendLine(" FROM EXAME_HC A ");
                    str.AppendLine(string.Format(" WHERE A.NUM_EXAME = {0} ", numExame));                    

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter dados exame hc para visualização de laudo neurof.
        /// </summary>        
        public DataView ObterDadosExameHCParaVisualizacaoDeLaudoNeurof(Int64 numPedidoExameItem)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();                   

                    str.AppendLine(" SELECT A.NUM_EXAME, ");
                    str.AppendLine("    A.NUM_ID_EXAME, ");
                    str.AppendLine("    '' DSC_TITULO_RELATORIO, ");
                    str.AppendLine("    A.DTA_HOR_EXAME, ");
                    str.AppendLine("    SUBSTR(FNC_CALCULA_TEMPO(A.DTA_HOR_EXAME, ");
                    str.AppendLine("    E.DTA_NASCIMENTO,'aammddhhmiss'),1,240) IDADE, ");
                    str.AppendLine("    C.SEQ_EXAME_HC_ARQUIVO, ");
                    str.AppendLine("    P.NOM_PROCEDIMENTO_HC, ");
                    str.AppendLine("    Trim(REPLACE(L.DSC_EXAME_LAUDO,chr(10),'<br>')) DSC_EXAME_LAUDO, ");
                    str.AppendLine("    L.NUM_ORDEM, ");
                    str.AppendLine("    A.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" FROM EXAME_HC A, ");
                    str.AppendLine("    PEDIDO_EXAME_ITEM_HC B, ");
                    str.AppendLine("    EXAME_HC_ARQUIVO C, ");
                    str.AppendLine("    PEDIDO_EXAME_HC D, ");
                    str.AppendLine("    PACIENTE E, ");
                    str.AppendLine("    PROCEDIMENTO_HC P, ");
                    str.AppendLine("    EXAME_LAUDO_HC L   ");
                    str.AppendLine(string.Format(" WHERE A.NUM_PEDIDO_EXAME_ITEM = {0} ", numPedidoExameItem));
                    str.AppendLine(" AND A.NUM_EXAME = C.NUM_EXAME(+) ");
                    str.AppendLine(" AND A.NUM_PEDIDO_EXAME_ITEM = B.NUM_PEDIDO_EXAME_ITEM ");
                    str.AppendLine(" AND B.NUM_PEDIDO = D.NUM_PEDIDO ");
                    str.AppendLine(" AND D.COD_PACIENTE = E.COD_PACIENTE ");
                    str.AppendLine(" AND B.COD_PROCEDIMENTO_HC = P.COD_PROCEDIMENTO_HC ");
                    str.AppendLine(" AND A.NUM_EXAME = L.NUM_EXAME ");                    

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter dados do resultado do exames de acordo com o NumPedidoExameItem
        /// </summary>        
        public string ObterDadosResultadoExameHC(Int64 numPedidoExameItem)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(string.Format(" SELECT FCN_RESULTADO_EXAME_RESUMIDO({0}) DSC_RESULTADO_EXAME FROM DUAL ", numPedidoExameItem));
                    
                    
                    query = new QueryCommandConfig(str.ToString());
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;
                    string result = "";
                    try
                    {
                        dr.Read();

                        result = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "DSC_RESULTADO_EXAME");

                    }
                    finally
                    {
                        dr.Close();
                        dr.Dispose();
                    }

                    return result;
                }
            }
            catch 
            {
                return "";
            }
        }

        /// <summary>
        /// Obter menu.
        /// </summary>        
        public DataTable ObterExamesRealizadosPaciente(decimal codTipoPaciente, string codPaciente, DateTime dataInicial, DateTime dataFinal, decimal codServico, decimal codTipoOrdenacao)
        {
            DataTable listaRetorno = new DataTable();
            
            try
            {
                using (Contexto ctx = new Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    using (OracleDataAdapter com = new OracleDataAdapter())
                    {
                        com.SelectCommand = new OracleCommand();
                        com.SelectCommand.Connection = ctx.Conn as OracleConnection;
                        com.SelectCommand.CommandType = CommandType.StoredProcedure;
                        com.SelectCommand.CommandText = "GO.PROC_EXAME_PACIENTE_HC";


                        com.SelectCommand.Parameters.Add("V_TIPO_PACIENTE", OracleDbType.Decimal, ParameterDirection.Input).Value = codTipoPaciente;

                        com.SelectCommand.Parameters.Add("V_COD_PACIENTE", OracleDbType.NVarchar2, ParameterDirection.Input).Value = codPaciente;
                        com.SelectCommand.Parameters.Add("V_DTA_INICIAL", OracleDbType.Date, ParameterDirection.Input).Value = dataInicial;

                        if (!dataFinal.Equals(DateTime.MinValue))
                            com.SelectCommand.Parameters.Add("V_DTA_FINAL", OracleDbType.Date, ParameterDirection.Input).Value = dataFinal;
                        else
                            com.SelectCommand.Parameters.Add("V_DTA_FINAL", OracleDbType.Date, ParameterDirection.Input).Value = DBNull.Value;

                        if (codServico > 0)
                            com.SelectCommand.Parameters.Add("V_SERVICO", OracleDbType.Decimal, ParameterDirection.Input).Value = codServico;
                        else
                            com.SelectCommand.Parameters.Add("V_SERVICO", OracleDbType.Decimal, ParameterDirection.Input).Value = DBNull.Value;

                        com.SelectCommand.Parameters.Add("V_TIPO_ORDENACAO", OracleDbType.Decimal, ParameterDirection.Input).Value = codTipoOrdenacao;

                        com.SelectCommand.Parameters.Add("CS_EXAMES", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        DataTable dt = new DataTable();

                        com.Fill(dt);

                        com.Dispose();

                        //FILTRA EXAMES PERMITIDOS VISUALIZACAO
                        dt.DefaultView.RowFilter = "IDF_SITUACAO_GERAL = 3";

                        return dt.DefaultView.ToTable();                           
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }            
        }


        public DataView ObterClearanceCreatininaMedido(string codPaciente)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT E.DTA_HOR_EXAME, ");
                    str.AppendLine(" (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                    str.AppendLine("        WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 517 ) VALOR, ");
                    str.AppendLine(" (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                    str.AppendLine(" WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 1087 ) VOLUME --ELEMENTO ");
                    str.AppendLine(" FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E ");
                    str.AppendLine(" WHERE  ");
                    str.AppendLine(" P.COD_PACIENTE = '" + codPaciente + "' AND ");
                    str.AppendLine(" PE.COD_PACIENTE = P.COD_PACIENTE  ");
                    str.AppendLine(" AND PEI.COD_PROCEDIMENTO_HC = 7787 --EXAME CLEARANCE CREATININA ");
                    str.AppendLine(" AND E.COD_CENTRO_CIRURGICO = 142 --LABORATÓRIO ");
                    str.AppendLine(" AND PEI.IDF_SITUACAO = 10 --SITUAÇÃO: ATENDIDO ");
                    str.AppendLine(" AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO  ");
                    str.AppendLine(" AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM   ");
                    str.AppendLine(" AND E.DTA_HOR_EXAME =  ");
                    str.AppendLine(" (SELECT MAX(E2.DTA_HOR_EXAME)FROM EXAME_HC E2, PEDIDO_EXAME_ITEM_HC PEI2, PEDIDO_EXAME_HC PE2 WHERE PE2.COD_PACIENTE = P.COD_PACIENTE AND PEI2.COD_PROCEDIMENTO_HC = 7787 AND PEI2.IDF_SITUACAO = 10  ");
                    str.AppendLine(" AND PE2.NUM_PEDIDO = PEI2.NUM_PEDIDO AND PEI2.NUM_PEDIDO_EXAME_ITEM = E2.NUM_PEDIDO_EXAME_ITEM) ");


                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        /// <summary>
        /// Obter Sorologia
        /// </summary>
        /// <param name="tipo">0 - HBSAG | 1 - Anti-HBS | 2 - Anti-HBC | 3 - Elisa Anti-HCV (HCV) | 4 - Elisa Anti-HIV | 
        /// 5 - VDRL (Sífilis) | 6- Toxoplasmose | 7 - HTLV (Elisa anti-HTLV I/II) | 
        /// 8 - UROCULTURA | 9 - GTT - Glicemia | 10 - Swab Reto-Vaginal |
        /// 11 - Carga Viral</param>
        /// <param name="codPaciente">Código do Paciente</param>
        /// <returns></returns>
        public DataView ObterSorologias(int tipo, string codPaciente)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;
                    
                    ctx.Open();

                    #region HBSAG
                    if (tipo == 0)
                    {
                        str.AppendLine("SELECT DTA_HOR_EXAME,\"HBS AG\" ");
                        str.AppendLine("FROM ( SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, ");
                        str.AppendLine("              DBMS_LOB.substr(VRE.CTU_RESULTADO) \"HBS AG\"      ");
                        str.AppendLine("          FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E,  ");
                        str.AppendLine("               RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("         WHERE P.COD_PACIENTE = '" + codPaciente + "' ");
                        str.AppendLine("           AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("           AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");
                        str.AppendLine("           AND E.NUM_EXAME = REL.NUM_EXAME ");
                        str.AppendLine("           AND REL.COD_TIPO_INFORMACAO= 1212 --ELEMENTO  ");
                        str.AppendLine("           AND PEI.COD_PROCEDIMENTO_HC = 12595 --EXAME SOROLOGIA PARA HEPATITES ");
                        str.AppendLine("           AND E.COD_CENTRO_CIRURGICO = 89 --LABORATÓRIO ");
                        str.AppendLine("           AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("           AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("           AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("          ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1 ");
                    } 
                    #endregion

                    #region Anti-HBS
                    if (tipo == 1)
                    {
                        str.AppendLine("SELECT DTA_HOR_EXAME,\"Elisa anti HBS AG\" ");
                        str.AppendLine("FROM ( SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, ");
                        str.AppendLine("              DBMS_LOB.substr(VRE.CTU_RESULTADO) \"Elisa anti HBS AG\"      ");
                        str.AppendLine("          FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("               RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("         WHERE P.COD_PACIENTE = '" + codPaciente + "'  ");
                        str.AppendLine("           AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("           AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");
                        str.AppendLine("           AND E.NUM_EXAME = REL.NUM_EXAME ");
                        str.AppendLine("            AND REL.COD_TIPO_INFORMACAO= 3131 --ELEMENTO  ");
                        str.AppendLine("           AND PEI.COD_PROCEDIMENTO_HC = 12595 --EXAME SOROLOGIA PARA HEPATITES ");
                        str.AppendLine("           AND E.COD_CENTRO_CIRURGICO = 89 --LABORATÓRIO ");
                        str.AppendLine("           AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("           AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("           AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("          ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1 ");
                    }
                    #endregion

                    #region Anti-HBC
                    if (tipo == 2)
                    {
                        str.AppendLine("SELECT DTA_HOR_EXAME,\"Anti - HBC AG IG Total\" ");
                        str.AppendLine("  FROM ( SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, ");
                        str.AppendLine("                DBMS_LOB.substr(VRE.CTU_RESULTADO) \"Anti - HBC AG IG Total\"      ");
                        str.AppendLine("            FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("                 RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("           WHERE P.COD_PACIENTE = '" + codPaciente + "'  ");
                        str.AppendLine("             AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("             AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");
                        str.AppendLine("             AND E.NUM_EXAME = REL.NUM_EXAME ");
                        str.AppendLine("             AND REL.COD_TIPO_INFORMACAO= 3130 --ELEMENTO  ");
                        str.AppendLine("             AND PEI.COD_PROCEDIMENTO_HC = 12595 --EXAME SOROLOGIA PARA HEPATITES ");
                        str.AppendLine("             AND E.COD_CENTRO_CIRURGICO = 89 --LABORATÓRIO ");
                        str.AppendLine("             AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("             AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("             AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("            ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1 ");

                    }
                    #endregion

                    #region HCV
                    if (tipo == 3)
                    {
                        str.AppendLine("SELECT DTA_HOR_EXAME,\"ANTI - HCV\" ");
                        str.AppendLine("  FROM ( SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, ");
                        str.AppendLine("            DBMS_LOB.substr(VRE.CTU_RESULTADO) \"ANTI - HCV\" ");
                        str.AppendLine("        FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("             RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("       WHERE P.COD_PACIENTE = '" + codPaciente + "' ");
                        str.AppendLine("         AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("         AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");
                        str.AppendLine("         AND E.NUM_EXAME = REL.NUM_EXAME ");
                        str.AppendLine("         AND REL.COD_TIPO_INFORMACAO= 1426 --ELEMENTO   ");
                        str.AppendLine("         AND PEI.COD_PROCEDIMENTO_HC = 12595 --EXAME SOROLOGIA PARA HEPATITES ");
                        str.AppendLine("         AND E.COD_CENTRO_CIRURGICO = 89 --LABORATÓRIO ");
                        str.AppendLine("         AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("         AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("         AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("        ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1 ");
                    }
                    #endregion

                    #region Elisa Anti-HIV
                    if (tipo == 4)
                    {
                        str.AppendLine("SELECT E.DTA_HOR_EXAME,  ");
                        str.AppendLine("     (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE  ");
                        str.AppendLine("        WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 1209 ) \"Cut-Off\",  ");
                        str.AppendLine("     (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE  ");
                        str.AppendLine("        WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 645 ) \"Título\",   ");
                        str.AppendLine("     (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE  ");
                        str.AppendLine("        WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 1294 ) \"Interpretação\" --ELEMENTO ");
                        str.AppendLine("  FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E  ");
                        str.AppendLine(" WHERE   ");
                        str.AppendLine("   P.COD_PACIENTE = '" + codPaciente + "' AND  ");
                        str.AppendLine("   PE.COD_PACIENTE = P.COD_PACIENTE   ");
                        str.AppendLine("   AND PEI.COD_PROCEDIMENTO_HC = 7854 --EXAME  ");
                        str.AppendLine("   AND E.COD_CENTRO_CIRURGICO = 89 --LABORATÓRIO  ");
                        str.AppendLine("   AND PEI.IDF_SITUACAO = 10  ");
                        str.AppendLine("   AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO   ");
                        str.AppendLine("   AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM    ");
                        str.AppendLine("   AND E.DTA_HOR_EXAME = (SELECT MAX(E2.DTA_HOR_EXAME)FROM EXAME_HC E2, PEDIDO_EXAME_ITEM_HC PEI2, PEDIDO_EXAME_HC PE2  ");
                        str.AppendLine("                        WHERE PE2.COD_PACIENTE = P.COD_PACIENTE AND PEI2.COD_PROCEDIMENTO_HC = 7854 AND PEI2.IDF_SITUACAO = 10 --SITUAÇÃO: ATENDIDO  ");
                        str.AppendLine("                          AND PE2.NUM_PEDIDO = PEI2.NUM_PEDIDO AND PEI2.NUM_PEDIDO_EXAME_ITEM = E2.NUM_PEDIDO_EXAME_ITEM)  ");
                    }
                    #endregion

                    #region VDRL (Sífilis)
                    if (tipo == 5)
                    {
                        str.AppendLine("SELECT \"VDRL\", DTA_HOR_EXAME ");
                        str.AppendLine("  FROM ( SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, ");
                        str.AppendLine("                DBMS_LOB.substr(VRE.CTU_RESULTADO) \"VDRL\" ");
                        str.AppendLine("            FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("                 RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine(" WHERE   ");
                        str.AppendLine("             P.COD_PACIENTE = '" + codPaciente + "' ");
                        str.AppendLine("             AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("             AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");
                        str.AppendLine("             AND E.NUM_EXAME = REL.NUM_EXAME ");
                        str.AppendLine("             AND REL.COD_TIPO_INFORMACAO = 1319 --ELEMENTO   ");
                        str.AppendLine("             AND PEI.COD_PROCEDIMENTO_HC = 7873 --EXAME VDRL");
                        str.AppendLine("             AND E.COD_CENTRO_CIRURGICO = 89 --LABORATÓRIO ");
                        str.AppendLine("             AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("             AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("             AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("            ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1 ");
                    }
                    #endregion

                    #region Toxoplasmose (Elisa para toxoplasmose IGG/IGM)
                    if (tipo == 6)
                    {
                        str.AppendLine("SELECT *");
                        str.AppendLine("  FROM ( SELECT  ");
                        str.AppendLine("            (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 1209 --ELEMENTO   ");
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) CutOffIGG,");
                        str.AppendLine("            (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 1304 --ELEMENTO   ");
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) TituloIGG,");
                        str.AppendLine("             (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 1294 --ELEMENTO   ");
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) InterpretacaoIGG,");
                        str.AppendLine("                (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 1299 --ELEMENTO   ");
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) CutOffIGM,");
                        str.AppendLine("            (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 1300 --ELEMENTO   ");
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) TituloIGM,");
                        str.AppendLine("             (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 1860 --ELEMENTO   ");
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) InterpretacaoIGM, E.DTA_HOR_EXAME");
                        str.AppendLine("            FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E");
                        str.AppendLine(" WHERE   ");
                        str.AppendLine("             P.COD_PACIENTE = '" + codPaciente + "' ");
                        str.AppendLine("             AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("             AND PEI.COD_PROCEDIMENTO_HC = 12581 --EXAME VDRL");
                        str.AppendLine("             AND E.COD_CENTRO_CIRURGICO = 89 --LABORATÓRIO ");
                        str.AppendLine("             AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("             AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("             AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("            ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1 ");
                    }
                    #endregion

                    #region HTLV (Elisa anti-HTLV I/II)
                    if (tipo == 7)
                    {
                        str.AppendLine("SELECT * FROM (");
                        str.AppendLine("      SELECT ");
                        str.AppendLine("         (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("          WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 1209) \"CutOff\",");
                        str.AppendLine("         (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("          WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 645) \"Título\",");
                        str.AppendLine("         (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("           WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 1294) \"Interpretação\",");
                        str.AppendLine("         E.DTA_HOR_EXAME ");
                        str.AppendLine("      FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("                       RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("      WHERE P.COD_PACIENTE = '" + codPaciente + "' ");
                        str.AppendLine("           AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("           AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");
                        str.AppendLine("           AND E.NUM_EXAME = REL.NUM_EXAME ");
                        str.AppendLine("           AND REL.COD_TIPO_INFORMACAO = 1294 --ELEMENTO   ");
                        str.AppendLine("           AND PEI.COD_PROCEDIMENTO_HC = 7887 --EXAME");
                        str.AppendLine("           AND E.COD_CENTRO_CIRURGICO = 89 --LABORATORIO ");
                        str.AppendLine("           AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("           AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("           AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("          ORDER BY E.DTA_HOR_EXAME DESC )");
                        str.AppendLine("WHERE ROWNUM = 1");
                    }
                    #endregion

                    #region UROCULTURA
                    if (tipo == 8)
                    {
                        //str.AppendLine("SELECT \"UROCULTURA\" FROM ( ");
                        str.AppendLine(" SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, ");
                        str.AppendLine("        DBMS_LOB.substr(EL.DSC_EXAME_LAUDO) \"UROCULTURA\" ");
                        str.AppendLine("    FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("        EXAME_LAUDO_HC EL");
                        str.AppendLine("   WHERE P.COD_PACIENTE = '" + codPaciente + "'");
                        str.AppendLine("     AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("     AND E.NUM_EXAME = EL.NUM_EXAME");
                        str.AppendLine("     AND PEI.COD_PROCEDIMENTO_HC = 8459 --EXAME");
                        str.AppendLine("     AND E.COD_CENTRO_CIRURGICO = 104 --LABORATORIO ");
                        str.AppendLine("     AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("     AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("     AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("     AND DTA_HOR_EXAME > SYSDATE - 455 ");
                        str.AppendLine("    ORDER BY E.DTA_HOR_EXAME DESC ");
                        //str.AppendLine(") WHERE ROWNUM = 1");
                    }
                    #endregion

                    #region GTT (Glicemia)
                    if (tipo == 9)
                    {
                        str.AppendLine("SELECT * FROM (");
                        str.AppendLine("      SELECT DTA_HOR_EXAME, ");
                        str.AppendLine("         (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("          WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 1225) \"Tempo0\",");
                        str.AppendLine("         (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("          WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 1174) \"Tempo60\",");
                        str.AppendLine("         (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("           WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 1176) \"Tempo120\"                ");
                        str.AppendLine("      FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("                       RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("       WHERE P.COD_PACIENTE = '" + codPaciente + "' ");
                        str.AppendLine("           AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("           AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");
                        str.AppendLine("           AND E.NUM_EXAME = REL.NUM_EXAME ");
                        str.AppendLine("           AND REL.COD_TIPO_INFORMACAO = 1225 --ELEMENTO   ");
                        str.AppendLine("             AND PEI.COD_PROCEDIMENTO_HC = 7807 --EXAME");
                        str.AppendLine("             AND E.COD_CENTRO_CIRURGICO = 91 --LABORATORIO ");
                        str.AppendLine("           AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("           AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("           AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("          ORDER BY E.DTA_HOR_EXAME DESC )");
                        str.AppendLine("WHERE ROWNUM = 1");
                    }
                    #endregion

                    #region Swab Reto-Vaginal
                    if (tipo == 10)
                    {
                        str.AppendLine("SELECT DTA_HOR_EXAME, \"SWAB\" ");
                        str.AppendLine("  FROM ( SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, ");
                        str.AppendLine("                DBMS_LOB.substr(VRE.CTU_RESULTADO) \"SWAB\" ");
                        str.AppendLine("            FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("                 RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE,");
                        str.AppendLine("                 ITEM_PEDIDO_AMOSTRA_EXAME IPA, AMOSTRA_EXAME_HC AE ");
                        str.AppendLine("            WHERE P.COD_PACIENTE = '" + codPaciente + "' ");
                        str.AppendLine("             AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("             AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");
                        str.AppendLine("             AND E.NUM_EXAME = REL.NUM_EXAME ");
                        str.AppendLine("             AND IPA.NUM_PEDIDO_EXAME_ITEM = PEI.NUM_PEDIDO_EXAME_ITEM");
                        str.AppendLine("             AND IPA.SEQ_AMOSTRA_EXAME = AE.SEQ_AMOSTRA_EXAME");
                        str.AppendLine("             AND AE.COD_TIPO_MATERIAL = 197 -- MATERIAL SWAB");
                        str.AppendLine("             AND REL.COD_TIPO_INFORMACAO = 1691 --ELEMENTO   ");
                        str.AppendLine("             AND PEI.COD_PROCEDIMENTO_HC = 13497 --EXAME");
                        str.AppendLine("             AND E.COD_CENTRO_CIRURGICO = 104 --LABORATORIO ");
                        str.AppendLine("             AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("             AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("             AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("            ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1");
                    }
                    #endregion

                    #region Carga Viral HIV
                    if (tipo == 11)
                    {
                        str.AppendLine("SELECT DTA_HOR_EXAME, \"CargaViralHIV\" ");
                        str.AppendLine("  FROM ( SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, ");
                        str.AppendLine("                DBMS_LOB.substr(VRE.CTU_RESULTADO) \"CargaViralHIV\" ");
                        str.AppendLine("            FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("                 RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("            WHERE P.COD_PACIENTE = '" + codPaciente + "' ");
                        str.AppendLine("             AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("             AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");
                        str.AppendLine("             AND E.NUM_EXAME = REL.NUM_EXAME ");
                        str.AppendLine("             AND REL.COD_TIPO_INFORMACAO = 1280 --ELEMENTO (COPIAS)  ");
                        str.AppendLine("             AND PEI.COD_PROCEDIMENTO_HC = 7877 --EXAME");
                        str.AppendLine("             AND E.COD_CENTRO_CIRURGICO = 89 --LABORATORIO ");
                        str.AppendLine("             AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("             AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("             AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("            ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1");
                    }
                    #endregion

                    #region Quantificacao Linfocitos T CD4/CD8 (pega só o CD4)
                    if (tipo == 12)
                    {
                        str.AppendLine("SELECT * FROM (");
                        str.AppendLine("      SELECT  DTA_HOR_EXAME, ");
                        str.AppendLine("         (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("          WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 960) \"MM3\",");
                        str.AppendLine("         (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("          WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 1253) \"Percentual\"");
                        str.AppendLine("      FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("                       RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("            WHERE P.COD_PACIENTE = '" + codPaciente + "' ");
                        str.AppendLine("           AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("           AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");
                        str.AppendLine("           AND E.NUM_EXAME = REL.NUM_EXAME ");
                        str.AppendLine("             AND PEI.COD_PROCEDIMENTO_HC = 13119 --EXAME: QUANTIFICACAO DE LINFOCITOS T CD4/CD8");
                        str.AppendLine("             AND E.COD_CENTRO_CIRURGICO = 139 --LABORATORIO: HEMOCENTRO - CITOMETRIA DE FLUXO");
                        str.AppendLine("           AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("           AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("           AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("          ORDER BY E.DTA_HOR_EXAME DESC )");
                        str.AppendLine("WHERE ROWNUM = 1");
                    }
                    #endregion

                    #region Hemograma
                    if (tipo == 13)
                    { 
                        str.AppendLine("SELECT * FROM ( ");
                        str.AppendLine("  SELECT  DTA_HOR_EXAME,  ");
                        str.AppendLine("   (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE  ");
                        str.AppendLine("    WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 551) \"HEMOGLOBINA\", ");
                        str.AppendLine("   (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("    WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 555) \"HEMATOCRITO\", ");
                        str.AppendLine("   (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) || ' ' || NVL(UN.NOM_ABREVIADO, '') FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE, UNIDADE UN  ");
                        str.AppendLine("    WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_UNIDADE = UN.COD_UNIDADE (+) AND REL.COD_TIPO_INFORMACAO= 570) \"PLAQUETAS\",  ");
                        str.AppendLine("   (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) || ' ' || NVL(UN.NOM_ABREVIADO, '') FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE, UNIDADE UN ");
                        str.AppendLine("    WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_UNIDADE = UN.COD_UNIDADE (+) AND REL.COD_TIPO_INFORMACAO= 547) \"GLOBULO_BRANCO\" ");
                        str.AppendLine("  FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, "); 
                        str.AppendLine("                 RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");                         
                        str.AppendLine("     WHERE P.COD_PACIENTE = '" + codPaciente + "' ");
                        str.AppendLine("     AND PE.COD_PACIENTE = P.COD_PACIENTE  ");
                        str.AppendLine("     AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME   ");
                        str.AppendLine("     AND E.NUM_EXAME = REL.NUM_EXAME  ");
                        str.AppendLine("       AND PEI.COD_PROCEDIMENTO_HC = 7539 --EXAME: HEMOGRAMA ");
                        str.AppendLine("       AND E.COD_CENTRO_CIRURGICO = 60 --LABORATORIO: HEMATO ROTINA ");
                        str.AppendLine("     AND PEI.IDF_SITUACAO = 10  ");
                        str.AppendLine("     AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO  ");
                        str.AppendLine("     AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM   ");
                        str.AppendLine("    ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1 ");
                    }

                    #region Rubeola (IGG/IGM)
                    if (tipo == 14)
                    {
                        str.AppendLine("SELECT *");
                        str.AppendLine("  FROM ( SELECT  ");
                        str.AppendLine("            (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 1209 --ELEMENTO   ");
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) CutOffIGG,");
                        str.AppendLine("            (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 645 --ELEMENTO   ");
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) TituloIGG,");
                        str.AppendLine("             (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 1294 --ELEMENTO   ");
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) InterpretacaoIGG,");
                        str.AppendLine("                (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 1299 --ELEMENTO   ");
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) CutOffIGM,");
                        str.AppendLine("            (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 1300 --ELEMENTO   ");
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) TituloIGM,");
                        str.AppendLine("             (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 1860 --ELEMENTO   ");
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) InterpretacaoIGM, E.DTA_HOR_EXAME");
                        str.AppendLine("            FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E");
                        str.AppendLine(" WHERE   ");
                        str.AppendLine("             P.COD_PACIENTE = '" + codPaciente + "' ");
                        str.AppendLine("             AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("             AND PEI.COD_PROCEDIMENTO_HC = 13549 --EXAME RUBEOLA");
                        str.AppendLine("             AND E.COD_CENTRO_CIRURGICO = 89 --LABORATÓRIO ");
                        str.AppendLine("             AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("             AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("             AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("            ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1 ");
                    }
                    #endregion

                    #region Hemoglobina Glicosada
                    if (tipo == 15)
                    {
                        str.AppendLine("SELECT *");
                        str.AppendLine("  FROM ( SELECT  ");                      
                        str.AppendLine("             (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 462 --ELEMENTO   ");
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) RESULTADO, E.DTA_HOR_EXAME");
                        str.AppendLine("            FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E");
                        str.AppendLine(" WHERE   ");
                        str.AppendLine("             P.COD_PACIENTE = '" + codPaciente + "' ");
                        str.AppendLine("             AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("             AND PEI.COD_PROCEDIMENTO_HC = 7809 --EXAME HEMOGLOBINA GLICOSADA");
                        str.AppendLine("             AND E.COD_CENTRO_CIRURGICO = 91 --LABORATÓRIO ");
                        str.AppendLine("             AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("             AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("             AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("            ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1 ");
                    }
                    #endregion

                    #region Glicemia
                    if (tipo == 16)
                    {
                        str.AppendLine("SELECT *");
                        str.AppendLine("  FROM ( SELECT  ");
                        str.AppendLine("             (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 517 --ELEMENTO   ");
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) RESULTADO, E.DTA_HOR_EXAME");
                        str.AppendLine("            FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E");
                        str.AppendLine(" WHERE   ");
                        str.AppendLine("             P.COD_PACIENTE = '" + codPaciente + "' ");
                        str.AppendLine("             AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("             AND PEI.COD_PROCEDIMENTO_HC = 7282 --GLICEMIA");
                        str.AppendLine("             AND E.COD_CENTRO_CIRURGICO IN (140,120,80) --LABORATÓRIO ");
                        str.AppendLine("             AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("             AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("             AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("            ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1 ");
                    }
                    #endregion

                    #region Parazitologico fezes
                    if (tipo == 17)
                    {
                        str.AppendLine("SELECT *");
                        str.AppendLine("  FROM ( SELECT  ");
                        str.AppendLine("            (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 746 --ELEMENTO   ");
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) PROTOZOARIOS,");
                        str.AppendLine("             (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 745 --ELEMENTO   "); //745 HELMINTOS e 746 PROTOZOARIOS
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) HELMINTOS, E.DTA_HOR_EXAME");
                        str.AppendLine("            FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E");
                        str.AppendLine(" WHERE   ");
                        str.AppendLine("             P.COD_PACIENTE = '" + codPaciente + "' ");
                        str.AppendLine("             AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("             AND PEI.COD_PROCEDIMENTO_HC = 7422 --PARAZITOLOGICO FEZES");
                        str.AppendLine("             AND E.COD_CENTRO_CIRURGICO = 142 --LABORATÓRIO ");
                        str.AppendLine("             AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("             AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("             AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("            ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1 ");
                    }
                    #endregion

                    #region Urina Rotina
                    if (tipo == 18)
                    {
                        str.AppendLine("SELECT *");
                        str.AppendLine("  FROM ( SELECT  ");
                        str.AppendLine("             (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) ");
                        str.AppendLine("               FROM RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("              WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("                AND REL.COD_TIPO_INFORMACAO = 548 --ELEMENTO GLICOSE  ");
                        str.AppendLine("                AND E.NUM_EXAME = REL.NUM_EXAME ) GLICOSE, E.DTA_HOR_EXAME");
                        str.AppendLine("            FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E");
                        str.AppendLine(" WHERE   ");
                        str.AppendLine("             P.COD_PACIENTE = '" + codPaciente + "' ");
                        str.AppendLine("             AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("             AND PEI.COD_PROCEDIMENTO_HC = 7250 --URINA");
                        str.AppendLine("             AND E.COD_CENTRO_CIRURGICO IN (80, 120, 142) --LABORATÓRIO ");
                        str.AppendLine("             AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("             AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("             AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("            ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1 ");
                    }
                    #endregion

                    #endregion

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        
        public DataView ObterResExameLaboratEAHM(int tipo, string codPaciente)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    ctx.Open();

                    #region TGOAST - 1
                    if (tipo == 1)
                    {
                        str.AppendLine("SELECT DTA_HOR_EXAME,\"TGO AST\", \"REFERENCIA\"");
                        str.AppendLine("FROM ( SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, RVR.VLR_MINIMO_REFERENCIA \"REFERENCIA\", ");
                        str.AppendLine("              DBMS_LOB.substr(VRE.CTU_RESULTADO) \"TGO AST\"      ");
                        str.AppendLine("          FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("               REFERENCIA_VALOR_RESULTADO RVR, RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("         WHERE P.COD_PACIENTE = '" + codPaciente + "'  ");
                        str.AppendLine("           AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("           AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");
                        str.AppendLine("           AND REL.SEQ_REFERENCIA_EXAME = RVR.SEQ_REFERENCIA (+)  ");
                        str.AppendLine("           AND E.NUM_EXAME = REL.NUM_EXAME ");
                        str.AppendLine("           AND REL.COD_TIPO_INFORMACAO= 517 --ELEMENTO  ");
                        str.AppendLine("           AND PEI.COD_PROCEDIMENTO_HC = 7246 --EXAME TGO/AST ");
                        str.AppendLine("           AND E.COD_CENTRO_CIRURGICO = 140 --LABORATÓRIO ");
                        str.AppendLine("           AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("           AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("           AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("           AND E.DTA_HOR_EXAME > SYSDATE - 90 ");
                        str.AppendLine("          ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1 ");
                    }
                    #endregion HBSAG

                    #region Plaquetas - 2
                    if (tipo == 2)
                    {
                        str.AppendLine("SELECT DTA_HOR_EXAME,\"PLAQUETAS\" ");
                        str.AppendLine("FROM ( SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, ");
                        str.AppendLine("              DBMS_LOB.substr(VRE.CTU_RESULTADO) \"PLAQUETAS\"      ");
                        str.AppendLine("          FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("               RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("         WHERE PE.COD_PACIENTE = '" + codPaciente + "'  ");
                        str.AppendLine("           AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("           AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");                       
                        str.AppendLine("           AND E.NUM_EXAME = REL.NUM_EXAME ");
                        str.AppendLine("           AND REL.COD_TIPO_INFORMACAO= 570 --ELEMENTO  ");
                        str.AppendLine("           AND PEI.COD_PROCEDIMENTO_HC = 7539 --EXAME TGO/AST ");
                        str.AppendLine("           AND E.COD_CENTRO_CIRURGICO = 60 --LABORATÓRIO ");
                        str.AppendLine("           AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("           AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("           AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("           AND E.DTA_HOR_EXAME > SYSDATE - 90 ");
                        str.AppendLine("          ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1 ");
                    }
                    #endregion Plaquetas

                    #region ALBUMINA - 3

                    if (tipo == 3)
                    {
                        str.AppendLine("SELECT DTA_HOR_EXAME,\"ALBUMINA\" ");
                        str.AppendLine("FROM ( SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, ");
                        str.AppendLine("              FC_TO_NUMBER(DBMS_LOB.substr(VRE.CTU_RESULTADO)) \"ALBUMINA\"      ");
                        str.AppendLine("          FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("               RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("         WHERE PE.COD_PACIENTE = '" + codPaciente + "'  ");
                        str.AppendLine("           AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("           AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");
                        str.AppendLine("           AND E.NUM_EXAME = REL.NUM_EXAME ");
                        str.AppendLine("           AND REL.COD_TIPO_INFORMACAO= 517 --ELEMENTO  ");
                        str.AppendLine("           AND PEI.COD_PROCEDIMENTO_HC = 7280 --EXAME TGO/AST ");
                        str.AppendLine("           AND E.COD_CENTRO_CIRURGICO = 140 --LABORATÓRIO ");
                        str.AppendLine("           AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("           AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("           AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("           AND E.DTA_HOR_EXAME > SYSDATE - 90 ");
                        str.AppendLine("          ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1 ");
                    }

                    #endregion ALBUMINA

                    #region TGPAST - 4
                    if (tipo == 4)
                    {
                        str.AppendLine("SELECT DTA_HOR_EXAME,\"TGP AST\", \"REFERENCIA\"");
                        str.AppendLine("FROM ( SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, RVR.VLR_MINIMO_REFERENCIA \"REFERENCIA\", ");
                        str.AppendLine("              DBMS_LOB.substr(VRE.CTU_RESULTADO) \"TGP AST\"      ");
                        str.AppendLine("          FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("               REFERENCIA_VALOR_RESULTADO RVR, RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("         WHERE P.COD_PACIENTE = '" + codPaciente + "'  ");
                        str.AppendLine("           AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                        str.AppendLine("           AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");
                        str.AppendLine("           AND REL.SEQ_REFERENCIA_EXAME = RVR.SEQ_REFERENCIA (+)  ");
                        str.AppendLine("           AND E.NUM_EXAME = REL.NUM_EXAME ");
                        str.AppendLine("           AND REL.COD_TIPO_INFORMACAO= 517 --ELEMENTO  ");
                        str.AppendLine("           AND PEI.COD_PROCEDIMENTO_HC = 7247 --EXAME TGO/AST ");
                        str.AppendLine("           AND E.COD_CENTRO_CIRURGICO = 140 --LABORATÓRIO ");
                        str.AppendLine("           AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("           AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                        str.AppendLine("           AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                        str.AppendLine("           AND E.DTA_HOR_EXAME > SYSDATE - 90 ");
                        str.AppendLine("          ORDER BY E.DTA_HOR_EXAME DESC ) ");
                        str.AppendLine("WHERE ROWNUM = 1 ");
                    }
                    #endregion HBSAG

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        /// <summary>
        /// Obter Sorologia
        /// </summary>
        /// <param name="tipo">0 - PTH | 1 - 25-VITD</param>
        /// <param name="codPaciente">Código do Paciente</param>
        /// <returns></returns>
        public DataView ObterDMODRC(int tipo, string codPaciente)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    ctx.Open();

                    #region PTH
                    if (tipo == 0)
                    {
                        str.AppendLine("SELECT E.DTA_HOR_EXAME,      ");
                        str.AppendLine("     (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("        WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 462 ) PTH --ELEMENTO ");
                        str.AppendLine("  FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E ");
                        str.AppendLine(" WHERE  ");
                        str.AppendLine("   P.COD_PACIENTE = '" + codPaciente + "' AND ");
                        str.AppendLine("   PE.COD_PACIENTE = P.COD_PACIENTE  ");
                        str.AppendLine("   AND PEI.COD_PROCEDIMENTO_HC = 7801 --EXAME ");
                        str.AppendLine("   AND E.COD_CENTRO_CIRURGICO = 91 --LABORATÓRIO ");
                        str.AppendLine("   AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("   AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO  ");
                        str.AppendLine("   AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM   ");
                        str.AppendLine("   AND E.DTA_HOR_EXAME = (SELECT MAX(E2.DTA_HOR_EXAME)FROM EXAME_HC E2, PEDIDO_EXAME_ITEM_HC PEI2, PEDIDO_EXAME_HC PE2 ");
                        str.AppendLine("                        WHERE PE2.COD_PACIENTE = P.COD_PACIENTE AND PEI2.COD_PROCEDIMENTO_HC = 7801 AND PEI2.IDF_SITUACAO = 10 --SITUAÇÃO: ATENDIDO ");
                        str.AppendLine("                          AND PE2.NUM_PEDIDO = PEI2.NUM_PEDIDO AND PEI2.NUM_PEDIDO_EXAME_ITEM = E2.NUM_PEDIDO_EXAME_ITEM) ");
                    }
                    #endregion

                    #region 25-VITD
                    if (tipo == 1)
                    {
                        str.AppendLine("SELECT E.DTA_HOR_EXAME,      ");
                        str.AppendLine("     (SELECT DBMS_LOB.substr(VRE.CTU_RESULTADO) FROM  RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                        str.AppendLine("        WHERE REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  AND E.NUM_EXAME = REL.NUM_EXAME AND REL.COD_TIPO_INFORMACAO= 462 ) PTH --ELEMENTO  ");
                        str.AppendLine("  FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E ");
                        str.AppendLine(" WHERE  ");
                        str.AppendLine("   P.COD_PACIENTE = '" + codPaciente + "' AND ");
                        str.AppendLine("   PE.COD_PACIENTE = P.COD_PACIENTE  ");
                        str.AppendLine("   AND PEI.COD_PROCEDIMENTO_HC = 12510 --EXAME ");
                        str.AppendLine("   AND E.COD_CENTRO_CIRURGICO = 91 --LABORATÓRIO ");
                        str.AppendLine("   AND PEI.IDF_SITUACAO = 10 ");
                        str.AppendLine("   AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO  ");
                        str.AppendLine("   AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM   ");
                        str.AppendLine("   AND E.DTA_HOR_EXAME = (SELECT MAX(E2.DTA_HOR_EXAME)FROM EXAME_HC E2, PEDIDO_EXAME_ITEM_HC PEI2, PEDIDO_EXAME_HC PE2 ");
                        str.AppendLine("                        WHERE PE2.COD_PACIENTE = P.COD_PACIENTE AND PEI2.COD_PROCEDIMENTO_HC = 12510 AND PEI2.IDF_SITUACAO = 10 --SITUAÇÃO: ATENDIDO ");
                        str.AppendLine("                          AND PE2.NUM_PEDIDO = PEI2.NUM_PEDIDO AND PEI2.NUM_PEDIDO_EXAME_ITEM = E2.NUM_PEDIDO_EXAME_ITEM) ");
                    }
                    #endregion

                 

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Obter dados exame hc para visualização de laudo neurof.
        /// </summary>        
        public DataView ObterArquivosExameHCParaVisualizacao(Int64 numExame)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT SEQ_EXAME_HC_ARQUIVO, ");
                    str.AppendLine("    NVL(DSC_ARQUIVO, ' ') DSC_ARQUIVO, ");
                    str.AppendLine("    DSC_TIPO_ARQUIVO ");

                    str.AppendLine(" FROM EXAME_HC_ARQUIVO ");

                    str.AppendLine(string.Format(" WHERE NUM_EXAME = {0} ", numExame));
                    

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Retorna os dados do Exame: TS ABO (Grupo/Tipo Sanguineo) pelo codigo do paciente.
        /// </summary>
        /// <param name="codPaciente"></param>
        /// <returns></returns>
        public DataView ObterTSABO(string codPaciente)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine("SELECT \"TS ABO\" ");
                    str.AppendLine("FROM (");
                    str.AppendLine("SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, ");
                    str.AppendLine("              DBMS_LOB.substr(VRE.CTU_RESULTADO) \"TS ABO\"      ");
                    str.AppendLine("          FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E,  ");
                    str.AppendLine("               RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                    str.AppendLine("         WHERE P.COD_PACIENTE = '" + codPaciente + "'");
                    str.AppendLine("           AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                    str.AppendLine("           AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");
                    str.AppendLine("           AND E.NUM_EXAME = REL.NUM_EXAME ");
                    str.AppendLine("           AND REL.COD_TIPO_INFORMACAO= 734 --Resultado ABO  ");
                    str.AppendLine("           AND PEI.COD_PROCEDIMENTO_HC = 7399");
                    str.AppendLine("           AND E.COD_CENTRO_CIRURGICO IN (43, 45)");
                    str.AppendLine("           AND PEI.IDF_SITUACAO = 10 ");
                    str.AppendLine("           AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                    str.AppendLine("           AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                    str.AppendLine("          ORDER BY E.DTA_HOR_EXAME DESC");
                    str.AppendLine("          )");
                    str.AppendLine("WHERE ROWNUM = 1");


                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Retorna os dados do Exame: RH (do Tipo Sanguineo) pelo codigo do paciente.
        /// </summary>
        /// <param name="codPaciente"></param>
        /// <returns></returns>
        public DataView ObterRH(string codPaciente)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine("SELECT \"RH\" ");
                    str.AppendLine("FROM (      ");
                    str.AppendLine("SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, ");
                    str.AppendLine("              DBMS_LOB.substr(VRE.CTU_RESULTADO) \"RH\"      ");
                    str.AppendLine("          FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E,  ");
                    str.AppendLine("               RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                    str.AppendLine("         WHERE P.COD_PACIENTE = '" + codPaciente + "'");
                    str.AppendLine("           AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                    str.AppendLine("           AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");
                    str.AppendLine("           AND E.NUM_EXAME = REL.NUM_EXAME ");
                    str.AppendLine("           AND REL.COD_TIPO_INFORMACAO= 735 --Res. RH");
                    str.AppendLine("           AND PEI.COD_PROCEDIMENTO_HC = 7399");
                    str.AppendLine("           AND E.COD_CENTRO_CIRURGICO IN (43, 45)");
                    str.AppendLine("           AND PEI.IDF_SITUACAO = 10 ");
                    str.AppendLine("           AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                    str.AppendLine("           AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                    str.AppendLine("          ORDER BY E.DTA_HOR_EXAME DESC");
                    str.AppendLine("          ) ");
                    str.AppendLine("WHERE ROWNUM = 1");


                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Retorna os dados do Exame: Coombs Indireto pelo codigo do paciente.
        /// </summary>
        /// <param name="codPaciente"></param>
        /// <returns></returns>
        public DataView ObterCoombsIndireto(string codPaciente)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine("SELECT \"COOMBS INDIRETO\" ");
                    str.AppendLine("FROM (          ");
                    str.AppendLine("SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, ");
                    str.AppendLine("              DBMS_LOB.substr(VRE.CTU_RESULTADO) \"COOMBS INDIRETO\"");
                    str.AppendLine("          FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E,  ");
                    str.AppendLine("               RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE ");
                    str.AppendLine("         WHERE P.COD_PACIENTE = '" + codPaciente + "'");
                    str.AppendLine("           AND PE.COD_PACIENTE = P.COD_PACIENTE ");
                    str.AppendLine("           AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME  ");
                    str.AppendLine("           AND E.NUM_EXAME = REL.NUM_EXAME ");
                    str.AppendLine("           AND REL.COD_TIPO_INFORMACAO= 730 --Coombs Indireto");
                    str.AppendLine("           AND PEI.COD_PROCEDIMENTO_HC = 7391 ");
                    str.AppendLine("           AND E.COD_CENTRO_CIRURGICO IN (43, 45)");
                    str.AppendLine("           AND PEI.IDF_SITUACAO = 10 ");
                    str.AppendLine("           AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO ");
                    str.AppendLine("           AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM  ");
                    str.AppendLine("          ORDER BY E.DTA_HOR_EXAME DESC");
                    str.AppendLine(") ");
                    str.AppendLine("WHERE ROWNUM = 1");


                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        /// <summary>
        /// Retorna os dados do Exame indicado no Tipo, pelo codigo do paciente.
        /// Tipos: 0 - Bilirrubina Sérica, 1 - INR, 2 - Creatinina Sérica
        /// </summary>
        /// <param name="codPaciente"></param>
        /// <returns> double - resultado do exame</returns>
        public double ObterResExameLaborat(int tipo, string codPaciente)
        {
            double ret = 0; 
            try
            {
                StringBuilder str = new StringBuilder();

                using (Contexto ctx = new Contexto())
                {
                    QueryCommandConfig query;

                    ctx.Open();
                    #region Bilirrubina
                    if (tipo == 0) // Bilirrubina Sérica
                    {
                        str.AppendLine("SELECT DTA_HOR_EXAME, EXAME, LN(EXAME) LN_EXAME ");
                        str.AppendLine("FROM ( SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, ");
                        str.AppendLine("              FC_TO_NUMBER(DBMS_LOB.substr(VRE.CTU_RESULTADO)) EXAME ");
                        str.AppendLine("          FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("               RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("         WHERE PE.COD_PACIENTE = '" + codPaciente + "'");
                        str.AppendLine("           AND PE.COD_PACIENTE = P.COD_PACIENTE");
                        str.AppendLine("           AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("           AND E.NUM_EXAME = REL.NUM_EXAME");
                        str.AppendLine("           AND REL.COD_TIPO_INFORMACAO= 529 --ELEMENTO ");
                        str.AppendLine("           AND PEI.COD_PROCEDIMENTO_HC = 7423 --EXAME bilirrubina");
                        str.AppendLine("           AND E.COD_CENTRO_CIRURGICO = 140 --LABORATÓRIO BIOQUÍMICA");
                        str.AppendLine("           AND PEI.IDF_SITUACAO = 10");
                        str.AppendLine("           AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO");
                        str.AppendLine("           AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM ");
                        str.AppendLine("           AND E.DTA_HOR_EXAME > SYSDATE - 90");
                        str.AppendLine("          ORDER BY E.DTA_HOR_EXAME DESC )");
                        str.AppendLine("WHERE ROWNUM = 1");
                        
                    }
                    #endregion  
                    #region INR
                    else if (tipo == 1) // INR
                    {
                        str.AppendLine("SELECT DTA_HOR_EXAME, EXAME, LN(EXAME) LN_EXAME ");
                        str.AppendLine("FROM ( SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME, ");
                        str.AppendLine("              DECODE (DBMS_LOB.substr(VRE.CTU_RESULTADO),'>12,0', 12, FC_TO_NUMBER(DBMS_LOB.substr(VRE.CTU_RESULTADO)) ) AS  EXAME ");
                        str.AppendLine("          FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("               RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("         WHERE PE.COD_PACIENTE = '" + codPaciente + "'");
                        str.AppendLine("           AND PE.COD_PACIENTE = P.COD_PACIENTE");
                        str.AppendLine("           AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("           AND E.NUM_EXAME = REL.NUM_EXAME");
                        str.AppendLine("           AND REL.COD_TIPO_INFORMACAO= 515 --ELEMENTO ");
                        str.AppendLine("           AND PEI.COD_PROCEDIMENTO_HC = 7193 --EXAME ttpa");
                        str.AppendLine("           AND E.COD_CENTRO_CIRURGICO = 56 --LABORATÓRIO BIOQUÍMICA");
                        str.AppendLine("           AND PEI.IDF_SITUACAO = 10");
                        str.AppendLine("           AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO");
                        str.AppendLine("           AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM ");
                        str.AppendLine("           AND E.DTA_HOR_EXAME > SYSDATE - 90");
                        str.AppendLine("          ORDER BY E.DTA_HOR_EXAME DESC )");
                        str.AppendLine("WHERE ROWNUM = 1");

                    }
                    #endregion
                    #region Creatinina
                    else if (tipo == 2) // Creatinina
                    {
                        str.AppendLine("SELECT DTA_HOR_EXAME, EXAME, LN(EXAME) LN_EXAME ");
                        str.AppendLine("FROM ( SELECT P.COD_PACIENTE, E.DTA_HOR_EXAME,");
                        str.AppendLine("              FC_TO_NUMBER(DBMS_LOB.substr(VRE.CTU_RESULTADO)) EXAME");
                        str.AppendLine("          FROM PACIENTE P, PEDIDO_EXAME_HC PE, PEDIDO_EXAME_ITEM_HC PEI, EXAME_HC E, ");
                        str.AppendLine("               RESULTADO_EXAME_LABORATORIAL REL, VALOR_RESULTADO_EXAME VRE");
                        str.AppendLine("         WHERE PE.COD_PACIENTE = '" + codPaciente + "'");
                        str.AppendLine("           AND PE.COD_PACIENTE = P.COD_PACIENTE");
                        str.AppendLine("           AND REL.SEQ_RESULTADO_EXAME = VRE.SEQ_RESULTADO_EXAME ");
                        str.AppendLine("           AND E.NUM_EXAME = REL.NUM_EXAME");
                        str.AppendLine("           AND REL.COD_TIPO_INFORMACAO= 517 --ELEMENTO ");
                        str.AppendLine("           AND PEI.COD_PROCEDIMENTO_HC = 7214 --EXAME CREATININA");
                        str.AppendLine("           AND E.COD_CENTRO_CIRURGICO = 140 --LABORATÓRIO BIOQUÍMICA");
                        str.AppendLine("           AND PEI.IDF_SITUACAO = 10");
                        str.AppendLine("           AND PE.NUM_PEDIDO = PEI.NUM_PEDIDO");
                        str.AppendLine("           AND PEI.NUM_PEDIDO_EXAME_ITEM = E.NUM_PEDIDO_EXAME_ITEM ");
                        str.AppendLine("           AND E.DTA_HOR_EXAME > SYSDATE - 90");
                        str.AppendLine("          ORDER BY E.DTA_HOR_EXAME DESC )");
                        str.AppendLine("WHERE ROWNUM = 1");

                    }
                    #endregion

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        dr.Read();

                        ret = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<Double>(dr, "EXAME");

                    }
                    finally
                    {
                        dr.Close();
                        dr.Dispose();

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            return ret;
        }

        #endregion
    }
}
