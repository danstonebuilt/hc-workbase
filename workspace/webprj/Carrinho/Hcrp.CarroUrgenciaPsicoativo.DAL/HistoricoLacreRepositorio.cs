using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class HistoricoLacreRepositorio
    {
        #region variáveis / construtor

        internal Hcrp.Infra.AcessoDado.TransacaoDinamica transacao;

        public HistoricoLacreRepositorio()
        {

        }

        public HistoricoLacreRepositorio(Hcrp.Infra.AcessoDado.TransacaoDinamica _trans)
        {
            this.transacao = _trans;
        }

        #endregion
        
        #region Métodos

        /// <summary>
        /// Excluir transacionado.
        /// </summary>
        public void ExcluirPorlacreRepositorioTrans(Int64 seqLacreRepositorio)
        {
            try
            {
                // obter o contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("HISTORICO_LACRE_REPOSITORIO");

                comando.Params["SEQ_LACRE_REPOSITORIO"] = seqLacreRepositorio;

                ctx.ExecuteDelete(comando);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter ultimo registro de historico.
        /// </summary>
        public Entity.HistoricoLacreRepositorio ObterUltimoRegistroDeHistorico(Int64 seqLacreRepositorio)
        {
            Entity.HistoricoLacreRepositorio histLacreRep = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT H.SEQ_LACRE_REPOSITORIO, ");
                    str.AppendLine("    H.COD_SITUACAO, ");
                    str.AppendLine("    H.NUM_USER_CADASTRO, ");
                    str.AppendLine("    H.DTA_CADASTRO, ");
                    str.AppendLine("    H.IDF_CONFERENCIA ");
                    str.AppendLine(" FROM HISTORICO_LACRE_REPOSITORIO H ");
                    str.AppendLine(" WHERE H.DTA_CADASTRO = (SELECT MAX(H1.DTA_CADASTRO) ");
                    str.AppendLine("                        FROM HISTORICO_LACRE_REPOSITORIO H1 ");
                    str.AppendLine("                        WHERE H1.SEQ_LACRE_REPOSITORIO = H.SEQ_LACRE_REPOSITORIO) ");
                    str.AppendLine(string.Format(" AND H.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            histLacreRep = new Entity.HistoricoLacreRepositorio();
                            histLacreRep.TipoSituacaoHc = new Entity.TipoSituacaoHc();
                            histLacreRep.UsuarioCadastro = new Entity.Usuario();
                            histLacreRep.LacreRepositorio = new Entity.LacreRepositorio();

                            if (dr["SEQ_LACRE_REPOSITORIO"] != DBNull.Value)
                                histLacreRep.LacreRepositorio.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO"]);

                            if (dr["COD_SITUACAO"] != DBNull.Value)
                                histLacreRep.TipoSituacaoHc.CodSituacao = Convert.ToInt64(dr["COD_SITUACAO"]);

                            if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                                histLacreRep.UsuarioCadastro.NumUserBanco = Convert.ToInt32(dr["NUM_USER_CADASTRO"]);

                            if (dr["DTA_CADASTRO"] != DBNull.Value)
                                histLacreRep.DataCadastro = Convert.ToDateTime(dr["DTA_CADASTRO"]);

                            if (dr["IDF_CONFERENCIA"] != DBNull.Value)
                                histLacreRep.IdfConferencia = dr["IDF_CONFERENCIA"].ToString();
                           
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

            return histLacreRep;
        }

        /// <summary>
        /// Adicionar.
        /// </summary>        
        public void Adicionar(Entity.HistoricoLacreRepositorio histLacreRep)
        {
            try
            {
                // Criar contexto
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("HISTORICO_LACRE_REPOSITORIO");

                    comando.Params["SEQ_LACRE_REPOSITORIO"] = histLacreRep.LacreRepositorio.SeqLacreRepositorio;
                    comando.Params["COD_SITUACAO"] = histLacreRep.TipoSituacaoHc.CodSituacao;
                    comando.Params["NUM_USER_CADASTRO"] = histLacreRep.UsuarioCadastro.NumUserBanco;
                    comando.Params["DTA_CADASTRO"] = histLacreRep.DataCadastro;
                    comando.Params["IDF_CONFERENCIA"] = histLacreRep.IdfConferencia;

                    // Executar o insert
                    ctx.ExecuteInsert(comando);
                }
            }
            catch (Exception)
            {
                throw;
            }            
        }

        /// <summary>
        /// Adicionar.
        /// </summary>        
        public void AdicionarTrans(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, Entity.HistoricoLacreRepositorio histLacreRep)
        {
            try
            {
                Contexto ctx = transacao.ctx;

                // Preparar o comando
                Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("HISTORICO_LACRE_REPOSITORIO");

                comando.Params["SEQ_LACRE_REPOSITORIO"] = histLacreRep.LacreRepositorio.SeqLacreRepositorio;
                comando.Params["COD_SITUACAO"] = histLacreRep.TipoSituacaoHc.CodSituacao;
                comando.Params["NUM_USER_CADASTRO"] = histLacreRep.UsuarioCadastro.NumUserBanco;
                comando.Params["DTA_CADASTRO"] = histLacreRep.DataCadastro;
                comando.Params["IDF_CONFERENCIA"] = histLacreRep.IdfConferencia;

                // Executar o insert
                ctx.ExecuteInsert(comando);
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter históricos por seq lacre repositorio.
        /// </summary>
        public List<Entity.HistoricoLacreRepositorio> ObterPorSeqLacreRepositorio(Int64 seqLacreRepositorio, bool historicoDeChecagemConferencia)
        {
            List<Entity.HistoricoLacreRepositorio> listHistLacreRep = new List<Entity.HistoricoLacreRepositorio>();
            Entity.HistoricoLacreRepositorio histLacreRep = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT H.SEQ_LACRE_REPOSITORIO, ");
                    str.AppendLine("        H.COD_SITUACAO, ");
                    str.AppendLine("        H.NUM_USER_CADASTRO, ");
                    str.AppendLine("        H.DTA_CADASTRO, ");
                    str.AppendLine("        H.IDF_CONFERENCIA, ");
                    str.AppendLine("        U.NOM_USUARIO ||' '||U.SBN_USUARIO AS RESPONSAVEL, ");
                    str.AppendLine("        T.NOM_SITUACAO ");
                    str.AppendLine(" FROM HISTORICO_LACRE_REPOSITORIO H, ");
                    str.AppendLine("      USUARIO U, ");
                    str.AppendLine("      TIPO_SITUACAO_HC T ");
                    str.AppendLine(string.Format(" WHERE H.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));
                    str.AppendLine("    AND H.NUM_USER_CADASTRO = U.NUM_USER_BANCO ");
                    str.AppendLine("    AND H.COD_SITUACAO = T.COD_SITUACAO ");

                    if (historicoDeChecagemConferencia == true)
                        str.AppendLine(" AND H.IDF_CONFERENCIA = 'S' ");
                    else
                        str.AppendLine(" AND H.IDF_CONFERENCIA = 'N' ");
                    
                    str.AppendLine(" ORDER BY H.DTA_CADASTRO DESC ");                    

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            histLacreRep = new Entity.HistoricoLacreRepositorio();
                            histLacreRep.TipoSituacaoHc = new Entity.TipoSituacaoHc();
                            histLacreRep.UsuarioCadastro = new Entity.Usuario();
                            histLacreRep.LacreRepositorio = new Entity.LacreRepositorio();

                            if (dr["SEQ_LACRE_REPOSITORIO"] != DBNull.Value)
                                histLacreRep.LacreRepositorio.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO"]);

                            if (dr["COD_SITUACAO"] != DBNull.Value)
                                histLacreRep.TipoSituacaoHc.CodSituacao = Convert.ToInt64(dr["COD_SITUACAO"]);

                            if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                                histLacreRep.UsuarioCadastro.NumUserBanco = Convert.ToInt32(dr["NUM_USER_CADASTRO"]);

                            if (dr["DTA_CADASTRO"] != DBNull.Value)
                                histLacreRep.DataCadastro = Convert.ToDateTime(dr["DTA_CADASTRO"]);

                            if (dr["IDF_CONFERENCIA"] != DBNull.Value)
                                histLacreRep.IdfConferencia = dr["IDF_CONFERENCIA"].ToString();

                            if (dr["RESPONSAVEL"] != DBNull.Value)
                                histLacreRep.UsuarioCadastro.Nome = dr["RESPONSAVEL"].ToString();

                            if (dr["NOM_SITUACAO"] != DBNull.Value)
                                histLacreRep.TipoSituacaoHc.NomSituacao = dr["NOM_SITUACAO"].ToString();

                            listHistLacreRep.Add(histLacreRep);
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

            return listHistLacreRep;
        }

        #endregion
    }
}
