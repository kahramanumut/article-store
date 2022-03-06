using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Article.Application.Features.Articles.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Article.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IRequestSender _requestSender;
        public ArticleController(IRequestSender mediator) 
        {
            _requestSender = mediator;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var articles = await _requestSender.SendAsync(new GetAllArticlesQuery());
            return StatusCode((int)HttpStatusCode.OK, new Response<IEnumerable<ArticleDto>>(articles, true, "All articles have been listed")); 
                //Ok(articles);
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<IActionResult> Get(Guid id)
        { 
            var query = new GetArticleByIdQuery { Id = id };
            var article = await _requestSender.SendAsync(query);

            return StatusCode((int)HttpStatusCode.OK, new Response<ArticleDto>(article, true, "article has been listed"));
            //return Ok(article);
        }
        
        [HttpPost]
        public async Task<ActionResult<Response<ArticleDto>>> Create(CreateArticleCommand command)
        {
            var result = await _requestSender.SendAsync(command);
            return StatusCode((int)HttpStatusCode.Created ,new Response<ArticleDto>(result, true, "New article created successfully"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response<ArticleDto>>> Update(UpdateArticleCommand command, Guid id)
        {
            command.SetId(id);

            var result = await _requestSender.SendAsync(command);
            return StatusCode((int)HttpStatusCode.OK ,new Response<ArticleDto>(result, true, "Article updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(Guid id)
        {
            DeleteArticleCommand command = new DeleteArticleCommand { ArticleId = id };

            var result = await _requestSender.SendAsync(command);
            return StatusCode((int)HttpStatusCode.OK ,new Response(result, "Article deleted successfully"));
        }

        [HttpPost("increment-star-count/{id}")]
        public async Task<ActionResult<Response>> IncrementStartCount(Guid id)
        {
            IncrementStarCommand command = new IncrementStarCommand { ArticleId = id };

            var result = await _requestSender.SendAsync(command);
            return StatusCode((int)HttpStatusCode.OK ,new Response(true, "Article star count incremented"));
        }
    }
}
