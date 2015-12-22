using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class LacreRepositorioUtilizacao
    {
        #region variáveis / construtor

        internal Hcrp.Infra.AcessoDado.TransacaoDinamica transacao;

        public LacreRepositorioUtilizacao()
        {

        }

        public LacreRepositorioUtilizacao(Hcrp.Infra.AcessoDado.TransacaoDinamica _trans)
        {
            this.transacao = _trans;
        }

        #endregion


        /// <summary>
        /// Atualizar ou adicionar quantidade de utilização por atendimento.
        /// </summary>        
        public void AtualizarOuAdicionarQuantidadeDeUtilizacao(Int64 seqLacreRepositorioItens, int qtdUtilizada, Int64 seqAtendimento, int numUserCadastro, string justificativaSemAtendimento)
        {
            try
            {
                Int64 seqLacreRepUtil = 0;
                
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                #region Obter PK caso exista

                seqLacreRepUtil = new DAL.LacreRepositorioItens().ObterSeqLacrerepositorioUtilPorSeqRespositorioItens_e_SeqAtendimento(ctx, seqLacreRepositorioItens, seqAtendimento);

                #endregion

                #region Atualizar

                if (seqLacreRepUtil > 0)
                {
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comandoupdate = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("LACRE_REPOSIT_UTILIZACAO");

                    comandoupdate.FilterParams["SEQ_LACRE_REPOSIT_UTIL"] = seqLacreRepUtil;

                    comandoupdate.Params["QTD_UTILIZADA"] = qtdUtilizada;

                    if (!string.IsNullOrWhiteSpace(justificativaSemAtendimento))
                        comandoupdate.Params["DSC_JUSTIFICATIVA"] = justificativaSemAtendimento;
                    else
                        comandoupdate.Params["DSC_JUSTIFICATIVA"] = DBNull.Value;

                    // Executar o insert
                    ctx.ExecuteUpdate(comandoupdate);

                    // Obter linhas afetadas
                    Int64 totalDeLinhaAfetadas = ctx.RowsAffected;
                }

                #endregion

                #region Inserir

                if (seqLacreRepUtil == 0)
                {
                    // Se não atualizou, então insere.
                    Hcrp.Infra.AcessoDado.CommandConfig comandoInsert = new Hcrp.Infra.AcessoDado.CommandConfig("LACRE_REPOSIT_UTILIZACAO");

                    if (seqAtendimento > 0)
                        comandoInsert.Params["SEQ_ATENDIMENTO"] = seqAtendimento;
                    
                    comandoInsert.Params["SEQ_LACRE_REPOSITORIO_ITENS"] = seqLacreRepositorioItens;
                    comandoInsert.Params["QTD_UTILIZADA"] = qtdUtilizada;
                    comandoInsert.Params["DTA_CADASTRO"] = DateTime.Now;
                    comandoInsert.Params["NUM_USER_CADASTRO"] = numUserCadastro;
                    
                    if (!string.IsNullOrWhiteSpace(justificativaSemAtendimento))
                        comandoInsert.Params["DSC_JUSTIFICATIVA"] = justificativaSemAtendimento;

                    // Executar o insert
                    ctx.ExecuteInsert(comandoInsert);
                }

                #endregion

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter por seq lacre repositorio itens.
        /// </summary>                
        public List<Entity.LacreRepositorioUtilizacao> ObterPorSeqLacreRepositorioItem(Int64 seqLacreRepositorioItem)
        {
            List<Entity.LacreRepositorioUtilizacao> listaRetorno = new List<Entity.LacreRepositorioUtilizacao>();
            Entity.LacreRepositorioUtilizacao lacreRepUtilizacao = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT U.SEQ_LACRE_REPOSIT_UTIL, ");
                    str.AppendLine("        U.SEQ_ATENDIMENTO, ");
                    str.AppendLine("        U.SEQ_LACRE_REPOSITORIO_ITENS, ");
                    str.AppendLine("        U.QTD_UTILIZADA, ");
                    str.AppendLine("        U.DSC_JUSTIFICATIVA, ");
                    str.AppendLine("        U.DTA_CADASTRO, ");
                    str.AppendLine("        U.NUM_USER_CADASTRO ");
                    str.AppendLine(" FROM LACRE_REPOSIT_UTILIZACAO U ");
                    str.AppendLine(string.Format(" WHERE U.SEQ_LACRE_REPOSITORIO_ITENS = {0} ", seqLacreRepositorioItem));                   

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lacreRepUtilizacao = new Entity.LacreRepositorioUtilizacao();
                            lacreRepUtilizacao.AtendimentoPaciente = new Entity.AtendimentoPaciente();
                            lacreRepUtilizacao.LacreRepositorioItens = new Entity.LacreRepositorioItens();
                            lacreRepUtilizacao.UsuarioCadastro = new Entity.Usuario();

                            if (dr["SEQ_LACRE_REPOSIT_UTIL"] != DBNull.Value)
                                lacreRepUtilizacao.SeqLacreRepositUtil = Convert.ToInt64(dr["SEQ_LACRE_REPOSIT_UTIL"]);

                            if (dr["SEQ_ATENDIMENTO"] != DBNull.Value)
                                lacreRepUtilizacao.AtendimentoPaciente.SeqAtendimento = Convert.ToInt64(dr["SEQ_ATENDIMENTO"]);

                            if (dr["SEQ_LACRE_REPOSITORIO_ITENS"] != DBNull.Value)
                                lacreRepUtilizacao.LacreRepositorioItens.SeqLacreRepositorioItens = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO_ITENS"]);

                            if (dr["QTD_UTILIZADA"] != DBNull.Value)
                                lacreRepUtilizacao.QtdUtilizada = Convert.ToInt32(dr["QTD_UTILIZADA"]);

                            if (dr["DSC_JUSTIFICATIVA"] != DBNull.Value)
                                lacreRepUtilizacao.DscJustificativa = dr["DSC_JUSTIFICATIVA"].ToString();

                            if (dr["DTA_CADASTRO"] != DBNull.Value)
                                lacreRepUtilizacao.DataCadastro = Convert.ToDateTime(dr["DTA_CADASTRO"]);

                            if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                                lacreRepUtilizacao.UsuarioCadastro.NumUserBanco = Convert.ToInt32(dr["NUM_USER_CADASTRO"]);

                            listaRetorno.Add(lacreRepUtilizacao);

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
        /// Obter por seq lacre repositorio itens.
        /// </summary>                
        public List<Entity.LacreRepositorioUtilizacao> ObterConsumoPorLacre(Int32 codInstituto, Int64? seqRepositorio, DateTime? periodoInicial, DateTime? periodoFinal)
        {
            List<Entity.LacreRepositorioUtilizacao> listaRetorno = new List<Entity.LacreRepositorioUtilizacao>();
            Entity.LacreRepositorioUtilizacao lacreRepUtilizacao = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine("  SELECT LR.SEQ_LACRE_REPOSITORIO, ");
                    str.AppendLine("	   LR.NUM_LACRE, ");
                    str.AppendLine("       TPH.NOM_SITUACAO, ");
                    str.AppendLine("       MAT.NOM_MATERIAL, ");
                    str.AppendLine("       LRI.DTA_VALIDADE_LOTE, ");
                    str.AppendLine("       LRI.NUM_LOTE_FABRICANTE, ");
                    str.AppendLine("       LRU.QTD_UTILIZADA, ");
                    str.AppendLine("       LRU.SEQ_ATENDIMENTO, ");
                    str.AppendLine("       LRU.DSC_JUSTIFICATIVA, ");
                    str.AppendLine("       U.NOM_USUARIO_BANCO, ");
                    str.AppendLine("       AP.COD_PACIENTE, ");
                    str.AppendLine("       P.NOM_PACIENTE || ' ' || P.SBN_PACIENTE NOM_PACIENTE");

                    str.AppendLine("  FROM LACRE_REPOSITORIO        LR, ");
                    str.AppendLine("       LACRE_REPOSITORIO_ITENS  LRI, ");
                    str.AppendLine("       LACRE_REPOSIT_UTILIZACAO LRU, ");
                    str.AppendLine("       MATERIAL                 MAT, ");
                    str.AppendLine("       TIPO_SITUACAO_HC         TPH, ");
                    str.AppendLine("       USUARIO                  U, ");
                    str.AppendLine("       ATENDIMENTO_PACIENTE     AP, ");
                    str.AppendLine("       PACIENTE     P ");
                    str.AppendLine(" WHERE LR.SEQ_LACRE_REPOSITORIO = LRI.SEQ_LACRE_REPOSITORIO ");
                    str.AppendLine("   AND LRI.SEQ_LACRE_REPOSITORIO_ITENS = LRU.SEQ_LACRE_REPOSITORIO_ITENS ");
                    str.AppendLine("   AND LR.COD_SITUACAO_ATUAL = TPH.COD_SITUACAO ");
                    str.AppendLine("   AND LRU.NUM_USER_CADASTRO = U.NUM_USER_BANCO ");
                    str.AppendLine("   AND LRI.COD_MATERIAL = MAT.COD_MATERIAL(+) ");
                    str.AppendLine("   AND LRU.SEQ_ATENDIMENTO = AP.SEQ_ATENDIMENTO(+) ");
                    str.AppendLine("   AND AP.COD_PACIENTE = P.COD_PACIENTE(+) ");
                    str.AppendLine(string.Format("   AND LR.SEQ_REPOSITORIO = {0} ", seqRepositorio));
                    str.AppendLine(string.Format("   AND LRU.DTA_CADASTRO BETWEEN TO_DATE('{0}', 'DD/MM/YYYY') AND TO_DATE('{1}', 'DD/MM/YYYY')", periodoInicial.Value.ToString("dd/MM/yyyy"), periodoFinal.Value.ToString("dd/MM/yyyy")));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lacreRepUtilizacao = new Entity.LacreRepositorioUtilizacao();
                            lacreRepUtilizacao.LacreRepositorioItens = new Entity.LacreRepositorioItens();
                            lacreRepUtilizacao.LacreRepositorioItens.Material = new Entity.Material();
                            lacreRepUtilizacao.LacreRepositorioItens.LacreRepositorio = new Entity.LacreRepositorio();
                            lacreRepUtilizacao.LacreRepositorioItens.LacreRepositorio.TipoSituacaoHc = new Entity.TipoSituacaoHc();
                            lacreRepUtilizacao.AtendimentoPaciente = new Entity.AtendimentoPaciente();
                            lacreRepUtilizacao.UsuarioCadastro = new Entity.Usuario();

                            if (dr["SEQ_LACRE_REPOSITORIO"] != DBNull.Value)
                                lacreRepUtilizacao.LacreRepositorioItens.LacreRepositorio.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO"]);

                            if (dr["NUM_LACRE"] != DBNull.Value)
                                lacreRepUtilizacao.LacreRepositorioItens.LacreRepositorio.NumLacre = Convert.ToInt64(dr["NUM_LACRE"]);

                            if (dr["NOM_SITUACAO"] != DBNull.Value)
                                lacreRepUtilizacao.LacreRepositorioItens.LacreRepositorio.TipoSituacaoHc.NomSituacao = dr["NOM_SITUACAO"].ToString();

                            if (dr["NOM_MATERIAL"] != DBNull.Value)
                                lacreRepUtilizacao.LacreRepositorioItens.Material.Nome = dr["NOM_MATERIAL"].ToString();

                            if (dr["DTA_VALIDADE_LOTE"] != DBNull.Value)
                                lacreRepUtilizacao.LacreRepositorioItens.DataValidadeLote = Convert.ToDateTime(dr["DTA_VALIDADE_LOTE"].ToString());

                            if (dr["NUM_LOTE_FABRICANTE"] != DBNull.Value)
                                lacreRepUtilizacao.LacreRepositorioItens.NumLoteFabricante = dr["NUM_LOTE_FABRICANTE"].ToString();

                            if (dr["QTD_UTILIZADA"] != DBNull.Value)
                                lacreRepUtilizacao.QtdUtilizada = Convert.ToInt32(dr["QTD_UTILIZADA"]);

                            if (dr["SEQ_ATENDIMENTO"] != DBNull.Value)
                                lacreRepUtilizacao.AtendimentoPaciente.SeqAtendimento = Convert.ToInt32(dr["SEQ_ATENDIMENTO"]);

                            if (dr["DSC_JUSTIFICATIVA"] != DBNull.Value)
                                lacreRepUtilizacao.DscJustificativa = dr["DSC_JUSTIFICATIVA"].ToString();

                            if (dr["NOM_USUARIO_BANCO"] != DBNull.Value)
                                lacreRepUtilizacao.UsuarioCadastro.NomeAcesso = dr["NOM_USUARIO_BANCO"].ToString();

                            if (dr["NOM_PACIENTE"] != DBNull.Value)
                                lacreRepUtilizacao.AtendimentoPaciente.NomePaciente = dr["COD_PACIENTE"].ToString() + " / " + dr["NOM_PACIENTE"].ToString();

                            listaRetorno.Add(lacreRepUtilizacao);

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
        /// Este método retorna o que foi consumido por seq lacre repositorio
        /// </summary>
        /// <param name="seqLacreRepositorio"></param>
        /// <returns></returns>
        public List<Entity.LacreRepositorioUtilizacao> ObterConsumoPorLacre(Int64 seqLacreRepositorio)
        {
            List<Entity.LacreRepositorioUtilizacao> listaRetorno = new List<Entity.LacreRepositorioUtilizacao>();
            Entity.LacreRepositorioUtilizacao lacreRepUtilizacao = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine("  SELECT LR.SEQ_LACRE_REPOSITORIO, ");
                    str.AppendLine("	   LR.NUM_LACRE, ");
                    str.AppendLine("       TPH.NOM_SITUACAO, ");
                    str.AppendLine("       MAT.NOM_MATERIAL, ");
                    str.AppendLine("       LRI.DTA_VALIDADE_LOTE, ");
                    str.AppendLine("       LRI.NUM_LOTE_FABRICANTE, ");
                    str.AppendLine("       LRU.QTD_UTILIZADA, ");
                    str.AppendLine("       LRU.SEQ_ATENDIMENTO, ");
                    str.AppendLine("       LRU.DSC_JUSTIFICATIVA, ");
                    str.AppendLine("       U.NOM_USUARIO_BANCO, ");
                    str.AppendLine("       MAT.COD_MATERIAL ");
                    str.AppendLine("  FROM LACRE_REPOSITORIO        LR, ");
                    str.AppendLine("       LACRE_REPOSITORIO_ITENS  LRI, ");
                    str.AppendLine("       LACRE_REPOSIT_UTILIZACAO LRU, ");
                    str.AppendLine("       MATERIAL                 MAT, ");
                    str.AppendLine("       TIPO_SITUACAO_HC         TPH, ");
                    str.AppendLine("       USUARIO                  U ");
                    str.AppendLine(" WHERE LR.SEQ_LACRE_REPOSITORIO = LRI.SEQ_LACRE_REPOSITORIO ");
                    str.AppendLine("   AND LRI.SEQ_LACRE_REPOSITORIO_ITENS = LRU.SEQ_LACRE_REPOSITORIO_ITENS ");
                    str.AppendLine("   AND LR.COD_SITUACAO_ATUAL = TPH.COD_SITUACAO ");
                    str.AppendLine("   AND LRU.NUM_USER_CADASTRO = U.NUM_USER_BANCO ");
                    str.AppendLine("   AND LRI.COD_MATERIAL = MAT.COD_MATERIAL(+) ");
                    str.AppendLine(string.Format(" AND LR.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lacreRepUtilizacao = new Entity.LacreRepositorioUtilizacao();
                            lacreRepUtilizacao.LacreRepositorioItens = new Entity.LacreRepositorioItens();
                            lacreRepUtilizacao.LacreRepositorioItens.Material = new Entity.Material();
                            lacreRepUtilizacao.LacreRepositorioItens.LacreRepositorio = new Entity.LacreRepositorio();
                            lacreRepUtilizacao.LacreRepositorioItens.LacreRepositorio.TipoSituacaoHc = new Entity.TipoSituacaoHc();
                            lacreRepUtilizacao.UsuarioCadastro = new Entity.Usuario();

                            if (dr["SEQ_LACRE_REPOSITORIO"] != DBNull.Value)
                                lacreRepUtilizacao.LacreRepositorioItens.LacreRepositorio.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO"]);

                            if (dr["NUM_LACRE"] != DBNull.Value)
                                lacreRepUtilizacao.LacreRepositorioItens.LacreRepositorio.NumLacre = Convert.ToInt64(dr["NUM_LACRE"]);

                            if (dr["NOM_SITUACAO"] != DBNull.Value)
                                lacreRepUtilizacao.LacreRepositorioItens.LacreRepositorio.TipoSituacaoHc.NomSituacao = dr["NOM_SITUACAO"].ToString();

                            if (dr["COD_MATERIAL"] != DBNull.Value)
                                lacreRepUtilizacao.LacreRepositorioItens.Material.Codigo = dr["COD_MATERIAL"].ToString();

                            if (dr["NOM_MATERIAL"] != DBNull.Value)
                                lacreRepUtilizacao.LacreRepositorioItens.Material.Nome = dr["NOM_MATERIAL"].ToString();

                            if (dr["DTA_VALIDADE_LOTE"] != DBNull.Value)
                                lacreRepUtilizacao.LacreRepositorioItens.DataValidadeLote = Convert.ToDateTime(dr["DTA_VALIDADE_LOTE"].ToString());

                            if (dr["NUM_LOTE_FABRICANTE"] != DBNull.Value)
                                lacreRepUtilizacao.LacreRepositorioItens.NumLoteFabricante = dr["NUM_LOTE_FABRICANTE"].ToString();

                            if (dr["QTD_UTILIZADA"] != DBNull.Value)
                                lacreRepUtilizacao.QtdUtilizada = Convert.ToInt32(dr["QTD_UTILIZADA"]);

                            if (dr["DSC_JUSTIFICATIVA"] != DBNull.Value)
                                lacreRepUtilizacao.DscJustificativa = dr["DSC_JUSTIFICATIVA"].ToString();

                            if (dr["NOM_USUARIO_BANCO"] != DBNull.Value)
                                lacreRepUtilizacao.UsuarioCadastro.NomeAcesso = dr["NOM_USUARIO_BANCO"].ToString();

                            listaRetorno.Add(lacreRepUtilizacao);
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

    }
}
