using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class UnidadeFederacao : Hcrp.Framework.Classes.UnidadeFederacao
    {
        public Hcrp.Framework.Classes.UnidadeFederacao BuscaUFSigla(string sigla)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT A.SGL_UF, A.NOM_UF, A.COD_IBGE " + Environment.NewLine);
                    sb.Append(" FROM UNIDADE_FEDERACAO A " + Environment.NewLine);
                    sb.Append(" WHERE A.SGL_UF = :SGL_UF " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SGL_UF"] = sigla;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        this.Sigla = Convert.ToString(dr["SGL_UF"]);
                        this.Nome = Convert.ToString(dr["NOM_UF"]);
                        this.CodigoIbge = Convert.ToInt32(dr["COD_IBGE"]);
                    }
                }
                return this;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Hcrp.Framework.Classes.UnidadeFederacao> BuscaUF()
        {
            List<Hcrp.Framework.Classes.UnidadeFederacao> l = new List<Hcrp.Framework.Classes.UnidadeFederacao>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT A.SGL_UF, A.NOM_UF, A.COD_IBGE, DECODE(A.SGL_UF,'SP',0,1) ORDEM " + Environment.NewLine);
                    sb.Append(" FROM UNIDADE_FEDERACAO A " + Environment.NewLine);
                    sb.Append(" WHERE SGL_PAIS = 'BR' " + Environment.NewLine);
                    sb.Append(" ORDER BY DECODE(A.SGL_UF,'SP',0,1), A.NOM_UF " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.UnidadeFederacao u = new Hcrp.Framework.Classes.UnidadeFederacao();
                        u.Sigla = Convert.ToString(dr["SGL_UF"]);
                        u.Nome = Convert.ToString(dr["NOM_UF"]);
                        u.CodigoIbge = Convert.ToInt32(dr["COD_IBGE"]);
                        l.Add(u);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Hcrp.Framework.Classes.UnidadeFederacao> BuscaUFPais(string sigla_pais)
        {
            List<Hcrp.Framework.Classes.UnidadeFederacao> l = new List<Hcrp.Framework.Classes.UnidadeFederacao>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT A.SGL_UF, A.NOM_UF, A.COD_IBGE " + Environment.NewLine);
                    sb.Append(" FROM UNIDADE_FEDERACAO A " + Environment.NewLine);
                    sb.Append(" WHERE A.SGL_PAIS = :SGL_PAIS " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SGL_PAIS"] = sigla_pais;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.UnidadeFederacao u = new Hcrp.Framework.Classes.UnidadeFederacao();
                        u.Sigla = Convert.ToString(dr["SGL_UF"]);
                        u.Nome = Convert.ToString(dr["NOM_UF"]);
                        if (dr["COD_IBGE"] is DBNull) 
                          ;
                        else
                          u.CodigoIbge = Convert.ToInt32(dr["COD_IBGE"]);
                        l.Add(u);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }

     
    }
}
