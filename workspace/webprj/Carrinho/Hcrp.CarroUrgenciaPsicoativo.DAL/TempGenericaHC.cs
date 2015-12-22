using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public partial class TempGenericaHC 
    {
        public TempGenericaHC()
        {
        }

        #region Recuperação de Informação [FB, MO, AJSO]
        public static Hcrp.CarroUrgenciaPsicoativo.Entity.TempGenericaHC getTempGenericaHC(long pNumID)
        {
            Hcrp.CarroUrgenciaPsicoativo.Entity.TempGenericaHC result = new Hcrp.CarroUrgenciaPsicoativo.Entity.TempGenericaHC();

            try
            {
                StringBuilder str = new StringBuilder();


                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT T.* ");
                    str.AppendLine(" FROM TEMP_GENERICA_HC T ");
                    str.AppendLine(" WHERE T.NUM_SEQUENCIA = :NUM_SEQUENCIA ");

                    query = new QueryCommandConfig(str.ToString());

                    query.Params["NUM_SEQUENCIA"] = pNumID;

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        dr.Read();
                        result.num_sequencia = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_SEQUENCIA");
                        result.dsc_conteudo = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "DSC_CONTEUDO");
                        result.num_campo1 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO1");
                        result.num_campo2 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO2");
                        result.num_campo3 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO3");
                        result.num_campo4 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO4");
                        result.num_campo5 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO5");
                        result.num_campo6 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO6");
                        result.num_campo7 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO7");
                        result.num_campo8 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO8");
                        result.num_campo9 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO9");
                        result.num_campo10 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO10");
                    }
                    finally
                    {
                        dr.Close();
                        dr.Dispose();
                        
                    }



                    //// Preparar o retorno
                    //while (dr.Read())
                    //{
                        

                    //    if (dr["NUM"] != DBNull.Value)
                    //        cid10.Descricao = dr["DOENCA"].ToString();

                    //    break;
                    //}
                }

            }
            catch (Exception err)
            {
                throw err;
            }

            return result;
        }
        public static List<Hcrp.CarroUrgenciaPsicoativo.Entity.TempGenericaHC> getTempGenericaHC(string pDscConteudo = "", int pNumSequencia = 0)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.TempGenericaHC> result = new List<Hcrp.CarroUrgenciaPsicoativo.Entity.TempGenericaHC>();

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT T.* ");
                    str.AppendLine(" FROM TEMP_GENERICA_HC T ");
                    str.AppendLine(" WHERE (1=1) " );

                    if (pNumSequencia > 0)
                    {
                        str.AppendLine(" AND (T.NUM_SEQUENCIA = :NUM_SEQUENCIA)");
                    }


                    if (!string.IsNullOrEmpty(pDscConteudo))
                    {
                        str.AppendLine(" AND (T.DSC_CONTEUDO LIKE '%:DSC_CONTEUDO%') ");
                    }

                    query = new QueryCommandConfig(str.ToString());

                    if (pNumSequencia > 0)
                    {
                        query.Params["NUM_SEQUENCIA"] = pNumSequencia;
                    }

                    if (!string.IsNullOrEmpty(pDscConteudo))
                    {
                        query.Params["DSC_CONTEUDO"] = pNumSequencia;
                    }

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        
                        while (dr.Read())
                        {
                            Hcrp.CarroUrgenciaPsicoativo.Entity.TempGenericaHC record = new Hcrp.CarroUrgenciaPsicoativo.Entity.TempGenericaHC();

                            record.num_sequencia = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_SEQUENCIA");
                            record.dsc_conteudo = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "DSC_CONTEUDO");
                            record.num_campo1 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO1");
                            record.num_campo2 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO2");
                            record.num_campo3 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO3");
                            record.num_campo4 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO4");
                            record.num_campo5 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO5");
                            record.num_campo6 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO6");
                            record.num_campo7 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO7");
                            record.num_campo8 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO8");
                            record.num_campo9 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO9");
                            record.num_campo10 = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUM_CAMPO10");

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
        public static bool setTempGenericaHC(ref string pOutError, Hcrp.CarroUrgenciaPsicoativo.Entity.TempGenericaHC pObjetoGravar, bool pControlarTransacao = true)
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
                    if (pObjetoGravar.num_sequencia == 0)
                    {
                        // Inserção [FB, MO, AJSO]

                        #region Gerando ID [FB, MO, AJSO]

                        // Comando de consulta [FB, MO, AJSO]
                        QueryCommandConfig query;

                        // Retornando ID por MAX na tabela
                        str.Clear();
                        str.AppendLine(" SELECT NVL( MAX( NUM_SEQUENCIA ), 0 ) + 1 NUMID ");
                        str.AppendLine(" FROM TEMP_GENERICA_HC ");

                        // Retornando ID por Sequence
                        //str.AppendLine(" SELECT SEQUENCE.NEXTVAL NUMID");
                        //str.AppendLine(" FROM DUAL ");

                        query = new QueryCommandConfig(str.ToString());

                        // Obter a lista de registros
                        ctx.ExecuteQuery(query);

                        IDataReader dr = ctx.Reader;

                        try
                        {
                            dr.Read();
                            pObjetoGravar.num_sequencia = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "NUMID");


                        }
                        finally
                        {
                            dr.Close();
                            dr.Dispose();
                        }

                        #endregion


                        Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("TEMP_GENERICA_HC");
                        comando.Params["NUM_SEQUENCIA"] = pObjetoGravar.num_sequencia;
                        comando.Params["NUM_CAMPO1"] = pObjetoGravar.num_campo1;
                        comando.Params["NUM_CAMPO2"] = pObjetoGravar.num_campo2;
                        comando.Params["NUM_CAMPO3"] = pObjetoGravar.num_campo3;
                        comando.Params["NUM_CAMPO4"] = pObjetoGravar.num_campo4;
                        comando.Params["NUM_CAMPO5"] = pObjetoGravar.num_campo5;
                        comando.Params["NUM_CAMPO6"] = pObjetoGravar.num_campo6;
                        comando.Params["NUM_CAMPO7"] = pObjetoGravar.num_campo7;
                        comando.Params["NUM_CAMPO8"] = pObjetoGravar.num_campo8;
                        comando.Params["NUM_CAMPO9"] = pObjetoGravar.num_campo9;
                        comando.Params["NUM_CAMPO10"] = pObjetoGravar.num_campo10;

                        ctx.ExecuteInsert(comando);

                        if (pControlarTransacao)
                        {
                            ctx.Commit();
                        }
                    }
                    else
                    {
                        // Atualização [FB, MO, AJSO]


                        if (pControlarTransacao)
                        {
                            // Iniciando a transação 
                            ctx.BeginTransaction();
                        }

                        Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("TEMP_GENERICA_HC");
                        comando.FilterParams["NUM_SEQUENCIA"] = pObjetoGravar.num_sequencia;
                        comando.Params["NUM_CAMPO1"] = pObjetoGravar.num_campo1;
                        comando.Params["NUM_CAMPO2"] = pObjetoGravar.num_campo2;
                        comando.Params["NUM_CAMPO3"] = pObjetoGravar.num_campo3;
                        comando.Params["NUM_CAMPO4"] = pObjetoGravar.num_campo4;
                        comando.Params["NUM_CAMPO5"] = pObjetoGravar.num_campo5;
                        comando.Params["NUM_CAMPO6"] = pObjetoGravar.num_campo6;
                        comando.Params["NUM_CAMPO7"] = pObjetoGravar.num_campo7;
                        comando.Params["NUM_CAMPO8"] = pObjetoGravar.num_campo8;
                        comando.Params["NUM_CAMPO9"] = pObjetoGravar.num_campo9;
                        comando.Params["NUM_CAMPO10"] = pObjetoGravar.num_campo10;

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
