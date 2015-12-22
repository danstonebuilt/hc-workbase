using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hcrp.Infra.AcessoDado;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class Paciente
    {
        #region Métodos

        /// <summary>
        /// Obter lista de paciente
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.Paciente> ObterListaDePaciente(string nome, string sobrenome, int qtdRegistroPorPagina, int paginaAtual, out int totalRegistro)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.Paciente> listaRetorno = new List<Hcrp.CarroUrgenciaPsicoativo.Entity.Paciente>();
            Hcrp.CarroUrgenciaPsicoativo.Entity.Paciente paciente;
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
                    strWhere.AppendLine(" WHERE 1 = 1  ");

                    if (!string.IsNullOrWhiteSpace(nome))
                        strWhere.AppendLine(" AND P.NOM_PACIENTE LIKE UPPER('" + nome.Trim().ToUpper().Replace("'", "") + "' || '%') ");

                    if (!string.IsNullOrWhiteSpace(sobrenome))
                        strWhere.AppendLine(" AND P.SBN_PACIENTE LIKE UPPER('%' || '" + sobrenome.Trim().ToUpper().Replace("'", "") + "' || '%') ");

                    // Query Principal
                    str.AppendLine("SELECT * ");
                    str.AppendLine(" FROM (SELECT A.*, ");
                    str.AppendLine("  ROWNUM AS RNUM FROM ");
                    str.AppendLine("   (    SELECT ");
                    str.AppendLine("            P.COD_PACIENTE, ");
                    str.AppendLine("            NVL(P.NOM_PACIENTE,'') NOM_PACIENTE, NVL(P.SBN_PACIENTE,'') SBN_PACIENTE, ");
                    str.AppendLine("            TO_CHAR(DTA_NASCIMENTO, 'DD/MM/YYYY') DATA_NASCIMENTO, NOM_MAE ");
                    str.AppendLine("         FROM PACIENTE P");
                    str.AppendLine(strWhere.ToString());
                    str.AppendLine(" ORDER BY P.NOM_PACIENTE) A ");
                    str.AppendLine(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

                    strTotalRegistro.AppendLine("SELECT COUNT(*) AS TOTAL FROM PACIENTE P ");
                    strTotalRegistro.AppendLine(strWhere.ToString());

                    query = new QueryCommandConfig(str.ToString());
                    QueryCommandConfig queryCount = new QueryCommandConfig(strTotalRegistro.ToString());

                    // Veriricar contador
                    ctx.ExecuteQuery(queryCount);

                    while (ctx.Reader.Read())
                    {
                        totalRegistro = Convert.ToInt32(ctx.Reader["total"]);
                        break;
                    }

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    // Preparar o retorno
                    while (ctx.Reader.Read())
                    {
                        paciente = new Entity.Paciente();

                        if (ctx.Reader["COD_PACIENTE"] != DBNull.Value)
                            paciente.RegistroPaciente = ctx.Reader["COD_PACIENTE"].ToString();

                        if (ctx.Reader["NOM_PACIENTE"] != DBNull.Value)
                            paciente.NomePaciente = ctx.Reader["NOM_PACIENTE"].ToString();

                        if (ctx.Reader["SBN_PACIENTE"] != DBNull.Value)
                            paciente.SobrenomePaciente = ctx.Reader["SBN_PACIENTE"].ToString();

                        if (ctx.Reader["DATA_NASCIMENTO"] != DBNull.Value)
                            paciente.DataNascimento = Convert.ToDateTime(ctx.Reader["DATA_NASCIMENTO"]);

                        if (ctx.Reader["NOM_MAE"] != DBNull.Value)
                            paciente.MaePaciente = ctx.Reader["NOM_MAE"].ToString();

                        listaRetorno.Add(paciente);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return listaRetorno;
        }       

        #endregion
    }
}
