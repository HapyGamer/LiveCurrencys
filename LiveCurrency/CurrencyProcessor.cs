using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace LiveCurrency
{
	public class CurrencyProcessor
	{
		/// <summary>
		/// add in date
		/// </summary>
		/// <returns></returns>
		public static async Task<CurrencyModel> LoadCurrency()
		{
			string url = "https://api.exchangeratesapi.io/latest";
			
			using (HttpResponseMessage response = await APIHelper.APIClient.GetAsync(url))
			{
				if (response.IsSuccessStatusCode)
				{
					CurrencyModel currency = await response.Content.ReadAsAsync<CurrencyModel>();
					return currency;
				}
				else
				{
					throw new Exception(response.ReasonPhrase);
				}
			}

		}
	}
}
