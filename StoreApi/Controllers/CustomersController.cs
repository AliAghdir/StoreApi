using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi.Contracts;
using StoreApi.Models;

namespace StoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private ICustomerRepository _customerRepository;
        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult GetCustomer()
        {
            var result =  new ObjectResult(_customerRepository.GetAll())
            {
                StatusCode = (int) HttpStatusCode.OK
            };
            Request.HttpContext.Response.Headers.Add("CustomerCount",_customerRepository.GetCountCustomer().ToString());
            return result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int id)
        {
            if (await CustomerExists(id))
            {
                var customer = await _customerRepository.Find(id);
                return Ok(customer);
            }
            else
            {
                return NotFound();
            }
        }

        private async Task<bool> CustomerExists(int id)=>await _customerRepository.IsExists(id);

        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _customerRepository.Add(customer);
            return CreatedAtAction("GetCustomer", new {id = customer.CustomerId}, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] Customer customer)
        {
            await _customerRepository.Update(customer);
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            await _customerRepository.Remove(id);
            return Ok();
        }        
    }
}