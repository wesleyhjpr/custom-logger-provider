using System;
using Microsoft.Extensions.Logging;

namespace CustomLoggerProvider.Logging
{
    public class SqliteLoggerProvider<T>: ILoggerProvider where T : LoggerRepository
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly T _repository;

        public SqliteLoggerProvider(Func<string, LogLevel, bool> filter, T repository)
        {
            this._filter = filter;
            this._repository = repository;
        }

        public ILogger CreateLogger(string categoryName) => new SqliteLogger<T>(_filter, _repository, categoryName);

        public void Dispose() {}
    }
}