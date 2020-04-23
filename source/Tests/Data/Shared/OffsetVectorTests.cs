using Annex.Data.Shared;
using NUnit.Framework;

namespace Tests.Data.Shared
{
    public class OffsetVectorTests
    {
        [Test]
        public void Assignment_X_ModifiesOffset_AndNotOriginal() {
            float originalX = 10;
            float originalY = 20;
            float offsetX = 30;
            float offsetY = 40;
            float newOffsetX = 50;
            float expected = originalX + newOffsetX;
            var original = Vector.Create(originalX, originalY);
            var offset = Vector.Create(offsetX, offsetY);
            var source = new OffsetVector(original, offset);

            source.X = newOffsetX;

            Assert.AreEqual(source.Offset.X, newOffsetX);
            Assert.AreEqual(source.X, expected);
            Assert.AreEqual(source.Original.X, originalX);
        }

        [Test]
        public void Assignment_Y_ModifiesScale_AndNotOriginal() {
            float originalX = 10;
            float originalY = 20;
            float offsetX = 30;
            float offsetY = 40;
            float newOffsetY = 50;
            float expected = originalY + newOffsetY;
            var original = Vector.Create(originalX, originalY);
            var offset = Vector.Create(offsetX, offsetY);
            var source = new OffsetVector(original, offset);

            source.Y = newOffsetY;

            Assert.AreEqual(source.Offset.Y, newOffsetY);
            Assert.AreEqual(source.Y, expected);
            Assert.AreEqual(source.Original.Y, originalY);
        }

        [Test]
        public void Constructor_SharedVector_SharedVector() {
            float originalX = 10;
            float originalY = 20;
            float offsetX = 30;
            float offsetY = 40;
            float expectedX = originalX + offsetX;
            float expectedY = originalY + offsetY;
            var original = Vector.Create(originalX, originalY);
            var offset = Vector.Create(offsetX, offsetY);
            var source = new OffsetVector(original, offset);

            Assert.AreEqual(source.Offset.X, offset.X);
            Assert.AreEqual(source.Offset.Y, offset.Y);
            Assert.AreEqual(source.Original.X, originalX);
            Assert.AreEqual(source.Original.Y, originalY);
            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void OffsetVector_NestedOriginal() {
            float originalX = 10;
            float originalY = 20;
            float offsetX = 30;
            float offsetY = 40;
            float expectedX = originalX + offsetX + offsetX;
            float expectedY = originalY + offsetY + offsetY;

            var original = Vector.Create(originalX, originalY);
            var offset = Vector.Create(offsetX, offsetY);

            OffsetVector nestedOriginal = new OffsetVector(original, offset);
            OffsetVector source = new OffsetVector(nestedOriginal, offset);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void OffsetVector_NestedOffset() {
            float originalX = 10;
            float originalY = 20;
            float offsetX = 30;
            float offsetY = 40;
            float expectedX = originalX + originalX + offsetX;
            float expectedY = originalY + originalY + offsetY;

            var original = Vector.Create(originalX, originalY);
            var offset = Vector.Create(offsetX, offsetY);

            OffsetVector nestedOffset = new OffsetVector(original, offset);
            OffsetVector source = new OffsetVector(original, nestedOffset);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }
    }
}
