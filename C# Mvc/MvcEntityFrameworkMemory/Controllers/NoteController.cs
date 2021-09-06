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
    public class NoteController : Controller
    {
        private readonly ILogger<NoteController> _logger;
        private readonly IItemRepository _itemRepository;

        public NoteController(ILogger<NoteController> logger, IItemRepository itemRepository)
        {
            _logger = logger;
            _itemRepository = itemRepository;
        }

        public IActionResult Edit(Guid id)
        {
            var item = _itemRepository.GetFiltered(new ItemFilter { NoteId = id }).SingleOrDefault();
            if (item == null)
            {
                ViewData.ModelState.AddModelError("Edit", "Item not found");
                return RedirectToAction("Index", "Item");
            }

            var note = item.Notes.First(n => n.Id == id);

            return View(new NoteSubmitModel()
            {
                Id = id,
                Content = note.Content
            });
        }

        public IActionResult New(Guid itemId)
        {
            return View("Edit", new NoteSubmitModel
            {
                ItemId = itemId
            });
        }

        [HttpPost]
        public IActionResult Submit(NoteSubmitModel note)
        {
            var item = note.Id == null ? _itemRepository.Get(note.ItemId) : _itemRepository.GetFiltered(new ItemFilter { NoteId = note.Id }).SingleOrDefault();
            if (item == null)
            {
                ViewData.ModelState.AddModelError("Submit note", "Item not found");
                return RedirectToAction("Index", "Item");
            }

            if (note.Id == null)
            {
                item.Notes.Add(new Note { Content = note.Content, CreateDate = DateTimeOffset.Now });
                _itemRepository.Upsert(item);
                return RedirectToAction("Detail", "Item", new { id = note.ItemId });
            }

            var existingNote = item.Notes.FirstOrDefault(n => n.Id == note.Id);
            if (existingNote == null)
            {
                ViewData.ModelState.AddModelError("Submit note", "Note not found");
                return RedirectToAction("Detail", "Item", new { id = note.ItemId });
            }

            existingNote.Content = note.Content;
            _itemRepository.Upsert(item);
            return RedirectToAction("Detail", "Item", new { id = item.Id });
        }

        public IActionResult Remove(Guid id)
        {
            var item = _itemRepository.GetFiltered(new ItemFilter { NoteId = id }).SingleOrDefault();
            if (item == null)
            {
                ViewData.ModelState.AddModelError("Remove Note", "Item not found");
                return RedirectToAction("Index", "Item");
            }

            var note = item.Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                ViewData.ModelState.AddModelError("Remove note", "Note not found");
                return RedirectToAction("Detail", "Item", new { id = item.Id });
            }

            return View(new NoteSubmitModel
            {
                Id = note.Id,
                Content = note.Content
            });
        }

        [HttpPost]
        public IActionResult SubmitRemove(Guid id)
        {
            var item = _itemRepository.GetFiltered(new ItemFilter { NoteId = id }).SingleOrDefault();
            if (item == null)
            {
                ViewData.ModelState.AddModelError("Remove Note", "Item not found");
                return RedirectToAction("Index", "Item");
            }

            var note = item.Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                ViewData.ModelState.AddModelError("Remove note", "Note not found");
                return RedirectToAction("Detail", "Item", new { id = item.Id });
            }

            item.Notes.Remove(note);
            _itemRepository.Upsert(item);
            return RedirectToAction("Detail", "Item", new { id = item.Id });
        }
    }
}
