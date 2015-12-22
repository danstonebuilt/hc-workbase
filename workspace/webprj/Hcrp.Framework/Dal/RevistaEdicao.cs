using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class RevistaEdicao : Hcrp.Framework.Classes.RevistaEdicao
    {
        public RevistaEdicao BuscarEdicaoCodigo(Int32 seqRevistaEdicao)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT SEQ_REVISTA_EDICAO, SEQ_REVISTA, NUM_EDICAO, ANO_EDICAO, " + Environment.NewLine);
                    sb.Append(" NOM_EDICAO, DTA_PUBLICACAO, IMG_CAPA " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_EDICAO " + Environment.NewLine);
                    sb.Append(" WHERE SEQ_REVISTA_EDICAO = :SEQ_REVISTA_EDICAO " + Environment.NewLine);
                    sb.Append(" ORDER BY NUM_EDICAO, ANO_EDICAO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_REVISTA_EDICAO"] = seqRevistaEdicao;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        this.SeqEdicao = Convert.ToInt32(dr["SEQ_REVISTA_EDICAO"]);
                        this.Numero = Convert.ToString(dr["NUM_EDICAO"]);
                        this.Ano = Convert.ToInt32(dr["ANO_EDICAO"]);
                        this.Nome = Convert.ToString(dr["NOM_EDICAO"]);
                        this.DataPublicacao = Convert.ToDateTime(dr["DTA_PUBLICACAO"]);
                        this.ImagemCapa = Convert.ToString(dr["IMG_CAPA"]); 
                    }
                }
                return this;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Hcrp.Framework.Classes.RevistaEdicao> ListarEdicoes(Int32 seqRevista)
        {
            List<Hcrp.Framework.Classes.RevistaEdicao> l = new List<Classes.RevistaEdicao>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT SEQ_REVISTA_EDICAO, SEQ_REVISTA, NUM_EDICAO, ANO_EDICAO, " + Environment.NewLine);
                    sb.Append(" NOM_EDICAO, DTA_PUBLICACAO, IMG_CAPA " + Environment.NewLine);
                    sb.Append(" FROM REVISTA_EDICAO " + Environment.NewLine);
                    sb.Append(" WHERE SEQ_REVISTA = :SEQ_REVISTA " + Environment.NewLine);
                    sb.Append(" ORDER BY ANO_EDICAO, NUM_EDICAO " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_REVISTA"] = seqRevista;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.RevistaEdicao edicao = new Hcrp.Framework.Classes.RevistaEdicao();
                        edicao.SeqEdicao = Convert.ToInt32(dr["SEQ_REVISTA_EDICAO"]);
                        edicao.Numero = Convert.ToString(dr["NUM_EDICAO"]);
                        edicao.Ano = Convert.ToInt32(dr["ANO_EDICAO"]);
                        edicao.Nome = Convert.ToString(dr["NOM_EDICAO"]);
                        edicao.DataPublicacao = Convert.ToDateTime(dr["DTA_PUBLICACAO"]);
                        edicao.ImagemCapa = Convert.ToString(dr["IMG_CAPA"]);
                        l.Add(edicao);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long InserirAtualizar(Hcrp.Framework.Classes.RevistaEdicao Edicao, int SeqRevista)
        {
            if ((Edicao.SeqEdicao == null) || (Edicao.SeqEdicao == 0)) //Inserir
            {
                try
                {
                    long retorno = 0;
                    using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                    {
                        // Abrir conexão
                        ctx.Open();

                        // Preparar o comando
                        Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("REVISTA_EDICAO");
                        comando.Params["SEQ_REVISTA"] = SeqRevista;
                        comando.Params["NUM_EDICAO"] = Edicao.Numero;
                        comando.Params["ANO_EDICAO"] = Edicao.Ano;
                        comando.Params["NOM_EDICAO"] = Edicao.Nome;
                        comando.Params["DTA_PUBLICACAO"] = Edicao.DataPublicacao;
                        if (!String.IsNullOrWhiteSpace(Edicao.ImagemCapa))
                            comando.Params["IMG_CAPA"] = Edicao.ImagemCapa;

                        // Executar o insert
                        ctx.ExecuteInsert(comando);

                        // Pegar o último ID
                        retorno = ctx.GetSequenceValue("GENERICO.SEQ_REVISTA_EDICAO", false);
                    }
                    return retorno;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else //Atualizar
            {
                try
                {
                    long retorno = 0;
                    using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                    {
                        // Abrir conexão
                        ctx.Open();

                        // Preparar o comando
                        Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REVISTA_EDICAO");
                        //comando.Params["SEQ_REVISTA_EDICAO"] = Artigo.Edicao.SeqEdicao;
                        comando.Params["SEQ_REVISTA"] = SeqRevista;
                        comando.Params["NUM_EDICAO"] = Edicao.Numero;
                        comando.Params["ANO_EDICAO"] = Edicao.Ano;
                        comando.Params["NOM_EDICAO"] = Edicao.Nome;
                        comando.Params["DTA_PUBLICACAO"] = Edicao.DataPublicacao;
                        if (!String.IsNullOrWhiteSpace(Edicao.ImagemCapa))
                            comando.Params["IMG_CAPA"] = Edicao.ImagemCapa;

                        comando.FilterParams["SEQ_REVISTA_EDICAO"] = Edicao.SeqEdicao;

                        // Executar o insert
                        ctx.ExecuteUpdate(comando);

                        // Pegar o último ID
                        retorno = Edicao.SeqEdicao;
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
