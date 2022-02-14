using MediatR;

public interface ICommand<out TResult> : IRequest<TResult>
{
}