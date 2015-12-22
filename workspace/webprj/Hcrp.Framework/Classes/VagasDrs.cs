using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class VagasDrs
    {
        public int _CodInstituto { get; set; }
        public int _NumSeqLocal { get; set; }

        public Hcrp.Framework.Classes.Instituto _Instituto { get; set; }
        public Hcrp.Framework.Classes.MapeamentoLocal _Local { get; set; }

        public int Seq { get; set; }
        public DateTime DataExame { get; set; }
        public string DiaSemana { get; set; }
        public string HoraInicial { get; set; }
        public string HoraFinal { get; set; }
        public int QtdVagas { get; set; }
        public int QtdAgendada { get; set; }
        public int SaldoVagas { get; set; }
        public Hcrp.Framework.Classes.Instituto Instituto
        { 
            get{
                if (_Instituto == null)
                    _Instituto = new Instituto().BuscarInstitutoCodigo(_CodInstituto);
                return _Instituto;
            }
        }
        public Hcrp.Framework.Classes.MapeamentoLocal Local
        {
            get
            {
                if (_Local == null)
                    _Local = new MapeamentoLocal().BuscarLocalCodigo(_NumSeqLocal);
                return _Local;
            }        
        }

        public VagasDrs()
        { }

        public List<Hcrp.Framework.Classes.VagasDrs> BuscarVagas(int SeqItemAtendimento)
        {
            return new Hcrp.Framework.Dal.VagasDrs().BuscarVagas(SeqItemAtendimento);
        }
    }
}
