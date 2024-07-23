using AccoSystemWebAPI.DataLayer;
using AccoSystemWebAPI.DataLayer.Dto.Customer;
using Microsoft.IdentityModel.Tokens;

namespace AccoSystemWebAPI.Services;

public class CustomerService : ICustomerService
{
    private readonly AccoSystemDbContext _dbContext;

    public CustomerService(AccoSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<CustomerDto> Get(string? query = null)
    {
        List<CustomerDto>? customer;
        if (query.IsNullOrEmpty())
        {
            customer = _dbContext.Customers.Select(customer => new CustomerDto()
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FullName,
                Addrese = customer.Addrese,
                Email = customer.Email,
                Mobile = customer.Mobile
            }).ToList();

            return customer;
        }

        customer = _dbContext.Customers.Where(a => a.FullName == query).Select(customer => new CustomerDto()
        {
            CustomerId = customer.CustomerId,
            FullName = customer.FullName,
            Addrese = customer.Addrese,
            Email = customer.Email,
            Mobile = customer.Mobile
        }).ToList();
        return customer;
    }

    public async Task<bool> AddAsync(string fullName, string mobile, string addrese, string email)
    {
        _dbContext.Customers.Add(new Customer()
        {
            FullName = fullName,
            Mobile = mobile,
            Addrese = addrese,
            Email = email
        });
        var isSuccessful = await _dbContext.SaveChangesAsync();

        return isSuccessful > 0;
    }

    public bool Edit(int id, string fullName, string mobile, string addrese, string email)
    {
        _dbContext.Customers.Update(new Customer()
        {
            CustomerId = id,
            FullName = fullName,
            Mobile = mobile,
            Addrese = addrese,
            Email = email
        });
        var isSuccessful = _dbContext.SaveChanges();

        return isSuccessful > 0;
    }

    public bool Delete(int id)
    {
        var customer = _dbContext.Customers.FirstOrDefault(c => c.CustomerId == id);
        _dbContext.Customers.Remove(customer);
        var isSuccessful = _dbContext.SaveChanges();
        //using var unit = new UnitOfWork(new AccoSystemDbContext());
        //var isSuccessful = unit.CustomerRepository.Delete(id);
        //unit.Save();

        return isSuccessful > 0;
    }

    // public List<Customer> Search(string query)
    // {
    //     using var unit = new UnitOfWork(new AccoSystemDbContext());
    //     return unit.CustomerRepository.Get(query).ToList();
    // }
}