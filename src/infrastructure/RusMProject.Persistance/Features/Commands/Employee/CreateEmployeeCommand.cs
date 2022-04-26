using AutoMapper;
using MediatR;
using RusMProjectApplication.Abstraction;
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

namespace RusMProject.Persistance.Features.Commands.Employee
{
    public class CreateEmployeeCommand : IRequest<IBaseResponseModelAble>
    {
        public CreateEmployeeCommand(EmployeeDSO employeeDSO)
        {
            EmployeeDSO = employeeDSO;
        }
        public EmployeeDSO EmployeeDSO { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, IBaseResponseModelAble>
    {
        public CreateEmployeeCommandHandler(IMapper mapper, IEmployeeWriteRepository employeeWriteRepository)
        {
            Mapper = mapper;
            EmployeeWriteRepository = employeeWriteRepository;
        }
        public IMapper Mapper { get; set; }
        public IEmployeeWriteRepository EmployeeWriteRepository;

        public async Task<IBaseResponseModelAble> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                var result = Mapper.Map<RusMProject.Domain.Entities.Employee>(request.EmployeeDSO);

                if (result != null)
                {
                    var success = await EmployeeWriteRepository.Add(result);
                    if (success)
                        return new SuccessResponseModel(ModelMessage.CanAdded, result);
                    throw new ServerErrorException("Server Error");
                }
                return new ErrorResponseModel(ModelMessage.CanNotAdded);
            }
            return new ErrorResponseModel(ModelMessage.CancelToken);
        }
    }
}
