using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensionmethods
{
    /// <summary>
    /// Checks whether a value is inside an array.
    /// </summary>
    /// <returns>Returns true if the value is in the array.</returns>
    public static bool ArrayContains(this object[] array, object val)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == val)
                return true;
        }
        return false;
    }
}
