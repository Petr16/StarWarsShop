using StarWarsShop.DAL.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StarWarsShop.DAL.Repositories
{
    public interface IRequestRepository : IGenericRepository<Request>
    {
        /// <summary>
        /// Данный метод является лишь примером вызова хранимой функции.
        /// <para/>
        /// В нормальных условиях список заявок должен быть получен средствами Entity Framework.
        /// </summary>
        Task<List<Request>> GetRequestsStoredFuncExample(CancellationToken cancellationToken);
    }
}