using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ProtocoloConsultaOftalmoProc
    {
        public Int64 NumSeqProtocoloConsultaOftalmologia { get; set; }
        
        public Int16 IdfProcedimento { get; set; }
        public string IdfProcedimentoExtenso
        {

            /*
            01 - Trabeculoplastia / ciclofotoagulação
            02 - Yag laser
            03 - Fotocoagulação a laser
            04 - Aplicação de Medicação Intra Vítrea
            99 - Outro  (Obrigatória a especificação)
            */

            get
            {
                if (this.IdfProcedimento == 1)
                    return "Trabeculoplastia / ciclofotoagulação";
                else if (this.IdfProcedimento == 2)
                    return "Yag laser";
                else if (this.IdfProcedimento == 3)
                    return "Fotocoagulação a laser";
                else if (this.IdfProcedimento == 4)
                    return "Aplicação de Medicação Intra Vítrea";
                else if (this.IdfProcedimento == 99)
                    return "Outro";               
                else
                    return "";
            }
        }
        
        public string DescricaoOutroProc { get; set; }
    }
}
