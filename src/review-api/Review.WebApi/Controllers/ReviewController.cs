using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace Review.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly ICommandSender _commandSender;
        private readonly ReviewDbContext _dbContext;
        public ReviewController(ICommandSender mediator, ReviewDbContext dbContext)
        {
            _commandSender = mediator;
            _dbContext = dbContext;
        }

        [HttpGet]
        [EnableQuery]
        public IEnumerable<Domain.Review.Review> Get()
        {
            return _dbContext.Reviews;
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public ActionResult<Domain.Review.Review> Get(Guid id)
        {
            return Ok(_dbContext.Reviews.Where(x => x.Id == id));
        }

        [HttpPost]
        public async Task<ActionResult<Response<ReviewDto>>> Create(CreateReviewCommand command)
        {
            var result = await _commandSender.SendAsync(command);
            return StatusCode((int)HttpStatusCode.Created, new Response<ReviewDto>(result, true, "New review added successfully"));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response<ReviewDto>>> Update(UpdateReviewCommand command, Guid id)
        {
            command.SetId(id);

            var result = await _commandSender.SendAsync(command);
            return StatusCode((int)HttpStatusCode.OK ,new Response<ReviewDto>(result, true, "Review updated successfully"));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(Guid id)
        {
            DeleteReviewCommand command = new DeleteReviewCommand { ReviewId = id };

            var result = await _commandSender.SendAsync(command);
            return StatusCode((int)HttpStatusCode.OK ,new Response(result, "Review deleted successfully"));
        }
    }
}
