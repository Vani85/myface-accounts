using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFace.Helpers;
using MyFace.Repositories;

namespace MyFace.Controllers
{
    [Authorize]
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly IUsersRepo _users;

        public HomeController(IUsersRepo users)
        {
            _users = users;
        }
        
        [HttpGet("")]
        public ActionResult<Dictionary<string, string>> Endpoints()
        {           
            return new Dictionary<string, string>
            {
                {"/users", "for information on users."},
                {"/posts", "for information on posts."},
                {"/interactions", "for information about the interactions between users and posts"},
                {"/feed", "all the data required to build a 'feed' of posts."},
            };
        }
    }
}
