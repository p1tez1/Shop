using System.Text.Json;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Restoran.Entity;
using Restoran.Service.Interface;

namespace Restoran.Service
{
    public class MenuService : IMenuService
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<Menu> _menuValidator;

        public MenuService(ApplicationDbContext context, IValidator<Menu> menuValidator)
        {
            _context = context;
            _menuValidator = menuValidator;

        }

        public async Task<Menu> GetMenuById(Guid menuId)
        {
            var menu = await _context.Menus.FirstOrDefaultAsync(l => l.Id == menuId) ?? throw new Exception();
            return menu;
        }

        public async Task<IEnumerable<Menu>> GetAllMenu()
        {
            return await _context.Menus.ToListAsync();
        }

        public async Task<Menu> PostMenu(string name)
        {
            var menu = new Menu(name);
            ValidateMenu(menu);

            _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();

            return menu;
        }

        public async Task<Menu> RenameMenu(Guid menuId, string name)
        {
            var menu = await GetMenuById(menuId);
            menu.Name = name;
            await _context.SaveChangesAsync();
            return menu;
        }

        public async Task<bool> DeleteMenu(Guid menuId)
        {
            var menu = await GetMenuById(menuId);
            _context.Remove(menu);
            await _context.SaveChangesAsync();

            return true;
        }

        private void ValidateMenu(Menu menu)
        {
            var result = _menuValidator.Validate(menu);
            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Menu validation failed: {errors}");
            }
        }
    }
}
