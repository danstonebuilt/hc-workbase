using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class ConsumoMaterialCentroCusto : Hcrp.Framework.Classes.ConsumoMaterialCentroCusto
    {
        public List<Hcrp.Framework.Classes.ConsumoMaterialCentroCusto> BuscarConsumo(bool paginacao, int paginaAtual, out int totalRegistro, string planoConta, string itemPlanoConta, string ano, string codGrupo, string sortExpression, string sortDirection)
        {
            List<Hcrp.Framework.Classes.ConsumoMaterialCentroCusto> _listaDeRetorno = new List<Hcrp.Framework.Classes.ConsumoMaterialCentroCusto>();
            Hcrp.Framework.Classes.ConsumoMaterialCentroCusto _cm = null;
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

                    str.AppendLine(" SELECT Y.COD_CENCUSTO, Y.NOM_CENCUSTO, Y.DSC_GRUPO, Y.NUM_ANO, Y.JANEIRO, Y.FEVEREIRO, Y.MARCO, Y.ABRIL, ");
                    str.AppendLine("        Y.MAIO, Y.JUNHO, Y.JULHO, Y.AGOSTO, Y.SETEMBRO, Y.OUTUBRO, Y.NOVEMBRO, Y.DEZEMBRO, ");
                    str.AppendLine("        SUM(Y.JANEIRO+Y.FEVEREIRO+Y.MARCO+Y.ABRIL+Y.MAIO+Y.JUNHO+Y.JULHO+Y.AGOSTO+Y.SETEMBRO+Y.OUTUBRO+Y.NOVEMBRO+Y.DEZEMBRO) AS TOTAL ");
                    str.AppendLine(" FROM ( ");
                    str.AppendLine(" SELECT X.COD_CENCUSTO, X.NOM_CENCUSTO, X.DSC_GRUPO, X.NUM_ANO, SUM(X.JANEIRO) AS JANEIRO, SUM(X.FEVEREIRO) AS FEVEREIRO, SUM(X.MARCO) AS MARCO, ");
                    str.AppendLine("        SUM(X.ABRIL) AS ABRIL, SUM(X.MAIO) AS MAIO, SUM(X.JUNHO) AS JUNHO, SUM(X.JULHO) AS JULHO, SUM(X.AGOSTO) AS AGOSTO, ");
                    str.AppendLine("        SUM(X.SETEMBRO) AS SETEMBRO, SUM(X.OUTUBRO) AS OUTUBRO, SUM(X.NOVEMBRO) AS NOVEMBRO, SUM(X.DEZEMBRO) AS DEZEMBRO ");
                    str.AppendLine(" FROM ( SELECT T.COD_CENCUSTO, T.NOM_CENCUSTO, T.DSC_GRUPO, T.NUM_ANO, ");
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
                    str.AppendLine(" SELECT CMM.COD_CENCUSTO, MAX(CC.NOM_CENCUSTO) NOM_CENCUSTO, GCC.DSC_GRUPO, CMM.NUM_ANO, CMM.NUM_MES, NVL(SUM(CMM.VLR_TOTAL_CONSUMO),0) VLR_TOTAL_CONSUMO ");
                    str.AppendLine(" FROM   CONSUMO_MENSAL_MATERIAL CMM, CENTRO_CUSTO CC, GRUPO_CENCUSTO_SICH GCC, MATERIAL M, GRUPO G");
                    if (planoConta != "0")
                        str.AppendLine(", GRUPO_PLANOCONTAITEM GP ");
                    str.AppendLine(string.Format(" WHERE  CMM.NUM_ANO = {0} ", ano));
                    str.AppendLine("    AND CMM.VLR_TOTAL_CONSUMO >= 0 ");
                    str.AppendLine(string.Format("    AND GCC.COD_INST_SISTEMA = {0} ", idInstSistema));
                    str.AppendLine("    AND CMM.COD_CENCUSTO     = CC.COD_CENCUSTO ");
                    str.AppendLine("    AND CC.COD_GRUPO_SICH    = GCC.COD_GRUPO_SICH ");
                    str.AppendLine("    AND CMM.COD_MATERIAL     = M.COD_MATERIAL ");
                    str.AppendLine("    AND M.COD_GRUPO          = G.COD_GRUPO ");
                    if (planoConta != "0")
                    {
                        str.AppendLine("    AND G.COD_GRUPO          = GP.COD_GRUPO ");
                        str.AppendLine(string.Format("    AND GP.SEQ_PLANO_CONTA = {0} ", planoConta));
                        if (itemPlanoConta != "0")
                        {
                            str.AppendLine(string.Format(" AND GP.SEQ_ITEM_PLANO_CONTA IN (SELECT SEQ_ITEM_PLANO_CONTA FROM PLANO_CONTA_ITEM WHERE SEQ_PLANO_CONTA = {0} ", planoConta));
                            str.AppendLine(" CONNECT BY PRIOR SEQ_ITEM_PLANO_CONTA = SEQ_ITEM_PLANO_CONTA_PAI ");
                            str.AppendLine(string.Format(" START WITH SEQ_ITEM_PLANO_CONTA = {0}) ", itemPlanoConta));
                        }
                    }
                    if (!string.IsNullOrEmpty(codGrupo))
                    str.AppendLine(string.Format("    AND GCC.COD_GRUPO_SICH = {0} ", codGrupo));
                    str.AppendLine(" GROUP BY CMM.COD_CENCUSTO, GCC.DSC_GRUPO, CMM.NUM_ANO, CMM.NUM_MES ");
                    str.AppendLine(" ORDER BY NOM_CENCUSTO,CMM.NUM_MES ");
                    str.AppendLine(" ) T GROUP BY T.COD_CENCUSTO, T.NOM_CENCUSTO, T.DSC_GRUPO, T.NUM_ANO, T.NUM_MES ");
                    str.AppendLine(" ) X GROUP BY X.COD_CENCUSTO, X.NOM_CENCUSTO, X.DSC_GRUPO, X.NUM_ANO ");
                    str.AppendLine(" ) Y GROUP BY Y.COD_CENCUSTO, Y.NOM_CENCUSTO, Y.DSC_GRUPO, Y.NUM_ANO, Y.JANEIRO, Y.FEVEREIRO, Y.MARCO, Y.ABRIL, Y.MAIO, ");
                    str.AppendLine("              Y.JUNHO, Y.JULHO, Y.AGOSTO, Y.SETEMBRO, Y.OUTUBRO, Y.NOVEMBRO, Y.DEZEMBRO ");
                    if (string.IsNullOrEmpty(sortDirection))
                        str.AppendLine(" ORDER BY Y.NOM_CENCUSTO ");
                    else
                    {
                        if (sortExpression == "Nome")
                            str.AppendLine(string.Format(" ORDER BY Y.NOM_CENCUSTO {0} ", sortDirection));
                        else if (sortExpression == "Grupo")
                            str.AppendLine(string.Format(" ORDER BY Y.DSC_GRUPO {0} ", sortDirection));
                        else if (sortExpression == "Codigo")
                            str.AppendLine(string.Format(" ORDER BY Y.COD_CENCUSTO {0} ", sortDirection));
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
                        _cm = new Hcrp.Framework.Classes.ConsumoMaterialCentroCusto();

                        if (dr["COD_CENCUSTO"] != DBNull.Value)
                            _cm.Codigo = dr["COD_CENCUSTO"].ToString();
                        if (dr["NOM_CENCUSTO"] != DBNull.Value)
                            _cm.Nome = dr["NOM_CENCUSTO"].ToString();
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
