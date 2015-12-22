using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class BannerRevista : Hcrp.Framework.Classes.BannerRevista
    {
        public Boolean Inserir(Hcrp.Framework.Classes.BannerRevista Banner)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.UpdateCommandConfig comando = new Hcrp.Infra.AcessoDado.UpdateCommandConfig("REVISTA_BANNER");
                    comando.Params["SEQ_REVISTA"] = new Hcrp.Framework.Classes.ConfiguracaoSistema().RevistaSite;
                    comando.Params["NOM_ARQUIVO"] = Banner.Nome;
                    comando.Params["NOM_LINK"] = Banner.Link;

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
        public List<Hcrp.Framework.Classes.BannerRevista> BuscarBanners()
        {
            List<Hcrp.Framework.Classes.BannerRevista> l = new List<Hcrp.Framework.Classes.BannerRevista>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT * " + Environment.NewLine);
                    sb.Append("  FROM REVISTA_BANNER " + Environment.NewLine);
                    sb.Append(" WHERE SEQ_REVISTA = :SEQ_REVISTA " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["SEQ_REVISTA"] = new Hcrp.Framework.Classes.ConfiguracaoSistema().RevistaSite;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.BannerRevista b = new Hcrp.Framework.Classes.BannerRevista();
                        b.SeqBanner = Convert.ToInt32(dr["SEQ_BANNER"]);
                        b.Nome = Convert.ToString(dr["NOM_ARQUIVO"]);
                        b.Link = Convert.ToString(dr["NOM_LINK"]);
                        l.Add(b);
                    }

                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Boolean Remover(int SeqBanner)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("REVISTA_BANNER");
                    comando.Params["SEQ_BANNER"] = SeqBanner;

                    // Executar o insert
                    ctx.ExecuteDelete(comando);                    
                    return true;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
