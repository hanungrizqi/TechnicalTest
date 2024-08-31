using Domain.Entities;
using MediatR;

namespace Application.Customers.Queries
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<Customer>>
    {
    }
}