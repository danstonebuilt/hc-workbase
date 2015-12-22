using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class IntegraAplicativo
    {
        #region Métodos

        /// <summary>
        /// Inserir no integra aplicativo. 
        /// </summary>        
        public void InserirIntegraAlicativo(string numChave, string nomUsuarioBanco, string dscSenha)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" INSERT INTO INTEGRA_APLICATIVO (NUM_CHAVE, NOM_USUARIO_BANCO, DSC_SENHA_USUARIO, DTA_VALIDADE_TOKEN) ");
                    str.AppendLine(" VALUES ('" + numChave + "','" + nomUsuarioBanco + "','" + dscSenha + "',TRUNC(SYSDATE)+0.99999)");

                    QueryCommandConfig query = new QueryCommandConfig(str.ToString());

                    ctx.ExecuteNonQuery(query);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inserir no integra aplicativo item. 
        /// </summary>        
        public void InserirIntegraAlicativoItem(string numChave, string nomParametro, string dscvalor)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" INSERT INTO INTEGRA_APLICATIVO_ITEM (NUM_CHAVE, NOM_PARAMETRO, DSC_VALOR) ");
                    str.AppendLine(" VALUES ('" + numChave + "','" + nomParametro + "','" + dscvalor + "')");

                    QueryCommandConfig query = new QueryCommandConfig(str.ToString());

                    ctx.ExecuteNonQuery(query);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
