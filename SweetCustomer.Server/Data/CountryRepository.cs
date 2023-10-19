using Dapper;
using SweetCustomer.Server.Dto;

namespace SweetCustomer.Server.Data;

public class CountryRepository : ICountryRepository
{
	private readonly DapperContext _context;
	public CountryRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<CountryDto>> GetCountries()
	{
		var query = "SELECT CountryId, Name FROM Country ORDER BY Name";

		using (var connection = _context.CreateConnection())
		{
			var countries = await connection.QueryAsync<CountryDto>(query);
			return countries.ToList();
		}
	}
}
