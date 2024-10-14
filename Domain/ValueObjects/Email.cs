using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects;
public class Email
{
    public string Value { get; private set; }

    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

    public Email(string value)
    {
        if (string.IsNullOrEmpty(value) || !EmailRegex.IsMatch(value))
        {
            throw new ArgumentException("Invalid email format.");
        }
        Value = value;
    }

    public override bool Equals(object obj)
    {
        return obj is Email email && Value == email.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}