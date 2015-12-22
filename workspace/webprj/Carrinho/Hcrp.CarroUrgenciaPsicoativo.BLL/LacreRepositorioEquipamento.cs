using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class LacreRepositorioEquipamento
    {
        #region Métodos

        /// <summary>
        /// Excluir transacionado.
        /// </summary>
        public void ExcluirPorLacreRepositorioTrans(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, Int64 seqLacreRepositorio)
        {
            new DAL.LacreRepositorioEquipamento(transacao).ExcluirPorLacreRepositorioTrans(seqLacreRepositorio);
        }

        /// <summary>
        /// Adicionar.
        /// </summary>        
        public long Adicionar(Entity.LacreRepositorioEquipamento lacreRepEquipamento)
        {
            return new DAL.LacreRepositorioEquipamento().Adicionar(lacreRepEquipamento);
        }

        /// <summary>
        /// Obter por lacre repositorio.
        /// </summary>
        public List<Entity.LacreRepositorioEquipamento> ObterPorLacreRepositorio(Int64 seqLacreRepositorio)
        {
            return new DAL.LacreRepositorioEquipamento().ObterPorLacreRepositorio(seqLacreRepositorio);
        }

        /// <summary>
        /// Atualizar dados de teste do equipamento.
        /// </summary>        
        public void AtualizarDadosDeTesteDoEquipamento(Int64 seqLacreRepositorioEquipamento, DateTime dataDoTeste, int numUserTeste)
        {
            new DAL.LacreRepositorioEquipamento().AtualizarDadosDeTesteDoEquipamento(seqLacreRepositorioEquipamento, dataDoTeste, numUserTeste);
        }

        /// <summary>
        /// Inativar equipamento.
        /// </summary>        
        public void InativarEquipamento(Int64 seqLacreRepositorioEquipamento)
        {
            new DAL.LacreRepositorioEquipamento().InativarEquipamento(seqLacreRepositorioEquipamento);
        }

        /// <summary>
        /// Obter por id.
        /// </summary>
        public Entity.LacreRepositorioEquipamento ObterPorId(Int64 seqLacreRepositorioEquipamento)
        {
            return new DAL.LacreRepositorioEquipamento().ObterPorId(seqLacreRepositorioEquipamento);
        }

        #endregion
    }
}
