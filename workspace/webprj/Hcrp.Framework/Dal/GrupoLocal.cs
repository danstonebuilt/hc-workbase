using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class GrupoLocal : Hcrp.Framework.Classes.GrupoLocal
    {
        /// <summary>
        /// Obter a lista de local por grupo
        /// </summary>
        /// <param name="idGrupo"></param>
        /// <param name="paginaAtual"></param>
        /// <param name="totalRegistro"></param>
        /// <returns></returns>
        public List<Hcrp.Framework.Classes.GrupoLocal> ObterListaDeLocalPorCentroDeCusto(string idCentroDeCusto, int paginaAtual, out int totalRegistro)
        {
            List<Hcrp.Framework.Classes.GrupoLocal> _listaDeRetorno = new List<Hcrp.Framework.Classes.GrupoLocal>();
            Hcrp.Framework.Classes.GrupoLocal _grupoLocal = null;
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
                    string concatenaOAnd = "";
                    string valorDoParametro = "";

                    int codInstSistema = new Hcrp.Framework.Classes.ConfiguracaoSistema().CodInstituicaoSistema;

                    str.AppendLine(" SELECT * FROM (SELECT A.*, ROWNUM AS RNUM FROM (SELECT DISTINCT ");
                    str.AppendLine("   L.COD_LOCAL, CCL.SEQ_CENTRO_CUSTO_LOCAL, ");
                    str.AppendLine("   L.NOM_LOCAL ");
                    str.AppendLine(" FROM CENTRO_CUSTO_LOCAL CCL ");
                    str.AppendLine(" INNER JOIN LOCAL L ON L.COD_LOCAL = CCL.COD_LOCAL ");
                    str.AppendLine(string.Format(" WHERE L.IDF_ATIVIDADE = 'A' AND COD_INST_SISTEMA = " + codInstSistema + " AND CCL.COD_CENCUSTO = '{0}' ", idCentroDeCusto));
                    if (strWhere.Length > 0)
                        str.AppendLine(strWhere.ToString());
                    str.AppendLine("ORDER BY NOM_LOCAL ASC) A ");
                    str.Append(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

                    // Montar query para pegar o total de registros.
                    strTotalRegistro.Append("SELECT COUNT(*) TOTAL ");
                    strTotalRegistro.AppendLine(" FROM CENTRO_CUSTO_LOCAL CCL ");
                    strTotalRegistro.AppendLine(" INNER JOIN LOCAL L ON L.COD_LOCAL = CCL.COD_LOCAL ");
                    strTotalRegistro.AppendLine(string.Format(" WHERE L.IDF_ATIVIDADE = 'A' AND COD_INST_SISTEMA = " + codInstSistema + " AND CCL.COD_CENCUSTO = '{0}' ", idCentroDeCusto));

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
                        _grupoLocal = new Hcrp.Framework.Classes.GrupoLocal();

                        if (dr["SEQ_CENTRO_CUSTO_LOCAL"] != DBNull.Value)
                            _grupoLocal.IdCentroCustoLocal = Convert.ToInt64(dr["SEQ_CENTRO_CUSTO_LOCAL"]);

                        if (dr["COD_LOCAL"] != DBNull.Value)
                            _grupoLocal.Local.IdLocal = dr["COD_LOCAL"].ToString();

                        if (dr["NOM_LOCAL"] != DBNull.Value)
                            _grupoLocal.Local.NomeLocal = dr["NOM_LOCAL"].ToString();

                        _listaDeRetorno.Add(_grupoLocal);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaDeRetorno;
        }

        public bool JahExisteOGrupoLocal(Hcrp.Framework.Classes.GrupoLocal _grupoLocal)
        {

            bool retorno = false;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    // Cria a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT COUNT(*) TOTAL FROM CENTRO_CUSTO_LOCAL WHERE COD_LOCAL = '" + _grupoLocal.Local.IdLocal.Trim() + "' AND COD_CENCUSTO = '" + _grupoLocal.IdCentroLocal.Trim() + "'");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        if (dr["TOTAL"] != DBNull.Value && Convert.ToInt32(dr["TOTAL"]) > 0)
                            retorno = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return retorno;
        }

        /// <summary>
        /// Adicionar relação entre grupo e centro de custo
        /// </summary>
        /// <param name="_centro"></param>
        public void Adicionar(Hcrp.Framework.Classes.GrupoLocal _grupoLocal)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abre conexão
                    ctx.Open();

                    // Configurar comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("CENTRO_CUSTO_LOCAL");

                    // Adicionar parametros
                    //comando.Params["SEQ_CENTRO_CUSTO_LOCAL"] = 1;
                    comando.Params["COD_LOCAL"] = _grupoLocal.Local.IdLocal;
                    comando.Params["COD_CENCUSTO"] = _grupoLocal.IdCentroLocal;

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
        /// Remove relacionamento
        /// </summary>
        /// <param name="_centro"></param>
        public void Remover(long idRelacionamento)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abre conexão
                    ctx.Open();

                    // Configurar comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("CENTRO_CUSTO_LOCAL");

                    // Adicionar parametros
                    comando.Params["SEQ_CENTRO_CUSTO_LOCAL"] = idRelacionamento;

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
