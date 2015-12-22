using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class FormularioPesquisaOpiniao
    {
        private List<Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite> _Convidados { get; set; }
        public int Seq { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }
        public string SqlExportacao { get; set; }
        public List<Hcrp.Framework.Classes.Usuario> UsuariosAcesso { get; set; }
        public List<Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite> Convidados {
            get { 
                if (_Convidados == null)
                    _Convidados = new Hcrp.Framework.Classes.FormularioPesquisaOpiniaoConvite().BuscarConvitesFormulario(this);
                return _Convidados;
            }
        }
        public ETipoFormulario TipoFormulario { get; set; }

        public enum ETipoFormulario { 
            CRH = 1,
            PCO2012 = 2
        }

        public FormularioPesquisaOpiniao() { }

        public Hcrp.Framework.Classes.FormularioPesquisaOpiniao BuscarDadosForm(ETipoFormulario Tipo)
        {
            return new Hcrp.Framework.Dal.FormularioPesquisaOpiniao().BuscarDadosForm(Tipo);
        }

        public List<Hcrp.Framework.Classes.FormularioPesquisaOpiniao> BuscarFormularios()
        {
            return new Hcrp.Framework.Dal.FormularioPesquisaOpiniao().BuscarFormularios();
        }
    }
}
