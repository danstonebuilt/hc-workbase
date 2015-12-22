using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class Usuario
    {
        #region Métodos

        /// <summary>
        /// Obter assinatura do usuário conectado.
        /// </summary>        
        public DataView ObterAssinaturaDoUsuarioConectado()
        {
            try
            {
                StringBuilder str = new StringBuilder();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    QueryCommandConfig query;

                    // Abrir conexão
                    ctx.Open();

                    // Query Principal
                    str.AppendLine(" SELECT NOM_USUARIO||' '||SBN_USUARIO AS DSC_ASSINATURA ");
                    str.AppendLine(" FROM USUARIO ");
                    str.AppendLine(" WHERE NUM_USER_BANCO = FC_NUM_USER_BANCO ");

                    query = new QueryCommandConfig(str.ToString());

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt.Load(ctx.Reader);

                    return dt.DefaultView;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        /// <summary>
        /// Obter as roles do usuário logado.
        /// </summary>        
        public List<Entity.RoleUsuario> ObterAsRolesDoUsuarioLogado(int codInstituicao, int codSistema)
        {
            List<Entity.RoleUsuario> _listaRetorno = new List<Entity.RoleUsuario>();
            Entity.RoleUsuario roleUsuario = null;

            try
            {  
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT R.COD_ROLE, R.NOM_ROLE ");
                    str.AppendLine(" FROM USUARIO_ROLE UR, ROLE R ");
                    str.AppendLine(" WHERE UR.NUM_USER_BANCO = FC_NUM_USER_BANCO ");
                    str.AppendLine(" AND UR.COD_ROLE=R.COD_ROLE ");
                    str.AppendLine(" AND R.COD_SISTEMA = :COD_SISTEMA ");
                    str.AppendLine(" AND UR.COD_INST_SISTEMA = :COD_INST_SISTEMA ");

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["COD_SISTEMA"] = codSistema;
                    query.Params["COD_INST_SISTEMA"] = codInstituicao;

                    // Abrir conexão
                    ctx.Open();

                    // Executar a query
                    ctx.ExecuteQuery(query);

                    // Preparar o retorno
                    while (ctx.Reader.Read())
                    {
                        roleUsuario = new Entity.RoleUsuario();

                        if (ctx.Reader["COD_ROLE"] != DBNull.Value)
                            roleUsuario.Codigo = Convert.ToInt64(ctx.Reader["COD_ROLE"]);

                        if (ctx.Reader["NOM_ROLE"] != DBNull.Value)
                            roleUsuario.Nome = ctx.Reader["NOM_ROLE"].ToString();

                        _listaRetorno.Add(roleUsuario);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return _listaRetorno;
        }

        /// <summary>
        /// Obter os usuários da role.
        /// </summary>        
        public List<Entity.Usuario> ObterOsUsuarioDaRole(Int64 codRole)
        {
            List<Entity.Usuario> _listaRetorno = new List<Entity.Usuario>();
            Entity.Usuario usuario = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    StringBuilder str = new StringBuilder();

                    str.AppendLine(" SELECT DISTINCT ");
                    str.AppendLine("    U.NUM_USER_BANCO, ");
                    str.AppendLine("    U.NOM_USUARIO, ");
                    str.AppendLine("    U.SBN_USUARIO, ");

                    str.AppendLine("    (SELECT DSC_EMAIL FROM COMPLEMENTO_USUARIO EM ");
                    str.AppendLine("        WHERE EM.NUM_USER_BANCO = U.NUM_USER_BANCO ");
                    str.AppendLine("        AND ROWNUM = 1) AS EMAIL ");

                    str.AppendLine(" FROM USUARIO_ROLE UR, ");
                    str.AppendLine("    ROLE R, ");
                    str.AppendLine("    USUARIO U ");
                    str.AppendLine(" WHERE UR.NUM_USER_BANCO = FC_NUM_USER_BANCO ");
                    str.AppendLine("    AND UR.COD_ROLE = R.COD_ROLE ");
                    str.AppendLine("    AND UR.NUM_USER_BANCO = U.NUM_USER_BANCO ");
                    str.AppendLine(string.Format(" AND R.COD_ROLE = {0} ", codRole));                    

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Abrir conexão
                    ctx.Open();

                    // Executar a query
                    ctx.ExecuteQuery(query);

                    // Preparar o retorno
                    while (ctx.Reader.Read())
                    {
                        usuario = new Entity.Usuario();

                        if (ctx.Reader["NUM_USER_BANCO"] != DBNull.Value)
                            usuario.NumUserBanco = Convert.ToInt32(ctx.Reader["NUM_USER_BANCO"]);

                        if (ctx.Reader["NOM_USUARIO"] != DBNull.Value)
                            usuario.Nome = ctx.Reader["NOM_USUARIO"].ToString();

                        if (ctx.Reader["SBN_USUARIO"] != DBNull.Value)
                            usuario.SobreNome = ctx.Reader["SBN_USUARIO"].ToString();

                        if (ctx.Reader["EMAIL"] != DBNull.Value)
                            usuario.Email = ctx.Reader["EMAIL"].ToString();

                        _listaRetorno.Add(usuario);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return _listaRetorno;
        }

        /// <summary>
        /// Obter os usuários da role.
        /// </summary>        
        public List<Entity.Usuario> ObterOsUsuarioGestoresDoRepositorio(Int64 codRole, Int64 codigoRepositorio)
        {
            List<Entity.Usuario> _listaRetorno = new List<Entity.Usuario>();
            Entity.Usuario usuario = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    StringBuilder str = new StringBuilder();

                    //str.AppendLine(" SELECT DISTINCT ");
                    //str.AppendLine("    U.NUM_USER_BANCO, ");
                    //str.AppendLine("    U.NOM_USUARIO, ");
                    //str.AppendLine("    U.SBN_USUARIO, ");

                    //str.AppendLine("    (SELECT DSC_EMAIL FROM COMPLEMENTO_USUARIO EM ");
                    //str.AppendLine("        WHERE EM.NUM_USER_BANCO = U.NUM_USER_BANCO ");
                    //str.AppendLine("        AND ROWNUM = 1) AS EMAIL ");

                    //str.AppendLine(" FROM USUARIO_ROLE UR, ");
                    //str.AppendLine("    ROLE R, ");
                    //str.AppendLine("    USUARIO U ");
                    //str.AppendLine(" WHERE  ");
                    //str.AppendLine("     UR.COD_ROLE = R.COD_ROLE ");
                    //str.AppendLine("    AND UR.NUM_USER_BANCO = U.NUM_USER_BANCO ");
                    //str.AppendLine(string.Format(" AND R.COD_ROLE = {0} ", codRole));

                    str.AppendLine("   SELECT DISTINCT U.NUM_USER_BANCO, ");
                    str.AppendLine("                U.NOM_USUARIO, ");
                    str.AppendLine("                U.SBN_USUARIO, ");
                    str.AppendLine("                USU_CPL.DSC_EMAIL EMAIL ");
                    str.AppendLine("  FROM USUARIO_ROLE             UR, ");
                    str.AppendLine("       ROLE                     R, ");
                    str.AppendLine("       USUARIO                  U, ");
                    str.AppendLine("       CENTRO_CUSTO_USUARIO     CC_USU, ");
                    str.AppendLine("       REPOSITORIO_CENTRO_CUSTO REPOSIT_CC, ");
                    str.AppendLine("       COMPLEMENTO_USUARIO      USU_CPL ");
                    str.AppendLine(" WHERE UR.COD_ROLE = R.COD_ROLE ");
                    str.AppendLine("   AND UR.NUM_USER_BANCO = U.NUM_USER_BANCO ");
                    str.AppendLine("   AND CC_USU.COD_CENCUSTO = REPOSIT_CC.COD_CENCUSTO ");
                    str.AppendLine("   AND CC_USU.NUM_USER_BANCO = U.NUM_USER_BANCO ");
                    str.AppendLine("   AND USU_CPL.NUM_USER_BANCO = U.NUM_USER_BANCO ");
                    str.AppendLine(string.Format("   AND REPOSIT_CC.SEQ_REPOSITORIO = {0} ",codigoRepositorio));
                    str.AppendLine(string.Format(" AND R.COD_ROLE = {0} ", codRole));

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Abrir conexão
                    ctx.Open();

                    // Executar a query
                    ctx.ExecuteQuery(query);

                    // Preparar o retorno
                    while (ctx.Reader.Read())
                    {
                        usuario = new Entity.Usuario();

                        if (ctx.Reader["NUM_USER_BANCO"] != DBNull.Value)
                            usuario.NumUserBanco = Convert.ToInt32(ctx.Reader["NUM_USER_BANCO"]);

                        if (ctx.Reader["NOM_USUARIO"] != DBNull.Value)
                            usuario.Nome = ctx.Reader["NOM_USUARIO"].ToString();

                        if (ctx.Reader["SBN_USUARIO"] != DBNull.Value)
                            usuario.SobreNome = ctx.Reader["SBN_USUARIO"].ToString();

                        if (ctx.Reader["EMAIL"] != DBNull.Value)
                            usuario.Email = ctx.Reader["EMAIL"].ToString();

                        _listaRetorno.Add(usuario);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }

            return _listaRetorno;
        }

        #endregion
    }
}
