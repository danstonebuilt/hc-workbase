using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class HistoricoLacreRepositorio
    {
        #region Métodos

        /// <summary>
        /// Excluir transacionado.
        /// </summary>
        public void ExcluirPorlacreRepositorioTrans(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, Int64 seqLacreRepositorio)
        {
            new DAL.HistoricoLacreRepositorio(transacao).ExcluirPorlacreRepositorioTrans(seqLacreRepositorio);
        }

        /// <summary>
        /// Obter ultimo registro de historico.
        /// </summary>
        public Entity.HistoricoLacreRepositorio ObterUltimoRegistroDeHistorico(Int64 seqLacreRepositorio)
        {
            return new DAL.HistoricoLacreRepositorio().ObterUltimoRegistroDeHistorico(seqLacreRepositorio);
        }

        /// <summary>
        /// Adicionar.
        /// </summary>        
        public void Adicionar(Entity.HistoricoLacreRepositorio histLacreRep)
        {
            new DAL.HistoricoLacreRepositorio().Adicionar(histLacreRep);
        }

        /// <summary>
        /// Adicionar.
        /// </summary>        
        public void AdicionarTrans(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, Entity.HistoricoLacreRepositorio histLacreRep)
        {
            new DAL.HistoricoLacreRepositorio().AdicionarTrans(transacao,histLacreRep);
        }

        /// <summary>
        /// Obter históricos por seq lacre repositorio.
        /// </summary>
        public List<Entity.HistoricoLacreRepositorio> ObterPorSeqLacreRepositorio(Int64 seqLacreRepositorio, bool historicoDeChecagemConferencia)
        {
            return new DAL.HistoricoLacreRepositorio().ObterPorSeqLacreRepositorio(seqLacreRepositorio, historicoDeChecagemConferencia);
        }

        #endregion
    }
}
