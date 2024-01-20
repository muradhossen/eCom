using Application.ServiceInterface;
using eCom.API.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eCom.API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public BuggyController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecrate()
        {
            return "This is secrate code";
        }

        [HttpGet("not-found")]
        public async Task<ActionResult> GetNotFound()
        {
            long id = -1;
            var thing =await _categoryService.GetByIdAsync(id);

            if (thing == null) return NotFound();

            return Ok(thing);
        }

        [HttpGet("server-error")]
        public async Task<ActionResult> GetServerError()
        {
            var thing =await _categoryService.GetByIdAsync(-1);
            var thingToReturn = thing.ToString();
            return Ok(thingToReturn);
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("This was not a good request");
        }
    }
}
