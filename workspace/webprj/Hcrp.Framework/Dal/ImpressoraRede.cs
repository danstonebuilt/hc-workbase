using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class ImpressoraRede
    {
        /// <summary>
        /// Obter lista de impressoras na rede
        /// </summary>
        /// <returns></returns>
        public List<Hcrp.Framework.Classes.ImpressoraRede> ObterListaDeImpressoraNaRede()
        {
            List<Hcrp.Framework.Classes.ImpressoraRede> listaRetorno = new List<Hcrp.Framework.Classes.ImpressoraRede>();
            Hcrp.Framework.Classes.ImpressoraRede item = null;

            try
            {
                using (Contexto ctx = new Contexto())
                {
                    StringBuilder str = new StringBuilder();
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal

                    string ip = System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    if (ip == "::1")
                    {
                        ip = "127.0.0.1";
                    }

                    str.AppendLine(" SELECT SEQ_IMPRESSORA_REDE, ENDERECO_IP, NOM_MAQUINA, NOM_IMPRESSORA FROM IMPRESSORA_REDE WHERE ENDERECO_IP = '" + ip + "'");

                    query = new QueryCommandConfig(str.ToString());

                    //query.Params["NUM_USER_BANCO"] = Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    // Preparar o retorno
                    while (dr.Read())
                    {
                        item = new Classes.ImpressoraRede();

                        if (dr["SEQ_IMPRESSORA_REDE"] != DBNull.Value)
                            item.SeqImpressoraRede = Convert.ToInt64(dr["SEQ_IMPRESSORA_REDE"]);

                        if (dr["ENDERECO_IP"] != DBNull.Value)
                            item.EnderecoIP = dr["ENDERECO_IP"].ToString();

                        if (dr["NOM_MAQUINA"] != DBNull.Value)
                            item.NomeMaquina = dr["NOM_MAQUINA"].ToString();

                        if (dr["NOM_IMPRESSORA"] != DBNull.Value)
                            item.NomeImpressora = dr["NOM_IMPRESSORA"].ToString();

                        listaRetorno.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return listaRetorno;
        }
    }
}
