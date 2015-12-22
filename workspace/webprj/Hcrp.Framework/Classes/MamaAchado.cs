using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class MamaAchado
    {
        public Int64 SeqPedidoExameMamaAchado { get; set; }
        public Mamografia Mamografia { get; set; }
        public string Lado { get; set; }
        public int LesaoPapilar { get; set; }
        public int DescargaPapilar { get; set; }
        public int LinfonodoPalpavelAxilar { get; set; }
        public int LinfonodoPalpavelSupraclavicular { get; set; }

        public string LesaoPapilarExtenso 
        {
            get
            {
                if (this.LesaoPapilar == 3)
                    return "Sim";
                else
                    return "";
            }

        }
        public string DescargaPapilarExtenso
        {
            get
            {
                if (this.DescargaPapilar == 0)
                    return "Não informada";
                else if (this.DescargaPapilar == 1)
                    return "Cristalina";
                else if (this.DescargaPapilar == 2)
                    return "Hemorrágica";
                else
                    return "";
            }

        }
        public string LinfonodoPalpavelAxilarExtenso
        {
            get
            {
                if (this.DescargaPapilar == 3)
                    return "Sim";               
                else
                    return "";
            }

        }
        public string LinfonodoPalpavelSupraclavicularExtenso
        {
            get
            {
                if (this.LinfonodoPalpavelSupraclavicular == 3)
                    return "Sim";
                else
                    return "";
            }

        }

        public MamaAchado() { }

        public void Gravar(long seqMamografia)
        {
            new Hcrp.Framework.Dal.MamaAchado().Gravar(this, seqMamografia);
        }

        public void GravarTrans(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, long seqMamografia)
        {
            new Hcrp.Framework.Dal.MamaAchado(transacao).GravarTrans(this, seqMamografia);
        }
    }
}
