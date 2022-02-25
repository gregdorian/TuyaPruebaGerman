using System;
using TuyaPruebaGerman.Application.Interfaces;

namespace TuyaPruebaGerman.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}