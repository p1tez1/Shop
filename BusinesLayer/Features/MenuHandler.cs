using BusinesLayer.Features.MenuQuery;
using MediatR;
using Restoran.Entity;
using Restoran.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Features
{
    public class GetMenuBuIdHandler : IRequestHandler<GetMenuByIdQuery,Menu>
    {
        private readonly IMenuService _menuService;
        public GetMenuBuIdHandler(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public async Task<Menu> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            var menu = await _menuService.GetMenuById(request.MenuId);
            return menu;
        }
    }
    public class GetAllMenuHendler : IRequestHandler <GetAllMenuQuery ,IEnumerable<Menu>>
    {
        private readonly IMenuService _menuService;
        public GetAllMenuHendler(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public async Task<IEnumerable<Menu>> Handle(GetAllMenuQuery request,CancellationToken cancellationToken)
        {
            var menu = await _menuService.GetAllMenu();
            return menu;
        }
    }
    public class PostMenuHendler : IRequestHandler<PostMenuQuery, Menu>
    {
        private readonly IMenuService _menuService;
        public PostMenuHendler (IMenuService menuService)
        {
            _menuService= menuService;
        }
        public async Task<Menu> Handle(PostMenuQuery request,CancellationToken cancellationToken)
        {
            var menu = await _menuService.PostMenu(request.Name);
            return menu;
        }
    }
    public class RenameMenuHendler : IRequestHandler<RenameMenuQuery, Menu>
    {
        private readonly IMenuService _menuService;
        public RenameMenuHendler(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public async Task<Menu> Handle(RenameMenuQuery request,CancellationToken cancellationToken)
        {
            var menu = await _menuService.RenameMenu(request.MenuId, request.Name);
            return menu;
        }
    }
    public class DeleteMenuHendler : IRequestHandler<DeleteMenuQuery,bool>
    {
        private readonly IMenuService _menuService;
        public DeleteMenuHendler(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public async Task<bool> Handle(DeleteMenuQuery request,CancellationToken cancellationToken)
        {
            var menu = await _menuService.DeleteMenu(request.Menuid);
            return menu;
        }
    }

   
}
