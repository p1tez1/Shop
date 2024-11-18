using MediatR;
using Microsoft.Identity.Client;
using Restoran.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Features.MenuQuery
{
    public class GetMenuByIdQuery : IRequest<Menu>
    {
        public Guid MenuId { get; }
        public GetMenuByIdQuery(Guid menuId)
        {
            this.MenuId = menuId;
        }
    }
    public class GetAllMenuQuery : IRequest<IEnumerable<Menu>> { }
    
    public class PostMenuQuery : IRequest<Menu> 
    { 
        public string Name { get; }

        public PostMenuQuery(string name)
        {
            this.Name = name;
        }
    }
    public class RenameMenuQuery : IRequest<Menu> 
    {
        public Guid MenuId { get; }
        public string Name { get; }

        public RenameMenuQuery(Guid menuId, string name)
        {
            this.MenuId = menuId;
            this.Name = name;
        }
    }
    public class DeleteMenuQuery : IRequest<bool>
    {
        public Guid Menuid { get; }

        public DeleteMenuQuery(Guid menuid)
        {
            this.Menuid = menuid;
        }   
    }
}
