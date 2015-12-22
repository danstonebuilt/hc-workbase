using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class Unidade : Hcrp.Framework.Classes.Unidade
    {
        public List<Hcrp.Framework.Classes.Unidade> BuscarListaDeUnidade(double codigo, string nome, string nomeabrev)
        {
            List<Hcrp.Framework.Classes.Unidade> _listaDeRetorno = new List<Hcrp.Framework.Classes.Unidade>();
            Hcrp.Framework.Classes.Unidade _unidade = null;            

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    // Cria a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine("SELECT COD_UNIDADE,");
                    str.AppendLine("       NOM_UNIDADE, NOM_ABREVIADO ");
                    str.AppendLine("FROM UNIDADE   ");
                    str.AppendLine(" WHERE COD_UNIDADE = :COD_UNIDADE");
                    str.AppendLine(" ORDER BY NOM_UNIDADE ");                  

                    
                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());                    

                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _unidade = new Hcrp.Framework.Classes.Unidade();

                        if (dr["COD_UNIDADE"] != DBNull.Value)
                            _unidade.Codigo = Convert.ToDouble(dr["COD_UNIDADE"]);

                        if (dr["NOM_UNIDADE"] != DBNull.Value)
                            _unidade.Nome = dr["NOM_UNIDADE"].ToString();

                        if (dr["NOM_ABREVIADO"] != DBNull.Value)
                            _unidade.NomeAbrev = dr["NOM_ABREVIADO"].ToString();

                        _listaDeRetorno.Add(_unidade);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaDeRetorno;

        }

        public Hcrp.Framework.Classes.Unidade BuscaUnidadeNomeAbrev(Int32 codUnid)
        {
            Hcrp.Framework.Classes.Unidade _un = new Hcrp.Framework.Classes.Unidade();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    // Cria a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine("SELECT NOM_ABREVIADO ");
                    str.AppendLine("FROM UNIDADE   ");
                    str.AppendLine(" WHERE COD_UNIDADE = :COD_UNIDADE");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["COD_UNIDADE"] = codUnid;
                    // Abre conexão
                    ctx.Open();

                    // Veriricar contador
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        
                        if (dr["NOM_ABREVIADO"] != DBNull.Value)
                            _un.NomeAbrev = dr["NOM_ABREVIADO"].ToString();

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _un;
        }

        public List<Hcrp.Framework.Classes.Unidade> BuscarListaDeUnidade()
        {
            List<Hcrp.Framework.Classes.Unidade> _listaDeRetorno = new List<Hcrp.Framework.Classes.Unidade>();
            Hcrp.Framework.Classes.Unidade _unidade = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    // Cria a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine("SELECT COD_UNIDADE,");
                    str.AppendLine("       NOM_UNIDADE, NOM_ABREVIADO ");
                    str.AppendLine("FROM PRESCRICAO.UNIDADE   ");
                    //str.AppendLine(" WHERE COD_UNIDADE = :COD_UNIDADE");
                    str.AppendLine(" ORDER BY NOM_UNIDADE ");


                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _unidade = new Hcrp.Framework.Classes.Unidade();

                        if (dr["COD_UNIDADE"] != DBNull.Value)
                            _unidade.Codigo = Convert.ToDouble(dr["COD_UNIDADE"]);

                        if (dr["NOM_UNIDADE"] != DBNull.Value)
                            _unidade.Nome = dr["NOM_UNIDADE"].ToString();

                        if (dr["NOM_ABREVIADO"] != DBNull.Value)
                            _unidade.NomeAbrev = dr["NOM_ABREVIADO"].ToString();

                        _listaDeRetorno.Add(_unidade);

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
