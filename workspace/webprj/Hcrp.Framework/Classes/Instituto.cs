using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Instituto
    {
        public int? _CodProtocoloAtendimento { get; set; }
        public int CodInstituto { get; set; }
        public string NomeInstituto { get; set; }
        public int CodInstSistema { get; set; }
        public Classes.TipoProtocoloAtendimento TipoProtocoloAtendimento {
            get 
            {
                int codProtocolo = 0;

                if (_CodProtocoloAtendimento > 0)
                    codProtocolo = (int)_CodProtocoloAtendimento;

                return new Classes.TipoProtocoloAtendimento().BuscarTipoProtocoloAtendimentoCodigo(codProtocolo);
            }            
            set{} }

        public Instituto()
        { }

        public Instituto BuscarInstitutoCodigo(int codInstituto)
        {
            return new Hcrp.Framework.Dal.Instituto().BuscarInstitutoCodigo(codInstituto);
        }

        public Instituto BuscarInstituto(string ip)
        {
            return new Dal.Instituto().BuscarInstituto(ip);
        }

        public List<Instituto> BuscarInstitutosPorInstituicao(int codigoInstituicao)
        {
            return new Dal.Instituto().BuscarInstitutosPorInstituicao(codigoInstituicao);
        }

        /// <summary>
        /// Tipos de informação que podem ser retornadas pelo método ToString()
        /// </summary>
        public enum EInfoToString
        {
            NomeInstituto = 1
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
                    case EInfoToString.NomeInstituto:
                        return this.NomeInstituto;
                }
            }
            return base.ToString();
        }
    }
}
