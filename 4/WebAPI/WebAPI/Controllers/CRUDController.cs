using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Models;
using Application.Customers.Commands;
using Application.Customers.Queries;
using Domain.Entities;
using MediatR;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CRUDController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/CRUD
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Customer>>>> GetCustomers()
        {
            var customers = await _mediator.Send(new GetAllCustomersQuery());
            return new ApiResponse<IEnumerable<Customer>>("Success", Guid.NewGuid().ToString(), customers);
        }

        // GET: api/CRUD/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Customer>>> GetCustomer(int id)
        {
            var customer = await _mediator.Send(new GetCustomerQuery(id));

            if (customer == null)
            {
                return NotFound();
            }

            var response = new ApiResponse<Customer>("Success", Guid.NewGuid().ToString(), customer);
            return Ok(response);
        }


        // POST: api/CRUD
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Customer>>> PostCustomer(CreateCustomerCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdCustomer = await _mediator.Send(command);

            if (createdCustomer == null)
            {
                return BadRequest(new ApiResponse<Customer>("Failed to create customer", Guid.NewGuid().ToString(), null));
            }

            var response = new ApiResponse<Customer>("Customer created successfully", Guid.NewGuid().ToString(), createdCustomer);

            return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.CustomerId }, response);
        }

        // PUT: api/CRUD/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<Customer>>> PutCustomer(int id, UpdateCustomerCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            command.Id = id;
            var updated = await _mediator.Send(command);

            if (!updated)
            {
                return NotFound(new ApiResponse<Customer>("Customer not found or invalid ID", Guid.NewGuid().ToString(), null));
            }

            var dataUpdated = await _mediator.Send(new GetCustomerQuery(id));
            return Ok(new ApiResponse<Customer>("Customer updated successfully", Guid.NewGuid().ToString(), dataUpdated));
        }

        // DELETE: api/CRUD/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteCustomer(int id)
        {
            var deleted = await _mediator.Send(new DeleteCustomerCommand(id));

            if (!deleted)
            {
                return NotFound(new ApiResponse<string>("Customer not found", Guid.NewGuid().ToString(), null));
            }

            var dataUpdated = await _mediator.Send(new GetCustomerQuery(id));
            return Ok(new ApiResponse<string>("Customer deleted successfully", Guid.NewGuid().ToString(), null));
        }
    }
}
