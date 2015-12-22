using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Web;

namespace Hcrp.Framework.Dal
{
    public class Usuario : Hcrp.Framework.Classes.Usuario
    {
        public void PreencherUsuario(Hcrp.Framework.Classes.Usuario u)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT NUM_USER_BANCO, NOM_USUARIO, SBN_USUARIO " + Environment.NewLine);
                    sb.Append(" FROM USUARIO " + Environment.NewLine);
                    sb.Append(" WHERE NUM_USER_BANCO = :NUM_USER_BANCO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["NUM_USER_BANCO"] = u.NumUserBanco;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        u.Nome = Convert.ToString(dr["NOM_USUARIO"]);
                        u.SobreNome = Convert.ToString(dr["SBN_USUARIO"]);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Usuario BuscarUsuarioCodigo(Int32 numUserBanco)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT NUM_USER_BANCO, NOM_USUARIO, SBN_USUARIO, NUM_DOCUMENTO, IDF_TIPO_DOCUMENTO, IDF_TIPO_USUARIO " + Environment.NewLine);
                    sb.Append(" FROM USUARIO " + Environment.NewLine);
                    sb.Append(" WHERE NUM_USER_BANCO = :NUM_USER_BANCO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["NUM_USER_BANCO"] = numUserBanco;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        if (dr["NUM_USER_BANCO"] != DBNull.Value)
                            this.NumUserBanco = Convert.ToInt32(dr["NUM_USER_BANCO"]);

                        if (dr["NOM_USUARIO"] != DBNull.Value)
                        this.Nome = Convert.ToString(dr["NOM_USUARIO"]);

                        if (dr["SBN_USUARIO"] != DBNull.Value)
                            this.SobreNome = Convert.ToString(dr["SBN_USUARIO"]);

                        if (dr["NUM_DOCUMENTO"] != DBNull.Value)
                            this.Documento = Convert.ToString(dr["NUM_DOCUMENTO"]);

                        if (dr["IDF_TIPO_DOCUMENTO"] != DBNull.Value)
                            this.TipoDocumento = (ETipoDocumento)Convert.ToInt32(dr["IDF_TIPO_DOCUMENTO"]);

                        if (dr["IDF_TIPO_USUARIO"] != DBNull.Value)
                            this.IdfTipoUsuario = Convert.ToInt16(dr["IDF_TIPO_USUARIO"]);
                    }
                }
                return this;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int BuscarUsuarioLogin(string login)
        {
            int retorno = -1;
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT NUM_USER_BANCO, NOM_USUARIO, SBN_USUARIO, NOM_USUARIO||' '||SBN_USUARIO NOME_COMPLETO " + Environment.NewLine);
                    sb.Append(" FROM USUARIO " + Environment.NewLine);
                    sb.Append(" WHERE NOM_USUARIO_BANCO = :NOM_USUARIO_BANCO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["NOM_USUARIO_BANCO"] = login.ToUpper();

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    
                    while (dr.Read())
                    {
                        HttpContext.Current.Session["NomeCompleto"] = Convert.ToString(dr["NOME_COMPLETO"]);
                        retorno = Convert.ToInt32(dr["NUM_USER_BANCO"]);  
                    }
                }
                return retorno;
            }
            catch (Exception)
            {
                return retorno;
            }
        }

        public static void InstanciarSessoesDoUsuario(string numUserBanco)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT NUM_USER_BANCO, NOM_USUARIO||' '||SBN_USUARIO NOME_COMPLETO, U.NUM_DOCUMENTO " + Environment.NewLine);
                    sb.Append(" FROM USUARIO U " + Environment.NewLine);
                    sb.Append(" WHERE U.NUM_USER_BANCO = :NUM_USER_BANCO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["NUM_USER_BANCO"] = numUserBanco;

                    ctx.ExecuteQuery(query);

                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        HttpContext.Current.Session["NomeCompleto"] = Infra.Util.Encryption.EncryptText(Convert.ToString(dr["NOME_COMPLETO"]));
                        HttpContext.Current.Session["CPF"] = Infra.Util.Encryption.EncryptText(Convert.ToString(dr["NUM_DOCUMENTO"]));
                    }
                }
            }
            catch (Exception)
            {
                HttpContext.Current.Session["NomeCompleto"] = "";
                HttpContext.Current.Session["CPF"] = "";
            }
        }

        public List<Hcrp.Framework.Classes.Usuario> ListarUsuarios(string login, string Nome, string Sobrenome)
        {
            List<Hcrp.Framework.Classes.Usuario> lu = new List<Hcrp.Framework.Classes.Usuario>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT U.NUM_USER_BANCO, U.NOM_USUARIO, U.SBN_USUARIO, U.NOM_USUARIO_BANCO" + Environment.NewLine);
                    sb.Append(" FROM USUARIO U" + Environment.NewLine);
                    sb.Append(" WHERE U.NOM_USUARIO LIKE UPPER('%" + Nome + "%')" + Environment.NewLine);
                    sb.Append(" AND U.SBN_USUARIO LIKE UPPER('%"+ Sobrenome +"%')" + Environment.NewLine);
                    sb.Append(" AND U.NOM_USUARIO_BANCO LIKE UPPER('%"+ login +"%')" + Environment.NewLine);
                    sb.Append(" ORDER BY U.NOM_USUARIO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.Usuario Usuario = new Hcrp.Framework.Classes.Usuario();
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

        public List<Hcrp.Framework.Classes.Usuario> ListarUsuariosDaRole(int CodRole, int CodInstSistema)
        {
            List<Hcrp.Framework.Classes.Usuario> lu = new List<Hcrp.Framework.Classes.Usuario>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT U.NUM_USER_BANCO, U.NOM_USUARIO, U.SBN_USUARIO, U.NOM_USUARIO_BANCO" + Environment.NewLine);
                    sb.Append(" FROM USUARIO U, USUARIO_ROLE UR" + Environment.NewLine);
                    sb.Append(" WHERE U.NUM_USER_BANCO = UR.NUM_USER_BANCO" + Environment.NewLine);
                    sb.Append(" AND UR.COD_ROLE = " + Convert.ToString(CodRole) + Environment.NewLine);
                    sb.Append(" AND UR.COD_INST_SISTEMA = " + Convert.ToString(CodInstSistema) + Environment.NewLine);
                    sb.Append(" ORDER BY U.NOM_USUARIO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;



                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.Usuario Usuario = new Hcrp.Framework.Classes.Usuario();
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

        public int SetDireitos(int NumUserBanco, int CodRole, int CodInstSistema)
        {
            
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("USUARIO_ROLE");
                    comando.Params["COD_ROLE"] = CodRole;
                    comando.Params["NUM_USER_BANCO"] = NumUserBanco;
                    comando.Params["COD_INST_SISTEMA"] = CodInstSistema;

                    // Executar o insert
                    ctx.ExecuteInsert(comando);
                    int retorno = 1;
                    return retorno;

                }
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

        public int ExcluirDireitos(int NumUserBanco, int CodRole, int CodInstSistema)
        {

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("USUARIO_ROLE");
                    comando.Params["COD_ROLE"] = CodRole;
                    comando.Params["NUM_USER_BANCO"] = NumUserBanco;
                    comando.Params["COD_INST_SISTEMA"] = CodInstSistema;

                    // Executar o insert
                    ctx.ExecuteDelete(comando);
                    int retorno = 1;
                    return retorno;

                }
            }
            catch (Exception)
            {
                return 0;
            }

        }

    }
}
