using AccoSystemWebAPI.DataLayer.Dto.Customer;
using AccoSystemWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccoSystemWebAPI.Controller;

[Route("api/[controller]/[action]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CustomerDto>> GetCustomer([FromQuery] string? query = null)
    {
        var customer = _customerService.Get(query);
        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult> PostCustomer([FromBody] CreateCustomerDto createCustomerDto)
    {
        try
        {
            var isSuccessful = await _customerService.AddAsync(createCustomerDto.FullName, createCustomerDto.Mobile,
                createCustomerDto.Addrese, createCustomerDto.Email);
            if (isSuccessful)
            {
                return Ok("Customer added successfully");
            }
            else
            {
                return BadRequest("Failed to add customer");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public ActionResult Edit(int id, [FromBody] CreateCustomerDto createCustomerDto)
    {
        try
        {
            var isSuccessful = _customerService.Edit(id, createCustomerDto.FullName, createCustomerDto.Mobile,
                createCustomerDto.Addrese, createCustomerDto.Email);
            if (isSuccessful)
            {
                return Ok("Customer edited successfully");
            }
            else
            {
                return BadRequest("Failed to add customer");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var isSuccessful = _customerService.Delete(id);
            if (isSuccessful)
            {
                return Ok("Customer deleted successfully");
            }
            else
            {
                return BadRequest("Failed to add customer");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}