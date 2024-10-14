using System;

namespace Domain.ValueObjects;
public class PhoneNumber
{
    public string Value { get; private set; }

    public PhoneNumber(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length != 8)
        {
            throw new ArgumentException("Phone number must be 8 digits long.");
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