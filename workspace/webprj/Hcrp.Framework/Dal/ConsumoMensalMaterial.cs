using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class ConsumoMensalMaterial : Hcrp.Framework.Classes.ConsumoMensalMaterial
    {
        public List<Hcrp.Framework.Classes.ConsumoMensalMaterial> BuscarConsumoMaterialPorAlinea(int classe, string ano, int ordem, int tipo, string codcencusto)
        {
            List<Hcrp.Framework.Classes.ConsumoMensalMaterial> _listaDeRetorno = new List<Hcrp.Framework.Classes.ConsumoMensalMaterial>();
            Hcrp.Framework.Classes.ConsumoMensalMaterial _cm = null;
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    int idInstSistema = new Hcrp.Framework.Classes.ConfiguracaoSistema().CodInstituicaoSistema;
                    // Cria a query
                    StringBuilder str = new StringBuilder();
                    if (tipo == 0) /*Grupo de Centro de Custo*/
                        str.AppendLine(" SELECT GCC.COD_GRUPO, MAX(GCC.DSC_GRUPO) DSC_GRUPO, CMM.NUM_ANO, CMM.NUM_MES, NVL(SUM(CMM.VLR_TOTAL_CONSUMO),0) VLR_TOTAL_CONSUMO ");
                    else if (tipo == 1) /*Centro de Custo*/
                        str.AppendLine(" SELECT CMM.COD_CENCUSTO, MAX(CC.NOM_CENCUSTO) NOM_CENCUSTO, GCC.COD_GRUPO, CMM.NUM_ANO, CMM.NUM_MES, NVL(SUM(CMM.VLR_TOTAL_CONSUMO),0) VLR_TOTAL_CONSUMO ");
                    else if (tipo == 2) /*Materiais*/
                    {
                        str.AppendLine(" SELECT CMM.COD_MATERIAL, MAX(M.NOM_MATERIAL) NOM_MATERIAL, MAX(U.COD_UNIDADE) UNIDADE, '0' PLANO,  CASE WHEN MNA.SEQ_MAT_NAO_APROP_CC IS NULL THEN 'SIM' ELSE 'NAO' END APROPRIAR, CMM.NUM_ANO, CMM.NUM_MES, ");
                        str.AppendLine("        NVL(SUM(CMM.VLR_TOTAL_CONSUMO),0) VLR_TOTAL_CONSUMO, NVL(SUM(CMM.QTD_CONSUMIDA),0) QTD_CONSUMIDA, NVL(SUM(CMM.QTD_PROGRAMADA_ANO),0) QTD_PROGRAMADA_ANO");
                    }

                    str.AppendLine(" FROM CONSUMO_MENSAL_MATERIAL CMM, CENTRO_CUSTO CC, GRUPO_CENCUSTO GCC, ");
                    str.AppendLine("      MATERIAL M, SUB_GRUPO SG, GRUPO G, ALINEA A ");
                    if (tipo == 2)
                        str.AppendLine("      ,MATERIAL_NAO_APROPRIACAO_CC MNA, UNIDADE U ");
                    str.AppendLine(" WHERE CMM.NUM_ANO = :NUM_ANO ");
                    str.AppendLine("   AND A.IDF_CLASSE = :IDF_CLASSE ");
                    str.AppendLine("   AND GCC.COD_INST_SISTEMA = :COD_INST_SISTEMA ");
                    str.AppendLine("   AND CMM.COD_CENCUSTO     = CC.COD_CENCUSTO ");
                    str.AppendLine("   AND CC.COD_GRUPO         = GCC.COD_GRUPO ");
                    str.AppendLine("   AND CMM.COD_MATERIAL     = M.COD_MATERIAL ");
                    str.AppendLine("   AND M.COD_GRUPO          = SG.COD_GRUPO ");
                    str.AppendLine("   AND M.COD_SUB_GRUPO      = SG.COD_SUB_GRUPO ");
                    str.AppendLine("   AND SG.COD_GRUPO         = G.COD_GRUPO ");
                    str.AppendLine("   AND G.COD_ALINEA         = A.COD_ALINEA ");
                    str.AppendLine("   AND CMM.COD_MATERIAL     IN ('26041236','15044373','1503138X')");
                    if (tipo == 0)
                        str.AppendLine(" GROUP BY GCC.COD_GRUPO, CMM.NUM_ANO, CMM.NUM_MES ");
                    else if (tipo == 1)
                        str.AppendLine(" GROUP BY CMM.COD_CENCUSTO, GCC.COD_GRUPO, CMM.NUM_ANO, CMM.NUM_MES ");
                    else if (tipo == 2)
                    {
                        str.AppendLine(" AND CMM.COD_MATERIAL = MNA.COD_MATERIAL(+) ");
                        str.AppendLine(" AND CMM.COD_CENCUSTO = MNA.COD_CENCUSTO(+) ");
                        str.AppendLine(" AND MNA.NUM_USER_EXCLUSAO(+) IS NULL ");
                        str.AppendLine(" AND CMM.COD_CENCUSTO = :COD_CENCUSTO");
                        str.AppendLine(" AND M.COD_UNIDADE = U.COD_UNIDADE");
                        str.AppendLine(" GROUP BY CMM.COD_MATERIAL, CMM.NUM_ANO, CMM.NUM_MES, MNA.SEQ_MAT_NAO_APROP_CC ");
                    }
                    if (ordem == 0)
                    {
                        str.AppendLine(" ORDER BY CMM.NUM_MES ");
                    }
                    else if (ordem == 1)
                    {
                        if (tipo == 0)
                            str.AppendLine(" ORDER BY DSC_GRUPO,CMM.NUM_MES ");
                        else if (tipo == 1)
                            str.AppendLine(" ORDER BY NOM_CENCUSTO,CMM.NUM_MES ");
                        else if (tipo == 2)
                            str.AppendLine(" ORDER BY NOM_MATERIAL,CMM.NUM_MES ");
                    }

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["NUM_ANO"] = ano;
                    query.Params["IDF_CLASSE"] = classe;
                    query.Params["COD_INST_SISTEMA"] = idInstSistema;
                    if (tipo == 2)
                        query.Params["COD_CENCUSTO"] = codcencusto;
                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _cm = new Hcrp.Framework.Classes.ConsumoMensalMaterial();
                        if (tipo == 0)
                        {
                            if (dr["COD_GRUPO"] != DBNull.Value)
                                _cm._CodGrupoCentroCusto = Convert.ToInt32(dr["COD_GRUPO"]);
                        }
                        else if (tipo == 1)
                        {
                            if (dr["COD_CENCUSTO"] != DBNull.Value)
                                _cm._CodCenCusto = dr["COD_CENCUSTO"].ToString();

                            if (dr["COD_GRUPO"] != DBNull.Value)
                                _cm._CodGrupoCentroCusto = Convert.ToInt32(dr["COD_GRUPO"]);
                        }
                        else if (tipo == 2)
                        {
                            if (dr["COD_MATERIAL"] != DBNull.Value)
                                _cm._CodMaterial = dr["COD_MATERIAL"].ToString();
                            if (dr["UNIDADE"] != DBNull.Value)
                                _cm._CodUnidade = Convert.ToInt32(dr["UNIDADE"]);
                            if (dr["PLANO"] != DBNull.Value)
                                _cm.PlanoConta = dr["PLANO"].ToString();
                            if (dr["APROPRIAR"] != DBNull.Value)
                                _cm.Apropriar = dr["APROPRIAR"].ToString();
                            if (dr["QTD_CONSUMIDA"] != DBNull.Value)
                                _cm.QtdConsumida = Convert.ToDouble(dr["QTD_CONSUMIDA"]);
                            if (dr["QTD_PROGRAMADA_ANO"] != DBNull.Value)
                                _cm.QtdProgramadaAno = Convert.ToDouble(dr["QTD_PROGRAMADA_ANO"]);
                        }
                        if (dr["NUM_ANO"] != DBNull.Value)
                            _cm.Ano = dr["NUM_ANO"].ToString();
                        if (dr["NUM_MES"] != DBNull.Value)
                            _cm.Mes = Convert.ToInt32(dr["NUM_MES"]);
                        if (dr["VLR_TOTAL_CONSUMO"] != DBNull.Value)
                            _cm.VlrTotalConsumo = Convert.ToDouble(dr["VLR_TOTAL_CONSUMO"]);
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

        public List<Hcrp.Framework.Classes.ConsumoMensalMaterial> BuscarConsumoMaterialPorAlineaGrupo(int classe, string ano, int ordem)
        {
            List<Hcrp.Framework.Classes.ConsumoMensalMaterial> _listaDeRetorno = new List<Hcrp.Framework.Classes.ConsumoMensalMaterial>();
            Hcrp.Framework.Classes.ConsumoMensalMaterial _cm = null;
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    int idInstSistema = new Hcrp.Framework.Classes.ConfiguracaoSistema().CodInstituicaoSistema;
                    // Cria a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT GCC.COD_GRUPO_SICH, MAX(GCC.DSC_GRUPO) DSC_GRUPO, CMM.NUM_ANO, CMM.NUM_MES, NVL(SUM(CMM.VLR_TOTAL_CONSUMO),0) VLR_TOTAL_CONSUMO ");
                    str.AppendLine(" FROM CONSUMO_MENSAL_MATERIAL CMM, CENTRO_CUSTO CC, GRUPO_CENCUSTO_SICH GCC, ");
                    str.AppendLine("      MATERIAL M, SUB_GRUPO SG, GRUPO G, ALINEA A ");
                    str.AppendLine(" WHERE CMM.NUM_ANO = :NUM_ANO ");
                    str.AppendLine("   AND A.IDF_CLASSE = :IDF_CLASSE ");
                    str.AppendLine("   AND GCC.COD_INST_SISTEMA = :COD_INST_SISTEMA ");
                    str.AppendLine("   AND CMM.COD_CENCUSTO     = CC.COD_CENCUSTO ");
                    str.AppendLine("   AND CC.COD_GRUPO_SICH    = GCC.COD_GRUPO_SICH ");
                    str.AppendLine("   AND CMM.COD_MATERIAL     = M.COD_MATERIAL ");
                    str.AppendLine("   AND M.COD_GRUPO          = SG.COD_GRUPO ");
                    str.AppendLine("   AND M.COD_SUB_GRUPO      = SG.COD_SUB_GRUPO ");
                    str.AppendLine("   AND SG.COD_GRUPO         = G.COD_GRUPO ");
                    str.AppendLine("   AND G.COD_ALINEA         = A.COD_ALINEA ");
                    str.AppendLine("   AND CMM.COD_MATERIAL     IN ('26041236','15044373','1503138X')");
                    str.AppendLine(" GROUP BY GCC.COD_GRUPO_SICH, CMM.NUM_ANO, CMM.NUM_MES ");
                    if (ordem == 0)
                        str.AppendLine(" ORDER BY CMM.NUM_MES ");
                    else if (ordem == 1)
                        str.AppendLine(" ORDER BY DSC_GRUPO,CMM.NUM_MES ");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["NUM_ANO"] = ano;
                    query.Params["IDF_CLASSE"] = classe;
                    query.Params["COD_INST_SISTEMA"] = idInstSistema;

                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _cm = new Hcrp.Framework.Classes.ConsumoMensalMaterial();

                        if (dr["COD_GRUPO_SICH"] != DBNull.Value)
                            _cm._CodGrupoCentroCusto = Convert.ToInt32(dr["COD_GRUPO_SICH"]);
                        if (dr["NUM_ANO"] != DBNull.Value)
                            _cm.Ano = dr["NUM_ANO"].ToString();
                        if (dr["NUM_MES"] != DBNull.Value)
                            _cm.Mes = Convert.ToInt32(dr["NUM_MES"]);
                        if (dr["VLR_TOTAL_CONSUMO"] != DBNull.Value)
                            _cm.VlrTotalConsumo = Convert.ToDouble(dr["VLR_TOTAL_CONSUMO"]);
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

        public List<Hcrp.Framework.Classes.ConsumoMensalMaterial> BuscarConsumoMaterialPorItemPlanoContaGrupo(string itemPlanoConta, string ano, int ordem)
        {
            List<Hcrp.Framework.Classes.ConsumoMensalMaterial> _listaDeRetorno = new List<Hcrp.Framework.Classes.ConsumoMensalMaterial>();
            Hcrp.Framework.Classes.ConsumoMensalMaterial _cm = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    int idInstSistema = new Hcrp.Framework.Classes.ConfiguracaoSistema().CodInstituicaoSistema;
                    // Cria a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT GCC.COD_GRUPO_SICH, MAX(GCC.DSC_GRUPO) DSC_GRUPO, CMM.NUM_ANO, CMM.NUM_MES, NVL(SUM(CMM.VLR_TOTAL_CONSUMO),0) VLR_TOTAL_CONSUMO ");
                    str.AppendLine(" FROM   CONSUMO_MENSAL_MATERIAL CMM, CENTRO_CUSTO CC, GRUPO_CENCUSTO_SICH GCC, MATERIAL M, GRUPO G, GRUPO_PLANOCONTAITEM GP ");
                    str.AppendLine(" WHERE ");
                    str.AppendLine(string.Format("         CMM.NUM_ANO             = {0} ", ano));
                    str.AppendLine(string.Format("     AND GCC.COD_INST_SISTEMA    = {0} ", idInstSistema));
                    str.AppendLine("     AND CMM.COD_CENCUSTO        = CC.COD_CENCUSTO ");
                    str.AppendLine("     AND CC.COD_GRUPO_SICH       = GCC.COD_GRUPO_SICH ");
                    str.AppendLine("     AND CMM.COD_MATERIAL        = M.COD_MATERIAL ");
                    str.AppendLine("     AND M.COD_GRUPO             = G.COD_GRUPO ");
                    str.AppendLine("     AND G.COD_GRUPO             = GP.COD_GRUPO ");
                    if (itemPlanoConta != "0")
                        str.AppendLine(string.Format("   AND GP.SEQ_ITEM_PLANO_CONTA = {0} ", itemPlanoConta));
                    str.AppendLine(" GROUP BY GCC.COD_GRUPO_SICH, CMM.NUM_ANO, CMM.NUM_MES ");
                    if (ordem == 0)
                        str.AppendLine(" ORDER BY CMM.NUM_MES ");
                    else if (ordem == 1)
                        str.AppendLine(" ORDER BY DSC_GRUPO,CMM.NUM_MES ");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _cm = new Hcrp.Framework.Classes.ConsumoMensalMaterial();

                        if (dr["COD_GRUPO_SICH"] != DBNull.Value)
                            _cm._CodGrupoCentroCusto = Convert.ToInt32(dr["COD_GRUPO_SICH"]);
                        if (dr["NUM_ANO"] != DBNull.Value)
                            _cm.Ano = dr["NUM_ANO"].ToString();
                        if (dr["NUM_MES"] != DBNull.Value)
                            _cm.Mes = Convert.ToInt32(dr["NUM_MES"]);
                        if (dr["VLR_TOTAL_CONSUMO"] != DBNull.Value)
                            _cm.VlrTotalConsumo = Convert.ToDouble(dr["VLR_TOTAL_CONSUMO"]);

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

        public List<Hcrp.Framework.Classes.ConsumoMensalMaterial> BuscarConsumoMaterialPorAlineaCC(int classe, string ano, int ordem, string codgrupo)
        {
            List<Hcrp.Framework.Classes.ConsumoMensalMaterial> _listaDeRetorno = new List<Hcrp.Framework.Classes.ConsumoMensalMaterial>();
            Hcrp.Framework.Classes.ConsumoMensalMaterial _cm = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    int idInstSistema = new Hcrp.Framework.Classes.ConfiguracaoSistema().CodInstituicaoSistema;
                    // Cria a query
                    StringBuilder str = new StringBuilder();
                        
                    str.AppendLine(" SELECT CMM.COD_CENCUSTO, MAX(CC.NOM_CENCUSTO) NOM_CENCUSTO, GCC.COD_GRUPO_SICH, CMM.NUM_ANO, CMM.NUM_MES, NVL(SUM(CMM.VLR_TOTAL_CONSUMO),0) VLR_TOTAL_CONSUMO ");
                    str.AppendLine(" FROM CONSUMO_MENSAL_MATERIAL CMM, CENTRO_CUSTO CC, GRUPO_CENCUSTO_SICH GCC, ");
                    str.AppendLine("      MATERIAL M, SUB_GRUPO SG, GRUPO G, ALINEA A ");
                    str.AppendLine(" WHERE CMM.NUM_ANO = :NUM_ANO ");
                    str.AppendLine("   AND A.IDF_CLASSE = :IDF_CLASSE ");
                    str.AppendLine("   AND GCC.COD_INST_SISTEMA = :COD_INST_SISTEMA ");
                    str.AppendLine("   AND CMM.COD_CENCUSTO     = CC.COD_CENCUSTO ");
                    str.AppendLine("   AND CC.COD_GRUPO_SICH    = GCC.COD_GRUPO_SICH ");
                    str.AppendLine("   AND CMM.COD_MATERIAL     = M.COD_MATERIAL ");
                    str.AppendLine("   AND M.COD_GRUPO          = SG.COD_GRUPO ");
                    str.AppendLine("   AND M.COD_SUB_GRUPO      = SG.COD_SUB_GRUPO ");
                    str.AppendLine("   AND SG.COD_GRUPO         = G.COD_GRUPO ");
                    str.AppendLine("   AND G.COD_ALINEA         = A.COD_ALINEA ");
                    str.AppendLine("   AND CMM.COD_MATERIAL     IN ('26041236','15044373','1503138X')");
                    if (!string.IsNullOrEmpty(codgrupo))
                        str.AppendLine("   AND GCC.COD_GRUPO_SICH = :COD_GRUPO_SICH ");
                    str.AppendLine(" GROUP BY CMM.COD_CENCUSTO, GCC.COD_GRUPO_SICH, CMM.NUM_ANO, CMM.NUM_MES ");
                    if (ordem == 0)
                        str.AppendLine(" ORDER BY CMM.NUM_MES ");
                    else if (ordem == 1)
                        str.AppendLine(" ORDER BY NOM_CENCUSTO,CMM.NUM_MES ");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["NUM_ANO"] = ano;
                    query.Params["IDF_CLASSE"] = classe;
                    query.Params["COD_INST_SISTEMA"] = idInstSistema;
                    if (!string.IsNullOrEmpty(codgrupo))
                        query.Params["COD_GRUPO_SICH"] = codgrupo;
                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _cm = new Hcrp.Framework.Classes.ConsumoMensalMaterial();
                        if (dr["COD_CENCUSTO"] != DBNull.Value)
                            _cm._CodCenCusto = dr["COD_CENCUSTO"].ToString();

                        if (dr["COD_GRUPO_SICH"] != DBNull.Value)
                            _cm._CodGrupoCentroCusto = Convert.ToInt32(dr["COD_GRUPO_SICH"]);
                        if (dr["NUM_ANO"] != DBNull.Value)
                            _cm.Ano = dr["NUM_ANO"].ToString();
                        if (dr["NUM_MES"] != DBNull.Value)
                            _cm.Mes = Convert.ToInt32(dr["NUM_MES"]);
                        if (dr["VLR_TOTAL_CONSUMO"] != DBNull.Value)
                            _cm.VlrTotalConsumo = Convert.ToDouble(dr["VLR_TOTAL_CONSUMO"]);
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

        public List<Hcrp.Framework.Classes.ConsumoMensalMaterial> BuscarConsumoMaterialPorItemPlanoContaCC(string itemPlanoConta, string ano, int ordem, string codgrupo)
        {
            List<Hcrp.Framework.Classes.ConsumoMensalMaterial> _listaDeRetorno = new List<Hcrp.Framework.Classes.ConsumoMensalMaterial>();
            Hcrp.Framework.Classes.ConsumoMensalMaterial _cm = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    int idInstSistema = new Hcrp.Framework.Classes.ConfiguracaoSistema().CodInstituicaoSistema;
                    // Cria a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT CMM.COD_CENCUSTO, MAX(CC.NOM_CENCUSTO) NOM_CENCUSTO, GCC.COD_GRUPO_SICH, CMM.NUM_ANO, CMM.NUM_MES, NVL(SUM(CMM.VLR_TOTAL_CONSUMO),0) VLR_TOTAL_CONSUMO  ");
                    str.AppendLine(" FROM CONSUMO_MENSAL_MATERIAL CMM, CENTRO_CUSTO CC, GRUPO_CENCUSTO_SICH GCC, ");
                    str.AppendLine("      MATERIAL M, GRUPO G, GRUPO_PLANOCONTAITEM GP ");
                    str.AppendLine(string.Format(" WHERE CMM.NUM_ANO = {0} ", ano));
                    str.AppendLine(string.Format("   AND GCC.COD_INST_SISTEMA = {0} ", idInstSistema));
                    str.AppendLine("   AND CMM.COD_CENCUSTO     = CC.COD_CENCUSTO ");
                    str.AppendLine("   AND CC.COD_GRUPO_SICH    = GCC.COD_GRUPO_SICH ");
                    str.AppendLine("   AND CMM.COD_MATERIAL     = M.COD_MATERIAL ");
                    str.AppendLine("   AND M.COD_GRUPO          = G.COD_GRUPO ");
                    str.AppendLine("   AND G.COD_GRUPO          = GP.COD_GRUPO");
                    if (itemPlanoConta != "0")
                        str.AppendLine(string.Format("   AND GP.SEQ_ITEM_PLANO_CONTA = {0} ", itemPlanoConta));
                    if (!string.IsNullOrEmpty(codgrupo))
                        str.AppendLine(string.Format("   AND GCC.COD_GRUPO_SICH = {0} ", codgrupo));
                    str.AppendLine(" GROUP BY CMM.COD_CENCUSTO, GCC.COD_GRUPO_SICH, CMM.NUM_ANO, CMM.NUM_MES  ");
                    if (ordem == 0)
                        str.AppendLine(" ORDER BY CMM.NUM_MES ");
                    else if (ordem == 1)
                        str.AppendLine(" ORDER BY NOM_CENCUSTO,CMM.NUM_MES ");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _cm = new Hcrp.Framework.Classes.ConsumoMensalMaterial();
                        if (dr["COD_CENCUSTO"] != DBNull.Value)
                            _cm._CodCenCusto = dr["COD_CENCUSTO"].ToString();

                        if (dr["COD_GRUPO_SICH"] != DBNull.Value)
                            _cm._CodGrupoCentroCusto = Convert.ToInt32(dr["COD_GRUPO_SICH"]);
                        if (dr["NUM_ANO"] != DBNull.Value)
                            _cm.Ano = dr["NUM_ANO"].ToString();
                        if (dr["NUM_MES"] != DBNull.Value)
                            _cm.Mes = Convert.ToInt32(dr["NUM_MES"]);
                        if (dr["VLR_TOTAL_CONSUMO"] != DBNull.Value)
                            _cm.VlrTotalConsumo = Convert.ToDouble(dr["VLR_TOTAL_CONSUMO"]);
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

        public List<Hcrp.Framework.Classes.ConsumoMensalMaterial> BuscarConsumoMaterialPorAlineaMaterial(int classe, string ano, int ordem, string codcencusto, string material)
        {
            List<Hcrp.Framework.Classes.ConsumoMensalMaterial> _listaDeRetorno = new List<Hcrp.Framework.Classes.ConsumoMensalMaterial>();
            Hcrp.Framework.Classes.ConsumoMensalMaterial _cm = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    int idInstSistema = new Hcrp.Framework.Classes.ConfiguracaoSistema().CodInstituicaoSistema;
                    // Cria a query
                    StringBuilder str = new StringBuilder();

                    //str.AppendLine(" SELECT CMM.COD_MATERIAL, MAX(M.NOM_MATERIAL) NOM_MATERIAL, MAX(U.COD_UNIDADE) UNIDADE, '0' PLANO,  CASE WHEN MNA.SEQ_MAT_NAO_APROP_CC IS NULL THEN 'SIM' ELSE 'NAO' END APROPRIAR, CMM.NUM_ANO, CMM.NUM_MES, ");
                    //str.AppendLine("        NVL(SUM(CMM.VLR_TOTAL_CONSUMO),0) VLR_TOTAL_CONSUMO, NVL(SUM(CMM.QTD_CONSUMIDA),0) QTD_CONSUMIDA, NVL(SUM(CMM.QTD_PROGRAMADA_ANO),0) QTD_PROGRAMADA_ANO");
                    //str.AppendLine(" FROM CONSUMO_MENSAL_MATERIAL CMM, CENTRO_CUSTO CC, GRUPO_CENCUSTO_SICH GCC, ");
                    //str.AppendLine("      MATERIAL M, SUB_GRUPO SG, GRUPO G, ALINEA A ");
                    //str.AppendLine("      ,MATERIAL_NAO_APROPRIACAO_CC MNA, UNIDADE U ");
                    //str.AppendLine(" WHERE CMM.NUM_ANO = :NUM_ANO ");
                    //str.AppendLine("   AND A.IDF_CLASSE = :IDF_CLASSE ");
                    //str.AppendLine("   AND GCC.COD_INST_SISTEMA = :COD_INST_SISTEMA ");
                    //str.AppendLine("   AND CMM.COD_CENCUSTO     = CC.COD_CENCUSTO ");
                    //str.AppendLine("   AND CC.COD_GRUPO_SICH    = GCC.COD_GRUPO_SICH ");
                    //str.AppendLine("   AND CMM.COD_MATERIAL     = M.COD_MATERIAL ");
                    //str.AppendLine("   AND M.COD_GRUPO          = SG.COD_GRUPO ");
                    //str.AppendLine("   AND M.COD_SUB_GRUPO      = SG.COD_SUB_GRUPO ");
                    //str.AppendLine("   AND SG.COD_GRUPO         = G.COD_GRUPO ");
                    //str.AppendLine("   AND G.COD_ALINEA         = A.COD_ALINEA ");
                    //str.AppendLine("   AND CMM.COD_MATERIAL     IN ('26041236','15044373','1503138X')");
                    //str.AppendLine(" AND CMM.COD_MATERIAL = MNA.COD_MATERIAL(+) ");
                    //str.AppendLine(" AND CMM.COD_CENCUSTO = MNA.COD_CENCUSTO(+) ");
                    //str.AppendLine(" AND MNA.NUM_USER_EXCLUSAO(+) IS NULL ");
                    //if (!string.IsNullOrEmpty(codcencusto))
                    //    str.AppendLine(" AND CMM.COD_CENCUSTO = :COD_CENCUSTO");
                    //str.AppendLine(" AND M.COD_UNIDADE = U.COD_UNIDADE");
                    //str.AppendLine(" GROUP BY CMM.COD_MATERIAL, CMM.NUM_ANO, CMM.NUM_MES, MNA.SEQ_MAT_NAO_APROP_CC ");
                    //if (ordem == 0)
                    //    str.AppendLine(" ORDER BY CMM.NUM_MES ");
                    //else if (ordem == 1)
                    //    str.AppendLine(" ORDER BY NOM_MATERIAL,CMM.NUM_MES ");

                    str.AppendLine(" SELECT CMM.COD_MATERIAL, MAX(M.NOM_MATERIAL) NOM_MATERIAL, MAX(U.COD_UNIDADE) UNIDADE, P.NOM_ITEM_PLANO_CONTA PLANO, CMM.NUM_ANO, CMM.NUM_MES, ");
                    str.AppendLine("     NVL(SUM(CMM.VLR_TOTAL_CONSUMO),0) VLR_TOTAL_CONSUMO, NVL(SUM(CMM.QTD_CONSUMIDA),0) QTD_CONSUMIDA, NVL(SUM(CMM.QTD_PROGRAMADA_ANO),0) QTD_PROGRAMADA_ANO ");
                    str.AppendLine(" FROM CONSUMO_MENSAL_MATERIAL CMM, CENTRO_CUSTO CC, GRUPO_CENCUSTO_SICH GCC, ");
                    str.AppendLine("     MATERIAL M, GRUPO G, ALINEA A , UNIDADE U, GRUPO_PLANOCONTAITEM GP, PLANO_CONTA_ITEM P ");
                    str.AppendLine(string.Format(" WHERE CMM.NUM_ANO = {0} ", ano));
                    str.AppendLine(string.Format("  AND A.IDF_CLASSE = {0} ", classe));
                    str.AppendLine(string.Format("  AND GCC.COD_INST_SISTEMA    = {0} ", idInstSistema));
                    str.AppendLine("  AND CMM.COD_CENCUSTO        = CC.COD_CENCUSTO ");
                    str.AppendLine("  AND CC.COD_GRUPO_SICH       = GCC.COD_GRUPO_SICH ");
                    str.AppendLine("  AND CMM.COD_MATERIAL        = M.COD_MATERIAL ");
                    str.AppendLine("  AND M.COD_GRUPO             = G.COD_GRUPO ");
                    str.AppendLine("  AND G.COD_ALINEA            = A.COD_ALINEA ");
                    str.AppendLine("  AND G.COD_GRUPO             = GP.COD_GRUPO(+) ");
                    str.AppendLine("  AND GP.SEQ_ITEM_PLANO_CONTA = P.SEQ_ITEM_PLANO_CONTA(+) ");
                    if (!string.IsNullOrEmpty(codcencusto))
                        str.AppendLine(string.Format(" AND CMM.COD_CENCUSTO = '{0}' ", codcencusto));
                    str.AppendLine(" AND M.COD_UNIDADE = U.COD_UNIDADE ");
                    if (!string.IsNullOrEmpty(material))
                        str.AppendLine(string.Format(" AND M.COD_MATERIAL = '{0}' ", material));
                    str.AppendLine(" GROUP BY CMM.COD_MATERIAL, P.NOM_ITEM_PLANO_CONTA, CMM.NUM_ANO, CMM.NUM_MES ");
                    if (ordem == 0)
                        str.AppendLine(" ORDER BY CMM.NUM_MES ");
                    else if (ordem == 1)
                        str.AppendLine(" ORDER BY NOM_MATERIAL,CMM.NUM_MES ");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _cm = new Hcrp.Framework.Classes.ConsumoMensalMaterial();
    
                        if (dr["COD_MATERIAL"] != DBNull.Value)
                            _cm._CodMaterial = dr["COD_MATERIAL"].ToString();
                        if (dr["UNIDADE"] != DBNull.Value)
                            _cm._CodUnidade = Convert.ToInt32(dr["UNIDADE"]);
                        if (dr["PLANO"] != DBNull.Value)
                            _cm.PlanoConta = dr["PLANO"].ToString();
                        //if (dr["APROPRIAR"] != DBNull.Value)
                        //    _cm.Apropriar = dr["APROPRIAR"].ToString();
                        if (dr["QTD_CONSUMIDA"] != DBNull.Value)
                            _cm.QtdConsumida = Convert.ToDouble(dr["QTD_CONSUMIDA"]);
                        if (dr["QTD_PROGRAMADA_ANO"] != DBNull.Value)
                            _cm.QtdProgramadaAno = Convert.ToDouble(dr["QTD_PROGRAMADA_ANO"]);
                        if (dr["NUM_ANO"] != DBNull.Value)
                            _cm.Ano = dr["NUM_ANO"].ToString();
                        if (dr["NUM_MES"] != DBNull.Value)
                            _cm.Mes = Convert.ToInt32(dr["NUM_MES"]);
                        if (dr["VLR_TOTAL_CONSUMO"] != DBNull.Value)
                            _cm.VlrTotalConsumo = Convert.ToDouble(dr["VLR_TOTAL_CONSUMO"]);
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
