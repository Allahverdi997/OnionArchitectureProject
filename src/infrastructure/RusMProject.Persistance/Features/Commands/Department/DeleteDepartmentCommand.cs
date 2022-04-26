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

namespace RusMProject.Persistance.Features.Commands.Department
{
    public class DeleteDepartmentCommand : IRequest<IBaseResponseModelAble>
    {
        public DeleteDepartmentCommand(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, IBaseResponseModelAble>
    {
        public DeleteDepartmentCommandHandler(IMapper mapper, IUnitOfWorkAble unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }
        public IMapper Mapper { get; set; }
        public IUnitOfWorkAble UnitOfWork { get; set; }

        public async Task<IBaseResponseModelAble> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                var result =await UnitOfWork.DepartmentReadRepository.GetAsync(request.Id);
                if (result != null)
                {
                    var rep = UnitOfWork.DepartmentWriteRepository;
                    var success =rep.Remove(result);
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
