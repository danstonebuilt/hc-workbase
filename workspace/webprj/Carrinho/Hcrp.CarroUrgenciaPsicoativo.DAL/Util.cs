using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class Util
    {
        #region Métodos

        /// <summary>
        /// Obter data do servidor.
        /// </summary>        
        public DateTime ObterDataEHoraDoBanco(bool comHora)
        {
            DateTime dataRetorno = DateTime.MinValue;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    if (comHora == true)
                        str.AppendLine(" SELECT TO_CHAR(SYSDATE, 'DD/MM/YYYY HH24:MI:SS') AS DATA FROM DUAL ");
                    else
                        str.AppendLine(" SELECT TO_CHAR(SYSDATE, 'DD/MM/YYYY') AS DATA FROM DUAL ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            dataRetorno = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<DateTime>(dr, "DATA");

                            break;
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

            return dataRetorno;
        }

        /// <summary>
        /// Obter servidor de imagem
        /// </summary>        
        public string ObterServidorDeImagem()
        {
            string retorno = string.Empty;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT GENERICO.FCN_BUSCA_SRV_IMG('') SERV_IMG FROM DUAL ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            retorno = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "SERV_IMG");

                            break;
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

            return retorno;
        }

        /// <summary>
        /// Obter servidor de imagem e servidor web.
        /// </summary>        
        public DataView ObterServidorDeImagemEServidorWeb(string servidorImagem, Int64 numExame, Int32 digito)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();                   

                    // Query Principal
                    str.AppendLine(" SELECT DECODE(GENERICO.FCN_BUSCA_SRV_IMG(''),'-1','10.165.0.100') SERV_IMG, ");
                    str.AppendLine("        GENERICO.FCN_BUSCA_IP_SERV_WEB SERV_WEB, ");
                    str.AppendLine("        EXR.COD_PACIENTE, EXR.NUM_PEDIDO_EXAME_ITEM, ");
                    str.AppendLine("        'http://" + servidorImagem + ":8080/lyriaViewer-web/login?action=login&&user=external&&password=6a21b6995a068148bbb65c8f949b3fb2&&patientid='||EXR.COD_PACIENTE||'&&accessionNumber='||NVL(EXR.NUM_PEDIDO_EXAME_ITEM,-1)||'&&doCheck=true' URL ");
                    str.AppendLine(" FROM EXAME_RADIOLOGICO EXR ");
                    str.AppendLine(string.Format(" WHERE EXR.NUM_EXAME = {0} ", numExame));
                    str.AppendLine(string.Format(" AND EXR.DIG_EXAME = {0} ", digito));

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
        /// Obter servidor web
        /// </summary>        
        public string ObterServidorWeb()
        {
            string retorno = string.Empty;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT GENERICO.FCN_BUSCA_IP_SERV_WEB SERVIDOR_WEB FROM DUAL ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            retorno = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "SERVIDOR_WEB");

                            break;
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

            return retorno;
        }

        /// <summary>
        /// Obter diferença entre duas datas retornando em dias.
        /// </summary>        
        public string ObterDiferencaEntreDuasDatasRetornandoEmDias(DateTime dataInicial, DateTime dataFinal)
        {
            string retorno = string.Empty;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(string.Format(" SELECT FCN_DIFERENCA_DATA(TO_DATE('{0}', 'DD/MM/YYYY HH24:MI:SS'), (TO_DATE('{1}', 'DD/MM/YYYY HH24:MI:SS')), 3 ) AS DIFDIAS FROM DUAL ", dataFinal.ToString("dd/MM/yyyy"), dataInicial.ToString("dd/MM/yyyy")));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            retorno = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "DIFDIAS");

                            break;
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

            return retorno;
        }

        #endregion
    }
}
