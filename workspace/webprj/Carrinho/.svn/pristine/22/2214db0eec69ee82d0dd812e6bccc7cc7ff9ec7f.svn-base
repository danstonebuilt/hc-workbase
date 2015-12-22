using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.CarroUrgenciaPsicoativo.Entity;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class RepositorioListaControle
    {
        #region Métodos
        /// <summary>
        /// Obter por id.
        /// </summary>        
        public Entity.RepositorioListaControle ObterPorId(Int64 seqRepositorio)
        {
            Entity.RepositorioListaControle repListControle = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT ");
                    str.AppendLine("    RLC.SEQ_REPOSITORIO, ");
                    str.AppendLine("    RLC.DSC_IDENTIFICACAO, ");
                    str.AppendLine("    RLC.SEQ_LISTA_CONTROLE, ");
                    str.AppendLine("    RLC.SEQ_TIP_REPOSIT_LST_CONTROL, ");
                    str.AppendLine("    RLC.DTA_CADASTRO, ");
                    str.AppendLine("    RLC.DTA_ULTIMA_ALTERACAO, ");
                    str.AppendLine("    RLC.NUM_USER_CADASTRO, ");
                    str.AppendLine("    RLC.NUM_USER_ULTIMA_ALTERACAO, ");
                    str.AppendLine("    RLC.IDF_ATIVO, ");
                    str.AppendLine("    RLC.NUM_BEM, ");
                    str.AppendLine("    TIPOREPOSIT.DSC_TIPO_REPOSITORIO, ");
                    str.AppendLine("    BP.NUM_PATRIMONIO, ");
                    str.AppendLine("    TIPPATRIMONIO.DSC_TIPO_PATRIMONIO, ");
                    str.AppendLine("    TIPPATRIMONIO.COD_TIPO_PATRIMONIO, ");
                    str.AppendLine("    LS.NOM_LISTA_CONTROLE ");
                    str.AppendLine(" FROM REPOSITORIO_LISTA_CONTROLE RLC, ");
                    str.AppendLine("    LISTA_CONTROLE LS, ");
                    str.AppendLine("    BEM_PATRIMONIAL BP, ");
                    str.AppendLine("    TIPO_REPOSITORIO_LST_CONTROLE TIPOREPOSIT, ");
                    str.AppendLine("    TIPO_PATRIMONIO TIPPATRIMONIO ");
                    str.AppendLine(" WHERE RLC.SEQ_LISTA_CONTROLE = LS.SEQ_LISTA_CONTROLE ");
                    str.AppendLine(" AND RLC.NUM_BEM = BP.NUM_BEM(+) ");
                    str.AppendLine(" AND TIPOREPOSIT.SEQ_TIP_REPOSIT_LST_CONTROL = RLC.SEQ_TIP_REPOSIT_LST_CONTROL ");
                    str.AppendLine(" AND TIPPATRIMONIO.COD_TIPO_PATRIMONIO(+) = BP.COD_TIPO_PATRIMONIO ");
                    str.AppendLine(string.Format(" AND RLC.SEQ_REPOSITORIO = {0} ", seqRepositorio));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            repListControle = new Entity.RepositorioListaControle();
                            repListControle.ListaControle = new Entity.ListaControle();
                            repListControle.TipoRepositorioListaControle = new Entity.TipoRepositorioListaControle();
                            repListControle.BemPatrimonial = new Entity.BemPatrimonial();
                            repListControle.UsuarioCadastro = new Entity.Usuario();
                            repListControle.UsuarioUltimaAlteracao = new Entity.Usuario();

                            if (dr["SEQ_REPOSITORIO"] != DBNull.Value)
                                repListControle.SeqRepositorio = Convert.ToInt64(dr["SEQ_REPOSITORIO"]);

                            if (dr["DSC_IDENTIFICACAO"] != DBNull.Value)
                                repListControle.DscIdentificacao = dr["DSC_IDENTIFICACAO"].ToString();

                            if (dr["SEQ_LISTA_CONTROLE"] != DBNull.Value)
                                repListControle.ListaControle.SeqListaControle = Convert.ToInt64(dr["SEQ_LISTA_CONTROLE"]);

                            if (dr["NOM_LISTA_CONTROLE"] != DBNull.Value)
                                repListControle.ListaControle.NomeListaControle = dr["NOM_LISTA_CONTROLE"].ToString();

                            if (dr["SEQ_TIP_REPOSIT_LST_CONTROL"] != DBNull.Value)
                                repListControle.TipoRepositorioListaControle.SeqTipoRepositorioLstControl = Convert.ToInt64(dr["SEQ_TIP_REPOSIT_LST_CONTROL"]);

                            if (dr["DSC_TIPO_REPOSITORIO"] != DBNull.Value)
                                repListControle.TipoRepositorioListaControle.DscTipoRepositorio = dr["DSC_TIPO_REPOSITORIO"].ToString();

                            if (dr["DTA_CADASTRO"] != DBNull.Value)
                                repListControle.DataCadastro = Convert.ToDateTime(dr["DTA_CADASTRO"]);

                            if (dr["DTA_ULTIMA_ALTERACAO"] != DBNull.Value)
                                repListControle.DataUltimaAlteracao = Convert.ToDateTime(dr["DTA_ULTIMA_ALTERACAO"]);

                            if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                                repListControle.UsuarioCadastro.NumUserBanco = Convert.ToInt32(dr["NUM_USER_CADASTRO"]);

                            if (dr["NUM_USER_ULTIMA_ALTERACAO"] != DBNull.Value)
                                repListControle.UsuarioUltimaAlteracao.NumUserBanco = Convert.ToInt32(dr["NUM_USER_ULTIMA_ALTERACAO"]);

                            if (dr["IDF_ATIVO"] != DBNull.Value)
                                repListControle.IdfAtivo = dr["IDF_ATIVO"].ToString();

                            if (dr["NUM_BEM"] != DBNull.Value)
                                repListControle.BemPatrimonial.NumBem = Convert.ToInt64(dr["NUM_BEM"]);

                            if (dr["NUM_PATRIMONIO"] != DBNull.Value)
                                repListControle.BemPatrimonial.NumeroPatrimonio = Convert.ToInt64(dr["NUM_PATRIMONIO"]);

                            if (dr["COD_TIPO_PATRIMONIO"] != DBNull.Value)
                                repListControle.BemPatrimonial.CodTipoPatrimonio = Convert.ToInt64(dr["COD_TIPO_PATRIMONIO"].ToString());

                            if (dr["DSC_TIPO_PATRIMONIO"] != DBNull.Value)
                                repListControle.BemPatrimonial.DscTipoPatrimonio = dr["DSC_TIPO_PATRIMONIO"].ToString();
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

            return repListControle;
        }

        /// <summary>
        /// Obter por instituto.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioListaControle> ObterPorInstituto(int codInstituto, bool listarInativos)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioListaControle> listRepositorioLstControle = new List<Entity.RepositorioListaControle>();
            Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioListaControle repListControle = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT ");
                    str.AppendLine("    RLC.SEQ_REPOSITORIO, ");
                    str.AppendLine("    RLC.DSC_IDENTIFICACAO, ");
                    str.AppendLine("    RLC.SEQ_LISTA_CONTROLE, ");
                    str.AppendLine("    RLC.SEQ_TIP_REPOSIT_LST_CONTROL, ");
                    str.AppendLine("    RLC.DTA_CADASTRO, ");
                    str.AppendLine("    RLC.DTA_ULTIMA_ALTERACAO, ");
                    str.AppendLine("    RLC.NUM_USER_CADASTRO, ");
                    str.AppendLine("    RLC.NUM_USER_ULTIMA_ALTERACAO, ");
                    str.AppendLine("    RLC.IDF_ATIVO, ");
                    str.AppendLine("    RLC.NUM_BEM, ");
                    str.AppendLine("    TIPOREPOSIT.DSC_TIPO_REPOSITORIO, ");
                    str.AppendLine("    BP.NUM_PATRIMONIO, ");
                    str.AppendLine("    TIPPATRIMONIO.DSC_TIPO_PATRIMONIO, ");
                    str.AppendLine("    LS.NOM_LISTA_CONTROLE ");
                    str.AppendLine(" FROM REPOSITORIO_LISTA_CONTROLE RLC, ");
                    str.AppendLine("    LISTA_CONTROLE LS, ");
                    str.AppendLine("    BEM_PATRIMONIAL BP, ");
                    str.AppendLine("    TIPO_REPOSITORIO_LST_CONTROLE TIPOREPOSIT, ");
                    str.AppendLine("    TIPO_PATRIMONIO TIPPATRIMONIO ");
                    str.AppendLine(" WHERE RLC.SEQ_LISTA_CONTROLE = LS.SEQ_LISTA_CONTROLE ");
                    str.AppendLine(" AND RLC.NUM_BEM = BP.NUM_BEM(+) ");
                    str.AppendLine(" AND TIPOREPOSIT.SEQ_TIP_REPOSIT_LST_CONTROL = RLC.SEQ_TIP_REPOSIT_LST_CONTROL ");
                    str.AppendLine(" AND TIPPATRIMONIO.COD_TIPO_PATRIMONIO(+) = BP.COD_TIPO_PATRIMONIO ");
                    str.AppendLine(string.Format(" AND LS.COD_INSTITUTO = {0} ", codInstituto));

                    if (!listarInativos)
                        str.AppendLine(" AND RLC.idf_ativo = 'S' ");
                    else
                        str.AppendLine(string.Format(" AND RLC.idf_ativo = '{0}' ", 'N'));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            repListControle = new Entity.RepositorioListaControle();
                            repListControle.ListaControle = new Entity.ListaControle();
                            repListControle.TipoRepositorioListaControle = new Entity.TipoRepositorioListaControle();
                            repListControle.BemPatrimonial = new Entity.BemPatrimonial();
                            repListControle.UsuarioCadastro = new Entity.Usuario();
                            repListControle.UsuarioUltimaAlteracao = new Entity.Usuario();

                            if (dr["SEQ_REPOSITORIO"] != DBNull.Value)
                                repListControle.SeqRepositorio = Convert.ToInt64(dr["SEQ_REPOSITORIO"]);

                            if (dr["DSC_IDENTIFICACAO"] != DBNull.Value)
                                repListControle.DscIdentificacao = dr["DSC_IDENTIFICACAO"].ToString();

                            if (dr["SEQ_LISTA_CONTROLE"] != DBNull.Value)
                                repListControle.ListaControle.SeqListaControle = Convert.ToInt64(dr["SEQ_LISTA_CONTROLE"]);

                            if (dr["NOM_LISTA_CONTROLE"] != DBNull.Value)
                                repListControle.ListaControle.NomeListaControle = dr["NOM_LISTA_CONTROLE"].ToString();

                            if (dr["SEQ_TIP_REPOSIT_LST_CONTROL"] != DBNull.Value)
                                repListControle.TipoRepositorioListaControle.SeqTipoRepositorioLstControl = Convert.ToInt64(dr["SEQ_TIP_REPOSIT_LST_CONTROL"]);

                            if (dr["DSC_TIPO_REPOSITORIO"] != DBNull.Value)
                                repListControle.TipoRepositorioListaControle.DscTipoRepositorio = dr["DSC_TIPO_REPOSITORIO"].ToString();

                            if (dr["DTA_CADASTRO"] != DBNull.Value)
                                repListControle.DataCadastro = Convert.ToDateTime(dr["DTA_CADASTRO"]);

                            if (dr["DTA_ULTIMA_ALTERACAO"] != DBNull.Value)
                                repListControle.DataUltimaAlteracao = Convert.ToDateTime(dr["DTA_ULTIMA_ALTERACAO"]);

                            if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                                repListControle.UsuarioCadastro.NumUserBanco = Convert.ToInt32(dr["NUM_USER_CADASTRO"]);

                            if (dr["NUM_USER_ULTIMA_ALTERACAO"] != DBNull.Value)
                                repListControle.UsuarioUltimaAlteracao.NumUserBanco = Convert.ToInt32(dr["NUM_USER_ULTIMA_ALTERACAO"]);

                            if (dr["IDF_ATIVO"] != DBNull.Value)
                                repListControle.IdfAtivo = dr["IDF_ATIVO"].ToString();

                            if (dr["NUM_BEM"] != DBNull.Value)
                                repListControle.BemPatrimonial.NumBem = Convert.ToInt64(dr["NUM_BEM"]);

                            if (dr["NUM_PATRIMONIO"] != DBNull.Value)
                                repListControle.BemPatrimonial.NumeroPatrimonio = Convert.ToInt64(dr["NUM_PATRIMONIO"]);

                            if (dr["DSC_TIPO_PATRIMONIO"] != DBNull.Value)
                                repListControle.BemPatrimonial.DscTipoPatrimonio = dr["DSC_TIPO_PATRIMONIO"].ToString();

                            listRepositorioLstControle.Add(repListControle);

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

            return listRepositorioLstControle;
        }

        /// <summary>
        /// Obter repositorio com medicamentos a vencer.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioListaControle> ObterRepositorioComMaterialAVencer(int codInstituto, int qtdDiasVencer)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioListaControle> listRepositorioLstControle = new List<Entity.RepositorioListaControle>();
            Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioListaControle repListControle = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT DISTINCT ");
                    str.AppendLine("     A.SEQ_REPOSITORIO, ");
                    str.AppendLine("     (J.NUM_PATRIMONIO || ' / ' ||   L.DSC_TIPO_PATRIMONIO || ' - ' ||  I.DSC_IDENTIFICACAO) AS DSC_IDENTIFICACAO ");
                    str.AppendLine(" FROM LACRE_REPOSITORIO          A, ");
                    str.AppendLine("    LACRE_REPOSITORIO_ITENS    B, ");
                    str.AppendLine("    ITENS_LISTA_CONTROLE       C, ");
                    str.AppendLine("    MATERIAL                   D, ");
                    str.AppendLine("    LOTE                       E, ");
                    str.AppendLine("    GRUPO                      G, ");
                    str.AppendLine("    REPOSITORIO_LISTA_CONTROLE I, ");
                    str.AppendLine("    BEM_PATRIMONIAL J, ");
                    str.AppendLine("    TIPO_PATRIMONIO L ");
                    str.AppendLine(" WHERE A.SEQ_LACRE_REPOSITORIO = B.SEQ_LACRE_REPOSITORIO ");
                    str.AppendLine("    AND B.SEQ_ITENS_LISTA_CONTROLE = C.SEQ_ITENS_LISTA_CONTROLE ");
                    str.AppendLine("    AND C.COD_MATERIAL = D.COD_MATERIAL(+) ");
                    str.AppendLine("    AND B.NUM_LOTE = E.NUM_LOTE(+) ");
                    str.AppendLine("    AND D.COD_GRUPO = G.COD_GRUPO(+) ");
                    str.AppendLine("    AND A.SEQ_REPOSITORIO = I.SEQ_REPOSITORIO ");
                    str.AppendLine("    AND J.NUM_BEM = I.NUM_BEM ");
                    str.AppendLine("    AND J.COD_TIPO_PATRIMONIO = L.COD_TIPO_PATRIMONIO ");
                    str.AppendLine("    AND TRUNC(NVL(B.DTA_VALIDADE_LOTE, E.DTA_VALIDADE_LOTE)) < ");
                    str.AppendLine(string.Format(" TRUNC(SYSDATE + {0}) ", qtdDiasVencer));

                    str.AppendLine(" AND I.SEQ_LISTA_CONTROLE IN ");
                    str.AppendLine(" (SELECT X.SEQ_LISTA_CONTROLE ");
                    str.AppendLine(" FROM LISTA_CONTROLE X ");
                    str.AppendLine(string.Format(" WHERE X.COD_INSTITUTO = {0}) ", codInstituto));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            repListControle = new Entity.RepositorioListaControle();

                            if (dr["SEQ_REPOSITORIO"] != DBNull.Value)
                                repListControle.SeqRepositorio = Convert.ToInt64(dr["SEQ_REPOSITORIO"]);

                            if (dr["DSC_IDENTIFICACAO"] != DBNull.Value)
                                repListControle.DscIdentificacao = dr["DSC_IDENTIFICACAO"].ToString();

                            listRepositorioLstControle.Add(repListControle);

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

            return listRepositorioLstControle;
        }

        public void AtualizarItem(Entity.RepositorioListaControle reposit)
        {
            var trans = new TransacaoDinamica();

            try
            {
                var ctx = trans.ctx;

                // Criar contexto
                //using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                //{
                //    ctx.Open();

                // Preparar o comando
                var comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REPOSITORIO_LISTA_CONTROLE");
                var comando1 = new Hcrp.Infra.AcessoDado.CommandConfig("REPOSITORIO_CENTRO_CUSTO");

                comando.Params["DSC_IDENTIFICACAO"] = reposit.DscIdentificacao;
                comando.Params["SEQ_LISTA_CONTROLE"] = reposit.ListaControle.SeqListaControle;
                comando.Params["SEQ_TIP_REPOSIT_LST_CONTROL"] = reposit.TipoRepositorioListaControle.SeqTipoRepositorioLstControl;


                if (reposit.BemPatrimonial != null && reposit.BemPatrimonial.NumBem > 0)
                {
                    comando.Params["NUM_BEM"] = reposit.BemPatrimonial.NumBem;
                }
                

                comando.FilterParams["SEQ_REPOSITORIO"] = reposit.SeqRepositorio;

                // Executar o insert
                ctx.ExecuteUpdate(comando);

                comando1.Params["SEQ_REPOSITORIO"] = reposit.SeqRepositorio;

                // Limpa os centros de custo
                ctx.ExecuteDelete(comando1);

                comando1 = new Hcrp.Infra.AcessoDado.CommandConfig("REPOSITORIO_CENTRO_CUSTO");

                //_seqRetorno = Convert.ToInt64(ctx.GetSequenceValue("SEQ_REPOSITORIO", false));

                foreach (var itensCC in reposit.listRepositorioCentroCusto)
                {
                    comando1.Params["COD_CENCUSTO"] = itensCC.codigoCentroCusto;
                    comando1.Params["SEQ_REPOSITORIO"] = reposit.SeqRepositorio;

                    // Executar o insert
                    ctx.ExecuteInsert(comando1);
                }

                trans.ComitarTransacao();
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long AdicionarRepositorio(Entity.RepositorioListaControle reposit)
        {
            long _seqRetorno = 0;

            try
            {
                // Criar contexto
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("REPOSITORIO_LISTA_CONTROLE");
                    Hcrp.Infra.AcessoDado.CommandConfig comando1 = new Hcrp.Infra.AcessoDado.CommandConfig("REPOSITORIO_CENTRO_CUSTO");

                    comando.Params["DSC_IDENTIFICACAO"] = reposit.DscIdentificacao.ToUpper();
                    comando.Params["SEQ_LISTA_CONTROLE"] = reposit.ListaControle.SeqListaControle;
                    comando.Params["SEQ_TIP_REPOSIT_LST_CONTROL"] = reposit.TipoRepositorioListaControle.SeqTipoRepositorioLstControl;
                    comando.Params["IDF_ATIVO"] = "S";

                    if (reposit.BemPatrimonial != null && reposit.BemPatrimonial.NumBem != 0)
                        comando.Params["NUM_BEM"] = reposit.BemPatrimonial.NumBem;

                    // Trigger na tabela
                    //comando.Params["NUM_USER_CADASTRO"] = 59851;//_itensListaControle.UsuarioCadastro.NumUserBanco;

                    comando.Params["IDF_ATIVO"] = "S";

                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                    _seqRetorno = Convert.ToInt64(ctx.GetSequenceValue("SEQ_REPOSITORIO", false));

                    foreach (var itensCC in reposit.listRepositorioCentroCusto)
                    {
                        comando1.Params["COD_CENCUSTO"] = itensCC.codigoCentroCusto;
                        comando1.Params["SEQ_REPOSITORIO"] = _seqRetorno;

                        // Executar o insert
                        ctx.ExecuteInsert(comando1);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _seqRetorno;
        }

        public void AtivarOuInativar(bool ehPraAtivar, long seqRepositorio)
        {
            try
            {
                // obter o contexto transacionado
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REPOSITORIO_LISTA_CONTROLE");

                    comando.FilterParams["SEQ_REPOSITORIO"] = seqRepositorio;

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

        /// <summary>
        /// Obter os centros de custo associados com o repositório
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioCentroCusto> ObterCentrosDeCustoDoRepositorio(Int64 seqRepositorio)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioCentroCusto> listRepositorioLstControle = new List<Entity.RepositorioCentroCusto>();
            Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioCentroCusto repListControle = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    str.AppendLine(" SELECT ");
                    str.AppendLine("    CC.COD_CENCUSTO, ");
                    str.AppendLine("    CC.NOM_CENCUSTO ");
                    str.AppendLine(" FROM CENTRO_CUSTO CC, REPOSITORIO_CENTRO_CUSTO RCC ");
                    str.AppendLine(" WHERE CC.COD_CENCUSTO = RCC.COD_CENCUSTO ");
                    str.AppendLine(" AND RCC.SEQ_REPOSITORIO = " + seqRepositorio);

                    // Abrir conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(str.ToString());

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            repListControle = new RepositorioCentroCusto();

                            if (dr["COD_CENCUSTO"] != DBNull.Value)
                                repListControle.codigoCentroCusto = dr["COD_CENCUSTO"].ToString();

                            if (dr["NOM_CENCUSTO"] != DBNull.Value)
                                repListControle.nomeCentroCusto = dr["NOM_CENCUSTO"].ToString();

                            listRepositorioLstControle.Add(repListControle);
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

            return listRepositorioLstControle;
        }


        /// <summary>
        /// Obter por instituto.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioListaControle> ObterPorInstitutoCentroCustoUsuarioLogado(int codInstituto, string roles)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioListaControle> listRepositorioLstControle = new List<Entity.RepositorioListaControle>();
            Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioListaControle repListControle = null;

            try
            {
                StringBuilder str = new StringBuilder();

                int cod_instituicao = Hcrp.Framework.Infra.Util.Parametrizacao.Instancia().CodInstituicao;

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT ");
                    str.AppendLine("    RLC.SEQ_REPOSITORIO, ");
                    str.AppendLine("    RLC.DSC_IDENTIFICACAO, ");
                    str.AppendLine("    RLC.SEQ_LISTA_CONTROLE, ");
                    str.AppendLine("    RLC.SEQ_TIP_REPOSIT_LST_CONTROL, ");
                    str.AppendLine("    RLC.DTA_CADASTRO, ");
                    str.AppendLine("    RLC.DTA_ULTIMA_ALTERACAO, ");
                    str.AppendLine("    RLC.NUM_USER_CADASTRO, ");
                    str.AppendLine("    RLC.NUM_USER_ULTIMA_ALTERACAO, ");
                    str.AppendLine("    RLC.IDF_ATIVO, ");
                    str.AppendLine("    RLC.NUM_BEM, ");
                    str.AppendLine("    TIPOREPOSIT.DSC_TIPO_REPOSITORIO, ");
                    str.AppendLine("    BP.NUM_PATRIMONIO, ");
                    str.AppendLine("    TIPPATRIMONIO.DSC_TIPO_PATRIMONIO, ");
                    str.AppendLine("    LS.NOM_LISTA_CONTROLE ");
                    str.AppendLine(" FROM REPOSITORIO_LISTA_CONTROLE RLC, ");
                    str.AppendLine("    LISTA_CONTROLE LS, ");
                    str.AppendLine("    BEM_PATRIMONIAL BP, ");
                    str.AppendLine("    TIPO_REPOSITORIO_LST_CONTROLE TIPOREPOSIT, ");
                    str.AppendLine("    TIPO_PATRIMONIO TIPPATRIMONIO, REPOSITORIO_CENTRO_CUSTO REPOSIT_CC ");
                    str.AppendLine(" WHERE RLC.SEQ_LISTA_CONTROLE = LS.SEQ_LISTA_CONTROLE ");
                    str.AppendLine(" AND RLC.NUM_BEM = BP.NUM_BEM(+) ");
                    str.AppendLine(" AND TIPOREPOSIT.SEQ_TIP_REPOSIT_LST_CONTROL = RLC.SEQ_TIP_REPOSIT_LST_CONTROL ");

                    str.AppendLine(" AND REPOSIT_CC.SEQ_REPOSITORIO =  RLC.SEQ_REPOSITORIO ");

                    str.AppendLine(" AND REPOSIT_CC.Cod_Cencusto in (SELECT X.Cod_Cencusto  ");
                    str.AppendLine("                   FROM USUARIO_ROLE_CENTRO_CUSTO X  ");

                    // todo: LEMBRAR DE MODIFICAR AS ROLES DE ENFERMAGEM 1087 FOI SOLUCAO PALIATIVA
                    str.AppendLine(string.Format("                  WHERE X.COD_ROLE IN ({0}) ", roles + ",1087"));
                    
                    str.AppendLine(string.Format("    AND X.COD_INST_SISTEMA = {0} ", cod_instituicao));
                    str.AppendLine(" AND X.NUM_USER_BANCO = FC_NUM_USER_BANCO )");

                    str.AppendLine(" AND BP.COD_TIPO_PATRIMONIO = TIPPATRIMONIO.COD_TIPO_PATRIMONIO(+) ");
                    str.AppendLine(string.Format(" AND LS.COD_INSTITUTO = {0} ", codInstituto));
                    str.AppendLine(" AND RLC.IDF_ATIVO = 'S' ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            repListControle = new Entity.RepositorioListaControle();
                            repListControle.ListaControle = new Entity.ListaControle();
                            repListControle.TipoRepositorioListaControle = new Entity.TipoRepositorioListaControle();
                            repListControle.BemPatrimonial = new Entity.BemPatrimonial();
                            repListControle.UsuarioCadastro = new Entity.Usuario();
                            repListControle.UsuarioUltimaAlteracao = new Entity.Usuario();

                            if (dr["SEQ_REPOSITORIO"] != DBNull.Value)
                                repListControle.SeqRepositorio = Convert.ToInt64(dr["SEQ_REPOSITORIO"]);

                            if (dr["DSC_IDENTIFICACAO"] != DBNull.Value)
                                repListControle.DscIdentificacao = dr["DSC_IDENTIFICACAO"].ToString();

                            if (dr["SEQ_LISTA_CONTROLE"] != DBNull.Value)
                                repListControle.ListaControle.SeqListaControle = Convert.ToInt64(dr["SEQ_LISTA_CONTROLE"]);

                            if (dr["NOM_LISTA_CONTROLE"] != DBNull.Value)
                                repListControle.ListaControle.NomeListaControle = dr["NOM_LISTA_CONTROLE"].ToString();

                            if (dr["SEQ_TIP_REPOSIT_LST_CONTROL"] != DBNull.Value)
                                repListControle.TipoRepositorioListaControle.SeqTipoRepositorioLstControl = Convert.ToInt64(dr["SEQ_TIP_REPOSIT_LST_CONTROL"]);

                            if (dr["DSC_TIPO_REPOSITORIO"] != DBNull.Value)
                                repListControle.TipoRepositorioListaControle.DscTipoRepositorio = dr["DSC_TIPO_REPOSITORIO"].ToString();

                            if (dr["DTA_CADASTRO"] != DBNull.Value)
                                repListControle.DataCadastro = Convert.ToDateTime(dr["DTA_CADASTRO"]);

                            if (dr["DTA_ULTIMA_ALTERACAO"] != DBNull.Value)
                                repListControle.DataUltimaAlteracao = Convert.ToDateTime(dr["DTA_ULTIMA_ALTERACAO"]);

                            if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                                repListControle.UsuarioCadastro.NumUserBanco = Convert.ToInt32(dr["NUM_USER_CADASTRO"]);

                            if (dr["NUM_USER_ULTIMA_ALTERACAO"] != DBNull.Value)
                                repListControle.UsuarioUltimaAlteracao.NumUserBanco = Convert.ToInt32(dr["NUM_USER_ULTIMA_ALTERACAO"]);

                            if (dr["IDF_ATIVO"] != DBNull.Value)
                                repListControle.IdfAtivo = dr["IDF_ATIVO"].ToString();

                            if (dr["NUM_BEM"] != DBNull.Value)
                                repListControle.BemPatrimonial.NumBem = Convert.ToInt64(dr["NUM_BEM"]);

                            if (dr["NUM_PATRIMONIO"] != DBNull.Value)
                                repListControle.BemPatrimonial.NumeroPatrimonio = Convert.ToInt64(dr["NUM_PATRIMONIO"]);

                            if (dr["DSC_TIPO_PATRIMONIO"] != DBNull.Value)
                                repListControle.BemPatrimonial.DscTipoPatrimonio = dr["DSC_TIPO_PATRIMONIO"].ToString();

                            listRepositorioLstControle.Add(repListControle);

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

            return listRepositorioLstControle;
        }

        #endregion
    }
}
