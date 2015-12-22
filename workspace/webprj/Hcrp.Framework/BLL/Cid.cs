using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.BLL
{
	public class Cid
	{
		public List<Hcrp.Framework.Classes.Cid> BuscarCids(String codCid, String descricaoCid, Int64? codCapitulo)
		{
			List<Hcrp.Framework.Classes.Cid> _cd = new Dal.Cid().BuscarCids(codCid, descricaoCid, codCapitulo);
			return _cd;
		}
	}
}
