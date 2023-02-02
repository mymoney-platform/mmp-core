namespace MMP.Core.Shared;

public struct Optional<T> : IEquatable<Optional<T>>
    where T : class
{
    private readonly T _value;
    public T Value
    {
        get
        {
            if (HasNoValue)
                throw new InvalidOperationException();

            return _value;
        }
    }

    public bool HasValue => _value != null;
    public bool HasNoValue => !HasValue;

    private Optional(T value)
    {
        _value = value;
    }

    public static implicit operator Optional<T>(T value)
    {
        return new Optional<T>(value);
    }

    public static bool operator ==(Optional<T> maybe, T value)
    {
        if (maybe.HasNoValue)
            return false;

        return maybe.Value.Equals(value);
    }

    public static bool operator !=(Optional<T> maybe, T value)
    {
        return !(maybe == value);
    }

    public static bool operator ==(Optional<T> first, Optional<T> second)
    {
        return first.Equals(second);
    }

    public static bool operator !=(Optional<T> first, Optional<T> second)
    {
        return !(first == second);
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Optional<T>))
            return false;

        var other = (Optional<T>)obj;
        return Equals(other);
    }

    public bool Equals(Optional<T> other)
    {
        if (HasNoValue && other.HasNoValue)
            return true;

        if (HasNoValue || other.HasNoValue)
            return false;

        return _value.Equals(other._value);
    }

    public override int GetHashCode()
    {
        return _value.GetHashCode();
    }

    public override string ToString()
    {
        if (HasNoValue)
            return "No value";

        return Value.ToString();
    }

    public T Unwrap()
    {
        if (HasValue)
            return Value;

        return default(T);
    }

    public K Unwrap<K>(Func<T, K> selector)
    {
        if (HasValue)
            return selector(Value);

        return default(K);
    }
}