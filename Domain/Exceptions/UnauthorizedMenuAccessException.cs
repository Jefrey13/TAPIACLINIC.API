using System;

namespace Domain.Exceptions;
public class UnauthorizedMenuAccessException : Exception
{
    public UnauthorizedMenuAccessException(string message) : base(message)
    {
    }
}