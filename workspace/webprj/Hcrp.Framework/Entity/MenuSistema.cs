using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Entity
{
    public partial class MenuSistema : Hcrp.Framework.Entity.IEntityHC
    {

        #region propriedades

        public string nom_sistema { get; set; }
        public string caminho { get; set; }
        public string nom_programa { get; set; }
        public DateTime dta_criacao_programa { get; set; }
        public string idf_ativo { get; set; }
        public string dsc_pagina_web { get; set; }

        public int level { get; set; }
        public int cod_programa { get; set; }
        public int cod_programa_pai { get; set; }

        /// <summary>
        /// Autorização de visualização de Menu;
        /// </summary>
        public string idf_menu { get; set; }

        /// <summary>
        /// Identificação da ordem do posicionamento do Menu
        /// </summary>
        public long num_ordem { get; set; }

        /// <summary>
        /// Autor do programa
        /// </summary>
        public int cod_autor { get; set; }

        /// <summary>
        /// Descrição do Programa 
        /// </summary>
        public string dsc_programa { get; set; }

        /// <summary>
        /// Nome de exibição do programa
        /// </summary>
        public string nom_exibicao_programa { get; set; }

        #endregion
        
        public MenuSistema()
        {
        }

    }
}

