using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class ItensListaControle
    {
        #region Métodos

        /// <summary>
        /// Obter itens por código de material e seq lacre repositório.
        /// </summary>        
        public Entity.ItensListaControle ObterItensPorCodMaterialESeqLacreRespositorio(Int64 seqRepositorio, string codMaterial)
        {
            return new DAL.ItensListaControle().ObterItensPorCodMaterialESeqLacreRespositorio(seqRepositorio, codMaterial);
        }

        /// <summary>
        /// Obter lista controle itens com materiais com código paginado.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle> ObterListaDeListaControleItemComMaterialComCodigoPaginado(Int64 seqLacreRepositorio,
                                                                                                                            string codigo,
                                                                                                                            string descricao,
                                                                                                                            int paginaAtual,
                                                                                                                            out int totalRegistro)
        {
            return new DAL.ItensListaControle().ObterListaDeListaControleItemComMaterialComCodigoPaginado(seqLacreRepositorio,
                                                                                                        codigo,
                                                                                                        descricao,
                                                                                                        Parametrizacao.Instancia().QuantidadeRegistroPagina,
                                                                                                        paginaAtual,
                                                                                                        out totalRegistro);
        }

        /// <summary>
        /// Obter itens por id.
        /// </summary>        
        public Entity.ItensListaControle ObterItensPorId(Int64 seqListaControleItem)
        {
            return new DAL.ItensListaControle().ObterItensPorId(seqListaControleItem);
        }

        /// <summary>
        /// Obter lista controle itens com materiais sem código.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle> ObterListaDeListaControleItemComMaterialSemCodigo(Int64 seqLacreRepositorio)
        {
            return new DAL.ItensListaControle().ObterListaDeListaControleItemComMaterialSemCodigo(seqLacreRepositorio);
        }

        /// <summary>
        /// Obter lista controle itens com materiais com código.
        /// somente itens com idf_ativo = 'S'
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.ItensListaControle> ObterListaDeListaControleItemComMaterialComCodigo(Int64 seqLacreRepositorio)
        {
            return new DAL.ItensListaControle().ObterListaDeListaControleItemComMaterialComCodigo(seqLacreRepositorio);
        }

        /// <summary>
        /// Adicionar itens na lista de controle
        /// </summary>
        /// <returns></returns>
        public long Adicionar(Entity.ItensListaControle itens)
        {
            if (itens.Material != null)
            {
                // Se verdadeiro o material ja existe na lista
                if (ObterMateriaL(itens.ListaControle.SeqListaControle, itens.Material.Codigo))
                    return 0;
            }
            else if (itens.TipoBem != null)
            {
                // Se verdadeiro o material ja existe na lista
                if (ObterTipoBem(itens.ListaControle.SeqListaControle, itens.TipoBem.CodigoTipoBem))
                    return 0;
            }
            //else
            //{
                return new DAL.ItensListaControle().Adicionar(itens);
            //}
        }

        /// <summary>
        /// Obter lista de itens para lista
        /// </summary>
        /// <param name="seqListaControle">Lista de controle</param>
        /// <param name="status">True - Inativos False - Ativos</param>
        /// <returns></returns>
        public List<Entity.ItensListaControle> ObterItensPorListaControle(long seqListaControle, bool status)
        {
            return new DAL.ItensListaControle().ObterItensPorListaControle(seqListaControle,status);
        }

        public void AtivarOuInativar(bool EhPraAtivar,long seqItemListaControle)
        {
            new DAL.ItensListaControle().AtivarOuInativar(EhPraAtivar, seqItemListaControle);
        }

        public long AtualizarItem(long seqItem, int quantidade)
        {
            return new DAL.ItensListaControle().AtualizarItem(seqItem, quantidade);
        }

        public void ImportarItens(long seqListaPara, long seqListaDe)
        {
            new DAL.ItensListaControle().ImportarItens(seqListaPara, seqListaDe);
        }

        /// <summary>
        /// Método que retorna verdadeiro se o material ja estava na lista
        /// </summary>
        /// <param name="seqLista">lista</param>
        /// <param name="codMaterial">código do materila</param>
        /// <returns>True - Já existe / False - Não existe</returns>
        public bool ObterMateriaL(long seqLista, string codMaterial)
        {
            return new DAL.ItensListaControle().ObterMateriaL(seqLista, codMaterial);
        }

        /// <summary>
        /// Método que retorna verdadeiro se o material ja estava na lista
        /// </summary>
        /// <param name="seqLista">lista</param>
        /// <param name="codTipoBem">código do materila</param>
        /// <returns>True - Já existe / False - Não existe</returns>
        public bool ObterTipoBem(long seqLista, long? codTipoBem)
        {
            return new DAL.ItensListaControle().ObterTipoBem(seqLista, codTipoBem);
        }

        /// <summary>
        /// Obter lista de itens da lista que estão não estão no repositório
        /// </summary>
        /// <param name="seqRepositorio">seqRepositorio</param>
        /// <returns></returns>
        public List<Entity.ItensListaControle> ObterItensEmFaltaNoRepositorio(long seqRepositorio)
        {
            return new DAL.ItensListaControle().ObterItensEmFaltaNoRepositorio(seqRepositorio);
        }

        #endregion
    }
}
