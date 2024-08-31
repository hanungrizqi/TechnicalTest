using Domain.Entities;
using MediatR;

namespace Application.Customers.Queries
{
    public class GetCustomerQuery : IRequest<Customer>
    {
        public int Id { get; set; }

        public GetCustomerQuery(int id)
        {
            Id = id;
        }
    }
}
