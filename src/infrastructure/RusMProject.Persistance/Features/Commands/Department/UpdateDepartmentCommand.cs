using AutoMapper;
using MediatR;
using RusMProject.Persistance.Statics;
using RusMProject.Persistance.Utilities.CrossCustingConcerns.Aspects.Transaction;
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

namespace RusMProject.Persistance.Features.Commands.Department
{
    public class UpdateDepartmentCommand : IRequest<IBaseResponseModelAble>
    {
        public UpdateDepartmentCommand(DepartmentUpdateDSO departmentUpdateDSO)
        {
            DepartmentUpdateDSO = departmentUpdateDSO;
        }
        public DepartmentUpdateDSO DepartmentUpdateDSO { get; set; }
    }

    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, IBaseResponseModelAble>
    {
        public UpdateDepartmentCommandHandler(IMapper mapper, IUnitOfWorkAble unitOfWorkAble)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWorkAble;
        }
        public IMapper Mapper { get; set; }
        public IUnitOfWorkAble UnitOfWork;

        
        public async Task<IBaseResponseModelAble> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                var result = Mapper.Map<RusMProject.Domain.Entities.Department>(request.DepartmentUpdateDSO);

                if (result != null)
                {
                    var entity = new RusMProject.Domain.Entities.Department();
                    entity = await UnitOfWork.DepartmentReadRepository.GetAsync(request.DepartmentUpdateDSO.Id);
                    if (!string.IsNullOrEmpty(request.DepartmentUpdateDSO.Name))
                        entity.Name = request.DepartmentUpdateDSO.Name;

                    var success = UnitOfWork.DepartmentWriteRepository.Update(entity);
                    if (success)
                        return new SuccessResponseModel(ModelMessage.CanUpdated, result);
                    throw new ServerErrorException("Server Error");
                }
                return new ErrorResponseModel(ModelMessage.CanNotUpdated);
            }
            return new ErrorResponseModel(ModelMessage.CancelToken);
        }
    }
}
