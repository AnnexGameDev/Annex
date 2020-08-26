using System;

namespace Annex
{
    public class AssertionFailedException : Exception
    {
        public AssertionFailedException(string? message) : base(message) {

        }
    }
}
