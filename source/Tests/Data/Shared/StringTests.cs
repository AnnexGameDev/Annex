using Annex.Data.Shared;
using NUnit.Framework;

namespace Tests.Data.Shared
{
    public class StringTests
    {
        [Test]
        public void Constructor_Default_ValueIsNull() {
            String source = new String();
            Assert.AreEqual(source.Value, null);
        }

        [Test]
        public void Constructor_String_ValueIsAssigned() {
            string initial = "Foo";
            string expected = initial;

            String source = new String(initial);

            Assert.AreEqual(source.Value, expected);
        }

        [Test]
        public void Constructor_Copy_ValueIsAssigned() {
            string initial = "Foo";
            string expected = initial;
            String source = initial;

            String copy = new String(source);

            Assert.AreEqual(source.Value, copy.Value);
        }

        [Test]
        public void Constructor_Copy_CopyDoesNotUpdateSource_ThroughCopySet() {
            string initial = "Foo";
            string expected = "Bar";
            String source = initial;
            String copy = new String(source);

            copy.Set(expected);

            Assert.AreEqual(copy.Value, expected);
            Assert.AreEqual(source.Value, initial);
            Assert.AreNotEqual(source.Value, copy.Value);
        }

        [Test]
        public void Constructor_Copy_SourceDoesNotUpdateCopy_ThroughSourceSet() {
            string initial = "Foo";
            string expected = "Bar";
            String source = initial;
            String copy = new String(source);

            source.Set(expected);

            Assert.AreEqual(source.Value, expected);
            Assert.AreEqual(copy.Value, initial);
            Assert.AreNotEqual(source.Value, copy.Value);
        }

        [Test]
        public void Operator_ImplicitCast_StringToSharedString_NullCase() {
            string? source = null;

            String copy = source;

            Assert.AreNotEqual(copy, null);
            Assert.AreEqual(copy.Value, source);
        }

        [Test]
        public void Operator_ImplicitCast_StringToSharedString() {
            string source = "Foo";

            String copy = source;

            Assert.AreEqual(source, copy.Value);
        }

        [Test]
        public void Operator_Implicit_SharedStringToString() {
            string expected = "Foo";
            String source = new String(expected);

            string? copy = source;

            Assert.AreEqual(copy, expected);
        }

        [Test]
        public void Operator_Implicit_SharedStringToString_NullCase() {
            string? expected = null;
            String source = new String(expected);

            string? copy = source;

            Assert.AreEqual(copy, expected);
        }

        [Test]
        public void Assignment_SharedString_SourceEqualsCopy() {
            string expected = "Foo";
            String source = expected;
            String copy = source;

            Assert.AreEqual(source.Value, expected);
            Assert.AreEqual(copy.Value, expected);
            Assert.AreEqual(copy.Value, source.Value);
        }

        [Test]
        public void Assignment_SharedString_SourceEqualsCopy_AfterCopySet() {
            string initial = "Foo";
            string expected = "Bar";
            String source = initial;
            String copy = source;

            copy.Set(expected);

            Assert.AreEqual(source.Value, expected);
            Assert.AreEqual(copy.Value, expected);
            Assert.AreEqual(copy.Value, source.Value);
        }
    }
}
