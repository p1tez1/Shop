using Microsoft.AspNetCore.Mvc;
using Shop.Service.Interface;
using FluentValidation;
using FluentValidation.Results;
using Restoran.Entity;

namespace Restoran.Controllers
{
    [Route("api/submenu")]
    [ApiController]
    public class SubMenuController : ControllerBase
    {
        private readonly ISubMenuService _submenuService;
        private readonly IValidator<SubMenu> _submenuValidator;

        public SubMenuController(ISubMenuService submenuService, IValidator<SubMenu> submenuValidator)
        {
            _submenuService = submenuService;
            _submenuValidator = submenuValidator;
        }

        [HttpGet("GetSubMenuById")]
        public ActionResult GetSubMenuById(Guid submenuId)
        {
            var submenu = _submenuService.GetSubMenuById(submenuId);
            return Ok(submenu);
        }

        [HttpGet("GetAllSubMenu")]
        public ActionResult GetAllSubMenu(Guid menuId)
        {
            var submenus = _submenuService.GetAllSubMenu(menuId);
            return Ok(submenus);
        }

        [HttpPost("AddNewSubMenu")]
        public ActionResult PostSubMenu(Guid menuId, string name)
        {
            var submenu = new SubMenu(name, menuId);
            ValidationResult result = _submenuValidator.Validate(submenu);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var newSubmenu = _submenuService.PostSubMenu(menuId, name);
            return Ok(newSubmenu);
        }

        [HttpPut("RenameSubMenu")]
        public ActionResult RenameSubMenu(Guid submenuId, string newName)
        {
            var submenu = _submenuService.GetSubMenuById(submenuId);
            submenu.Name = newName;
            ValidationResult result = _submenuValidator.Validate(submenu);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var updatedSubmenu = _submenuService.RenameSubMenu(submenuId, newName);
            return Ok(updatedSubmenu);
        }

        [HttpDelete("DeleteSubMenu")]
        public ActionResult DeleteSubMenu(Guid submenuId)
        {
            var result = _submenuService.DeleteSubMenu(submenuId);
            return Ok(result);
        }
    }
}
