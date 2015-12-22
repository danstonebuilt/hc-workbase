using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hcrp.Framework.BLL
{
	public class CidCapitulo
	{
		public List<Hcrp.Framework.Classes.CidCapitulo> BuscarTodosCapitulos()
		{
			List<Hcrp.Framework.Classes.CidCapitulo> _cc = new Dal.CidCapitulo().BuscarTodosCapitulos();
			return _cc;
		}
	}
}
