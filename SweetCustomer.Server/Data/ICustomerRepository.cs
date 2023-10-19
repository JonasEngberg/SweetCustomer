using SweetCustomer.Server.Dto;
using SweetCustomer.Server.Models;

namespace SweetCustomer.Server.Data
{
	public interface ICustomerRepository
	{
		Task CreateCustomer(CreateCustomerDto customer);

		Task<IEnumerable<CustomerDto>> GetCustomers();

		Task<Customer?> GetCustomer(int customerId);

		Task DeleteCustomer(int customerId);
	}
}