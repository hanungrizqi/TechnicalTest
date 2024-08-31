using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;
using Application.Customers.Queries;
using Domain.Entities;
using MediatR;
using System.Threading.Tasks;
using Application.Common.Models; // Pastikan ini sesuai dengan lokasi ApiResponse di proyek Anda

namespace WebAPI.Tests
{
    public class CRUDControllerTests
    {
        [Fact]
        public async Task GetCustomer_ReturnsOkObjectResult_WhenCustomerExists()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerQuery>(), default))
                        .ReturnsAsync(new Customer { CustomerId = 1, CustomerName = "Hanung Rizqi" });

            var controller = new CRUDController(mediatorMock.Object);

            // Act
            var result = await controller.GetCustomer(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ApiResponse<Customer>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result); // Periksa tipe di dalam ActionResult
            var customerResponse = Assert.IsType<ApiResponse<Customer>>(okResult.Value);
            Assert.Equal("Hanung Rizqi", customerResponse.Data.CustomerName);
        }

        [Fact]
        public async Task GetCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerQuery>(), default))
                        .ReturnsAsync((Customer)null);

            var controller = new CRUDController(mediatorMock.Object);

            // Act
            var result = await controller.GetCustomer(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ApiResponse<Customer>>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result); // Periksa tipe di dalam ActionResult
        }
    }
}
