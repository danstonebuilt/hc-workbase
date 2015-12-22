using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class ProtocoloConsultaOftalmologiaExame
    {
        public Int64 NumSeqProtocoloConsultaOftalmologia { get; set; }
        
        public Int16 IdfExame { get; set; }
        public string IdfExameExtenso
        {
            
            /*
            01 - Topografia de Córnea
            02 - Paquimetria
            03 - Ecografia Ocular
            04 - Microscopia Especular
            05 - PAM
            06 - Campo Visual Computadorizado
            07 - OCT
            08 - Eletrorretinograma
            09 - Potencial Visual Evocado
            10 - Eletro Oculograma
            11 - Angiografia
            12 - Retinografia
            13 - Eletrofisiologia
            99 - Outro (Obrigatória a especificação)
            */
            
            get
            {
                if (this.IdfExame == 1)
                    return "Topografia de Córnea";
                else if (this.IdfExame == 2)
                    return "Paquimetria";
                else if (this.IdfExame == 3)
                    return "Ecografia Ocular";
                else if (this.IdfExame == 4)
                    return " Microscopia Especular";
                else if (this.IdfExame == 5)
                    return "PAM";
                else if (this.IdfExame == 6)
                    return "Campo Visual Computadorizado";
                else if (this.IdfExame == 7)
                    return "OCT";
                else if (this.IdfExame == 8)
                    return "Eletrorretinograma";
                else if (this.IdfExame == 9)
                    return "Potencial Visual Evocado";
                else if (this.IdfExame == 10)
                    return "Eletro Oculograma";
                else if (this.IdfExame == 11)
                    return "Angiografia";
                else if (this.IdfExame == 12)
                    return "Retinografia";
                else if (this.IdfExame == 13)
                    return "Eletrofisiologia";
                else if (this.IdfExame == 99)
                    return "Outro";
                else
                    return "";
            }            
        }
        
        public string DscOutroExame { get; set; }
    }
}
