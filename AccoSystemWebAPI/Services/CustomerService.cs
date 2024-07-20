using AccoSystem.DataLayer;
using AccoSystem.DataLayer.Context;
using AccoSystemWebAPI.DataLayer;
using AccoSystemWebAPI.DataLayer.Dto.Customer;
using Microsoft.IdentityModel.Tokens;

namespace AccoSystem.Services;

public class CustomerService : ICustomerService
{
    public List<CustomerDto> Get(string? query = null)
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        List<CustomerDto>? customer;
        if (query.IsNullOrEmpty())
        {
            customer = unit.CustomerRepository.Get().Select(customer => new CustomerDto()
            {
                CustomerId = customer.CustomerId,
                FullName = customer.FullName,
                Addrese = customer.Addrese,
                Email = customer.Email,
                Mobile = customer.Mobile
            }).ToList();

            return customer;
        }
        customer = unit.CustomerRepository.Get(a => a.FullName == query).Select(customer => new CustomerDto()
        {
            CustomerId = customer.CustomerId,
            FullName = customer.FullName,
            Addrese = customer.Addrese,
            Email = customer.Email,
            Mobile = customer.Mobile
        }).ToList();
        return customer;
    }

    public bool Add(string fullName, string mobile, string addrese, string email)
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        var isSuccessful = unit.CustomerRepository.Insert(new Customer()
        {
            FullName = fullName,
            Mobile = mobile,
            Addrese = addrese,
            Email = email
        });
        unit.Save();

        return isSuccessful;
    }

    public bool Edit(int id, string fullName, string mobile, string addrese, string email)
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        var isSuccessful = unit.CustomerRepository.Update(new Customer()
        {
            CustomerId = id,
            FullName = fullName,
            Mobile = mobile,
            Addrese = addrese,
            Email = email
        });
        unit.Save();

        return isSuccessful;
    }

    public bool Delete(int id)
    {
        using var unit = new UnitOfWork(new AccoSystemDbContext());
        var isSuccessful = unit.CustomerRepository.Delete(id);
        unit.Save();

        return isSuccessful;
    }

    // public List<Customer> Search(string query)
    // {
    //     using var unit = new UnitOfWork(new AccoSystemDbContext());
    //     return unit.CustomerRepository.Get(query).ToList();
    // }
}