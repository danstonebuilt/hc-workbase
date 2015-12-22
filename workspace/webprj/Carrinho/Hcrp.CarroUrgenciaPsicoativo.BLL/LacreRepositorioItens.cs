using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class LacreRepositorioItens
    {
        /// <summary>
        /// Obter por lacre repositorio.
        /// </summary>
        public List<Entity.LacreRepositorioItens> ObterPorLacreRepositorio001(Int64 seqLacreRepositorio, Int64 seqAtendimento)
        {
            return new DAL.LacreRepositorioItens().ObterPorLacreRepositorio001(seqLacreRepositorio, seqAtendimento);
        }

        /// <summary>
        /// Atualizar data de vencimento do lote.
        /// </summary>        
        public void AtualizarDataVencimentoDoLote(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, Int64 seqLacreRepositorioItens, DateTime dataVencimentoLote)
        {
            new DAL.LacreRepositorioItens(transacao).AtualizarDataVencimentoDoLote(seqLacreRepositorioItens, dataVencimentoLote);
        }

        /// <summary>
        /// Adicionar.
        /// </summary>        
        public long Adicionar(Entity.LacreRepositorioItens lacreRepItens)
        {
            return new DAL.LacreRepositorioItens().Adicionar(lacreRepItens);
        }

        /// <summary>
        /// Atualizar quantidade.
        /// </summary>        
        public void AtualizarQuantidadeUtilizada(Entity.LacreRepositorioItens lacreRepItens)
        {
            new DAL.LacreRepositorioItens().AtualizarQuantidadeUtilizada(lacreRepItens);
        }

        /// <summary>
        /// Obter por lacre repositorio.
        /// </summary>
        public List<Entity.LacreRepositorioItens> ObterPorLacreRepositorio(Int64 seqLacreRepositorio, Int64? seqAtendimento)
        {
            return new DAL.LacreRepositorioItens().ObterPorLacreRepositorio(seqLacreRepositorio, seqAtendimento);
        }

        /// <summary>
        /// Excluir.
        /// </summary>
        public void Excluir(Int64 seqLacreRepositorioItem)
        {
            new DAL.LacreRepositorioItens().Excluir(seqLacreRepositorioItem);
        }

        /// <summary>
        /// Verificar se existem lacre repositorios itens com quantidade disponível diferente da quantidade necessária.
        /// </summary>
        public bool VerificarSeExistemQtdDisponivelDiferenteDaQtdNecessaria(Int64 seqLacreRepositorio)
        {
            return new DAL.LacreRepositorioItens().VerificarSeExistemQtdDisponivelDiferenteDaQtdNecessaria(seqLacreRepositorio);
        }

        /// <summary>
        /// Verificar se o material já foi adicionado para o lacre repositorio itens.
        /// </summary>
        public bool VerificarSeOMaterialJahFoiAdicionadoParaLacreRepositorioItens(Int64 seqLacreRepositorio, Int64 seqItensListaControle, Int64? numLote)
        {
            return new DAL.LacreRepositorioItens().VerificarSeOMaterialJahFoiAdicionadoParaLacreRepositorioItens(seqLacreRepositorio, seqItensListaControle, numLote);
        }

        /// <summary>
        /// Obter as quantidades disponíveis para consumo saida.
        /// </summary>
        public List<Entity.QuantidadeRegistroConsumoSaida> ObterQuantidadeDisponivelParaConsumoSaida(Int64 seqLacreRepositorio, Int64 seqAtendimento)
        {
            return new DAL.LacreRepositorioItens().ObterQuantidadeDisponivelParaConsumoSaida(seqLacreRepositorio, seqAtendimento);
        }

        /// <summary>
        /// Obter para troca de material paginado.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens> ObterParaTrocaDematerialPaginado(Int32 codInstituto, Int64 seqRepositorio,
                                                                                                             int qtdDiasVencer,
                                                                                                             int paginaAtual,
                                                                                                             out int totalRegistro)
        {
            return new DAL.LacreRepositorioItens().ObterParaTrocaDematerialPaginado(codInstituto,seqRepositorio,
                                                                                    qtdDiasVencer,
                                                                                    Parametrizacao.Instancia().QuantidadeRegistroPagina,
                                                                                    paginaAtual,
                                                                                    out totalRegistro);
        }

        /// <summary>
        /// Obter para troca de material.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens> ObterParaTrocaDematerial(Int32 codInstituto, int qtdDiasVencer, long seqRepositorio)
        {
            return new DAL.LacreRepositorioItens().ObterParaTrocaDematerial(codInstituto, qtdDiasVencer, seqRepositorio);
        }

        /// <summary>
        /// Obter para requisição de material.
        /// Mostra somente os materias que estao com a quantidade necessaria diferente da quantidade disponivel
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorioItens> ObterParaRequisicaoDeMaterial(Int64 seqRepositorio, bool Ehsoro)
        {
            return new DAL.LacreRepositorioItens().ObterParaRequisicaoDeMaterial(seqRepositorio, Ehsoro);
        }

        /// <summary>
        /// Obter itens pelo lacre repositorio
        /// Só mostra itens com a quantidade disponivel maior que 0, afinal se 0 quer dizer que o item ja foi consumido e não esta mais no lacre repositorio
        /// </summary>
        public List<Entity.LacreRepositorioItens> ObterItensNoLacreRepositorio(Int64 seqLacreRepositorio)
        {
            return new DAL.LacreRepositorioItens().ObterItensNoLacreRepositorio(seqLacreRepositorio);
        }

        public string RequisitarItensParaORepositorio(Int64 seqLacreRepositorio, bool ehSoro)
        {
            return new DAL.LacreRepositorioItens().RequisitarItensParaORepositorio(seqLacreRepositorio, ehSoro);
        }
    }
}
