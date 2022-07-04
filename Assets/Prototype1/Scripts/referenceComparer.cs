using System.Collections.Generic;

// This class included in a later version of .NET but is not supported in Unity yet
// https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.referenceequalitycomparer

public class ReferenceEqualityComparer<T> : IEqualityComparer<T>
{
    public bool Equals(T x, T y) {return ReferenceEquals(x, y);}
    public int GetHashCode(T obj) {return obj.GetHashCode();}
}