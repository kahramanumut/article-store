using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR; 

public class CommandSender : ICommandSender
{
    private readonly IMediator _mediator;

    public CommandSender(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(command, cancellationToken);
    }

    public Task SendAsync(Expression command, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(command, cancellationToken);
    }
}