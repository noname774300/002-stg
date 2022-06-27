#nullable enable

// https://qiita.com/up-hash/items/91a4ea63ccfa7a1323c5#comment-171affec2cc167759766
using System;

public static class UnityNullConverter
{
    public static void IfNotNull<T>(this T obj, Action<T> action)
    {
        if (obj == null)
        {
            return;
        }
        action(obj);
    }

    public static TResult IfNotNull<T, TResult>(this T obj, Func<T, TResult> func, TResult defaultValue)
      => (obj != null) ? func(obj) : defaultValue;
}
