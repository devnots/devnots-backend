using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevNots.Application.Contracts;
using DevNots.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevNots.RestApi.Controllers
{
    [Route("api/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteService _noteService;

        public NoteController(NoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] NoteDto note)
        {
            var response = await _noteService.CreateNoteAsync(note);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error);

            return StatusCode(201, new { id = response.Result });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNote([FromBody] DeleteNoteDto note)
        {
            var response = await _noteService.DeleteNoteAsync(note);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error);

            return Ok(new { message = "Note deleted." });
        }

        [HttpGet]
        public async Task<IActionResult> GetNotes([FromQuery] int limit = 20)
        {
            var response = await _noteService.GetNotesAsync(new NoteListDto()
            {
                Limit = limit,
            });

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error.Message);

            return Ok(response.Result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNote([FromBody]NoteDto note)
        {
            var response = await _noteService.UpdateNoteAsync(note);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error.Message);

            var isUpdated = response.Result;

            if (!isUpdated)
                return NotFound(new { message = "404 not found."});

            return Ok(new { message = "user updated." });
        }
    }
}
