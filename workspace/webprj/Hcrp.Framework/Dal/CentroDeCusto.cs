using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class CentroDeCusto : Hcrp.Framework.Classes.CentroDeCusto
    {
        public List<Hcrp.Framework.Classes.CentroDeCusto> ObterListaDeCentroRelacionadosAoGrupo(Int32 idGrupo, int paginaAtual, out int totalRegistro, string filtroId, string filtroNome)
        {
            List<Hcrp.Framework.Classes.CentroDeCusto> _listaDeRetorno = new List<Hcrp.Framework.Classes.CentroDeCusto>();
            Hcrp.Framework.Classes.CentroDeCusto _centro = null;
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
                    Int32 numeroRegistroPorPagina = Hcrp.Framework.Infra.Util.Parametrizacao.Instancia().QuantidadeRegistroPagina;
                    Int32 ultimoIndice = (numeroRegistroPorPagina * paginaAtual);
                    Int32 primeiroIndice = (ultimoIndice - numeroRegistroPorPagina) + 1;

                    strWhere.AppendLine(string.Format(" WHERE IDF_ATIVO = 'S' AND CC.COD_GRUPO = {0} ", idGrupo));

                    if (!string.IsNullOrWhiteSpace(filtroId) || !string.IsNullOrWhiteSpace(filtroNome))
                    {

                        if (!string.IsNullOrWhiteSpace(filtroId))
                        {
                            strWhere.AppendLine(string.Format(" AND COD_CENCUSTO LIKE '%{0}%' ", filtroId.ToUpper()));
                        }

                        if (!string.IsNullOrWhiteSpace(filtroNome))
                        {
                            strWhere.AppendLine(string.Format(" AND NOM_CENCUSTO LIKE '%{0}%' ", filtroNome.ToUpper()));
                        }
                    }

                    str.AppendLine(" SELECT * FROM (SELECT A.*, ROWNUM AS RNUM FROM (SELECT DISTINCT ");
                    str.AppendLine("  CC.COD_CENCUSTO, ");
                    str.AppendLine("  CC.NOM_CENCUSTO, CC.COD_GRUPO ");
                    str.AppendLine("FROM CENTRO_CUSTO CC ");
                    if (strWhere.Length > 0)
                        str.AppendLine(strWhere.ToString());
                    str.AppendLine(" ORDER BY NOM_CENCUSTO ASC) A ");
                    str.Append(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

                    // Montar query para pegar o total de registros.
                    strTotalRegistro.Append("SELECT COUNT(*) TOTAL ");
                    strTotalRegistro.AppendLine(" FROM CENTRO_CUSTO CC ");
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
                        _centro = new Hcrp.Framework.Classes.CentroDeCusto();

                        if (dr["COD_CENCUSTO"] != DBNull.Value)
                            _centro.Codigo = dr["COD_CENCUSTO"].ToString();

                        if (dr["NOM_CENCUSTO"] != DBNull.Value)
                            _centro.Nome = dr["NOM_CENCUSTO"].ToString();

                        if (dr["COD_GRUPO"] != DBNull.Value)
                            _centro.CodigoGrupo = Convert.ToInt32(dr["COD_GRUPO"]);

                        _listaDeRetorno.Add(_centro);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaDeRetorno;
        }

        public Hcrp.Framework.Classes.CentroDeCusto BuscaCentroCustoCodigo(string codCC)
        {
            Hcrp.Framework.Classes.CentroDeCusto _cc = new Hcrp.Framework.Classes.CentroDeCusto();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    // Cria a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine("SELECT C.COD_CENCUSTO,");
                    str.AppendLine("       C.NOM_CENCUSTO, C.COD_GRUPO ");
                    str.AppendLine("FROM CENTRO_CUSTO C   ");
                    str.AppendLine(" WHERE C.COD_CENCUSTO = :COD_CENCUSTO");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["COD_CENCUSTO"] = codCC;
                    // Abre conexão
                    ctx.Open();

                    // Veriricar contador
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        if (dr["COD_CENCUSTO"] != DBNull.Value)
                            _cc.Codigo = dr["COD_CENCUSTO"].ToString();

                        if (dr["NOM_CENCUSTO"] != DBNull.Value)
                            _cc.Nome = dr["NOM_CENCUSTO"].ToString();

                        if (dr["COD_GRUPO"] != DBNull.Value)
                            _cc.CodigoGrupo = Convert.ToInt32(dr["COD_GRUPO"]);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _cc;
        }

        public List<Hcrp.Framework.Classes.CentroDeCusto> ObterListaDeCentroRelacionadosAoGrupoSICH(Int32 idGrupo, int paginaAtual, out int totalRegistro, string filtroId, string filtroNome)
        {
            List<Hcrp.Framework.Classes.CentroDeCusto> _listaDeRetorno = new List<Hcrp.Framework.Classes.CentroDeCusto>();
            Hcrp.Framework.Classes.CentroDeCusto _centro = null;
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
                    Int32 numeroRegistroPorPagina = Hcrp.Framework.Infra.Util.Parametrizacao.Instancia().QuantidadeRegistroPagina;
                    Int32 ultimoIndice = (numeroRegistroPorPagina * paginaAtual);
                    Int32 primeiroIndice = (ultimoIndice - numeroRegistroPorPagina) + 1;

                    strWhere.AppendLine(string.Format(" WHERE IDF_ATIVO = 'S' AND CC.COD_GRUPO_SICH = {0} ", idGrupo));

                    if (!string.IsNullOrWhiteSpace(filtroId) || !string.IsNullOrWhiteSpace(filtroNome))
                    {

                        if (!string.IsNullOrWhiteSpace(filtroId))
                        {
                            strWhere.AppendLine(string.Format(" AND COD_CENCUSTO LIKE '%{0}%' ", filtroId.ToUpper()));
                        }

                        if (!string.IsNullOrWhiteSpace(filtroNome))
                        {
                            strWhere.AppendLine(string.Format(" AND NOM_CENCUSTO LIKE '%{0}%' ", filtroNome.ToUpper()));
                        }
                    }

                    str.AppendLine(" SELECT * FROM (SELECT A.*, ROWNUM AS RNUM FROM (SELECT DISTINCT ");
                    str.AppendLine("  CC.COD_CENCUSTO, ");
                    str.AppendLine("  CC.NOM_CENCUSTO, CC.COD_GRUPO_SICH ");
                    str.AppendLine("FROM CENTRO_CUSTO CC ");
                    if (strWhere.Length > 0)
                        str.AppendLine(strWhere.ToString());
                    str.AppendLine(" ORDER BY NOM_CENCUSTO ASC) A ");
                    str.Append(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

                    // Montar query para pegar o total de registros.
                    strTotalRegistro.Append("SELECT COUNT(*) TOTAL ");
                    strTotalRegistro.AppendLine(" FROM CENTRO_CUSTO CC ");
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
                        _centro = new Hcrp.Framework.Classes.CentroDeCusto();

                        if (dr["COD_CENCUSTO"] != DBNull.Value)
                            _centro.Codigo = dr["COD_CENCUSTO"].ToString();

                        if (dr["NOM_CENCUSTO"] != DBNull.Value)
                            _centro.Nome = dr["NOM_CENCUSTO"].ToString();

                        if (dr["COD_GRUPO_SICH"] != DBNull.Value)
                            _centro.CodigoGrupo = Convert.ToInt32(dr["COD_GRUPO_SICH"]);

                        _listaDeRetorno.Add(_centro);

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
        /// Obter lista de centro de custo que ainda não possuem relacionamento
        /// </summary>
        /// <param name="paginaAtual"></param>
        /// <param name="totalRegistro"></param>
        /// <returns></returns>
        public List<Hcrp.Framework.Classes.CentroDeCusto> ObterListaDeCentroSemRelacionamento(int paginaAtual, string ordenacao, out int totalRegistro, string filtroId, string filtroNome)
        {
            List<Hcrp.Framework.Classes.CentroDeCusto> _listaDeRetorno = new List<Hcrp.Framework.Classes.CentroDeCusto>();
            Hcrp.Framework.Classes.CentroDeCusto _centro = null;
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
                    Int32 numeroRegistroPorPagina = Hcrp.Framework.Infra.Util.Parametrizacao.Instancia().QuantidadeRegistroPagina;
                    Int32 ultimoIndice = (numeroRegistroPorPagina * paginaAtual);
                    Int32 primeiroIndice = (ultimoIndice - numeroRegistroPorPagina) + 1;

                    strWhere.AppendLine(" WHERE IDF_ATIVO = 'S' AND COD_INSTITUTO IN (1,2) AND CC.COD_GRUPO_SICH IS NULL ");

                    if (!string.IsNullOrWhiteSpace(filtroId) || !string.IsNullOrWhiteSpace(filtroNome))
                    {

                        if (!string.IsNullOrWhiteSpace(filtroId))
                        {
                            strWhere.AppendLine(string.Format(" AND COD_CENCUSTO LIKE '%{0}%' ", filtroId.ToUpper()));
                        }

                        if (!string.IsNullOrWhiteSpace(filtroNome))
                        {
                            strWhere.AppendLine(string.Format(" AND NOM_CENCUSTO LIKE '%{0}%' ", filtroNome.ToUpper()));
                        }
                    }

                    str.AppendLine(" SELECT * FROM (SELECT A.*, ROWNUM AS RNUM FROM (SELECT DISTINCT ");
                    str.AppendLine("  CC.COD_CENCUSTO, ");
                    str.AppendLine("  CC.NOM_CENCUSTO, CC.COD_GRUPO_SICH ");
                    str.AppendLine("FROM CENTRO_CUSTO CC  ");
                    if (strWhere.Length > 0)
                        str.AppendLine(strWhere.ToString());
                    str.AppendLine(string.Format(" ORDER BY {0}) A ", ordenacao));
                    str.Append(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

                    // Montar query para pegar o total de registros.
                    strTotalRegistro.Append("SELECT COUNT(*) TOTAL ");
                    strTotalRegistro.AppendLine("FROM CENTRO_CUSTO CC ");
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
                        _centro = new Hcrp.Framework.Classes.CentroDeCusto();

                        if (dr["COD_CENCUSTO"] != DBNull.Value)
                            _centro.Codigo = dr["COD_CENCUSTO"].ToString();

                        if (dr["NOM_CENCUSTO"] != DBNull.Value)
                            _centro.Nome = dr["NOM_CENCUSTO"].ToString();

                        if (dr["COD_GRUPO_SICH"] != DBNull.Value)
                            _centro.CodigoGrupo = Convert.ToInt32(dr["COD_GRUPO_SICH"]);

                        _listaDeRetorno.Add(_centro);

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
        /// Obter centro de custo por id
        /// </summary>
        /// <param name="idCentroDeCusto"></param>
        /// <returns></returns>
        public Hcrp.Framework.Classes.CentroDeCusto ObterCentroDeCustoComOId(string idCentroDeCusto)
        {

            Hcrp.Framework.Classes.CentroDeCusto _centroDeCusto = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT ");
                    str.AppendLine("    COD_CENCUSTO, ");
                    str.AppendLine("    NOM_CENCUSTO ");
                    str.AppendLine(" FROM CENTRO_CUSTO WHERE COD_CENCUSTO = '" + idCentroDeCusto + "'");

                    // Abrir conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(str.ToString());

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _centroDeCusto = new Hcrp.Framework.Classes.CentroDeCusto();

                        if (dr["COD_CENCUSTO"] != DBNull.Value)
                            _centroDeCusto.Codigo = dr["COD_CENCUSTO"].ToString();

                        if (dr["NOM_CENCUSTO"] != DBNull.Value)
                            _centroDeCusto.Nome = dr["NOM_CENCUSTO"].ToString();

                        break;

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _centroDeCusto;
        }

        /// <summary>
        /// Adicionar relação entre grupo e centro de custo
        /// </summary>
        /// <param name="_centro"></param>
        public void Adicionar(Hcrp.Framework.Classes.CentroDeCusto _centro)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abre conexão
                    ctx.Open();

                    // Configurar comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("CENTRO_CUSTO");

                    // Adicionar parametros
                    comando.FilterParams["COD_CENCUSTO"] = _centro.Codigo;
                    comando.Params["COD_GRUPO_SICH"] = _centro.CodigoGrupo;

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
        /// Remove relacionamento
        /// </summary>
        /// <param name="_centro"></param>
        public void Remover(Hcrp.Framework.Classes.CentroDeCusto _centro)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abre conexão
                    ctx.Open();

                    // Configurar comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("CENTRO_CUSTO");

                    // Adicionar parametros
                    comando.FilterParams["COD_CENCUSTO"] = _centro.Codigo;
                    comando.Params["COD_GRUPO_SICH"] = DBNull.Value;

                    // Executar o comando
                    ctx.ExecuteUpdate(comando);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
