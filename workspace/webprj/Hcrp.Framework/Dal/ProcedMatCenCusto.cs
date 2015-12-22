using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class ProcedMatCenCusto : Hcrp.Framework.Classes.ProcedMatCenCusto
    {

        public List<Hcrp.Framework.Classes.ProcedMatCenCusto> BuscaProcedimentoPorCentroCustoMaterial(string CodMaterial, string CodCenCusto)
        {
            List<Hcrp.Framework.Classes.ProcedMatCenCusto> _listaDeRetorno = new List<Hcrp.Framework.Classes.ProcedMatCenCusto>();
            Hcrp.Framework.Classes.ProcedMatCenCusto _procedmatcc = null;
            
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    // Cria a query
                    StringBuilder str = new StringBuilder();                    
                    
                    
                    str.AppendLine("SELECT NUM_USER_CADASTRO, DTA_HOR_CADASTRO,");
                    str.AppendLine("       COD_MATERIAL, COD_CENCUSTO, COD_PROCEDIMENTO_HC, ");
                    str.AppendLine("       NUM_USER_EXCLUSAO, DTA_HOR_EXCLUSAO, SEQ_PROCED_MAT_CENCUSTO ");
                    str.AppendLine("FROM PROCED_MAT_CENCUSTO   ");
                    str.Append(" WHERE COD_MATERIAL = :COD_MATERIAL");
                    str.Append(" WHERE COD_CENCUSTO = :COD_CENCUSTO");
                    
                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    query.Params["COD_MATERIAL"] = CodMaterial;
                    query.Params["COD_CENCUSTO"] = CodCenCusto;
                    // Abre conexão
                    ctx.Open();

                    // Veriricar contador
                    ctx.ExecuteQuery(query);

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {

                        _procedmatcc = new Hcrp.Framework.Classes.ProcedMatCenCusto();

                        if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                            _procedmatcc.UsuarioCad = Convert.ToDouble(dr["NUM_USER_CADASTRO"]);
                        if (dr["DTA_HOR_CADASTRO"] != DBNull.Value)
                            _procedmatcc.DataCadastro = Convert.ToDateTime(dr["DTA_HOR_CADASTRO"]);
                        if (dr["COD_MATERIAL"] != DBNull.Value)
                            _procedmatcc.CodMaterial = dr["COD_MATERIAL"].ToString();
                        if (dr["COD_CENCUSTO"] != DBNull.Value)
                            _procedmatcc.CodCenCusto = dr["COD_CENCUSTO"].ToString();
                        if (dr["DTA_HOR_EXCLUSAO"] != DBNull.Value)
                            _procedmatcc.DataExclusao = Convert.ToDateTime(dr["DTA_HOR_EXCLUSAO"]);
                        if (dr["NUM_USER_EXCLUSAO"] != DBNull.Value)
                            _procedmatcc.UsuarioExc = Convert.ToDouble(dr["NUM_USER_EXCLUSAO"]);                                             
                        if (dr["COD_PROCEDIMENTO_HC"] != DBNull.Value)
                            _procedmatcc.CodProcHC = Convert.ToDouble(dr["COD_PROCEDIMENTO_HC"]);
                        if (dr["SEQ_PROCED_MAT_CENCUSTO"] != DBNull.Value)
                            _procedmatcc.Seq = Convert.ToDouble(dr["SEQ_PROCED_MAT_CENCUSTO"]);

                        _listaDeRetorno.Add(_procedmatcc);

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
