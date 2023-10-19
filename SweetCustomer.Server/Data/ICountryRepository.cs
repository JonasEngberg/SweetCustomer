using SweetCustomer.Server.Dto;

namespace SweetCustomer.Server.Data
{
	public interface ICountryRepository
	{
		Task<IEnumerable<CountryDto>> GetCountries();
	}
}