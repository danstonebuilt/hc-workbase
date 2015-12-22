using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hcrp.Infra.AcessoDado;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class BemPatrimonial
    {
        #region Métodos

        /// <summary>
        /// Obter o bem patrimonio por número e tipo de patrimonio vinculados a itens lista controle.
        /// </summary>        
        public Entity.BemPatrimonial ObterPatrimonioPorNumeroETipoVinculadoItemListaControle(Int64? numBem, Int64? numPatrimonio, Int64? tipoPatrimonio)
        {
            Entity.BemPatrimonial bemPatrimonio = null;

            try
            {
                if (numBem != null || numPatrimonio != null || tipoPatrimonio != null)
                {

                    StringBuilder str = new StringBuilder();

                    using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                    {
                        QueryCommandConfig query;

                        // Abrir conexão
                        ctx.Open();

                        // Query Principal
                        str.AppendLine(" SELECT A.NUM_BEM, ");
                        str.AppendLine("     NVL(TO_CHAR(A.NUM_PATRIMONIO),'SEM NÚMERO') || ' / ' || C.DSC_TIPO_PATRIMONIO PATRIMONIO, ");
                        str.AppendLine("     A.DSC_COMPLEMENTAR, ");
                        str.AppendLine("     A.CPL_LOCALIZACAO, ");
                        str.AppendLine("     A.NUM_PATRIMONIO ");
                        str.AppendLine(" FROM BEM_PATRIMONIAL A, ");
                        str.AppendLine("    ITENS_LISTA_CONTROLE B, ");
                        str.AppendLine("    TIPO_PATRIMONIO C ");
                        str.AppendLine(" WHERE A.COD_TIPO_BEM = B.COD_TIPO_BEM ");
                        str.AppendLine(" AND A.COD_TIPO_PATRIMONIO = C.COD_TIPO_PATRIMONIO ");

                        if (tipoPatrimonio != null)
                            str.AppendLine(string.Format(" AND A.COD_TIPO_PATRIMONIO = {0} ", tipoPatrimonio.Value));

                        if (numBem != null)
                            str.AppendLine(string.Format(" AND A.NUM_BEM = {0} ", numBem.Value));

                        if (numPatrimonio != null)
                            str.AppendLine(string.Format(" AND A.NUM_PATRIMONIO = {0} ", numPatrimonio.Value));

                        query = new QueryCommandConfig(str.ToString());

                        // Obter a lista de registros
                        ctx.ExecuteQuery(query);

                        // Preparar o retorno
                        while (ctx.Reader.Read())
                        {
                            bemPatrimonio = new Entity.BemPatrimonial();
                            
                            if (ctx.Reader["NUM_BEM"] != DBNull.Value)
                                bemPatrimonio.NumBem = Convert.ToInt64(ctx.Reader["NUM_BEM"]);

                            if (ctx.Reader["PATRIMONIO"] != DBNull.Value)
                                bemPatrimonio.DscTipoPatrimonio = ctx.Reader["PATRIMONIO"].ToString();

                            if (ctx.Reader["DSC_COMPLEMENTAR"] != DBNull.Value)
                                bemPatrimonio.DscModelo = ctx.Reader["DSC_COMPLEMENTAR"].ToString();

                            if (ctx.Reader["CPL_LOCALIZACAO"] != DBNull.Value)
                                bemPatrimonio.DscComplementoLocalizacao = ctx.Reader["CPL_LOCALIZACAO"].ToString();

                            if (ctx.Reader["NUM_PATRIMONIO"] != DBNull.Value)
                                bemPatrimonio.NumeroPatrimonio = Convert.ToInt64(ctx.Reader["NUM_PATRIMONIO"]);

                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bemPatrimonio;
        }

        /// <summary>
        /// Obter lista de bens patrimoniais.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.BemPatrimonial> ObterListaDeBensPatrimoniais(Int64? numPatrimonio, 
                                                                                                    string dscModelo, 
                                                                                                    Int64 codTipoPatrimonio,
                                                                                                    Int64 seqItemListaControle,
                                                                                                    int qtdRegistroPorPagina, 
                                                                                                    int paginaAtual, 
                                                                                                    out int totalRegistro)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.BemPatrimonial> listaRetorno = new List<Hcrp.CarroUrgenciaPsicoativo.Entity.BemPatrimonial>();
            Hcrp.CarroUrgenciaPsicoativo.Entity.BemPatrimonial bemPatrimonial;
            totalRegistro = 0;

            try
            {
                StringBuilder str = new StringBuilder();
                StringBuilder strTotalRegistro = new StringBuilder();
                StringBuilder strWhere = new StringBuilder();

                // Montar escopo de paginação.
                Int32 numeroRegistroPorPagina = qtdRegistroPorPagina;
                Int32 ultimoIndice = (numeroRegistroPorPagina * paginaAtual);
                Int32 primeiroIndice = (ultimoIndice - numeroRegistroPorPagina) + 1;

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Where da Query
                    strWhere.AppendLine(" WHERE A1.COD_TIPO_BEM = B.COD_TIPO_BEM ");

                    strWhere.AppendLine(" AND A1.COD_TIPO_PATRIMONIO = C.COD_TIPO_PATRIMONIO ");
                    strWhere.AppendLine(string.Format(" AND A1.COD_TIPO_PATRIMONIO = {0} ", codTipoPatrimonio));

                    strWhere.AppendLine(string.Format(" AND B.SEQ_LISTA_CONTROLE = {0} ", seqItemListaControle));
                    
                    if (!string.IsNullOrWhiteSpace(dscModelo))
                        strWhere.AppendLine(string.Format(" AND A1.DSC_COMPLEMENTAR LIKE '{0}%' ", dscModelo.ToUpper().Trim().Replace("'", "")));
                    
                    if (numPatrimonio != null)
                        strWhere.AppendLine(string.Format(" AND A1.NUM_PATRIMONIO = {0} ", numPatrimonio.Value));

                    // Query Principal
                    str.AppendLine("SELECT * ");
                    str.AppendLine(" FROM (SELECT A.*, ");
                    str.AppendLine("  ROWNUM AS RNUM FROM ");
                    str.AppendLine("   (     ");

                    str.AppendLine("     SELECT A1.NUM_BEM, ");
                    //str.AppendLine("            NVL(TO_CHAR(A1.NUM_PATRIMONIO),'SEM NÚMERO') || ' / ' || C.DSC_TIPO_PATRIMONIO PATRIMONIO, ");

                    str.AppendLine("            NVL(TO_CHAR(A1.NUM_PATRIMONIO),'SEM NÚMERO') PATRIMONIO, ");

                    str.AppendLine("            A1.DSC_COMPLEMENTAR, ");
                    str.AppendLine("            A1.CPL_LOCALIZACAO, ");
                    str.AppendLine("            A1.COD_TIPO_PATRIMONIO ");
                    str.AppendLine("         FROM BEM_PATRIMONIAL A1, ");
                    str.AppendLine("              ITENS_LISTA_CONTROLE B, ");
                    str.AppendLine("              TIPO_PATRIMONIO C ");
                    
                    str.AppendLine(strWhere.ToString());
                    str.AppendLine(" ORDER BY A1.NUM_BEM) A ");
                    str.AppendLine(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

                    strTotalRegistro.AppendLine(" SELECT COUNT(*) AS TOTAL FROM BEM_PATRIMONIAL A1, ");
                    strTotalRegistro.AppendLine("                           ITENS_LISTA_CONTROLE B, ");
                    strTotalRegistro.AppendLine("                           TIPO_PATRIMONIO C ");
                    strTotalRegistro.AppendLine(strWhere.ToString());

                    query = new QueryCommandConfig(str.ToString());
                    QueryCommandConfig queryCount = new QueryCommandConfig(strTotalRegistro.ToString());

                    // Veriricar contador
                    ctx.ExecuteQuery(queryCount);

                    while (ctx.Reader.Read())
                    {
                        totalRegistro = Convert.ToInt32(ctx.Reader["TOTAL"]);
                        break;
                    }

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    // Preparar o retorno
                    while (ctx.Reader.Read())
                    {
                        bemPatrimonial = new Entity.BemPatrimonial();

                        if (ctx.Reader["NUM_BEM"] != DBNull.Value)
                            bemPatrimonial.NumBem = Convert.ToInt64(ctx.Reader["NUM_BEM"]);

                        if (ctx.Reader["PATRIMONIO"] != DBNull.Value)
                            bemPatrimonial.DscModelo = ctx.Reader["PATRIMONIO"].ToString();

                        if (ctx.Reader["DSC_COMPLEMENTAR"] != DBNull.Value)
                            bemPatrimonial.DscComplementar = ctx.Reader["DSC_COMPLEMENTAR"].ToString();

                        if (ctx.Reader["CPL_LOCALIZACAO"] != DBNull.Value)
                            bemPatrimonial.DscComplementoLocalizacao = ctx.Reader["CPL_LOCALIZACAO"].ToString();

                        listaRetorno.Add(bemPatrimonial);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return listaRetorno;
        }

        public Entity.BemPatrimonial ObterPatrimonioPorNumeroETipo(Int64? numPatrimonio, Int64? tipoPatrimonio)
        {
            Entity.BemPatrimonial bemPatrimonio = null;

            try
            {
                if (numPatrimonio != null || tipoPatrimonio != null)
                {

                    StringBuilder str = new StringBuilder();

                    using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                    {
                        QueryCommandConfig query;

                        // Abrir conexão
                        ctx.Open();

                        // Query Principal
                        str.AppendLine(" SELECT * FROM BEM_PATRIMONIAL X ");

                        if (tipoPatrimonio != null)
                            str.AppendLine(string.Format(" WHERE X.COD_TIPO_PATRIMONIO = {0} ", tipoPatrimonio.Value));

                        if (numPatrimonio != null)
                            str.AppendLine(string.Format(" AND X.NUM_PATRIMONIO = {0} ", numPatrimonio.Value));

                        query = new QueryCommandConfig(str.ToString());

                        // Obter a lista de registros
                        ctx.ExecuteQuery(query);

                        // Preparar o retorno
                        while (ctx.Reader.Read())
                        {
                            bemPatrimonio = new Entity.BemPatrimonial();

                            if (ctx.Reader["NUM_BEM"] != DBNull.Value)
                                bemPatrimonio.NumBem = Convert.ToInt64(ctx.Reader["NUM_BEM"]);

                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bemPatrimonio;
        }

        #endregion
    }
}
