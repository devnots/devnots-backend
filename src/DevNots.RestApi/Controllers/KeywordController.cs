using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevNots.Application.Contracts.Keyword;
using DevNots.Application.Contracts.Note;
using DevNots.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevNots.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeywordController : ControllerBase
    {
        private readonly KeywordService keywordService;
        public KeywordController(KeywordService keywordService)
        {
            this.keywordService = keywordService;
        }
        /// <summary>
        /// Create a keyword 
        /// </summary>
        /// <param name="keyword">Keyword object</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] KeywordDto keyword)
        {
            var response = await keywordService.CreateAsync(keyword);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error);

            return StatusCode(201, new { id = response.Result });
        }
        /// <summary>
        /// Delete the keyword
        /// </summary>
        /// <param name="keyword">DeleteKeywordDto object</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteKeyword([FromBody] DeleteKeywordDto keyword)
        {
            var response = await keywordService.DeleteKeywordAsync(keyword);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error);

            return Ok(new { message = "Keyword deleted." });
        }
        /// <summary>
        /// Get list of Keywords
        /// </summary>
        /// <param name="limit">Set the note limit  ( Default limit = 20 )</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetKeywords([FromQuery] int limit = 20)
        {
            var response = await keywordService.GetKeywordsAsync(new KeywordListDto()
            {
                Limit = limit,
            });

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error.Message);

            return Ok(response.Result);
        }
        /// <summary>
        /// Update the keyword
        /// </summary>
        /// <param name="keyword">Keyword Object</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateKeyword([FromBody] KeywordDto keyword)
        {
            var response = await keywordService.UpdateNoteAsync(keyword);

            if (response.Error != null)
                return StatusCode(response.Error.StatusCode, response.Error.Message);

            var isUpdated = response.Result;

            if (!isUpdated)
                return NotFound(new { message = "404 not found." });

            return Ok(new { message = "Keyword updated." });
        }
    }
}
