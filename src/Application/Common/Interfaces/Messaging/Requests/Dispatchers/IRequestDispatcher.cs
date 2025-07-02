using Application.Common.Interfaces.Messaging.Requests.Base;

namespace Application.Common.Interfaces.Messaging.Requests.Dispatchers;

public interface IRequestDispatcher
{
    Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
}