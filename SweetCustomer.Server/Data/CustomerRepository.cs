using Dapper;
using SweetCustomer.Server.Dto;
using SweetCustomer.Server.Models;
using System.Data;

namespace SweetCustomer.Server.Data;

public class CustomerRepository : ICustomerRepository
{
	private readonly DapperContext _context;
	public CustomerRepository(DapperContext context)
	{
		_context = context;
	}

	public async Task<IEnumerable<CustomerDto>> GetCustomers()
	{
		var query = """
			SELECT cu.CustomerId, cu.Name, co.Name AS Country FROM Customer cu
				JOIN Country co ON co.CountryId = cu.CountryId
				ORDER BY cu.Name
			""";

		using (var connection = _context.CreateConnection())
		{
			var customers = await connection.QueryAsync<CustomerDto>(query);
			return customers.ToList();
		}
	}

	public async Task<Customer?> GetCustomer(int customerId)
	{
		var query = "SELECT CustomerId, CountryId, Name FROM Customer WHERE CustomerId = @customerId";
		
		using (var connection = _context.CreateConnection())
		{
			return await connection.QuerySingleOrDefaultAsync<Customer>(query, new { customerId });
		}
	}

	public async Task CreateCustomer(CreateCustomerDto customer)
	{
		var query = """
			INSERT INTO Customer (Name, CountryId) 
				VALUES (@Name, @CountryId)
			""";

		var parameters = new DynamicParameters();
		parameters.Add("Name", customer.Name, DbType.String);
		parameters.Add("CountryId", customer.CountryId, DbType.Int32);

		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, parameters);
		}
	}

	public async Task DeleteCustomer(int customerId)
	{
		var query = "DELETE FROM Customer WHERE CustomerId = @customerId";
		using (var connection = _context.CreateConnection())
		{
			await connection.ExecuteAsync(query, new { customerId });
		}
	}
}
