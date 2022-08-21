namespace CryptoOcean.Models
{
	public class ShortCryptocurrencyInfo
	{
		public string ShortName { get; set; }
		public string PriceToUsd { get; set; }

		public ShortCryptocurrencyInfo(string cryptocurrencyShortName, string cryptocurrencyPriceToUsd)
		{
			ShortName = cryptocurrencyShortName;
			PriceToUsd = cryptocurrencyPriceToUsd;
		}
	}
}
