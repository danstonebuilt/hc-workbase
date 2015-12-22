using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;

namespace Hcrp.Framework.Dal
{
    public class Material : Hcrp.Framework.Classes.Material
    {
        public List<Hcrp.Framework.Classes.Material> ObterListaDeMaterial(int paginaAtual, out int totalRegistro, string filtroCodMaterial, string filtroNomeMaterial, string filtroCodGrupo, string filtroCodSubGrupo)
        {
            List<Hcrp.Framework.Classes.Material> _listaDeRetorno = new List<Hcrp.Framework.Classes.Material>();
            Hcrp.Framework.Classes.Material _material = null;
            totalRegistro = 0;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    // Cria a query
                    StringBuilder str = new StringBuilder();
                    StringBuilder strTotalRegistro = new StringBuilder();
                    StringBuilder strWhere = new StringBuilder();

                    // Montar escopo de paginação.
                    Int32 numeroRegistroPorPagina = 10; /*Ver*/
                    Int32 ultimoIndice = (numeroRegistroPorPagina * paginaAtual);
                    Int32 primeiroIndice = (ultimoIndice - numeroRegistroPorPagina) + 1;

                    strWhere.AppendLine(string.Format(" WHERE IDF_ATIVO = 'S'"));

                    if (!string.IsNullOrWhiteSpace(filtroCodMaterial) || !string.IsNullOrWhiteSpace(filtroNomeMaterial))
                    {

                        if (!string.IsNullOrWhiteSpace(filtroCodMaterial))
                        {
                            strWhere.AppendLine(string.Format(" AND COD_MATERIAL LIKE '%{0}%' ", filtroCodMaterial.ToUpper()));
                        }

                        if (!string.IsNullOrWhiteSpace(filtroNomeMaterial))
                        {
                            strWhere.AppendLine(string.Format(" AND NOM_MATERIAL LIKE '%{0}%' ", filtroNomeMaterial.ToUpper()));
                        }
                        if (((!string.IsNullOrWhiteSpace(filtroCodGrupo) && !filtroCodGrupo.Equals("0"))) && ((!string.IsNullOrWhiteSpace(filtroCodSubGrupo) && !filtroCodSubGrupo.Equals("0"))))
                        {
                            strWhere.AppendLine(string.Format(" AND COD_GRUPO = {0} AND COD_SUB_GRUPO = {1} ", filtroCodGrupo.ToUpper(), filtroCodSubGrupo.ToUpper()));
                        }
                    }

                    str.AppendLine(" SELECT * FROM (SELECT A.*, ROWNUM AS RNUM FROM (SELECT DISTINCT ");
                    str.AppendLine("  MAT.COD_MATERIAL, ");
                    str.AppendLine("  MAT.NOM_MATERIAL, MAT.COD_GRUPO, MAT.COD_SUB_GRUPO ");
                    str.AppendLine("FROM MATERIAL MAT ");
                    if (strWhere.Length > 0)
                        str.AppendLine(strWhere.ToString());
                    str.AppendLine(" ORDER BY MAT.NOM_MATERIAL ASC) A ");
                    str.Append(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

                    // Montar query para pegar o total de registros.
                    strTotalRegistro.Append("SELECT COUNT(*) TOTAL ");
                    strTotalRegistro.AppendLine(" FROM MATERIAL MAT ");
                    if (strWhere.Length > 0)
                        strTotalRegistro.AppendLine(strWhere.ToString());

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());
                    Hcrp.Infra.AcessoDado.QueryCommandConfig queryCount = new Hcrp.Infra.AcessoDado.QueryCommandConfig(strTotalRegistro.ToString());

                    // Abre conexão
                    ctx.Open();

                    // Veriricar contador
                    ctx.ExecuteQuery(queryCount);

                    while (ctx.Reader.Read())
                    {
                        totalRegistro = Convert.ToInt32(ctx.Reader["TOTAL"]);
                    }

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _material = new Hcrp.Framework.Classes.Material();

                        if (dr["COD_MATERIAL"] != DBNull.Value)
                            _material.Codigo = dr["COD_MATERIAL"].ToString();

                        if (dr["NOM_MATERIAL"] != DBNull.Value)
                            _material.Nome = dr["NOM_MATERIAL"].ToString();

                        if (dr["COD_GRUPO"] != DBNull.Value)
                            _material.CodigoGrupo = dr["COD_GRUPO"].ToString();

                        if (dr["COD_SUB_GRUPO"] != DBNull.Value)
                            _material.CodigoGrupo = dr["COD_SUB_GRUPO"].ToString();
                        _listaDeRetorno.Add(_material);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaDeRetorno;

        }

        public Hcrp.Framework.Classes.Material BuscaMaterialCodigo(string codMat)
        {
            Hcrp.Framework.Classes.Material _mat = new Hcrp.Framework.Classes.Material();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Cria a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT * ");
                    str.AppendLine(" FROM MATERIAL   ");
                    str.AppendLine(" WHERE COD_MATERIAL = :COD_MATERIAL");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["COD_MATERIAL"] = codMat;
                    // Abre conexão
                    ctx.Open();

                    // Veriricar contador
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        if (dr["COD_MATERIAL"] != DBNull.Value)
                            _mat.Codigo = dr["COD_MATERIAL"].ToString();

                        if (dr["NOM_MATERIAL"] != DBNull.Value)
                            _mat.Nome = dr["NOM_MATERIAL"].ToString();

                        if (dr["COD_GRUPO"] != DBNull.Value)
                            _mat.CodigoGrupo = dr["COD_GRUPO"].ToString();

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _mat;
        }

        public List<Hcrp.Framework.Classes.Material> BuscaMaterialNaoApropriado(string CodMaterial, string CodCenCusto)
        {
            List<Hcrp.Framework.Classes.Material> _listaDeRetorno = new List<Hcrp.Framework.Classes.Material>();
            Hcrp.Framework.Classes.Material _matnaoap = null;            

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    // Cria a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine("SELECT NUM_USER_CADASTRO, DTA_HOR_CADASTRO,");
                    str.AppendLine("       COD_MATERIAL, COD_CENCUSTO,  ");
                    str.AppendLine("       NUM_USER_EXCLUSAO, DTA_HOR_EXCLUSAO , SEQ_MAT_NAO_APROP_CC ");
                    str.AppendLine("FROM MATERIAL_NAO_APROPRIACAO_CC   ");
                    str.AppendLine(" WHERE COD_MATERIAL = " + CodMaterial);
                    str.AppendLine("   AND COD_CENCUSTO = " + CodCenCusto);                    

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());
                    
                    // Abre conexão
                    ctx.Open();

                    // Veriricar contador
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {

                        _matnaoap = new Hcrp.Framework.Classes.Material();

                        if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                            _matnaoap.ApropUsuarioCad = dr["NUM_USER_CADASTRO"].ToString();
                        if (dr["DTA_HOR_CADASTRO"] != DBNull.Value)
                            _matnaoap.ApropDataCadastro = Convert.ToDateTime(dr["DTA_HOR_CADASTRO"]);
                        if (dr["COD_MATERIAL"] != DBNull.Value)
                            _matnaoap.Codigo = dr["COD_MATERIAL"].ToString();
                        if (dr["COD_CENCUSTO"] != DBNull.Value)
                            _matnaoap.ApropCodCenCusto = dr["COD_CENCUSTO"].ToString();
                        if (dr["DTA_HOR_EXCLUSAO"] != DBNull.Value)                            
                            _matnaoap.ApropDataExclusao = Convert.ToDateTime(dr["DTA_HOR_EXCLUSAO"]);
                        if (dr["NUM_USER_EXCLUSAO"] != DBNull.Value)
                            _matnaoap.ApropUsuarioExc = dr["NUM_USER_EXCLUSAO"].ToString();
                        if (dr["SEQ_MAT_NAO_APROP_CC"] != DBNull.Value)
                            _matnaoap.ApropSeq = Convert.ToInt32(dr["SEQ_MAT_NAO_APROP_CC"]);

                        _listaDeRetorno.Add(_matnaoap);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaDeRetorno;
        }
        public long InserirMatNaoAprop(Framework.Classes.Material Material)
        {
            try
            {
                long retorno = 0;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("MATERIAL_NAO_APROPRIACAO_CC");
                    comando.Params["NUM_USER_CADASTRO"] = Material.ApropUsuarioCad;
                    comando.Params["DTA_HOR_CADASTRO"] = Material.ApropDataCadastro;
                    comando.Params["COD_CENCUSTO"] = Material.ApropCodCenCusto;
                    comando.Params["COD_MATERIAL"] = Material.Codigo;                    
                    
                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                    // Pegar o último ID
                    retorno = ctx.GetSequenceValue("GENERICO.SEQ_MAT_NAO_APROP_CC", false);

                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public double AlterarMatNaoAprop(Framework.Classes.Material Material)
        {
            try
            {
                double retorno = 0;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("UPDATE MATERIAL_NAO_APROPRIACAO_CC SET NUM_USER_EXCLUSAO = " + Material.ApropUsuarioCad + ", DTA_HOR_EXCLUSAO = SYSDATE WHERE COD_MATERIAL = " +  Material.Codigo + "    AND COD_CENCUSTO = " + Material.ApropCodCenCusto + " AND NUM_USER_EXCLUSAO IS NULL");                    

                    // Executar o Update
                    ctx.ExecuteUpdate(comando);

                    // Pegar o último ID
                    retorno = Material.ApropSeq;

                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
    
}
