using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Collections.Specialized;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class VagasDrs : Hcrp.Framework.Classes.VagasDrs
    {
        public List<Hcrp.Framework.Classes.VagasDrs> BuscarVagas(int SeqItemAtendimento)
        {
            List<Hcrp.Framework.Classes.VagasDrs> l = new List<Hcrp.Framework.Classes.VagasDrs>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    //Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig("GENERICO.PROC_VAGAS_DISPONIVEIS");
                    //query.Params["V_SEQ_ITEM_PED_ATEND"] = SeqItemAtendimento;
                    //query.Params["CS_DADOS"] = "";
                    //query.Params["V_ERRO"] = "";

                    ListDictionary Params = ctx.DeriveParameters("GENERICO.PROC_VAGAS_DISPONIVEIS");

                    ((OracleParameter)Params["V_SEQ_ITEM_PED_ATEND"]).Value = SeqItemAtendimento;

                    DataSet ds = ctx.GetDataSet("GENERICO.PROC_VAGAS_DISPONIVEIS", Params);
                    //ctx.ExecuteQuery("GENERICO.PROC_VAGAS_DISPONIVEIS", Params);

                    // Cria objeto de material
                    //OracleDataReader dr = ctx.Reader as OracleDataReader;

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                Hcrp.Framework.Classes.VagasDrs v = new Hcrp.Framework.Classes.VagasDrs();
                                v._CodInstituto = Convert.ToInt32(ds.Tables[0].Rows[i]["COD_INSTITUTO"]);
                                v._NumSeqLocal = Convert.ToInt32(ds.Tables[0].Rows[i]["NUM_SEQ_LOCAL"]);
                                v.Seq = Convert.ToInt32(ds.Tables[0].Rows[i]["SEQ_AGENDA_RADIOLIGIA_QTD"]);
                                v.DataExame = Convert.ToDateTime(ds.Tables[0].Rows[i]["DATA_EXAME"]);
                                v.DiaSemana = Convert.ToString(ds.Tables[0].Rows[i]["DIA_SEMANA"]);
                                v.HoraInicial = Convert.ToString(ds.Tables[0].Rows[i]["HOR_INICIAL"]);
                                v.HoraFinal = Convert.ToString(ds.Tables[0].Rows[i]["HOR_FINAL"]);
                                v.QtdVagas = Convert.ToInt32(ds.Tables[0].Rows[i]["QTD_VAGAS"]);
                                v.QtdAgendada = Convert.ToInt32(ds.Tables[0].Rows[i]["QTD_AGENDADA"]);
                                v.SaldoVagas = Convert.ToInt32(ds.Tables[0].Rows[i]["SLD_VAGAS"]);
                                l.Add(v);
                            }
                        }
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
