using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class GrupoPlanoContaItem : Hcrp.Framework.Classes.GrupoPlanoContaItem 
    {
        public List<Hcrp.Framework.Classes.GrupoPlanoContaItem> ObterListaDeGrupoPlanoContaItem(long idPlanoConta)
        {
            List<Hcrp.Framework.Classes.GrupoPlanoContaItem> _listaDeRetorno = new List<Hcrp.Framework.Classes.GrupoPlanoContaItem>();
            Hcrp.Framework.Classes.GrupoPlanoContaItem _grupoPlanoContaItem = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    // Cria a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT A.DSC_ALINEA AS ALINEA, G.COD_GRUPO, G.DSC_GRUPO AS GRUPO, Y.CAMINHO AS ITEM ");
                    str.AppendLine(" FROM ALINEA A, GRUPO G, GRUPO_PLANOCONTAITEM P, ");
                    str.AppendLine(" (SELECT * ");
                    str.AppendLine(" FROM ( ");
                    str.AppendLine("     SELECT PL.SEQ_ITEM_PLANO_CONTA,  ");
                    str.AppendLine("            SYS_CONNECT_BY_PATH( PL.NOM_ITEM_PLANO_CONTA, '-> ' ) CAMINHO ");
                    str.AppendLine("     FROM PLANO_CONTA_ITEM PL ");
                    str.AppendLine(string.Format("     WHERE SEQ_PLANO_CONTA = {0} ", idPlanoConta));
                    str.AppendLine("     CONNECT BY PRIOR ");
                    str.AppendLine("        PL.SEQ_ITEM_PLANO_CONTA = PL.SEQ_ITEM_PLANO_CONTA_PAI ");
                    str.AppendLine("     START WITH ");
                    str.AppendLine("        PL.SEQ_ITEM_PLANO_CONTA_PAI IS NULL ");
                    str.AppendLine(" ) X  WHERE NOT EXISTS  ");
                    str.AppendLine("   (SELECT S1.SEQ_ITEM_PLANO_CONTA FROM PLANO_CONTA_ITEM S1  ");
                    str.AppendLine("    WHERE S1.SEQ_ITEM_PLANO_CONTA_PAI = X.SEQ_ITEM_PLANO_CONTA)  ");
                    str.AppendLine("    AND X.SEQ_ITEM_PLANO_CONTA = SEQ_ITEM_PLANO_CONTA ");
                    str.AppendLine(" ORDER BY CAMINHO) Y ");
                    str.AppendLine(" WHERE A.COD_ALINEA = G.COD_ALINEA AND  ");
                    str.AppendLine("       G.COD_GRUPO = P.COD_GRUPO(+) AND ");
                    str.AppendLine("       P.SEQ_ITEM_PLANO_CONTA = Y.SEQ_ITEM_PLANO_CONTA(+) AND ");
                    str.AppendLine(string.Format("     P.SEQ_PLANO_CONTA(+) = {0} ", idPlanoConta));
                    str.AppendLine(" ORDER BY A.IDF_CLASSE, A.DSC_ALINEA, G.DSC_GRUPO ");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _grupoPlanoContaItem = new Hcrp.Framework.Classes.GrupoPlanoContaItem();

                        if (dr["COD_GRUPO"] != DBNull.Value)
                            _grupoPlanoContaItem.CodGrupo = dr["COD_GRUPO"].ToString();

                        if (dr["GRUPO"] != DBNull.Value)
                            _grupoPlanoContaItem.NomeGrupo = dr["GRUPO"].ToString();

                        if (dr["ALINEA"] != DBNull.Value)
                            _grupoPlanoContaItem.Alinea = dr["ALINEA"].ToString();

                        if (dr["ITEM"] != DBNull.Value)
                            _grupoPlanoContaItem.ItemPlanoConta = dr["ITEM"].ToString();

                        _listaDeRetorno.Add(_grupoPlanoContaItem);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaDeRetorno;
        }

        public List<Hcrp.Framework.Classes.GrupoPlanoContaItem> ObterListaDeGrupoPlanoContaItem(int paginaAtual, out int totalRegistro, string filtroCodAlinea)
        {
            List<Hcrp.Framework.Classes.GrupoPlanoContaItem> _listaDeRetorno = new List<Hcrp.Framework.Classes.GrupoPlanoContaItem>();
            Hcrp.Framework.Classes.GrupoPlanoContaItem _grupoPlanoContaItem = null;
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
                    Int32 numeroRegistroPorPagina = Hcrp.Framework.Infra.Util.Parametrizacao.Instancia().QuantidadeRegistroPagina; /*Ver*/
                    Int32 ultimoIndice = (numeroRegistroPorPagina * paginaAtual);
                    Int32 primeiroIndice = (ultimoIndice - numeroRegistroPorPagina) + 1;

                    if (!string.IsNullOrWhiteSpace(filtroCodAlinea) && !filtroCodAlinea.Equals("0"))
                    {
                        strWhere.AppendLine(string.Format(" AND A.COD_ALINEA = {0} ", filtroCodAlinea.ToUpper()));
                    }

                    str.AppendLine(" SELECT * FROM (SELECT B.*, ROWNUM AS RNUM FROM (SELECT ");
                    str.AppendLine(" A.DSC_ALINEA AS ALINEA, G.COD_GRUPO, G.DSC_GRUPO AS GRUPO, P.SEQ_ITEM_PLANO_CONTA AS ITEM ");
                    str.AppendLine(" FROM ALINEA A, GRUPO G, GRUPO_PLANOCONTAITEM P ");
                    str.AppendLine(" WHERE A.COD_ALINEA = G.COD_ALINEA AND ");
                    str.AppendLine(" G.COD_GRUPO = P.COD_GRUPO(+) ");
                    if (strWhere.Length > 0)
                        str.AppendLine(strWhere.ToString());
                    str.AppendLine(" ORDER BY A.IDF_CLASSE, A.DSC_ALINEA, G.DSC_GRUPO) B ");
                    str.Append(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

                    // Montar query para pegar o total de registros.
                    strTotalRegistro.Append("SELECT COUNT(*) AS TOTAL ");
                    strTotalRegistro.AppendLine(" FROM ALINEA A, GRUPO G ");
                    strTotalRegistro.AppendLine(" WHERE A.COD_ALINEA = G.COD_ALINEA ");
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
                        _grupoPlanoContaItem = new Hcrp.Framework.Classes.GrupoPlanoContaItem();

                        if (dr["COD_GRUPO"] != DBNull.Value)
                            _grupoPlanoContaItem.CodGrupo = dr["COD_GRUPO"].ToString();

                        if (dr["GRUPO"] != DBNull.Value)
                            _grupoPlanoContaItem.NomeGrupo = dr["GRUPO"].ToString();

                        if (dr["ALINEA"] != DBNull.Value)
                            _grupoPlanoContaItem.Alinea = dr["ALINEA"].ToString();

                        if (dr["ITEM"] != DBNull.Value)
                            _grupoPlanoContaItem.ItemPlanoConta = dr["ITEM"].ToString();

                        _listaDeRetorno.Add(_grupoPlanoContaItem);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaDeRetorno;
        }

        /// <summary>
        /// Gravar
        /// </summary>
        /// <param name="_programa"></param>
        public void Gravar(List<Hcrp.Framework.Classes.GrupoPlanoContaItem> _grupoPlanoContaItem)
        {
            Hcrp.Framework.Classes.GrupoPlanoContaItem _grupo = null;

            try
            {
                foreach (Hcrp.Framework.Classes.GrupoPlanoContaItem _item in _grupoPlanoContaItem)
                {
                    using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                    {
                        string seq_plano_conta = _item.PlanoConta;
                        string seq_item_plano_conta = _item.ItemPlanoConta;
                        string cod_grupo = _item.CodGrupo;

                        Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(string.Format("SELECT SEQ_PLANO_CONTA, SEQ_ITEM_PLANO_CONTA, COD_GRUPO FROM GENERICO.GRUPO_PLANOCONTAITEM WHERE COD_GRUPO = '{0}' AND SEQ_PLANO_CONTA = {1}", cod_grupo, seq_plano_conta));

                        // Abre conexão
                        ctx.Open();

                        // Obter registro
                        ctx.ExecuteQuery(query);

                        IDataReader dr = ctx.Reader;

                        while (dr.Read())
                        {
                            _grupo = new Hcrp.Framework.Classes.GrupoPlanoContaItem();

                            if (dr["COD_GRUPO"] != DBNull.Value)
                                _grupo.CodGrupo = dr["COD_GRUPO"].ToString();

                            if (dr["SEQ_PLANO_CONTA"] != DBNull.Value)
                                _grupo.PlanoConta = dr["SEQ_PLANO_CONTA"].ToString();

                            if (dr["SEQ_ITEM_PLANO_CONTA"] != DBNull.Value)
                                _grupo.ItemPlanoConta = dr["SEQ_ITEM_PLANO_CONTA"].ToString();
                        }

                        if (_grupo == null && seq_item_plano_conta != null)
                        {
                            //Inserir

                            Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("GRUPO_PLANOCONTAITEM");

                            // Adicionar parametros
                            comando.Params["SEQ_PLANO_CONTA"] = seq_plano_conta.ToUpper();
                            comando.Params["SEQ_ITEM_PLANO_CONTA"] = seq_item_plano_conta.ToUpper();
                            comando.Params["COD_GRUPO"] = cod_grupo.ToUpper();

                            // Executar o comando
                            ctx.ExecuteInsert(comando);
                        }
                        else if (_grupo != null && _grupo.ItemPlanoConta != seq_item_plano_conta)
                        {
                            if (seq_item_plano_conta == null)
                            {
                                //Remover

                                Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("GRUPO_PLANOCONTAITEM");

                                // Adicionar parametros
                                comando.Params["COD_GRUPO"] = cod_grupo;
                                comando.Params["SEQ_PLANO_CONTA"] = seq_plano_conta;

                                // Executar o comando
                                ctx.ExecuteDelete(comando);
                            }
                            else
                            {
                                //Atualizar

                                Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("GRUPO_PLANOCONTAITEM");

                                // Adicionar parametros
                                comando.FilterParams["COD_GRUPO"] = cod_grupo;
                                comando.FilterParams["SEQ_PLANO_CONTA"] = seq_plano_conta;

                                comando.Params["SEQ_ITEM_PLANO_CONTA"] = seq_item_plano_conta;

                                // Executar o comando
                                ctx.ExecuteUpdate(comando);
                            }
                        }

                        _grupo = null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
