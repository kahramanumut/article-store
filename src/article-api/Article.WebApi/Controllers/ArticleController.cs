using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Article.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ICommandSender _commandSender;
        private readonly ArticleDbContext _dbContext;
        public ArticleController(ICommandSender mediator, ArticleDbContext dbContext) 
        {
            _commandSender = mediator;
            _dbContext = dbContext;
        }

        [HttpGet]
        [EnableQuery]
        public IEnumerable<Domain.Article.Article> Get()
        {
            return _dbContext.Articles;
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public IActionResult Get(Guid id)
        {
            return Ok(_dbContext.Articles.Where(x => x.Id == id));
        }
        
        [HttpPost]
        public async Task<ActionResult<Response<ArticleDto>>> Create(CreateArticleCommand command)
        {
            var result = await _commandSender.SendAsync(command);
            return StatusCode((int)HttpStatusCode.Created ,new Response<ArticleDto>(result, true, "New article created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response<ArticleDto>>> Update(UpdateArticleCommand command, Guid id)
        {
            command.SetId(id);

            var result = await _commandSender.SendAsync(command);
            return StatusCode((int)HttpStatusCode.OK ,new Response<ArticleDto>(result, true, "Article updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(Guid id)
        {
            DeleteArticleCommand command = new DeleteArticleCommand { ArticleId = id };

            var result = await _commandSender.SendAsync(command);
            return StatusCode((int)HttpStatusCode.OK ,new Response(result, "Article deleted successfully"));
        }

        [HttpPost("increment-star-count/{id}")]
        public async Task<ActionResult<Response>> IncrementStartCount(Guid id)
        {
            IncrementStarCommand command = new IncrementStarCommand { ArticleId = id };

            var result = await _commandSender.SendAsync(command);
            return StatusCode((int)HttpStatusCode.OK ,new Response(true, "Article star count incremented"));
        }
    }
}
