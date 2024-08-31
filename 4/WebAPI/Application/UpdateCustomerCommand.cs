using Domain.Entities;
using MediatR;

namespace Application.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? CustomerCode { get; set; }
        public string? CustomerName { get; set; }
        public string CustomerAddress { get; set; } = string.Empty;
        public int? ModifiedBy { get; set; }
    }
}