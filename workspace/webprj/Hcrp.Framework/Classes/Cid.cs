using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
    public class Cid
    {
        #region Propriedades

        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public char StatusCategoria { get; set; }
        public char StatusSubCategoria { get; set; }
        public char RestricaoSexo { get; set; }
        public string CodigoPai { get; set; }
        public Int64 CodigoCapitulo { get; set; }
        public char Status { get; set; }

        #endregion

        #region Construtor

        public Cid()
        { }

        #endregion

        #region Métodos

        /// <summary>
        /// Obter lista de todos diagnosticos
        /// a partir do filtro Informado
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<Hcrp.Framework.Classes.Cid> BuscarCids(String codCid, String descricaoCid, Int64? codCapitulo)
        {
            List<Hcrp.Framework.Classes.Cid> LCids = new List<Hcrp.Framework.Classes.Cid>();

            LCids = new Hcrp.Framework.Dal.Cid().BuscarCids(codCid, descricaoCid, codCapitulo);

            return LCids;
        }

        /// <summary>
        /// Obter lista de diagnosticos apenas Ativos ou Inativos
        /// a partir do filtro Informado
        /// </summary>
        /// <param name="filtros"></param>
        /// <returns></returns>
        public List<Hcrp.Framework.Classes.Cid> BuscarCids(String codCid, String descricaoCid, Int64? codCapitulo, string Ativo)
        {
            List<Hcrp.Framework.Classes.Cid> LCids = new List<Hcrp.Framework.Classes.Cid>();

            LCids = new Hcrp.Framework.Dal.Cid().BuscarCids(codCid, descricaoCid, codCapitulo, Ativo);

            return LCids;
        }

        #endregion
    }
}
