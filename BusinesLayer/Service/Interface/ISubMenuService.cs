using Restoran.Entity;

namespace Shop.Service.Interface
{
    public interface ISubMenuService
    {
        public SubMenu PostSubMenu(Guid menuid, string name);
        public SubMenu GetSubMenuById(Guid SubMenuId);
        public SubMenu RenameSubMenu(Guid id, string newname);
        public bool DeleteSubMenu(Guid id);
        public IEnumerable<SubMenu> GetAllSubMenu(Guid menuid);

    }
}