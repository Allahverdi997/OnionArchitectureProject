using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RusMProject.Persistance.Features.Commands.Employee;
using RusMProject.Persistance.Features.Queries.Department;
using RusMProject.Persistance.Features.Queries.Employee;
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
    public class EmployeeController : ControllerBase
    {
        public IMediator Mediator { get; set; }
        public EmployeeController(IMediator mediator)
        {
            Mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllEmployeeQuery();
            var model = await Mediator.Send(query);
            return Ok(model);
        }
        [HttpGet("{page}/{value}")]
        public async Task<IActionResult> GetAllWithPage(int page, int value, CancellationToken cancellationToken)
        {
            var query = new GetAllEmployeeQueryWithPage(page,value);
            var model = await Mediator.Send(query, cancellationToken);
            return Ok(model);
        }
        [HttpGet("{id}")]
        public  async Task<IActionResult> Get(int id)
        {
            var query = new GetEmployeeQuery(id);
            var model = await Mediator.Send(query);
            return Ok(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(EmployeeDSO employeeDSO)
        {
            var command = new CreateEmployeeCommand(employeeDSO);
            return Ok(await Mediator.Send(command));
        }
        [HttpPut]
        public async Task<IActionResult> Update(EmployeeUpdateDSO employeeUpdateDSO)
        {
            var command = new UpdateEmployeeCommand(employeeUpdateDSO);
            return Ok(await Mediator.Send(command));
        }
        [HttpDelete("{id}")]
        public  async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteEmployeeCommand(id);
            return Ok(await Mediator.Send(command));
        }
    }
}
