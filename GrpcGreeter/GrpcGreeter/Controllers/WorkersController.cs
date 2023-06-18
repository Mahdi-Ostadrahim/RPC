using Microsoft.AspNetCore.Mvc;
using GrpcGreeter.Models;
using GrpcGreeter.Services;
namespace GrpcGreeter.Controllers
{
    public class WorkersController : ControllerBase
    {
        private readonly WorkersService _workersService;

        public WorkersController(WorkersService workersService) =>
            _workersService = workersService;

        [HttpGet]
        public async Task<List<Worker>> Get() =>
            await _workersService.GetAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Worker>> Get(string id)
        {
            var worker = await _workersService.GetAsync(id);

            if (worker is null)
            {
                return NotFound();
            }

            return worker;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Worker newWorker)
        {
            await _workersService.CreateAsync(newWorker);

            return CreatedAtAction(nameof(Get), new { id = newWorker._nationalID }, newWorker);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Worker updatedWorker)
        {
            var worker = await _workersService.GetAsync(id);

            if (worker is null)
            {
                return NotFound();
            }

            updatedWorker._nationalID = worker._nationalID;

            await _workersService.UpdateAsync(id, updatedWorker);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var worker = await _workersService.GetAsync(id);

            if (worker is null)
            {
                return NotFound();
            }

            await _workersService.RemoveAsync(id);

            return NoContent();
        }
    }
}
