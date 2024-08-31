using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Customers.Queries
{
    public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, Customer?>
    {
        private readonly _dbContext _context;

        public GetCustomerHandler(_dbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers.FindAsync(request.Id);
        }

    }
}
