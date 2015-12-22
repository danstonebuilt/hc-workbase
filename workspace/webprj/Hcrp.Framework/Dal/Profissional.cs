using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Dal
{
    public class Profissional : Hcrp.Framework.Classes.Profissional
    {
        public Hcrp.Framework.Classes.Profissional BuscarMedicoCRM(string crm, string uf, string nome, string sobrenome, string cpf)
        {
            Hcrp.Framework.Classes.Profissional p = new Hcrp.Framework.Classes.Profissional();
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                ctx.Open();
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT COD_PROFISSIONAL, NUM_USER_BANCO, ");
                sql.Append(" NOM_PROFISSIONAL, SBN_PROFISSIONAL, CPF_PROFISSIONAL, ");
                sql.Append(" NUM_DOC_PROFISSIONAL ");
                sql.Append(" FROM PROFISSIONAL ");
                sql.Append(" WHERE NUM_PROFISSAO = 1");
                sql.Append("   AND ((DSC_UF_DOC_PROFISSIONAL IS NULL) OR (DSC_UF_DOC_PROFISSIONAL = :DSC_UF_DOC_PROFISSIONAL)) ");
                
                if (!string.IsNullOrWhiteSpace(crm))
                    sql.Append("   AND NUM_DOC_PROFISSIONAL = :NUM_DOC_PROFISSIONAL ");
                if (!string.IsNullOrWhiteSpace(nome))
                    sql.Append("   AND NOM_PROFISSIONAL LIKE :NOM_PROFISSIONAL ");
                if (!string.IsNullOrWhiteSpace(sobrenome))
                    sql.Append("   AND SBN_PROFISSIONAL LIKE :SBN_PROFISSIONAL ");
                if (!string.IsNullOrWhiteSpace(cpf))
                    sql.Append("   AND CPF_PROFISSIONAL = :CPF_PROFISSIONAL ");

                Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql.ToString());
                query.Params["DSC_UF_DOC_PROFISSIONAL"] = uf;
                if (!string.IsNullOrWhiteSpace(crm))
                    query.Params["NUM_DOC_PROFISSIONAL"] = crm;
                if (!string.IsNullOrWhiteSpace(nome))
                    query.Params["NOM_PROFISSIONAL"] = "%" + nome.ToUpper() + "%";
                if (!string.IsNullOrWhiteSpace(sobrenome))
                    query.Params["SBN_PROFISSIONAL"] = "%" + sobrenome.ToUpper() + "%";
                if (!string.IsNullOrWhiteSpace(cpf))
                    query.Params["CPF_PROFISSIONAL"] = cpf.ToUpper();


                ctx.ExecuteQuery(query);

                while (ctx.Reader.Read())
                {                    
                    p.Codigo = Convert.ToInt32(ctx.Reader["COD_PROFISSIONAL"]);
                    p.Documento = Convert.ToString(ctx.Reader["NUM_DOC_PROFISSIONAL"]);
                    p.Nome = Convert.ToString(ctx.Reader["NOM_PROFISSIONAL"]);
                    p.Sobrenome = Convert.ToString(ctx.Reader["SBN_PROFISSIONAL"]);
                    p._NumUserBanco = Convert.ToInt32(ctx.Reader["NUM_USER_BANCO"]);
                    p.Cpf = Convert.ToString(ctx.Reader["CPF_PROFISSIONAL"]);
                }
            }
            return p;        
        }

        public Hcrp.Framework.Classes.Profissional BuscarMedicoCRM(string nome, string sobrenome, string cpf)
        {
            Hcrp.Framework.Classes.Profissional p = new Hcrp.Framework.Classes.Profissional();
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                ctx.Open();
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT COD_PROFISSIONAL, NUM_USER_BANCO, ");
                sql.Append("NOM_PROFISSIONAL, SBN_PROFISSIONAL, CPF_PROFISSIONAL, ");
                sql.Append("NUM_DOC_PROFISSIONAL ");
                sql.Append("FROM PROFISSIONAL ");
                sql.Append("WHERE 1=1");                
                if (!string.IsNullOrWhiteSpace(nome))
                    sql.Append(" AND NOM_PROFISSIONAL LIKE :NOM_PROFISSIONAL ");
                if (!string.IsNullOrWhiteSpace(sobrenome))
                    sql.Append(" AND SBN_PROFISSIONAL LIKE :SBN_PROFISSIONAL ");
                if (!string.IsNullOrWhiteSpace(cpf))
                    sql.Append(" AND CPF_PROFISSIONAL = :CPF_PROFISSIONAL ");

                Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql.ToString());                
                if (!string.IsNullOrEmpty(nome))
                    query.Params["NOM_PROFISSIONAL"] = "%" + nome.ToUpper() + "%";
                if (!string.IsNullOrEmpty(sobrenome))
                    query.Params["SBN_PROFISSIONAL"] = "%" + sobrenome.ToUpper() + "%";
                if (!string.IsNullOrEmpty(cpf))
                    query.Params["CPF_PROFISSIONAL"] = cpf.ToUpper();

                ctx.ExecuteQuery(query);

                while (ctx.Reader.Read())
                {
                    p.Codigo = Convert.ToInt32(ctx.Reader["COD_PROFISSIONAL"]);
                    p.Documento = Convert.ToString(ctx.Reader["NUM_DOC_PROFISSIONAL"]);
                    p.Nome = Convert.ToString(ctx.Reader["NOM_PROFISSIONAL"]);
                    p.Sobrenome = Convert.ToString(ctx.Reader["SBN_PROFISSIONAL"]);
                    p._NumUserBanco = Convert.ToInt32(ctx.Reader["NUM_USER_BANCO"]);
                    p.Cpf = Convert.ToString(ctx.Reader["CPF_PROFISSIONAL"]);
                }
            }
            return p;
        }

        public Hcrp.Framework.Classes.Profissional BuscarProfissionalCodigo(int codigo)
        {
            Hcrp.Framework.Classes.Profissional p = null;

            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                ctx.Open();
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT P.COD_PROFISSIONAL, P.NUM_USER_BANCO, ");
                sql.Append(" P.NOM_PROFISSIONAL, P.SBN_PROFISSIONAL, P.CPF_PROFISSIONAL, ");
                sql.Append(" P.NUM_DOC_PROFISSIONAL, P.NUM_PROFISSAO, PROF.SGL_DOCUMENTO, ");
                sql.Append(" P.NUM_CNS, PROF.NOM_PROFISSAO ");
                sql.Append(" FROM PROFISSIONAL P, PROFISSAO PROF ");
                sql.Append(" WHERE P.NUM_PROFISSAO = PROF.NUM_PROFISSAO AND P.COD_PROFISSIONAL = :COD_PROFISSIONAL ");

                Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql.ToString());
                query.Params["COD_PROFISSIONAL"] = codigo;

                ctx.ExecuteQuery(query);

                while (ctx.Reader.Read())
                {
                    p = new Hcrp.Framework.Classes.Profissional();

                    if (ctx.Reader["COD_PROFISSIONAL"] != DBNull.Value)
                        p.Codigo = Convert.ToInt32(ctx.Reader["COD_PROFISSIONAL"]);

                    if (ctx.Reader["NUM_DOC_PROFISSIONAL"] != DBNull.Value)
                        p.Documento = Convert.ToString(ctx.Reader["NUM_DOC_PROFISSIONAL"]);

                    if (ctx.Reader["NOM_PROFISSIONAL"] != DBNull.Value)
                        p.Nome = Convert.ToString(ctx.Reader["NOM_PROFISSIONAL"]);

                    if (ctx.Reader["SBN_PROFISSIONAL"] != DBNull.Value)
                        p.Sobrenome = Convert.ToString(ctx.Reader["SBN_PROFISSIONAL"]);

                    if (ctx.Reader["NUM_USER_BANCO"] != DBNull.Value)
                        p._NumUserBanco = Convert.ToInt32(ctx.Reader["NUM_USER_BANCO"]);

                    if (ctx.Reader["CPF_PROFISSIONAL"] != DBNull.Value)
                        p.Cpf = Convert.ToString(ctx.Reader["CPF_PROFISSIONAL"]);

                    if (ctx.Reader["NUM_PROFISSAO"] != DBNull.Value)
                        p.NumProfissao = Convert.ToInt32(ctx.Reader["NUM_PROFISSAO"]);

                    if (ctx.Reader["NOM_PROFISSAO"] != DBNull.Value)
                        p.NomeProfissao = Convert.ToString(ctx.Reader["NOM_PROFISSAO"]);

                    if (ctx.Reader["SGL_DOCUMENTO"] != DBNull.Value)
                        p.SiglaProfissao = ctx.Reader["SGL_DOCUMENTO"].ToString();

                    if (ctx.Reader["NUM_CNS"] != DBNull.Value)
                        p.NumeroCNS = ctx.Reader["NUM_CNS"].ToString();

                    break;
                }
            }

            return p;        
        }
        
        public int Inserir(Hcrp.Framework.Classes.Profissional P)
        {
            try
            {
                long retorno = 0;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("PROFISSIONAL");                  
                    comando.Params["NUM_DOC_PROFISSIONAL"] = P.Documento;
                    comando.Params["DSC_UF_DOC_PROFISSIONAL"] = P.UfDocumento;
                    comando.Params["NOM_PROFISSIONAL"] = P.Nome.ToUpper();
                    comando.Params["SBN_PROFISSIONAL"] = P.Sobrenome.ToUpper();
                    comando.Params["CPF_PROFISSIONAL"] = P.Cpf;
                    comando.Params["NUM_PROFISSAO"] = P.NumProfissao;

                    // Executar o insert
                    ctx.ExecuteInsert(comando);

                    // Pegar o último ID
                    retorno = ctx.GetSequenceValue("SEQUENCE_MEDICO", false);
                }
                return (int)retorno;
            }
            catch (Exception)
            {
                throw;
            }                
        }

        /// <summary>
        /// Buscar os tipos de profissional.
        /// </summary>        
        public List<Hcrp.Framework.Classes.Profissional> BuscarOsTiposDeProfissao()
        {
            List<Hcrp.Framework.Classes.Profissional> _listRetorno = new List<Classes.Profissional>();
            Hcrp.Framework.Classes.Profissional _item = null;

            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                ctx.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(" SELECT V.NUM_PROFISSAO, ");
                sql.AppendLine("        V.NOM_PROFISSAO, ");
                sql.AppendLine("        V.SGL_DOCUMENTO ");
                sql.AppendLine(" FROM V_PROFISSAO_SARA V ");

                Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql.ToString());                

                ctx.ExecuteQuery(query);

                while (ctx.Reader.Read())
                {
                    _item = new Hcrp.Framework.Classes.Profissional();

                    if (ctx.Reader["NUM_PROFISSAO"] != DBNull.Value)
                        _item.NumProfissao = Convert.ToInt32(ctx.Reader["NUM_PROFISSAO"]);

                    if (ctx.Reader["NOM_PROFISSAO"] != DBNull.Value)
                        _item.NomeProfissao = Convert.ToString(ctx.Reader["NOM_PROFISSAO"]);

                    if (ctx.Reader["SGL_DOCUMENTO"] != DBNull.Value)
                        _item.SiglaProfissao = Convert.ToString(ctx.Reader["SGL_DOCUMENTO"]);

                    _listRetorno.Add(_item);
                }
            }

            return _listRetorno;
        }

        /// <summary>
        /// Buscar os profissionais por número de profissão, UF CRM e número CRM.
        /// </summary>        
        public List<Hcrp.Framework.Classes.Profissional> BuscarProfissionalPorNumeroProfissaoUFCRMNumeroCRMAtivos(int numeroProfissao, string UFCRM, string numeroCRM)
        {
            List<Hcrp.Framework.Classes.Profissional> _listRetorno = new List<Classes.Profissional>();
            Hcrp.Framework.Classes.Profissional _item = null;

            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                ctx.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine(" SELECT P.COD_PROFISSIONAL, ");
                sql.AppendLine("        P.NOM_PROFISSIONAL, ");
                sql.AppendLine("        P.SBN_PROFISSIONAL, ");                
                sql.AppendLine("        P.NUM_CNS, ");
                sql.AppendLine("        P.CPF_PROFISSIONAL, ");
                sql.AppendLine("        P.NUM_DOC_PROFISSIONAL, ");
                sql.AppendLine("        P.DSC_UF_DOC_PROFISSIONAL, ");
                sql.AppendLine("        P.NUM_PROFISSAO ");
                sql.AppendLine(" FROM   PROFISSIONAL P ");
                sql.AppendLine(" WHERE P.DTA_DESATIVACAO IS NULL ");
                sql.AppendLine("       AND P.NUM_PROFISSAO = :NUM_PROFISSAO ");
                sql.AppendLine("       AND P.NUM_DOC_PROFISSIONAL = :NUMERO_DOC "); // crm
                sql.AppendLine("       AND NVL(P.DSC_UF_DOC_PROFISSIONAL,'SP') = :UF ");

                Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql.ToString());

                query.Params["NUM_PROFISSAO"] = numeroProfissao;
                query.Params["NUMERO_DOC"] = numeroCRM;
                query.Params["UF"] = UFCRM;

                ctx.ExecuteQuery(query);

                while (ctx.Reader.Read())
                {
                    _item = new Hcrp.Framework.Classes.Profissional();
                    
                    if (ctx.Reader["COD_PROFISSIONAL"] != DBNull.Value)
                        _item.Codigo = Convert.ToInt32(ctx.Reader["COD_PROFISSIONAL"]);

                    if (ctx.Reader["NUM_PROFISSAO"] != DBNull.Value)
                        _item.NumProfissao = Convert.ToInt32(ctx.Reader["NUM_PROFISSAO"]);

                    if (ctx.Reader["NOM_PROFISSIONAL"] != DBNull.Value)
                        _item.Nome = Convert.ToString(ctx.Reader["NOM_PROFISSIONAL"]);

                    if (ctx.Reader["SBN_PROFISSIONAL"] != DBNull.Value)
                        _item.Sobrenome = Convert.ToString(ctx.Reader["SBN_PROFISSIONAL"]);

                    if (ctx.Reader["NUM_DOC_PROFISSIONAL"] != DBNull.Value)
                        _item.Documento = Convert.ToString(ctx.Reader["NUM_DOC_PROFISSIONAL"]);

                    if (ctx.Reader["NUM_CNS"] != DBNull.Value)
                        _item.NumeroCNS = Convert.ToString(ctx.Reader["NUM_CNS"]);

                    if (ctx.Reader["CPF_PROFISSIONAL"] != DBNull.Value)
                        _item.Cpf = Convert.ToString(ctx.Reader["CPF_PROFISSIONAL"]);

                    if (ctx.Reader["DSC_UF_DOC_PROFISSIONAL"] != DBNull.Value)
                        _item.UfDocumento = Convert.ToString(ctx.Reader["DSC_UF_DOC_PROFISSIONAL"]);

                    _listRetorno.Add(_item);
                }
            }

            return _listRetorno;
        }
    }
}
