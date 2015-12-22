using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.Framework.Infra.Util;

namespace Hcrp.Infra.Util
{
    public class QueryStringSegura
    {
        public string Url { get; set; }
        public Dictionary<string, string> Parametros { get; set; }

        public QueryStringSegura()
        {
            this.Parametros = new Dictionary<string, string>();
        }

        public string ObterUrlCriptografada()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.Url))
                    new ApplicationException("Url não informada");

                if (Parametros.Count == 0)
                    new ApplicationException("Parâmetros não informados");

                string parametroAux = "";

                foreach (var item in this.Parametros)
                {
                    if (string.IsNullOrWhiteSpace(parametroAux))
                    {
                        parametroAux = string.Concat(item.Key, "=", item.Value);
                    }
                    else
                    {
                        parametroAux = string.Concat(parametroAux, "&", item.Key, "=", item.Value);
                    }
                }

                return string.Concat(this.Url, "?", Encryption.EncryptText(parametroAux));

            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ObterOValorDoParametro(string nomeParametro)
        {
            string retorno = "";

            try
            {
                string url = "";
                url = System.Web.HttpContext.Current.Request.RawUrl;
                url = url.Substring(url.IndexOf('?') + 1);

                if (!string.IsNullOrWhiteSpace(url) && System.Web.HttpContext.Current.Request.QueryString.Count > 0)
                {
                    // *********************************************
                    // *********************************************
                    // *********************************************
                    // ATENÇÃO: POR FAVOR NÃO COMENTAR A LINHA ABAIXO, ESTA É UTILIZADA EM VÁRIOS SISTEMA QUE UTILIZAM
                    // A QUERYSTRING CRIPTOGRAFADA, SE FOR COMENTADA IRÁ AFETAR VÁRIOS SISTEMAS QUE UTILIZAM DESTA PARA 
                    // TRANSFERENCIAS DE DADOS ENTRE PÁGINAS
                    // POR LUIZ ANTONIO SGARGETA - TRINAPSE EM 22/10/2011
                    // *********************************************
                    // *********************************************
                    // *********************************************
                    url = Encryption.DecryptText(url);

                    string[] parametros = url.Split('&');
                    string[] parametroValor;

                    for (int i = 0; i <= parametros.Count() - 1; i++)
                    {
                        parametroValor = parametros[i].Split('=');

                        if (parametroValor.Length > 0)
                        {
                            if (string.Equals(parametroValor[0].Trim(), nomeParametro.Trim()))
                            {
                                retorno = parametroValor[1];
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return retorno;

        }
    }
}
