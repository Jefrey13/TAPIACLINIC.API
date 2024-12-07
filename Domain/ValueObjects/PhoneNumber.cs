using System;

namespace Domain.ValueObjects;
public class PhoneNumber
{
    public string Value { get; private set; }

    public PhoneNumber(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("Phone number is required.");
        }
        Value = value;
    }

    public override bool Equals(object obj)
    {
        return obj is PhoneNumber phoneNumber && Value == phoneNumber.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}