using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Dal
{
    public class PesquisaSatisfacaoCrhQualidade
    {
        public bool Gravar(Hcrp.Framework.Classes.PesquisaSatisfacaoCrhQualidade p, long seqPesquisaCrh)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    // Preparar o comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("GENERICO.CRH_PESQ_SATISF_QUALIDADE_ITEM");

                    comando.Params["SEQ_CRH_PESQUISA_SATISFACAO"] = seqPesquisaCrh;
                    comando.Params["IDF_TIPO_QUALIDADE_CRH"] = (int)p.TipoQualidade;
                    comando.Params["IDF_RAPIDEZ_ATEND"] = (int)p.RapidezAtendimento;
                    comando.Params["IDF_ATEND_CORDIAL"] = (int)p.AtendimentoCordial;
                    comando.Params["IDF_RESOLV_PROB"] = (int)p.CapacidadeResolverProblemas;
                    comando.Params["IDF_CONHEC_TECNICO"] = (int)p.ConhecimentoTecnico;
                    comando.Params["IDF_CLAREZA_EXPL"] = (int)p.ClarezaExplicacao;
                    comando.Params["IDF_CONFIAB_INF"] = (int)p.ConfiabilidadeInformacoes;
                    
                    // Executar o insert
                    ctx.ExecuteInsert(comando);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
