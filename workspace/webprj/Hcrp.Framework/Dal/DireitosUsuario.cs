using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class DireitosUsuario : Hcrp.Framework.Classes.DireitosUsuario
    {
        public List<Hcrp.Framework.Classes.DireitosUsuario> BuscarModulos(int sistema, Hcrp.Framework.Classes.UsuarioConexao usuario)
        {
            List<Hcrp.Framework.Classes.DireitosUsuario> l = new List<Hcrp.Framework.Classes.DireitosUsuario>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT D.NOM_PROGRAMA, D.DSC_PROGRAMA " + Environment.NewLine);
                    sb.Append("  FROM USUARIO_ROLE A, ROLE B, ROLE_PROGRAMA C, PROGRAMA D " + Environment.NewLine);
                    sb.Append(" WHERE A.NUM_USER_BANCO = " + Convert.ToString(usuario.NumUserBanco) + Environment.NewLine);
                    sb.Append("   AND B.COD_ROLE = A.COD_ROLE " + Environment.NewLine);
                    sb.Append("   AND B.COD_SISTEMA = " + Convert.ToString(sistema) + Environment.NewLine);
                    sb.Append("   AND A.COD_INST_SISTEMA = " + Convert.ToString(new Framework.Classes.ConfiguracaoSistema().CodInstituicaoSistema) + Environment.NewLine);
                    sb.Append("   AND C.COD_ROLE = B.COD_ROLE " + Environment.NewLine);
                    sb.Append("   AND D.COD_PROGRAMA = C.COD_PROGRAMA " + Environment.NewLine);
                    sb.Append("   AND D.IDF_ATIVO = 'A' " + Environment.NewLine);
                    sb.Append(" UNION ALL " + Environment.NewLine);
                    sb.Append(" SELECT B.NOM_PROGRAMA, B.DSC_PROGRAMA " + Environment.NewLine);
                    sb.Append("  FROM DIREITOS_EXTRA_USUARIO A, PROGRAMA B " + Environment.NewLine);
                    sb.Append(" WHERE A.NUM_USER_BANCO = " + Convert.ToString(usuario.NumUserBanco) + Environment.NewLine);
                    sb.Append("   AND A.COD_SISTEMA = " + Convert.ToString(sistema) + Environment.NewLine);
                    sb.Append("   AND B.COD_PROGRAMA = A.COD_PROGRAMA " + Environment.NewLine);
                    sb.Append("   AND B.IDF_ATIVO = 'A' " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.DireitosUsuario du = new Hcrp.Framework.Classes.DireitosUsuario();
                        du.Modulo = Convert.ToString(dr["NOM_PROGRAMA"]);
                        du.Descricao = Convert.ToString(dr["DSC_PROGRAMA"]);
                        l.Add(du);
                    }

                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Hcrp.Framework.Classes.DireitosUsuario> BuscarPerfilPrograma(int sistema, Hcrp.Framework.Classes.UsuarioConexao usuario)
        {
            List<Hcrp.Framework.Classes.DireitosUsuario> l = new List<Hcrp.Framework.Classes.DireitosUsuario>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    /*
                    SELECT A.COD_PROGRAMA, A.COD_PROGRAMA_PAI, B.NOM_PROGRAMA, B.DSC_PROGRAMA
                      FROM SISTEMA_PROGRAMA A, PROGRAMA B, ROLE_PROGRAMA C, USUARIO_ROLE D
                    WHERE A.COD_SISTEMA = 18
                      AND D.COD_INST_SISTEMA = 1
                      AND D.NUM_USER_BANCO = 1306
                      AND B.COD_PROGRAMA = A.COD_PROGRAMA
                      AND C.COD_PROGRAMA = B.COD_PROGRAMA
                      AND D.COD_ROLE = C.COD_ROLE
                     */


                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT D.COD_PROGRAMA, D.NOM_PROGRAMA, D.DSC_PROGRAMA " + Environment.NewLine);
                    sb.Append("  FROM USUARIO_ROLE A, SISTEMA_PERFIL E,PERFIL B, ROLE_PROGRAMA C, PROGRAMA D " + Environment.NewLine);
                    sb.Append(" WHERE A.NUM_USER_BANCO = :NUM_USER_BANCO" + Convert.ToString(usuario.NumUserBanco) + Environment.NewLine);
                    sb.Append("   AND B.COD_ROLE = A.COD_ROLE " + Environment.NewLine);
                    sb.Append("   AND E.COD_SISTEMA = :COD_SISTEMA" + Convert.ToString(sistema) + Environment.NewLine);
                    sb.Append("   AND E.COD_ROLE = B.COD_ROLE " + Convert.ToString(sistema) + Environment.NewLine);
                    sb.Append("   AND A.COD_INST_SISTEMA = :COD_INST_SISTEMA " + Convert.ToString(new Framework.Classes.ConfiguracaoSistema().CodInstituicaoSistema) + Environment.NewLine);
                    sb.Append("   AND C.COD_ROLE = B.COD_ROLE " + Environment.NewLine);
                    sb.Append("   AND D.COD_PROGRAMA = C.COD_PROGRAMA " + Environment.NewLine);
                    sb.Append("   AND D.IDF_ATIVO = 'A' " + Environment.NewLine);
                    sb.Append(" UNION ALL " + Environment.NewLine);
                    sb.Append(" SELECT B.COD_PROGRAMA, B.NOM_PROGRAMA, B.DSC_PROGRAMA " + Environment.NewLine);
                    sb.Append("  FROM DIREITOS_EXTRA_USUARIO A, PROGRAMA B " + Environment.NewLine);
                    sb.Append(" WHERE A.NUM_USER_BANCO = :NUM_USER_BANCO " + Convert.ToString(usuario.NumUserBanco) + Environment.NewLine);
                    sb.Append("   AND A.COD_SISTEMA = :COD_SISTEMA " + Convert.ToString(sistema) + Environment.NewLine);
                    sb.Append("   AND B.COD_PROGRAMA = A.COD_PROGRAMA " + Environment.NewLine);
                    sb.Append("   AND B.IDF_ATIVO = 'A' " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.DireitosUsuario du = new Hcrp.Framework.Classes.DireitosUsuario();
                        du.Codigo = Convert.ToInt32(dr["NOM_PROGRAMA"]);
                        du.Modulo = Convert.ToString(dr["NOM_PROGRAMA"]);
                        du.Descricao = Convert.ToString(dr["DSC_PROGRAMA"]);
                        l.Add(du);
                    }

                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Hcrp.Framework.Classes.UsuarioConexao.EPerfilRevista> BuscarRoles(int sistema, Hcrp.Framework.Classes.UsuarioConexao usuario)
        {
            List<Hcrp.Framework.Classes.UsuarioConexao.EPerfilRevista> l = new List<Hcrp.Framework.Classes.UsuarioConexao.EPerfilRevista>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT B.COD_ROLE " + Environment.NewLine);
                    sb.Append("  FROM USUARIO_ROLE A, ROLE B " + Environment.NewLine);
                    sb.Append(" WHERE A.NUM_USER_BANCO = " + Convert.ToString(usuario.NumUserBanco) + Environment.NewLine);
                    sb.Append("   AND A.COD_ROLE = B.COD_ROLE " + Environment.NewLine);
                    sb.Append("   AND B.COD_SISTEMA = " + Convert.ToString(sistema) + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        l.Add((Hcrp.Framework.Classes.UsuarioConexao.EPerfilRevista)Convert.ToInt32(dr["COD_ROLE"]));
                    }

                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string BuscarNomeSobrenome(Hcrp.Framework.Classes.UsuarioConexao usuario)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    string _nomeSobrenome = "";
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT NUM_USER_BANCO, NOM_USUARIO || ' ' || SBN_USUARIO NOME_COMPLETO " + Environment.NewLine);
                    sb.Append(" FROM USUARIO " + Environment.NewLine);
                    sb.Append(" WHERE NUM_USER_BANCO = :NUM_USER_BANCO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["NUM_USER_BANCO"] = usuario.NumUserBanco;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        _nomeSobrenome = Convert.ToString(dr["NOME_COMPLETO"]);
                    }

                    return _nomeSobrenome;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool AdministraSite(Hcrp.Framework.Classes.UsuarioConexao usuario, int cod_site)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT * FROM SITE_ADMINISTRADOR A " + Environment.NewLine);
                    sb.Append(" WHERE A.NUM_USER_BANCO = :NUM_USER_BANCO " + Environment.NewLine);
                    sb.Append(" AND A.COD_SITE = :COD_SITE " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["NUM_USER_BANCO"] = usuario.NumUserBanco;
                    query.Params["COD_SITE"] = cod_site;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    return dr.HasRows;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string BuscarEmail(Hcrp.Framework.Classes.UsuarioConexao usuario)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    string _email = "";
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT DSC_EMAIL " + Environment.NewLine);
                    sb.Append(" FROM COMPLEMENTO_USUARIO " + Environment.NewLine);
                    sb.Append(" WHERE NUM_USER_BANCO = :NUM_USER_BANCO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["NUM_USER_BANCO"] = usuario.NumUserBanco;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        _email = Convert.ToString(dr["DSC_EMAIL"]);
                    }

                    return _email;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SalvarEmail(Hcrp.Framework.Classes.UsuarioConexao usuario, string email, Int64 numUserBanco = 0)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    bool possuiComplemento = false;
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COUNT(*) QTD_COMPL " + Environment.NewLine);
                    sb.Append(" FROM COMPLEMENTO_USUARIO " + Environment.NewLine);
                    sb.Append(" WHERE NUM_USER_BANCO = :NUM_USER_BANCO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    var numUser = numUserBanco > 0 ? numUserBanco : usuario.NumUserBanco;

                    query.Params["NUM_USER_BANCO"] = numUser;

                    ctx.ExecuteQuery(query);

                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        possuiComplemento = Convert.ToInt32(dr["QTD_COMPL"]) == 1 ? true : false;
                    }

                    if (possuiComplemento)
                    {
                        Hcrp.Infra.AcessoDado.UpdateCommandConfig update = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("COMPLEMENTO_USUARIO");

                        update.Params["DSC_EMAIL"] = email;
                        update.FilterParams["NUM_USER_BANCO"] = numUser;

                        ctx.ExecuteUpdate(update);
                    }
                    else
                    {
                        Hcrp.Infra.AcessoDado.CommandConfig insert = new Hcrp.Infra.AcessoDado.CommandConfig("COMPLEMENTO_USUARIO");

                        insert.Params["NUM_USER_BANCO"] = numUser;
                        insert.Params["DSC_EMAIL"] = email;

                        ctx.ExecuteInsert(insert);
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string BuscarCpf(Classes.UsuarioConexao usuario)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    string _cpf = "";
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT NUM_DOCUMENTO " + Environment.NewLine);
                    sb.Append(" FROM USUARIO " + Environment.NewLine);
                    sb.Append(" WHERE NUM_USER_BANCO = :NUM_USER_BANCO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["NUM_USER_BANCO"] = usuario.NumUserBanco;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        _cpf = Convert.ToString(dr["NUM_DOCUMENTO"]);
                    }

                    return _cpf;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string BuscarUltimaArea(Classes.UsuarioConexao usuario)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    string _area = "";
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT A.DSC_AREA " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_AUTOR_ARTIGO A " + Environment.NewLine);
                    sb.Append(" WHERE A.NUM_DOC_AUTOR = :NUM_DOC_AUTOR " + Environment.NewLine);
                    sb.Append(" AND A.SEQ_REVISTA_ARTIGO = " + Environment.NewLine);
                    sb.Append("     (SELECT MAX(B.SEQ_REVISTA_ARTIGO) " + Environment.NewLine);
                    sb.Append("      FROM REVISTA_AUTOR_ARTIGO B " + Environment.NewLine);
                    sb.Append("      WHERE B.NUM_DOC_AUTOR = :NUM_DOC_AUTOR) " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["NUM_DOC_AUTOR"] = usuario.Cpf;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        _area = Convert.ToString(dr["DSC_AREA"]);
                    }

                    return _area;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
