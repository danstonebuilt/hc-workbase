using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class ListaControle
    {
        #region Métodos

        /// <summary>
        /// Obter lista de controle por cod instituto
        /// </summary>
        /// <param name="codInstituto"></param>
        /// <returns></returns>
        public List<Entity.ListaControle> ObterPorInstituto(int codInstituto, string status)
        {
            List<Entity.ListaControle> listaRetorno = new List<Entity.ListaControle>();
            Entity.ListaControle _listaControle = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT ");
                    str.AppendLine("  SEQ_LISTA_CONTROLE, ");
                    str.AppendLine("  NOM_LISTA_CONTROLE, ");
                    str.AppendLine("  IDF_ATIVO, ");
                    str.AppendLine("  IDF_CAIXA_INTUBACAO, ");
                    str.AppendLine("  LC.COD_INSTITUTO, ");
                    str.AppendLine("  DTA_CADASTRO, ");
                    str.AppendLine("  NUM_USER_CADASTRO, ");
                    str.AppendLine("  DTA_ULTIMA_ALTERACAO, ");
                    str.AppendLine("  NUM_USER_ULTIMA_ALTERACAO, ");
                    str.AppendLine("  I.NOM_INSTITUTO ");
                    str.AppendLine(" FROM LISTA_CONTROLE LC, INSTITUTO I ");
                    str.AppendLine(string.Format(" WHERE  LC.COD_INSTITUTO = I.COD_INSTITUTO AND LC.COD_INSTITUTO = {0}", codInstituto));

                    if (status == "S")
                    {
                        str.AppendLine(string.Format(" AND LC.IDF_ATIVO  = '{0}' ", status));    
                    }
                    

                    str.AppendLine("  ORDER BY LC.NOM_LISTA_CONTROLE ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            _listaControle = new Entity.ListaControle();
                            _listaControle.Instituto = new Entity.Instituto();
                            _listaControle.UsuarioCadastro = new Entity.Usuario();
                            _listaControle.UsuarioUltimaAlteracao = new Entity.Usuario();

                            if (dr["SEQ_LISTA_CONTROLE"] != DBNull.Value)
                                _listaControle.SeqListaControle = Convert.ToInt64(dr["SEQ_LISTA_CONTROLE"]);

                            if (dr["NOM_LISTA_CONTROLE"] != DBNull.Value)
                                _listaControle.NomeListaControle = dr["NOM_LISTA_CONTROLE"].ToString();

                            if (dr["IDF_ATIVO"] != DBNull.Value)
                                _listaControle.IdfAtivo = dr["IDF_ATIVO"].ToString();

                            if (dr["IDF_CAIXA_INTUBACAO"] != DBNull.Value)
                                _listaControle.IdfCaixaIntubacao = Convert.ToInt32(dr["IDF_CAIXA_INTUBACAO"]);

                            if (dr["COD_INSTITUTO"] != DBNull.Value)
                                _listaControle.Instituto.CodInstituto = Convert.ToInt32(dr["COD_INSTITUTO"]);

                            if (dr["NOM_INSTITUTO"] != DBNull.Value)
                                _listaControle.Instituto.NomeInstituto = dr["NOM_INSTITUTO"].ToString();

                            if (dr["DTA_CADASTRO"] != DBNull.Value)
                                _listaControle.DataCadastro = Convert.ToDateTime(dr["DTA_CADASTRO"]);

                            if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                                _listaControle.UsuarioCadastro.NumUserBanco = Convert.ToInt32(dr["NUM_USER_CADASTRO"]);

                            if (dr["DTA_ULTIMA_ALTERACAO"] != DBNull.Value)
                                _listaControle.DataUltimaAlteracao = Convert.ToDateTime(dr["DTA_ULTIMA_ALTERACAO"]);

                            if (dr["NUM_USER_ULTIMA_ALTERACAO"] != DBNull.Value)
                                _listaControle.UsuarioUltimaAlteracao.NumUserBanco = Convert.ToInt32(dr["NUM_USER_ULTIMA_ALTERACAO"]);

                            listaRetorno.Add(_listaControle);

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

            return listaRetorno;
        }

        /// <summary>
        /// Obter lista de controle por cod instituto
        /// </summary>
        /// <param name="codInstituto"></param>
        /// <returns></returns>
        public List<Entity.ListaControle> ObterPorInstituto(int codInstituto)
        {
            List<Entity.ListaControle> listaRetorno = new List<Entity.ListaControle>();
            Entity.ListaControle _listaControle = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT ");
                    str.AppendLine("  SEQ_LISTA_CONTROLE, ");
                    str.AppendLine("  NOM_LISTA_CONTROLE, ");
                    str.AppendLine("  IDF_ATIVO, ");
                    str.AppendLine("  IDF_CAIXA_INTUBACAO, ");
                    str.AppendLine("  LC.COD_INSTITUTO, ");
                    str.AppendLine("  DTA_CADASTRO, ");
                    str.AppendLine("  NUM_USER_CADASTRO, ");
                    str.AppendLine("  DTA_ULTIMA_ALTERACAO, ");
                    str.AppendLine("  NUM_USER_ULTIMA_ALTERACAO, ");
                    str.AppendLine("  I.NOM_INSTITUTO ");
                    str.AppendLine(" FROM LISTA_CONTROLE LC, INSTITUTO I ");
                    str.AppendLine(string.Format(" WHERE  LC.COD_INSTITUTO = I.COD_INSTITUTO AND LC.COD_INSTITUTO = {0}", codInstituto));
                    str.AppendLine("  AND LC.IDF_ATIVO = 'S' ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            _listaControle = new Entity.ListaControle();
                            _listaControle.Instituto = new Entity.Instituto();
                            _listaControle.UsuarioCadastro = new Entity.Usuario();
                            _listaControle.UsuarioUltimaAlteracao = new Entity.Usuario();

                            if (dr["SEQ_LISTA_CONTROLE"] != DBNull.Value)
                                _listaControle.SeqListaControle = Convert.ToInt64(dr["SEQ_LISTA_CONTROLE"]);

                            if (dr["NOM_LISTA_CONTROLE"] != DBNull.Value)
                                _listaControle.NomeListaControle = dr["NOM_LISTA_CONTROLE"].ToString();

                            if (dr["IDF_ATIVO"] != DBNull.Value)
                                _listaControle.IdfAtivo = dr["IDF_ATIVO"].ToString();

                            if (dr["IDF_CAIXA_INTUBACAO"] != DBNull.Value)
                                _listaControle.IdfCaixaIntubacao = Convert.ToInt32(dr["IDF_CAIXA_INTUBACAO"]);

                            if (dr["COD_INSTITUTO"] != DBNull.Value)
                                _listaControle.Instituto.CodInstituto = Convert.ToInt32(dr["COD_INSTITUTO"]);

                            if (dr["NOM_INSTITUTO"] != DBNull.Value)
                                _listaControle.Instituto.NomeInstituto = dr["NOM_INSTITUTO"].ToString();

                            if (dr["DTA_CADASTRO"] != DBNull.Value)
                                _listaControle.DataCadastro = Convert.ToDateTime(dr["DTA_CADASTRO"]);

                            if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                                _listaControle.UsuarioCadastro.NumUserBanco = Convert.ToInt32(dr["NUM_USER_CADASTRO"]);

                            if (dr["DTA_ULTIMA_ALTERACAO"] != DBNull.Value)
                                _listaControle.DataUltimaAlteracao = Convert.ToDateTime(dr["DTA_ULTIMA_ALTERACAO"]);

                            if (dr["NUM_USER_ULTIMA_ALTERACAO"] != DBNull.Value)
                                _listaControle.UsuarioUltimaAlteracao.NumUserBanco = Convert.ToInt32(dr["NUM_USER_ULTIMA_ALTERACAO"]);

                            listaRetorno.Add(_listaControle);

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

            return listaRetorno;
        }

        /// <summary>
        /// Adicionar lista de controle
        /// </summary>
        /// <param name="listaControle"></param>
        /// <returns></returns>
        public long Adicionar(Entity.ListaControle listaControle, out bool naoInseriu)
        {
            long _seqRetorno = 0;
            naoInseriu = false;

            try
            {
                if (!this.JahExisteComEstaDescricaoParaEsteInstituto(listaControle.Instituto.CodInstituto, listaControle.NomeListaControle.Trim().ToUpper()))
                {
                    // Criar contexto
                    using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                    {
                        ctx.Open();

                        // Preparar o comando
                        Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("LISTA_CONTROLE");

                        comando.Params["NOM_LISTA_CONTROLE"] = listaControle.NomeListaControle.Trim().ToUpper();
                        comando.Params["IDF_ATIVO"] = "S";
                        comando.Params["IDF_CAIXA_INTUBACAO"] = listaControle.IdfCaixaIntubacao;
                        comando.Params["COD_INSTITUTO"] = listaControle.Instituto.CodInstituto;
                        comando.Params["NUM_USER_CADASTRO"] = listaControle.UsuarioCadastro.NumUserBanco;

                        // Executar o insert
                        ctx.ExecuteInsert(comando);

                        _seqRetorno = Convert.ToInt64(ctx.GetSequenceValue("SEQ_LISTA_CONTROLE", false));
                    }
                }
                else
                    naoInseriu = true;
            }
            catch (Exception)
            {
                throw;
            }

            return _seqRetorno;
        }

        /// <summary>
        /// Ativar ou inativar
        /// </summary>
        /// <param name="ehPraAtivar"></param>
        /// <param name="seqListaControle"></param>
        public void AtivarOuInativar(bool ehPraAtivar, long seqListaControle, long numUsuarioAlteracao)
        {
            try
            {
                // obter o contexto transacionado
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("LISTA_CONTROLE");

                    comando.FilterParams["SEQ_LISTA_CONTROLE"] = seqListaControle;
                    comando.Params["IDF_ATIVO"] = (ehPraAtivar ? "S" : "N");
                    comando.Params["DTA_ULTIMA_ALTERACAO"] = DateTime.Now;
                    comando.Params["NUM_USER_ULTIMA_ALTERACAO"] = numUsuarioAlteracao;

                    // Executar o insert
                    ctx.ExecuteUpdate(comando);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool JahExisteComEstaDescricaoParaEsteInstituto(int codInstituto, string nomeListaControle)
        {
            bool jahExiste = false;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(string.Format("SELECT COUNT(*) TOTAL_ENCONTRADO FROM LISTA_CONTROLE WHERE COD_INSTITUTO = {0} AND TRIM(UPPER(NOM_LISTA_CONTROLE)) = '{1}'",
                                                codInstituto,
                                                nomeListaControle));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            if (dr["TOTAL_ENCONTRADO"] != DBNull.Value)
                                if (Convert.ToInt32(dr["TOTAL_ENCONTRADO"]) > 0)
                                    jahExiste = true;

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

            return jahExiste;
        }

        #endregion
    }
}
