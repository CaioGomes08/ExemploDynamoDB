using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using DemoDynamoDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoDynamoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IDynamoDBContext _dynamoDBContext;

        public PersonController(IDynamoDBContext dynamoDBContext)
        {
            _dynamoDBContext = dynamoDBContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(string id)
        {
            var persons = await _dynamoDBContext.LoadAsync<Person>(id);
            return Ok(persons);
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            var scanConditions = new List<ScanCondition>();

            var persons = await _dynamoDBContext.ScanAsync<Person>(scanConditions).GetRemainingAsync();
            return Ok(persons);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter(string? street)
        {

            var scanConditions = new List<ScanCondition>();

            var persons = await _dynamoDBContext.ScanAsync<Person>(scanConditions).GetRemainingAsync();

            persons = persons.Where(p => p.Address.Street == street).ToList(); 

            return Ok(persons);
        }


        [HttpPost]
        public async Task<IActionResult> SavePerson(Person person)
        {
                person.Id = Guid.NewGuid().ToString();
                await _dynamoDBContext.SaveAsync(person);
                return Ok(person);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePerson(Person person)
        {
            await _dynamoDBContext.SaveAsync(person);
            return Ok();
        }


    }
}
