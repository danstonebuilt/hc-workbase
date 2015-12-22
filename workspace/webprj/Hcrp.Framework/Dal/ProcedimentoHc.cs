using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    class ProcedimentoHc : Hcrp.Framework.Classes.ProcedimentoHc
    {
        public ProcedimentoHc BuscaProcedimentoCodigo(int codProcedimento)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COD_PROCEDIMENTO_HC, NOM_PROCEDIMENTO_HC " + Environment.NewLine);
                    sb.Append(" FROM PROCEDIMENTO_HC " + Environment.NewLine);
                    sb.Append(" WHERE COD_PROCEDIMENTO_HC = :COD_PROCEDIMENTO_HC " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_PROCEDIMENTO_HC"] = codProcedimento;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        this.Codigo = Convert.ToInt32(dr["COD_PROCEDIMENTO_HC"]);
                        this.Descricao = Convert.ToString(dr["NOM_PROCEDIMENTO_HC"]);
                    }

                }
                return this;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public double InserirProcedMatCC(Framework.Classes.ProcedimentoHc Proc)
        {
            try
            {
                double retorno = 0;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PROCED_MAT_CENCUSTO");
                    comando.Params["NUM_USER_CADASTRO"] = Proc.MatCCUsuarioCad;
                    comando.Params["DTA_HOR_CADASTRO"] = Proc.MatCCDataCadastro;
                    comando.Params["COD_CENCUSTO"] = Proc._CodCC;
                    comando.Params["COD_MATERIAL"] = Proc._CodMaterial;
                    comando.Params["COD_PROCEDIMENTO_HC"] = Proc.Codigo;
                     
                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                    // Pegar o último ID
                    retorno = ctx.GetSequenceValue("GENERICO.SEQ_PROCED_MAT_CENCUSTO", false);

                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Hcrp.Framework.Classes.ProcedimentoHc> BuscaProcedimentoMatCC(string CodMaterial, string CodCenCusto)
        {
            List<Hcrp.Framework.Classes.ProcedimentoHc> _listaDeRetorno = new List<Hcrp.Framework.Classes.ProcedimentoHc>();
            Hcrp.Framework.Classes.ProcedimentoHc _procmatcc = null;
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder str = new StringBuilder();
                    str.AppendLine(" SELECT PMC.SEQ_PROCED_MAT_CENCUSTO, PMC.COD_PROCEDIMENTO_HC, P.NOM_PROCEDIMENTO_HC ");
                    str.AppendLine("  FROM PROCED_MAT_CENCUSTO PMC, PROCEDIMENTO_HC P ");
                    str.AppendLine("  WHERE PMC.COD_MATERIAL = " + CodMaterial);
                    str.AppendLine("    AND PMC.COD_CENCUSTO = " + CodCenCusto);
                    str.AppendLine("    AND PMC.NUM_USER_EXCLUSAO IS NULL");
                    str.AppendLine("    AND PMC.COD_PROCEDIMENTO_HC = P.COD_PROCEDIMENTO_HC");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());
                    
                    // Veriricar contador
                    ctx.ExecuteQuery(query);
                    
                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        _procmatcc = new Hcrp.Framework.Classes.ProcedimentoHc();

                        _procmatcc.Codigo = Convert.ToInt32(dr["COD_PROCEDIMENTO_HC"]);
                        _procmatcc.Descricao = Convert.ToString(dr["NOM_PROCEDIMENTO_HC"]);
                        _procmatcc.MatCCSeq = Convert.ToInt32(dr["SEQ_PROCED_MAT_CENCUSTO"]);
                        _listaDeRetorno.Add(_procmatcc);
                    }

                }                
            }
            catch (Exception)
            {
                throw;
            }
            return _listaDeRetorno;
        }

        public double AlterarProcedMatCC(Framework.Classes.ProcedimentoHc Proc)
        {
            try
            {
                double retorno = 0;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("UPDATE PROCED_MAT_CENCUSTO SET NUM_USER_EXCLUSAO = " + Proc.MatCCUsuarioExc.ToString() + ", DTA_HOR_EXCLUSAO = SYSDATE WHERE SEQ_PROCED_MAT_CENCUSTO = " + Proc.MatCCSeq.ToString());                                        

                    // Executar o Update
                    ctx.ExecuteUpdate(comando);

                    // Pegar o último ID
                    retorno = Proc.MatCCSeq;

                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter os procedimentos HC em vigência.
        /// </summary>       
        public List<Hcrp.Framework.Classes.ProcedimentoHc> ObterProcedimentoHCEmVigencia()
        {
            List<Hcrp.Framework.Classes.ProcedimentoHc> _listaDeRetorno = new List<Hcrp.Framework.Classes.ProcedimentoHc>();
            Hcrp.Framework.Classes.ProcedimentoHc _item = null;
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder str = new StringBuilder();
                    str.AppendLine(" SELECT DISTINCT P.COD_PROCEDIMENTO_HC, P.NOM_PROCEDIMENTO_HC ");
                    str.AppendLine(" FROM   SADT_CONF_PROC_AGENDA A, PROCEDIMENTO_HC P ");
                    str.AppendLine(" WHERE A.DTA_FIM_VIGENCIA>SYSDATE ");
                    str.AppendLine(" AND A.COD_PROCEDIMENTO_HC=P.COD_PROCEDIMENTO_HC ");
                    str.AppendLine(" ORDER BY NOM_PROCEDIMENTO_HC ");

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Veriricar contador
                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        _item = new Hcrp.Framework.Classes.ProcedimentoHc();

                        if (dr["COD_PROCEDIMENTO_HC"] != DBNull.Value)
                            _item.Codigo = Convert.ToInt32(dr["COD_PROCEDIMENTO_HC"]);

                        if (dr["NOM_PROCEDIMENTO_HC"] != DBNull.Value)
                            _item.Descricao = dr["NOM_PROCEDIMENTO_HC"].ToString();

                        _listaDeRetorno.Add(_item);
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            return _listaDeRetorno;
        }

        /// <summary>
		/// Método utilizado para buscar os institutos pelos filtros informados
		/// </summary>
		/// <param name="codigoInstituicao">Código da instituição</param>
		/// <param name="codigoProcedimento">Código do procedimento</param>
		/// <param name="descricaoProcedimento">Descrição do procedimento</param>
		/// <returns>Procedimentos listados</returns>
		public List<Hcrp.Framework.Classes.ProcedimentoHc> ObterProcedimentoHC(Int32? codigoInstituicao, Int32? codigoProcedimento, String descricaoProcedimento)
		{
			List<Hcrp.Framework.Classes.ProcedimentoHc> _listaDeRetorno = new List<Hcrp.Framework.Classes.ProcedimentoHc>();
			Hcrp.Framework.Classes.ProcedimentoHc _item = null;
			try
			{
				using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
				{
					ctx.Open();
					StringBuilder str = new StringBuilder();

					str.AppendLine(" SELECT DISTINCT P.COD_PROCEDIMENTO_HC, P.NOM_PROCEDIMENTO_HC ");
					str.AppendLine("   FROM PROCEDIMENTO_HC       P, ");
					str.AppendLine("        GRUPO_PROCEDIMENTO_HC G, ");
					str.AppendLine("        MAPEAMENTO_LOCAL      ML, ");
					str.AppendLine("        INSTITUTO             I ");
					str.AppendLine("  WHERE P.COD_PROCEDIMENTO_HC = G.COD_PROCEDIMENTO_HC ");
					str.AppendLine("    AND G.NUM_SEQ_LOCAL       = ML.NUM_SEQ_LOCAL ");
					str.AppendLine("    AND ML.COD_INSTITUTO      = I.COD_INSTITUTO ");
					str.AppendLine("    AND P.IDF_ATIVO           = 'S' ");
					if (codigoInstituicao.HasValue) { str.AppendLine(" AND I.COD_INST_SISTEMA  = " + codigoInstituicao.Value.ToString()); }
					if (codigoProcedimento.HasValue) { str.AppendLine(" AND I.COD_PROCEDIMENTO_HC = " + codigoProcedimento.Value.ToString()); }
                    if (!String.IsNullOrEmpty(descricaoProcedimento)) { str.AppendLine(String.Format(" AND P.NOM_PROCEDIMENTO_HC LIKE '%{0}%'", descricaoProcedimento)); }
					str.AppendLine(" UNION ");
					str.AppendLine(" SELECT DISTINCT P.COD_PROCEDIMENTO_HC, P.NOM_PROCEDIMENTO_HC ");
					str.AppendLine("   FROM PROCEDIMENTO_HC               P, ");
					str.AppendLine("        MAPEAMENTO_LOCAL_PROCEDIMENTO MP, ");
					str.AppendLine("        MAPEAMENTO_LOCAL              ML, ");
					str.AppendLine("        INSTITUTO                     I ");
					str.AppendLine("  WHERE P.COD_PROCEDIMENTO_HC = MP.COD_PROCEDIMENTO_HC ");
					str.AppendLine("    AND MP.NUM_SEQ_LOCAL      = ML.NUM_SEQ_LOCAL ");
					str.AppendLine("    AND ML.COD_INSTITUTO      = I.COD_INSTITUTO ");
					str.AppendLine("    AND P.IDF_ATIVO           = 'S' ");
					if (codigoInstituicao.HasValue) { str.AppendLine(" AND I.COD_INST_SISTEMA  = " + codigoInstituicao.Value.ToString()); }
					if (codigoProcedimento.HasValue) { str.AppendLine(" AND I.COD_PROCEDIMENTO_HC = " + codigoProcedimento.Value.ToString()); }
                    if (!String.IsNullOrEmpty(descricaoProcedimento)) { str.AppendLine(String.Format(" AND P.NOM_PROCEDIMENTO_HC LIKE '%{0}%'", descricaoProcedimento)); }
					str.AppendLine(" UNION ");
					str.AppendLine(" SELECT DISTINCT P.COD_PROCEDIMENTO_HC, P.NOM_PROCEDIMENTO_HC ");
					str.AppendLine("   FROM SADT_CONF_PROC_AGENDA  A, ");
					str.AppendLine("        PROCEDIMENTO_HC        P, ");
					str.AppendLine("        PROCEDIMENTO_INSTITUTO PI, ");
					str.AppendLine("        INSTITUTO              I ");
					str.AppendLine("  WHERE A.DTA_FIM_VIGENCIA    > SYSDATE ");
					str.AppendLine("    AND A.COD_PROCEDIMENTO_HC = P.COD_PROCEDIMENTO_HC ");
					str.AppendLine("    AND P.COD_PROCEDIMENTO_HC = PI.COD_PROCEDIMENTO_HC ");
					str.AppendLine("    AND PI.COD_INSTITUTO      = I.COD_INSTITUTO ");
					if (codigoInstituicao.HasValue) { str.AppendLine(" AND I.COD_INST_SISTEMA  = " + codigoInstituicao.Value.ToString()); }
					if (codigoProcedimento.HasValue) { str.AppendLine(" AND I.COD_PROCEDIMENTO_HC = " + codigoProcedimento.Value.ToString()); }
					if (!String.IsNullOrEmpty(descricaoProcedimento)) { str.AppendLine(String.Format(" AND P.NOM_PROCEDIMENTO_HC LIKE '%{0}%'", descricaoProcedimento)); }
					str.AppendLine("  ORDER BY NOM_PROCEDIMENTO_HC ");

					// Preparar a query
					Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

					// Veriricar contador
					ctx.ExecuteQuery(query);

					// Cria objeto de material
					OracleDataReader dr = ctx.Reader as OracleDataReader;

					while (dr.Read())
					{
						_item = new Hcrp.Framework.Classes.ProcedimentoHc();

						if (dr["COD_PROCEDIMENTO_HC"] != DBNull.Value)
							_item.Codigo = Convert.ToInt32(dr["COD_PROCEDIMENTO_HC"]);

						if (dr["NOM_PROCEDIMENTO_HC"] != DBNull.Value)
							_item.Descricao = dr["NOM_PROCEDIMENTO_HC"].ToString();

						_listaDeRetorno.Add(_item);
					}

				}
			}
			catch (Exception)
			{
				throw;
			}
			return _listaDeRetorno;
		}
    
    }
}
