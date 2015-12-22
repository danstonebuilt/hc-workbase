using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class MamaLocalizacao
    {
        public Int64 SeqPedidoExameMamaLocalizacao { get; set; }
        public Mamografia Mamografia { get; set; }
        public PedidoAtendimento PedidoAtendimento { get; set; }        
        public int TipoLocalizacao { get; set; }
        public int RegiaoCorpo { get; set; }
        public string Lado { get; set; }

        public string DescricaoRegiaoCorpo { get; set; }
        public string SiglaRegiaoCorpo { get; set; }

        public string TipoLocalizacaoExtenso 
        { 
            get
            {
                if (this.TipoLocalizacao == 1)
                    return "MAMOGRAFIA - Localização do Nódulo";
                else if (this.TipoLocalizacao == 2)
                    return "MAMOGRAFIA - Localização do espessamento";
                else if (this.TipoLocalizacao == 3)
                    return "HISTOPATOLOGICO - Localização da lesão";
                else if (this.TipoLocalizacao == 4)
                    return "CITOPATOLOGICO - Localização do Nódulo";
                else
                    return "";
            }            
        }        

        public MamaLocalizacao() { }

        public void Gravar(long seqMamografia)
        {
            new Hcrp.Framework.Dal.MamaLocalizacao().Gravar(this, seqMamografia);
        }

        public void GravarTrans(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, long seqMamografia)
        {
            new Hcrp.Framework.Dal.MamaLocalizacao(transacao).GravarTrans(this, seqMamografia);
        }
    }
}
