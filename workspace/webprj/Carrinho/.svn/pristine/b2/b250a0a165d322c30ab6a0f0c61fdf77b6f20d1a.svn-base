using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hcrp.Infra.AcessoDado;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class LacreRepositorioComplemento
    {
        public List<Entity.Lacre_reposit_complemento> ObterComplemento(Int64 seq_lacre_repositorio)
        {
            List<Entity.Lacre_reposit_complemento> listaRetorno = new List<Entity.Lacre_reposit_complemento>();
            Entity.Lacre_reposit_complemento _itemLacreComplemento = null;

            
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT B.DSC_COMPLEMENTO, B.NUM_LACRE, B.IDF_ATIVO, B.SEQ_REPOSITORIO_COMPLEMENTO ");
                    str.AppendLine("   FROM LACRE_REPOSITORIO A, LACRE_REPOSIT_COMPLEMENTO B ");
                    str.AppendLine("  WHERE A.SEQ_LACRE_REPOSITORIO = B.SEQ_LACRE_REPOSITORIO ");
                    str.AppendLine(string.Format("    AND A.SEQ_LACRE_REPOSITORIO = {0} ", seq_lacre_repositorio));
                    str.AppendLine("  AND B.IDF_ATIVO = 'S' ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            _itemLacreComplemento = new Entity.Lacre_reposit_complemento();

                            if (dr["DSC_COMPLEMENTO"] != DBNull.Value)
                                _itemLacreComplemento.DSC_COMPLEMENTO = (dr["DSC_COMPLEMENTO"]).ToString();

                            if (dr["DSC_COMPLEMENTO"] != DBNull.Value)
                                _itemLacreComplemento.DSC_COMPLEMENTO = (dr["DSC_COMPLEMENTO"]).ToString();

                            if (dr["NUM_LACRE"] != DBNull.Value)
                                _itemLacreComplemento.NUM_LACRE = dr["NUM_LACRE"].ToString();

                            if (dr["IDF_ATIVO"] != DBNull.Value)
                                _itemLacreComplemento.IDF_ATIVO = dr["IDF_ATIVO"].ToString();

                            if (dr["SEQ_REPOSITORIO_COMPLEMENTO"] != DBNull.Value)
                                _itemLacreComplemento.SEQ_REPOSITORIO_COMPLEMENTO =
                                    Convert.ToInt64(dr["SEQ_REPOSITORIO_COMPLEMENTO"]);
                            

                            

                            listaRetorno.Add(_itemLacreComplemento);
                        }
                    }
                    finally
                    {
                        dr.Close();
                        dr.Dispose();
                    }
                }

            return listaRetorno;
        }

        public long Adicionar(Entity.Lacre_reposit_complemento repositorioComplemento)
        {
            long _seqRetorno = 0;

            try
            {
                // Criar contexto
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("GENERICO.LACRE_REPOSIT_COMPLEMENTO");

                    comando.Params["SEQ_LACRE_REPOSITORIO"] = repositorioComplemento.SEQ_LACRE_REPOSITORIO;

                    //comando.Params["SEQ_REPOSITORIO_COMPLEMENTO"]  

                    comando.Params["DSC_COMPLEMENTO"] = repositorioComplemento.DSC_COMPLEMENTO.ToUpper();

                    comando.Params["NUM_LACRE"] = repositorioComplemento.NUM_LACRE.ToUpper();

                    comando.Params["DTA_HOR_CADASTRO"]= DateTime.Now;

                    comando.Params["NUM_USER_CADASTRO"] = repositorioComplemento.NUM_USER_CADASTRO;
                    //comando.Params["IDF_ATIVO"] = "S"

                    // Trigger na tabela
                    //comando.Params["NUM_USER_CADASTRO"] = 59851;//_itensListaControle.UsuarioCadastro.NumUserBanco;

                    //comando.Params["IDF_ATIVO"] = "S";

                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                    _seqRetorno = Convert.ToInt64(ctx.GetSequenceValue("SEQ_REPOSITORIO_COMPLEMENTO", false));
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _seqRetorno;
        }

        public void AtivarOuInativar(bool ehPraAtivar, long SEQ_REPOSITORIO_COMPLEMENTO)
        {
            try
            {
                // obter o contexto transacionado
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("GENERICO.LACRE_REPOSIT_COMPLEMENTO");

                    comando.FilterParams["SEQ_REPOSITORIO_COMPLEMENTO"] = SEQ_REPOSITORIO_COMPLEMENTO;

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
    }
}
