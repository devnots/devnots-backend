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
        public async Task<IActionResult> CreateNoteAsync([FromBody] NoteDto note)
        {
            var response = await _noteService.CreateNoteAsync(note);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error);

            return StatusCode(201, new { id = response.Result });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNoteAsync([FromBody] DeleteNoteDto note)
        {
            var response = await _noteService.DeleteNoteAsync(note);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error);

            return Ok(new { message = "User deleted." });
        }

        [HttpGet]
        public async Task<IActionResult> GetNotesAsync([FromQuery] int limit = 20)
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
        public async Task<IActionResult> UpdateNoteAsync([FromBody]NoteDto note)
        {
            var response = await _noteService.UpdateNoteAsync(note);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error.Message);

            return Ok(response.Result);
        }
    }
}
