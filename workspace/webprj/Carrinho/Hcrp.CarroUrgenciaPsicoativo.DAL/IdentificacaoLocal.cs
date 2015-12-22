using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public partial class IdentificacaoLocal 
    {
        public static string NomTabela = "IDENTIFICACAO_LOCAL";
        public static string NomSequence = "SEQ_IDENTIFICACAO_LOCAL";
            
        #region Recuperação de Informação [FB, MO, AJSO]
        public static Hcrp.CarroUrgenciaPsicoativo.Entity.IdentificacaoLocal getIdentificacaoLocal(long pNumID)
        {
            Hcrp.CarroUrgenciaPsicoativo.Entity.IdentificacaoLocal result = new Hcrp.CarroUrgenciaPsicoativo.Entity.IdentificacaoLocal();

            try
            {
                StringBuilder str = new StringBuilder();


                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT * ");
                    str.AppendLine(string.Format( " FROM {0} ", IdentificacaoLocal.NomTabela) );
                    str.AppendLine(" WHERE NUM_ID_LOCAL = :NUM_ID_LOCAL ");

                    query = new QueryCommandConfig(str.ToString());

                    query.Params["NUM_ID_LOCAL"] = pNumID;

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        dr.Read();
                        result.num_id_local = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<long>(dr, "NUM_ID_LOCAL");
                        result.dsc_id_local = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "DSC_ID_LOCAL");
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

            return result;
        }
        public static List<Hcrp.CarroUrgenciaPsicoativo.Entity.IdentificacaoLocal> getIdentificacaoLocal(string pDscIDLocal = "", long pNumID = 0)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.IdentificacaoLocal> result = new List<Hcrp.CarroUrgenciaPsicoativo.Entity.IdentificacaoLocal>();

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT * ");
                    str.AppendLine(" FROM IDENTIFICACAO_LOCAL ");
                    str.AppendLine(" WHERE (1=1) ");

                    if (pNumID > 0)
                    {
                        str.AppendLine(" AND (NUM_ID_LOCAL = :NUM_ID_LOCAL)");
                    }


                    if (!string.IsNullOrEmpty(pDscIDLocal))
                    {
                        str.AppendLine(" AND (DSC_ID_LOCAL LIKE '%' || :DSC_ID_LOCAL || '%') ");
                    }

                    query = new QueryCommandConfig(str.ToString());

                    if (pNumID > 0)
                    {
                        query.Params["NUM_ID_LOCAL"] = pNumID;
                    }

                    if (!string.IsNullOrEmpty(pDscIDLocal))
                    {
                        query.Params["DSC_ID_LOCAL"] = pDscIDLocal;
                    }

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            Hcrp.CarroUrgenciaPsicoativo.Entity.IdentificacaoLocal record = new Hcrp.CarroUrgenciaPsicoativo.Entity.IdentificacaoLocal();

                            record.num_id_local = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<long>(dr, "NUM_ID_LOCAL");
                            record.dsc_id_local = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "DSC_ID_LOCAL");
                            result.Add(record);
                        }
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



            return result.ToList();

        }

        #endregion

        #region Gravação [FB, MO, AJSO]
        public static bool setIdentificacaoLocal(ref string pOutError, Hcrp.CarroUrgenciaPsicoativo.Entity.IdentificacaoLocal pObjetoGravar, bool pControlarTransacao = true)
        {
            pOutError = string.Empty;
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                // Abrir conexão
                ctx.Open();

                if (pControlarTransacao)
                {
                    // Iniciando a transação 
                    ctx.BeginTransaction();

                }

                try
                {
                    StringBuilder str = new StringBuilder();

                    // Verificando se é inserção [FB, MO, AJSO]
                    if (pObjetoGravar.num_id_local == 0)
                    {
                        // Inserção [FB, MO, AJSO]

                        #region Gerando ID [FB, MO, AJSO]

                        // Comando de consulta [FB, MO, AJSO]
                        QueryCommandConfig query;

                        // Retornando ID por MAX na tabela
                        //str.Clear();
                        //str.AppendLine(" SELECT NVL( MAX( NUM_SEQUENCIA ), 0 ) + 1 NUMID ");
                        //str.AppendLine(" FROM TEMP_GENERICA_HC ");

                        // Retornando ID por Sequence
                        str.AppendLine(string.Format( " SELECT {0}.NEXTVAL NUMID FROM DUAL ", IdentificacaoLocal.NomSequence));

                        query = new QueryCommandConfig(str.ToString());

                        // Obter a lista de registros
                        ctx.ExecuteQuery(query);

                        IDataReader dr = ctx.Reader;

                        try
                        {
                            dr.Read();
                            pObjetoGravar.num_id_local = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUMID");
                        }
                        finally
                        {
                            dr.Close();
                            dr.Dispose();
                        }

                        #endregion


                        Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig(IdentificacaoLocal.NomTabela);
                        comando.Params["NUM_ID_LOCAL"] = pObjetoGravar.num_id_local;
                        comando.Params["DSC_ID_LOCAL"] = pObjetoGravar.dsc_id_local;

                        ctx.ExecuteInsert(comando);

                        if (pControlarTransacao)
                        {
                            ctx.Commit();
                        }
                    }
                    else
                    {
                        // Atualização [FB, MO, AJSO]
                        Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig(IdentificacaoLocal.NomTabela);

                        comando.FilterParams["NUM_ID_LOCAL"] = pObjetoGravar.num_id_local;

                        comando.Params["DSC_ID_LOCAL"] = pObjetoGravar.dsc_id_local;
                        ctx.ExecuteUpdate(comando);

                        if (pControlarTransacao)
                        {
                            ctx.Commit();
                        }

                    }
                }
                catch (Exception err)
                {
                    pOutError = err.Message;

                    if (pControlarTransacao)
                    {
                        if (ctx.Conn.State == ConnectionState.Open)
                        {
                            ctx.Rollback();
                        }
                    }

                }
            }

            return (string.IsNullOrEmpty(pOutError));

        }


        #endregion

    }
}
