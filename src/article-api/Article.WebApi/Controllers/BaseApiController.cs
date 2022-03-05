using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Article.WebApi.Controllers
{
    /// <summary>
    /// Abstract BaseApi Controller Class
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController<T> : ControllerBase
    {
        private ICommandSender _commandSenderInstance;
        private IQuerySender _querySenderInstance;
        private ILogger<T> _loggerInstance;
        protected ICommandSender _commandSender => _commandSenderInstance ??= HttpContext.RequestServices.GetService<ICommandSender>();
        protected IQuerySender _querySender => _querySenderInstance ??= HttpContext.RequestServices.GetService<IQuerySender>();
        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
    }
}
