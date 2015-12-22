using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class LacreRepositorioUtilizacao
    {
        #region Métodos

        /// <summary>
        /// Atualizar ou adicionar quantidade de utilização por atendimento.
        /// </summary>        
        public void AtualizarOuAdicionarQuantidadeDeUtilizacao(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, Int64 seqLacreRepositorioItens, int qtdUtilizada, Int64 seqAtendimento, int numUserCadastro, string justificativaSemAtendimento)
        {
            new DAL.LacreRepositorioUtilizacao(transacao).AtualizarOuAdicionarQuantidadeDeUtilizacao(seqLacreRepositorioItens, qtdUtilizada, seqAtendimento, numUserCadastro, justificativaSemAtendimento);
        }

        /// <summary>
        /// Obter por seq lacre repositorio itens.
        /// </summary>                
        public List<Entity.LacreRepositorioUtilizacao> ObterPorSeqLacreRepositorioItem(Int64 seqLacreRepositorioItem)
        {
            return new DAL.LacreRepositorioUtilizacao().ObterPorSeqLacreRepositorioItem(seqLacreRepositorioItem);
        }

        /// <summary>
        /// Obter consumo do carrinho por lacre.
        /// </summary>                
        public List<Entity.LacreRepositorioUtilizacao> ObterConsumoPorLacre(Int32 codInstituto, Int64? seqRepositorio, DateTime? periodoInicial, DateTime? periodoFinal)
        {
            return new DAL.LacreRepositorioUtilizacao().ObterConsumoPorLacre(codInstituto, seqRepositorio, periodoInicial, periodoFinal);
        }

        public List<Entity.LacreRepositorioUtilizacao> ObterConsumoPorLacre(Int64 seqLacreRepositorio)
        {
            return new DAL.LacreRepositorioUtilizacao().ObterConsumoPorLacre(seqLacreRepositorio);
        }

        #endregion
    }
}
