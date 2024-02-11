using AutoMapper;
using StarWarsShop.BLL.ViewModels;
using StarWarsShop.DAL.Entities;
using StarWarsShop.DAL.Repositories;
using StarWarsShop.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StarWarsShop.BLL.Services
{
    public class RequestService
    {
        private readonly IMapper _mapper;


        public RequestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IUnitOfWork UnitOfWork { get; }

        public IRequestRepository RequestRepo => UnitOfWork.RequestRepo;

        public async Task<List<RequestVM>> GetAll(CancellationToken cancellationToken = default)
        {
            List<Request> requests = await RequestRepo.GetAllToListAsync(cancellationToken);

            // Пример вызова хранимой функции
            //List<Request> requests1 = await UnitOfWork.RequestRepo.GetRequestsStoredFuncExample(cancellationToken);

            return _mapper.Map<List<RequestVM>>(requests);
        }

        /// <summary>
        /// Новый метод для работы с devextreme
        /// </summary>
        /// <returns></returns>
        public IQueryable<RequestVM> GetAll()
        {
            IQueryable<Request> requests = RequestRepo.GetAll();
            return _mapper.ProjectTo<RequestVM>(requests);
        }


        public async Task<RequestVM> Get(int id)
        {
            Request request = await RequestRepo.GetByIdAsync(id);
            return _mapper.Map<RequestVM>(request);
        }

        public async Task<RequestVM> Create(RequestVM requestVM)
        {
            Request newRequest = _mapper.Map<Request>(requestVM);
            await RequestRepo.AddAsync(newRequest);
            await UnitOfWork.SaveAsync();

            return _mapper.Map<RequestVM>(newRequest);
        }

        public async Task Update(RequestVM requestVM)
        {
            Request existingRequest = await RequestRepo.GetByIdAsync(requestVM.Id);
            if (existingRequest == null)
                throw new ArgumentException($"Изменения не сохранены, т.к. заявка с ID={requestVM.Id} не найдена.");

            _mapper.Map(requestVM, existingRequest);
            await UnitOfWork.SaveAsync();
        }

        public async Task Delete(int id)
        {
            bool foundAndRemoved = await RequestRepo.RemoveByIdAsync(id);
            if (!foundAndRemoved)
                throw new ArgumentException($"Удаление не выполнено, т.к. заявка с ID={id} не найдена");

            await UnitOfWork.SaveAsync();
        }
    }
}
