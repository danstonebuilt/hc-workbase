using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hcrp.Infra.AcessoDado;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class TipoBem
    {
        public List<Entity.TipoBem> ObterListaTipoBem(string codigoTipoBem, string nomeTipoBem)
        {
            List<Entity.TipoBem> listRetorno = new List<Entity.TipoBem>();

            Entity.TipoBem tipoBem = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    str.AppendLine(" SELECT TP.COD_TIPO_BEM, TP.NOM_TIPO_BEM ");
                    str.AppendLine("   FROM MANUTENCAO.TIPO_BEM TP ");
                    str.AppendLine("  WHERE TP.COD_TIPO_BEM = NVL(:COD_TIPO_BEM,TP.COD_TIPO_BEM) ");
                    str.AppendLine("    AND TP.NOM_TIPO_BEM LIKE NVL(:NOM_TIPO_BEM,TP.NOM_TIPO_BEM) AND TP.IDF_ATIVO = 'S' ");


                    QueryCommandConfig query = new QueryCommandConfig(str.ToString());

                    if (!string.IsNullOrEmpty(codigoTipoBem))
                        query.Params["COD_TIPO_BEM"] = codigoTipoBem.ToUpper();
                    else
                        query.Params["COD_TIPO_BEM"] = DBNull.Value;

                    if (!string.IsNullOrEmpty(nomeTipoBem))
                        query.Params["NOM_TIPO_BEM"] = nomeTipoBem.ToUpper() + "%";
                    else
                        query.Params["NOM_TIPO_BEM"] = DBNull.Value;

                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        tipoBem = new Entity.TipoBem();

                        if (dr["COD_TIPO_BEM"] != DBNull.Value)
                            tipoBem.CodigoTipoBem = Convert.ToInt64(dr["COD_TIPO_BEM"].ToString());

                        if (dr["NOM_TIPO_BEM"] != DBNull.Value)
                            tipoBem.NomeTipoBem = dr["NOM_TIPO_BEM"].ToString();

                        listRetorno.Add(tipoBem);
                    }
                }
            }
            catch (Exception err)
            {
                throw;
            }

            return listRetorno;
        }
    }
}
