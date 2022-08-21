using CryptoOcean.Models;
using System.Text.RegularExpressions;

namespace CryptoOcean.Services
{
	public class CryptoCompareAPICryptocurrencyService //: ICryptocurrencyInfoService  todo only with cryptocurrency short name and price
	{
		private string _cryptoAPIRegexPattern = @"[0-9].*?(?=})";
		private readonly IConfiguration _config;
		public CryptoCompareAPICryptocurrencyService(IConfiguration config)
		{
			_config = config;
		}

		public async Task<List<ShortCryptocurrencyInfo>> GetOceanExcitementIndexCryptocurrenciesInfo()
		{
			var topCryptocurrenciesNames = _config["OceanExcitementIndexCryptocurrenciesString"].Split(',');
			return await GetSelectedCryptoInfo(topCryptocurrenciesNames);
		}

		public async Task<List<ShortCryptocurrencyInfo>> GetSelectedCryptoInfo(string[] cryptocurrenciesNames)
		{
			var client = new HttpClient();
			var fsyms = string.Empty;
			var lastName = cryptocurrenciesNames.Last();

			foreach (var name in cryptocurrenciesNames)
			{
				if (!name.Equals(lastName)) fsyms += name + ',';
				else fsyms += name;
			}

			var cryptocurrencyPriceResponse = await client.GetAsync("https://min-api.cryptocompare.com/data/pricemulti?fsyms=" + fsyms + "&tsyms=USD"); //ссылку тоже вынести

			var selectedCryptoInfo = new List<ShortCryptocurrencyInfo>();

			if (cryptocurrencyPriceResponse.IsSuccessStatusCode)
			{
				var responseResult = await cryptocurrencyPriceResponse.Content.ReadAsStringAsync();
				var regex = new Regex(_cryptoAPIRegexPattern);
				var matchCollection = regex.Matches(responseResult);
				var quotes = cryptocurrenciesNames.Zip(matchCollection, (name, price) => name + ":" + price);

				foreach (var quote in quotes)
				{
					var separatorIndex = quote.IndexOf(':');
					selectedCryptoInfo.Add(new ShortCryptocurrencyInfo(quote.Substring(0, separatorIndex), quote.Substring(separatorIndex + 1)));
				}
			}

			return selectedCryptoInfo;
		}
	}
}
