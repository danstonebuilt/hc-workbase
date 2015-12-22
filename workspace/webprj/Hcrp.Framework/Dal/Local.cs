using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class Local : Hcrp.Framework.Classes.Local
    {
        /// <summary>
        /// Obter lista de grupo de centro de custo
        /// </summary>
        /// <param name="paginaAtual"></param>
        /// <param name="totalRegistro"></param>
        /// <param name="filtroIdLocal"></param>
        /// <param name="filtroNomeLocal"></param>
        /// <returns></returns>
        public List<Hcrp.Framework.Classes.Local> ObterListaDeGrupoDeCentroDeCusto(int paginaAtual, out int totalRegistro, string filtroIdLocal = "", string filtroNomeLocal = "")
        {
            List<Hcrp.Framework.Classes.Local> _listaDeRetorno = new List<Hcrp.Framework.Classes.Local>();
            Hcrp.Framework.Classes.Local _local = null;
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

                    strWhere.AppendLine(" WHERE IDF_ATIVIDADE = 'A' AND COD_INST_SISTEMA = " + codInstSistema);

                    if (!string.IsNullOrWhiteSpace(filtroIdLocal) || !string.IsNullOrWhiteSpace(filtroNomeLocal))
                    {
                        if (strWhere.Length == 0)
                        {

                        }

                        if (!string.IsNullOrWhiteSpace(filtroIdLocal))
                        {
                            strWhere.AppendLine(string.Format(" AND COD_LOCAL LIKE '%{0}%' ", filtroIdLocal.ToUpper()));
                        }

                        if (!string.IsNullOrWhiteSpace(filtroNomeLocal))
                        {
                            strWhere.AppendLine(string.Format(" AND NOM_LOCAL LIKE '%{0}%' ", filtroNomeLocal.ToUpper()));
                        }
                    }

                    str.AppendLine("SELECT * FROM (SELECT A.*, ROWNUM AS RNUM FROM (SELECT DISTINCT ");
                    str.AppendLine("  COD_LOCAL, ");
                    str.AppendLine("  NOM_LOCAL ");
                    str.AppendLine("FROM LOCAL ");
                    if (strWhere.Length > 0)
                        str.AppendLine(strWhere.ToString());
                    str.AppendLine("ORDER BY NOM_LOCAL ASC) A ");
                    str.Append(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

                    // Montar query para pegar o total de registros.
                    strTotalRegistro.Append("SELECT COUNT(*) TOTAL ");
                    strTotalRegistro.AppendLine("FROM LOCAL ");
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
                        _local = new Hcrp.Framework.Classes.Local();

                        if (dr["COD_LOCAL"] != DBNull.Value)
                            _local.IdLocal = dr["COD_LOCAL"].ToString();

                        if (dr["NOM_LOCAL"] != DBNull.Value)
                            _local.NomeLocal = dr["NOM_LOCAL"].ToString();

                        _listaDeRetorno.Add(_local);

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
