using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class RevisaoRevistaArtigo : Hcrp.Framework.Classes.RevisaoRevistaArtigo
    {
        public List<Hcrp.Framework.Classes.RevisaoRevistaArtigo> BuscarRevisoes(int NumArtigo, int NumUserBanco)
        {
            List<Hcrp.Framework.Classes.RevisaoRevistaArtigo> l = new List<Hcrp.Framework.Classes.RevisaoRevistaArtigo>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    
                    sb.Append(" SELECT A.SEQ_REVISTA_ARTIGO, A.NUM_USER_REVISAO, A.NUM_ITEM_REVISAO, A.IDF_ACEITACAO, " + Environment.NewLine);
                    sb.Append(" A.IDF_AVALIACAO, A.DSC_PARECER_AUTOR, A.DTA_CONVITE, A.DTA_ACEITACAO, A.DTA_FINALIZACAO, " + Environment.NewLine);
                    sb.Append(" A.DSC_PARECER_TRIADOR FROM REVISTA_ARTIGO_REVISAO A " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_REVISTA_ARTIGO = "+ NumArtigo + Environment.NewLine);
                    if (NumUserBanco > -1)
                    {
                        sb.Append(" AND A.NUM_USER_REVISAO = " + NumUserBanco + Environment.NewLine);
                    }
                    sb.Append(" ORDER BY A.DTA_CONVITE " + Environment.NewLine);
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.RevisaoRevistaArtigo dra = new Hcrp.Framework.Classes.RevisaoRevistaArtigo();


                        dra.NumRevisao = Convert.ToInt32(dr["NUM_ITEM_REVISAO"]);
                        dra.NumUserRevisor = Convert.ToInt32(dr["NUM_USER_REVISAO"]);

                        if (!String.IsNullOrEmpty(Convert.ToString(dr["IDF_ACEITACAO"])))
                        {
                            dra.IdfAceite = Convert.ToString(dr["IDF_ACEITACAO"]);
                        }

                        if (!String.IsNullOrEmpty(Convert.ToString(dr["IDF_AVALIACAO"])))
                        {
                            dra.IdfAvaliacao = Convert.ToString(dr["IDF_AVALIACAO"]);
                        }

                        if (!String.IsNullOrEmpty(Convert.ToString(dr["DSC_PARECER_AUTOR"])))
                        {
                            dra.ParecerAutor = Convert.ToString(dr["DSC_PARECER_AUTOR"]);
                        }

                        if (!String.IsNullOrEmpty(Convert.ToString(dr["DSC_PARECER_TRIADOR"])))
                        {
                            dra.ParecerTriador = Convert.ToString(dr["DSC_PARECER_TRIADOR"]);
                        }

                        if (!String.IsNullOrEmpty(Convert.ToString(dr["DTA_CONVITE"])))
                        {
                            dra.DataConvite = Convert.ToDateTime(dr["DTA_CONVITE"]);
                        }
                        
                        if (!String.IsNullOrEmpty(Convert.ToString(dr["DTA_ACEITACAO"])))
                        {
                            dra.DataAceite = Convert.ToDateTime(dr["DTA_ACEITACAO"]);
                        }

                        if (!String.IsNullOrEmpty(Convert.ToString(dr["DTA_FINALIZACAO"])))
                        {
                            dra.DataFinalizacao = Convert.ToDateTime(dr["DTA_FINALIZACAO"]);
                        }
                        
                        l.Add(dra);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Hcrp.Framework.Classes.RevisaoRevistaArtigo> BuscarPareceres(int NumArtigo)
        {
            List<Hcrp.Framework.Classes.RevisaoRevistaArtigo> l = new List<Hcrp.Framework.Classes.RevisaoRevistaArtigo>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style1\">Enviado por: '||U.NOM_USUARIO||' '||U.SBN_USUARIO||' - '||C.DSC_EMAIL||'</span><span class=\"parecer_style2\"><br>'|| " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style3\">AO AUTOR:</span><br />'|| " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style4\">'||A.DSC_PARECER_AUTOR||'</span><br />'|| " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style5\"><strong>AO TRIADOR:</strong><br />'|| " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style6\">'||A.DSC_PARECER_TRIADOR||'</span></span></span>' PARECER, " + Environment.NewLine);
                    sb.Append(" A.DTA_FINALIZACAO " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_ARTIGO_REVISAO A, USUARIO U, COMPLEMENTO_USUARIO C " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_REVISTA_ARTIGO = " + NumArtigo + Environment.NewLine);
                    sb.Append(" AND A.NUM_USER_REVISAO = U.NUM_USER_BANCO " + Environment.NewLine);
                    sb.Append(" AND U.NUM_USER_BANCO = C.NUM_USER_BANCO " + Environment.NewLine);
                    sb.Append(" AND A.DTA_FINALIZACAO IS NOT NULL " + Environment.NewLine);
                    sb.Append(" ORDER BY A.DTA_FINALIZACAO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.RevisaoRevistaArtigo dra = new Hcrp.Framework.Classes.RevisaoRevistaArtigo();

                        dra.ParecerCompleto = Convert.ToString(dr["PARECER"]);
                        dra.DataFinalizacao = Convert.ToDateTime(dr["DTA_FINALIZACAO"]);

                        l.Add(dra);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public long SetAceite(Hcrp.Framework.Classes.RevisaoRevistaArtigo Revisao, int NumArtigo, string Aceite)
        {
            try
            {
                long retorno = 0;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();


                        // Preparar o comando
                        Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REVISTA_ARTIGO_REVISAO");
                        if (Aceite == "S")
                        {
                            comando.Params["IDF_ACEITACAO"] = "1";
                        }
                        else
                        {
                            comando.Params["IDF_ACEITACAO"] = "0";
                        }
                        comando.Params["DTA_ACEITACAO"] = DateTime.Now;

                        comando.FilterParams["SEQ_REVISTA_ARTIGO"] = NumArtigo;
                        comando.FilterParams["NUM_ITEM_REVISAO"] = Revisao.NumRevisao;
                        comando.FilterParams["NUM_USER_REVISAO"] = Revisao.NumUserRevisor;

                        // Executar o insert
                        ctx.ExecuteUpdate(comando);

                    // Pegar o último ID
                    retorno = NumArtigo;
                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public long SetParecer(Hcrp.Framework.Classes.RevisaoRevistaArtigo Revisao, int NumArtigo, int Parecer)
        {
            try
            {
                long retorno = 0;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REVISTA_ARTIGO_REVISAO");
                    if (Parecer != -1)
                    {
                        comando.Params["IDF_AVALIACAO"] = Convert.ToString(Parecer);
                        comando.Params["DTA_FINALIZACAO"] = DateTime.Now;
                    }
                    comando.Params["DSC_PARECER_AUTOR"] = Revisao.ParecerAutor;
                    comando.Params["DSC_PARECER_TRIADOR"] = Revisao.ParecerTriador;

                    
                    comando.FilterParams["SEQ_REVISTA_ARTIGO"] = NumArtigo;
                    comando.FilterParams["NUM_ITEM_REVISAO"] = Revisao.NumRevisao;
                    comando.FilterParams["NUM_USER_REVISAO"] = Revisao.NumUserRevisor;

                    // Executar o insert
                    ctx.ExecuteUpdate(comando);

                    // Pegar o último ID
                    retorno = NumArtigo;
                }
                return retorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Boolean InserirUsuarioRevisao(Hcrp.Framework.Classes.RevisaoRevistaArtigo Revisao, int NumArtigo)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    //Verificar o ultimo item da revisao do mesmo
                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT NVL(MAX(NUM_ITEM_REVISAO),0) + 1 NUM_ITEM FROM REVISTA_ARTIGO_REVISAO " + Environment.NewLine);
                    sb.Append(" WHERE SEQ_REVISTA_ARTIGO = :SEQ_REVISTA_ARTIGO " + Environment.NewLine);
                    sb.Append(" AND NUM_USER_REVISAO = :NUM_USER_REVISAO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_REVISTA_ARTIGO"] = NumArtigo;
                    query.Params["NUM_USER_REVISAO"] = Revisao.NumUserRevisor;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    int ultimarevisao = 0;

                    while (dr.Read())
                    {
                        ultimarevisao = Convert.ToInt32(dr["NUM_ITEM"]);
                    }

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REVISTA_ARTIGO_REVISAO");
                    comando.Params["SEQ_REVISTA_ARTIGO"] = NumArtigo;
                    comando.Params["NUM_USER_REVISAO"] = Revisao.NumUserRevisor;
                    comando.Params["NUM_ITEM_REVISAO"] = ultimarevisao;
                    comando.Params["DTA_CONVITE"] = System.DateTime.Now;

                    // Executar o insert
                    ctx.ExecuteInsert(comando);
                    return true;
                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message;
                return false;
            }
        
        }


        public List<Hcrp.Framework.Classes.RevisaoRevistaArtigo> BuscarPareceresFinaisAnteriores(int NumArtigo, int NumUserBanco)
        {
            List<Hcrp.Framework.Classes.RevisaoRevistaArtigo> l = new List<Hcrp.Framework.Classes.RevisaoRevistaArtigo>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.SEQ_REVISTA_ARTIGO, A.NUM_ITEM_PARECER, A.IDF_AVALIACAO, A.DSC_PARECER, A.DTA_PARECER " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_PARECER_FINAL A " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_REVISTA_ARTIGO = " + NumArtigo + Environment.NewLine);
                    if (NumUserBanco > -1)
                    {
                        sb.Append(" AND A.NUM_USER_BANCO = " + NumUserBanco + Environment.NewLine);
                    }
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.RevisaoRevistaArtigo dra = new Hcrp.Framework.Classes.RevisaoRevistaArtigo();

                        if (!String.IsNullOrEmpty(Convert.ToString(dr["NUM_ITEM_PARECER"])))
                        {
                            dra.NumItemParecerFinal = Convert.ToInt32(dr["NUM_ITEM_PARECER"]);
                        }

                        if (!String.IsNullOrEmpty(Convert.ToString(dr["IDF_AVALIACAO"])))
                        {
                            dra.IdfAvaliacaoFinal = Convert.ToString(dr["IDF_AVALIACAO"]);
                        }

                        if (!String.IsNullOrEmpty(Convert.ToString(dr["DSC_PARECER"])))
                        {
                            dra.ParecerFinal = Convert.ToString(dr["DSC_PARECER"]);
                        }

                        if (!String.IsNullOrEmpty(Convert.ToString(dr["DTA_PARECER"])))
                        {
                            dra.DataParecerFinal = Convert.ToDateTime(dr["DTA_PARECER"]);
                        }

                        dra.NumUserParecerFinal = NumUserBanco;

                        l.Add(dra);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Hcrp.Framework.Classes.RevisaoRevistaArtigo> BuscarParecerFinal(int NumArtigo)
        {
            List<Hcrp.Framework.Classes.RevisaoRevistaArtigo> l = new List<Hcrp.Framework.Classes.RevisaoRevistaArtigo>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style1\">Enviado por: '||U.NOM_USUARIO||' '||U.SBN_USUARIO||' - '||C.DSC_EMAIL||'</span><span class=\"parecer_style2\"><br>'|| " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style3\">PARECER FINAL:</span><br />'|| " + Environment.NewLine);
                    sb.Append(" '<span class=\"parecer_style4\">'||A.DSC_PARECER||'</span><br />' PARECER " + Environment.NewLine);
                    sb.Append(" A.DTA_PARECER " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_PARECER_FINAL A, USUARIO U, COMPLEMENTO_USUARIO C " + Environment.NewLine);
                    sb.Append(" WHERE A.SEQ_REVISTA_ARTIGO = " + NumArtigo + Environment.NewLine);
                    sb.Append(" AND A.NUM_USER_BANCO = U.NUM_USER_BANCO " + Environment.NewLine);
                    sb.Append(" AND U.NUM_USER_BANCO = C.NUM_USER_BANCO " + Environment.NewLine);
                    sb.Append(" AND A.DTA_PARECER IS NOT NULL " + Environment.NewLine);
                    sb.Append(" ORDER BY A.DTA_PARECER " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.RevisaoRevistaArtigo dra = new Hcrp.Framework.Classes.RevisaoRevistaArtigo();


                        dra.ParecerCompleto = Convert.ToString(dr["PARECER"]);
                        dra.DataFinalizacao = Convert.ToDateTime(dr["DTA_PARECER"]);

                        l.Add(dra);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public long SetParecerFinal(Hcrp.Framework.Classes.RevisaoRevistaArtigo Revisao, int NumArtigo, int Parecer)
        {
            if ((Revisao.NumItemParecerFinal == null) || (Revisao.NumItemParecerFinal == 0)) //Inserir
            {
                try
                {
                    long retorno = 0;
                    using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                    {
                        // Abrir conexão
                        ctx.Open();

                            // Pegando o próximo ítem do parecer final
                            StringBuilder sb = new StringBuilder();
                            sb.Append(" SELECT NVL(MAX(A.NUM_ITEM_PARECER),0) NEW_ITEM FROM REVISTA_PARECER_FINAL A " + Environment.NewLine);
                            sb.Append(" WHERE A.SEQ_REVISTA_ARTIGO = " + Convert.ToString(NumArtigo) + Environment.NewLine);
                            sb.Append(" AND A.NUM_USER_BANCO = "+ Convert.ToString(Revisao.NumUserParecerFinal) + Environment.NewLine);
                            Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                            ctx.ExecuteQuery(query);
                            OracleDataReader dr = ctx.Reader as OracleDataReader;
                            int NewItem = new int();
                            while (dr.Read())
                            {
                                NewItem = Convert.ToInt32(dr["NEW_ITEM"])+1;
                            }

                        // Preparar o comando
                        Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("REVISTA_PARECER_FINAL");
                        if (Parecer != -1)
                        {
                            comando.Params["IDF_AVALIACAO"] = Convert.ToString(Parecer);
                            comando.Params["DTA_PARECER"] = DateTime.Now;
                        }
                        if (!string.IsNullOrWhiteSpace(Revisao.ParecerFinal))
                            comando.Params["DSC_PARECER"] = Revisao.ParecerFinal;


                        comando.Params["SEQ_REVISTA_ARTIGO"] = NumArtigo;
                        comando.Params["NUM_ITEM_PARECER"] = NewItem;
                        comando.Params["NUM_USER_BANCO"] = Revisao.NumUserParecerFinal;

                        // Executar o insert
                        ctx.ExecuteInsert(comando);

                        // Pegar o último ID
                        retorno = NumArtigo;
                    }
                    return retorno;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {

                try
                {
                    long retorno = 0;
                    using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                    {
                        // Abrir conexão
                        ctx.Open();

                        // Preparar o comando
                        Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REVISTA_PARECER_FINAL");
                        if (Parecer != -1)
                        {
                            comando.Params["IDF_AVALIACAO"] = Convert.ToString(Parecer);
                            comando.Params["DTA_PARECER"] = DateTime.Now;
                        }
                        comando.Params["DSC_PARECER"] = Revisao.ParecerFinal;


                        comando.FilterParams["SEQ_REVISTA_ARTIGO"] = NumArtigo;
                        comando.FilterParams["NUM_ITEM_PARECER"] = Revisao.NumItemParecerFinal;
                        comando.FilterParams["NUM_USER_BANCO"] = Revisao.NumUserParecerFinal;

                        // Executar o insert
                        ctx.ExecuteUpdate(comando);

                        // Pegar o último ID
                        retorno = NumArtigo;
                    }
                    return retorno;
                }
                catch (Exception)
                {
                    throw;
                }
            
            
            }
        }

    }
}
