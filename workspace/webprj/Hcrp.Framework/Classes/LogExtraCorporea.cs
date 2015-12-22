using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class LogExtraCorporea
    {
        int SeqLogExtraCorporea;

        private Usuario _Usuario;
        public int _codusuario { get; set; }
        public Hcrp.Framework.Classes.Usuario Usuario
        {
            get
            {
                if (this._Usuario == null)
                    _Usuario = new Hcrp.Framework.Classes.Usuario().BuscarUsuarioCodigo(_codusuario);
                return this._Usuario;
            }
            set { _Usuario = value; }
        }

        Paciente _Paciente;
        public string _registroPaciente { get; set; }
        public Hcrp.Framework.Classes.Paciente Paciente
        {
            get
            {
                if (this._Paciente == null)
                    _Paciente = new Hcrp.Framework.Classes.Paciente().BuscarPacienteRegistro(_registroPaciente);
                return this._Paciente;
            }
            set { _Paciente = value; }
        }

        public double Altura;
        public double Peso;
        public double SuperficieCorporea;
        public int CanulaArterialAbaixo;
        public int CanulaArterialOtima;
        public int CanulaArterialAcima;
        public int CanulaArterialFemural;
        public int CanulaVenosaSuperior;
        public int CanulaVenosaInferior;
        public int CanulaVenosaUnica;
        public int HeparinaInicialMg;
        public double HeparinaInicialMl;
        public double SuperficieCorporeaArredondado;
        public double Mitral;
        public double Tricuspide;
        public double Aortico;
        public double Pulmonar;

        public Boolean Inserir()
        {
            return new Hcrp.Framework.Dal.LogExtraCorporea().Inserir(this);
        }
    }
}
