using Microsoft.AspNetCore.Mvc;
using SweetCustomer.Server.Data;

namespace SweetCustomer.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
	private readonly ILogger<CountryController> _logger;
	private readonly ICountryRepository _repository;

	public CountryController(ILogger<CountryController> logger, ICountryRepository repository)
	{
		_logger = logger;
		_repository = repository;
	}

	[HttpGet()]
	public async Task<IActionResult> Get()
	{
		var allCountries = await _repository.GetCountries();
		return Ok(allCountries);
	}
}
