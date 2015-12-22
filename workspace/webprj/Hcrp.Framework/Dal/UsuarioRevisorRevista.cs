using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class UsuarioRevisorRevista : Hcrp.Framework.Classes.UsuarioRevisorRevista
    {
        public int RetornaQtdRevisando(Hcrp.Framework.Classes.UsuarioRevisorRevista u) 
        { 
            int retorno = 0;
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COUNT(*) QTD FROM revista_artigo_revisao A " + Environment.NewLine);
                    sb.Append(" WHERE A.NUM_USER_REVISAO = :NUM_USER_REVISAO " + Environment.NewLine);
                    sb.Append(" AND A.IDF_ACEITACAO = 1 " + Environment.NewLine);
                    sb.Append("   AND A.DTA_FINALIZACAO IS NULL " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["NUM_USER_REVISAO"] = u.NumUserBanco;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        retorno = Convert.ToInt32(dr["QTD"]);
                    }
                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int RetornaQtdRevisado(Hcrp.Framework.Classes.UsuarioRevisorRevista u)
        {
            int retorno = 0;
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COUNT(*) QTD FROM revista_artigo_revisao A " + Environment.NewLine);
                    sb.Append(" WHERE A.NUM_USER_REVISAO = :NUM_USER_REVISAO " + Environment.NewLine);
                    sb.Append(" AND A.IDF_ACEITACAO = 1 " + Environment.NewLine);
                    sb.Append("   AND A.DTA_FINALIZACAO IS NOT NULL " + Environment.NewLine);
                    sb.Append("   AND A.DTA_CONVITE > TO_DATE('01/03/2012', 'DD/MM/YYYY') " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["NUM_USER_REVISAO"] = u.NumUserBanco;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        retorno = Convert.ToInt32(dr["QTD"]);
                    }
                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int RetornaQtdRecusado(Hcrp.Framework.Classes.UsuarioRevisorRevista u)
        {
            int retorno = 0;
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COUNT(*) QTD FROM revista_artigo_revisao A " + Environment.NewLine);
                    sb.Append(" WHERE A.NUM_USER_REVISAO = :NUM_USER_REVISAO " + Environment.NewLine);
                    sb.Append(" AND A.IDF_ACEITACAO = 0 " + Environment.NewLine);
                    sb.Append("   AND A.DTA_CONVITE > TO_DATE('01/03/2012', 'DD/MM/YYYY') " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["NUM_USER_REVISAO"] = u.NumUserBanco;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        retorno = Convert.ToInt32(dr["QTD"]);
                    }
                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int RetornaQtdEnviado(Hcrp.Framework.Classes.UsuarioRevisorRevista u)
        {
            int retorno = 0;
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COUNT(*) QTD FROM revista_artigo_revisao A " + Environment.NewLine);
                    sb.Append(" WHERE A.NUM_USER_REVISAO = :NUM_USER_REVISAO " + Environment.NewLine);
                    sb.Append("   AND A.DTA_CONVITE > TO_DATE('01/03/2012', 'DD/MM/YYYY') " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["NUM_USER_REVISAO"] = u.NumUserBanco;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        retorno = Convert.ToInt32(dr["QTD"]);
                    }
                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Hcrp.Framework.Classes.UsuarioRevisorRevista> ListarUsuariosRevisores(int codRevista)
        {
            List<Hcrp.Framework.Classes.UsuarioRevisorRevista> lu = new List<Hcrp.Framework.Classes.UsuarioRevisorRevista>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT U.NUM_USER_BANCO, U.NOM_USUARIO, U.SBN_USUARIO, U.NOM_USUARIO_BANCO" + Environment.NewLine);
                    sb.Append(" FROM USUARIO U, USUARIO_ROLE UR" + Environment.NewLine);
                    sb.Append(" WHERE U.NUM_USER_BANCO = UR.NUM_USER_BANCO" + Environment.NewLine);
                    sb.Append(" AND UR.COD_ROLE = " + Convert.ToString((int)Hcrp.Framework.Classes.UsuarioConexao.EPerfilRevista.PerfilRevisor)  + Environment.NewLine);
                    sb.Append(" AND UR.COD_INST_SISTEMA = 1 " + Environment.NewLine);
                    sb.Append(" ORDER BY U.NOM_USUARIO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.UsuarioRevisorRevista Usuario = new Hcrp.Framework.Classes.UsuarioRevisorRevista();
                        Usuario.NumUserBanco = Convert.ToInt32(dr["NUM_USER_BANCO"]);
                        Usuario.NomeAcesso = Convert.ToString(dr["NOM_USUARIO_BANCO"]);
                        Usuario.Nome = Convert.ToString(dr["NOM_USUARIO"]);
                        Usuario.SobreNome = Convert.ToString(dr["SBN_USUARIO"]);
                        lu.Add(Usuario);
                    }
                }
                return lu;
            }
            catch (Exception)
            {
                return lu;
            }
        }

        public string BuscaStatusJuntoArtigo(int CodArtigo, Hcrp.Framework.Classes.UsuarioRevisorRevista u)
        {
            string retorno = "";
            List<Hcrp.Framework.Classes.UsuarioRevisorRevista> lu = new List<Hcrp.Framework.Classes.UsuarioRevisorRevista>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(@" SELECT CASE WHEN A.IDF_ACEITACAO = 0 THEN 'http://10.165.5.50/InterfaceHC/imagens/X.jpg' ");
                    sb.Append(@" WHEN A.IDF_ACEITACAO = 1 THEN 'http://10.165.5.50/InterfaceHC/imagens/Liberado.gif' ");
                    sb.Append(@" ELSE 'http://10.165.5.50/InterfaceHC/imagens/interrogacao.gif' END SITUACAO ");
                    sb.Append(@" FROM REVISTA_ARTIGO_REVISAO A ");
                    sb.Append(@" WHERE A.SEQ_REVISTA_ARTIGO = :SEQ_REVISTA_ARTIGO ");
                    sb.Append(@" AND A.NUM_USER_REVISAO = :NUM_USER_REVISAO ");

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_REVISTA_ARTIGO"] = CodArtigo;
                    query.Params["NUM_USER_REVISAO"] = u.NumUserBanco;
                    
                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;                   

                    while (dr.Read())
                    {
                        retorno = Convert.ToString(dr["SITUACAO"]);
                    }
                }
                return retorno;
            }
            catch (Exception)
            {
                return retorno;
            }
        }

    }
}
