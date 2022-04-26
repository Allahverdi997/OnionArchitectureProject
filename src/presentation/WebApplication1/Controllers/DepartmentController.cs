using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RusMProject.Persistance.Features.Commands.Department;
using RusMProject.Persistance.Features.Queries.Department;
using RusMProjectApplication.Registration.CreateDSO;
using RusMProjectApplication.Registration.UpdateDSO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RusMProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        public IMediator Mediator { get; set; }
        public DepartmentController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query =new GetAllDepartmentQuery();
            var model = await Mediator.Send(query, cancellationToken);
            return Ok(model);
        }
        [HttpGet("{page}/{value}")]
        public async Task<IActionResult> GetAllWithPage(int page,int value,CancellationToken cancellationToken)
        {
            var query = new GetAllDepartmentQueryWithPage(page,value);
            var model = await Mediator.Send(query, cancellationToken);
            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetDepartmentQuery(id);
            var model = await Mediator.Send(query);
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(DepartmentDSO departmentDSO)
        {
            var command = new CreateDepartmentCommand(departmentDSO);
            return Ok(await Mediator.Send(command));
        }
        [HttpPut]
        public async Task<IActionResult> Update(DepartmentUpdateDSO departmentUpdateDSO)
        {
            var command = new UpdateDepartmentCommand(departmentUpdateDSO);
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteDepartmentCommand(id);
            return Ok(await Mediator.Send(command));
        }
    }
}
