namespace Domain.ValueObjects;
public class DateRange
{
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }

    public DateRange(DateTime start, DateTime end)
    {
        if (start >= end)
        {
            throw new ArgumentException("Start date must be earlier than end date.");
        }
        Start = start;
        End = end;
    }

    public bool Overlaps(DateRange other)
    {
        return Start < other.End && End > other.Start;
    }
}