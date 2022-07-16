using System.Reflection;
using DataAccess.Models;
using FluentAssertions;

namespace Tests.Helpers;

public static class Comparisons
{
    public static bool AreEqual<T>(T a, T b)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        foreach (PropertyInfo prop in properties)
        {
            var valA = a.GetType().GetProperty(prop.Name)?.GetValue(a);
            var valB = a.GetType().GetProperty(prop.Name)?.GetValue(b);
            if (!string.Equals(valA, valB)) return false;
        }

        return true;
    }
}