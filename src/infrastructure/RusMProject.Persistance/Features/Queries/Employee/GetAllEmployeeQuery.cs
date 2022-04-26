using AutoMapper;
using MediatR;
using RusMProject.Persistance.Utilities.CrossCustingConcerns.Aspects.Transaction;
using RusMProjectApplication.Abstraction;
using RusMProjectApplication.Abstraction.Repositories.ApplicationRepositories.Read;
using RusMProjectApplication.Messages;
using RusMProjectApplication.Models;
using RusMProjectApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RusMProject.Persistance.Features.Queries.Employee
{
    public class GetAllEmployeeQuery : IRequest<IBaseResponseModelAble>
    {
    }

    public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, IBaseResponseModelAble>
    {
        public GetAllEmployeeQueryHandler(IMapper mapper, IEmployeeReadRepository employeeReadRepository)
        {
            Mapper = mapper;
            EmployeeReadRepository = employeeReadRepository;
        }
        public IMapper Mapper { get; set; }
        public readonly IEmployeeReadRepository EmployeeReadRepository;
        [TransactionAspect]
        public async Task<IBaseResponseModelAble> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                var model = EmployeeReadRepository.GetEmployeesWithInclude().ToList();

                var result = Mapper.Map<List<GetEmployeeResponseModel>>(model);

                if (result.Count != 0)
                    return new SuccessResponseModel(ModelMessage.Data, result);

                return new ErrorResponseModel(ModelMessage.NoData);
            }
            return new ErrorResponseModel(ModelMessage.CancelToken);
        }
    }
}
