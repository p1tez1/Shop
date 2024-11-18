using Restoran.Entity;

namespace Restoran.Service.Interface
{
    public interface IMenuService
    {
        public Task<Menu> GetMenuById(Guid MenuId);
        public Task<IEnumerable<Menu>> GetAllMenu();
        public Task<Menu> PostMenu(string name);
        public Task<Menu> RenameMenu(Guid MenuId, string name);
        public Task<bool> DeleteMenu(Guid MenuId);
    }
}
