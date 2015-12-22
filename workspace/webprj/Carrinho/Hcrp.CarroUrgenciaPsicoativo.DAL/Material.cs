using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hcrp.Infra.AcessoDado;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class Material
    {
        public List<Entity.Material> ObterListaMateriais(string codigoMaterial, string nomeMaterial, int? codigoAlinea)
        {
            List<Entity.Material> listRetorno = new List<Entity.Material>();

            Entity.Material material = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    str.AppendLine(" SELECT A.COD_MATERIAL, ");
                    str.AppendLine("        A.NOM_MATERIAL, ");
                    str.AppendLine("        E.NOM_UNIDADE, ");
                    str.AppendLine("        D.DSC_ALINEA ");
                    str.AppendLine("   FROM MATERIAL           A, ");
                    str.AppendLine("        SUB_GRUPO          B, ");
                    str.AppendLine("        ESTOQUE.GRUPO      C, ");
                    str.AppendLine("        ESTOQUE.ALINEA     D, ");
                    str.AppendLine("        PRESCRICAO.UNIDADE E ");
                    str.AppendLine(" WHERE A.COD_GRUPO = B.COD_GRUPO ");
                    str.AppendLine("    AND A.COD_SUB_GRUPO = B.COD_SUB_GRUPO ");
                    str.AppendLine("    AND C.COD_GRUPO = B.COD_GRUPO ");
                    str.AppendLine("    AND D.COD_ALINEA = C.COD_ALINEA ");
                    str.AppendLine("    AND A.COD_UNIDADE = E.COD_UNIDADE ");
                    str.AppendLine("    AND D.COD_ALINEA = NVL(:COD_ALINEA,D.COD_ALINEA) ");
                    str.AppendLine("    AND A.COD_MATERIAL = NVL(:COD_MATERIAL,A.COD_MATERIAL) ");
                    str.AppendLine("    and a.NOM_MATERIAL like NVL(:NOM_MATERIAL,a.NOM_MATERIAL) ");


                    QueryCommandConfig query = new QueryCommandConfig(str.ToString());

                    if (codigoAlinea.HasValue)
                        query.Params["COD_ALINEA"] = codigoAlinea;
                    else
                        query.Params["COD_ALINEA"] = DBNull.Value;

                    if (!string.IsNullOrEmpty(codigoMaterial))
                        query.Params["COD_MATERIAL"] = codigoMaterial.ToUpper();
                    else
                        query.Params["COD_MATERIAL"] = DBNull.Value;

                    if (!string.IsNullOrEmpty(nomeMaterial))
                        query.Params["NOM_MATERIAL"] = nomeMaterial.ToUpper() + "%";
                    else
                        query.Params["NOM_MATERIAL"] = DBNull.Value;

                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        material = new Entity.Material();
                        material.Alinea = new Framework.Classes.Alinea();
                        material.Unidade = new Framework.Classes.Unidade();

                        if (dr["COD_MATERIAL"] != DBNull.Value)
                            material.Codigo = dr["COD_MATERIAL"].ToString();

                        if (dr["NOM_MATERIAL"] != DBNull.Value)
                            material.Nome = dr["NOM_MATERIAL"].ToString();

                        if (dr["NOM_UNIDADE"] != DBNull.Value)
                            material.Unidade.Nome = dr["NOM_UNIDADE"].ToString();
                            
                        if (dr["DSC_ALINEA"] != DBNull.Value)
                            material.Alinea.Nome = dr["DSC_ALINEA"].ToString();

                        listRetorno.Add(material);
                    }
                }
            }
            catch (Exception err)
            {
                throw ;
            }

            return listRetorno;
        }

        public List<Entity.Material> ObterMaterial(string codigoMaterial)
        {
            List<Entity.Material> listRetorno = new List<Entity.Material>();

            Entity.Material material = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    str.AppendLine(" SELECT A.COD_MATERIAL, ");
                    str.AppendLine("        A.NOM_MATERIAL, ");
                    str.AppendLine("        E.NOM_UNIDADE, ");
                    str.AppendLine("        D.DSC_ALINEA ");
                    str.AppendLine("   FROM MATERIAL           A, ");
                    str.AppendLine("        SUB_GRUPO          B, ");
                    str.AppendLine("        ESTOQUE.GRUPO      C, ");
                    str.AppendLine("        ESTOQUE.ALINEA     D, ");
                    str.AppendLine("        PRESCRICAO.UNIDADE E ");
                    str.AppendLine(" WHERE A.COD_GRUPO = B.COD_GRUPO ");
                    str.AppendLine("    AND A.COD_SUB_GRUPO = B.COD_SUB_GRUPO ");
                    str.AppendLine("    AND C.COD_GRUPO = B.COD_GRUPO ");
                    str.AppendLine("    AND D.COD_ALINEA = C.COD_ALINEA ");
                    str.AppendLine("    AND A.COD_UNIDADE = E.COD_UNIDADE ");
                    str.AppendLine("    AND A.COD_MATERIAL = :COD_MATERIAL ");

                    QueryCommandConfig query = new QueryCommandConfig(str.ToString());

                    if (!string.IsNullOrEmpty(codigoMaterial))
                        query.Params["COD_MATERIAL"] = codigoMaterial.ToUpper();
                    else
                        query.Params["COD_MATERIAL"] = DBNull.Value;

                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        material = new Entity.Material();
                        material.Alinea = new Framework.Classes.Alinea();
                        material.Unidade = new Framework.Classes.Unidade();

                        if (dr["COD_MATERIAL"] != DBNull.Value)
                            material.Codigo = dr["COD_MATERIAL"].ToString();

                        if (dr["NOM_MATERIAL"] != DBNull.Value)
                            material.Nome = dr["NOM_MATERIAL"].ToString();

                        if (dr["NOM_UNIDADE"] != DBNull.Value)
                            material.Unidade.Nome = dr["NOM_UNIDADE"].ToString();

                        if (dr["DSC_ALINEA"] != DBNull.Value)
                            material.Alinea.Nome = dr["DSC_ALINEA"].ToString();

                        listRetorno.Add(material);

                        break;
                    }
                }
            }
            catch (Exception err)
            {
                throw;
            }

            return listRetorno;
        }

        #region Código de barras
        /// <summary>
        /// Verificar se é um caracter de tipo de código de barras.
        /// </summary>               
        public bool EhCaracterDeTipoDeCodigoDeBarras(string caracter)
        {
            bool ehDoTipoDeCodigoDeBarras = false;

            try
            {

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT COUNT(*) AS QTD ");
                    str.AppendLine(" FROM TIPO_IDENTIFICACAO_BARRAS A, ");
                    str.AppendLine("      TIPO_IDENTIFICACAO_BARRAS_ITEM B ");
                    str.AppendLine(" WHERE A.SGL_TIPO_IDENTIFICACAO = :SGL_TIPO_IDENTIFICACAO  "); // AQUI SÃO INFORMADOS OS 2 CARACTERES INICIAIS
                    str.AppendLine(" AND A.SGL_TIPO_IDENTIFICACAO = B.SGL_TIPO_IDENTIFICACAO ");

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["SGL_TIPO_IDENTIFICACAO"] = caracter;

                    // Abrir conexão
                    ctx.Open();

                    // Executar a query
                    ctx.ExecuteQuery(query);

                    while (ctx.Reader.Read())
                    {

                        if (ctx.Reader["QTD"] != DBNull.Value)
                        {
                            if (Convert.ToInt32(ctx.Reader["QTD"]) > 0)
                                ehDoTipoDeCodigoDeBarras = true;
                        }

                        break;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return ehDoTipoDeCodigoDeBarras;
        }

        /// <summary>
        /// Obter a unidade do material.
        /// </summary>               
        public void ObterCodigoDaUnidadeDoMaterial(string codigoMaterial, out string codMaterialEncontrado, out int? codUnidade)
        {
            codMaterialEncontrado = string.Empty;
            codUnidade = null;

            try
            {

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT M.COD_UNIDADE, ");
                    str.AppendLine("        M.COD_MATERIAL ");
                    str.AppendLine("   FROM MATERIAL M ");
                    str.AppendLine(" WHERE M.COD_MATERIAL = :COD_MATERIAL "); // CARACTERES INSERIDOS;

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["COD_MATERIAL"] = codigoMaterial;

                    // Abrir conexão
                    ctx.Open();

                    // Executar a query
                    ctx.ExecuteQuery(query);

                    while (ctx.Reader.Read())
                    {

                        if (ctx.Reader["COD_UNIDADE"] != DBNull.Value)
                            codUnidade = Convert.ToInt32(ctx.Reader["COD_UNIDADE"]);

                        if (ctx.Reader["COD_MATERIAL"] != DBNull.Value)
                            codMaterialEncontrado = ctx.Reader["COD_MATERIAL"].ToString();


                        break;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter a unidade do material Comercial.
        /// </summary>               
        public void ObterCodigoDaUnidadeDoMaterialComercial(string codigoMaterial, out string codMaterialEncontrado, out int? codUnidade)
        {
            codMaterialEncontrado = string.Empty;
            codUnidade = null;

            try
            {

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT M.COD_MATERIAL, M.COD_UNIDADE ");
                    str.AppendLine(" FROM NOME_COMERCIAL NC, MATERIAL M ");
                    str.AppendLine(" WHERE NC.COD_COMERCIAL = :COD_MATERIAL ");
                    str.AppendLine(" AND NC.COD_MATERIAL = M.COD_MATERIAL  ");
                    str.AppendLine(" AND ROWNUM = 1 ");

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["COD_MATERIAL"] = codigoMaterial;

                    // Abrir conexão
                    ctx.Open();

                    // Executar a query
                    ctx.ExecuteQuery(query);

                    while (ctx.Reader.Read())
                    {

                        if (ctx.Reader["COD_UNIDADE"] != DBNull.Value)
                            codUnidade = Convert.ToInt32(ctx.Reader["COD_UNIDADE"]);

                        if (ctx.Reader["COD_MATERIAL"] != DBNull.Value)
                            codMaterialEncontrado = ctx.Reader["COD_MATERIAL"].ToString();


                        break;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Obter dados da instuição para livre esclarecido por código do instituto.
        /// </summary>        
        public DataView ObterEtiquetaBeiraLeitoPorNumeroDoLoteEmBase36(string numLoteEmBase36)
        {
            DataTable dtRetorno = new DataTable();

            try
            {

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT L.NUM_LOTE, ");
                    str.AppendLine("        INF.COD_MATERIAL, ");
                    str.AppendLine("        M.COD_UNIDADE ");
                    str.AppendLine(" FROM LOTE L, ");
                    str.AppendLine("      ITEM_NOTA_FISCAL INF, ");
                    str.AppendLine("      MATERIAL M ");
                    str.AppendLine(string.Format(" WHERE L.NUM_LOTE = GENERICO.FNC_CONVERTE_DECIMAL_B36('{0}',2) ", numLoteEmBase36));
                    str.AppendLine(" AND L.SEQ_NOTA_FISCAL = INF.SEQ_NOTA_FISCAL ");
                    str.AppendLine(" AND L.COD_MATERIAL = INF.COD_MATERIAL ");
                    str.AppendLine(" AND INF.COD_MATERIAL = M.COD_MATERIAL ");

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Abrir conexão
                    ctx.Open();

                    // Executar a query
                    ctx.ExecuteQuery(query);

                    dtRetorno.Load(ctx.Reader);

                }
            }
            catch (Exception)
            {
                throw;
            }

            return dtRetorno.DefaultView;
        }

        /// <summary>
        /// Obter o local do material pelo número do lote.
        /// </summary>               
        public int ObterLocalDoMaterialPeloNumeroDeLote(long numeroLote)
        {
            int numSeqLocal = 0;

            try
            {

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT ML.NUM_SEQ_LOCAL ");
                    str.AppendLine(" FROM MAPEAMENTO_LOCAL ML, ");
                    str.AppendLine("      CENTRO_CUSTO CC, ");
                    str.AppendLine("      ITEM_NOTA_FISCAL INF, ");
                    str.AppendLine("      LOTE L ");
                    str.AppendLine(" WHERE ML.COD_CENCUSTO = CC.COD_CENCUSTO ");
                    str.AppendLine(" AND CC.COD_CENCUSTO = INF.COD_CENCUSTO_RECEBIMENTO ");
                    str.AppendLine(" AND INF.SEQ_NOTA_FISCAL = L.SEQ_NOTA_FISCAL ");
                    str.AppendLine(" AND INF.COD_MATERIAL = L.COD_MATERIAL ");
                    str.AppendLine(string.Format(" AND L.NUM_LOTE = {0} ", numeroLote));

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Abrir conexão
                    ctx.Open();

                    // Executar a query
                    ctx.ExecuteQuery(query);

                    while (ctx.Reader.Read())
                    {
                        if (ctx.Reader["NUM_SEQ_LOCAL"] != DBNull.Value)
                            numSeqLocal = Convert.ToInt32(ctx.Reader["NUM_SEQ_LOCAL"]);

                        break;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return numSeqLocal;
        }

        /// <summary>
        /// Obter o código de material e código de unidade do material por seq insumo unitário.
        /// </summary>               
        public void ObterCodigoEUnidadeDoMaterialPorSeqInsumoUnitario(long seqInsumoUnitario, out int? codigoDaUnidade, out string codigoMaterial)
        {
            codigoDaUnidade = null;
            codigoMaterial = string.Empty;

            try
            {

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT I.COD_MATERIAL, ");
                    str.AppendLine("        I.COD_UNIDADE ");
                    str.AppendLine(" FROM INSUMO_UNITARIO IU, INSUMO I ");
                    str.AppendLine(" WHERE IU.SEQ_INSUMO = I.SEQ_INSUMO ");
                    str.AppendLine(string.Format(" AND IU.SEQ_INSUMO_UNITARIO = {0} ", seqInsumoUnitario)); // SEQ_INSUMO_UNITARIO INFORMADO

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Abrir conexão
                    ctx.Open();

                    // Executar a query
                    ctx.ExecuteQuery(query);

                    while (ctx.Reader.Read())
                    {
                        if (ctx.Reader["COD_MATERIAL"] != DBNull.Value)
                            codigoMaterial = ctx.Reader["COD_MATERIAL"].ToString();

                        if (ctx.Reader["COD_UNIDADE"] != DBNull.Value)
                            codigoDaUnidade = Convert.ToInt32(ctx.Reader["COD_UNIDADE"]);

                        break;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter lote, série, local atendente e seq insumo.
        /// </summary>               
        public void ObterLoteSerieLocalAtendenteESeqInsumo(long seqInsumoUnitario, out long numLote)
        {
            numLote = 0;

            try
            {

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT IU.NUM_LOTE ");
                    str.AppendLine(" FROM INSUMO_UNITARIO IU ");
                    str.AppendLine(" WHERE IU.SEQ_INSUMO_UNITARIO = :SEQ_INSUMO_UNITARIO "); // SEQ_INSUMO_UNITARIO INFORMADO 

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["SEQ_INSUMO_UNITARIO"] = seqInsumoUnitario;

                    // Abrir conexão
                    ctx.Open();

                    // Executar a query
                    ctx.ExecuteQuery(query);

                    while (ctx.Reader.Read())
                    {
                        if (ctx.Reader["NUM_LOTE"] != DBNull.Value)
                            numLote = Convert.ToInt64(ctx.Reader["NUM_LOTE"]);

                        break;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter o código de material e código de unidade do material por seq insumo.
        /// </summary>               
        public void ObterCodigoEUnidadeDoMaterialPorSeqInsumo(long seqInsumo, out int? codigoDaUnidade, out string codigoMaterial)
        {
            codigoDaUnidade = null;
            codigoMaterial = string.Empty;

            try
            {

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT I.COD_MATERIAL, ");
                    str.AppendLine("        I.COD_UNIDADE ");
                    str.AppendLine(" FROM INSUMO I ");
                    str.AppendLine(" WHERE I.SEQ_INSUMO = :SEQ_INSUMO "); // SEQ_INSUMO

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["SEQ_INSUMO"] = seqInsumo;

                    // Abrir conexão
                    ctx.Open();

                    // Executar a query
                    ctx.ExecuteQuery(query);

                    while (ctx.Reader.Read())
                    {
                        if (ctx.Reader["COD_MATERIAL"] != DBNull.Value)
                            codigoMaterial = ctx.Reader["COD_MATERIAL"].ToString();

                        if (ctx.Reader["COD_UNIDADE"] != DBNull.Value)
                            codigoDaUnidade = Convert.ToInt32(ctx.Reader["COD_UNIDADE"]);

                        break;
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
