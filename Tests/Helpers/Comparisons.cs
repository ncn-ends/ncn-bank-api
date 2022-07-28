using System.Diagnostics;
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

    public static bool AreRoughlyEqual<T>(T a, T b, int maxMismatches = 1)
    {
        PropertyInfo[] properties = typeof(T).GetProperties();

        int mismatches = 0;
        foreach (PropertyInfo prop in properties)
        {
            var valA = a.GetType().GetProperty(prop.Name)?.GetValue(a);
            var valB = a.GetType().GetProperty(prop.Name)?.GetValue(b);
            Debugger.Break();
            if (!string.Equals(valA, valB))
            {
                if (mismatches >= maxMismatches) return false;
                mismatches++;
            }
        }

        return true;
    }
}