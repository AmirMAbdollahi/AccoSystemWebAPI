using System.Linq.Expressions;
using AccoSystem.DataLayer;
using AccoSystem.DataLayer.Context;
using AccoSystemWebAPI.DataLayer;

namespace AccoSystemWebAPI.Services;

public class TransactionService : ITransactionService
{
    private AccoSystemDbContext _dbContext;
    public TransactionService(AccoSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Accounting> Get(TransactionType type = default)
    {
        return type is TransactionType.Cost or TransactionType.Income
            ? _dbContext.Accountings.Where(a => a.TransactionType == type).ToList()
            : _dbContext.Accountings.ToList();
    }

    public bool Add(int customerId, int amount, TransactionType type, string description)
    {
        _dbContext.Accountings.Add(new Accounting()
        {
            CustomerId = customerId,
            Amount = amount,
            TransactionType = type,
            DateTime = DateTime.Now,
            Description = description,
        });
        var isSuccessful =_dbContext.SaveChanges();

        return isSuccessful>0;
    }

    public bool Edit(int id, int customerId, int amount, TransactionType type, string description)
    {
        _dbContext.Accountings.Update(new Accounting()
        {
            Id = id,
            DateTime = DateTime.Now,
            Amount = amount,
            Description = description,
            CustomerId = customerId,
            TransactionType = type
        });
        var isSuccessful =_dbContext.SaveChanges();

        return isSuccessful>0;
    }

    public bool Delete(int id)
    {
        var accounting = _dbContext.Accountings.FirstOrDefault(a => a.Id == id);
        _dbContext.Accountings.Remove(accounting);
        var isSuccessful =_dbContext.SaveChanges();

        return isSuccessful>0;
    }

    public List<Accounting> Search(DateTime fromDate, DateTime toDate)
    {
        var result = Get();
        result = result.Where(r => r.DateTime >= fromDate && r.DateTime <= toDate).ToList();

        return result;
    }
}