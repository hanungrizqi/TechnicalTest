using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;
using Application.Customers.Commands;
using Application.Customers.Queries;
using Domain.Entities;
using MediatR;
using System.Threading.Tasks;
using Application.Common.Models;

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
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
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
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task CreateCustomer_ReturnsCreatedAtActionResult_WhenCustomerIsCreated()
        {
            // Arrange
            var createCommand = new CreateCustomerCommand
            {
                CustomerCode = "ABC123",
                CustomerName = "Hanung Rizqi",
                CustomerAddress = "Pemalang",
                CreatedBy = 1
            };
            var createdCustomer = new Customer
            {
                //CustomerId = 99,
                CustomerCode = createCommand.CustomerCode,
                CustomerName = createCommand.CustomerName,
                CustomerAddress = createCommand.CustomerAddress,
                CreatedBy = createCommand.CreatedBy
            };

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<CreateCustomerCommand>(), default))
                        .ReturnsAsync(createdCustomer);

            var controller = new CRUDController(mediatorMock.Object);

            // Act
            var result = await controller.PostCustomer(createCommand);

            // Assert
            Assert.NotNull(result); 
            var actionResult = Assert.IsType<ActionResult<ApiResponse<Customer>>>(result);
            Assert.NotNull(actionResult.Result);  

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var customerResponse = Assert.IsType<ApiResponse<Customer>>(createdAtActionResult.Value);
            Assert.Equal("Hanung Rizqi", customerResponse.Data.CustomerName);
            Assert.Equal("ABC123", customerResponse.Data.CustomerCode);
        }

        [Fact]
        public async Task UpdateCustomer_ReturnsOk_WhenCustomerIsUpdated()
        {
            // Arrange
            var updateCommand = new UpdateCustomerCommand
            {
                Id = 4,
                CustomerCode = "DEV014",
                CustomerName = "Hanung Rizqi",
                CustomerAddress = "Pemalang",
                ModifiedBy = 2
            };

            var updatedCustomer = new Customer
            {
                CustomerId = 4,
                CustomerCode = "DEV014",
                CustomerName = "Hanung Rizqi",
                CustomerAddress = "Pemalang",
                ModifiedBy = 2,
                ModifiedAt = DateTime.Now
            };

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateCustomerCommand>(), default))
                        .ReturnsAsync(true);

            mediatorMock.Setup(m => m.Send(It.IsAny<GetCustomerQuery>(), default))
                        .ReturnsAsync(updatedCustomer);

            var controller = new CRUDController(mediatorMock.Object);

            // Act
            var result = await controller.PutCustomer(1, updateCommand);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ApiResponse<Customer>>>(result);
            Assert.NotNull(actionResult.Result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<ApiResponse<Customer>>(okResult.Value);
            Assert.Equal("Customer updated successfully", response.Message);
            Assert.Equal("Hanung Rizqi", response.Data.CustomerName);
        }

        [Fact]
        public async Task UpdateCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var updateCommand = new UpdateCustomerCommand
            {
                Id = 4,
                CustomerCode = "DEV014",
                CustomerName = "Hanung Rizqi",
                CustomerAddress = "Pemalang",
                ModifiedBy = 2
            };

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateCustomerCommand>(), default))
                        .ReturnsAsync(false);

            var controller = new CRUDController(mediatorMock.Object);

            // Act
            var result = await controller.PutCustomer(100, updateCommand);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ApiResponse<Customer>>>(result);
            Assert.NotNull(actionResult.Result);  
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
            var response = Assert.IsType<ApiResponse<Customer>>(notFoundResult.Value);
            Assert.Equal("Customer not found or invalid ID", response.Message);
        }

        [Fact]
        public async Task DeleteCustomer_ReturnsOk_WhenCustomerIsDeleted()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCustomerCommand>(), default))
                        .ReturnsAsync(true);

            var controller = new CRUDController(mediatorMock.Object);

            // Act
            var result = await controller.DeleteCustomer(5);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ApiResponse<string>>>(result);
            Assert.NotNull(actionResult.Result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var response = Assert.IsType<ApiResponse<string>>(okResult.Value);
            Assert.Equal("Customer deleted successfully", response.Message);
        }

        [Fact]
        public async Task DeleteCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(m => m.Send(It.IsAny<DeleteCustomerCommand>(), default))
                        .ReturnsAsync(false);

            var controller = new CRUDController(mediatorMock.Object);

            // Act
            var result = await controller.DeleteCustomer(100);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ApiResponse<string>>>(result);
            Assert.NotNull(actionResult.Result);  
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        }

    }
}
