using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class AtendimentoPaciente
    {
        #region Métodos

        /// <summary>
        /// Obter o atendimento da paciente.
        /// </summary>        
        public Int64 ObterAtendimentoDoPacienteParaAData(string codigoPaciente, DateTime dataAtendimento)
        {
            return new DAL.AtendimentoPaciente().ObterAtendimentoDoPacienteParaAData(codigoPaciente, dataAtendimento);
        }

        /// <summary>
        /// Obter dados do atendimento por seq atendimento.
        /// </summary>        
        public DataView ObterDadosDoAtendimentoPorSeqAtendimento(Int64 seqAtendimento)
        {
            return new DAL.AtendimentoPaciente().ObterDadosDoAtendimentoPorSeqAtendimento(seqAtendimento);
        }

        #endregion
    }
}
