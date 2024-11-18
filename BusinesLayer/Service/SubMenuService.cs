using Restoran.Entity;
using Shop.Service.Interface;
using System.ComponentModel.DataAnnotations;
using Restoran.Service;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;


namespace Shop.Servises
{
    public class SubMenuService:ISubMenuService
    {
        private readonly ApplicationDbContext _context;
        private readonly MenuService _menuService;
        public SubMenuService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public SubMenu PostSubMenu(Guid menuid, string name)
        {
            var submenu = new SubMenu(name, menuid);

            _context.SubMenus.Add(submenu);
            _context.SaveChanges();

            return submenu;
        }
        public SubMenu GetSubMenuById(Guid SubMenuId) 
        {
            var submenu = _context.SubMenus.FirstOrDefault(l => l.Id == SubMenuId)?? throw new Exception();

            return submenu;
        }
        public SubMenu RenameSubMenu(Guid id, string newname)
        {
            var submenu = GetSubMenuById(id);

            submenu.Name = newname;
            _context.SaveChanges();

            return submenu;
        }
        public bool DeleteSubMenu(Guid id)
        {
            var submenu = GetSubMenuById(id);

            _context.SubMenus.Remove(submenu);
            _context.SaveChanges();

            return true;
        }
        public IEnumerable<SubMenu> GetAllSubMenu(Guid menuid)
        {
            var subMenus = _context.SubMenus.Where(sm => sm.MenuId == menuid).ToList();

            return subMenus;
        }         
    }
}
