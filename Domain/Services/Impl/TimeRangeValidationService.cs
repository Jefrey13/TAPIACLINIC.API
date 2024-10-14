using Domain.ValueObjects;

/// <summary>
/// Service that checks if two time ranges overlap. This can be used to ensure that
/// appointments or schedules do not conflict with each other.
/// </summary>
namespace Domain.Services.Impl;
public class TimeRangeValidationService : ITimeRangeValidationService
{
    /// <summary>
    /// Checks if two time ranges overlap.
    /// </summary>
    /// <param name="existingRange">The existing time range.</param>
    /// <param name="newRange">The new time range to check for overlap.</param>
    /// <returns>True if the time ranges overlap; otherwise, false.</returns>
    public bool HasConflict(DateRange existingRange, DateRange newRange)
    {
        return existingRange.Overlaps(newRange);
    }
}