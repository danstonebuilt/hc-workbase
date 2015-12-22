using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hcrp.Infra.AcessoDado;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class Programa
    {
        #region Métodos

        /// <summary>
        /// Remover numeros da string.
        /// </summary>        
        protected string RemoverNumerosDaString(string valor)
        {
            try
            {
                valor = valor.Replace("_1", "")
                             .Replace("_2", "")
                             .Replace("_3", "")
                             .Replace("_4", "")
                             .Replace("_5", "")
                             .Replace("_6", "")
                             .Replace("_7", "")
                             .Replace("_8", "")
                             .Replace("_9", "")
                             .Replace("_0", "")
                             .Replace("1", "")
                             .Replace("1", "")
                             .Replace("3", "")
                             .Replace("4", "")
                             .Replace("5", "")
                             .Replace("6", "")
                             .Replace("7", "")
                             .Replace("8", "")
                             .Replace("9", "")
                             .Replace("0", "");

                return valor;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter programa por nome programa.
        /// </summary>        
        public Hcrp.CarroUrgenciaPsicoativo.Entity.Programa ObterProgramaPorNomePrograma(string nomePrograma, int codSistema)
        {
            Hcrp.CarroUrgenciaPsicoativo.Entity.Programa programa = null;

            try
            {
                nomePrograma = this.RemoverNumerosDaString(nomePrograma);
                
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT P.COD_PROGRAMA, ");
                    str.AppendLine("        P.NOM_PROGRAMA, ");
                    str.AppendLine("        P.DSC_PROGRAMA, ");
                    str.AppendLine("        P.NOM_EXIBICAO_PROGRAMA, ");
                    str.AppendLine("        P_PAI.COD_PROGRAMA COD_PROG_PAI, ");
                    str.AppendLine("        P_PAI.NOM_PROGRAMA NOM_PROG_PAI ");
                    str.AppendLine(" FROM PROGRAMA P, ");
                    str.AppendLine("      SISTEMA_PROGRAMA SP, ");
                    str.AppendLine("      PROGRAMA P_PAI ");
                    str.AppendLine(string.Format(" WHERE P.NOM_PROGRAMA = '{0}' ", nomePrograma));
                    str.AppendLine(" AND P.COD_PROGRAMA = SP.COD_PROGRAMA ");
                    str.AppendLine(string.Format(" AND SP.COD_SISTEMA = {0} ", codSistema));
                    str.AppendLine(" AND SP.COD_PROGRAMA_PAI = P_PAI.COD_PROGRAMA(+) ");                    

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            programa = new Entity.Programa();

                            programa.CodigoPrograma = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "COD_PROGRAMA");
                            programa.NomePrograma = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "NOM_PROGRAMA");
                            programa.DscPrograma = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "DSC_PROGRAMA");
                            programa.NomeExibicaoPrograma = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "NOM_EXIBICAO_PROGRAMA");
                            programa.CodigoProgramaPai = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "COD_PROG_PAI");
                            programa.NomeProgramaPai = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "NOM_PROG_PAI", "");

                            break;
                        }
                    }
                    finally
                    {
                        dr.Close();
                        dr.Dispose();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

            return programa;
        }

        #endregion
    }
}
