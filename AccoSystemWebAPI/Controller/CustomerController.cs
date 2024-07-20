using AccoSystem.Services;
using AccoSystemWebAPI.DataLayer;
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
    public ActionResult<IEnumerable<Customer>> Get([FromQuery] string? query=null)
    {
        var customer = _customerService.Get(query);
        return Ok(customer);
    }
    
}