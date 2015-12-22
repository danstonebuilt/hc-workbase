using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Infra.Util
{
    public class Util
    {

        public static string Encrypt(String sOriginal) // utilizado para senha do usuario no banco de dados
        {
            int t, i;
            string sResult = "";

            t = sOriginal.Trim().Length;
            if (t == 2)
                sResult = "J" + sOriginal.Substring(t - 1, 1) + "WP36" + sOriginal.Substring(0, 1) + "YJSIO";
            if (t == 3)
                sResult = "WRYH" + sOriginal.Substring(t - 1, 1) + "JW6F" + sOriginal.Substring(1, 1) + "BD";
            if (t == 4)
                sResult = "YKTIH" + sOriginal.Substring(2, 1) + "JE56" + sOriginal.Substring(0, 1) + sOriginal.Substring(1, 1);
            if (t == 5)
                sResult = sOriginal.Substring(t - 1, 1) + "QQCF" + sOriginal.Substring(1, 1) + sOriginal.Substring(0, 1) + "RDFT" + sOriginal.Substring(2, 1);
            if (t == 6)
                sResult = "RN" + sOriginal.Substring(1, 1) + "A" + sOriginal.Substring(t - 1, 1) + "4" + sOriginal.Substring(4, 1) + "ABB" + sOriginal.Substring(0, 1) + sOriginal.Substring(3, 1);
            if (t == 7)
                sResult = "SVN" + sOriginal.Substring(1, 1) + "7" + sOriginal.Substring(t - 1, 1) + "4" + sOriginal.Substring(4, 1) + "ABB" + sOriginal.Substring(0, 1) + sOriginal.Substring(3, 1);
            if (t == 8)
                sResult = "GRD" + sOriginal.Substring(1, 1) + "A" + sOriginal.Substring(t - 1, 1) + "4" + sOriginal.Substring(4, 1) + "ABB" + sOriginal.Substring(0, 1) + sOriginal.Substring(3, 1);
            if (t == 9)
                sResult = "IPC" + sOriginal.Substring(1, 1) + "H" + sOriginal.Substring(t - 1, 1) + "4" + sOriginal.Substring(4, 1) + "ABB" + sOriginal.Substring(0, 1) + sOriginal.Substring(3, 1);
            if (t >= 10)
                sResult = "PTS" + sOriginal.Substring(1, 1) + "K" + sOriginal.Substring(t - 1, 1) + "4" + sOriginal.Substring(4, 1) + "ABB" + sOriginal.Substring(0, 1) + sOriginal.Substring(3, 1);

            return sResult;
        }

        public static string CryptHCOracle(string ActionE, string SrcE)
        {
            try
            {
                string Resultado = String.Empty;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    string str = " SELECT FCN_CRYPTHC('" + SrcE + "', '" + ActionE + "') X FROM DUAL ";
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str);
                    ctx.ExecuteQuery(str);

                    // Cria objeto 
                    Oracle.DataAccess.Client.OracleDataReader dr = ctx.Reader as Oracle.DataAccess.Client.OracleDataReader;
                    while (dr.Read())
                    {
                        Resultado = Convert.ToString(dr["X"]);
                    }
                }
                return Resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string CryptHCOraclePerpetua(string ActionE, string SrcE)
        {
            try
            {
                string Resultado = String.Empty;
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Hcrp.Infra.AcessoDado.Contexto())
                {
                    ctx.Open();
                    string str = " SELECT FCN_CRYPTHC('" + SrcE + "', '" + ActionE + "', 0) X FROM DUAL ";
                    Hcrp.Infra.AcessoDado.QueryCommandConfig query = new Hcrp.Infra.AcessoDado.QueryCommandConfig(str);
                    ctx.ExecuteQuery(str);

                    // Cria objeto 
                    Oracle.DataAccess.Client.OracleDataReader dr = ctx.Reader as Oracle.DataAccess.Client.OracleDataReader;
                    while (dr.Read())
                    {
                        Resultado = Convert.ToString(dr["X"]);
                    }
                }
                return Resultado;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string CryptHC(string ActionE, string SrcE) 
        {
            DateTime dataServidor = Hcrp.Framework.Dal.DadosServidor.BuscaDataServidor();
            if (ActionE.Equals("E")) 
                return Crypt(ActionE, SrcE + "|" + Convert.ToString(dataServidor), "chavedeseguranca");
            else {
                string str = Crypt(ActionE, SrcE, "chavedeseguranca");
                string strdata = str.Substring(str.IndexOf("|") + 1);
                if (strdata.Trim().Equals(""))
                    return "";
                DateTime data = Convert.ToDateTime(strdata);
                string dado = str.Substring(0, str.IndexOf("|"));
                if ( (data > dataServidor.AddMinutes(-10)) && (data <= dataServidor.AddMinutes(10)) )
                    return dado;
                else
                    return ""; 
            }
        }

        public static string Crypt(string action, string src, string key) 
        {
            string dest = "";
            int keyPos = -1;
            int keyLen = key.Length;

            int Range = 256;
            if (action.Equals("E")) 
            {
                Random rnd = new Random (DateTime.Now.Millisecond);
                int offset = rnd.Next(Range);
                dest = string.Format("{0:X}",offset);
                for (int srcPos = 0; srcPos < src.Length; srcPos++)
                {
                    int SrcAsc = (Convert.ToInt32(src[srcPos]) + offset) % 255;

                    if (keyPos < keyLen-1)
                        keyPos = keyPos + 1;
                    else
                        keyPos = 0;

                    SrcAsc = SrcAsc ^ Convert.ToInt32(key[keyPos]);

                    dest += string.Format("{0:X}", SrcAsc);
                    offset = SrcAsc;
                }
            }
            else if (action.Equals("D")) 
            {                
                try 
                {
                    int offset = Convert.ToInt16("0x" + src.Substring(0, 2),16);
                    int srcPos = 2;
                    do {

                        int srcAsc = Convert.ToInt16("0x" + src.Substring(srcPos, 2), 16);
                        if (keyPos < keyLen-1)
                            keyPos = keyPos + 1;
                        else
                            keyPos = 0;

                        int tmpSrcAsc = srcAsc ^ Convert.ToInt32(key[keyPos]);     
       
                        if (tmpSrcAsc <= offset)
                            tmpSrcAsc = 255 + tmpSrcAsc - offset;
                        else
                            tmpSrcAsc = tmpSrcAsc - offset;

                        dest += Convert.ToChar(tmpSrcAsc);
                        offset = srcAsc;
                        srcPos = srcPos + 2;
      
                    } while (srcPos < src.Length);
                }
                catch (Exception e) {
                    dest = "";
                }
            }
            return dest;
        }
    }
}
