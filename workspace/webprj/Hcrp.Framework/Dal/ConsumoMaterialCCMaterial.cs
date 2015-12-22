using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class ConsumoMaterialCCMaterial : Hcrp.Framework.Classes.ConsumoMaterialCCMaterial
    {
        public List<Hcrp.Framework.Classes.ConsumoMaterialCCMaterial> BuscarConsumo(bool paginacao, int paginaAtual, out int totalRegistro, int classe, string ano, string codCentroCusto, string codMaterial, string planoConta, string itemPlanoConta, string sortExpression, string sortDirection)
        {
            List<Hcrp.Framework.Classes.ConsumoMaterialCCMaterial> _listaDeRetorno = new List<Hcrp.Framework.Classes.ConsumoMaterialCCMaterial>();
            Hcrp.Framework.Classes.ConsumoMaterialCCMaterial _cm = null;
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

                    str.AppendLine("  SELECT Z.COD_MATERIAL, Z.NOM_MATERIAL, Z.UNIDADE, Z.COD_PLANO, Z.PLANO, Z.NUM_ANO, Z.JANVALOR, Z.JANQTD, Z.JANPROG,  ");
                    str.AppendLine("         Z.FEVVALOR, Z.FEVQTD, Z.FEVPROG, Z.MARVALOR, Z.MARQTD, Z.MARPROG, Z.ABRVALOR, Z.ABRQTD, Z.ABRPROG,  ");
                    str.AppendLine("         Z.MAIVALOR, Z.MAIQTD, Z.MAIPROG, Z.JUNVALOR, Z.JUNQTD, Z.JUNPROG, Z.JULVALOR, Z.JULQTD, Z.JULPROG, ");
                    str.AppendLine("         Z.AGOVALOR, Z.AGOQTD, Z.AGOPROG, Z.SETVALOR, Z.SETQTD, Z.SETPROG, Z.OUTVALOR, Z.OUTQTD, Z.OUTPROG,  ");
                    str.AppendLine("         Z.NOVVALOR, Z.NOVQTD, Z.NOVPROG, Z.DEZVALOR, Z.DEZQTD, Z.DEZPROG, ");
                    str.AppendLine("         SUM(Z.JANVALOR+Z.FEVVALOR+Z.MARVALOR+Z.ABRVALOR+Z.MAIVALOR+Z.JUNVALOR+Z.JULVALOR+Z.AGOVALOR+Z.SETVALOR+Z.OUTVALOR+Z.NOVVALOR+Z.DEZVALOR) AS TOTALVALOR, ");
                    str.AppendLine("         SUM(Z.JANQTD+Z.FEVQTD+Z.MARQTD+Z.ABRQTD+Z.MAIQTD+Z.JUNQTD+Z.JULQTD+Z.AGOQTD+Z.SETQTD+Z.OUTQTD+Z.NOVQTD+Z.DEZQTD) AS TOTALQTD, ");
                    str.AppendLine("         SUM(Z.JANPROG+Z.FEVPROG+Z.MARPROG+Z.ABRPROG+Z.MAIPROG+Z.JUNPROG+Z.JULPROG+Z.AGOPROG+Z.SETPROG+Z.OUTPROG+Z.NOVPROG+Z.DEZPROG) AS TOTALPROG ");
                    str.AppendLine("  FROM ( ");
                    str.AppendLine("  SELECT Y.COD_MATERIAL, Y.NOM_MATERIAL, Y.UNIDADE, Y.COD_PLANO, Y.PLANO, Y.NUM_ANO, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.JANVALOR ELSE Y.JANVALOR-(Y.JANVALOR*(SUM(RG.PCT_RATEIO)/100)) END AS JANVALOR, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.JANQTD ELSE Y.JANQTD-(Y.JANQTD*(SUM(RG.PCT_RATEIO)/100)) END AS JANQTD, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.JANPROG ELSE Y.JANPROG-(Y.JANPROG*(SUM(RG.PCT_RATEIO)/100)) END AS JANPROG, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.FEVVALOR ELSE Y.FEVVALOR-(Y.FEVVALOR*(SUM(RG.PCT_RATEIO)/100)) END AS FEVVALOR, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.FEVQTD ELSE Y.FEVQTD-(Y.FEVQTD*(SUM(RG.PCT_RATEIO)/100)) END AS FEVQTD, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.FEVPROG ELSE Y.FEVPROG-(Y.FEVPROG*(SUM(RG.PCT_RATEIO)/100)) END AS FEVPROG, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.MARVALOR ELSE Y.MARVALOR-(Y.MARVALOR*(SUM(RG.PCT_RATEIO)/100)) END AS MARVALOR, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.MARQTD ELSE Y.MARQTD-(Y.MARQTD*(SUM(RG.PCT_RATEIO)/100)) END AS MARQTD, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.MARPROG ELSE Y.MARPROG-(Y.MARPROG*(SUM(RG.PCT_RATEIO)/100)) END AS MARPROG, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.ABRVALOR ELSE Y.ABRVALOR-(Y.ABRVALOR*(SUM(RG.PCT_RATEIO)/100)) END AS ABRVALOR, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.ABRQTD ELSE Y.ABRQTD-(Y.ABRQTD*(SUM(RG.PCT_RATEIO)/100)) END AS ABRQTD, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.ABRPROG ELSE Y.ABRPROG-(Y.ABRPROG*(SUM(RG.PCT_RATEIO)/100)) END AS ABRPROG, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.MAIVALOR ELSE Y.MAIVALOR-(Y.MAIVALOR*(SUM(RG.PCT_RATEIO)/100)) END AS MAIVALOR, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.MAIQTD ELSE Y.MAIQTD-(Y.MAIQTD*(SUM(RG.PCT_RATEIO)/100)) END AS MAIQTD, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.MAIPROG ELSE Y.MAIPROG-(Y.MAIPROG*(SUM(RG.PCT_RATEIO)/100)) END AS MAIPROG, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.JUNVALOR ELSE Y.JUNVALOR-(Y.JUNVALOR*(SUM(RG.PCT_RATEIO)/100)) END AS JUNVALOR, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.JUNQTD ELSE Y.JUNQTD-(Y.JUNQTD*(SUM(RG.PCT_RATEIO)/100)) END AS JUNQTD, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.JUNPROG ELSE Y.JUNPROG-(Y.JUNPROG*(SUM(RG.PCT_RATEIO)/100)) END AS JUNPROG, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.JULVALOR ELSE Y.JULVALOR-(Y.JULVALOR*(SUM(RG.PCT_RATEIO)/100)) END AS JULVALOR, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.JULQTD ELSE Y.JULQTD-(Y.JULQTD*(SUM(RG.PCT_RATEIO)/100)) END AS JULQTD, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.JULPROG ELSE Y.JULPROG-(Y.JULPROG*(SUM(RG.PCT_RATEIO)/100)) END AS JULPROG, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.AGOVALOR ELSE Y.AGOVALOR-(Y.AGOVALOR*(SUM(RG.PCT_RATEIO)/100)) END AS AGOVALOR, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.AGOQTD ELSE Y.AGOQTD-(Y.AGOQTD*(SUM(RG.PCT_RATEIO)/100)) END AS AGOQTD, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.AGOPROG ELSE Y.AGOPROG-(Y.AGOPROG*(SUM(RG.PCT_RATEIO)/100)) END AS AGOPROG, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.SETVALOR ELSE Y.SETVALOR-(Y.SETVALOR*(SUM(RG.PCT_RATEIO)/100)) END AS SETVALOR, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.SETQTD ELSE Y.SETQTD-(Y.SETQTD*(SUM(RG.PCT_RATEIO)/100)) END AS SETQTD, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.SETPROG ELSE Y.SETPROG-(Y.SETPROG*(SUM(RG.PCT_RATEIO)/100)) END AS SETPROG, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.OUTVALOR ELSE Y.OUTVALOR-(Y.OUTVALOR*(SUM(RG.PCT_RATEIO)/100)) END AS OUTVALOR, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.OUTQTD ELSE Y.OUTQTD-(Y.OUTQTD*(SUM(RG.PCT_RATEIO)/100)) END AS OUTQTD, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.OUTPROG ELSE Y.OUTPROG-(Y.OUTPROG*(SUM(RG.PCT_RATEIO)/100)) END AS OUTPROG, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.NOVVALOR ELSE Y.NOVVALOR-(Y.NOVVALOR*(SUM(RG.PCT_RATEIO)/100)) END AS NOVVALOR, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.NOVQTD ELSE Y.NOVQTD-(Y.NOVQTD*(SUM(RG.PCT_RATEIO)/100)) END AS NOVQTD, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.NOVPROG ELSE Y.NOVPROG-(Y.NOVPROG*(SUM(RG.PCT_RATEIO)/100)) END AS NOVPROG, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.DEZVALOR ELSE Y.DEZVALOR-(Y.DEZVALOR*(SUM(RG.PCT_RATEIO)/100)) END AS DEZVALOR, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.DEZQTD ELSE Y.DEZQTD-(Y.DEZQTD*(SUM(RG.PCT_RATEIO)/100)) END AS DEZQTD, ");
                    str.AppendLine("         CASE WHEN SUM(RG.PCT_RATEIO) IS NULL THEN Y.DEZPROG ELSE Y.DEZPROG-(Y.DEZPROG*(SUM(RG.PCT_RATEIO)/100)) END AS DEZPROG ");
                    str.AppendLine("  FROM ( ");
                    str.AppendLine("  SELECT X.COD_MATERIAL, X.NOM_MATERIAL, X.UNIDADE, ");
                    str.AppendLine("         CASE WHEN P.SEQ_ITEM_PLANO_CONTA IS NULL THEN X.COD_PLANO ELSE P.SEQ_ITEM_PLANO_CONTA END AS COD_PLANO, ");
                    str.AppendLine("         CASE WHEN P.SEQ_ITEM_PLANO_CONTA IS NULL THEN X.PLANO ELSE P.NOM_ITEM_PLANO_CONTA END AS PLANO, ");
                    str.AppendLine("         X.NUM_ANO, ");
                    str.AppendLine("         SUM(X.JANVALOR) AS JANVALOR, SUM(X.JANQTD) AS JANQTD, SUM(X.JANPROG) AS JANPROG, ");
                    str.AppendLine("         SUM(X.FEVVALOR) AS FEVVALOR, SUM(X.FEVQTD) AS FEVQTD, SUM(X.FEVPROG) AS FEVPROG, ");
                    str.AppendLine("         SUM(X.MARVALOR) AS MARVALOR, SUM(X.MARQTD) AS MARQTD, SUM(X.MARPROG) AS MARPROG, ");
                    str.AppendLine("         SUM(X.ABRVALOR) AS ABRVALOR, SUM(X.ABRQTD) AS ABRQTD, SUM(X.ABRPROG) AS ABRPROG, ");
                    str.AppendLine("         SUM(X.MAIVALOR) AS MAIVALOR, SUM(X.MAIQTD) AS MAIQTD, SUM(X.MAIPROG) AS MAIPROG, ");
                    str.AppendLine("         SUM(X.JUNVALOR) AS JUNVALOR, SUM(X.JUNQTD) AS JUNQTD, SUM(X.JUNPROG) AS JUNPROG, ");
                    str.AppendLine("         SUM(X.JULVALOR) AS JULVALOR, SUM(X.JULQTD) AS JULQTD, SUM(X.JULPROG) AS JULPROG, ");
                    str.AppendLine("         SUM(X.AGOVALOR) AS AGOVALOR, SUM(X.AGOQTD) AS AGOQTD, SUM(X.AGOPROG) AS AGOPROG, ");
                    str.AppendLine("         SUM(X.SETVALOR) AS SETVALOR, SUM(X.SETQTD) AS SETQTD, SUM(X.SETPROG) AS SETPROG, ");
                    str.AppendLine("         SUM(X.OUTVALOR) AS OUTVALOR, SUM(X.OUTQTD) AS OUTQTD, SUM(X.OUTPROG) AS OUTPROG, ");
                    str.AppendLine("         SUM(X.NOVVALOR) AS NOVVALOR, SUM(X.NOVQTD) AS NOVQTD, SUM(X.NOVPROG) AS NOVPROG, ");
                    str.AppendLine("         SUM(X.DEZVALOR) AS DEZVALOR, SUM(X.DEZQTD) AS DEZQTD, SUM(X.DEZPROG) AS DEZPROG ");
                    str.AppendLine("  FROM ( ");
                    str.AppendLine("  SELECT T.COD_MATERIAL, T.NOM_MATERIAL, T.UNIDADE, ");
                    str.AppendLine("         CASE WHEN P.SEQ_ITEM_PLANO_CONTA IS NULL THEN T.COD_PLANO ELSE P.SEQ_ITEM_PLANO_CONTA END AS COD_PLANO, ");
                    str.AppendLine("         CASE WHEN P.SEQ_ITEM_PLANO_CONTA IS NULL THEN T.PLANO ELSE P.NOM_ITEM_PLANO_CONTA END AS PLANO, ");
                    str.AppendLine("         T.NUM_ANO, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 1 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS JANVALOR, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 1 THEN SUM(T.QTD_CONSUMIDA) ELSE 0 END AS JANQTD, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 1 THEN SUM(T.QTD_PROGRAMADA_ANO) ELSE 0 END AS JANPROG, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 2 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS FEVVALOR, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 2 THEN SUM(T.QTD_CONSUMIDA) ELSE 0 END AS FEVQTD, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 2 THEN SUM(T.QTD_PROGRAMADA_ANO) ELSE 0 END AS FEVPROG,  ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 3 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS MARVALOR, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 3 THEN SUM(T.QTD_CONSUMIDA) ELSE 0 END AS MARQTD, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 3 THEN SUM(T.QTD_PROGRAMADA_ANO) ELSE 0 END AS MARPROG, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 4 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS ABRVALOR, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 4 THEN SUM(T.QTD_CONSUMIDA) ELSE 0 END AS ABRQTD, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 4 THEN SUM(T.QTD_PROGRAMADA_ANO) ELSE 0 END AS ABRPROG, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 5 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS MAIVALOR, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 5 THEN SUM(T.QTD_CONSUMIDA) ELSE 0 END AS MAIQTD, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 5 THEN SUM(T.QTD_PROGRAMADA_ANO) ELSE 0 END AS MAIPROG, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 6 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS JUNVALOR, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 6 THEN SUM(T.QTD_CONSUMIDA) ELSE 0 END AS JUNQTD, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 6 THEN SUM(T.QTD_PROGRAMADA_ANO) ELSE 0 END AS JUNPROG, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 7 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS JULVALOR, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 7 THEN SUM(T.QTD_CONSUMIDA) ELSE 0 END AS JULQTD, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 7 THEN SUM(T.QTD_PROGRAMADA_ANO) ELSE 0 END AS JULPROG, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 8 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS AGOVALOR, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 8 THEN SUM(T.QTD_CONSUMIDA) ELSE 0 END AS AGOQTD, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 8 THEN SUM(T.QTD_PROGRAMADA_ANO) ELSE 0 END AS AGOPROG, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 9 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS SETVALOR, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 9 THEN SUM(T.QTD_CONSUMIDA) ELSE 0 END AS SETQTD, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 9 THEN SUM(T.QTD_PROGRAMADA_ANO) ELSE 0 END AS SETPROG, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 10 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS OUTVALOR, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 10 THEN SUM(T.QTD_CONSUMIDA) ELSE 0 END AS OUTQTD, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 10 THEN SUM(T.QTD_PROGRAMADA_ANO) ELSE 0 END AS OUTPROG, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 11 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS NOVVALOR, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 11 THEN SUM(T.QTD_CONSUMIDA) ELSE 0 END AS NOVQTD, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 11 THEN SUM(T.QTD_PROGRAMADA_ANO) ELSE 0 END AS NOVPROG, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 12 THEN SUM(T.VLR_TOTAL_CONSUMO) ELSE 0 END AS DEZVALOR, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 12 THEN SUM(T.QTD_CONSUMIDA) ELSE 0 END AS DEZQTD, ");
                    str.AppendLine("         CASE WHEN T.NUM_MES = 12 THEN SUM(T.QTD_PROGRAMADA_ANO) ELSE 0 END AS DEZPROG ");
                    str.AppendLine(" FROM ( ");
                    str.AppendLine(" SELECT CMM.COD_MATERIAL, MAX(M.NOM_MATERIAL) NOM_MATERIAL, MAX(U.NOM_UNIDADE) UNIDADE, P.SEQ_ITEM_PLANO_CONTA COD_PLANO, P.NOM_ITEM_PLANO_CONTA PLANO, CMM.NUM_ANO, CMM.NUM_MES,  ");
                    str.AppendLine("     NVL(SUM(CMM.VLR_TOTAL_CONSUMO),0) VLR_TOTAL_CONSUMO, NVL(SUM(CMM.QTD_CONSUMIDA),0) QTD_CONSUMIDA, NVL(SUM(CMM.QTD_PROGRAMADA_ANO),0) QTD_PROGRAMADA_ANO  ");
                    str.AppendLine(" FROM CONSUMO_MENSAL_MATERIAL CMM, CENTRO_CUSTO CC, GRUPO_CENCUSTO_SICH GCC, MATERIAL M, GRUPO G, ALINEA A , UNIDADE U, GRUPO_PLANOCONTAITEM GP, PLANO_CONTA_ITEM P  ");
                    str.AppendLine(string.Format(" WHERE CMM.NUM_ANO = {0} ", ano));
                    str.AppendLine("    AND CMM.VLR_TOTAL_CONSUMO >= 0");
                    if (classe > -1)
                        str.AppendLine(string.Format("     AND A.IDF_CLASSE = {0} ", classe));
                    str.AppendLine(string.Format("     AND GCC.COD_INST_SISTEMA    = {0} ", idInstSistema));
                    str.AppendLine("     AND CMM.COD_CENCUSTO        = CC.COD_CENCUSTO  ");
                    str.AppendLine("     AND CC.COD_GRUPO_SICH       = GCC.COD_GRUPO_SICH  ");
                    str.AppendLine("     AND CMM.COD_MATERIAL        = M.COD_MATERIAL  ");
                    str.AppendLine("     AND M.COD_GRUPO             = G.COD_GRUPO  ");
                    str.AppendLine("     AND G.COD_ALINEA            = A.COD_ALINEA  ");
                    str.AppendLine("     AND G.COD_GRUPO             = GP.COD_GRUPO(+)  ");
                    str.AppendLine("     AND GP.SEQ_ITEM_PLANO_CONTA = P.SEQ_ITEM_PLANO_CONTA(+)  ");
                    if (!string.IsNullOrEmpty(codCentroCusto))
                        str.AppendLine(string.Format("     AND CMM.COD_CENCUSTO = '{0}' ", codCentroCusto));
                    str.AppendLine("     AND M.COD_UNIDADE = U.COD_UNIDADE  ");
                    if (!string.IsNullOrEmpty(planoConta))
                        str.AppendLine(string.Format("     AND GP.SEQ_PLANO_CONTA(+) = {0} ", planoConta));
                    if (!string.IsNullOrEmpty(codMaterial))
                        str.AppendLine(string.Format("     AND M.COD_MATERIAL = '{0}' ", codMaterial));
                    str.AppendLine(" GROUP BY CMM.COD_MATERIAL, P.SEQ_ITEM_PLANO_CONTA, P.NOM_ITEM_PLANO_CONTA, CMM.NUM_ANO, CMM.NUM_MES  ");
                    str.AppendLine(" ORDER BY NOM_MATERIAL,CMM.NUM_MES ");
                    str.AppendLine(" ) T, MATERIAL M, GRUPO G, RATEIO_GRUPO_PCONTAITEM_CCUSTO R, PLANO_CONTA_ITEM P ");
                    str.AppendLine(" WHERE T.COD_MATERIAL = M.COD_MATERIAL ");
                    str.AppendLine("       AND M.COD_GRUPO = G.COD_GRUPO ");
                    str.AppendLine("       AND G.COD_GRUPO = R.COD_GRUPO(+) ");
                    str.AppendLine("       AND R.SEQ_ITEM_PLANO_CONTA = P.SEQ_ITEM_PLANO_CONTA(+) ");
                    if (!string.IsNullOrEmpty(codCentroCusto))
                        str.AppendLine(string.Format("       AND R.COD_CENCUSTO(+) = '{0}' ", codCentroCusto));
                    if (!string.IsNullOrEmpty(planoConta))
                        str.AppendLine(string.Format("       AND R.SEQ_PLANO_CONTA(+) = {0} ", planoConta));
                    str.AppendLine(" GROUP BY T.COD_MATERIAL, T.NOM_MATERIAL, T.UNIDADE, T.COD_PLANO, T.PLANO, P.SEQ_ITEM_PLANO_CONTA, P.NOM_ITEM_PLANO_CONTA, T.NUM_ANO, T.NUM_MES ");
                    str.AppendLine(" ) X, MATERIAL M, RATEIO_MAT_PCONTAITEM_CENCUSTO RM, PLANO_CONTA_ITEM P ");
                    str.AppendLine(" WHERE X.COD_MATERIAL = M.COD_MATERIAL ");
                    str.AppendLine("       AND M.COD_MATERIAL = RM.COD_MATERIAL(+) ");
                    str.AppendLine("       AND RM.SEQ_ITEM_PLANO_CONTA = P.SEQ_ITEM_PLANO_CONTA(+) ");
                    if (!string.IsNullOrEmpty(codCentroCusto))
                        str.AppendLine(string.Format("       AND RM.COD_CENCUSTO(+) = '{0}' ", codCentroCusto));
                    if (!string.IsNullOrEmpty(planoConta))
                        str.AppendLine(string.Format("       AND RM.SEQ_PLANO_CONTA(+) = {0} ", planoConta));
                    str.AppendLine(" GROUP BY X.COD_MATERIAL, X.NOM_MATERIAL, X.UNIDADE, X.COD_PLANO, X.PLANO, P.SEQ_ITEM_PLANO_CONTA, P.NOM_ITEM_PLANO_CONTA, X.NUM_ANO ");
                    str.AppendLine(" ) Y, RATEIO_MAT_GP_CENCUSTO RG ");
                    str.AppendLine(" WHERE Y.COD_MATERIAL = RG.COD_MATERIAL(+) ");
                    if (!string.IsNullOrEmpty(codCentroCusto))
                        str.AppendLine(string.Format("       AND RG.COD_CENCUSTO(+) = '{0}' ", codCentroCusto));
                    str.AppendLine(" GROUP BY Y.COD_MATERIAL, Y.NOM_MATERIAL, Y.UNIDADE, Y.COD_PLANO, Y.PLANO, Y.NUM_ANO, Y.JANVALOR, Y.JANQTD, Y.JANPROG,  ");
                    str.AppendLine("          Y.FEVVALOR, Y.FEVQTD, Y.FEVPROG, Y.MARVALOR, Y.MARQTD, Y.MARPROG, Y.ABRVALOR, Y.ABRQTD, Y.ABRPROG,  ");
                    str.AppendLine("          Y.MAIVALOR, Y.MAIQTD, Y.MAIPROG, Y.JUNVALOR, Y.JUNQTD, Y.JUNPROG, Y.JULVALOR, Y.JULQTD, Y.JULPROG, ");
                    str.AppendLine("          Y.AGOVALOR, Y.AGOQTD, Y.AGOPROG, Y.SETVALOR, Y.SETQTD, Y.SETPROG, Y.OUTVALOR, Y.OUTQTD, Y.OUTPROG,  ");
                    str.AppendLine("          Y.NOVVALOR, Y.NOVQTD, Y.NOVPROG, Y.DEZVALOR, Y.DEZQTD, Y.DEZPROG ");
                    str.AppendLine(" ) Z ");
                    if (itemPlanoConta != "0")
                        str.AppendLine(string.Format(" WHERE Z.COD_PLANO = {0} ", itemPlanoConta));
                    str.AppendLine(" GROUP BY Z.COD_MATERIAL, Z.NOM_MATERIAL, Z.UNIDADE, Z.COD_PLANO, Z.PLANO, Z.NUM_ANO, Z.JANVALOR, Z.JANQTD, Z.JANPROG,  ");
                    str.AppendLine("                 Z.FEVVALOR, Z.FEVQTD, Z.FEVPROG, Z.MARVALOR, Z.MARQTD, Z.MARPROG, Z.ABRVALOR, Z.ABRQTD, Z.ABRPROG,  ");
                    str.AppendLine("                 Z.MAIVALOR, Z.MAIQTD, Z.MAIPROG, Z.JUNVALOR, Z.JUNQTD, Z.JUNPROG, Z.JULVALOR, Z.JULQTD, Z.JULPROG, ");
                    str.AppendLine("                 Z.AGOVALOR, Z.AGOQTD, Z.AGOPROG, Z.SETVALOR, Z.SETQTD, Z.SETPROG, Z.OUTVALOR, Z.OUTQTD, Z.OUTPROG,  ");
                    str.AppendLine("                 Z.NOVVALOR, Z.NOVQTD, Z.NOVPROG, Z.DEZVALOR, Z.DEZQTD, Z.DEZPROG ");
                    if (string.IsNullOrEmpty(sortDirection))
                        str.AppendLine(" ORDER BY Z.NOM_MATERIAL ");
                    else
                    {
                        if (sortExpression == "Material")
                            str.AppendLine(string.Format(" ORDER BY Z.NOM_MATERIAL {0} ", sortDirection));
                        else if (sortExpression == "Codigo")
                            str.AppendLine(string.Format(" ORDER BY Z.COD_MATERIAL {0} ", sortDirection));
                        else
                            str.AppendLine(string.Format(" ORDER BY {0} {1}", sortExpression.ToUpper(), sortDirection));
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
                        _cm = new Hcrp.Framework.Classes.ConsumoMaterialCCMaterial();

                        if (dr["COD_MATERIAL"] != DBNull.Value)
                            _cm.Codigo = dr["COD_MATERIAL"].ToString();
                        if (dr["NOM_MATERIAL"] != DBNull.Value)
                            _cm.Material = dr["NOM_MATERIAL"].ToString();
                        if (dr["UNIDADE"] != DBNull.Value)
                            _cm.Unidade = dr["UNIDADE"].ToString();
                        if (dr["PLANO"] != DBNull.Value)
                            _cm.Plano = dr["PLANO"].ToString();
                        if (dr["JANVALOR"] != DBNull.Value)
                            _cm.JanValor = Convert.ToDouble(dr["JANVALOR"]);
                        if (dr["FEVVALOR"] != DBNull.Value)
                            _cm.FevValor = Convert.ToDouble(dr["FEVVALOR"]);
                        if (dr["MARVALOR"] != DBNull.Value)
                            _cm.MarValor = Convert.ToDouble(dr["MARVALOR"]);
                        if (dr["ABRVALOR"] != DBNull.Value)
                            _cm.AbrValor = Convert.ToDouble(dr["ABRVALOR"]);
                        if (dr["MAIVALOR"] != DBNull.Value)
                            _cm.MaiValor = Convert.ToDouble(dr["MAIVALOR"]);
                        if (dr["JUNVALOR"] != DBNull.Value)
                            _cm.JunValor = Convert.ToDouble(dr["JUNVALOR"]);
                        if (dr["JULVALOR"] != DBNull.Value)
                            _cm.JulValor = Convert.ToDouble(dr["JULVALOR"]);
                        if (dr["AGOVALOR"] != DBNull.Value)
                            _cm.AgoValor = Convert.ToDouble(dr["AGOVALOR"]);
                        if (dr["SETVALOR"] != DBNull.Value)
                            _cm.SetValor = Convert.ToDouble(dr["SETVALOR"]);
                        if (dr["OUTVALOR"] != DBNull.Value)
                            _cm.OutValor = Convert.ToDouble(dr["OUTVALOR"]);
                        if (dr["NOVVALOR"] != DBNull.Value)
                            _cm.NovValor = Convert.ToDouble(dr["NOVVALOR"]);
                        if (dr["DEZVALOR"] != DBNull.Value)
                            _cm.DezValor = Convert.ToDouble(dr["DEZVALOR"]);
                        if (dr["TOTALVALOR"] != DBNull.Value)
                            _cm.TotalValor = Convert.ToDouble(dr["TOTALVALOR"]);
                        if (dr["JANQTD"] != DBNull.Value)
                            _cm.JanQtd = Convert.ToDouble(dr["JANQTD"]);
                        if (dr["FEVQTD"] != DBNull.Value)
                            _cm.FevQtd = Convert.ToDouble(dr["FEVQTD"]);
                        if (dr["MARQTD"] != DBNull.Value)
                            _cm.MarQtd = Convert.ToDouble(dr["MARQTD"]);
                        if (dr["ABRQTD"] != DBNull.Value)
                            _cm.AbrQtd = Convert.ToDouble(dr["ABRQTD"]);
                        if (dr["MAIQTD"] != DBNull.Value)
                            _cm.MaiQtd = Convert.ToDouble(dr["MAIQTD"]);
                        if (dr["JUNQTD"] != DBNull.Value)
                            _cm.JunQtd = Convert.ToDouble(dr["JUNQTD"]);
                        if (dr["JULQTD"] != DBNull.Value)
                            _cm.JulQtd = Convert.ToDouble(dr["JULQTD"]);
                        if (dr["AGOQTD"] != DBNull.Value)
                            _cm.AgoQtd = Convert.ToDouble(dr["AGOQTD"]);
                        if (dr["SETQTD"] != DBNull.Value)
                            _cm.SetQtd = Convert.ToDouble(dr["SETQTD"]);
                        if (dr["OUTQTD"] != DBNull.Value)
                            _cm.OutQtd = Convert.ToDouble(dr["OUTQTD"]);
                        if (dr["NOVQTD"] != DBNull.Value)
                            _cm.NovQtd = Convert.ToDouble(dr["NOVQTD"]);
                        if (dr["DEZQTD"] != DBNull.Value)
                            _cm.DezQtd = Convert.ToDouble(dr["DEZQTD"]);
                        if (dr["TOTALQTD"] != DBNull.Value)
                            _cm.TotalQtd = Convert.ToDouble(dr["TOTALQTD"]);
                        if (dr["JANPROG"] != DBNull.Value)
                            _cm.JanProg = Convert.ToDouble(dr["JANPROG"]);
                        if (dr["FEVPROG"] != DBNull.Value)
                            _cm.FevProg = Convert.ToDouble(dr["FEVPROG"]);
                        if (dr["MARPROG"] != DBNull.Value)
                            _cm.MarProg = Convert.ToDouble(dr["MARPROG"]);
                        if (dr["ABRPROG"] != DBNull.Value)
                            _cm.AbrProg = Convert.ToDouble(dr["ABRPROG"]);
                        if (dr["MAIPROG"] != DBNull.Value)
                            _cm.MaiProg = Convert.ToDouble(dr["MAIPROG"]);
                        if (dr["JUNPROG"] != DBNull.Value)
                            _cm.JunProg = Convert.ToDouble(dr["JUNPROG"]);
                        if (dr["JULPROG"] != DBNull.Value)
                            _cm.JulProg = Convert.ToDouble(dr["JULPROG"]);
                        if (dr["AGOPROG"] != DBNull.Value)
                            _cm.AgoProg = Convert.ToDouble(dr["AGOPROG"]);
                        if (dr["SETPROG"] != DBNull.Value)
                            _cm.SetProg = Convert.ToDouble(dr["SETPROG"]);
                        if (dr["OUTPROG"] != DBNull.Value)
                            _cm.OutProg = Convert.ToDouble(dr["OUTPROG"]);
                        if (dr["NOVPROG"] != DBNull.Value)
                            _cm.NovProg = Convert.ToDouble(dr["NOVPROG"]);
                        if (dr["DEZPROG"] != DBNull.Value)
                            _cm.DezProg = Convert.ToDouble(dr["DEZPROG"]);
                        if (dr["TOTALPROG"] != DBNull.Value)
                            _cm.TotalProg = Convert.ToDouble(dr["TOTALPROG"]);

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
