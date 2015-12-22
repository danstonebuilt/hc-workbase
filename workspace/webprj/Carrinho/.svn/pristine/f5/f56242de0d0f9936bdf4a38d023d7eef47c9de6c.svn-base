using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;
using Oracle.DataAccess.Client;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class LacreRepositorioItens
    {
        #region variáveis / construtor

        internal Hcrp.Infra.AcessoDado.TransacaoDinamica transacao;

        public LacreRepositorioItens()
        {

        }

        public LacreRepositorioItens(Hcrp.Infra.AcessoDado.TransacaoDinamica _trans)
        {
            this.transacao = _trans;
        }

        #endregion

        /// <summary>
        /// Obter por lacre repositorio.
        /// </summary>
        public List<Entity.LacreRepositorioItens> ObterPorLacreRepositorio001(Int64 seqLacreRepositorio, Int64 seqAtendimento)
        {
            List<Entity.LacreRepositorioItens> listaRetorno = new List<Entity.LacreRepositorioItens>();
            Entity.LacreRepositorioItens lacreRepositorioItens = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT I.SEQ_LACRE_REPOSITORIO_ITENS, ");
                    str.AppendLine("        I.SEQ_LACRE_REPOSITORIO, ");
                    str.AppendLine("        I.QTD_DISPONIVEL, ");
                    str.AppendLine("        I.COD_MATERIAL, ");
                    str.AppendLine("        I.NUM_LOTE, ");
                    str.AppendLine("        I.SEQ_ITENS_LISTA_CONTROLE, ");
                    str.AppendLine("        I.NUM_LOTE_FABRICANTE, ");
                    str.AppendLine("        I.DTA_VALIDADE_LOTE, ");
                    str.AppendLine("        I.DTA_CADASTRO, ");
                    str.AppendLine("        I.DTA_ULTIMA_ALTERACAO, ");
                    str.AppendLine("        I.NUM_USER_CADASTRO, ");
                    str.AppendLine("        I.NUM_USER_ULTIMA_ALTERACAO, ");
                    str.AppendLine("        ALI.DSC_ALINEA, ");
                    str.AppendLine("        NVL(MAT.NOM_MATERIAL, ILC.DSC_MATERIAL) DSC_MATERIAL, ");
                    str.AppendLine("        UNID.NOM_UNIDADE, ");
                    str.AppendLine("        NVL(LOT.NUM_LOTE_FABRICANTE, I.NUM_LOTE_FABRICANTE) AS LOTE_FABRICANTE, ");
                    str.AppendLine("        NVL(LOT.DTA_VALIDADE_LOTE, I.DTA_VALIDADE_LOTE) AS VALIDADE_LOTE_FABRICANTE, ");

                    str.AppendLine("        (SELECT LRU.QTD_UTILIZADA ");
                    str.AppendLine("          FROM LACRE_REPOSIT_UTILIZACAO LRU ");
                    str.AppendLine("          WHERE LRU.SEQ_LACRE_REPOSITORIO_ITENS = I.SEQ_LACRE_REPOSITORIO_ITENS ");
                    str.AppendLine(string.Format(" AND LRU.SEQ_ATENDIMENTO = {0} ) AS QTD_UTILIZADA ", seqAtendimento));

                    str.AppendLine(" FROM LACRE_REPOSITORIO_ITENS I, ");
                    str.AppendLine("      ITENS_LISTA_CONTROLE ILC, ");
                    str.AppendLine("      ALINEA ALI, ");
                    str.AppendLine("      MATERIAL MAT, ");
                    str.AppendLine("      UNIDADE UNID, ");
                    str.AppendLine("      LOTE LOT ");
                    str.AppendLine(string.Format(" WHERE I.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));
                    str.AppendLine(" AND I.SEQ_ITENS_LISTA_CONTROLE = ILC.SEQ_ITENS_LISTA_CONTROLE ");
                    str.AppendLine(" AND ILC.COD_ALINEA = ALI.COD_ALINEA(+) ");
                    str.AppendLine(" AND ILC.COD_MATERIAL = MAT.COD_MATERIAL(+) ");
                    str.AppendLine(" AND ILC.COD_UNIDADE = UNID.COD_UNIDADE(+) ");
                    str.AppendLine(" AND I.NUM_LOTE = LOT.NUM_LOTE(+) ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lacreRepositorioItens = new Entity.LacreRepositorioItens();
                            lacreRepositorioItens.LacreRepositorio = new Entity.LacreRepositorio();
                            lacreRepositorioItens.Material = new Entity.Material();
                            lacreRepositorioItens.Lote = new Entity.Lote();
                            lacreRepositorioItens.ItensListaControle = new Entity.ItensListaControle();
                            lacreRepositorioItens.UsuarioCadastro = new Entity.Usuario();
                            lacreRepositorioItens.UsuarioUltimaAlteracao = new Entity.Usuario();
                            lacreRepositorioItens.ItensListaControle.Alinea = new Entity.Alinea();
                            lacreRepositorioItens.ItensListaControle.Material = new Entity.Material();
                            lacreRepositorioItens.ItensListaControle.Unidade = new Entity.Unidade();

                            if (dr["SEQ_LACRE_REPOSITORIO_ITENS"] != DBNull.Value)
                                lacreRepositorioItens.SeqLacreRepositorioItens = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO_ITENS"]);

                            if (dr["SEQ_LACRE_REPOSITORIO"] != DBNull.Value)
                                lacreRepositorioItens.LacreRepositorio.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO"]);

                            if (dr["QTD_DISPONIVEL"] != DBNull.Value)
                                lacreRepositorioItens.QtdDisponivel = Convert.ToInt32(dr["QTD_DISPONIVEL"]);

                            if (dr["COD_MATERIAL"] != DBNull.Value)
                                lacreRepositorioItens.Material.Codigo = dr["COD_MATERIAL"].ToString();

                            if (dr["NUM_LOTE"] != DBNull.Value)
                                lacreRepositorioItens.Lote.NumLote = Convert.ToInt64(dr["NUM_LOTE"]);

                            if (dr["SEQ_ITENS_LISTA_CONTROLE"] != DBNull.Value)
                                lacreRepositorioItens.ItensListaControle.SeqItensListaControle = Convert.ToInt64(dr["SEQ_ITENS_LISTA_CONTROLE"]);

                            if (dr["NUM_LOTE_FABRICANTE"] != DBNull.Value)
                                lacreRepositorioItens.Lote.NumLoteFabricante = dr["NUM_LOTE_FABRICANTE"].ToString();

                            if (dr["DTA_VALIDADE_LOTE"] != DBNull.Value)
                                lacreRepositorioItens.Lote.DataValidadeLote = Convert.ToDateTime(dr["DTA_VALIDADE_LOTE"]);

                            if (dr["DTA_CADASTRO"] != DBNull.Value)
                                lacreRepositorioItens.DataCadastro = Convert.ToDateTime(dr["DTA_CADASTRO"]);

                            if (dr["DTA_ULTIMA_ALTERACAO"] != DBNull.Value)
                                lacreRepositorioItens.DataUltimaAlteracao = Convert.ToDateTime(dr["DTA_ULTIMA_ALTERACAO"]);

                            if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                                lacreRepositorioItens.UsuarioCadastro.NumUserBanco = Convert.ToInt32(dr["NUM_USER_CADASTRO"]);

                            if (dr["NUM_USER_ULTIMA_ALTERACAO"] != DBNull.Value)
                                lacreRepositorioItens.UsuarioUltimaAlteracao.NumUserBanco = Convert.ToInt32(dr["NUM_USER_ULTIMA_ALTERACAO"]);

                            if (dr["DSC_ALINEA"] != DBNull.Value)
                                lacreRepositorioItens.ItensListaControle.Alinea.Nome = dr["DSC_ALINEA"].ToString();

                            if (dr["DSC_MATERIAL"] != DBNull.Value)
                                lacreRepositorioItens.ItensListaControle.Material.Nome = dr["DSC_MATERIAL"].ToString();

                            if (dr["NOM_UNIDADE"] != DBNull.Value)
                                lacreRepositorioItens.ItensListaControle.Unidade.Nome = dr["NOM_UNIDADE"].ToString();

                            if (dr["LOTE_FABRICANTE"] != DBNull.Value)
                                lacreRepositorioItens.Lote.NumLoteFabricante = dr["LOTE_FABRICANTE"].ToString();

                            if (dr["VALIDADE_LOTE_FABRICANTE"] != DBNull.Value)
                                lacreRepositorioItens.Lote.DataValidadeLote = Convert.ToDateTime(dr["VALIDADE_LOTE_FABRICANTE"]);

                            if (dr["QTD_UTILIZADA"] != DBNull.Value)
                                lacreRepositorioItens.QtdUtilizada = Convert.ToInt32(dr["QTD_UTILIZADA"]);

                            if (dr["QTD_NECESSARIA"] != DBNull.Value)
                                lacreRepositorioItens.ItensListaControle.QuantidadeNecessaria = Convert.ToInt32(dr["QTD_NECESSARIA"]);

                            listaRetorno.Add(lacreRepositorioItens);

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
        /// Atualizar data de vencimento do lote.
        /// </summary>        
        public void AtualizarDataVencimentoDoLote(Int64 seqLacreRepositorioItens, DateTime dataVencimentoLote)
        {
            try
            {
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                Hcrp.Infra.AcessoDado.UpdateCommandConfig comandoDataValidadeLote = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("LACRE_REPOSITORIO_ITENS");

                comandoDataValidadeLote.FilterParams["SEQ_LACRE_REPOSITORIO_ITENS"] = seqLacreRepositorioItens;

                comandoDataValidadeLote.Params["DTA_VALIDADE_LOTE"] = dataVencimentoLote;

                // Executar o insert
                ctx.ExecuteUpdate(comandoDataValidadeLote);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Atualizar quantidade.
        /// </summary>        
        public void AtualizarQuantidadeUtilizada(Entity.LacreRepositorioItens lacreRepItens)
        {
            try
            {
                // Criar contexto
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    // Preparar o comando
                    var comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("LACRE_REPOSITORIO_ITENS");

                    comando.FilterParams["SEQ_LACRE_REPOSITORIO_ITENS"] = lacreRepItens.SeqLacreRepositorioItens;
                    comando.Params["QTD_DISPONIVEL"] = lacreRepItens.QtdDisponivelInserida;

                    // Executar o insert
                    ctx.ExecuteUpdate(comando);
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
        public long Adicionar(Entity.LacreRepositorioItens lacreRepItens)
        {
            long _seqRetorno = 0;

            try
            {
                // Criar contexto
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("LACRE_REPOSITORIO_ITENS");

                    comando.Params["SEQ_LACRE_REPOSITORIO"] = lacreRepItens.LacreRepositorio.SeqLacreRepositorio;
                    comando.Params["QTD_DISPONIVEL"] = lacreRepItens.QtdDisponivel;

                    if (lacreRepItens.Material != null && !string.IsNullOrWhiteSpace(lacreRepItens.Material.Codigo))
                        comando.Params["COD_MATERIAL"] = lacreRepItens.Material.Codigo;

                    if (lacreRepItens.Lote != null && lacreRepItens.Lote.NumLote > 0)
                        comando.Params["NUM_LOTE"] = lacreRepItens.Lote.NumLote;

                    if (lacreRepItens.ItensListaControle != null && lacreRepItens.ItensListaControle.SeqItensListaControle > 0)
                        comando.Params["SEQ_ITENS_LISTA_CONTROLE"] = lacreRepItens.ItensListaControle.SeqItensListaControle;

                    if (!string.IsNullOrWhiteSpace(lacreRepItens.NumLoteFabricante))
                        comando.Params["NUM_LOTE_FABRICANTE"] = lacreRepItens.NumLoteFabricante;

                    if (lacreRepItens.DataValidadeLote != null)
                        comando.Params["DTA_VALIDADE_LOTE"] = lacreRepItens.DataValidadeLote.Value;

                    if (lacreRepItens.UsuarioCadastro != null && lacreRepItens.UsuarioCadastro.NumUserBanco > 0)
                        comando.Params["NUM_USER_CADASTRO"] = lacreRepItens.UsuarioCadastro.NumUserBanco;

                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                    _seqRetorno = Convert.ToInt64(ctx.GetSequenceValue("SEQ_LACRE_REPOSITORIO_ITENS", false));
                }

            }
            catch (Exception)
            {
                throw;
            }

            return _seqRetorno;
        }

        /// <summary>
        /// Obter por lacre repositorios somente itens com o status ativo.
        /// </summary>
        public List<Entity.LacreRepositorioItens> ObterPorLacreRepositorio(Int64 seqLacreRepositorio, Int64? seqAtendimento)
        {
            List<Entity.LacreRepositorioItens> listaRetorno = new List<Entity.LacreRepositorioItens>();
            Entity.LacreRepositorioItens lacreRepositorioItens = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT B.SEQ_LACRE_REPOSITORIO_ITENS, ");
                    str.AppendLine("        B.NUM_LOTE, ");
                    str.AppendLine("        A.SEQ_LACRE_REPOSITORIO, ");
                    str.AppendLine("        A.SEQ_REPOSITORIO, ");
                    str.AppendLine("        I.DSC_IDENTIFICACAO, H.IDF_CLASSE,  ");
                    str.AppendLine("        DECODE(NVL(H.IDF_CLASSE, ");

                    str.AppendLine("        (SELECT X.IDF_CLASSE ");
                    str.AppendLine("            FROM ALINEA X ");
                    str.AppendLine("            WHERE X.COD_ALINEA = C.COD_ALINEA)),1,'MATERIAIS DE CONSUMO',2,'MEDICAMENTOS',3,'CONSUMO DURÁVEL',4,'PATRIMÔNIO','PRESTAÇÃO DE SERVIÇOS') DSC_ALINEA, ");

                    str.AppendLine("        NVL(B.COD_MATERIAL, D.COD_MATERIAL) COD_MATERIAL, ");
                    str.AppendLine("        NVL(D.NOM_MATERIAL, C.DSC_MATERIAL) NOM_MATERIAL, ");
                    str.AppendLine("        NVL(F.NOM_UNIDADE, ");

                    str.AppendLine("        (SELECT X1.NOM_UNIDADE ");
                    str.AppendLine("            FROM UNIDADE X1 ");
                    str.AppendLine("            WHERE X1.COD_UNIDADE = C.COD_UNIDADE)) DSC_UNIDADE_MEDIDA, ");

                    //str.AppendLine("        E.NUM_LOTE_FABRICANTE, ");
                    str.AppendLine("        C.QTD_NECESSARIA, ");



                    str.AppendLine("        NVL((SELECT B.QTD_DISPONIVEL - SUM(X.QTD_UTILIZADA) ");
                    str.AppendLine("                FROM LACRE_REPOSIT_UTILIZACAO X ");
                    str.AppendLine("                WHERE X.SEQ_LACRE_REPOSITORIO_ITENS = ");
                    str.AppendLine("                B.SEQ_LACRE_REPOSITORIO_ITENS), ");
                    str.AppendLine("                B.QTD_DISPONIVEL) QTD_DISPONIVEL, ");

                    str.AppendLine("        (SELECT LRU.QTD_UTILIZADA ");
                    str.AppendLine("          FROM LACRE_REPOSIT_UTILIZACAO LRU ");
                    str.AppendLine("          WHERE LRU.SEQ_LACRE_REPOSITORIO_ITENS = B.SEQ_LACRE_REPOSITORIO_ITENS ");


                    if (seqAtendimento != null)
                        str.AppendLine(string.Format(" AND LRU.SEQ_ATENDIMENTO = {0} AND ROWNUM = 1 ) AS QTD_UTILIZADA_ATEND, ", seqAtendimento.Value));
                    else
                        str.AppendLine(" AND LRU.SEQ_ATENDIMENTO IS NULL AND ROWNUM = 1) AS QTD_UTILIZADA_ATEND, ");


                    str.AppendLine("        (SELECT LRU.DSC_JUSTIFICATIVA ");
                    str.AppendLine("          FROM LACRE_REPOSIT_UTILIZACAO LRU ");
                    str.AppendLine("          WHERE LRU.SEQ_LACRE_REPOSITORIO_ITENS = B.SEQ_LACRE_REPOSITORIO_ITENS ");


                    if (seqAtendimento != null)
                        str.AppendLine(string.Format(" AND LRU.SEQ_ATENDIMENTO = {0} AND ROWNUM = 1) AS DSC_JUSTIFICATIVA_ATEND, ", seqAtendimento.Value));
                    else
                        str.AppendLine(" AND LRU.SEQ_ATENDIMENTO IS NULL AND ROWNUM = 1) AS DSC_JUSTIFICATIVA_ATEND, ");


                    str.AppendLine("        NVL(NVL(B.NUM_LOTE_FABRICANTE, E.NUM_LOTE_FABRICANTE),'NÃO POSSUI') LOTE_FABRICANTE, ");
                    str.AppendLine("        NVL(B.DTA_VALIDADE_LOTE, E.DTA_VALIDADE_LOTE) DTA_VALIDADE_LOTE, ");

                    str.AppendLine("    B.QTD_DISPONIVEL AS QTD_INSERIDA_ITEM_CARRINHO ");

                    str.AppendLine(" FROM LACRE_REPOSITORIO     A, ");
                    str.AppendLine("    LACRE_REPOSITORIO_ITENS    B, ");
                    str.AppendLine("    ITENS_LISTA_CONTROLE       C, ");
                    str.AppendLine("    MATERIAL                   D, ");
                    str.AppendLine("    LOTE                       E, ");
                    str.AppendLine("    UNIDADE                    F, ");
                    str.AppendLine("    GRUPO                      G, ");
                    str.AppendLine("    ALINEA                     H, ");
                    str.AppendLine("    REPOSITORIO_LISTA_CONTROLE I ");
                    str.AppendLine(" WHERE A.SEQ_LACRE_REPOSITORIO = B.SEQ_LACRE_REPOSITORIO ");
                    str.AppendLine("    AND B.SEQ_ITENS_LISTA_CONTROLE = C.SEQ_ITENS_LISTA_CONTROLE ");
                    str.AppendLine("    AND C.COD_MATERIAL = D.COD_MATERIAL(+) ");
                    str.AppendLine("    AND B.NUM_LOTE = E.NUM_LOTE(+) ");
                    str.AppendLine("    AND D.COD_UNIDADE = F.COD_UNIDADE(+) ");
                    str.AppendLine("    AND D.COD_GRUPO = G.COD_GRUPO(+) ");
                    str.AppendLine("    AND G.COD_ALINEA = H.COD_ALINEA(+) ");
                    str.AppendLine("    AND A.SEQ_REPOSITORIO = I.SEQ_REPOSITORIO ");

                    str.AppendLine("    AND C.IDF_ATIVO = 'S' ");

                    str.AppendLine(string.Format(" AND A.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));                    
                    str.AppendLine("  ORDER BY DSC_ALINEA DESC, NVL(D.NOM_MATERIAL,B.COD_MATERIAL)  ");


                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lacreRepositorioItens = new Entity.LacreRepositorioItens();
                            lacreRepositorioItens.LacreRepositorio = new Entity.LacreRepositorio();
                            lacreRepositorioItens.Material = new Entity.Material();
                            lacreRepositorioItens.Lote = new Entity.Lote();
                            lacreRepositorioItens.ItensListaControle = new Entity.ItensListaControle();
                            lacreRepositorioItens.UsuarioCadastro = new Entity.Usuario();
                            lacreRepositorioItens.UsuarioUltimaAlteracao = new Entity.Usuario();
                            lacreRepositorioItens.ItensListaControle.Alinea = new Entity.Alinea();
                            lacreRepositorioItens.ItensListaControle.Material = new Entity.Material();
                            lacreRepositorioItens.ItensListaControle.Unidade = new Entity.Unidade();
                            lacreRepositorioItens.LacreRepositorio.RepositorioListaControle = new Entity.RepositorioListaControle();

                            if (dr["SEQ_LACRE_REPOSITORIO_ITENS"] != DBNull.Value)
                                lacreRepositorioItens.SeqLacreRepositorioItens = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO_ITENS"]);

                            if (dr["SEQ_REPOSITORIO"] != DBNull.Value)
                                lacreRepositorioItens.LacreRepositorio.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_REPOSITORIO"]);

                            if (dr["DSC_IDENTIFICACAO"] != DBNull.Value)
                                lacreRepositorioItens.LacreRepositorio.RepositorioListaControle.DscIdentificacao = dr["DSC_IDENTIFICACAO"].ToString();

                            if (dr["DSC_ALINEA"] != DBNull.Value)
                                lacreRepositorioItens.ItensListaControle.Alinea.Nome = dr["DSC_ALINEA"].ToString();

                            if (dr["IDF_CLASSE"] != DBNull.Value)
                                lacreRepositorioItens.ItensListaControle.Alinea.IdfClasse = Convert.ToInt32(dr["IDF_CLASSE"]);

                            if (dr["COD_MATERIAL"] != DBNull.Value)
                                lacreRepositorioItens.Material.Codigo = dr["COD_MATERIAL"].ToString();

                            if (dr["NOM_MATERIAL"] != DBNull.Value)
                                lacreRepositorioItens.Material.Nome = dr["NOM_MATERIAL"].ToString();

                            if (dr["DSC_UNIDADE_MEDIDA"] != DBNull.Value)
                                lacreRepositorioItens.ItensListaControle.Unidade.Nome = dr["DSC_UNIDADE_MEDIDA"].ToString();

                            if (dr["QTD_NECESSARIA"] != DBNull.Value)
                                lacreRepositorioItens.ItensListaControle.QuantidadeNecessaria = Convert.ToInt32(dr["QTD_NECESSARIA"]);

                            if (dr["QTD_DISPONIVEL"] != DBNull.Value)
                                lacreRepositorioItens.QtdDisponivel = Convert.ToInt32(dr["QTD_DISPONIVEL"]);

                            if (dr["QTD_UTILIZADA_ATEND"] != DBNull.Value)
                                lacreRepositorioItens.QtdUtilizada = Convert.ToInt32(dr["QTD_UTILIZADA_ATEND"]);

                            if (dr["DSC_JUSTIFICATIVA_ATEND"] != DBNull.Value)
                                lacreRepositorioItens.DscJustificativaConsumoSemAtendimento = dr["DSC_JUSTIFICATIVA_ATEND"].ToString();

                            if (dr["LOTE_FABRICANTE"] != DBNull.Value)
                                lacreRepositorioItens.Lote.NumLoteFabricante = dr["LOTE_FABRICANTE"].ToString();

                            if (dr["NUM_LOTE"] != DBNull.Value)
                                lacreRepositorioItens.Lote.NumLote = Convert.ToInt64(dr["NUM_LOTE"]);

                            if (dr["DTA_VALIDADE_LOTE"] != DBNull.Value)
                                lacreRepositorioItens.Lote.DataValidadeLote = Convert.ToDateTime(dr["DTA_VALIDADE_LOTE"]);

                            if (dr["QTD_INSERIDA_ITEM_CARRINHO"] != DBNull.Value)
                                lacreRepositorioItens.QtdDisponivelInserida = Convert.ToInt32(dr["QTD_INSERIDA_ITEM_CARRINHO"]);

                            listaRetorno.Add(lacreRepositorioItens);
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
        /// Excluir.
        /// </summary>
        public void Excluir(Int64 seqLacreRepositorioItem)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("LACRE_REPOSITORIO_ITENS");

                    comando.Params["SEQ_LACRE_REPOSITORIO_ITENS"] = seqLacreRepositorioItem;

                    ctx.ExecuteDelete(comando);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Verificar se existem lacre repositorios itens com quantidade disponível diferente da quantidade necessária.
        /// </summary>
        public bool VerificarSeExistemQtdDisponivelDiferenteDaQtdNecessaria(Int64 seqLacreRepositorio)
        {
            bool existe = false;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT COUNT(*) AS QTD ");
                    str.AppendLine(" FROM LACRE_REPOSITORIO_ITENS RI, ");
                    str.AppendLine("      ITENS_LISTA_CONTROLE IL ");
                    str.AppendLine(" WHERE RI.SEQ_ITENS_LISTA_CONTROLE = IL.SEQ_ITENS_LISTA_CONTROLE ");
                    str.AppendLine(" AND RI.QTD_DISPONIVEL <> IL.QTD_NECESSARIA ");
                    str.AppendLine(string.Format(" AND RI.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            if (dr["QTD"] != DBNull.Value)
                            {
                                if (Convert.ToInt32(dr["QTD"]) > 0)
                                    existe = true;
                            }

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

            return existe;
        }

        /// <summary>
        /// Verificar se o material já foi adicionado para o lacre repositorio itens.
        /// </summary>
        public bool VerificarSeOMaterialJahFoiAdicionadoParaLacreRepositorioItens(Int64 seqLacreRepositorio, Int64 seqItensListaControle, Int64? numLote)
        {
            bool existe = false;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT COUNT(*) AS QTD ");
                    str.AppendLine(" FROM LACRE_REPOSITORIO_ITENS I ");
                    str.AppendLine(string.Format(" WHERE I.SEQ_LACRE_REPOSITORIO = {0} AND I.SEQ_ITENS_LISTA_CONTROLE = {1} ", seqLacreRepositorio, seqItensListaControle));

                    if (numLote != null)
                        str.AppendLine(string.Format(" AND NUM_LOTE = {0} ", numLote.Value));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            if (dr["QTD"] != DBNull.Value)
                            {
                                if (Convert.ToInt32(dr["QTD"]) > 0)
                                    existe = true;
                            }

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

            return existe;
        }

        /// <summary>
        /// Obter por id por seqRepositorioItens e seqAtendimento.
        /// </summary>
        public Int64 ObterSeqLacrerepositorioUtilPorSeqRespositorioItens_e_SeqAtendimento(Hcrp.Infra.AcessoDado.Contexto ctx, Int64 seqLacreRepositorioItens, Int64 seqAtendimento)
        {
            Int64 seqLacreRepositorioUtil = 0;

            try
            {
                QueryCommandConfig query = null;
                StringBuilder str = new StringBuilder();

                str.AppendLine(" SELECT UTI.SEQ_LACRE_REPOSIT_UTIL ");
                str.AppendLine(" FROM LACRE_REPOSIT_UTILIZACAO UTI ");
                str.AppendLine(string.Format(" WHERE UTI.SEQ_LACRE_REPOSITORIO_ITENS = {0} ", seqLacreRepositorioItens));

                if (seqAtendimento > 0)
                    str.AppendLine(string.Format(" AND UTI.SEQ_ATENDIMENTO = {0} ", seqAtendimento));
                else
                    str.AppendLine(" AND UTI.SEQ_ATENDIMENTO IS NULL ");

                query = new QueryCommandConfig(str.ToString());

                // Obter a lista de registros
                ctx.ExecuteQuery(query);

                IDataReader dr = ctx.Reader;

                try
                {
                    while (dr.Read())
                    {
                        if (dr["SEQ_LACRE_REPOSIT_UTIL"] != DBNull.Value)
                            seqLacreRepositorioUtil = Convert.ToInt64(dr["SEQ_LACRE_REPOSIT_UTIL"]);

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

            return seqLacreRepositorioUtil;
        }

        /// <summary>
        /// Obter as quantidades disponíveis para consumo saida.
        /// </summary>
        public List<Entity.QuantidadeRegistroConsumoSaida> ObterQuantidadeDisponivelParaConsumoSaida(Int64 seqLacreRepositorio, Int64 seqAtendimento)
        {
            List<Entity.QuantidadeRegistroConsumoSaida> listRet = new List<Entity.QuantidadeRegistroConsumoSaida>();
            Entity.QuantidadeRegistroConsumoSaida qtdRegConsumoSaida = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT B.SEQ_LACRE_REPOSITORIO_ITENS, ");

                    str.AppendLine("    (SELECT X.QTD_UTILIZADA ");
                    str.AppendLine("        FROM LACRE_REPOSIT_UTILIZACAO X ");
                    str.AppendLine("        WHERE X.SEQ_LACRE_REPOSITORIO_ITENS = B.SEQ_LACRE_REPOSITORIO_ITENS ");

                    if (seqAtendimento == 0)
                        str.AppendLine(" AND X.SEQ_ATENDIMENTO IS NULL AND ROWNUM = 1) ");
                    else
                        str.AppendLine(string.Format(" AND X.SEQ_ATENDIMENTO = {0} AND ROWNUM = 1) ", seqAtendimento));

                    str.AppendLine("         QTD_UTILIZADA_COM_ATEND, ");

                    str.AppendLine("    (SELECT B.QTD_DISPONIVEL - NVL(SUM(X.QTD_UTILIZADA),0) ");
                    str.AppendLine("        FROM LACRE_REPOSIT_UTILIZACAO X ");
                    str.AppendLine("        WHERE X.SEQ_LACRE_REPOSITORIO_ITENS = B.SEQ_LACRE_REPOSITORIO_ITENS) ");
                    str.AppendLine("         QTD_DISPONIVEL_COM_TD_ATEND, ");

                    str.AppendLine("    B.QTD_DISPONIVEL ");

                    str.AppendLine(" FROM LACRE_REPOSITORIO_ITENS B ");
                    str.AppendLine(" WHERE ");
                    str.AppendLine(string.Format(" B.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));
                    str.AppendLine(" ORDER BY B.SEQ_LACRE_REPOSITORIO_ITENS ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            qtdRegConsumoSaida = new Entity.QuantidadeRegistroConsumoSaida();

                            if (dr["SEQ_LACRE_REPOSITORIO_ITENS"] != DBNull.Value)
                                qtdRegConsumoSaida.SeqLacreRepositorioItens = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO_ITENS"]);

                            if (dr["QTD_UTILIZADA_COM_ATEND"] != DBNull.Value)
                                qtdRegConsumoSaida.QtdUtilizadaComAtendimento = Convert.ToInt32(dr["QTD_UTILIZADA_COM_ATEND"]);

                            if (dr["QTD_DISPONIVEL_COM_TD_ATEND"] != DBNull.Value)
                                qtdRegConsumoSaida.QtdDisponivelComTodoAtendimento = Convert.ToInt32(dr["QTD_DISPONIVEL_COM_TD_ATEND"]);

                            if (dr["QTD_DISPONIVEL"] != DBNull.Value)
                                qtdRegConsumoSaida.QtdDisponivel = Convert.ToInt32(dr["QTD_DISPONIVEL"]);


                            listRet.Add(qtdRegConsumoSaida);
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

            return listRet;
        }

        /// <summary>
        /// Obter para copia.
        /// </summary>
        public List<Entity.LacreRepositorioItens> ObterParaCopia(Int64 seqLacreRepositorio)
        {
            List<Entity.LacreRepositorioItens> listRet = new List<Entity.LacreRepositorioItens>();
            Entity.LacreRepositorioItens lacreRepItem = null;

            try
            {
                StringBuilder str = new StringBuilder();

                QueryCommandConfig query;

                // obter o contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                str.AppendLine(" SELECT I.SEQ_LACRE_REPOSITORIO_ITENS, ");
                str.AppendLine("        I.SEQ_LACRE_REPOSITORIO, ");

                str.AppendLine("         NVL((SELECT I.QTD_DISPONIVEL - SUM(X.QTD_UTILIZADA) ");
                str.AppendLine("                 FROM LACRE_REPOSIT_UTILIZACAO X ");
                str.AppendLine("                 WHERE X.SEQ_LACRE_REPOSITORIO_ITENS = ");
                str.AppendLine("                 I.SEQ_LACRE_REPOSITORIO_ITENS),I.QTD_DISPONIVEL) QTD_DISPONIVEL,");
                
                str.AppendLine("        I.COD_MATERIAL, ");
                str.AppendLine("        I.NUM_LOTE, ");
                str.AppendLine("        I.SEQ_ITENS_LISTA_CONTROLE, ");
                str.AppendLine("        I.NUM_LOTE_FABRICANTE, ");
                str.AppendLine("        I.DTA_VALIDADE_LOTE, ");
                str.AppendLine("        I.DTA_CADASTRO, ");
                str.AppendLine("        I.DTA_ULTIMA_ALTERACAO, ");
                str.AppendLine("        I.NUM_USER_CADASTRO, ");
                str.AppendLine("        I.NUM_USER_ULTIMA_ALTERACAO ");
                str.AppendLine(" FROM LACRE_REPOSITORIO_ITENS I ");
                str.AppendLine(string.Format("  WHERE I.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));

                str.AppendLine(" AND (NVL((SELECT I.QTD_DISPONIVEL - SUM(X.QTD_UTILIZADA)  ");
                str.AppendLine("                 FROM LACRE_REPOSIT_UTILIZACAO X   ");
                str.AppendLine("                 WHERE X.SEQ_LACRE_REPOSITORIO_ITENS =   ");
                str.AppendLine("                 I.SEQ_LACRE_REPOSITORIO_ITENS),I.QTD_DISPONIVEL)) > 0  ");

                

                query = new QueryCommandConfig(str.ToString());

                // Obter a lista de registros
                ctx.ExecuteQuery(query);

                IDataReader dr = ctx.Reader;

                try
                {
                    while (dr.Read())
                    {
                        lacreRepItem = new Entity.LacreRepositorioItens();
                        lacreRepItem.LacreRepositorio = new Entity.LacreRepositorio();
                        lacreRepItem.Material = new Entity.Material();
                        lacreRepItem.Lote = new Entity.Lote();
                        lacreRepItem.ItensListaControle = new Entity.ItensListaControle();
                        lacreRepItem.UsuarioCadastro = new Entity.Usuario();
                        lacreRepItem.UsuarioUltimaAlteracao = new Entity.Usuario();

                        if (dr["SEQ_LACRE_REPOSITORIO_ITENS"] != DBNull.Value)
                            lacreRepItem.SeqLacreRepositorioItens = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO_ITENS"]);

                        if (dr["SEQ_LACRE_REPOSITORIO"] != DBNull.Value)
                            lacreRepItem.LacreRepositorio.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO"]);

                        if (dr["QTD_DISPONIVEL"] != DBNull.Value)
                            lacreRepItem.QtdDisponivel = Convert.ToInt32(dr["QTD_DISPONIVEL"]);

                        if (dr["COD_MATERIAL"] != DBNull.Value)
                            lacreRepItem.Material.Codigo = dr["COD_MATERIAL"].ToString();

                        if (dr["NUM_LOTE"] != DBNull.Value)
                            lacreRepItem.Lote.NumLote = Convert.ToInt64(dr["NUM_LOTE"]);

                        if (dr["SEQ_ITENS_LISTA_CONTROLE"] != DBNull.Value)
                            lacreRepItem.ItensListaControle.SeqItensListaControle = Convert.ToInt64(dr["SEQ_ITENS_LISTA_CONTROLE"]);

                        if (dr["NUM_LOTE_FABRICANTE"] != DBNull.Value)
                            lacreRepItem.NumLoteFabricante = dr["NUM_LOTE_FABRICANTE"].ToString();

                        if (dr["DTA_VALIDADE_LOTE"] != DBNull.Value)
                            lacreRepItem.DataValidadeLote = Convert.ToDateTime(dr["DTA_VALIDADE_LOTE"]);

                        if (dr["DTA_CADASTRO"] != DBNull.Value)
                            lacreRepItem.DataCadastro = Convert.ToDateTime(dr["DTA_CADASTRO"]);

                        if (dr["DTA_ULTIMA_ALTERACAO"] != DBNull.Value)
                            lacreRepItem.DataUltimaAlteracao = Convert.ToDateTime(dr["DTA_ULTIMA_ALTERACAO"]);

                        if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                            lacreRepItem.UsuarioCadastro.NumUserBanco = Convert.ToInt32(dr["NUM_USER_CADASTRO"]);

                        if (dr["NUM_USER_ULTIMA_ALTERACAO"] != DBNull.Value)
                            lacreRepItem.UsuarioUltimaAlteracao.NumUserBanco = Convert.ToInt32(dr["NUM_USER_ULTIMA_ALTERACAO"]);

                        listRet.Add(lacreRepItem);
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

            return listRet;
        }

        /// <summary>
        /// Adicionar transacionado
        /// </summary>        
        public long AdicionarTrans(Entity.LacreRepositorioItens lacreRepItens)
        {
            long _seqRetorno = 0;

            try
            {
                // obter o contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                // Preparar o comando
                Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("LACRE_REPOSITORIO_ITENS");

                comando.Params["SEQ_LACRE_REPOSITORIO"] = lacreRepItens.LacreRepositorio.SeqLacreRepositorio;
                comando.Params["QTD_DISPONIVEL"] = lacreRepItens.QtdDisponivel;

                if (lacreRepItens.Material != null && !string.IsNullOrWhiteSpace(lacreRepItens.Material.Codigo))
                    comando.Params["COD_MATERIAL"] = lacreRepItens.Material.Codigo;

                if (lacreRepItens.Lote != null && lacreRepItens.Lote.NumLote > 0)
                    comando.Params["NUM_LOTE"] = lacreRepItens.Lote.NumLote;

                if (lacreRepItens.ItensListaControle != null && lacreRepItens.ItensListaControle.SeqItensListaControle > 0)
                    comando.Params["SEQ_ITENS_LISTA_CONTROLE"] = lacreRepItens.ItensListaControle.SeqItensListaControle;

                if (!string.IsNullOrWhiteSpace(lacreRepItens.NumLoteFabricante))
                    comando.Params["NUM_LOTE_FABRICANTE"] = lacreRepItens.NumLoteFabricante;

                if (lacreRepItens.DataValidadeLote != null)
                    comando.Params["DTA_VALIDADE_LOTE"] = lacreRepItens.DataValidadeLote.Value;

                if (lacreRepItens.DataCadastro != null)
                    comando.Params["DTA_CADASTRO"] = lacreRepItens.DataCadastro.Value;

                if (lacreRepItens.UsuarioCadastro != null && lacreRepItens.UsuarioCadastro.NumUserBanco > 0)
                    comando.Params["NUM_USER_CADASTRO"] = lacreRepItens.UsuarioCadastro.NumUserBanco;

                // Executar o insert
                ctx.ExecuteInsert(comando);

                _seqRetorno = Convert.ToInt64(ctx.GetSequenceValue("SEQ_LACRE_REPOSITORIO_ITENS", false));

            }
            catch (Exception)
            {
                throw;
            }

            return _seqRetorno;
        }

        /// <summary>
        /// Obter para troca de material.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens> ObterParaTrocaDematerial(Int32 codInstituto, int qtdDiasVencer, long seqRepositorio)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens> listLacreRep = new List<Entity.LacreRepositorioItens>();
            Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens lacreRepItens = null;

            try
            {
                StringBuilder str = new StringBuilder();
                StringBuilder strWhere = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;
                    QueryCommandConfig queryCount;

                    #region query where

                    strWhere.AppendLine(" WHERE A.SEQ_LACRE_REPOSITORIO = B.SEQ_LACRE_REPOSITORIO ");
                    strWhere.AppendLine("    AND B.SEQ_ITENS_LISTA_CONTROLE = C.SEQ_ITENS_LISTA_CONTROLE ");
                    strWhere.AppendLine("    AND C.COD_MATERIAL = D.COD_MATERIAL(+) ");
                    strWhere.AppendLine("    AND B.NUM_LOTE = E.NUM_LOTE(+) ");
                    strWhere.AppendLine("    AND D.COD_UNIDADE = F.COD_UNIDADE(+) ");
                    strWhere.AppendLine("    AND D.COD_GRUPO = G.COD_GRUPO(+) ");
                    strWhere.AppendLine("    AND G.COD_ALINEA = H.COD_ALINEA(+) ");
                    strWhere.AppendLine("    AND A.SEQ_REPOSITORIO = I.SEQ_REPOSITORIO ");
                    strWhere.AppendLine("    AND I.NUM_BEM = J.NUM_BEM ");
                    strWhere.AppendLine("    AND J.COD_TIPO_PATRIMONIO = K.COD_TIPO_PATRIMONIO ");
                    strWhere.AppendLine(string.Format(" AND TRUNC(NVL(B.DTA_VALIDADE_LOTE, E.DTA_VALIDADE_LOTE)) <  TRUNC(SYSDATE + {0}) ", qtdDiasVencer));

                    
                    strWhere.AppendLine(" AND (SELECT B.QTD_DISPONIVEL - NVL(SUM(X.QTD_UTILIZADA), 0) ");
                    strWhere.AppendLine(" FROM LACRE_REPOSIT_UTILIZACAO X ");
                    strWhere.AppendLine(" WHERE X.SEQ_LACRE_REPOSITORIO_ITENS = B.SEQ_LACRE_REPOSITORIO_ITENS) > 0 ");

                    strWhere.AppendLine(string.Format(" AND I.SEQ_REPOSITORIO = {0} ", seqRepositorio));

                    strWhere.AppendLine("    AND I.SEQ_LISTA_CONTROLE IN ");
                    strWhere.AppendLine("       (SELECT X.SEQ_LISTA_CONTROLE ");
                    strWhere.AppendLine("       FROM LISTA_CONTROLE X ");
                    strWhere.AppendLine(string.Format(" WHERE X.COD_INSTITUTO = {0}) ", codInstituto));

                    // Ultimo lacre é 
                    strWhere.AppendLine(string.Format(" AND A.SEQ_LACRE_REPOSITORIO = (SELECT MAX(X.SEQ_LACRE_REPOSITORIO) FROM LACRE_REPOSITORIO X WHERE X.SEQ_REPOSITORIO = {0}) ", seqRepositorio));

                    #endregion

                    // Abrir conexão
                    ctx.Open();

                    #region query principal

                    str.AppendLine(" SELECT B.SEQ_LACRE_REPOSITORIO_ITENS, ");
                    str.AppendLine("        NVL(TO_CHAR(J.NUM_PATRIMONIO),'SEM NÚMERO') || ' / ' || K.DSC_TIPO_PATRIMONIO PATRIMONIO,  ");
                    str.AppendLine("        A.SEQ_LACRE_REPOSITORIO, ");
                    str.AppendLine("        A.SEQ_REPOSITORIO, ");
                    str.AppendLine("        I.DSC_IDENTIFICACAO, ");
                    str.AppendLine("        NVL(H.DSC_ALINEA, ");
                    str.AppendLine("        (SELECT X.DSC_ALINEA ");
                    str.AppendLine("            FROM ALINEA X ");
                    str.AppendLine("            WHERE X.COD_ALINEA = C.COD_ALINEA)) DSC_ALINEA, ");
                    str.AppendLine("        NVL(B.COD_MATERIAL, D.COD_MATERIAL) COD_MATERIAL, ");
                    str.AppendLine("        NVL(D.NOM_MATERIAL, C.DSC_MATERIAL) NOM_MATERIAL, ");
                    //str.AppendLine("        E.NUM_LOTE_FABRICANTE, ");
                    str.AppendLine("        C.QTD_NECESSARIA, ");

                    str.AppendLine("            (SELECT B.QTD_DISPONIVEL - NVL(SUM(X.QTD_UTILIZADA),0) ");
                    str.AppendLine("            FROM LACRE_REPOSIT_UTILIZACAO X ");
                    str.AppendLine("            WHERE X.SEQ_LACRE_REPOSITORIO_ITENS = ");
                    str.AppendLine("            B.SEQ_LACRE_REPOSITORIO_ITENS) ");
                    str.AppendLine("            QTD_VENCENDO, ");

                    str.AppendLine("        NVL(B.NUM_LOTE_FABRICANTE, E.NUM_LOTE_FABRICANTE) LOTE_FABRICANTE, ");
                    str.AppendLine("        NVL(B.DTA_VALIDADE_LOTE, E.DTA_VALIDADE_LOTE) DTA_VALIDADE_LOTE ");
                    str.AppendLine(" FROM LACRE_REPOSITORIO          A, ");
                    str.AppendLine("    LACRE_REPOSITORIO_ITENS    B, ");
                    str.AppendLine("    ITENS_LISTA_CONTROLE       C, ");
                    str.AppendLine("    MATERIAL                   D, ");
                    str.AppendLine("    LOTE                       E, ");
                    str.AppendLine("    UNIDADE                    F, ");
                    str.AppendLine("    GRUPO                      G, ");
                    str.AppendLine("    ALINEA                     H, ");
                    str.AppendLine("    REPOSITORIO_LISTA_CONTROLE I, ");
                    str.AppendLine("    BEM_PATRIMONIAL            J, ");
                    str.AppendLine("    TIPO_PATRIMONIO            K ");

                    str.AppendLine(strWhere.ToString());
                    str.AppendLine("  ORDER BY H.DSC_ALINEA ");
                    #endregion

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lacreRepItens = new Entity.LacreRepositorioItens();
                            lacreRepItens.LacreRepositorio = new Entity.LacreRepositorio();
                            lacreRepItens.LacreRepositorio.RepositorioListaControle = new Entity.RepositorioListaControle();
                            lacreRepItens.ItensListaControle = new Entity.ItensListaControle();
                            lacreRepItens.ItensListaControle.Material = new Entity.Material();
                            lacreRepItens.ItensListaControle.Alinea = new Entity.Alinea();
                            lacreRepItens.LacreRepositorio.RepositorioListaControle.BemPatrimonial = new Entity.BemPatrimonial();

                            if (dr["SEQ_LACRE_REPOSITORIO_ITENS"] != DBNull.Value)
                                lacreRepItens.SeqLacreRepositorioItens = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO_ITENS"]);

                            if (dr["PATRIMONIO"] != DBNull.Value)
                                lacreRepItens.LacreRepositorio.RepositorioListaControle.BemPatrimonial.DscTipoPatrimonio = dr["PATRIMONIO"].ToString(); ;

                            if (dr["SEQ_REPOSITORIO"] != DBNull.Value)
                                lacreRepItens.LacreRepositorio.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_REPOSITORIO"]);

                            if (dr["DSC_IDENTIFICACAO"] != DBNull.Value)
                                lacreRepItens.LacreRepositorio.RepositorioListaControle.DscIdentificacao = dr["DSC_IDENTIFICACAO"].ToString();

                            if (dr["DSC_ALINEA"] != DBNull.Value)
                                lacreRepItens.ItensListaControle.Alinea.Nome = dr["DSC_ALINEA"].ToString();

                            if (dr["COD_MATERIAL"] != DBNull.Value)
                                lacreRepItens.ItensListaControle.Material.Codigo = dr["COD_MATERIAL"].ToString();

                            if (dr["NOM_MATERIAL"] != DBNull.Value)
                                lacreRepItens.ItensListaControle.Material.Nome = dr["NOM_MATERIAL"].ToString();

                            if (dr["QTD_NECESSARIA"] != DBNull.Value)
                                lacreRepItens.ItensListaControle.QuantidadeNecessaria = Convert.ToInt32(dr["QTD_NECESSARIA"]);

                            if (dr["QTD_VENCENDO"] != DBNull.Value)
                                lacreRepItens.QtdVencendo = Convert.ToInt32(dr["QTD_VENCENDO"]);

                            if (dr["LOTE_FABRICANTE"] != DBNull.Value)
                                lacreRepItens.NumLoteFabricante = dr["LOTE_FABRICANTE"].ToString();

                            if (dr["DTA_VALIDADE_LOTE"] != DBNull.Value)
                                lacreRepItens.DataValidadeLote = Convert.ToDateTime(dr["DTA_VALIDADE_LOTE"]);

                            listLacreRep.Add(lacreRepItens);
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
        /// Obter para troca de material paginado.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens> ObterParaTrocaDematerialPaginado(Int32 codInstituto, long seqRepositorio,
                                                                                                             int qtdDiasVencer,
                                                                                                             int qtdRegistroPagina,
                                                                                                             int paginaAtual,
                                                                                                             out int totalRegistro)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens> listLacreRep = new List<Entity.LacreRepositorioItens>();
            Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens lacreRepItens = null;
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

                    strWhere.AppendLine(" WHERE A.SEQ_LACRE_REPOSITORIO = B.SEQ_LACRE_REPOSITORIO ");
                    strWhere.AppendLine("    AND B.SEQ_ITENS_LISTA_CONTROLE = C.SEQ_ITENS_LISTA_CONTROLE ");
                    strWhere.AppendLine("    AND C.COD_MATERIAL = D.COD_MATERIAL(+) ");
                    strWhere.AppendLine("    AND B.NUM_LOTE = E.NUM_LOTE(+) ");
                    strWhere.AppendLine("    AND D.COD_UNIDADE = F.COD_UNIDADE(+) ");
                    strWhere.AppendLine("    AND D.COD_GRUPO = G.COD_GRUPO(+) ");
                    strWhere.AppendLine("    AND G.COD_ALINEA = H.COD_ALINEA(+) ");
                    strWhere.AppendLine("    AND A.SEQ_REPOSITORIO = I.SEQ_REPOSITORIO ");
                    strWhere.AppendLine("    AND I.NUM_BEM = J.NUM_BEM(+) ");
                    strWhere.AppendLine("    AND J.COD_TIPO_PATRIMONIO = K.COD_TIPO_PATRIMONIO(+) ");

                    strWhere.AppendLine(" AND C.IDF_ATIVO = 'S' ");

                    strWhere.AppendLine(string.Format(" AND (TRUNC(NVL(B.DTA_VALIDADE_LOTE, E.DTA_VALIDADE_LOTE)) <  TRUNC(SYSDATE + {0}) ", qtdDiasVencer));

                    strWhere.AppendLine(" AND NVL((SELECT B.QTD_DISPONIVEL - SUM(X.QTD_UTILIZADA) ");
                    strWhere.AppendLine("         FROM LACRE_REPOSIT_UTILIZACAO X ");
                    strWhere.AppendLine("        WHERE X.SEQ_LACRE_REPOSITORIO_ITENS = ");
                    strWhere.AppendLine("              B.SEQ_LACRE_REPOSITORIO_ITENS), ");
                    strWhere.AppendLine("       B.QTD_DISPONIVEL) > 0) ");


                    strWhere.AppendLine(string.Format(" AND I.SEQ_REPOSITORIO = {0} ", seqRepositorio));

                    strWhere.AppendLine("    AND I.SEQ_LISTA_CONTROLE IN ");
                    strWhere.AppendLine("       (SELECT X.SEQ_LISTA_CONTROLE ");
                    strWhere.AppendLine("       FROM LISTA_CONTROLE X ");
                    strWhere.AppendLine(string.Format(" WHERE X.COD_INSTITUTO = {0}) ", codInstituto));

                    // Somente itens com quantidade maior que 0
                    strWhere.AppendLine(" AND (SELECT B.QTD_DISPONIVEL - NVL(SUM(X.QTD_UTILIZADA), 0) ");
                    strWhere.AppendLine(" FROM LACRE_REPOSIT_UTILIZACAO X ");
                    strWhere.AppendLine(" WHERE X.SEQ_LACRE_REPOSITORIO_ITENS = B.SEQ_LACRE_REPOSITORIO_ITENS) > 0 ");


                    strWhere.AppendLine(string.Format(" AND A.SEQ_LACRE_REPOSITORIO = (SELECT MAX(X.SEQ_LACRE_REPOSITORIO) FROM LACRE_REPOSITORIO X WHERE X.SEQ_REPOSITORIO = {0}) ", seqRepositorio));

                    #endregion

                    // Abrir conexão
                    ctx.Open();

                    #region query principal

                    str.AppendLine(" SELECT *");
                    str.AppendLine(" FROM (SELECT A.*,");
                    str.AppendLine("              ROWNUM AS RNUM FROM( ");

                    str.AppendLine(" SELECT B.SEQ_LACRE_REPOSITORIO_ITENS, ");
                    str.AppendLine("        NVL(TO_CHAR(J.NUM_PATRIMONIO),'SEM NÚMERO') || ' / ' || K.DSC_TIPO_PATRIMONIO PATRIMONIO,  ");
                    str.AppendLine("        A.SEQ_LACRE_REPOSITORIO, ");
                    str.AppendLine("        A.SEQ_REPOSITORIO, ");
                    str.AppendLine("        I.DSC_IDENTIFICACAO, ");
                    str.AppendLine("        NVL(H.DSC_ALINEA, ");
                    str.AppendLine("        (SELECT X.DSC_ALINEA ");
                    str.AppendLine("            FROM ALINEA X ");
                    str.AppendLine("            WHERE X.COD_ALINEA = C.COD_ALINEA)) DSC_ALINEA, ");
                    str.AppendLine("        NVL(B.COD_MATERIAL, D.COD_MATERIAL) COD_MATERIAL, ");
                    str.AppendLine("        NVL(D.NOM_MATERIAL, C.DSC_MATERIAL) NOM_MATERIAL, ");
                    //str.AppendLine("        E.NUM_LOTE_FABRICANTE, ");
                    str.AppendLine("        C.QTD_NECESSARIA, ");

                    str.AppendLine("            (SELECT B.QTD_DISPONIVEL - NVL(SUM(X.QTD_UTILIZADA),0) ");
                    str.AppendLine("            FROM LACRE_REPOSIT_UTILIZACAO X ");
                    str.AppendLine("            WHERE X.SEQ_LACRE_REPOSITORIO_ITENS = ");
                    str.AppendLine("            B.SEQ_LACRE_REPOSITORIO_ITENS) ");
                    str.AppendLine("            QTD_VENCENDO, ");

                    str.AppendLine("        NVL(B.NUM_LOTE_FABRICANTE, E.NUM_LOTE_FABRICANTE) LOTE_FABRICANTE, ");
                    str.AppendLine("        NVL(B.DTA_VALIDADE_LOTE, E.DTA_VALIDADE_LOTE) DTA_VALIDADE_LOTE ");
                    str.AppendLine(" FROM LACRE_REPOSITORIO          A, ");
                    str.AppendLine("    LACRE_REPOSITORIO_ITENS    B, ");
                    str.AppendLine("    ITENS_LISTA_CONTROLE       C, ");
                    str.AppendLine("    MATERIAL                   D, ");
                    str.AppendLine("    LOTE                       E, ");
                    str.AppendLine("    UNIDADE                    F, ");
                    str.AppendLine("    GRUPO                      G, ");
                    str.AppendLine("    ALINEA                     H, ");
                    str.AppendLine("    REPOSITORIO_LISTA_CONTROLE I, ");
                    str.AppendLine("    BEM_PATRIMONIAL            J, ");
                    str.AppendLine("    TIPO_PATRIMONIO            K ");

                    str.AppendLine(strWhere.ToString());
                    str.AppendLine("  ORDER BY H.DSC_ALINEA  ) A ");
                    str.AppendLine(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");
                    #endregion

                    #region query count
                    strTotalRegistro.AppendLine(" SELECT COUNT(*) TOTAL ");
                    strTotalRegistro.AppendLine(" FROM LACRE_REPOSITORIO        A, ");
                    strTotalRegistro.AppendLine("    LACRE_REPOSITORIO_ITENS    B, ");
                    strTotalRegistro.AppendLine("    ITENS_LISTA_CONTROLE       C, ");
                    strTotalRegistro.AppendLine("    MATERIAL                   D, ");
                    strTotalRegistro.AppendLine("    LOTE                       E, ");
                    strTotalRegistro.AppendLine("    UNIDADE                    F, ");
                    strTotalRegistro.AppendLine("    GRUPO                      G, ");
                    strTotalRegistro.AppendLine("    ALINEA                     H, ");
                    strTotalRegistro.AppendLine("    REPOSITORIO_LISTA_CONTROLE I, ");
                    strTotalRegistro.AppendLine("    BEM_PATRIMONIAL            J, ");
                    strTotalRegistro.AppendLine("    TIPO_PATRIMONIO            K ");
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
                            lacreRepItens = new Entity.LacreRepositorioItens();
                            lacreRepItens.LacreRepositorio = new Entity.LacreRepositorio();
                            lacreRepItens.LacreRepositorio.RepositorioListaControle = new Entity.RepositorioListaControle();
                            lacreRepItens.ItensListaControle = new Entity.ItensListaControle();
                            lacreRepItens.ItensListaControle.Material = new Entity.Material();
                            lacreRepItens.ItensListaControle.Alinea = new Entity.Alinea();
                            lacreRepItens.LacreRepositorio.RepositorioListaControle.BemPatrimonial = new Entity.BemPatrimonial();

                            if (dr["SEQ_LACRE_REPOSITORIO_ITENS"] != DBNull.Value)
                                lacreRepItens.SeqLacreRepositorioItens = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO_ITENS"]);

                            if (dr["PATRIMONIO"] != DBNull.Value)
                                lacreRepItens.LacreRepositorio.RepositorioListaControle.BemPatrimonial.DscTipoPatrimonio = dr["PATRIMONIO"].ToString(); ;

                            if (dr["SEQ_REPOSITORIO"] != DBNull.Value)
                                lacreRepItens.LacreRepositorio.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_REPOSITORIO"]);

                            if (dr["DSC_IDENTIFICACAO"] != DBNull.Value)
                                lacreRepItens.LacreRepositorio.RepositorioListaControle.DscIdentificacao = dr["DSC_IDENTIFICACAO"].ToString();

                            if (dr["DSC_ALINEA"] != DBNull.Value)
                                lacreRepItens.ItensListaControle.Alinea.Nome = dr["DSC_ALINEA"].ToString();

                            if (dr["COD_MATERIAL"] != DBNull.Value)
                                lacreRepItens.ItensListaControle.Material.Codigo = dr["COD_MATERIAL"].ToString();

                            if (dr["NOM_MATERIAL"] != DBNull.Value)
                                lacreRepItens.ItensListaControle.Material.Nome = dr["NOM_MATERIAL"].ToString();

                            if (dr["QTD_NECESSARIA"] != DBNull.Value)
                                lacreRepItens.ItensListaControle.QuantidadeNecessaria = Convert.ToInt32(dr["QTD_NECESSARIA"]);

                            if (dr["QTD_VENCENDO"] != DBNull.Value)
                                lacreRepItens.QtdVencendo = Convert.ToInt32(dr["QTD_VENCENDO"]);

                            if (dr["LOTE_FABRICANTE"] != DBNull.Value)
                                lacreRepItens.NumLoteFabricante = dr["LOTE_FABRICANTE"].ToString();

                            if (dr["DTA_VALIDADE_LOTE"] != DBNull.Value)
                                lacreRepItens.DataValidadeLote = Convert.ToDateTime(dr["DTA_VALIDADE_LOTE"]);

                            listLacreRep.Add(lacreRepItens);
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
        /// Obter para requisição de medicamentos
        /// </summary>
        /// <param name="seqRepositorio">
        /// Repositório
        /// </param>
        /// <param name="Ehsoro">
        /// Existem dois tipos de requisição para Soro e Medicamentos não soro.
        /// </param>
        /// <returns>Lista de itens que estão com a quantidade disponivel menor que a quantidade necessária</returns>
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens> ObterParaRequisicaoDeMaterial(Int64 seqRepositorio, bool Ehsoro)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens> listLacreRep = new List<Entity.LacreRepositorioItens>();
            Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens lacreRepItens = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    #region query principal

                    #region Codigo Comentado
                    //str.AppendLine(" SELECT B.SEQ_LACRE_REPOSITORIO_ITENS, ");
                    //str.AppendLine("    NVL(TO_CHAR(J.NUM_PATRIMONIO),'SEM NÚMERO') || ' / ' || K.DSC_TIPO_PATRIMONIO PATRIMONIO, ");
                    //str.AppendLine("    A.SEQ_LACRE_REPOSITORIO, ");
                    //str.AppendLine("    A.SEQ_REPOSITORIO, ");
                    //str.AppendLine("    I.DSC_IDENTIFICACAO, ");
                    //str.AppendLine("    NVL(H.DSC_ALINEA, ");
                    //str.AppendLine("    (SELECT X.DSC_ALINEA ");
                    //str.AppendLine("        FROM ALINEA X ");
                    //str.AppendLine("        WHERE X.COD_ALINEA = C.COD_ALINEA)) DSC_ALINEA, ");
                    //str.AppendLine("        NVL(B.COD_MATERIAL, D.COD_MATERIAL) COD_MATERIAL, ");
                    //str.AppendLine("    NVL(D.NOM_MATERIAL, C.DSC_MATERIAL) NOM_MATERIAL, ");
                    ////str.AppendLine("    E.NUM_LOTE_FABRICANTE, ");
                    //str.AppendLine("    C.QTD_NECESSARIA, ");

                    //str.AppendLine("    NVL((SELECT B.QTD_DISPONIVEL - SUM(X.QTD_UTILIZADA) ");
                    //str.AppendLine("    FROM LACRE_REPOSIT_UTILIZACAO X ");
                    //str.AppendLine("    WHERE X.SEQ_LACRE_REPOSITORIO_ITENS = ");
                    //str.AppendLine("    B.SEQ_LACRE_REPOSITORIO_ITENS), ");
                    //str.AppendLine("    B.QTD_DISPONIVEL) QTD_VENCENDO, ");

                    //str.AppendLine("    NVL(B.NUM_LOTE_FABRICANTE, E.NUM_LOTE_FABRICANTE) LOTE_FABRICANTE, ");
                    //str.AppendLine("    NVL(B.DTA_VALIDADE_LOTE, E.DTA_VALIDADE_LOTE) DTA_VALIDADE_LOTE ");
                    //str.AppendLine("    FROM LACRE_REPOSITORIO          A, ");
                    //str.AppendLine("    LACRE_REPOSITORIO_ITENS    B, ");
                    //str.AppendLine("    ITENS_LISTA_CONTROLE       C, ");
                    //str.AppendLine("    MATERIAL                   D, ");
                    //str.AppendLine("    LOTE                       E, ");
                    //str.AppendLine("    UNIDADE                    F, ");
                    //str.AppendLine("    GRUPO                      G, ");
                    //str.AppendLine("    ALINEA                     H, ");
                    //str.AppendLine("    REPOSITORIO_LISTA_CONTROLE I, ");
                    //str.AppendLine("    BEM_PATRIMONIAL            J, ");
                    //str.AppendLine("    TIPO_PATRIMONIO            K  ");
                    //str.AppendLine(" WHERE A.SEQ_LACRE_REPOSITORIO = B.SEQ_LACRE_REPOSITORIO ");
                    //str.AppendLine("    AND B.SEQ_ITENS_LISTA_CONTROLE = C.SEQ_ITENS_LISTA_CONTROLE ");
                    //str.AppendLine("    AND C.COD_MATERIAL = D.COD_MATERIAL(+) ");
                    //str.AppendLine("    AND B.NUM_LOTE = E.NUM_LOTE(+) ");
                    //str.AppendLine("    AND D.COD_UNIDADE = F.COD_UNIDADE(+) ");
                    //str.AppendLine("    AND D.COD_GRUPO = G.COD_GRUPO(+) ");
                    //str.AppendLine("    AND G.COD_ALINEA = H.COD_ALINEA(+) ");
                    //str.AppendLine("    AND A.SEQ_REPOSITORIO = I.SEQ_REPOSITORIO ");
                    //str.AppendLine("    AND I.NUM_BEM = J.NUM_BEM ");
                    //str.AppendLine("    AND J.COD_TIPO_PATRIMONIO = K.COD_TIPO_PATRIMONIO ");
                    //str.AppendLine(string.Format(" AND TRUNC(NVL(B.DTA_VALIDADE_LOTE, E.DTA_VALIDADE_LOTE)) <  TRUNC(SYSDATE + {0}) ", qtdDiasVencer));
                    //str.AppendLine(string.Format(" AND I.SEQ_REPOSITORIO = {0} ", seqRepositorio));
                    //str.AppendLine("    ORDER BY DSC_ALINEA ");
                    #endregion

                    str.AppendLine(" SELECT   D.COD_MATERIAL, ");
                    str.AppendLine("                 D.NOM_MATERIAL, ");
                    str.AppendLine("                 A.SEQ_LACRE_REPOSITORIO, ");
                    str.AppendLine("                 A.SEQ_REPOSITORIO, ");
                    str.AppendLine("                 I.DSC_IDENTIFICACAO, C.Qtd_Necessaria, ");
                    str.AppendLine("                 sum(B.Qtd_Disponivel) - SUM(DECODE((SELECT NVL(SUM(X.QTD_UTILIZADA), 0) ");
                    str.AppendLine("                          FROM LACRE_REPOSIT_UTILIZACAO X");
                    str.AppendLine("                         WHERE X.SEQ_LACRE_REPOSITORIO_ITENS =");
                    str.AppendLine("                               B.SEQ_LACRE_REPOSITORIO_ITENS),");
                    str.AppendLine("                        0,");
                    str.AppendLine("                        C.QTD_NECESSARIA,");
                    str.AppendLine("                        (SELECT SUM(B.QTD_DISPONIVEL) - NVL(SUM(X.QTD_UTILIZADA), 0) ");
                    str.AppendLine("                           FROM LACRE_REPOSIT_UTILIZACAO X");
                    str.AppendLine("                          WHERE X.SEQ_LACRE_REPOSITORIO_ITENS =");
                    str.AppendLine("                                B.SEQ_LACRE_REPOSITORIO_ITENS)) ) QTD_REQUISITAR ");
                    str.AppendLine("");
                    str.AppendLine("          FROM LACRE_REPOSITORIO          A, ");
                    str.AppendLine("               LACRE_REPOSITORIO_ITENS    B, ");
                    str.AppendLine("               ITENS_LISTA_CONTROLE       C, ");
                    str.AppendLine("               MATERIAL                   D, ");
                    str.AppendLine("               UNIDADE                    F, ");
                    str.AppendLine("               GRUPO                      G, ");
                    str.AppendLine("               ALINEA                     H, ");
                    str.AppendLine("               REPOSITORIO_LISTA_CONTROLE I, material_medicamento       mat_med ");
                    str.AppendLine("         WHERE A.SEQ_LACRE_REPOSITORIO = B.SEQ_LACRE_REPOSITORIO ");
                    str.AppendLine("           AND B.SEQ_ITENS_LISTA_CONTROLE = C.SEQ_ITENS_LISTA_CONTROLE ");
                    str.AppendLine("           AND C.COD_MATERIAL = D.COD_MATERIAL(+) ");
                    str.AppendLine("           AND D.COD_UNIDADE = F.COD_UNIDADE(+) ");
                    str.AppendLine("           AND D.COD_GRUPO = G.COD_GRUPO(+) ");
                    str.AppendLine("           AND G.COD_ALINEA = H.COD_ALINEA(+) ");
                    str.AppendLine("           AND A.SEQ_REPOSITORIO = I.SEQ_REPOSITORIO ");
                    str.AppendLine("           AND MAT_MED.COD_MATERIAL = D.COD_MATERIAL "); // SOMENTE MEDICAMENTOS

                    str.AppendLine("           AND C.IDF_ATIVO = 'S' ");

                    if (Ehsoro)
                        str.AppendLine(" AND MAT_MED.COD_TIPO_MEDICAMENTO = 5 "); //5 é soro | <> 5 é medicamento
                    else
                        str.AppendLine(" AND MAT_MED.COD_TIPO_MEDICAMENTO <> 5"); //5 é soro | <> 5 é medicamento
                    

                    str.AppendLine("           AND ");
                    str.AppendLine(" ");
                    str.AppendLine("               (SELECT SUM(B.QTD_DISPONIVEL) - NVL(SUM(X.QTD_UTILIZADA), 0) ");
                    str.AppendLine("                  FROM LACRE_REPOSIT_UTILIZACAO X ");
                    str.AppendLine("                 WHERE X.SEQ_LACRE_REPOSITORIO_ITENS = B.SEQ_LACRE_REPOSITORIO_ITENS) < ");
                    str.AppendLine("               C.QTD_NECESSARIA ");
                    str.AppendLine(" ");
                    str.AppendLine(string.Format(" AND I.SEQ_REPOSITORIO = {0} ", seqRepositorio));
                    str.AppendLine(string.Format(" AND A.SEQ_LACRE_REPOSITORIO = (SELECT MAX(X.SEQ_LACRE_REPOSITORIO) FROM LACRE_REPOSITORIO X WHERE X.SEQ_REPOSITORIO = {0}) ", seqRepositorio));
                    str.AppendLine(" ");
                    str.AppendLine("           GROUP BY D.COD_MATERIAL, ");
                    str.AppendLine("                 D.NOM_MATERIAL, ");
                    str.AppendLine("                 A.SEQ_LACRE_REPOSITORIO, ");
                    str.AppendLine("                 A.SEQ_REPOSITORIO, ");
                    str.AppendLine("                 I.DSC_IDENTIFICACAO, C.Qtd_Necessaria ");
                    str.AppendLine("         ORDER BY D.NOM_MATERIAL ");

                    #endregion

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lacreRepItens = new Entity.LacreRepositorioItens();
                            lacreRepItens.LacreRepositorio = new Entity.LacreRepositorio();
                            lacreRepItens.LacreRepositorio.RepositorioListaControle = new Entity.RepositorioListaControle();
                            lacreRepItens.ItensListaControle = new Entity.ItensListaControle();
                            lacreRepItens.ItensListaControle.Material = new Entity.Material();
                            lacreRepItens.ItensListaControle.Alinea = new Entity.Alinea();
                            lacreRepItens.LacreRepositorio.RepositorioListaControle.BemPatrimonial = new Entity.BemPatrimonial();

                            //if (dr["SEQ_LACRE_REPOSITORIO_ITENS"] != DBNull.Value)
                            //    lacreRepItens.SeqLacreRepositorioItens = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO_ITENS"]);

                            //if (dr["PATRIMONIO"] != DBNull.Value)
                            //    lacreRepItens.LacreRepositorio.RepositorioListaControle.BemPatrimonial.DscTipoPatrimonio = dr["PATRIMONIO"].ToString(); 

                            if (dr["SEQ_REPOSITORIO"] != DBNull.Value)
                                lacreRepItens.LacreRepositorio.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_REPOSITORIO"]);

                            if (dr["DSC_IDENTIFICACAO"] != DBNull.Value)
                                lacreRepItens.LacreRepositorio.RepositorioListaControle.DscIdentificacao = dr["DSC_IDENTIFICACAO"].ToString();

                            //if (dr["DSC_ALINEA"] != DBNull.Value)
                            //    lacreRepItens.ItensListaControle.Alinea.Nome = dr["DSC_ALINEA"].ToString();

                            if (dr["COD_MATERIAL"] != DBNull.Value)
                                lacreRepItens.ItensListaControle.Material.Codigo = dr["COD_MATERIAL"].ToString();

                            if (dr["NOM_MATERIAL"] != DBNull.Value)
                                lacreRepItens.ItensListaControle.Material.Nome = dr["NOM_MATERIAL"].ToString();

                            if (dr["QTD_NECESSARIA"] != DBNull.Value)
                                lacreRepItens.ItensListaControle.QuantidadeNecessaria = Convert.ToInt32(dr["QTD_NECESSARIA"]);

                            if (dr["QTD_REQUISITAR"] != DBNull.Value)
                                lacreRepItens.QtdVencendo = Convert.ToInt32(dr["QTD_REQUISITAR"]);

                            //if (dr["LOTE_FABRICANTE"] != DBNull.Value)
                            //    lacreRepItens.NumLoteFabricante = dr["LOTE_FABRICANTE"].ToString();

                            //if (dr["DTA_VALIDADE_LOTE"] != DBNull.Value)
                            //    lacreRepItens.DataValidadeLote = Convert.ToDateTime(dr["DTA_VALIDADE_LOTE"]);

                            listLacreRep.Add(lacreRepItens);
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
        /// Obter por lacre repositorio.
        /// </summary>
        public List<Entity.LacreRepositorioItens> ObterItensNoLacreRepositorio(Int64 seqLacreRepositorio)
        {
            List<Entity.LacreRepositorioItens> listaRetorno = new List<Entity.LacreRepositorioItens>();
            Entity.LacreRepositorioItens lacreRepositorioItens = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT B.SEQ_LACRE_REPOSITORIO_ITENS, ");
                    str.AppendLine("        B.NUM_LOTE, ");
                    str.AppendLine("        A.SEQ_LACRE_REPOSITORIO, ");
                    str.AppendLine("        A.SEQ_REPOSITORIO, ");
                    str.AppendLine("        I.DSC_IDENTIFICACAO, ");
                    str.AppendLine("        H.IDF_CLASSE, ");
                    str.AppendLine("        DECODE(NVL(H.IDF_CLASSE, ");

                    str.AppendLine("        (SELECT X.IDF_CLASSE ");
                    str.AppendLine("            FROM ALINEA X ");
                    str.AppendLine("            WHERE X.COD_ALINEA = C.COD_ALINEA)),1,'MATERIAIS DE CONSUMO',2,'MEDICAMENTOS',3,'CONSUMO DURÁVEL',4,'PATRIMÔNIO','PRESTAÇÃO DE SERVIÇOS') DSC_ALINEA, ");

                    str.AppendLine("        NVL(B.COD_MATERIAL, D.COD_MATERIAL) COD_MATERIAL, ");
                    str.AppendLine("        NVL(D.NOM_MATERIAL, C.DSC_MATERIAL) NOM_MATERIAL, ");
                    str.AppendLine("        NVL(F.NOM_UNIDADE, ");

                    str.AppendLine("        (SELECT X1.NOM_UNIDADE ");
                    str.AppendLine("            FROM UNIDADE X1 ");
                    str.AppendLine("            WHERE X1.COD_UNIDADE = C.COD_UNIDADE)) DSC_UNIDADE_MEDIDA, ");

                    //str.AppendLine("        E.NUM_LOTE_FABRICANTE, ");
                    str.AppendLine("        C.QTD_NECESSARIA, ");


                    str.AppendLine("        (SELECT B.QTD_DISPONIVEL - NVL(SUM(X.QTD_UTILIZADA),0) ");
                    str.AppendLine("                FROM LACRE_REPOSIT_UTILIZACAO X ");
                    str.AppendLine("                WHERE X.SEQ_LACRE_REPOSITORIO_ITENS = ");
                    str.AppendLine("                B.SEQ_LACRE_REPOSITORIO_ITENS) ");
                    str.AppendLine("                 QTD_DISPONIVEL, ");

                    //str.AppendLine("        (SELECT LRU.QTD_UTILIZADA ");
                    //str.AppendLine("          FROM LACRE_REPOSIT_UTILIZACAO LRU ");
                    //str.AppendLine("          WHERE LRU.SEQ_LACRE_REPOSITORIO_ITENS = B.SEQ_LACRE_REPOSITORIO_ITENS ");


                    //if (seqAtendimento != null)
                    //    str.AppendLine(string.Format(" AND LRU.SEQ_ATENDIMENTO = {0} AND ROWNUM = 1 ) AS QTD_UTILIZADA_ATEND, ", seqAtendimento.Value));
                    //else
                    //    str.AppendLine(" AND LRU.SEQ_ATENDIMENTO IS NULL AND ROWNUM = 1) AS QTD_UTILIZADA_ATEND, ");


                    //str.AppendLine("        (SELECT LRU.DSC_JUSTIFICATIVA ");
                    //str.AppendLine("          FROM LACRE_REPOSIT_UTILIZACAO LRU ");
                    //str.AppendLine("          WHERE LRU.SEQ_LACRE_REPOSITORIO_ITENS = B.SEQ_LACRE_REPOSITORIO_ITENS ");


                    //if (seqAtendimento != null)
                    //    str.AppendLine(string.Format(" AND LRU.SEQ_ATENDIMENTO = {0} AND ROWNUM = 1) AS DSC_JUSTIFICATIVA_ATEND, ", seqAtendimento.Value));
                    //else
                    //    str.AppendLine(" AND LRU.SEQ_ATENDIMENTO IS NULL AND ROWNUM = 1) AS DSC_JUSTIFICATIVA_ATEND, ");


                    str.AppendLine("        NVL(B.NUM_LOTE_FABRICANTE, E.NUM_LOTE_FABRICANTE) LOTE_FABRICANTE, ");
                    str.AppendLine("        NVL(B.DTA_VALIDADE_LOTE, E.DTA_VALIDADE_LOTE) DTA_VALIDADE_LOTE, ");

                    str.AppendLine("    B.QTD_DISPONIVEL AS QTD_INSERIDA_ITEM_CARRINHO ");

                    str.AppendLine(" FROM LACRE_REPOSITORIO     A, ");
                    str.AppendLine("    LACRE_REPOSITORIO_ITENS    B, ");
                    str.AppendLine("    ITENS_LISTA_CONTROLE       C, ");
                    str.AppendLine("    MATERIAL                   D, ");
                    str.AppendLine("    LOTE                       E, ");
                    str.AppendLine("    UNIDADE                    F, ");
                    str.AppendLine("    GRUPO                      G, ");
                    str.AppendLine("    ALINEA                     H, ");
                    str.AppendLine("    REPOSITORIO_LISTA_CONTROLE I ");
                    str.AppendLine(" WHERE A.SEQ_LACRE_REPOSITORIO = B.SEQ_LACRE_REPOSITORIO ");
                    str.AppendLine("    AND B.SEQ_ITENS_LISTA_CONTROLE = C.SEQ_ITENS_LISTA_CONTROLE ");
                    str.AppendLine("    AND C.COD_MATERIAL = D.COD_MATERIAL(+) ");
                    str.AppendLine("    AND B.NUM_LOTE = E.NUM_LOTE(+) ");
                    str.AppendLine("    AND D.COD_UNIDADE = F.COD_UNIDADE(+) ");
                    str.AppendLine("    AND D.COD_GRUPO = G.COD_GRUPO(+) ");
                    str.AppendLine("    AND G.COD_ALINEA = H.COD_ALINEA(+) ");
                    str.AppendLine("    AND A.SEQ_REPOSITORIO = I.SEQ_REPOSITORIO ");

                    str.AppendLine("   AND (SELECT B.QTD_DISPONIVEL - NVL(SUM(X.QTD_UTILIZADA),0) ");
                    str.AppendLine("                FROM LACRE_REPOSIT_UTILIZACAO X ");
                    str.AppendLine("                WHERE X.SEQ_LACRE_REPOSITORIO_ITENS = ");
                    str.AppendLine("                B.SEQ_LACRE_REPOSITORIO_ITENS) > 0 ");

                    str.AppendLine(string.Format(" AND A.Seq_Lacre_Repositorio = {0} ", seqLacreRepositorio));
                    str.AppendLine("  ORDER BY DSC_ALINEA DESC, NVL(D.NOM_MATERIAL,B.COD_MATERIAL)  ");


                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lacreRepositorioItens = new Entity.LacreRepositorioItens();
                            lacreRepositorioItens.LacreRepositorio = new Entity.LacreRepositorio();
                            lacreRepositorioItens.Material = new Entity.Material();
                            lacreRepositorioItens.Lote = new Entity.Lote();
                            lacreRepositorioItens.ItensListaControle = new Entity.ItensListaControle();
                            lacreRepositorioItens.UsuarioCadastro = new Entity.Usuario();
                            lacreRepositorioItens.UsuarioUltimaAlteracao = new Entity.Usuario();
                            lacreRepositorioItens.ItensListaControle.Alinea = new Entity.Alinea();
                            lacreRepositorioItens.ItensListaControle.Material = new Entity.Material();
                            lacreRepositorioItens.ItensListaControle.Unidade = new Entity.Unidade();
                            lacreRepositorioItens.LacreRepositorio.RepositorioListaControle = new Entity.RepositorioListaControle();

                            if (dr["SEQ_LACRE_REPOSITORIO_ITENS"] != DBNull.Value)
                                lacreRepositorioItens.SeqLacreRepositorioItens = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO_ITENS"]);

                            if (dr["SEQ_REPOSITORIO"] != DBNull.Value)
                                lacreRepositorioItens.LacreRepositorio.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_REPOSITORIO"]);

                            if (dr["DSC_IDENTIFICACAO"] != DBNull.Value)
                                lacreRepositorioItens.LacreRepositorio.RepositorioListaControle.DscIdentificacao = dr["DSC_IDENTIFICACAO"].ToString();

                            if (dr["IDF_CLASSE"] != DBNull.Value)
                                lacreRepositorioItens.ItensListaControle.Alinea.IdfClasse = Convert.ToInt32(dr["IDF_CLASSE"]);

                            if (dr["DSC_ALINEA"] != DBNull.Value)
                                lacreRepositorioItens.ItensListaControle.Alinea.Nome = dr["DSC_ALINEA"].ToString();

                            if (dr["COD_MATERIAL"] != DBNull.Value)
                                lacreRepositorioItens.Material.Codigo = dr["COD_MATERIAL"].ToString();

                            if (dr["NOM_MATERIAL"] != DBNull.Value)
                                lacreRepositorioItens.Material.Nome = dr["NOM_MATERIAL"].ToString();

                            if (dr["DSC_UNIDADE_MEDIDA"] != DBNull.Value)
                                lacreRepositorioItens.ItensListaControle.Unidade.Nome = dr["DSC_UNIDADE_MEDIDA"].ToString();

                            if (dr["QTD_NECESSARIA"] != DBNull.Value)
                                lacreRepositorioItens.ItensListaControle.QuantidadeNecessaria = Convert.ToInt32(dr["QTD_NECESSARIA"]);

                            if (dr["QTD_DISPONIVEL"] != DBNull.Value)
                                lacreRepositorioItens.QtdDisponivel = Convert.ToInt32(dr["QTD_DISPONIVEL"]);

                            //if (dr["QTD_UTILIZADA_ATEND"] != DBNull.Value)
                            //    lacreRepositorioItens.QtdUtilizada = Convert.ToInt32(dr["QTD_UTILIZADA_ATEND"]);

                            //if (dr["DSC_JUSTIFICATIVA_ATEND"] != DBNull.Value)
                            //    lacreRepositorioItens.DscJustificativaConsumoSemAtendimento = dr["DSC_JUSTIFICATIVA_ATEND"].ToString();

                            if (dr["LOTE_FABRICANTE"] != DBNull.Value)
                                lacreRepositorioItens.Lote.NumLoteFabricante = dr["LOTE_FABRICANTE"].ToString();

                            if (dr["NUM_LOTE"] != DBNull.Value)
                                lacreRepositorioItens.Lote.NumLote = Convert.ToInt64(dr["NUM_LOTE"]);

                            if (dr["DTA_VALIDADE_LOTE"] != DBNull.Value)
                                lacreRepositorioItens.Lote.DataValidadeLote = Convert.ToDateTime(dr["DTA_VALIDADE_LOTE"]);

                            if (dr["QTD_INSERIDA_ITEM_CARRINHO"] != DBNull.Value)
                                lacreRepositorioItens.QtdDisponivelInserida = Convert.ToInt32(dr["QTD_INSERIDA_ITEM_CARRINHO"]);

                            listaRetorno.Add(lacreRepositorioItens);
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

        public string RequisitarItensParaORepositorio(Int64 seqRepositorio, bool Ehsoro)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    using (OracleCommand com = new OracleCommand())
                    {
                        com.Connection = ctx.Conn as OracleConnection;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "GENERICO.PROC_GERA_REQUISICAO_CARRINHO";

                        //com.Parameters.Add("P_COD_INST_SISTEMA", OracleDbType.Int64, ParameterDirection.Input).Value = 1;
                        com.Parameters.Add("P_IDF_TIPO_REQ", OracleDbType.Int64, ParameterDirection.Input).Value = Ehsoro == true ? 1 : 0;
                        com.Parameters.Add("P_SEQ_REPOSITORIO", OracleDbType.Int64, ParameterDirection.Input).Value = seqRepositorio;
                        //com.Parameters.Add("P_NUM_REQUISICAO", OracleDbType.Int64, ParameterDirection.Output).Value = seqRepositorio;

                        com.Parameters.Add("P_NUM_REQ_GERADA", OracleDbType.Varchar2, 50000).Direction = ParameterDirection.Output;
                        com.Parameters.Add("P_DSC_ERRO", OracleDbType.Varchar2, 50000).Direction = ParameterDirection.Output;

                        //com.Parameters.Add("P_NUMERO_REQ", OracleDbType.Varchar2).

                        //com.Parameters.Add("P_ERRO", OracleDbType.Varchar2).Direction = ParameterDirection.Output;

                        com.ExecuteNonQuery();

                        if (!string.IsNullOrWhiteSpace(com.Parameters["P_NUM_REQ_GERADA"].Value.ToString()))
                        {
                            return com.Parameters["P_NUM_REQ_GERADA"].Value.ToString();
                        }
                        else
                        {
                            return com.Parameters["P_DSC_ERRO"].Value.ToString();
                        }
                        
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<Entity.LacreRepositorioItens> ObterItensQtdDispDiferenteQtdNecessario(Int64 seqLacreRepositorio)
        {
            List<Entity.LacreRepositorioItens> listaRetorno = new List<Entity.LacreRepositorioItens>();
            Entity.LacreRepositorioItens lacreRepositorioItens = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT Y.QTD_REQUISITAR, " +
                                       " Y.QTD_CARREGADA," +
                                       " Y.QTD_UTILIZADA," +
                                       " Y.QTD_NECESSARIA," +
                                       " Y.COD_MATERIAL," +
                                       " Y.NOM_MATERIAL," +
                                       " Y.NOM_UNIDADE," +
                                       " Y.SEQ_ITENS_LISTA_CONTROLE ");
                    str.AppendLine("        FROM (SELECT x.QTD_NECESSARIA - (SUM(QTD_DISPONIVEL) - SUM(QTD_UTILIZADA)) QTD_REQUISITAR, ");
                    str.AppendLine("                     SUM(QTD_DISPONIVEL) QTD_CARREGADA, ");
                    str.AppendLine("                     SUM(QTD_UTILIZADA) QTD_UTILIZADA, ");
                    str.AppendLine("                     X.QTD_NECESSARIA, ");
                    str.AppendLine("                     X.COD_MATERIAL, ");
                    str.AppendLine("                     NVL(M.NOM_MATERIAL,X.DSC_MATERIAL) NOM_MATERIAL, ");
                    str.AppendLine("                     X.SEQ_ITENS_LISTA_CONTROLE ");
                    str.AppendLine("                FROM (SELECT A.QTD_DISPONIVEL, ");
                    str.AppendLine("                             NVL(SUM(B.QTD_UTILIZADA), 0) QTD_UTILIZADA, ");
                    str.AppendLine("                             A.COD_MATERIAL, ");
                    str.AppendLine("                             A.NUM_LOTE, ");
                    str.AppendLine("                             A.NUM_LOTE_FABRICANTE, ");
                    str.AppendLine("                             A.SEQ_ITENS_LISTA_CONTROLE, ");
                    str.AppendLine("                             D.QTD_NECESSARIA, ");
                    str.AppendLine("                             D.DSC_MATERIAL ");
                    str.AppendLine("                        FROM LACRE_REPOSITORIO_ITENS  A, ");
                    str.AppendLine("                             LACRE_REPOSIT_UTILIZACAO B, ");
                    str.AppendLine("                             ITENS_LISTA_CONTROLE     D ");

                    str.AppendLine(string.Format(" WHERE A.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));
                    
                    str.AppendLine("                         AND B.SEQ_LACRE_REPOSITORIO_ITENS(+) = ");
                    str.AppendLine("                             A.SEQ_LACRE_REPOSITORIO_ITENS ");
                    str.AppendLine("                         AND A.SEQ_ITENS_LISTA_CONTROLE = ");
                    str.AppendLine("                             D.SEQ_ITENS_LISTA_CONTROLE(+) ");
                    str.AppendLine("                       GROUP BY A.COD_MATERIAL, ");
                    str.AppendLine("                                A.SEQ_ITENS_LISTA_CONTROLE, ");
                    str.AppendLine("                                A.QTD_DISPONIVEL, ");
                    str.AppendLine("                                A.NUM_LOTE, ");
                    str.AppendLine("                                A.NUM_LOTE_FABRICANTE, ");
                    str.AppendLine("                                D.QTD_NECESSARIA, ");
                    str.AppendLine("                                D.DSC_MATERIAL) X, ");
                    str.AppendLine("                     MATERIAL M, ");
                    str.AppendLine("                     UNIDADE U, ");
                    str.AppendLine("                     Itens_Lista_Controle itens_lista ");
                    str.AppendLine("               WHERE X.COD_MATERIAL = M.COD_MATERIAL(+) ");
                    str.AppendLine("                 AND M.COD_UNIDADE = U.COD_UNIDADE(+) ");
                    str.AppendLine("                 AND ITENS_LISTA.SEQ_ITENS_LISTA_CONTROLE = X.SEQ_ITENS_LISTA_CONTROLE ");
                    str.AppendLine("               GROUP BY x.SEQ_ITENS_LISTA_CONTROLE, ");
                    str.AppendLine("                        X.COD_MATERIAL, ");
                    str.AppendLine("                        X.QTD_NECESSARIA, ");
                    str.AppendLine("                        M.NOM_MATERIAL, ");
                    str.AppendLine("                        X.DSC_MATERIAL) Y ");

                    str.AppendLine("       WHERE Y.QTD_REQUISITAR > 0; ");
 
                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lacreRepositorioItens = new Entity.LacreRepositorioItens();
                            lacreRepositorioItens.LacreRepositorio = new Entity.LacreRepositorio();
                            lacreRepositorioItens.Material = new Entity.Material();
                            lacreRepositorioItens.Lote = new Entity.Lote();
                            lacreRepositorioItens.ItensListaControle = new Entity.ItensListaControle();
                            lacreRepositorioItens.UsuarioCadastro = new Entity.Usuario();
                            lacreRepositorioItens.UsuarioUltimaAlteracao = new Entity.Usuario();
                            lacreRepositorioItens.ItensListaControle.Alinea = new Entity.Alinea();
                            lacreRepositorioItens.ItensListaControle.Material = new Entity.Material();
                            lacreRepositorioItens.ItensListaControle.Unidade = new Entity.Unidade();
                            lacreRepositorioItens.LacreRepositorio.RepositorioListaControle = new Entity.RepositorioListaControle();

                            if (dr["SEQ_LACRE_REPOSITORIO_ITENS"] != DBNull.Value)
                                lacreRepositorioItens.SeqLacreRepositorioItens = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO_ITENS"]);

                            if (dr["SEQ_REPOSITORIO"] != DBNull.Value)
                                lacreRepositorioItens.LacreRepositorio.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_REPOSITORIO"]);

                            if (dr["DSC_IDENTIFICACAO"] != DBNull.Value)
                                lacreRepositorioItens.LacreRepositorio.RepositorioListaControle.DscIdentificacao = dr["DSC_IDENTIFICACAO"].ToString();

                            if (dr["DSC_ALINEA"] != DBNull.Value)
                                lacreRepositorioItens.ItensListaControle.Alinea.Nome = dr["DSC_ALINEA"].ToString();

                            if (dr["COD_MATERIAL"] != DBNull.Value)
                                lacreRepositorioItens.Material.Codigo = dr["COD_MATERIAL"].ToString();

                            if (dr["NOM_MATERIAL"] != DBNull.Value)
                                lacreRepositorioItens.Material.Nome = dr["NOM_MATERIAL"].ToString();

                            if (dr["DSC_UNIDADE_MEDIDA"] != DBNull.Value)
                                lacreRepositorioItens.ItensListaControle.Unidade.Nome = dr["DSC_UNIDADE_MEDIDA"].ToString();

                            if (dr["QTD_NECESSARIA"] != DBNull.Value)
                                lacreRepositorioItens.ItensListaControle.QuantidadeNecessaria = Convert.ToInt32(dr["QTD_NECESSARIA"]);

                            if (dr["QTD_DISPONIVEL"] != DBNull.Value)
                                lacreRepositorioItens.QtdDisponivel = Convert.ToInt32(dr["QTD_DISPONIVEL"]);

                            //if (dr["QTD_UTILIZADA_ATEND"] != DBNull.Value)
                            //    lacreRepositorioItens.QtdUtilizada = Convert.ToInt32(dr["QTD_UTILIZADA_ATEND"]);

                            //if (dr["DSC_JUSTIFICATIVA_ATEND"] != DBNull.Value)
                            //    lacreRepositorioItens.DscJustificativaConsumoSemAtendimento = dr["DSC_JUSTIFICATIVA_ATEND"].ToString();

                            if (dr["LOTE_FABRICANTE"] != DBNull.Value)
                                lacreRepositorioItens.Lote.NumLoteFabricante = dr["LOTE_FABRICANTE"].ToString();

                            if (dr["NUM_LOTE"] != DBNull.Value)
                                lacreRepositorioItens.Lote.NumLote = Convert.ToInt64(dr["NUM_LOTE"]);

                            if (dr["DTA_VALIDADE_LOTE"] != DBNull.Value)
                                lacreRepositorioItens.Lote.DataValidadeLote = Convert.ToDateTime(dr["DTA_VALIDADE_LOTE"]);

                            if (dr["QTD_INSERIDA_ITEM_CARRINHO"] != DBNull.Value)
                                lacreRepositorioItens.QtdDisponivelInserida = Convert.ToInt32(dr["QTD_INSERIDA_ITEM_CARRINHO"]);

                            listaRetorno.Add(lacreRepositorioItens);
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
