using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
    public class MaterialNaoApropriacaoCC
    {
        public List<Hcrp.Framework.Classes.MaterialNaoApropriacaoCC> BuscaMaterialNaoApropriado(string CodMaterial, string CodCenCusto)
        {
            List<Hcrp.Framework.Classes.MaterialNaoApropriacaoCC> _listaDeRetorno = new List<Hcrp.Framework.Classes.MaterialNaoApropriacaoCC>();
            Hcrp.Framework.Classes.MaterialNaoApropriacaoCC _matnaoap = null;

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {

                    // Cria a query
                    StringBuilder str = new StringBuilder();


                    str.AppendLine("SELECT NUM_USER_CADASTRO, DTA_HOR_CADASTRO,");
                    str.AppendLine("       COD_MATERIAL, COD_CENCUSTO,  ");
                    str.AppendLine("       NUM_USER_EXCLUSAO, DTA_HOR_EXCLUSAO, SEQ_MAT_NAO_APROP_CC ");
                    str.AppendLine("FROM MATERIAL_NAO_APROPRIACAO_CC   ");
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

                        _matnaoap = new Hcrp.Framework.Classes.MaterialNaoApropriacaoCC();

                        if (dr["NUM_USER_CADASTRO"] != DBNull.Value)
                            _matnaoap.UsuarioCad = Convert.ToDouble(dr["NUM_USER_CADASTRO"]);
                        if (dr["DTA_HOR_CADASTRO"] != DBNull.Value)
                            _matnaoap.DataCadastro = Convert.ToDateTime(dr["DTA_HOR_CADASTRO"]);
                        if (dr["COD_MATERIAL"] != DBNull.Value)
                            _matnaoap.CodMaterial = dr["COD_MATERIAL"].ToString();
                        if (dr["COD_CENCUSTO"] != DBNull.Value)
                            _matnaoap.CodCenCusto = dr["COD_CENCUSTO"].ToString();
                        if (dr["DTA_HOR_EXCLUSAO"] != DBNull.Value)
                            _matnaoap.DataExclusao = Convert.ToDateTime(dr["DTA_HOR_EXCLUSAO"]);
                        if (dr["NUM_USER_EXCLUSAO"] != DBNull.Value)
                            _matnaoap.UsuarioExc = Convert.ToDouble(dr["NUM_USER_EXCLUSAO"]);                        
                        if (dr["SEQ_PROCED_MAT_CENCUSTO"] != DBNull.Value)
                            _matnaoap.Seq = Convert.ToDouble(dr["SEQ_PROCED_MAT_CENCUSTO"]);

                        _listaDeRetorno.Add(_matnaoap);

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
