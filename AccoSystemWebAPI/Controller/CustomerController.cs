using AccoSystem.Services;
using AccoSystemWebAPI.DataLayer;
using AccoSystemWebAPI.DataLayer.Dto.Customer;
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
    public ActionResult PostCustomer([FromBody] CreateCustomerDto createCustomerDto)
    {
        try
        {
            var isSuccessful = _customerService.Add(createCustomerDto.FullName, createCustomerDto.Mobile,
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
            var isSuccessful = _customerService.Edit(id,createCustomerDto.FullName, createCustomerDto.Mobile,
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
    
    
}