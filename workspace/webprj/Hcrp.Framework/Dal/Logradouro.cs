using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class Logradouro : Hcrp.Framework.Classes.Logradouro
    {
        public List<Hcrp.Framework.Classes.Logradouro> BuscaLogradouro()
        {
            List<Hcrp.Framework.Classes.Logradouro> l = new List<Hcrp.Framework.Classes.Logradouro>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COD_TIPO_LOGRADOURO, ABV_TIPO_LOGRADOURO, NOM_TIPO_LOGRADOURO " + Environment.NewLine);
                    sb.Append(" FROM TIPO_LOGRADOURO " + Environment.NewLine);
                    sb.Append(" ORDER BY nOM_TIPO_LOGRADOURO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.Logradouro lo = new Hcrp.Framework.Classes.Logradouro();
                        lo.Codigo = Convert.ToInt32(dr["COD_TIPO_LOGRADOURO"]);
                        lo.Sigla = Convert.ToString(dr["ABV_TIPO_LOGRADOURO"]);
                        lo.Nome = Convert.ToString(dr["NOM_TIPO_LOGRADOURO"]);
                        l.Add(lo);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }        
        }
        public Hcrp.Framework.Classes.Logradouro BuscaLogradouroCodigo(string codigo)
        {
            Hcrp.Framework.Classes.Logradouro lo = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COD_TIPO_LOGRADOURO, ABV_TIPO_LOGRADOURO, NOM_TIPO_LOGRADOURO " + Environment.NewLine);
                    sb.Append(" FROM TIPO_LOGRADOURO " + Environment.NewLine);
                    sb.Append(" WHERE COD_TIPO_LOGRADOURO = :COD_TIPO_LOGRADOURO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_TIPO_LOGRADOURO"] = codigo.PadLeft(3,'0');

                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        lo = new Classes.Logradouro();

                        if (dr["COD_TIPO_LOGRADOURO"] != DBNull.Value)
                            lo.Codigo = Convert.ToInt32(dr["COD_TIPO_LOGRADOURO"]);

                        if (dr["ABV_TIPO_LOGRADOURO"] != DBNull.Value)
                            lo.Sigla = Convert.ToString(dr["ABV_TIPO_LOGRADOURO"]);

                        if (dr["NOM_TIPO_LOGRADOURO"] != DBNull.Value)
                            lo.Nome = Convert.ToString(dr["NOM_TIPO_LOGRADOURO"]);

                        break;
                    }
                }
                return lo;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Hcrp.Framework.Classes.Logradouro BuscaLogradouroCep(string cep)
        {
            Hcrp.Framework.Classes.Logradouro l = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT L.COD_LOGRADOURO, L.COD_LOCALIDADE, T.COD_TIPO_LOGRADOURO, L.NOM_LOGRADOURO NOM_LOGRADOURO, B.NOM_BAIRRO, LOC.NOM_LOCALIDADE, LOC.SGL_UF " + Environment.NewLine);
                    sb.Append(" FROM LOGRADOURO L, TIPO_LOGRADOURO T, BAIRRO B, LOCALIDADE LOC " + Environment.NewLine);
                    sb.Append(" WHERE L.COD_TIPO = T.COD_TIPO_LOGRADOURO");
                    sb.Append("   AND L.COD_BAIRRO_INICIAL = B.COD_BAIRRO");
                    sb.Append("   AND L.COD_LOCALIDADE = B.COD_LOCALIDADE");
                    sb.Append("   AND L.SGL_UF = B.SGL_UF");
                    sb.Append("   AND L.SGL_PAIS = B.SGL_PAIS");
                    sb.Append("   AND L.COD_LOCALIDADE = LOC.COD_LOCALIDADE");
                    sb.Append("   AND L.SGL_UF = LOC.SGL_UF");
                    sb.Append("   AND L.SGL_PAIS = LOC.SGL_PAIS");
                    sb.Append("   AND L.CEP_LOGRADOURO = :CEP_LOGRADOURO");
                    sb.Append(" ORDER BY NOM_LOGRADOURO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query.Params["CEP_LOGRADOURO"] = cep.ToString();

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.Logradouro lo = new Hcrp.Framework.Classes.Logradouro();
                        lo.CodigoLogra = Convert.ToInt32(dr["COD_LOGRADOURO"]);
                        lo.CodigoTipoLogradouro = Convert.ToInt32(dr["COD_TIPO_LOGRADOURO"]);
                        lo.NomeLogra = Convert.ToString(dr["NOM_LOGRADOURO"]);
                        lo.BairroLogra = Convert.ToString(dr["NOM_BAIRRO"]);
                        lo.CodigoCidadeLogra = Convert.ToString(dr["COD_LOCALIDADE"]);
                        lo.CidadeLogra = Convert.ToString(dr["NOM_LOCALIDADE"]);
                        lo.EstadoLogra = Convert.ToString(dr["SGL_UF"]);
                        l = lo;
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Buscar dados do logradouro por número de CEP para o cadastro de paciente externo.
        /// </summary>       
        public Hcrp.Framework.Classes.Logradouro BuscaLogradouroParaOCadastroDePacienteExternoPorCEP(string cep)
        {
            Hcrp.Framework.Classes.Logradouro _logradouro = null;
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(" SELECT TL.ABV_TIPO_LOGRADOURO, ");
                    sb.AppendLine("        LG.NOM_LOGRADOURO, ");
                    sb.AppendLine("        B.NOM_BAIRRO, ");
                    sb.AppendLine("        L.NOM_LOCALIDADE, ");
                    sb.AppendLine("        L.SGL_UF, ");
                    sb.AppendLine("        L.SGL_PAIS, ");
                    sb.AppendLine("        L.COD_LOCALIDADE, ");
                    sb.AppendLine("        TL.COD_TIPO_LOGRADOURO, ");
                    sb.AppendLine("        LG.CPL_LOGRADOURO ");
                    sb.AppendLine("   FROM LOGRADOURO LG, ");
                    sb.AppendLine("        TIPO_LOGRADOURO TL, ");
                    sb.AppendLine("        LOCALIDADE L, ");
                    sb.AppendLine("        BAIRRO B ");
                    sb.AppendLine(" WHERE LG.CEP_LOGRADOURO=:CEP_LOGRADOURO "); // CONFORME CEP INFORMADO
                    sb.AppendLine("       AND LG.COD_TIPO=TL.COD_TIPO_LOGRADOURO ");
                    sb.AppendLine("       AND LG.SGL_PAIS=L.SGL_PAIS ");
                    sb.AppendLine("       AND LG.SGL_UF=L.SGL_UF ");
                    sb.AppendLine("       AND LG.COD_LOCALIDADE=L.COD_LOCALIDADE ");
                    sb.AppendLine("       AND LG.COD_BAIRRO_INICIAL=B.COD_BAIRRO(+) ");
                    sb.AppendLine("       AND LG.SGL_PAIS=B.SGL_PAIS(+) ");
                    sb.AppendLine("       AND LG.SGL_UF=B.SGL_UF(+) ");
                    sb.AppendLine("       AND LG.COD_LOCALIDADE=B.COD_LOCALIDADE ");

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query.Params["CEP_LOGRADOURO"] = cep;

                    // Executar a query
                    ctx.ExecuteQuery(query);

                    while (ctx.Reader.Read())
                    {
                        _logradouro = new Classes.Logradouro();

                        if (ctx.Reader["COD_TIPO_LOGRADOURO"] != DBNull.Value)
                            _logradouro.Codigo =  Convert.ToInt32(ctx.Reader["COD_TIPO_LOGRADOURO"]);

                        if (ctx.Reader["SGL_UF"] != DBNull.Value)
                            _logradouro.EstadoLogra = ctx.Reader["SGL_UF"].ToString();

                        if (ctx.Reader["SGL_PAIS"] != DBNull.Value)
                            _logradouro.PaisLogra = ctx.Reader["SGL_PAIS"].ToString();

                        if (ctx.Reader["COD_LOCALIDADE"] != DBNull.Value)
                            _logradouro.CodigoCidadeLogra = ctx.Reader["COD_LOCALIDADE"].ToString();

                        if (ctx.Reader["NOM_BAIRRO"] != DBNull.Value)
                            _logradouro.BairroLogra = ctx.Reader["NOM_BAIRRO"].ToString();

                        if (ctx.Reader["NOM_LOGRADOURO"] != DBNull.Value)
                            _logradouro.NomeLogra = ctx.Reader["NOM_LOGRADOURO"].ToString();

                        if (ctx.Reader["CPL_LOGRADOURO"] != DBNull.Value)
                            _logradouro.ComplementoLogra = ctx.Reader["CPL_LOGRADOURO"].ToString();                        

                        break;
                    }
                }
                
            }
            catch (Exception)
            {
                throw;
            }

            return _logradouro;
        }
    }
}
