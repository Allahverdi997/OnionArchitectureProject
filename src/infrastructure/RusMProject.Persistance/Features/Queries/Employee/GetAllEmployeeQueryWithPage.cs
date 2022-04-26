using AutoMapper;
using MediatR;
using RusMProject.Persistance.Utilities.CrossCustingConcerns.Aspects.Logging;
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
    public class GetAllEmployeeQueryWithPage : IRequest<IBaseResponseModelAble>
    {
        public GetAllEmployeeQueryWithPage(int page, int value)
        {
            Page = page;
            Value = value;
        }
        public GetAllEmployeeQueryWithPage(int value)
        {
            Value = value;
        }
        public int Page { get; set; }
        public int Value { get; set; }
    }

    public class GetAllEmployeeQueryWithPageHandler : IRequestHandler<GetAllEmployeeQueryWithPage, IBaseResponseModelAble>
    {
        public GetAllEmployeeQueryWithPageHandler(IMapper mapper, IEmployeeReadRepository employeeReadRepository)
        {
            Mapper = mapper;
            EmployeeReadRepository = employeeReadRepository;
        }
        public IMapper Mapper { get; set; }
        public readonly IEmployeeReadRepository EmployeeReadRepository;

        [LoggingAspect(Priority = 1)]
        public async Task<IBaseResponseModelAble> Handle(GetAllEmployeeQueryWithPage request, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                var model = EmployeeReadRepository.GetEmployeesWithInclude();
                if (request.Page == 1 || request.Page == 0)
                {
                    model = model.Take(request.Value);
                }
                else
                {
                    model = model.Skip((request.Page - 1) * request.Value).Take(request.Value);
                }

                var result = Mapper.Map<List<GetEmployeeResponseModel>>(model);

                if (result.Count != 0)
                    return new SuccessResponseModel(ModelMessage.Data, result);

                return new ErrorResponseModel(ModelMessage.NoData);
            }
            return new ErrorResponseModel(ModelMessage.CancelToken);
        }
    }
}
