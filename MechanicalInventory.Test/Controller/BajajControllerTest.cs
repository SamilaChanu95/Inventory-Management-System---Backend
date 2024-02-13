using Castle.Core.Logging;
using FakeItEasy;
using MechanicalInventory.Services;
using MechanicalInventory.Controllers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MechanicalInventory.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MechanicalInventory.Test.Controller
{
    public class BajajControllerTest
    {
        private readonly IBajajService _bajajService;
        private readonly ILogger<BajajProductController> _logger;
        public BajajControllerTest() 
        {
            _bajajService = A.Fake<IBajajService>();
            _logger = A.Fake<ILogger<BajajProductController>>();
        }

        [Fact]
        public async Task BajajProductController_GetProductsList_ReturnOk() 
        {
            //Arrange
            var products = A.Fake<List<BajajProduct>>();
            A.CallTo(() => _bajajService.GetBajajProductsList()).Returns(products);
            var controller = new BajajProductController(_bajajService, _logger);

            //Act
            var result = await controller.GetProductsList();

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task BajajProductController_GetProductsList_ReturnBadRequest()
        {
            //Arrange
            var controller = new BajajProductController(_bajajService, _logger);
            controller.ModelState.AddModelError("ModelStateInvalid", "Model state is not valid.");

            //Act
            var result = await controller.GetProductsList();

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task BajajProductController_GetProductsList_ReturnBadRequestwithErrorMessage()
        {
            //Arrange
            var controller = new BajajProductController(_bajajService, _logger);
            A.CallTo(() => _bajajService.GetBajajProductsList()).ThrowsAsync(new Exception("Error in loading Bajaj products list."));

            //Act
            var result = await controller.GetProductsList();

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be("Error in loading Bajaj products list.");
        }

        [Fact]
        public async Task BajajProductController_AddProduct_ReturnOk()
        {
            //Arrange
            var controller = new BajajProductController(_bajajService, _logger);
            var product = A.Fake<BajajProduct>();
            A.CallTo(() => _bajajService.AddBajajProduct(product)).Returns(true);

            //Act
            var result = await controller.AddProduct(product);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task BajajProductController_AddProduct_ReturnUnauthorized()
        {
            //Arrange
            var controller = new BajajProductController(_bajajService, _logger);
            var product = A.Fake<BajajProduct>();
            A.CallTo(() => _bajajService.AddBajajProduct(product)).Returns(false);

            //Act
            var result = await controller.AddProduct(product);

            //Assert
            result.Should().BeOfType<UnauthorizedObjectResult>().Which.Value.Should().Be("Cannot added new Bajaj product.");
        }

        [Fact]
        public async Task BajajProductController_AddProduct_ReturnBadRequest()
        {
            //Arrange
            var product = A.Fake<BajajProduct>();
            var controller = new BajajProductController(_bajajService, _logger);
            controller.ModelState.AddModelError("ModelStatusInvalid", "Model state is not valid.");

            //Act
            var result = await controller.AddProduct(product);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task BajajProductController_AddProduct_ReturnBadRequestWithErrorMessage()
        {
            //Arrange
            var product = A.Fake<BajajProduct>();
            var controller = new BajajProductController(_bajajService, _logger);
            A.CallTo(() => _bajajService.AddBajajProduct(product)).ThrowsAsync(new Exception("Error in adding new Bajaj product."));

            //Act
            var result = await controller.AddProduct(product);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be("Error in adding new Bajaj product.");
        }

        [Fact]
        public async Task BajajProductController_GetPrduct_ReturnOk() 
        {
            //Arrange
            var controller = new BajajProductController(_bajajService, _logger);
            int id = new Int32();
            var product = A.Fake<BajajProduct>();
            A.CallTo(() => _bajajService.IsExistProduct(id)).Returns(true);
            A.CallTo(() => _bajajService.GetBajajProduct(id)).Returns(product);

            //Act
            var result = await controller.GetProduct(id);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task BajajProductController_GetPrduct_ReturnNotFound()
        {
            //Arrange
            var controller = new BajajProductController(_bajajService, _logger);
            int id = new Int32();
            var product = A.Fake<BajajProduct>();
            A.CallTo(() => _bajajService.IsExistProduct(id)).Returns(false);

            //Act
            var result = await controller.GetProduct(id);

            //Assert
            result.Should().BeOfType<NotFoundObjectResult>().Which.Value.Should().Be("Requested Bajaj product doesn't exist.");
        }

        [Fact]
        public async Task BajajProductController_GetPrduct_ReturnBadRequest()
        {
            //Arrange
            var id = new Int32();
            var controller = new BajajProductController(_bajajService, _logger);
            controller.ModelState.AddModelError("ModelStatusInvalid", "Model state is not valid.");

            //Act
            var result = await controller.GetProduct(id);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task BajajProductController_GetPrduct_ReturnBadRequestWithErrorMessage()
        {
            //Arrange
            var id = new Int32();
            var controller = new BajajProductController(_bajajService, _logger);
            A.CallTo(() => _bajajService.IsExistProduct(id)).ThrowsAsync(new Exception("Error in getting the Bajaj product."));
            A.CallTo(() => _bajajService.GetBajajProduct(id)).ThrowsAsync(new Exception("Error in getting the Bajaj product."));

            //Act
            var result = await controller.GetProduct(id);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be("Error in getting the Bajaj product.");
        }

        [Fact]
        public async Task BajajProductController_DeletePrduct_ReturnOk()
        {
            //Arrange
            var id = new Int32();
            var controller = new BajajProductController(_bajajService, _logger);
            A.CallTo(() => _bajajService.IsExistProduct(id)).Returns(true);
            A.CallTo(() => _bajajService.DeleteBajajProduct(id)).Returns(true);

            //Act
            var result = await controller.DeleteProduct(id);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task BajajProductController_DeletePrduct_ReturnUnauthorized()
        {
            //Arrange
            var id = new Int32();
            var controller = new BajajProductController(_bajajService, _logger);
            A.CallTo(() => _bajajService.IsExistProduct(id)).Returns(true);
            A.CallTo(() => _bajajService.DeleteBajajProduct(id)).Returns(false);

            //Act
            var result = await controller.DeleteProduct(id);

            //Assert
            result.Should().BeOfType<UnauthorizedObjectResult>().Which.Value.Should().Be($"Cannot delete existing Bajaj product with id : {id}.");
        }

        [Fact]
        public async Task BajajProductController_DeletePrduct_ReturnNotFound()
        {
            //Arrange
            var id = new Int32();
            var controller = new BajajProductController(_bajajService, _logger);
            A.CallTo(() => _bajajService.IsExistProduct(id)).Returns(false);

            //Act
            var result = await controller.DeleteProduct(id);

            //Assert
            result.Should().BeOfType<NotFoundObjectResult>().Which.Value.Should().Be("Requested Bajaj product doesn't exist.");

        }

        [Fact]
        public async Task BajajProductController_DeletePrduct_ReturnBadRequest()
        {
            //Arrange
            var id = new Int32();
            var controller = new BajajProductController(_bajajService, _logger);
            controller.ModelState.AddModelError("ModelStatusInvalid", "Model state is not valid.");

            //Act
            var result = await controller.DeleteProduct(id);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task BajajProductController_DeletePrduct_ReturnBadRequestWithErrorMessage()
        {
            //Arrange
            var id = new Int32();
            var controller = new BajajProductController(_bajajService, _logger);
            A.CallTo(() => _bajajService.IsExistProduct(id)).ThrowsAsync(new Exception("Error in deleting existing Bajaj product."));
            A.CallTo(() => _bajajService.DeleteBajajProduct(id)).ThrowsAsync(new Exception("Error in deleting existing Bajaj product."));

            //Act
            var result = await controller.DeleteProduct(id);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be("Error in deleting existing Bajaj product.");
        }

        [Fact]
        public async Task BajajProductController_UpdatePrduct_ReturnOk()
        {
            //Arrange
            var id = new Int32();
            var product = new BajajProduct();
            var controller = new BajajProductController(_bajajService, _logger);
            A.CallTo(() => _bajajService.IsExistProduct(id)).Returns(true);
            A.CallTo(() => _bajajService.UpdateBajajProduct(product)).Returns(true);

            //Act
            var result = await controller.UpdateProduct(product);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task BajajProductController_UpdatePrduct_ReturnUnauthorized()
        {
            //Arrange
            var id = new Int32();
            var product = new BajajProduct();
            var controller = new BajajProductController(_bajajService, _logger);
            A.CallTo(() => _bajajService.IsExistProduct(id)).Returns(true);
            A.CallTo(() => _bajajService.UpdateBajajProduct(product)).Returns(false);

            //Act
            var result = await controller.UpdateProduct(product);

            //Assert
            result.Should().BeOfType<UnauthorizedObjectResult>().Which.Value.Should().Be($"Cannot updated existing Bajaj product with id : {id}.");
        }

        [Fact]
        public async Task BajajProductController_UpdatePrduct_ReturnNotFound()
        {
            //Arrange
            var id = new Int32();
            var product = new BajajProduct();
            var controller = new BajajProductController(_bajajService, _logger);
            A.CallTo(() => _bajajService.IsExistProduct(id)).Returns(false);
            
            //Act
            var result = await controller.UpdateProduct(product);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task BajajProductController_UpdatePrduct_ReturnBadRequest()
        {
            //Arrange
            var id = new Int32();
            var product = new BajajProduct();
            var controller = new BajajProductController(_bajajService, _logger);
            controller.ModelState.AddModelError("ModelStateInvalid", "Model state is not valid.");
       
            //Act
            var result = await controller.UpdateProduct(product);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task BajajProductController_UpdatePrduct_ReturnBadRequestWithErrorMessage()
        {
            //Arrange
            var id = new Int32();
            var product = new BajajProduct();
            var controller = new BajajProductController(_bajajService, _logger);
            A.CallTo(() => _bajajService.IsExistProduct(id)).ThrowsAsync(new Exception("Error in updating existing Bajaj product."));
            A.CallTo(() => _bajajService.UpdateBajajProduct(product)).ThrowsAsync(new Exception("Error in updating existing Bajaj product."));

            //Act
            var result = await controller.UpdateProduct(product);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>().Which.Value.Should().Be("Error in updating existing Bajaj product.");
        }
    }
}
