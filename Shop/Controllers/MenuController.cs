using Microsoft.AspNetCore.Mvc;
using Restoran.Service.Interface;
using FluentValidation;
using FluentValidation.Results;
using Restoran.Entity;
using System.Runtime.CompilerServices;
using MediatR;
using BusinesLayer.Features.MenuQuery;
using BusinesLayer.Features;

namespace Restoran.Controllers
{
    [Route("api/menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IValidator<Menu> _menuValidator;
        private readonly IMediator _mediator;
        public MenuController(IMenuService menuService, IValidator<Menu> menuValidator, IMediator mediator)
        {
            _menuService = menuService;
            _menuValidator = menuValidator;
            _mediator = mediator;
        }

        [HttpGet("FindMenuByID")]
        public async Task<ActionResult> GetMenuById(Guid menuId)
        {
            var query = new GetMenuByIdQuery(menuId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllMenus()
        {
            var query = new GetAllMenuQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("AddToMenu")]
        public async Task<ActionResult> PostMenu(string name)
        {
            var menu = new Menu(name);
            ValidationResult result = _menuValidator.Validate(menu);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            var query = new PostMenuQuery(name);
            var newMenu = await _mediator.Send(query);
            return Ok(newMenu);
        }
        [HttpPut("RenameMenu")]
        public async Task<ActionResult> RenameMenu(Guid menuid, string menuname)
        {
            var query = new RenameMenuQuery(menuid, menuname);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpDelete("DeleteMenu")]
        public async Task<ActionResult> DeleteMenu(Guid menuid)
        {
            var query = new DeleteMenuQuery(menuid);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}

