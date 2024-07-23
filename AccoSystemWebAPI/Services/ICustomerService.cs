using AccoSystemWebAPI.DataLayer.Dto.Customer;

namespace AccoSystemWebAPI.Services;

public interface ICustomerService
{
    public List<CustomerDto> Get(string? query = null);
    public Task<bool> AddAsync(string fullName, string mobile, string addrese, string email);

    public bool Edit(int id, string fullName, string mobile, string addrese, string email);

    public bool Delete(int id);

    //public List<Customer> Search(string query);
}