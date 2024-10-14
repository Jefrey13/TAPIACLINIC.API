using System;

namespace Domain.Exceptions;
public class SurgeryAlreadyScheduledException : Exception
{
    public SurgeryAlreadyScheduledException(string message) : base(message)
    {
    }
}