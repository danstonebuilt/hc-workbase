using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    [Serializable]
    public class SituacaoHc
    {
        public int CodSituacao { get; set; }
        public string DescricaoSituacao { get; set; }
        public string DescricaoSituacaoColorida {
            get { 
                switch (this.CodSituacao)
	            {
		            case 112 : return "<font color='#666666'>" + this.DescricaoSituacao + "</font>";
                        break;
                    case 116 : return "<font color='#0000CC'>" + this.DescricaoSituacao + "</font>";
                        break;
                    case 117: return "<font color='#0000CC'>" + this.DescricaoSituacao + "</font>";
                        break;
                    case 118: return "<font color='#0000CC'>" + this.DescricaoSituacao + "</font>";
                        break;
                    case 119: return "<font color='#990000'>" + this.DescricaoSituacao + "</font>";
                        break;
                    case 120: return "<font color='#990000'>" + this.DescricaoSituacao + "</font>";
                        break;
                    case 124: return "<font color='#990000'>" + this.DescricaoSituacao + "</font>";
                        break;
                    case 52: return "<font color='#006600'>" + this.DescricaoSituacao + "</font>";
                        break;
                    case 121: return "<font color='#006600'>" + this.DescricaoSituacao + "</font>";
                        break;
                    case 122: return "<font color='#006600'>" + this.DescricaoSituacao + "</font>";
                        break;
                    case 123: return "<font color='#006600'>" + this.DescricaoSituacao + "</font>";
                        break;
                    default: return this.DescricaoSituacao;
                    break;
	            }
            }
        }

        public SituacaoHc()
        { }

        public SituacaoHc BuscarSituacaoCodigo(Int32 codSituacao)
        {
            return new Hcrp.Framework.Dal.SituacaoHc().BuscarSituacaoCodigo(codSituacao);
        }
    }
}
