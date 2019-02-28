using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CustomLoggerProvider.Logging
{
    public static class SqliteLoggerExtensions 
    {
        public static ILoggingBuilder AddSqliteProvider<T>(this ILoggingBuilder builder, T repository) where T: LoggerRepository
        {
            builder.Services.AddSingleton<ILoggerProvider, SqliteLoggerProvider<T>>(p => new SqliteLoggerProvider<T>((_, logLevel) => logLevel >= LogLevel.Debug, repository));

            return builder;
        }
    }
}