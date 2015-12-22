using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Logradouro
    {
        public int Codigo { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public int CodigoLogra { get; set; }
        public int CodigoTipoLogradouro { get; set; }
        public string NomeLogra { get; set; }
        public string BairroLogra { get; set; }
        public string CodigoCidadeLogra { get; set; }
        public string CidadeLogra { get; set; }
        public string EstadoLogra { get; set; }
        public string PaisLogra { get; set; }
        public string ComplementoLogra { get; set; }


        public Logradouro()
        { }

        public List<Hcrp.Framework.Classes.Logradouro> BuscaLogradouro()
        {
            return new Hcrp.Framework.Dal.Logradouro().BuscaLogradouro();
        }
        public Hcrp.Framework.Classes.Logradouro BuscaLogradouroCodigo(string codigo)
        {
            return new Hcrp.Framework.Dal.Logradouro().BuscaLogradouroCodigo(codigo);
        }
        public Hcrp.Framework.Classes.Logradouro BuscaLogradouroCep(string cep)
        {
            return new Hcrp.Framework.Dal.Logradouro().BuscaLogradouroCep(cep);
        }

        /// <summary>
        /// Buscar dados do logradouro por número de CEP para o cadastro de paciente externo.
        /// </summary>        
        public Hcrp.Framework.Classes.Logradouro BuscaLogradouroParaOCadastroDePacienteExternoPorCEP(string cep)
        {
            return new Hcrp.Framework.Dal.Logradouro().BuscaLogradouroParaOCadastroDePacienteExternoPorCEP(cep);
        } 
    }
}
