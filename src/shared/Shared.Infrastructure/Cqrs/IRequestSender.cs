using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

public interface IRequestSender
{
    Task<TResult> SendAsync<TResult>(IRequest<TResult> command, CancellationToken cancellationToken = default);

    Task SendAsync(Expression command, CancellationToken cancellationToken = default);
}