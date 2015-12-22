using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class GrupoDeCentroDeCusto : Hcrp.Framework.Classes.GrupoDeCentroDeCusto 
    {
        public List<Hcrp.Framework.Classes.GrupoDeCentroDeCusto> ObterListaDeGrupoDeCentroDeCusto(int paginaAtual, out int totalRegistro, string filtroIdGrupo, string filtroNomeGrupo, string filtroIdTipoGrupo)
        {
            List<Hcrp.Framework.Classes.GrupoDeCentroDeCusto> _listaDeRetorno = new List<Hcrp.Framework.Classes.GrupoDeCentroDeCusto>();
            Hcrp.Framework.Classes.GrupoDeCentroDeCusto _grupo = null;
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
                    string concatenaOAnd = "";
                    string valorDoParametro = "";


                    int codInstSistema = new Hcrp.Framework.Classes.ConfiguracaoSistema().CodInstituicaoSistema;

                    strWhere.AppendLine(" WHERE COD_INST_SISTEMA = " + codInstSistema);

                    if (!string.IsNullOrWhiteSpace(filtroIdGrupo) || !string.IsNullOrWhiteSpace(filtroNomeGrupo) || !string.IsNullOrWhiteSpace(filtroIdTipoGrupo))
                    {
                        if (strWhere.Length == 0)
                        {

                        }

                        if (!string.IsNullOrWhiteSpace(filtroIdGrupo))
                        {
                            strWhere.AppendLine(string.Format(" AND COD_GRUPO LIKE '%{0}%' ", filtroIdGrupo.ToUpper()));
                        }

                        if (!string.IsNullOrWhiteSpace(filtroNomeGrupo))
                        {
                            strWhere.AppendLine(string.Format(" AND DSC_GRUPO LIKE '%{0}%' ", filtroNomeGrupo.ToUpper()));
                        }

                        if (!string.IsNullOrWhiteSpace(filtroIdTipoGrupo) && !filtroIdTipoGrupo.Equals("0"))
                        {
                            strWhere.AppendLine(string.Format(" AND IDF_TIPO_GRUPO = {0} ", filtroIdTipoGrupo.ToUpper()));
                        }
                    }

                    str.AppendLine("SELECT * FROM (SELECT A.*, ROWNUM AS RNUM FROM (SELECT DISTINCT ");
                    str.AppendLine("  G.COD_GRUPO,");
                    str.AppendLine("  G.DSC_GRUPO, G.IDF_TIPO_GRUPO, ");
                    str.AppendLine("  CASE WHEN G.IDF_TIPO_GRUPO = 1 THEN 'APOIO' ");
                    str.AppendLine("       WHEN G.IDF_TIPO_GRUPO = 2 THEN 'AUXILIAR'");
                    str.AppendLine("       WHEN G.IDF_TIPO_GRUPO = 3 THEN 'ESPECIAL'");
                    str.AppendLine("       WHEN G.IDF_TIPO_GRUPO = 4 THEN 'PRODUTIVO'");
                    str.AppendLine("       WHEN G.IDF_TIPO_GRUPO = 5 THEN 'ADMINISTRATIVO'");
                    str.AppendLine("       ELSE ''");
                    str.AppendLine("  END DSC_TIPO_GRUPO ");
                    str.AppendLine("FROM GRUPO_CENCUSTO G ");
                    if (strWhere.Length > 0)
                        str.AppendLine(strWhere.ToString());
                    str.AppendLine("ORDER BY DSC_GRUPO ASC) A ");
                    str.Append(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

                    // Montar query para pegar o total de registros.
                    strTotalRegistro.Append("SELECT COUNT(*) TOTAL ");
                    strTotalRegistro.AppendLine("FROM GRUPO_CENCUSTO ");
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
                        _grupo = new Hcrp.Framework.Classes.GrupoDeCentroDeCusto();

                        if (dr["COD_GRUPO"] != DBNull.Value)
                            _grupo.Codigo = Convert.ToInt32(dr["COD_GRUPO"]);

                        if (dr["DSC_GRUPO"] != DBNull.Value)
                            _grupo.Nome = dr["DSC_GRUPO"].ToString();

                        if (dr["IDF_TIPO_GRUPO"] != DBNull.Value)
                            _grupo.IdfTipoGrupo = dr["IDF_TIPO_GRUPO"].ToString();

                        if (dr["DSC_TIPO_GRUPO"] != DBNull.Value)
                            _grupo.DescricaoTipoGrupo = dr["DSC_TIPO_GRUPO"].ToString();

                        _listaDeRetorno.Add(_grupo);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaDeRetorno;

        }

        public Hcrp.Framework.Classes.GrupoDeCentroDeCusto BuscaGrupoCentroCustoCodigo(int codGrupoCC)
        {
            Hcrp.Framework.Classes.GrupoDeCentroDeCusto _grupo = new Hcrp.Framework.Classes.GrupoDeCentroDeCusto();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    // Cria a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine("SELECT G.COD_GRUPO,");
                    str.AppendLine("  G.DSC_GRUPO, G.IDF_TIPO_GRUPO, ");
                    str.AppendLine("  CASE WHEN G.IDF_TIPO_GRUPO = 1 THEN 'APOIO' ");
                    str.AppendLine("       WHEN G.IDF_TIPO_GRUPO = 2 THEN 'AUXILIAR'");
                    str.AppendLine("       WHEN G.IDF_TIPO_GRUPO = 3 THEN 'ESPECIAL'");
                    str.AppendLine("       WHEN G.IDF_TIPO_GRUPO = 4 THEN 'PRODUTIVO'");
                    str.AppendLine("       WHEN G.IDF_TIPO_GRUPO = 5 THEN 'ADMINISTRATIVO'");
                    str.AppendLine("       ELSE ''");
                    str.AppendLine("  END DSC_TIPO_GRUPO ");
                    str.AppendLine("FROM GRUPO_CENCUSTO G ");
                    str.Append(" WHERE G.COD_GRUPO = :COD_GRUPO");
                    
                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["COD_GRUPO"] = codGrupoCC;
                    // Abre conexão
                    ctx.Open();

                    // Veriricar contador
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        if (dr["COD_GRUPO"] != DBNull.Value)
                            _grupo.Codigo = Convert.ToInt32(dr["COD_GRUPO"]);

                        if (dr["DSC_GRUPO"] != DBNull.Value)
                            _grupo.Nome = dr["DSC_GRUPO"].ToString();

                        if (dr["IDF_TIPO_GRUPO"] != DBNull.Value)
                            _grupo.IdfTipoGrupo = dr["IDF_TIPO_GRUPO"].ToString();

                        if (dr["DSC_TIPO_GRUPO"] != DBNull.Value)
                            _grupo.DescricaoTipoGrupo = dr["DSC_TIPO_GRUPO"].ToString();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _grupo;
        }

        public List<Hcrp.Framework.Classes.GrupoDeCentroDeCusto> ObterListaDeGrupoSICHDeCentroDeCusto(int paginaAtual, out int totalRegistro, string filtroIdGrupo, string filtroNomeGrupo, string filtroIdTipoGrupo)
        {
            List<Hcrp.Framework.Classes.GrupoDeCentroDeCusto> _listaDeRetorno = new List<Hcrp.Framework.Classes.GrupoDeCentroDeCusto>();
            Hcrp.Framework.Classes.GrupoDeCentroDeCusto _grupo = null;
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
                    Int32 numeroRegistroPorPagina = Hcrp.Framework.Infra.Util.Parametrizacao.Instancia().QuantidadeRegistroPagina; /*Ver*/
                    Int32 ultimoIndice = (numeroRegistroPorPagina * paginaAtual);
                    Int32 primeiroIndice = (ultimoIndice - numeroRegistroPorPagina) + 1;
                    string concatenaOAnd = "";
                    string valorDoParametro = "";


                    int codInstSistema = new Hcrp.Framework.Classes.ConfiguracaoSistema().CodInstituicaoSistema;

                    strWhere.AppendLine(" WHERE COD_INST_SISTEMA = " + codInstSistema);

                    if (!string.IsNullOrWhiteSpace(filtroIdGrupo) || !string.IsNullOrWhiteSpace(filtroNomeGrupo) || !string.IsNullOrWhiteSpace(filtroIdTipoGrupo))
                    {
                        if (strWhere.Length == 0)
                        {

                        }

                        if (!string.IsNullOrWhiteSpace(filtroIdGrupo))
                        {
                            strWhere.AppendLine(string.Format(" AND COD_GRUPO_SICH LIKE '%{0}%' ", filtroIdGrupo.ToUpper()));
                        }

                        if (!string.IsNullOrWhiteSpace(filtroNomeGrupo))
                        {
                            strWhere.AppendLine(string.Format(" AND DSC_GRUPO LIKE '%{0}%' ", filtroNomeGrupo.ToUpper()));
                        }

                        if (!string.IsNullOrWhiteSpace(filtroIdTipoGrupo) && !filtroIdTipoGrupo.Equals("0"))
                        {
                            strWhere.AppendLine(string.Format(" AND IDF_TIPO_GRUPO = {0} ", filtroIdTipoGrupo.ToUpper()));
                        }
                    }

                    str.AppendLine("SELECT * FROM (SELECT A.*, ROWNUM AS RNUM FROM (SELECT DISTINCT ");
                    str.AppendLine("  G.COD_GRUPO_SICH,");
                    str.AppendLine("  G.DSC_GRUPO, G.IDF_TIPO_GRUPO, ");
                    str.AppendLine("  CASE WHEN G.IDF_TIPO_GRUPO = 1 THEN 'APOIO' ");
                    str.AppendLine("       WHEN G.IDF_TIPO_GRUPO = 2 THEN 'AUXILIAR'");
                    str.AppendLine("       WHEN G.IDF_TIPO_GRUPO = 3 THEN 'ESPECIAL'");
                    str.AppendLine("       WHEN G.IDF_TIPO_GRUPO = 4 THEN 'PRODUTIVO'");
                    str.AppendLine("       WHEN G.IDF_TIPO_GRUPO = 5 THEN 'ADMINISTRATIVO'");
                    str.AppendLine("       ELSE ''");
                    str.AppendLine("  END DSC_TIPO_GRUPO ");
                    str.AppendLine("FROM GRUPO_CENCUSTO_SICH G ");
                    if (strWhere.Length > 0)
                        str.AppendLine(strWhere.ToString());
                    str.AppendLine("ORDER BY DSC_GRUPO ASC) A ");
                    str.Append(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

                    // Montar query para pegar o total de registros.
                    strTotalRegistro.Append("SELECT COUNT(*) TOTAL ");
                    strTotalRegistro.AppendLine("FROM GRUPO_CENCUSTO_SICH ");
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
                        _grupo = new Hcrp.Framework.Classes.GrupoDeCentroDeCusto();

                        if (dr["COD_GRUPO_SICH"] != DBNull.Value)
                            _grupo.Codigo = Convert.ToInt32(dr["COD_GRUPO_SICH"]);

                        if (dr["DSC_GRUPO"] != DBNull.Value)
                            _grupo.Nome = dr["DSC_GRUPO"].ToString();

                        if (dr["IDF_TIPO_GRUPO"] != DBNull.Value)
                            _grupo.IdfTipoGrupo = dr["IDF_TIPO_GRUPO"].ToString();

                        if (dr["DSC_TIPO_GRUPO"] != DBNull.Value)
                            _grupo.DescricaoTipoGrupo = dr["DSC_TIPO_GRUPO"].ToString();

                        _listaDeRetorno.Add(_grupo);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaDeRetorno;
        }

        /// <summary>
        /// Obter grupo por id
        /// </summary>
        /// <param name="idGrupo"></param>
        /// <returns></returns>
        public Hcrp.Framework.Classes.GrupoDeCentroDeCusto ObterGrupoComOId(int idGrupo)
        {

            Hcrp.Framework.Classes.GrupoDeCentroDeCusto _grupo = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    StringBuilder str = new StringBuilder();


                    str.AppendLine(" SELECT ");
                    str.AppendLine("    COD_GRUPO_SICH, ");
                    str.AppendLine("    DSC_GRUPO, ");
                    str.AppendLine("    IDF_TIPO_GRUPO, ");
                    str.AppendLine("    ORD_IMPRESSAO, ");
                    str.AppendLine("  CASE WHEN IDF_TIPO_GRUPO = 1 THEN 'APOIO' ");
                    str.AppendLine("       WHEN IDF_TIPO_GRUPO = 2 THEN 'AUXILIAR'");
                    str.AppendLine("       WHEN IDF_TIPO_GRUPO = 3 THEN 'ESPECIAL'");
                    str.AppendLine("       WHEN IDF_TIPO_GRUPO = 4 THEN 'PRODUTIVO'");
                    str.AppendLine("       WHEN IDF_TIPO_GRUPO = 5 THEN 'ADMINISTRATIVO'");
                    str.AppendLine("       ELSE ''");
                    str.AppendLine("  END DSC_TIPO_GRUPO ");
                    str.AppendLine(" FROM GRUPO_CENCUSTO_SICH WHERE COD_GRUPO_SICH = " + idGrupo);


                    // Abrir conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(str.ToString());

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _grupo = new Hcrp.Framework.Classes.GrupoDeCentroDeCusto();

                        if (dr["COD_GRUPO_SICH"] != DBNull.Value)
                            _grupo.Codigo = Convert.ToInt32(dr["COD_GRUPO_SICH"]);

                        if (dr["DSC_GRUPO"] != DBNull.Value)
                            _grupo.Nome = dr["DSC_GRUPO"].ToString();

                        if (dr["IDF_TIPO_GRUPO"] != DBNull.Value)
                            _grupo.IdfTipoGrupo = dr["IDF_TIPO_GRUPO"].ToString();

                        if (dr["ORD_IMPRESSAO"] != DBNull.Value)
                            _grupo.OrdenacaoImpressao = dr["ORD_IMPRESSAO"].ToString();

                        if (dr["DSC_TIPO_GRUPO"] != DBNull.Value)
                            _grupo.DescricaoTipoGrupo = dr["DSC_TIPO_GRUPO"].ToString();

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _grupo;
        }

        /// <summary>
        /// Adicionar
        /// </summary>
        /// <param name="_programa"></param>
        public void Adicionar(Hcrp.Framework.Classes.GrupoDeCentroDeCusto _grupo)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abre conexão
                    ctx.Open();

                    // Configurar comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("GRUPO_CENCUSTO_SICH");

                    // Adicionar parametros
                    comando.Params["DSC_GRUPO"] = _grupo.Nome.ToUpper();
                    comando.Params["ORD_IMPRESSAO"] = _grupo.OrdenacaoImpressao.ToUpper();
                    comando.Params["IDF_TIPO_GRUPO"] = _grupo.IdfTipoGrupo;
                    comando.Params["COD_INST_SISTEMA"] = new Hcrp.Framework.Classes.ConfiguracaoSistema().CodInstituicaoSistema;

                    // Executar o comando
                    ctx.ExecuteInsert(comando);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Atualizar
        /// </summary>
        /// <param name="_grupo"></param>
        public void Atualizar(Hcrp.Framework.Classes.GrupoDeCentroDeCusto _grupo)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abre conexão
                    ctx.Open();

                    // Configurar comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("GRUPO_CENCUSTO_SICH");

                    // Adicionar parametros
                    comando.FilterParams["COD_GRUPO_SICH"] = _grupo.Codigo;

                    if (!string.IsNullOrWhiteSpace(_grupo.Nome))
                        comando.Params["DSC_GRUPO"] = _grupo.Nome.ToUpper();
                    else
                        comando.Params["DSC_GRUPO"] = DBNull.Value;

                    if (!string.IsNullOrWhiteSpace(_grupo.OrdenacaoImpressao))
                        comando.Params["ORD_IMPRESSAO"] = _grupo.OrdenacaoImpressao.ToUpper();
                    else
                        comando.Params["ORD_IMPRESSAO"] = DBNull.Value;

                    if (!string.IsNullOrWhiteSpace(_grupo.IdfTipoGrupo))
                        comando.Params["IDF_TIPO_GRUPO"] = _grupo.IdfTipoGrupo;
                    else
                        comando.Params["IDF_TIPO_GRUPO"] = DBNull.Value;

                    comando.Params["COD_INST_SISTEMA"] = new Hcrp.Framework.Classes.ConfiguracaoSistema().CodInstituicaoSistema;

                    // Executar o comando
                    ctx.ExecuteUpdate(comando);

                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Remover grupo
        /// </summary>
        /// <param name="_grupo"></param>
        public void Remover(Hcrp.Framework.Classes.GrupoDeCentroDeCusto _grupo)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abre conexão
                    ctx.Open();

                    // Configurar comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("GRUPO_CENCUSTO_SICH");

                    // Adicionar parametros
                    comando.Params["COD_GRUPO_SICH"] = _grupo.Codigo;

                    // Executar o comando
                    ctx.ExecuteDelete(comando);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
