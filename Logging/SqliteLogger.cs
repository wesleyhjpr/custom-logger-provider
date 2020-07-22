using System;
using System.Text;
using CustomLoggerProvider.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions.Internal;

namespace CustomLoggerProvider.Logging
{
    public class SqliteLogger<T> : ILogger where T: LoggerRepository
    {
        private Func<string, LogLevel, bool> _filter;
        private T _repository;
        private string _categoryName;
        private readonly int maxLength = 1024;
        private IExternalScopeProvider ScopeProvider { get; set; }

        public SqliteLogger(Func<string, LogLevel, bool> filter, T repository, string categoryName)
        {
            _filter = filter;
            _repository = repository;
            _categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state) => ScopeProvider?.Push(state) ?? NullScope.Instance;

        public bool IsEnabled(LogLevel logLevel) => (_filter == null || _filter(_categoryName, logLevel));

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!_categoryName.Contains(AppDomain.CurrentDomain.FriendlyName) && logLevel == LogLevel.Information)
                return;

            if (!IsEnabled(logLevel)) return; 
            
            if (formatter == null) throw new ArgumentNullException(nameof(formatter)); 
            
            var logBuilder = new StringBuilder();

            var message = formatter(state, exception);
            if (!string.IsNullOrEmpty(message)) 
            { 
                logBuilder.Append(message);
                logBuilder.Append(Environment.NewLine);
            }

            GetScope(logBuilder);
            
            if (exception != null) 
               message = logBuilder.Append(exception.ToString()).ToString(); 

            //if (logBuilder.Capacity > maxLength)
            //    logBuilder.Capacity = maxLength;

            var eventLog = new EventLog 
            { 
                Message = message, 
                EventId = eventId.Id,
                Category = _categoryName,
                LogLevel = logLevel.ToString(), 
                CreatedTime = DateTime.UtcNow 
            };

            _repository.Add(eventLog);
        }

        private void GetScope(StringBuilder stringBuilder)
        {
            var scopeProvider = ScopeProvider;
            if (scopeProvider != null)
            {
                var initialLength = stringBuilder.Length;

                scopeProvider.ForEachScope((scope, state) =>
                {
                    var (builder, length) = state;
                    var first = length == builder.Length;
                    builder.Append(first ? "=> " : " => ").Append(scope);
                }, (stringBuilder, initialLength));

                stringBuilder.AppendLine();
            }
        }
    }
}