using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
	class Cid : Hcrp.Framework.Classes.Cid
	{
		public List<Hcrp.Framework.Classes.Cid> BuscarCids(String codCid, String descricaoCid, Int64? codCapitulo)
		{
			List<Hcrp.Framework.Classes.Cid> _cdList = new List<Hcrp.Framework.Classes.Cid>();
			try
			{
				using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
				{
					ctx.Open();
					StringBuilder sb = new StringBuilder();
					sb.Append(" SELECT C1.COD_CID_10,");
					sb.Append("        C1.DSC_CID_10,");
					sb.Append("        C1.IDF_CATEGORIA,");
					sb.Append("        C1.IDF_SUBCATEGORIA,");
					sb.Append("        C1.IDF_RESTRICAO_SEXO,");
					sb.Append("        C1.COD_CID_10_PAI,");
					sb.Append("        C1.SEQ_CID_10_CAPITULO,");
					sb.Append("        C1.IDF_ATIVO ");
					sb.Append("   FROM CID_10 C1");
					sb.Append("  WHERE 1 = 1");

					if (!string.IsNullOrEmpty(codCid))		{ sb.Append(string.Format("    AND C1.COD_CID_10 LIKE '%{0}%' ", codCid)); }
					if (!string.IsNullOrEmpty(descricaoCid)){ sb.Append(string.Format("    AND C1.DSC_CID_10 LIKE '%{0}%' ", descricaoCid.ToUpper())); }
					if (codCapitulo > 0)					{ sb.Append(string.Format("    AND C1.SEQ_CID_10_CAPITULO = {0} ", codCapitulo)); }

					Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

					ctx.ExecuteQuery(query);
					
					// Cria objeto de material
					OracleDataReader dr = ctx.Reader as OracleDataReader;
					
					while (dr.Read())
					{
						Hcrp.Framework.Classes.Cid _cd = new Hcrp.Framework.Classes.Cid();

						if (!string.IsNullOrEmpty(dr["COD_CID_10"].ToString())) { _cd.Codigo = Convert.ToString(dr["COD_CID_10"]); }
						if (!string.IsNullOrEmpty(dr["DSC_CID_10"].ToString())) { _cd.Descricao = Convert.ToString(dr["DSC_CID_10"]); }
						if (!string.IsNullOrEmpty(dr["IDF_CATEGORIA"].ToString())) { _cd.StatusCategoria = Convert.ToChar(dr["IDF_CATEGORIA"]); }
						if (!string.IsNullOrEmpty(dr["IDF_SUBCATEGORIA"].ToString())) { _cd.StatusSubCategoria = Convert.ToChar(dr["IDF_SUBCATEGORIA"]); }
						if (!string.IsNullOrEmpty(dr["IDF_RESTRICAO_SEXO"].ToString())) { _cd.RestricaoSexo = Convert.ToChar(dr["IDF_RESTRICAO_SEXO"]); }
						if (!string.IsNullOrEmpty(dr["COD_CID_10_PAI"].ToString())) { _cd.CodigoPai = Convert.ToString(dr["COD_CID_10_PAI"]); }
						if (!string.IsNullOrEmpty(dr["SEQ_CID_10_CAPITULO"].ToString())) { _cd.CodigoCapitulo = Convert.ToInt64(dr["SEQ_CID_10_CAPITULO"]); }

						_cdList.Add(_cd);
					}

					ctx.Close();
				}
				return _cdList;
			}
			catch (Exception)
			{
				throw;
			}
		}

        public List<Hcrp.Framework.Classes.Cid> BuscarCids(String codCid, String descricaoCid, Int64? codCapitulo, string Ativo)
        {
            List<Hcrp.Framework.Classes.Cid> _cdList = new List<Hcrp.Framework.Classes.Cid>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT C1.COD_CID_10,");
                    sb.Append("        C1.DSC_CID_10,");
                    sb.Append("        C1.IDF_CATEGORIA,");
                    sb.Append("        C1.IDF_SUBCATEGORIA,");
                    sb.Append("        C1.IDF_RESTRICAO_SEXO,");
                    sb.Append("        C1.COD_CID_10_PAI,");
                    sb.Append("        C1.SEQ_CID_10_CAPITULO,");
                    sb.Append("        C1.IDF_ATIVO ");
                    sb.Append("   FROM CID_10 C1");
                    sb.Append("  WHERE 1 = 1");

                    if (!string.IsNullOrEmpty(codCid)) { sb.Append(string.Format("    AND C1.COD_CID_10 LIKE '%{0}%' ", codCid)); }
                    if (!string.IsNullOrEmpty(descricaoCid)) { sb.Append(string.Format("    AND C1.DSC_CID_10 LIKE '%{0}%' ", descricaoCid.ToUpper())); }
                    if (codCapitulo > 0) { sb.Append(string.Format("    AND C1.SEQ_CID_10_CAPITULO = {0} ", codCapitulo)); }
                    if (!string.IsNullOrEmpty(Ativo)) { sb.Append(string.Format("    AND C1.IDF_ATIVO = '{0}' ", Ativo)); }

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.Cid _cd = new Hcrp.Framework.Classes.Cid();

                        if (!string.IsNullOrEmpty(dr["COD_CID_10"].ToString())) { _cd.Codigo = Convert.ToString(dr["COD_CID_10"]); }
                        if (!string.IsNullOrEmpty(dr["DSC_CID_10"].ToString())) { _cd.Descricao = Convert.ToString(dr["DSC_CID_10"]); }
                        if (!string.IsNullOrEmpty(dr["IDF_CATEGORIA"].ToString())) { _cd.StatusCategoria = Convert.ToChar(dr["IDF_CATEGORIA"]); }
                        if (!string.IsNullOrEmpty(dr["IDF_SUBCATEGORIA"].ToString())) { _cd.StatusSubCategoria = Convert.ToChar(dr["IDF_SUBCATEGORIA"]); }
                        if (!string.IsNullOrEmpty(dr["IDF_RESTRICAO_SEXO"].ToString())) { _cd.RestricaoSexo = Convert.ToChar(dr["IDF_RESTRICAO_SEXO"]); }
                        if (!string.IsNullOrEmpty(dr["COD_CID_10_PAI"].ToString())) { _cd.CodigoPai = Convert.ToString(dr["COD_CID_10_PAI"]); }
                        if (!string.IsNullOrEmpty(dr["SEQ_CID_10_CAPITULO"].ToString())) { _cd.CodigoCapitulo = Convert.ToInt64(dr["SEQ_CID_10_CAPITULO"]); }

                        _cdList.Add(_cd);
                    }

                    ctx.Close();
                }
                return _cdList;
            }
            catch (Exception)
            {
                throw;
            }
        }
	}
}
