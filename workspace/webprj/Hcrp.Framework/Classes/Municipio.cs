using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Municipio
    {
        public string Codigo { get; set; }
        public Pais Pais { get; set; }
        public UnidadeFederacao UF { get; set; }
        public string Nome { get; set; }
        public string Chave
        {
            get{
                if ((this.Pais != null) && (this.UF != null))
                    return this.Pais.Sigla + "|" + this.UF.Sigla + "|" + this.Codigo;
                else return "";
            }
            set { }
        }
        public Hcrp.Framework.Classes.Drs Drs { get; set; }

        public Municipio()
        { }

        public List<Hcrp.Framework.Classes.Municipio> BuscaMunicipiosUsuarioConectado()
        {
            return new Hcrp.Framework.Dal.Municipio().BuscaMunicipiosUsuarioConectado();
        }

        public List<Hcrp.Framework.Classes.Municipio> BuscaMunicipiosUF(string uf)
        {
            return new Hcrp.Framework.Dal.Municipio().BuscaMunicipiosUF(uf);
        }

        public List<Hcrp.Framework.Classes.Municipio> BuscaMunicipiosDrs(int codDrs)
        {
            return new Hcrp.Framework.Dal.Municipio().BuscaMunicipiosDrs(codDrs);
        }

        public Hcrp.Framework.Classes.Municipio BuscaMunicipiosChave(string chave)
        {
            return new Hcrp.Framework.Dal.Municipio().BuscaMunicipiosChave(chave);       
        }
        
        public Hcrp.Framework.Classes.Municipio BuscaMunicipiosInstituicao(Hcrp.Framework.Classes.Instituicao i)
        {
            return new Hcrp.Framework.Dal.Municipio().BuscaMunicipiosInstituicao(i);
        }

        public int BuscarCodigoDRSDoMunicipio(string siglaPais, string siglaUF, string codigoLocalidade)
        {
            return new Dal.Municipio().BuscarCodigoDRSDoMunicipio(siglaPais, siglaUF, codigoLocalidade);
        }
    }
}
