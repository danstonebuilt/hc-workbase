using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Infra.Util
{
    public class Cripto
    {
        public string Criptografa(string cChave)
        {
            string cChaveCripto;
            Byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(cChave);
            cChaveCripto = Convert.ToBase64String(b);
            return cChaveCripto;
        }

        public string Decriptografa(string cChaveCripto)
        {
            string cChaveDecripto;
            Byte[] b = Convert.FromBase64String(cChaveCripto);
            cChaveDecripto = System.Text.ASCIIEncoding.ASCII.GetString(b);
            return cChaveDecripto;
        }
    }
}
