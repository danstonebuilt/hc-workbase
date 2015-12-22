using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    class Especialidade : Hcrp.Framework.Classes.Especialidade
    {
        public Especialidade BuscaEspecialidadeCodigo(int codEspecialidade)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COD_ESPECIALIDADE_HC, SGL_ESPECIALIDADE_HC, NOM_ESPECIALIDADE_HC " + Environment.NewLine);
                    sb.Append(" FROM ESPECIALIDADE_HC " + Environment.NewLine);
                    sb.Append(" WHERE COD_ESPECIALIDADE_HC = :COD_ESPECIALIDADE_HC " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_ESPECIALIDADE_HC"] = codEspecialidade;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        this.Codigo = Convert.ToInt32(dr["COD_ESPECIALIDADE_HC"]);
                        this.Sigla = Convert.ToString(dr["SGL_ESPECIALIDADE_HC"]);
                        this.Nome = Convert.ToString(dr["NOM_ESPECIALIDADE_HC"]);
                    }
                }
                return this;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Hcrp.Framework.Classes.Especialidade> BuscaEspecialidadesSigla(string sigla)
        {
            List<Hcrp.Framework.Classes.Especialidade> l = new List<Hcrp.Framework.Classes.Especialidade>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.Append(" SELECT A.COD_ESPECIALIDADE_HC, A.NOM_ESPECIALIDADE_HC, A.SGL_ESPECIALIDADE_HC FROM ESPECIALIDADE_HC A " + Environment.NewLine);
                    sb.Append(" WHERE A.SGL_ESPECIALIDADE_HC LIKE :SGL_ESPECIALIDADE_HC " + Environment.NewLine);
                    sb.Append(" ORDER BY A.NOM_ESPECIALIDADE_HC " + Environment.NewLine);
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    query.Params["SGL_ESPECIALIDADE_HC"] = "%" + sigla + "%";
                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.Especialidade e = new Hcrp.Framework.Classes.Especialidade();
                        e.Codigo = Convert.ToInt32(dr["COD_ESPECIALIDADE_HC"]);
                        e.Nome = Convert.ToString(dr["NOM_ESPECIALIDADE_HC"]);
                        e.Sigla = Convert.ToString(dr["SGL_ESPECIALIDADE_HC"]);
                        l.Add(e);
                    }
                }
                return l;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna lista de EspecialidadeHC
        /// </summary>
        /// <param name="pCodInstituto">Instituto</param>
        /// <param name="pCodTipoAtendimento">Tipo Atendimento</param>
        /// <param name="pSglEspecialidadeHC">Sigla da Especialidade</param>
        /// <returns></returns>
        public static List<Hcrp.Framework.Entity.EspecialidadeHC> getEspecialidadeDDL(int? pCodInstituto, int? pCodTipoAtendimento, string pSglEspecialidadeHC = "")
        {
            List<Hcrp.Framework.Entity.EspecialidadeHC> result = new List<Hcrp.Framework.Entity.EspecialidadeHC>();
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(" SELECT E.COD_ESPECIALIDADE_HC, E.NOM_ESPECIALIDADE_HC, E.SGL_ESPECIALIDADE_HC ");
                    sb.AppendLine(" FROM ESPECIALIDADE_HC E ");
                    sb.AppendLine(" WHERE 1 = 1");

                    if ((pCodInstituto.HasValue) || (pCodTipoAtendimento.HasValue))
                    {
                        sb.AppendLine(" AND EXISTS ( SELECT COD_ESPECIALIDADE_HC FROM ESPEC_INSTITUTO_ATENDIMENTO EIA2 ");
                        sb.AppendLine("               WHERE EIA2.COD_ESPECIALIDADE_HC = E.COD_ESPECIALIDADE_HC");
                        if (pCodInstituto.HasValue) { sb.AppendLine("       AND EIA2.COD_INSTITUTO        = " + pCodInstituto.Value.ToString()); }
                        if (pCodTipoAtendimento.HasValue) { sb.AppendLine(" AND EIA2.COD_TIPO_ATENDIMENTO = " + pCodTipoAtendimento.Value.ToString()); }
                        sb.AppendLine("           )");
                    }

                    if (!string.IsNullOrEmpty(pSglEspecialidadeHC))
                    {
                        sb.AppendLine(" AND E.SGL_ESPECIALIDADE_HC = :SGL_ESPECIALIDADE_HC ");
                    }


                    sb.Append(" ORDER BY E.NOM_ESPECIALIDADE_HC " + Environment.NewLine);
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());
                    if (!string.IsNullOrEmpty(pSglEspecialidadeHC))
                    {
                        query.Params["SGL_ESPECIALIDADE_HC"] = pSglEspecialidadeHC.Trim().ToUpper();
                    }
                    ctx.ExecuteQuery(query);
                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;
                    while (dr.Read())
                    {
                        result.Add(new Entity.EspecialidadeHC(
                            Hcrp.Framework.Infra.Util.DataReader.GetDataValue<long>(dr, "COD_ESPECIALIDADE_HC", 0),
                            Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "SGL_ESPECIALIDADE_HC", ""),
                            Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "NOM_ESPECIALIDADE_HC", "")));
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<Hcrp.Framework.Entity.EspecialidadeHC> getEspecialidadeGrid(int pCodInstituto, int pCodTipoAtendimento, string pSglEspecialidadeHCLike = "", string pNomEspecialidadeHCLike = "" )
        {
            List<Hcrp.Framework.Entity.EspecialidadeHC> result = new List<Hcrp.Framework.Entity.EspecialidadeHC>();

            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();

                    sb.AppendLine(" SELECT E.COD_ESPECIALIDADE_HC, E.NOM_ESPECIALIDADE_HC, E.SGL_ESPECIALIDADE_HC ");
                    sb.AppendLine(" FROM ESPECIALIDADE_HC E ");
                    sb.AppendLine(" WHERE ");
                    sb.AppendLine(" EXISTS ( SELECT COD_ESPECIALIDADE_HC FROM ESPEC_INSTITUTO_ATENDIMENTO EIA2 WHERE EIA2.COD_INSTITUTO = :COD_INSTITUTO AND EIA2.COD_TIPO_ATENDIMENTO = :COD_TIPO_ATENDIMENTO AND EIA2.COD_ESPECIALIDADE_HC = E.COD_ESPECIALIDADE_HC )");

                    if (!string.IsNullOrEmpty(pSglEspecialidadeHCLike))
                    {
                        sb.AppendLine(" AND E.SGL_ESPECIALIDADE_HC LIKE :SGL_ESPECIALIDADE_HC ");
                    }

                    if (!string.IsNullOrEmpty(pNomEspecialidadeHCLike))
                    {
                        sb.AppendLine(" AND E.NOM_ESPECIALIDADE_HC LIKE :NOM_ESPECIALIDADE_HC ");

                    }

                    sb.Append(" ORDER BY E.NOM_ESPECIALIDADE_HC " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_INSTITUTO"] = pCodInstituto;
                    query.Params["COD_TIPO_ATENDIMENTO"] = pCodTipoAtendimento;

                    if (!string.IsNullOrEmpty(pSglEspecialidadeHCLike))
                    {
                        query.Params["SGL_ESPECIALIDADE_HC"] = "%" + pSglEspecialidadeHCLike.Trim().ToUpper() + "%" ;
                    }

                    if (!string.IsNullOrEmpty(pNomEspecialidadeHCLike))
                    {
                        query.Params["NOM_ESPECIALIDADE_HC"] = "%" + pNomEspecialidadeHCLike.Trim().ToUpper() + "%";
                    }

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        result.Add(new Entity.EspecialidadeHC(
                            Hcrp.Framework.Infra.Util.DataReader.GetDataValue<long>(dr, "COD_ESPECIALIDADE_HC", 0),
                            Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "SGL_ESPECIALIDADE_HC", ""),
                            Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "NOM_ESPECIALIDADE_HC", "")));
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
