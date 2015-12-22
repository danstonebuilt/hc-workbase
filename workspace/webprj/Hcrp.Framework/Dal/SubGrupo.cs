using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class SubGrupo : Hcrp.Framework.Classes.SubGrupo
    {
        public List<Hcrp.Framework.Classes.SubGrupo> ObterListaDeSubGrupo(int paginaAtual, out int totalRegistro, string filtroCodSubGrupo, string filtroNomeSubGrupo, string filtroCodGrupo)
        {
            List<Hcrp.Framework.Classes.SubGrupo> _listaDeRetorno = new List<Hcrp.Framework.Classes.SubGrupo>();
            Hcrp.Framework.Classes.SubGrupo _subgrupo = null;
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

                    
                    if (!string.IsNullOrWhiteSpace(filtroCodSubGrupo) || !string.IsNullOrWhiteSpace(filtroNomeSubGrupo) )
                    {

                        if (!string.IsNullOrWhiteSpace(filtroCodSubGrupo))
                        {
                            strWhere.AppendLine(string.Format(" AND COD_SUB_GRUPO LIKE '%{0}%' ", filtroCodSubGrupo.ToUpper()));
                        }

                        if (!string.IsNullOrWhiteSpace(filtroNomeSubGrupo))
                        {
                            strWhere.AppendLine(string.Format(" AND DSC_SUB_GRUPO LIKE '%{0}%' ", filtroNomeSubGrupo.ToUpper()));
                        }

                        
                        if (!string.IsNullOrWhiteSpace(filtroCodGrupo) && !filtroCodGrupo.Equals("0"))
                        {
                            strWhere.AppendLine(string.Format(" AND COD_GRUPO = {0} ", filtroCodGrupo.ToUpper()));
                        }

                    }

                    str.AppendLine(" SELECT * FROM (SELECT A.*, ROWNUM AS RNUM FROM (SELECT DISTINCT ");
                    str.AppendLine("  COD_SUB_GRUPO, ");
                    str.AppendLine("  DSC_SUB_GRUPO, COD_GRUPO'");
                    str.AppendLine("FROM SUB_GRUPO ");
                    if (strWhere.Length > 0)
                        str.AppendLine(strWhere.ToString());
                    str.AppendLine(" ORDER BY DSC_SUB_GRUPO ASC) A ");
                    str.Append(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

                    // Montar query para pegar o total de registros.
                    strTotalRegistro.Append("SELECT COUNT(*) TOTAL ");
                    strTotalRegistro.AppendLine(" FROM SUB_GRUPO ");
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
                        _subgrupo = new Hcrp.Framework.Classes.SubGrupo();

                        if (dr["COD_SUB_GRUPO"] != DBNull.Value)
                            _subgrupo.Codigo = dr["COD_SUB_GRUPO"].ToString();

                        if (dr["DSC_SUB_GRUPO"] != DBNull.Value)
                            _subgrupo.Nome = dr["DSC_SUB_GRUPO"].ToString();

                        if (dr["COD_GRUPO"] != DBNull.Value)
                            _subgrupo.CodGrupo = dr["COD_GRUPO"].ToString();  

                        _listaDeRetorno.Add(_subgrupo);

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
