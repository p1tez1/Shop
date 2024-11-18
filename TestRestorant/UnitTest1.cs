using Xunit;
using Moq;
using FluentValidation;
using Restoran.Controllers;
using Restoran.Service.Interface;
using Restoran.Entity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BusinesLayer.Features.MenuQuery;

namespace Restoran.Tests
{
    public class MenuControllerTests
    {
        private readonly Mock<IMenuService> _menuServiceMock;
        private readonly Mock<IValidator<Menu>> _menuValidatorMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly MenuController _menuController;

        public MenuControllerTests()
        {
            _menuServiceMock = new Mock<IMenuService>();
            _menuValidatorMock = new Mock<IValidator<Menu>>();
            _mediatorMock = new Mock<IMediator>();
            _menuController = new MenuController(_menuServiceMock.Object, _menuValidatorMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task GetMenuById_ShouldReturnOkResult_WhenMenuExists()
        {
            // Arrange
            var menuId = Guid.NewGuid();
            var menu = new Menu { Id = menuId };
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetMenuByIdQuery>(), default)).ReturnsAsync(menu);

            // Act
            var result = await _menuController.GetMenuById(menuId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedMenu = Assert.IsType<Menu>(okResult.Value);
            Assert.Equal(menuId, returnedMenu.Id);
        }
    }
}
