using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.Framework.Dal
{
	public class CidCapitulo : Hcrp.Framework.Classes.CidCapitulo
	{
		public List<Hcrp.Framework.Classes.CidCapitulo> BuscarTodosCapitulos()
		{
			Hcrp.Framework.Classes.CidCapitulo _cc = new Hcrp.Framework.Classes.CidCapitulo();
			List<Hcrp.Framework.Classes.CidCapitulo> _ccList = new List<Hcrp.Framework.Classes.CidCapitulo>();
			try
			{
				using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
				{

					// Cria a query
					StringBuilder str = new StringBuilder();

					str.AppendLine("SELECT CC.SEQ_CID_10_CAPITULO,");
					str.AppendLine("       CC.COD_CID_10_INICIO,");
					str.AppendLine("       CC.COD_CID_10_FINAL,");
					str.AppendLine("       CC.DSC_CAPITULO");
					str.AppendLine("  FROM CID_10_CAPITULO CC");

					// Preparar a query
					Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str.ToString());

					// Abre conexão
					ctx.Open();

					// Veriricar contador
					ctx.ExecuteQuery(query);

					IDataReader dr = ctx.Reader;

					while (dr.Read())
					{
						_cc = new Classes.CidCapitulo();

						if (dr["SEQ_CID_10_CAPITULO"] != DBNull.Value)
							_cc.Codigo = Convert.ToInt64(dr["SEQ_CID_10_CAPITULO"].ToString());

						if (dr["COD_CID_10_INICIO"] != DBNull.Value)
							_cc.CodigoCidInicio = dr["COD_CID_10_INICIO"].ToString();

						if (dr["COD_CID_10_FINAL"] != DBNull.Value)
							_cc.CodigoCidFim = dr["COD_CID_10_FINAL"].ToString();

						if (dr["DSC_CAPITULO"] != DBNull.Value)
							_cc.Descricao = dr["DSC_CAPITULO"].ToString();

						_ccList.Add(_cc);
					}
				}
			}
			catch (Exception)
			{
				throw;
			}
			return _ccList;
		}
	}
}
