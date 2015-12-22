using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class Usuario
    {
        #region Métodos

        /// <summary>
        /// Obter assinatura do usuário conectado.
        /// </summary>        
        public DataView ObterAssinaturaDoUsuarioConectado()
        {
            return new DAL.Usuario().ObterAssinaturaDoUsuarioConectado();
        }

        /// <summary>
        /// Obter as roles do usuário logado.
        /// </summary>        
        public List<Entity.RoleUsuario> ObterAsRolesDoUsuarioLogado(int codInstituicao, int codSistema)
        {
            return new DAL.Usuario().ObterAsRolesDoUsuarioLogado(codInstituicao, codSistema);
        }

        /// <summary>
        /// O usuário logado está na role.
        /// </summary>        
        public bool UsuarioLogadoEstaNaRole(Int64 codRole)
        {
            bool estaNaRole = false;
            
            try
            {
                List<Entity.RoleUsuario> listRoleUsuario = new DAL.Usuario().
                    ObterAsRolesDoUsuarioLogado(Framework.Infra.Util.Parametrizacao.Instancia().CodInstituicao, Parametrizacao.Instancia().CodigoDoSistema);

                if (listRoleUsuario != null)
                {
                    var conta = (from contaL in listRoleUsuario
                                 where contaL.Codigo == codRole || contaL.Codigo == BLL.Parametrizacao.Instancia().CodigoDaRoleAdministrador
                                 select contaL).Count();

                    if (conta > 0)
                        estaNaRole = true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return estaNaRole;
        }

        /// <summary>
        /// Obter os usuários da role.
        /// </summary>        
        public List<Entity.Usuario> ObterOsUsuarioDaRole(Int64 codRole)
        {
            return new DAL.Usuario().ObterOsUsuarioDaRole(codRole);
        }

        /// <summary>
        /// Obter Gestores Do Repositorio
        /// </summary>        
        public List<Entity.Usuario> ObterOsUsuarioGestoresDoRepositorio(Int64 codRole, Int64 codigoRepositorio)
        {
            return new DAL.Usuario().ObterOsUsuarioGestoresDoRepositorio(codRole, codigoRepositorio);
        }

        #endregion
    }
}
