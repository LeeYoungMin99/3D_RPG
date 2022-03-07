using System.Collections.Generic;

public class EnumComparer : IEqualityComparer<EStateTag>
{
    public bool Equals(EStateTag x, EStateTag y)
    {
        return x == y;
    }

    public int GetHashCode(EStateTag x)
    {
        return (int)x;
    }
}
