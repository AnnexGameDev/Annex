using Annex.Data.Shared;
using NUnit.Framework;

namespace Tests.Data.Shared
{
    public class OffsetScalingVectorTests
    {
        [Test]
        public void Assignment_X_ModifiesOffset_AndNotOriginalOrScale() {
            float scaleX = 6;
            float offsetX = 2;
            float originalX = 4;
            Vector offset = Vector.Create(offsetX, 3);
            Vector original = Vector.Create(originalX, 5);
            Vector scale = Vector.Create(scaleX, 7);
            float newOffsetX = 8;
            float expectedX = newOffsetX + (scaleX * originalX);
            var source = new OffsetScalingVector(original, offset, scale);

            source.X = newOffsetX;

            Assert.AreEqual(source.Offset.X, newOffsetX);
            Assert.AreEqual(source.Scale.X, scale.X);
            Assert.AreEqual(source.Original.X, originalX);
            Assert.AreEqual(source.X, expectedX);
        }

        [Test]
        public void Assignment_Y_ModifiesOffset_AndNotOriginalOrScale() {
            float scaleY = 7;
            float offsetY = 3;
            float originalY = 5;
            Vector offset = Vector.Create(2, offsetY);
            Vector original = Vector.Create(4, originalY);
            Vector scale = Vector.Create(6, scaleY);
            float newOffsetY = 8;
            float expectedY = newOffsetY + (scaleY * originalY);
            var source = new OffsetScalingVector(original, offset, scale);

            source.Y = newOffsetY;

            Assert.AreEqual(source.Offset.Y, newOffsetY);
            Assert.AreEqual(source.Scale.Y, scale.Y);
            Assert.AreEqual(source.Original.Y, originalY);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void Constructor_SharedVector_SharedVector_SharedVector() {
            float offsetX = 2;
            float offsetY = 3;
            float originalX = 4;
            float originalY = 5;
            float scaleX = 6;
            float scaleY = 7;
            float expectedX = offsetX + (scaleX * originalX);
            float expectedY = offsetY + (scaleY * originalY);

            Vector offset = Vector.Create(offsetX, offsetY);
            Vector original = Vector.Create(originalX, originalY);
            Vector scale = Vector.Create(scaleX, scaleY);

            OffsetScalingVector source = new OffsetScalingVector(original, offset, scale);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void Constructor_SharedVector_SharedVector_Float_Float() {
            float offsetX = 2;
            float offsetY = 3;
            float originalX = 4;
            float originalY = 5;
            float scaleX = 6;
            float scaleY = 7;
            float expectedX = offsetX + (scaleX * originalX);
            float expectedY = offsetY + (scaleY * originalY);

            Vector scale = Vector.Create(scaleX, scaleY);
            Vector original = Vector.Create(originalX, originalY);

            OffsetScalingVector source = new OffsetScalingVector(original, scale, offsetX, offsetY);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void OffsetScalingVector_NestedOriginal() {
            float offsetX = 2;
            float offsetY = 3;
            float originalX = 4;
            float originalY = 5;
            float scaleX = 6;
            float scaleY = 7;
            float expectedX = offsetX + (scaleX * (offsetX + (scaleX * originalX)));
            float expectedY = offsetY + (scaleY * (offsetY + (scaleY * originalY)));

            Vector scale = Vector.Create(scaleX, scaleY);
            Vector offset = Vector.Create(offsetX, offsetY);
            Vector original = Vector.Create(originalX, originalY);

            Vector nestedOriginal = new OffsetScalingVector(original, offset, scale);

            OffsetScalingVector source = new OffsetScalingVector(nestedOriginal, offset, scale);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void OffsetScalingVector_NestedOffset() {
            float offsetX = 2;
            float offsetY = 3;
            float originalX = 4;
            float originalY = 5;
            float scaleX = 6;
            float scaleY = 7;
            float expectedX = (offsetX + (scaleX * originalX)) + (scaleX * originalX);
            float expectedY = (offsetY + (scaleY * originalY)) + (scaleY * originalY);

            Vector scale = Vector.Create(scaleX, scaleY);
            Vector offset = Vector.Create(offsetX, offsetY);
            Vector original = Vector.Create(originalX, originalY);

            Vector nestedOffset = new OffsetScalingVector(original, offset, scale);

            OffsetScalingVector source = new OffsetScalingVector(original, nestedOffset, scale);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }

        [Test]
        public void OffsetScalingVector_NestedScale() {
            float offsetX = 2;
            float offsetY = 3;
            float originalX = 4;
            float originalY = 5;
            float scaleX = 6;
            float scaleY = 7;
            float expectedX = offsetX + ((offsetX + (scaleX * originalX)) * originalX);
            float expectedY = offsetY + ((offsetY + (scaleY * originalY)) * originalY);

            Vector scale = Vector.Create(scaleX, scaleY);
            Vector offset = Vector.Create(offsetX, offsetY);
            Vector original = Vector.Create(originalX, originalY);

            Vector nestedScale = new OffsetScalingVector(original, offset, scale);

            OffsetScalingVector source = new OffsetScalingVector(original, offset, nestedScale);

            Assert.AreEqual(source.X, expectedX);
            Assert.AreEqual(source.Y, expectedY);
        }
    }
}
