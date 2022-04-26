using AutoMapper;
using MediatR;
using RusMProject.Persistance.Statics;
using RusMProjectApplication.Abstraction;
using RusMProjectApplication.Abstraction.Repositories.ApplicationRepositories.Write;
using RusMProjectApplication.Exceptions;
using RusMProjectApplication.Messages;
using RusMProjectApplication.Models;
using RusMProjectApplication.Registration.UpdateDSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RusMProject.Persistance.Features.Commands.Employee
{
    public class UpdateEmployeeCommand : IRequest<IBaseResponseModelAble>
    {
        public UpdateEmployeeCommand(EmployeeUpdateDSO employeeUpdateDSO)
        {
            EmployeeUpdateDSO = employeeUpdateDSO;
        }
        public EmployeeUpdateDSO EmployeeUpdateDSO { get; set; }
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, IBaseResponseModelAble>
    {
        public UpdateEmployeeCommandHandler(IMapper mapper, IUnitOfWorkAble unitOfWorkAble)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWorkAble;
        }
        public IMapper Mapper { get; set; }
        public IUnitOfWorkAble UnitOfWork;

        public async Task<IBaseResponseModelAble> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                var result = Mapper.Map<RusMProject.Domain.Entities.Employee>(request.EmployeeUpdateDSO);

                if (result != null)
                {
                    var entity = new RusMProject.Domain.Entities.Employee();
                    entity =await UnitOfWork.EmployeeReadRepository.GetAsync(request.EmployeeUpdateDSO.Id);

                    if (!string.IsNullOrEmpty(request.EmployeeUpdateDSO.Name))
                        entity.Name = request.EmployeeUpdateDSO.Name;

                    if (request.EmployeeUpdateDSO.BirthDate!=DateTime.MinValue)
                        entity.BirthDate = request.EmployeeUpdateDSO.BirthDate;

                    if (request.EmployeeUpdateDSO.DepartmentId!=0)
                        entity.DepartmentId = request.EmployeeUpdateDSO.DepartmentId;

                    if (!string.IsNullOrEmpty(request.EmployeeUpdateDSO.Surname))
                        entity.Surname = request.EmployeeUpdateDSO.Surname;

                    var success =UnitOfWork.EmployeeWriteRepository.Update(entity);
                    if (success)
                        return new SuccessResponseModel(ModelMessage.CanUpdated, result);
                    throw new ServerErrorException("Server Error");
                }
                return new ErrorResponseModel(ModelMessage.CanNotAdded);
            }
            return new ErrorResponseModel(ModelMessage.CancelToken);
        }
    }
}
