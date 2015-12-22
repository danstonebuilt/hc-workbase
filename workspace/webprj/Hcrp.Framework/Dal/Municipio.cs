using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class Municipio : Hcrp.Framework.Classes.Municipio
    {
        public List<Hcrp.Framework.Classes.Municipio> BuscaMunicipiosUsuarioConectado()
        {
            List<Hcrp.Framework.Classes.Municipio> l = new List<Hcrp.Framework.Classes.Municipio>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.SGL_PAIS, A.SGL_UF, A.COD_LOCALIDADE, A.NOM_LOCALIDADE, A.COD_DIR, P.DSC_PAIS, UF.NOM_UF, UF.COD_IBGE " + Environment.NewLine);
                    sb.Append(" FROM LOCALIDADE A, USUARIO_LOCALIDADE B, PAIS P, UNIDADE_FEDERACAO UF " + Environment.NewLine);
                    sb.Append(" WHERE B.NUM_USER_BANCO = :NUM_USER_BANCO " + Environment.NewLine);
                    sb.Append(" AND A.SGL_PAIS = B.SGL_PAIS " + Environment.NewLine);
                    sb.Append(" AND A.SGL_UF = B.SGL_UF " + Environment.NewLine);
                    sb.Append(" AND A.COD_LOCALIDADE = B.COD_LOCALIDADE " + Environment.NewLine);
                    sb.Append(" AND P.SGL_PAIS = A.SGL_PAIS " + Environment.NewLine);
                    sb.Append(" AND UF.SGL_UF =+ A.SGL_UF " + Environment.NewLine);
                    sb.Append(" UNION " + Environment.NewLine);
                    sb.Append(" SELECT B.SGL_PAIS, B.SGL_UF, B.COD_LOCALIDADE, B.NOM_LOCALIDADE, B.COD_DIR, P.DSC_PAIS, UF.NOM_UF, UF.COD_IBGE " + Environment.NewLine);
                    sb.Append(" FROM DIR A, LOCALIDADE B, USUARIO_DIR C, PAIS P, UNIDADE_FEDERACAO UF " + Environment.NewLine);
                    sb.Append(" WHERE C.NUM_USER_BANCO = :NUM_USER_BANCO " + Environment.NewLine);
                    sb.Append("   AND C.COD_DIR = B.COD_DIR " + Environment.NewLine);
                    sb.Append("   AND B.COD_DIR = A.COD_DIR " + Environment.NewLine);
                    sb.Append("   AND P.SGL_PAIS = B.SGL_PAIS " + Environment.NewLine);
                    sb.Append("   AND UF.SGL_UF =+ B.SGL_UF " + Environment.NewLine);
                    sb.Append(" ORDER BY NOM_LOCALIDADE  " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    
                    query.Params["NUM_USER_BANCO"] = new Hcrp.Framework.Classes.UsuarioConexao().NumUserBanco;
                    query.Params["NUM_USER_BANCO"] = new Hcrp.Framework.Classes.UsuarioConexao().NumUserBanco;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    Hcrp.Framework.Classes.Municipio municipio;
                    Hcrp.Framework.Classes.Pais pais;
                    Hcrp.Framework.Classes.UnidadeFederacao unidadeFederacao;

                    while (dr.Read())
                    {
                        //Hcrp.Framework.Classes.Municipio M = new Hcrp.Framework.Classes.Municipio();
                        //Hcrp.Framework.Classes.Pais P = new Hcrp.Framework.Classes.Pais().BuscaPaisSigla(Convert.ToString(dr["SGL_PAIS"]));
                        //Hcrp.Framework.Classes.UnidadeFederacao Uf = new Hcrp.Framework.Classes.UnidadeFederacao().BuscaUFSigla(Convert.ToString(dr["SGL_UF"]));
                        //M.Pais = P;
                        //M.UF = Uf;
                        //M.Codigo = Convert.ToString(dr["COD_LOCALIDADE"]);
                        //M.Nome = Convert.ToString(dr["NOM_LOCALIDADE"]);
                        
                        municipio = new Hcrp.Framework.Classes.Municipio();
                        pais = new Classes.Pais();
                        unidadeFederacao = new Classes.UnidadeFederacao();

                        if (dr["SGL_PAIS"] != DBNull.Value)
                            pais.Sigla = dr["SGL_PAIS"].ToString();

                        if (dr["DSC_PAIS"] != DBNull.Value)
                            pais.Nome = dr["DSC_PAIS"].ToString();

                        if (dr["SGL_UF"] != DBNull.Value)
                            unidadeFederacao.Sigla = dr["SGL_UF"].ToString();

                        if (dr["NOM_UF"] != DBNull.Value)
                            unidadeFederacao.Nome = dr["NOM_UF"].ToString();

                        if (dr["COD_IBGE"] != DBNull.Value)
                            unidadeFederacao.CodigoIbge = Convert.ToInt32(dr["COD_IBGE"]);

                        municipio.Pais = pais;

                        municipio.UF = unidadeFederacao;

                        if (dr["COD_LOCALIDADE"] != DBNull.Value)
                            municipio.Codigo = dr["COD_LOCALIDADE"].ToString();

                        if (dr["NOM_LOCALIDADE"] != DBNull.Value)
                            municipio.Nome = Convert.ToString(dr["NOM_LOCALIDADE"]);

                        l.Add(municipio);

                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Hcrp.Framework.Classes.Municipio> BuscaMunicipiosUF(string uf)
        {
            List<Hcrp.Framework.Classes.Municipio> l = new List<Hcrp.Framework.Classes.Municipio>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine(" SELECT A.SGL_PAIS, A.SGL_UF, A.COD_LOCALIDADE, A.NOM_LOCALIDADE, A.COD_DIR, P.DSC_PAIS, UF.NOM_UF, UF.COD_IBGE ");
                    sb.AppendLine(" FROM LOCALIDADE A ");
                    sb.AppendLine(" INNER JOIN PAIS P ON P.SGL_PAIS = A.SGL_PAIS ");
                    sb.AppendLine(" LEFT JOIN UNIDADE_FEDERACAO UF ON UF.SGL_UF = A.SGL_UF ");
                    sb.AppendLine(" WHERE A.SGL_PAIS = 'BR' ");
                    sb.AppendLine(" AND A.SGL_UF = :SGL_UF ");
                    sb.AppendLine(" ORDER BY A.NOM_LOCALIDADE  ");

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query.Params["SGL_UF"] = uf.ToUpper();

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    Hcrp.Framework.Classes.Municipio municipio;
                    Hcrp.Framework.Classes.Pais pais;
                    Hcrp.Framework.Classes.UnidadeFederacao unidadeFederacao;

                    while (dr.Read())
                    {
                        municipio = new Hcrp.Framework.Classes.Municipio();
                        pais = new Classes.Pais();
                        unidadeFederacao = new Classes.UnidadeFederacao();

                        if (dr["SGL_PAIS"] != DBNull.Value)
                            pais.Sigla = dr["SGL_PAIS"].ToString();

                        if (dr["DSC_PAIS"] != DBNull.Value)
                            pais.Nome = dr["DSC_PAIS"].ToString();

                        if (dr["SGL_UF"] != DBNull.Value)
                            unidadeFederacao.Sigla = dr["SGL_UF"].ToString();

                        if (dr["NOM_UF"] != DBNull.Value)
                            unidadeFederacao.Nome = dr["NOM_UF"].ToString();

                        if (dr["COD_IBGE"] != DBNull.Value)
                            unidadeFederacao.CodigoIbge = Convert.ToInt32(dr["COD_IBGE"]);

                        municipio.Pais = pais;

                        unidadeFederacao.Pais = pais;

                        municipio.UF = unidadeFederacao;

                        if (dr["COD_LOCALIDADE"] != DBNull.Value)
                            municipio.Codigo = dr["COD_LOCALIDADE"].ToString();

                        if (dr["NOM_LOCALIDADE"] != DBNull.Value)
                            municipio.Nome = Convert.ToString(dr["NOM_LOCALIDADE"]);

                        l.Add(municipio);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        
        }

        public List<Hcrp.Framework.Classes.Municipio> BuscaMunicipiosDrs(int codDrs)
        {
            List<Hcrp.Framework.Classes.Municipio> l = new List<Hcrp.Framework.Classes.Municipio>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT '' SGL_PAIS, '' SGL_UF, '' COD_LOCALIDADE, ' ' NOM_LOCALIDADE,  -1 COD_DIR FROM DUAL " + Environment.NewLine);
                    sb.Append(" UNION ALL " + Environment.NewLine);
                    sb.Append(" SELECT A.SGL_PAIS, A.SGL_UF, A.COD_LOCALIDADE, A.NOM_LOCALIDADE, A.COD_DIR " + Environment.NewLine);
                    sb.Append(" FROM LOCALIDADE A " + Environment.NewLine);
                    sb.Append(" WHERE A.COD_DIR = :COD_DIR " + Environment.NewLine);
                    sb.Append(" ORDER BY 4  " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query.Params["COD_DIR"] = codDrs;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.Municipio M = new Hcrp.Framework.Classes.Municipio();
                        Hcrp.Framework.Classes.Pais P = new Hcrp.Framework.Classes.Pais().BuscaPaisSigla(Convert.ToString(dr["SGL_PAIS"]));
                        Hcrp.Framework.Classes.UnidadeFederacao Uf = new Hcrp.Framework.Classes.UnidadeFederacao().BuscaUFSigla(Convert.ToString(dr["SGL_UF"]));
                        M.Pais = P;
                        M.UF = Uf;
                        M.Codigo = Convert.ToString(dr["COD_LOCALIDADE"]);
                        M.Nome = Convert.ToString(dr["NOM_LOCALIDADE"]);
                        l.Add(M);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public Hcrp.Framework.Classes.Municipio BuscaMunicipiosChave(string chave)
        {
            Hcrp.Framework.Classes.Municipio municipio = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine(" SELECT A.SGL_PAIS, ");
                    sb.AppendLine("        A.SGL_UF,  ");
                    sb.AppendLine("        A.COD_LOCALIDADE,  ");
                    sb.AppendLine("        A.NOM_LOCALIDADE,  ");
                    sb.AppendLine("        A.COD_DIR, ");
                    sb.AppendLine("        UF.NOM_UF, ");
                    sb.AppendLine("        UF.COD_IBGE, ");
                    sb.AppendLine("        P.DSC_PAIS ");
                    sb.AppendLine(" FROM LOCALIDADE A, PAIS P, UNIDADE_FEDERACAO UF ");
                    sb.AppendLine(" WHERE P.SGL_PAIS = A.SGL_PAIS AND  ");
                    sb.AppendLine("       UF.SGL_UF = A.SGL_UF AND ");
                    sb.AppendLine("       A.SGL_PAIS = :SGL_PAIS AND A.SGL_UF = :SGL_UF AND A.COD_LOCALIDADE = :COD_LOCALIDADE ");
                    
                    //sb.Append(" SELECT A.SGL_PAIS, A.SGL_UF, A.COD_LOCALIDADE, A.NOM_LOCALIDADE, A.COD_DIR " + Environment.NewLine);
                    //sb.Append(" FROM LOCALIDADE A " + Environment.NewLine);
                    //sb.Append(" WHERE A.SGL_PAIS = :SGL_PAIS " + Environment.NewLine);
                    //sb.Append(" AND A.SGL_UF = :SGL_UF " + Environment.NewLine);
                    //sb.Append(" AND A.COD_LOCALIDADE = :COD_LOCALIDADE " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    
                    string[] parametros = chave.Split(new char[] { '|' });

                    if(parametros.Length <3)
                        throw new Exception("Informe corretamente a chave do municipio para busca. PAIS|ESTADO|COD_LOCALIDADE.");

                    query.Params["SGL_PAIS"] = parametros[0];
                    query.Params["SGL_UF"] = parametros[1];
                    query.Params["COD_LOCALIDADE"] = parametros[2];

                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        municipio = new Classes.Municipio();

                        if (dr["SGL_PAIS"] != DBNull.Value && dr["SGL_UF"] != DBNull.Value)
                        {
                            Classes.Pais pais = new Classes.Pais();

                            if (dr["SGL_PAIS"] != DBNull.Value && dr["DSC_PAIS"] != DBNull.Value)
                            {
                                pais.Nome = dr["DSC_PAIS"].ToString();
                                pais.Sigla = dr["SGL_PAIS"].ToString();
                                municipio.Pais = pais;
                            }

                            if (dr["NOM_UF"] != DBNull.Value && dr["COD_IBGE"] != DBNull.Value)
                            {
                                Classes.UnidadeFederacao uf = new Classes.UnidadeFederacao();

                                if(dr["COD_IBGE"] !=DBNull.Value)
                                    uf.CodigoIbge = Convert.ToInt32(dr["COD_IBGE"]);

                                if(dr["NOM_UF"] !=DBNull.Value)
                                    uf.Nome = dr["NOM_UF"].ToString();

                                if (dr["SGL_UF"] != DBNull.Value)
                                    uf.Sigla = dr["SGL_UF"].ToString();

                                uf.Pais = pais;

                                municipio.UF = uf;
                            }

                            if (dr["COD_LOCALIDADE"] != DBNull.Value)
                                municipio.Codigo = Convert.ToString(dr["COD_LOCALIDADE"]);

                            if (dr["NOM_LOCALIDADE"] != DBNull.Value)
                                municipio.Nome = Convert.ToString(dr["NOM_LOCALIDADE"]);

                            break;
                        }
                    }
                }

                return municipio;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Hcrp.Framework.Classes.Municipio BuscaMunicipiosInstituicao(Hcrp.Framework.Classes.Instituicao i)
        {
            Hcrp.Framework.Classes.Municipio M = new Hcrp.Framework.Classes.Municipio();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.SGL_PAIS, A.SGL_UF, A.COD_LOCALIDADE, A.NOM_LOCALIDADE, A.COD_DIR " + Environment.NewLine);
                    sb.Append(" FROM LOCALIDADE A, INSTITUICAO B " + Environment.NewLine);
                    sb.Append(" WHERE B.COD_INSTITUICAO = :COD_INSTITUICAO " + Environment.NewLine);
                    sb.Append("   AND A.SGL_PAIS = B.SGL_PAIS " + Environment.NewLine);
                    sb.Append("   AND A.SGL_UF  = B.SGL_UF " + Environment.NewLine);
                    sb.Append("   AND A.COD_LOCALIDADE = B.COD_LOCALIDADE " + Environment.NewLine);
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_INSTITUICAO"] = i.Codigo;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.Pais P = new Hcrp.Framework.Classes.Pais().BuscaPaisSigla(Convert.ToString(dr["SGL_PAIS"]));
                        Hcrp.Framework.Classes.UnidadeFederacao Uf = new Hcrp.Framework.Classes.UnidadeFederacao().BuscaUFSigla(Convert.ToString(dr["SGL_UF"]));
                        M.Pais = P;
                        M.UF = Uf;
                        M.Codigo = Convert.ToString(dr["COD_LOCALIDADE"]);
                        M.Nome = Convert.ToString(dr["NOM_LOCALIDADE"]);

                        if (dr["COD_DIR"] != DBNull.Value)
                            M.Drs = new Classes.Drs { Codigo = Convert.ToInt32(dr["COD_DIR"]) };
                    }
                }
                return M;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int BuscarCodigoDRSDoMunicipio(string siglaPais, string siglaUF, string codigoLocalidade)
        {
           int codDRS = 0;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine(" SELECT ");
                    sb.AppendLine("        A.COD_DIR ");                   
                    sb.AppendLine(" FROM LOCALIDADE A ");
                    sb.AppendLine(string.Format(" WHERE A.SGL_PAIS = '{0}' ", siglaPais));
                    sb.AppendLine(string.Format("   AND A.SGL_UF = '{0}' ", siglaUF));
                    sb.AppendLine(string.Format("   AND A.COD_LOCALIDADE = '{0}' ", codigoLocalidade));                 

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        if (dr["COD_DIR"] != DBNull.Value)                        
                            codDRS = Convert.ToInt32(dr["COD_DIR"]);                       

                        break;
                    }
                }

                return codDRS;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
