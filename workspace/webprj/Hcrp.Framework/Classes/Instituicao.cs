using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Instituicao
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Cnes { get; set; }
        public string Endereco { get; set; } //DSC_ENDERECO
        public string Complemento { get; set; } //DSC_COMPLEMENTO
        public string Numero { get; set; } //NUM_ENDERECO
        public string Cep { get; set; } //DSC_CEP
        public string Bairro { get; set; } //DSC_BAIRRO
        public string UF { get; set; } //SGL_UF
        public string TelefoneUnidade { get; set; } //NUM_TEL_UNIDADE


        public Framework.Classes.Drs Drs { get; set; }

        public Hcrp.Framework.Classes.Municipio Municipio {
            get
            {
                return new Hcrp.Framework.Classes.Municipio().BuscaMunicipiosInstituicao(this);
            }            
        }

        public Instituicao()
        { }

        public List<Hcrp.Framework.Classes.Instituicao> BuscaInstituicaoMunicipio(string chaveMunicipio)
        {
            if (!string.IsNullOrEmpty(chaveMunicipio) && !string.IsNullOrWhiteSpace(chaveMunicipio))
                return new Hcrp.Framework.Dal.Instituicao().BuscaInstituicaoMunicipio(chaveMunicipio);
            else return null;
        }

        public Hcrp.Framework.Classes.Instituicao BuscaInstituicaoCodigo(string codigo)
        {
            return new Hcrp.Framework.Dal.Instituicao().BuscaInstituicaoCodigo(codigo);
        }

        public Hcrp.Framework.Classes.Instituicao BuscaInstituicaoEnderecoCompleto(int intCodigoInst)
        {
            return new Hcrp.Framework.Dal.Instituicao().BuscaInstituicaoEnderecoCompleto(intCodigoInst);
        }

        public void AtualizarCNES(Hcrp.Framework.Classes.Instituicao i, string cnes)
        {
            new Hcrp.Framework.Dal.Instituicao().AtualizarCNES(i, cnes);
        }

        /// <summary>
        /// Lista as instituições validas para o HC, ou seja, instituições que possuem institutos
        /// </summary>
        /// <returns></returns>
        public List<Hcrp.Framework.Classes.Instituicao> BuscaInstituicaoHC()
        {
            return new Hcrp.Framework.Dal.Instituicao().BuscaInstituicaoHC();
        }

    }
}
