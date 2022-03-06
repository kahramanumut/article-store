using MediatR;

public interface IRequest<out TResult> : MediatR.IRequest<TResult>
{
}