using CustomLoggerProvider.Models;
using CustomLoggerProvider.Repositories;
using Microsoft.Extensions.Configuration;

namespace CustomLoggerProvider.Logging
{
    public abstract class LoggerRepository : AbstractRepository<EventLog>
    {
        public LoggerRepository(IConfiguration configuration) : base(configuration){}
    }
}