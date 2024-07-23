using AccoSystemWebAPI.DataLayer;
using AccoSystemWebAPI.DataLayer.DTO.TransactionDto;
using AccoSystemWebAPI.Services;
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
        var transaction = _transaction.Get(type ?? default);
        return Ok(transaction);
    }

    [HttpPost]
    public ActionResult PostTransaction([FromBody] CreateTransactionDto transactionDto)
    {
        try
        {
            var isSuccessful = _transaction.Add(transactionDto.CustomerId, transactionDto.Amount,
                transactionDto.Type, transactionDto.Description);
            if (isSuccessful)
            {
                return Ok("Transaction added successfully");
            }
            else
            {
                return BadRequest("Failed to add Transaction");
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id}")]
    public ActionResult PutTransaction(int id,CreateTransactionDto transactionDto)
    {
        try
        {
            var isSuccessful = _transaction.Edit(id,transactionDto.CustomerId, transactionDto.Amount,
                transactionDto.Type, transactionDto.Description);
            if (isSuccessful)
            {
                return Ok("Transaction edited successfully");
            }
            else
            {
                return BadRequest("Failed to edit Transaction");
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteTransaction(int id)
    {
        try
        {
            var isSuccessful = _transaction.Delete(id);
            if (isSuccessful)
            {
                return Ok("Transaction deleted successfully");
            }
            else
            {
                return BadRequest("Failed to delete Transaction");
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal server error");
        }
    }
    
}