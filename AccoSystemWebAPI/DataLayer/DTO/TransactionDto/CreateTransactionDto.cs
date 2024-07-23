namespace AccoSystemWebAPI.DataLayer.DTO.TransactionDto;

public class CreateTransactionDto
{
    public int CustomerId { get; set; }
    public int Amount { get; set; }
    public string Description { get; set; }
    public TransactionType Type { get; set; }
}