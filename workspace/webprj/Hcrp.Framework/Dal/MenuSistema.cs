using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Hcrp.Infra.AcessoDado;
using Hcrp.Infra.Util;
using Oracle.DataAccess.Client;

namespace Hcrp.Framework.Dal
{
    public partial class MenuSistema
    {
        #region métodos

        //VarFixa de consulta Data Access
        public static List<Hcrp.Framework.Entity.MenuSistema> getMenuSistema(long pNumeroUsuarioBanco, int pCodigoInstituto, int pCodigoSistema, bool pListarAtivos = true, bool pListarInativos = false, bool pListarNaoMenus = false)
        {
            List<Hcrp.Framework.Entity.MenuSistema> result = new List<Hcrp.Framework.Entity.MenuSistema>();

            try
            {
                using (Contexto ctx = new Contexto())
                {
                    //Abrir Conexão com o Banco.
                    ctx.Open();


                    using (OracleCommand com = new OracleCommand())
                    {
                        com.Connection = ctx.Conn as OracleConnection;
                        com.CommandType = CommandType.StoredProcedure;
                        com.CommandText = "ACESSO.PROC_MENU_SISTEMA_USUARIO";

                        com.Parameters.Add("P_NUM_USER_BANCO", OracleDbType.Int64, ParameterDirection.Input).Value = pNumeroUsuarioBanco;
                        com.Parameters.Add("P_COD_SISTEMA", OracleDbType.Int64, ParameterDirection.Input).Value = pCodigoSistema;
                        com.Parameters.Add("P_COD_INST_SISTEMA", OracleDbType.Int64, ParameterDirection.Input).Value = pCodigoInstituto;

                        com.Parameters.Add("CS_MENU", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        IDataReader dr = com.ExecuteReader();

                        try
                        {
                            while (dr.Read())
                            {
                                if (Convert.ToString(dr["IDF_MENU"]).ToUpper() == "N" && !pListarNaoMenus)
                                {
                                    continue;
                                }

                                if (((Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "IDF_ATIVO", "A") == "A") && (pListarAtivos)) ||
                                     ((Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "IDF_ATIVO", "A") == "I") && (pListarInativos)))
                                {

                                    Hcrp.Framework.Entity.MenuSistema record = new Hcrp.Framework.Entity.MenuSistema();

                                    record.cod_programa = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "COD_PROGRAMA", 0);
                                    record.cod_programa_pai = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "COD_PROGRAMA_PAI", 0);
                                    record.dsc_programa = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "DSC_PROGRAMA", "");
                                    record.level = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<int>(dr, "LEVEL", 0);
                                    record.idf_menu = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "IDF_MENU", "");
                                    record.nom_exibicao_programa = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "NOM_EXIBICAO_PROGRAMA", "");

                                    if (string.IsNullOrEmpty(record.nom_exibicao_programa))
                                    {
                                        record.nom_exibicao_programa = record.dsc_programa;
                                    }

                                    record.num_ordem = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<long>(dr, "NUM_ORDEM", 0);
                                    record.nom_sistema = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "NOM_SISTEMA", "");
                                    record.caminho = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "CAMINHO", "");
                                    record.nom_programa = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "NOM_PROGRAMA", "");
                                    record.dta_criacao_programa = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<DateTime>(dr, "DTA_CRIACAO_PROGRAMA");
                                    record.idf_ativo = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "IDF_ATIVO", "");
                                    record.dsc_pagina_web = Hcrp.Framework.Infra.Util.DataReader.GetDataValue<string>(dr, "DSC_PAGINA_WEB", "");

                                    result.Add(record);
                                }
                            }

                        }
                        finally
                        {
                            dr.Close();
                            dr.Dispose();

                        }


                    }

                }

            }
            catch (Exception err)
            {
                throw err;
            }

            return result;
        }
        #endregion

    }
}

