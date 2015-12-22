using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hcrp.CarroUrgenciaPsicoativo.Entity;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class LacreRepositorio
    {
        #region Métodos

        public Int64? ObterUltimoLacreRepositorio(Int64? seqRepositorio)
        {
            return new DAL.LacreRepositorio().ObterUltimoLacreRepositorio(seqRepositorio);
        }

        /// <summary>
        /// Obter por repositório.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio> ObterPorFiltro(Int64? seqLacreRepositorio, Int64? seqRepositorio, DateTime? periodoInicial, DateTime? periodoFinal)
        {
            return new DAL.LacreRepositorio().ObterPorFiltro(seqLacreRepositorio, seqRepositorio, periodoInicial, periodoFinal, Parametrizacao.Instancia().CodigoDaSituacaoLacrado);
        }

        /// <summary>
        /// Salvar.
        /// </summary>        
        public Int64 Adicionar(Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio lacreRepositorio)
        {
            return new DAL.LacreRepositorio().Adicionar(lacreRepositorio);
        }

        /// <summary>
        /// Obter por filtro paginado.
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio> ObterPorFiltroPaginado(Int64? seqLacreRepositorio,
                                                                                                 Int64? seqRepositorio,
                                                                                                 DateTime? periodoInicial,
                                                                                                 DateTime? periodoFinal,                                                                                                                                                                                                  
                                                                                                 int paginaAtual,
                                                                                                 out int totalRegistro)
        {
            return new DAL.LacreRepositorio().ObterPorFiltroPaginado(seqLacreRepositorio,
                                                                    seqRepositorio,
                                                                    periodoInicial,
                                                                    periodoFinal,
                                                                    Parametrizacao.Instancia().CodigoDaSituacaoLacrado,
                                                                    Parametrizacao.Instancia().QuantidadeRegistroPagina,                                                                    
                                                                    paginaAtual,
                                                                    out totalRegistro);
        }

        /// <summary>
        /// Excluir transacionado.
        /// </summary>
        public void ExcluirTrans(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, Int64 seqLacreRepositorio)
        {
            new DAL.LacreRepositorio(transacao).ExcluirTrans(seqLacreRepositorio);
        }

        /// <summary>
        /// Lacrar carrinho.
        /// </summary>        
        public void LacrarCarrinho(Int64 seqLacreRepositorio, Int64 numLacre, string numCaixaIntubacao, DateTime? dataAtualizacao)
        {
            new DAL.LacreRepositorio().LacrarCarrinho(seqLacreRepositorio, numLacre, numCaixaIntubacao, dataAtualizacao, Parametrizacao.Instancia().CodigoDaSituacaoLacrado);
        }

        /// <summary>
        /// Obter por id.
        /// </summary>        
        public Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio ObterPorId(Int64 seqLacreRepositorio)
        {
            return new DAL.LacreRepositorio().ObterPorId(seqLacreRepositorio);
        }

        public Hcrp.CarroUrgenciaPsicoativo.Entity.LacreRepositorio ObterPorIdTrans(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, Int64 seqLacreRepositorio)
        {
            return new DAL.LacreRepositorio().ObterPorIdTrans(transacao,seqLacreRepositorio);
        }

        /// <summary>
        /// Quebrar lacre carrinho.
        /// </summary>        
        public void QuebrarLacreCarrinho(Int64 seqLacreRepositorio, Int64 seqTipoOcorrencia)
        {
            new DAL.LacreRepositorio().QuebrarLacreCarrinho(seqLacreRepositorio, seqTipoOcorrencia, Parametrizacao.Instancia().CodigoDaSituacaoRompido);
        }

        /// <summary>
        /// Gerar lacre provisório.
        /// </summary>        
        public Int64 GerarLacreProvisorio(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, Int64 seqLacreProvisorioOrigem)
        {
            Int64 seqLacreRepositorioGerado = 0;
            
            try
            {
                DAL.LacreRepositorio adLacreRep = new DAL.LacreRepositorio(transacao);
                DAL.LacreRepositorioItens adLacreRepItens = new DAL.LacreRepositorioItens(transacao);
                
                // Obter lacre repositorio
                Entity.LacreRepositorio lacreRep = adLacreRep.ObterParaCopia(seqLacreProvisorioOrigem);
                List<Entity.LacreRepositorioItens> listLacreRepItens = null;

                if (lacreRep != null && lacreRep.SeqLacreRepositorio > 0)
                {
                    // Obter lacre repositorio itens.
                    listLacreRepItens = adLacreRepItens.ObterParaCopia(seqLacreProvisorioOrigem);

                    if (listLacreRepItens != null)
                    {
                        // inserir header.
                        lacreRep.DataCadastro = DateTime.Now;

                        if (lacreRep.UsuarioCadastro == null)
                            lacreRep.UsuarioCadastro = new Entity.Usuario();
                        lacreRep.UsuarioCadastro.NumUserBanco = Framework.Infra.Util.Parametrizacao.Instancia().NumeroNoBancoDoUsuarioLogado;

                        
                        if (lacreRep.TipoSituacaoHc == null)
                            lacreRep.TipoSituacaoHc = new Entity.TipoSituacaoHc();
                        lacreRep.TipoSituacaoHc.CodSituacao = BLL.Parametrizacao.Instancia().CodigoDaSituacaoProvisorio;

                        
                        if (lacreRep.LacreTipoOcorrencia == null)
                            lacreRep.LacreTipoOcorrencia = new Entity.LacreTipoOcorrencia();
                        lacreRep.LacreTipoOcorrencia.SeqLacreTipoOCorrencia = BLL.Parametrizacao.Instancia().SeqTipoOcorrenciaNovaLacracao;

                        seqLacreRepositorioGerado = adLacreRep.AdicionarTrans(lacreRep);

                        if (seqLacreRepositorioGerado > 0)
                        {
                            foreach (Entity.LacreRepositorioItens item in listLacreRepItens)
                            {
                                // Inserir itens.
                                item.DataCadastro = DateTime.Now;

                                if (item.LacreRepositorio == null)
                                    item.LacreRepositorio = new Entity.LacreRepositorio();                                
                                item.LacreRepositorio.SeqLacreRepositorio = seqLacreRepositorioGerado;

                                if (item.UsuarioCadastro == null)
                                    item.UsuarioCadastro = new Entity.Usuario();
                                item.UsuarioCadastro = lacreRep.UsuarioCadastro;

                                adLacreRepItens.AdicionarTrans(item);
                            }
                        }

                        Entity.LacreRepositorioEquipamento lacreRepEquipamento = null;

                        DAL.LacreRepositorioEquipamento dalLacreRepositorioEquipamento = new DAL.LacreRepositorioEquipamento(transacao);

                        // Recupera a lista de equipamento do lacre antigo
                        List<Entity.LacreRepositorioEquipamento>  
                            listlacreRepEquipamento = new BLL.LacreRepositorioEquipamento().ObterPorLacreRepositorio(seqLacreProvisorioOrigem);

                        foreach (Entity.LacreRepositorioEquipamento item in listlacreRepEquipamento)
                        {
                            lacreRepEquipamento = new Entity.LacreRepositorioEquipamento();

                            lacreRepEquipamento.LacreRepositorio  = new Entity.LacreRepositorio();

                            lacreRepEquipamento.LacreRepositorio.SeqLacreRepositorio = seqLacreRepositorioGerado;

                            lacreRepEquipamento.IdfAtivo = "S";
                            lacreRepEquipamento.DataCadastro = DateTime.Now;

                            lacreRepEquipamento.BemPatrimonial = item.BemPatrimonial;

                            dalLacreRepositorioEquipamento.AdicionarTrans(lacreRepEquipamento);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return seqLacreRepositorioGerado;
        }

        #endregion
    }
}
