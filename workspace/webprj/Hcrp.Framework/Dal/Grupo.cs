using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class Grupo : Hcrp.Framework.Classes.Grupo
    {
        public Hcrp.Framework.Classes.Grupo BuscaGrupoCodigo(string codGrupo)
        {
            Hcrp.Framework.Classes.Grupo _grupo = new Hcrp.Framework.Classes.Grupo();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Cria a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT COD_GRUPO, ");
                    str.AppendLine("        DSC_GRUPO, ");
                    str.AppendLine("        COD_ALINEA ");
                    str.AppendLine(" FROM GRUPO ");
                    str.AppendLine("  WHERE COD_GRUPO = :COD_GRUPO");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["COD_GRUPO"] = codGrupo;
                    // Abre conexão
                    ctx.Open();

                    // Veriricar contador
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        if (dr["COD_GRUPO"] != DBNull.Value)
                            _grupo.Codigo = dr["COD_GRUPO"].ToString();

                        if (dr["DSC_GRUPO"] != DBNull.Value)
                            _grupo.Nome = dr["DSC_GRUPO"].ToString();

                        if (dr["COD_ALINEA"] != DBNull.Value)
                            _grupo.CodigoAlinea = Convert.ToInt32(dr["COD_ALINEA"]);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _grupo;
        }

        public List<Hcrp.Framework.Classes.Grupo> ObterListaDeGrupo(int paginaAtual, out int totalRegistro, string filtroCodGrupo, string filtroNomeGrupo, string filtroCodAlinea) 
        {
            List<Hcrp.Framework.Classes.Grupo> _listaDeRetorno = new List<Hcrp.Framework.Classes.Grupo>();
            Hcrp.Framework.Classes.Grupo _grupo = null;
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
                    
                    if (!string.IsNullOrWhiteSpace(filtroCodGrupo) || !string.IsNullOrWhiteSpace(filtroNomeGrupo))
                    {

                        if (!string.IsNullOrWhiteSpace(filtroCodGrupo))
                        {
                            strWhere.AppendLine(string.Format(" AND COD_GRUPO LIKE '%{0}%' ", filtroCodGrupo.ToUpper()));
                        }

                        if (!string.IsNullOrWhiteSpace(filtroNomeGrupo))
                        {
                            strWhere.AppendLine(string.Format(" AND DSC_GRUPO LIKE '%{0}%' ", filtroNomeGrupo.ToUpper()));
                        }
                        if (!string.IsNullOrWhiteSpace(filtroCodAlinea) && !filtroCodAlinea.Equals("0"))
                        {
                            strWhere.AppendLine(string.Format(" AND COD_ALINEA = {0} ", filtroCodAlinea.ToUpper()));
                        }

                    }

                    str.AppendLine(" SELECT * FROM (SELECT A.*, ROWNUM AS RNUM FROM (SELECT DISTINCT ");
                    str.AppendLine("  COD_GRUPO, ");
                    str.AppendLine("  DSC_GRUPO, COD_ALINEA ");
                    str.AppendLine("FROM GRUPO ");
                    if (strWhere.Length > 0)
                        str.AppendLine(strWhere.ToString());
                    str.AppendLine(" ORDER BY DSC_GRUPO ASC) A ");
                    str.Append(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

                    // Montar query para pegar o total de registros.
                    strTotalRegistro.Append("SELECT COUNT(*) TOTAL ");
                    strTotalRegistro.AppendLine(" FROM GRUPO ");
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
                        _grupo = new Hcrp.Framework.Classes.Grupo();

                        if (dr["COD_GRUPO"] != DBNull.Value)
                            _grupo.Codigo = dr["COD_GRUPO"].ToString();

                        if (dr["DSC_GRUPO"] != DBNull.Value)
                            _grupo.Nome = dr["DSC_GRUPO"].ToString();

                        if (dr["COD_ALINEA"] != DBNull.Value)
                            _grupo.CodigoAlinea = Convert.ToInt32(dr["COD_ALINEA"]);

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
    }
}
