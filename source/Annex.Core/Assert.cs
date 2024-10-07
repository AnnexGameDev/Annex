namespace Annex.Core;

public static class Assert
{
    public static void IsNotNull(object? instance, string message = "") {
        if (instance is null)
        {
            throw new AssertionFailedException(message);
        }
    }

    public static void IsNull(object? instance, string message = "") {
        if (instance is not null)
        {
            throw new AssertionFailedException(message);
        }
    }

    public static T IsOfType<T>(object instance, string message = "") {
        if (instance is not T t)
        {
            throw new AssertionFailedException(message);
        }
        return t;
    }
}
