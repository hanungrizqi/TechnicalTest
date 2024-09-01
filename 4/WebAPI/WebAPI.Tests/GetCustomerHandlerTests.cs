using Xunit;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Persistence;
using Domain.Entities;
using Application.Customers.Queries;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Tests.Handlers
{
    public class GetCustomerHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsCustomer_WhenCustomerExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<_dbContext>()
                .UseInMemoryDatabase(databaseName: "DB_TECHNICAL_TEST")
                .Options;

            using var context = new _dbContext(options);
            context.Customers.Add(new Customer { CustomerId = 1, CustomerName = "Hanung Rizqi" });
            await context.SaveChangesAsync();

            var handler = new GetCustomerHandler(context);

            // Act
            var result = await handler.Handle(new GetCustomerQuery(1), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Hanung Rizqi", result.CustomerName);
        }

        [Fact]
        public async Task Handle_ReturnsNull_WhenCustomerDoesNotExist()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<_dbContext>()
                .UseInMemoryDatabase(databaseName: "DB_TECHNICAL_TEST")
                .Options;

            using var context = new _dbContext(options);

            var handler = new GetCustomerHandler(context);

            // Act
            var result = await handler.Handle(new GetCustomerQuery(1), CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
