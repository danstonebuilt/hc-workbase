using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class PlanoDeContaItem : Hcrp.Framework.Classes.PlanoDeContaItem
    {
        public List<Hcrp.Framework.Classes.PlanoDeContaItem> ObterListaDeItemPorIdDePlanoDeConta(long idPlanoConta)
        {
            List<Hcrp.Framework.Classes.PlanoDeContaItem> _listaDeRetorno = new List<Hcrp.Framework.Classes.PlanoDeContaItem>();
            Hcrp.Framework.Classes.PlanoDeContaItem _planoDeConta = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(string.Format(" SELECT * FROM PLANO_CONTA_ITEM WHERE SEQ_PLANO_CONTA = {0} ORDER BY NUM_ORDEM", idPlanoConta));

                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _planoDeConta = new Hcrp.Framework.Classes.PlanoDeContaItem();

                        if (dr["SEQ_ITEM_PLANO_CONTA"] != DBNull.Value)
                            _planoDeConta.IdPlanoDeContaItem = Convert.ToInt64(dr["SEQ_ITEM_PLANO_CONTA"]);

                        if (dr["SEQ_PLANO_CONTA"] != DBNull.Value)
                            _planoDeConta.PlanoDeConta.IdPlanoDeConta = Convert.ToInt64(dr["SEQ_PLANO_CONTA"]);

                        if (dr["NOM_ITEM_PLANO_CONTA"] != DBNull.Value)
                            _planoDeConta.NomeDoItem = dr["NOM_ITEM_PLANO_CONTA"].ToString();

                        if (dr["NUM_ORDEM_LOCAL"] != DBNull.Value)
                            _planoDeConta.Ordem = Convert.ToInt32(dr["NUM_ORDEM_LOCAL"]);

                        if (dr["SEQ_ITEM_PLANO_CONTA_PAI"] != DBNull.Value)
                            _planoDeConta.IdPlanoDeContaItemPai = Convert.ToInt64(dr["SEQ_ITEM_PLANO_CONTA_PAI"]);

                        if (dr["NUM_ORDEM"] != DBNull.Value)
                            _planoDeConta.OrdemHierarquia = dr["NUM_ORDEM"].ToString();

                        _planoDeConta.NivelIdentacao = 1;

                        _listaDeRetorno.Add(_planoDeConta);

                    }

                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaDeRetorno;
        }

        public List<Hcrp.Framework.Classes.PlanoDeContaItem> ObterListaHierarquicaDeItemPorIdDePlanoDeConta(long idPlanoConta)
        {
            List<Hcrp.Framework.Classes.PlanoDeContaItem> _listaDeRetorno = new List<Hcrp.Framework.Classes.PlanoDeContaItem>();
            Hcrp.Framework.Classes.PlanoDeContaItem _planoDeConta = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT * ");
                    str.AppendLine(" FROM ( ");
                    str.AppendLine("     SELECT PL.SEQ_ITEM_PLANO_CONTA, ");
                    str.AppendLine("            SYS_CONNECT_BY_PATH( PL.NOM_ITEM_PLANO_CONTA, '-> ' ) CAMINHO ");
                    str.AppendLine("     FROM PLANO_CONTA_ITEM PL ");
                    str.AppendLine(string.Format("     WHERE SEQ_PLANO_CONTA = {0} ", idPlanoConta));
                    str.AppendLine("     CONNECT BY PRIOR ");
                    str.AppendLine("       PL.SEQ_ITEM_PLANO_CONTA = PL.SEQ_ITEM_PLANO_CONTA_PAI ");
                    str.AppendLine("     START WITH ");
                    str.AppendLine("       PL.SEQ_ITEM_PLANO_CONTA_PAI IS NULL ");
                    str.AppendLine(" ) X  WHERE NOT EXISTS  ");
                    str.AppendLine("   (SELECT S1.SEQ_ITEM_PLANO_CONTA FROM PLANO_CONTA_ITEM S1  ");
                    str.AppendLine("    WHERE S1.SEQ_ITEM_PLANO_CONTA_PAI = X.SEQ_ITEM_PLANO_CONTA) ");
                    str.AppendLine(" ORDER BY CAMINHO ");                   

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _planoDeConta = new Hcrp.Framework.Classes.PlanoDeContaItem();

                        if (dr["SEQ_ITEM_PLANO_CONTA"] != DBNull.Value)
                            _planoDeConta.IdPlanoDeContaItem = Convert.ToInt64(dr["SEQ_ITEM_PLANO_CONTA"]);

                        if (dr["CAMINHO"] != DBNull.Value)
                            _planoDeConta.NomeDoItem = dr["CAMINHO"].ToString();

                        _planoDeConta.NivelIdentacao = 1;

                        _listaDeRetorno.Add(_planoDeConta);

                    }

                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaDeRetorno;
        }

        public List<Hcrp.Framework.Classes.PlanoDeContaItem> ObterListaHierarquicaDePlanoDeContaDeMateriais(string idPlanoConta)
        {
            List<Hcrp.Framework.Classes.PlanoDeContaItem> _listaDeRetorno = new List<Hcrp.Framework.Classes.PlanoDeContaItem>();
            Hcrp.Framework.Classes.PlanoDeContaItem _planoDeConta = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT X.* ");
                    str.AppendLine(" FROM ( ");
                    str.AppendLine("     SELECT PL.SEQ_ITEM_PLANO_CONTA,  ");
                    str.AppendLine("            SYS_CONNECT_BY_PATH( PL.NOM_ITEM_PLANO_CONTA, '-> ' ) CAMINHO ");
                    str.AppendLine("     FROM  PLANO_CONTA_ITEM PL ");
                    str.AppendLine(string.Format("     WHERE SEQ_PLANO_CONTA = {0} ", idPlanoConta));
                    str.AppendLine("     CONNECT BY PRIOR ");
                    str.AppendLine("        PL.SEQ_ITEM_PLANO_CONTA = PL.SEQ_ITEM_PLANO_CONTA_PAI ");
                    str.AppendLine("     START WITH ");
                    str.AppendLine("        PL.SEQ_ITEM_PLANO_CONTA_PAI IS NULL ");
                    str.AppendLine(" ) X WHERE X.SEQ_ITEM_PLANO_CONTA IN  ");
                    str.AppendLine("   (SELECT SEQ_ITEM_PLANO_CONTA  ");
                    str.AppendLine("    FROM PLANO_CONTA_ITEM  ");
                    str.AppendLine(string.Format("    WHERE SEQ_PLANO_CONTA = {0}  ", idPlanoConta));
                    str.AppendLine("    CONNECT BY PRIOR SEQ_ITEM_PLANO_CONTA_PAI = SEQ_ITEM_PLANO_CONTA ");
                    str.AppendLine("    START WITH SEQ_ITEM_PLANO_CONTA IN (SELECT SEQ_ITEM_PLANO_CONTA  ");
                    str.AppendLine("                                        FROM GRUPO_PLANOCONTAITEM)) ");
                    str.AppendLine(" ORDER BY X.CAMINHO ");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _planoDeConta = new Hcrp.Framework.Classes.PlanoDeContaItem();

                        if (dr["SEQ_ITEM_PLANO_CONTA"] != DBNull.Value)
                            _planoDeConta.IdPlanoDeContaItem = Convert.ToInt64(dr["SEQ_ITEM_PLANO_CONTA"]);

                        if (dr["CAMINHO"] != DBNull.Value)
                            _planoDeConta.NomeDoItem = dr["CAMINHO"].ToString();

                        _planoDeConta.NivelIdentacao = 1;

                        _listaDeRetorno.Add(_planoDeConta);

                    }

                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaDeRetorno;
        }

        public string[] ObterListaDeOrdem(long idPlanoConta)
        {
            List<string> listaRetorno = new List<string>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(string.Format(" SELECT NUM_ORDEM_LOCAL FROM PLANO_CONTA_ITEM WHERE SEQ_PLANO_CONTA = {0} AND SEQ_ITEM_PLANO_CONTA_PAI IS NULL", idPlanoConta));

                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        if (dr["NUM_ORDEM_LOCAL"] != DBNull.Value)
                            listaRetorno.Add(dr["NUM_ORDEM_LOCAL"].ToString());
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return listaRetorno.ToArray();
        }

        /// <summary>
        /// Adicionar relação entre grupo e centro de custo
        /// </summary>
        /// <param name="_centro"></param>
        public void Adicionar(Hcrp.Framework.Classes.PlanoDeContaItem _planoDeContaItem)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abre conexão
                    ctx.Open();

                    // Configurar comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PLANO_CONTA_ITEM");

                    // Adicionar parametros
                    comando.Params["SEQ_PLANO_CONTA"] = _planoDeContaItem.PlanoDeConta.IdPlanoDeConta;
                    comando.Params["NOM_ITEM_PLANO_CONTA"] = _planoDeContaItem.NomeDoItem;
                    comando.Params["NUM_ORDEM_LOCAL"] = _planoDeContaItem.Ordem;
                    comando.Params["NUM_ORDEM"] = " ";

                    if (_planoDeContaItem.IdPlanoDeContaItemPai.HasValue)
                        comando.Params["SEQ_ITEM_PLANO_CONTA_PAI"] = _planoDeContaItem.IdPlanoDeContaItemPai;
                    else
                        comando.Params["NUM_ORDEM"] = _planoDeContaItem.Ordem.ToString();

                    // Executar o comando
                    ctx.ExecuteInsert(comando);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Remove relacionamento
        /// </summary>
        /// <param name="_centro"></param>
        public void Remover(long idPlanoDeContaItem)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abre conexão
                    ctx.Open();

                    // Configurar comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PLANO_CONTA_ITEM");

                    List<long> listaDeRemocao = this.ObterListaDeItemASeremExcluidos(idPlanoDeContaItem);

                    foreach (var i in listaDeRemocao)
                    {
                        // Adicionar parametros
                        comando.Params["SEQ_ITEM_PLANO_CONTA"] = i;

                        // Executar o comando
                        ctx.ExecuteDelete(comando);
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<long> ObterListaDeItemASeremExcluidos(long id)
        {
            List<long> listaRetorno = new List<long>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT ");
                    str.AppendLine("       PCI.SEQ_ITEM_PLANO_CONTA, ");
                    str.AppendLine("       SYS_CONNECT_BY_PATH( PCI.NOM_ITEM_PLANO_CONTA, '-> ' ) CAMINHO, ");
                    str.AppendLine("       LEVEL ");
                    str.AppendLine(" FROM PLANO_CONTA_ITEM PCI ");
                    str.AppendLine(" CONNECT BY NOCYCLE PRIOR ");
                    str.AppendLine("   PCI.SEQ_ITEM_PLANO_CONTA = PCI.SEQ_ITEM_PLANO_CONTA_PAI ");
                    str.AppendLine(" START WITH ");
                    str.AppendLine(string.Format(" PCI.SEQ_ITEM_PLANO_CONTA = {0} ", id));
                    str.AppendLine(" ORDER BY SEQ_ITEM_PLANO_CONTA DESC ");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        if (dr["SEQ_ITEM_PLANO_CONTA"] != DBNull.Value)
                            listaRetorno.Add(Convert.ToInt64(dr["SEQ_ITEM_PLANO_CONTA"]));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return listaRetorno;
        }

        public int ObterOrdem(long idPai)
        {
            int retorno = 0;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    string queryMax = "";

                    queryMax = " SELECT MAX(NUM_ORDEM_LOCAL) TOTAL FROM PLANO_CONTA_ITEM WHERE " + (idPai > 0 ? " SEQ_ITEM_PLANO_CONTA_PAI = " + idPai : " SEQ_ITEM_PLANO_CONTA_PAI IS NULL ");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(queryMax);

                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(queryMax);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        if (dr["TOTAL"] != DBNull.Value)
                            retorno = Convert.ToInt32(dr["TOTAL"]) + 1;
                        else
                            retorno = 1;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return retorno;
        }

        public void AtualizarOrdemHierarquica(long idPlanoContaItem, string ordemHierarquica)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abre conexão
                    ctx.Open();

                    // Configurar comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("PLANO_CONTA_ITEM");

                    // Adicionar parametros
                    comando.FilterParams["SEQ_ITEM_PLANO_CONTA"] = idPlanoContaItem;
                    comando.Params["NUM_ORDEM"] = ordemHierarquica;

                    // Executar o comando
                    ctx.ExecuteUpdate(comando);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AtualizarOrdemDoItem(long idPlanoDeContaItem, long idPlanoDeContaItemPai, int ordemAtual, bool mudarParaORegistroAcima)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    // Obter registro que será realizada a troca de ordem
                    string queryMax = "";
                    long idPlanoDeContaItemQueSeraTrocadoAOrdem = 0;
                    int numeroOrdem = 0;
                    IDataReader dr;

                    // Abre conexão
                    ctx.Open();

                    if (idPlanoDeContaItemPai > 0)
                    {
                        queryMax = string.Format(" SELECT SEQ_ITEM_PLANO_CONTA, NUM_ORDEM_LOCAL FROM PLANO_CONTA_ITEM WHERE {0} AND NUM_ORDEM_LOCAL = {1}", (idPlanoDeContaItemPai > 0 ? " SEQ_ITEM_PLANO_CONTA_PAI = " + idPlanoDeContaItemPai : " SEQ_ITEM_PLANO_CONTA_PAI IS NULL "), (mudarParaORegistroAcima ? ordemAtual - 1 : ordemAtual + 1));

                        // Preparar a query
                        Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(queryMax);

                        // Obter a lista de registros
                        ctx.ExecuteQuery(queryMax);

                        dr = ctx.Reader;

                        while (dr.Read())
                        {
                            if (dr["SEQ_ITEM_PLANO_CONTA"] != DBNull.Value)
                                idPlanoDeContaItemQueSeraTrocadoAOrdem = Convert.ToInt64(dr["SEQ_ITEM_PLANO_CONTA"]);

                            if (dr["NUM_ORDEM_LOCAL"] != DBNull.Value)
                                numeroOrdem = Convert.ToInt32(dr["NUM_ORDEM_LOCAL"]);

                        }

                    }
                    else
                    {
                        if (mudarParaORegistroAcima)
                        {
                            queryMax = string.Format(" SELECT MAX(NUM_ORDEM_LOCAL) NUM_ORDEM_LOCAL FROM PLANO_CONTA_ITEM WHERE SEQ_ITEM_PLANO_CONTA_PAI IS NULL AND NUM_ORDEM_LOCAL < {0}", ordemAtual);
                        }
                        else
                        {
                            queryMax = string.Format(" SELECT MIN(NUM_ORDEM_LOCAL) NUM_ORDEM_LOCAL FROM PLANO_CONTA_ITEM WHERE SEQ_ITEM_PLANO_CONTA_PAI IS NULL AND NUM_ORDEM_LOCAL > {0}", ordemAtual);
                        }

                        // Preparar a query
                        Hcrp.Infra.AcessoDado.QueryCommandConfig queryOrdem = new Hcrp.Infra.AcessoDado.QueryCommandConfig(queryMax);

                        // Obter a lista de registros
                        ctx.ExecuteQuery(queryOrdem);

                        dr = ctx.Reader;

                        while (dr.Read())
                        {
                            if (dr["NUM_ORDEM_LOCAL"] != DBNull.Value)
                                numeroOrdem = Convert.ToInt32(dr["NUM_ORDEM_LOCAL"]);

                        }

                        queryMax = string.Format(" SELECT SEQ_ITEM_PLANO_CONTA FROM PLANO_CONTA_ITEM WHERE SEQ_ITEM_PLANO_CONTA_PAI IS NULL AND NUM_ORDEM_LOCAL = {0}", numeroOrdem);

                        // Obter a lista de registros
                        ctx.ExecuteQuery(queryMax);

                        dr = ctx.Reader;

                        while (dr.Read())
                        {
                            if (dr["SEQ_ITEM_PLANO_CONTA"] != DBNull.Value)
                                idPlanoDeContaItemQueSeraTrocadoAOrdem = Convert.ToInt32(dr["SEQ_ITEM_PLANO_CONTA"]);

                        }

                    }

                    // Realizar a troca da ordem
                    // Configurar comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("PLANO_CONTA_ITEM");

                    // Adicionar parametros
                    comando.FilterParams["SEQ_ITEM_PLANO_CONTA"] = idPlanoDeContaItem;
                    comando.Params["NUM_ORDEM_LOCAL"] = numeroOrdem;
                    comando.Params["NUM_ORDEM"] = (idPlanoDeContaItemPai > 0 ? " " : numeroOrdem.ToString());

                    // Executar o comando
                    ctx.ExecuteUpdate(comando);

                    // Atualizar o outro registro
                    comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("PLANO_CONTA_ITEM");

                    // Adicionar parametros
                    comando.FilterParams["SEQ_ITEM_PLANO_CONTA"] = idPlanoDeContaItemQueSeraTrocadoAOrdem;
                    comando.Params["NUM_ORDEM_LOCAL"] = ordemAtual;
                    comando.Params["NUM_ORDEM"] = (idPlanoDeContaItemPai > 0 ? " " : ordemAtual.ToString());

                    // Executar o comando
                    ctx.ExecuteUpdate(comando);

                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
