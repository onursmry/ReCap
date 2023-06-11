using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<CustomerDetailDto>> GetCustomerDetails();
        IResult Add(Customer customer);
        IDataResult<Customer> GetById(int customerId);
        IDataResult<Customer> GetByUserId(int userId);
        IResult Update(Customer customer);
        IResult Delete(Customer customer);
    }
}
