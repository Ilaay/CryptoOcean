using CryptoOcean.Interfaces;
using CryptoOcean.Models;
using System.Net;
using System.Text.Json;
using System.Web;

namespace CryptoOcean.Services
{
	public class CoinMarketCapAPICryptocurrencyService : ICryptocurrencyService
	{
		private readonly IConfiguration _config;
		public CoinMarketCapAPICryptocurrencyService(IConfiguration config)
		{
			_config = config;
		}

		public string GetOceanExcitementIndexCryptocurrenciesResponseResult()
		{
			var URL = new UriBuilder("https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest");

			var queryString = HttpUtility.ParseQueryString(string.Empty);
			queryString["symbol"] = _config["OceanExcitementIndexCryptocurrenciesString"];
			URL.Query = queryString.ToString();

			var client = new WebClient();
			client.Headers.Add("X-CMC_PRO_API_KEY", _config["CoinMarketCapAPIKeyString"]);
			client.Headers.Add("Accepts", "application/json");

			var responseResult = client.DownloadString(URL.ToString());
			return responseResult;
		}

		public List<FullCryptocurrencyInfo> GetOceanExcitementIndexCryptocurrenciesInfo(string jsonAPIResponseString)
		{
			var fullOceanIndexCryptocurrenciesInfo = new List<FullCryptocurrencyInfo>();
			var cryptocurrencyNameList = _config["OceanExcitementIndexCryptocurrenciesString"].Split(',');

			using (var jsonDocument = JsonDocument.Parse(jsonAPIResponseString))
			{
				var data = jsonDocument.RootElement.GetProperty("data");

				foreach (var cryptocurrencyShortName in cryptocurrencyNameList)
				{
					var cryptocurrencyInfo = data.GetProperty(cryptocurrencyShortName);

					var cryptocurrencyFullName = cryptocurrencyInfo.GetProperty("name").ToString();
					var quote = cryptocurrencyInfo.GetProperty("quote").GetProperty("USD");
					var cryptocurrencyPrice = quote.GetProperty("price").GetDecimal();
					var cryptocurrencyPercentChangeDay = quote.GetProperty("percent_change_24h").GetDecimal();

					var fullCryptocurrencyInfo = new FullCryptocurrencyInfo(cryptocurrencyFullName, cryptocurrencyShortName,
						cryptocurrencyPrice, cryptocurrencyPercentChangeDay);

					fullOceanIndexCryptocurrenciesInfo.Add(fullCryptocurrencyInfo);
				}
			}

			return fullOceanIndexCryptocurrenciesInfo;
		}
	}
}
