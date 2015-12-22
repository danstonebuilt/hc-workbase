using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    /// <summary>
    /// Entidade integra aplicativo
    /// </summary>
    public class IntegraAplicativo
    {
        /// <summary>
        /// Chave do token gerado para gravar a integração - Esta chave deve ser
        /// passada por parâmetro para a aplicação que consumirá os dados
        /// </summary>
        public string Chave { get; set; }

        /// <summary>
        /// Login do Oracle do usuário que está logado - No outro sistema com base na chave passada por parametro
        /// este valor será recuperado para gerar a nova string de conexão
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Senha do Oracle do usuário logado - No outro sistema com base na chave passada por parametro
        /// este valor será recuperado para gerar a nova string de conexão
        /// </summary>
        public string Senha { get; set; }

        /// <summary>
        /// Data e hora de validade do token - O tempo máximo será de 30 segundos para que o aplicativo consuma os dados 
        /// que foram passados por parametro, do contrário a aplicação irá gerar uma exception no caso da chave estar expirada.
        /// </summary>
        public DateTime DataValidadeToken { get { return DateTime.Now.AddSeconds(30); } }

        /// <summary>
        /// Parâmetros a serem passados para o outro aplicativo.
        /// </summary>
        public Dictionary<string, object> Parametros { get; set; }

        public IntegraAplicativo()
        {
            this.Parametros = new Dictionary<string, object>();
        }

        public IntegraAplicativo(string chave)
        {
            this.Parametros = new Dictionary<string, object>();
            this.Chave = chave;

            // Obter parametros desta chave
            new Dal.IntegracaoDeAplicativo(this).ValidaTokenECarregaOsParametros();
        }

        public IntegraAplicativo(string chave, bool CriptOracle)
        {
            this.Parametros = new Dictionary<string, object>();
            this.Chave = chave;

            // Obter parametros desta chave
            new Dal.IntegracaoDeAplicativo(this).ValidaTokenECarregaOsParametros(CriptOracle);
        }

        public void GerarChave()
        {
            new Dal.IntegracaoDeAplicativo(this).GerarToken();
        }

        public void GerarChave(bool CriptOracle)
        {
            new Dal.IntegracaoDeAplicativo(this).GerarToken(CriptOracle);
        }

        public int GetNumUserBanco()
        {
            return new Dal.IntegracaoDeAplicativo(this).GetNumUserBanco();
        }
    }
}
