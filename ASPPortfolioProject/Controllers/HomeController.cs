using CryptoOcean.Interfaces;
using CryptoOcean.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CryptoOcean.Controllers
{
	public class HomeController : Controller
	{
		private readonly ICryptocurrencyService _cryptocurrencyInfoService;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory,
			ICryptocurrencyService cryptocurrencyInfoService, IConfiguration config, ICryptocurrencyService coinMarketCapAPICryptocurrencyService)
		{
			_logger = logger;
			_cryptocurrencyInfoService = coinMarketCapAPICryptocurrencyService;
		}

		public async Task<IActionResult> Index()
		{
			var jsonAPIResponse = _cryptocurrencyInfoService.GetOceanExcitementIndexCryptocurrenciesResponseResult();
			var oceanExcitementIndexCryptocurrenciesInfo = _cryptocurrencyInfoService.GetOceanExcitementIndexCryptocurrenciesInfo(jsonAPIResponse);

			return View(oceanExcitementIndexCryptocurrenciesInfo);
		}

		[Authorize]
		public async Task<IActionResult> OceanExcitementIndexCryptocurrenciesInfo()
		{
			var jsonAPIResponse = _cryptocurrencyInfoService.GetOceanExcitementIndexCryptocurrenciesResponseResult();
			var cryptoTopInformation = _cryptocurrencyInfoService.GetOceanExcitementIndexCryptocurrenciesInfo(jsonAPIResponse);

			return View(cryptoTopInformation);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}