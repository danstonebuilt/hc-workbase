using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hcrp.Infra.AcessoDado;

namespace Hcrp.Framework.Dal
{
    /// <summary>
    /// Responsável por gerenciar as operações referentes a integração de aplicativos
    /// </summary>
    public class IntegracaoDeAplicativo 
    {
        private Classes.IntegraAplicativo IntegraAplicativo { get; set; }

        public IntegracaoDeAplicativo( Classes.IntegraAplicativo _integra)
        {
            this.IntegraAplicativo = _integra;
        }

        /// <summary>
        /// Gerar token de integração
        /// </summary>
        /// <returns>Chave gerada do token, esta deverá ser passada por querystring para a aplicação que consumirá os dados.</returns>
        public string GerarToken()
        {
            string _retorno = "";

            try
            {
                // Validar se algum paramatro foi informado
                if (this.IntegraAplicativo.Parametros.Count == 0)
                    throw new ApplicationException("Nenhum parâmetro de integração foi informado.");
                
                // Criar a chave
               this.IntegraAplicativo.Chave = this.CriarEObterChaveDoToken();

                // Criar os parâmetros
               if (!string.IsNullOrWhiteSpace(this.IntegraAplicativo.Chave))
               {
                   this.CriarOsParametros();
               }

            }
            catch (Exception)
            {
                throw;
            }

            return _retorno;

        }

        /// <summary>
        /// Gerar token de integração usando
        /// </summary>
        /// <returns>Chave gerada do token, esta deverá ser passada por querystring para a aplicação que consumirá os dados.</returns>
        public string GerarToken(bool CriptOracle)
        {
            if (!CriptOracle)
            {
                return GerarToken();
            }
            else
            {
                string _retorno = "";

                try
                {
                    // Validar se algum paramatro foi informado
                    if (this.IntegraAplicativo.Parametros.Count == 0)
                        throw new ApplicationException("Nenhum parâmetro de integração foi informado.");

                    // Criar a chave
                    this.IntegraAplicativo.Chave = this.CriarEObterChaveDoToken(true);

                    // Criar os parâmetros
                    if (!string.IsNullOrWhiteSpace(this.IntegraAplicativo.Chave))
                    {
                        this.CriarOsParametros();
                    }

                }
                catch (Exception)
                {
                    throw;
                }

                return _retorno;
            }
        }

        /// <summary>
        /// Obter os parâmetros com base na chave passada para a classe pai
        /// </summary>
        public void ValidaTokenECarregaOsParametros()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(this.IntegraAplicativo.Chave))
                {
                    try
                    {
                        // Buscar no banco
                        using (Hcrp.Infra.AcessoDado.Contexto ctx = new Contexto())
                        {
                            // Abrir conexão
                            ctx.Open();
                            QueryCommandConfig query;
                            IDataReader dr;
                            DateTime? dataValidadeToken = null;

                            query = new QueryCommandConfig(string.Format(" SELECT * FROM INTEGRA_APLICATIVO WHERE NUM_CHAVE = '{0}' ", this.IntegraAplicativo.Chave));

                            // Executar a query
                            ctx.ExecuteQuery(query);

                            // Obter os dados de retorno
                            dr = ctx.Reader;

                            while (dr.Read())
                            {

                                if (dr["NOM_USUARIO_BANCO"] != DBNull.Value)
                                    //this.IntegraAplicativo.Login = Infra.Util.Encryption.DecryptText(dr["NOM_USUARIO_BANCO"].ToString());
                                    //this.IntegraAplicativo.Login = HCRP.CommonLib.TUtil.CryptHC(null, "D",dr["NOM_USUARIO_BANCO"].ToString());
                                    this.IntegraAplicativo.Login = Hcrp.Framework.Infra.Util.Util.CryptHC("D", dr["NOM_USUARIO_BANCO"].ToString());

                                if (dr["DSC_SENHA_USUARIO"] != DBNull.Value)
                                    //this.IntegraAplicativo.Senha = Infra.Util.Encryption.DecryptText(dr["DSC_SENHA_USUARIO"].ToString());
                                    //this.IntegraAplicativo.Senha = HCRP.CommonLib.TUtil.CryptHC(null, "D", dr["DSC_SENHA_USUARIO"].ToString());
                                    this.IntegraAplicativo.Senha = Hcrp.Framework.Infra.Util.Util.CryptHC("D", dr["DSC_SENHA_USUARIO"].ToString());

                                if (dr["DTA_VALIDADE_TOKEN"] != DBNull.Value)
                                    dataValidadeToken = Convert.ToDateTime(dr["DTA_VALIDADE_TOKEN"]);

                                break;
                            }

                            // Validar o token
                            if (!dataValidadeToken.HasValue)
                                throw new ApplicationException("Token de integração inválido.");

                            if (dataValidadeToken.Value < DateTime.Now)
                                throw new ApplicationException("Token de integração expirado.");

                            // Configurar o comando
                            query = new QueryCommandConfig(string.Format(" SELECT NOM_PARAMETRO, DSC_VALOR FROM INTEGRA_APLICATIVO_ITEM WHERE NUM_CHAVE = '{0}' ", this.IntegraAplicativo.Chave));

                            // Executar a query
                            ctx.ExecuteQuery(query);

                            // Obter os dados de retorno
                            dr = ctx.Reader;

                            while (dr.Read())
                            {
                                if (dr["NOM_PARAMETRO"] != DBNull.Value && dr["DSC_VALOR"] != DBNull.Value)
                                    this.IntegraAplicativo.Parametros.Add(dr["NOM_PARAMETRO"].ToString(), dr["DSC_VALOR"]);
                            }

                        }

                        // Remove o token
                        this.DeletarToken();

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obter os parâmetros com base na chave passada para a classe pai
        /// </summary>
        public void ValidaTokenECarregaOsParametros(bool CriptOracle)
        {
            if (!CriptOracle)
            {
                ValidaTokenECarregaOsParametros();
            }
            else
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(this.IntegraAplicativo.Chave))
                    {
                        try
                        {
                            // Buscar no banco
                            using (Hcrp.Infra.AcessoDado.Contexto ctx = new Contexto())
                            {
                                // Abrir conexão
                                ctx.Open();
                                QueryCommandConfig query;
                                IDataReader dr;
                                DateTime? dataValidadeToken = null;

                                query = new QueryCommandConfig(string.Format(" SELECT * FROM INTEGRA_APLICATIVO WHERE NUM_CHAVE = '{0}' ", this.IntegraAplicativo.Chave));

                                // Executar a query
                                ctx.ExecuteQuery(query);

                                // Obter os dados de retorno
                                dr = ctx.Reader;

                                while (dr.Read())
                                {

                                    if (dr["NOM_USUARIO_BANCO"] != DBNull.Value)
                                        //this.IntegraAplicativo.Login = Infra.Util.Encryption.DecryptText(dr["NOM_USUARIO_BANCO"].ToString());
                                        //this.IntegraAplicativo.Login = HCRP.CommonLib.TUtil.CryptHC(null, "D",dr["NOM_USUARIO_BANCO"].ToString());
                                        this.IntegraAplicativo.Login = Hcrp.Framework.Infra.Util.Util.CryptHCOracle("D", dr["NOM_USUARIO_BANCO"].ToString());

                                    if (dr["DSC_SENHA_USUARIO"] != DBNull.Value)
                                        //this.IntegraAplicativo.Senha = Infra.Util.Encryption.DecryptText(dr["DSC_SENHA_USUARIO"].ToString());
                                        //this.IntegraAplicativo.Senha = HCRP.CommonLib.TUtil.CryptHC(null, "D", dr["DSC_SENHA_USUARIO"].ToString());
                                        this.IntegraAplicativo.Senha = Hcrp.Framework.Infra.Util.Util.CryptHCOracle("D", dr["DSC_SENHA_USUARIO"].ToString());

                                    if (dr["DTA_VALIDADE_TOKEN"] != DBNull.Value)
                                        dataValidadeToken = Convert.ToDateTime(dr["DTA_VALIDADE_TOKEN"]);

                                    break;
                                }

                                // Validar o token
                                if (!dataValidadeToken.HasValue)
                                    throw new ApplicationException("Token de integração inválido.");

                                if (dataValidadeToken.Value < DateTime.Now)
                                    throw new ApplicationException("Token de integração expirado.");

                                // Configurar o comando
                                query = new QueryCommandConfig(string.Format(" SELECT NOM_PARAMETRO, DSC_VALOR FROM INTEGRA_APLICATIVO_ITEM WHERE NUM_CHAVE = '{0}' ", this.IntegraAplicativo.Chave));

                                // Executar a query
                                ctx.ExecuteQuery(query);

                                // Obter os dados de retorno
                                dr = ctx.Reader;

                                while (dr.Read())
                                {
                                    if (dr["NOM_PARAMETRO"] != DBNull.Value && dr["DSC_VALOR"] != DBNull.Value)
                                        this.IntegraAplicativo.Parametros.Add(dr["NOM_PARAMETRO"].ToString(), dr["DSC_VALOR"]);
                                }

                            }

                            // Remove o token
                            this.DeletarToken();

                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int GetNumUserBanco()
        {
            try
            {
                // Buscar no banco
                using (Hcrp.Infra.AcessoDado.Contexto ctx = new Contexto())
                {
                    // Abrir conexão
                    ctx.Open();
                    QueryCommandConfig query;
                    IDataReader dr;

                    query = new QueryCommandConfig(string.Format("SELECT NUM_USER_BANCO FROM USUARIO WHERE NOM_USUARIO_BANCO = '{0}' ", this.IntegraAplicativo.Login));

                    // Executar a query
                    ctx.ExecuteQuery(query);

                    // Obter os dados de retorno
                    dr = ctx.Reader;

                    int numeroUserBanco = 0;

                    while (dr.Read())
                    {
                        if (dr["NUM_USER_BANCO"] != DBNull.Value)
                        {
                            numeroUserBanco = Convert.ToInt32(dr["NUM_USER_BANCO"]);
                            break;
                        }
                    }

                    return numeroUserBanco;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region Métodos Privados

        /// <summary>
        /// Criar e obter a chave do token
        /// </summary>
        /// <returns>Chave gerada pelo token</returns>
        private string CriarEObterChaveDoToken()
        {
            string _chave = Guid.NewGuid().ToString().Replace("-", "");

            try
            {
                using (Contexto ctx = new Contexto())
                {
                    // Abre conexão
                    ctx.Open();

                    // Configurar comando
                    CommandConfig comando = new CommandConfig("INTEGRA_APLICATIVO");

                    // Obter login e senha do cookie
                    Classes.ConfiguracaoSistema _config = new Classes.ConfiguracaoSistema();

                    // Adicionar parametros
                    comando.Params["NUM_CHAVE"] = _chave;
                    
                    //comando.Params["NOM_USUARIO_BANCO"] = Infra.Util.Encryption.EncryptText(this.IntegraAplicativo.Login);
                    //comando.Params["DSC_SENHA_USUARIO"] = Infra.Util.Encryption.EncryptText(this.IntegraAplicativo.Senha);

                    // No método "ValidaTokenECarregaOsParametros()" a decriptografia está sendo utilizada "CryptHC", então a criptografia
                    // deve utililizar o mesmo algoritmo, senão no momento de decriptografar ocorrerá erro.
                    comando.Params["NOM_USUARIO_BANCO"] = Hcrp.Framework.Infra.Util.Util.CryptHC("E", this.IntegraAplicativo.Login);
                    comando.Params["DSC_SENHA_USUARIO"] = Hcrp.Framework.Infra.Util.Util.CryptHC("E", this.IntegraAplicativo.Senha);                    
                    
                    comando.Params["DTA_VALIDADE_TOKEN"] = this.IntegraAplicativo.DataValidadeToken;

                    // Executar o comando
                    ctx.ExecuteInsert(comando);

                }
            }
            catch (Exception)
            {
                throw;
            }

            return _chave;
        }


        /// <summary>
        /// Criar e obter a chave do token usando função Oracle
        /// </summary>
        /// <returns>Chave gerada pelo token</returns>
        private string CriarEObterChaveDoToken(bool CriptOracle)
        {
            string _chave = Guid.NewGuid().ToString().Replace("-", "");

            try
            {
                using (Contexto ctx = new Contexto())
                {
                    // Abre conexão
                    ctx.Open();

                    // Configurar comando
                    CommandConfig comando = new CommandConfig("INTEGRA_APLICATIVO");

                    // Obter login e senha do cookie
                    Classes.ConfiguracaoSistema _config = new Classes.ConfiguracaoSistema();

                    // Adicionar parametros
                    comando.Params["NUM_CHAVE"] = _chave;

                    //comando.Params["NOM_USUARIO_BANCO"] = Infra.Util.Encryption.EncryptText(this.IntegraAplicativo.Login);
                    //comando.Params["DSC_SENHA_USUARIO"] = Infra.Util.Encryption.EncryptText(this.IntegraAplicativo.Senha);

                    // No método "ValidaTokenECarregaOsParametros()" a decriptografia está sendo utilizada "CryptHC", então a criptografia
                    // deve utililizar o mesmo algoritmo, senão no momento de decriptografar ocorrerá erro.
                    comando.Params["NOM_USUARIO_BANCO"] = Hcrp.Framework.Infra.Util.Util.CryptHCOracle("E", this.IntegraAplicativo.Login);
                    comando.Params["DSC_SENHA_USUARIO"] = Hcrp.Framework.Infra.Util.Util.CryptHCOracle("E", this.IntegraAplicativo.Senha);

                    comando.Params["DTA_VALIDADE_TOKEN"] = this.IntegraAplicativo.DataValidadeToken;

                    // Executar o comando
                    ctx.ExecuteInsert(comando);

                }
            }
            catch (Exception)
            {
                throw;
            }

            return _chave;
        }

        /// <summary>
        /// Criar os parametros da integração
        /// </summary>
        private void CriarOsParametros()
        {
            try
            {
                using (Contexto ctx = new Contexto())
                {
                    // Abre conexão
                    ctx.Open();

                    // Configurar comando
                    CommandConfig comando;

                    foreach (var item in this.IntegraAplicativo.Parametros)
                    {
                        comando = new CommandConfig("INTEGRA_APLICATIVO_ITEM");

                        // Adicionar parametros
                        comando.Params["NUM_CHAVE"] = this.IntegraAplicativo.Chave;
                        comando.Params["NOM_PARAMETRO"] = item.Key;
                        comando.Params["DSC_VALOR"] = item.Value;

                        // Executar o comando
                        ctx.ExecuteInsert(comando);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Deletar o token
        /// </summary>
        private void DeletarToken()
        {
            try
            {
                using (Contexto ctx = new Contexto())
                {
                    // Abre conexão
                    ctx.Open();

                    // Configurar comando
                    CommandConfig comando = new CommandConfig("INTEGRA_APLICATIVO_ITEM");

                    // Adicionar parametros
                    comando.Params["NUM_CHAVE"] = this.IntegraAplicativo.Chave;
                    
                    // Executar o comando
                    ctx.ExecuteDelete(comando);

                    // Configurar comando
                    comando = new CommandConfig("INTEGRA_APLICATIVO");

                    // Adicionar parametros
                    comando.Params["NUM_CHAVE"] = this.IntegraAplicativo.Chave;

                    // Executar o comando
                    ctx.ExecuteDelete(comando);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
