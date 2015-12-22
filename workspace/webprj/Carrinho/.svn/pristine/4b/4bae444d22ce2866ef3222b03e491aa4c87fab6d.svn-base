using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class LacreRepositorioComplemento
    {
        public void AtivarOuInativar(bool ehPraAtivar, long SEQ_REPOSITORIO_COMPLEMENTO)
        {
            new DAL.LacreRepositorioComplemento().AtivarOuInativar(ehPraAtivar, SEQ_REPOSITORIO_COMPLEMENTO);
        }

        public long Adicionar(Entity.Lacre_reposit_complemento repositorioComplemento)
        {
            return new DAL.LacreRepositorioComplemento().Adicionar(repositorioComplemento);
        }

        public List<Entity.Lacre_reposit_complemento> ObterComplemento(Int64 seq_lacre_repositorio)
        {
           return new DAL.LacreRepositorioComplemento().ObterComplemento(seq_lacre_repositorio);
        }
    }
}
