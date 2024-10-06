namespace Annex.Core;

[Serializable]
internal class AssertionFailedException : Exception
{
    public AssertionFailedException(string? message) : base("Assertion failed: " + message) {
    }
}