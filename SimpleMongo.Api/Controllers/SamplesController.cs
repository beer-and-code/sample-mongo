using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleMongo.Api.Models;
using SimpleMongo.Api.Repositories;

namespace SimpleMongo.Api.Controllers
{
    [Route("api/samples")]
    [ApiController]
    public class SamplesController : ControllerBase
    {
        private readonly IMongoRepository<Sample> _repository;
        
        public SamplesController(IMongoRepository<Sample> repository)
        {
            _repository = repository;
        }
        

        [HttpGet]
        public async Task<IEnumerable<Sample>> Get()
        {
            return await  _repository.FindAll();
        }

        [HttpGet("{id}")]
        public async Task<Sample> Get(string id)
        {
            return await _repository.FindById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Sample sample)
        {
            await _repository.Add(sample);

            return CreatedAtAction (nameof (Get), new { id = sample.Id }, sample);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Sample sample)
        {
            sample.LoadId(id);
            await _repository.Update(sample);
            return Accepted();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _repository.Delete(id);
            return Ok();
        }
    }
}
