using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Framework.Infra.Util;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class Lote
    {
        #region Métodos

        /// <summary>
        /// Obter por id.
        /// </summary>        
        public Entity.Lote ObterPorId(Int64 numLote)
        {   
            Entity.Lote lote = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT L.NUM_LOTE, ");
                    str.AppendLine("        L.COD_MATERIAL, ");
                    str.AppendLine("        L.NUM_LOTE_FABRICANTE, ");
                    str.AppendLine("        L.DTA_VALIDADE_LOTE, ");
                    str.AppendLine("        L.QTD_LOTE, ");
                    str.AppendLine("        L.SEQ_NOTA_FISCAL, ");
                    str.AppendLine("        L.SEQ_LOTE ");
                    str.AppendLine(" FROM LOTE L ");
                    str.AppendLine(string.Format(" WHERE L.NUM_LOTE = {0} ", numLote));                    

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lote = new Entity.Lote();
                            lote.Material = new Entity.Material();

                            if (dr["NUM_LOTE"] != DBNull.Value)
                                lote.NumLote = Convert.ToInt64(dr["NUM_LOTE"]);

                            if (dr["COD_MATERIAL"] != DBNull.Value)
                                lote.Material.Codigo = dr["COD_MATERIAL"].ToString();

                            if (dr["NUM_LOTE_FABRICANTE"] != DBNull.Value)
                                lote.NumLoteFabricante = dr["NUM_LOTE_FABRICANTE"].ToString();

                            if (dr["DTA_VALIDADE_LOTE"] != DBNull.Value)
                                lote.DataValidadeLote = Convert.ToDateTime(dr["DTA_VALIDADE_LOTE"]);

                            if (dr["QTD_LOTE"] != DBNull.Value)
                                lote.QtdLote = Convert.ToInt32(dr["QTD_LOTE"]);

                            if (dr["SEQ_NOTA_FISCAL"] != DBNull.Value)
                                lote.SeqNotaFiscal = Convert.ToInt64(dr["SEQ_NOTA_FISCAL"]);

                            if (dr["SEQ_LOTE"] != DBNull.Value)
                                lote.SeqLote = Convert.ToInt64(dr["SEQ_LOTE"]);

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

            return lote;
        }

        /// <summary>
        /// Obter por codigo do material.
        /// </summary>        
        public List<Entity.Lote> ObterPorCodMaterial(string codMaterial)
        {
            List<Entity.Lote> listRetorno = new List<Entity.Lote>();
            Entity.Lote lote = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    //str.AppendLine(" SELECT DISTINCT MAX(A.NUM_LOTE) NUM_LOTE, ");
                    //str.AppendLine("    A.NUM_LOTE_FABRICANTE || ' - ' || TO_CHAR(A.DTA_VALIDADE_LOTE, 'DD/MM/YYYY') AS NUM_LOTE_FABRICANTE, ");
                    //str.AppendLine("    A.COD_MATERIAL, ");
                    //str.AppendLine("    A.DTA_VALIDADE_LOTE ");
                    ////str.AppendLine("    A.QTD_LOTE, ");
                    ////str.AppendLine("    A.SEQ_NOTA_FISCAL, ");
                    ////str.AppendLine("    A.SEQ_LOTE ");

                    //str.AppendLine(" FROM ESTOQUE.LOTE A ");
                    //str.AppendLine(string.Format(" WHERE A.COD_MATERIAL = '{0}' ", codMaterial));
                    //str.AppendLine("  AND TO_CHAR(A.DTA_VALIDADE_LOTE, 'YYYY') >= TO_CHAR(SYSDATE, 'YYYY') ");
                    //str.AppendLine(" group by  NUM_LOTE_FABRICANTE, A.COD_MATERIAl, A.DTA_VALIDADE_LOTE ");

                    str.AppendLine(" SELECT  A.NUM_LOTE NUM_LOTE, ");
                    str.AppendLine("    A.NUM_LOTE_FABRICANTE || ' - ' || TO_CHAR(A.DTA_VALIDADE_LOTE, 'DD/MM/YYYY') AS NUM_LOTE_FABRICANTE, ");
                    str.AppendLine("    A.COD_MATERIAL, ");
                    str.AppendLine("    A.DTA_VALIDADE_LOTE ");
                    //str.AppendLine("    A.QTD_LOTE, ");
                    //str.AppendLine("    A.SEQ_NOTA_FISCAL, ");
                    //str.AppendLine("    A.SEQ_LOTE ");

                    str.AppendLine(" FROM ESTOQUE.LOTE A, ITEM_NOTA_FISCAL b, CENTRO_CUSTO cc, INSTITUTO I  ");
                    str.AppendLine(" WHERE a.seq_nota_fiscal = b.seq_nota_fiscal ");
                    str.AppendLine(" AND a.cod_material = b.cod_material ");
                    str.AppendLine(" and CC.COD_CENCUSTO = B.COD_CENCUSTO_RECEBIMENTO ");

                    str.AppendLine(string.Format(" AND A.COD_MATERIAL = '{0}' ", codMaterial));
                    str.AppendLine("  AND TO_CHAR(A.DTA_VALIDADE_LOTE, 'YYYY') >= TO_CHAR(SYSDATE, 'YYYY') ");

                    str.AppendLine("  AND CC.COD_INSTITUTO = I.COD_INSTITUTO  ");
                    str.AppendLine(string.Format(" AND I.COD_INST_SISTEMA = '{0}' ", Parametrizacao.Instancia().CodInstituicao));
                    //str.AppendLine(" group by  NUM_LOTE_FABRICANTE, A.COD_MATERIAl, A.DTA_VALIDADE_LOTE ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lote = new Entity.Lote();
                            lote.Material = new Entity.Material();

                            if (dr["NUM_LOTE"] != DBNull.Value)
                                lote.NumLote = Convert.ToInt64(dr["NUM_LOTE"]);

                            if (dr["COD_MATERIAL"] != DBNull.Value)
                                lote.Material.Codigo = dr["COD_MATERIAL"].ToString();

                            if (dr["NUM_LOTE_FABRICANTE"] != DBNull.Value)
                                lote.NumLoteFabricante = dr["NUM_LOTE_FABRICANTE"].ToString();

                            if (dr["DTA_VALIDADE_LOTE"] != DBNull.Value)
                                lote.DataValidadeLote = Convert.ToDateTime(dr["DTA_VALIDADE_LOTE"]);

                            //if (dr["QTD_LOTE"] != DBNull.Value)
                            //    lote.QtdLote = Convert.ToInt32(dr["QTD_LOTE"]);

                            //if (dr["SEQ_NOTA_FISCAL"] != DBNull.Value)
                            //    lote.SeqNotaFiscal = Convert.ToInt64(dr["SEQ_NOTA_FISCAL"]);

                            //if (dr["SEQ_LOTE"] != DBNull.Value)
                            //    lote.SeqLote = Convert.ToInt64(dr["SEQ_LOTE"]);

                            listRetorno.Add(lote);
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

            return listRetorno;
        }

        #endregion
    }
}
