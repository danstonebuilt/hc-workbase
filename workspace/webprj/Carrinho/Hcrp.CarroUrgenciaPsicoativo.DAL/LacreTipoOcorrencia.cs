using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class LacreTipoOcorrencia
    {
        #region Métodos

        /// <summary>
        /// Obter por todos os lacre tipo ocorrencia ativos.
        /// </summary>                
        public List<Entity.LacreTipoOcorrencia> ObterTodosAtivos()
        {
            List<Entity.LacreTipoOcorrencia> listaRetorno = new List<Entity.LacreTipoOcorrencia>();
            Entity.LacreTipoOcorrencia lacreTipoOcorrencia = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT O.SEQ_LACRE_TIP_OCORRENCIA, ");
                    str.AppendLine("        O.DSC_TIPO_OCORRENCIA, ");
                    str.AppendLine("        O.IDF_ATIVO ");
                    str.AppendLine(" FROM LACRE_TIPO_OCORRENCIA O ");
                    str.AppendLine(" WHERE O.IDF_ATIVO = 'S' ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lacreTipoOcorrencia = new Entity.LacreTipoOcorrencia();

                            if (dr["SEQ_LACRE_TIP_OCORRENCIA"] != DBNull.Value)
                                lacreTipoOcorrencia.SeqLacreTipoOCorrencia = Convert.ToInt64(dr["SEQ_LACRE_TIP_OCORRENCIA"]);

                            if (dr["DSC_TIPO_OCORRENCIA"] != DBNull.Value)
                                lacreTipoOcorrencia.DscTipoOcorrencia = dr["DSC_TIPO_OCORRENCIA"].ToString();

                            if (dr["IDF_ATIVO"] != DBNull.Value)
                                lacreTipoOcorrencia.IdfAtivo = dr["IDF_ATIVO"].ToString();

                            listaRetorno.Add(lacreTipoOcorrencia);

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

            return listaRetorno;
        }

        #endregion
    }
}
