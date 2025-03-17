using Microsoft.AspNetCore.Mvc;
using MyFace.Helpers;
using MyFace.Models.Request;
using MyFace.Models.Response;
using MyFace.Repositories;

namespace MyFace.Controllers
{
    [ApiController]
    [Route("/interactions")]
    public class InteractionsController : ControllerBase
    {
        private readonly IInteractionsRepo _interactions;
        private readonly IUsersRepo _users;

        public InteractionsController(IInteractionsRepo interactions,IUsersRepo users)
        {
            _interactions = interactions;
            _users = users;
        }
    
        [HttpGet("")]
        public ActionResult<ListResponse<InteractionResponse>> Search([FromQuery] SearchRequest search)
        {
            if (!UserPasswordHelper.ReadAuthorizationHeaderAndValidateLogin(Request.Headers["Authorization"], _users))
                return BadRequest("Invalid login.");
            var interactions = _interactions.Search(search);
            var interactionCount = _interactions.Count(search);
            return InteractionListResponse.Create(search, interactions, interactionCount);
        }

        [HttpGet("{id}")]
        public ActionResult<InteractionResponse> GetById([FromRoute] int id)
        {
            if (!UserPasswordHelper.ReadAuthorizationHeaderAndValidateLogin(Request.Headers["Authorization"], _users))
                return BadRequest("Invalid login.");
            var interaction = _interactions.GetById(id);
            return new InteractionResponse(interaction);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateInteractionRequest newUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!UserPasswordHelper.ReadAuthorizationHeaderAndValidateLogin(Request.Headers["Authorization"], _users))
                return BadRequest("Invalid login.");
            var interaction = _interactions.Create(newUser);

            var url = Url.Action("GetById", new { id = interaction.Id });
            var responseViewModel = new InteractionResponse(interaction);
            return Created(url, responseViewModel);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!UserPasswordHelper.ReadAuthorizationHeaderAndValidateLogin(Request.Headers["Authorization"], _users))
                return BadRequest("Invalid login.");
            _interactions.Delete(id);
            return Ok();
        }
    }
}
