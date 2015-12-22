using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class LacreOcorrencia
    {
        #region Métodos

        /// <summary>
        /// Obter por seq lacre repositório.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreOcorrencia> ObterPorSeqLacreRepositorio(Int64 seqLacreRepositorio)
        {
            List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreOcorrencia> listLacreOcorrencia = new List<Entity.LacreOcorrencia>();
            Hcrp.CarroUrgenciaPsicoativo.Entity.LacreOcorrencia lacreOcorrencia = null;

            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    str.AppendLine(" SELECT B.SEQ_LACRE_OCORRENCIA, ");
                    str.AppendLine("        B.SEQ_LACRE_REPOSITORIO, ");
                    str.AppendLine("        B.DSC_OCORRENCIA, ");
                    str.AppendLine("        B.DTA_CADASTRO, ");
                    str.AppendLine("        C.NOM_USUARIO ");
                    str.AppendLine(" FROM LACRE_REPOSITORIO A, ");
                    str.AppendLine("    LACRE_OCORRENCIA B, ");
                    str.AppendLine("    USUARIO C ");
                    str.AppendLine(" WHERE ");
                    str.AppendLine("    A.SEQ_LACRE_REPOSITORIO = B.SEQ_LACRE_REPOSITORIO ");
                    str.AppendLine("    AND B.NUM_USER_CADASTRO = C.NUM_USER_BANCO ");
                    str.AppendLine(string.Format(" AND A.SEQ_LACRE_REPOSITORIO = {0} ", seqLacreRepositorio));
                    str.AppendLine(" ORDER BY B.DTA_CADASTRO DESC ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    try
                    {
                        while (dr.Read())
                        {
                            lacreOcorrencia = new Entity.LacreOcorrencia();
                            lacreOcorrencia.LacreRepositorio = new Entity.LacreRepositorio();
                            lacreOcorrencia.UsuarioCadastro = new Entity.Usuario();

                            if (dr["SEQ_LACRE_OCORRENCIA"] != DBNull.Value)
                                lacreOcorrencia.SeqLacreOcorrencia = Convert.ToInt64(dr["SEQ_LACRE_OCORRENCIA"]);

                            if (dr["SEQ_LACRE_REPOSITORIO"] != DBNull.Value)
                                lacreOcorrencia.LacreRepositorio.SeqLacreRepositorio = Convert.ToInt64(dr["SEQ_LACRE_REPOSITORIO"]);

                            if (dr["DSC_OCORRENCIA"] != DBNull.Value)
                                lacreOcorrencia.DscOcorrencia = dr["DSC_OCORRENCIA"].ToString();

                            if (dr["DTA_CADASTRO"] != DBNull.Value)
                                lacreOcorrencia.DataCadastro = Convert.ToDateTime(dr["DTA_CADASTRO"]);

                            if (dr["NOM_USUARIO"] != DBNull.Value)
                                lacreOcorrencia.UsuarioCadastro.Nome = dr["NOM_USUARIO"].ToString();

                            listLacreOcorrencia.Add(lacreOcorrencia);
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

            return listLacreOcorrencia;
        }

        /// <summary>
        /// Adicionar.
        /// </summary>        
        public long Adicionar(Entity.LacreOcorrencia lacreOcorrencia)
        {
            long _seqRetorno = 0;

            try
            {
                // Criar contexto
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("LACRE_OCORRENCIA");

                    if (lacreOcorrencia.LacreRepositorio != null && lacreOcorrencia.LacreRepositorio.SeqLacreRepositorio > 0)
                        comando.Params["SEQ_LACRE_REPOSITORIO"] = lacreOcorrencia.LacreRepositorio.SeqLacreRepositorio;

                    if (!string.IsNullOrWhiteSpace(lacreOcorrencia.DscOcorrencia))
                        comando.Params["DSC_OCORRENCIA"] = lacreOcorrencia.DscOcorrencia;

                    comando.Params["DTA_CADASTRO"] = lacreOcorrencia.DataCadastro;

                    if (lacreOcorrencia.UsuarioCadastro != null && lacreOcorrencia.UsuarioCadastro.NumUserBanco > 0)
                        comando.Params["NUM_USER_CADASTRO"] = lacreOcorrencia.UsuarioCadastro.NumUserBanco;

                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                    _seqRetorno = Convert.ToInt64(ctx.GetSequenceValue("SEQ_LACRE_OCORRENCIA", false));
                }

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
