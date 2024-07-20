using AccoSystem.Services;
using AccoSystemWebAPI.DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace AccoSystemWebAPI.Controller;

[Route("api/[controller]/[action]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transaction;

    public TransactionController(ITransactionService transactionService)
    {
        _transaction = transactionService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Accounting>> Get([FromQuery] TransactionType? type)
    {
        //try
        //{
            var transaction = _transaction.Get(type ?? default);
            return Ok(transaction);
        //}
        // catch (Exception ex)
        // {
        //     // Log the exception (not shown here)
        //     return StatusCode(500, "Internal server error");
        // }
    }
}