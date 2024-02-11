using StarWarsShop.DAL.Repositories;
using StarWarsShop.DAL;
using System.Threading.Tasks;

namespace StarWarsShop.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StarWarsShopDBContext _dbContext;

        public IRequestRepository RequestRepo { get; }
        //public ICustomerRepository CustomerRepo { get; }
        //public IStatusRequestRepository StatusRequestRepo { get; }

        public UnitOfWork(StarWarsShopDBContext dbContext)
        {
            _dbContext = dbContext;
            RequestRepo = new RequestRepository(_dbContext);
            //CustomerRepo = new CustomerRepository(_dbContext);
            //StatusRequestRepo = new StatusRequestRepository(_dbContext);
        }


        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>he task result contains the number of state entries written to the database</returns>
        public Task<int> SaveAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
