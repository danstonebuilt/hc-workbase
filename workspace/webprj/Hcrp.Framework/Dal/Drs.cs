using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public class Drs : Hcrp.Framework.Classes.Drs
    {
        public Hcrp.Framework.Classes.Drs BuscarDrsCodigo(int codigo)
        {
            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT COD_DIR, IDF_ATIVO, NUM_DIR, NOM_DIR, IDF_ABRANGENCIA_COMPLEXO_HC " + Environment.NewLine);
                    sb.Append(" FROM DIR " + Environment.NewLine);
                    sb.Append(" WHERE COD_DIR = :COD_DIR " + Environment.NewLine);                    
                    
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["COD_DIR"] = codigo;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        this.Codigo = Convert.ToInt32(dr["COD_DIR"]);
                        this.Ativa = Convert.ToString(dr["IDF_ATIVO"]) == "S";
                        this.Numero = Convert.ToString(dr["NUM_DIR"]);
                        this.Nome = Convert.ToString(dr["NOM_DIR"]);
                        this.FazParteComplexoHc = Convert.ToString(dr["IDF_ABRANGENCIA_COMPLEXO_HC"]) == "S";
                    }
                }
                return this;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Hcrp.Framework.Classes.Drs> BuscarDrsUsuario(Hcrp.Framework.Classes.UsuarioConexao u)
        {
            try
            {
                List<Hcrp.Framework.Classes.Drs> l = new List<Hcrp.Framework.Classes.Drs>();

                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();

                    StringBuilder sb = new StringBuilder();
                    sb.Append(" SELECT A.COD_DIR, A.IDF_ATIVO, A.NUM_DIR, A.NOM_DIR, A.IDF_ABRANGENCIA_COMPLEXO_HC " + Environment.NewLine);
                    sb.Append(" FROM DIR A, USUARIO_DIR B" + Environment.NewLine);
                    sb.Append(" WHERE B.NUM_USER_BANCO = :NUM_USER_BANCO " + Environment.NewLine);
                    sb.Append("   AND A.COD_DIR = B.COD_DIR " + Environment.NewLine);

                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                    query.Params["NUM_USER_BANCO"] = u.NumUserBanco;

                    ctx.ExecuteQuery(query);

                    // Cria objeto de material
                    OracleDataReader dr = ctx.Reader as OracleDataReader;

                    while (dr.Read())
                    {
                        Hcrp.Framework.Classes.Drs d = new Hcrp.Framework.Classes.Drs();
                        d.Codigo = Convert.ToInt32(dr["COD_DIR"]);
                        d.Ativa = Convert.ToString(dr["IDF_ATIVO"]) == "S";
                        d.Numero = Convert.ToString(dr["NUM_DIR"]);
                        d.Nome = Convert.ToString(dr["NOM_DIR"]);
                        d.FazParteComplexoHc = Convert.ToString(dr["IDF_ABRANGENCIA_COMPLEXO_HC"]) == "S";
                        l.Add(d);
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
