using System.Collections.Generic;
using System.Linq;
using Domain.Model;

namespace Application.Services
{



    public interface ICustomerHelper
    {
        bool DeleteCustomerById(int Id);
        List<CustomerDb> GetAllCustomer();
        CustomerDb GetCustomerById(int Id);
        bool UpdateCustomerById(CustomerDb Customer);
        bool CreateNewCustomer(CustomerDb Customer);
    }
}