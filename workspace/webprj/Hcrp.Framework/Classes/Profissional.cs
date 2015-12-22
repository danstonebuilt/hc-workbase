using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Profissional
    {
        public int _NumUserBanco { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string NomeCompleto
        {
            get
            {
                return this.Nome + " " + this.Sobrenome;
            }
        }
        public string Documento { get; set; }
        public string Cpf { get; set; }
        public string UfDocumento { get; set; }
        public Hcrp.Framework.Classes.Usuario Usuario { get; set; }
        public int NumProfissao { get; set; }
        
        public string NomeProfissao { get; set; }
        public string SiglaProfissao { get; set; }
        public string NumeroCNS { get; set; }

        public Profissional()
        { }

        public Hcrp.Framework.Classes.Profissional BuscarMedicoCRM(string crm, string uf, string nome, string sobrenome, string cpf)
        {
            return new Hcrp.Framework.Dal.Profissional().BuscarMedicoCRM(crm, uf, nome, sobrenome, cpf);
        }

        public Hcrp.Framework.Classes.Profissional BuscarMedicoCRM(string nome, string sobrenome, string cpf)
        {
            return new Hcrp.Framework.Dal.Profissional().BuscarMedicoCRM(nome, sobrenome, cpf);
        }

        public Hcrp.Framework.Classes.Profissional BuscarProfissionalCodigo(int codigo)
        {
            return new Hcrp.Framework.Dal.Profissional().BuscarProfissionalCodigo(codigo);
        }

        public int Inserir()
        {
            return new Hcrp.Framework.Dal.Profissional().Inserir(this);
        }

        /// <summary>
        /// Buscar os tipos de profissional.
        /// </summary>        
        public List<Hcrp.Framework.Classes.Profissional> BuscarOsTiposDeProfissao()
        {
            return new Hcrp.Framework.Dal.Profissional().BuscarOsTiposDeProfissao();
        }

        /// <summary>
        /// Buscar os profissionais por número de profissão, UF CRM e número CRM.
        /// </summary>            
        public List<Hcrp.Framework.Classes.Profissional> BuscarProfissionalPorNumeroProfissaoUFCRMNumeroCRMAtivos(int numeroProfissao, string UFCRM, string numeroCRM)
        {
            return new Hcrp.Framework.Dal.Profissional().BuscarProfissionalPorNumeroProfissaoUFCRMNumeroCRMAtivos(numeroProfissao, UFCRM, numeroCRM);
        }
    }
}
