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

namespace RusMProject.Persistance.Features.Queries.Department
{
    public class GetAllDepartmentQueryWithPage : IRequest<IBaseResponseModelAble>
    {
        public GetAllDepartmentQueryWithPage(int page,int value)
        {
            Page = page;
            Value = value;
        }
        public int Page { get; set; }
        public int Value { get; set; }
    }

    public class GetAllDepartmentQueryWithPageHandler : IRequestHandler<GetAllDepartmentQueryWithPage, IBaseResponseModelAble>
    {
        public GetAllDepartmentQueryWithPageHandler(IMapper mapper, IDepartmentReadRepository departmentReadRepository)
        {
            Mapper = mapper;
            DepartmentReadRepository = departmentReadRepository;
        }
        public IMapper Mapper { get; set; }
        public readonly IDepartmentReadRepository DepartmentReadRepository;

        [LoggingAspect(Priority = 1)]
        public async Task<IBaseResponseModelAble> Handle(GetAllDepartmentQueryWithPage request, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                var model = DepartmentReadRepository.GetAll();
                if (request.Page == 1 || request.Page == 0)
                {
                    model = model.Take(request.Value);
                }
                else
                {
                    model = model.Skip((request.Page - 1) * request.Value).Take(request.Value);
                }
                
                var result = Mapper.Map<List<GetDepartmentResponseModel>>(model);

                if (result.Count != 0)
                    return new SuccessResponseModel(ModelMessage.Data, result);

                return new ErrorResponseModel(ModelMessage.NoData);
            }
            return new ErrorResponseModel(ModelMessage.CancelToken);
        }
    }
}
