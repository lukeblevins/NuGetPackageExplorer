using Microsoft.Extensions.Logging;

namespace PackageExplorer.Framework.Query
{
    internal class LoggingHttpClientHandler : DelegatingHandler
	{
		private readonly Lazy<ILogger> _logger = new Lazy<ILogger>(typeof(LoggingHttpClientHandler).Log);

		public LoggingHttpClientHandler(HttpMessageHandler handler) => InnerHandler = handler;

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var response = default(HttpResponseMessage);

			try
			{
				if (_logger.Value.IsEnabled(LogLevel.Debug))
				{
					_logger.Value.LogDebug($"{request.Method.ToString().ToUpper()} ... {request.RequestUri}");
				}

				return response = await base.SendAsync(request, cancellationToken);
			}
			finally
			{
				if (_logger.Value.IsEnabled(LogLevel.Debug))
				{
					_logger.Value.LogDebug($"{request.Method.ToString().ToUpper()} {response?.StatusCode} {request.RequestUri} ");
				}
			}
		}
	}
}
