using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Especialidade
    {
        public List<Hcrp.Framework.Classes.Paciente> _Pacientes;
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public List<Hcrp.Framework.Classes.Paciente> Pacientes {
            get {
                if (_Pacientes == null)
                    _Pacientes = new Hcrp.Framework.Classes.Paciente().BuscaPacientesEspecialidade(this);
                return _Pacientes;
            }
        }

        public Especialidade()
        { }

        public Especialidade BuscaEspecialidadeCodigo(int codEspecialidade)
        {
            return new Hcrp.Framework.Dal.Especialidade().BuscaEspecialidadeCodigo(codEspecialidade);
        }

        public List<Hcrp.Framework.Classes.Especialidade> BuscaEspecialidadesSigla(string sigla)
        {
            return new Hcrp.Framework.Dal.Especialidade().BuscaEspecialidadesSigla(sigla);
        }

        /// <summary>
        /// Tipos de informação que podem ser retornadas pelo método ToString()
        /// </summary>
        public enum EInfoToString
        {
            Nome = 1,
            Sigla = 2,
            NomeSigla = 3,
            SiglaNome = 4
        }

        /// <summary>
        /// Define o tipo de informação que será retornada pelo método ToString()
        /// </summary>
        public EInfoToString InfoToString { get; set; }

        /// <summary>
        /// Sobrescreve o método ToString() da classe base (Object), retornando o tipo de informação definido pelo atributo InfoToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (InfoToString != null)
            {
                switch (InfoToString)
                {
                    case EInfoToString.Nome:
                        return this.Nome;
                    case EInfoToString.Sigla:
                        return this.Sigla;
                    case EInfoToString.NomeSigla:
                        return (this.Nome + " - " + this.Sigla);
                    case EInfoToString.SiglaNome:
                        return (this.Sigla + " - " + this.Nome);
                }
            }
            return base.ToString();
        }
    }
}
