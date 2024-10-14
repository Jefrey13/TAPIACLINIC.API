using System;

namespace Domain.Exceptions;
public class OverlappingAppointmentException : Exception
{
    public OverlappingAppointmentException(string message) : base(message)
    {
    }
}