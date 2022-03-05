using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

public interface IQuerySender
{
    Task<TResult> SendAsync<TResult>(IQuery<TResult> Query, CancellationToken cancellationToken = default);

    Task SendAsync(Expression Query, CancellationToken cancellationToken = default);
}