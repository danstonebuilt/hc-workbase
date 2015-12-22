using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class TipoRepositorioListaControle
    {
        #region Métodos

        /// <summary>
        /// Obter tipos de patrimonios.
        /// </summary>        
        public List<Entity.TipoRepositorioListaControle> ListarTiposRepositorio()
        {
            List<Entity.TipoRepositorioListaControle> listTipoRepositorio = new List<Entity.TipoRepositorioListaControle>();
            Entity.TipoRepositorioListaControle tipoRepositorio = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT SEQ_TIP_REPOSIT_LST_CONTROL, ");
                    str.AppendLine("          DSC_TIPO_REPOSITORIO ");
                    str.AppendLine("    FROM GENERICO.TIPO_REPOSITORIO_LST_CONTROLE X ");
                    str.AppendLine("   WHERE X.IDF_ATIVO = 'S' ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    // Preparar o retorno
                    while (ctx.Reader.Read())
                    {
                        tipoRepositorio = new Entity.TipoRepositorioListaControle();

                        if (ctx.Reader["SEQ_TIP_REPOSIT_LST_CONTROL"] != DBNull.Value)
                            tipoRepositorio.SeqTipoRepositorioLstControl = Convert.ToInt64(ctx.Reader["SEQ_TIP_REPOSIT_LST_CONTROL"]);

                        if (ctx.Reader["DSC_TIPO_REPOSITORIO"] != DBNull.Value)
                            tipoRepositorio.DscTipoRepositorio = ctx.Reader["DSC_TIPO_REPOSITORIO"].ToString();

                        listTipoRepositorio.Add(tipoRepositorio);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return listTipoRepositorio;
        }

        #endregion
    }
}
