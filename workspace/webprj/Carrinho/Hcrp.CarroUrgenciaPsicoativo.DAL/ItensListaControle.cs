using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hcrp.Infra.AcessoDado;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class ItensListaControle
    {
        #region Métodos

        /// <summary>
        /// Obter itens por lista controle
        /// </summary>
        /// <param name="seqListaControle"></param>
        /// <param name="status">S - Ativos N - Inativos</param>
        /// <returns></returns>
        public List<Entity.ItensListaControle> ObterItensPorListaControle(long seqListaControle, bool statusItensAtivos)
        {
            List<Entity.ItensListaControle> listaRetorno = new List<Entity.ItensListaControle>();
            Entity.ItensListaControle _itemListaControle = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine("select * from ( SELECT A.SEQ_ITENS_LISTA_CONTROLE, ");
                    str.AppendLine("        DECODE(E.IDF_CLASSE,1,'MATERIAIS DE CONSUMO',2,'MEDICAMENTOS',3,'CONSUMO DURÁVEL',4,'PATRIMÔNIO','PRESTAÇÃO DE SERVIÇOS') DSC_ALINEA, ");
                    str.AppendLine("        A.COD_MATERIAL, ");
                    str.AppendLine("        B.NOM_MATERIAL, ");
                    str.AppendLine("        A.QTD_NECESSARIA || ' - ' || F.NOM_UNIDADE QTD, ");
                    str.AppendLine("        A.QTD_NECESSARIA, ");
                    str.AppendLine("        F.NOM_UNIDADE, ");
                    str.AppendLine("        A.IDF_ATIVO ");
                    str.AppendLine("   FROM ITENS_LISTA_CONTROLE A, ");
                    str.AppendLine("        MATERIAL             B, ");
                    str.AppendLine("        SUB_GRUPO            C, ");
                    str.AppendLine("        ESTOQUE.GRUPO        D, ");
                    str.AppendLine("        ESTOQUE.ALINEA       E, ");
                    str.AppendLine("        PRESCRICAO.UNIDADE   F ");
                    str.AppendLine(" WHERE A.COD_MATERIAL = B.COD_MATERIAL ");
                    str.AppendLine("    AND B.COD_GRUPO = C.COD_GRUPO ");
                    str.AppendLine("    AND B.COD_SUB_GRUPO = C.COD_SUB_GRUPO ");
                    str.AppendLine("    AND D.COD_GRUPO = C.COD_GRUPO ");
                    str.AppendLine("    AND E.COD_ALINEA = D.COD_ALINEA ");
                    str.AppendLine("    AND B.COD_UNIDADE = F.COD_UNIDADE ");
                    str.AppendLine(string.Format("    AND A.SEQ_LISTA_CONTROLE = {0} ", seqListaControle));
                    str.AppendLine(string.Format("   AND A.IDF_ATIVO = '{0}' ", statusItensAtivos == true ? "N" : "S"));
                    str.AppendLine(" UNION ALL ");
                    str.AppendLine(" SELECT A.SEQ_ITENS_LISTA_CONTROLE, ");
                    str.AppendLine("        B.DSC_ALINEA, ");
                    str.AppendLine("        A.COD_MATERIAL, ");
                    str.AppendLine("        A.DSC_MATERIAL, ");
                    str.AppendLine("        A.QTD_NECESSARIA || ' - ' || C.NOM_UNIDADE QTD, ");
                    str.AppendLine("        A.QTD_NECESSARIA, ");
                    str.AppendLine("        C.NOM_UNIDADE, ");
                    str.AppendLine("        A.IDF_ATIVO ");
                    str.AppendLine("   FROM ITENS_LISTA_CONTROLE A, ");
                    str.AppendLine("        ESTOQUE.ALINEA       B, ");
                    str.AppendLine("        PRESCRICAO.UNIDADE   C ");
                    str.AppendLine(" WHERE A.COD_ALINEA = B.COD_ALINEA ");
                    str.AppendLine("   AND A.COD_UNIDADE = C.COD_UNIDADE ");
                    str.AppendLine(string.Format("    AND A.SEQ_LISTA_CONTROLE = {0} ", seqListaControle));
                    str.AppendLine(string.Format("   AND A.IDF_ATIVO = '{0}' ", statusItensAtivos == true ? "N" : "S"));
                    str.AppendLine(" UNION ALL ");
                    str.AppendLine(" SELECT A.SEQ_ITENS_LISTA_CONTROLE, ");
                    str.AppendLine("        'EQUIPAMENTO' DSC_ALINEA, ");
                    str.AppendLine("        TO_CHAR(D.COD_TIPO_BEM) as COD_MATERIAL, ");
                    str.AppendLine("        D.NOM_TIPO_BEM as DSC_MATERIAL, ");
                    str.AppendLine("        A.QTD_NECESSARIA || ' - ' || 'UMA' QTD, ");
                    str.AppendLine("        A.QTD_NECESSARIA, ");
                    str.AppendLine("        'UMA' UNIDADE, ");
                    str.AppendLine("        A.IDF_ATIVO ");
                    str.AppendLine("   FROM ITENS_LISTA_CONTROLE A, ");
                    str.AppendLine("        Manutencao.Tipo_Bem  D ");
                    str.AppendLine("  WHERE ");
                    str.AppendLine(string.Format("   A.SEQ_LISTA_CONTROLE = {0} ", seqListaControle));
                    str.AppendLine(string.Format("   AND A.IDF_ATIVO = '{0}' ", statusItensAtivos == true ? "N" : "S"));
                    str.AppendLine("    AND A.COD_TIPO_BEM IS NOT NULL ");
                    str.AppendLine("    AND D.COD_TIPO_BEM = A.COD_TIPO_BEM ");
                    str.AppendLine("    ) X ORDER BY X.DSC_ALINEA DESC , X.NOM_MATERIAL ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            _itemListaControle = new Entity.ItensListaControle();
                            _itemListaControle.ListaControle = new Entity.ListaControle();
                            _itemListaControle.Material = new Entity.Material();
                            _itemListaControle.Alinea = new Entity.Alinea();
                            _itemListaControle.Unidade = new Entity.Unidade();

                            if (dr["SEQ_ITENS_LISTA_CONTROLE"] != DBNull.Value)
                                _itemListaControle.SeqItensListaControle = Convert.ToInt64(dr["SEQ_ITENS_LISTA_CONTROLE"]);

                            if (dr["DSC_ALINEA"] != DBNull.Value)
                                _itemListaControle.Alinea.Nome = dr["DSC_ALINEA"].ToString();

                            if (dr["COD_MATERIAL"] != DBNull.Value)
                                _itemListaControle.Material.Codigo = dr["COD_MATERIAL"].ToString();
                            else
                                _itemListaControle.Material.Codigo = string.Empty;

                            if (dr["NOM_MATERIAL"] != DBNull.Value)
                                _itemListaControle.Material.Nome = dr["NOM_MATERIAL"].ToString();
                            else
                                _itemListaControle.Material.Nome = string.Empty;

                            if (dr["QTD_NECESSARIA"] != DBNull.Value)
                                _itemListaControle.QuantidadeNecessaria = Convert.ToInt32(dr["QTD_NECESSARIA"]);

                            if (dr["NOM_UNIDADE"] != DBNull.Value)
                                _itemListaControle.Unidade.Nome = dr["NOM_UNIDADE"].ToString();

                            if (dr["IDF_ATIVO"] != DBNull.Value)
                                _itemListaControle.IdfAtivo = dr["IDF_ATIVO"].ToString();

                            listaRetorno.Add(_itemListaControle);
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
        /// Adicionar itens lista controle
        /// </summary>
        /// <param name="_itensListaControle"></param>
        /// <returns></returns>
        public long Adicionar(Entity.ItensListaControle _itensListaControle)
        {
            long _seqRetorno = 0;

            try
            {
                // Criar contexto
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("ITENS_LISTA_CONTROLE");

                    comando.Params["SEQ_LISTA_CONTROLE"] = _itensListaControle.ListaControle.SeqListaControle;

                    if (_itensListaControle.Material != null)
                    {
                        comando.Params["COD_MATERIAL"] = _itensListaControle.Material.Codigo;
                    }
                    else if (!string.IsNullOrWhiteSpace(_itensListaControle.DescricaoMaterial))
                    {
                        comando.Params["DSC_MATERIAL"] = _itensListaControle.DescricaoMaterial;
                        comando.Params["COD_UNIDADE"] = _itensListaControle.Unidade.Codigo;
                        comando.Params["COD_ALINEA"] = _itensListaControle.Alinea.Codigo;
                    }

                    comando.Params["QTD_NECESSARIA"] = _itensListaControle.QuantidadeNecessaria;

                    if (_itensListaControle.TipoBem.CodigoTipoBem.HasValue)
                        comando.Params["COD_TIPO_BEM"] = _itensListaControle.TipoBem.CodigoTipoBem;

                    // Trigger na tabela
                    //comando.Params["NUM_USER_CADASTRO"] = 59851;//_itensListaControle.UsuarioCadastro.NumUserBanco;

                    comando.Params["IDF_ATIVO"] = "S";

                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                    _seqRetorno = Convert.ToInt64(ctx.GetSequenceValue("SEQ_ITENS_LISTA_CONTROLE", false));
                }
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
        public void AtivarOuInativar(bool ehPraAtivar, long seqItemListaControle)
        {
            try
            {
                // obter o contexto transacionado
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("ITENS_LISTA_CONTROLE");

                    comando.FilterParams["SEQ_ITENS_LISTA_CONTROLE"] = seqItemListaControle;

                    comando.Params["IDF_ATIVO"] = (ehPraAtivar ? "S" : "N");

                    //comando.Params["DTA_ULTIMA_ALTERACAO"] = DateTime.Now;

                    //comando.Params["NUM_USER_ULTIMA_ALTERACAO"] = numUsuarioAlteracao;

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

        /// <summary>
        /// Obter itens por código de material e seq lacre repositório.
        /// </summary>        
        public Entity.ItensListaControle ObterItensPorCodMaterialESeqLacreRespositorio(Int64 seqLacreRepositorio, string codMaterial)
        {
            Entity.ItensListaControle _itemListaControle = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT A.SEQ_ITENS_LISTA_CONTROLE, ");
                    str.AppendLine("        E.DSC_ALINEA, ");
                    str.AppendLine("        A.COD_MATERIAL, ");
                    str.AppendLine("        B.NOM_MATERIAL, ");
                    str.AppendLine("        A.QTD_NECESSARIA || ' - ' || F.NOM_UNIDADE QTD, A.QTD_NECESSARIA, F.NOM_UNIDADE ");
                    str.AppendLine(" FROM ITENS_LISTA_CONTROLE A, ");
                    str.AppendLine("        MATERIAL             B, ");
                    str.AppendLine("        SUB_GRUPO            C, ");
                    str.AppendLine("        ESTOQUE.GRUPO        D, ");
                    str.AppendLine("        ESTOQUE.ALINEA       E, ");
                    str.AppendLine("        PRESCRICAO.UNIDADE   F, ");
                    str.AppendLine("        REPOSITORIO_LISTA_CONTROLE RLC, ");
                    str.AppendLine("        LACRE_REPOSITORIO LR ");
                    str.AppendLine(" WHERE A.COD_MATERIAL = B.COD_MATERIAL ");
                    str.AppendLine(" AND B.COD_GRUPO = C.COD_GRUPO ");
                    str.AppendLine(" AND B.COD_SUB_GRUPO = C.COD_SUB_GRUPO ");
                    str.AppendLine(" AND D.COD_GRUPO = C.COD_GRUPO ");
                    str.AppendLine(" AND E.COD_ALINEA = D.COD_ALINEA ");
                    str.AppendLine(" AND B.COD_UNIDADE = F.COD_UNIDADE ");
                    //str.AppendLine(" AND A.IDF_ATIVO = 'S' ");
                    str.AppendLine(" AND A.SEQ_LISTA_CONTROLE = RLC.SEQ_LISTA_CONTROLE ");
                    str.AppendLine(" AND RLC.SEQ_REPOSITORIO = LR.SEQ_REPOSITORIO ");
                    str.AppendLine(string.Format(" AND LR.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));
                    str.AppendLine(string.Format(" AND A.COD_MATERIAL = '{0}' ", codMaterial));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            _itemListaControle = new Entity.ItensListaControle();
                            _itemListaControle.ListaControle = new Entity.ListaControle();
                            _itemListaControle.Material = new Entity.Material();
                            _itemListaControle.Alinea = new Entity.Alinea();
                            _itemListaControle.Unidade = new Entity.Unidade();

                            if (dr["SEQ_ITENS_LISTA_CONTROLE"] != DBNull.Value)
                                _itemListaControle.SeqItensListaControle = Convert.ToInt64(dr["SEQ_ITENS_LISTA_CONTROLE"]);

                            if (dr["DSC_ALINEA"] != DBNull.Value)
                                _itemListaControle.Alinea.Nome = dr["DSC_ALINEA"].ToString();

                            if (dr["COD_MATERIAL"] != DBNull.Value)
                                _itemListaControle.Material.Codigo = dr["COD_MATERIAL"].ToString();

                            if (dr["NOM_MATERIAL"] != DBNull.Value)
                                _itemListaControle.Material.Nome = dr["NOM_MATERIAL"].ToString();

                            if (dr["QTD_NECESSARIA"] != DBNull.Value)
                                _itemListaControle.QuantidadeNecessaria = Convert.ToInt32(dr["QTD_NECESSARIA"]);

                            if (dr["NOM_UNIDADE"] != DBNull.Value)
                                _itemListaControle.Unidade.Nome = dr["NOM_UNIDADE"].ToString();

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

            return _itemListaControle;
        }

        /// <summary>
        /// Obter lista controle itens com materiais com código paginado.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle> ObterListaDeListaControleItemComMaterialComCodigoPaginado(Int64 seqLacreRepositorio,
                                                                                                                                     string codigo,
                                                                                                                                     string descricao,
                                                                                                                                     int qtdRegistroPorPagina,
                                                                                                                                     int paginaAtual,
                                                                                                                                     out int totalRegistro)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle> listaRetorno = new List<Entity.ItensListaControle>();
            Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle itemListaControle;
            totalRegistro = 0;

            try
            {
                StringBuilder str = new StringBuilder();
                StringBuilder strTotalRegistro = new StringBuilder();
                StringBuilder strWhere = new StringBuilder();

                // Montar escopo de paginação.
                Int32 numeroRegistroPorPagina = qtdRegistroPorPagina;
                Int32 ultimoIndice = (numeroRegistroPorPagina * paginaAtual);
                Int32 primeiroIndice = (ultimoIndice - numeroRegistroPorPagina) + 1;

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Where da Query
                    strWhere.AppendLine(" WHERE 1 = 1 ");

                    strWhere.AppendLine(" AND I.COD_MATERIAL = M.COD_MATERIAL ");
                    strWhere.AppendLine(" AND G.COD_GRUPO = M.COD_GRUPO ");
                    strWhere.AppendLine(" AND G.COD_ALINEA = ALI.COD_ALINEA ");
                    strWhere.AppendLine(" AND I.SEQ_LISTA_CONTROLE = RLC.SEQ_LISTA_CONTROLE ");
                    strWhere.AppendLine(" AND RLC.SEQ_REPOSITORIO = LR.SEQ_REPOSITORIO ");
                    strWhere.AppendLine(" AND M.COD_UNIDADE = UN.COD_UNIDADE ");
                    strWhere.AppendLine(string.Format(" AND LR.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));

                    if (!string.IsNullOrWhiteSpace(codigo))
                        strWhere.AppendLine(string.Format(" AND I.COD_MATERIAL LIKE '%{0}%' ", codigo.Replace("'", "").Replace("%", "")));

                    if (!string.IsNullOrWhiteSpace(descricao))
                        strWhere.AppendLine(string.Format(" AND M.NOM_MATERIAL LIKE '%{0}%' ", descricao.Replace("'", "").Replace("%", "")));

                    // Query Principal
                    str.AppendLine("SELECT * ");
                    str.AppendLine(" FROM (SELECT A.*, ");
                    str.AppendLine("  ROWNUM AS RNUM FROM ( ");

                    str.AppendLine(" SELECT I.SEQ_ITENS_LISTA_CONTROLE, ");
                    str.AppendLine("        I.COD_MATERIAL, ");
                    str.AppendLine("        M.NOM_MATERIAL, ");
                    str.AppendLine("        ALI.COD_ALINEA, ");
                    str.AppendLine("        ALI.DSC_ALINEA, ");
                    str.AppendLine("        UN.NOM_UNIDADE ");

                    str.AppendLine(" FROM ITENS_LISTA_CONTROLE I, ");
                    str.AppendLine("        MATERIAL M, ");
                    str.AppendLine("        GRUPO G, ");
                    str.AppendLine("        ALINEA ALI, ");
                    str.AppendLine("        REPOSITORIO_LISTA_CONTROLE RLC, ");
                    str.AppendLine("        LACRE_REPOSITORIO LR, ");
                    str.AppendLine("        UNIDADE UN ");
                    str.AppendLine(strWhere.ToString());

                    str.AppendLine(" ORDER BY M.NOM_MATERIAL) A ");
                    str.AppendLine(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

                    strTotalRegistro.AppendLine(" SELECT COUNT(*) AS TOTAL  ");
                    strTotalRegistro.AppendLine(" FROM ITENS_LISTA_CONTROLE I, ");
                    strTotalRegistro.AppendLine(" MATERIAL M, ");
                    strTotalRegistro.AppendLine(" GRUPO G, ");
                    strTotalRegistro.AppendLine(" ALINEA ALI, ");
                    strTotalRegistro.AppendLine(" REPOSITORIO_LISTA_CONTROLE RLC, ");
                    strTotalRegistro.AppendLine(" LACRE_REPOSITORIO LR, ");
                    strTotalRegistro.AppendLine(" UNIDADE UN ");
                    strTotalRegistro.AppendLine(strWhere.ToString());

                    query = new QueryCommandConfig(str.ToString());
                    QueryCommandConfig queryCount = new QueryCommandConfig(strTotalRegistro.ToString());

                    // Veriricar contador
                    ctx.ExecuteQuery(queryCount);

                    while (ctx.Reader.Read())
                    {
                        totalRegistro = Convert.ToInt32(ctx.Reader["TOTAL"]);
                        break;
                    }

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    // Preparar o retorno
                    while (ctx.Reader.Read())
                    {
                        itemListaControle = new Entity.ItensListaControle();
                        itemListaControle.Material = new Entity.Material();
                        itemListaControle.Alinea = new Entity.Alinea();
                        itemListaControle.Unidade = new Entity.Unidade();

                        if (ctx.Reader["SEQ_ITENS_LISTA_CONTROLE"] != DBNull.Value)
                            itemListaControle.SeqItensListaControle = Convert.ToInt64(ctx.Reader["SEQ_ITENS_LISTA_CONTROLE"]);

                        if (ctx.Reader["COD_MATERIAL"] != DBNull.Value)
                            itemListaControle.Material.Codigo = ctx.Reader["COD_MATERIAL"].ToString();

                        if (ctx.Reader["NOM_MATERIAL"] != DBNull.Value)
                            itemListaControle.Material.Nome = ctx.Reader["NOM_MATERIAL"].ToString();

                        if (ctx.Reader["COD_ALINEA"] != DBNull.Value)
                            itemListaControle.Alinea.Codigo = ctx.Reader["COD_ALINEA"].ToString();

                        if (ctx.Reader["DSC_ALINEA"] != DBNull.Value)
                            itemListaControle.Alinea.Nome = ctx.Reader["DSC_ALINEA"].ToString();

                        if (ctx.Reader["NOM_UNIDADE"] != DBNull.Value)
                            itemListaControle.Unidade.Nome = ctx.Reader["NOM_UNIDADE"].ToString();

                        listaRetorno.Add(itemListaControle);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return listaRetorno;
        }

        /// <summary>
        /// Obter itens por id.
        /// </summary>        
        public Entity.ItensListaControle ObterItensPorId(Int64 seqListaControleItem)
        {
            Entity.ItensListaControle _itemListaControle = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT A.SEQ_ITENS_LISTA_CONTROLE, ");
                    str.AppendLine("        E.DSC_ALINEA, ");
                    str.AppendLine("        A.COD_MATERIAL, ");
                    str.AppendLine("        B.NOM_MATERIAL, ");
                    str.AppendLine("        A.QTD_NECESSARIA || ' - ' || F.NOM_UNIDADE QTD, A.QTD_NECESSARIA, F.NOM_UNIDADE ");
                    str.AppendLine("        FROM ITENS_LISTA_CONTROLE A, ");
                    str.AppendLine("        MATERIAL             B, ");
                    str.AppendLine("        SUB_GRUPO            C, ");
                    str.AppendLine("        ESTOQUE.GRUPO        D, ");
                    str.AppendLine("        ESTOQUE.ALINEA       E, ");
                    str.AppendLine("        PRESCRICAO.UNIDADE   F ");

                    str.AppendLine(" WHERE A.COD_MATERIAL = B.COD_MATERIAL(+) ");
                    str.AppendLine("        AND B.COD_GRUPO = C.COD_GRUPO(+) ");
                    str.AppendLine("        AND B.COD_SUB_GRUPO = C.COD_SUB_GRUPO(+) ");
                    str.AppendLine("        AND C.COD_GRUPO = D.COD_GRUPO(+) ");
                    str.AppendLine("        AND D.COD_ALINEA = E.COD_ALINEA(+) ");
                    str.AppendLine("        AND B.COD_UNIDADE = F.COD_UNIDADE(+) ");
                    //str.AppendLine("        AND A.IDF_ATIVO = 'S' ");
                    str.AppendLine(string.Format(" AND A.SEQ_ITENS_LISTA_CONTROLE = {0} ", seqListaControleItem));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            _itemListaControle = new Entity.ItensListaControle();
                            _itemListaControle.ListaControle = new Entity.ListaControle();
                            _itemListaControle.Material = new Entity.Material();
                            _itemListaControle.Alinea = new Entity.Alinea();
                            _itemListaControle.Unidade = new Entity.Unidade();

                            if (dr["SEQ_ITENS_LISTA_CONTROLE"] != DBNull.Value)
                                _itemListaControle.SeqItensListaControle = Convert.ToInt64(dr["SEQ_ITENS_LISTA_CONTROLE"]);

                            if (dr["DSC_ALINEA"] != DBNull.Value)
                                _itemListaControle.Alinea.Nome = dr["DSC_ALINEA"].ToString();

                            if (dr["COD_MATERIAL"] != DBNull.Value)
                                _itemListaControle.Material.Codigo = dr["COD_MATERIAL"].ToString();

                            if (dr["NOM_MATERIAL"] != DBNull.Value)
                                _itemListaControle.Material.Nome = dr["NOM_MATERIAL"].ToString();

                            if (dr["QTD_NECESSARIA"] != DBNull.Value)
                                _itemListaControle.QuantidadeNecessaria = Convert.ToInt32(dr["QTD_NECESSARIA"]);

                            if (dr["NOM_UNIDADE"] != DBNull.Value)
                                _itemListaControle.Unidade.Nome = dr["NOM_UNIDADE"].ToString();

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

            return _itemListaControle;
        }

        /// <summary>
        /// Obter lista controle itens com materiais sem código.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle> ObterListaDeListaControleItemComMaterialSemCodigo(Int64 seqLacreRepositorio)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle> listaRetorno = new List<Entity.ItensListaControle>();
            Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle itemListaControle;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT I.SEQ_ITENS_LISTA_CONTROLE, ");
                    str.AppendLine("        I.DSC_MATERIAL, ");
                    str.AppendLine("        I.COD_ALINEA, ");
                    str.AppendLine("        ALI.DSC_ALINEA ");
                    str.AppendLine(" FROM ITENS_LISTA_CONTROLE I, ");
                    str.AppendLine("      ALINEA ALI, ");
                    str.AppendLine("      REPOSITORIO_LISTA_CONTROLE RLC, ");
                    str.AppendLine("      LACRE_REPOSITORIO LR ");
                    str.AppendLine(" WHERE 1 = 1 ");
                    str.AppendLine(" AND I.COD_ALINEA = ALI.COD_ALINEA ");
                    str.AppendLine(" AND I.SEQ_LISTA_CONTROLE = RLC.SEQ_LISTA_CONTROLE ");
                    str.AppendLine(" AND RLC.SEQ_REPOSITORIO = LR.SEQ_REPOSITORIO ");
                    str.AppendLine(string.Format(" AND LR.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));
                    str.AppendLine(" AND I.DSC_MATERIAL IS NOT NULL ");
                    str.AppendLine(" AND I.COD_MATERIAL IS NULL AND I.IDF_ATIVO = 'S' ");                    

                    str.AppendLine(" ORDER BY I.DSC_MATERIAL ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    // Preparar o retorno
                    while (ctx.Reader.Read())
                    {
                        itemListaControle = new Entity.ItensListaControle();
                        itemListaControle.Material = new Entity.Material();
                        itemListaControle.Alinea = new Entity.Alinea();

                        if (ctx.Reader["SEQ_ITENS_LISTA_CONTROLE"] != DBNull.Value)
                            itemListaControle.SeqItensListaControle = Convert.ToInt64(ctx.Reader["SEQ_ITENS_LISTA_CONTROLE"]);

                        if (ctx.Reader["DSC_MATERIAL"] != DBNull.Value)
                            itemListaControle.DescricaoMaterial = ctx.Reader["DSC_MATERIAL"].ToString();

                        if (ctx.Reader["COD_ALINEA"] != DBNull.Value)
                            itemListaControle.Alinea.Codigo = ctx.Reader["COD_ALINEA"].ToString();

                        if (ctx.Reader["DSC_ALINEA"] != DBNull.Value)
                            itemListaControle.Alinea.Nome = ctx.Reader["DSC_ALINEA"].ToString();

                        listaRetorno.Add(itemListaControle);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return listaRetorno;
        }

        /// <summary>
        /// Obter lista controle itens com materiais com código.
        /// somente itens ativos IDF_ATIVO = 'S'
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle> ObterListaDeListaControleItemComMaterialComCodigo(Int64 seqLacreRepositorio)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle> listaRetorno = new List<Entity.ItensListaControle>();
            Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle itemListaControle;            

            try
            {
                StringBuilder str = new StringBuilder();                
               
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT I.SEQ_ITENS_LISTA_CONTROLE, ");
                    str.AppendLine("    I.COD_MATERIAL, ");
                    str.AppendLine("    M.NOM_MATERIAL, ");
                    str.AppendLine("    ALI.COD_ALINEA, ");
                    str.AppendLine("    ALI.DSC_ALINEA, ");
                    str.AppendLine("    UN.NOM_UNIDADE ");

                    str.AppendLine(" FROM ITENS_LISTA_CONTROLE I, ");
                    str.AppendLine("    MATERIAL M, ");
                    str.AppendLine("    GRUPO G, ");
                    str.AppendLine("    ALINEA ALI, ");
                    str.AppendLine("    REPOSITORIO_LISTA_CONTROLE RLC, ");
                    str.AppendLine("    LACRE_REPOSITORIO LR, ");
                    str.AppendLine("    UNIDADE UN ");
                    str.AppendLine(" WHERE ");

                    str.AppendLine("    I.COD_MATERIAL = M.COD_MATERIAL ");
                    str.AppendLine("    AND G.COD_GRUPO = M.COD_GRUPO ");
                    str.AppendLine("    AND G.COD_ALINEA = ALI.COD_ALINEA ");
                    str.AppendLine("    AND I.SEQ_LISTA_CONTROLE = RLC.SEQ_LISTA_CONTROLE ");
                    str.AppendLine("    AND RLC.SEQ_REPOSITORIO = LR.SEQ_REPOSITORIO ");
                    str.AppendLine("    AND M.COD_UNIDADE = UN.COD_UNIDADE ");
                    str.AppendLine("    AND I.IDF_ATIVO = 'S' ");
                    str.AppendLine(string.Format(" AND LR.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));                    
                    str.AppendLine(" ORDER BY M.NOM_MATERIAL ");

                    query = new QueryCommandConfig(str.ToString());                    
                   
                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    // Preparar o retorno
                    while (ctx.Reader.Read())
                    {
                        itemListaControle = new Entity.ItensListaControle();
                        itemListaControle.Material = new Entity.Material();
                        itemListaControle.Alinea = new Entity.Alinea();
                        itemListaControle.Unidade = new Entity.Unidade();

                        if (ctx.Reader["SEQ_ITENS_LISTA_CONTROLE"] != DBNull.Value)
                            itemListaControle.SeqItensListaControle = Convert.ToInt64(ctx.Reader["SEQ_ITENS_LISTA_CONTROLE"]);

                        if (ctx.Reader["COD_MATERIAL"] != DBNull.Value)
                            itemListaControle.Material.Codigo = ctx.Reader["COD_MATERIAL"].ToString();

                        if (ctx.Reader["NOM_MATERIAL"] != DBNull.Value)
                            itemListaControle.Material.Nome = ctx.Reader["NOM_MATERIAL"].ToString();

                        if (ctx.Reader["COD_ALINEA"] != DBNull.Value)
                            itemListaControle.Alinea.Codigo = ctx.Reader["COD_ALINEA"].ToString();

                        if (ctx.Reader["DSC_ALINEA"] != DBNull.Value)
                            itemListaControle.Alinea.Nome = ctx.Reader["DSC_ALINEA"].ToString();

                        if (ctx.Reader["NOM_UNIDADE"] != DBNull.Value)
                            itemListaControle.Unidade.Nome = ctx.Reader["NOM_UNIDADE"].ToString();

                        listaRetorno.Add(itemListaControle);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return listaRetorno;
        }

        public long AtualizarItem(long seqItem, int quantidade)
        {
            try
            {
                // obter o contexto transacionado
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("ITENS_LISTA_CONTROLE");

                    comando.FilterParams["SEQ_ITENS_LISTA_CONTROLE"] = seqItem;
                    comando.Params["QTD_NECESSARIA"] = quantidade;

                    // Executar o insert
                    return ctx.ExecuteUpdate(comando);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Importar itens de uma lista para outra
        /// </summary>
        /// <param name="seqListaPara">Lista que vai receber os itens</param>
        /// <param name="seqListaDe">Lista que vai ser copiado os itens</param>
        public void ImportarItens(long seqListaPara, long seqListaDe)
        {
            try
            {
                // obter o contexto transacionado
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    StringBuilder str = new StringBuilder();     

                    ctx.Open();

                    str.AppendLine(" INSERT INTO GENERICO.ITENS_LISTA_CONTROLE ");
                    str.AppendLine("   (COD_MATERIAL, ");
                    str.AppendLine("    QTD_NECESSARIA, ");
                    str.AppendLine("    DSC_MATERIAL, ");
                    str.AppendLine("    COD_UNIDADE, ");
                    str.AppendLine("    COD_ALINEA, ");
                    str.AppendLine("    COD_TIPO_BEM, ");
                    str.AppendLine("    IDF_ATIVO, ");
                    str.AppendLine("    SEQ_LISTA_CONTROLE) ");
                    str.AppendLine("   SELECT COD_MATERIAL, ");
                    str.AppendLine("          QTD_NECESSARIA, ");
                    str.AppendLine("          DSC_MATERIAL, ");
                    str.AppendLine("          COD_UNIDADE, ");
                    str.AppendLine("          COD_ALINEA, ");
                    str.AppendLine("          COD_TIPO_BEM, ");
                    str.AppendLine("          IDF_ATIVO, ");
                    str.AppendLine(string.Format("          {0}             SEQ_LISTA_CONTROLE ", seqListaPara));
                    str.AppendLine("     FROM GENERICO.ITENS_LISTA_CONTROLE X ");
                    str.AppendLine(string.Format("    WHERE X.SEQ_LISTA_CONTROLE = {0} ",seqListaDe));
                    str.AppendLine("      AND NOT EXISTS (SELECT *  ");
                    str.AppendLine("             FROM ITENS_LISTA_CONTROLE Y ");
                    str.AppendLine(string.Format("            WHERE Y.SEQ_LISTA_CONTROLE = {0} ", seqListaPara));
                    str.AppendLine("            AND (Y.COD_MATERIAL = X.COD_MATERIAL ");
                    str.AppendLine("            OR Y.DSC_MATERIAL = X.DSC_MATERIAL ");
                    str.AppendLine("           OR Y.COD_TIPO_BEM = X.COD_TIPO_BEM)) ");

                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig(str.ToString());

                    // Executar o insert
                    ctx.ExecuteQuery(comando);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que retorna verdadeiro se o material ja estava na lista
        /// </summary>
        /// <param name="seqLista">lista</param>
        /// <param name="codMaterial">código do materila</param>
        /// <returns>True - Já existe / False - Não existe</returns>
        public bool ObterMateriaL(Int64 seqLista, string codMaterial)
        {
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT ");
                str.AppendLine("  1 ");
                str.AppendLine(" FROM  ITENS_LISTA_CONTROLE A ");
                str.AppendLine(string.Format(" WHERE A.SEQ_LISTA_CONTROLE = {0} ", seqLista));
                str.AppendLine(string.Format(" AND A.COD_MATERIAL = '{0}' ", codMaterial));

                ctx.Open();
                
                bool Ret = ctx.ExecuteSingletonQuery(str.ToString());
                
                ctx.Close();

                return Ret;
            }
        }

        /// <summary>
        /// Método que retorna verdadeiro se o tipo bem já estava na lista
        /// </summary>
        /// <param name="seqLista">lista</param>
        /// <param name="codTipoBem">código do tipo bem</param>
        /// <returns>True - Já existe / False - Não existe</returns>
        public bool ObterTipoBem(Int64 seqLista, long? codTipoBem)
        {
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine("SELECT ");
                str.AppendLine("  1 ");
                str.AppendLine(" FROM  ITENS_LISTA_CONTROLE A ");
                str.AppendLine(string.Format(" WHERE A.SEQ_LISTA_CONTROLE = {0} ", seqLista));
                str.AppendLine(string.Format(" AND A.COD_TIPO_BEM = '{0}' ", codTipoBem));

                ctx.Open();

                bool Ret = ctx.ExecuteSingletonQuery(str.ToString());

                ctx.Close();

                return Ret;
            }
        }

        /// <summary>
        /// Obter lista de itens da lista que estão não estão no repositório
        /// </summary>
        /// <param name="seqListaControle"></param>
        /// <returns></returns>
        public List<Entity.ItensListaControle> ObterItensEmFaltaNoRepositorio(long SeqRepositorio)
        {
            List<Entity.ItensListaControle> listaRetorno = new List<Entity.ItensListaControle>();
            Entity.ItensListaControle _itemListaControle = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT ITENS.COD_MATERIAL, ");
                    str.AppendLine("                  NVL(ITENS.DSC_MATERIAL, MAT.NOM_MATERIAL) NOM_MATERIAL, ");
                    str.AppendLine("                  ITENS.QTD_NECESSARIA, ");
                    str.AppendLine("                  UNI.NOM_UNIDADE ");
                    str.AppendLine("             FROM ITENS_LISTA_CONTROLE ITENS, ");
                    str.AppendLine("                  MATERIAL MAT, ");
                    str.AppendLine("                  UNIDADE UNI, ");
                    str.AppendLine("                  (SELECT ITENS.SEQ_ITENS_LISTA_CONTROLE ");
                    str.AppendLine("                     FROM ITENS_LISTA_CONTROLE       ITENS, ");
                    str.AppendLine("                          REPOSITORIO_LISTA_CONTROLE REPOSITORIO ");
                    str.AppendLine("                    WHERE ITENS.SEQ_LISTA_CONTROLE = REPOSITORIO.SEQ_LISTA_CONTROLE ");
                    str.AppendLine("                      AND ITENS.IDF_ATIVO = 'S' ");
                    str.AppendLine("                      AND ITENS.COD_TIPO_BEM IS NULL ");
                    
                    str.AppendLine(string.Format(" AND REPOSITORIO.SEQ_REPOSITORIO = {0}   ", SeqRepositorio));

                    str.AppendLine("                   MINUS ");

                    str.AppendLine("                   SELECT DISTINCT REPOSIT_ITENS.SEQ_ITENS_LISTA_CONTROLE ");
                    str.AppendLine("                     FROM LACRE_REPOSITORIO_ITENS REPOSIT_ITENS, ");
                    str.AppendLine("                          LACRE_REPOSITORIO       LACRE_REPOSIT ");
                    str.AppendLine("                    WHERE REPOSIT_ITENS.SEQ_LACRE_REPOSITORIO = ");
                    str.AppendLine("                          LACRE_REPOSIT.SEQ_LACRE_REPOSITORIO ");
                    str.AppendLine("                      AND REPOSIT_ITENS.SEQ_LACRE_REPOSITORIO = ");
                    str.AppendLine("                          (SELECT MAX(Y.SEQ_LACRE_REPOSITORIO) ");
                    str.AppendLine("                             FROM LACRE_REPOSITORIO Y ");

                    str.AppendLine(string.Format(" WHERE Y.SEQ_REPOSITORIO =  {0} ) ", SeqRepositorio));

                    str.AppendLine("  And (((SELECT REPOSIT_ITENS.QTD_DISPONIVEL - ");
                    str.AppendLine("    NVL(SUM(X.QTD_UTILIZADA), 0) ");
                    str.AppendLine(" FROM LACRE_REPOSIT_UTILIZACAO X ");
                    str.AppendLine(" WHERE X.SEQ_LACRE_REPOSITORIO_ITENS = ");
                    str.AppendLine("    REPOSIT_ITENS.SEQ_LACRE_REPOSITORIO_ITENS)) > 0) ) X ");

                    str.AppendLine("            WHERE X.SEQ_ITENS_LISTA_CONTROLE = ITENS.SEQ_ITENS_LISTA_CONTROLE ");
                    str.AppendLine("              AND ITENS.COD_MATERIAL = MAT.COD_MATERIAL(+) ");
                    str.AppendLine("              AND MAT.COD_UNIDADE = UNI.COD_UNIDADE(+) ");
                    str.AppendLine("            ORDER BY MAT.NOM_MATERIAL ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            _itemListaControle = new Entity.ItensListaControle();
                            _itemListaControle.ListaControle = new Entity.ListaControle();
                            _itemListaControle.Material = new Entity.Material();
                            //_itemListaControle.Alinea = new Entity.Alinea();
                            _itemListaControle.Unidade = new Entity.Unidade();

                            //if (dr["SEQ_ITENS_LISTA_CONTROLE"] != DBNull.Value)
                            //    _itemListaControle.SeqItensListaControle = Convert.ToInt64(dr["SEQ_ITENS_LISTA_CONTROLE"]);

                            //if (dr["DSC_ALINEA"] != DBNull.Value)
                            //    _itemListaControle.Alinea.Nome = dr["DSC_ALINEA"].ToString();

                            if (dr["COD_MATERIAL"] != DBNull.Value)
                                _itemListaControle.Material.Codigo = dr["COD_MATERIAL"].ToString();
                            else
                                _itemListaControle.Material.Codigo = string.Empty;

                            if (dr["NOM_MATERIAL"] != DBNull.Value)
                                _itemListaControle.Material.Nome = dr["NOM_MATERIAL"].ToString();
                            else
                                _itemListaControle.Material.Nome = string.Empty;

                            if (dr["QTD_NECESSARIA"] != DBNull.Value)
                                _itemListaControle.QuantidadeNecessaria = Convert.ToInt32(dr["QTD_NECESSARIA"]);

                            if (dr["NOM_UNIDADE"] != DBNull.Value)
                                _itemListaControle.Unidade.Nome = dr["NOM_UNIDADE"].ToString();

                            //if (dr["IDF_ATIVO"] != DBNull.Value)
                            //    _itemListaControle.IdfAtivo = dr["IDF_ATIVO"].ToString();

                            listaRetorno.Add(_itemListaControle);
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
        #endregion
    }
}
