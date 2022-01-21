using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiEntityFrameworkMemory.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Todo.Domain;
using Todo.Repositories;
using Todo.Repositories.Filters;

namespace ApiEntityFrameworkMemory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItemRepository _itemRepository;

        public ItemController(ILogger<ItemController> logger, IItemRepository itemRepository)
        {
            _logger = logger;
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public IEnumerable<Item> Get(bool archivedOnly = false, bool unarchivedOnly = false, Guid? noteId = null)
        {
            return _itemRepository.GetFiltered(new ItemFilter
            {
                ArchivedOnly = archivedOnly,
                NoteId = noteId,
                UnArchivedOnly = unarchivedOnly
            });
        }

        [HttpGet]
        [Route("{id}")]
        public Item Get(Guid id)
        {
            return _itemRepository.Get(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody]string description)
        {
            if (description.IsNullOrWhitespace())
            {
                return Problem("Description can not be null or empty");
            }

            var newItem = _itemRepository.Upsert(new Item
            {
                Description = description,
                CreateDate = DateTimeOffset.Now
            });

            if (newItem == null)
            {
                return Problem("Could not create item in repository");
            }

            return Created("/ItemController/" + newItem.Id, newItem);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(Guid id, [FromBody] string description)
        {
            if (description.IsNullOrWhitespace())
            {
                return Problem("Description can not be null or empty");
            }

            var existingItem = _itemRepository.Get(id);
            if (existingItem == null)
            {
                return NotFound(id);
            }

            existingItem.Description = description;
            var updatedItem = _itemRepository.Upsert(existingItem);

            return Ok(updatedItem);
        }

        [HttpPut]
        [Route("{id}/ToggleComplete")]
        public IActionResult ToggleComplete(Guid id)
        {
            var existingItem = _itemRepository.Get(id);
            if (existingItem == null)
            {
                return NotFound(id);
            }

            existingItem.CompleteDate = existingItem.CompleteDate != null ? DateTimeOffset.Now : null;
            var updatedItem = _itemRepository.Upsert(existingItem);

            return Ok(updatedItem);
        }

        [HttpPut]
        [Route("{id}/ToggleArchived")]
        public IActionResult ToggleArchived(Guid id)
        {
            var existingItem = _itemRepository.Get(id);
            if (existingItem == null)
            {
                return NotFound(id);
            }

            existingItem.ArchiveDate = existingItem.ArchiveDate != null ? DateTimeOffset.Now : null;
            var updatedItem = _itemRepository.Upsert(existingItem);

            return Ok(updatedItem);
        }

        [HttpPost]
        [Route("{id}/Note")]
        public IActionResult Note(Guid id, [FromBody] string content)
        {
            if (content.IsNullOrWhitespace())
            {
                return Problem("Content can not be null or empty");
            }

            var existingItem = _itemRepository.Get(id);
            if (existingItem == null)
            {
                return NotFound(id);
            }

            var now = DateTimeOffset.Now;

            existingItem.Notes.Add(new Note
            {
                CreateDate = now,
                Content = content
            });

            var updatedItem = _itemRepository.Upsert(existingItem);
            var lastNote = updatedItem.Notes.SingleOrDefault(n => n.CreateDate == now);
            if (lastNote == null)
            {
                return Problem("Could not persist note in repository");
            }

            return Created("/Note/" + lastNote.Id, lastNote);
        }

        [HttpGet]
        [Route("/Note/{id}")]
        public Note GetNote(Guid id)
        {
            var item =  _itemRepository.GetFiltered(new ItemFilter
            {
                NoteId = id
            }).SingleOrDefault();

            return item?.Notes.FirstOrDefault(n => n.Id == id);
        }

        [HttpPut]
        [Route("/Note/{id}")]
        public IActionResult PutNote(Guid id, [FromBody] string content)
        {
            if (content.IsNullOrWhitespace())
            {
                return Problem("Content can not be null or empty");
            }

            var item = _itemRepository.GetFiltered(new ItemFilter
            {
                NoteId = id
            }).SingleOrDefault();

            var note = item?.Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound(id);
            }

            note.Content = content;
            var updatedItem = _itemRepository.Upsert(item);
            return Ok(updatedItem.Notes.FirstOrDefault(n => n.Id == id));
        }

        [HttpDelete]
        [Route("/Note/{id}")]
        public IActionResult DeleteNote(Guid id)
        {
            var item = _itemRepository.GetFiltered(new ItemFilter
            {
                NoteId = id
            }).SingleOrDefault();

            var note = item?.Notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                return NotFound(id);
            }

            item.Notes.Remove(note);
            _itemRepository.Upsert(item);

            return Ok();
        }
    }
}
