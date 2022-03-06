using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR; 

public class RequestSender : IRequestSender
{
    private readonly IMediator _mediator;

    public RequestSender(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<TResult> SendAsync<TResult>(IRequest<TResult> command, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(command, cancellationToken);
    }

    public Task SendAsync(Expression command, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(command, cancellationToken);
    }
}