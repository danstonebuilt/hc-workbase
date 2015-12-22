using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class ConsumoMaterialGrupo : Hcrp.Framework.Classes.ConsumoMaterialGrupo
    {
        public List<Hcrp.Framework.Classes.ConsumoMaterialGrupo> BuscarConsumo(bool paginacao, int paginaAtual, out int totalRegistro, string planoConta, string itemPlanoConta, string ano, string sortExpression, string sortDirection)
        {
            List<Hcrp.Framework.Classes.ConsumoMaterialGrupo> _listaDeRetorno = new List<Hcrp.Framework.Classes.ConsumoMaterialGrupo>();
            Hcrp.Framework.Classes.ConsumoMaterialGrupo _cm = null;
            Hcrp.Infra.AcessoDado.QueryCommandConfig query = null;
            Hcrp.Infra.AcessoDado.QueryCommandConfig queryCount = null;
            totalRegistro = 0;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    int idInstSistema = new Hcrp.Framework.Classes.ConfiguracaoSistema().CodInstituicaoSistema;
                    // Cria a query
                    StringBuilder str = new StringBuilder();
                    StringBuilder strTotalRegistro = new StringBuilder();
                    StringBuilder strPaginacao = new StringBuilder();

                    // Montar escopo de paginação.
                    Int32 numeroRegistroPorPagina = Hcrp.Framework.Infra.Util.Parametrizacao.Instancia().QuantidadeRegistroPagina; /*Ver*/
                    Int32 ultimoIndice = (numeroRegistroPorPagina * paginaAtual);
                    Int32 primeiroIndice = (ultimoIndice - numeroRegistroPorPagina) + 1;

                    str.AppendLine(" SELECT Y.COD_GRUPO_SICH, Y.DSC_GRUPO, Y.NUM_ANO, Y.JANEIRO, Y.FEVEREIRO, Y.MARCO, Y.ABRIL, ");
                    str.AppendLine("        Y.MAIO, Y.JUNHO, Y.JULHO, Y.AGOSTO, Y.SETEMBRO, Y.OUTUBRO, Y.NOVEMBRO, Y.DEZEMBRO, ");
                    str.AppendLine("        SUM(Y.JANEIRO+Y.FEVEREIRO+Y.MARCO+Y.ABRIL+Y.MAIO+Y.JUNHO+Y.JULHO+Y.AGOSTO+Y.SETEMBRO+Y.OUTUBRO+Y.NOVEMBRO+Y.DEZEMBRO) AS TOTAL ");
                    str.AppendLine(" FROM ( ");
                    str.AppendLine(" SELECT X.COD_GRUPO_SICH, X.DSC_GRUPO, X.NUM_ANO, SUM(X.JANEIRO) AS JANEIRO, SUM(X.FEVEREIRO) AS FEVEREIRO, SUM(X.MARCO) AS MARCO, ");
                    str.AppendLine("        SUM(X.ABRIL) AS ABRIL, SUM(X.MAIO) AS MAIO, SUM(X.JUNHO) AS JUNHO, SUM(X.JULHO) AS JULHO, SUM(X.AGOSTO) AS AGOSTO, ");
                    str.AppendLine("        SUM(X.SETEMBRO) AS SETEMBRO, SUM(X.OUTUBRO) AS OUTUBRO, SUM(X.NOVEMBRO) AS NOVEMBRO, SUM(X.DEZEMBRO) AS DEZEMBRO ");
                    str.AppendLine(" FROM ( SELECT T.COD_GRUPO_SICH, T.DSC_GRUPO, T.NUM_ANO, ");
                    str.AppendLine("        CASE WHEN T.NUM_MES = 1 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS JANEIRO, ");
                    str.AppendLine("        CASE WHEN T.NUM_MES = 2 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS FEVEREIRO, ");
                    str.AppendLine("        CASE WHEN T.NUM_MES = 3 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS MARCO, ");
                    str.AppendLine("        CASE WHEN T.NUM_MES = 4 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS ABRIL, ");
                    str.AppendLine("        CASE WHEN T.NUM_MES = 5 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS MAIO, ");
                    str.AppendLine("        CASE WHEN T.NUM_MES = 6 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS JUNHO, ");
                    str.AppendLine("        CASE WHEN T.NUM_MES = 7 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS JULHO, ");
                    str.AppendLine("        CASE WHEN T.NUM_MES = 8 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS AGOSTO, ");
                    str.AppendLine("        CASE WHEN T.NUM_MES = 9 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS SETEMBRO, ");
                    str.AppendLine("        CASE WHEN T.NUM_MES = 10 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS OUTUBRO, ");
                    str.AppendLine("        CASE WHEN T.NUM_MES = 11 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS NOVEMBRO, ");
                    str.AppendLine("        CASE WHEN T.NUM_MES = 12 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS DEZEMBRO ");
                    str.AppendLine(" FROM ( ");
                    str.AppendLine(" SELECT O.COD_GRUPO_SICH, MAX(O.DSC_GRUPO) DSC_GRUPO, O.NUM_ANO, O.NUM_MES, NVL(SUM(O.VLR_TOTAL_CONSUMO),0) VLR_TOTAL_CONSUMO ");
                    str.AppendLine(" FROM ( ");
                    str.AppendLine(" SELECT GCC.COD_GRUPO_SICH, GCC.DSC_GRUPO, CMM.NUM_ANO, CMM.NUM_MES, ");
                    str.AppendLine("        CASE ");
                    str.AppendLine("            WHEN R.PCT_RATEIO IS NULL THEN CMM.VLR_TOTAL_CONSUMO ");
                    str.AppendLine("            ELSE CMM.VLR_TOTAL_CONSUMO-(CMM.VLR_TOTAL_CONSUMO*(R.PCT_RATEIO/100)) ");
                    str.AppendLine("        END AS VLR_TOTAL_CONSUMO ");
                    str.AppendLine(" FROM   CONSUMO_MENSAL_MATERIAL CMM ");
                    str.AppendLine(" INNER JOIN CENTRO_CUSTO CC ON CMM.COD_CENCUSTO = CC.COD_CENCUSTO ");
                    str.AppendLine(" INNER JOIN GRUPO_CENCUSTO_SICH GCC ON CC.COD_GRUPO_SICH = GCC.COD_GRUPO_SICH ");
                    str.AppendLine(" INNER JOIN MATERIAL M ON CMM.COD_MATERIAL = M.COD_MATERIAL ");
                    str.AppendLine(" INNER JOIN GRUPO G ON M.COD_GRUPO = G.COD_GRUPO ");
                    if (planoConta != "0")
                    {
                        str.AppendLine(" INNER JOIN GRUPO_PLANOCONTAITEM GP ON G.COD_GRUPO = GP.COD_GRUPO ");
                        str.AppendLine(string.Format(" AND GP.SEQ_PLANO_CONTA = {0} ", planoConta));
                        if (itemPlanoConta != "0")
                        {
                            str.AppendLine(string.Format(" AND GP.SEQ_ITEM_PLANO_CONTA IN (SELECT SEQ_ITEM_PLANO_CONTA FROM PLANO_CONTA_ITEM WHERE SEQ_PLANO_CONTA = {0} ", planoConta));
                            str.AppendLine(" CONNECT BY PRIOR SEQ_ITEM_PLANO_CONTA = SEQ_ITEM_PLANO_CONTA_PAI ");
                            str.AppendLine(string.Format(" START WITH SEQ_ITEM_PLANO_CONTA = {0})", itemPlanoConta));
                        }
                    }
                    str.AppendLine(" LEFT JOIN RATEIO_MAT_GP_CENCUSTO R ON M.COD_MATERIAL = R.COD_MATERIAL AND CC.COD_CENCUSTO = R.COD_CENCUSTO ");
                    str.AppendLine(string.Format(" WHERE  CMM.NUM_ANO = {0} ", ano));
                    str.AppendLine(string.Format("    AND GCC.COD_INST_SISTEMA    = {0} ", idInstSistema));
                    str.AppendLine("    AND CMM.VLR_TOTAL_CONSUMO >= 0 ");
                    str.AppendLine(" UNION ");
                    str.AppendLine(" SELECT GCC2.COD_GRUPO_SICH, MAX(GCC2.DSC_GRUPO) DSC_GRUPO, CMM.NUM_ANO, CMM.NUM_MES, ");
                    str.AppendLine("       (CMM.VLR_TOTAL_CONSUMO*(R.PCT_RATEIO/100)) VLR_TOTAL_CONSUMO ");
                    str.AppendLine(" FROM  CONSUMO_MENSAL_MATERIAL CMM ");
                    str.AppendLine("       INNER JOIN CENTRO_CUSTO CC ON CMM.COD_CENCUSTO = CC.COD_CENCUSTO ");
                    str.AppendLine("       INNER JOIN GRUPO_CENCUSTO_SICH GCC ON CC.COD_GRUPO_SICH = GCC.COD_GRUPO_SICH ");
                    str.AppendLine("       INNER JOIN MATERIAL M ON CMM.COD_MATERIAL = M.COD_MATERIAL ");
                    str.AppendLine("       INNER JOIN GRUPO G ON M.COD_GRUPO = G.COD_GRUPO ");
                    if (planoConta != "0")
                    {
                        str.AppendLine("       INNER JOIN GRUPO_PLANOCONTAITEM GP ON G.COD_GRUPO = GP.COD_GRUPO ");
                        str.AppendLine(string.Format(" AND GP.SEQ_PLANO_CONTA = {0} ", planoConta));
                        if (itemPlanoConta != "0")
                        {
                            str.AppendLine(string.Format(" AND GP.SEQ_ITEM_PLANO_CONTA IN (SELECT SEQ_ITEM_PLANO_CONTA FROM PLANO_CONTA_ITEM WHERE SEQ_PLANO_CONTA = {0} ", planoConta));
                            str.AppendLine(" CONNECT BY PRIOR SEQ_ITEM_PLANO_CONTA = SEQ_ITEM_PLANO_CONTA_PAI ");
                            str.AppendLine(string.Format(" START WITH SEQ_ITEM_PLANO_CONTA = {0})", itemPlanoConta));
                        }
                    }
                    str.AppendLine("       RIGHT JOIN RATEIO_MAT_GP_CENCUSTO R ON M.COD_MATERIAL = R.COD_MATERIAL AND CC.COD_CENCUSTO = R.COD_CENCUSTO ");
                    str.AppendLine("       INNER JOIN GRUPO_CENCUSTO_SICH GCC2 ON GCC2.COD_GRUPO_SICH = R.COD_GRUPO_SICH ");
                    str.AppendLine(string.Format(" WHERE  CMM.NUM_ANO = {0} ", ano));
                    str.AppendLine(string.Format("    AND GCC.COD_INST_SISTEMA    = {0} ", idInstSistema));
                    str.AppendLine("    AND CMM.VLR_TOTAL_CONSUMO >= 0 ");
                    str.AppendLine(" GROUP BY GCC2.COD_GRUPO_SICH, CMM.NUM_ANO, CMM.NUM_MES, R.PCT_RATEIO, CMM.VLR_TOTAL_CONSUMO ");
                    str.AppendLine(" ) O GROUP BY O.COD_GRUPO_SICH, O.NUM_ANO, O.NUM_MES ");
                    str.AppendLine(" ) T GROUP BY T.COD_GRUPO_SICH, T.DSC_GRUPO, T.NUM_ANO, T.NUM_MES ");
                    str.AppendLine(" ) X GROUP BY X.COD_GRUPO_SICH, X.DSC_GRUPO, X.NUM_ANO ");
                    str.AppendLine(" ) Y GROUP BY Y.COD_GRUPO_SICH, Y.DSC_GRUPO, Y.NUM_ANO, Y.JANEIRO, Y.FEVEREIRO, Y.MARCO, Y.ABRIL, Y.MAIO, ");
                    str.AppendLine("              Y.JUNHO, Y.JULHO, Y.AGOSTO, Y.SETEMBRO, Y.OUTUBRO, Y.NOVEMBRO, Y.DEZEMBRO ");
                    if (string.IsNullOrEmpty(sortDirection))
                        str.AppendLine(" ORDER BY Y.DSC_GRUPO ");
                    else
                    {
                        if (sortExpression == "Grupo")
                            str.AppendLine(string.Format(" ORDER BY Y.DSC_GRUPO {0} ", sortDirection));
                        else if (sortExpression == "Codigo")
                            str.AppendLine(string.Format(" ORDER BY Y.COD_GRUPO_SICH {0} ", sortDirection));
                        else
                            str.AppendLine(string.Format(" ORDER BY {0} {1}", sortExpression, sortDirection));
                    }

                    // Preparar a query
                    if (paginacao)
                    {
                        strTotalRegistro.AppendLine(" SELECT COUNT(*) TOTAL FROM ( ");
                        strTotalRegistro.AppendLine(str.ToString());
                        strTotalRegistro.AppendLine(" ) A ");

                        strPaginacao.AppendLine(" SELECT * FROM (SELECT A.*, ROWNUM AS RNUM FROM ( ");
                        strPaginacao.AppendLine(str.ToString());
                        strPaginacao.AppendLine(" ) A WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

                        query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(strPaginacao.ToString());
                        queryCount = new Hcrp.Infra.AcessoDado.QueryCommandConfig(strTotalRegistro.ToString());
                    }
                    else
                        query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Abre conexão
                    ctx.Open();

                    if (paginacao)
                    {
                        // Veriricar contador
                        ctx.ExecuteQuery(queryCount);

                        while (ctx.Reader.Read())
                        {
                            totalRegistro = Convert.ToInt32(ctx.Reader["TOTAL"]);
                        }
                    }

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _cm = new Hcrp.Framework.Classes.ConsumoMaterialGrupo();

                        if (dr["COD_GRUPO_SICH"] != DBNull.Value)
                            _cm.Codigo = Convert.ToInt32(dr["COD_GRUPO_SICH"]);
                        if (dr["DSC_GRUPO"] != DBNull.Value)
                            _cm.Grupo = dr["DSC_GRUPO"].ToString();
                        if (dr["JANEIRO"] != DBNull.Value)
                            _cm.Janeiro = Convert.ToDouble(dr["JANEIRO"]);
                        if (dr["FEVEREIRO"] != DBNull.Value)
                            _cm.Fevereiro = Convert.ToDouble(dr["FEVEREIRO"]);
                        if (dr["MARCO"] != DBNull.Value)
                            _cm.Marco = Convert.ToDouble(dr["MARCO"]);
                        if (dr["ABRIL"] != DBNull.Value)
                            _cm.Abril = Convert.ToDouble(dr["ABRIL"]);
                        if (dr["MAIO"] != DBNull.Value)
                            _cm.Maio = Convert.ToDouble(dr["MAIO"]);
                        if (dr["JUNHO"] != DBNull.Value)
                            _cm.Junho = Convert.ToDouble(dr["JUNHO"]);
                        if (dr["JULHO"] != DBNull.Value)
                            _cm.Julho = Convert.ToDouble(dr["JULHO"]);
                        if (dr["AGOSTO"] != DBNull.Value)
                            _cm.Agosto = Convert.ToDouble(dr["AGOSTO"]);
                        if (dr["SETEMBRO"] != DBNull.Value)
                            _cm.Setembro = Convert.ToDouble(dr["SETEMBRO"]);
                        if (dr["OUTUBRO"] != DBNull.Value)
                            _cm.Outubro = Convert.ToDouble(dr["OUTUBRO"]);
                        if (dr["NOVEMBRO"] != DBNull.Value)
                            _cm.Novembro = Convert.ToDouble(dr["NOVEMBRO"]);
                        if (dr["DEZEMBRO"] != DBNull.Value)
                            _cm.Dezembro = Convert.ToDouble(dr["DEZEMBRO"]);
                        if (dr["TOTAL"] != DBNull.Value)
                            _cm.Total = Convert.ToDouble(dr["TOTAL"]);

                        _listaDeRetorno.Add(_cm);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaDeRetorno;
        }
    }
}
