using InternetShop.Contract.Requests;
using InternetShop.Contract.Responses;
using InternetShop.Service;
using InternetShop.Service.Commands;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[Controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync(
            [FromServices] IRequestHandler<IList<CustomerResponse>> getCustomersQuery)
        {
            return Ok(await getCustomersQuery.Handle());
        }

        [HttpGet("{customerId}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomerByIdAsync(long customerId,
            [FromServices] IRequestHandler<long, CustomerResponse> getCustomerByIdQuery)
        {
            return Ok(await getCustomerByIdQuery.Handle(customerId));
        }

        [HttpGet("{customerId}/Orders", Name = "GetCustomerOrders")]
        public async Task<IActionResult> GetCustomerOrdersByIdAsync(long customerId,
            [FromServices] IRequestHandler<long, IList<OrderResponse>> getCustomerOrdersByIdQuery)
        {
            return Ok(await getCustomerOrdersByIdQuery.Handle(customerId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync(
            [FromServices] IRequestHandler<CreateCustomerCommand, CustomerResponse> createCustomerCommand,
            [FromBody] CreateCustomerRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest("First Name or Last Name fields are missing");

            var customer = await createCustomerCommand.Handle(new CreateCustomerCommand
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate
            });

            return CreatedAtRoute(
                "GetCustomer",      // 1. GET-метод для нової сутності
                new { customerId = customer.CustomerId }, // 2. Згенерований ID
                customer                                // 3. Створений об'єкт
            );
        }

        [HttpPatch("{customerId}")]
        public async Task<IActionResult> UpdateCategoryAsync(long customerId,
            [FromServices] IRequestHandler<UpdateCustomerCommand, CustomerResponse> updateCustomerCommand,
            [FromBody] UpdateCustomerRequest request)
        {
            var customer = await updateCustomerCommand.Handle(new UpdateCustomerCommand
            {
                CustomerId = customerId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate
            });

            if (customer == null)
                return NotFound($"Customer with ID {customerId} not found.");

            return Ok(customer);
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomerByIdAsync(int customerId,
            [FromServices] IRequestHandler<DeleteCustomerCommand, bool> deleteCustomerCommand)
        {
            var result = await deleteCustomerCommand.Handle(new DeleteCustomerCommand { CustomerId = customerId });

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
