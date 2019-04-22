using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCurrency
{
	public class CurrencyModel
	{
		public string Base { get; set; }
		public Dictionary<string, decimal> Rates { get; set; } = new Dictionary<string, decimal>();
		public string Date { get; set; }
	}
}
