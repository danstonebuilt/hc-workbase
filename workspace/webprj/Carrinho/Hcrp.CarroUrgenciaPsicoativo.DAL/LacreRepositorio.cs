using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.CarroUrgenciaPsicoativo.Entity;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class LacreRepositorio
    {
        #region variáveis / construtor

        internal Hcrp.Infra.AcessoDado.TransacaoDinamica transacao;

        public LacreRepositorio()
        {

        }

        public LacreRepositorio(Hcrp.Infra.AcessoDado.TransacaoDinamica _trans)
        {
            this.transacao = _trans;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método que retorna o id do ultimo lacre do repositório
        /// </summary>
        /// <param name="seqRepositorio">id repositório</param>
        /// <returns></returns>
        public Int64? ObterUltimoLacreRepositorio(Int64? seqRepositorio)
        {          
            StringBuilder str = new StringBuilder();
            Int64? retorno = null;

            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                QueryCommandConfig query;

                // Abrir conexão
                ctx.Open();

                str.AppendLine(string.Format(" SELECT MAX(A.SEQ_LACRE_REPOSITORIO) SEQ_LACRE_REPOSITORIO FROM  LACRE_REPOSITORIO A WHERE A.SEQ_REPOSITORIO = {0} ",seqRepositorio));

                query = new QueryCommandConfig(str.ToString());

                // Obter a lista de registros
                ctx.ExecuteQuery(query);

                IDataReader dr = ctx.Reader;

                try
                {
                    while (dr.Read())
                    {
                        retorno = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO"]);
                    }
                }
                finally
                {
                    dr.Close();
                    dr.Dispose();
                }
            }

            return retorno;
        }

        /// <summary>
        /// Obter por repositório.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio> ObterPorFiltro(Int64? seqLacreRepositorio,
                                                                                         Int64? seqRepositorio,
                                                                                         DateTime? periodoInicial,
                                                                                         DateTime? periodoFinal,
                                                                                         Int64 codSituacaoLacracao)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio> listLacreRep = new List<Entity.LacreRepositorio>();
            Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio lacreRep = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT ");
                    str.AppendLine("    LR.SEQ_LACRE_REPOSITORIO, ");
                    str.AppendLine("    LR.SEQ_REPOSITORIO, ");
                    str.AppendLine("    LR.NUM_USER_CADASTRO, ");
                    str.AppendLine("    LR.NUM_USER_ULTIMA_ALTERACAO, ");
                    str.AppendLine("    LR.DTA_CADASTRO, ");
                    str.AppendLine("    LR.DTA_ULTIMA_ALTERACAO, ");
                    str.AppendLine("    LR.NUM_LACRE, ");
                    str.AppendLine("    LR.NUM_CAIXA_INTUBACAO, ");
                    str.AppendLine("    LR.COD_SITUACAO_ATUAL, ");
                    str.AppendLine("    LR.SEQ_LACRE_TIP_OCORRENCIA," +
                                   "    LC.SEQ_LISTA_CONTROLE, " +
                                   "    LC.IDF_CAIXA_INTUBACAO, ");

                    str.AppendLine("    (SELECT H.DTA_CADASTRO ");
                    str.AppendLine("        FROM HISTORICO_LACRE_REPOSITORIO H ");
                    str.AppendLine("        WHERE H.SEQ_LACRE_REPOSITORIO = LR.SEQ_LACRE_REPOSITORIO ");
                    str.AppendLine(string.Format(" AND H.COD_SITUACAO = {0} AND ROWNUM = 1 AND H.IDF_CONFERENCIA = 'N') AS DATA_LACRACAO, ", codSituacaoLacracao));

                    str.AppendLine("    (SELECT RESPLACRACAO.NOM_USUARIO || ' ' || RESPLACRACAO.SBN_USUARIO ");
                    str.AppendLine("        FROM HISTORICO_LACRE_REPOSITORIO H, ");
                    str.AppendLine("        USUARIO RESPLACRACAO ");
                    str.AppendLine("        WHERE RESPLACRACAO.NUM_USER_BANCO = H.NUM_USER_CADASTRO ");
                    str.AppendLine("        AND H.SEQ_LACRE_REPOSITORIO = LR.SEQ_LACRE_REPOSITORIO ");
                    str.AppendLine(string.Format(" AND H.COD_SITUACAO = {0} AND ROWNUM = 1 AND H.IDF_CONFERENCIA = 'N') AS RESPONSAVEL_LACRACAO, ", codSituacaoLacracao));

                    str.AppendLine(" SIT.NOM_SITUACAO, ");

                    str.AppendLine("    (SELECT H.DTA_CADASTRO ");
                    str.AppendLine("        FROM HISTORICO_LACRE_REPOSITORIO H ");
                    str.AppendLine("        WHERE H.SEQ_LACRE_REPOSITORIO = LR.SEQ_LACRE_REPOSITORIO ");
                    str.AppendLine("        AND H.COD_SITUACAO = LR.COD_SITUACAO_ATUAL AND ROWNUM = 1 AND H.IDF_CONFERENCIA = 'N') AS DATA_SITUACAO, ");

                    str.AppendLine("    LTO.DSC_TIPO_OCORRENCIA, ");

                    str.AppendLine("    CASE WHEN EXISTS (SELECT 1 FROM LACRE_REPOSITORIO_ITENS LRI ");
                    str.AppendLine("         WHERE LRI.SEQ_LACRE_REPOSITORIO = LR.SEQ_LACRE_REPOSITORIO) THEN 'S' ");
                    str.AppendLine("    ELSE 'N' END AS EXISTE_MAT_LANCADO, ");

                    str.AppendLine("    INST.NOM_INSTITUTO, ");
                    str.AppendLine("    LC.NOM_LISTA_CONTROLE, ");
                    str.AppendLine("    RLC.DSC_IDENTIFICACAO ");

                    str.AppendLine(" FROM LACRE_REPOSITORIO LR, ");
                    str.AppendLine("    TIPO_SITUACAO_HC SIT, ");
                    str.AppendLine("    LACRE_TIPO_OCORRENCIA LTO, ");
                    str.AppendLine("    REPOSITORIO_LISTA_CONTROLE RLC, ");
                    str.AppendLine("    LISTA_CONTROLE LC, ");
                    str.AppendLine("    INSTITUTO INST ");
                    str.AppendLine(" WHERE LR.COD_SITUACAO_ATUAL = SIT.COD_SITUACAO AND ");
                    str.AppendLine("    LR.SEQ_LACRE_TIP_OCORRENCIA = LTO.SEQ_LACRE_TIP_OCORRENCIA AND ");
                    str.AppendLine("    LR.SEQ_REPOSITORIO = RLC.SEQ_REPOSITORIO AND ");
                    str.AppendLine("    RLC.SEQ_LISTA_CONTROLE = LC.SEQ_LISTA_CONTROLE AND ");
                    str.AppendLine("    LC.COD_INSTITUTO = INST.COD_INSTITUTO ");

                    if (seqLacreRepositorio != null)
                        str.AppendLine(string.Format(" AND LR.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio.Value));

                    if (seqRepositorio != null)
                        str.AppendLine(string.Format(" AND LR.SEQ_REPOSITORIO = {0} ", seqRepositorio.Value));

                    if (periodoInicial != null && periodoFinal != null)
                    {
                        str.AppendLine(" AND ");
                        str.AppendLine(string.Format(" LR.DTA_CADASTRO BETWEEN TO_DATE('{0}', 'DD/MM/YYYY') AND TO_DATE('{1}', 'DD/MM/YYYY HH24:MI:SS') ", periodoInicial.Value.ToString("dd/MM/yyyy"), periodoFinal.Value.ToString("dd/MM/yyyy")));
                    }

                    str.AppendLine(" ORDER BY LR.DTA_CADASTRO ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lacreRep = new Entity.LacreRepositorio();

                            lacreRep.RepositorioListaControle = new Entity.RepositorioListaControle();
                            lacreRep.RepositorioListaControle.ListaControle = new Entity.ListaControle();
                            lacreRep.RepositorioListaControle.ListaControle.Instituto = new Entity.Instituto();

                            lacreRep.UsuarioCadastro = new Entity.Usuario();
                            lacreRep.UsuarioUltimaAlteracao = new Entity.Usuario();
                            lacreRep.TipoSituacaoHc = new Entity.TipoSituacaoHc();
                            lacreRep.LacreTipoOcorrencia = new Entity.LacreTipoOcorrencia();
                            lacreRep.UsuarioResponsavelLacracao = new Entity.Usuario();
                            lacreRep.LacreTipoOcorrencia = new Entity.LacreTipoOcorrencia();

                            if (dr["SEQ_LACRE_REPOSITORIO"] != DBNull.Value)
                                lacreRep.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO"]);

                            if (dr["SEQ_REPOSITORIO"] != DBNull.Value)
                                lacreRep.RepositorioListaControle.SeqRepositorio = Convert.ToInt64(dr["SEQ_REPOSITORIO"]);

                            if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                                lacreRep.UsuarioCadastro.NumUserBanco = Convert.ToInt32(dr["NUM_USER_CADASTRO"]);

                            if (dr["NUM_USER_ULTIMA_ALTERACAO"] != DBNull.Value)
                                lacreRep.UsuarioUltimaAlteracao.NumUserBanco = Convert.ToInt32(dr["NUM_USER_ULTIMA_ALTERACAO"]);

                            if (dr["DTA_CADASTRO"] != DBNull.Value)
                                lacreRep.DataCadastro = Convert.ToDateTime(dr["DTA_CADASTRO"]);

                            if (dr["DTA_ULTIMA_ALTERACAO"] != DBNull.Value)
                                lacreRep.DataHoraUltimaAlteracao = Convert.ToDateTime(dr["DTA_ULTIMA_ALTERACAO"]);

                            if (dr["NUM_LACRE"] != DBNull.Value)
                                lacreRep.NumLacre = Convert.ToInt64(dr["NUM_LACRE"]);

                            if (dr["NUM_CAIXA_INTUBACAO"] != DBNull.Value)
                                lacreRep.NumCaixaIntubacao = dr["NUM_CAIXA_INTUBACAO"].ToString();

                            if (dr["COD_SITUACAO_ATUAL"] != DBNull.Value)
                                lacreRep.TipoSituacaoHc.CodSituacao = Convert.ToInt64(dr["COD_SITUACAO_ATUAL"]);

                            if (dr["SEQ_LACRE_TIP_OCORRENCIA"] != DBNull.Value)
                                lacreRep.LacreTipoOcorrencia.SeqLacreTipoOCorrencia = Convert.ToInt64(dr["SEQ_LACRE_TIP_OCORRENCIA"]);

                            if (dr["DATA_LACRACAO"] != DBNull.Value)
                                lacreRep.DataLacracao = Convert.ToDateTime(dr["DATA_LACRACAO"]);

                            if (dr["RESPONSAVEL_LACRACAO"] != DBNull.Value)
                                lacreRep.UsuarioResponsavelLacracao.Nome = dr["RESPONSAVEL_LACRACAO"].ToString();

                            if (dr["NOM_SITUACAO"] != DBNull.Value)
                                lacreRep.TipoSituacaoHc.NomSituacao = dr["NOM_SITUACAO"].ToString();

                            if (dr["DATA_SITUACAO"] != DBNull.Value)
                                lacreRep.DataDaSituacao = Convert.ToDateTime(dr["DATA_SITUACAO"]);

                            if (dr["DSC_TIPO_OCORRENCIA"] != DBNull.Value)
                                lacreRep.LacreTipoOcorrencia.DscTipoOcorrencia = dr["DSC_TIPO_OCORRENCIA"].ToString();

                            if (dr["EXISTE_MAT_LANCADO"] != DBNull.Value)
                                lacreRep.ExisteLancamentoDeMaterial = dr["EXISTE_MAT_LANCADO"].ToString();

                            if (dr["NOM_INSTITUTO"] != DBNull.Value)
                                lacreRep.RepositorioListaControle.ListaControle.Instituto.NomeInstituto = dr["NOM_INSTITUTO"].ToString();

                            if (dr["NOM_LISTA_CONTROLE"] != DBNull.Value)
                                lacreRep.RepositorioListaControle.ListaControle.NomeListaControle = dr["NOM_LISTA_CONTROLE"].ToString();

                            if (dr["IDF_CAIXA_INTUBACAO"] != DBNull.Value)
                                lacreRep.RepositorioListaControle.ListaControle.IdfCaixaIntubacao = Convert.ToInt32(dr["IDF_CAIXA_INTUBACAO"].ToString());

                            if (dr["SEQ_LISTA_CONTROLE"] != DBNull.Value)
                                lacreRep.RepositorioListaControle.ListaControle.SeqListaControle = Convert.ToInt64(dr["SEQ_LISTA_CONTROLE"].ToString());

                            if (dr["NOM_LISTA_CONTROLE"] != DBNull.Value)
                                lacreRep.RepositorioListaControle.DscIdentificacao = dr["DSC_IDENTIFICACAO"].ToString();

                            listLacreRep.Add(lacreRep);

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

            return listLacreRep;
        }

        /// <summary>
        /// Salvar.
        /// </summary>        
        public Int64 Adicionar(Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio lacreRepositorio)
        {
            Int64 seqGerada = 0;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    Hcrp.Infra.AcessoDado.CommandConfig comandoInsert = new Hcrp.Infra.AcessoDado.CommandConfig("LACRE_REPOSITORIO");

                    if (lacreRepositorio.RepositorioListaControle != null && lacreRepositorio.RepositorioListaControle.SeqRepositorio > 0)
                        comandoInsert.Params["SEQ_REPOSITORIO"] = lacreRepositorio.RepositorioListaControle.SeqRepositorio;

                    if (lacreRepositorio.UsuarioCadastro != null && lacreRepositorio.UsuarioCadastro.NumUserBanco > 0)
                        comandoInsert.Params["NUM_USER_CADASTRO"] = lacreRepositorio.UsuarioCadastro.NumUserBanco;

                    comandoInsert.Params["DTA_CADASTRO"] = lacreRepositorio.DataCadastro;

                    if (lacreRepositorio.TipoSituacaoHc != null && lacreRepositorio.TipoSituacaoHc.CodSituacao > 0)
                        comandoInsert.Params["COD_SITUACAO_ATUAL"] = lacreRepositorio.TipoSituacaoHc.CodSituacao;

                    if (lacreRepositorio.LacreTipoOcorrencia != null && lacreRepositorio.LacreTipoOcorrencia.SeqLacreTipoOCorrencia > 0)
                        comandoInsert.Params["SEQ_LACRE_TIP_OCORRENCIA"] = lacreRepositorio.LacreTipoOcorrencia.SeqLacreTipoOCorrencia;

                    // Executar o insert
                    ctx.ExecuteInsert(comandoInsert);

                    seqGerada = Convert.ToInt64(ctx.GetSequenceValue("SEQ_LACRE_REPOSITORIO", false));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return seqGerada;
        }

        /// <summary>
        /// Obter por filtro paginado.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio> ObterPorFiltroPaginado(Int64? seqLacreRepositorio,
                                                                                                 Int64? seqRepositorio,
                                                                                                 DateTime? periodoInicial,
                                                                                                 DateTime? periodoFinal,
                                                                                                 Int64 codSituacaoLacracao,
                                                                                                 int qtdRegistroPagina,
                                                                                                 int paginaAtual,
                                                                                                 out int totalRegistro)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio> listLacreRep = new List<Entity.LacreRepositorio>();
            Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio lacreRep = null;
            totalRegistro = 0;

            try
            {
                StringBuilder str = new StringBuilder();
                StringBuilder strTotalRegistro = new StringBuilder();
                StringBuilder strWhere = new StringBuilder();

                // Montar escopo de paginação.
                Int32 numeroRegistroPorPagina = qtdRegistroPagina;
                Int32 ultimoIndice = (numeroRegistroPorPagina * paginaAtual);
                Int32 primeiroIndice = (ultimoIndice - numeroRegistroPorPagina) + 1;

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;
                    QueryCommandConfig queryCount;

                    #region query where
                    strWhere.AppendLine(" WHERE LR.COD_SITUACAO_ATUAL = SIT.COD_SITUACAO AND ");
                    strWhere.AppendLine("    LR.SEQ_LACRE_TIP_OCORRENCIA = LTO.SEQ_LACRE_TIP_OCORRENCIA ");

                    if (seqLacreRepositorio != null)
                        strWhere.AppendLine(string.Format(" AND LR.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio.Value));

                    if (seqRepositorio != null)
                        strWhere.AppendLine(string.Format(" AND LR.SEQ_REPOSITORIO = {0} ", seqRepositorio.Value));

                    //if (periodoInicial != null && periodoFinal != null)
                    //{
                    //    strWhere.AppendLine(" AND ");
                    //    strWhere.AppendLine(string.Format(" LR.DTA_CADASTRO BETWEEN TO_DATE('{0}', 'DD/MM/YYYY') AND TO_DATE('{1}', 'DD/MM/YYYY HH24:MI:SS') ", periodoInicial.Value.ToString("dd/MM/yyyy"), periodoFinal.Value.ToString("dd/MM/yyyy HH:mm:ss")));
                    //}
                    #endregion

                    // Abrir conexão
                    ctx.Open();

                    #region query principal

                    str.AppendLine(" SELECT *");
                    str.AppendLine(" FROM (SELECT A.*,");
                    str.AppendLine("              ROWNUM AS RNUM FROM( ");

                    str.AppendLine(" SELECT ");
                    str.AppendLine("    LR.SEQ_LACRE_REPOSITORIO, ");
                    str.AppendLine("    LR.SEQ_REPOSITORIO, ");
                    str.AppendLine("    LR.NUM_USER_CADASTRO, ");
                    str.AppendLine("    LR.NUM_USER_ULTIMA_ALTERACAO, ");
                    str.AppendLine("    LR.DTA_CADASTRO, ");
                    str.AppendLine("    LR.DTA_ULTIMA_ALTERACAO, ");
                    str.AppendLine("    LR.NUM_LACRE, ");
                    str.AppendLine("    LR.NUM_CAIXA_INTUBACAO, ");
                    str.AppendLine("    LR.COD_SITUACAO_ATUAL, ");
                    str.AppendLine("    LR.SEQ_LACRE_TIP_OCORRENCIA, ");

                    str.AppendLine("    (SELECT H.DTA_CADASTRO ");
                    str.AppendLine("        FROM HISTORICO_LACRE_REPOSITORIO H ");
                    str.AppendLine("        WHERE H.SEQ_LACRE_REPOSITORIO = LR.SEQ_LACRE_REPOSITORIO ");
                    str.AppendLine(string.Format(" AND H.COD_SITUACAO = {0} AND ROWNUM = 1 AND H.IDF_CONFERENCIA = 'N') AS DATA_LACRACAO, ", codSituacaoLacracao));

                    str.AppendLine("    (SELECT RESPLACRACAO.NOM_USUARIO || ' ' || RESPLACRACAO.SBN_USUARIO ");
                    str.AppendLine("        FROM HISTORICO_LACRE_REPOSITORIO H, ");
                    str.AppendLine("        USUARIO RESPLACRACAO ");
                    str.AppendLine("        WHERE RESPLACRACAO.NUM_USER_BANCO = H.NUM_USER_CADASTRO ");
                    str.AppendLine("        AND H.SEQ_LACRE_REPOSITORIO = LR.SEQ_LACRE_REPOSITORIO ");
                    str.AppendLine(string.Format(" AND H.COD_SITUACAO = {0} AND ROWNUM = 1 AND H.IDF_CONFERENCIA = 'N') AS RESPONSAVEL_LACRACAO, ", codSituacaoLacracao));

                    str.AppendLine(" SIT.NOM_SITUACAO, ");

                    //str.AppendLine("    (SELECT H.DTA_CADASTRO ");
                    //str.AppendLine("        FROM HISTORICO_LACRE_REPOSITORIO H ");
                    //str.AppendLine("        WHERE H.SEQ_LACRE_REPOSITORIO = LR.SEQ_LACRE_REPOSITORIO ");
                    //str.AppendLine("        AND H.COD_SITUACAO = LR.COD_SITUACAO_ATUAL AND ROWNUM = 1 AND H.IDF_CONFERENCIA = 'N' order by H.dta_cadastro asc  ) AS DATA_SITUACAO, ");

                    str.AppendLine("    (SELECT MAX(H.DTA_CADASTRO) ");
                    str.AppendLine("        FROM HISTORICO_LACRE_REPOSITORIO H ");
                    str.AppendLine("        WHERE H.SEQ_LACRE_REPOSITORIO = LR.SEQ_LACRE_REPOSITORIO ");
                    str.AppendLine("        AND H.COD_SITUACAO = LR.COD_SITUACAO_ATUAL AND H.IDF_CONFERENCIA = 'N' ) AS DATA_SITUACAO, ");

                    str.AppendLine("    LTO.DSC_TIPO_OCORRENCIA ");
                    str.AppendLine(" FROM LACRE_REPOSITORIO LR, ");
                    str.AppendLine("      TIPO_SITUACAO_HC SIT, ");
                    str.AppendLine("      LACRE_TIPO_OCORRENCIA LTO ");

                    str.AppendLine(strWhere.ToString());
                    str.AppendLine("  ORDER BY LR.DTA_CADASTRO desc) A ");
                    str.AppendLine(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");
                    #endregion

                    #region query count
                    strTotalRegistro.AppendLine(" SELECT COUNT(*) TOTAL ");
                    strTotalRegistro.AppendLine(" FROM LACRE_REPOSITORIO LR, ");
                    strTotalRegistro.AppendLine("      TIPO_SITUACAO_HC SIT, ");
                    strTotalRegistro.AppendLine("      LACRE_TIPO_OCORRENCIA LTO ");
                    strTotalRegistro.AppendLine(strWhere.ToString());
                    #endregion

                    query = new QueryCommandConfig(str.ToString());
                    queryCount = new Hcrp.Infra.AcessoDado.QueryCommandConfig(strTotalRegistro.ToString());

                    // Veriricar contador
                    ctx.ExecuteQuery(queryCount);

                    while (ctx.Reader.Read())
                    {
                        totalRegistro = Convert.ToInt32(ctx.Reader["TOTAL"]);
                        break;
                    }

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lacreRep = new Entity.LacreRepositorio();
                            lacreRep.RepositorioListaControle = new Entity.RepositorioListaControle();
                            lacreRep.UsuarioCadastro = new Entity.Usuario();
                            lacreRep.UsuarioUltimaAlteracao = new Entity.Usuario();
                            lacreRep.TipoSituacaoHc = new Entity.TipoSituacaoHc();
                            lacreRep.LacreTipoOcorrencia = new Entity.LacreTipoOcorrencia();
                            lacreRep.UsuarioResponsavelLacracao = new Entity.Usuario();
                            lacreRep.LacreTipoOcorrencia = new Entity.LacreTipoOcorrencia();

                            if (dr["SEQ_LACRE_REPOSITORIO"] != DBNull.Value)
                                lacreRep.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO"]);

                            if (dr["SEQ_REPOSITORIO"] != DBNull.Value)
                                lacreRep.RepositorioListaControle.SeqRepositorio = Convert.ToInt64(dr["SEQ_REPOSITORIO"]);

                            if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                                lacreRep.UsuarioCadastro.NumUserBanco = Convert.ToInt32(dr["NUM_USER_CADASTRO"]);

                            if (dr["NUM_USER_ULTIMA_ALTERACAO"] != DBNull.Value)
                                lacreRep.UsuarioUltimaAlteracao.NumUserBanco = Convert.ToInt32(dr["NUM_USER_ULTIMA_ALTERACAO"]);

                            if (dr["DTA_CADASTRO"] != DBNull.Value)
                                lacreRep.DataCadastro = Convert.ToDateTime(dr["DTA_CADASTRO"]);

                            if (dr["DTA_ULTIMA_ALTERACAO"] != DBNull.Value)
                                lacreRep.DataHoraUltimaAlteracao = Convert.ToDateTime(dr["DTA_ULTIMA_ALTERACAO"]);

                            if (dr["NUM_LACRE"] != DBNull.Value)
                                lacreRep.NumLacre = Convert.ToInt64(dr["NUM_LACRE"]);

                            if (dr["NUM_CAIXA_INTUBACAO"] != DBNull.Value)
                                lacreRep.NumCaixaIntubacao = dr["NUM_CAIXA_INTUBACAO"].ToString();

                            if (dr["COD_SITUACAO_ATUAL"] != DBNull.Value)
                                lacreRep.TipoSituacaoHc.CodSituacao = Convert.ToInt64(dr["COD_SITUACAO_ATUAL"]);

                            if (dr["SEQ_LACRE_TIP_OCORRENCIA"] != DBNull.Value)
                                lacreRep.LacreTipoOcorrencia.SeqLacreTipoOCorrencia = Convert.ToInt64(dr["SEQ_LACRE_TIP_OCORRENCIA"]);

                            if (dr["DATA_LACRACAO"] != DBNull.Value)
                                lacreRep.DataLacracao = Convert.ToDateTime(dr["DATA_LACRACAO"]);

                            if (dr["RESPONSAVEL_LACRACAO"] != DBNull.Value)
                                lacreRep.UsuarioResponsavelLacracao.Nome = dr["RESPONSAVEL_LACRACAO"].ToString();

                            if (dr["NOM_SITUACAO"] != DBNull.Value)
                                lacreRep.TipoSituacaoHc.NomSituacao = dr["NOM_SITUACAO"].ToString();

                            if (dr["DATA_SITUACAO"] != DBNull.Value)
                                lacreRep.DataDaSituacao = Convert.ToDateTime(dr["DATA_SITUACAO"]);

                            if (dr["DSC_TIPO_OCORRENCIA"] != DBNull.Value)
                                lacreRep.LacreTipoOcorrencia.DscTipoOcorrencia = dr["DSC_TIPO_OCORRENCIA"].ToString();

                            listLacreRep.Add(lacreRep);
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

            return listLacreRep;
        }

        /// <summary>
        /// Excluir transacionado.
        /// </summary>
        public void ExcluirTrans(Int64 seqLacreRepositorio)
        {
            try
            {
                // obter o contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("LACRE_REPOSITORIO");

                comando.Params["SEQ_LACRE_REPOSITORIO"] = seqLacreRepositorio;

                ctx.ExecuteDelete(comando);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lacrar carrinho.
        /// </summary>        
        public void LacrarCarrinho(Int64 seqLacreRepositorio, Int64 numLacre, string numCaixaIntubacao, DateTime? dataAtualizacao, Int64 codSituacaoLacrado)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comandoDataValidadeLote = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("LACRE_REPOSITORIO");

                    comandoDataValidadeLote.FilterParams["SEQ_LACRE_REPOSITORIO"] = seqLacreRepositorio;

                    comandoDataValidadeLote.Params["NUM_LACRE"] = numLacre;

                    //if (numCaixaIntubacao != null)
                    //    comandoDataValidadeLote.Params["NUM_CAIXA_INTUBACAO"] = numCaixaIntubacao;

                    comandoDataValidadeLote.Params["COD_SITUACAO_ATUAL"] = codSituacaoLacrado;

                    if (dataAtualizacao != null)
                        comandoDataValidadeLote.Params["DTA_ULTIMA_ALTERACAO"] = dataAtualizacao.Value;

                    // Executar o update
                    ctx.ExecuteUpdate(comandoDataValidadeLote);


                    Entity.LacreRepositorio lacreRep = new Entity.LacreRepositorio()
                                                           {
                                                               SeqLacreRepositorio = seqLacreRepositorio
                                                           };

                    if (lacreRep != null)
                    {
                        Entity.HistoricoLacreRepositorio histLacreRep = new Entity.HistoricoLacreRepositorio();

                        histLacreRep.LacreRepositorio = lacreRep;
                        histLacreRep.DataCadastro = DateTime.Now;

                        histLacreRep.IdfConferencia = "N";

                        Entity.TipoSituacaoHc sit = new TipoSituacaoHc()
                                                        {
                                                            CodSituacao = codSituacaoLacrado
                                                        };

                        histLacreRep.TipoSituacaoHc = sit;

                        histLacreRep.UsuarioCadastro = new Entity.Usuario();

                        histLacreRep.UsuarioCadastro.NumUserBanco = Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;

                        new DAL.HistoricoLacreRepositorio().Adicionar(histLacreRep);
                    }
                    
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter por id.
        /// </summary>        
        public Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio ObterPorId(Int64 seqLacreRepositorio)
        {
            Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio lacreRep = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT ");
                    str.AppendLine("    LR.SEQ_LACRE_REPOSITORIO, ");
                    str.AppendLine("    LR.SEQ_REPOSITORIO, ");
                    str.AppendLine("    LR.NUM_USER_CADASTRO, ");
                    str.AppendLine("    LR.NUM_USER_ULTIMA_ALTERACAO, ");
                    str.AppendLine("    LR.DTA_CADASTRO, ");
                    str.AppendLine("    LR.DTA_ULTIMA_ALTERACAO, ");
                    str.AppendLine("    LR.NUM_LACRE, ");
                    str.AppendLine("    LR.NUM_CAIXA_INTUBACAO, ");
                    str.AppendLine("    LR.COD_SITUACAO_ATUAL, ");
                    str.AppendLine("    LR.SEQ_LACRE_TIP_OCORRENCIA, ");
                    str.AppendLine("    SIT.NOM_SITUACAO, ");
                    str.AppendLine("    LTO.DSC_TIPO_OCORRENCIA, ");
                    str.AppendLine("    TPLIS.DSC_TIPO_REPOSITORIO, ");
                    str.AppendLine("    RLC.DSC_IDENTIFICACAO AS DSC_REPOSITORIO ");

                    str.AppendLine(" FROM LACRE_REPOSITORIO LR, ");
                    str.AppendLine("    TIPO_SITUACAO_HC SIT, ");
                    str.AppendLine("    LACRE_TIPO_OCORRENCIA LTO, ");
                    str.AppendLine("    REPOSITORIO_LISTA_CONTROLE RLC, ");
                    str.AppendLine("    TIPO_REPOSITORIO_LST_CONTROLE TPLIS ");

                    str.AppendLine(" WHERE LR.COD_SITUACAO_ATUAL = SIT.COD_SITUACAO AND  ");
                    str.AppendLine("    LR.SEQ_LACRE_TIP_OCORRENCIA = LTO.SEQ_LACRE_TIP_OCORRENCIA AND ");
                    str.AppendLine("    LR.SEQ_REPOSITORIO = RLC.SEQ_REPOSITORIO AND ");
                    str.AppendLine("    RLC.SEQ_TIP_REPOSIT_LST_CONTROL = TPLIS.SEQ_TIP_REPOSIT_LST_CONTROL AND ");
                    str.AppendLine(string.Format(" LR.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lacreRep = new Entity.LacreRepositorio();
                            lacreRep.RepositorioListaControle = new Entity.RepositorioListaControle();
                            lacreRep.UsuarioCadastro = new Entity.Usuario();
                            lacreRep.UsuarioUltimaAlteracao = new Entity.Usuario();
                            lacreRep.TipoSituacaoHc = new Entity.TipoSituacaoHc();
                            lacreRep.LacreTipoOcorrencia = new Entity.LacreTipoOcorrencia();
                            lacreRep.RepositorioListaControle.TipoRepositorioListaControle = new Entity.TipoRepositorioListaControle();


                            if (dr["SEQ_LACRE_REPOSITORIO"] != DBNull.Value)
                                lacreRep.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO"]);

                            if (dr["SEQ_REPOSITORIO"] != DBNull.Value)
                                lacreRep.RepositorioListaControle.SeqRepositorio = Convert.ToInt64(dr["SEQ_REPOSITORIO"]);

                            if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                                lacreRep.UsuarioCadastro.NumUserBanco = Convert.ToInt32(dr["NUM_USER_CADASTRO"]);

                            if (dr["NUM_USER_ULTIMA_ALTERACAO"] != DBNull.Value)
                                lacreRep.UsuarioUltimaAlteracao.NumUserBanco = Convert.ToInt32(dr["NUM_USER_ULTIMA_ALTERACAO"]);

                            if (dr["DTA_CADASTRO"] != DBNull.Value)
                                lacreRep.DataCadastro = Convert.ToDateTime(dr["DTA_CADASTRO"]);

                            if (dr["DTA_ULTIMA_ALTERACAO"] != DBNull.Value)
                                lacreRep.DataHoraUltimaAlteracao = Convert.ToDateTime(dr["DTA_ULTIMA_ALTERACAO"]);

                            if (dr["NUM_LACRE"] != DBNull.Value)
                                lacreRep.NumLacre = Convert.ToInt64(dr["NUM_LACRE"]);

                            if (dr["NUM_CAIXA_INTUBACAO"] != DBNull.Value)
                                lacreRep.NumCaixaIntubacao = dr["NUM_CAIXA_INTUBACAO"].ToString();

                            if (dr["COD_SITUACAO_ATUAL"] != DBNull.Value)
                                lacreRep.TipoSituacaoHc.CodSituacao = Convert.ToInt64(dr["COD_SITUACAO_ATUAL"]);

                            if (dr["SEQ_LACRE_TIP_OCORRENCIA"] != DBNull.Value)
                                lacreRep.LacreTipoOcorrencia.SeqLacreTipoOCorrencia = Convert.ToInt64(dr["SEQ_LACRE_TIP_OCORRENCIA"]);

                            if (dr["NOM_SITUACAO"] != DBNull.Value)
                                lacreRep.TipoSituacaoHc.NomSituacao = dr["NOM_SITUACAO"].ToString();

                            if (dr["DSC_TIPO_OCORRENCIA"] != DBNull.Value)
                                lacreRep.LacreTipoOcorrencia.DscTipoOcorrencia = dr["DSC_TIPO_OCORRENCIA"].ToString();

                            if (dr["DSC_TIPO_REPOSITORIO"] != DBNull.Value)
                                lacreRep.RepositorioListaControle.TipoRepositorioListaControle.DscTipoRepositorio = dr["DSC_TIPO_REPOSITORIO"].ToString();

                            if (dr["DSC_REPOSITORIO"] != DBNull.Value)
                                lacreRep.RepositorioListaControle.DscIdentificacao = dr["DSC_REPOSITORIO"].ToString();

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

            return lacreRep;
        }

        public Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio ObterPorIdTrans(Infra.AcessoDado.TransacaoDinamica transacao, Int64 seqLacreRepositorio)
        {
            Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio lacreRep = null;

            try
            {
                StringBuilder str = new StringBuilder();

                Contexto ctx = transacao.ctx;
                //using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                //{
                    QueryCommandConfig query;

                    str.AppendLine(" SELECT ");
                    str.AppendLine("    LR.SEQ_LACRE_REPOSITORIO, ");
                    str.AppendLine("    LR.SEQ_REPOSITORIO, ");
                    str.AppendLine("    LR.NUM_USER_CADASTRO, ");
                    str.AppendLine("    LR.NUM_USER_ULTIMA_ALTERACAO, ");
                    str.AppendLine("    LR.DTA_CADASTRO, ");
                    str.AppendLine("    LR.DTA_ULTIMA_ALTERACAO, ");
                    str.AppendLine("    LR.NUM_LACRE, ");
                    str.AppendLine("    LR.NUM_CAIXA_INTUBACAO, ");
                    str.AppendLine("    LR.COD_SITUACAO_ATUAL, ");
                    str.AppendLine("    LR.SEQ_LACRE_TIP_OCORRENCIA, ");
                    str.AppendLine("    SIT.NOM_SITUACAO, ");
                    str.AppendLine("    LTO.DSC_TIPO_OCORRENCIA, ");
                    str.AppendLine("    TPLIS.DSC_TIPO_REPOSITORIO, ");
                    str.AppendLine("    RLC.DSC_IDENTIFICACAO AS DSC_REPOSITORIO ");

                    str.AppendLine(" FROM LACRE_REPOSITORIO LR, ");
                    str.AppendLine("    TIPO_SITUACAO_HC SIT, ");
                    str.AppendLine("    LACRE_TIPO_OCORRENCIA LTO, ");
                    str.AppendLine("    REPOSITORIO_LISTA_CONTROLE RLC, ");
                    str.AppendLine("    TIPO_REPOSITORIO_LST_CONTROLE TPLIS ");

                    str.AppendLine(" WHERE LR.COD_SITUACAO_ATUAL = SIT.COD_SITUACAO AND  ");
                    str.AppendLine("    LR.SEQ_LACRE_TIP_OCORRENCIA = LTO.SEQ_LACRE_TIP_OCORRENCIA AND ");
                    str.AppendLine("    LR.SEQ_REPOSITORIO = RLC.SEQ_REPOSITORIO AND ");
                    str.AppendLine("    RLC.SEQ_TIP_REPOSIT_LST_CONTROL = TPLIS.SEQ_TIP_REPOSIT_LST_CONTROL AND ");
                    str.AppendLine(string.Format(" LR.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    
                    while (dr.Read())
                    {
                        lacreRep = new Entity.LacreRepositorio();
                        lacreRep.RepositorioListaControle = new Entity.RepositorioListaControle();
                        lacreRep.UsuarioCadastro = new Entity.Usuario();
                        lacreRep.UsuarioUltimaAlteracao = new Entity.Usuario();
                        lacreRep.TipoSituacaoHc = new Entity.TipoSituacaoHc();
                        lacreRep.LacreTipoOcorrencia = new Entity.LacreTipoOcorrencia();
                        lacreRep.RepositorioListaControle.TipoRepositorioListaControle =
                            new Entity.TipoRepositorioListaControle();


                        if (dr["SEQ_LACRE_REPOSITORIO"] != DBNull.Value)
                            lacreRep.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO"]);

                        if (dr["SEQ_REPOSITORIO"] != DBNull.Value)
                            lacreRep.RepositorioListaControle.SeqRepositorio = Convert.ToInt64(dr["SEQ_REPOSITORIO"]);

                        if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                            lacreRep.UsuarioCadastro.NumUserBanco = Convert.ToInt32(dr["NUM_USER_CADASTRO"]);

                        if (dr["NUM_USER_ULTIMA_ALTERACAO"] != DBNull.Value)
                            lacreRep.UsuarioUltimaAlteracao.NumUserBanco =
                                Convert.ToInt32(dr["NUM_USER_ULTIMA_ALTERACAO"]);

                        if (dr["DTA_CADASTRO"] != DBNull.Value)
                            lacreRep.DataCadastro = Convert.ToDateTime(dr["DTA_CADASTRO"]);

                        if (dr["DTA_ULTIMA_ALTERACAO"] != DBNull.Value)
                            lacreRep.DataHoraUltimaAlteracao = Convert.ToDateTime(dr["DTA_ULTIMA_ALTERACAO"]);

                        if (dr["NUM_LACRE"] != DBNull.Value)
                            lacreRep.NumLacre = Convert.ToInt64(dr["NUM_LACRE"]);

                        if (dr["NUM_CAIXA_INTUBACAO"] != DBNull.Value)
                            lacreRep.NumCaixaIntubacao = dr["NUM_CAIXA_INTUBACAO"].ToString();

                        if (dr["COD_SITUACAO_ATUAL"] != DBNull.Value)
                            lacreRep.TipoSituacaoHc.CodSituacao = Convert.ToInt64(dr["COD_SITUACAO_ATUAL"]);

                        if (dr["SEQ_LACRE_TIP_OCORRENCIA"] != DBNull.Value)
                            lacreRep.LacreTipoOcorrencia.SeqLacreTipoOCorrencia =
                                Convert.ToInt64(dr["SEQ_LACRE_TIP_OCORRENCIA"]);

                        if (dr["NOM_SITUACAO"] != DBNull.Value)
                            lacreRep.TipoSituacaoHc.NomSituacao = dr["NOM_SITUACAO"].ToString();

                        if (dr["DSC_TIPO_OCORRENCIA"] != DBNull.Value)
                            lacreRep.LacreTipoOcorrencia.DscTipoOcorrencia = dr["DSC_TIPO_OCORRENCIA"].ToString();

                        if (dr["DSC_TIPO_REPOSITORIO"] != DBNull.Value)
                            lacreRep.RepositorioListaControle.TipoRepositorioListaControle.DscTipoRepositorio =
                                dr["DSC_TIPO_REPOSITORIO"].ToString();

                        if (dr["DSC_REPOSITORIO"] != DBNull.Value)
                            lacreRep.RepositorioListaControle.DscIdentificacao = dr["DSC_REPOSITORIO"].ToString();

                        break;
                    }
                    
            }
            catch (Exception err)
            {
                throw err;
            }

            return lacreRep;
        }

        /// <summary>
        /// Quebrar lacre carrinho.
        /// </summary>        
        public void QuebrarLacreCarrinho(Int64 seqLacreRepositorio, Int64 seqTipoOcorrencia, Int64 codSituacao)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comandoDataValidadeLote =
                        new Hcrp.Infra.AcessoDado.UpdateCommandConfig("LACRE_REPOSITORIO");

                    comandoDataValidadeLote.FilterParams["SEQ_LACRE_REPOSITORIO"] = seqLacreRepositorio;

                    comandoDataValidadeLote.Params["COD_SITUACAO_ATUAL"] = codSituacao;

                    comandoDataValidadeLote.Params["SEQ_LACRE_TIP_OCORRENCIA"] = seqTipoOcorrencia;

                    // Executar o update
                    ctx.ExecuteUpdate(comandoDataValidadeLote);



                    // REGISTRA A QUEBRA DE LACRE NA TABELA DE HISTORICO
                    Entity.LacreRepositorio lacreRep = new Entity.LacreRepositorio()
                                                           {
                                                               SeqLacreRepositorio = seqLacreRepositorio
                                                           };


                    Entity.HistoricoLacreRepositorio histLacreRep = new Entity.HistoricoLacreRepositorio();

                    histLacreRep.LacreRepositorio = lacreRep;
                    histLacreRep.DataCadastro = DateTime.Now;

                    histLacreRep.IdfConferencia = "N";

                    Entity.TipoSituacaoHc sit = new TipoSituacaoHc()
                                                    {
                                                        CodSituacao = codSituacao
                                                    };

                    histLacreRep.TipoSituacaoHc = sit;

                    histLacreRep.UsuarioCadastro = new Entity.Usuario();

                    histLacreRep.UsuarioCadastro.NumUserBanco =
                        Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;

                    new DAL.HistoricoLacreRepositorio().Adicionar(histLacreRep);
                }

            }
            catch (Exception )
            {
                throw;
            }
        }

        /// <summary>
        /// Obter para copia.
        /// </summary>        
        public Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio ObterParaCopia(Int64 seqLacreRepositorio)
        {
            Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio lacreRep = null;

            try
            {
                StringBuilder str = new StringBuilder();


                QueryCommandConfig query;

                // obter o contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                str.AppendLine(" SELECT  L.SEQ_LACRE_REPOSITORIO, ");
                str.AppendLine("        L.SEQ_REPOSITORIO, ");
                str.AppendLine("        L.NUM_USER_CADASTRO, ");
                str.AppendLine("        L.NUM_USER_ULTIMA_ALTERACAO, ");
                str.AppendLine("        L.DTA_CADASTRO, ");
                str.AppendLine("        L.DTA_ULTIMA_ALTERACAO, ");
                str.AppendLine("        L.NUM_LACRE, ");
                str.AppendLine("        L.NUM_CAIXA_INTUBACAO, ");
                str.AppendLine("        L.COD_SITUACAO_ATUAL, ");
                str.AppendLine("        L.SEQ_LACRE_TIP_OCORRENCIA ");
                str.AppendLine(" FROM LACRE_REPOSITORIO L ");
                str.AppendLine(string.Format(" WHERE L.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));

                query = new QueryCommandConfig(str.ToString());

                // Obter a lista de registros
                ctx.ExecuteQuery(query);

                IDataReader dr = ctx.Reader;

                try
                {
                    while (dr.Read())
                    {
                        lacreRep = new Entity.LacreRepositorio();

                        lacreRep.RepositorioListaControle = new Entity.RepositorioListaControle();                        
                        lacreRep.UsuarioCadastro = new Entity.Usuario();
                        lacreRep.UsuarioUltimaAlteracao = new Entity.Usuario();
                        lacreRep.TipoSituacaoHc = new Entity.TipoSituacaoHc();
                        lacreRep.LacreTipoOcorrencia = new Entity.LacreTipoOcorrencia();

                        if (dr["SEQ_LACRE_REPOSITORIO"] != DBNull.Value)
                            lacreRep.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO"]);

                        if (dr["SEQ_REPOSITORIO"] != DBNull.Value)
                            lacreRep.RepositorioListaControle.SeqRepositorio = Convert.ToInt64(dr["SEQ_REPOSITORIO"]);

                        if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                            lacreRep.UsuarioCadastro.NumUserBanco = Convert.ToInt32(dr["NUM_USER_CADASTRO"]);

                        if (dr["NUM_USER_ULTIMA_ALTERACAO"] != DBNull.Value)
                            lacreRep.UsuarioUltimaAlteracao.NumUserBanco = Convert.ToInt32(dr["NUM_USER_ULTIMA_ALTERACAO"]);

                        if (dr["DTA_CADASTRO"] != DBNull.Value)
                            lacreRep.DataCadastro = Convert.ToDateTime(dr["DTA_CADASTRO"]);

                        if (dr["DTA_ULTIMA_ALTERACAO"] != DBNull.Value)
                            lacreRep.DataHoraUltimaAlteracao = Convert.ToDateTime(dr["DTA_ULTIMA_ALTERACAO"]);

                        if (dr["NUM_LACRE"] != DBNull.Value)
                            lacreRep.NumLacre = Convert.ToInt64(dr["NUM_LACRE"]);

                        if (dr["NUM_CAIXA_INTUBACAO"] != DBNull.Value)
                            lacreRep.NumCaixaIntubacao = dr["NUM_CAIXA_INTUBACAO"].ToString();

                        if (dr["COD_SITUACAO_ATUAL"] != DBNull.Value)
                            lacreRep.TipoSituacaoHc.CodSituacao = Convert.ToInt64(dr["COD_SITUACAO_ATUAL"]);

                        if (dr["SEQ_LACRE_TIP_OCORRENCIA"] != DBNull.Value)
                            lacreRep.LacreTipoOcorrencia.SeqLacreTipoOCorrencia = Convert.ToInt64(dr["SEQ_LACRE_TIP_OCORRENCIA"]);

                        break;

                    }
                }
                finally
                {
                    dr.Close();
                    dr.Dispose();
                }

            }
            catch (Exception err)
            {
                throw err;
            }

            return lacreRep;
        }

        /// <summary>
        /// Salvar.
        /// </summary>        
        public Int64 AdicionarTrans(Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio lacreRepositorio)
        {
            Int64 seqGerada = 0;

            try
            {
                // obter o contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                Hcrp.Infra.AcessoDado.CommandConfig comandoInsert = new Hcrp.Infra.AcessoDado.CommandConfig("LACRE_REPOSITORIO");

                if (lacreRepositorio.RepositorioListaControle != null && lacreRepositorio.RepositorioListaControle.SeqRepositorio > 0)
                    comandoInsert.Params["SEQ_REPOSITORIO"] = lacreRepositorio.RepositorioListaControle.SeqRepositorio;

                if (lacreRepositorio.UsuarioCadastro != null && lacreRepositorio.UsuarioCadastro.NumUserBanco > 0)
                    comandoInsert.Params["NUM_USER_CADASTRO"] = lacreRepositorio.UsuarioCadastro.NumUserBanco;

                comandoInsert.Params["DTA_CADASTRO"] = lacreRepositorio.DataCadastro;

                if (lacreRepositorio.TipoSituacaoHc != null && lacreRepositorio.TipoSituacaoHc.CodSituacao > 0)
                    comandoInsert.Params["COD_SITUACAO_ATUAL"] = lacreRepositorio.TipoSituacaoHc.CodSituacao;

                if (lacreRepositorio.LacreTipoOcorrencia != null && lacreRepositorio.LacreTipoOcorrencia.SeqLacreTipoOCorrencia > 0)
                    comandoInsert.Params["SEQ_LACRE_TIP_OCORRENCIA"] = lacreRepositorio.LacreTipoOcorrencia.SeqLacreTipoOCorrencia;

                // Executar o insert
                ctx.ExecuteInsert(comandoInsert);

                seqGerada = Convert.ToInt64(ctx.GetSequenceValue("SEQ_LACRE_REPOSITORIO", false));

                

            }
            catch (Exception)
            {
                throw;
            }

            return seqGerada;
        }
       
        #endregion
    }
}
