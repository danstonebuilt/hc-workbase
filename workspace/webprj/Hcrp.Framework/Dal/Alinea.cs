using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class Alinea : Hcrp.Framework.Classes.Alinea
    {
        public List<Hcrp.Framework.Classes.Alinea> ObterListaDeAlinea()
        {
            List<Hcrp.Framework.Classes.Alinea> _listaDeRetorno = new List<Hcrp.Framework.Classes.Alinea>();
            Hcrp.Framework.Classes.Alinea _alinea = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    // Cria a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT COD_ALINEA, DSC_ALINEA, SGL_ALINEA, IDF_CLASSE ");
                    str.AppendLine(" FROM ALINEA ");
                    str.AppendLine(" ORDER BY DSC_ALINEA ASC ");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _alinea = new Hcrp.Framework.Classes.Alinea();

                        if (dr["COD_ALINEA"] != DBNull.Value)
                            _alinea.Codigo = dr["COD_ALINEA"].ToString();

                        if (dr["DSC_ALINEA"] != DBNull.Value)
                            _alinea.Nome = dr["DSC_ALINEA"].ToString();

                        if (dr["SGL_ALINEA"] != DBNull.Value)
                            _alinea.Sigla = dr["SGL_ALINEA"].ToString();

                        if (dr["IDF_CLASSE"] != DBNull.Value)
                            _alinea.IdfClasse = Convert.ToInt32(dr["IDF_CLASSE"]);

                        _listaDeRetorno.Add(_alinea);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaDeRetorno;
        }

        public List<Hcrp.Framework.Classes.Alinea> ObterListaDeAlinea(int paginaAtual, out int totalRegistro, string filtroCodAlinea, string filtroNomeAlinea, string filtroSglAlinea, string filtroIdfClasse )
        {
            List<Hcrp.Framework.Classes.Alinea> _listaDeRetorno = new List<Hcrp.Framework.Classes.Alinea>();
            Hcrp.Framework.Classes.Alinea _alinea = null;
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

                    
                    if (!string.IsNullOrWhiteSpace(filtroCodAlinea) || !string.IsNullOrWhiteSpace(filtroNomeAlinea) || !string.IsNullOrWhiteSpace(filtroSglAlinea))
                    {

                        if (!string.IsNullOrWhiteSpace(filtroCodAlinea))
                        {
                            strWhere.AppendLine(string.Format(" AND COD_ALINEA LIKE '%{0}%' ", filtroCodAlinea.ToUpper()));
                        }

                        if (!string.IsNullOrWhiteSpace(filtroNomeAlinea))
                        {
                            strWhere.AppendLine(string.Format(" AND DSC_ALINEA LIKE '%{0}%' ", filtroNomeAlinea.ToUpper()));
                        }

                        if (!string.IsNullOrWhiteSpace(filtroSglAlinea))
                        {
                            strWhere.AppendLine(string.Format(" AND SGL_ALINEA LIKE '%{0}%' ", filtroSglAlinea.ToUpper()));
                        }

                        if (!string.IsNullOrWhiteSpace(filtroIdfClasse) && !filtroIdfClasse.Equals("0"))
                        {
                            strWhere.AppendLine(string.Format(" AND IDF_CLASSE = {0} ", filtroIdfClasse.ToUpper()));
                        }

                    }

                    str.AppendLine(" SELECT * FROM (SELECT A.*, ROWNUM AS RNUM FROM (SELECT DISTINCT ");
                    str.AppendLine("  COD_ALINEA, ");
                    str.AppendLine("  DSC_ALINEA, SGL_ALINEA, IDF_CLASSE,  ");
                    str.AppendLine("  CASE WHEN IDF_CLASSE = 1 THEN 'MATERIAIS DE CONSUMO' ");
                    str.AppendLine("       WHEN IDF_CLASSE = 2 THEN 'MEDICAMENTOS'");
                    str.AppendLine("       WHEN IDF_CLASSE = 3 THEN 'CONSUMO DURÁVEL'");
                    str.AppendLine("       WHEN IDF_CLASSE = 4 THEN 'PATRIMÔNIO'");
                    str.AppendLine("       WHEN IDF_CLASSE = 5 THEN 'PRESTACAO DE SERVIÇOS END CLASSE'");
                    str.AppendLine("FROM ALINEA ");
                    if (strWhere.Length > 0)
                        str.AppendLine(strWhere.ToString());
                    str.AppendLine(" ORDER BY DSC_ALINEA ASC) A ");
                    str.Append(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

                    // Montar query para pegar o total de registros.
                    strTotalRegistro.Append("SELECT COUNT(*) TOTAL ");
                    strTotalRegistro.AppendLine(" FROM ALINEA ");
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
                        _alinea = new Hcrp.Framework.Classes.Alinea();

                        if (dr["COD_ALINEA"] != DBNull.Value)
                            _alinea.Codigo = dr["COD_ALINEA"].ToString();

                        if (dr["DSC_ALINEA"] != DBNull.Value)
                            _alinea.Nome = dr["DSC_ALINEA"].ToString();

                        if (dr["SGL_ALINEA"] != DBNull.Value)
                            _alinea.Sigla = dr["SGL_ALINEA"].ToString();

                        if (dr["IDF_CLASSE"] != DBNull.Value)
                            _alinea.IdfClasse = Convert.ToInt32(dr["IDF_CLASSE"]);

                        _listaDeRetorno.Add(_alinea);

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
