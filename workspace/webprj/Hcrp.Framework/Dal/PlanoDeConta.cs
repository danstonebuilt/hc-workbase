using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class PlanoDeConta : Hcrp.Framework.Classes.PlanoDeConta
    {
        /// <summary>
        /// Obter lista de plano de conta
        /// </summary>
        /// <returns></returns>
        public List<Hcrp.Framework.Classes.PlanoDeConta> ObterListaDePlanoDeConta()
        {
            List<Hcrp.Framework.Classes.PlanoDeConta> _listaDeRetorno = new List<Hcrp.Framework.Classes.PlanoDeConta>();
            Hcrp.Framework.Classes.PlanoDeConta _planoDeConta = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(string.Format(" SELECT SEQ_PLANO_CONTA, NOM_PLANO_CONTA FROM PLANO_CONTA WHERE COD_INST_SISTEMA={0}", new Hcrp.Framework.Classes.ConfiguracaoSistema().CodInstituto));

                    // Abre conexão
                    ctx.Open();

                    // Obter a lista de registros
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _planoDeConta = new Hcrp.Framework.Classes.PlanoDeConta();

                        if (dr["SEQ_PLANO_CONTA"] != DBNull.Value)
                            _planoDeConta.IdPlanoDeConta = Convert.ToInt64(dr["SEQ_PLANO_CONTA"]);

                        if (dr["NOM_PLANO_CONTA"] != DBNull.Value)
                            _planoDeConta.NomeDoPlanoDeConta = dr["NOM_PLANO_CONTA"].ToString();

                        _listaDeRetorno.Add(_planoDeConta);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaDeRetorno;
        }
    }
}
