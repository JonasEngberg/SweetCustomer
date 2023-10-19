using Microsoft.AspNetCore.Mvc;
using SweetCustomer.Server.Data;
using SweetCustomer.Server.Dto;

namespace SweetCustomer.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerRepository _repository;

	public CustomerController(ILogger<CustomerController> logger, ICustomerRepository repository)
	{
		_logger = logger;
		_repository = repository;
	}

	[HttpGet()]
    public async Task<IActionResult> Get()
    {
        var allCustomers = await _repository.GetCustomers();
        return Ok(allCustomers);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCustomerDto customer)
    {
		try
		{
			await _repository.CreateCustomer(customer);
			return Ok();
		}
		catch (Exception ex)
		{
			return StatusCode(500, ex.Message);
		}
	}

    [HttpDelete("{customerId}")]
    public async Task<IActionResult> Delete(int customerId)
    {
		try
		{
			var dbCustomer = await _repository.GetCustomer(customerId);
			if (dbCustomer == null)
				return NotFound();
			await _repository.DeleteCustomer(customerId);
			return NoContent();
		}
		catch (Exception ex)
		{
			//log error
			return StatusCode(500, ex.Message);
		}
	}
}
