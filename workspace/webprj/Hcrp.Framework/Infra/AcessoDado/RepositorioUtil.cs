using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Hcrp.Infra.AcessoDado
{
    public class RepositorioUtil : RepositorioBase
    {

        public Int32 ObterCodFormularioPrincipal(long codNotificacao)
        {
            try
            {
                Int32 retorno = 0; // Id do formulario 
                
                using (AcessoDado.Contexto ctx = new Contexto())
                {
                    ctx.Open();

                    QueryCommandConfig query = new QueryCommandConfig("SELECT COD_TIPO_NOTIFICACAO FROM GRS_NOTIFICACAO WHERE SEQ_GRS_NOTIFICACAO = " + codNotificacao);

                    ctx.ExecuteQuery(query);

                    while (ctx.Reader.Read())
                    {
                        retorno = Convert.ToInt32(ctx.Reader[0]);
                    }
                }

                return retorno;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Obter medidas preventivas
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ObterMedidaPreventiva()
        {
            try
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("SELECT *");
                str.AppendLine(" FROM MEDIDA_PREVENTIVA");
                str.AppendLine("  WHERE IDF_ATIVO = 'S'");
                
                return ObterCodigoEDescricao(str.ToString());

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter outros locais evento
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ObterLocalEvento()
        {
            try
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("SELECT LE.SEQ_LOCAL_EVENTO, LE.DSC_LOCAL_EVENTO");
                str.AppendLine(" FROM GRS_LOCAL_EVENTO LE, GRS_LOCAL_EVENTO_TP_NOTIFIC LET");
                str.AppendLine("  WHERE LE.SEQ_LOCAL_EVENTO=LET.SEQ_LOCAL_EVENTO");
                str.AppendLine("   AND LET.COD_TIPO_NOTIFICACAO=" + Hcrp.Framework.Infra.Util.Parametrizacao.Instancia().CodNotificacao);
                str.AppendLine("    AND LE.IDF_ATIVO='S'");
                str.AppendLine("      ORDER BY LET.NUM_ORDEM");

                return ObterCodigoEDescricao(str.ToString());

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter outros locais evento
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ObterLocalEvento(int codInstituto)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("SELECT TO_CHAR(LE.SEQ_LOCAL_EVENTO) || '*' || LE.IDF_COMPLEMENTO_LOCAL, LE.DSC_LOCAL_EVENTO");
                str.AppendLine(" FROM GRS_LOCAL_EVENTO LE, GRS_LOCAL_EVENTO_TP_NOTIFIC LET");
                str.AppendLine("  WHERE LE.SEQ_LOCAL_EVENTO=LET.SEQ_LOCAL_EVENTO");
                str.AppendLine("   AND LET.COD_TIPO_NOTIFICACAO=" + Hcrp.Framework.Infra.Util.Parametrizacao.Instancia().CodNotificacao);
                str.AppendLine("    AND LE.IDF_ATIVO='S'");
                str.AppendLine("     AND LET.COD_INSTITUTO=" + codInstituto);
                str.AppendLine("      ORDER BY LET.NUM_ORDEM");
                
                return ObterCodigoEDescricao(str.ToString());

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter Possiveis causas evento relacionado
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ObterPossiveisCausasEventoRelac()
        {
            try
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("SELECT T.COD_OCORRENCIA, T.DSC_OCORRENCIA");
                str.AppendLine(" FROM TIPO_OCORRENCIA T");
                str.AppendLine("  INNER JOIN OCORRENCIA_SISTEMA OS ON OS.COD_OCORRENCIA = T.COD_OCORRENCIA");
                str.AppendLine("   WHERE OS.COD_DOMINIO = 19");
                str.AppendLine("    ORDER BY OS.NUM_ORDENACAO");

                return ObterCodigoEDescricao(str.ToString());

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter Ocorrencias mediante codigo do dominio
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ObterOcorrenciaPorCodDeDominio(int codDominio)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("SELECT T.COD_OCORRENCIA, T.DSC_OCORRENCIA");
                str.AppendLine(" FROM TIPO_OCORRENCIA T");
                str.AppendLine("  INNER JOIN OCORRENCIA_SISTEMA OS ON OS.COD_OCORRENCIA = T.COD_OCORRENCIA");
                str.AppendLine("   WHERE OS.COD_DOMINIO = " + codDominio);
                str.AppendLine("    ORDER BY OS.NUM_ORDENACAO");

                return ObterCodigoEDescricao(str.ToString());

            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Obter Ocorrencias Eventos Relacionados
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ObterOcorrenciaEventoRelac()
        {
            try
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("SELECT T.COD_OCORRENCIA, T.DSC_OCORRENCIA");
                str.AppendLine(" FROM TIPO_OCORRENCIA T");
                str.AppendLine("  INNER JOIN OCORRENCIA_SISTEMA OS ON OS.COD_OCORRENCIA = T.COD_OCORRENCIA");
                str.AppendLine("   WHERE OS.COD_DOMINIO = 18");
                str.AppendLine("    ORDER BY OS.NUM_ORDENACAO");

                return ObterCodigoEDescricao(str.ToString());

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter Queixa técnica equipamento
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ObterQueixaTecnica()
        {
            try
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("SELECT T.COD_OCORRENCIA, T.DSC_OCORRENCIA");
                str.AppendLine(" FROM TIPO_OCORRENCIA T");
                str.AppendLine("  INNER JOIN OCORRENCIA_SISTEMA OS ON OS.COD_OCORRENCIA = T.COD_OCORRENCIA");
                str.AppendLine("   WHERE OS.COD_DOMINIO = 17");
                str.AppendLine("    ORDER BY OS.NUM_ORDENACAO");

                return ObterCodigoEDescricao(str.ToString());

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter Patrimonio
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ObterPatrimonio(decimal numeroPatrimonio)
        {
            try
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("SELECT TP.COD_TIPO_PATRIMONIO, TP.DSC_TIPO_PATRIMONIO");
                str.AppendLine(" FROM NUMERO_PATRIMONIO NP, TIPO_PATRIMONIO TP");
                str.AppendLine("  WHERE NP.COD_TIPO_PATRIMONIO=TP.COD_TIPO_PATRIMONIO AND");
                str.AppendLine(string.Concat("    NP.NUM_PATRIMONIO=", numeroPatrimonio.ToString()));
                str.AppendLine("        ORDER BY DSC_TIPO_PATRIMONIO");

                return ObterCodigoEDescricao(str.ToString());

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter tipos de consequencia.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ObterTipoConsequencia()
        {
            try
            {
                StringBuilder str = new StringBuilder();

                str.AppendLine("SELECT COD_TIPO_CONSEQUENCIA, DSC_TIPO_CONSEQUENCIA");
                str.AppendLine(" FROM GRS_TIPO_CONSEQUENCIA");
                str.AppendLine("  WHERE IDF_ATIVO = 'S'");
                
                return ObterCodigoEDescricao(str.ToString());

            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter a lista de materiais
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ObterCentroDeCusto(string nome)
        {
            StringBuilder str = new StringBuilder();

            str.AppendLine("SELECT CC.COD_CENCUSTO, CC.NOM_CENCUSTO");
            str.AppendLine(" FROM CENTRO_CUSTO CC, INSTITUTO I");
            str.AppendLine("  WHERE I.COD_INSTITUTO=CC.COD_INSTITUTO AND CC.IDF_ATIVO='S' AND");
            str.AppendLine("   I.COD_INST_SISTEMA=GENERICO.FCN_BUSCA_INST_SISTEMAIP('10.165.0.14',1) AND");
            str.AppendLine("    CC.NOM_CENCUSTO LIKE  '%" + nome.ToUpper() + "%'");

            return ObterCodigoEDescricao(str.ToString());
        }

        /// <summary>
        /// Obter informações do paciente
        /// </summary>
        /// <returns></returns>
        public string[] ObterInfoPaciente(string  codPaciente)
        {
            try
            {
                string[] retorno = new string[4];
                

                StringBuilder str = new StringBuilder();

                str.AppendLine("SELECT ");
                str.AppendLine(" DECODE(IDF_COR,0,'NÃO INFORMADO',1,'BRANCO',2,'PRETO',3,'AMARELO',4,'VERMELHO',5,'MULATO') COR,");
                str.AppendLine("  CASE WHEN IDF_SEXO = 'F' THEN 'FEMININO' ELSE 'MASCULINO' END SEXO,");
                str.AppendLine("   TO_CHAR(DTA_NASCIMENTO,'dd/MM/yyyy') DATANASC, NVL(PACIENTE.NOM_PACIENTE,'') || ' ' || NVL(PACIENTE.SBN_PACIENTE,'') as NOME");
                str.AppendLine("    FROM PACIENTE WHERE COD_PACIENTE = '" + codPaciente + "'");

                using (AcessoDado.Contexto ctx = new Contexto())
                {
                    ctx.Open();

                    QueryCommandConfig query = new QueryCommandConfig(str.ToString());

                    ctx.ExecuteQuery(query);

                    while (ctx.Reader.Read())
                    {
                        retorno[0] = base.GetDataValue<string>(ctx.Reader, "COR");
                        retorno[1] = base.GetDataValue<string>(ctx.Reader, "SEXO");
                        retorno[2] = base.GetDataValue<string>(ctx.Reader, "DATANASC");
                        retorno[3] = base.GetDataValue<string>(ctx.Reader, "NOME");
                    }
                }

                return retorno;

            }
            catch (Exception)
            {
                throw;
            }
        }



        /// <summary>
        /// Obter a lista de ocorrencias
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ObterOcorrencia()
        { 
            StringBuilder str = new StringBuilder();

            str.AppendLine("SELECT T.COD_OCORRENCIA, T.DSC_OCORRENCIA");
            str.AppendLine(" FROM TIPO_OCORRENCIA T");
            str.AppendLine("  INNER JOIN OCORRENCIA_SISTEMA OS");
            str.AppendLine("   ON OS.COD_OCORRENCIA = T.COD_OCORRENCIA");
            str.AppendLine("    WHERE OS.COD_DOMINIO = 16");
            str.AppendLine("     ORDER BY OS.NUM_ORDENACAO");

            return ObterCodigoEDescricao(str.ToString());
        }

        /// <summary>
        /// Obter id e descrição de qualquer tabela
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> ObterCodigoEDescricao(string queryBusca)
        {
            try
            {
                Dictionary<string, string> retorno = new Dictionary<string, string>();
                
                using (AcessoDado.Contexto ctx = new Contexto())
                {
                    ctx.Open();

                    QueryCommandConfig query = new QueryCommandConfig(queryBusca);

                    ctx.ExecuteQuery(query);

                    while (ctx.Reader.Read())
                    {
                        retorno.Add(ctx.Reader[0].ToString(), ctx.Reader[1].ToString());
                    }
                }

                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Obter institutos do usuário.
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> ObterTodosInstitutos()
        {
            try
            {
                Dictionary<int, string> retorno = new Dictionary<int, string>();

                using (AcessoDado.Contexto ctx = new Contexto())
                {
                    ctx.Open();

                    string ipUsuario = Hcrp.Framework.Infra.Util.Parametrizacao.Instancia().ObterIpDoUsuario; //System.Web.HttpContext.Current.Request.UserHostAddress;
                    string sql = string.Concat("SELECT COD_INSTITUTO, NOM_INSTITUTO FROM INSTITUTO I WHERE I.COD_INST_SISTEMA=GENERICO.FCN_BUSCA_INST_SISTEMAIP('" + ipUsuario + "',1)");

                    QueryCommandConfig query = new QueryCommandConfig(sql);
                    
                    ctx.ExecuteQuery(query);

                    while (ctx.Reader.Read())
                    {
                        retorno.Add(Convert.ToInt32(ctx.Reader["COD_INSTITUTO"]), ctx.Reader["NOM_INSTITUTO"].ToString());
                    }

                }

                return retorno;

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Obter instituto do usuário.
        /// </summary>
        /// <returns></returns>
        public int ObterInstituto(bool instituto = true)
        {
            try
            {
                int retorno = 0;

                using (AcessoDado.Contexto ctx = new Contexto())
                {
                    ctx.Open();

                    string ipUsuario = Hcrp.Framework.Infra.Util.Parametrizacao.Instancia().ObterIpDoUsuario; //System.Web.HttpContext.Current.Request.UserHostAddress;

                    if (ipUsuario == "::1")
                        ipUsuario = "127.0.0.1";

                    // todo: RETIRARRRRRRR
                    if (ipUsuario == "127.0.0.1")
                        ipUsuario = "10.165.100.76";
                    
                    string sql;

                    if(instituto)
                        sql = string.Concat("SELECT GENERICO.FCN_BUSCA_INST_SISTEMAIP('", ipUsuario, "',2) as codInstituto FROM DUAL");
                    else
                        sql = string.Concat("SELECT GENERICO.FCN_BUSCA_INST_SISTEMAIP('", ipUsuario, "',1) as codInstituto FROM DUAL");

                    QueryCommandConfig query = new QueryCommandConfig(sql);

                    ctx.ExecuteQuery(query);

                    while (ctx.Reader.Read())
                    {
                        if (ctx.Reader["codInstituto"] != DBNull.Value)
                            retorno = Convert.ToInt32(ctx.Reader["codInstituto"]);
                    }
                   
                }

                return retorno;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Obter número do usuario logado no banco.
        /// </summary>
        /// <returns></returns>
        public int ObterNumeroNoBancoDoUsuarioLogado()
        {
            try
            {
                int retorno = 0;

                using (AcessoDado.Contexto ctx = new Contexto())
                {
                    ctx.Open();

                    string sql;

                    sql = string.Concat("SELECT FC_NUM_USER_BANCO NUM FROM DUAL");

                    QueryCommandConfig query = new QueryCommandConfig(sql);

                    ctx.ExecuteQuery(query);

                    while (ctx.Reader.Read())
                    {
                        if (ctx.Reader["NUM"] != DBNull.Value)
                            retorno = Convert.ToInt32(ctx.Reader["NUM"]);
                    }
                }

                return retorno;

            }
            catch (Exception)
            {
                throw;
            }

        }

        #region Pesquisa

        /// <summary>
        /// Obter a lista de pacientes
        /// </summary>
        /// <returns></returns>
        public System.Data.DataView ObterPaciente(string nome, string sobrenome, int paginaAtual, out int totalRegistro)
        {
            StringBuilder str = new StringBuilder();
            StringBuilder strTotalRegistro = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();

            // Montar escopo de paginação.
            Int32 numeroRegistroPorPagina = Hcrp.Framework.Infra.Util.Parametrizacao.Instancia().QuantidadeRegistroPagina;
            Int32 ultimoIndice = (numeroRegistroPorPagina * paginaAtual);
            Int32 primeiroIndice = (ultimoIndice - numeroRegistroPorPagina) + 1;
            Int32 totalRegistroAux = 0;

            // Query Principal
            str.AppendLine("SELECT *");
            str.AppendLine(" FROM (SELECT A.*,");
            str.AppendLine("  ROWNUM AS RNUM FROM");
            str.AppendLine("   (SELECT");
            str.AppendLine("    PACIENTE.COD_PACIENTE AS \"Código\",");
            //str.Append("     PACIENTE.NOM_PACIENTE,");
            //str.Append("      PACIENTE.SBN_PACIENTE,");
            str.AppendLine("       NVL(PACIENTE.NOM_PACIENTE,'') || ' ' || NVL(PACIENTE.SBN_PACIENTE,'') AS \"Nome Completo\",");
            str.AppendLine("        TO_CHAR(DTA_NASCIMENTO, 'DD/MM/YYYY') AS \"Data de Nascimento\"");
            str.AppendLine("         FROM PACIENTE");

            if (!string.IsNullOrWhiteSpace(nome) || !string.IsNullOrWhiteSpace(sobrenome))
            {
                // Where da Query
                strWhere.AppendLine(" WHERE");
                strWhere.AppendLine(" (NOM_PACIENTE LIKE UPPER('" + nome + "' || '%') AND");
                strWhere.AppendLine(" SBN_PACIENTE LIKE UPPER('%' || '" + sobrenome + "' || '%'))");
            }

            str.AppendLine(strWhere.ToString());
            str.AppendLine(" ORDER BY PACIENTE.NOM_PACIENTE) A");
            str.AppendLine(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

            strTotalRegistro.AppendLine("SELECT COUNT(*) as total FROM PACIENTE ");
            strTotalRegistro.AppendLine(strWhere.ToString());

            using (AcessoDado.Contexto ctx = new Contexto())
            {
                ctx.Open();

                QueryCommandConfig query = new QueryCommandConfig(str.ToString());
                QueryCommandConfig queryCount = new QueryCommandConfig(strTotalRegistro.ToString());

                // Veriricar contador
                ctx.ExecuteQuery(queryCount);

                while (ctx.Reader.Read())
                {
                    totalRegistroAux = Convert.ToInt32(ctx.Reader["total"]);
                }

                // Obter a lista de registros
                ctx.ExecuteQuery(query);

                System.Data.DataTable dt = new System.Data.DataTable();

                dt.Load(ctx.Reader);

                dt.Columns.Remove("RNUM");

                // Preparar retorno
                totalRegistro = totalRegistroAux;
                return dt.DefaultView;
            }
        }



        /// <summary>
        /// Obter a lista de materiais
        /// </summary>
        /// <returns></returns>
        public System.Data.DataView ObterMaterial(string codigo, string nome, int paginaAtual, out int totalRegistro)
        {
            StringBuilder str = new StringBuilder();
            StringBuilder strTotalRegistro = new StringBuilder();
            StringBuilder strWhere = new StringBuilder();

            // Montar escopo de paginação.
            Int32 numeroRegistroPorPagina = Hcrp.Framework.Infra.Util.Parametrizacao.Instancia().QuantidadeRegistroPagina;
            Int32 ultimoIndice = (numeroRegistroPorPagina * paginaAtual);
            Int32 primeiroIndice = (ultimoIndice - numeroRegistroPorPagina) + 1;
            Int32 totalRegistroAux = 0;

            // Query Principal
            str.Append("SELECT * FROM (SELECT A.*, ROWNUM AS RNUM FROM (SELECT M.COD_MATERIAL as \"Código\", M.NOM_MATERIAL as \"Nome\" FROM MATERIAL M");

            if (!string.IsNullOrWhiteSpace(codigo) || !string.IsNullOrWhiteSpace(nome))
            {
                // Where da Query
                strWhere.Append(" WHERE");

                if (!string.IsNullOrWhiteSpace(codigo) && !string.IsNullOrWhiteSpace(nome))
                {
                    strWhere.Append(" M.COD_MATERIAL='" + codigo + "'");
                    strWhere.Append(" AND M.NOM_MATERIAL LIKE '" + nome.ToUpper() + "%'");
                }
                else if (!string.IsNullOrWhiteSpace(codigo) && string.IsNullOrWhiteSpace(nome))
                {
                    strWhere.Append(" M.COD_MATERIAL='" + codigo + "'");
                }
                else if (string.IsNullOrWhiteSpace(codigo) && !string.IsNullOrWhiteSpace(nome))
                {
                    strWhere.Append(" M.NOM_MATERIAL LIKE '" + nome.ToUpper() + "%'");
                }
            }

            str.Append(strWhere.ToString());
            str.Append(" ORDER BY M.NOM_MATERIAL) A");
            str.Append(" WHERE ROWNUM <= " + ultimoIndice.ToString() + " ) WHERE RNUM >= " + primeiroIndice + "");

            strTotalRegistro.Append("SELECT COUNT(*) as total FROM MATERIAL M");
            strTotalRegistro.Append(strWhere.ToString());

            using (AcessoDado.Contexto ctx = new Contexto())
            {
                ctx.Open();

                QueryCommandConfig query = new QueryCommandConfig(str.ToString());
                QueryCommandConfig queryCount = new QueryCommandConfig(strTotalRegistro.ToString());

                // Veriricar contador
                ctx.ExecuteQuery(queryCount);

                while (ctx.Reader.Read())
                {
                    totalRegistroAux = Convert.ToInt32(ctx.Reader["total"]);
                }

                // Obter a lista de registros
                ctx.ExecuteQuery(query);

                System.Data.DataTable dt = new System.Data.DataTable();

                dt.Load(ctx.Reader);

                dt.Columns.Remove("RNUM");

                // Preparar retorno
                totalRegistro = totalRegistroAux;
                return dt.DefaultView;
            }
        }

        #endregion

    }
}
