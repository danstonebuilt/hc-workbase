using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class PesquisaGenerica : Hcrp.Framework.Classes.PesquisaGenerica 
    {
        public List<Hcrp.Framework.Classes.PesquisaGenerica> ListaPesquisaGenerica(string sql)
        {
            List<Hcrp.Framework.Classes.PesquisaGenerica> _listaDeRetorno = new List<Hcrp.Framework.Classes.PesquisaGenerica>();
            Hcrp.Framework.Classes.PesquisaGenerica _pesquisa = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);

                    // Abre conexão
                    ctx.Open();

                    // Veriricar contador
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _pesquisa = new Hcrp.Framework.Classes.PesquisaGenerica();

                        if (dr["CODIGO"] != DBNull.Value)
                            _pesquisa.CODIGO = dr["CODIGO"].ToString();

                        if (dr["DESCRICAO"] != DBNull.Value)
                            _pesquisa.DESCRICAO = dr["DESCRICAO"].ToString();

                        _listaDeRetorno.Add(_pesquisa);
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
