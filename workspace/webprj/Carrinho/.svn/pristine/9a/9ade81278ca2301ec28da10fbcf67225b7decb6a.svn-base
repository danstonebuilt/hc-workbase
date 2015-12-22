using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class LacreRepositorioEquipamento
    {
        #region variáveis / construtor

        internal Hcrp.Infra.AcessoDado.TransacaoDinamica transacao;

        public LacreRepositorioEquipamento()
        {

        }

        public LacreRepositorioEquipamento(Hcrp.Infra.AcessoDado.TransacaoDinamica _trans)
        {
            this.transacao = _trans;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Excluir transacionado.
        /// </summary>
        public void ExcluirPorLacreRepositorioTrans(Int64 seqLacreRepositorio)
        {
            try
            {
                // obter o contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("LACRE_REPOSIT_EQUIPAMENTO");

                comando.Params["SEQ_LACRE_REPOSITORIO"] = seqLacreRepositorio;

                ctx.ExecuteDelete(comando);

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Adicionar.
        /// </summary>        
        public long Adicionar(Entity.LacreRepositorioEquipamento lacreRepEquipamento)
        {
            long _seqRetorno = 0;

            try
            {
                // Criar contexto
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("LACRE_REPOSIT_EQUIPAMENTO");

                    if (lacreRepEquipamento.LacreRepositorio != null && lacreRepEquipamento.LacreRepositorio.SeqLacreRepositorio > 0)
                        comando.Params["SEQ_LACRE_REPOSITORIO"] = lacreRepEquipamento.LacreRepositorio.SeqLacreRepositorio;

                    if (!string.IsNullOrWhiteSpace(lacreRepEquipamento.IdfAtivo))
                        comando.Params["IDF_ATIVO"] = lacreRepEquipamento.IdfAtivo;

                    comando.Params["DTA_CADASTRO"] = lacreRepEquipamento.DataCadastro;

                    if (lacreRepEquipamento.BemPatrimonial != null && lacreRepEquipamento.BemPatrimonial.NumBem > 0)
                        comando.Params["NUM_BEM"] = lacreRepEquipamento.BemPatrimonial.NumBem;

                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                    _seqRetorno = Convert.ToInt64(ctx.GetSequenceValue("SEQ_LACRE_REPOSIT_EQUIP", false));
                }

            }
            catch (Exception)
            {
                throw;
            }

            return _seqRetorno;
        }

        /// <summary>
        /// Obter por lacre repositorio.
        /// </summary>
        public List<Entity.LacreRepositorioEquipamento> ObterPorLacreRepositorio(Int64 seqLacreRepositorio)
        {
            List<Entity.LacreRepositorioEquipamento> listaRetorno = new List<Entity.LacreRepositorioEquipamento>();
            Entity.LacreRepositorioEquipamento lacreRepositorioEquip = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT E.SEQ_LACRE_REPOSIT_EQUIP, ");
                    str.AppendLine("        A.NUM_BEM, ");
                    str.AppendLine("        NVL(TO_CHAR(A.NUM_PATRIMONIO),'SEM NÚMERO') || ' / ' || C.DSC_TIPO_PATRIMONIO PATRIMONIO, ");
                    str.AppendLine("        A.DSC_COMPLEMENTAR, ");
                    str.AppendLine("        F.NOM_USUARIO, ");
                    str.AppendLine("        E.DTA_TESTE ");
                    str.AppendLine(" FROM LACRE_REPOSITORIO D, ");
                    str.AppendLine("        LACRE_REPOSIT_EQUIPAMENTO E, ");
                    str.AppendLine("        BEM_PATRIMONIAL A, ");
                    //str.AppendLine("        ITENS_LISTA_CONTROLE B, ");
                    str.AppendLine("        TIPO_PATRIMONIO C, USUARIO F ");
                    str.AppendLine(" WHERE D.SEQ_LACRE_REPOSITORIO =  E.SEQ_LACRE_REPOSITORIO ");
                    //str.AppendLine("    AND A.COD_TIPO_BEM = B.COD_TIPO_BEM ");
                    str.AppendLine("    AND A.COD_TIPO_PATRIMONIO = C.COD_TIPO_PATRIMONIO ");
                    str.AppendLine("    AND E.NUM_BEM = A.NUM_BEM ");                    
                    str.AppendLine("    AND E.NUM_USER_RESP_TESTE = F.NUM_USER_BANCO(+) ");
                    str.AppendLine("    AND E.IDF_ATIVO = 'S' ");
                    str.AppendLine(string.Format(" AND D.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lacreRepositorioEquip = new Entity.LacreRepositorioEquipamento();
                            lacreRepositorioEquip.LacreRepositorio = new Entity.LacreRepositorio();
                            lacreRepositorioEquip.BemPatrimonial = new Entity.BemPatrimonial();
                            lacreRepositorioEquip.UsuarioResponsavelTeste = new Entity.Usuario();

                            if (dr["SEQ_LACRE_REPOSIT_EQUIP"] != DBNull.Value)
                                lacreRepositorioEquip.SeqLacreRepositorioEquipamento = Convert.ToInt64(dr["SEQ_LACRE_REPOSIT_EQUIP"]);

                            if (dr["NUM_BEM"] != DBNull.Value)
                                lacreRepositorioEquip.BemPatrimonial.NumBem = Convert.ToInt64(dr["NUM_BEM"]);

                            if (dr["PATRIMONIO"] != DBNull.Value)
                                lacreRepositorioEquip.BemPatrimonial.DscModelo = dr["PATRIMONIO"].ToString();

                            if (dr["DSC_COMPLEMENTAR"] != DBNull.Value)
                                lacreRepositorioEquip.BemPatrimonial.DscComplementar = dr["DSC_COMPLEMENTAR"].ToString();

                            if (dr["NOM_USUARIO"] != DBNull.Value)
                                lacreRepositorioEquip.UsuarioResponsavelTeste.Nome = dr["NOM_USUARIO"].ToString();

                            if (dr["DTA_TESTE"] != DBNull.Value)
                                lacreRepositorioEquip.DataTeste = Convert.ToDateTime(dr["DTA_TESTE"]);

                            listaRetorno.Add(lacreRepositorioEquip);
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
        /// Atualizar dados de teste do equipamento.
        /// </summary>        
        public void AtualizarDadosDeTesteDoEquipamento(Int64 seqLacreRepositorioEquipamento, DateTime dataDoTeste, int numUserTeste)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comandoDataValidadeLote = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("LACRE_REPOSIT_EQUIPAMENTO");

                    comandoDataValidadeLote.FilterParams["SEQ_LACRE_REPOSIT_EQUIP"] = seqLacreRepositorioEquipamento;

                    comandoDataValidadeLote.Params["DTA_TESTE"] = dataDoTeste;
                    comandoDataValidadeLote.Params["NUM_USER_RESP_TESTE"] = numUserTeste;

                    // Executar o insert
                    ctx.ExecuteUpdate(comandoDataValidadeLote);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inativar equipamento.
        /// </summary>        
        public void InativarEquipamento(Int64 seqLacreRepositorioEquipamento)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comandoDataValidadeLote = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("LACRE_REPOSIT_EQUIPAMENTO");

                    comandoDataValidadeLote.FilterParams["SEQ_LACRE_REPOSIT_EQUIP"] = seqLacreRepositorioEquipamento;

                    comandoDataValidadeLote.Params["IDF_ATIVO"] = "N";                    

                    // Executar o insert
                    ctx.ExecuteUpdate(comandoDataValidadeLote);
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
        public Entity.LacreRepositorioEquipamento ObterPorId(Int64 seqLacreRepositorioEquipamento)
        {   
            Entity.LacreRepositorioEquipamento lacreRepositorioEquip = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT E.SEQ_LACRE_REPOSIT_EQUIP, ");
                    str.AppendLine("        A.NUM_BEM, ");
                    str.AppendLine("        NVL(TO_CHAR(A.NUM_PATRIMONIO),'SEM NÚMERO') || ' / ' || C.DSC_TIPO_PATRIMONIO PATRIMONIO, ");
                    str.AppendLine("        A.DSC_COMPLEMENTAR, ");
                    str.AppendLine("        F.NOM_USUARIO, ");
                    str.AppendLine("        E.DTA_TESTE ");
                    str.AppendLine(" FROM LACRE_REPOSITORIO D, ");
                    str.AppendLine("        LACRE_REPOSIT_EQUIPAMENTO E, ");
                    str.AppendLine("        BEM_PATRIMONIAL A, ");
                    str.AppendLine("        ITENS_LISTA_CONTROLE B, ");
                    str.AppendLine("        TIPO_PATRIMONIO C, USUARIO F ");
                    str.AppendLine(" WHERE D.SEQ_LACRE_REPOSITORIO =  E.SEQ_LACRE_REPOSITORIO ");
                    str.AppendLine("    AND A.COD_TIPO_BEM = B.COD_TIPO_BEM ");
                    str.AppendLine("    AND A.COD_TIPO_PATRIMONIO = C.COD_TIPO_PATRIMONIO ");
                    str.AppendLine("    AND E.NUM_BEM = A.NUM_BEM ");                    
                    str.AppendLine("    AND E.NUM_USER_RESP_TESTE = F.NUM_USER_BANCO(+) ");
                    str.AppendLine(string.Format(" AND E.SEQ_LACRE_REPOSIT_EQUIP = {0} ", seqLacreRepositorioEquipamento));

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lacreRepositorioEquip = new Entity.LacreRepositorioEquipamento();
                            lacreRepositorioEquip.LacreRepositorio = new Entity.LacreRepositorio();
                            lacreRepositorioEquip.BemPatrimonial = new Entity.BemPatrimonial();
                            lacreRepositorioEquip.UsuarioResponsavelTeste = new Entity.Usuario();

                            if (dr["SEQ_LACRE_REPOSIT_EQUIP"] != DBNull.Value)
                                lacreRepositorioEquip.SeqLacreRepositorioEquipamento = Convert.ToInt64(dr["SEQ_LACRE_REPOSIT_EQUIP"]);

                            if (dr["NUM_BEM"] != DBNull.Value)
                                lacreRepositorioEquip.BemPatrimonial.NumBem = Convert.ToInt64(dr["NUM_BEM"]);

                            if (dr["PATRIMONIO"] != DBNull.Value)
                                lacreRepositorioEquip.BemPatrimonial.DscModelo = dr["PATRIMONIO"].ToString();

                            if (dr["DSC_COMPLEMENTAR"] != DBNull.Value)
                                lacreRepositorioEquip.BemPatrimonial.DscComplementar = dr["DSC_COMPLEMENTAR"].ToString();

                            if (dr["NOM_USUARIO"] != DBNull.Value)
                                lacreRepositorioEquip.UsuarioResponsavelTeste.Nome = dr["NOM_USUARIO"].ToString();

                            if (dr["DTA_TESTE"] != DBNull.Value)
                                lacreRepositorioEquip.DataTeste = Convert.ToDateTime(dr["DTA_TESTE"]);

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

            return lacreRepositorioEquip;
        }

        /// <summary>
        /// Adicionar.
        /// </summary>        
        public long AdicionarTrans(Entity.LacreRepositorioEquipamento lacreRepEquipamento)
        {
            long _seqRetorno = 0;

            try
            {
               // obter o contexto transacionado
                Hcrp.Infra.AcessoDado.Contexto ctx = this.transacao.ctx;

                // Preparar o comando
                Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("LACRE_REPOSIT_EQUIPAMENTO");

                if (lacreRepEquipamento.LacreRepositorio != null && lacreRepEquipamento.LacreRepositorio.SeqLacreRepositorio > 0)
                    comando.Params["SEQ_LACRE_REPOSITORIO"] = lacreRepEquipamento.LacreRepositorio.SeqLacreRepositorio;

                if (!string.IsNullOrWhiteSpace(lacreRepEquipamento.IdfAtivo))
                    comando.Params["IDF_ATIVO"] = lacreRepEquipamento.IdfAtivo;

                comando.Params["DTA_CADASTRO"] = lacreRepEquipamento.DataCadastro;

                if (lacreRepEquipamento.BemPatrimonial != null && lacreRepEquipamento.BemPatrimonial.NumBem > 0)
                    comando.Params["NUM_BEM"] = lacreRepEquipamento.BemPatrimonial.NumBem;

                // Executar o insert
                ctx.ExecuteInsert(comando);

                _seqRetorno = Convert.ToInt64(ctx.GetSequenceValue("SEQ_LACRE_REPOSIT_EQUIP", false));
                

            }
            catch (Exception)
            {
                throw;
            }

            return _seqRetorno;

        }

        #endregion
    }
}
