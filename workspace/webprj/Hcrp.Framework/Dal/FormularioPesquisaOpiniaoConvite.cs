using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class FormularioPesquisaOpiniaoConvite
    {
        public string VerificaConviteValido(Int64 chave, int formPesquisa)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COUNT(*) QTD FROM GENERICO.FORM_PESQUISA_CONVITE A, GENERICO.FORM_PESQUISA B " + Environment.NewLine);
                    sb.Append(" WHERE A.IDF_RESPONDIDO = 'N' " + Environment.NewLine);
                    sb.Append(" AND A.SEQ_FORM_PESQUISA = B.SEQ_FORM_PESQUISA " + Environment.NewLine);
                    sb.Append(" AND SYSDATE BETWEEN B.DTA_HOR_INI AND B.DTA_HOR_FIM " + Environment.NewLine);
                    sb.Append(" AND A.SEQ_FORM_PESQUISA_CONVITE = :SEQ_FORM_PESQUISA_CONVITE " + Environment.NewLine);
                    sb.Append(" AND A.SEQ_FORM_PESQUISA = :SEQ_FORM_PESQUISA " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_FORM_PESQUISA_CONVITE"] = chave;
                    query.Params["SEQ_FORM_PESQUISA"] = formPesquisa;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    int qtdConvites = 0;
                    while (dr.Read())
                    {
                        qtdConvites = Convert.ToInt32(dr["QTD"]);
                    }
                    if (qtdConvites == 0) 
                        return "Convite já respondido ou chave inválida.";
                    else return "";
                }                
            }
            catch (Exception)
            {
                throw;
            }            
        }
        public void MarcarConviteRespondido(Int64 chave)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("GENERICO.FORM_PESQUISA_CONVITE");
                    comando.Params["IDF_RESPONDIDO"] = "S";
                    comando.FilterParams["SEQ_FORM_PESQUISA_CONVITE"] = chave;
                    // Executar o insert
                    ctx.ExecuteUpdate(comando);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void MarcarConviteEnviado(Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite convite)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();
                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("GENERICO.FORM_PESQUISA_CONVITE");
                    comando.Params["DTA_HOR_CONVITE"] = DateTime.Now;
                    comando.FilterParams["SEQ_FORM_PESQUISA_CONVITE"] = convite.Seq;
                    // Executar o insert
                    ctx.ExecuteUpdate(comando);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite BuscarConvite(int seq)
        {
            try
            {
                Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite Convite = new Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite();
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(" SELECT SEQ_FORM_PESQUISA_CONVITE, NOM_CONVIDADO, DSC_EMAIL, DTA_HOR_CONVITE, IDF_RESPONDIDO, SEQ_FORM_PESQUISA ");
                    sb.AppendLine(" FROM GENERICO.FORM_PESQUISA_CONVITE ");
                    sb.AppendLine(" WHERE SEQ_FORM_PESQUISA_CONVITE = :SEQ_FORM_PESQUISA_CONVITE ");

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_FORM_PESQUISA_CONVITE"] = seq;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Convite.Seq = Convert.ToInt32(dr["SEQ_FORM_PESQUISA_CONVITE"]);
                        Convite.Nome = Convert.ToString(dr["NOM_CONVIDADO"]);
                        Convite.Email = Convert.ToString(dr["DSC_EMAIL"]);
                        if (dr["DTA_HOR_CONVITE"] != DBNull.Value)
                            Convite.DataHoraConvite = Convert.ToDateTime(dr["DTA_HOR_CONVITE"]);
                        Convite.Respondido = Convert.ToString(dr["IDF_RESPONDIDO"]) == "S";
                    }
                    return Convite;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite> BuscarConvitesFormulario(Hcrp.Framework.Classes.FormularioPesquisaOpiniao formulario)
        { 
            try
            {
                List<Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite> l = new List<Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite>();                
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(" SELECT SEQ_FORM_PESQUISA_CONVITE, NOM_CONVIDADO, DSC_EMAIL, DTA_HOR_CONVITE, IDF_RESPONDIDO, SEQ_FORM_PESQUISA ");
                    sb.AppendLine(" FROM GENERICO.FORM_PESQUISA_CONVITE ");
                    sb.AppendLine(" WHERE SEQ_FORM_PESQUISA = :SEQ_FORM_PESQUISA ");
                    sb.Append(" ORDER BY NOM_CONVIDADO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_FORM_PESQUISA"] = formulario.Seq;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite Convite = new Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite();
                        Convite.Seq = Convert.ToInt32(dr["SEQ_FORM_PESQUISA_CONVITE"]);
                        Convite.Nome = Convert.ToString(dr["NOM_CONVIDADO"]);
                        Convite.Email = Convert.ToString(dr["DSC_EMAIL"]);
                        if (dr["DTA_HOR_CONVITE"]!=DBNull.Value)
                            Convite.DataHoraConvite = Convert.ToDateTime(dr["DTA_HOR_CONVITE"]);
                        Convite.Respondido = Convert.ToString(dr["IDF_RESPONDIDO"]) == "S";
                        l.Add(Convite);
                    }
                    return l;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite> BuscarTodosConviteRelatorio(Int64 formulario)
        { 
            try
            {
                List<Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite> l = new List<Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite>();                
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(" SELECT SEQ_FORM_PESQUISA_CONVITE, NOM_CONVIDADO, DSC_EMAIL, DTA_HOR_CONVITE, IDF_RESPONDIDO, SEQ_FORM_PESQUISA ");
                    sb.AppendLine(" FROM GENERICO.FORM_PESQUISA_CONVITE ");
                    sb.AppendLine(" WHERE SEQ_FORM_PESQUISA = :SEQ_FORM_PESQUISA ");
                    sb.AppendLine(" AND DTA_HOR_CONVITE IS NULL ");
                    sb.AppendLine(" AND DSC_EMAIL IS NULL ");
                    sb.AppendLine(" ORDER BY NOM_CONVIDADO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_FORM_PESQUISA"] = formulario;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite Convite = new Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite();
                        Convite.Seq = Convert.ToInt32(dr["SEQ_FORM_PESQUISA_CONVITE"]);
                        Convite.Nome = Convert.ToString(dr["NOM_CONVIDADO"]);
                        Convite.Email = Convert.ToString(dr["DSC_EMAIL"]);
                        if (dr["DTA_HOR_CONVITE"]!=DBNull.Value)
                            Convite.DataHoraConvite = Convert.ToDateTime(dr["DTA_HOR_CONVITE"]);
                        Convite.Respondido = Convert.ToString(dr["IDF_RESPONDIDO"]) == "S";
                        l.Add(Convite);
                    }
                    return l;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite> BuscarTodosConviteEmail(Int64 formulario)
        {
            try
            {
                List<Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite> l = new List<Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite>();
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(" SELECT SEQ_FORM_PESQUISA_CONVITE, NOM_CONVIDADO, DSC_EMAIL, DTA_HOR_CONVITE, IDF_RESPONDIDO, SEQ_FORM_PESQUISA ");
                    sb.AppendLine(" FROM GENERICO.FORM_PESQUISA_CONVITE ");
                    sb.AppendLine(" WHERE SEQ_FORM_PESQUISA = :SEQ_FORM_PESQUISA ");
                    sb.AppendLine(" AND DTA_HOR_CONVITE IS NULL ");
                    sb.AppendLine(" AND DSC_EMAIL IS NOT NULL ");
                    sb.AppendLine(" ORDER BY NOM_CONVIDADO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_FORM_PESQUISA"] = formulario;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite Convite = new Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite();
                        Convite.Seq = Convert.ToInt32(dr["SEQ_FORM_PESQUISA_CONVITE"]);
                        Convite.Nome = Convert.ToString(dr["NOM_CONVIDADO"]);
                        Convite.Email = Convert.ToString(dr["DSC_EMAIL"]);
                        if (dr["DTA_HOR_CONVITE"] != DBNull.Value)
                            Convite.DataHoraConvite = Convert.ToDateTime(dr["DTA_HOR_CONVITE"]);
                        Convite.Respondido = Convert.ToString(dr["IDF_RESPONDIDO"]) == "S";
                        l.Add(Convite);
                    }
                    return l;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Gravar(Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite c, int seqForm)
        {
            try
            {
                bool existe;

                if (string.IsNullOrWhiteSpace(c.Email))
                    existe = VerificaExiteConviteNome(c.Nome, seqForm);
                else existe = VerificaExiteConvite(c.Email, seqForm);

                if (!existe)
                {
                    using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                    {
                        ctx.Open();

                        // Preparar o comando
                        Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("GENERICO.FORM_PESQUISA_CONVITE");
                        comando.Params["SEQ_FORM_PESQUISA"] = seqForm;
                        comando.Params["NOM_CONVIDADO"] = c.Nome.ToUpper();
                        comando.Params["DSC_EMAIL"] = c.Email.ToLower();
                        comando.Params["DTA_HOR_CONVITE"] = DBNull.Value;
                        if (c.Respondido)
                            comando.Params["IDF_RESPONDIDO"] = "S";
                        else comando.Params["IDF_RESPONDIDO"] = "N";

                        // Executar o insert
                        ctx.ExecuteInsert(comando);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Excluir(Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite c)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("GENERICO.FORM_PESQUISA_CONVITE");
                    comando.Params["SEQ_FORM_PESQUISA_CONVITE"] = c.Seq;
                    // Executar o Delete
                    ctx.ExecuteDelete(comando);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool VerificaExiteConvite(string email, int formPesquisa)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COUNT(*) QTD FROM GENERICO.FORM_PESQUISA_CONVITE " + Environment.NewLine);
                    sb.Append(" WHERE SEQ_FORM_PESQUISA = :SEQ_FORM_PESQUISA " + Environment.NewLine);
                    sb.Append(" AND DSC_EMAIL = :DSC_EMAIL " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_FORM_PESQUISA"] = formPesquisa;
                    query.Params["DSC_EMAIL"] = email.ToLower();

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    int qtdConvites = 0;
                    while (dr.Read())
                    {
                        qtdConvites = Convert.ToInt32(dr["QTD"]);
                    }
                    if (qtdConvites == 0)
                        return false;
                    else return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool VerificaExiteConviteNome(string nome, int formPesquisa)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COUNT(*) QTD FROM GENERICO.FORM_PESQUISA_CONVITE " + Environment.NewLine);
                    sb.Append(" WHERE SEQ_FORM_PESQUISA = :SEQ_FORM_PESQUISA " + Environment.NewLine);
                    sb.Append(" AND NOM_CONVIDADO = :NOM_CONVIDADO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_FORM_PESQUISA"] = formPesquisa;
                    query.Params["NOM_CONVIDADO"] = nome.ToUpper();

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    int qtdConvites = 0;
                    while (dr.Read())
                    {
                        qtdConvites = Convert.ToInt32(dr["QTD"]);
                    }
                    if (qtdConvites == 0)
                        return false;
                    else return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
