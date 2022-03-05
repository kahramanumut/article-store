using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR; 

public class QuerySender : IQuerySender
{
    private readonly IMediator _mediator;

    public QuerySender(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<TResult> SendAsync<TResult>(IQuery<TResult> Query, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(Query, cancellationToken);
    }

    public Task SendAsync(Expression Query, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(Query, cancellationToken);
    }
}