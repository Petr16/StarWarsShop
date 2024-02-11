using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StarWarsShop.DAL.Repositories;

namespace StarWarsShop.DAL
{
    public interface IUnitOfWork
    {
        IRequestRepository RequestRepo { get; }
        //ICustomerRepository CustomerRepo { get; }
        //IStatusRequestRepository StatusRequestRepo { get; }

        Task<int> SaveAsync();
    }
}