using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class CentroDeCusto
    {
        /// <summary>
        /// Obter centro de custo por id e instituto
        /// </summary>
        /// <param name="idCentroDeCusto"></param>
        /// <returns></returns>
        public Hcrp.Framework.Classes.CentroDeCusto ObterCentroDeCusto(string idCentroDeCusto, int cod_instituto)
        {
            Hcrp.Framework.Classes.CentroDeCusto _centroDeCusto = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT ");
                    str.AppendLine("    COD_CENCUSTO, ");
                    str.AppendLine("    NOM_CENCUSTO ");
                    str.AppendLine(" FROM CENTRO_CUSTO WHERE COD_CENCUSTO = '" + idCentroDeCusto + "'");
                    str.AppendLine(" AND COD_INSTITUTO = '" + cod_instituto + "'");
                    str.AppendLine(" AND IDF_ATIVO = 'S' ");


                    // Abrir conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(str.ToString());

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _centroDeCusto = new Hcrp.Framework.Classes.CentroDeCusto();

                        if (dr["COD_CENCUSTO"] != DBNull.Value)
                            _centroDeCusto.Codigo = dr["COD_CENCUSTO"].ToString();

                        if (dr["NOM_CENCUSTO"] != DBNull.Value)
                            _centroDeCusto.Nome = dr["NOM_CENCUSTO"].ToString();

                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _centroDeCusto;
        }

    }
}
