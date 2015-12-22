using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class PlanoDeContaItem
    {
        public long IdPlanoDeContaItem { get; set; }
        public PlanoDeConta PlanoDeConta { get; set; }
        public string NomeDoItem { get; set; }
        public Int32 Ordem { get; set; }
        public long? IdPlanoDeContaItemPai { get; set; }
        public string OrdemHierarquia { get; set; }
        public List<PlanoDeContaItem> ListaDeFilho { get; set; }
        public int NivelIdentacao { get; set; }

        // Propriedades de ajuda
        public bool ORegistroPodeSubir { get; set; }
        public bool ORegistroPodeDescer { get; set; }

        public PlanoDeContaItem()
        {
            this.PlanoDeConta = new PlanoDeConta();
            this.ListaDeFilho = new List<PlanoDeContaItem>();
        }

        public List<Hcrp.Framework.Classes.PlanoDeContaItem> ObterListaDeItemPorIdDePlanoDeConta(long idPlanoConta)
        {
            return new Hcrp.Framework.Dal.PlanoDeContaItem().ObterListaDeItemPorIdDePlanoDeConta(idPlanoConta);
        }

        public List<Hcrp.Framework.Classes.PlanoDeContaItem> ObterListaHierarquicaDeItemPorIdDePlanoDeConta(long idPlanoConta)
        {
            return new Hcrp.Framework.Dal.PlanoDeContaItem().ObterListaHierarquicaDeItemPorIdDePlanoDeConta(idPlanoConta);
        }

        public List<Hcrp.Framework.Classes.PlanoDeContaItem> ObterListaHierarquicaDePlanoDeContaDeMateriais(string idPlanoConta)
        {
            return new Hcrp.Framework.Dal.PlanoDeContaItem().ObterListaHierarquicaDePlanoDeContaDeMateriais(idPlanoConta);
        }

        public string[] ObterListaDeOrdem(long idPlanoConta)
        {
            return new Hcrp.Framework.Dal.PlanoDeContaItem().ObterListaDeOrdem(idPlanoConta);
        }

        public void Adicionar(Hcrp.Framework.Classes.PlanoDeContaItem _planoDeContaItem)
        {
            new Hcrp.Framework.Dal.PlanoDeContaItem().Adicionar(_planoDeContaItem);
        }

        public void Remover(long idPlanoDeContaItem)
        {
            new Hcrp.Framework.Dal.PlanoDeContaItem().Remover(idPlanoDeContaItem);
        }

        public int ObterOrdem(long idPai)
        {
            return new Hcrp.Framework.Dal.PlanoDeContaItem().ObterOrdem(idPai);
        }

        public void AtualizarOrdemHierarquica(long idPlanoContaItem, string ordemHierarquica)
        {
            new Hcrp.Framework.Dal.PlanoDeContaItem().AtualizarOrdemHierarquica(idPlanoContaItem, ordemHierarquica);
        }

        public void AtualizarOrdemDoItem(long idPlanoDeContaItem, long idPlanoDeContaItemPai, int ordemAtual, bool mudarParaORegistroAcima)
        {
            new Hcrp.Framework.Dal.PlanoDeContaItem().AtualizarOrdemDoItem(idPlanoDeContaItem, idPlanoDeContaItemPai, ordemAtual, mudarParaORegistroAcima);
        }
    }
}
