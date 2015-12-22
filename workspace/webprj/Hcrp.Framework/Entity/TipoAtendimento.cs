using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Entity
{
    public partial class TipoAtendimento : Hcrp.Framework.Entity.IEntityHC
    {

        #region propriedades

        public long cod_tipo_atendimento    { get; set; }        
        public string nom_tipo_atendimento  { get; set; }
        public string idf_exige_leito       { get; set; }
        public string idf_exige_prontuario	{ get; set; }
        public string nom_tipo_abreviado	{ get; set; }                
        /// <summary>
        /// 1 - AMBULATORIAL / 2 - HOSPITALAR
        /// </summary>
        public int idf_tipo_fat_sus	        { get; set; }
        public long seq_modalidade_atendimento_sus	 { get; set; }

        /// <summary>
        /// S - Sim e N- Não
        /// </summary>
        public string idf_gera_alta_internacao	 { get; set; }
        

        #endregion

        #region Métodos

        public TipoAtendimento(long pCod_Tipo_Atendimento, string pNom_Tipo_Atendimento, string pNom_Tipo_Abreviado)
        {
            this.cod_tipo_atendimento = pCod_Tipo_Atendimento;
            this.nom_tipo_atendimento = pNom_Tipo_Atendimento;
            this.nom_tipo_abreviado = pNom_Tipo_Abreviado;
        }

        #endregion
    }
}
