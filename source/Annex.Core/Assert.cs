namespace Annex.Core;

public static class Assert
{
    public static T IsOfType<T>(object instance, string message = "") {
        if (instance is not T t)
        {
            throw new AssertionFailedException(message);
        }
        return t;
    }
}
