using CryptoOcean.Models;

namespace CryptoOcean.Interfaces
{
	public interface ICryptocurrencyService
	{
		public List<FullCryptocurrencyInfo> GetOceanExcitementIndexCryptocurrenciesInfo(string jsonAPIResponse);
		public string GetOceanExcitementIndexCryptocurrenciesResponseResult();
	}
}
