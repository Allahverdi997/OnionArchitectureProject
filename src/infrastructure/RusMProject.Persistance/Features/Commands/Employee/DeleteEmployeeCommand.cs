using AutoMapper;
using MediatR;
using RusMProjectApplication.Abstraction;
using RusMProjectApplication.Abstraction.Repositories.ApplicationRepositories.Read;
using RusMProjectApplication.Abstraction.Repositories.ApplicationRepositories.Write;
using RusMProjectApplication.Exceptions;
using RusMProjectApplication.Messages;
using RusMProjectApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RusMProject.Persistance.Features.Commands.Employee
{
    public class DeleteEmployeeCommand : IRequest<IBaseResponseModelAble>
    {
        public DeleteEmployeeCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, IBaseResponseModelAble>
    {
        public DeleteEmployeeCommandHandler(IMapper mapper, IUnitOfWorkAble unitOfWork)
        { 
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }
        public IMapper Mapper { get; set; }
        public IUnitOfWorkAble UnitOfWork { get; set; }

        public async Task<IBaseResponseModelAble> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                var result =await UnitOfWork.EmployeeReadRepository.GetAsync(request.Id);
                if (result != null)
                {
                    var success =UnitOfWork.EmployeeWriteRepository.Remove(result);
                    if (success)
                        return new SuccessResponseModel(ModelMessage.CanDeleted, result);
                    throw new ServerErrorException("Server Error");
                }
                return new ErrorResponseModel(ModelMessage.CanNotAdded);
            }
            return new ErrorResponseModel(ModelMessage.CancelToken);
        }
    }
}
