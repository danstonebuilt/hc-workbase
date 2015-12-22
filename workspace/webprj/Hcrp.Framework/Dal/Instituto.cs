using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Dal
{
    class Instituto : Hcrp.Framework.Classes.Instituto
    {
        public Instituto BuscarInstituto(string ip)
        {            
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                ctx.Open();
                string sql;

                // todo:RETIRAR
                if ((ip == "127.0.0.1") || (ip == "::1"))  ip = "10.165.100.76";
                
                sql = string.Concat("SELECT GENERICO.FCN_BUSCA_INST_SISTEMAIP('", ip, "',2) COD_INSTITUTO_IP, GENERICO.FCN_BUSCA_INST_SISTEMAIP('", ip, "',1) COD_INST_SISTEMA, NOM_INSTITUTO FROM INSTITUTO WHERE COD_INSTITUTO = GENERICO.FCN_BUSCA_INST_SISTEMAIP('", ip, "',2) ");

                Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sql);
                ctx.ExecuteQuery(query);
                
                while (ctx.Reader.Read())
                {
                    if (ctx.Reader["COD_INSTITUTO_IP"] != DBNull.Value)
                    {
                        this.CodInstituto = Convert.ToInt32(ctx.Reader["COD_INSTITUTO_IP"]);
                        this.CodInstSistema = Convert.ToInt32(ctx.Reader["COD_INST_SISTEMA"]);
                        this.NomeInstituto = Convert.ToString(ctx.Reader["NOM_INSTITUTO"]);
                    }
                }
            }
            return this;
        }

        public Instituto BuscarInstitutoCodigo(int codInstituto)
        {
            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                ctx.Open();

                StringBuilder sb = new StringBuilder();

                sb.Append(" SELECT COD_INSTITUTO, NOM_INSTITUTO, COD_INST_SISTEMA, COD_PROTOCOLO_ATENDIMENTO " + Environment.NewLine);
                sb.Append(" FROM INSTITUTO " + Environment.NewLine);
                sb.Append(" WHERE COD_INSTITUTO = :COD_INSTITUTO " + Environment.NewLine);

                Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                query.Params["COD_INSTITUTO"] = codInstituto;
                ctx.ExecuteQuery(query);

                while (ctx.Reader.Read())
                {
                    if (ctx.Reader["COD_INSTITUTO"] != DBNull.Value)
                    {
                        this.CodInstituto = Convert.ToInt32(ctx.Reader["COD_INSTITUTO"]);
                        this.CodInstSistema = Convert.ToInt32(ctx.Reader["COD_INST_SISTEMA"]);
                        this.NomeInstituto = Convert.ToString(ctx.Reader["NOM_INSTITUTO"]);
                        if (ctx.Reader["COD_PROTOCOLO_ATENDIMENTO"] != DBNull.Value)
                            this._CodProtocoloAtendimento = Convert.ToInt32(ctx.Reader["COD_PROTOCOLO_ATENDIMENTO"]);
                    }
                }
            }
            return this;
        }

    	public List<Classes.Instituto> BuscarInstitutosPorInstituicao(int codigoInstituicao)
        {
            List<Classes.Instituto> lista = new List<Classes.Instituto>();

            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
            {
                ctx.Open();

                StringBuilder sb = new StringBuilder();

                sb.Append(" SELECT COD_INSTITUTO, NOM_INSTITUTO, COD_INST_SISTEMA, COD_PROTOCOLO_ATENDIMENTO " + Environment.NewLine);
                sb.Append(" FROM INSTITUTO " + Environment.NewLine);

                //caso NÃO for passado o codigo da instituicao, será utilizado a função que realiza a busca por Ip.
                if (codigoInstituicao.Equals(0))
                {
                    sb.Append(" WHERE COD_INST_SISTEMA = FCN_BUSCA_INST_SISTEMA(1) " + Environment.NewLine);
                }
                else
                {
                    sb.Append(" WHERE COD_INST_SISTEMA = :COD_INST_SISTEMA " + Environment.NewLine);
                }

                Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(sb.ToString());

                if (!codigoInstituicao.Equals(0))
                {
                    query.Params["COD_INST_SISTEMA"] = codigoInstituicao;
                }

                ctx.ExecuteQuery(query);

                while (ctx.Reader.Read())
                {
                    Classes.Instituto item = new Classes.Instituto();
                    if (ctx.Reader["COD_INSTITUTO"] != DBNull.Value)
                    {
                        item.CodInstituto = Convert.ToInt32(ctx.Reader["COD_INSTITUTO"]);
                        item.CodInstSistema = Convert.ToInt32(ctx.Reader["COD_INST_SISTEMA"]);
                        item.NomeInstituto = Convert.ToString(ctx.Reader["NOM_INSTITUTO"]);
                        if (ctx.Reader["COD_PROTOCOLO_ATENDIMENTO"] != DBNull.Value)
                            item._CodProtocoloAtendimento = Convert.ToInt32(ctx.Reader["COD_PROTOCOLO_ATENDIMENTO"]);
                    }
                    lista.Add(item);
                }
            }

            return lista;
        }
    }
}
