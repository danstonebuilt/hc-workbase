using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class MamaControleLesao
    {
        public Int64 SeqPedidoExameMamaControleLesao { get; set; }
        public Mamografia Mamografia { get; set; }
        public string Lado { get; set; }
        public int Nodulo { get; set; }
        public int Microcalcificacao { get; set; }
        public int AssimetriaFocal { get; set; }
        public int AssimetriaDifusa { get; set; }
        public int AreaDensa { get; set; }
        public int DistorcaoFocal { get; set; }

        public string NoduloExtenso 
        {
            get
            {
                if (this.Nodulo == 3)
                    return "Sim";
                else
                    return "";
            }
        }
        public string MicrocalcificacaoExtenso
        {
            get
            {
                if (this.Microcalcificacao == 3)
                    return "Sim";
                else
                    return "";
            }
        }
        public string AssimetriaFocalExtenso
        {
            get
            {
                if (this.AssimetriaFocal == 3)
                    return "Sim";
                else
                    return "";
            }
        }
        public string AssimetriaDifusaExtenso
        {
            get
            {
                if (this.AssimetriaDifusa == 3)
                    return "Sim";
                else
                    return "";
            }
        }
        public string AreaDensaExtenso
        {
            get
            {
                if (this.AreaDensa == 3)
                    return "Sim";
                else
                    return "";
            }
        }
        public string DistorcaoFocalExtenso
        {
            get
            {
                if (this.DistorcaoFocal == 3)
                    return "Sim";
                else
                    return "";
            }
        }

        public MamaControleLesao() { }

        public void Gravar(long seqMamografia)
        {
            new Hcrp.Framework.Dal.MamaControleLesao().Gravar(this, seqMamografia);
        }

        public void GravarTrans(Hcrp.Infra.AcessoDado.TransacaoDinamica transacao, long seqMamografia)
        {
            new Hcrp.Framework.Dal.MamaControleLesao(transacao).GravarTrans(this, seqMamografia);
        }
    }
}
