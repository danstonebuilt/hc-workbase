using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class RateioMaterialGrupoCencusto : Hcrp.Framework.Classes.RateioMaterialGrupoCencusto 
    {
        public List<Hcrp.Framework.Classes.RateioMaterialGrupoCencusto> BuscaRateioMaterialGrupoCencusto(string CodMaterial, string CodCenCusto)
        {
            List<Hcrp.Framework.Classes.RateioMaterialGrupoCencusto> _listaDeRetorno = new List<Hcrp.Framework.Classes.RateioMaterialGrupoCencusto>();
            Hcrp.Framework.Classes.RateioMaterialGrupoCencusto _rateio = null;
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder str = new StringBuilder();
                    str.AppendLine(" SELECT R.COD_CENCUSTO, R.COD_MATERIAL, R.COD_GRUPO_SICH, R.PCT_RATEIO ");
                    str.AppendLine("  FROM RATEIO_MAT_GP_CENCUSTO R ");
                    str.AppendLine("  WHERE R.COD_MATERIAL = " + CodMaterial);
                    str.AppendLine("    AND R.COD_CENCUSTO = " + CodCenCusto);

                    // Preparar a query
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

                    // Veriricar contador
                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        _rateio = new Hcrp.Framework.Classes.RateioMaterialGrupoCencusto();

                        _rateio.CodCenCusto = Convert.ToString(dr["COD_CENCUSTO"]);
                        _rateio.CodMaterial = Convert.ToString(dr["COD_MATERIAL"]);
                        _rateio.CodGrupoCentroCusto = Convert.ToInt32(dr["COD_GRUPO_SICH"]);
                        _rateio.PctRateio = Convert.ToInt32(dr["PCT_RATEIO"]);
                        _listaDeRetorno.Add(_rateio);
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaDeRetorno;
        }

        public void Adicionar(Hcrp.Framework.Classes.RateioMaterialGrupoCencusto _rateio)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abre conexão
                    ctx.Open();

                    // Configurar comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("RATEIO_MAT_GP_CENCUSTO");

                    // Adicionar parametros
                    comando.Params["COD_CENCUSTO"] = _rateio.CodCenCusto;
                    comando.Params["COD_MATERIAL"] = _rateio.CodMaterial;
                    comando.Params["COD_GRUPO_SICH"] = _rateio.CodGrupoCentroCusto;
                    comando.Params["PCT_RATEIO"] = _rateio.PctRateio;

                    // Executar o comando
                    ctx.ExecuteInsert(comando);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Remover(Hcrp.Framework.Classes.RateioMaterialGrupoCencusto _rateio)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abre conexão
                    ctx.Open();

                    // Configurar comando
                    Hcrp.Infra.AcessoDado.CommandConfig comando = new Hcrp.Infra.AcessoDado.CommandConfig("RATEIO_MAT_GP_CENCUSTO");

                    // Adicionar parametros
                    comando.Params["COD_CENCUSTO"] = _rateio.CodCenCusto;
                    comando.Params["COD_MATERIAL"] = _rateio.CodMaterial;
                    comando.Params["COD_GRUPO_SICH"] = _rateio.CodGrupoCentroCusto;

                    // Executar o comando
                    ctx.ExecuteDelete(comando);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
