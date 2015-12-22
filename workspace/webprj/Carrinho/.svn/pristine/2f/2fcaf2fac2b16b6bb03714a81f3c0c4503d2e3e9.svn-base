using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class ListaControle
    {
        /// <summary>
        /// Obter lista de controle por cod instituto e status(ativa ou inativa)
        /// </summary>
        /// <param name="codInstituto"></param>
        /// <returns></returns>
        public List<Entity.ListaControle> ObterPorInstituto(int codInstituto,string status)
        {
            return new DAL.ListaControle().ObterPorInstituto(codInstituto,status);
        }

        /// <summary>
        /// Obter lista de controle por cod instituto
        /// </summary>
        /// <param name="codInstituto"></param>
        /// <returns></returns>
        public List<Entity.ListaControle> ObterPorInstituto(int codInstituto)
        {
            return new DAL.ListaControle().ObterPorInstituto(codInstituto);
        }

        /// <summary>
        /// Adicionar lista de controle
        /// </summary>
        /// <param name="listaControle"></param>
        /// <returns></returns>
        public long Adicionar(Entity.ListaControle listaControle, out bool naoInseriu)
        {
            return new DAL.ListaControle().Adicionar(listaControle, out naoInseriu);
        }

        /// <summary>
        /// Ativar ou inativar
        /// </summary>
        /// <param name="ehPraAtivar"></param>
        /// <param name="seqListaControle"></param>
        public void AtivarOuInativar(bool ehPraAtivar, long seqListaControle, int numUsuarioAlteracao)
        {
            new DAL.ListaControle().AtivarOuInativar(ehPraAtivar, seqListaControle, numUsuarioAlteracao);
        }
    }
}
