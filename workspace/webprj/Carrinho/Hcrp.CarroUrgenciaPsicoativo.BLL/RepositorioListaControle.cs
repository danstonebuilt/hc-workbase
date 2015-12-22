using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class RepositorioListaControle
    {
        #region Métodos

        /// <summary>
        /// Obter por instituto.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioListaControle> ObterPorInstituto(int codInstituto, bool listarInativos = false)
        {
            return new DAL.RepositorioListaControle().ObterPorInstituto(codInstituto, listarInativos);
        }

        /// <summary>
        /// Obter por id.
        /// </summary>        
        public Entity.RepositorioListaControle ObterPorId(Int64 seqRepositorio)
        {
            return new DAL.RepositorioListaControle().ObterPorId(seqRepositorio);
        }

        /// <summary>
        /// Obter repositorio com medicamentos a vencer.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioListaControle> ObterRepositorioComMaterialAVencer(int codInstituto, int qtdDiasVencer)
        {
            return new DAL.RepositorioListaControle().ObterRepositorioComMaterialAVencer(codInstituto, qtdDiasVencer);
        }

        public void AdicionarRepositorio(Entity.RepositorioListaControle reposit)
        {
            new DAL.RepositorioListaControle().AdicionarRepositorio(reposit);
        }

        public void AtualizarItem(Entity.RepositorioListaControle reposit)
        {
            new DAL.RepositorioListaControle().AtualizarItem(reposit);
        }

        public void AtivarOuInativar(bool EhPraAtivar, long seqRepositorio)
        {
            new DAL.RepositorioListaControle().AtivarOuInativar(EhPraAtivar, seqRepositorio);
        }

        /// <summary>
        /// Obter os centros de custo associados com o repositório
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioCentroCusto> ObterCentrosDeCustoDoRepositorio(Int64 seqRepositorio)
        {
            return new DAL.RepositorioListaControle().ObterCentrosDeCustoDoRepositorio(seqRepositorio);
        }

        /// <summary>
        /// Obter por instituto e centro de custo do usuário logado
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.RepositorioListaControle> ObterPorInstitutoCentroCustoUsuarioLogado(int codInstituto, string roles)
        {
            return new DAL.RepositorioListaControle().ObterPorInstitutoCentroCustoUsuarioLogado(codInstituto, roles);
        }

        #endregion
    }
}
