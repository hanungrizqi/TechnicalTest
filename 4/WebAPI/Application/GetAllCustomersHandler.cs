using Domain.Entities;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.Customers.Queries
{
    public class GetAllCustomersHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<Customer>>
    {
        private readonly _dbContext _context;

        public GetAllCustomersHandler(_dbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers.ToListAsync();
        }
    }
}