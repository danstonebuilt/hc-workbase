using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.Classes
{
	public class CidCapitulo
	{
		#region Propriedades

		public Int64 Codigo { get; set; }
		public string CodigoCidInicio { get; set; }
		public string CodigoCidFim { get; set; }
		public string Descricao { get; set; }
        public char StatusCategoria { get; set; }
        public char StatusSubCategoria { get; set; }
        public char RestricaoSexo { get; set; }
        public string CodigoPai { get; set; }
        public Int64 CodigoCapitulo { get; set; }
        public char Status { get; set; }

		#endregion

		#region Construtor

		public CidCapitulo() { }

		#endregion

		#region Métodos

		/// <summary>
		/// Obter lista de pacientes vinculados ao pedido de atendimento
		/// </summary>
		/// <param name="filtros"></param>
		/// <returns></returns>
		public List<Hcrp.Framework.Classes.CidCapitulo> BuscarTodosCapitulos()
		{
			List<Hcrp.Framework.Classes.CidCapitulo> LCapitulos = new List<Hcrp.Framework.Classes.CidCapitulo>();

			LCapitulos = new Hcrp.Framework.BLL.CidCapitulo().BuscarTodosCapitulos();
			
			//Senão trazer das duas
			return LCapitulos;
		}

        /// <summary>
        /// Obter lista de diagnosticos
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

		#endregion
	}
}
