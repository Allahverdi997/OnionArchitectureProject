using AutoMapper;
using MediatR;
using RusMProjectApplication.Abstraction;
using RusMProjectApplication.Abstraction.Repositories.ApplicationRepositories.Read;
using RusMProjectApplication.Abstraction.Repositories.ApplicationRepositories.Write;
using RusMProjectApplication.Exceptions;
using RusMProjectApplication.Messages;
using RusMProjectApplication.Models;
using RusMProjectApplication.Registration.CreateDSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RusMProject.Persistance.Features.Commands.Department
{
    public class CreateDepartmentCommand:IRequest<IBaseResponseModelAble>
    {
        public CreateDepartmentCommand(DepartmentDSO departmentDSO)
        {
            DepartmentDSO = departmentDSO;
        }
        public DepartmentDSO DepartmentDSO { get; set; }
    }

    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, IBaseResponseModelAble>
    {
        public CreateDepartmentCommandHandler(IMapper mapper, IDepartmentWriteRepository departmentWriteRepository)
        {
            Mapper = mapper;
            DepartmentWriteRepository = departmentWriteRepository;
        }
        public IMapper Mapper { get; set; }
        public IDepartmentWriteRepository DepartmentWriteRepository;

        public async Task<IBaseResponseModelAble> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                var result = Mapper.Map<RusMProject.Domain.Entities.Department>(request.DepartmentDSO);

                if (result != null)
                {
                    var success=await DepartmentWriteRepository.Add(result);
                    if (success)
                        return new SuccessResponseModel(ModelMessage.CanAdded,result);
                    throw new ServerErrorException("Server Error");
                }
                return new ErrorResponseModel(ModelMessage.CanNotAdded);
            }
            return new ErrorResponseModel(ModelMessage.CancelToken);
        }
    }
}
