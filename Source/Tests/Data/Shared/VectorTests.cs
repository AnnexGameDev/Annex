using Annex.Data.Shared;
using NUnit.Framework;
using SFML.System;

namespace Tests.Data.Shared
{
    public class VectorTests
    {
        [Test]
        public void Constructor_Default_ValueIsZeroZero() {
            Vector source = new Vector();

            Assert.AreEqual(source.X, 0);
            Assert.AreEqual(source.Y, 0);
        }

        [Test]
        public void Constructor_FloatFloat_ValueIsAssigned() {
            float expectedX = 10;
            float expectedY = 20;

            Vector source = new Vector(expectedX, expectedY);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void Operator_ImplicitCast_VectorToSFMLVector2f() {
            float expectedX = 10;
            float expectedY = 20;
            Vector source = new Vector(expectedX, expectedY);

            Vector2f copy = source;

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
            Assert.AreEqual(copy.X, expectedX);
            Assert.AreEqual(copy.Y, expectedY);

            Assert.AreEqual(source.X, copy.X);
            Assert.AreEqual(source.Y, copy.Y);
        }

        [Test]
        public void Set_ValueChanges() {
            float expectedX = 10;
            float expectedY = 20;
            Vector source = new Vector();

            source.Set(expectedX, expectedY);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void Assignment_X_ValueChanges() {
            float initialX = 0;
            float initialY = 0;
            float expectedX = 10;
            Vector source = new Vector(initialX, initialY);

            source.X = expectedX;

            Assert.AreEqual(source.X, expectedX);
        }

        [Test]
        public void Assignment_Y_ValueChanges() {
            float initialX = 0;
            float initialY = 0;
            float expectedY = 10;
            Vector source = new Vector(initialX, initialY);

            source.Y = expectedY;

            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void Add_ValueChanges() {
            float initialX = 0;
            float initialY = 0;
            float addX = 10;
            float addY = 20;
            float expectedX = initialX + addX;
            float expectedY = initialY + addY;
            Vector source = new Vector(initialX, initialY);

            source.Add(addX, addY);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void Assignment_SharedVector_SourceEqualsCopy() {
            float initialX = 10;
            float initialY = 20;
            float expectedX = initialX;
            float expectedY = initialY;
            Vector source = new Vector(initialX, initialY);
            Vector copy = source;

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
            Assert.AreEqual(source.X, copy.X);
            Assert.AreEqual(source.Y, copy.Y);
            Assert.AreEqual(copy.X, expectedX);
            Assert.AreEqual(copy.Y, expectedY);
        }

        [Test]
        public void Assignment_SharedVector_SourceEqualsCopy_AfterCopySet() {
            float initialX = 10;
            float initialY = 20;
            float expectedX = 20;
            float expectedY = 30;
            Vector source = new Vector(initialX, initialY);
            Vector copy = source;

            copy.Set(expectedX, expectedY);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
            Assert.AreEqual(source.X, copy.X);
            Assert.AreEqual(source.Y, copy.Y);
            Assert.AreEqual(copy.X, expectedX);
            Assert.AreEqual(copy.Y, expectedY);
        }

        [Test]
        public void Assignment_SharedVector_SourceEqualsCopy_AfterCopyValueAssignment() {
            float initialX = 10;
            float initialY = 20;
            float expectedX = 20;
            float expectedY = 30;
            Vector source = new Vector(initialX, initialY);
            Vector copy = source;

            copy.X = expectedX;
            copy.Y = expectedY;

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
            Assert.AreEqual(source.X, copy.X);
            Assert.AreEqual(source.Y, copy.Y);
            Assert.AreEqual(copy.X, expectedX);
            Assert.AreEqual(copy.Y, expectedY);
        }

        [Test]
        public void Assignment_SharedVector_SourceEqualsCopy_AfterSourceSet() {
            float initialX = 10;
            float initialY = 20;
            float expectedX = 20;
            float expectedY = 30;
            Vector source = new Vector(initialX, initialY);
            Vector copy = source;

            source.Set(expectedX, expectedY);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
            Assert.AreEqual(source.X, copy.X);
            Assert.AreEqual(source.Y, copy.Y);
            Assert.AreEqual(copy.X, expectedX);
            Assert.AreEqual(copy.Y, expectedY);
        }

        [Test]
        public void Assignment_SharedVector_SourceEqualsCopy_AfterSourceValueAssignment() {
            float initialX = 10;
            float initialY = 20;
            float expectedX = 20;
            float expectedY = 30;
            Vector source = new Vector(initialX, initialY);
            Vector copy = source;

            source.X = expectedX;
            source.Y = expectedY;

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
            Assert.AreEqual(source.X, copy.X);
            Assert.AreEqual(source.Y, copy.Y);
            Assert.AreEqual(copy.X, expectedX);
            Assert.AreEqual(copy.Y, expectedY);
        }
    }
}
