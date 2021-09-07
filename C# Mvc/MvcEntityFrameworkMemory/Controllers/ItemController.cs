using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcEntityFrameworkMemory.Models;
using System;
using System.Linq;
using Todo.Domain;
using Todo.Repositories;
using Todo.Repositories.Filters;

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
            var items = _itemRepository.GetFiltered(new ItemFilter { UnArchivedOnly = true });
            return View(new ItemIndexViewModel { Items = items.ToList() });
        }

        [HttpPost]
        public IActionResult ToggleComplete(Guid id)
        {
            var item = _itemRepository.Get(id);
            if (item == null)
            {
                TempData["Invalid Action"] = "Can not toggle, Item not found";
                return RedirectToAction("Index");
            }

            item.CompleteDate = item.CompleteDate == null ? DateTimeOffset.Now : null;
            _itemRepository.Upsert(item);

            return RedirectToAction("Index");
        }

        public IActionResult Detail(Guid id)
        {
            var item = _itemRepository.Get(id);
            if (item == null)
            {
                TempData["Invalid Action"] = "Cannot show detail, Item not found";
                return RedirectToAction("Index");
            }

            return View(item);
        }

        public IActionResult Edit(Guid id)
        {
            var item = _itemRepository.Get(id);
            if (item == null)
            {
                TempData["Invalid Action"] = "Cannot Edit, Item not found";
                return RedirectToAction("Index");
            }

            return View(new ItemSubmitModel
            {
                Id = item.Id,
                Description = item.Description
            });
        }

        public IActionResult New()
        {
            return View("Edit", new ItemSubmitModel());
        }

        [HttpPost]
        public IActionResult Submit(ItemSubmitModel item)
        {
            if (item.Id == null)
            {
                _itemRepository.Upsert(new Item
                {
                    CreateDate = DateTimeOffset.Now,
                    Description = item.Description
                });
                return RedirectToAction("Index");
            }

            var existingItem = _itemRepository.Get(item.Id.Value);
            existingItem.Description = item.Description;
            _itemRepository.Upsert(existingItem);
            return RedirectToAction("Index");
        }

        public IActionResult Archive(Guid id)
        {
            var item = _itemRepository.Get(id);
            if (item == null)
            {
                TempData["Invalid Action"] = "Cannot archive, Item not found";
                return RedirectToAction("Index");
            }

            return View(new ItemSubmitModel
            {
                Id = item.Id,
                Description = item.Description
            });
        }

        [HttpPost]
        public IActionResult SubmitArchive(Guid id)
        {
            var item = _itemRepository.Get(id);
            if (item == null)
            {
                TempData["Invalid Action"] = "Cannot archive, Item not found";
                return RedirectToAction("Index");
            }
            item.ArchiveDate = DateTimeOffset.Now;
            _itemRepository.Upsert(item);
            return RedirectToAction("Index");
        }
    }
}
