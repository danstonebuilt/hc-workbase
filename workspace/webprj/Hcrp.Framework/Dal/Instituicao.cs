using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Collections.Specialized;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class Instituicao : Framework.Classes.Instituicao
    {
        public List<Hcrp.Framework.Classes.Instituicao> BuscaInstituicaoMunicipio(string chaveMunicipio)
        {
            List<Hcrp.Framework.Classes.Instituicao> l = new List<Hcrp.Framework.Classes.Instituicao>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    string[] Municipio = chaveMunicipio.Split(new char[] { '|' });

                    sb.Append(" SELECT -1 COD_INSTITUICAO, ' ' NOM_INSTITUICAO, ' ' NUM_CNES FROM DUAL " + Environment.NewLine);
                    sb.Append(" UNION ALL " + Environment.NewLine);
                    sb.Append(" SELECT I.COD_INSTITUICAO, I.NOM_INSTITUICAO, I.NUM_CNES " + Environment.NewLine);
                    sb.Append(" FROM INSTITUICAO I, LOCALIDADE L " + Environment.NewLine);
                    sb.Append(" WHERE I.SGL_PAIS = :SGL_PAIS " + Environment.NewLine);
                    sb.Append("   AND I.SGL_UF = :SGL_UF " + Environment.NewLine);
                    sb.Append("   AND I.COD_LOCALIDADE = :COD_LOCALIDADE " + Environment.NewLine);
                    sb.Append("   AND I.IDF_SITUACAO = 'A' " + Environment.NewLine);

                    sb.Append(" AND L.SGL_PAIS = I.SGL_PAIS " + Environment.NewLine);
                    sb.Append(" AND L.SGL_UF = I.SGL_UF " + Environment.NewLine);
                    sb.Append(" AND L.COD_LOCALIDADE = I.COD_LOCALIDADE " + Environment.NewLine);
                    sb.Append(" AND NVL(I.COD_DIR, L.COD_DIR) = L.COD_DIR " + Environment.NewLine);

                    sb.Append(" ORDER BY NOM_INSTITUICAO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query.Params["SGL_PAIS"] = Municipio[0];
                    query.Params["SGL_UF"] = Municipio[1];
                    query.Params["COD_LOCALIDADE"] = Municipio[2];

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.Instituicao I = new Hcrp.Framework.Classes.Instituicao();
                        I.Codigo = Convert.ToInt32(dr["COD_INSTITUICAO"]);
                        I.Nome = Convert.ToString(dr["NOM_INSTITUICAO"]);
                        I.Cnes = Convert.ToString(dr["NUM_CNES"]);
                        l.Add(I);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Hcrp.Framework.Classes.Instituicao BuscaInstituicaoCodigo(string codigo)
        {
            Hcrp.Framework.Classes.Instituicao instituicao = null;
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT COD_INSTITUICAO, NOM_INSTITUICAO, NUM_CNES " + Environment.NewLine);
                    sb.Append(" FROM INSTITUICAO " + Environment.NewLine);
                    sb.Append(" WHERE COD_INSTITUICAO = :COD_INSTITUICAO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query.Params["COD_INSTITUICAO"] = codigo;

                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        instituicao = new Classes.Instituicao();

                        if (dr["COD_INSTITUICAO"] != DBNull.Value)
                            instituicao.Codigo = Convert.ToInt32(dr["COD_INSTITUICAO"]);

                        if (dr["NOM_INSTITUICAO"] != DBNull.Value)
                            instituicao.Nome = Convert.ToString(dr["NOM_INSTITUICAO"]);

                        if (dr["NUM_CNES"] != DBNull.Value)
                            instituicao.Cnes = Convert.ToString(dr["NUM_CNES"]);

                        break;
                    }
                }

                return instituicao;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AtualizarCNES(Hcrp.Framework.Classes.Instituicao i, string cnes)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    ListDictionary Params = new ListDictionary();
                    Params["NUM_CNES"] = cnes;
                    Params["COD_INSTITUICAO"] = i.Codigo;
                    ctx.ExecuteUpdate("UPDATE INSTITUICAO SET NUM_CNES = :NUM_CNES WHERE COD_INSTITUICAO = :COD_INSTITUICAO AND NUM_CNES IS NULL", Params);

                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        
        public List<Hcrp.Framework.Classes.Instituicao> BuscaInstituicaoHC()
        {
            List<Hcrp.Framework.Classes.Instituicao> instituicoes = new List<Hcrp.Framework.Classes.Instituicao>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT DISTINCT IT.COD_INSTITUICAO, IT.NOM_INSTITUICAO " + Environment.NewLine);
                    sb.Append("   FROM INSTITUTO I, INSTITUICAO IT " + Environment.NewLine);
                    sb.Append("  WHERE I.COD_INST_SISTEMA = IT.COD_INSTITUICAO " + Environment.NewLine);
                    sb.Append("    AND IT.IDF_SITUACAO = 'A' " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Classes.Instituicao instituicao = new Classes.Instituicao();

                        if (dr["COD_INSTITUICAO"] != DBNull.Value) { instituicao.Codigo = Convert.ToInt32(dr["COD_INSTITUICAO"]); }
                        if (dr["NOM_INSTITUICAO"] != DBNull.Value) { instituicao.Nome = Convert.ToString(dr["NOM_INSTITUICAO"]); }

                        instituicoes.Add(instituicao);
                    }
                }

                return instituicoes;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Instituicao BuscaInstituicaoEnderecoCompleto(int CodInstituicao)
        {
            Instituicao objInstituicao = new Instituicao();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT INS.SGL_UF,  TL.ABV_TIPO_LOGRADOURO || ' ' || INS.DSC_ENDERECO DSC_ENDERECO, " + Environment.NewLine);
                    sb.Append(" INS.NUM_ENDERECO, INS.DSC_COMPLEMENTO, " + Environment.NewLine);
                    sb.Append(" INS.DSC_CEP, INS.DSC_BAIRRO, INS.NUM_TEL_UNIDADE" + Environment.NewLine);
                    sb.Append(" FROM INSTITUICAO INS, TIPO_LOGRADOURO TL WHERE" + Environment.NewLine);
                    sb.Append(" INS.COD_TIPO_LOGRADOURO = TL.COD_TIPO_LOGRADOURO (+) AND" + Environment.NewLine);
                    sb.AppendFormat(" INS.COD_INSTITUICAO = {0}", CodInstituicao);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        if (dr["SGL_UF"] != DBNull.Value)
                            objInstituicao.UF = Convert.ToString(dr["SGL_UF"]);

                        if (dr["DSC_ENDERECO"] != DBNull.Value)
                            objInstituicao.Endereco = Convert.ToString(dr["DSC_ENDERECO"]);

                        if (dr["NUM_ENDERECO"] != DBNull.Value)
                            objInstituicao.Numero = Convert.ToString(dr["NUM_ENDERECO"]);

                        if (dr["DSC_COMPLEMENTO"] != DBNull.Value)
                            objInstituicao.Complemento = Convert.ToString(dr["DSC_COMPLEMENTO"]);

                        if (dr["DSC_CEP"] != DBNull.Value)
                            objInstituicao.Cep = Convert.ToString(dr["DSC_CEP"]);

                        if (dr["DSC_BAIRRO"] != DBNull.Value)
                            objInstituicao.Bairro = Convert.ToString(dr["DSC_BAIRRO"]);

                        if (dr["NUM_TEL_UNIDADE"] != DBNull.Value)
                            objInstituicao.TelefoneUnidade = Convert.ToString(dr["NUM_TEL_UNIDADE"]);
                    }
                }
                return objInstituicao;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
