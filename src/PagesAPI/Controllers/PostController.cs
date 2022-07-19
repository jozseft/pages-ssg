using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagesCommon.DTOs;
using PagesServices.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PagesAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: api/<PostController>
        [HttpGet]
        public IEnumerable<PostListItemDTO> Get()
        {
            return _postService.GetAllPosts();
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public PostDTO Get(Guid id)
        {
            return _postService.GetPost(id); 
        }

        // POST api/<PostController>
        [HttpPost]
        public Guid Post([FromBody] PostDTO value)
        {
            return _postService.SavePost(value);
        }

        // POST api/<PostController>
        [HttpPost("Publish/{id}")]
        public bool PublishPost([FromRoute] Guid id)
        {
            return _postService.PublishPost(id);
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
