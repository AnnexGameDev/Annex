using Annex.Data.Shared;
using NUnit.Framework;

namespace Tests.Data.Shared
{
    public class IntTests
    {
        [Test]
        public void Constructor_Default_ValueIsZero() {
            Int source = new Int();
            Assert.AreEqual(source.Value, 0);
        }

        [Test]
        public void Constructor_Int32_ValueIsAssigned() {
            int initial = 1;
            int expected = initial;
            Int source = new Int(initial);
            Assert.AreEqual(source.Value, expected);
        }

        [Test]
        public void Constructor_Copy_ValueIsAssigned() {
            int initial = 3;
            Int source = initial;
            Int copy = new Int(source);
            Assert.AreEqual(source.Value, copy.Value);
        }

        [Test]
        public void Constructor_Copy_SourceDoesNotUpdateCopy_ThroughSourceValueAssignment() {
            int initial = 3;
            int expected = 4;
            Int source = initial;
            Int copy = new Int(source);

            source.Value = expected;

            Assert.AreEqual(source.Value, expected);
            Assert.AreEqual(copy.Value, initial);
            Assert.AreNotEqual(source.Value, copy.Value);
        }

        [Test]
        public void Constructor_Copy_CopyDoesNotUpdateSource_ThroughCopyValueAssignment() {
            int initial = 3;
            int expected = 4;
            Int source = initial;
            Int copy = new Int(source);

            copy.Value = expected;

            Assert.AreEqual(source.Value, initial);
            Assert.AreEqual(copy.Value, expected);
            Assert.AreNotEqual(source.Value, copy.Value);
        }

        [Test]
        public void Constructor_Copy_SourceDoesNotUpdateCopy_ThroughSourceSet() {
            int initial = 3;
            int expected = 4;
            Int source = initial;
            Int copy = new Int(source);

            source.Set(expected);

            Assert.AreEqual(source.Value, expected);
            Assert.AreEqual(copy.Value, initial);
            Assert.AreNotEqual(source.Value, copy.Value);
        }

        [Test]
        public void Constructor_Copy_CopyDoesNotUpdateSource_ThroughCopySet() {
            int initial = 3;
            int expected = 4;
            Int source = initial;
            Int copy = new Int(source);

            copy.Set(expected);

            Assert.AreEqual(source.Value, initial);
            Assert.AreEqual(copy.Value, expected);
            Assert.AreNotEqual(source.Value, copy.Value);
        }

        [Test]
        public void Assignment_Value_ValueChanges() {
            int initial = 1;
            int expected = 2;
            Int source = initial;

            source.Value = expected;

            Assert.AreEqual(source.Value, expected);
        }

        [Test]
        public void Assignment_SharedInt_SourceEqualsCopy() {
            int initial = 1;
            int expected = initial;
            Int source = initial;
            Int copy = source;

            Assert.AreEqual(source.Value, copy.Value);
            Assert.AreEqual(copy.Value, expected);
            Assert.AreEqual(source.Value, expected);
        }

        [Test]
        public void Assignment_SharedInt_SourceEqualsCopy_AfterSourceValueAssignment() {
            int initial = 1;
            int expected = 2;

            Int source = initial;
            Int copy = source;

            source.Value = expected;

            Assert.AreEqual(source.Value, copy.Value);
            Assert.AreEqual(source.Value, expected);
            Assert.AreEqual(copy.Value, expected);
        }

        [Test]
        public void Assignment_SharedInt_SourceEqualsCopy_AfterCopyValueAssignment() {
            int initial = 1;
            int expected = 2;

            Int source = initial;
            Int copy = source;

            copy.Value = expected;

            Assert.AreEqual(source.Value, copy.Value);
            Assert.AreEqual(source.Value, expected);
            Assert.AreEqual(copy.Value, expected);
        }

        [Test]
        public void Assignment_SharedInt_SourceEqualsCopy_AfterSourceSet() {
            int initial = 1;
            int expected = 2;

            Int source = initial;
            Int copy = source;

            source.Set(expected);

            Assert.AreEqual(source.Value, copy.Value);
            Assert.AreEqual(source.Value, expected);
            Assert.AreEqual(copy.Value, expected);
        }

        [Test]
        public void Assignment_SharedInt_SourceEqualsCopy_AfterCopySet() {
            int initial = 1;
            int expected = 2;

            Int source = initial;
            Int copy = source;

            copy.Set(expected);

            Assert.AreEqual(source.Value, copy.Value);
            Assert.AreEqual(source.Value, expected);
            Assert.AreEqual(copy.Value, expected);
        }

        [Test]
        public void Set_ValueChanges() {
            int initial = 1;
            int expected = 2;
            Int source = initial;

            source.Set(expected);

            Assert.AreEqual(source.Value, expected);
        }

        [Test]
        public void Operator_ImplicitCast_SharedIntToInt32() {
            int expected = 1;
            Int source = new Int(expected);

            int copy = source;

            Assert.AreEqual(copy, expected);
        }

        [Test]
        public void Operator_ImplicitCast_Int32ToSharedInt() {
            int initial = 1;

            Int source = initial;

            Assert.AreEqual(source.Value, initial);
        }
    }
}
