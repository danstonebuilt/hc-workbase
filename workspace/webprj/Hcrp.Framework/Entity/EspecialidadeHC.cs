using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Entity
{
    public class EspecialidadeHC : IEntityHC
    {
        #region Propriedades
        public long cod_especialidade_hc { get; set; }
        public string idf_ativo { get; set; }
        public string idf_gera_ficha_op { get; set; }
        public string idf_lib_cad_diagnostico_fop { get; set; }
        public string idf_lib_cad_incisao_fop { get; set; }
        public int idf_nivel_user_modelo_fop { get; set; }
        public string idf_lib_complemento_proc { get; set; }
        public string idf_informa_cir_proj_genoma { get; set; }
        public string idf_gera_requisicao_materiais { get; set; }
        public int cod_prof_responsavel { get; set; }
        public string sgl_especialidade_hc { get; set; }
        public string nom_abreviado { get; set; }
        public string dsc_origem_lab { get; set; }
        public string nom_especialidade_hc { get; set; }
        public long cod_especialidade_hc_pai { get; set; }
        public long seq_especialidade_sus { get; set; }
        public string cod_inst_prodesp { get; set; }
        public string cod_tipo_serv_prodesp { get; set; }
        public string idf_agenda_ambulatorio { get; set; }
        public string idf_agenda_serie { get; set; }
        public DateTime dta_fat_hospitalar { get; set; }
        public string idf_recupera_receita { get; set; }
        public string idf_valida_reagendamento { get; set; }
        public string idf_cirurgica { get; set; }
        public string idf_solic_exam_amb_radio { get; set; }
        public string idf_imp_espec_receita { get; set; }
        public string idf_totalizar { get; set; }

        #endregion

        #region Metodos
        public EspecialidadeHC(long pCod_Especialidade_HC, string pSgl_Especialidade_HC, string pNom_Especialidade_HC)
        {
            this.cod_especialidade_hc = pCod_Especialidade_HC;
            this.sgl_especialidade_hc = pSgl_Especialidade_HC;
            this.nom_especialidade_hc = pNom_Especialidade_HC;
        }
        #endregion
    }
}
