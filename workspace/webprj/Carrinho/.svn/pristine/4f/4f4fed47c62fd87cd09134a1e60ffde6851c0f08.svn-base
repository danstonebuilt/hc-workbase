using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Hcrp.Framework.Infra.Util;
using Hcrp.Infra.AcessoDado;

namespace Hcrp.CarroUrgenciaPsicoativo.DAL
{
    public class Perfil
    {
        /// <summary>
        /// Obter a lista de Perfil que o usuário logado possui
        /// baseado no SISTEMA e na INSTITUIÇÃO !!!
        /// </summary>
        /// <returns></returns>
        public List<Entity.Perfil> ObterOPerfilDoUsuarioLogado()
        {
            List<Entity.Perfil> _listaRetorno = new List<Entity.Perfil>();

            Entity.Perfil _perfil = null;

            int codInstituicao = Framework.Infra.Util.Parametrizacao.Instancia().CodInstituicao;


            int codSistema = Parametrizacao.Instancia().CodigoSistema;


            try
            {
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    // Abrir conexão
                    ctx.Open();

                    StringBuilder str = new StringBuilder();
                    str.AppendLine(" SELECT ");
                    str.AppendLine("        PE.COD_ROLE, ");
                    str.AppendLine("        PE.NOM_ROLE ");
                    str.AppendLine(" FROM USUARIO_ROLE UR, ");
                    str.AppendLine("      ACESSO.PERFIL PE, ");
                    str.AppendLine("      ACESSO.SISTEMA_PERFIL SP ");
                    str.AppendLine(" WHERE UR.NUM_USER_BANCO = FC_NUM_USER_BANCO "); // Usuario Logado
                    str.AppendLine(string.Format(" AND UR.COD_INST_SISTEMA = {0} ", codInstituicao)); // Parametro
                    str.AppendLine("               AND PE.COD_ROLE = UR.COD_ROLE ");
                    str.AppendLine("               AND SP.COD_ROLE = UR.COD_ROLE ");
                    str.AppendLine("               AND SP.COD_INST_SISTEMA = UR.COD_INST_SISTEMA ");
                    str.AppendLine(string.Format(" AND SP.COD_SISTEMA = {0} ", codSistema)); // Parametro
                    str.AppendLine(" GROUP BY ");
                    str.AppendLine(" PE.COD_ROLE, ");
                    str.AppendLine(" PE.NOM_ROLE ");

                    // Obter a lista de registros
                    ctx.ExecuteQuery(str.ToString());

                    IDataReader dr = ctx.Reader;

                    while (dr.Read())
                    {
                        _perfil = new Entity.Perfil();

                        if (dr["COD_ROLE"] != DBNull.Value)
                            _perfil.IdPerfil = Convert.ToInt64(dr["COD_ROLE"]);

                        if (dr["NOM_ROLE"] != DBNull.Value)
                            _perfil.Nome = dr["NOM_ROLE"].ToString();

                        _listaRetorno.Add(_perfil);

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return _listaRetorno;
        }
    }
}
