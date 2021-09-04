using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcEntityFrameworkMemory.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Todo.Domain;
using Todo.Repositories;

namespace MvcEntityFrameworkMemory.Controllers
{
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItemRepository _itemRepository;

        public ItemController(ILogger<ItemController> logger, IItemRepository itemRepository)
        {
            _logger = logger;
            _itemRepository = itemRepository;
        }

        public IActionResult Index()
        {
            // test: Every time we refresh the page, we add a new item
            var now = DateTimeOffset.Now;
            _itemRepository.Upsert(new Item
            {
                CreateDate = now,
                Description = $"Added at {now:dd/MM/yyyy hh:mm:ss}"
            });

            var items = _itemRepository.GetFiltered(null);

            return View(new ItemIndexViewModel{Items = items.ToList()});
        }
    }
}
