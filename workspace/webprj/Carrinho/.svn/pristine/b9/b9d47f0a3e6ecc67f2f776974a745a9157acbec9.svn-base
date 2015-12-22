using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class TipoPatrimonio
    {
        #region Métodos

        /// <summary>
        /// Obter tipos de patrimonios.
        /// </summary>        
        public List<Entity.TipoPatrimonio> ObterTiposDePatrimonio()
        {
            List<Entity.TipoPatrimonio> listTipoPatrimonio = new List<Entity.TipoPatrimonio>();
            Entity.TipoPatrimonio tipoPatrimonio = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT COD_TIPO_PATRIMONIO, ");
                    str.AppendLine("        DSC_TIPO_PATRIMONIO ");
                    str.AppendLine(" FROM TIPO_PATRIMONIO ");
                    str.AppendLine(" ORDER BY DSC_TIPO_PATRIMONIO ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    // Preparar o retorno
                    while (ctx.Reader.Read())
                    {
                        tipoPatrimonio = new Entity.TipoPatrimonio();
                        
                        if (ctx.Reader["COD_TIPO_PATRIMONIO"] != DBNull.Value)
                            tipoPatrimonio.CodigoTipoPatrimonio = Convert.ToInt64(ctx.Reader["COD_TIPO_PATRIMONIO"]);

                        if (ctx.Reader["DSC_TIPO_PATRIMONIO"] != DBNull.Value)
                            tipoPatrimonio.Descricao = ctx.Reader["DSC_TIPO_PATRIMONIO"].ToString();

                        listTipoPatrimonio.Add(tipoPatrimonio);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return listTipoPatrimonio;
        }

        #endregion
    }
}
