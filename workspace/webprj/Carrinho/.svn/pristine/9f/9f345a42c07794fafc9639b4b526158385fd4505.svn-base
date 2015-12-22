using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.CarroUrgenciaPsicoativo.BLL
{
    public class Paciente
    {
        #region Métodos

        /// <summary>
        /// Idade formatada em x anos x meses e x dias.
        /// </summary>
        public string IdadeFormatadaEmAnoMesDia(string idadeASerFormatada)
        {
            string retorno = string.Empty;
            
            try
            {
                string[] idadeSemFormatacao = idadeASerFormatada.Split(':');

                if (idadeSemFormatacao.Length == 3)
                {
                    retorno = string.Format("{0} ano(s) {1} mes(es) {2} dia(s)", idadeSemFormatacao[0], idadeSemFormatacao[1], idadeSemFormatacao[2]);
                }
            }
            catch (Exception)
            {   
                throw;
            }

            return retorno;
        }

        /// <summary>
        /// Obter lista de paciente
        /// </summary>        
        public List<Hcrp.CarroUrgenciaPsicoativo.Entity.Paciente> ObterListaDePaciente(string nome, string sobrenome, int paginaAtual, out int totalRegistro)
        {
            return new DAL.Paciente().ObterListaDePaciente(nome, sobrenome, Parametrizacao.Instancia().QuantidadeRegistroPagina, paginaAtual, out totalRegistro);
        }

        #endregion
    }
}
