namespace CryptoOcean.Models
{
	public class FullCryptocurrencyInfo
	{
		public string Name { get; set; }
		public string Symbol { get; set; }
		public decimal Price { get; set; }
		public decimal PercentChangePerDay { get; set; }

		public FullCryptocurrencyInfo(string name, string symbol, decimal price, decimal percentChangePerDay)
		{
			Name = name;
			Symbol = symbol;
			Price = price;
			PercentChangePerDay = percentChangePerDay;
		}
	}
}
